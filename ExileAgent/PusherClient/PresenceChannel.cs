using System;
using System.Runtime.CompilerServices;

namespace PusherClient
{
	[Dynamic(new bool[]
	{
		false,
		true
	})]
	public sealed class PresenceChannel : GenericPresenceChannel<object>
	{
		internal PresenceChannel(string channelName, ITriggerChannels pusher) : base(channelName, pusher)
		{
		}
	}
}
