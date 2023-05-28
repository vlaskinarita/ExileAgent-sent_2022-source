using System;
using System.Runtime.CompilerServices;

namespace NetMQ.Utils
{
	internal static class ArrayExtensions
	{
		[NullableContext(1)]
		public static void Clear<[Nullable(2)] T>(this T[] array)
		{
			if (array != null)
			{
				Array.Clear(array, 0, array.Length);
			}
		}
	}
}
