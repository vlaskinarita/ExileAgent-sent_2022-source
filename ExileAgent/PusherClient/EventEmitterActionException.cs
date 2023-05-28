using System;
using ns20;

namespace PusherClient
{
	public sealed class EventEmitterActionException<TData> : PusherException
	{
		public EventEmitterActionException(ErrorCodes code, string eventName, TData data, Exception innerException) : base(string.Concat(new string[]
		{
			Class401.smethod_0(107302597),
			eventName,
			Class401.smethod_0(107395211),
			Environment.NewLine,
			innerException.Message
		}), code, innerException)
		{
			this.EventData = data;
			this.EventName = eventName;
		}

		public TData EventData { get; private set; }

		public string EventName { get; private set; }
	}
}
