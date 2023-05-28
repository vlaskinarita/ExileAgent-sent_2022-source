using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class ChannelUnauthorizedException : ChannelAuthorizationFailureException
	{
		public ChannelUnauthorizedException(string authorizationEndpoint, string channelName, string socketId) : base(ChannelUnauthorizedException.getString_2(107312240) + channelName + ChannelUnauthorizedException.getString_2(107376165), ErrorCodes.ChannelUnauthorized, authorizationEndpoint, channelName, socketId)
		{
		}

		static ChannelUnauthorizedException()
		{
			Strings.CreateGetStringDelegate(typeof(ChannelUnauthorizedException));
		}

		[NonSerialized]
		internal static GetString getString_2;
	}
}
