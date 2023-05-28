using System;

namespace NaCl.Internal
{
	internal static class Fe25519
	{
		public static void Zero(Span<int> h)
		{
			h.Slice(0, 10).Fill(0);
		}

		public unsafe static void One(Span<int> h)
		{
			*h[0] = 1;
			h.Slice(1, 9).Fill(0);
		}

		public unsafe static void Copy(Span<int> h, Span<int> f)
		{
			int num = *f[0];
			int num2 = *f[1];
			int num3 = *f[2];
			int num4 = *f[3];
			int num5 = *f[4];
			int num6 = *f[5];
			int num7 = *f[6];
			int num8 = *f[7];
			int num9 = *f[8];
			int num10 = *f[9];
			*h[0] = num;
			*h[1] = num2;
			*h[2] = num3;
			*h[3] = num4;
			*h[4] = num5;
			*h[5] = num6;
			*h[6] = num7;
			*h[7] = num8;
			*h[8] = num9;
			*h[9] = num10;
		}

		private unsafe static long Load3(ReadOnlySpan<byte> input)
		{
			return (long)((ulong)(*input[0]) | (ulong)(*input[1]) << 8 | (ulong)(*input[2]) << 16);
		}

		private unsafe static long Load4(ReadOnlySpan<byte> input)
		{
			return (long)((ulong)(*input[0]) | (ulong)(*input[1]) << 8 | (ulong)(*input[2]) << 16 | (ulong)(*input[3]) << 24);
		}

		public unsafe static void FromBytes(Span<int> h, ReadOnlySpan<byte> s)
		{
			long num = Fe25519.Load4(s);
			long num2 = Fe25519.Load3(s.Slice(4)) << 6;
			long num3 = Fe25519.Load3(s.Slice(7)) << 5;
			long num4 = Fe25519.Load3(s.Slice(10)) << 3;
			long num5 = Fe25519.Load3(s.Slice(13)) << 2;
			long num6 = Fe25519.Load4(s.Slice(16));
			long num7 = Fe25519.Load3(s.Slice(20)) << 7;
			long num8 = Fe25519.Load3(s.Slice(23)) << 5;
			long num9 = Fe25519.Load3(s.Slice(26)) << 4;
			long num10 = (Fe25519.Load3(s.Slice(29)) & 8388607L) << 2;
			long num11 = num10 + 16777216L >> 25;
			num += num11 * 19L;
			num10 -= num11 << 25;
			long num12 = num2 + 16777216L >> 25;
			num3 += num12;
			num2 -= num12 << 25;
			long num13 = num4 + 16777216L >> 25;
			num5 += num13;
			num4 -= num13 << 25;
			long num14 = num6 + 16777216L >> 25;
			num7 += num14;
			num6 -= num14 << 25;
			long num15 = num8 + 16777216L >> 25;
			num9 += num15;
			num8 -= num15 << 25;
			long num16 = num + 33554432L >> 26;
			num2 += num16;
			num -= num16 << 26;
			long num17 = num3 + 33554432L >> 26;
			num4 += num17;
			num3 -= num17 << 26;
			long num18 = num5 + 33554432L >> 26;
			num6 += num18;
			num5 -= num18 << 26;
			long num19 = num7 + 33554432L >> 26;
			num8 += num19;
			num7 -= num19 << 26;
			long num20 = num9 + 33554432L >> 26;
			num10 += num20;
			num9 -= num20 << 26;
			*h[0] = (int)num;
			*h[1] = (int)num2;
			*h[2] = (int)num3;
			*h[3] = (int)num4;
			*h[4] = (int)num5;
			*h[5] = (int)num6;
			*h[6] = (int)num7;
			*h[7] = (int)num8;
			*h[8] = (int)num9;
			*h[9] = (int)num10;
		}

