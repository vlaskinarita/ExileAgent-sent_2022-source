using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class WebsocketException : PusherException
	{
		public WebsocketException(ConnectionState state, Exception innerException) : base(WebsocketException.getString_0(107311715) + Environment.NewLine + innerException.Message, ErrorCodes.WebSocketError, innerException)
		{
			this.State = state;
		}

		public ConnectionState State { get; private set; }

		static WebsocketException()
		{
			Strings.CreateGetStringDelegate(typeof(WebsocketException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
