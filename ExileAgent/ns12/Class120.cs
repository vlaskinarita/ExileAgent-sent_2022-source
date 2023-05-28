using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using ns0;
using ns14;
using ns16;
using ns18;
using ns20;
using ns24;
using ns27;
using ns29;
using ns3;
using ns31;
using ns33;
using ns34;
using ns35;
using ns38;
using ns4;
using ns5;
using ns8;
using PoEv2;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns12
{
	internal sealed class Class120
	{
		public static bool UsingWindows7 { get; set; }

		public static bool UsingWindows8 { get; set; }

		public static List<string> EnglishDictionary { get; set; }

		public static string PoEDirectory
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.GamePath);
			}
		}

		public static string PoeIniPath
		{
			get
			{
				return Path.GetPathRoot(Environment.SystemDirectory) + Class120.getString_0(107371938) + Class120.string_6 + Class120.getString_0(107371961);
			}
		}

		public static string PoELogFile
		{
			get
			{
				return Class120.PoEDirectory + Class120.getString_0(107371852);
			}
		}

		public static void smethod_0()
		{
			Class120.dictionary_0 = new Dictionary<Enum2, int>
			{
				{
					Enum2.const_0,
					Class255.class105_0.method_7(ConfigOptions.ChangeStashTab)
				},
				{
					Enum2.const_1,
					Class255.class105_0.method_7(ConfigOptions.LoadStashTab)
				},
				{
					Enum2.const_2,
					Class255.class105_0.method_7(ConfigOptions.ClickItemFromStash)
				},
				{
					Enum2.const_3,
					Class255.class105_0.method_7(ConfigOptions.ClickItemFromInventory)
				},
				{
					Enum2.const_5,
					Class255.class105_0.method_7(ConfigOptions.ScanItemInTrade)
				},
				{
					Enum2.const_7,
					Class255.class105_0.method_7(ConfigOptions.ReconnectDelay) * 1000
				},
				{
					Enum2.const_8,
					Class255.class105_0.method_7(ConfigOptions.TimeBeforeSaleExpires) * 1000
				},
				{
					Enum2.const_9,
					Class255.class105_0.method_7(ConfigOptions.MaxTradeTime) * 1000
				},
				{
					Enum2.const_10,
					Class255.class105_0.method_7(ConfigOptions.MaxTimeAcceptTrade) * 1000
				},
				{
					Enum2.const_11,
					Class255.class105_0.method_7(ConfigOptions.IntervalBetweenTrades) * 1000
				},
				{
					Enum2.const_12,
					Class255.class105_0.method_7(ConfigOptions.PartyInvite)
				},
				{
					Enum2.const_13,
					Class255.class105_0.method_7(ConfigOptions.PartyKick)
				},
				{
					Enum2.const_14,
					Class255.class105_0.method_7(ConfigOptions.AcceptDeclineRequest)
				},
				{
					Enum2.const_15,
					Class255.class105_0.method_7(ConfigOptions.Whisper)
				},
				{
					Enum2.const_16,
					Class255.class105_0.method_7(ConfigOptions.TimeBeforeBuyInviteExpires) * 1000
				},
				{
					Enum2.const_17,
					Class255.class105_0.method_7(ConfigOptions.BuyAcceptDeclineRequest)
				},
				{
					Enum2.const_18,
					Class255.class105_0.method_7(ConfigOptions.SetNote)
				},
				{
					Enum2.const_19,
					50 * (11 - Class255.class105_0.method_5(ConfigOptions.CraftSpeed))
				},
				{
					Enum2.const_20,
					Class255.class105_0.method_7(ConfigOptions.CleanerClickSpeed)
				},
				{
					Enum2.const_22,
					200
				},
				{
					Enum2.const_23,
					10
				},
				{
					Enum2.const_24,
					200
				},
				{
					Enum2.const_25,
					50
				},
				{
					Enum2.const_26,
					200
				},
				{
					Enum2.const_27,
					50
				},
				{
					Enum2.const_28,
					10000
				},
				{
					Enum2.const_29,
					10000
				},
				{
					Enum2.const_30,
					10
				},
				{
					Enum2.const_31,
					50
				},
				{
					Enum2.const_32,
					3500
				},
				{
					Enum2.const_33,
					500
				},
				{
					Enum2.const_4,
					40
				},
				{
					Enum2.const_34,
					1000
				}
			};
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class120()
		{
			Strings.CreateGetStringDelegate(typeof(Class120));
			Class120.char_0 = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
			Class120.string_0 = Registry.GetValue(Class120.getString_0(107371863), Class120.getString_0(107372290), string.Empty).smethod_10().Replace(Class120.getString_0(107372309), Class120.getString_0(107392066));
			Class120.string_1 = Class120.getString_0(107372272);
			Class120.string_2 = Class120.getString_0(107372287);
			Class120.string_3 = Class120.getString_0(107372230);
			Class120.string_4 = Class120.getString_0(107372249);
			Class120.string_5 = Path.GetDirectoryName(Application.ExecutablePath);
			Class120.bool_2 = false;
			Class120.EnglishDictionary = new List<string>();
			Class120.color_0 = Color.FromArgb(1, 1, 29);
			Class120.string_6 = null;
			Class120.list_1 = new List<Class253>
			{
				new Class253(800, 600, typeof(Class236)),
				new Class253(1024, 768, typeof(Class221)),
				new Class253(1152, 864, typeof(Class222)),
				new Class253(1176, 664, typeof(Class223)),
				new Class253(1280, 720, typeof(Class225)),
				new Class253(1280, 768, typeof(Class226)),
				new Class253(1280, 800, typeof(Class227)),
				new Class253(1280, 960, typeof(Class228)),
				new Class253(1280, 1024, typeof(Class224)),
				new Class253(1360, 768, typeof(Class229)),
				new Class253(1366, 768, typeof(Class230)),
				new Class253(1440, 900, typeof(Class231)),
				new Class253(1600, 900, typeof(Class233)),
				new Class253(1600, 1024, typeof(Class232)),
				new Class253(1680, 1050, typeof(Class234)),
				new Class253(1920, 1080, typeof(Class235))
			};
			Class120.list_2 = new List<string>
			{
				Class120.getString_0(107391100),
				Class120.getString_0(107372200)
			};
			Class120.list_3 = new List<string>
			{
				Class120.getString_0(107390743),
				Class120.getString_0(107396870),
				Class120.getString_0(107390698)
			};
		}

		public static char char_0;

		public static string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool bool_1;

		public static string string_1;

		public static string string_2;

		public const int int_0 = 20;

		public const int int_1 = 3;

		public static string string_3;

		public static string string_4;

		public static string string_5;

		public static Dictionary<Enum2, int> dictionary_0;

		public const int int_2 = 180;

		public static bool bool_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static List<string> list_0;

		public static Color color_0;

		public const int int_3 = 4000;

		public const int int_4 = 5500;

		public const int int_5 = 7300;

		public const double double_0 = 0.47;

		public static string string_6;

		public static List<Class253> list_1;

		public static List<string> list_2;

		public static Dictionary<string, Class241> dictionary_1;

		public static List<string> list_3;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
