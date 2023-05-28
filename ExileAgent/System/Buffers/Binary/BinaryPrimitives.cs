using System;
using System.Runtime.InteropServices;

namespace System.Buffers.Binary
{
	public static class BinaryPrimitives
	{
		[CLSCompliant(false)]
		public static sbyte ReverseEndianness(sbyte value)
		{
			return value;
		}

		public static short ReverseEndianness(short value)
		{
			return (short)((int)(value & 255) << 8 | ((int)value & 65280) >> 8);
		}

		public static int ReverseEndianness(int value)
		{
			return (int)BinaryPrimitives.ReverseEndianness((uint)value);
		}

		public static long ReverseEndianness(long value)
		{
			return (long)BinaryPrimitives.ReverseEndianness((ulong)value);
		}

		public static byte ReverseEndianness(byte value)
		{
			return value;
		}

		[CLSCompliant(false)]
		public static ushort ReverseEndianness(ushort value)
		{
			return (ushort)((value >> 8) + ((int)value << 8));
		}

		[CLSCompliant(false)]
		public static uint ReverseEndianness(uint value)
		{
			uint num = value & 16711935U;
			uint num2 = value & 4278255360U;
			return (num >> 8 | num << 24) + (num2 << 8 | num2 >> 24);
		}

		[CLSCompliant(false)]
		public static ulong ReverseEndianness(ulong value)
		{
			return ((ulong)BinaryPrimitives.ReverseEndianness((uint)value) << 32) + (ulong)BinaryPrimitives.ReverseEndianness((uint)(value >> 32));
		}

		public static short ReadInt16BigEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		public static int ReadInt32BigEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		public static long ReadInt64BigEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		[CLSCompliant(false)]
		public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		[CLSCompliant(false)]
		public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		[CLSCompliant(false)]
		public static ulong ReadUInt64BigEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		public static bool TryReadInt16BigEndian(ReadOnlySpan<byte> source, out short value)
		{
			bool result = MemoryMarshal.TryRead<short>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		public static bool TryReadInt32BigEndian(ReadOnlySpan<byte> source, out int value)
		{
			bool result = MemoryMarshal.TryRead<int>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		public static bool TryReadInt64BigEndian(ReadOnlySpan<byte> source, out long value)
		{
			bool result = MemoryMarshal.TryRead<long>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		[CLSCompliant(false)]
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			bool result = MemoryMarshal.TryRead<ushort>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		[CLSCompliant(false)]
		public static bool TryReadUInt32BigEndian(ReadOnlySpan<byte> source, out uint value)
		{
			bool result = MemoryMarshal.TryRead<uint>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		[CLSCompliant(false)]
		public static bool TryReadUInt64BigEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			bool result = MemoryMarshal.TryRead<ulong>(source, out value);
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		public static short ReadInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		public static int ReadInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		public static long ReadInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		[CLSCompliant(false)]
		public static ushort ReadUInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		[CLSCompliant(false)]
		public static uint ReadUInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		[CLSCompliant(false)]
		public static ulong ReadUInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		public static bool TryReadInt16LittleEndian(ReadOnlySpan<byte> source, out short value)
		{
			bool result = MemoryMarshal.TryRead<short>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		public static bool TryReadInt32LittleEndian(ReadOnlySpan<byte> source, out int value)
		{
			bool result = MemoryMarshal.TryRead<int>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		public static bool TryReadInt64LittleEndian(ReadOnlySpan<byte> source, out long value)
		{
			bool result = MemoryMarshal.TryRead<long>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		[CLSCompliant(false)]
		public static bool TryReadUInt16LittleEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			bool result = MemoryMarshal.TryRead<ushort>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		[CLSCompliant(false)]
		public static bool TryReadUInt32LittleEndian(ReadOnlySpan<byte> source, out uint value)
		{
			bool result = MemoryMarshal.TryRead<uint>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		[CLSCompliant(false)]
		public static bool TryReadUInt64LittleEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			bool result = MemoryMarshal.TryRead<ulong>(source, out value);
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return result;
		}

		public static void WriteInt16BigEndian(Span<byte> destination, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<short>(destination, ref value);
		}

		public static void WriteInt32BigEndian(Span<byte> destination, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<int>(destination, ref value);
		}

		public static void WriteInt64BigEndian(Span<byte> destination, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<long>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static void WriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ushort>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static void WriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<uint>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static void WriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ulong>(destination, ref value);
		}

		public static bool TryWriteInt16BigEndian(Span<byte> destination, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<short>(destination, ref value);
		}

		public static bool TryWriteInt32BigEndian(Span<byte> destination, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<int>(destination, ref value);
		}

		public static bool TryWriteInt64BigEndian(Span<byte> destination, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<long>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static bool TryWriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ushort>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static bool TryWriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<uint>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static bool TryWriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ulong>(destination, ref value);
		}

		public static void WriteInt16LittleEndian(Span<byte> destination, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<short>(destination, ref value);
		}

		public static void WriteInt32LittleEndian(Span<byte> destination, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<int>(destination, ref value);
		}

		public static void WriteInt64LittleEndian(Span<byte> destination, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<long>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static void WriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ushort>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static void WriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<uint>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static void WriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ulong>(destination, ref value);
		}

		public static bool TryWriteInt16LittleEndian(Span<byte> destination, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<short>(destination, ref value);
		}

		public static bool TryWriteInt32LittleEndian(Span<byte> destination, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<int>(destination, ref value);
		}

		public static bool TryWriteInt64LittleEndian(Span<byte> destination, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<long>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static bool TryWriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ushort>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static bool TryWriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<uint>(destination, ref value);
		}

		[CLSCompliant(false)]
		public static bool TryWriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ulong>(destination, ref value);
		}
	}
}
