using System;
using System.IO;
using System.Security.Cryptography;
using ns36;

namespace ns29
{
	internal static class Class402
	{
		private static ICryptoTransform smethod_0(byte[] byte_0, byte[] byte_1, bool bool_0)
		{
			ICryptoTransform result;
			using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
			{
				result = (bool_0 ? rijndaelManaged.CreateDecryptor(byte_0, byte_1) : rijndaelManaged.CreateEncryptor(byte_0, byte_1));
			}
			return result;
		}

		public unsafe static Enum21 smethod_1(byte[] byte_0)
		{
			void* ptr = stackalloc byte[8];
			if (byte_0 == null || byte_0.Length < 4)
			{
				return (Enum21)(-1);
			}
			using (Class402.Stream0 stream = new Class402.Stream0(byte_0))
			{
				*(int*)ptr = stream.method_3();
			}
			if (*(int*)ptr == 67324752)
			{
				return Enum21.const_0;
			}
			*(int*)((byte*)ptr + 4) = *(int*)ptr >> 24;
			*(int*)ptr = *(int*)ptr - (*(int*)((byte*)ptr + 4) << 24);
			if (*(int*)ptr == 8223355)
			{
				return (Enum21)(*(int*)((byte*)ptr + 4));
			}
			return (Enum21)(-2);
		}

		public unsafe static byte[] smethod_2(byte[] byte_0)
		{
			void* ptr = stackalloc byte[16];
			Class402.Stream0 stream = new Class402.Stream0(byte_0);
			byte[] array = new byte[0];
			int num = stream.method_3();
			int num2 = num >> 24;
			if (num - (num2 << 24) == 8223355)
			{
				switch (num2)
				{
				case 1:
					*(int*)ptr = stream.method_3();
					array = new byte[*(int*)ptr];
					*(int*)((byte*)ptr + 4) = 0;
					while (*(int*)((byte*)ptr + 4) < *(int*)ptr)
					{
						*(int*)((byte*)ptr + 8) = stream.method_3();
						*(int*)((byte*)ptr + 12) = stream.method_3();
						byte[] array2 = new byte[*(int*)((byte*)ptr + 8)];
						stream.Read(array2, 0, array2.Length);
						new Class402.Class403(array2).method_2(array, *(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 12));
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + *(int*)((byte*)ptr + 12);
					}
					goto IL_135;
				case 3:
				{
					byte[] byte_ = new byte[]
					{
						232,
						59,
						176,
						59,
						4,
						112,
						16,
						188,
						107,
						250,
						39,
						162,
						129,
						25,
						30,
						238
					};
					byte[] byte_2 = new byte[]
					{
						37,
						47,
						145,
						23,
						65,
						148,
						130,
						37,
						67,
						65,
						35,
						125,
						135,
						90,
						228,
						134
					};
					using (ICryptoTransform cryptoTransform = Class402.smethod_0(byte_, byte_2, true))
					{
						array = Class402.smethod_2(cryptoTransform.TransformFinalBlock(byte_0, 4, byte_0.Length - 4));
						goto IL_135;
					}
					goto IL_12A;
				}
				}
				throw new ArgumentOutOfRangeException("version", num2, "Selected compression algorithm is not supported.");
				IL_135:
				stream.Close();
				stream = null;
				return array;
			}
			IL_12A:
			throw new FormatException("Unknown Header");
		}

		public static byte[] smethod_3(byte[] byte_0)
		{
			return Class402.smethod_5(byte_0, Enum21.const_1, null, null);
		}

		public static byte[] smethod_4(byte[] byte_0, byte[] byte_1, byte[] byte_2)
		{
			return Class402.smethod_5(byte_0, Enum21.const_3, byte_1, byte_2);
		}

		private unsafe static byte[] smethod_5(byte[] byte_0, Enum21 enum21_0, byte[] byte_1, byte[] byte_2)
		{
			void* ptr = stackalloc byte[28];
			byte[] result;
			try
			{
				Class402.Stream0 stream = new Class402.Stream0();
				switch (enum21_0)
				{
				case Enum21.const_1:
					stream.method_1(25000571);
					stream.method_1(byte_0.Length);
					*(int*)((byte*)ptr + 16) = 0;
					while (*(int*)((byte*)ptr + 16) < byte_0.Length)
					{
						byte[] array = new byte[Math.Min(2097151, byte_0.Length - *(int*)((byte*)ptr + 16))];
						Buffer.BlockCopy(byte_0, *(int*)((byte*)ptr + 16), array, 0, array.Length);
						*(long*)ptr = stream.Position;
						stream.method_1(0);
						stream.method_1(array.Length);
						Class402.Class408 @class = new Class402.Class408();
						@class.method_1(array);
						while (!@class.IsNeedingInput)
						{
							byte[] array2 = new byte[512];
							*(int*)((byte*)ptr + 20) = @class.method_2(array2);
							if (*(int*)((byte*)ptr + 20) <= 0)
							{
								break;
							}
							stream.Write(array2, 0, *(int*)((byte*)ptr + 20));
						}
						@class.method_0();
						while (!@class.IsFinished)
						{
							byte[] array3 = new byte[512];
							*(int*)((byte*)ptr + 24) = @class.method_2(array3);
							if (*(int*)((byte*)ptr + 24) <= 0)
							{
								break;
							}
							stream.Write(array3, 0, *(int*)((byte*)ptr + 24));
						}
						*(long*)((byte*)ptr + 8) = stream.Position;
						stream.Position = *(long*)ptr;
						stream.method_1((int)@class.TotalOut);
						stream.Position = *(long*)((byte*)ptr + 8);
						*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + array.Length;
					}
					goto IL_1B3;
				case Enum21.const_3:
				{
					stream.method_1(58555003);
					byte[] array4 = Class402.smethod_5(byte_0, Enum21.const_1, null, null);
					using (ICryptoTransform cryptoTransform = Class402.smethod_0(byte_1, byte_2, false))
					{
						byte[] array5 = cryptoTransform.TransformFinalBlock(array4, 0, array4.Length);
						stream.Write(array5, 0, array5.Length);
					}
					goto IL_1B3;
				}
				}
				throw new ArgumentOutOfRangeException("algorithm", enum21_0, "Selected compression algorithm is not supported.");
				IL_1B3:
				stream.Flush();
				stream.Close();
				result = stream.ToArray();
			}
			catch (Exception ex)
			{
				Class402.string_0 = "ERR 2003: " + ex.Message;
				throw;
			}
			return result;
		}

		public static string string_0;

		internal sealed class Class403
		{
			public Class403(byte[] byte_0)
			{
				this.class404_0 = new Class402.Class404();
				this.class405_0 = new Class402.Class405();
				this.int_17 = 2;
				this.class404_0.method_5(byte_0, 0, byte_0.Length);
			}

			private unsafe bool method_0()
			{
				void* ptr = stackalloc byte[12];
				int i = this.class405_0.method_5();
				while (i >= 258)
				{
					*(int*)ptr = this.int_17;
					int num;
					switch (*(int*)ptr)
					{
					case 7:
						while (((num = this.class406_0.method_1(this.class404_0)) & -256) == 0)
						{
							this.class405_0.method_0(num);
							if (--i < 258)
							{
								return true;
							}
						}
						if (num >= 257)
						{
							this.int_19 = Class402.Class403.int_0[num - 257];
							this.int_18 = Class402.Class403.int_1[num - 257];
							goto IL_A4;
						}
						if (num < 0)
						{
							return false;
						}
						this.class406_1 = null;
						this.class406_0 = null;
						this.int_17 = 2;
						return true;
					case 8:
						goto IL_A4;
					case 9:
						goto IL_FD;
					case 10:
						break;
					default:
						continue;
					}
					IL_130:
					if (this.int_18 > 0)
					{
						this.int_17 = 10;
						*(int*)((byte*)ptr + 8) = this.class404_0.method_0(this.int_18);
						if (*(int*)((byte*)ptr + 8) < 0)
						{
							return false;
						}
						this.class404_0.method_1(this.int_18);
						this.int_20 += *(int*)((byte*)ptr + 8);
					}
					this.class405_0.method_2(this.int_19, this.int_20);
					i -= this.int_19;
					this.int_17 = 7;
					continue;
					IL_FD:
					num = this.class406_1.method_1(this.class404_0);
					if (num >= 0)
					{
						this.int_20 = Class402.Class403.int_2[num];
						this.int_18 = Class402.Class403.int_3[num];
						goto IL_130;
					}
					return false;
					IL_A4:
					if (this.int_18 > 0)
					{
						this.int_17 = 8;
						*(int*)((byte*)ptr + 4) = this.class404_0.method_0(this.int_18);
						if (*(int*)((byte*)ptr + 4) < 0)
						{
							return false;
						}
						this.class404_0.method_1(this.int_18);
						this.int_19 += *(int*)((byte*)ptr + 4);
					}
					this.int_17 = 9;
					goto IL_FD;
				}
				return true;
			}

