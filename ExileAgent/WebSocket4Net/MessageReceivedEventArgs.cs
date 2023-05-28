using System;

namespace WebSocket4Net
{
	public sealed class MessageReceivedEventArgs : EventArgs
	{
		public MessageReceivedEventArgs(string message)
		{
			this.Message = message;
		}

		public string Message { get; private set; }
	}
}
