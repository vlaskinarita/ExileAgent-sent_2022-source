using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	internal sealed class XElementWrapper : XContainerWrapper, IXmlNode, IXmlElement
	{
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		public XElementWrapper(XElement element) : base(element)
		{
		}

		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute attribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(attribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute(XElementWrapper.getString_0(107320743), namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!string.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && string.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == XElementWrapper.getString_0(107320743) && string.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode result = base.AppendChild(newChild);
			this._attributes = null;
			return result;
		}

		public override string Value
		{
			get
			{
				return this.Element.Value;
			}
			set
			{
				this.Element.Value = value;
			}
		}

		public override string LocalName
		{
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		public override string NamespaceUri
		{
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		static XElementWrapper()
		{
			Strings.CreateGetStringDelegate(typeof(XElementWrapper));
		}

		private List<IXmlNode> _attributes;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
