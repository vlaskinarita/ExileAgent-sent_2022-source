using System;
using NetMQ.Utils;

namespace NaCl.Internal
{
	internal static class StreamSalsa20
	{
		public unsafe static void Transform(Span<byte> c, ReadOnlySpan<byte> n, ReadOnlySpan<byte> k)
		{
			if (c.IsEmpty)
			{
				return;
			}
			byte[] array = new byte[16];
			byte[] array2 = new byte[64];
			byte[] array3 = k.ToArray();
			try
			{
				for (int i = 0; i < 8; i++)
				{
					array[i] = *n[i];
				}
				while (c.Length >= 64)
				{
					Salsa20.Transform(array2, array, array3, null);
					array2.CopyTo(c);
					uint num = 1U;
					for (int j = 8; j < 16; j++)
					{
						num += (uint)array[j];
						array[j] = (byte)num;
						num >>= 8;
					}
					c = c.Slice(64);
				}
				if (c.Length != 0)
				{
					Salsa20.Transform(array2, array, array3, null);
					for (int l = 0; l < c.Length; l++)
					{
						*c[l] = array2[l];
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

		public const int KeyLength = 32;
	}
}
