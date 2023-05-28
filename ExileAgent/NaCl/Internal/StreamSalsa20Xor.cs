using System;
using NetMQ.Utils;

namespace NaCl.Internal
{
	internal static class StreamSalsa20Xor
	{
		public unsafe static void Transform(Span<byte> c, ReadOnlySpan<byte> m, ReadOnlySpan<byte> n, ReadOnlySpan<byte> k, ulong ic = 0UL)
		{
			if (m.Length == 0)
			{
				return;
			}
			byte[] array = new byte[16];
			byte[] array2 = new byte[64];
			byte[] array3 = k.Slice(0, 32).ToArray();
			try
			{
				n.Slice(0, 8).CopyTo(array);
				for (int i = 8; i < 16; i++)
				{
					array[i] = (byte)(ic & 255UL);
					ic >>= 8;
				}
				int j = m.Length;
				while (j >= 64)
				{
					Salsa20.Transform(array2, array, array3, null);
					for (int l = 0; l < 64; l++)
					{
						*c[l] = (*m[l] ^ array2[l]);
					}
					uint num = 1U;
					for (int num2 = 8; num2 < 16; num2++)
					{
						num += (uint)array[num2];
						array[num2] = (byte)num;
						num >>= 8;
					}
					j -= 64;
					c = c.Slice(64);
					m = m.Slice(64);
				}
				if (j != 0)
				{
					Salsa20.Transform(array2, array, array3, null);
					for (int num3 = 0; num3 < j; num3++)
					{
						*c[num3] = (*m[num3] ^ array2[num3]);
					}
				}
			}
			finally
			{
				array.Clear<byte>();
				array2.Clear<byte>();
				array3.Clear<byte>();
			}
		}
	}
}
