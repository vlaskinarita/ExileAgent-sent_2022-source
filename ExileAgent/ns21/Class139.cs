using System;
using ns0;
using ns29;
using ns6;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns21
{
	internal static class Class139
	{
		public static string AuthKey
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.AuthKey);
			}
		}

		public static string ChannelName
		{
			get
			{
				return Class139.getString_0(107370525) + Class139.AuthKey;
			}
		}

		public static string smethod_0(Enum3 enum3_0)
		{
			return Class139.getString_0(107370540) + Class142.smethod_0(enum3_0);
		}

		static Class139()
		{
			Strings.CreateGetStringDelegate(typeof(Class139));
		}

		public const int int_0 = 15;

		public const int int_1 = 14;

		public const int int_2 = 13;

		public const string string_0 = "YGkDYCYUN9zuTKsd4SrBXSeG";

		public const string string_1 = "exileagent.com";

		public const string string_2 = "https://exileagent.com/broadcasting/auth";

		public const string string_3 = "https://forum.exileagent.com/viewtopic.php?f=15&t=31";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
