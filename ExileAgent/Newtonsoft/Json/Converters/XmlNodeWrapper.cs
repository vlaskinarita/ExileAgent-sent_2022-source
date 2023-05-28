using System;
using System.Collections.Generic;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	internal class XmlNodeWrapper : IXmlNode
	{
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		public List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode node = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(node));
						}
					}
				}
				return this._childNodes;
			}
		}

		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == XmlNodeType.DocumentType)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		public List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute node = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(node));
						}
					}
				}
				return this._attributes;
			}
		}

		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement;
				if ((xmlElement = (this._node as XmlElement)) != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute;
				XmlNode xmlNode = ((xmlAttribute = (this._node as XmlAttribute)) != null) ? xmlAttribute.OwnerElement : this._node.ParentNode;
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		private readonly XmlNode _node;

		private List<IXmlNode> _childNodes;

		private List<IXmlNode> _attributes;
	}
}