			private unsafe bool method_1()
			{
				void* ptr = stackalloc byte[12];
				*(int*)((byte*)ptr + 4) = this.int_17;
				switch (*(int*)((byte*)ptr + 4))
				{
				case 2:
					if (this.bool_0)
					{
						this.int_17 = 12;
						return false;
					}
					*(int*)ptr = this.class404_0.method_0(3);
					if (*(int*)ptr < 0)
					{
						return false;
					}
					this.class404_0.method_1(3);
					if ((*(int*)ptr & 1) != 0)
					{
						this.bool_0 = true;
					}
					switch (*(int*)ptr >> 1)
					{
					case 0:
						this.class404_0.method_2();
						this.int_17 = 3;
						break;
					case 1:
						this.class406_0 = Class402.Class406.class406_0;
						this.class406_1 = Class402.Class406.class406_1;
						this.int_17 = 7;
						break;
					case 2:
						this.class407_0 = new Class402.Class407();
						this.int_17 = 6;
						break;
					}
					return true;
				case 3:
					if ((this.int_21 = this.class404_0.method_0(16)) < 0)
					{
						return false;
					}
					this.class404_0.method_1(16);
					this.int_17 = 4;
					break;
				case 4:
					break;
				case 5:
					goto IL_141;
				case 6:
					if (!this.class407_0.method_0(this.class404_0))
					{
						return false;
					}
					this.class406_0 = this.class407_0.method_1();
					this.class406_1 = this.class407_0.method_2();
					this.int_17 = 7;
					goto IL_1CB;
				case 7:
				case 8:
				case 9:
				case 10:
					goto IL_1CB;
				case 11:
					return false;
				case 12:
					return false;
				default:
					return false;
				}
				if (this.class404_0.method_0(16) < 0)
				{
					return false;
				}
				this.class404_0.method_1(16);
				this.int_17 = 5;
				IL_141:
				*(int*)((byte*)ptr + 8) = this.class405_0.method_3(this.class404_0, this.int_21);
				this.int_21 -= *(int*)((byte*)ptr + 8);
				if (this.int_21 == 0)
				{
					this.int_17 = 2;
					return true;
				}
				return !this.class404_0.IsNeedingInput;
				IL_1CB:
				return this.method_0();
			}

			public unsafe int method_2(byte[] byte_0, int int_22, int int_23)
			{
				void* ptr = stackalloc byte[8];
				*(int*)ptr = 0;
				for (;;)
				{
					if (this.int_17 != 11)
					{
						*(int*)((byte*)ptr + 4) = this.class405_0.method_7(byte_0, int_22, int_23);
						int_22 += *(int*)((byte*)ptr + 4);
						*(int*)ptr = *(int*)ptr + *(int*)((byte*)ptr + 4);
						int_23 -= *(int*)((byte*)ptr + 4);
						if (int_23 == 0)
						{
							goto Block_4;
						}
					}
					if (!this.method_1())
					{
						if (this.class405_0.method_6() <= 0)
						{
							break;
						}
						if (this.int_17 == 11)
						{
							break;
						}
					}
				}
				goto IL_6B;
				Block_4:
				return *(int*)ptr;
				IL_6B:
				return *(int*)ptr;
			}

			private static readonly int[] int_0 = new int[]
			{
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10,
				11,
				13,
				15,
				17,
				19,
				23,
				27,
				31,
				35,
				43,
				51,
				59,
				67,
				83,
				99,
				115,
				131,
				163,
				195,
				227,
				258
			};

			private static readonly int[] int_1 = new int[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				1,
				1,
				1,
				2,
				2,
				2,
				2,
				3,
				3,
				3,
				3,
				4,
				4,
				4,
				4,
				5,
				5,
				5,
				5,
				0
			};

			private static readonly int[] int_2 = new int[]
			{
				1,
				2,
				3,
				4,
				5,
				7,
				9,
				13,
				17,
				25,
				33,
				49,
				65,
				97,
				129,
				193,
				257,
				385,
				513,
				769,
				1025,
				1537,
				2049,
				3073,
				4097,
				6145,
				8193,
				12289,
				16385,
				24577
			};

			private static readonly int[] int_3 = new int[]
			{
				0,
				0,
				0,
				0,
				1,
				1,
				2,
				2,
				3,
				3,
				4,
				4,
				5,
				5,
				6,
				6,
				7,
				7,
				8,
				8,
				9,
				9,
				10,
				10,
				11,
				11,
				12,
				12,
				13,
				13
			};

			private const int int_4 = 0;

			private const int int_5 = 1;

			private const int int_6 = 2;

			private const int int_7 = 3;

			private const int int_8 = 4;

			private const int int_9 = 5;

			private const int int_10 = 6;

			private const int int_11 = 7;

			private const int int_12 = 8;

			private const int int_13 = 9;

			private const int int_14 = 10;

			private const int int_15 = 11;

			private const int int_16 = 12;

			private int int_17;

			private int int_18;

			private int int_19;

			private int int_20;

			private int int_21;

			private bool bool_0;

			private Class402.Class404 class404_0;

			private Class402.Class405 class405_0;

			private Class402.Class407 class407_0;

			private Class402.Class406 class406_0;

			private Class402.Class406 class406_1;
		}

		internal sealed class Class404
		{
			public int method_0(int int_3)
			{
				if (this.int_2 < int_3)
				{
					if (this.int_0 == this.int_1)
					{
						return -1;
					}
					uint num = this.uint_0;
					byte[] array = this.byte_0;
					int num2 = this.int_0;
					this.int_0 = num2 + 1;
					uint num3 = array[num2] & 255U;
					byte[] array2 = this.byte_0;
					num2 = this.int_0;
					this.int_0 = num2 + 1;
					this.uint_0 = (num | (num3 | (array2[num2] & 255U) << 8) << this.int_2);
					this.int_2 += 16;
				}
				return (int)((ulong)this.uint_0 & (ulong)((long)((1 << int_3) - 1)));
			}

			public void method_1(int int_3)
			{
				this.uint_0 >>= int_3;
				this.int_2 -= int_3;
			}

			public int AvailableBits
			{
				get
				{
					return this.int_2;
				}
			}

			public int AvailableBytes
			{
				get
				{
					return this.int_1 - this.int_0 + (this.int_2 >> 3);
				}
			}

			public void method_2()
			{
				this.uint_0 >>= (this.int_2 & 7);
				this.int_2 &= -8;
			}

			public bool IsNeedingInput
			{
				get
				{
					return this.int_0 == this.int_1;
				}
			}

			public unsafe int method_3(byte[] byte_1, int int_3, int int_4)
			{
				void* ptr = stackalloc byte[12];
				*(int*)ptr = 0;
				while (this.int_2 > 0 && int_4 > 0)
				{
					byte_1[int_3++] = (byte)this.uint_0;
					this.uint_0 >>= 8;
					this.int_2 -= 8;
					int_4--;
					*(int*)ptr = *(int*)ptr + 1;
				}
				if (int_4 == 0)
				{
					return *(int*)ptr;
				}
				*(int*)((byte*)ptr + 4) = this.int_1 - this.int_0;
				if (int_4 > *(int*)((byte*)ptr + 4))
				{
					int_4 = *(int*)((byte*)ptr + 4);
				}
				Array.Copy(this.byte_0, this.int_0, byte_1, int_3, int_4);
				this.int_0 += int_4;
				if ((this.int_0 - this.int_1 & 1) != 0)
				{
					byte[] array = this.byte_0;
					*(int*)((byte*)ptr + 8) = this.int_0;
					this.int_0 = *(int*)((byte*)ptr + 8) + 1;
					this.uint_0 = (array[*(int*)((byte*)ptr + 8)] & 255U);
					this.int_2 = 8;
				}
				return *(int*)ptr + int_4;
			}

			public void method_4()
			{
				this.int_2 = 0;
				this.int_1 = 0;
				this.int_0 = 0;
				this.uint_0 = 0U;
			}

			public void method_5(byte[] byte_1, int int_3, int int_4)
			{
				if (this.int_0 < this.int_1)
				{
					throw new InvalidOperationException();
				}
				int num = int_3 + int_4;
				if (0 <= int_3 && int_3 <= num && num <= byte_1.Length)
				{
					if ((int_4 & 1) != 0)
					{
						this.uint_0 |= (uint)((uint)(byte_1[int_3++] & byte.MaxValue) << this.int_2);
						this.int_2 += 8;
					}
					this.byte_0 = byte_1;
					this.int_0 = int_3;
					this.int_1 = num;
					return;
				}
				throw new ArgumentOutOfRangeException();
			}

			private byte[] byte_0;

			private int int_0;

			private int int_1;

			private uint uint_0;

			private int int_2;
		}

		internal sealed class Class405
		{
			public void method_0(int int_4)
			{
				int num = this.int_3;
				this.int_3 = num + 1;
				if (num == 32768)
				{
					throw new InvalidOperationException();
				}
				byte[] array = this.byte_0;
				num = this.int_2;
				this.int_2 = num + 1;
				array[num] = (byte)int_4;
				this.int_2 &= 32767;
			}

			private void method_1(int int_4, int int_5)
			{
				while (int_5-- > 0)
				{
					byte[] array = this.byte_0;
					int num = this.int_2;
					this.int_2 = num + 1;
					array[num] = this.byte_0[int_4++];
					this.int_2 &= 32767;
					int_4 &= 32767;
				}
			}

