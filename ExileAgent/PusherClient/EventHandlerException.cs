using System;

namespace PusherClient
{
	public class EventHandlerException : PusherException
	{
		public EventHandlerException(string message, ErrorCodes code, Exception innerException) : base(message, code, innerException)
		{
		}
	}
}
