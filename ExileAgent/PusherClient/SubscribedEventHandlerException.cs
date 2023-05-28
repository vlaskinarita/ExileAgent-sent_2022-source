using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class SubscribedEventHandlerException : EventHandlerException
	{
		public SubscribedEventHandlerException(Channel channel, Exception innerException, string data) : base(SubscribedEventHandlerException.getString_0(107311766) + Environment.NewLine + innerException.Message, ErrorCodes.SubscribedEventHandlerError, innerException)
		{
			this.Channel = channel;
			this.MessageData = data;
		}

		public Channel Channel { get; private set; }

		public string MessageData { get; private set; }

		static SubscribedEventHandlerException()
		{
			Strings.CreateGetStringDelegate(typeof(SubscribedEventHandlerException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
