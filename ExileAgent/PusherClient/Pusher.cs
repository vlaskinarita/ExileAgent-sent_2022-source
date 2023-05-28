using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class Pusher : EventEmitter, IPusher, ITriggerChannels
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ConnectedEventHandler Connected;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ConnectedEventHandler Disconnected;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ConnectionStateChangedEventHandler ConnectionStateChanged;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ErrorEventHandler Error;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SubscribedEventHandler Subscribed;

		private static string Version { get; }

		public string SocketID
		{
			get
			{
				IConnection connection = this._connection;
				return (connection != null) ? connection.SocketId : null;
			}
		}

		public ConnectionState State
		{
			get
			{
				IConnection connection = this._connection;
				return (connection != null) ? connection.State : ConnectionState.Uninitialized;
			}
		}

		private ConcurrentDictionary<string, Channel> Channels { get; } = new ConcurrentDictionary<string, Channel>();

		private ConcurrentBag<Channel> Backlog { get; } = new ConcurrentBag<Channel>();

		internal PusherOptions Options { get; private set; }

		public Pusher(string applicationKey, PusherOptions options = null)
		{
			if (string.IsNullOrWhiteSpace(applicationKey))
			{
				throw new ArgumentException(Pusher.getString_1(107311672), Pusher.getString_1(107311095));
			}
			this._applicationKey = applicationKey;
			this.Options = (options ?? new PusherOptions());
			((IPusher)this).PusherOptions = this.Options;
			base.SetEventEmitterErrorHandler(new Action<PusherException>(this.InvokeErrorHandler));
		}

		PusherOptions IPusher.PusherOptions { get; set; }

		void IPusher.ChangeConnectionState(ConnectionState state)
		{
			if (state == ConnectionState.Connected)
			{
				Task.Run(delegate()
				{
					if (this.Connected != null)
					{
						try
						{
							this.Connected(this);
						}
						catch (Exception innerException)
						{
							this.InvokeErrorHandler(new ConnectedEventHandlerException(innerException));
						}
					}
					this.SubscribeExistingChannels();
					this.UnsubscribeBacklog();
					if (this.ConnectionStateChanged != null)
					{
						try
						{
							this.ConnectionStateChanged(this, state);
						}
						catch (Exception innerException2)
						{
							this.InvokeErrorHandler(new ConnectionStateChangedEventHandlerException(state, innerException2));
						}
					}
				});
			}
			else if (state == ConnectionState.Disconnected)
			{
				Task.Run(delegate()
				{
					this.MarkChannelsAsUnsubscribed();
					if (this.Disconnected != null)
					{
						try
						{
							this.Disconnected(this);
						}
						catch (Exception innerException)
						{
							this.InvokeErrorHandler(new DisconnectedEventHandlerException(innerException));
						}
					}
					if (this.ConnectionStateChanged != null)
					{
						try
						{
							this.ConnectionStateChanged(this, state);
						}
						catch (Exception innerException2)
						{
							this.InvokeErrorHandler(new ConnectionStateChangedEventHandlerException(state, innerException2));
						}
					}
				});
			}
			else if (this.ConnectionStateChanged != null)
			{
				Task.Run(delegate()
				{
					try
					{
						this.ConnectionStateChanged(this, state);
					}
					catch (Exception innerException)
					{
						this.InvokeErrorHandler(new ConnectionStateChangedEventHandlerException(state, innerException));
					}
				});
			}
		}

		void IPusher.ErrorOccured(PusherException pusherException)
		{
			this.RaiseError(pusherException, true);
		}

		IEventBinder IPusher.GetEventBinder(string eventBinderKey)
		{
			return base.GetEventBinder(eventBinderKey);
		}

		IEventBinder IPusher.GetChannelEventBinder(string eventBinderKey, string channelName)
		{
			IEventBinder result = null;
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				result = channel.GetEventBinder(eventBinderKey);
			}
			return result;
		}

		void IPusher.AddMember(string channelName, string member)
		{
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				IPresenceChannelManagement presenceChannelManagement = channel as IPresenceChannelManagement;
				if (presenceChannelManagement != null)
				{
					presenceChannelManagement.AddMember(member);
				}
			}
		}

		void IPusher.RemoveMember(string channelName, string member)
		{
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				IPresenceChannelManagement presenceChannelManagement = channel as IPresenceChannelManagement;
				if (presenceChannelManagement != null)
				{
					presenceChannelManagement.RemoveMember(member);
				}
			}
		}

		void IPusher.SubscriptionSuceeded(string channelName, string data)
		{
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				channel.SubscriptionSucceeded(data);
				if (this.Subscribed != null)
				{
					Task.Run(delegate()
					{
						try
						{
							this.Subscribed(this, channel);
						}
						catch (Exception innerException)
						{
							this.InvokeErrorHandler(new SubscribedEventHandlerException(channel, innerException, data));
						}
					});
				}
				if (channel._subscribeCompleted != null)
				{
					channel._subscribeCompleted.Release();
				}
			}
		}

		void IPusher.SubscriptionFailed(string channelName, string data)
		{
			ChannelException ex = new ChannelException(Pusher.getString_1(107312360) + channelName, ErrorCodes.SubscriptionError, channelName, this._connection.SocketId)
			{
				MessageData = data
			};
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				ex.Channel = channel;
				channel._subscriptionError = ex;
				SemaphoreSlim subscribeCompleted = channel._subscribeCompleted;
				if (subscribeCompleted != null)
				{
					subscribeCompleted.Release();
				}
			}
			this.RaiseError(ex, true);
		}

		byte[] IPusher.GetSharedSecret(string channelName)
		{
			byte[] result = null;
			Channel channel = this.GetChannel(channelName);
			PrivateChannel privateChannel;
			bool flag;
			if (channel != null)
			{
				privateChannel = (channel as PrivateChannel);
				flag = (privateChannel != null);
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				result = privateChannel.SharedSecret;
			}
			return result;
		}

		public async Task ConnectAsync()
		{
			if (this._connection == null || !this._connection.IsConnected)
			{
				try
				{
					TimeSpan timeoutPeriod = this.Options.ClientTimeout;
					bool flag = await this._connectLock.WaitAsync(timeoutPeriod).ConfigureAwait(false);
					if (!flag)
					{
						throw new OperationTimeoutException(timeoutPeriod, Pusher.<ConnectAsync>d__50.getString_0(107313220));
					}
					try
					{
						if (this._connection != null && this._connection.IsConnected)
						{
							return;
						}
						string url = this.ConstructUrl();
						this._connection = new Connection(this, url);
						this._disconnectLock = new SemaphoreSlim(1);
						await this._connection.ConnectAsync().ConfigureAwait(false);
						url = null;
					}
					finally
					{
						this._connectLock.Release();
					}
					timeoutPeriod = default(TimeSpan);
				}
				catch (Exception e)
				{
					this.HandleOperationException(ErrorCodes.ConnectError, Pusher.<ConnectAsync>d__50.getString_0(107310676), e);
					throw;
				}
			}
		}

		private string ConstructUrl()
		{
			string text = this.Options.Encrypted ? Pusher.getString_1(107311065) : Pusher.getString_1(107311042);
			return string.Concat(new string[]
			{
				text,
				this.Options.Host,
				Pusher.getString_1(107311024),
				this._applicationKey,
				Pusher.getString_1(107311015),
				Pusher.Version
			});
		}

		public async Task DisconnectAsync()
		{
			if (this._connection != null && this.State != ConnectionState.Disconnected)
			{
				try
				{
					TimeSpan timeoutPeriod = this.Options.ClientTimeout;
					bool flag = await this._disconnectLock.WaitAsync(timeoutPeriod).ConfigureAwait(false);
					if (!flag)
					{
						throw new OperationTimeoutException(timeoutPeriod, Pusher.<DisconnectAsync>d__52.getString_0(107310656));
					}
					try
					{
						if (this._connection != null && this.State != ConnectionState.Disconnected)
						{
							this._connectLock = new SemaphoreSlim(1);
							this.MarkChannelsAsUnsubscribed();
							await this._connection.DisconnectAsync().ConfigureAwait(false);
						}
					}
					finally
					{
						this._disconnectLock.Release();
					}
					timeoutPeriod = default(TimeSpan);
				}
				catch (Exception e)
				{
					this.HandleOperationException(ErrorCodes.DisconnectError, Pusher.<DisconnectAsync>d__52.getString_0(107310656), e);
					throw;
				}
			}
		}

		[DebuggerStepThrough]
		public Task<Channel> SubscribeAsync(string channelName, SubscriptionEventHandler subscribedEventHandler = null)
		{
			Pusher.<SubscribeAsync>d__53 <SubscribeAsync>d__ = new Pusher.<SubscribeAsync>d__53();
			<SubscribeAsync>d__.<>4__this = this;
			<SubscribeAsync>d__.channelName = channelName;
			<SubscribeAsync>d__.subscribedEventHandler = subscribedEventHandler;
			<SubscribeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Channel>.Create();
			<SubscribeAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<Channel> <>t__builder = <SubscribeAsync>d__.<>t__builder;
			<>t__builder.Start<Pusher.<SubscribeAsync>d__53>(ref <SubscribeAsync>d__);
			return <SubscribeAsync>d__.<>t__builder.Task;
		}

		[DebuggerStepThrough]
		public Task<GenericPresenceChannel<MemberT>> SubscribePresenceAsync<MemberT>(string channelName, SubscriptionEventHandler subscribedEventHandler = null)
		{
			Pusher.<SubscribePresenceAsync>d__54<MemberT> <SubscribePresenceAsync>d__ = new Pusher.<SubscribePresenceAsync>d__54<MemberT>();
			<SubscribePresenceAsync>d__.<>4__this = this;
			<SubscribePresenceAsync>d__.channelName = channelName;
			<SubscribePresenceAsync>d__.subscribedEventHandler = subscribedEventHandler;
			<SubscribePresenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GenericPresenceChannel<MemberT>>.Create();
			<SubscribePresenceAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<GenericPresenceChannel<MemberT>> <>t__builder = <SubscribePresenceAsync>d__.<>t__builder;
			<>t__builder.Start<Pusher.<SubscribePresenceAsync>d__54<MemberT>>(ref <SubscribePresenceAsync>d__);
			return <SubscribePresenceAsync>d__.<>t__builder.Task;
		}

		public Channel GetChannel(string channelName)
		{
			Channel result = null;
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				result = channel;
			}
			return result;
		}

		public IList<Channel> GetAllChannels()
		{
			List<Channel> list = new List<Channel>(this.Channels.Count);
			foreach (Channel item in this.Channels.Values)
			{
				list.Add(item);
			}
			return list;
		}

		[DebuggerStepThrough]
		public Task UnsubscribeAsync(string channelName)
		{
			Pusher.<UnsubscribeAsync>d__57 <UnsubscribeAsync>d__ = new Pusher.<UnsubscribeAsync>d__57();
			<UnsubscribeAsync>d__.<>4__this = this;
			<UnsubscribeAsync>d__.channelName = channelName;
			<UnsubscribeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UnsubscribeAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UnsubscribeAsync>d__.<>t__builder;
			<>t__builder.Start<Pusher.<UnsubscribeAsync>d__57>(ref <UnsubscribeAsync>d__);
			return <UnsubscribeAsync>d__.<>t__builder.Task;
		}

		[DebuggerStepThrough]
		public Task UnsubscribeAllAsync()
		{
			Pusher.<UnsubscribeAllAsync>d__58 <UnsubscribeAllAsync>d__ = new Pusher.<UnsubscribeAllAsync>d__58();
			<UnsubscribeAllAsync>d__.<>4__this = this;
			<UnsubscribeAllAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UnsubscribeAllAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UnsubscribeAllAsync>d__.<>t__builder;
			<>t__builder.Start<Pusher.<UnsubscribeAllAsync>d__58>(ref <UnsubscribeAllAsync>d__);
			return <UnsubscribeAllAsync>d__.<>t__builder.Task;
		}

		private async Task<Channel> SubscribeAsync(string channelName, Channel channel, SubscriptionEventHandler subscribedEventHandler = null)
		{
			if (channel == null)
			{
				channel = this.CreateChannel(channelName);
			}
			if (subscribedEventHandler != null)
			{
				channel.Subscribed -= subscribedEventHandler;
				channel.Subscribed += subscribedEventHandler;
			}
			if (this.State == ConnectionState.Connected)
			{
				try
				{
					TimeSpan timeoutPeriod = this.Options.ClientTimeout;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = channel._subscribeLock.WaitAsync(timeoutPeriod).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					bool result = configuredTaskAwaiter.GetResult();
					if (!result)
					{
						throw new OperationTimeoutException(timeoutPeriod, Pusher.<SubscribeAsync>d__59.getString_0(107310294) + channelName);
					}
					if (!channel.IsSubscribed && this.Channels.ContainsKey(channelName))
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.SubscribeChannelAsync(channel).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter3.GetResult();
					}
					timeoutPeriod = default(TimeSpan);
				}
				catch (Exception error)
				{
					this.HandleSubscribeChannelError(channel, error);
					throw;
				}
				finally
				{
					channel._subscribeLock.Release();
				}
			}
			return channel;
		}

		[DebuggerStepThrough]
		private Task SubscribeChannelAsync(Channel channel)
		{
			Pusher.<SubscribeChannelAsync>d__60 <SubscribeChannelAsync>d__ = new Pusher.<SubscribeChannelAsync>d__60();
			<SubscribeChannelAsync>d__.<>4__this = this;
			<SubscribeChannelAsync>d__.channel = channel;
			<SubscribeChannelAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SubscribeChannelAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SubscribeChannelAsync>d__.<>t__builder;
			<>t__builder.Start<Pusher.<SubscribeChannelAsync>d__60>(ref <SubscribeChannelAsync>d__);
			return <SubscribeChannelAsync>d__.<>t__builder.Task;
		}

		private void HandleSubscribeChannelError(Channel channel, Exception exception)
		{
			channel.IsSubscribed = false;
			PusherException ex = exception as PusherException;
			if (ex != null)
			{
				IChannelException ex2 = ex as IChannelException;
				if (ex2 != null)
				{
					ex2.ChannelName = channel.Name;
					ex2.Channel = channel;
					ex2.SocketID = this.SocketID;
					Channel channel2;
					this.Channels.TryRemove(channel.Name, out channel2);
				}
			}
			else
			{
				ex = new OperationException(ErrorCodes.SubscriptionError, Pusher.getString_1(107310950) + channel.Name, exception);
			}
			this.RaiseError(ex, true);
		}

		private string CreateAuthorizedChannelSubscribeMessage(Channel channel, string jsonAuth)
		{
			string name = channel.Name;
			string auth = null;
			string channelData = null;
			JObject jobject = JObject.Parse(jsonAuth);
			JToken jtoken = jobject.SelectToken(Pusher.getString_1(107310929));
			if (jtoken != null && jtoken.Type == JTokenType.String)
			{
				auth = jtoken.Value<string>();
			}
			jtoken = jobject.SelectToken(Pusher.getString_1(107310920));
			if (jtoken != null && jtoken.Type == JTokenType.String)
			{
				channelData = jtoken.Value<string>();
			}
			jtoken = jobject.SelectToken(Pusher.getString_1(107310935));
			PrivateChannel privateChannel;
			bool flag;
			if (jtoken != null && jtoken.Type == JTokenType.String && channel.ChannelType == ChannelTypes.PrivateEncrypted)
			{
				privateChannel = (channel as PrivateChannel);
				flag = (privateChannel != null);
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				string s = jtoken.Value<string>();
				privateChannel.SharedSecret = Convert.FromBase64String(s);
			}
			PusherChannelSubscriptionData data = new PusherAuthorizedChannelSubscriptionData(name, auth, channelData);
			return DefaultSerializer.Default.Serialize(new PusherChannelSubscribeEvent(data));
		}

		private string CreateChannelSubscribeMessage(string channelName)
		{
			PusherChannelSubscriptionData data = new PusherChannelSubscriptionData(channelName);
			return DefaultSerializer.Default.Serialize(new PusherChannelSubscribeEvent(data));
		}

		private Channel CreateChannel(string channelName)
		{
			Channel channel;
			switch (Channel.GetChannelType(channelName))
			{
			case ChannelTypes.Private:
			case ChannelTypes.PrivateEncrypted:
				this.AuthEndpointCheck(channelName);
				channel = new PrivateChannel(channelName, this);
				break;
			case ChannelTypes.Presence:
				this.AuthEndpointCheck(channelName);
				channel = new PresenceChannel(channelName, this);
				break;
			default:
				channel = new Channel(channelName, this);
				break;
			}
			if (!this.Channels.TryAdd(channelName, channel))
			{
				channel = this.Channels[channelName];
			}
			return channel;
		}

		private void AuthEndpointCheck(string channelName)
		{
			if (this.Options.Authorizer == null)
			{
				string message = Pusher.getString_1(107310882) + channelName + Pusher.getString_1(107244025);
				ChannelException ex = new ChannelException(message, ErrorCodes.ChannelAuthorizerNotSet, channelName, this.SocketID);
				this.RaiseError(ex, true);
				throw ex;
			}
		}

		private void ValidateTriggerInput(string channelName, string eventName)
		{
			if (Channel.GetChannelType(channelName) == ChannelTypes.Public)
			{
				string message = Pusher.getString_1(107311273) + eventName + Pusher.getString_1(107311236);
				throw new TriggerEventException(message, ErrorCodes.TriggerEventPublicChannelError);
			}
			if (Channel.GetChannelType(channelName) == ChannelTypes.PrivateEncrypted)
			{
				string message2 = Pusher.getString_1(107311273) + eventName + Pusher.getString_1(107311119);
				throw new TriggerEventException(message2, ErrorCodes.TriggerEventPrivateEncryptedChannelError);
			}
			string text = Pusher.getString_1(107310550);
			if (eventName.IndexOf(text, StringComparison.OrdinalIgnoreCase) != 0)
			{
				string message3 = string.Concat(new string[]
				{
					Pusher.getString_1(107311273),
					eventName,
					Pusher.getString_1(107310505),
					text,
					Pusher.getString_1(107244025)
				});
				throw new TriggerEventException(message3, ErrorCodes.TriggerEventNameInvalidError);
			}
			if (this.State != ConnectionState.Connected)
			{
				string message4 = string.Format(Pusher.getString_1(107310488), eventName, this.State);
				throw new TriggerEventException(message4, ErrorCodes.TriggerEventNotConnectedError);
			}
			bool flag = false;
			Channel channel;
			if (this.Channels.TryGetValue(channelName, out channel))
			{
				flag = channel.IsSubscribed;
			}
			if (!flag)
			{
				string message5 = string.Concat(new string[]
				{
					Pusher.getString_1(107311273),
					eventName,
					Pusher.getString_1(107310363),
					channelName,
					Pusher.getString_1(107310826)
				});
				throw new TriggerEventException(message5, ErrorCodes.TriggerEventNotSubscribedError);
			}
		}

		[DebuggerStepThrough]
		Task ITriggerChannels.TriggerAsync(string channelName, string eventName, object obj)
		{
			Pusher.<PusherClient-ITriggerChannels-TriggerAsync>d__67 <PusherClient-ITriggerChannels-TriggerAsync>d__ = new Pusher.<PusherClient-ITriggerChannels-TriggerAsync>d__67();
			<PusherClient-ITriggerChannels-TriggerAsync>d__.<>4__this = this;
			<PusherClient-ITriggerChannels-TriggerAsync>d__.channelName = channelName;
			<PusherClient-ITriggerChannels-TriggerAsync>d__.eventName = eventName;
			<PusherClient-ITriggerChannels-TriggerAsync>d__.obj = obj;
			<PusherClient-ITriggerChannels-TriggerAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PusherClient-ITriggerChannels-TriggerAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <PusherClient-ITriggerChannels-TriggerAsync>d__.<>t__builder;
			<>t__builder.Start<Pusher.<PusherClient-ITriggerChannels-TriggerAsync>d__67>(ref <PusherClient-ITriggerChannels-TriggerAsync>d__);
			return <PusherClient-ITriggerChannels-TriggerAsync>d__.<>t__builder.Task;
		}

		async Task ITriggerChannels.ChannelUnsubscribeAsync(string channelName)
		{
			await this.UnsubscribeAsync(channelName);
		}

		void ITriggerChannels.RaiseChannelError(PusherException error)
		{
			this.RaiseError(error, false);
		}

		private void RaiseError(PusherException error, bool runAsNewTask = true)
		{
			if (this.Error != null)
			{
				if (!error.EmittedToErrorHandler)
				{
					if (runAsNewTask)
					{
						Task.Run(delegate()
						{
							this.InvokeErrorHandler(error);
						});
					}
					else
					{
						this.InvokeErrorHandler(error);
					}
				}
			}
			else
			{
				error.EmittedToErrorHandler = true;
			}
		}

		private void InvokeErrorHandler(PusherException error)
		{
			try
			{
				if (this.Error != null && !error.EmittedToErrorHandler)
				{
					try
					{
						this.Error(this, error);
					}
					catch (Exception arg)
					{
						if (this.Options.TraceLogger != null)
						{
							this.Options.TraceLogger.TraceError(string.Format(Pusher.getString_1(107310789), Environment.NewLine, arg));
						}
					}
				}
			}
			finally
			{
				error.EmittedToErrorHandler = true;
			}
		}

		private async Task SendUnsubscribeAsync(Channel channel)
		{
			if (this._connection != null && this._connection.IsConnected)
			{
				if (channel.IsSubscribed)
				{
					try
					{
						if (channel.IsSubscribed)
						{
							await this._connection.SendAsync(DefaultSerializer.Default.Serialize(new PusherChannelUnsubscribeEvent(channel.Name))).ConfigureAwait(false);
						}
					}
					catch (Exception e)
					{
						this.HandleOperationException(ErrorCodes.SubscriptionError, Pusher.<SendUnsubscribeAsync>d__72.getString_0(107310277), e);
						throw;
					}
					finally
					{
						channel.IsSubscribed = false;
					}
				}
			}
			else
			{
				channel.IsSubscribed = false;
			}
		}

		private void HandleOperationException(ErrorCodes code, string operation, Exception exception)
		{
			PusherException ex = exception as PusherException;
			if (ex == null)
			{
				ex = new OperationException(code, operation, exception);
			}
			this.RaiseError(ex, true);
		}

		private void HandleSubscriptionException(string action, Channel channel, Exception exception)
		{
			Exception ex = exception;
			AggregateException ex2 = exception as AggregateException;
			if (ex2 != null)
			{
				ex = ex2.InnerException;
			}
			if (!(ex is PusherException))
			{
				ex = new ChannelException(string.Concat(new string[]
				{
					action,
					Pusher.getString_1(107310752),
					channel.Name,
					Pusher.getString_1(107311412),
					Environment.NewLine,
					exception.Message
				}), ErrorCodes.SubscriptionError, channel.Name, this.SocketID, ex)
				{
					Channel = channel
				};
			}
			this.RaiseError(ex as PusherException, true);
		}

		private void UnsubscribeBacklog()
		{
			while (!this.Backlog.IsEmpty)
			{
				Channel channel;
				if (this.Backlog.TryTake(out channel) && !this.Channels.ContainsKey(channel.Name))
				{
					try
					{
						channel.IsSubscribed = true;
						Task.WaitAll(new Task[]
						{
							Task.Run(() => this.SendUnsubscribeAsync(channel))
						});
					}
					catch (Exception exception)
					{
						this.HandleSubscriptionException(Pusher.getString_1(107310691), channel, exception);
					}
				}
			}
		}

		private void MarkChannelsAsUnsubscribed()
		{
			List<Channel> list = new List<Channel>(this.Channels.Count);
			bool isConnected = this._connection.IsConnected;
			foreach (Channel channel2 in this.Channels.Values)
			{
				if (channel2.IsSubscribed)
				{
					if (isConnected)
					{
						list.Add(channel2);
					}
					else
					{
						channel2.IsSubscribed = false;
					}
				}
			}
			if (list.Count > 0)
			{
				IOrderedEnumerable<Channel> orderedEnumerable = from c in list
				select c into c
				orderby c.ChannelType descending
				select c;
				using (IEnumerator<Channel> enumerator2 = orderedEnumerable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Channel channel = enumerator2.Current;
						try
						{
							channel._subscribeLock = new SemaphoreSlim(1);
							Task.WaitAll(new Task[]
							{
								Task.Run(() => this.SendUnsubscribeAsync(channel))
							});
						}
						catch (Exception exception)
						{
							this.HandleSubscriptionException(Pusher.getString_1(107310691), channel, exception);
						}
					}
				}
			}
		}

		private void SubscribeExistingChannels()
		{
			IList<Channel> allChannels = this.GetAllChannels();
			if (allChannels.Count > 0)
			{
				IOrderedEnumerable<Channel> orderedEnumerable = from c in allChannels
				select c into c
				orderby c.ChannelType
				select c;
				using (IEnumerator<Channel> enumerator = orderedEnumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Channel channel = enumerator.Current;
						try
						{
							Task.WaitAll(new Task[]
							{
								Task.Run<Channel>(() => this.SubscribeAsync(channel.Name, channel, null))
							});
						}
						catch (AggregateException exception)
						{
							this.HandleSubscriptionException(Pusher.getString_1(107310706), channel, exception);
						}
					}
				}
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Pusher()
		{
			Strings.CreateGetStringDelegate(typeof(Pusher));
			Pusher.Version = typeof(Pusher).GetTypeInfo().Assembly.GetName().Version.ToString(3);
		}

		private readonly string _applicationKey;

		private IConnection _connection;

		private SemaphoreSlim _connectLock = new SemaphoreSlim(1);

		private SemaphoreSlim _disconnectLock = new SemaphoreSlim(1);

		[NonSerialized]
		internal static GetString getString_1;
	}
}