			public void method_2(int int_4, int int_5)
			{
				if ((this.int_3 += int_4) > 32768)
				{
					throw new InvalidOperationException();
				}
				int num = this.int_2 - int_5 & 32767;
				int num2 = 32768 - int_4;
				if (num > num2 || this.int_2 >= num2)
				{
					this.method_1(num, int_4);
					return;
				}
				if (int_4 <= int_5)
				{
					Array.Copy(this.byte_0, num, this.byte_0, this.int_2, int_4);
					this.int_2 += int_4;
					return;
				}
				while (int_4-- > 0)
				{
					byte[] array = this.byte_0;
					int num3 = this.int_2;
					this.int_2 = num3 + 1;
					array[num3] = this.byte_0[num++];
				}
			}

			public unsafe int method_3(Class402.Class404 class404_0, int int_4)
			{
				void* ptr = stackalloc byte[8];
				int_4 = Math.Min(Math.Min(int_4, 32768 - this.int_3), class404_0.AvailableBytes);
				*(int*)((byte*)ptr + 4) = 32768 - this.int_2;
				if (int_4 > *(int*)((byte*)ptr + 4))
				{
					*(int*)ptr = class404_0.method_3(this.byte_0, this.int_2, *(int*)((byte*)ptr + 4));
					if (*(int*)ptr == *(int*)((byte*)ptr + 4))
					{
						*(int*)ptr = *(int*)ptr + class404_0.method_3(this.byte_0, 0, int_4 - *(int*)((byte*)ptr + 4));
					}
				}
				else
				{
					*(int*)ptr = class404_0.method_3(this.byte_0, this.int_2, int_4);
				}
				this.int_2 = (this.int_2 + *(int*)ptr & 32767);
				this.int_3 += *(int*)ptr;
				return *(int*)ptr;
			}

			public void method_4(byte[] byte_1, int int_4, int int_5)
			{
				if (this.int_3 > 0)
				{
					throw new InvalidOperationException();
				}
				if (int_5 > 32768)
				{
					int_4 += int_5 - 32768;
					int_5 = 32768;
				}
				Array.Copy(byte_1, int_4, this.byte_0, 0, int_5);
				this.int_2 = (int_5 & 32767);
			}

			public int method_5()
			{
				return 32768 - this.int_3;
			}

			public int method_6()
			{
				return this.int_3;
			}

			public unsafe int method_7(byte[] byte_1, int int_4, int int_5)
			{
				void* ptr = stackalloc byte[12];
				*(int*)ptr = this.int_2;
				if (int_5 > this.int_3)
				{
					int_5 = this.int_3;
				}
				else
				{
					*(int*)ptr = (this.int_2 - this.int_3 + int_5 & 32767);
				}
				*(int*)((byte*)ptr + 4) = int_5;
				*(int*)((byte*)ptr + 8) = int_5 - *(int*)ptr;
				if (*(int*)((byte*)ptr + 8) > 0)
				{
					Array.Copy(this.byte_0, 32768 - *(int*)((byte*)ptr + 8), byte_1, int_4, *(int*)((byte*)ptr + 8));
					int_4 += *(int*)((byte*)ptr + 8);
					int_5 = *(int*)ptr;
				}
				Array.Copy(this.byte_0, *(int*)ptr - int_5, byte_1, int_4, int_5);
				this.int_3 -= *(int*)((byte*)ptr + 4);
				if (this.int_3 < 0)
				{
					throw new InvalidOperationException();
				}
				return *(int*)((byte*)ptr + 4);
			}

			public void method_8()
			{
				this.int_2 = 0;
				this.int_3 = 0;
			}

			private const int int_0 = 32768;

			private const int int_1 = 32767;

			private byte[] byte_0 = new byte[32768];

			private int int_2;

			private int int_3;
		}

		internal sealed class Class406
		{
			static Class406()
			{
				byte[] array = new byte[288];
				int i = 0;
				while (i < 144)
				{
					array[i++] = 8;
				}
				while (i < 256)
				{
					array[i++] = 9;
				}
				while (i < 280)
				{
					array[i++] = 7;
				}
				while (i < 288)
				{
					array[i++] = 8;
				}
				Class402.Class406.class406_0 = new Class402.Class406(array);
				array = new byte[32];
				i = 0;
				while (i < 32)
				{
					array[i++] = 5;
				}
				Class402.Class406.class406_1 = new Class402.Class406(array);
			}

			public Class406(byte[] byte_0)
			{
				this.method_0(byte_0);
			}

