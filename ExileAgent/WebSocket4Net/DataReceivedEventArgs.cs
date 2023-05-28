using System;

namespace WebSocket4Net
{
	public sealed class DataReceivedEventArgs : EventArgs
	{
		public DataReceivedEventArgs(byte[] data)
		{
			this.Data = data;
		}

		public byte[] Data { get; private set; }
	}
}
