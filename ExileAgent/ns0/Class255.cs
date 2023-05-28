using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ns11;
using ns12;
using ns27;
using ns33;
using PoEv2;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns0
{
	internal static class Class255
	{
		public static void smethod_0(MainForm mainForm_0)
		{
			Class255.class105_0 = new Class105(mainForm_0);
			Class255.class105_0.method_0(0);
		}

		public static Class253 Resolution
		{
			get
			{
				return Class120.list_1[Class255.class105_0.method_5(ConfigOptions.Resolution)];
			}
		}

		public static Dictionary<int, List<int>> StashProfileTabs
		{
			get
			{
				return Class255.class105_0.method_2<Dictionary<int, List<int>>>(ConfigOptions.StashProfileTabs);
			}
		}

		public static Dictionary<string, string> Cookies
		{
			get
			{
				return new Dictionary<string, string>
				{
					{
						Class255.getString_0(107442087),
						Class255.class105_0.method_3(ConfigOptions.POESESSID)
					}
				};
			}
		}

		public static List<int> CurrentStashProfile
		{
			get
			{
				return Class255.StashProfileTabs[Class255.class105_0.method_5(ConfigOptions.StashProfileSelected)];
			}
		}

		public static List<LiveSearchListItem> LiveSearchList
		{
			get
			{
				return Class255.class105_0.method_8<LiveSearchListItem>(ConfigOptions.LiveSearchList);
			}
		}

		public static List<FlippingListItem> FlippingList
		{
			get
			{
				return Class255.class105_0.method_8<FlippingListItem>(ConfigOptions.FlippingList);
			}
		}

		public static List<DecimalCurrencyListItem> DecimalCurrencyList
		{
			get
			{
				return Class255.class105_0.method_8<DecimalCurrencyListItem>(ConfigOptions.DecimalCurrencyList);
			}
		}

		public static List<BulkBuyingListItem> BulkBuyingList
		{
			get
			{
				return Class255.class105_0.method_8<BulkBuyingListItem>(ConfigOptions.BulkBuyingList);
			}
		}

		public static List<ItemBuyingListItem> ItemBuyingList
		{
			get
			{
				return Class255.class105_0.method_8<ItemBuyingListItem>(ConfigOptions.ItemBuyingList);
			}
		}

		public static List<string> AcceptedCurrencyList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.AcceptedCurrencyList);
			}
		}

		public static List<int> MuleStashIds
		{
			get
			{
				return Class255.class105_0.method_8<int>(ConfigOptions.MuleStashList);
			}
		}

		public static List<int> BeastStashIds
		{
			get
			{
				return Class255.class105_0.method_8<int>(ConfigOptions.BeastStashList);
			}
		}

		public static List<int> StackedDeckStashIds
		{
			get
			{
				return Class255.class105_0.method_8<int>(ConfigOptions.StackedDeckList);
			}
		}

		public static List<int> GwennenStashIds
		{
			get
			{
				return Class255.class105_0.method_8<int>(ConfigOptions.GwennenStashList);
			}
		}

		public static List<string> BulkTypesList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.BulkTypeList);
			}
		}

		public static List<string> MapPreventedModList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.MapPreventedMods);
			}
		}

		public static List<string> MapForcedModList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.MapForcedMods);
			}
		}

		public static List<string> TradingPriorityList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.TradingPriority);
			}
		}

		public static List<string> BuyingPriorityList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.BuyingPriority);
			}
		}

		public static List<string> CheapChaosList
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.CheapChaosList);
			}
		}

		public static bool LiveSearchEnabled
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.EnableLiveSearch);
			}
		}

		public static bool ItemBuyEnabled
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.EnableItemBuying);
			}
		}

		public static bool BulkBuyEnabled
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.EnableBulkBuying);
			}
		}

		public static List<string> SoldMessages
		{
			get
			{
				return Class255.class105_0.method_8<string>(ConfigOptions.SoldMessageList);
			}
		}

		public static string NetworkingMode
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.NetworkingMode).ToLower();
			}
		}

		public static Class241 GameClient
		{
			get
			{
				return Class120.dictionary_1[Class255.class105_0.method_3(ConfigOptions.GameClient)];
			}
		}

		public static string LeagueClean
		{
			get
			{
				string text = Class255.class105_0.method_3(ConfigOptions.League);
				string result;
				if (string.IsNullOrEmpty(text))
				{
					result = string.Empty;
				}
				else
				{
					Match match = Regex.Match(text, Class255.getString_0(107442074));
					result = match.Groups[1].Value;
				}
				return result;
			}
		}

		static Class255()
		{
			Strings.CreateGetStringDelegate(typeof(Class255));
		}

		public static Class105 class105_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
