using System;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	internal sealed class XmlElementWrapper : XmlNodeWrapper, IXmlNode, IXmlElement
	{
		public XmlElementWrapper(XmlElement element) : base(element)
		{
			this._element = element;
		}

		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		private readonly XmlElement _element;
	}
}
