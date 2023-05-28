using System;

namespace SuperSocket.ClientEngine.Proxy
{
	internal sealed class ReceiveState
	{
		public ReceiveState(byte[] buffer)
		{
			this.Buffer = buffer;
		}

		public byte[] Buffer { get; private set; }

		public int Length { get; set; }
	}
}
