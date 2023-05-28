using System;
using ns20;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns0
{
	internal static class Class307
	{
		public unsafe static void smethod_0(ConfigOptions configOptions_0, string string_0, string string_1)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!Class255.class105_0.method_4(configOptions_0)) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.class105_0.method_4(ConfigOptions.PushBulletEnabled) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class164.smethod_1(Class255.class105_0.method_3(ConfigOptions.PushBulletToken), string_0, string.Format(Class307.getString_0(107364456), Class255.class105_0.method_3(ConfigOptions.AccountName), string_1));
				}
				((byte*)ptr)[2] = (Class255.class105_0.method_4(ConfigOptions.DiscordEnabled) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class164.smethod_2(Class255.class105_0.method_3(ConfigOptions.DiscordWebHookUrl), string.Format(Class307.getString_0(107285058), string_1));
				}
			}
		}

		public unsafe static void smethod_1(ConfigOptions configOptions_0, string string_0, string string_1, params object[] object_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!Class255.class105_0.method_4(configOptions_0)) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.class105_0.method_4(ConfigOptions.PushBulletEnabled) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class164.smethod_1(Class255.class105_0.method_3(ConfigOptions.PushBulletToken), string_0, string.Format(Class307.getString_0(107364456), Class255.class105_0.method_3(ConfigOptions.AccountName), string_1, object_0));
				}
				((byte*)ptr)[2] = (Class255.class105_0.method_4(ConfigOptions.DiscordEnabled) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class164.smethod_2(Class255.class105_0.method_3(ConfigOptions.DiscordWebHookUrl), string.Format(Class307.getString_0(107285058), string.Format(string_1, object_0)));
				}
			}
		}

		static Class307()
		{
			Strings.CreateGetStringDelegate(typeof(Class307));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
