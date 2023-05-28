using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	[DebuggerDisplay("Name: {OriginalName}")]
	public class HtmlNode : IXPathNavigable
	{
		static HtmlNode()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlNode));
			HtmlNode.HtmlNodeTypeNameComment = HtmlNode.getString_0(107242547);
			HtmlNode.HtmlNodeTypeNameDocument = HtmlNode.getString_0(107242566);
			HtmlNode.HtmlNodeTypeNameText = HtmlNode.getString_0(107242521);
			HtmlNode.ElementsFlags = new Dictionary<string, HtmlElementFlag>(StringComparer.OrdinalIgnoreCase);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107245811), HtmlElementFlag.CData);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107245834), HtmlElementFlag.CData);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242512), HtmlElementFlag.CData);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242531), HtmlElementFlag.CData);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107245118), HtmlElementFlag.CData);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242486), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242477), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107245094), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242500), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107245152), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242455), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242450), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242445), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242468), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242459), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242418), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242413), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242432), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242391), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242382), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242405), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242396), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242351), HtmlElementFlag.Empty);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107245680), HtmlElementFlag.CanOverlap);
			HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107242374), HtmlElementFlag.Empty | HtmlElementFlag.Closed);
			if (!HtmlDocument.DisableBehaviorTagP)
			{
				HtmlNode.ElementsFlags.Add(HtmlNode.getString_0(107398423), HtmlElementFlag.Empty | HtmlElementFlag.Closed);
			}
		}

		public HtmlNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
		{
			this._nodetype = type;
			this._ownerdocument = ownerdocument;
			this._outerstartindex = index;
			switch (type)
			{
			case HtmlNodeType.Document:
				this.Name = HtmlNode.HtmlNodeTypeNameDocument;
				this._endnode = this;
				break;
			case HtmlNodeType.Comment:
				this.Name = HtmlNode.HtmlNodeTypeNameComment;
				this._endnode = this;
				break;
			case HtmlNodeType.Text:
				this.Name = HtmlNode.HtmlNodeTypeNameText;
				this._endnode = this;
				break;
			}
			if (this._ownerdocument.Openednodes != null && !this.Closed && -1 != index)
			{
				this._ownerdocument.Openednodes.Add(index, this);
			}
			if (-1 == index && type != HtmlNodeType.Comment)
			{
				if (type != HtmlNodeType.Text)
				{
					this.SetChanged();
					return;
				}
			}
		}

		public HtmlAttributeCollection Attributes
		{
			get
			{
				if (!this.HasAttributes)
				{
					this._attributes = new HtmlAttributeCollection(this);
				}
				return this._attributes;
			}
			internal set
			{
				this._attributes = value;
			}
		}

		public HtmlNodeCollection ChildNodes
		{
			get
			{
				HtmlNodeCollection result;
				if ((result = this._childnodes) == null)
				{
					result = (this._childnodes = new HtmlNodeCollection(this));
				}
				return result;
			}
			internal set
			{
				this._childnodes = value;
			}
		}

		public bool Closed
		{
			get
			{
				return this._endnode != null;
			}
		}

		public HtmlAttributeCollection ClosingAttributes
		{
			get
			{
				if (this.HasClosingAttributes)
				{
					return this._endnode.Attributes;
				}
				return new HtmlAttributeCollection(this);
			}
		}

		public HtmlNode EndNode
		{
			get
			{
				return this._endnode;
			}
		}

		public HtmlNode FirstChild
		{
			get
			{
				if (this.HasChildNodes)
				{
					return this._childnodes[0];
				}
				return null;
			}
		}

		public bool HasAttributes
		{
			get
			{
				return this._attributes != null && this._attributes.Count > 0;
			}
		}

		public bool HasChildNodes
		{
			get
			{
				return this._childnodes != null && this._childnodes.Count > 0;
			}
		}

		public bool HasClosingAttributes
		{
			get
			{
				if (this._endnode != null)
				{
					if (this._endnode != this)
					{
						return this._endnode._attributes != null && this._endnode._attributes.Count > 0;
					}
				}
				return false;
			}
		}

		public string Id
		{
			get
			{
				if (this._ownerdocument.Nodesid == null)
				{
					throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
				}
				return this.GetId();
			}
			set
			{
				if (this._ownerdocument.Nodesid == null)
				{
					throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
				}
				if (value == null)
				{
					throw new ArgumentNullException(HtmlNode.getString_0(107452008));
				}
				this.SetId(value);
			}
		}

		public virtual string InnerHtml
		{
			get
			{
				if (this._changed)
				{
					this.UpdateHtml();
					return this._innerhtml;
				}
				if (this._innerhtml != null)
				{
					return this._innerhtml;
				}
				if (this._innerstartindex >= 0 && this._innerlength >= 0)
				{
					return this._ownerdocument.Text.Substring(this._innerstartindex, this._innerlength);
				}
				return string.Empty;
			}
			set
			{
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.LoadHtml(value);
				this.RemoveAllChildren();
				this.AppendChildren(htmlDocument.DocumentNode.ChildNodes);
			}
		}

		public virtual string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = this.Name;
				if (text != null)
				{
					text = text.ToLowerInvariant();
					bool isDisplayScriptingText = text == HtmlNode.getString_0(107242369) || text == HtmlNode.getString_0(107245811) || text == HtmlNode.getString_0(107245834);
					this.InternalInnerText(stringBuilder, isDisplayScriptingText);
				}
				else
				{
					this.InternalInnerText(stringBuilder, false);
				}
				return stringBuilder.ToString();
			}
		}

		internal virtual void InternalInnerText(StringBuilder sb, bool isDisplayScriptingText)
		{
			if (!this._ownerdocument.BackwardCompatibility)
			{
				if (this.HasChildNodes)
				{
					this.AppendInnerText(sb, isDisplayScriptingText);
					return;
				}
				sb.Append(this.GetCurrentNodeText());
				return;
			}
			else
			{
				if (this._nodetype == HtmlNodeType.Text)
				{
					sb.Append(((HtmlTextNode)this).Text);
					return;
				}
				if (this._nodetype == HtmlNodeType.Comment)
				{
					return;
				}
				if (this.HasChildNodes && (!this._isHideInnerText || isDisplayScriptingText))
				{
					foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.ChildNodes))
					{
						htmlNode.InternalInnerText(sb, isDisplayScriptingText);
					}
					return;
				}
				return;
			}
		}

		public virtual string GetDirectInnerText()
		{
			if (!this._ownerdocument.BackwardCompatibility)
			{
				if (this.HasChildNodes)
				{
					StringBuilder stringBuilder = new StringBuilder();
					this.AppendDirectInnerText(stringBuilder);
					return stringBuilder.ToString();
				}
				return this.GetCurrentNodeText();
			}
			else
			{
				if (this._nodetype == HtmlNodeType.Text)
				{
					return ((HtmlTextNode)this).Text;
				}
				if (this._nodetype == HtmlNodeType.Comment)
				{
					return HtmlNode.getString_0(107399671);
				}
				if (!this.HasChildNodes)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.ChildNodes))
				{
					if (htmlNode._nodetype == HtmlNodeType.Text)
					{
						stringBuilder2.Append(((HtmlTextNode)htmlNode).Text);
					}
				}
				return stringBuilder2.ToString();
			}
		}

		internal string GetCurrentNodeText()
		{
			if (this._nodetype == HtmlNodeType.Text)
			{
				string text = ((HtmlTextNode)this).Text;
				if (this.ParentNode.Name != HtmlNode.getString_0(107245105))
				{
					text = text.Replace(HtmlNode.getString_0(107398465), HtmlNode.getString_0(107399671)).Replace(HtmlNode.getString_0(107464041), HtmlNode.getString_0(107399671)).Replace(HtmlNode.getString_0(107242840), HtmlNode.getString_0(107399671));
				}
				return text;
			}
			return HtmlNode.getString_0(107399671);
		}

		internal void AppendDirectInnerText(StringBuilder sb)
		{
			if (this._nodetype == HtmlNodeType.Text)
			{
				sb.Append(this.GetCurrentNodeText());
			}
			if (!this.HasChildNodes)
			{
				return;
			}
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.ChildNodes))
			{
				sb.Append(htmlNode.GetCurrentNodeText());
			}
		}

		internal void AppendInnerText(StringBuilder sb, bool isShowHideInnerText)
		{
			if (this._nodetype == HtmlNodeType.Text)
			{
				sb.Append(this.GetCurrentNodeText());
			}
			if (this.HasChildNodes && (!this._isHideInnerText || isShowHideInnerText))
			{
				foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.ChildNodes))
				{
					htmlNode.AppendInnerText(sb, isShowHideInnerText);
				}
				return;
			}
		}

		public HtmlNode LastChild
		{
			get
			{
				if (this.HasChildNodes)
				{
					return this._childnodes[this._childnodes.Count - 1];
				}
				return null;
			}
		}

		public int Line
		{
			get
			{
				return this._line;
			}
			internal set
			{
				this._line = value;
			}
		}

		public int LinePosition
		{
			get
			{
				return this._lineposition;
			}
			internal set
			{
				this._lineposition = value;
			}
		}

		public int InnerStartIndex
		{
			get
			{
				return this._innerstartindex;
			}
		}

		public int InnerLength
		{
			get
			{
				return this.InnerHtml.Length;
			}
		}

		public int OuterLength
		{
			get
			{
				return this.OuterHtml.Length;
			}
		}

		public string Name
		{
			get
			{
				if (this._optimizedName == null)
				{
					if (this._name == null)
					{
						this.Name = this._ownerdocument.Text.Substring(this._namestartindex, this._namelength);
					}
					if (this._name == null)
					{
						this._optimizedName = string.Empty;
					}
					else
					{
						this._optimizedName = this._name.ToLowerInvariant();
					}
				}
				return this._optimizedName;
			}
			set
			{
				this._name = value;
				this._optimizedName = null;
			}
		}

		public HtmlNode NextSibling
		{
			get
			{
				return this._nextnode;
			}
			internal set
			{
				this._nextnode = value;
			}
		}

		public HtmlNodeType NodeType
		{
			get
			{
				return this._nodetype;
			}
			internal set
			{
				this._nodetype = value;
			}
		}

		public string OriginalName
		{
			get
			{
				return this._name;
			}
		}

		public virtual string OuterHtml
		{
			get
			{
				if (this._changed)
				{
					this.UpdateHtml();
					return this._outerhtml;
				}
				if (this._outerhtml != null)
				{
					return this._outerhtml;
				}
				if (this._outerstartindex >= 0 && this._outerlength >= 0)
				{
					return this._ownerdocument.Text.Substring(this._outerstartindex, this._outerlength);
				}
				return string.Empty;
			}
		}

		public HtmlDocument OwnerDocument
		{
			get
			{
				return this._ownerdocument;
			}
			internal set
			{
				this._ownerdocument = value;
			}
		}

		public HtmlNode ParentNode
		{
			get
			{
				return this._parentnode;
			}
			internal set
			{
				this._parentnode = value;
			}
		}

		public HtmlNode PreviousSibling
		{
			get
			{
				return this._prevnode;
			}
			internal set
			{
				this._prevnode = value;
			}
		}

		public int StreamPosition
		{
			get
			{
				return this._streamposition;
			}
		}

		public string XPath
		{
			get
			{
				return ((this.ParentNode == null || this.ParentNode.NodeType == HtmlNodeType.Document) ? HtmlNode.getString_0(107374943) : (this.ParentNode.XPath + HtmlNode.getString_0(107374943))) + this.GetRelativeXpath();
			}
		}

		public int Depth { get; set; }

		public static bool CanOverlapElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107372343));
			}
			HtmlElementFlag htmlElementFlag;
			return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.CanOverlap) > (HtmlElementFlag)0;
		}

		public static HtmlNode CreateNode(string html)
		{
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			if (!htmlDocument.DocumentNode.IsSingleElementNode())
			{
				throw new Exception(HtmlNode.getString_0(107242835));
			}
			for (HtmlNode htmlNode = htmlDocument.DocumentNode.FirstChild; htmlNode != null; htmlNode = htmlNode.NextSibling)
			{
				if (htmlNode.NodeType == HtmlNodeType.Element && htmlNode.OuterHtml != HtmlNode.getString_0(107242814))
				{
					return htmlNode;
				}
			}
			return htmlDocument.DocumentNode.FirstChild;
		}

		public static bool IsCDataElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107372343));
			}
			HtmlElementFlag htmlElementFlag;
			return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.CData) > (HtmlElementFlag)0;
		}

		public static bool IsClosedElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107372343));
			}
			HtmlElementFlag htmlElementFlag;
			return HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.Closed) > (HtmlElementFlag)0;
		}

		public static bool IsEmptyElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107372343));
			}
			HtmlElementFlag htmlElementFlag;
			return name.Length == 0 || '!' == name[0] || '?' == name[0] || (HtmlNode.ElementsFlags.TryGetValue(name, out htmlElementFlag) && (htmlElementFlag & HtmlElementFlag.Empty) > (HtmlElementFlag)0);
		}

		public static bool IsOverlappedClosingElement(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107400714));
			}
			if (text.Length <= 4)
			{
				return false;
			}
			if (text[0] == '<' && text[text.Length - 1] == '>')
			{
				if (text[1] == '/')
				{
					return HtmlNode.CanOverlapElement(text.Substring(2, text.Length - 3));
				}
			}
			return false;
		}

		public IEnumerable<HtmlNode> Ancestors()
		{
			HtmlNode.<Ancestors>d__116 <Ancestors>d__ = new HtmlNode.<Ancestors>d__116(-2);
			<Ancestors>d__.<>4__this = this;
			return <Ancestors>d__;
		}

		public IEnumerable<HtmlNode> Ancestors(string name)
		{
			HtmlNode.<Ancestors>d__117 <Ancestors>d__ = new HtmlNode.<Ancestors>d__117(-2);
			<Ancestors>d__.<>4__this = this;
			<Ancestors>d__.<>3__name = name;
			return <Ancestors>d__;
		}

		public IEnumerable<HtmlNode> AncestorsAndSelf()
		{
			HtmlNode.<AncestorsAndSelf>d__118 <AncestorsAndSelf>d__ = new HtmlNode.<AncestorsAndSelf>d__118(-2);
			<AncestorsAndSelf>d__.<>4__this = this;
			return <AncestorsAndSelf>d__;
		}

		public IEnumerable<HtmlNode> AncestorsAndSelf(string name)
		{
			HtmlNode.<AncestorsAndSelf>d__119 <AncestorsAndSelf>d__ = new HtmlNode.<AncestorsAndSelf>d__119(-2);
			<AncestorsAndSelf>d__.<>4__this = this;
			<AncestorsAndSelf>d__.<>3__name = name;
			return <AncestorsAndSelf>d__;
		}

		public HtmlNode AppendChild(HtmlNode newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242777));
			}
			this.ChildNodes.Append(newChild);
			this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
			this.SetChildNodesId(newChild);
			this.SetChanged();
			return newChild;
		}

		public void SetChildNodesId(HtmlNode chilNode)
		{
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)chilNode.ChildNodes))
			{
				this._ownerdocument.SetIdForNode(htmlNode, htmlNode.GetId());
				this.SetChildNodesId(htmlNode);
			}
		}

		public void AppendChildren(HtmlNodeCollection newChildren)
		{
			if (newChildren == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242764));
			}
			foreach (HtmlNode newChild in ((IEnumerable<HtmlNode>)newChildren))
			{
				this.AppendChild(newChild);
			}
		}

		public IEnumerable<HtmlAttribute> ChildAttributes(string name)
		{
			return this.Attributes.AttributesWithName(name);
		}

		public HtmlNode Clone()
		{
			return this.CloneNode(true);
		}

		public HtmlNode CloneNode(string newName)
		{
			return this.CloneNode(newName, true);
		}

		public HtmlNode CloneNode(string newName, bool deep)
		{
			if (newName == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242779));
			}
			HtmlNode htmlNode = this.CloneNode(deep);
			htmlNode.Name = newName;
			return htmlNode;
		}

		public HtmlNode CloneNode(bool deep)
		{
			HtmlNode htmlNode = this._ownerdocument.CreateNode(this._nodetype);
			htmlNode.Name = this.OriginalName;
			HtmlNodeType nodetype = this._nodetype;
			if (nodetype == HtmlNodeType.Comment)
			{
				((HtmlCommentNode)htmlNode).Comment = ((HtmlCommentNode)this).Comment;
				return htmlNode;
			}
			if (nodetype == HtmlNodeType.Text)
			{
				((HtmlTextNode)htmlNode).Text = ((HtmlTextNode)this).Text;
				return htmlNode;
			}
			if (this.HasAttributes)
			{
				foreach (HtmlAttribute htmlAttribute in ((IEnumerable<HtmlAttribute>)this._attributes))
				{
					HtmlAttribute newAttribute = htmlAttribute.Clone();
					htmlNode.Attributes.Append(newAttribute);
				}
			}
			if (this.HasClosingAttributes)
			{
				htmlNode._endnode = this._endnode.CloneNode(false);
				foreach (HtmlAttribute htmlAttribute2 in ((IEnumerable<HtmlAttribute>)this._endnode._attributes))
				{
					HtmlAttribute newAttribute2 = htmlAttribute2.Clone();
					htmlNode._endnode._attributes.Append(newAttribute2);
				}
			}
			if (!deep)
			{
				return htmlNode;
			}
			if (!this.HasChildNodes)
			{
				return htmlNode;
			}
			foreach (HtmlNode htmlNode2 in ((IEnumerable<HtmlNode>)this._childnodes))
			{
				HtmlNode newChild = htmlNode2.CloneNode(deep);
				htmlNode.AppendChild(newChild);
			}
			return htmlNode;
		}

		public void CopyFrom(HtmlNode node)
		{
			this.CopyFrom(node, true);
		}

		public void CopyFrom(HtmlNode node, bool deep)
		{
			if (node == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242588));
			}
			this.Attributes.RemoveAll();
			if (node.HasAttributes)
			{
				foreach (HtmlAttribute htmlAttribute in ((IEnumerable<HtmlAttribute>)node.Attributes))
				{
					HtmlAttribute newAttribute = htmlAttribute.Clone();
					this.Attributes.Append(newAttribute);
				}
			}
			if (deep)
			{
				this.RemoveAllChildren();
				if (node.HasChildNodes)
				{
					foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)node.ChildNodes))
					{
						this.AppendChild(htmlNode.CloneNode(true));
					}
				}
			}
		}

		[Obsolete("Use Descendants() instead, the results of this function will change in a future version")]
		public IEnumerable<HtmlNode> DescendantNodes(int level = 0)
		{
			HtmlNode.<DescendantNodes>d__130 <DescendantNodes>d__ = new HtmlNode.<DescendantNodes>d__130(-2);
			<DescendantNodes>d__.<>4__this = this;
			<DescendantNodes>d__.<>3__level = level;
			return <DescendantNodes>d__;
		}

		[Obsolete("Use DescendantsAndSelf() instead, the results of this function will change in a future version")]
		public IEnumerable<HtmlNode> DescendantNodesAndSelf()
		{
			return this.DescendantsAndSelf();
		}

		public IEnumerable<HtmlNode> Descendants()
		{
			return this.Descendants(0);
		}

		public IEnumerable<HtmlNode> Descendants(int level)
		{
			HtmlNode.<Descendants>d__133 <Descendants>d__ = new HtmlNode.<Descendants>d__133(-2);
			<Descendants>d__.<>4__this = this;
			<Descendants>d__.<>3__level = level;
			return <Descendants>d__;
		}

		public IEnumerable<HtmlNode> Descendants(string name)
		{
			HtmlNode.<Descendants>d__134 <Descendants>d__ = new HtmlNode.<Descendants>d__134(-2);
			<Descendants>d__.<>4__this = this;
			<Descendants>d__.<>3__name = name;
			return <Descendants>d__;
		}

		public IEnumerable<HtmlNode> DescendantsAndSelf()
		{
			HtmlNode.<DescendantsAndSelf>d__135 <DescendantsAndSelf>d__ = new HtmlNode.<DescendantsAndSelf>d__135(-2);
			<DescendantsAndSelf>d__.<>4__this = this;
			return <DescendantsAndSelf>d__;
		}

		public IEnumerable<HtmlNode> DescendantsAndSelf(string name)
		{
			HtmlNode.<DescendantsAndSelf>d__136 <DescendantsAndSelf>d__ = new HtmlNode.<DescendantsAndSelf>d__136(-2);
			<DescendantsAndSelf>d__.<>4__this = this;
			<DescendantsAndSelf>d__.<>3__name = name;
			return <DescendantsAndSelf>d__;
		}

		public HtmlNode Element(string name)
		{
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.ChildNodes))
			{
				if (htmlNode.Name == name)
				{
					return htmlNode;
				}
			}
			return null;
		}

		public IEnumerable<HtmlNode> Elements(string name)
		{
			HtmlNode.<Elements>d__138 <Elements>d__ = new HtmlNode.<Elements>d__138(-2);
			<Elements>d__.<>4__this = this;
			<Elements>d__.<>3__name = name;
			return <Elements>d__;
		}

		public HtmlAttribute GetDataAttribute(string key)
		{
			return this.Attributes.Hashitems.SingleOrDefault((KeyValuePair<string, HtmlAttribute> x) => x.Key.Equals(HtmlNode.<>c__DisplayClass139_0.getString_0(107320387) + key, StringComparison.OrdinalIgnoreCase)).Value;
		}

		public IEnumerable<HtmlAttribute> GetDataAttributes()
		{
			return (from x in this.Attributes.Hashitems
			where x.Key.StartsWith(HtmlNode.<>c.getString_0(107320391), StringComparison.OrdinalIgnoreCase)
			select x.Value).ToList<HtmlAttribute>();
		}

		public IEnumerable<HtmlAttribute> GetAttributes()
		{
			return this.Attributes.items;
		}

		public IEnumerable<HtmlAttribute> GetAttributes(params string[] attributeNames)
		{
			List<HtmlAttribute> list = new List<HtmlAttribute>();
			foreach (string name in attributeNames)
			{
				list.Add(this.Attributes[name]);
			}
			return list;
		}

		public string GetAttributeValue(string name, string def)
		{
			return this.GetAttributeValue<string>(name, def);
		}

		public int GetAttributeValue(string name, int def)
		{
			return this.GetAttributeValue<int>(name, def);
		}

		public bool GetAttributeValue(string name, bool def)
		{
			return this.GetAttributeValue<bool>(name, def);
		}

		public T GetAttributeValue<T>(string name, T def)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107372343));
			}
			if (!this.HasAttributes)
			{
				return def;
			}
			HtmlAttribute htmlAttribute = this.Attributes[name];
			if (htmlAttribute == null)
			{
				return def;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
			T result;
			try
			{
				if (converter != null && converter.CanConvertTo(htmlAttribute.Value.GetType()))
				{
					result = (T)((object)converter.ConvertTo(htmlAttribute.Value, typeof(T)));
				}
				else
				{
					result = (T)((object)htmlAttribute.Value);
				}
			}
			catch
			{
				result = def;
			}
			return result;
		}

		public HtmlNode InsertAfter(HtmlNode newChild, HtmlNode refChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242777));
			}
			if (refChild == null)
			{
				return this.PrependChild(newChild);
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			int num = -1;
			if (this._childnodes != null)
			{
				num = this._childnodes[refChild];
			}
			if (num == -1)
			{
				throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
			}
			if (this._childnodes != null)
			{
				this._childnodes.Insert(num + 1, newChild);
			}
			this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
			this.SetChildNodesId(newChild);
			this.SetChanged();
			return newChild;
		}

		public HtmlNode InsertBefore(HtmlNode newChild, HtmlNode refChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242777));
			}
			if (refChild == null)
			{
				return this.AppendChild(newChild);
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			int num = -1;
			if (this._childnodes != null)
			{
				num = this._childnodes[refChild];
			}
			if (num == -1)
			{
				throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
			}
			if (this._childnodes != null)
			{
				this._childnodes.Insert(num, newChild);
			}
			this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
			this.SetChildNodesId(newChild);
			this.SetChanged();
			return newChild;
		}

		public HtmlNode PrependChild(HtmlNode newChild)
		{
			if (newChild == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242777));
			}
			this.ChildNodes.Prepend(newChild);
			this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
			this.SetChildNodesId(newChild);
			this.SetChanged();
			return newChild;
		}

		public void PrependChildren(HtmlNodeCollection newChildren)
		{
			if (newChildren == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242764));
			}
			for (int i = newChildren.Count - 1; i >= 0; i--)
			{
				this.PrependChild(newChildren[i]);
			}
		}

		public void Remove()
		{
			if (this.ParentNode != null)
			{
				this.ParentNode.ChildNodes.Remove(this);
			}
		}

		public void RemoveAll()
		{
			this.RemoveAllChildren();
			if (this.HasAttributes)
			{
				this._attributes.Clear();
			}
			if (this._endnode != null && this._endnode != this && this._endnode._attributes != null)
			{
				this._endnode._attributes.Clear();
			}
			this.SetChanged();
		}

		public void RemoveAllChildren()
		{
			if (!this.HasChildNodes)
			{
				return;
			}
			if (this._ownerdocument.OptionUseIdAttribute)
			{
				foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this._childnodes))
				{
					this._ownerdocument.SetIdForNode(null, htmlNode.GetId());
					this.RemoveAllIDforNode(htmlNode);
				}
			}
			this._childnodes.Clear();
			this.SetChanged();
		}

		public void RemoveAllIDforNode(HtmlNode node)
		{
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)node.ChildNodes))
			{
				this._ownerdocument.SetIdForNode(null, htmlNode.GetId());
				this.RemoveAllIDforNode(htmlNode);
			}
		}

		public HtmlNode RemoveChild(HtmlNode oldChild)
		{
			if (oldChild == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242734));
			}
			int num = -1;
			if (this._childnodes != null)
			{
				num = this._childnodes[oldChild];
			}
			if (num == -1)
			{
				throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
			}
			if (this._childnodes != null)
			{
				this._childnodes.Remove(num);
			}
			this._ownerdocument.SetIdForNode(null, oldChild.GetId());
			this.RemoveAllIDforNode(oldChild);
			this.SetChanged();
			return oldChild;
		}

		public HtmlNode RemoveChild(HtmlNode oldChild, bool keepGrandChildren)
		{
			if (oldChild == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242734));
			}
			if (oldChild._childnodes != null && keepGrandChildren)
			{
				HtmlNode refChild = oldChild.PreviousSibling;
				foreach (HtmlNode newChild in ((IEnumerable<HtmlNode>)oldChild._childnodes))
				{
					refChild = this.InsertAfter(newChild, refChild);
				}
			}
			this.RemoveChild(oldChild);
			this.SetChanged();
			return oldChild;
		}

		public HtmlNode ReplaceChild(HtmlNode newChild, HtmlNode oldChild)
		{
			if (newChild == null)
			{
				return this.RemoveChild(oldChild);
			}
			if (oldChild == null)
			{
				return this.AppendChild(newChild);
			}
			int num = -1;
			if (this._childnodes != null)
			{
				num = this._childnodes[oldChild];
			}
			if (num == -1)
			{
				throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
			}
			if (this._childnodes != null)
			{
				this._childnodes.Replace(num, newChild);
			}
			this._ownerdocument.SetIdForNode(null, oldChild.GetId());
			this.RemoveAllIDforNode(oldChild);
			this._ownerdocument.SetIdForNode(newChild, newChild.GetId());
			this.SetChildNodesId(newChild);
			this.SetChanged();
			return newChild;
		}

		public HtmlAttribute SetAttributeValue(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107372343));
			}
			HtmlAttribute htmlAttribute = this.Attributes[name];
			if (htmlAttribute == null)
			{
				return this.Attributes.Append(this._ownerdocument.CreateAttribute(name, value));
			}
			htmlAttribute.Value = value;
			return htmlAttribute;
		}

		public void WriteContentTo(TextWriter outText, int level = 0)
		{
			if (level > HtmlDocument.MaxDepthLevel)
			{
				throw new ArgumentException(HtmlNode.getString_0(107242753));
			}
			if (this._childnodes == null)
			{
				return;
			}
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this._childnodes))
			{
				htmlNode.WriteTo(outText, level + 1);
			}
		}

		public string WriteContentTo()
		{
			StringWriter stringWriter = new StringWriter();
			this.WriteContentTo(stringWriter, 0);
			stringWriter.Flush();
			return stringWriter.ToString();
		}

		public virtual void WriteTo(TextWriter outText, int level = 0)
		{
			switch (this._nodetype)
			{
			case HtmlNodeType.Document:
				if (this._ownerdocument.OptionOutputAsXml)
				{
					outText.Write(HtmlNode.getString_0(107242650) + this._ownerdocument.GetOutEncoding().BodyName + HtmlNode.getString_0(107242609));
					if (this._ownerdocument.DocumentNode.HasChildNodes)
					{
						int num = this._ownerdocument.DocumentNode._childnodes.Count;
						if (num > 0)
						{
							if (this._ownerdocument.GetXmlDeclaration() != null)
							{
								num--;
							}
							if (num > 1)
							{
								if (!this._ownerdocument.BackwardCompatibility)
								{
									this.WriteContentTo(outText, level);
									return;
								}
								if (this._ownerdocument.OptionOutputUpperCase)
								{
									outText.Write(HtmlNode.getString_0(107242604));
									this.WriteContentTo(outText, level);
									outText.Write(HtmlNode.getString_0(107242627));
									return;
								}
								outText.Write(HtmlNode.getString_0(107242070));
								this.WriteContentTo(outText, level);
								outText.Write(HtmlNode.getString_0(107371616));
								return;
							}
						}
					}
				}
				this.WriteContentTo(outText, level);
				return;
			case HtmlNodeType.Element:
			{
				string text = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpperInvariant() : this.Name;
				if (this._ownerdocument.OptionOutputOriginalCase)
				{
					text = this.OriginalName;
				}
				if (this._ownerdocument.OptionOutputAsXml)
				{
					if (text.Length <= 0 || text[0] == '?' || text.Trim().Length == 0)
					{
						return;
					}
					text = HtmlDocument.GetXmlName(text, false, this._ownerdocument.OptionPreserveXmlNamespaces);
				}
				outText.Write(HtmlNode.getString_0(107245544) + text);
				this.WriteAttributes(outText, false);
				if (this.HasChildNodes)
				{
					outText.Write(HtmlNode.getString_0(107409771));
					bool flag = false;
					if (this._ownerdocument.OptionOutputAsXml && HtmlNode.IsCDataElement(this.Name))
					{
						flag = true;
						outText.Write(HtmlNode.getString_0(107242061));
					}
					if (flag)
					{
						if (this.HasChildNodes)
						{
							this.ChildNodes[0].WriteTo(outText, level);
						}
						outText.Write(HtmlNode.getString_0(107242040));
					}
					else
					{
						this.WriteContentTo(outText, level);
					}
					if (this._ownerdocument.OptionOutputAsXml || !this._isImplicitEnd)
					{
						outText.Write(HtmlNode.getString_0(107245816) + text);
						if (!this._ownerdocument.OptionOutputAsXml)
						{
							this.WriteAttributes(outText, true);
						}
						outText.Write(HtmlNode.getString_0(107409771));
						return;
					}
				}
				else if (HtmlNode.IsEmptyElement(this.Name))
				{
					if (!this._ownerdocument.OptionWriteEmptyNodes && !this._ownerdocument.OptionOutputAsXml)
					{
						if (this.Name.Length > 0 && this.Name[0] == '?')
						{
							outText.Write(HtmlNode.getString_0(107285548));
						}
						outText.Write(HtmlNode.getString_0(107409771));
						return;
					}
					outText.Write(HtmlNode.getString_0(107242055));
					return;
				}
				else
				{
					if (!this._isImplicitEnd)
					{
						outText.Write(HtmlNode.getString_0(107242050) + text + HtmlNode.getString_0(107409771));
						return;
					}
					outText.Write(HtmlNode.getString_0(107409771));
				}
				return;
			}
			case HtmlNodeType.Comment:
			{
				string text2 = ((HtmlCommentNode)this).Comment;
				if (!this._ownerdocument.OptionOutputAsXml)
				{
					outText.Write(text2);
					return;
				}
				HtmlCommentNode htmlCommentNode = (HtmlCommentNode)this;
				if (!this._ownerdocument.BackwardCompatibility && htmlCommentNode.Comment.ToLowerInvariant().StartsWith(HtmlNode.getString_0(107242672)))
				{
					outText.Write(htmlCommentNode.Comment);
					return;
				}
				outText.Write(HtmlNode.getString_0(107246208) + HtmlNode.GetXmlComment(htmlCommentNode) + HtmlNode.getString_0(107242691));
				return;
			}
			case HtmlNodeType.Text:
			{
				string text2 = ((HtmlTextNode)this).Text;
				outText.Write(this._ownerdocument.OptionOutputAsXml ? HtmlDocument.HtmlEncodeWithCompatibility(text2, this._ownerdocument.BackwardCompatibility) : text2);
				return;
			}
			default:
				return;
			}
		}

		public void WriteTo(XmlWriter writer)
		{
			switch (this._nodetype)
			{
			case HtmlNodeType.Document:
				writer.WriteProcessingInstruction(HtmlNode.getString_0(107242045), HtmlNode.getString_0(107242008) + this._ownerdocument.GetOutEncoding().BodyName + HtmlNode.getString_0(107371684));
				if (!this.HasChildNodes)
				{
					return;
				}
				using (IEnumerator<HtmlNode> enumerator = ((IEnumerable<HtmlNode>)this.ChildNodes).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						HtmlNode htmlNode = enumerator.Current;
						htmlNode.WriteTo(writer);
					}
					return;
				}
				break;
			case HtmlNodeType.Element:
				break;
			case HtmlNodeType.Comment:
				writer.WriteComment(HtmlNode.GetXmlComment((HtmlCommentNode)this));
				return;
			case HtmlNodeType.Text:
			{
				string text = ((HtmlTextNode)this).Text;
				writer.WriteString(text);
				return;
			}
			default:
				return;
			}
			string localName = this._ownerdocument.OptionOutputUpperCase ? this.Name.ToUpperInvariant() : this.Name;
			if (this._ownerdocument.OptionOutputOriginalCase)
			{
				localName = this.OriginalName;
			}
			writer.WriteStartElement(localName);
			HtmlNode.WriteAttributes(writer, this);
			if (this.HasChildNodes)
			{
				foreach (HtmlNode htmlNode2 in ((IEnumerable<HtmlNode>)this.ChildNodes))
				{
					htmlNode2.WriteTo(writer);
				}
			}
			writer.WriteEndElement();
		}

		public string WriteTo()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				this.WriteTo(stringWriter, 0);
				stringWriter.Flush();
				result = stringWriter.ToString();
			}
			return result;
		}

		public void SetParent(HtmlNode parent)
		{
			if (parent == null)
			{
				return;
			}
			this.ParentNode = parent;
			if (this.OwnerDocument.OptionMaxNestedChildNodes > 0)
			{
				this.Depth = parent.Depth + 1;
				if (this.Depth > this.OwnerDocument.OptionMaxNestedChildNodes)
				{
					throw new Exception(string.Format(HtmlNode.getString_0(107241975), this.OwnerDocument.OptionMaxNestedChildNodes));
				}
			}
		}

		internal void SetChanged()
		{
			this._changed = true;
			if (this.ParentNode != null)
			{
				this.ParentNode.SetChanged();
			}
		}

		private void UpdateHtml()
		{
			this._innerhtml = this.WriteContentTo();
			this._outerhtml = this.WriteTo();
			this._changed = false;
		}

		internal static string GetXmlComment(HtmlCommentNode comment)
		{
			string comment2 = comment.Comment;
			return comment2.Substring(4, comment2.Length - 7).Replace(HtmlNode.getString_0(107241841), HtmlNode.getString_0(107241836));
		}

		internal static void WriteAttributes(XmlWriter writer, HtmlNode node)
		{
			if (!node.HasAttributes)
			{
				return;
			}
			foreach (HtmlAttribute htmlAttribute in node.Attributes.Hashitems.Values)
			{
				writer.WriteAttributeString(htmlAttribute.XmlName, htmlAttribute.Value);
			}
		}

		internal void UpdateLastNode()
		{
			HtmlNode htmlNode = null;
			if (this._prevwithsamename != null && this._prevwithsamename._starttag)
			{
				htmlNode = this._prevwithsamename;
			}
			else if (this._ownerdocument.Openednodes != null)
			{
				foreach (KeyValuePair<int, HtmlNode> keyValuePair in this._ownerdocument.Openednodes)
				{
					if ((keyValuePair.Key < this._outerstartindex || keyValuePair.Key > this._outerstartindex + this._outerlength) && keyValuePair.Value._name == this._name)
					{
						if (htmlNode == null && keyValuePair.Value._starttag)
						{
							htmlNode = keyValuePair.Value;
						}
						else if (htmlNode != null && htmlNode.InnerStartIndex < keyValuePair.Key && keyValuePair.Value._starttag)
						{
							htmlNode = keyValuePair.Value;
						}
					}
				}
			}
			if (htmlNode != null)
			{
				this._ownerdocument.Lastnodes[htmlNode.Name] = htmlNode;
			}
		}

		internal void CloseNode(HtmlNode endnode, int level = 0)
		{
			if (level > HtmlDocument.MaxDepthLevel)
			{
				throw new ArgumentException(HtmlNode.getString_0(107242753));
			}
			if (!this._ownerdocument.OptionAutoCloseOnEnd && this._childnodes != null)
			{
				foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this._childnodes))
				{
					if (!htmlNode.Closed)
					{
						HtmlNode htmlNode2 = new HtmlNode(this.NodeType, this._ownerdocument, -1);
						htmlNode2._endnode = htmlNode2;
						htmlNode.CloseNode(htmlNode2, level + 1);
					}
				}
			}
			if (!this.Closed)
			{
				this._endnode = endnode;
				if (this._ownerdocument.Openednodes != null)
				{
					this._ownerdocument.Openednodes.Remove(this._outerstartindex);
				}
				if (Utilities.GetDictionaryValueOrDefault<string, HtmlNode>(this._ownerdocument.Lastnodes, this.Name, null) == this)
				{
					this._ownerdocument.Lastnodes.Remove(this.Name);
					this._ownerdocument.UpdateLastParentNode();
					if (this._starttag && !string.IsNullOrEmpty(this.Name))
					{
						this.UpdateLastNode();
					}
				}
				if (endnode == this)
				{
					return;
				}
				this._innerstartindex = this._outerstartindex + this._outerlength;
				this._innerlength = endnode._outerstartindex - this._innerstartindex;
				this._outerlength = endnode._outerstartindex + endnode._outerlength - this._outerstartindex;
			}
		}

		internal string GetId()
		{
			HtmlAttribute htmlAttribute = this.Attributes[HtmlNode.getString_0(107453773)];
			if (htmlAttribute != null)
			{
				return htmlAttribute.Value;
			}
			return string.Empty;
		}

		internal void SetId(string id)
		{
			HtmlAttribute htmlAttribute = this.Attributes[HtmlNode.getString_0(107453773)] ?? this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107453773));
			htmlAttribute.Value = id;
			this._ownerdocument.SetIdForNode(this, htmlAttribute.Value);
			this.Attributes[HtmlNode.getString_0(107453773)] = htmlAttribute;
			this.SetChanged();
		}

		internal void WriteAttribute(TextWriter outText, HtmlAttribute att)
		{
			if (att.Value == null)
			{
				return;
			}
			string text = (att.QuoteType == AttributeValueQuote.DoubleQuote) ? HtmlNode.getString_0(107371684) : HtmlNode.getString_0(107455368);
			string text2;
			if (this._ownerdocument.OptionOutputAsXml)
			{
				text2 = (this._ownerdocument.OptionOutputUpperCase ? att.XmlName.ToUpperInvariant() : att.XmlName);
				if (this._ownerdocument.OptionOutputOriginalCase)
				{
					text2 = att.OriginalName;
				}
				outText.Write(string.Concat(new string[]
				{
					HtmlNode.getString_0(107399708),
					text2,
					HtmlNode.getString_0(107225981),
					text,
					HtmlDocument.HtmlEncodeWithCompatibility(att.XmlValue, this._ownerdocument.BackwardCompatibility),
					text
				}));
				return;
			}
			text2 = (this._ownerdocument.OptionOutputUpperCase ? att.Name.ToUpperInvariant() : att.Name);
			if (this._ownerdocument.OptionOutputOriginalCase)
			{
				text2 = att.OriginalName;
			}
			if (att.Name.Length >= 4 && att.Name[0] == '<' && att.Name[1] == '%' && att.Name[att.Name.Length - 1] == '>' && att.Name[att.Name.Length - 2] == '%')
			{
				outText.Write(HtmlNode.getString_0(107399708) + text2);
				return;
			}
			string text3 = (att.QuoteType == AttributeValueQuote.DoubleQuote) ? ((!att.Value.StartsWith(HtmlNode.getString_0(107381537))) ? att.Value.Replace(HtmlNode.getString_0(107371684), HtmlNode.getString_0(107245489)) : att.Value) : att.Value.Replace(HtmlNode.getString_0(107455368), HtmlNode.getString_0(107241859));
			if (!this._ownerdocument.OptionOutputOptimizeAttributeValues)
			{
				outText.Write(string.Concat(new string[]
				{
					HtmlNode.getString_0(107399708),
					text2,
					HtmlNode.getString_0(107225981),
					text,
					text3,
					text
				}));
				return;
			}
			if (att.Value.IndexOfAny(new char[]
			{
				'\n',
				'\r',
				'\t',
				' '
			}) < 0)
			{
				outText.Write(HtmlNode.getString_0(107399708) + text2 + HtmlNode.getString_0(107225981) + att.Value);
				return;
			}
			outText.Write(string.Concat(new string[]
			{
				HtmlNode.getString_0(107399708),
				text2,
				HtmlNode.getString_0(107225981),
				text,
				text3,
				text
			}));
		}

		internal void WriteAttributes(TextWriter outText, bool closing)
		{
			if (!this._ownerdocument.OptionOutputAsXml)
			{
				if (!closing)
				{
					if (this._attributes != null)
					{
						foreach (HtmlAttribute att in ((IEnumerable<HtmlAttribute>)this._attributes))
						{
							this.WriteAttribute(outText, att);
						}
					}
					if (!this._ownerdocument.OptionAddDebuggingAttributes)
					{
						return;
					}
					this.WriteAttribute(outText, this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107242330), this.Closed.ToString()));
					this.WriteAttribute(outText, this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107242317), this.ChildNodes.Count.ToString()));
					int num = 0;
					using (IEnumerator<HtmlNode> enumerator2 = ((IEnumerable<HtmlNode>)this.ChildNodes).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							HtmlNode htmlNode = enumerator2.Current;
							this.WriteAttribute(outText, this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107242336) + num.ToString(), htmlNode.Name));
							num++;
						}
						return;
					}
				}
				if (this._endnode != null && this._endnode._attributes != null)
				{
					if (this._endnode != this)
					{
						foreach (HtmlAttribute att2 in ((IEnumerable<HtmlAttribute>)this._endnode._attributes))
						{
							this.WriteAttribute(outText, att2);
						}
						if (!this._ownerdocument.OptionAddDebuggingAttributes)
						{
							return;
						}
						this.WriteAttribute(outText, this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107242330), this.Closed.ToString()));
						this.WriteAttribute(outText, this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107242317), this.ChildNodes.Count.ToString()));
						return;
					}
				}
				return;
			}
			if (this._attributes == null)
			{
				return;
			}
			foreach (HtmlAttribute att3 in this._attributes.Hashitems.Values)
			{
				this.WriteAttribute(outText, att3);
			}
		}

		private string GetRelativeXpath()
		{
			if (this.ParentNode == null)
			{
				return this.Name;
			}
			if (this.NodeType == HtmlNodeType.Document)
			{
				return string.Empty;
			}
			int num = 1;
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)this.ParentNode.ChildNodes))
			{
				if (!(htmlNode.Name != this.Name))
				{
					if (htmlNode == this)
					{
						break;
					}
					num++;
				}
			}
			return this.Name + HtmlNode.getString_0(107371662) + num.ToString() + HtmlNode.getString_0(107371689);
		}

		private bool IsSingleElementNode()
		{
			int num = 0;
			for (HtmlNode htmlNode = this.FirstChild; htmlNode != null; htmlNode = htmlNode.NextSibling)
			{
				if (htmlNode.NodeType == HtmlNodeType.Element && htmlNode.OuterHtml != HtmlNode.getString_0(107242814))
				{
					num++;
				}
			}
			return num <= 1;
		}

		public void AddClass(string name)
		{
			this.AddClass(name, false);
		}

		public void AddClass(string name, bool throwError)
		{
			IEnumerable<HtmlAttribute> enumerable = this.Attributes.AttributesWithName(HtmlNode.getString_0(107453791));
			if (!this.IsEmpty(enumerable))
			{
				using (IEnumerator<HtmlAttribute> enumerator = enumerable.GetEnumerator())
				{
					Func<string, bool> <>9__0;
					while (enumerator.MoveNext())
					{
						HtmlAttribute htmlAttribute = enumerator.Current;
						if (htmlAttribute.Value != null)
						{
							IEnumerable<string> source = htmlAttribute.Value.Split(new char[]
							{
								' '
							}).ToList<string>();
							Func<string, bool> predicate;
							if ((predicate = <>9__0) == null)
							{
								predicate = (<>9__0 = ((string x) => x.Equals(name)));
							}
							if (source.Any(predicate))
							{
								if (throwError)
								{
									throw new Exception(HtmlDocument.HtmlExceptionClassExists);
								}
								continue;
							}
						}
						this.SetAttributeValue(htmlAttribute.Name, htmlAttribute.Value + HtmlNode.getString_0(107399708) + name);
					}
					return;
				}
			}
			HtmlAttribute newAttribute = this._ownerdocument.CreateAttribute(HtmlNode.getString_0(107453791), name);
			this.Attributes.Append(newAttribute);
		}

		public void RemoveClass()
		{
			this.RemoveClass(false);
		}

		public void RemoveClass(bool throwError)
		{
			IEnumerable<HtmlAttribute> enumerable = this.Attributes.AttributesWithName(HtmlNode.getString_0(107453791));
			if (this.IsEmpty(enumerable) && throwError)
			{
				throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
			}
			foreach (HtmlAttribute attribute in enumerable)
			{
				this.Attributes.Remove(attribute);
			}
		}

		public void RemoveClass(string name)
		{
			this.RemoveClass(name, false);
		}

		public void RemoveClass(string name, bool throwError)
		{
			IEnumerable<HtmlAttribute> enumerable = this.Attributes.AttributesWithName(HtmlNode.getString_0(107453791));
			if (this.IsEmpty(enumerable) && throwError)
			{
				throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
			}
			Func<string, bool> <>9__0;
			foreach (HtmlAttribute htmlAttribute in enumerable)
			{
				if (htmlAttribute.Value != null)
				{
					if (htmlAttribute.Value.Equals(name))
					{
						this.Attributes.Remove(htmlAttribute);
					}
					else
					{
						if (htmlAttribute.Value != null)
						{
							IEnumerable<string> source = htmlAttribute.Value.Split(new char[]
							{
								' '
							}).ToList<string>();
							Func<string, bool> predicate;
							if ((predicate = <>9__0) == null)
							{
								predicate = (<>9__0 = ((string x) => x.Equals(name)));
							}
							if (source.Any(predicate))
							{
								string[] array = htmlAttribute.Value.Split(new char[]
								{
									' '
								});
								string text = HtmlNode.getString_0(107399671);
								foreach (string text2 in array)
								{
									if (!text2.Equals(name))
									{
										text = text + text2 + HtmlNode.getString_0(107399708);
									}
								}
								text = text.Trim();
								this.SetAttributeValue(htmlAttribute.Name, text);
								goto IL_15B;
							}
						}
						if (throwError)
						{
							throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
						}
					}
					IL_15B:
					if (string.IsNullOrEmpty(htmlAttribute.Value))
					{
						this.Attributes.Remove(htmlAttribute);
					}
				}
			}
		}

		public void ReplaceClass(string newClass, string oldClass)
		{
			this.ReplaceClass(newClass, oldClass, false);
		}

		public void ReplaceClass(string newClass, string oldClass, bool throwError)
		{
			if (string.IsNullOrEmpty(newClass))
			{
				this.RemoveClass(oldClass);
			}
			if (string.IsNullOrEmpty(oldClass))
			{
				this.AddClass(newClass);
			}
			IEnumerable<HtmlAttribute> enumerable = this.Attributes.AttributesWithName(HtmlNode.getString_0(107453791));
			if (this.IsEmpty(enumerable) && throwError)
			{
				throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
			}
			foreach (HtmlAttribute htmlAttribute in enumerable)
			{
				if (htmlAttribute.Value != null)
				{
					if (!htmlAttribute.Value.Equals(oldClass) && !htmlAttribute.Value.Contains(oldClass))
					{
						if (throwError)
						{
							throw new Exception(HtmlDocument.HtmlExceptionClassDoesNotExist);
						}
					}
					else
					{
						string value = htmlAttribute.Value.Replace(oldClass, newClass);
						this.SetAttributeValue(htmlAttribute.Name, value);
					}
				}
			}
		}

		public IEnumerable<string> GetClasses()
		{
			HtmlNode.<GetClasses>d__185 <GetClasses>d__ = new HtmlNode.<GetClasses>d__185(-2);
			<GetClasses>d__.<>4__this = this;
			return <GetClasses>d__;
		}

		public bool HasClass(string className)
		{
			foreach (string text in this.GetClasses())
			{
				string[] array = text.Split(null, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == className)
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool IsEmpty(IEnumerable en)
		{
			using (IEnumerator enumerator = en.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					return false;
				}
			}
			return true;
		}

		public T GetEncapsulatedData<T>()
		{
			return (T)((object)this.GetEncapsulatedData(typeof(T), null));
		}

		public T GetEncapsulatedData<T>(HtmlDocument htmlDocument)
		{
			return (T)((object)this.GetEncapsulatedData(typeof(T), htmlDocument));
		}

		public object GetEncapsulatedData(Type targetType, HtmlDocument htmlDocument = null)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107242291));
			}
			HtmlDocument htmlDocument2;
			if (htmlDocument == null)
			{
				htmlDocument2 = this.OwnerDocument;
			}
			else
			{
				htmlDocument2 = htmlDocument;
			}
			if (!targetType.IsInstantiable())
			{
				throw new MissingMethodException(HtmlNode.getString_0(107242282) + targetType.FullName);
			}
			object obj = Activator.CreateInstance(targetType);
			if (targetType.IsDefinedAttribute(typeof(HasXPathAttribute)))
			{
				IEnumerable<PropertyInfo> propertiesDefinedXPath = targetType.GetPropertiesDefinedXPath();
				if (propertiesDefinedXPath.CountOfIEnumerable<PropertyInfo>() == 0)
				{
					throw new MissingXPathException(HtmlNode.getString_0(107242193) + targetType.FullName + HtmlNode.getString_0(107242216));
				}
				using (IEnumerator<PropertyInfo> enumerator = propertiesDefinedXPath.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PropertyInfo propertyInfo = enumerator.Current;
						XPathAttribute xpathAttribute = ((IList)propertyInfo.GetCustomAttributes(typeof(XPathAttribute), false))[0] as XPathAttribute;
						if (!propertyInfo.IsIEnumerable())
						{
							HtmlNode htmlNode = null;
							try
							{
								htmlNode = htmlDocument2.DocumentNode.SelectSingleNode(xpathAttribute.XPath);
							}
							catch (XPathException ex)
							{
								throw new XPathException(string.Concat(new string[]
								{
									ex.Message,
									HtmlNode.getString_0(107241559),
									propertyInfo.PropertyType.FullName,
									HtmlNode.getString_0(107399708),
									propertyInfo.Name
								}));
							}
							catch (Exception inner)
							{
								throw new NodeNotFoundException(HtmlNode.getString_0(107241458) + propertyInfo.PropertyType.FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name, inner);
							}
							if (htmlNode == null)
							{
								if (!propertyInfo.IsDefined(typeof(SkipNodeNotFoundAttribute), false))
								{
									throw new NodeNotFoundException(HtmlNode.getString_0(107241458) + propertyInfo.PropertyType.FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name);
								}
							}
							else if (propertyInfo.PropertyType.IsDefinedAttribute(typeof(HasXPathAttribute)))
							{
								HtmlDocument htmlDocument3 = new HtmlDocument();
								htmlDocument3.LoadHtml(htmlNode.InnerHtml);
								object encapsulatedData = this.GetEncapsulatedData(propertyInfo.PropertyType, htmlDocument3);
								propertyInfo.SetValue(obj, encapsulatedData, null);
							}
							else
							{
								string text = string.Empty;
								if (xpathAttribute.AttributeName == null)
								{
									text = Tools.GetNodeValueBasedOnXPathReturnType<string>(htmlNode, xpathAttribute);
								}
								else
								{
									text = htmlNode.GetAttributeValue(xpathAttribute.AttributeName, null);
								}
								if (text == null)
								{
									throw new NodeAttributeNotFoundException(string.Concat(new string[]
									{
										HtmlNode.getString_0(107241393),
										xpathAttribute.AttributeName,
										HtmlNode.getString_0(107241404),
										htmlNode.Name,
										HtmlNode.getString_0(107241383),
										propertyInfo.PropertyType.FullName,
										HtmlNode.getString_0(107399708),
										propertyInfo.Name
									}));
								}
								object value;
								try
								{
									value = Convert.ChangeType(text, propertyInfo.PropertyType);
								}
								catch (FormatException)
								{
									throw new FormatException(HtmlNode.getString_0(107241334) + propertyInfo.PropertyType.FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name);
								}
								catch (Exception ex2)
								{
									throw new Exception(HtmlNode.getString_0(107241829) + ex2.Message);
								}
								propertyInfo.SetValue(obj, value, null);
							}
						}
						else
						{
							IList<Type> list = propertyInfo.GetGenericTypes() as IList<Type>;
							if (list == null || list.Count == 0)
							{
								throw new ArgumentException(propertyInfo.Name + HtmlNode.getString_0(107241796));
							}
							if (list.Count > 1)
							{
								throw new ArgumentException(propertyInfo.Name + HtmlNode.getString_0(107241796));
							}
							if (list.Count == 1)
							{
								HtmlNodeCollection htmlNodeCollection;
								try
								{
									htmlNodeCollection = htmlDocument2.DocumentNode.SelectNodes(xpathAttribute.XPath);
								}
								catch (XPathException ex3)
								{
									throw new XPathException(string.Concat(new string[]
									{
										ex3.Message,
										HtmlNode.getString_0(107241559),
										propertyInfo.PropertyType.FullName,
										HtmlNode.getString_0(107399708),
										propertyInfo.Name
									}));
								}
								catch (Exception inner2)
								{
									throw new NodeNotFoundException(HtmlNode.getString_0(107241458) + propertyInfo.PropertyType.FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name, inner2);
								}
								if (htmlNodeCollection != null && htmlNodeCollection.Count != 0)
								{
									IList list2 = list[0].CreateIListOfType();
									if (list[0].IsDefinedAttribute(typeof(HasXPathAttribute)))
									{
										using (IEnumerator<HtmlNode> enumerator2 = ((IEnumerable<HtmlNode>)htmlNodeCollection).GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												HtmlNode htmlNode2 = enumerator2.Current;
												HtmlDocument htmlDocument4 = new HtmlDocument();
												htmlDocument4.LoadHtml(htmlNode2.InnerHtml);
												object encapsulatedData2 = this.GetEncapsulatedData(list[0], htmlDocument4);
												list2.Add(encapsulatedData2);
											}
											goto IL_667;
										}
										goto IL_459;
									}
									goto IL_459;
									IL_62F:
									if (list2.Count != 0)
									{
										propertyInfo.SetValue(obj, list2, null);
										continue;
									}
									goto IL_753;
									IL_667:
									if (list2 != null)
									{
										goto IL_62F;
									}
									goto IL_753;
									IL_459:
									if (xpathAttribute.AttributeName == null)
									{
										try
										{
											list2 = Tools.GetNodesValuesBasedOnXPathReturnType(htmlNodeCollection, xpathAttribute, list[0]);
											goto IL_667;
										}
										catch (FormatException)
										{
											throw new FormatException(HtmlNode.getString_0(107241715) + list[0].FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name);
										}
										catch (Exception ex4)
										{
											throw new Exception(HtmlNode.getString_0(107241829) + ex4.Message);
										}
									}
									using (IEnumerator<HtmlNode> enumerator2 = ((IEnumerable<HtmlNode>)htmlNodeCollection).GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											HtmlNode htmlNode3 = enumerator2.Current;
											string attributeValue = htmlNode3.GetAttributeValue(xpathAttribute.AttributeName, null);
											if (attributeValue == null)
											{
												throw new NodeAttributeNotFoundException(string.Concat(new string[]
												{
													HtmlNode.getString_0(107241393),
													xpathAttribute.AttributeName,
													HtmlNode.getString_0(107241404),
													htmlNode3.Name,
													HtmlNode.getString_0(107241383),
													propertyInfo.PropertyType.FullName,
													HtmlNode.getString_0(107399708),
													propertyInfo.Name
												}));
											}
											object value2;
											try
											{
												value2 = Convert.ChangeType(attributeValue, list[0]);
											}
											catch (FormatException)
											{
												throw new FormatException(HtmlNode.getString_0(107241334) + list[0].FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name);
											}
											catch (Exception ex5)
											{
												throw new Exception(HtmlNode.getString_0(107241829) + ex5.Message);
											}
											list2.Add(value2);
										}
										goto IL_667;
									}
									goto IL_62F;
									IL_753:
									throw new Exception(string.Concat(new string[]
									{
										HtmlNode.getString_0(107241674),
										propertyInfo.PropertyType.FullName,
										HtmlNode.getString_0(107399708),
										propertyInfo.Name,
										HtmlNode.getString_0(107241625)
									}));
								}
								if (!propertyInfo.IsDefined(typeof(SkipNodeNotFoundAttribute), false))
								{
									throw new NodeNotFoundException(HtmlNode.getString_0(107241458) + propertyInfo.PropertyType.FullName + HtmlNode.getString_0(107399708) + propertyInfo.Name);
								}
							}
						}
					}
					return obj;
				}
			}
			throw new MissingXPathException(HtmlNode.getString_0(107241628));
		}

		public XPathNavigator CreateNavigator()
		{
			return new HtmlNodeNavigator(this.OwnerDocument, this);
		}

		public XPathNavigator CreateRootNavigator()
		{
			return new HtmlNodeNavigator(this.OwnerDocument, this.OwnerDocument.DocumentNode);
		}

		public HtmlNodeCollection SelectNodes(string xpath)
		{
			HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection(null);
			XPathNodeIterator xpathNodeIterator = new HtmlNodeNavigator(this.OwnerDocument, this).Select(xpath);
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				HtmlNodeNavigator htmlNodeNavigator = (HtmlNodeNavigator)xpathNavigator;
				htmlNodeCollection.Add(htmlNodeNavigator.CurrentNode, false);
			}
			if (htmlNodeCollection.Count == 0 && !this.OwnerDocument.OptionEmptyCollection)
			{
				return null;
			}
			return htmlNodeCollection;
		}

		public HtmlNodeCollection SelectNodes(XPathExpression xpath)
		{
			HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection(null);
			XPathNodeIterator xpathNodeIterator = new HtmlNodeNavigator(this.OwnerDocument, this).Select(xpath);
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				HtmlNodeNavigator htmlNodeNavigator = (HtmlNodeNavigator)xpathNavigator;
				htmlNodeCollection.Add(htmlNodeNavigator.CurrentNode, false);
			}
			if (htmlNodeCollection.Count == 0 && !this.OwnerDocument.OptionEmptyCollection)
			{
				return null;
			}
			return htmlNodeCollection;
		}

		public HtmlNode SelectSingleNode(string xpath)
		{
			if (xpath == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107240971));
			}
			XPathNodeIterator xpathNodeIterator = new HtmlNodeNavigator(this.OwnerDocument, this).Select(xpath);
			if (!xpathNodeIterator.MoveNext())
			{
				return null;
			}
			return ((HtmlNodeNavigator)xpathNodeIterator.Current).CurrentNode;
		}

		public HtmlNode SelectSingleNode(XPathExpression xpath)
		{
			if (xpath == null)
			{
				throw new ArgumentNullException(HtmlNode.getString_0(107240971));
			}
			XPathNodeIterator xpathNodeIterator = new HtmlNodeNavigator(this.OwnerDocument, this).Select(xpath);
			if (!xpathNodeIterator.MoveNext())
			{
				return null;
			}
			return ((HtmlNodeNavigator)xpathNodeIterator.Current).CurrentNode;
		}

		internal const string DepthLevelExceptionMessage = "The document is too complex to parse";

		internal HtmlAttributeCollection _attributes;

		internal HtmlNodeCollection _childnodes;

		internal HtmlNode _endnode;

		private bool _changed;

		internal string _innerhtml;

		internal int _innerlength;

		internal int _innerstartindex;

		internal int _line;

		internal int _lineposition;

		private string _name;

		internal int _namelength;

		internal int _namestartindex;

		internal HtmlNode _nextnode;

		internal HtmlNodeType _nodetype;

		internal string _outerhtml;

		internal int _outerlength;

		internal int _outerstartindex;

		private string _optimizedName;

		internal HtmlDocument _ownerdocument;

		internal HtmlNode _parentnode;

		internal HtmlNode _prevnode;

		internal HtmlNode _prevwithsamename;

		internal bool _starttag;

		internal int _streamposition;

		internal bool _isImplicitEnd;

		internal bool _isHideInnerText;

		public static readonly string HtmlNodeTypeNameComment;

		public static readonly string HtmlNodeTypeNameDocument;

		public static readonly string HtmlNodeTypeNameText;

		public static Dictionary<string, HtmlElementFlag> ElementsFlags;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