			private unsafe void method_0(byte[] byte_0)
			{
				void* ptr = stackalloc byte[64];
				int[] array = new int[16];
				int[] array2 = new int[16];
				*(int*)((byte*)ptr + 12) = 0;
				while (*(int*)((byte*)ptr + 12) < byte_0.Length)
				{
					*(int*)((byte*)ptr + 16) = (int)byte_0[*(int*)((byte*)ptr + 12)];
					if (*(int*)((byte*)ptr + 16) > 0)
					{
						array[*(int*)((byte*)ptr + 16)]++;
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)ptr = 0;
				*(int*)((byte*)ptr + 4) = 512;
				*(int*)((byte*)ptr + 20) = 1;
				while (*(int*)((byte*)ptr + 20) <= 15)
				{
					array2[*(int*)((byte*)ptr + 20)] = *(int*)ptr;
					*(int*)ptr = *(int*)ptr + (array[*(int*)((byte*)ptr + 20)] << 16 - *(int*)((byte*)ptr + 20));
					if (*(int*)((byte*)ptr + 20) >= 10)
					{
						*(int*)((byte*)ptr + 24) = (array2[*(int*)((byte*)ptr + 20)] & 130944);
						*(int*)((byte*)ptr + 28) = (*(int*)ptr & 130944);
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + (*(int*)((byte*)ptr + 28) - *(int*)((byte*)ptr + 24) >> 16 - *(int*)((byte*)ptr + 20));
					}
					*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
				}
				this.short_0 = new short[*(int*)((byte*)ptr + 4)];
				*(int*)((byte*)ptr + 8) = 512;
				*(int*)((byte*)ptr + 32) = 15;
				while (*(int*)((byte*)ptr + 32) >= 10)
				{
					*(int*)((byte*)ptr + 36) = (*(int*)ptr & 130944);
					*(int*)ptr = *(int*)ptr - (array[*(int*)((byte*)ptr + 32)] << 16 - *(int*)((byte*)ptr + 32));
					*(int*)((byte*)ptr + 40) = (*(int*)ptr & 130944);
					while (*(int*)((byte*)ptr + 40) < *(int*)((byte*)ptr + 36))
					{
						this.short_0[(int)Class402.Class409.smethod_0(*(int*)((byte*)ptr + 40))] = (short)(-(*(int*)((byte*)ptr + 8)) << 4 | *(int*)((byte*)ptr + 32));
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + (1 << *(int*)((byte*)ptr + 32) - 9);
						*(int*)((byte*)ptr + 40) = *(int*)((byte*)ptr + 40) + 128;
					}
					*(int*)((byte*)ptr + 32) = *(int*)((byte*)ptr + 32) - 1;
				}
				*(int*)((byte*)ptr + 44) = 0;
				while (*(int*)((byte*)ptr + 44) < byte_0.Length)
				{
					*(int*)((byte*)ptr + 48) = (int)byte_0[*(int*)((byte*)ptr + 44)];
					if (*(int*)((byte*)ptr + 48) != 0)
					{
						*(int*)ptr = array2[*(int*)((byte*)ptr + 48)];
						*(int*)((byte*)ptr + 52) = (int)Class402.Class409.smethod_0(*(int*)ptr);
						if (*(int*)((byte*)ptr + 48) <= 9)
						{
							do
							{
								this.short_0[*(int*)((byte*)ptr + 52)] = (short)(*(int*)((byte*)ptr + 44) << 4 | *(int*)((byte*)ptr + 48));
								*(int*)((byte*)ptr + 52) = *(int*)((byte*)ptr + 52) + (1 << *(int*)((byte*)ptr + 48));
							}
							while (*(int*)((byte*)ptr + 52) < 512);
						}
						else
						{
							*(int*)((byte*)ptr + 56) = (int)this.short_0[*(int*)((byte*)ptr + 52) & 511];
							*(int*)((byte*)ptr + 60) = 1 << (*(int*)((byte*)ptr + 56) & 15);
							*(int*)((byte*)ptr + 56) = -(*(int*)((byte*)ptr + 56) >> 4);
							do
							{
								this.short_0[*(int*)((byte*)ptr + 56) | *(int*)((byte*)ptr + 52) >> 9] = (short)(*(int*)((byte*)ptr + 44) << 4 | *(int*)((byte*)ptr + 48));
								*(int*)((byte*)ptr + 52) = *(int*)((byte*)ptr + 52) + (1 << *(int*)((byte*)ptr + 48));
							}
							while (*(int*)((byte*)ptr + 52) < *(int*)((byte*)ptr + 60));
						}
						array2[*(int*)((byte*)ptr + 48)] = *(int*)ptr + (1 << 16 - *(int*)((byte*)ptr + 48));
					}
					*(int*)((byte*)ptr + 44) = *(int*)((byte*)ptr + 44) + 1;
				}
			}

			public unsafe int method_1(Class402.Class404 class404_0)
			{
				void* ptr = stackalloc byte[16];
				int num;
				if ((num = class404_0.method_0(9)) >= 0)
				{
					int num2;
					if ((num2 = (int)this.short_0[num]) >= 0)
					{
						class404_0.method_1(num2 & 15);
						return num2 >> 4;
					}
					*(int*)ptr = -(num2 >> 4);
					*(int*)((byte*)ptr + 4) = (num2 & 15);
					if ((num = class404_0.method_0(*(int*)((byte*)ptr + 4))) >= 0)
					{
						num2 = (int)this.short_0[*(int*)ptr | num >> 9];
						class404_0.method_1(num2 & 15);
						return num2 >> 4;
					}
					*(int*)((byte*)ptr + 8) = class404_0.AvailableBits;
					num = class404_0.method_0(*(int*)((byte*)ptr + 8));
					num2 = (int)this.short_0[*(int*)ptr | num >> 9];
					if ((num2 & 15) <= *(int*)((byte*)ptr + 8))
					{
						class404_0.method_1(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
				else
				{
					*(int*)((byte*)ptr + 12) = class404_0.AvailableBits;
					num = class404_0.method_0(*(int*)((byte*)ptr + 12));
					int num2 = (int)this.short_0[num];
					if (num2 >= 0 && (num2 & 15) <= *(int*)((byte*)ptr + 12))
					{
						class404_0.method_1(num2 & 15);
						return num2 >> 4;
					}
					return -1;
				}
			}

			private const int int_0 = 15;

			private short[] short_0;

			public static readonly Class402.Class406 class406_0;

			public static readonly Class402.Class406 class406_1;
		}

		internal sealed class Class407
		{
			public unsafe bool method_0(Class402.Class404 class404_0)
			{
				void* ptr = stackalloc byte[16];
				for (;;)
				{
					*(int*)ptr = this.int_8;
					switch (*(int*)ptr)
					{
					case 0:
						this.int_9 = class404_0.method_0(5);
						if (this.int_9 >= 0)
						{
							this.int_9 += 257;
							class404_0.method_1(5);
							this.int_8 = 1;
							goto IL_1DD;
						}
						return false;
					case 1:
						goto IL_1DD;
					case 2:
						goto IL_18F;
					case 3:
						goto IL_156;
					case 4:
						break;
					case 5:
						goto IL_0B;
					default:
						continue;
					}
					IL_D8:
					int num;
					while (((num = this.class406_0.method_1(class404_0)) & -16) == 0)
					{
						byte[] array = this.byte_1;
						*(int*)((byte*)ptr + 8) = this.int_14;
						this.int_14 = *(int*)((byte*)ptr + 8) + 1;
						array[*(int*)((byte*)ptr + 8)] = (this.byte_2 = (byte)num);
						if (this.int_14 == this.int_12)
						{
							return true;
						}
					}
					if (num >= 0)
					{
						if (num >= 17)
						{
							this.byte_2 = 0;
						}
						this.int_13 = num - 16;
						this.int_8 = 5;
						goto IL_0B;
					}
					return false;
					IL_156:
					while (this.int_14 < this.int_11)
					{
						*(int*)((byte*)ptr + 4) = class404_0.method_0(3);
						if (*(int*)((byte*)ptr + 4) < 0)
						{
							return false;
						}
						class404_0.method_1(3);
						this.byte_0[Class402.Class407.int_15[this.int_14]] = (byte)(*(int*)((byte*)ptr + 4));
						this.int_14++;
					}
					this.class406_0 = new Class402.Class406(this.byte_0);
					this.byte_0 = null;
					this.int_14 = 0;
					this.int_8 = 4;
					goto IL_D8;
					IL_0B:
					*(int*)((byte*)ptr + 12) = Class402.Class407.int_7[this.int_13];
					int num2 = class404_0.method_0(*(int*)((byte*)ptr + 12));
					if (num2 < 0)
					{
						return false;
					}
					class404_0.method_1(*(int*)((byte*)ptr + 12));
					num2 += Class402.Class407.int_6[this.int_13];
					while (num2-- > 0)
					{
						byte[] array2 = this.byte_1;
						*(int*)((byte*)ptr + 8) = this.int_14;
						this.int_14 = *(int*)((byte*)ptr + 8) + 1;
						array2[*(int*)((byte*)ptr + 8)] = this.byte_2;
					}
					if (this.int_14 == this.int_12)
					{
						break;
					}
					this.int_8 = 4;
					continue;
					IL_18F:
					this.int_11 = class404_0.method_0(4);
					if (this.int_11 >= 0)
					{
						this.int_11 += 4;
						class404_0.method_1(4);
						this.byte_0 = new byte[19];
						this.int_14 = 0;
						this.int_8 = 3;
						goto IL_156;
					}
					return false;
					IL_1DD:
					this.int_10 = class404_0.method_0(5);
					if (this.int_10 >= 0)
					{
						this.int_10++;
						class404_0.method_1(5);
						this.int_12 = this.int_9 + this.int_10;
						this.byte_1 = new byte[this.int_12];
						this.int_8 = 2;
						goto IL_18F;
					}
					return false;
				}
				return true;
			}

			public Class402.Class406 method_1()
			{
				byte[] destinationArray = new byte[this.int_9];
				Array.Copy(this.byte_1, 0, destinationArray, 0, this.int_9);
				return new Class402.Class406(destinationArray);
			}

			public Class402.Class406 method_2()
			{
				byte[] destinationArray = new byte[this.int_10];
				Array.Copy(this.byte_1, this.int_9, destinationArray, 0, this.int_10);
				return new Class402.Class406(destinationArray);
			}

			private const int int_0 = 0;

			private const int int_1 = 1;

			private const int int_2 = 2;

			private const int int_3 = 3;

			private const int int_4 = 4;

			private const int int_5 = 5;

			private static readonly int[] int_6 = new int[]
			{
				3,
				3,
				11
			};

			private static readonly int[] int_7 = new int[]
			{
				2,
				3,
				7
			};

			private byte[] byte_0;

			private byte[] byte_1;

			private Class402.Class406 class406_0;

			private int int_8;

			private int int_9;

			private int int_10;

			private int int_11;

			private int int_12;

			private int int_13;

			private byte byte_2;

			private int int_14;

			private static readonly int[] int_15 = new int[]
			{
				16,
				17,
				18,
				0,
				8,
				7,
				9,
				6,
				10,
				5,
				11,
				4,
				12,
				3,
				13,
				2,
				14,
				1,
				15
			};
		}

		internal sealed class Class408
		{
			public Class408()
			{
				this.class412_0 = new Class402.Class412();
				this.class411_0 = new Class402.Class411(this.class412_0);
			}

			public long TotalOut
			{
				get
				{
					return this.long_0;
				}
			}

			public void method_0()
			{
				this.int_6 |= 12;
			}

			public bool IsFinished
			{
				get
				{
					return this.int_6 == 30 && this.class412_0.IsFlushed;
				}
			}

			public bool IsNeedingInput
			{
				get
				{
					return this.class411_0.method_8();
				}
			}

			public void method_1(byte[] byte_0)
			{
				this.class411_0.method_7(byte_0);
			}

			public unsafe int method_2(byte[] byte_0)
			{
				void* ptr = stackalloc byte[20];
				*(int*)ptr = 0;
				*(int*)((byte*)ptr + 4) = byte_0.Length;
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 4);
				for (;;)
				{
					*(int*)((byte*)ptr + 12) = this.class412_0.method_4(byte_0, *(int*)ptr, *(int*)((byte*)ptr + 4));
					*(int*)ptr = *(int*)ptr + *(int*)((byte*)ptr + 12);
					this.long_0 += (long)(*(int*)((byte*)ptr + 12));
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) - *(int*)((byte*)ptr + 12);
					if (*(int*)((byte*)ptr + 4) == 0 || this.int_6 == 30)
					{
						goto IL_11E;
					}
					if (!this.class411_0.method_6((this.int_6 & 4) != 0, (this.int_6 & 8) != 0))
					{
						if (this.int_6 == 16)
						{
							break;
						}
						if (this.int_6 == 20)
						{
							*(int*)((byte*)ptr + 16) = 8 + (-this.class412_0.BitCount & 7);
							while (*(int*)((byte*)ptr + 16) > 0)
							{
								this.class412_0.method_3(2, 10);
								*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) - 10;
							}
							this.int_6 = 16;
						}
						else if (this.int_6 == 28)
						{
							this.class412_0.method_2();
							this.int_6 = 30;
						}
					}
				}
				return *(int*)((byte*)ptr + 8) - *(int*)((byte*)ptr + 4);
				IL_11E:
				return *(int*)((byte*)ptr + 8) - *(int*)((byte*)ptr + 4);
			}

			private const int int_0 = 4;

			private const int int_1 = 8;

			private const int int_2 = 16;

			private const int int_3 = 20;

			private const int int_4 = 28;

			private const int int_5 = 30;

			private int int_6 = 16;

			private long long_0;

			private Class402.Class412 class412_0;

			private Class402.Class411 class411_0;
		}