		public unsafe static void CSwap(Span<int> f, Span<int> g, int b)
		{
			int num = -b;
			int num2 = *f[0];
			int num3 = *f[1];
			int num4 = *f[2];
			int num5 = *f[3];
			int num6 = *f[4];
			int num7 = *f[5];
			int num8 = *f[6];
			int num9 = *f[7];
			int num10 = *f[8];
			int num11 = *f[9];
			int num12 = *g[0];
			int num13 = *g[1];
			int num14 = *g[2];
			int num15 = *g[3];
			int num16 = *g[4];
			int num17 = *g[5];
			int num18 = *g[6];
			int num19 = *g[7];
			int num20 = *g[8];
			int num21 = *g[9];
			int num22 = num2 ^ num12;
			int num23 = num3 ^ num13;
			int num24 = num4 ^ num14;
			int num25 = num5 ^ num15;
			int num26 = num6 ^ num16;
			int num27 = num7 ^ num17;
			int num28 = num8 ^ num18;
			int num29 = num9 ^ num19;
			int num30 = num10 ^ num20;
			int num31 = num11 ^ num21;
			num22 &= num;
			num23 &= num;
			num24 &= num;
			num25 &= num;
			num26 &= num;
			num27 &= num;
			num28 &= num;
			num29 &= num;
			num30 &= num;
			num31 &= num;
			*f[0] = (num2 ^ num22);
			*f[1] = (num3 ^ num23);
			*f[2] = (num4 ^ num24);
			*f[3] = (num5 ^ num25);
			*f[4] = (num6 ^ num26);
			*f[5] = (num7 ^ num27);
			*f[6] = (num8 ^ num28);
			*f[7] = (num9 ^ num29);
			*f[8] = (num10 ^ num30);
			*f[9] = (num11 ^ num31);
			*g[0] = (num12 ^ num22);
			*g[1] = (num13 ^ num23);
			*g[2] = (num14 ^ num24);
			*g[3] = (num15 ^ num25);
			*g[4] = (num16 ^ num26);
			*g[5] = (num17 ^ num27);
			*g[6] = (num18 ^ num28);
			*g[7] = (num19 ^ num29);
			*g[8] = (num20 ^ num30);
			*g[9] = (num21 ^ num31);
		}

		public unsafe static void Sub(Span<int> h, Span<int> f, Span<int> g)
		{
			int num = *f[0] - *g[0];
			int num2 = *f[1] - *g[1];
			int num3 = *f[2] - *g[2];
			int num4 = *f[3] - *g[3];
			int num5 = *f[4] - *g[4];
			int num6 = *f[5] - *g[5];
			int num7 = *f[6] - *g[6];
			int num8 = *f[7] - *g[7];
			int num9 = *f[8] - *g[8];
			int num10 = *f[9] - *g[9];
			*h[0] = num;
			*h[1] = num2;
			*h[2] = num3;
			*h[3] = num4;
			*h[4] = num5;
			*h[5] = num6;
			*h[6] = num7;
			*h[7] = num8;
			*h[8] = num9;
			*h[9] = num10;
		}

		public unsafe static void Add(Span<int> h, Span<int> f, Span<int> g)
		{
			int num = *f[0] + *g[0];
			int num2 = *f[1] + *g[1];
			int num3 = *f[2] + *g[2];
			int num4 = *f[3] + *g[3];
			int num5 = *f[4] + *g[4];
			int num6 = *f[5] + *g[5];
			int num7 = *f[6] + *g[6];
			int num8 = *f[7] + *g[7];
			int num9 = *f[8] + *g[8];
			int num10 = *f[9] + *g[9];
			*h[0] = num;
			*h[1] = num2;
			*h[2] = num3;
			*h[3] = num4;
			*h[4] = num5;
			*h[5] = num6;
			*h[6] = num7;
			*h[7] = num8;
			*h[8] = num9;
			*h[9] = num10;
		}

