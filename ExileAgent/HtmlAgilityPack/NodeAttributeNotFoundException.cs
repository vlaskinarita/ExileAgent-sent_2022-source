using System;

namespace HtmlAgilityPack
{
	public sealed class NodeAttributeNotFoundException : Exception
	{
		public NodeAttributeNotFoundException()
		{
		}

		public NodeAttributeNotFoundException(string message) : base(message)
		{
		}

		public NodeAttributeNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