		internal sealed class Class409
		{
			public static short smethod_0(int int_11)
			{
				return (short)((int)Class402.Class409.byte_0[int_11 & 15] << 12 | (int)Class402.Class409.byte_0[int_11 >> 4 & 15] << 8 | (int)Class402.Class409.byte_0[int_11 >> 8 & 15] << 4 | (int)Class402.Class409.byte_0[int_11 >> 12]);
			}

			static Class409()
			{
				int i = 0;
				while (i < 144)
				{
					Class402.Class409.short_1[i] = Class402.Class409.smethod_0(48 + i << 8);
					Class402.Class409.byte_2[i++] = 8;
				}
				while (i < 256)
				{
					Class402.Class409.short_1[i] = Class402.Class409.smethod_0(256 + i << 7);
					Class402.Class409.byte_2[i++] = 9;
				}
				while (i < 280)
				{
					Class402.Class409.short_1[i] = Class402.Class409.smethod_0(-256 + i << 9);
					Class402.Class409.byte_2[i++] = 7;
				}
				while (i < 286)
				{
					Class402.Class409.short_1[i] = Class402.Class409.smethod_0(-88 + i << 8);
					Class402.Class409.byte_2[i++] = 8;
				}
				Class402.Class409.short_2 = new short[30];
				Class402.Class409.byte_3 = new byte[30];
				for (i = 0; i < 30; i++)
				{
					Class402.Class409.short_2[i] = Class402.Class409.smethod_0(i << 11);
					Class402.Class409.byte_3[i] = 5;
				}
			}

			public Class409(Class402.Class412 class412_1)
			{
				this.class412_0 = class412_1;
				this.class410_0 = new Class402.Class409.Class410(this, 286, 257, 15);
				this.class410_1 = new Class402.Class409.Class410(this, 30, 1, 15);
				this.class410_2 = new Class402.Class409.Class410(this, 19, 4, 7);
				this.short_0 = new short[16384];
				this.byte_1 = new byte[16384];
			}

			public void method_0()
			{
				this.int_9 = 0;
				this.int_10 = 0;
			}

			private int method_1(int int_11)
			{
				if (int_11 == 255)
				{
					return 285;
				}
				int num = 257;
				while (int_11 >= 8)
				{
					num += 4;
					int_11 >>= 1;
				}
				return num + int_11;
			}

			private int method_2(int int_11)
			{
				int num = 0;
				while (int_11 >= 4)
				{
					num += 2;
					int_11 >>= 1;
				}
				return num + int_11;
			}

			public void method_3(int int_11)
			{
				this.class410_2.method_2();
				this.class410_0.method_2();
				this.class410_1.method_2();
				this.class412_0.method_3(this.class410_0.int_1 - 257, 5);
				this.class412_0.method_3(this.class410_1.int_1 - 1, 5);
				this.class412_0.method_3(int_11 - 4, 4);
				for (int i = 0; i < int_11; i++)
				{
					this.class412_0.method_3((int)this.class410_2.byte_0[Class402.Class409.int_8[i]], 3);
				}
				this.class410_0.method_7(this.class410_2);
				this.class410_1.method_7(this.class410_2);
			}

			public unsafe void method_4()
			{
				void* ptr = stackalloc byte[20];
				*(int*)ptr = 0;
				while (*(int*)ptr < this.int_9)
				{
					*(int*)((byte*)ptr + 4) = (int)(this.byte_1[*(int*)ptr] & byte.MaxValue);
					int num = (int)this.short_0[*(int*)ptr];
					if (num-- != 0)
					{
						*(int*)((byte*)ptr + 8) = this.method_1(*(int*)((byte*)ptr + 4));
						this.class410_0.method_0(*(int*)((byte*)ptr + 8));
						*(int*)((byte*)ptr + 12) = (*(int*)((byte*)ptr + 8) - 261) / 4;
						if (*(int*)((byte*)ptr + 12) > 0 && *(int*)((byte*)ptr + 12) <= 5)
						{
							this.class412_0.method_3(*(int*)((byte*)ptr + 4) & (1 << *(int*)((byte*)ptr + 12)) - 1, *(int*)((byte*)ptr + 12));
						}
						*(int*)((byte*)ptr + 16) = this.method_2(num);
						this.class410_1.method_0(*(int*)((byte*)ptr + 16));
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 16) / 2 - 1;
						if (*(int*)((byte*)ptr + 12) > 0)
						{
							this.class412_0.method_3(num & (1 << *(int*)((byte*)ptr + 12)) - 1, *(int*)((byte*)ptr + 12));
						}
					}
					else
					{
						this.class410_0.method_0(*(int*)((byte*)ptr + 4));
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				this.class410_0.method_0(256);
			}

			public void method_5(byte[] byte_4, int int_11, int int_12, bool bool_0)
			{
				this.class412_0.method_3(bool_0 ? 1 : 0, 3);
				this.class412_0.method_2();
				this.class412_0.method_0(int_12);
				this.class412_0.method_0(~int_12);
				this.class412_0.method_1(byte_4, int_11, int_12);
				this.method_0();
			}

			public unsafe void method_6(byte[] byte_4, int int_11, int int_12, bool bool_0)
			{
				void* ptr = stackalloc byte[24];
				short[] array = this.class410_0.short_0;
				int num = 256;
				array[num] += 1;
				this.class410_0.method_4();
				this.class410_1.method_4();
				this.class410_0.method_6(this.class410_2);
				this.class410_1.method_6(this.class410_2);
				this.class410_2.method_4();
				*(int*)ptr = 4;
				*(int*)((byte*)ptr + 12) = 18;
				while (*(int*)((byte*)ptr + 12) > *(int*)ptr)
				{
					if (this.class410_2.byte_0[Class402.Class409.int_8[*(int*)((byte*)ptr + 12)]] > 0)
					{
						*(int*)ptr = *(int*)((byte*)ptr + 12) + 1;
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) - 1;
				}
				*(int*)((byte*)ptr + 4) = 14 + *(int*)ptr * 3 + this.class410_2.method_5() + this.class410_0.method_5() + this.class410_1.method_5() + this.int_10;
				*(int*)((byte*)ptr + 8) = this.int_10;
				*(int*)((byte*)ptr + 16) = 0;
				while (*(int*)((byte*)ptr + 16) < 286)
				{
					*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + (int)(this.class410_0.short_0[*(int*)((byte*)ptr + 16)] * (short)Class402.Class409.byte_2[*(int*)((byte*)ptr + 16)]);
					*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + 1;
				}
				*(int*)((byte*)ptr + 20) = 0;
				while (*(int*)((byte*)ptr + 20) < 30)
				{
					*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + (int)(this.class410_1.short_0[*(int*)((byte*)ptr + 20)] * (short)Class402.Class409.byte_3[*(int*)((byte*)ptr + 20)]);
					*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
				}
				if (*(int*)((byte*)ptr + 4) >= *(int*)((byte*)ptr + 8))
				{
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 8);
				}
				if (int_11 >= 0 && int_12 + 4 < *(int*)((byte*)ptr + 4) >> 3)
				{
					this.method_5(byte_4, int_11, int_12, bool_0);
					return;
				}
				if (*(int*)((byte*)ptr + 4) == *(int*)((byte*)ptr + 8))
				{
					this.class412_0.method_3(2 + (bool_0 ? 1 : 0), 3);
					this.class410_0.method_1(Class402.Class409.short_1, Class402.Class409.byte_2);
					this.class410_1.method_1(Class402.Class409.short_2, Class402.Class409.byte_3);
					this.method_4();
					this.method_0();
					return;
				}
				this.class412_0.method_3(4 + (bool_0 ? 1 : 0), 3);
				this.method_3(*(int*)ptr);
				this.method_4();
				this.method_0();
			}

			public bool method_7()
			{
				return this.int_9 >= 16384;
			}

			public bool method_8(int int_11)
			{
				this.short_0[this.int_9] = 0;
				byte[] array = this.byte_1;
				int num = this.int_9;
				this.int_9 = num + 1;
				array[num] = (byte)int_11;
				short[] array2 = this.class410_0.short_0;
				array2[int_11] += 1;
				return this.method_7();
			}

			public unsafe bool method_9(int int_11, int int_12)
			{
				void* ptr = stackalloc byte[12];
				this.short_0[this.int_9] = (short)int_11;
				byte[] array = this.byte_1;
				*(int*)((byte*)ptr + 8) = this.int_9;
				this.int_9 = *(int*)((byte*)ptr + 8) + 1;
				array[*(int*)((byte*)ptr + 8)] = (byte)(int_12 - 3);
				*(int*)ptr = this.method_1(int_12 - 3);
				short[] array2 = this.class410_0.short_0;
				int num = *(int*)ptr;
				array2[num] += 1;
				if (*(int*)ptr >= 265 && *(int*)ptr < 285)
				{
					this.int_10 += (*(int*)ptr - 261) / 4;
				}
				*(int*)((byte*)ptr + 4) = this.method_2(int_11 - 1);
				short[] array3 = this.class410_1.short_0;
				int num2 = *(int*)((byte*)ptr + 4);
				array3[num2] += 1;
				if (*(int*)((byte*)ptr + 4) >= 4)
				{
					this.int_10 += *(int*)((byte*)ptr + 4) / 2 - 1;
				}
				return this.method_7();
			}