		public unsafe static void Mul(Span<int> h, Span<int> f, Span<int> g)
		{
			int num = *f[0];
			int num2 = *f[1];
			int num3 = *f[2];
			int num4 = *f[3];
			int num5 = *f[4];
			int num6 = *f[5];
			int num7 = *f[6];
			int num8 = *f[7];
			int num9 = *f[8];
			int num10 = *f[9];
			int num11 = *g[0];
			int num12 = *g[1];
			int num13 = *g[2];
			int num14 = *g[3];
			int num15 = *g[4];
			int num16 = *g[5];
			int num17 = *g[6];
			int num18 = *g[7];
			int num19 = *g[8];
			int num20 = *g[9];
			int num21 = 19 * num12;
			int num22 = 19 * num13;
			int num23 = 19 * num14;
			int num24 = 19 * num15;
			int num25 = 19 * num16;
			int num26 = 19 * num17;
			int num27 = 19 * num18;
			int num28 = 19 * num19;
			int num29 = 19 * num20;
			int num30 = 2 * num2;
			int num31 = 2 * num4;
			int num32 = 2 * num6;
			int num33 = 2 * num8;
			int num34 = 2 * num10;
			long num35 = (long)num * (long)num11;
			long num36 = (long)num * (long)num12;
			long num37 = (long)num * (long)num13;
			long num38 = (long)num * (long)num14;
			long num39 = (long)num * (long)num15;
			long num40 = (long)num * (long)num16;
			long num41 = (long)num * (long)num17;
			long num42 = (long)num * (long)num18;
			long num43 = (long)num * (long)num19;
			long num44 = (long)num * (long)num20;
			long num45 = (long)num2 * (long)num11;
			long num46 = (long)num30 * (long)num12;
			long num47 = (long)num2 * (long)num13;
			long num48 = (long)num30 * (long)num14;
			long num49 = (long)num2 * (long)num15;
			long num50 = (long)num30 * (long)num16;
			long num51 = (long)num2 * (long)num17;
			long num52 = (long)num30 * (long)num18;
			long num53 = (long)num2 * (long)num19;
			long num54 = (long)num30 * (long)num29;
			long num55 = (long)num3 * (long)num11;
			long num56 = (long)num3 * (long)num12;
			long num57 = (long)num3 * (long)num13;
			long num58 = (long)num3 * (long)num14;
			long num59 = (long)num3 * (long)num15;
			long num60 = (long)num3 * (long)num16;
			long num61 = (long)num3 * (long)num17;
			long num62 = (long)num3 * (long)num18;
			long num63 = (long)num3 * (long)num28;
			long num64 = (long)num3 * (long)num29;
			long num65 = (long)num4 * (long)num11;
			long num66 = (long)num31 * (long)num12;
			long num67 = (long)num4 * (long)num13;
			long num68 = (long)num31 * (long)num14;
			long num69 = (long)num4 * (long)num15;
			long num70 = (long)num31 * (long)num16;
			long num71 = (long)num4 * (long)num17;
			long num72 = (long)num31 * (long)num27;
			long num73 = (long)num4 * (long)num28;
			long num74 = (long)num31 * (long)num29;
			long num75 = (long)num5 * (long)num11;
			long num76 = (long)num5 * (long)num12;
			long num77 = (long)num5 * (long)num13;
			long num78 = (long)num5 * (long)num14;
			long num79 = (long)num5 * (long)num15;
			long num80 = (long)num5 * (long)num16;
			long num81 = (long)num5 * (long)num26;
			long num82 = (long)num5 * (long)num27;
			long num83 = (long)num5 * (long)num28;
			long num84 = (long)num5 * (long)num29;
			long num85 = (long)num6 * (long)num11;
			long num86 = (long)num32 * (long)num12;
			long num87 = (long)num6 * (long)num13;
			long num88 = (long)num32 * (long)num14;
			long num89 = (long)num6 * (long)num15;
			long num90 = (long)num32 * (long)num25;
			long num91 = (long)num6 * (long)num26;
			long num92 = (long)num32 * (long)num27;
			long num93 = (long)num6 * (long)num28;
			long num94 = (long)num32 * (long)num29;
			long num95 = (long)num7 * (long)num11;
			long num96 = (long)num7 * (long)num12;
			long num97 = (long)num7 * (long)num13;
			long num98 = (long)num7 * (long)num14;
			long num99 = (long)num7 * (long)num24;
			long num100 = (long)num7 * (long)num25;
			long num101 = (long)num7 * (long)num26;
			long num102 = (long)num7 * (long)num27;
			long num103 = (long)num7 * (long)num28;
			long num104 = (long)num7 * (long)num29;
			long num105 = (long)num8 * (long)num11;
			long num106 = (long)num33 * (long)num12;
			long num107 = (long)num8 * (long)num13;
			long num108 = (long)num33 * (long)num23;
			long num109 = (long)num8 * (long)num24;
			long num110 = (long)num33 * (long)num25;
			long num111 = (long)num8 * (long)num26;
			long num112 = (long)num33 * (long)num27;
			long num113 = (long)num8 * (long)num28;
			long num114 = (long)num33 * (long)num29;
			long num115 = (long)num9 * (long)num11;
			long num116 = (long)num9 * (long)num12;
			long num117 = (long)num9 * (long)num22;
			long num118 = (long)num9 * (long)num23;
			long num119 = (long)num9 * (long)num24;
			long num120 = (long)num9 * (long)num25;
			long num121 = (long)num9 * (long)num26;
			long num122 = (long)num9 * (long)num27;
			long num123 = (long)num9 * (long)num28;
			long num124 = (long)num9 * (long)num29;
			long num125 = (long)num10 * (long)num11;
			long num126 = (long)num34 * (long)num21;
			long num127 = (long)num10 * (long)num22;
			long num128 = (long)num34 * (long)num23;
			long num129 = (long)num10 * (long)num24;
			long num130 = (long)num34 * (long)num25;
			long num131 = (long)num10 * (long)num26;
			long num132 = (long)num34 * (long)num27;
			long num133 = (long)num10 * (long)num28;
			long num134 = (long)num34 * (long)num29;
			long num135 = num35 + num54 + num63 + num72 + num81 + num90 + num99 + num108 + num117 + num126;
			long num136 = num36 + num45 + num64 + num73 + num82 + num91 + num100 + num109 + num118 + num127;
			long num137 = num37 + num46 + num55 + num74 + num83 + num92 + num101 + num110 + num119 + num128;
			long num138 = num38 + num47 + num56 + num65 + num84 + num93 + num102 + num111 + num120 + num129;
			long num139 = num39 + num48 + num57 + num66 + num75 + num94 + num103 + num112 + num121 + num130;
			long num140 = num40 + num49 + num58 + num67 + num76 + num85 + num104 + num113 + num122 + num131;
			long num141 = num41 + num50 + num59 + num68 + num77 + num86 + num95 + num114 + num123 + num132;
			long num142 = num42 + num51 + num60 + num69 + num78 + num87 + num96 + num105 + num124 + num133;
			long num143 = num43 + num52 + num61 + num70 + num79 + num88 + num97 + num106 + num115 + num134;
			long num144 = num44 + num53 + num62 + num71 + num80 + num89 + num98 + num107 + num116 + num125;
			long num145 = num135 + 33554432L >> 26;
			num136 += num145;
			num135 -= num145 * 67108864L;
			long num146 = num139 + 33554432L >> 26;
			num140 += num146;
			num139 -= num146 * 67108864L;
			long num147 = num136 + 16777216L >> 25;
			num137 += num147;
			num136 -= num147 * 33554432L;
			long num148 = num140 + 16777216L >> 25;
			num141 += num148;
			num140 -= num148 * 33554432L;
			long num149 = num137 + 33554432L >> 26;
			num138 += num149;
			num137 -= num149 * 67108864L;
			long num150 = num141 + 33554432L >> 26;
			num142 += num150;
			num141 -= num150 * 67108864L;
			long num151 = num138 + 16777216L >> 25;
			num139 += num151;
			num138 -= num151 * 33554432L;
			long num152 = num142 + 16777216L >> 25;
			num143 += num152;
			num142 -= num152 * 33554432L;
			num146 = num139 + 33554432L >> 26;
			num140 += num146;
			num139 -= num146 * 67108864L;
			long num153 = num143 + 33554432L >> 26;
			num144 += num153;
			num143 -= num153 * 67108864L;
			long num154 = num144 + 16777216L >> 25;
			num135 += num154 * 19L;
			num144 -= num154 * 33554432L;
			num145 = num135 + 33554432L >> 26;
			num136 += num145;
			num135 -= num145 * 67108864L;
			*h[0] = (int)num135;
			*h[1] = (int)num136;
			*h[2] = (int)num137;
			*h[3] = (int)num138;
			*h[4] = (int)num139;
			*h[5] = (int)num140;
			*h[6] = (int)num141;
			*h[7] = (int)num142;
			*h[8] = (int)num143;
			*h[9] = (int)num144;
		}

