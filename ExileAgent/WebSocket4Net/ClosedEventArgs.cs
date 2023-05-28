using System;

namespace WebSocket4Net
{
	public sealed class ClosedEventArgs : EventArgs
	{
		public short Code { get; private set; }

		public string Reason { get; private set; }

		public ClosedEventArgs(short code, string reason)
		{
			this.Code = code;
			this.Reason = reason;
		}
	}
}
