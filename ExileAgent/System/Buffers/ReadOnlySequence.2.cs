using System;

namespace System.Buffers
{
	internal static class ReadOnlySequence
	{
		public static int SegmentToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		public static int SegmentToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		public static int ArrayToSequenceStart(int startIndex)
		{
			return startIndex | 0;
		}

		public static int ArrayToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		public static int MemoryManagerToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		public static int MemoryManagerToSequenceEnd(int endIndex)
		{
			return endIndex | 0;
		}

		public static int StringToSequenceStart(int startIndex)
		{
			return startIndex | int.MinValue;
		}

		public static int StringToSequenceEnd(int endIndex)
		{
			return endIndex | int.MinValue;
		}

		public const int FlagBitMask = -2147483648;

		public const int IndexBitMask = 2147483647;

		public const int SegmentStartMask = 0;

		public const int SegmentEndMask = 0;

		public const int ArrayStartMask = 0;

		public const int ArrayEndMask = -2147483648;

		public const int MemoryManagerStartMask = -2147483648;

		public const int MemoryManagerEndMask = 0;

		public const int StringStartMask = -2147483648;

		public const int StringEndMask = -2147483648;
	}
}
