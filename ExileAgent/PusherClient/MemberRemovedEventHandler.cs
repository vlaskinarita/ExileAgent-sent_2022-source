using System;
using System.Collections.Generic;

namespace PusherClient
{
	public delegate void MemberRemovedEventHandler<T>(object sender, KeyValuePair<string, T> member);
}
