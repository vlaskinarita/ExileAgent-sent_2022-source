using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class DisconnectedEventHandlerException : EventHandlerException
	{
		public DisconnectedEventHandlerException(Exception innerException) : base(DisconnectedEventHandlerException.getString_0(107312101) + Environment.NewLine + innerException.Message, ErrorCodes.DisconnectedEventHandlerError, innerException)
		{
		}

		static DisconnectedEventHandlerException()
		{
			Strings.CreateGetStringDelegate(typeof(DisconnectedEventHandlerException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
