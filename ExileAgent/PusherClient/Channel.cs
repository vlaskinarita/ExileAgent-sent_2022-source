using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public class Channel : EventEmitter
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event SubscriptionEventHandler Subscribed;

		public bool IsSubscribed
		{
			get
			{
				return this._isSubscribed;
			}
			internal set
			{
				this._isSubscribed = value;
				if (value)
				{
					this.IsServerSubscribed = true;
				}
			}
		}

		public string Name { get; }

		public ChannelTypes ChannelType
		{
			get
			{
				return Channel.GetChannelType(this.Name);
			}
		}

		internal bool IsServerSubscribed { get; set; }

		internal Channel(string channelName, ITriggerChannels pusher)
		{
			this._pusher = pusher;
			this.Name = channelName;
			base.SetEventEmitterErrorHandler(new Action<PusherException>(pusher.RaiseChannelError));
		}

		internal virtual void SubscriptionSucceeded(string data)
		{
			if (!this.IsSubscribed)
			{
				this.IsSubscribed = true;
				if (this.Subscribed != null)
				{
					try
					{
						this.Subscribed(this);
					}
					catch (Exception innerException)
					{
						this._pusher.RaiseChannelError(new SubscribedEventHandlerException(this, innerException, data));
					}
				}
			}
		}

		public void Unsubscribe()
		{
			Task.WaitAll(new Task[]
			{
				this.UnsubscribeAsync()
			});
		}

		public async Task UnsubscribeAsync()
		{
			await this._pusher.ChannelUnsubscribeAsync(this.Name).ConfigureAwait(false);
		}

		public void Trigger(string eventName, object obj)
		{
			Guard.EventName(eventName);
			Task.WaitAll(new Task[]
			{
				this._pusher.TriggerAsync(this.Name, eventName, obj)
			});
		}

		public async Task TriggerAsync(string eventName, object obj)
		{
			Guard.EventName(eventName);
			await this._pusher.TriggerAsync(this.Name, eventName, obj).ConfigureAwait(false);
		}

		public static ChannelTypes GetChannelType(string channelName)
		{
			Guard.ChannelName(channelName);
			ChannelTypes result = ChannelTypes.Public;
			if (channelName.StartsWith(Channel.getString_1(107313195), StringComparison.OrdinalIgnoreCase))
			{
				result = ChannelTypes.PrivateEncrypted;
			}
			else if (channelName.StartsWith(Channel.getString_1(107313202), StringComparison.OrdinalIgnoreCase))
			{
				result = ChannelTypes.Private;
			}
			else if (channelName.StartsWith(Channel.getString_1(107313157), StringComparison.OrdinalIgnoreCase))
			{
				result = ChannelTypes.Presence;
			}
			return result;
		}

		static Channel()
		{
			Strings.CreateGetStringDelegate(typeof(Channel));
		}

		internal readonly ITriggerChannels _pusher;

		internal SemaphoreSlim _subscribeLock = new SemaphoreSlim(1);

		internal SemaphoreSlim _subscribeCompleted;

		internal Exception _subscriptionError;

		private bool _isSubscribed;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
