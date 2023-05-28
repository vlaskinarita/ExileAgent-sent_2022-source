using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns14
{
	internal static class Class395
	{
		public static string SettingsPath
		{
			get
			{
				return string.Format(Class395.getString_0(107231711), Environment.CurrentDirectory);
			}
		}

		static Class395()
		{
			Strings.CreateGetStringDelegate(typeof(Class395));
		}

		public const string string_0 = "Craft Settings|*.craftsettings";

		public const string string_1 = "Flipping Settings|*.fsettings";

		public const string string_2 = "Live Search Settings|*.lssettings";

		public const string string_3 = "Item Buy Settings|*.ibsettings";

		public const string string_4 = "Bulk Buy Settings|*.bbsettings";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
