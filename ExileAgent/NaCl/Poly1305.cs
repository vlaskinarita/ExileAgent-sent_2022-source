using System;
using System.Runtime.CompilerServices;
using NaCl.Internal;

namespace NaCl
{
	public sealed class Poly1305 : IDisposable
	{
		public Poly1305(ReadOnlySpan<byte> key)
		{
			this.Initialize();
			this.SetKey(key);
		}

		public Poly1305()
		{
			this.Initialize();
		}

		public void Dispose()
		{
			this.Reset();
		}

		public void Reset()
		{
			Array.Clear(this.r, 0, 5);
			Array.Clear(this.h, 0, 5);
			Array.Clear(this.pad, 0, 4);
			Array.Clear(this.buffer, 0, 16);
			this.leftover = 0;
			this.final = false;
		}

		public void SetKey(ReadOnlySpan<byte> key)
		{
			this.r[0] = (Common.Load32(key, 0) & 67108863U);
			this.r[1] = (Common.Load32(key, 3) >> 2 & 67108611U);
			this.r[2] = (Common.Load32(key, 6) >> 4 & 67092735U);
			this.r[3] = (Common.Load32(key, 9) >> 6 & 66076671U);
			this.r[4] = (Common.Load32(key, 12) >> 8 & 1048575U);
			this.pad[0] = Common.Load32(key, 16);
			this.pad[1] = Common.Load32(key, 20);
			this.pad[2] = Common.Load32(key, 24);
			this.pad[3] = Common.Load32(key, 28);
		}

		[NullableContext(1)]
		public void SetKey(byte[] key, int offset)
		{
			this.SetKey(new Span<byte>(key, offset, 32));
		}

		private void Initialize()
		{
			this.h[0] = 0U;
			this.h[1] = 0U;
			this.h[2] = 0U;
			this.h[3] = 0U;
			this.h[4] = 0U;
			Array.Clear(this.buffer, 0, 16);
			this.leftover = 0;
			this.final = false;
		}

		private void Blocks(ReadOnlySpan<byte> m, int count)
		{
			uint num = this.final ? 0U : 16777216U;
			uint num2 = this.r[0];
			uint num3 = this.r[1];
			uint num4 = this.r[2];
			uint num5 = this.r[3];
			uint num6 = this.r[4];
			uint num7 = num3 * 5U;
			uint num8 = num4 * 5U;
			uint num9 = num5 * 5U;
			uint num10 = num6 * 5U;
			uint num11 = this.h[0];
			uint num12 = this.h[1];
			uint num13 = this.h[2];
			uint num14 = this.h[3];
			uint num15 = this.h[4];
			while (count >= 16)
			{
				num11 += (Common.Load32(m, 0) & 67108863U);
				num12 += (Common.Load32(m, 3) >> 2 & 67108863U);
				num13 += (Common.Load32(m, 6) >> 4 & 67108863U);
				num14 += (Common.Load32(m, 9) >> 6 & 67108863U);
				num15 += (Common.Load32(m, 12) >> 8 | num);
				ulong num16 = (ulong)num11 * (ulong)num2 + (ulong)num12 * (ulong)num10 + (ulong)num13 * (ulong)num9 + (ulong)num14 * (ulong)num8 + (ulong)num15 * (ulong)num7;
				ulong num17 = (ulong)num11 * (ulong)num3 + (ulong)num12 * (ulong)num2 + (ulong)num13 * (ulong)num10 + (ulong)num14 * (ulong)num9 + (ulong)num15 * (ulong)num8;
				ulong num18 = (ulong)num11 * (ulong)num4 + (ulong)num12 * (ulong)num3 + (ulong)num13 * (ulong)num2 + (ulong)num14 * (ulong)num10 + (ulong)num15 * (ulong)num9;
				ulong num19 = (ulong)num11 * (ulong)num5 + (ulong)num12 * (ulong)num4 + (ulong)num13 * (ulong)num3 + (ulong)num14 * (ulong)num2 + (ulong)num15 * (ulong)num10;
				ulong num20 = (ulong)num11 * (ulong)num6 + (ulong)num12 * (ulong)num5 + (ulong)num13 * (ulong)num4 + (ulong)num14 * (ulong)num3 + (ulong)num15 * (ulong)num2;
				uint num21 = (uint)(num16 >> 26);
				num11 = ((uint)num16 & 67108863U);
				num17 += (ulong)num21;
				num21 = (uint)(num17 >> 26);
				num12 = ((uint)num17 & 67108863U);
				num18 += (ulong)num21;
				num21 = (uint)(num18 >> 26);
				num13 = ((uint)num18 & 67108863U);
				num19 += (ulong)num21;
				num21 = (uint)(num19 >> 26);
				num14 = ((uint)num19 & 67108863U);
				ulong num22 = num20 + (ulong)num21;
				num21 = (uint)(num22 >> 26);
				num15 = ((uint)num22 & 67108863U);
				num11 += num21 * 5U;
				num21 = num11 >> 26;
				num11 &= 67108863U;
				num12 += num21;
				m = m.Slice(16);
				count -= 16;
			}
			this.h[0] = num11;
			this.h[1] = num12;
			this.h[2] = num13;
			this.h[3] = num14;
			this.h[4] = num15;
		}

