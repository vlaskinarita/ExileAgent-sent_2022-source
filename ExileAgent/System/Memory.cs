using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ns20;

namespace System
{
	[IsReadOnlyAttribute1]
	[DebuggerTypeProxy(typeof(MemoryDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public struct Memory<T>
	{
		public Memory(T[] array)
		{
			if (array == null)
			{
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._object = array;
			this._index = 0;
			this._length = array.Length;
		}

		internal Memory(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = array.Length - start;
		}

		public Memory(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				this = default(Memory<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = array;
			this._index = start;
			this._length = length;
		}

		internal Memory(MemoryManager<T> manager, int length)
		{
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = int.MinValue;
			this._length = length;
		}

		internal Memory(MemoryManager<T> manager, int start, int length)
		{
			if (length < 0 || start < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			this._object = manager;
			this._index = (start | int.MinValue);
			this._length = length;
		}

		internal Memory(object obj, int start, int length)
		{
			this._object = obj;
			this._index = start;
			this._length = length;
		}

		public static implicit operator Memory<T>(T[] array)
		{
			return new Memory<T>(array);
		}

		public static implicit operator Memory<T>(ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		public unsafe static implicit operator ReadOnlyMemory<T>(Memory<T> memory)
		{
			return *Unsafe.As<Memory<T>, ReadOnlyMemory<T>>(ref memory);
		}

		public static Memory<T> Empty
		{
			get
			{
				return default(Memory<T>);
			}
		}

		public int Length
		{
			get
			{
				return this._length & int.MaxValue;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return (this._length & int.MaxValue) == 0;
			}
		}

		public override string ToString()
		{
			if (!(typeof(T) == typeof(char)))
			{
				return string.Format(Class401.smethod_0(107134151), typeof(T).Name, this._length & int.MaxValue);
			}
			string text;
			if ((text = (this._object as string)) == null)
			{
				return this.Span.ToString();
			}
			return text.Substring(this._index, this._length & int.MaxValue);
		}

		public Memory<T> Slice(int start)
		{
			int length = this._length;
			int num = length & int.MaxValue;
			if (start > num)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(this._object, this._index + start, length - start);
		}

		public Memory<T> Slice(int start, int length)
		{
			int length2 = this._length;
			int num = length2 & int.MaxValue;
			if (start > num || length > num - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(this._object, this._index + start, length | (length2 & int.MinValue));
		}

		public Span<T> Span
		{
			get
			{
				if (this._index < 0)
				{
					return ((MemoryManager<T>)this._object).GetSpan().Slice(this._index & int.MaxValue, this._length);
				}
				string text;
				if (typeof(T) == typeof(char) && (text = (this._object as string)) != null)
				{
					return new Span<T>(Unsafe.As<Pinnable<T>>(text), MemoryExtensions.StringAdjustment, text.Length).Slice(this._index, this._length);
				}
				if (this._object != null)
				{
					return new Span<T>((T[])this._object, this._index, this._length & int.MaxValue);
				}
				return default(Span<T>);
			}
		}

		public void CopyTo(Memory<T> destination)
		{
			this.Span.CopyTo(destination.Span);
		}

		public bool TryCopyTo(Memory<T> destination)
		{
			return this.Span.TryCopyTo(destination.Span);
		}

		public unsafe MemoryHandle Pin()
		{
			if (this._index < 0)
			{
				return ((MemoryManager<T>)this._object).Pin(this._index & int.MaxValue);
			}
			string value;
			if (typeof(T) == typeof(char) && (value = (this._object as string)) != null)
			{
				GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);
				void* pointer = Unsafe.Add<T>((void*)handle.AddrOfPinnedObject(), this._index);
				return new MemoryHandle(pointer, handle, null);
			}
			T[] array;
			if ((array = (this._object as T[])) == null)
			{
				return default(MemoryHandle);
			}
			if (this._length < 0)
			{
				void* pointer2 = Unsafe.Add<T>(Unsafe.AsPointer<T>(MemoryMarshal.GetReference<T>(array)), this._index);
				return new MemoryHandle(pointer2, default(GCHandle), null);
			}
			GCHandle handle2 = GCHandle.Alloc(array, GCHandleType.Pinned);
			void* pointer3 = Unsafe.Add<T>((void*)handle2.AddrOfPinnedObject(), this._index);
			return new MemoryHandle(pointer3, handle2, null);
		}

		public T[] ToArray()
		{
			return this.Span.ToArray();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is ReadOnlyMemory<T>)
			{
				return ((ReadOnlyMemory<T>)obj).Equals(this);
			}
			if (obj is Memory<T>)
			{
				Memory<T> other = (Memory<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		public bool Equals(Memory<T> other)
		{
			return this._object == other._object && this._index == other._index && this._length == other._length;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this._object == null)
			{
				return 0;
			}
			return Memory<T>.CombineHashCodes(this._object.GetHashCode(), this._index.GetHashCode(), this._length.GetHashCode());
		}

		private static int CombineHashCodes(int left, int right)
		{
			return (left << 5) + left ^ right;
		}

		private static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Memory<T>.CombineHashCodes(Memory<T>.CombineHashCodes(h1, h2), h3);
		}

		private readonly object _object;

		private readonly int _index;

		private readonly int _length;

		private const int RemoveFlagsBitMask = 2147483647;
	}
}
