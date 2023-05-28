using System;
using System.Runtime.CompilerServices;

namespace PusherClient
{
	[Dynamic(new bool[]
	{
		false,
		true
	})]
	public sealed class DynamicEventEmitter : EventEmitter<object>
	{
	}
}
