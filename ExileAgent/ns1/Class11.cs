using System;
using System.Drawing;

namespace ns1
{
	internal sealed class Class11 : Interface3
	{
		public unsafe bool[,] imethod_0(Bitmap bitmap_0, Bitmap bitmap_1)
		{
			void* ptr = stackalloc byte[11];
			bool[,] array = new bool[bitmap_0.Width, bitmap_0.Height];
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[10] = ((*(int*)ptr < bitmap_0.Width) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[9] = ((*(int*)((byte*)ptr + 4) < bitmap_0.Height) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						break;
					}
					Color pixel = bitmap_0.GetPixel(*(int*)ptr, *(int*)((byte*)ptr + 4));
					Color pixel2 = bitmap_1.GetPixel(*(int*)ptr, *(int*)((byte*)ptr + 4));
					((byte*)ptr)[8] = ((pixel != pixel2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						array[*(int*)ptr, *(int*)((byte*)ptr + 4)] = true;
					}
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			return array;
		}
	}
}
