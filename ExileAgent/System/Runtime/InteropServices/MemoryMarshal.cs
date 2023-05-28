using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	public static class MemoryMarshal
	{
		public static bool TryGetArray<T>(ReadOnlyMemory<T> memory, out ArraySegment<T> segment)
		{
			int num;
			int num2;
			object objectStartLength = memory.GetObjectStartLength(out num, out num2);
			T[] array;
			if (num < 0)
			{
				ArraySegment<T> arraySegment;
				if (((MemoryManager<T>)objectStartLength).TryGetArray(out arraySegment))
				{
					segment = new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + (num & int.MaxValue), num2);
					return true;
				}
			}
			else if ((array = (objectStartLength as T[])) != null)
			{
				segment = new ArraySegment<T>(array, num, num2 & int.MaxValue);
				return true;
			}
			if ((num2 & 2147483647) == 0)
			{
				segment = new ArraySegment<T>(SpanHelpers.PerTypeValues<T>.EmptyArray);
				return true;
			}
			segment = default(ArraySegment<T>);
			return false;
		}

		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager) where TManager : MemoryManager<T>
		{
			int num;
			int num2;
			manager = (memory.GetObjectStartLength(out num, out num2) as TManager);
			return manager != null;
		}

		public static bool TryGetMemoryManager<T, TManager>(ReadOnlyMemory<T> memory, out TManager manager, out int start, out int length) where TManager : MemoryManager<T>
		{
			manager = (memory.GetObjectStartLength(out start, out length) as TManager);
			start &= int.MaxValue;
			if (manager == null)
			{
				start = 0;
				length = 0;
				return false;
			}
			return true;
		}

		public static IEnumerable<T> ToEnumerable<T>(ReadOnlyMemory<T> memory)
		{
			MemoryMarshal.<ToEnumerable>d__3<T> <ToEnumerable>d__ = new MemoryMarshal.<ToEnumerable>d__3<T>(-2);
			<ToEnumerable>d__.<>3__memory = memory;
			return <ToEnumerable>d__;
		}

		public static bool TryGetString(ReadOnlyMemory<char> memory, out string text, out int start, out int length)
		{
			int num;
			int num2;
			string text2;
			if ((text2 = (memory.GetObjectStartLength(out num, out num2) as string)) != null)
			{
				text = text2;
				start = num;
				length = num2;
				return true;
			}
			text = null;
			start = 0;
			length = 0;
			return false;
		}

		public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > source.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
		}

		public static bool TryRead<T>(ReadOnlySpan<byte> source, out T value) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)source.Length))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
			return true;
		}

		public static void Write<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
		}

		public static bool TryWrite<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)destination.Length))
			{
				return false;
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		public static Memory<T> CreateFromPinnedArray<T>(T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Memory<T>);
			}
			if (default(T) == null && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(array, start, length | int.MinValue);
		}

		public static Span<byte> AsBytes<T>(Span<T> span) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			int length = checked(span.Length * Unsafe.SizeOf<T>());
			return new Span<byte>(Unsafe.As<Pinnable<byte>>(span.Pinnable), span.ByteOffset, length);
		}

		public static ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span) where T : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(T));
			}
			int length = checked(span.Length * Unsafe.SizeOf<T>());
			return new ReadOnlySpan<byte>(Unsafe.As<Pinnable<byte>>(span.Pinnable), span.ByteOffset, length);
		}

		public unsafe static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory)
		{
			return *Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
		}

		public static ref T GetReference<T>(Span<T> span)
		{
			if (span.Pinnable == null)
			{
				return Unsafe.AsRef<T>(span.ByteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref span.Pinnable.Data, span.ByteOffset);
		}

		public static ref T GetReference<T>(ReadOnlySpan<T> span)
		{
			if (span.Pinnable == null)
			{
				return Unsafe.AsRef<T>(span.ByteOffset.ToPointer());
			}
			return Unsafe.AddByteOffset<T>(ref span.Pinnable.Data, span.ByteOffset);
		}

		public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (SpanHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			int length = checked((int)(unchecked((long)span.Length) * unchecked((long)Unsafe.SizeOf<TFrom>()) / unchecked((long)Unsafe.SizeOf<TTo>())));
			return new Span<TTo>(Unsafe.As<Pinnable<TTo>>(span.Pinnable), span.ByteOffset, length);
		}

		public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (SpanHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (SpanHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowArgumentException_InvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			int length = checked((int)(unchecked((long)span.Length) * unchecked((long)Unsafe.SizeOf<TFrom>()) / unchecked((long)Unsafe.SizeOf<TTo>())));
			return new ReadOnlySpan<TTo>(Unsafe.As<Pinnable<TTo>>(span.Pinnable), span.ByteOffset, length);
		}
	}
}
