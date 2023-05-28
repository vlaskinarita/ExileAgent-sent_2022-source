using System;

namespace System
{
	internal static class DecimalDecCalc
	{
		private static uint D32DivMod1E9(uint hi32, ref uint lo32)
		{
			ulong num = (ulong)hi32 << 32 | (ulong)lo32;
			lo32 = (uint)(num / 1000000000UL);
			return (uint)(num % 1000000000UL);
		}

		internal static uint DecDivMod1E9(ref MutableDecimal value)
		{
			return DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(0U, ref value.High), ref value.Mid), ref value.Low);
		}

		internal static void DecAddInt32(ref MutableDecimal value, uint i)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, i) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
		}

		private static bool D32AddCarry(ref uint value, uint i)
		{
			uint num = value;
			uint num2 = num + i;
			value = num2;
			return num2 < num || num2 < i;
		}

		internal static void DecMul10(ref MutableDecimal value)
		{
			MutableDecimal d = value;
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecAdd(ref value, d);
			DecimalDecCalc.DecShiftLeft(ref value);
		}

		private static void DecShiftLeft(ref MutableDecimal value)
		{
			uint num = ((value.Low & 2147483648U) != 0U) ? 1U : 0U;
			uint num2 = ((value.Mid & 2147483648U) != 0U) ? 1U : 0U;
			value.Low <<= 1;
			value.Mid = (value.Mid << 1 | num);
			value.High = (value.High << 1 | num2);
		}

		private static void DecAdd(ref MutableDecimal value, MutableDecimal d)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, d.Low) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			if (DecimalDecCalc.D32AddCarry(ref value.Mid, d.Mid))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			DecimalDecCalc.D32AddCarry(ref value.High, d.High);
		}
	}
}
