using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns20;

namespace System
{
	[IsReadOnlyAttribute1]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	[DebuggerTypeProxy(typeof(SpanDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public ref struct Span<T>
	{
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		public static bool operator !=(Span<T> left, Span<T> right)
		{
			return !(left == right);
		}

		[Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(SR1.NotSupported_CannotCallEqualsOnSpan);
		}

		[Obsolete("GetHashCode() on Span will always throw an exception.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(SR1.NotSupported_CannotCallGetHashCodeOnSpan);
		}

		public static implicit operator Span<T>(T[] array)
		{
			return new Span<T>(array);
		}

		public static implicit operator Span<T>(ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		public static Span<T> Empty
		{
			get
			{
				return default(Span<T>);
			}
		}

		public Span<T>.Enumerator GetEnumerator()
		{
			return new Span<T>.Enumerator(this);
		}

		public Span(T[] array)
		{
			if (array == null)
			{
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			this._length = array.Length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment;
		}

		internal static Span<T> Create(T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(Span<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
			int length = array.Length - start;
			return new Span<T>(Unsafe.As<Pinnable<T>>(array), byteOffset, length);
		}

		public Span(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				this = default(Span<T>);
				return;
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = Unsafe.As<Pinnable<T>>(array);
			this._byteOffset = SpanHelpers.PerTypeValues<T>.ArrayAdjustment.Add(start);
		}

		[CLSCompliant(false)]
		public unsafe Span(void* pointer, int length)
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (length < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			this._length = length;
			this._pinnable = null;
			this._byteOffset = new IntPtr(pointer);
		}

		internal Span(Pinnable<T> pinnable, IntPtr byteOffset, int length)
		{
			this._length = length;
			this._pinnable = pinnable;
			this._byteOffset = byteOffset;
		}

		public T this[int index]
		{
			get
			{
				if (index >= this._length)
				{
					ThrowHelper.ThrowIndexOutOfRangeException();
				}
				if (this._pinnable == null)
				{
					return Unsafe.Add<T>(Unsafe.AsRef<T>(this._byteOffset.ToPointer()), index);
				}
				return Unsafe.Add<T>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset), index);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ref T GetPinnableReference()
		{
			if (this._length == 0)
			{
				return Unsafe.AsRef<T>(null);
			}
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		public unsafe void Clear()
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			UIntPtr byteLength = (UIntPtr)((ulong)length * (ulong)((long)Unsafe.SizeOf<T>()));
			if ((Unsafe.SizeOf<T>() & sizeof(IntPtr) - 1) != 0)
			{
				if (this._pinnable == null)
				{
					byte* ptr = (byte*)this._byteOffset.ToPointer();
					SpanHelpers.ClearLessThanPointerSized(ptr, byteLength);
					return;
				}
				ref byte b = ref Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
				SpanHelpers.ClearLessThanPointerSized(ref b, byteLength);
				return;
			}
			else
			{
				if (SpanHelpers.IsReferenceOrContainsReferences<T>())
				{
					UIntPtr pointerSizeLength = (UIntPtr)((ulong)((long)(length * Unsafe.SizeOf<T>() / sizeof(IntPtr))));
					ref IntPtr ip = ref Unsafe.As<T, IntPtr>(this.DangerousGetPinnableReference());
					SpanHelpers.ClearPointerSizedWithReferences(ref ip, pointerSizeLength);
					return;
				}
				ref byte b2 = ref Unsafe.As<T, byte>(this.DangerousGetPinnableReference());
				SpanHelpers.ClearPointerSizedWithoutReferences(ref b2, byteLength);
				return;
			}
		}

		public unsafe void Fill(T value)
		{
			int length = this._length;
			if (length == 0)
			{
				return;
			}
			if (Unsafe.SizeOf<T>() != 1)
			{
				ref T source = ref this.DangerousGetPinnableReference();
				int i;
				for (i = 0; i < (length & -8); i += 8)
				{
					*Unsafe.Add<T>(ref source, i) = value;
					*Unsafe.Add<T>(ref source, i + 1) = value;
					*Unsafe.Add<T>(ref source, i + 2) = value;
					*Unsafe.Add<T>(ref source, i + 3) = value;
					*Unsafe.Add<T>(ref source, i + 4) = value;
					*Unsafe.Add<T>(ref source, i + 5) = value;
					*Unsafe.Add<T>(ref source, i + 6) = value;
					*Unsafe.Add<T>(ref source, i + 7) = value;
				}
				if (i < (length & -4))
				{
					*Unsafe.Add<T>(ref source, i) = value;
					*Unsafe.Add<T>(ref source, i + 1) = value;
					*Unsafe.Add<T>(ref source, i + 2) = value;
					*Unsafe.Add<T>(ref source, i + 3) = value;
					i += 4;
				}
				while (i < length)
				{
					*Unsafe.Add<T>(ref source, i) = value;
					i++;
				}
				return;
			}
			byte value2 = *Unsafe.As<T, byte>(ref value);
			if (this._pinnable == null)
			{
				Unsafe.InitBlockUnaligned(this._byteOffset.ToPointer(), value2, (uint)length);
				return;
			}
			ref byte startAddress = ref Unsafe.As<T, byte>(Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset));
			Unsafe.InitBlockUnaligned(ref startAddress, value2, (uint)length);
		}

		public void CopyTo(Span<T> destination)
		{
			if (!this.TryCopyTo(destination))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
		}

		public bool TryCopyTo(Span<T> destination)
		{
			int length = this._length;
			int length2 = destination._length;
			if (length == 0)
			{
				return true;
			}
			if (length > length2)
			{
				return false;
			}
			ref T src = ref this.DangerousGetPinnableReference();
			ref T dst = ref destination.DangerousGetPinnableReference();
			SpanHelpers.CopyTo<T>(ref dst, length2, ref src, length);
			return true;
		}

		public static bool operator ==(Span<T> left, Span<T> right)
		{
			return left._length == right._length && Unsafe.AreSame<T>(left.DangerousGetPinnableReference(), right.DangerousGetPinnableReference());
		}

		public static implicit operator ReadOnlySpan<T>(Span<T> span)
		{
			return new ReadOnlySpan<T>(span._pinnable, span._byteOffset, span._length);
		}

		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				fixed (char* ptr = Unsafe.As<T, char>(this.DangerousGetPinnableReference()))
				{
					char* value = ptr;
					return new string(value, 0, this._length);
				}
			}
			return string.Format(Class401.smethod_0(107134064), typeof(T).Name, this._length);
		}

		public Span<T> Slice(int start)
		{
			if (start > this._length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = this._byteOffset.Add(start);
			int length = this._length - start;
			return new Span<T>(this._pinnable, byteOffset, length);
		}

		public Span<T> Slice(int start, int length)
		{
			if (start > this._length || length > this._length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			IntPtr byteOffset = this._byteOffset.Add(start);
			return new Span<T>(this._pinnable, byteOffset, length);
		}

		public T[] ToArray()
		{
			if (this._length == 0)
			{
				return SpanHelpers.PerTypeValues<T>.EmptyArray;
			}
			T[] array = new T[this._length];
			this.CopyTo(array);
			return array;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal ref T DangerousGetPinnableReference()
		{
			if (this._pinnable == null)
			{
				return Unsafe.AsRef<T>(this._byteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref this._pinnable.Data, this._byteOffset);
		}

		internal Pinnable<T> Pinnable
		{
			get
			{
				return this._pinnable;
			}
		}

		internal IntPtr ByteOffset
		{
			get
			{
				return this._byteOffset;
			}
		}

		private readonly Pinnable<T> _pinnable;

		private readonly IntPtr _byteOffset;

		private readonly int _length;

		public ref struct Enumerator
		{
			internal Enumerator(Span<T> span)
			{
				this._span = span;
				this._index = -1;
			}

			public bool MoveNext()
			{
				int num = this._index + 1;
				if (num < this._span.Length)
				{
					this._index = num;
					return true;
				}
				return false;
			}

			public ref T Current
			{
				get
				{
					return this._span[this._index];
				}
			}

			private readonly Span<T> _span;

			private int _index;
		}
	}
}
