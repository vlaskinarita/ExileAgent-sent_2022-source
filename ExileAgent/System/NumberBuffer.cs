﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace System
{
	internal ref struct NumberBuffer
	{
		public Span<byte> Digits
		{
			get
			{
				return new Span<byte>(Unsafe.AsPointer<byte>(ref this._b0), 51);
			}
		}

		public unsafe byte* UnsafeDigits
		{
			get
			{
				return (byte*)Unsafe.AsPointer<byte>(ref this._b0);
			}
		}

		public int NumDigits
		{
			get
			{
				return this.Digits.IndexOf(0);
			}
		}

		[Conditional("DEBUG")]
		public void CheckConsistency()
		{
		}

		public unsafe override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append('"');
			Span<byte> digits = this.Digits;
			for (int i = 0; i < 51; i++)
			{
				byte b = *digits[i];
				if (b == 0)
				{
					break;
				}
				stringBuilder.Append((char)b);
			}
			stringBuilder.Append('"');
			stringBuilder.Append(NumberBuffer.getString_0(107144203) + this.Scale);
			stringBuilder.Append(NumberBuffer.getString_0(107143642) + this.IsNegative.ToString());
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		static NumberBuffer()
		{
			Strings.CreateGetStringDelegate(typeof(NumberBuffer));
		}

		public int Scale;

		public bool IsNegative;

		public const int BufferSize = 51;

		private byte _b0;

		private byte _b1;

		private byte _b2;

		private byte _b3;

		private byte _b4;

		private byte _b5;

		private byte _b6;

		private byte _b7;

		private byte _b8;

		private byte _b9;

		private byte _b10;

		private byte _b11;

		private byte _b12;

		private byte _b13;

		private byte _b14;

		private byte _b15;

		private byte _b16;

		private byte _b17;

		private byte _b18;

		private byte _b19;

		private byte _b20;

		private byte _b21;

		private byte _b22;

		private byte _b23;

		private byte _b24;

		private byte _b25;

		private byte _b26;

		private byte _b27;

		private byte _b28;

		private byte _b29;

		private byte _b30;

		private byte _b31;

		private byte _b32;

		private byte _b33;

		private byte _b34;

		private byte _b35;

		private byte _b36;

		private byte _b37;

		private byte _b38;

		private byte _b39;

		private byte _b40;

		private byte _b41;

		private byte _b42;

		private byte _b43;

		private byte _b44;

		private byte _b45;

		private byte _b46;

		private byte _b47;

		private byte _b48;

		private byte _b49;

		private byte _b50;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