		public unsafe static void Sq(Span<int> h, Span<int> f)
		{
			int num = *f[0];
			int num2 = *f[1];
			int num3 = *f[2];
			int num4 = *f[3];
			int num5 = *f[4];
			int num6 = *f[5];
			int num7 = *f[6];
			int num8 = *f[7];
			int num9 = *f[8];
			int num10 = *f[9];
			int num11 = 2 * num;
			int num12 = 2 * num2;
			int num13 = 2 * num3;
			int num14 = 2 * num4;
			int num15 = 2 * num5;
			int num16 = 2 * num6;
			int num17 = 2 * num7;
			int num18 = 2 * num8;
			int num19 = 38 * num6;
			int num20 = 19 * num7;
			int num21 = 38 * num8;
			int num22 = 19 * num9;
			int num23 = 38 * num10;
			long num24 = (long)num * (long)num;
			long num25 = (long)num11 * (long)num2;
			long num26 = (long)num11 * (long)num3;
			long num27 = (long)num11 * (long)num4;
			long num28 = (long)num11 * (long)num5;
			long num29 = (long)num11 * (long)num6;
			long num30 = (long)num11 * (long)num7;
			long num31 = (long)num11 * (long)num8;
			long num32 = (long)num11 * (long)num9;
			long num33 = (long)num11 * (long)num10;
			long num34 = (long)num12 * (long)num2;
			long num35 = (long)num12 * (long)num3;
			long num36 = (long)num12 * (long)num14;
			long num37 = (long)num12 * (long)num5;
			long num38 = (long)num12 * (long)num16;
			long num39 = (long)num12 * (long)num7;
			long num40 = (long)num12 * (long)num18;
			long num41 = (long)num12 * (long)num9;
			long num42 = (long)num12 * (long)num23;
			long num43 = (long)num3 * (long)num3;
			long num44 = (long)num13 * (long)num4;
			long num45 = (long)num13 * (long)num5;
			long num46 = (long)num13 * (long)num6;
			long num47 = (long)num13 * (long)num7;
			long num48 = (long)num13 * (long)num8;
			long num49 = (long)num13 * (long)num22;
			long num50 = (long)num3 * (long)num23;
			long num51 = (long)num14 * (long)num4;
			long num52 = (long)num14 * (long)num5;
			long num53 = (long)num14 * (long)num16;
			long num54 = (long)num14 * (long)num7;
			long num55 = (long)num14 * (long)num21;
			long num56 = (long)num14 * (long)num22;
			long num57 = (long)num14 * (long)num23;
			long num58 = (long)num5 * (long)num5;
			long num59 = (long)num15 * (long)num6;
			long num60 = (long)num15 * (long)num20;
			long num61 = (long)num5 * (long)num21;
			long num62 = (long)num15 * (long)num22;
			long num63 = (long)num5 * (long)num23;
			long num64 = (long)num6 * (long)num19;
			long num65 = (long)num16 * (long)num20;
			long num66 = (long)num16 * (long)num21;
			long num67 = (long)num16 * (long)num22;
			long num68 = (long)num16 * (long)num23;
			long num69 = (long)num7 * (long)num20;
			long num70 = (long)num7 * (long)num21;
			long num71 = (long)num17 * (long)num22;
			long num72 = (long)num7 * (long)num23;
			long num73 = (long)num8 * (long)num21;
			long num74 = (long)num18 * (long)num22;
			long num75 = (long)num18 * (long)num23;
			long num76 = (long)num9 * (long)num22;
			long num77 = (long)num9 * (long)num23;
			long num78 = (long)num10 * (long)num23;
			long num79 = num24 + num42 + num49 + num55 + num60 + num64;
			long num80 = num25 + num50 + num56 + num61 + num65;
			long num81 = num26 + num34 + num57 + num62 + num66 + num69;
			long num82 = num27 + num35 + num63 + num67 + num70;
			long num83 = num28 + num36 + num43 + num68 + num71 + num73;
			long num84 = num29 + num37 + num44 + num72 + num74;
			long num85 = num30 + num38 + num45 + num51 + num75 + num76;
			long num86 = num31 + num39 + num46 + num52 + num77;
			long num87 = num32 + num40 + num47 + num53 + num58 + num78;
			long num88 = num33 + num41 + num48 + num54 + num59;
			long num89 = num79 + 33554432L >> 26;
			num80 += num89;
			num79 -= num89 * 67108864L;
			long num90 = num83 + 33554432L >> 26;
			num84 += num90;
			num83 -= num90 * 67108864L;
			long num91 = num80 + 16777216L >> 25;
			num81 += num91;
			num80 -= num91 * 33554432L;
			long num92 = num84 + 16777216L >> 25;
			num85 += num92;
			num84 -= num92 * 33554432L;
			long num93 = num81 + 33554432L >> 26;
			num82 += num93;
			num81 -= num93 * 67108864L;
			long num94 = num85 + 33554432L >> 26;
			num86 += num94;
			num85 -= num94 * 67108864L;
			long num95 = num82 + 16777216L >> 25;
			num83 += num95;
			num82 -= num95 * 33554432L;
			long num96 = num86 + 16777216L >> 25;
			num87 += num96;
			num86 -= num96 * 33554432L;
			num90 = num83 + 33554432L >> 26;
			num84 += num90;
			num83 -= num90 * 67108864L;
			long num97 = num87 + 33554432L >> 26;
			num88 += num97;
			num87 -= num97 * 67108864L;
			long num98 = num88 + 16777216L >> 25;
			num79 += num98 * 19L;
			num88 -= num98 * 33554432L;
			num89 = num79 + 33554432L >> 26;
			num80 += num89;
			num79 -= num89 * 67108864L;
			*h[0] = (int)num79;
			*h[1] = (int)num80;
			*h[2] = (int)num81;
			*h[3] = (int)num82;
			*h[4] = (int)num83;
			*h[5] = (int)num84;
			*h[6] = (int)num85;
			*h[7] = (int)num86;
			*h[8] = (int)num87;
			*h[9] = (int)num88;
		}

