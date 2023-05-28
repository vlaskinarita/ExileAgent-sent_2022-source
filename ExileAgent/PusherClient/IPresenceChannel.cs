using System;
using System.Collections.Generic;

namespace PusherClient
{
	public interface IPresenceChannel<T>
	{
		T GetMember(string userId);

		Dictionary<string, T> GetMembers();
	}
}
