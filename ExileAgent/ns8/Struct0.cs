using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using ns9;

namespace ns8
{
	internal struct Struct0
	{
		public double L { get; set; }

		public double a { get; set; }

		public double b { get; set; }

		public Struct0(double double_3, double double_4, double double_5)
		{
			this = default(Struct0);
			this.L = double_3;
			this.a = double_4;
			this.b = double_5;
		}

		public static Struct0 smethod_0(Color color_0)
		{
			return Struct0.smethod_1(Struct1.smethod_0(color_0));
		}

		public unsafe static Struct0 smethod_1(Struct1 struct1_0)
		{
			void* ptr = stackalloc byte[48];
			*(double*)ptr = Struct0.smethod_2(struct1_0.x / 95.047);
			*(double*)((byte*)ptr + 8) = Struct0.smethod_2(struct1_0.y / 100.0);
			*(double*)((byte*)ptr + 16) = Struct0.smethod_2(struct1_0.z / 108.883);
			*(double*)((byte*)ptr + 24) = 116.0 * *(double*)((byte*)ptr + 8) - 16.0;
			*(double*)((byte*)ptr + 32) = 500.0 * (*(double*)ptr - *(double*)((byte*)ptr + 8));
			*(double*)((byte*)ptr + 40) = 200.0 * (*(double*)((byte*)ptr + 8) - *(double*)((byte*)ptr + 16));
			return new Struct0(*(double*)((byte*)ptr + 24), *(double*)((byte*)ptr + 32), *(double*)((byte*)ptr + 40));
		}

		private static double smethod_2(double double_3)
		{
			return (double_3 > 0.008856) ? Math.Pow(double_3, 0.3333333333333333) : (7.787 * double_3 + 0.13793103448275862);
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_2;
	}
}
