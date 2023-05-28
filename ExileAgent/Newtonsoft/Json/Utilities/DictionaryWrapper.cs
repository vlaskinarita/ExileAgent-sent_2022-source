using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ns20;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class DictionaryWrapper<TKey, TValue> : IEnumerable, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, ICollection, IDictionary, IWrappedDictionary
	{
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, Class401.smethod_0(107338353));
			this._dictionary = dictionary;
		}

		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, Class401.smethod_0(107338353));
			this._genericDictionary = dictionary;
		}

		public DictionaryWrapper(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, Class401.smethod_0(107338353));
			this._readOnlyDictionary = dictionary;
		}

		public void Add(TKey key, TValue value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._genericDictionary == null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Add(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary.Contains(key);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey(key);
			}
			return this._genericDictionary.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Keys.Cast<TKey>().ToList<TKey>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Keys.ToList<TKey>();
				}
				return this._genericDictionary.Keys;
			}
		}

		public bool Remove(TKey key)
		{
			if (this._dictionary != null)
			{
				if (this._dictionary.Contains(key))
				{
					this._dictionary.Remove(key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this._genericDictionary.Remove(key);
			}
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(key))
				{
					value = default(TValue);
					return false;
				}
				value = (TValue)((object)this._dictionary[key]);
				return true;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this._genericDictionary.TryGetValue(key, out value);
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Values.Cast<TValue>().ToList<TValue>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Values.ToList<TValue>();
				}
				return this._genericDictionary.Values;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return (TValue)((object)this._dictionary[key]);
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[key];
				}
				return this._genericDictionary[key];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this._genericDictionary[key] = value;
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				((IList)this._dictionary).Add(item);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			IDictionary<TKey, TValue> genericDictionary = this._genericDictionary;
			if (genericDictionary == null)
			{
				return;
			}
			genericDictionary.Add(item);
		}

		public void Clear()
		{
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.Contains(item);
			}
			return this._genericDictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry entry = enumerator.Entry;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value));
					}
					return;
				}
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Count;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Count;
				}
				return this._genericDictionary.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this._readOnlyDictionary != null || this._genericDictionary.IsReadOnly;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				if (!this._dictionary.Contains(item.Key))
				{
					return true;
				}
				if (object.Equals(this._dictionary[item.Key], item.Value))
				{
					this._dictionary.Remove(item.Key);
					return true;
				}
				return false;
			}
			else
			{
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				return this._genericDictionary.Remove(item);
			}
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return (from DictionaryEntry de in this._dictionary
				select new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.GetEnumerator();
			}
			return this._genericDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		void IDictionary.Add(object key, object value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		object IDictionary.this[object key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary[(TKey)((object)key)];
				}
				return this._genericDictionary[(TKey)((object)key)];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				if (this._readOnlyDictionary != null)
				{
					throw new NotSupportedException();
				}
				this._genericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			if (this._readOnlyDictionary != null)
			{
				return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._readOnlyDictionary.GetEnumerator());
			}
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._genericDictionary.GetEnumerator());
		}

		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			if (this._readOnlyDictionary != null)
			{
				return this._readOnlyDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && (this._readOnlyDictionary != null || this._dictionary.IsFixedSize);
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys.ToList<TKey>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Keys.ToList<TKey>();
				}
				return this._dictionary.Keys;
			}
		}

		public void Remove(object key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.Remove((TKey)((object)key));
		}

		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values.ToList<TValue>();
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary.Values.ToList<TValue>();
				}
				return this._dictionary.Values;
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			if (this._dictionary != null)
			{
				this._dictionary.CopyTo(array, index);
				return;
			}
			if (this._readOnlyDictionary != null)
			{
				throw new NotSupportedException();
			}
			this._genericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		public object UnderlyingDictionary
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary;
				}
				if (this._readOnlyDictionary != null)
				{
					return this._readOnlyDictionary;
				}
				return this._genericDictionary;
			}
		}

		private readonly IDictionary _dictionary;

		private readonly IDictionary<TKey, TValue> _genericDictionary;

		private readonly IReadOnlyDictionary<TKey, TValue> _readOnlyDictionary;

		private object _syncRoot;

		private readonly struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : IEnumerator, IDictionaryEnumerator
		{
			public DictionaryEnumerator(IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, Class401.smethod_0(107310222));
				this._e = e;
			}

			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object key = keyValuePair.Key;
					keyValuePair = this._e.Current;
					return new DictionaryEntry(key, keyValuePair.Value);
				}
			}

			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			public void Reset()
			{
				this._e.Reset();
			}

			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}
