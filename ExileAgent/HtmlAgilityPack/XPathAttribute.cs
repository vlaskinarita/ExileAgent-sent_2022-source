using System;

namespace HtmlAgilityPack
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class XPathAttribute : Attribute
	{
		public string XPath { get; }

		public string AttributeName { get; set; }

		public ReturnType NodeReturnType { get; set; }

		public XPathAttribute(string xpathString)
		{
			this.XPath = xpathString;
			this.NodeReturnType = ReturnType.InnerText;
		}

		public XPathAttribute(string xpathString, ReturnType nodeReturnType)
		{
			this.XPath = xpathString;
			this.NodeReturnType = nodeReturnType;
		}

		public XPathAttribute(string xpathString, string attributeName)
		{
			this.XPath = xpathString;
			this.AttributeName = attributeName;
		}
	}
}
