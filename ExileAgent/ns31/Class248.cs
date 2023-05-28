using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns31
{
	internal sealed class Class248
	{
		public int Numerator { get; set; }

		public int Denominator { get; set; }

		public Class248()
		{
		}

		public Class248(int int_2, int int_3)
		{
			this.Numerator = int_2;
			this.Denominator = int_3;
		}

		public decimal Value
		{
			get
			{
				return this.Numerator / this.Denominator;
			}
		}

		public void method_0(int int_2)
		{
			this.Numerator *= int_2;
			this.Denominator *= int_2;
		}

		public string ToString()
		{
			decimal num = 0m;
			if (this.Denominator > 0)
			{
				num = this.Numerator / this.Denominator;
			}
			return string.Format(Class248.getString_0(107441822), this.Numerator, this.Denominator, num);
		}

		public unsafe string method_1(bool bool_0)
		{
			void* ptr = stackalloc byte[7];
			((byte*)ptr)[4] = (bool_0 ? 1 : 0);
			string result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				((byte*)ptr)[5] = ((this.Numerator == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					*(int*)ptr = this.Denominator;
					result = ((int*)ptr)->ToString();
				}
				else
				{
					result = string.Format(Class248.getString_0(107363723), this.Denominator, this.Numerator);
				}
			}
			else
			{
				((byte*)ptr)[6] = ((this.Denominator == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					*(int*)ptr = this.Numerator;
					result = ((int*)ptr)->ToString();
				}
				else
				{
					result = string.Format(Class248.getString_0(107363723), this.Numerator, this.Denominator);
				}
			}
			return result;
		}

		public unsafe static Class248 smethod_0(decimal decimal_0, int int_2)
		{
			void* ptr = stackalloc byte[26];
			decimal d = decimal_0;
			*(int*)ptr = 1;
			*(int*)((byte*)ptr + 4) = 1;
			*(int*)((byte*)ptr + 8) = 1;
			((byte*)ptr)[20] = ((int_2 > 3000) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 20) != 0)
			{
				*(int*)((byte*)ptr + 8) = 10;
			}
			((byte*)ptr)[21] = ((int_2 >= 10000) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				*(int*)((byte*)ptr + 8) = 100;
			}
			((byte*)ptr)[22] = ((int_2 >= 50000) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 22) != 0)
			{
				*(int*)((byte*)ptr + 8) = 500;
			}
			*(int*)((byte*)ptr + 12) = 1;
			for (;;)
			{
				((byte*)ptr)[25] = ((*(int*)((byte*)ptr + 12) < int_2 + 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 25) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 16) = 1;
				for (;;)
				{
					((byte*)ptr)[24] = ((*(int*)((byte*)ptr + 16) < int_2 + 1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 24) == 0)
					{
						break;
					}
					decimal d2 = *(int*)((byte*)ptr + 12) / *(int*)((byte*)ptr + 16);
					((byte*)ptr)[23] = ((Math.Abs(decimal_0 - d2) < d) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 23) != 0)
					{
						d = Math.Abs(decimal_0 - d2);
						*(int*)ptr = *(int*)((byte*)ptr + 12);
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 16);
					}
					*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + *(int*)((byte*)ptr + 8);
				}
				*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + *(int*)((byte*)ptr + 8);
			}
			return new Class248(*(int*)ptr, *(int*)((byte*)ptr + 4));
		}

		public static decimal smethod_1(Class248 class248_0, Class248 class248_1)
		{
			return Math.Round((1m - class248_0.Value / class248_1.Value) * 100m, 2);
		}

		static Class248()
		{
			Strings.CreateGetStringDelegate(typeof(Class248));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