		public unsafe static void Mul121666(Span<int> h, Span<int> f)
		{
			long num = (long)(*f[0]);
			int num2 = *f[1];
			int num3 = *f[2];
			int num4 = *f[3];
			int num5 = *f[4];
			int num6 = *f[5];
			int num7 = *f[6];
			int num8 = *f[7];
			int num9 = *f[8];
			int num10 = *f[9];
			long num11 = num * 121666L;
			long num12 = (long)num2 * 121666L;
			long num13 = (long)num3 * 121666L;
			long num14 = (long)num4 * 121666L;
			long num15 = (long)num5 * 121666L;
			long num16 = (long)num6 * 121666L;
			long num17 = (long)num7 * 121666L;
			long num18 = (long)num8 * 121666L;
			long num19 = (long)num9 * 121666L;
			long num20 = (long)num10 * 121666L;
			long num21 = num20 + 16777216L >> 25;
			num11 += num21 * 19L;
			num20 -= num21 << 25;
			long num22 = num12 + 16777216L >> 25;
			num13 += num22;
			num12 -= num22 << 25;
			long num23 = num14 + 16777216L >> 25;
			num15 += num23;
			num14 -= num23 << 25;
			long num24 = num16 + 16777216L >> 25;
			num17 += num24;
			num16 -= num24 << 25;
			long num25 = num18 + 16777216L >> 25;
			num19 += num25;
			num18 -= num25 << 25;
			long num26 = num11 + 33554432L >> 26;
			num12 += num26;
			num11 -= num26 << 26;
			long num27 = num13 + 33554432L >> 26;
			num14 += num27;
			num13 -= num27 << 26;
			long num28 = num15 + 33554432L >> 26;
			num16 += num28;
			num15 -= num28 << 26;
			long num29 = num17 + 33554432L >> 26;
			num18 += num29;
			num17 -= num29 << 26;
			long num30 = num19 + 33554432L >> 26;
			num20 += num30;
			num19 -= num30 << 26;
			*h[0] = (int)num11;
			*h[1] = (int)num12;
			*h[2] = (int)num13;
			*h[3] = (int)num14;
			*h[4] = (int)num15;
			*h[5] = (int)num16;
			*h[6] = (int)num17;
			*h[7] = (int)num18;
			*h[8] = (int)num19;
			*h[9] = (int)num20;
		}

