﻿using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlNodeNavigator : XPathNavigator
	{
		internal HtmlNodeNavigator()
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this.Reset();
		}

		internal HtmlNodeNavigator(HtmlDocument doc, HtmlNode currentNode)
		{
			if (currentNode == null)
			{
				throw new ArgumentNullException(HtmlNodeNavigator.getString_0(107240136));
			}
			if (currentNode.OwnerDocument != doc)
			{
				throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
			}
			if (doc == null)
			{
				throw new Exception(HtmlNodeNavigator.getString_0(107240087));
			}
			this._doc = doc;
			this._nametable = new HtmlNameTable();
			this.Reset();
			this._currentnode = currentNode;
		}

		private HtmlNodeNavigator(HtmlNodeNavigator nav)
		{
			if (nav == null)
			{
				throw new ArgumentNullException(HtmlNodeNavigator.getString_0(107245381));
			}
			this._doc = nav._doc;
			this._currentnode = nav._currentnode;
			this._attindex = nav._attindex;
			this._nametable = nav._nametable;
		}

		public HtmlNodeNavigator(Stream stream)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(stream);
			this.Reset();
		}

		public HtmlNodeNavigator(Stream stream, bool detectEncodingFromByteOrderMarks)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(stream, detectEncodingFromByteOrderMarks);
			this.Reset();
		}

		public HtmlNodeNavigator(Stream stream, Encoding encoding)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(stream, encoding);
			this.Reset();
		}

		public HtmlNodeNavigator(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(stream, encoding, detectEncodingFromByteOrderMarks);
			this.Reset();
		}

		public HtmlNodeNavigator(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(stream, encoding, detectEncodingFromByteOrderMarks, buffersize);
			this.Reset();
		}

		public HtmlNodeNavigator(TextReader reader)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(reader);
			this.Reset();
		}

		public HtmlNodeNavigator(string path)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(path);
			this.Reset();
		}

		public HtmlNodeNavigator(string path, bool detectEncodingFromByteOrderMarks)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(path, detectEncodingFromByteOrderMarks);
			this.Reset();
		}

		public HtmlNodeNavigator(string path, Encoding encoding)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(path, encoding);
			this.Reset();
		}

		public HtmlNodeNavigator(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(path, encoding, detectEncodingFromByteOrderMarks);
			this.Reset();
		}

		public HtmlNodeNavigator(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
		{
			this._doc = new HtmlDocument();
			this._nametable = new HtmlNameTable();
			this._doc.Load(path, encoding, detectEncodingFromByteOrderMarks, buffersize);
			this.Reset();
		}

		public override string BaseURI
		{
			get
			{
				return this._nametable.GetOrAdd(string.Empty);
			}
		}

		public HtmlDocument CurrentDocument
		{
			get
			{
				return this._doc;
			}
		}

		public HtmlNode CurrentNode
		{
			get
			{
				return this._currentnode;
			}
		}

		public override bool HasAttributes
		{
			get
			{
				return this._currentnode.Attributes.Count > 0;
			}
		}

		public override bool HasChildren
		{
			get
			{
				return this._currentnode.ChildNodes.Count > 0;
			}
		}

		public override bool IsEmptyElement
		{
			get
			{
				return !this.HasChildren;
			}
		}

		public override string LocalName
		{
			get
			{
				if (this._attindex != -1)
				{
					return this._nametable.GetOrAdd(this._currentnode.Attributes[this._attindex].Name);
				}
				return this._nametable.GetOrAdd(this._currentnode.Name);
			}
		}

		public override string Name
		{
			get
			{
				return this._nametable.GetOrAdd(this._currentnode.Name);
			}
		}

		public override string NamespaceURI
		{
			get
			{
				return this._nametable.GetOrAdd(string.Empty);
			}
		}

		public override XmlNameTable NameTable
		{
			get
			{
				return this._nametable;
			}
		}

		public override XPathNodeType NodeType
		{
			get
			{
				switch (this._currentnode.NodeType)
				{
				case HtmlNodeType.Document:
					return XPathNodeType.Root;
				case HtmlNodeType.Element:
					if (this._attindex != -1)
					{
						return XPathNodeType.Attribute;
					}
					return XPathNodeType.Element;
				case HtmlNodeType.Comment:
					return XPathNodeType.Comment;
				case HtmlNodeType.Text:
					return XPathNodeType.Text;
				default:
					throw new NotImplementedException(HtmlNodeNavigator.getString_0(107240034) + this._currentnode.NodeType.ToString());
				}
			}
		}

		public override string Prefix
		{
			get
			{
				return this._nametable.GetOrAdd(string.Empty);
			}
		}

		public override string Value
		{
			get
			{
				switch (this._currentnode.NodeType)
				{
				case HtmlNodeType.Document:
					return HtmlNodeNavigator.getString_0(107399905);
				case HtmlNodeType.Element:
					if (this._attindex != -1)
					{
						return this._currentnode.Attributes[this._attindex].Value;
					}
					return this._currentnode.InnerText;
				case HtmlNodeType.Comment:
					return ((HtmlCommentNode)this._currentnode).Comment;
				case HtmlNodeType.Text:
					return ((HtmlTextNode)this._currentnode).Text;
				default:
					throw new NotImplementedException(HtmlNodeNavigator.getString_0(107240034) + this._currentnode.NodeType.ToString());
				}
			}
		}

		public override string XmlLang
		{
			get
			{
				return this._nametable.GetOrAdd(string.Empty);
			}
		}

		public override XPathNavigator Clone()
		{
			return new HtmlNodeNavigator(this);
		}

		public override string GetAttribute(string localName, string namespaceURI)
		{
			HtmlAttribute htmlAttribute = this._currentnode.Attributes[localName];
			if (htmlAttribute == null)
			{
				return null;
			}
			return htmlAttribute.Value;
		}

		public override string GetNamespace(string name)
		{
			return string.Empty;
		}

		public override bool IsSamePosition(XPathNavigator other)
		{
			HtmlNodeNavigator htmlNodeNavigator = other as HtmlNodeNavigator;
			return htmlNodeNavigator != null && htmlNodeNavigator._currentnode == this._currentnode;
		}

		public override bool MoveTo(XPathNavigator other)
		{
			HtmlNodeNavigator htmlNodeNavigator = other as HtmlNodeNavigator;
			if (htmlNodeNavigator == null)
			{
				return false;
			}
			if (htmlNodeNavigator._doc == this._doc)
			{
				this._currentnode = htmlNodeNavigator._currentnode;
				this._attindex = htmlNodeNavigator._attindex;
				return true;
			}
			return false;
		}

		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			int attributeIndex = this._currentnode.Attributes.GetAttributeIndex(localName);
			if (attributeIndex == -1)
			{
				return false;
			}
			this._attindex = attributeIndex;
			return true;
		}

		public override bool MoveToFirst()
		{
			if (this._currentnode.ParentNode == null)
			{
				return false;
			}
			if (this._currentnode.ParentNode.FirstChild == null)
			{
				return false;
			}
			this._currentnode = this._currentnode.ParentNode.FirstChild;
			return true;
		}

		public override bool MoveToFirstAttribute()
		{
			if (!this.HasAttributes)
			{
				return false;
			}
			this._attindex = 0;
			return true;
		}

		public override bool MoveToFirstChild()
		{
			if (!this._currentnode.HasChildNodes)
			{
				return false;
			}
			this._currentnode = this._currentnode.ChildNodes[0];
			return true;
		}

		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		public override bool MoveToId(string id)
		{
			HtmlNode elementbyId = this._doc.GetElementbyId(id);
			if (elementbyId == null)
			{
				return false;
			}
			this._currentnode = elementbyId;
			return true;
		}

		public override bool MoveToNamespace(string name)
		{
			return false;
		}

		public override bool MoveToNext()
		{
			if (this._currentnode.NextSibling == null)
			{
				return false;
			}
			this._currentnode = this._currentnode.NextSibling;
			return true;
		}

		public override bool MoveToNextAttribute()
		{
			if (this._attindex >= this._currentnode.Attributes.Count - 1)
			{
				return false;
			}
			this._attindex++;
			return true;
		}

		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		public override bool MoveToParent()
		{
			if (this._currentnode.ParentNode == null)
			{
				return false;
			}
			this._currentnode = this._currentnode.ParentNode;
			return true;
		}

		public override bool MoveToPrevious()
		{
			if (this._currentnode.PreviousSibling == null)
			{
				return false;
			}
			this._currentnode = this._currentnode.PreviousSibling;
			return true;
		}

		public override void MoveToRoot()
		{
			this._currentnode = this._doc.DocumentNode;
		}

		private void Reset()
		{
			this._currentnode = this._doc.DocumentNode;
			this._attindex = -1;
		}

		static HtmlNodeNavigator()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlNodeNavigator));
		}

		private int _attindex;

		private HtmlNode _currentnode;

		private readonly HtmlDocument _doc;

		private readonly HtmlNameTable _nametable;

		internal bool Trace;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
