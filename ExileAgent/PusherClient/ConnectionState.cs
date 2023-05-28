using System;

namespace PusherClient
{
	public enum ConnectionState
	{
		Uninitialized,
		Connecting,
		Connected,
		Disconnecting,
		Disconnected,
		WaitingToReconnect
	}
}
