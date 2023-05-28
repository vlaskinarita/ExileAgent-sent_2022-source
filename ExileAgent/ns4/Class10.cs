using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using ns1;
using ns8;

namespace ns4
{
	internal sealed class Class10 : Interface3
	{
		private double JustNoticeableDifference { get; set; }

		public Class10(double double_1)
		{
			this.JustNoticeableDifference = double_1;
		}

		public unsafe bool[,] imethod_0(Bitmap bitmap_0, Bitmap bitmap_1)
		{
			void* ptr = stackalloc byte[18];
			bool[,] array = new bool[bitmap_0.Width, bitmap_0.Height];
			*(int*)((byte*)ptr + 8) = 0;
			for (;;)
			{
				((byte*)ptr)[17] = ((*(int*)((byte*)ptr + 8) < bitmap_0.Width) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = 0;
				for (;;)
				{
					((byte*)ptr)[16] = ((*(int*)((byte*)ptr + 12) < bitmap_0.Height) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) == 0)
					{
						break;
					}
					Struct0 @struct = Struct0.smethod_0(bitmap_0.GetPixel(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)));
					Struct0 struct2 = Struct0.smethod_0(bitmap_1.GetPixel(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)));
					*(double*)ptr = Math.Sqrt(Math.Pow(struct2.L - @struct.L, 2.0) + Math.Pow(struct2.a - @struct.a, 2.0) + Math.Pow(struct2.b - @struct.b, 2.0));
					array[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)] = (*(double*)ptr >= this.JustNoticeableDifference);
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return array;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_0;
	}
}