		public unsafe static void Invert(Span<int> output, Span<int> z)
		{
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span2 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span3 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Span<int> span4 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Fe25519.Sq(span, z);
			Fe25519.Sq(span2, span);
			Fe25519.Sq(span2, span2);
			Fe25519.Mul(span2, z, span2);
			Fe25519.Mul(span, span, span2);
			Fe25519.Sq(span3, span);
			Fe25519.Mul(span2, span2, span3);
			Fe25519.Sq(span3, span2);
			for (int i = 1; i < 5; i++)
			{
				Fe25519.Sq(span3, span3);
			}
			Fe25519.Mul(span2, span3, span2);
			Fe25519.Sq(span3, span2);
			for (int i = 1; i < 10; i++)
			{
				Fe25519.Sq(span3, span3);
			}
			Fe25519.Mul(span3, span3, span2);
			Fe25519.Sq(span4, span3);
			for (int i = 1; i < 20; i++)
			{
				Fe25519.Sq(span4, span4);
			}
			Fe25519.Mul(span3, span4, span3);
			Fe25519.Sq(span3, span3);
			for (int i = 1; i < 10; i++)
			{
				Fe25519.Sq(span3, span3);
			}
			Fe25519.Mul(span2, span3, span2);
			Fe25519.Sq(span3, span2);
			for (int i = 1; i < 50; i++)
			{
				Fe25519.Sq(span3, span3);
			}
			Fe25519.Mul(span3, span3, span2);
			Fe25519.Sq(span4, span3);
			for (int i = 1; i < 100; i++)
			{
				Fe25519.Sq(span4, span4);
			}
			Fe25519.Mul(span3, span4, span3);
			Fe25519.Sq(span3, span3);
			for (int i = 1; i < 50; i++)
			{
				Fe25519.Sq(span3, span3);
			}
			Fe25519.Mul(span2, span3, span2);
			Fe25519.Sq(span2, span2);
			for (int i = 1; i < 5; i++)
			{
				Fe25519.Sq(span2, span2);
			}
			Fe25519.Mul(output, span2, span);
		}

