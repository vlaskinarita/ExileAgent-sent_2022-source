using System;
using PoEv2.Classes;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns31
{
	internal static class Class148
	{
		public unsafe static bool smethod_0(Order order_0)
		{
			void* ptr = stackalloc byte[2];
			string type = API.smethod_7(order_0.my_item_name).Type;
			if (type == Class148.getString_0(107371500) || type == Class148.getString_0(107394292))
			{
				*(byte*)ptr = 0;
			}
			else
			{
				((byte*)ptr)[1] = ((!order_0.IsSingleItem) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					*(byte*)ptr = 0;
				}
				else
				{
					*(byte*)ptr = 1;
				}
			}
			return *(sbyte*)ptr != 0;
		}

		static Class148()
		{
			Strings.CreateGetStringDelegate(typeof(Class148));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
