using System;
using System.Collections.Generic;
using System.Threading;

namespace SuperSocket.ClientEngine
{
	public sealed class ConcurrentBatchQueue<T> : IBatchQueue<T>
	{
		public ConcurrentBatchQueue() : this(16)
		{
		}

		public ConcurrentBatchQueue(int capacity) : this(new T[capacity])
		{
		}

		public ConcurrentBatchQueue(int capacity, Func<T, bool> nullValidator) : this(new T[capacity], nullValidator)
		{
		}

		public ConcurrentBatchQueue(T[] array) : this(array, (T t) => t == null)
		{
		}

		public ConcurrentBatchQueue(T[] array, Func<T, bool> nullValidator)
		{
			this.m_Entity = new ConcurrentBatchQueue<T>.Entity
			{
				Array = array
			};
			this.m_BackEntity = new ConcurrentBatchQueue<T>.Entity();
			this.m_BackEntity.Array = new T[array.Length];
			this.m_NullValidator = nullValidator;
		}

		public bool Enqueue(T item)
		{
			bool flag;
			while (!this.TryEnqueue(item, out flag) && !flag)
			{
			}
			return !flag;
		}

		private bool TryEnqueue(T item, out bool full)
		{
			full = false;
			ConcurrentBatchQueue<T>.Entity entity = this.m_Entity as ConcurrentBatchQueue<T>.Entity;
			T[] array = entity.Array;
			int count = entity.Count;
			if (count >= array.Length)
			{
				full = true;
				return false;
			}
			if (entity != this.m_Entity)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref entity.Count, count + 1, count) != count)
			{
				return false;
			}
			array[count] = item;
			return true;
		}

		public bool Enqueue(IList<T> items)
		{
			bool flag;
			while (!this.TryEnqueue(items, out flag) && !flag)
			{
			}
			return !flag;
		}

		private bool TryEnqueue(IList<T> items, out bool full)
		{
			full = false;
			ConcurrentBatchQueue<T>.Entity entity = this.m_Entity as ConcurrentBatchQueue<T>.Entity;
			T[] array = entity.Array;
			int count = entity.Count;
			int count2 = items.Count;
			int num = count + count2;
			if (num > array.Length)
			{
				full = true;
				return false;
			}
			if (entity != this.m_Entity)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref entity.Count, num, count) != count)
			{
				return false;
			}
			foreach (T t in items)
			{
				array[count++] = t;
			}
			return true;
		}

		public bool TryDequeue(IList<T> outputItems)
		{
			ConcurrentBatchQueue<T>.Entity entity = this.m_Entity as ConcurrentBatchQueue<T>.Entity;
			if (entity.Count <= 0)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref this.m_Entity, this.m_BackEntity, entity) != entity)
			{
				return false;
			}
			SpinWait spinWait = default(SpinWait);
			spinWait.SpinOnce();
			T[] array = entity.Array;
			int num = 0;
			for (;;)
			{
				T t = array[num];
				while (this.m_NullValidator(t))
				{
					spinWait.SpinOnce();
					t = array[num];
				}
				outputItems.Add(t);
				array[num] = ConcurrentBatchQueue<T>.m_Null;
				if (entity.Count <= num + 1)
				{
					break;
				}
				num++;
			}
			entity.Count = 0;
			this.m_BackEntity = entity;
			return true;
		}

		public bool IsEmpty
		{
			get
			{
				return this.Count <= 0;
			}
		}

		public int Count
		{
			get
			{
				return ((ConcurrentBatchQueue<T>.Entity)this.m_Entity).Count;
			}
		}

		private object m_Entity;

		private ConcurrentBatchQueue<T>.Entity m_BackEntity;

		private static readonly T m_Null;

		private Func<T, bool> m_NullValidator;

		private sealed class Entity
		{
			public T[] Array { get; set; }

			public int Count;
		}
	}
}