		private unsafe static void Reduce(Span<int> h, Span<int> f)
		{
			int num = *f[0];
			int num2 = *f[1];
			int num3 = *f[2];
			int num4 = *f[3];
			int num5 = *f[4];
			int num6 = *f[5];
			int num7 = *f[6];
			int num8 = *f[7];
			int num9 = *f[8];
			int num10 = *f[9];
			int num11 = 19 * num10 + 16777216 >> 25;
			num11 = num + num11 >> 26;
			num11 = num2 + num11 >> 25;
			num11 = num3 + num11 >> 26;
			num11 = num4 + num11 >> 25;
			num11 = num5 + num11 >> 26;
			num11 = num6 + num11 >> 25;
			num11 = num7 + num11 >> 26;
			num11 = num8 + num11 >> 25;
			num11 = num9 + num11 >> 26;
			num11 = num10 + num11 >> 25;
			num += 19 * num11;
			int num12 = num >> 26;
			num2 += num12;
			num -= num12 * 67108864;
			int num13 = num2 >> 25;
			num3 += num13;
			num2 -= num13 * 33554432;
			int num14 = num3 >> 26;
			num4 += num14;
			num3 -= num14 * 67108864;
			int num15 = num4 >> 25;
			num5 += num15;
			num4 -= num15 * 33554432;
			int num16 = num5 >> 26;
			num6 += num16;
			num5 -= num16 * 67108864;
			int num17 = num6 >> 25;
			num7 += num17;
			num6 -= num17 * 33554432;
			int num18 = num7 >> 26;
			num8 += num18;
			num7 -= num18 * 67108864;
			int num19 = num8 >> 25;
			num9 += num19;
			num8 -= num19 * 33554432;
			int num20 = num9 >> 26;
			num10 += num20;
			num9 -= num20 * 67108864;
			int num21 = num10 >> 25;
			num10 -= num21 * 33554432;
			*h[0] = num;
			*h[1] = num2;
			*h[2] = num3;
			*h[3] = num4;
			*h[4] = num5;
			*h[5] = num6;
			*h[6] = num7;
			*h[7] = num8;
			*h[8] = num9;
			*h[9] = num10;
		}

