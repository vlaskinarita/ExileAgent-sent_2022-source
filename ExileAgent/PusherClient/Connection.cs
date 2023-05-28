using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace PusherClient
{
	internal sealed class Connection : IConnection
	{
		public string SocketId { get; private set; }

		public ConnectionState State { get; private set; } = ConnectionState.Uninitialized;

		public bool IsConnected
		{
			get
			{
				return this.State == ConnectionState.Connected;
			}
		}

		public Connection(IPusher pusher, string url)
		{
			this._pusher = pusher;
			this._url = url;
		}

		public async Task ConnectAsync()
		{
			this._connectionSemaphore = new SemaphoreSlim(0, 1);
			try
			{
				this._currentError = null;
				if (this._pusher.PusherOptions.TraceLogger != null)
				{
					this._pusher.PusherOptions.TraceLogger.TraceInformation(Connection.<ConnectAsync>d__21.getString_0(107311903) + this._url);
				}
				this.ChangeState(ConnectionState.Connecting);
				this._websocket = new WebSocket(this._url, Connection.<ConnectAsync>d__21.getString_0(107404018), null, null, Connection.<ConnectAsync>d__21.getString_0(107404018), Connection.<ConnectAsync>d__21.getString_0(107404018), WebSocketVersion.None, null, SslProtocols.None, 0)
				{
					EnableAutoSendPing = true,
					AutoSendPingInterval = 1
				};
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.Run(delegate()
				{
					this._websocket.Error += this.WebsocketConnectionError;
					this._websocket.MessageReceived += this.WebsocketMessageReceived;
					this._websocket.Open();
				}).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
				TimeSpan timeoutPeriod = this._pusher.PusherOptions.InnerClientTimeout;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this._connectionSemaphore.WaitAsync(timeoutPeriod).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				bool result = configuredTaskAwaiter3.GetResult();
				if (!result)
				{
					throw new OperationTimeoutException(timeoutPeriod, Connection.<ConnectAsync>d__21.getString_0(107313049));
				}
				if (this._currentError != null)
				{
					throw this._currentError;
				}
				timeoutPeriod = default(TimeSpan);
			}
			finally
			{
				if (this._websocket != null)
				{
					this._websocket.Error -= this.WebsocketConnectionError;
				}
				this._connectionSemaphore.Dispose();
				this._connectionSemaphore = null;
			}
		}

		public async Task DisconnectAsync()
		{
			if (this._websocket != null && this.State != ConnectionState.Disconnected)
			{
				this.ChangeState(ConnectionState.Disconnecting);
				if (this._pusher.PusherOptions.TraceLogger != null)
				{
					this._pusher.PusherOptions.TraceLogger.TraceInformation(Connection.<DisconnectAsync>d__22.getString_0(107311855) + this._url);
				}
				await Task.Run(delegate()
				{
					this._websocket.Close();
				}).ConfigureAwait(false);
				this.DisposeWebsocket();
			}
		}

		[DebuggerStepThrough]
		public Task<bool> SendAsync(string message)
		{
			Connection.<SendAsync>d__23 <SendAsync>d__ = new Connection.<SendAsync>d__23();
			<SendAsync>d__.<>4__this = this;
			<SendAsync>d__.message = message;
			<SendAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SendAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <SendAsync>d__.<>t__builder;
			<>t__builder.Start<Connection.<SendAsync>d__23>(ref <SendAsync>d__);
			return <SendAsync>d__.<>t__builder.Task;
		}

		private static Dictionary<string, object> GetEventPropertiesFromMessage(string messageJson)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			JObject jobject = JObject.Parse(messageJson);
			foreach (JToken jtoken in jobject.Children())
			{
				JToken jtoken2 = jobject[jtoken.Path];
				if (Connection.getString_0(107402719).Equals(jtoken.Path))
				{
					if (jtoken2.Type != JTokenType.String)
					{
						dictionary[jtoken.Path] = jtoken2.ToString(Formatting.None, Array.Empty<JsonConverter>());
					}
					else
					{
						dictionary[jtoken.Path] = jtoken2.Value<string>();
					}
				}
				else if (jtoken2.Type == JTokenType.String)
				{
					dictionary[jtoken.Path] = jtoken2.Value<string>();
				}
			}
			return dictionary;
		}

		private static void EmitEvent(string eventName, IEventBinder binder, string jsonMessage, Dictionary<string, object> message)
		{
			if (binder.HasListeners)
			{
				PusherEventEmitter pusherEventEmitter = binder as PusherEventEmitter;
				if (pusherEventEmitter != null)
				{
					PusherEvent pusherEvent = new PusherEvent(message, jsonMessage);
					if (pusherEvent != null)
					{
						pusherEventEmitter.EmitEvent(eventName, pusherEvent);
					}
				}
				else
				{
					TextEventEmitter textEventEmitter = binder as TextEventEmitter;
					if (textEventEmitter != null)
					{
						if (jsonMessage != null)
						{
							textEventEmitter.EmitEvent(eventName, jsonMessage);
						}
					}
					else
					{
						DynamicEventEmitter dynamicEventEmitter = binder as DynamicEventEmitter;
						if (dynamicEventEmitter != null)
						{
							var anonymousTypeObject = new
							{
								@event = string.Empty,
								data = string.Empty,
								channel = string.Empty,
								user_id = string.Empty
							};
							object obj = JsonConvert.DeserializeAnonymousType(jsonMessage, anonymousTypeObject);
							if (Connection.<>o__25.<>p__1 == null)
							{
								Connection.<>o__25.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(Connection), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							Func<CallSite, object, bool> target = Connection.<>o__25.<>p__1.Target;
							CallSite <>p__ = Connection.<>o__25.<>p__1;
							if (Connection.<>o__25.<>p__0 == null)
							{
								Connection.<>o__25.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(Connection), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							if (target(<>p__, Connection.<>o__25.<>p__0.Target(Connection.<>o__25.<>p__0, obj, null)))
							{
								if (Connection.<>o__25.<>p__2 == null)
								{
									Connection.<>o__25.<>p__2 = CallSite<Action<CallSite, DynamicEventEmitter, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, Connection.getString_0(107313022), null, typeof(Connection), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Connection.<>o__25.<>p__2.Target(Connection.<>o__25.<>p__2, dynamicEventEmitter, eventName, obj);
							}
						}
					}
				}
			}
		}

		private void EmitChannelEvent(string eventName, string jsonMessage, string channelName, Dictionary<string, object> message)
		{
			foreach (string eventBinderKey in EventEmitter.EmitterKeys)
			{
				IEventBinder channelEventBinder = this._pusher.GetChannelEventBinder(eventBinderKey, channelName);
				Connection.EmitEvent(eventName, channelEventBinder, jsonMessage, message);
			}
		}

		private void EmitEvent(string eventName, string jsonMessage, Dictionary<string, object> message)
		{
			foreach (string eventBinderKey in EventEmitter.EmitterKeys)
			{
				IEventBinder eventBinder = this._pusher.GetEventBinder(eventBinderKey);
				Connection.EmitEvent(eventName, eventBinder, jsonMessage, message);
			}
		}

		private void ProcessPusherEvent(string eventName, string messageData)
		{
			if (eventName != null && eventName == Connection.getString_0(107313041))
			{
				this.ParseConnectionEstablished(messageData);
			}
		}

		private bool ProcessPusherChannelEvent(string eventName, string channelName, string messageData)
		{
			bool result = true;
			if (eventName != null)
			{
				if (eventName == Connection.getString_0(107312456))
				{
					this._pusher.SubscriptionSuceeded(channelName, messageData);
					return result;
				}
				if (eventName == Connection.getString_0(107312435))
				{
					this._pusher.SubscriptionFailed(channelName, messageData);
					return result;
				}
				if (eventName == Connection.getString_0(107312354))
				{
					this._pusher.AddMember(channelName, messageData);
					return result;
				}
				if (eventName == Connection.getString_0(107312345))
				{
					this._pusher.RemoveMember(channelName, messageData);
					return result;
				}
			}
			result = false;
			return result;
		}

		private void WebsocketMessageReceived(object sender, MessageReceivedEventArgs e)
		{
			string text = null;
			string text2 = null;
			try
			{
				if (this._pusher.PusherOptions.TraceLogger != null)
				{
					this._pusher.PusherOptions.TraceLogger.TraceInformation(Connection.getString_0(107312304) + Environment.NewLine + e.Message);
				}
				JObject jobject = JObject.Parse(e.Message);
				string text3 = jobject.ToString(Formatting.None, Array.Empty<JsonConverter>());
				Dictionary<string, object> eventPropertiesFromMessage = Connection.GetEventPropertiesFromMessage(text3);
				if (eventPropertiesFromMessage.ContainsKey(Connection.getString_0(107312235)))
				{
					text = (string)eventPropertiesFromMessage[Connection.getString_0(107312235)];
					if (text == Connection.getString_0(107312226))
					{
						this.ParseError(jobject[Connection.getString_0(107402719)]);
					}
					else
					{
						string text4 = string.Empty;
						if (eventPropertiesFromMessage.ContainsKey(Connection.getString_0(107402719)))
						{
							text4 = (string)eventPropertiesFromMessage[Connection.getString_0(107402719)];
						}
						if (eventPropertiesFromMessage.ContainsKey(Connection.getString_0(107312241)))
						{
							text2 = (string)eventPropertiesFromMessage[Connection.getString_0(107312241)];
							if (!this.ProcessPusherChannelEvent(text, text2, text4))
							{
								byte[] sharedSecret = this._pusher.GetSharedSecret(text2);
								if (sharedSecret != null)
								{
									eventPropertiesFromMessage[Connection.getString_0(107402719)] = this._dataDecrypter.DecryptData(sharedSecret, EncryptedChannelData.CreateFromJson(text4));
								}
								this.EmitEvent(text, text3, eventPropertiesFromMessage);
								this.EmitChannelEvent(text, text3, text2, eventPropertiesFromMessage);
							}
						}
						else
						{
							this.ProcessPusherEvent(text, text4);
						}
					}
				}
			}
			catch (Exception ex)
			{
				string text5 = Connection.getString_0(107312708);
				PusherException error;
				if (text2 != null)
				{
					ChannelException ex2 = ex as ChannelException;
					if (ex2 != null)
					{
						ex2.ChannelName = text2;
						ex2.EventName = text;
						ex2.SocketID = this.SocketId;
						error = ex2;
					}
					else
					{
						error = new ChannelException(Connection.getString_0(107312675) + text5 + Connection.getString_0(107459707), ErrorCodes.MessageReceivedError, text2, this.SocketId, ex)
						{
							EventName = text
						};
					}
				}
				else
				{
					if (text != null)
					{
						text5 = text5 + Connection.getString_0(107312586) + text + Connection.getString_0(107459707);
					}
					error = new OperationException(ErrorCodes.MessageReceivedError, text5, ex);
				}
				this.RaiseError(error);
			}
		}

		private void WebsocketConnectionError(object sender, ErrorEventArgs e)
		{
			this._currentError = e.Exception;
			SemaphoreSlim connectionSemaphore = this._connectionSemaphore;
			if (connectionSemaphore != null)
			{
				connectionSemaphore.Release();
			}
			this.WebsocketError(sender, e);
		}

		private void WebsocketError(object sender, ErrorEventArgs e)
		{
			if (this._pusher.PusherOptions.TraceLogger != null)
			{
				this._pusher.PusherOptions.TraceLogger.TraceError(string.Format(Connection.getString_0(107312601), Environment.NewLine, e.Exception));
			}
			this.RaiseError(new WebsocketException(this.State, e.Exception));
		}

		private void ParseConnectionEstablished(string messageData)
		{
			JToken jtoken = JToken.Parse(messageData);
			JObject jobject = JObject.Parse(jtoken.ToString());
			jtoken = jobject.SelectToken(Connection.getString_0(107312568));
			if (jtoken.Type == JTokenType.String)
			{
				this.SocketId = jtoken.Value<string>();
			}
			this.ChangeState(ConnectionState.Connected);
			SemaphoreSlim connectionSemaphore = this._connectionSemaphore;
			if (connectionSemaphore != null)
			{
				connectionSemaphore.Release();
			}
			this._websocket.Closed += this.WebsocketAutoReconnect;
			this._websocket.Error += this.WebsocketError;
			this._backOffMillis = 0;
		}

		private void WebsocketAutoReconnect(object sender, EventArgs e)
		{
			if (!this._autoReconnecting)
			{
				try
				{
					this._autoReconnecting = true;
					this.RecreateWebSocket();
				}
				catch
				{
					this._autoReconnecting = false;
					throw;
				}
				Task.Run(delegate()
				{
					try
					{
						this._backOffMillis = Math.Min(Connection.MAX_BACKOFF_MILLIS, this._backOffMillis + Connection.BACK_OFF_MILLIS_INCREMENT);
						if (this._pusher.PusherOptions.TraceLogger != null)
						{
							this._pusher.PusherOptions.TraceLogger.TraceWarning(string.Format(Connection.getString_0(107312514), this._backOffMillis));
						}
						this.ChangeState(ConnectionState.WaitingToReconnect);
						Task.WaitAll(new Task[]
						{
							Task.Delay(this._backOffMillis)
						});
						if (this._websocket != null && this.State != ConnectionState.Disconnected)
						{
							if (this._pusher.PusherOptions.TraceLogger != null)
							{
								this._pusher.PusherOptions.TraceLogger.TraceWarning(Connection.getString_0(107311941));
							}
							this.ChangeState(ConnectionState.Connecting);
							this._websocket.MessageReceived += this.WebsocketMessageReceived;
							this._websocket.Closed += this.WebsocketAutoReconnect;
							this._websocket.Error += this.WebsocketError;
							this._websocket.Open();
						}
					}
					catch (Exception innerException)
					{
						this.RaiseError(new OperationException(ErrorCodes.ReconnectError, Connection.getString_0(107311928), innerException));
					}
					finally
					{
						this._autoReconnecting = false;
					}
				});
			}
		}

		private void DisposeWebsocket()
		{
			this._currentError = null;
			this._websocket.MessageReceived -= this.WebsocketMessageReceived;
			this._websocket.Closed -= this.WebsocketAutoReconnect;
			this._websocket.Error -= this.WebsocketError;
			this._websocket.Dispose();
			this._websocket = null;
			this.ChangeState(ConnectionState.Disconnected);
		}

		private void RecreateWebSocket()
		{
			this.DisposeWebsocket();
			this._websocket = new WebSocket(this._url, Connection.getString_0(107404010), null, null, Connection.getString_0(107404010), Connection.getString_0(107404010), WebSocketVersion.None, null, SslProtocols.None, 0)
			{
				EnableAutoSendPing = true,
				AutoSendPingInterval = 1
			};
		}

		private void ParseError(JToken jToken)
		{
			JToken jtoken = jToken.SelectToken(Connection.getString_0(107255817));
			if (jtoken != null && jtoken.Type == JTokenType.String)
			{
				ErrorCodes code = ErrorCodes.Unknown;
				JToken jtoken2 = jToken.SelectToken(Connection.getString_0(107312523));
				if (jtoken2 != null && jtoken2.Type == JTokenType.Integer && Enum.IsDefined(typeof(ErrorCodes), jtoken2.Value<int>()))
				{
					code = (ErrorCodes)jtoken2.Value<int>();
				}
				this.RaiseError(new PusherException(jtoken.Value<string>(), code));
			}
		}

		private void ChangeState(ConnectionState state)
		{
			this.State = state;
			this._pusher.ChangeConnectionState(state);
		}

		private void RaiseError(PusherException error)
		{
			this._currentError = error;
			this._pusher.ErrorOccured(error);
			if (this._connectionSemaphore != null)
			{
				this._connectionSemaphore.Release();
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Connection()
		{
			Strings.CreateGetStringDelegate(typeof(Connection));
			Connection.MAX_BACKOFF_MILLIS = 10000;
			Connection.BACK_OFF_MILLIS_INCREMENT = 1000;
		}

		private WebSocket _websocket;

		private readonly string _url;

		private readonly IPusher _pusher;

		private readonly IChannelDataDecrypter _dataDecrypter = new ChannelDataDecrypter();

		private int _backOffMillis;

		private static readonly int MAX_BACKOFF_MILLIS;

		private static readonly int BACK_OFF_MILLIS_INCREMENT;

		private SemaphoreSlim _connectionSemaphore;

		private Exception _currentError;

		private bool _autoReconnecting;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
