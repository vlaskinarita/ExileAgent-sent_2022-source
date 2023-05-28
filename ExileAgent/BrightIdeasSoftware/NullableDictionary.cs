using System;
using System.Collections;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	internal sealed class NullableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		public new TValue this[TKey key]
		{
			get
			{
				if (key != null)
				{
					return base[key];
				}
				if (!this.hasNullKey)
				{
					throw new KeyNotFoundException();
				}
				return this.nullValue;
			}
			set
			{
				if (key == null)
				{
					this.hasNullKey = true;
					this.nullValue = value;
					return;
				}
				base[key] = value;
			}
		}

		public new bool ContainsKey(TKey key)
		{
			if (key != null)
			{
				return base.ContainsKey(key);
			}
			return this.hasNullKey;
		}

		public new IList Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList(base.Keys);
				if (this.hasNullKey)
				{
					arrayList.Add(null);
				}
				return arrayList;
			}
		}

		public new IList<TValue> Values
		{
			get
			{
				List<TValue> list = new List<TValue>(base.Values);
				if (this.hasNullKey)
				{
					list.Add(this.nullValue);
				}
				return list;
			}
		}

		private bool hasNullKey;

		private TValue nullValue;
	}
}
