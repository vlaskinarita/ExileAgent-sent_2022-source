using System;
using System.Collections.Generic;

namespace PusherClient
{
	public delegate void MemberAddedEventHandler<T>(object sender, KeyValuePair<string, T> member);
}
