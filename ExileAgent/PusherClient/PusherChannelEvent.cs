using System;
using Newtonsoft.Json;

namespace PusherClient
{
	internal sealed class PusherChannelEvent : PusherSystemEvent
	{
		public PusherChannelEvent(string eventName, object data, string channelName) : base(eventName, data)
		{
			this.Channel = channelName;
		}

		[JsonProperty(PropertyName = "channel")]
		public string Channel { get; }
	}
}
