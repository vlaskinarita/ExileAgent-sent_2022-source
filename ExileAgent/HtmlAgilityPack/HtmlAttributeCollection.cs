using System;
using System.Collections;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlAttributeCollection : IEnumerable<HtmlAttribute>, IList<HtmlAttribute>, ICollection<HtmlAttribute>, IEnumerable
	{
		internal HtmlAttributeCollection(HtmlNode ownernode)
		{
			this._ownernode = ownernode;
		}

		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public HtmlAttribute this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				HtmlAttribute htmlAttribute = this.items[index];
				this.items[index] = value;
				if (htmlAttribute.Name != value.Name)
				{
					this.Hashitems.Remove(htmlAttribute.Name);
				}
				this.Hashitems[value.Name] = value;
				value._ownernode = this._ownernode;
				this._ownernode.SetChanged();
			}
		}

		public HtmlAttribute this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107372299));
				}
				HtmlAttribute result;
				if (!this.Hashitems.TryGetValue(name, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				HtmlAttribute item;
				if (!this.Hashitems.TryGetValue(name, out item))
				{
					this.Append(value);
					return;
				}
				this[this.items.IndexOf(item)] = value;
			}
		}

		public void Add(string name, string value)
		{
			this.Append(name, value);
		}

		public void Add(HtmlAttribute item)
		{
			this.Append(item);
		}

		public void AddRange(IEnumerable<HtmlAttribute> items)
		{
			foreach (HtmlAttribute newAttribute in items)
			{
				this.Append(newAttribute);
			}
		}

		public void AddRange(Dictionary<string, string> items)
		{
			foreach (KeyValuePair<string, string> keyValuePair in items)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		void ICollection<HtmlAttribute>.Clear()
		{
			this.items.Clear();
		}

		public bool Contains(HtmlAttribute item)
		{
			return this.items.Contains(item);
		}

		public void CopyTo(HtmlAttribute[] array, int arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}

		IEnumerator<HtmlAttribute> IEnumerable<HtmlAttribute>.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		public int IndexOf(HtmlAttribute item)
		{
			return this.items.IndexOf(item);
		}

		public void Insert(int index, HtmlAttribute item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107250510));
			}
			this.Hashitems[item.Name] = item;
			item._ownernode = this._ownernode;
			this.items.Insert(index, item);
			this._ownernode.SetChanged();
		}

		bool ICollection<HtmlAttribute>.Remove(HtmlAttribute item)
		{
			return this.items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			HtmlAttribute htmlAttribute = this.items[index];
			this.Hashitems.Remove(htmlAttribute.Name);
			this.items.RemoveAt(index);
			this._ownernode.SetChanged();
		}

		public HtmlAttribute Append(HtmlAttribute newAttribute)
		{
			if (this._ownernode.NodeType != HtmlNodeType.Text)
			{
				if (this._ownernode.NodeType != HtmlNodeType.Comment)
				{
					if (newAttribute == null)
					{
						throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107246203));
					}
					this.Hashitems[newAttribute.Name] = newAttribute;
					newAttribute._ownernode = this._ownernode;
					this.items.Add(newAttribute);
					this._ownernode.SetChanged();
					return newAttribute;
				}
			}
			throw new Exception(HtmlAttributeCollection.getString_0(107246268));
		}

		public HtmlAttribute Append(string name)
		{
			HtmlAttribute newAttribute = this._ownernode._ownerdocument.CreateAttribute(name);
			return this.Append(newAttribute);
		}

		public HtmlAttribute Append(string name, string value)
		{
			HtmlAttribute newAttribute = this._ownernode._ownerdocument.CreateAttribute(name, value);
			return this.Append(newAttribute);
		}

		public bool Contains(string name)
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				if (string.Equals(this.items[i].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		public HtmlAttribute Prepend(HtmlAttribute newAttribute)
		{
			this.Insert(0, newAttribute);
			return newAttribute;
		}

		public void Remove(HtmlAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107246154));
			}
			int attributeIndex = this.GetAttributeIndex(attribute);
			if (attributeIndex == -1)
			{
				throw new IndexOutOfRangeException();
			}
			this.RemoveAt(attributeIndex);
		}

		public void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107372299));
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (string.Equals(this.items[i].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					this.RemoveAt(i);
				}
			}
		}

		public void RemoveAll()
		{
			this.Hashitems.Clear();
			this.items.Clear();
			this._ownernode.SetChanged();
		}

		public IEnumerable<HtmlAttribute> AttributesWithName(string attributeName)
		{
			HtmlAttributeCollection.<AttributesWithName>d__35 <AttributesWithName>d__ = new HtmlAttributeCollection.<AttributesWithName>d__35(-2);
			<AttributesWithName>d__.<>4__this = this;
			<AttributesWithName>d__.<>3__attributeName = attributeName;
			return <AttributesWithName>d__;
		}

		public void Remove()
		{
			this.items.Clear();
		}

		internal void Clear()
		{
			this.Hashitems.Clear();
			this.items.Clear();
		}

		internal int GetAttributeIndex(HtmlAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107246154));
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this.items[i] == attribute)
				{
					return i;
				}
			}
			return -1;
		}

		internal int GetAttributeIndex(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(HtmlAttributeCollection.getString_0(107372299));
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if (string.Equals(this.items[i].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		static HtmlAttributeCollection()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlAttributeCollection));
		}

		internal Dictionary<string, HtmlAttribute> Hashitems = new Dictionary<string, HtmlAttribute>(StringComparer.OrdinalIgnoreCase);

		private HtmlNode _ownernode;

		internal List<HtmlAttribute> items = new List<HtmlAttribute>();

		[NonSerialized]
		internal static GetString getString_0;
	}
}
