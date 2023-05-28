using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	public static class BuffersExtensions
	{
		public static SequencePosition? PositionOf<T>([IsReadOnlyAttribute1] [In] this ReadOnlySequence<T> source, T value) where T : IEquatable<T>
		{
			if (!source.IsSingleSegment)
			{
				return BuffersExtensions.PositionOfMultiSegment<T>(ref source, value);
			}
			int num = source.First.Span.IndexOf(value);
			if (num != -1)
			{
				return new SequencePosition?(source.GetPosition((long)num));
			}
			return null;
		}

		private static SequencePosition? PositionOfMultiSegment<T>([IsReadOnlyAttribute1] [In] ref ReadOnlySequence<T> source, T value) where T : IEquatable<T>
		{
			SequencePosition start = source.Start;
			SequencePosition origin = start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (source.TryGet(ref start, out readOnlyMemory, true))
			{
				int num = readOnlyMemory.Span.IndexOf(value);
				if (num != -1)
				{
					return new SequencePosition?(source.GetPosition((long)num, origin));
				}
				if (start.GetObject() == null)
				{
					break;
				}
				origin = start;
			}
			return null;
		}

		public static void CopyTo<T>([IsReadOnlyAttribute1] [In] this ReadOnlySequence<T> source, Span<T> destination)
		{
			if (source.Length > (long)destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.destination);
			}
			if (source.IsSingleSegment)
			{
				source.First.Span.CopyTo(destination);
				return;
			}
			BuffersExtensions.CopyToMultiSegment<T>(ref source, destination);
		}

		private static void CopyToMultiSegment<T>([IsReadOnlyAttribute1] [In] ref ReadOnlySequence<T> sequence, Span<T> destination)
		{
			SequencePosition start = sequence.Start;
			ReadOnlyMemory<T> readOnlyMemory;
			while (sequence.TryGet(ref start, out readOnlyMemory, true))
			{
				ReadOnlySpan<T> span = readOnlyMemory.Span;
				span.CopyTo(destination);
				if (start.GetObject() == null)
				{
					break;
				}
				destination = destination.Slice(span.Length);
			}
		}

		public static T[] ToArray<T>([IsReadOnlyAttribute1] [In] this ReadOnlySequence<T> sequence)
		{
			T[] array = new T[sequence.Length];
			ref sequence.CopyTo(array);
			return array;
		}

		public static void Write<T>(this IBufferWriter<T> writer, ReadOnlySpan<T> value)
		{
			Span<T> span = writer.GetSpan(0);
			if (value.Length <= span.Length)
			{
				value.CopyTo(span);
				writer.Advance(value.Length);
				return;
			}
			BuffersExtensions.WriteMultiSegment<T>(writer, ref value, span);
		}

		private static void WriteMultiSegment<T>(IBufferWriter<T> writer, [IsReadOnlyAttribute1] [In] ref ReadOnlySpan<T> source, Span<T> destination)
		{
			ReadOnlySpan<T> readOnlySpan = source;
			for (;;)
			{
				int num = Math.Min(destination.Length, readOnlySpan.Length);
				readOnlySpan.Slice(0, num).CopyTo(destination);
				writer.Advance(num);
				readOnlySpan = readOnlySpan.Slice(num);
				if (readOnlySpan.Length <= 0)
				{
					break;
				}
				destination = writer.GetSpan(readOnlySpan.Length);
			}
		}
	}
}
