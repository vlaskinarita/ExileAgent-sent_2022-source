using System;
using System.Runtime.CompilerServices;

namespace NaCl.Internal
{
	internal static class Common
	{
		[NullableContext(1)]
		public static uint Load32(byte[] src, int offset)
		{
			return (uint)((int)src[offset] | (int)src[offset + 1] << 8 | (int)src[offset + 2] << 16 | (int)src[offset + 3] << 24);
		}

		public unsafe static uint Load32(ReadOnlySpan<byte> src, int offset)
		{
			return (uint)((int)(*src[offset]) | (int)(*src[offset + 1]) << 8 | (int)(*src[offset + 2]) << 16 | (int)(*src[offset + 3]) << 24);
		}

		[NullableContext(1)]
		public static void Store(byte[] dst, int offset, uint w)
		{
			dst[offset] = (byte)w;
			w >>= 8;
			dst[offset + 1] = (byte)w;
			w >>= 8;
			dst[offset + 2] = (byte)w;
			w >>= 8;
			dst[offset + 3] = (byte)w;
		}

		public unsafe static void Store(Span<byte> dst, int offset, uint w)
		{
			*dst[offset] = (byte)w;
			w >>= 8;
			*dst[offset + 1] = (byte)w;
			w >>= 8;
			*dst[offset + 2] = (byte)w;
			w >>= 8;
			*dst[offset + 3] = (byte)w;
		}

		public static uint RotateLeft(in uint x, in int b)
		{
			return x << b | x >> 32 - b;
		}
	}
}
