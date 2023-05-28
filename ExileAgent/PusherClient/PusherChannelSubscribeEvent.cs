using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	internal sealed class PusherChannelSubscribeEvent : PusherSystemEvent
	{
		public PusherChannelSubscribeEvent(PusherChannelSubscriptionData data) : base(PusherChannelSubscribeEvent.getString_0(107310257), data)
		{
		}

		static PusherChannelSubscribeEvent()
		{
			Strings.CreateGetStringDelegate(typeof(PusherChannelSubscribeEvent));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
