using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	internal class XContainerWrapper : XObjectWrapper
	{
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		public XContainerWrapper(XContainer container) : base(container)
		{
		}

		public override List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>();
						foreach (XNode node in this.Container.Nodes())
						{
							this._childNodes.Add(XContainerWrapper.WrapNode(node));
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
				return this.Container.LastNode != null;
			}
		}

		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		internal static IXmlNode WrapNode(XObject node)
		{
			XDocument document;
			if ((document = (node as XDocument)) != null)
			{
				return new XDocumentWrapper(document);
			}
			XElement element;
			if ((element = (node as XElement)) != null)
			{
				return new XElementWrapper(element);
			}
			XContainer container;
			if ((container = (node as XContainer)) != null)
			{
				return new XContainerWrapper(container);
			}
			XProcessingInstruction processingInstruction;
			if ((processingInstruction = (node as XProcessingInstruction)) != null)
			{
				return new XProcessingInstructionWrapper(processingInstruction);
			}
			XText text;
			if ((text = (node as XText)) != null)
			{
				return new XTextWrapper(text);
			}
			XComment text2;
			if ((text2 = (node as XComment)) != null)
			{
				return new XCommentWrapper(text2);
			}
			XAttribute attribute;
			if ((attribute = (node as XAttribute)) != null)
			{
				return new XAttributeWrapper(attribute);
			}
			XDocumentType documentType;
			if ((documentType = (node as XDocumentType)) != null)
			{
				return new XDocumentTypeWrapper(documentType);
			}
			return new XObjectWrapper(node);
		}

		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			this._childNodes = null;
			return newChild;
		}

		private List<IXmlNode> _childNodes;
	}
}
