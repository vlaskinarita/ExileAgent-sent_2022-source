using System;

namespace PusherClient
{
	public class PusherException : Exception
	{
		public PusherException(string message, ErrorCodes code) : base(message)
		{
			this.PusherCode = code;
		}

		public PusherException(string message, ErrorCodes code, Exception innerException) : base(message, innerException)
		{
			this.PusherCode = code;
		}

		public ErrorCodes PusherCode { get; }

		public bool EmittedToErrorHandler { get; set; }
	}
}