			private const int int_0 = 16384;

			private const int int_1 = 286;

			private const int int_2 = 30;

			private const int int_3 = 19;

			private const int int_4 = 16;

			private const int int_5 = 17;

			private const int int_6 = 18;

			private const int int_7 = 256;

			private static readonly int[] int_8 = new int[]
			{
				16,
				17,
				18,
				0,
				8,
				7,
				9,
				6,
				10,
				5,
				11,
				4,
				12,
				3,
				13,
				2,
				14,
				1,
				15
			};

			private static readonly byte[] byte_0 = new byte[]
			{
				0,
				8,
				4,
				12,
				2,
				10,
				6,
				14,
				1,
				9,
				5,
				13,
				3,
				11,
				7,
				15
			};

			private Class402.Class412 class412_0;

			private Class402.Class409.Class410 class410_0;

			private Class402.Class409.Class410 class410_1;

			private Class402.Class409.Class410 class410_2;

			private short[] short_0;

			private byte[] byte_1;

			private int int_9;

			private int int_10;

			private static readonly short[] short_1 = new short[286];

			private static readonly byte[] byte_2 = new byte[286];

			private static readonly short[] short_2;

			private static readonly byte[] byte_3;

			public sealed class Class410
			{
				public Class410(Class402.Class409 class409_1, int int_4, int int_5, int int_6)
				{
					this.class409_0 = class409_1;
					this.int_0 = int_5;
					this.int_3 = int_6;
					this.short_0 = new short[int_4];
					this.int_2 = new int[int_6];
				}

				public void method_0(int int_4)
				{
					this.class409_0.class412_0.method_3((int)this.short_1[int_4] & 65535, (int)this.byte_0[int_4]);
				}

				public void method_1(short[] short_2, byte[] byte_1)
				{
					this.short_1 = short_2;
					this.byte_0 = byte_1;
				}

				public unsafe void method_2()
				{
					void* ptr = stackalloc byte[16];
					int[] array = new int[this.int_3];
					*(int*)ptr = 0;
					this.short_1 = new short[this.short_0.Length];
					*(int*)((byte*)ptr + 4) = 0;
					while (*(int*)((byte*)ptr + 4) < this.int_3)
					{
						array[*(int*)((byte*)ptr + 4)] = *(int*)ptr;
						*(int*)ptr = *(int*)ptr + (this.int_2[*(int*)((byte*)ptr + 4)] << 15 - *(int*)((byte*)ptr + 4));
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					*(int*)((byte*)ptr + 8) = 0;
					while (*(int*)((byte*)ptr + 8) < this.int_1)
					{
						*(int*)((byte*)ptr + 12) = (int)this.byte_0[*(int*)((byte*)ptr + 8)];
						if (*(int*)((byte*)ptr + 12) > 0)
						{
							this.short_1[*(int*)((byte*)ptr + 8)] = Class402.Class409.smethod_0(array[*(int*)((byte*)ptr + 12) - 1]);
							array[*(int*)((byte*)ptr + 12) - 1] += 1 << 16 - *(int*)((byte*)ptr + 12);
						}
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
				}

				private unsafe void method_3(int[] int_4)
				{
					void* ptr = stackalloc byte[36];
					this.byte_0 = new byte[this.short_0.Length];
					*(int*)ptr = int_4.Length / 2;
					*(int*)((byte*)ptr + 4) = (*(int*)ptr + 1) / 2;
					*(int*)((byte*)ptr + 8) = 0;
					*(int*)((byte*)ptr + 12) = 0;
					while (*(int*)((byte*)ptr + 12) < this.int_3)
					{
						this.int_2[*(int*)((byte*)ptr + 12)] = 0;
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
					}
					int[] array = new int[*(int*)ptr];
					array[*(int*)ptr - 1] = 0;
					*(int*)((byte*)ptr + 16) = *(int*)ptr - 1;
					while (*(int*)((byte*)ptr + 16) >= 0)
					{
						if (int_4[2 * *(int*)((byte*)ptr + 16) + 1] != -1)
						{
							*(int*)((byte*)ptr + 20) = array[*(int*)((byte*)ptr + 16)] + 1;
							if (*(int*)((byte*)ptr + 20) > this.int_3)
							{
								*(int*)((byte*)ptr + 20) = this.int_3;
								*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
							}
							array[int_4[2 * *(int*)((byte*)ptr + 16)]] = (array[int_4[2 * *(int*)((byte*)ptr + 16) + 1]] = *(int*)((byte*)ptr + 20));
						}
						else
						{
							*(int*)((byte*)ptr + 24) = array[*(int*)((byte*)ptr + 16)];
							this.int_2[*(int*)((byte*)ptr + 24) - 1]++;
							this.byte_0[int_4[2 * *(int*)((byte*)ptr + 16)]] = (byte)array[*(int*)((byte*)ptr + 16)];
						}
						*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) - 1;
					}
					if (*(int*)((byte*)ptr + 8) == 0)
					{
						return;
					}
					int num = this.int_3 - 1;
					for (;;)
					{
						if (this.int_2[--num] != 0)
						{
							do
							{
								this.int_2[num]--;
								this.int_2[++num]++;
								*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) - (1 << this.int_3 - 1 - num);
							}
							while (*(int*)((byte*)ptr + 8) > 0 && num < this.int_3 - 1);
							if (*(int*)((byte*)ptr + 8) <= 0)
							{
								break;
							}
						}
					}
					this.int_2[this.int_3 - 1] += *(int*)((byte*)ptr + 8);
					this.int_2[this.int_3 - 2] -= *(int*)((byte*)ptr + 8);
					int num2 = 2 * *(int*)((byte*)ptr + 4);
					*(int*)((byte*)ptr + 28) = this.int_3;
					while (*(int*)((byte*)ptr + 28) != 0)
					{
						*(int*)((byte*)ptr + 32) = this.int_2[*(int*)((byte*)ptr + 28) - 1];
						while (*(int*)((byte*)ptr + 32) > 0)
						{
							int num3 = 2 * int_4[num2++];
							if (int_4[num3 + 1] == -1)
							{
								this.byte_0[int_4[num3]] = (byte)(*(int*)((byte*)ptr + 28));
								*(int*)((byte*)ptr + 32) = *(int*)((byte*)ptr + 32) - 1;
							}
						}
						*(int*)((byte*)ptr + 28) = *(int*)((byte*)ptr + 28) - 1;
					}
				}

