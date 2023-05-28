using System;
using ns20;

namespace PusherClient
{
	public sealed class MemberRemovedEventHandlerException<T> : EventHandlerException
	{
		public MemberRemovedEventHandlerException(string memberKey, T member, Exception innerException) : base(Class401.smethod_0(107302475) + Environment.NewLine + innerException.Message, ErrorCodes.MemberRemovedEventHandlerError, innerException)
		{
			this.MemberKey = memberKey;
			this.Member = member;
		}

		public string MemberKey { get; private set; }

		public T Member { get; private set; }
	}
}
