using System;
using System.Collections.Generic;
using ns29;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns6
{
	internal static class Class142
	{
		public static string smethod_0(Enum3 enum3_0)
		{
			string result;
			if (!Class142.dictionary_0.ContainsKey(enum3_0))
			{
				result = string.Empty;
			}
			else
			{
				result = Class142.dictionary_0[enum3_0];
			}
			return result;
		}

		public static Enum3 smethod_1(string string_0)
		{
			foreach (KeyValuePair<Enum3, string> keyValuePair in Class142.dictionary_0)
			{
				if (keyValuePair.Value == string_0)
				{
					return keyValuePair.Key;
				}
			}
			return Enum3.const_0;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class142()
		{
			Strings.CreateGetStringDelegate(typeof(Class142));
			Class142.dictionary_0 = new Dictionary<Enum3, string>
			{
				{
					Enum3.const_1,
					Class142.getString_0(107370513)
				},
				{
					Enum3.const_3,
					Class142.getString_0(107370468)
				},
				{
					Enum3.const_4,
					Class142.getString_0(107370487)
				},
				{
					Enum3.const_5,
					Class142.getString_0(107370438)
				},
				{
					Enum3.const_6,
					Class142.getString_0(107370449)
				},
				{
					Enum3.const_2,
					Class142.getString_0(107370932)
				},
				{
					Enum3.const_7,
					Class142.getString_0(107370891)
				},
				{
					Enum3.const_8,
					Class142.getString_0(107370906)
				}
			};
		}

		private static Dictionary<Enum3, string> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
