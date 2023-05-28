using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns30
{
	internal static class Class146
	{
		public static bool smethod_0(string string_0)
		{
			return string_0.Contains(Class146.getString_0(107369773)) || string_0.Contains(Class146.getString_0(107369760)) || string_0.Contains(Class146.getString_0(107369739)) || string_0.Contains(Class146.getString_0(107369746)) || string_0.Contains(Class146.getString_0(107369697));
		}

		static Class146()
		{
			Strings.CreateGetStringDelegate(typeof(Class146));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
