using System;

namespace NaCl.Internal
{
	internal static class SafeComparison
	{
		public unsafe static bool Verify16(ReadOnlySpan<byte> x, ReadOnlySpan<byte> y)
		{
			uint num = (uint)(0 | (*x[0] ^ *y[0]));
			num |= (uint)(*x[1] ^ *y[1]);
			num |= (uint)(*x[2] ^ *y[2]);
			num |= (uint)(*x[3] ^ *y[3]);
			num |= (uint)(*x[4] ^ *y[4]);
			num |= (uint)(*x[5] ^ *y[5]);
			num |= (uint)(*x[6] ^ *y[6]);
			num |= (uint)(*x[7] ^ *y[7]);
			num |= (uint)(*x[8] ^ *y[8]);
			num |= (uint)(*x[9] ^ *y[9]);
			num |= (uint)(*x[10] ^ *y[10]);
			num |= (uint)(*x[11] ^ *y[11]);
			num |= (uint)(*x[12] ^ *y[12]);
			num |= (uint)(*x[13] ^ *y[13]);
			num |= (uint)(*x[14] ^ *y[14]);
			num |= (uint)(*x[15] ^ *y[15]);
			return (1U & num - 1U >> 8) == 1U;
		}

		public unsafe static bool Verify32(Span<byte> x, Span<byte> y)
		{
			uint num = (uint)(0 | (*x[0] ^ *y[0]));
			num |= (uint)(*x[1] ^ *y[1]);
			num |= (uint)(*x[2] ^ *y[2]);
			num |= (uint)(*x[3] ^ *y[3]);
			num |= (uint)(*x[4] ^ *y[4]);
			num |= (uint)(*x[5] ^ *y[5]);
			num |= (uint)(*x[6] ^ *y[6]);
			num |= (uint)(*x[7] ^ *y[7]);
			num |= (uint)(*x[8] ^ *y[8]);
			num |= (uint)(*x[9] ^ *y[9]);
			num |= (uint)(*x[10] ^ *y[10]);
			num |= (uint)(*x[11] ^ *y[11]);
			num |= (uint)(*x[12] ^ *y[12]);
			num |= (uint)(*x[13] ^ *y[13]);
			num |= (uint)(*x[14] ^ *y[14]);
			num |= (uint)(*x[15] ^ *y[15]);
			num |= (uint)(*x[16] ^ *y[16]);
			num |= (uint)(*x[17] ^ *y[17]);
			num |= (uint)(*x[18] ^ *y[18]);
			num |= (uint)(*x[19] ^ *y[19]);
			num |= (uint)(*x[20] ^ *y[20]);
			num |= (uint)(*x[21] ^ *y[21]);
			num |= (uint)(*x[22] ^ *y[22]);
			num |= (uint)(*x[23] ^ *y[23]);
			num |= (uint)(*x[24] ^ *y[24]);
			num |= (uint)(*x[25] ^ *y[25]);
			num |= (uint)(*x[26] ^ *y[26]);
			num |= (uint)(*x[27] ^ *y[27]);
			num |= (uint)(*x[28] ^ *y[28]);
			num |= (uint)(*x[29] ^ *y[29]);
			num |= (uint)(*x[30] ^ *y[30]);
			num |= (uint)(*x[31] ^ *y[31]);
			return (1U & num - 1U >> 8) == 1U;
		}
	}
}
