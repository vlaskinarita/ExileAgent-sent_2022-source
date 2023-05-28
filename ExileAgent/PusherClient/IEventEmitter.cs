using System;

namespace PusherClient
{
	public interface IEventEmitter<TData> : IEventBinder<TData>, IEventBinder
	{
		void EmitEvent(string eventName, TData data);
	}
}
