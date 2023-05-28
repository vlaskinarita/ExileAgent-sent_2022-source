using System;
using System.Threading.Tasks;

namespace PusherClient
{
	public interface IAuthorizerAsync
	{
		TimeSpan? Timeout { get; set; }

		Task<string> AuthorizeAsync(string channelName, string socketId);
	}
}
