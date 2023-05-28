using System;

namespace PusherClient
{
	public interface IAuthorizer
	{
		string Authorize(string channelName, string socketId);
	}
}
