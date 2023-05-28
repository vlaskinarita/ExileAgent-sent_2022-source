using System;
using ns20;

namespace PusherClient
{
	public sealed class MemberAddedEventHandlerException<T> : EventHandlerException
	{
		public MemberAddedEventHandlerException(string memberKey, T member, Exception innerException) : base(Class401.smethod_0(107302532) + Environment.NewLine + innerException.Message, ErrorCodes.MemberAddedEventHandlerError, innerException)
		{
			this.MemberKey = memberKey;
			this.Member = member;
		}

		public string MemberKey { get; private set; }

		public T Member { get; private set; }
	}
}