				public unsafe void method_4()
				{
					void* ptr = stackalloc byte[32];
					*(int*)ptr = this.short_0.Length;
					int[] array = new int[*(int*)ptr];
					int i = 0;
					int num = 0;
					*(int*)((byte*)ptr + 4) = 0;
					while (*(int*)((byte*)ptr + 4) < *(int*)ptr)
					{
						*(int*)((byte*)ptr + 8) = (int)this.short_0[*(int*)((byte*)ptr + 4)];
						if (*(int*)((byte*)ptr + 8) != 0)
						{
							int num2 = i++;
							int num3;
							while (num2 > 0 && (int)this.short_0[array[num3 = (num2 - 1) / 2]] > *(int*)((byte*)ptr + 8))
							{
								array[num2] = array[num3];
								num2 = num3;
							}
							array[num2] = *(int*)((byte*)ptr + 4);
							num = *(int*)((byte*)ptr + 4);
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					while (i < 2)
					{
						int num4 = (num < 2) ? (++num) : 0;
						array[i++] = num4;
					}
					this.int_1 = Math.Max(num + 1, this.int_0);
					int num5 = i;
					int[] array2 = new int[4 * i - 2];
					int[] array3 = new int[2 * i - 1];
					int num6 = num5;
					*(int*)((byte*)ptr + 12) = 0;
					while (*(int*)((byte*)ptr + 12) < i)
					{
						*(int*)((byte*)ptr + 16) = array[*(int*)((byte*)ptr + 12)];
						array2[2 * *(int*)((byte*)ptr + 12)] = *(int*)((byte*)ptr + 16);
						array2[2 * *(int*)((byte*)ptr + 12) + 1] = -1;
						array3[*(int*)((byte*)ptr + 12)] = (int)this.short_0[*(int*)((byte*)ptr + 16)] << 8;
						array[*(int*)((byte*)ptr + 12)] = *(int*)((byte*)ptr + 12);
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
					}
					do
					{
						*(int*)((byte*)ptr + 20) = array[0];
						int num7 = array[--i];
						int num8 = 0;
						int j;
						for (j = 1; j < i; j = j * 2 + 1)
						{
							if (j + 1 < i && array3[array[j]] > array3[array[j + 1]])
							{
								j++;
							}
							array[num8] = array[j];
							num8 = j;
						}
						int num9 = array3[num7];
						while ((j = num8) > 0 && array3[array[num8 = (j - 1) / 2]] > num9)
						{
							array[j] = array[num8];
						}
						array[j] = num7;
						*(int*)((byte*)ptr + 24) = array[0];
						num7 = num6++;
						array2[2 * num7] = *(int*)((byte*)ptr + 20);
						array2[2 * num7 + 1] = *(int*)((byte*)ptr + 24);
						*(int*)((byte*)ptr + 28) = Math.Min(array3[*(int*)((byte*)ptr + 20)] & 255, array3[*(int*)((byte*)ptr + 24)] & 255);
						num9 = (array3[num7] = array3[*(int*)((byte*)ptr + 20)] + array3[*(int*)((byte*)ptr + 24)] - *(int*)((byte*)ptr + 28) + 1);
						num8 = 0;
						for (j = 1; j < i; j = num8 * 2 + 1)
						{
							if (j + 1 < i && array3[array[j]] > array3[array[j + 1]])
							{
								j++;
							}
							array[num8] = array[j];
							num8 = j;
						}
						while ((j = num8) > 0 && array3[array[num8 = (j - 1) / 2]] > num9)
						{
							array[j] = array[num8];
						}
						array[j] = num7;
					}
					while (i > 1);
					this.method_3(array2);
				}

				public unsafe int method_5()
				{
					void* ptr = stackalloc byte[8];
					*(int*)ptr = 0;
					*(int*)((byte*)ptr + 4) = 0;
					while (*(int*)((byte*)ptr + 4) < this.short_0.Length)
					{
						*(int*)ptr = *(int*)ptr + (int)(this.short_0[*(int*)((byte*)ptr + 4)] * (short)this.byte_0[*(int*)((byte*)ptr + 4)]);
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					return *(int*)ptr;
				}

				public unsafe void method_6(Class402.Class409.Class410 class410_0)
				{
					void* ptr = stackalloc byte[20];
					*(int*)((byte*)ptr + 8) = -1;
					*(int*)((byte*)ptr + 12) = 0;
					while (*(int*)((byte*)ptr + 12) < this.int_1)
					{
						int num = 1;
						*(int*)((byte*)ptr + 16) = (int)this.byte_0[*(int*)((byte*)ptr + 12)];
						if (*(int*)((byte*)ptr + 16) == 0)
						{
							*(int*)ptr = 138;
							*(int*)((byte*)ptr + 4) = 3;
						}
						else
						{
							*(int*)ptr = 6;
							*(int*)((byte*)ptr + 4) = 3;
							if (*(int*)((byte*)ptr + 8) != *(int*)((byte*)ptr + 16))
							{
								short[] array = class410_0.short_0;
								int num2 = *(int*)((byte*)ptr + 16);
								array[num2] += 1;
								num = 0;
							}
						}
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 16);
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
						while (*(int*)((byte*)ptr + 12) < this.int_1)
						{
							if (*(int*)((byte*)ptr + 8) != (int)this.byte_0[*(int*)((byte*)ptr + 12)])
							{
								break;
							}
							*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
							if (++num >= *(int*)ptr)
							{
								break;
							}
						}
						if (num < *(int*)((byte*)ptr + 4))
						{
							short[] array2 = class410_0.short_0;
							int num3 = *(int*)((byte*)ptr + 8);
							array2[num3] += (short)num;
						}
						else if (*(int*)((byte*)ptr + 8) != 0)
						{
							short[] array3 = class410_0.short_0;
							int num4 = 16;
							array3[num4] += 1;
						}
						else if (num <= 10)
						{
							short[] array4 = class410_0.short_0;
							int num5 = 17;
							array4[num5] += 1;
						}
						else
						{
							short[] array5 = class410_0.short_0;
							int num6 = 18;
							array5[num6] += 1;
						}
					}
				}

				public unsafe void method_7(Class402.Class409.Class410 class410_0)
				{
					void* ptr = stackalloc byte[20];
					*(int*)((byte*)ptr + 8) = -1;
					*(int*)((byte*)ptr + 12) = 0;
					while (*(int*)((byte*)ptr + 12) < this.int_1)
					{
						int num = 1;
						*(int*)((byte*)ptr + 16) = (int)this.byte_0[*(int*)((byte*)ptr + 12)];
						if (*(int*)((byte*)ptr + 16) == 0)
						{
							*(int*)ptr = 138;
							*(int*)((byte*)ptr + 4) = 3;
						}
						else
						{
							*(int*)ptr = 6;
							*(int*)((byte*)ptr + 4) = 3;
							if (*(int*)((byte*)ptr + 8) != *(int*)((byte*)ptr + 16))
							{
								class410_0.method_0(*(int*)((byte*)ptr + 16));
								num = 0;
							}
						}
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 16);
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
						while (*(int*)((byte*)ptr + 12) < this.int_1)
						{
							if (*(int*)((byte*)ptr + 8) != (int)this.byte_0[*(int*)((byte*)ptr + 12)])
							{
								break;
							}
							*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
							if (++num >= *(int*)ptr)
							{
								break;
							}
						}
						if (num < *(int*)((byte*)ptr + 4))
						{
							while (num-- > 0)
							{
								class410_0.method_0(*(int*)((byte*)ptr + 8));
							}
						}
						else if (*(int*)((byte*)ptr + 8) != 0)
						{
							class410_0.method_0(16);
							this.class409_0.class412_0.method_3(num - 3, 2);
						}
						else if (num <= 10)
						{
							class410_0.method_0(17);
							this.class409_0.class412_0.method_3(num - 3, 3);
						}
						else
						{
							class410_0.method_0(18);
							this.class409_0.class412_0.method_3(num - 11, 7);
						}
					}
				}

				public short[] short_0;

				public byte[] byte_0;

				public int int_0;

				public int int_1;

				private short[] short_1;

				private int[] int_2;

				private int int_3;

				private Class402.Class409 class409_0;
			}
		}

		internal sealed class Class411
		{
			public Class411(Class402.Class412 class412_1)
			{
				this.class412_0 = class412_1;
				this.class409_0 = new Class402.Class409(class412_1);
				this.byte_0 = new byte[65536];
				this.short_0 = new short[32768];
				this.short_1 = new short[32768];
				this.int_14 = 1;
				this.int_13 = 1;
			}

			private void method_0()
			{
				this.int_10 = ((int)this.byte_0[this.int_14] << 5 ^ (int)this.byte_0[this.int_14 + 1]);
			}

			private int method_1()
			{
				int num = (this.int_10 << 5 ^ (int)this.byte_0[this.int_14 + 2]) & 32767;
				short num2 = this.short_1[this.int_14 & 32767] = this.short_0[num];
				this.short_0[num] = (short)this.int_14;
				this.int_10 = num;
				return (int)num2 & 65535;
			}

			private unsafe void method_2()
			{
				void* ptr = stackalloc byte[16];
				Array.Copy(this.byte_0, 32768, this.byte_0, 0, 32768);
				this.int_11 -= 32768;
				this.int_14 -= 32768;
				this.int_13 -= 32768;
				*(int*)ptr = 0;
				while (*(int*)ptr < 32768)
				{
					*(int*)((byte*)ptr + 4) = ((int)this.short_0[*(int*)ptr] & 65535);
					this.short_0[*(int*)ptr] = (short)((*(int*)((byte*)ptr + 4) >= 32768) ? (*(int*)((byte*)ptr + 4) - 32768) : 0);
					*(int*)ptr = *(int*)ptr + 1;
				}
				*(int*)((byte*)ptr + 8) = 0;
				while (*(int*)((byte*)ptr + 8) < 32768)
				{
					*(int*)((byte*)ptr + 12) = ((int)this.short_1[*(int*)((byte*)ptr + 8)] & 65535);
					this.short_1[*(int*)((byte*)ptr + 8)] = (short)((*(int*)((byte*)ptr + 12) >= 32768) ? (*(int*)((byte*)ptr + 12) - 32768) : 0);
					*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
				}
			}

			public void method_3()
			{
				if (this.int_14 >= 65274)
				{
					this.method_2();
				}
				while (this.int_15 < 262 && this.int_17 < this.int_18)
				{
					int num = 65536 - this.int_15 - this.int_14;
					if (num > this.int_18 - this.int_17)
					{
						num = this.int_18 - this.int_17;
					}
					Array.Copy(this.byte_1, this.int_17, this.byte_0, this.int_14 + this.int_15, num);
					this.int_17 += num;
					this.int_16 += num;
					this.int_15 += num;
				}
				if (this.int_15 >= 3)
				{
					this.method_0();
				}
			}

