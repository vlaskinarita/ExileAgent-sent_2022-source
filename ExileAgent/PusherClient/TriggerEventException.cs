using System;

namespace PusherClient
{
	public sealed class TriggerEventException : PusherException
	{
		public TriggerEventException(string message, ErrorCodes code) : base(message, code)
		{
		}
	}
}