		public unsafe static void ToBytes(Span<byte> s, Span<int> h)
		{
			Span<int> h2 = new Span<int>(stackalloc byte[(UIntPtr)40], 10);
			Fe25519.Reduce(h2, h);
			*s[0] = (byte)(*h2[0]);
			*s[1] = (byte)(*h2[0] >> 8);
			*s[2] = (byte)(*h2[0] >> 16);
			*s[3] = (byte)(*h2[0] >> 24 | *h2[1] * 4);
			*s[4] = (byte)(*h2[1] >> 6);
			*s[5] = (byte)(*h2[1] >> 14);
			*s[6] = (byte)(*h2[1] >> 22 | *h2[2] * 8);
			*s[7] = (byte)(*h2[2] >> 5);
			*s[8] = (byte)(*h2[2] >> 13);
			*s[9] = (byte)(*h2[2] >> 21 | *h2[3] * 32);
			*s[10] = (byte)(*h2[3] >> 3);
			*s[11] = (byte)(*h2[3] >> 11);
			*s[12] = (byte)(*h2[3] >> 19 | *h2[4] * 64);
			*s[13] = (byte)(*h2[4] >> 2);
			*s[14] = (byte)(*h2[4] >> 10);
			*s[15] = (byte)(*h2[4] >> 18);
			*s[16] = (byte)(*h2[5]);
			*s[17] = (byte)(*h2[5] >> 8);
			*s[18] = (byte)(*h2[5] >> 16);
			*s[19] = (byte)(*h2[5] >> 24 | *h2[6] * 2);
			*s[20] = (byte)(*h2[6] >> 7);
			*s[21] = (byte)(*h2[6] >> 15);
			*s[22] = (byte)(*h2[6] >> 23 | *h2[7] * 8);
			*s[23] = (byte)(*h2[7] >> 5);
			*s[24] = (byte)(*h2[7] >> 13);
			*s[25] = (byte)(*h2[7] >> 21 | *h2[8] * 16);
			*s[26] = (byte)(*h2[8] >> 4);
			*s[27] = (byte)(*h2[8] >> 12);
			*s[28] = (byte)(*h2[8] >> 20 | *h2[9] * 64);
			*s[29] = (byte)(*h2[9] >> 2);
			*s[30] = (byte)(*h2[9] >> 10);
			*s[31] = (byte)(*h2[9] >> 18);
		}

		public const int ArraySize = 10;
	}
}
