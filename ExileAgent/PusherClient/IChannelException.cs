using System;

namespace PusherClient
{
	public interface IChannelException
	{
		string ChannelName { get; set; }

		string EventName { get; set; }

		string SocketID { get; set; }

		Channel Channel { get; set; }
	}
}
