using System;
using System.Collections;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class MixedCodeDocumentFragmentList : IEnumerable
	{
		internal MixedCodeDocumentFragmentList(MixedCodeDocument doc)
		{
			this._doc = doc;
		}

		public MixedCodeDocument Doc
		{
			get
			{
				return this._doc;
			}
		}

		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		public MixedCodeDocumentFragment this[int index]
		{
			get
			{
				return this._items[index];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Append(MixedCodeDocumentFragment newFragment)
		{
			if (newFragment == null)
			{
				throw new ArgumentNullException(MixedCodeDocumentFragmentList.getString_0(107320640));
			}
			this._items.Add(newFragment);
		}

		public MixedCodeDocumentFragmentList.MixedCodeDocumentFragmentEnumerator GetEnumerator()
		{
			return new MixedCodeDocumentFragmentList.MixedCodeDocumentFragmentEnumerator(this._items);
		}

		public void Prepend(MixedCodeDocumentFragment newFragment)
		{
			if (newFragment == null)
			{
				throw new ArgumentNullException(MixedCodeDocumentFragmentList.getString_0(107320640));
			}
			this._items.Insert(0, newFragment);
		}

		public void Remove(MixedCodeDocumentFragment fragment)
		{
			if (fragment == null)
			{
				throw new ArgumentNullException(MixedCodeDocumentFragmentList.getString_0(107320655));
			}
			int fragmentIndex = this.GetFragmentIndex(fragment);
			if (fragmentIndex == -1)
			{
				throw new IndexOutOfRangeException();
			}
			this.RemoveAt(fragmentIndex);
		}

		public void RemoveAll()
		{
			this._items.Clear();
		}

		public void RemoveAt(int index)
		{
			this._items.RemoveAt(index);
		}

		internal void Clear()
		{
			this._items.Clear();
		}

		internal int GetFragmentIndex(MixedCodeDocumentFragment fragment)
		{
			if (fragment == null)
			{
				throw new ArgumentNullException(MixedCodeDocumentFragmentList.getString_0(107320655));
			}
			for (int i = 0; i < this._items.Count; i++)
			{
				if (this._items[i] == fragment)
				{
					return i;
				}
			}
			return -1;
		}

		static MixedCodeDocumentFragmentList()
		{
			Strings.CreateGetStringDelegate(typeof(MixedCodeDocumentFragmentList));
		}

		private MixedCodeDocument _doc;

		private IList<MixedCodeDocumentFragment> _items = new List<MixedCodeDocumentFragment>();

		[NonSerialized]
		internal static GetString getString_0;

		public sealed class MixedCodeDocumentFragmentEnumerator : IEnumerator
		{
			internal MixedCodeDocumentFragmentEnumerator(IList<MixedCodeDocumentFragment> items)
			{
				this._items = items;
				this._index = -1;
			}

			public MixedCodeDocumentFragment Current
			{
				get
				{
					return this._items[this._index];
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			public bool MoveNext()
			{
				this._index++;
				return this._index < this._items.Count;
			}

			public void Reset()
			{
				this._index = -1;
			}

			private int _index;

			private IList<MixedCodeDocumentFragment> _items;
		}
	}
}
