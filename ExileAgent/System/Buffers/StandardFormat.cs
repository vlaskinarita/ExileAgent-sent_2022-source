using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	[IsReadOnlyAttribute1]
	public struct StandardFormat : IEquatable<StandardFormat>
	{
		public char Symbol
		{
			get
			{
				return (char)this._format;
			}
		}

		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		public bool HasPrecision
		{
			get
			{
				return this._precision != byte.MaxValue;
			}
		}

		public bool IsDefault
		{
			get
			{
				return this._format == 0 && this._precision == 0;
			}
		}

		public StandardFormat(char symbol, byte precision = 255)
		{
			if (precision != 255 && precision > 99)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PrecisionTooLarge();
			}
			if (symbol != (char)((byte)symbol))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_SymbolDoesNotFit();
			}
			this._format = (byte)symbol;
			this._precision = precision;
		}

		public static implicit operator StandardFormat(char symbol)
		{
			return new StandardFormat(symbol, byte.MaxValue);
		}

		public unsafe static StandardFormat Parse(ReadOnlySpan<char> format)
		{
			if (format.Length == 0)
			{
				return default(StandardFormat);
			}
			char symbol = (char)(*format[0]);
			byte precision;
			if (format.Length == 1)
			{
				precision = byte.MaxValue;
			}
			else
			{
				uint num = 0U;
				for (int i = 1; i < format.Length; i++)
				{
					uint num2 = (uint)(*format[i] - 48);
					if (num2 > 9U)
					{
						throw new FormatException(SR1.Format(SR1.Argument_CannotParsePrecision, 99));
					}
					num = num * 10U + num2;
					if (num > 99U)
					{
						throw new FormatException(SR1.Format(SR1.Argument_PrecisionTooLarge, 99));
					}
				}
				precision = (byte)num;
			}
			return new StandardFormat(symbol, precision);
		}

		public static StandardFormat Parse(string format)
		{
			if (format != null)
			{
				return StandardFormat.Parse(format.AsSpan());
			}
			return default(StandardFormat);
		}

		public override bool Equals(object obj)
		{
			if (obj is StandardFormat)
			{
				StandardFormat other = (StandardFormat)obj;
				return this.Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this._format.GetHashCode() ^ this._precision.GetHashCode();
		}

		public bool Equals(StandardFormat other)
		{
			return this._format == other._format && this._precision == other._precision;
		}

		public unsafe override string ToString()
		{
			char* ptr = stackalloc char[(UIntPtr)8];
			int length = 0;
			char symbol = this.Symbol;
			if (symbol != '\0')
			{
				ptr[(IntPtr)(length++) * 2] = symbol;
				byte b = this.Precision;
				if (b != 255)
				{
					if (b >= 100)
					{
						ptr[(IntPtr)(length++) * 2] = (char)(48 + b / 100 % 10);
						b %= 100;
					}
					if (b >= 10)
					{
						ptr[(IntPtr)(length++) * 2] = (char)(48 + b / 10 % 10);
						b %= 10;
					}
					ptr[(IntPtr)(length++) * 2] = (char)(48 + b);
				}
			}
			return new string(ptr, 0, length);
		}

		public static bool operator ==(StandardFormat left, StandardFormat right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(StandardFormat left, StandardFormat right)
		{
			return !left.Equals(right);
		}

		public const byte NoPrecision = 255;

		public const byte MaxPrecision = 99;

		private readonly byte _format;

		private readonly byte _precision;
	}
}
