using System;

namespace System
{
	internal struct NUInt
	{
		private NUInt(uint value)
		{
			this._value = value;
		}

		private NUInt(ulong value)
		{
			this._value = value;
		}

		public static implicit operator NUInt(uint value)
		{
			return new NUInt(value);
		}

		public static implicit operator IntPtr(NUInt value)
		{
			return (IntPtr)value._value;
		}

		public static explicit operator NUInt(int value)
		{
			return new NUInt((uint)value);
		}

		public unsafe static explicit operator void*(NUInt value)
		{
			return value._value;
		}

		public static NUInt operator *(NUInt left, NUInt right)
		{
			if (sizeof(IntPtr) != 4)
			{
				return new NUInt(left._value * right._value);
			}
			return new NUInt(left._value * right._value);
		}

		private unsafe readonly void* _value;
	}
}
