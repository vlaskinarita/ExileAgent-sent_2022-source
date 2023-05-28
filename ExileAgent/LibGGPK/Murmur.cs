using System;
using System.Text;

namespace LibGGPK
{
	internal static class Murmur
	{
		public static uint Hash2(string str, uint seed = 0U)
		{
			return Murmur.Hash2(Encoding.Unicode.GetBytes(str.ToLower()), 0U);
		}

		private static uint Hash2(byte[] data, uint seed = 0U)
		{
			uint num = seed ^ (uint)data.Length;
			int num2 = 0;
			int i;
			for (i = data.Length; i >= 4; i -= 4)
			{
				uint num3 = BitConverter.ToUInt32(data, num2);
				num3 *= 1540483477U;
				num3 ^= num3 >> 24;
				num3 *= 1540483477U;
				num *= 1540483477U;
				num ^= num3;
				num2 += 4;
			}
			switch (i)
			{
			case 1:
				goto IL_7D;
			case 2:
				break;
			case 3:
				num ^= (uint)((uint)data[num2 + 2] << 16);
				break;
			default:
				goto IL_8B;
			}
			num ^= (uint)((uint)data[num2 + 1] << 8);
			IL_7D:
			num ^= (uint)data[num2];
			num *= 1540483477U;
			IL_8B:
			num ^= num >> 13;
			num *= 1540483477U;
			return num ^ num >> 15;
		}

		private const uint DefaultMurmurSeed = 0U;
	}
}
