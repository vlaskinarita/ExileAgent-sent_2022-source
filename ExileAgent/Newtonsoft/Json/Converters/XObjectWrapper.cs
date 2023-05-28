using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	internal class XObjectWrapper : IXmlNode
	{
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		public virtual XmlNodeType NodeType
		{
			get
			{
				return this._xmlObject.NodeType;
			}
		}

		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		public virtual List<IXmlNode> ChildNodes
		{
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		public virtual List<IXmlNode> Attributes
		{
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		private readonly XObject _xmlObject;
	}
}
