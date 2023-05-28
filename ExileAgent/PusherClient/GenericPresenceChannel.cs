using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PusherClient
{
	public class GenericPresenceChannel<T> : PrivateChannel, IPresenceChannel<T>, IPresenceChannelManagement
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event MemberAddedEventHandler<T> MemberAdded;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event MemberRemovedEventHandler<T> MemberRemoved;

		internal GenericPresenceChannel(string channelName, ITriggerChannels pusher) : base(channelName, pusher)
		{
		}

		private ConcurrentDictionary<string, T> Members { get; set; } = new ConcurrentDictionary<string, T>();

		public T GetMember(string userId)
		{
			T result = default(T);
			T t;
			if (this.Members.TryGetValue(userId, out t))
			{
				result = t;
			}
			return result;
		}

		public Dictionary<string, T> GetMembers()
		{
			Dictionary<string, T> dictionary = new Dictionary<string, T>(this.Members.Count);
			foreach (KeyValuePair<string, T> keyValuePair in this.Members)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		internal override void SubscriptionSucceeded(string data)
		{
			if (!base.IsSubscribed)
			{
				this.Members = this.ParseMembersList(data);
				base.SubscriptionSucceeded(data);
			}
		}

		void IPresenceChannelManagement.AddMember(string data)
		{
			KeyValuePair<string, T> member = this.ParseMember(data);
			this.Members[member.Key] = member.Value;
			if (this.MemberAdded != null)
			{
				try
				{
					this.MemberAdded(this, member);
				}
				catch (Exception innerException)
				{
					this._pusher.RaiseChannelError(new MemberAddedEventHandlerException<T>(member.Key, member.Value, innerException));
				}
			}
		}

		void IPresenceChannelManagement.RemoveMember(string data)
		{
			KeyValuePair<string, T> keyValuePair = this.ParseMember(data);
			T t;
			if (this.Members.TryRemove(keyValuePair.Key, out t) && this.MemberRemoved != null)
			{
				try
				{
					this.MemberRemoved(this, new KeyValuePair<string, T>(keyValuePair.Key, t));
				}
				catch (Exception innerException)
				{
					this._pusher.RaiseChannelError(new MemberRemovedEventHandlerException<T>(keyValuePair.Key, t, innerException));
				}
			}
		}

		private ConcurrentDictionary<string, T> ParseMembersList(string data)
		{
			JToken jtoken = JToken.Parse(data);
			JObject jobject = JObject.Parse(jtoken.ToString());
			ConcurrentDictionary<string, T> concurrentDictionary = new ConcurrentDictionary<string, T>();
			GenericPresenceChannel<T>.SubscriptionData subscriptionData = JsonConvert.DeserializeObject<GenericPresenceChannel<T>.SubscriptionData>(jobject.ToString(Formatting.None, Array.Empty<JsonConverter>()));
			for (int i = 0; i < subscriptionData.presence.ids.Count; i++)
			{
				string key = subscriptionData.presence.ids[i];
				T value = subscriptionData.presence.hash[key];
				concurrentDictionary[key] = value;
			}
			return concurrentDictionary;
		}

		private KeyValuePair<string, T> ParseMember(string data)
		{
			GenericPresenceChannel<T>.MemberData memberData = JsonConvert.DeserializeObject<GenericPresenceChannel<T>.MemberData>(data);
			string user_id = memberData.user_id;
			T user_info = memberData.user_info;
			return new KeyValuePair<string, T>(user_id, user_info);
		}

		private sealed class SubscriptionData
		{
			public GenericPresenceChannel<T>.SubscriptionData.Presence presence { get; set; }

			internal sealed class Presence
			{
				public List<string> ids { get; set; }

				public Dictionary<string, T> hash { get; set; }
			}
		}

		private sealed class MemberData
		{
			public string user_id { get; set; }

			public T user_info { get; set; }
		}
	}
}
