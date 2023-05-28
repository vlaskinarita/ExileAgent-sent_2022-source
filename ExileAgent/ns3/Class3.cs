using System;
using ns4;

namespace ns3
{
	internal sealed class Class3 : Interface1
	{
		public unsafe int[,] imethod_0(bool[,] bool_0)
		{
			void* ptr = stackalloc byte[19];
			*(int*)ptr = bool_0.GetLength(0);
			*(int*)((byte*)ptr + 4) = bool_0.GetLength(1);
			int[,] array = new int[*(int*)ptr, *(int*)((byte*)ptr + 4)];
			*(int*)((byte*)ptr + 8) = 0;
			for (;;)
			{
				((byte*)ptr)[18] = ((*(int*)((byte*)ptr + 8) < *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = 0;
				for (;;)
				{
					((byte*)ptr)[17] = ((*(int*)((byte*)ptr + 12) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) == 0)
					{
						break;
					}
					((byte*)ptr)[16] = (bool_0[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)] ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						array[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)] = 1;
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return array;
		}
	}
}