		public void Final(Span<byte> tag)
		{
			if (this.leftover > 0)
			{
				int i = this.leftover;
				this.buffer[i++] = 1;
				while (i < 16)
				{
					this.buffer[i] = 0;
					i++;
				}
				this.final = true;
				this.Blocks(this.buffer, 16);
			}
			uint num = this.h[0];
			uint num2 = this.h[1];
			uint num3 = this.h[2];
			uint num4 = this.h[3];
			uint num5 = this.h[4];
			uint num6 = num2 >> 26;
			num2 &= 67108863U;
			num3 += num6;
			num6 = num3 >> 26;
			num3 &= 67108863U;
			num4 += num6;
			num6 = num4 >> 26;
			num4 &= 67108863U;
			num5 += num6;
			num6 = num5 >> 26;
			num5 &= 67108863U;
			num += num6 * 5U;
			num6 = num >> 26;
			num &= 67108863U;
			num2 += num6;
			uint num7 = num + 5U;
			num6 = num7 >> 26;
			num7 &= 67108863U;
			uint num8 = num2 + num6;
			num6 = num8 >> 26;
			num8 &= 67108863U;
			uint num9 = num3 + num6;
			num6 = num9 >> 26;
			num9 &= 67108863U;
			uint num10 = num4 + num6;
			num6 = num10 >> 26;
			num10 &= 67108863U;
			uint num11 = num5 + num6 - 67108864U;
			uint num12 = (num11 >> 31) - 1U;
			num7 &= num12;
			num8 &= num12;
			num9 &= num12;
			num10 &= num12;
			num11 &= num12;
			num12 = ~num12;
			num = ((num & num12) | num7);
			num2 = ((num2 & num12) | num8);
			num3 = ((num3 & num12) | num9);
			num4 = ((num4 & num12) | num10);
			num5 = ((num5 & num12) | num11);
			num = ((num | num2 << 26) & uint.MaxValue);
			num2 = ((num2 >> 6 | num3 << 20) & uint.MaxValue);
			num3 = ((num3 >> 12 | num4 << 14) & uint.MaxValue);
			num4 = ((num4 >> 18 | num5 << 8) & uint.MaxValue);
			ulong num13 = (ulong)num + (ulong)this.pad[0];
			num = (uint)num13;
			num13 = (ulong)num2 + (ulong)this.pad[1] + (num13 >> 32);
			num2 = (uint)num13;
			num13 = (ulong)num3 + (ulong)this.pad[2] + (num13 >> 32);
			num3 = (uint)num13;
			num13 = (ulong)num4 + (ulong)this.pad[3] + (num13 >> 32);
			num4 = (uint)num13;
			Common.Store(tag, 0, num);
			Common.Store(tag, 4, num2);
			Common.Store(tag, 8, num3);
			Common.Store(tag, 12, num4);
			this.Initialize();
		}

		public unsafe void Update(ReadOnlySpan<byte> bytes)
		{
			int num = bytes.Length;
			if (this.leftover != 0)
			{
				int num2 = 16 - this.leftover;
				if (num2 > num)
				{
					num2 = num;
				}
				for (int i = 0; i < num2; i++)
				{
					this.buffer[this.leftover + i] = *bytes[i];
				}
				num -= num2;
				bytes = bytes.Slice(num2);
				this.leftover += num2;
				if (this.leftover < 16)
				{
					return;
				}
				this.Blocks(this.buffer, 16);
				this.leftover = 0;
			}
			if (num >= 16)
			{
				int num3 = num & -16;
				this.Blocks(bytes, num3);
				bytes = bytes.Slice(num3);
				num -= num3;
			}
			if (num > 0)
			{
				for (int j = 0; j < num; j++)
				{
					this.buffer[this.leftover + j] = *bytes[j];
				}
				this.leftover += num;
			}
		}

		[NullableContext(1)]
		public void Update(byte[] bytes, int offset, int count)
		{
			Span<byte> span = new Span<byte>(bytes, offset, count);
			this.Update(span);
		}

		[NullableContext(1)]
		public byte[] Final()
		{
			byte[] array = new byte[16];
			this.Final(array);
			return array;
		}

		public bool Verify(ReadOnlySpan<byte> tag, ReadOnlySpan<byte> input)
		{
			this.Update(input);
			byte[] array = this.Final();
			return SafeComparison.Verify16(tag, array);
		}

		[NullableContext(1)]
		public bool Verify(byte[] tag, int tagOffset, byte[] input, int inputOffset, int inputCount)
		{
			Span<byte> span = new Span<byte>(tag, tagOffset, 16);
			Span<byte> span2 = new Span<byte>(input, inputOffset, inputCount);
			return this.Verify(span, span2);
		}

		private const int BlockLength = 16;

		public const int KeyLength = 32;

		public const int TagLength = 16;

		[Nullable(1)]
		private uint[] r = new uint[5];

		[Nullable(1)]
		private uint[] h = new uint[5];

		[Nullable(1)]
		private uint[] pad = new uint[4];

		private int leftover;

		[Nullable(1)]
		private byte[] buffer = new byte[16];

		private bool final;
	}
}
