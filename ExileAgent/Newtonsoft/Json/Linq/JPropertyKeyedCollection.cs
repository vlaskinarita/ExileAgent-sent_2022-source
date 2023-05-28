using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	internal sealed class JPropertyKeyedCollection : Collection<JToken>
	{
		public JPropertyKeyedCollection() : base(new List<JToken>())
		{
		}

		private void AddKey(string key, JToken item)
		{
			this.EnsureDictionary();
			this._dictionary[key] = item;
		}

		protected void ChangeItemKey(JToken item, string newKey)
		{
			if (!this.ContainsItem(item))
			{
				throw new ArgumentException(JPropertyKeyedCollection.getString_0(107292453));
			}
			string keyForItem = this.GetKeyForItem(item);
			if (!JPropertyKeyedCollection.Comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		protected override void ClearItems()
		{
			base.ClearItems();
			Dictionary<string, JToken> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Clear();
		}

		public bool Contains(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException(JPropertyKeyedCollection.getString_0(107454154));
			}
			return this._dictionary != null && this._dictionary.ContainsKey(key);
		}

		private bool ContainsItem(JToken item)
		{
			if (this._dictionary == null)
			{
				return false;
			}
			string keyForItem = this.GetKeyForItem(item);
			JToken jtoken;
			return this._dictionary.TryGetValue(keyForItem, out jtoken);
		}

		private void EnsureDictionary()
		{
			if (this._dictionary == null)
			{
				this._dictionary = new Dictionary<string, JToken>(JPropertyKeyedCollection.Comparer);
			}
		}

		private string GetKeyForItem(JToken item)
		{
			return ((JProperty)item).Name;
		}

		protected override void InsertItem(int index, JToken item)
		{
			this.AddKey(this.GetKeyForItem(item), item);
			base.InsertItem(index, item);
		}

		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException(JPropertyKeyedCollection.getString_0(107454154));
			}
			return this._dictionary != null && this._dictionary.ContainsKey(key) && base.Remove(this._dictionary[key]);
		}

		protected override void RemoveItem(int index)
		{
			string keyForItem = this.GetKeyForItem(base.Items[index]);
			this.RemoveKey(keyForItem);
			base.RemoveItem(index);
		}

		private void RemoveKey(string key)
		{
			Dictionary<string, JToken> dictionary = this._dictionary;
			if (dictionary == null)
			{
				return;
			}
			dictionary.Remove(key);
		}

		protected override void SetItem(int index, JToken item)
		{
			string keyForItem = this.GetKeyForItem(item);
			string keyForItem2 = this.GetKeyForItem(base.Items[index]);
			if (JPropertyKeyedCollection.Comparer.Equals(keyForItem2, keyForItem))
			{
				if (this._dictionary != null)
				{
					this._dictionary[keyForItem] = item;
				}
			}
			else
			{
				this.AddKey(keyForItem, item);
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		public JToken this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException(JPropertyKeyedCollection.getString_0(107454154));
				}
				if (this._dictionary == null)
				{
					throw new KeyNotFoundException();
				}
				return this._dictionary[key];
			}
		}

		public bool TryGetValue(string key, out JToken value)
		{
			if (this._dictionary == null)
			{
				value = null;
				return false;
			}
			return this._dictionary.TryGetValue(key, out value);
		}

		public ICollection<string> Keys
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Keys;
			}
		}

		public ICollection<JToken> Values
		{
			get
			{
				this.EnsureDictionary();
				return this._dictionary.Values;
			}
		}

		public int IndexOfReference(JToken t)
		{
			return ((List<JToken>)base.Items).IndexOfReference(t);
		}

		public bool Compare(JPropertyKeyedCollection other)
		{
			if (this == other)
			{
				return true;
			}
			Dictionary<string, JToken> dictionary = this._dictionary;
			Dictionary<string, JToken> dictionary2 = other._dictionary;
			if (dictionary == null && dictionary2 == null)
			{
				return true;
			}
			if (dictionary == null)
			{
				return dictionary2.Count == 0;
			}
			if (dictionary2 == null)
			{
				return dictionary.Count == 0;
			}
			if (dictionary.Count != dictionary2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in dictionary)
			{
				JToken jtoken;
				if (!dictionary2.TryGetValue(keyValuePair.Key, out jtoken))
				{
					return false;
				}
				JProperty jproperty = (JProperty)keyValuePair.Value;
				JProperty jproperty2 = (JProperty)jtoken;
				if (jproperty.Value == null)
				{
					return jproperty2.Value == null;
				}
				if (!jproperty.Value.DeepEquals(jproperty2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JPropertyKeyedCollection()
		{
			Strings.CreateGetStringDelegate(typeof(JPropertyKeyedCollection));
			JPropertyKeyedCollection.Comparer = StringComparer.Ordinal;
		}

		private static readonly IEqualityComparer<string> Comparer;

		private Dictionary<string, JToken> _dictionary;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
