using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	internal static class Guard
	{
		internal static void ChannelName(string channelName)
		{
			if (string.IsNullOrWhiteSpace(channelName))
			{
				throw new ArgumentNullException(Guard.getString_0(107311692));
			}
		}

		internal static void EventName(string eventName)
		{
			if (string.IsNullOrWhiteSpace(eventName))
			{
				throw new ArgumentNullException(Guard.getString_0(107311707));
			}
		}

		static Guard()
		{
			Strings.CreateGetStringDelegate(typeof(Guard));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
