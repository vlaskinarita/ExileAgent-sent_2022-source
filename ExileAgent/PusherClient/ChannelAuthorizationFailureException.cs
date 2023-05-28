using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public class ChannelAuthorizationFailureException : ChannelException
	{
		public ChannelAuthorizationFailureException(string message, ErrorCodes code, string authorizationEndpoint, string channelName, string socketId) : base(message, code, channelName, socketId)
		{
			this.AuthorizationEndpoint = authorizationEndpoint;
		}

		public ChannelAuthorizationFailureException(ErrorCodes code, string authorizationEndpoint, string channelName, string socketId, Exception innerException) : base(string.Concat(new string[]
		{
			ChannelAuthorizationFailureException.getString_1(107311813),
			channelName,
			ChannelAuthorizationFailureException.getString_1(107404101),
			Environment.NewLine,
			innerException.Message
		}), code, channelName, socketId, innerException)
		{
			this.AuthorizationEndpoint = authorizationEndpoint;
		}

		public string AuthorizationEndpoint { get; private set; }

		static ChannelAuthorizationFailureException()
		{
			Strings.CreateGetStringDelegate(typeof(ChannelAuthorizationFailureException));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
