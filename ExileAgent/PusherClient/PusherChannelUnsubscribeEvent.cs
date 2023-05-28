using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	internal sealed class PusherChannelUnsubscribeEvent : PusherSystemEvent
	{
		public PusherChannelUnsubscribeEvent(string channelName) : base(PusherChannelUnsubscribeEvent.getString_0(107310265), new PusherChannelSubscriptionData(channelName))
		{
		}

		static PusherChannelUnsubscribeEvent()
		{
			Strings.CreateGetStringDelegate(typeof(PusherChannelUnsubscribeEvent));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
