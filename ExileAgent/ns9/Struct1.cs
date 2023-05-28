using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ns9
{
	internal struct Struct1
	{
		public double x { get; set; }

		public double y { get; set; }

		public double z { get; set; }

		public Struct1(double double_6, double double_7, double double_8)
		{
			this = default(Struct1);
			this.x = double_6;
			this.y = double_7;
			this.z = double_8;
		}

		public unsafe static Struct1 smethod_0(Color color_0)
		{
			void* ptr = stackalloc byte[24];
			*(double*)ptr = (double)color_0.R / 255.0;
			*(double*)((byte*)ptr + 8) = (double)color_0.G / 255.0;
			*(double*)((byte*)ptr + 16) = (double)color_0.B / 255.0;
			double num = (*(double*)ptr > 0.04045) ? Math.Pow((*(double*)ptr + 0.055) / 1.055, 2.4) : (*(double*)ptr / 12.92);
			double num2 = (*(double*)((byte*)ptr + 8) > 0.04045) ? Math.Pow((*(double*)((byte*)ptr + 8) + 0.055) / 1.055, 2.4) : (*(double*)((byte*)ptr + 8) / 12.92);
			double num3 = (*(double*)((byte*)ptr + 16) > 0.04045) ? Math.Pow((*(double*)((byte*)ptr + 16) + 0.055) / 1.055, 2.4) : (*(double*)((byte*)ptr + 16) / 12.92);
			return new Struct1(num * 0.4124 + num2 * 0.3576 + num3 * 0.1805, num * 0.2126 + num2 * 0.7152 + num3 * 0.0722, num * 0.0193 + num2 * 0.1192 + num3 * 0.9505);
		}

		public const double double_0 = 95.047;

		public const double double_1 = 100.0;

		public const double double_2 = 108.883;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_5;
	}
}
