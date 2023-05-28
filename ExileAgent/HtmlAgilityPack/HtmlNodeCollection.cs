using System;
using System.Collections;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlNodeCollection : IEnumerable<HtmlNode>, IList<HtmlNode>, IEnumerable, ICollection<HtmlNode>
	{
		public HtmlNodeCollection(HtmlNode parentnode)
		{
			this._parentnode = parentnode;
		}

		public int this[HtmlNode node]
		{
			get
			{
				int nodeIndex = this.GetNodeIndex(node);
				if (nodeIndex == -1)
				{
					throw new ArgumentOutOfRangeException(HtmlNodeCollection.getString_0(107242782), HtmlNodeCollection.getString_0(107240143) + node.CloneNode(false).OuterHtml + HtmlNodeCollection.getString_0(107240166));
				}
				return nodeIndex;
			}
		}

		public HtmlNode this[string nodeName]
		{
			get
			{
				for (int i = 0; i < this._items.Count; i++)
				{
					if (string.Equals(this._items[i].Name, nodeName, StringComparison.OrdinalIgnoreCase))
					{
						return this._items[i];
					}
				}
				return null;
			}
		}

		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public HtmlNode this[int index]
		{
			get
			{
				return this._items[index];
			}
			set
			{
				this._items[index] = value;
			}
		}

		public void Add(HtmlNode node)
		{
			this.Add(node, true);
		}

		public void Add(HtmlNode node, bool setParent)
		{
			this._items.Add(node);
			if (setParent)
			{
				node.ParentNode = this._parentnode;
			}
		}

		public void Clear()
		{
			foreach (HtmlNode htmlNode in this._items)
			{
				htmlNode.ParentNode = null;
				htmlNode.NextSibling = null;
				htmlNode.PreviousSibling = null;
			}
			this._items.Clear();
		}

		public bool Contains(HtmlNode item)
		{
			return this._items.Contains(item);
		}

		public void CopyTo(HtmlNode[] array, int arrayIndex)
		{
			this._items.CopyTo(array, arrayIndex);
		}

		IEnumerator<HtmlNode> IEnumerable<HtmlNode>.GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._items.GetEnumerator();
		}

		public int IndexOf(HtmlNode item)
		{
			return this._items.IndexOf(item);
		}

		public void Insert(int index, HtmlNode node)
		{
			HtmlNode htmlNode = null;
			HtmlNode htmlNode2 = null;
			if (index > 0)
			{
				htmlNode2 = this._items[index - 1];
			}
			if (index < this._items.Count)
			{
				htmlNode = this._items[index];
			}
			this._items.Insert(index, node);
			if (htmlNode2 != null)
			{
				if (node == htmlNode2)
				{
					throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
				}
				htmlNode2._nextnode = node;
			}
			if (htmlNode != null)
			{
				htmlNode._prevnode = node;
			}
			node._prevnode = htmlNode2;
			if (htmlNode == node)
			{
				throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
			}
			node._nextnode = htmlNode;
			node.SetParent(this._parentnode);
		}

		public bool Remove(HtmlNode item)
		{
			int index = this._items.IndexOf(item);
			this.RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index)
		{
			HtmlNode htmlNode = null;
			HtmlNode htmlNode2 = null;
			HtmlNode htmlNode3 = this._items[index];
			HtmlNode htmlNode4 = this._parentnode ?? htmlNode3._parentnode;
			if (index > 0)
			{
				htmlNode2 = this._items[index - 1];
			}
			if (index < this._items.Count - 1)
			{
				htmlNode = this._items[index + 1];
			}
			this._items.RemoveAt(index);
			if (htmlNode2 != null)
			{
				if (htmlNode == htmlNode2)
				{
					throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
				}
				htmlNode2._nextnode = htmlNode;
			}
			if (htmlNode != null)
			{
				htmlNode._prevnode = htmlNode2;
			}
			htmlNode3._prevnode = null;
			htmlNode3._nextnode = null;
			htmlNode3._parentnode = null;
			if (htmlNode4 != null)
			{
				htmlNode4.SetChanged();
			}
		}

		public static HtmlNode FindFirst(HtmlNodeCollection items, string name)
		{
			foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)items))
			{
				if (htmlNode.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return htmlNode;
				}
				if (htmlNode.HasChildNodes)
				{
					HtmlNode htmlNode2 = HtmlNodeCollection.FindFirst(htmlNode.ChildNodes, name);
					if (htmlNode2 != null)
					{
						return htmlNode2;
					}
				}
			}
			return null;
		}

		public void Append(HtmlNode node)
		{
			HtmlNode htmlNode = null;
			if (this._items.Count > 0)
			{
				htmlNode = this._items[this._items.Count - 1];
			}
			this._items.Add(node);
			node._prevnode = htmlNode;
			node._nextnode = null;
			node.SetParent(this._parentnode);
			if (htmlNode == null)
			{
				return;
			}
			if (htmlNode == node)
			{
				throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
			}
			htmlNode._nextnode = node;
		}

		public HtmlNode FindFirst(string name)
		{
			return HtmlNodeCollection.FindFirst(this, name);
		}

		public int GetNodeIndex(HtmlNode node)
		{
			for (int i = 0; i < this._items.Count; i++)
			{
				if (node == this._items[i])
				{
					return i;
				}
			}
			return -1;
		}

		public void Prepend(HtmlNode node)
		{
			HtmlNode htmlNode = null;
			if (this._items.Count > 0)
			{
				htmlNode = this._items[0];
			}
			this._items.Insert(0, node);
			if (node == htmlNode)
			{
				throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
			}
			node._nextnode = htmlNode;
			node._prevnode = null;
			node.SetParent(this._parentnode);
			if (htmlNode != null)
			{
				htmlNode._prevnode = node;
			}
		}

		public bool Remove(int index)
		{
			this.RemoveAt(index);
			return true;
		}

		public void Replace(int index, HtmlNode node)
		{
			HtmlNode htmlNode = null;
			HtmlNode htmlNode2 = null;
			HtmlNode htmlNode3 = this._items[index];
			if (index > 0)
			{
				htmlNode2 = this._items[index - 1];
			}
			if (index < this._items.Count - 1)
			{
				htmlNode = this._items[index + 1];
			}
			this._items[index] = node;
			if (htmlNode2 != null)
			{
				if (node == htmlNode2)
				{
					throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
				}
				htmlNode2._nextnode = node;
			}
			if (htmlNode != null)
			{
				htmlNode._prevnode = node;
			}
			node._prevnode = htmlNode2;
			if (htmlNode == node)
			{
				throw new InvalidProgramException(HtmlNodeCollection.getString_0(107240089));
			}
			node._nextnode = htmlNode;
			node.SetParent(this._parentnode);
			htmlNode3._prevnode = null;
			htmlNode3._nextnode = null;
			htmlNode3._parentnode = null;
		}

		public IEnumerable<HtmlNode> Descendants()
		{
			HtmlNodeCollection.<Descendants>d__32 <Descendants>d__ = new HtmlNodeCollection.<Descendants>d__32(-2);
			<Descendants>d__.<>4__this = this;
			return <Descendants>d__;
		}

		public IEnumerable<HtmlNode> Descendants(string name)
		{
			HtmlNodeCollection.<Descendants>d__33 <Descendants>d__ = new HtmlNodeCollection.<Descendants>d__33(-2);
			<Descendants>d__.<>4__this = this;
			<Descendants>d__.<>3__name = name;
			return <Descendants>d__;
		}

		public IEnumerable<HtmlNode> Elements()
		{
			HtmlNodeCollection.<Elements>d__34 <Elements>d__ = new HtmlNodeCollection.<Elements>d__34(-2);
			<Elements>d__.<>4__this = this;
			return <Elements>d__;
		}

		public IEnumerable<HtmlNode> Elements(string name)
		{
			HtmlNodeCollection.<Elements>d__35 <Elements>d__ = new HtmlNodeCollection.<Elements>d__35(-2);
			<Elements>d__.<>4__this = this;
			<Elements>d__.<>3__name = name;
			return <Elements>d__;
		}

		public IEnumerable<HtmlNode> Nodes()
		{
			HtmlNodeCollection.<Nodes>d__36 <Nodes>d__ = new HtmlNodeCollection.<Nodes>d__36(-2);
			<Nodes>d__.<>4__this = this;
			return <Nodes>d__;
		}

		static HtmlNodeCollection()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlNodeCollection));
		}

		private readonly HtmlNode _parentnode;

		private readonly List<HtmlNode> _items = new List<HtmlNode>();

		[NonSerialized]
		internal static GetString getString_0;
	}
}
