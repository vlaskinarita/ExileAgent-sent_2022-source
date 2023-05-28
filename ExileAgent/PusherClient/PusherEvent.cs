using System;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class PusherEvent
	{
		public PusherEvent(Dictionary<string, object> eventData, string rawEvent)
		{
			this._eventData = eventData;
			this._rawEvent = rawEvent;
		}

		public object GetProperty(string key)
		{
			object result;
			this._eventData.TryGetValue(key, out result);
			return result;
		}

		public string UserId
		{
			get
			{
				string result = null;
				object obj;
				if (this._eventData.TryGetValue(PusherEvent.getString_0(107310243), out obj))
				{
					result = obj.ToString();
				}
				return result;
			}
		}

		public string ChannelName
		{
			get
			{
				string result = null;
				object obj;
				if (this._eventData.TryGetValue(PusherEvent.getString_0(107312542), out obj))
				{
					result = obj.ToString();
				}
				return result;
			}
		}

		public string EventName
		{
			get
			{
				string result = null;
				object obj;
				if (this._eventData.TryGetValue(PusherEvent.getString_0(107312536), out obj))
				{
					result = obj.ToString();
				}
				return result;
			}
		}

		public string Data
		{
			get
			{
				string result = null;
				object obj;
				if (this._eventData.TryGetValue(PusherEvent.getString_0(107403020), out obj))
				{
					if (obj is string)
					{
						result = (string)obj;
					}
					else
					{
						result = DefaultSerializer.Default.Serialize(obj);
					}
				}
				return result;
			}
		}

		public override string ToString()
		{
			return this._rawEvent;
		}

		static PusherEvent()
		{
			Strings.CreateGetStringDelegate(typeof(PusherEvent));
		}

		private readonly Dictionary<string, object> _eventData;

		private readonly string _rawEvent;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
