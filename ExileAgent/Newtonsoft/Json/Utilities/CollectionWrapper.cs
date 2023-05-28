using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using ns20;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class CollectionWrapper<T> : IEnumerable<!0>, IEnumerable, ICollection<T>, IList, ICollection, IWrappedCollection
	{
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, Class401.smethod_0(107339463));
			ICollection<T> genericCollection;
			if ((genericCollection = (list as ICollection<T>)) != null)
			{
				this._genericCollection = genericCollection;
				return;
			}
			this._list = list;
		}

		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, Class401.smethod_0(107339463));
			this._genericCollection = list;
		}

		public void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		public void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		public bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		public bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		public IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> genericCollection = this._genericCollection;
			return (genericCollection ?? this._list.Cast<T>()).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable genericCollection = this._genericCollection;
			return (genericCollection ?? this._list).GetEnumerator();
		}

		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException(Class401.smethod_0(107339486));
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException(Class401.smethod_0(107339421));
			}
			this._list.RemoveAt(index);
		}

		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException(Class401.smethod_0(107339352));
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		object IList.this[int index]
		{
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException(Class401.smethod_0(107339287));
				}
				return this._list[index];
			}
			set
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException(Class401.smethod_0(107339287));
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
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

		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException(Class401.smethod_0(107339734).FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), Class401.smethod_0(107447549));
			}
		}

		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		public object UnderlyingCollection
		{
			get
			{
				return this._genericCollection ?? this._list;
			}
		}

		private readonly IList _list;

		private readonly ICollection<T> _genericCollection;

		private object _syncRoot;
	}
}
