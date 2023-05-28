using System;

namespace HtmlAgilityPack
{
	public sealed class HtmlWebException : Exception
	{
		public HtmlWebException(string message) : base(message)
		{
		}
	}
}
