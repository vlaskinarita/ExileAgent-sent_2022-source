using System;

namespace PusherClient
{
	public class PrivateChannel : Channel
	{
		internal PrivateChannel(string channelName, ITriggerChannels pusher) : base(channelName, pusher)
		{
		}

		internal byte[] SharedSecret { get; set; }
	}
}
