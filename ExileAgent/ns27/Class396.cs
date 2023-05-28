using System;
using System.Runtime.CompilerServices;

namespace ns27
{
	[CompilerGenerated]
	internal sealed class Class396
	{
		internal unsafe static uint smethod_0(string string_0)
		{
			void* ptr = stackalloc byte[8];
			if (string_0 != null)
			{
				*(int*)ptr = -2128831035;
				*(int*)((byte*)ptr + 4) = 0;
				while (*(int*)((byte*)ptr + 4) < string_0.Length)
				{
					*(int*)ptr = (int)(((uint)string_0[*(int*)((byte*)ptr + 4)] ^ *(uint*)ptr) * 16777619U);
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
			}
			return *(uint*)ptr;
		}
	}
}