			private unsafe bool method_4(int int_19)
			{
				void* ptr = stackalloc byte[22];
				int num = 128;
				*(int*)ptr = 128;
				short[] array = this.short_1;
				int num2 = this.int_14;
				*(int*)((byte*)ptr + 4) = this.int_14 + this.int_12;
				*(int*)((byte*)ptr + 8) = Math.Max(this.int_12, 2);
				*(int*)((byte*)ptr + 12) = Math.Max(this.int_14 - 32506, 0);
				*(int*)((byte*)ptr + 16) = this.int_14 + 258 - 1;
				((byte*)ptr)[20] = this.byte_0[*(int*)((byte*)ptr + 4) - 1];
				((byte*)ptr)[21] = this.byte_0[*(int*)((byte*)ptr + 4)];
				if (*(int*)((byte*)ptr + 8) >= 8)
				{
					num >>= 2;
				}
				if (*(int*)ptr > this.int_15)
				{
					*(int*)ptr = this.int_15;
				}
				do
				{
					if (this.byte_0[int_19 + *(int*)((byte*)ptr + 8)] == ((byte*)ptr)[21] && this.byte_0[int_19 + *(int*)((byte*)ptr + 8) - 1] == ((byte*)ptr)[20] && this.byte_0[int_19] == this.byte_0[num2] && this.byte_0[int_19 + 1] == this.byte_0[num2 + 1])
					{
						int num3 = int_19 + 2;
						num2 += 2;
						while (this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && this.byte_0[++num2] == this.byte_0[++num3] && num2 < *(int*)((byte*)ptr + 16))
						{
						}
						if (num2 > *(int*)((byte*)ptr + 4))
						{
							this.int_11 = int_19;
							*(int*)((byte*)ptr + 4) = num2;
							*(int*)((byte*)ptr + 8) = num2 - this.int_14;
							if (*(int*)((byte*)ptr + 8) >= *(int*)ptr)
							{
								break;
							}
							((byte*)ptr)[20] = this.byte_0[*(int*)((byte*)ptr + 4) - 1];
							((byte*)ptr)[21] = this.byte_0[*(int*)((byte*)ptr + 4)];
						}
						num2 = this.int_14;
					}
					if ((int_19 = ((int)array[int_19 & 32767] & 65535)) <= *(int*)((byte*)ptr + 12))
					{
						break;
					}
				}
				while (--num != 0);
				this.int_12 = Math.Min(*(int*)((byte*)ptr + 8), this.int_15);
				return this.int_12 >= 3;
			}

			private unsafe bool method_5(bool bool_1, bool bool_2)
			{
				void* ptr = stackalloc byte[12];
				if (this.int_15 < 262 && !bool_1)
				{
					return false;
				}
				while (this.int_15 >= 262 || bool_1)
				{
					if (this.int_15 == 0)
					{
						if (this.bool_0)
						{
							this.class409_0.method_8((int)(this.byte_0[this.int_14 - 1] & byte.MaxValue));
						}
						this.bool_0 = false;
						this.class409_0.method_6(this.byte_0, this.int_13, this.int_14 - this.int_13, bool_2);
						this.int_13 = this.int_14;
						return false;
					}
					if (this.int_14 >= 65274)
					{
						this.method_2();
					}
					*(int*)ptr = this.int_11;
					int num = this.int_12;
					if (this.int_15 >= 3)
					{
						*(int*)((byte*)ptr + 4) = this.method_1();
						if (*(int*)((byte*)ptr + 4) != 0 && this.int_14 - *(int*)((byte*)ptr + 4) <= 32506 && this.method_4(*(int*)((byte*)ptr + 4)) && this.int_12 <= 5 && this.int_12 == 3 && this.int_14 - this.int_11 > 4096)
						{
							this.int_12 = 2;
						}
					}
					if (num >= 3 && this.int_12 <= num)
					{
						this.class409_0.method_9(this.int_14 - 1 - *(int*)ptr, num);
						num -= 2;
						do
						{
							this.int_14++;
							this.int_15--;
							if (this.int_15 >= 3)
							{
								this.method_1();
							}
						}
						while (--num > 0);
						this.int_14++;
						this.int_15--;
						this.bool_0 = false;
						this.int_12 = 2;
					}
					else
					{
						if (this.bool_0)
						{
							this.class409_0.method_8((int)(this.byte_0[this.int_14 - 1] & byte.MaxValue));
						}
						this.bool_0 = true;
						this.int_14++;
						this.int_15--;
					}
					if (this.class409_0.method_7())
					{
						*(int*)((byte*)ptr + 8) = this.int_14 - this.int_13;
						if (this.bool_0)
						{
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) - 1;
						}
						bool flag = bool_2 && this.int_15 == 0 && !this.bool_0;
						this.class409_0.method_6(this.byte_0, this.int_13, *(int*)((byte*)ptr + 8), flag);
						this.int_13 += *(int*)((byte*)ptr + 8);
						return !flag;
					}
				}
				return true;
			}

			public bool method_6(bool bool_1, bool bool_2)
			{
				bool flag;
				do
				{
					this.method_3();
					bool bool_3 = bool_1 && this.int_17 == this.int_18;
					flag = this.method_5(bool_3, bool_2);
				}
				while (this.class412_0.IsFlushed && flag);
				return flag;
			}

			public void method_7(byte[] byte_2)
			{
				this.byte_1 = byte_2;
				this.int_17 = 0;
				this.int_18 = byte_2.Length;
			}

			public bool method_8()
			{
				return this.int_18 == this.int_17;
			}

			private const int int_0 = 258;

			private const int int_1 = 3;

			private const int int_2 = 32768;

			private const int int_3 = 32767;

			private const int int_4 = 32768;

			private const int int_5 = 32767;

			private const int int_6 = 5;

			private const int int_7 = 262;

			private const int int_8 = 32506;

			private const int int_9 = 4096;

			private int int_10;

			private short[] short_0;

			private short[] short_1;

			private int int_11;

			private int int_12;

			private bool bool_0;

			private int int_13;

			private int int_14;

			private int int_15;

			private byte[] byte_0;

			private byte[] byte_1;

			private int int_16;

			private int int_17;

			private int int_18;

			private Class402.Class412 class412_0;

			private Class402.Class409 class409_0;
		}

		internal sealed class Class412
		{
			public void method_0(int int_3)
			{
				byte[] array = this.byte_0;
				int num = this.int_1;
				this.int_1 = num + 1;
				array[num] = (byte)int_3;
				byte[] array2 = this.byte_0;
				num = this.int_1;
				this.int_1 = num + 1;
				array2[num] = (byte)(int_3 >> 8);
			}

			public void method_1(byte[] byte_1, int int_3, int int_4)
			{
				Array.Copy(byte_1, int_3, this.byte_0, this.int_1, int_4);
				this.int_1 += int_4;
			}

			public int BitCount
			{
				get
				{
					return this.int_2;
				}
			}

			public void method_2()
			{
				if (this.int_2 > 0)
				{
					byte[] array = this.byte_0;
					int num = this.int_1;
					this.int_1 = num + 1;
					array[num] = (byte)this.uint_0;
					if (this.int_2 > 8)
					{
						byte[] array2 = this.byte_0;
						num = this.int_1;
						this.int_1 = num + 1;
						array2[num] = (byte)(this.uint_0 >> 8);
					}
				}
				this.uint_0 = 0U;
				this.int_2 = 0;
			}

			public void method_3(int int_3, int int_4)
			{
				this.uint_0 |= (uint)((uint)int_3 << this.int_2);
				this.int_2 += int_4;
				if (this.int_2 >= 16)
				{
					byte[] array = this.byte_0;
					int num = this.int_1;
					this.int_1 = num + 1;
					array[num] = (byte)this.uint_0;
					byte[] array2 = this.byte_0;
					num = this.int_1;
					this.int_1 = num + 1;
					array2[num] = (byte)(this.uint_0 >> 8);
					this.uint_0 >>= 16;
					this.int_2 -= 16;
				}
			}

			public bool IsFlushed
			{
				get
				{
					return this.int_1 == 0;
				}
			}

			public int method_4(byte[] byte_1, int int_3, int int_4)
			{
				if (this.int_2 >= 8)
				{
					byte[] array = this.byte_0;
					int num = this.int_1;
					this.int_1 = num + 1;
					array[num] = (byte)this.uint_0;
					this.uint_0 >>= 8;
					this.int_2 -= 8;
				}
				if (int_4 > this.int_1 - this.int_0)
				{
					int_4 = this.int_1 - this.int_0;
					Array.Copy(this.byte_0, this.int_0, byte_1, int_3, int_4);
					this.int_0 = 0;
					this.int_1 = 0;
				}
				else
				{
					Array.Copy(this.byte_0, this.int_0, byte_1, int_3, int_4);
					this.int_0 += int_4;
				}
				return int_4;
			}

			protected byte[] byte_0 = new byte[65536];

			private int int_0;

			private int int_1;

			private uint uint_0;

			private int int_2;
		}

		internal sealed class Stream0 : MemoryStream
		{
			public void method_0(int int_0)
			{
				this.WriteByte((byte)(int_0 & 255));
				this.WriteByte((byte)(int_0 >> 8 & 255));
			}

			public void method_1(int int_0)
			{
				this.method_0(int_0);
				this.method_0(int_0 >> 16);
			}

			public int method_2()
			{
				return this.ReadByte() | this.ReadByte() << 8;
			}

			public int method_3()
			{
				return this.method_2() | this.method_2() << 16;
			}

			public Stream0()
			{
			}

			public Stream0(byte[] byte_0) : base(byte_0, false)
			{
			}
		}
	}
}
