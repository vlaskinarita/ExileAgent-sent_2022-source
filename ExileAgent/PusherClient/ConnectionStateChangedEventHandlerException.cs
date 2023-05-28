using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class ConnectionStateChangedEventHandlerException : EventHandlerException
	{
		public ConnectionStateChangedEventHandlerException(ConnectionState state, Exception innerException) : base(ConnectionStateChangedEventHandlerException.getString_0(107312137) + Environment.NewLine + innerException.Message, ErrorCodes.ConnectionStateChangedEventHandlerError, innerException)
		{
			this.State = state;
		}

		public ConnectionState State { get; private set; }

		static ConnectionStateChangedEventHandlerException()
		{
			Strings.CreateGetStringDelegate(typeof(ConnectionStateChangedEventHandlerException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
