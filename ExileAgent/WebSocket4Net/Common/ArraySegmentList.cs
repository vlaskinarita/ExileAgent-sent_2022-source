using System;
using System.Collections;
using System.Collections.Generic;
using SuperSocket.ClientEngine;

namespace WebSocket4Net.Common
{
	public class ArraySegmentList<T> : IEnumerable<!0>, IEnumerable, ICollection<T>, IList<T> where T : IEquatable<T>
	{
		internal IList<ArraySegmentEx<T>> Segments
		{
			get
			{
				return this.m_Segments;
			}
		}

		public ArraySegmentList()
		{
			this.m_Segments = new List<ArraySegmentEx<T>>();
		}

		private void CalculateSegmentsInfo(IList<ArraySegmentEx<T>> segments)
		{
			int num = 0;
			foreach (ArraySegmentEx<T> arraySegmentEx in segments)
			{
				if (arraySegmentEx.Count > 0)
				{
					arraySegmentEx.From = num;
					arraySegmentEx.To = num + arraySegmentEx.Count - 1;
					this.m_Segments.Add(arraySegmentEx);
					num += arraySegmentEx.Count;
				}
			}
			this.m_Count = num;
		}

		public int IndexOf(T item)
		{
			int num = 0;
			for (int i = 0; i < this.m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[i];
				int offset = arraySegmentEx.Offset;
				for (int j = 0; j < arraySegmentEx.Count; j++)
				{
					if (arraySegmentEx.Array[j + offset].Equals(item))
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public T this[int index]
		{
			get
			{
				ArraySegmentEx<T> arraySegmentEx;
				int elementInternalIndex = this.GetElementInternalIndex(index, out arraySegmentEx);
				if (elementInternalIndex < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return arraySegmentEx.Array[elementInternalIndex];
			}
			set
			{
				ArraySegmentEx<T> arraySegmentEx;
				int elementInternalIndex = this.GetElementInternalIndex(index, out arraySegmentEx);
				if (elementInternalIndex < 0)
				{
					throw new IndexOutOfRangeException();
				}
				arraySegmentEx.Array[elementInternalIndex] = value;
			}
		}

		private int GetElementInternalIndex(int index, out ArraySegmentEx<T> segment)
		{
			segment = null;
			if (index < 0 || index > this.Count - 1)
			{
				return -1;
			}
			if (index == 0)
			{
				this.m_PrevSegment = this.m_Segments[0];
				this.m_PrevSegmentIndex = 0;
				segment = this.m_PrevSegment;
				return this.m_PrevSegment.Offset;
			}
			int num = 0;
			if (this.m_PrevSegment != null)
			{
				if (index >= this.m_PrevSegment.From)
				{
					if (index <= this.m_PrevSegment.To)
					{
						segment = this.m_PrevSegment;
						return this.m_PrevSegment.Offset + index - this.m_PrevSegment.From;
					}
					num = 1;
				}
				else
				{
					num = -1;
				}
			}
			int num2;
			int to;
			if (num != 0)
			{
				num2 = this.m_PrevSegmentIndex + num;
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[num2];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segment = arraySegmentEx;
					return arraySegmentEx.Offset + index - arraySegmentEx.From;
				}
				num2 += num;
				ArraySegmentEx<T> arraySegmentEx2 = this.m_Segments[num2];
				if (index >= arraySegmentEx2.From && index <= arraySegmentEx2.To)
				{
					this.m_PrevSegment = arraySegmentEx2;
					this.m_PrevSegmentIndex = num2;
					segment = arraySegmentEx2;
					return arraySegmentEx2.Offset + index - arraySegmentEx2.From;
				}
				if (num > 0)
				{
					num2++;
					to = this.m_Segments.Count - 1;
				}
				else
				{
					int num3 = num2 - 1;
					num2 = 0;
					to = num3;
				}
			}
			else
			{
				num2 = 0;
				to = this.m_Segments.Count - 1;
			}
			int prevSegmentIndex = -1;
			ArraySegmentEx<T> arraySegmentEx3 = this.QuickSearchSegment(num2, to, index, out prevSegmentIndex);
			if (arraySegmentEx3 != null)
			{
				this.m_PrevSegment = arraySegmentEx3;
				this.m_PrevSegmentIndex = prevSegmentIndex;
				segment = this.m_PrevSegment;
				return arraySegmentEx3.Offset + index - arraySegmentEx3.From;
			}
			this.m_PrevSegment = null;
			return -1;
		}

		internal ArraySegmentEx<T> QuickSearchSegment(int from, int to, int index, out int segmentIndex)
		{
			segmentIndex = -1;
			int num = to - from;
			if (num == 0)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[from];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = from;
					return arraySegmentEx;
				}
				return null;
			}
			else if (num == 1)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[from];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = from;
					return arraySegmentEx;
				}
				arraySegmentEx = this.m_Segments[to];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = to;
					return arraySegmentEx;
				}
				return null;
			}
			else
			{
				int num2 = from + num / 2;
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[num2];
				if (index < arraySegmentEx.From)
				{
					return this.QuickSearchSegment(from, num2 - 1, index, out segmentIndex);
				}
				if (index <= arraySegmentEx.To)
				{
					segmentIndex = num2;
					return arraySegmentEx;
				}
				return this.QuickSearchSegment(num2 + 1, to, index, out segmentIndex);
			}
		}

		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(T item)
		{
			throw new NotSupportedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, 0, arrayIndex, Math.Min(array.Length, this.Count - arrayIndex));
		}

		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		public void RemoveSegmentAt(int index)
		{
			ArraySegmentEx<T> arraySegmentEx = this.m_Segments[index];
			int num = arraySegmentEx.To - arraySegmentEx.From + 1;
			this.m_Segments.RemoveAt(index);
			this.m_PrevSegment = null;
			if (index != this.m_Segments.Count)
			{
				for (int i = index; i < this.m_Segments.Count; i++)
				{
					this.m_Segments[i].From -= num;
					this.m_Segments[i].To -= num;
				}
			}
			this.m_Count -= num;
		}

		public void AddSegment(T[] array, int offset, int length)
		{
			this.AddSegment(array, offset, length, false);
		}

		public void AddSegment(T[] array, int offset, int length, bool toBeCopied)
		{
			if (length <= 0)
			{
				return;
			}
			int count = this.m_Count;
			ArraySegmentEx<T> arraySegmentEx;
			if (!toBeCopied)
			{
				arraySegmentEx = new ArraySegmentEx<T>(array, offset, length);
			}
			else
			{
				arraySegmentEx = new ArraySegmentEx<T>(array.CloneRange(offset, length), 0, length);
			}
			arraySegmentEx.From = count;
			this.m_Count = count + arraySegmentEx.Count;
			arraySegmentEx.To = this.m_Count - 1;
			this.m_Segments.Add(arraySegmentEx);
		}

		public int SegmentCount
		{
			get
			{
				return this.m_Segments.Count;
			}
		}

		public void ClearSegements()
		{
			this.m_Segments.Clear();
			this.m_PrevSegment = null;
			this.m_Count = 0;
		}

		public T[] ToArrayData()
		{
			return this.ToArrayData(0, this.m_Count);
		}

		public T[] ToArrayData(int startIndex, int length)
		{
			T[] array = new T[length];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (startIndex != 0)
			{
				ArraySegmentEx<T> arraySegmentEx = this.QuickSearchSegment(0, this.m_Segments.Count - 1, startIndex, out num3);
				num = startIndex - arraySegmentEx.From;
				if (arraySegmentEx == null)
				{
					throw new IndexOutOfRangeException();
				}
			}
			for (int i = num3; i < this.m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx2 = this.m_Segments[i];
				int num4 = Math.Min(arraySegmentEx2.Count - num, length - num2);
				Array.Copy(arraySegmentEx2.Array, arraySegmentEx2.Offset + num, array, num2, num4);
				num2 += num4;
				if (num2 >= length)
				{
					break;
				}
				num = 0;
			}
			return array;
		}

		public void TrimEnd(int trimSize)
		{
			if (trimSize <= 0)
			{
				return;
			}
			int num = this.Count - trimSize - 1;
			for (int i = this.m_Segments.Count - 1; i >= 0; i--)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[i];
				if (arraySegmentEx.From <= num && num < arraySegmentEx.To)
				{
					arraySegmentEx.To = num;
					this.m_Count -= trimSize;
					return;
				}
				this.RemoveSegmentAt(i);
			}
		}

		public int SearchLastSegment(SearchMarkState<T> state)
		{
			if (this.m_Segments.Count <= 0)
			{
				return -1;
			}
			ArraySegmentEx<T> arraySegmentEx = this.m_Segments[this.m_Segments.Count - 1];
			if (arraySegmentEx == null)
			{
				return -1;
			}
			int? num = arraySegmentEx.Array.SearchMark(arraySegmentEx.Offset, arraySegmentEx.Count, state.Mark);
			if (num == null)
			{
				return -1;
			}
			if (num.Value > 0)
			{
				state.Matched = 0;
				return num.Value - arraySegmentEx.Offset + arraySegmentEx.From;
			}
			state.Matched = 0 - num.Value;
			return -1;
		}

		public int CopyTo(T[] to)
		{
			return this.CopyTo(to, 0, 0, Math.Min(this.m_Count, to.Length));
		}

		public int CopyTo(T[] to, int srcIndex, int toIndex, int length)
		{
			int num = 0;
			int num2;
			ArraySegmentEx<T> arraySegmentEx;
			if (srcIndex > 0)
			{
				arraySegmentEx = this.QuickSearchSegment(0, this.m_Segments.Count - 1, srcIndex, out num2);
			}
			else
			{
				arraySegmentEx = this.m_Segments[0];
				num2 = 0;
			}
			int num3 = srcIndex - arraySegmentEx.From + arraySegmentEx.Offset;
			int num4 = Math.Min(arraySegmentEx.Count - num3 + arraySegmentEx.Offset, length - num);
			Array.Copy(arraySegmentEx.Array, num3, to, num + toIndex, num4);
			num += num4;
			if (num >= length)
			{
				return num;
			}
			for (int i = num2 + 1; i < this.m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx2 = this.m_Segments[i];
				num4 = Math.Min(arraySegmentEx2.Count, length - num);
				Array.Copy(arraySegmentEx2.Array, arraySegmentEx2.Offset, to, num + toIndex, num4);
				num += num4;
				if (num >= length)
				{
					break;
				}
			}
			return num;
		}

		private IList<ArraySegmentEx<T>> m_Segments;

		private ArraySegmentEx<T> m_PrevSegment;

		private int m_PrevSegmentIndex;

		private int m_Count;
	}
}
