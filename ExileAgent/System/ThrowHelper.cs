using System;
using System.Buffers;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace System
{
	internal static class ThrowHelper
	{
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		internal static void ThrowArrayTypeMismatchException()
		{
			throw ThrowHelper.CreateArrayTypeMismatchException();
		}

		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		internal static void ThrowArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			throw ThrowHelper.CreateArgumentException_InvalidTypeWithPointersNotSupported(type);
		}

		private static Exception CreateArgumentException_InvalidTypeWithPointersNotSupported(Type type)
		{
			return new ArgumentException(SR1.Format(SR1.Argument_InvalidTypeWithPointersNotSupported, type));
		}

		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException(SR1.Argument_DestinationTooShort);
		}

		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		internal static void ThrowArgumentOutOfRangeException()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException();
		}

		private static Exception CreateArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		internal static void ThrowArgumentOutOfRangeException_PrecisionTooLarge()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PrecisionTooLarge();
		}

		private static Exception CreateArgumentOutOfRangeException_PrecisionTooLarge()
		{
			return new ArgumentOutOfRangeException(ThrowHelper.getString_0(107144200), SR1.Format(SR1.Argument_PrecisionTooLarge, 99));
		}

		internal static void ThrowArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_SymbolDoesNotFit();
		}

		private static Exception CreateArgumentOutOfRangeException_SymbolDoesNotFit()
		{
			return new ArgumentOutOfRangeException(ThrowHelper.getString_0(107144155), SR1.Argument_BadFormatSpecifier);
		}

		internal static void ThrowInvalidOperationException()
		{
			throw ThrowHelper.CreateInvalidOperationException();
		}

		private static Exception CreateInvalidOperationException()
		{
			return new InvalidOperationException();
		}

		internal static void ThrowInvalidOperationException_OutstandingReferences()
		{
			throw ThrowHelper.CreateInvalidOperationException_OutstandingReferences();
		}

		private static Exception CreateInvalidOperationException_OutstandingReferences()
		{
			return new InvalidOperationException(SR1.OutstandingReferences);
		}

		internal static void ThrowInvalidOperationException_UnexpectedSegmentType()
		{
			throw ThrowHelper.CreateInvalidOperationException_UnexpectedSegmentType();
		}

		private static Exception CreateInvalidOperationException_UnexpectedSegmentType()
		{
			return new InvalidOperationException(SR1.UnexpectedSegmentType);
		}

		internal static void ThrowInvalidOperationException_EndPositionNotReached()
		{
			throw ThrowHelper.CreateInvalidOperationException_EndPositionNotReached();
		}

		private static Exception CreateInvalidOperationException_EndPositionNotReached()
		{
			return new InvalidOperationException(SR1.EndPositionNotReached);
		}

		internal static void ThrowArgumentOutOfRangeException_PositionOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_PositionOutOfRange();
		}

		private static Exception CreateArgumentOutOfRangeException_PositionOutOfRange()
		{
			return new ArgumentOutOfRangeException(ThrowHelper.getString_0(107144146));
		}

		internal static void ThrowArgumentOutOfRangeException_OffsetOutOfRange()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException_OffsetOutOfRange();
		}

		private static Exception CreateArgumentOutOfRangeException_OffsetOutOfRange()
		{
			return new ArgumentOutOfRangeException(ThrowHelper.getString_0(107144165));
		}

		internal static void ThrowObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			throw ThrowHelper.CreateObjectDisposedException_ArrayMemoryPoolBuffer();
		}

		private static Exception CreateObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			return new ObjectDisposedException(ThrowHelper.getString_0(107144124));
		}

		internal static void ThrowFormatException_BadFormatSpecifier()
		{
			throw ThrowHelper.CreateFormatException_BadFormatSpecifier();
		}

		private static Exception CreateFormatException_BadFormatSpecifier()
		{
			return new FormatException(SR1.Argument_BadFormatSpecifier);
		}

		internal static void ThrowArgumentException_OverlapAlignmentMismatch()
		{
			throw ThrowHelper.CreateArgumentException_OverlapAlignmentMismatch();
		}

		private static Exception CreateArgumentException_OverlapAlignmentMismatch()
		{
			return new ArgumentException(SR1.Argument_OverlapAlignmentMismatch);
		}

		internal static void ThrowNotSupportedException()
		{
			throw ThrowHelper.CreateThrowNotSupportedException();
		}

		private static Exception CreateThrowNotSupportedException()
		{
			return new NotSupportedException();
		}

		public static bool TryFormatThrowFormatException(out int bytesWritten)
		{
			bytesWritten = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		public static bool TryParseThrowFormatException<T>(out T value, out int bytesConsumed)
		{
			value = default(T);
			bytesConsumed = 0;
			ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			return false;
		}

		public static void ThrowArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			throw ThrowHelper.CreateArgumentValidationException<T>(startSegment, startIndex, endSegment);
		}

		private static Exception CreateArgumentValidationException<T>(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment)
		{
			if (startSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.startSegment);
			}
			if (endSegment == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.endSegment);
			}
			if (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endSegment);
			}
			if (startSegment.Memory.Length < startIndex)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.startIndex);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.endIndex);
		}

		public static void ThrowArgumentValidationException(Array array, int start)
		{
			throw ThrowHelper.CreateArgumentValidationException(array, start);
		}

		private static Exception CreateArgumentValidationException(Array array, int start)
		{
			if (array == null)
			{
				return ThrowHelper.CreateArgumentNullException(ExceptionArgument.array);
			}
			if (start > array.Length)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}

		public static void ThrowStartOrEndArgumentValidationException(long start)
		{
			throw ThrowHelper.CreateStartOrEndArgumentValidationException(start);
		}

		private static Exception CreateStartOrEndArgumentValidationException(long start)
		{
			if (start < 0L)
			{
				return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return ThrowHelper.CreateArgumentOutOfRangeException(ExceptionArgument.length);
		}

		static ThrowHelper()
		{
			Strings.CreateGetStringDelegate(typeof(ThrowHelper));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
