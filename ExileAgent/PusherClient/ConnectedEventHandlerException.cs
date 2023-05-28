using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class ConnectedEventHandlerException : EventHandlerException
	{
		public ConnectedEventHandlerException(Exception innerException) : base(ConnectedEventHandlerException.getString_0(107312220) + Environment.NewLine + innerException.Message, ErrorCodes.ConnectedEventHandlerError, innerException)
		{
		}

		static ConnectedEventHandlerException()
		{
			Strings.CreateGetStringDelegate(typeof(ConnectedEventHandlerException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
