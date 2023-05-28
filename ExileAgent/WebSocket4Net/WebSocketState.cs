using System;

namespace WebSocket4Net
{
	public enum WebSocketState
	{
		None = -1,
		Connecting,
		Open,
		Closing,
		Closed
	}
}
