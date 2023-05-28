using System;

namespace NaCl.Internal
{
	internal sealed class HSalsa20
	{
		public static void Transform(Span<byte> output, ReadOnlySpan<byte> input, ReadOnlySpan<byte> k, ReadOnlySpan<byte> c)
		{
			uint num;
			uint num2;
			uint num3;
			uint num4;
			if (c.IsEmpty)
			{
				num = 1634760805U;
				num2 = 857760878U;
				num3 = 2036477234U;
				num4 = 1797285236U;
			}
			else
			{
				num = Common.Load32(c, 0);
				num2 = Common.Load32(c, 4);
				num3 = Common.Load32(c, 8);
				num4 = Common.Load32(c, 12);
			}
			uint num5 = Common.Load32(k, 0);
			uint num6 = Common.Load32(k, 4);
			uint num7 = Common.Load32(k, 8);
			uint num8 = Common.Load32(k, 12);
			uint num9 = Common.Load32(k, 16);
			uint num10 = Common.Load32(k, 20);
			uint num11 = Common.Load32(k, 24);
			uint num12 = Common.Load32(k, 28);
			uint num13 = Common.Load32(input, 0);
			uint num14 = Common.Load32(input, 4);
			uint num15 = Common.Load32(input, 8);
			uint num16 = Common.Load32(input, 12);
			for (int i = 20; i > 0; i -= 2)
			{
				uint num17 = num8;
				uint num18 = num + num10;
				int num19 = 7;
				num8 = (num17 ^ Common.RotateLeft(num18, num19));
				uint num20 = num15;
				num18 = num8 + num;
				num19 = 9;
				num15 = (num20 ^ Common.RotateLeft(num18, num19));
				uint num21 = num10;
				num18 = num15 + num8;
				num19 = 13;
				num10 = (num21 ^ Common.RotateLeft(num18, num19));
				uint num22 = num;
				num18 = num10 + num15;
				num19 = 18;
				num = (num22 ^ Common.RotateLeft(num18, num19));
				uint num23 = num16;
				num18 = num2 + num5;
				num19 = 7;
				num16 = (num23 ^ Common.RotateLeft(num18, num19));
				uint num24 = num11;
				num18 = num16 + num2;
				num19 = 9;
				num11 = (num24 ^ Common.RotateLeft(num18, num19));
				uint num25 = num5;
				num18 = num11 + num16;
				num19 = 13;
				num5 = (num25 ^ Common.RotateLeft(num18, num19));
				uint num26 = num2;
				num18 = num5 + num11;
				num19 = 18;
				num2 = (num26 ^ Common.RotateLeft(num18, num19));
				uint num27 = num12;
				num18 = num3 + num13;
				num19 = 7;
				num12 = (num27 ^ Common.RotateLeft(num18, num19));
				uint num28 = num6;
				num18 = num12 + num3;
				num19 = 9;
				num6 = (num28 ^ Common.RotateLeft(num18, num19));
				uint num29 = num13;
				num18 = num6 + num12;
				num19 = 13;
				num13 = (num29 ^ Common.RotateLeft(num18, num19));
				uint num30 = num3;
				num18 = num13 + num6;
				num19 = 18;
				num3 = (num30 ^ Common.RotateLeft(num18, num19));
				uint num31 = num7;
				num18 = num4 + num9;
				num19 = 7;
				num7 = (num31 ^ Common.RotateLeft(num18, num19));
				uint num32 = num14;
				num18 = num7 + num4;
				num19 = 9;
				num14 = (num32 ^ Common.RotateLeft(num18, num19));
				uint num33 = num9;
				num18 = num14 + num7;
				num19 = 13;
				num9 = (num33 ^ Common.RotateLeft(num18, num19));
				uint num34 = num4;
				num18 = num9 + num14;
				num19 = 18;
				num4 = (num34 ^ Common.RotateLeft(num18, num19));
				uint num35 = num5;
				num18 = num + num7;
				num19 = 7;
				num5 = (num35 ^ Common.RotateLeft(num18, num19));
				uint num36 = num6;
				num18 = num5 + num;
				num19 = 9;
				num6 = (num36 ^ Common.RotateLeft(num18, num19));
				uint num37 = num7;
				num18 = num6 + num5;
				num19 = 13;
				num7 = (num37 ^ Common.RotateLeft(num18, num19));
				uint num38 = num;
				num18 = num7 + num6;
				num19 = 18;
				num = (num38 ^ Common.RotateLeft(num18, num19));
				uint num39 = num13;
				num18 = num2 + num8;
				num19 = 7;
				num13 = (num39 ^ Common.RotateLeft(num18, num19));
				uint num40 = num14;
				num18 = num13 + num2;
				num19 = 9;
				num14 = (num40 ^ Common.RotateLeft(num18, num19));
				uint num41 = num8;
				num18 = num14 + num13;
				num19 = 13;
				num8 = (num41 ^ Common.RotateLeft(num18, num19));
				uint num42 = num2;
				num18 = num8 + num14;
				num19 = 18;
				num2 = (num42 ^ Common.RotateLeft(num18, num19));
				uint num43 = num9;
				num18 = num3 + num16;
				num19 = 7;
				num9 = (num43 ^ Common.RotateLeft(num18, num19));
				uint num44 = num15;
				num18 = num9 + num3;
				num19 = 9;
				num15 = (num44 ^ Common.RotateLeft(num18, num19));
				uint num45 = num16;
				num18 = num15 + num9;
				num19 = 13;
				num16 = (num45 ^ Common.RotateLeft(num18, num19));
				uint num46 = num3;
				num18 = num16 + num15;
				num19 = 18;
				num3 = (num46 ^ Common.RotateLeft(num18, num19));
				uint num47 = num10;
				num18 = num4 + num12;
				num19 = 7;
				num10 = (num47 ^ Common.RotateLeft(num18, num19));
				uint num48 = num11;
				num18 = num10 + num4;
				num19 = 9;
				num11 = (num48 ^ Common.RotateLeft(num18, num19));
				uint num49 = num12;
				num18 = num11 + num10;
				num19 = 13;
				num12 = (num49 ^ Common.RotateLeft(num18, num19));
				uint num50 = num4;
				num18 = num12 + num11;
				num19 = 18;
				num4 = (num50 ^ Common.RotateLeft(num18, num19));
			}
			Common.Store(output, 0, num);
			Common.Store(output, 4, num2);
			Common.Store(output, 8, num3);
			Common.Store(output, 12, num4);
			Common.Store(output, 16, num13);
			Common.Store(output, 20, num14);
			Common.Store(output, 24, num15);
			Common.Store(output, 28, num16);
		}

		private const int Rounds = 20;

		public const int BlockLength = 32;
	}
}
