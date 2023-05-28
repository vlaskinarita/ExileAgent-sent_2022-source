using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using ns0;
using ns1;
using ns12;
using ns22;
using ns27;
using ns29;
using ns33;
using ns35;
using ns36;
using ns39;
using ns40;
using ns8;
using ns9;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.Models.Query;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers
{
	public static class Pricing
	{
		private static decimal smethod_0(List<FetchTradeResult> list_0)
		{
			IEnumerable<FetchTradeResult> source = list_0.Where(new Func<FetchTradeResult, bool>(Pricing.<>c.<>9.method_0));
			decimal result;
			if (source.Count<FetchTradeResult>() == 0)
			{
				result = 0m;
			}
			else
			{
				result = source.Sum(new Func<FetchTradeResult, decimal>(Pricing.<>c.<>9.method_1)) / source.Count<FetchTradeResult>();
			}
			return result;
		}

		private unsafe static string smethod_1(List<FetchTradeResult> list_0)
		{
			void* ptr = stackalloc byte[3];
			List<string> list = new List<string>();
			*(byte*)ptr = (list_0.Any<FetchTradeResult>() ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				Pricing.Class165 @class = new Pricing.Class165();
				foreach (FetchTradeResult fetchTradeResult in list_0)
				{
					((byte*)ptr)[1] = ((fetchTradeResult.listing.price == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						list.Add(fetchTradeResult.listing.price.currency);
					}
				}
				((byte*)ptr)[2] = ((!list.Any<string>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					result = null;
				}
				else
				{
					IEnumerable<IGrouping<string, string>> source = list.GroupBy(new Func<string, string>(Pricing.<>c.<>9.method_2));
					@class.int_0 = source.Max(new Func<IGrouping<string, string>, int>(Pricing.<>c.<>9.method_3));
					string[] array = source.Where(new Func<IGrouping<string, string>, bool>(@class.method_0)).Select(new Func<IGrouping<string, string>, string>(Pricing.<>c.<>9.method_4)).ToArray<string>();
					result = array[0];
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		private unsafe static List<FetchTradeResult> smethod_2(string string_0, string string_1, int int_0, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[8];
			*(int*)ptr = (int)Class255.class105_0.method_6(ConfigOptions.ListingsToSkip);
			*(int*)((byte*)ptr + 4) = (int)Class255.class105_0.method_6(ConfigOptions.ListingsToTake);
			return Pricing.smethod_3(string_0, string_1, int_0, *(int*)ptr, *(int*)((byte*)ptr + 4), bool_0);
		}

		private unsafe static List<FetchTradeResult> smethod_3(string string_0, string string_1, int int_0, int int_1, int int_2, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((int_0 == 0) ? 1 : 0);
			List<FetchTradeResult> result;
			if (*(sbyte*)ptr != 0)
			{
				result = null;
			}
			else
			{
				string text = JsonConvert.SerializeObject(new Class272(new Class270(string_0, string_1, int_0)), Util.smethod_10());
				List<FetchTradeResult> list = new List<FetchTradeResult>();
				Class181.smethod_3(Enum11.const_3, Pricing.getString_0(107463709) + text);
				Class247 @class = Web.smethod_8(Class103.ExchangeApiUrl, text, Enum5.const_1);
				((byte*)ptr)[1] = ((!bool_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Thread.Sleep(7300);
				}
				((byte*)ptr)[2] = ((@class.Response > Enum7.const_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Pricing.getString_0(107463700), new object[]
					{
						string_1,
						string_0,
						@class.Reason
					});
					result = null;
				}
				else
				{
					Class266 class2 = JsonConvert.DeserializeObject<Class266>(@class.WebData, new JsonConverter[]
					{
						new FetchTradeResult.GClass20()
					});
					((byte*)ptr)[3] = ((class2.total < int_1 + int_2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						Thread.Sleep(1000);
						list = Pricing.smethod_2(string_0, string_1, int_0 / 2, false);
					}
					else
					{
						list = class2.result.Skip(int_1).Take(int_2).ToList<FetchTradeResult>();
					}
					result = list;
				}
			}
			return result;
		}

		private unsafe static List<FetchTradeResult> smethod_4(JsonItem jsonItem_0, int int_0, int int_1, string string_0)
		{
			void* ptr = stackalloc byte[5];
			string text = JsonConvert.SerializeObject(Class180.smethod_0(jsonItem_0, string_0), Util.smethod_11());
			List<FetchTradeResult> list = new List<FetchTradeResult>();
			Class181.smethod_3(Enum11.const_3, Pricing.getString_0(107463709) + text);
			Class247 @class = Web.smethod_8(Class103.TradeApiUrl, text, Enum5.const_1);
			Thread.Sleep(5500);
			*(byte*)ptr = ((@class.Response > Enum7.const_0) ? 1 : 0);
			List<FetchTradeResult> result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_2(Enum11.const_2, Pricing.getString_0(107464163), new object[]
				{
					jsonItem_0.Name,
					@class.Reason
				});
				result = null;
			}
			else
			{
				Class271 class2 = JsonConvert.DeserializeObject<Class271>(@class.WebData);
				List<string> list2 = class2.result.ToList<string>();
				((byte*)ptr)[1] = ((list2.Count < int_0 + int_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Pricing.getString_0(107464106), new object[]
					{
						jsonItem_0.Name
					});
					result = null;
				}
				else
				{
					list2.RemoveRange(0, int_0);
					for (;;)
					{
						if (list2.Any<string>())
						{
							goto IL_22E;
						}
						bool flag = false;
						IL_FF:
						if (!flag)
						{
							goto IL_273;
						}
						IEnumerable<string> enumerable = list2.Take(10);
						Stream stream = Web.smethod_9(enumerable, class2.id, false);
						Thread.Sleep(4000);
						list2.RemoveRange(0, enumerable.Count<string>());
						((byte*)ptr)[2] = ((stream == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							break;
						}
						using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
						{
							Class276 class3 = JsonConvert.DeserializeObject<Class276>(streamReader.ReadToEnd());
							((byte*)ptr)[3] = (Class255.class105_0.method_4(ConfigOptions.UniqueListings) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								using (IEnumerator<FetchTradeResult> enumerator = class3.result.Where(new Func<FetchTradeResult, bool>(Pricing.<>c.<>9.method_5)).GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										Pricing.Class166 class4 = new Pricing.Class166();
										class4.fetchTradeResult_0 = enumerator.Current;
										((byte*)ptr)[4] = ((!list.Any(new Func<FetchTradeResult, bool>(class4.method_0))) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 4) != 0)
										{
											list.Add(class4.fetchTradeResult_0);
										}
									}
								}
								continue;
							}
							list.AddRange(class3.result);
							goto IL_273;
						}
						IL_22E:
						flag = (list.Count < int_1);
						goto IL_FF;
					}
					Class181.smethod_2(Enum11.const_2, Pricing.getString_0(107464037), new object[]
					{
						jsonItem_0.Name
					});
					return null;
					IL_273:
					result = list.Take(int_1).ToList<FetchTradeResult>();
				}
			}
			return result;
		}

		public static Dictionary<string, decimal> smethod_5(string string_0, string string_1, int int_0 = 1, bool bool_0 = false)
		{
			Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
			List<FetchTradeResult> list = Pricing.smethod_2(string_0, string_1, int_0, bool_0);
			if (list == null)
			{
				dictionary.Add(Pricing.getString_0(107367345), 0m);
			}
			else
			{
				decimal value = Pricing.smethod_0(list);
				dictionary.Add(Pricing.getString_0(107362595), value);
			}
			return dictionary;
		}

		public static Dictionary<string, decimal> smethod_6(string string_0, string string_1, int int_0, int int_1, int int_2)
		{
			Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
			List<FetchTradeResult> list = Pricing.smethod_3(string_0, string_1, int_0, int_1, int_2, false);
			if (list == null)
			{
				dictionary.Add(Pricing.getString_0(107367345), 0m);
			}
			else
			{
				decimal value = Pricing.smethod_0(list);
				dictionary.Add(Pricing.getString_0(107362595), value);
			}
			return dictionary;
		}

		public static string smethod_7(JsonItem jsonItem_0, string string_0)
		{
			return Pricing.smethod_9(Pricing.smethod_4(jsonItem_0, Class255.class105_0.method_5(ConfigOptions.ListingsToSkip), Class255.class105_0.method_5(ConfigOptions.ListingsToTake), string_0));
		}

		public static string smethod_8(JsonItem jsonItem_0, int int_0, int int_1, string string_0)
		{
			return Pricing.smethod_9(Pricing.smethod_4(jsonItem_0, int_0, int_1, string_0));
		}

		private unsafe static string smethod_9(List<FetchTradeResult> list_0)
		{
			void* ptr = stackalloc byte[27];
			string result;
			if (list_0 == null || !list_0.Any<FetchTradeResult>())
			{
				result = null;
			}
			else
			{
				string text = Pricing.smethod_1(list_0);
				*(double*)ptr = (double)Pricing.smethod_0(list_0);
				((byte*)ptr)[24] = ((*(double*)ptr == 0.0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 24) != 0)
				{
					result = null;
				}
				else
				{
					*(double*)((byte*)ptr + 8) = Math.Round(*(double*)ptr, 1);
					((byte*)ptr)[25] = ((text != API.smethod_4(Pricing.getString_0(107383718))) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 25) != 0)
					{
						*(double*)((byte*)ptr + 16) = *(double*)((byte*)ptr + 8) - (double)((int)(*(double*)((byte*)ptr + 8)));
						((byte*)ptr)[26] = ((*(double*)((byte*)ptr + 16) < 0.5) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 26) != 0)
						{
							*(double*)((byte*)ptr + 8) = Math.Floor(*(double*)((byte*)ptr + 8));
						}
						else
						{
							*(double*)((byte*)ptr + 8) = Math.Ceiling(*(double*)((byte*)ptr + 8));
						}
					}
					result = string.Format(Pricing.getString_0(107463964), ((double*)((byte*)ptr + 8))->ToString().Replace(Pricing.getString_0(107392900), Pricing.getString_0(107369330)), text);
				}
			}
			return result;
		}

		public unsafe static string smethod_10(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[2];
			Dictionary<string, List<JsonItem>> dictionary = new Dictionary<string, List<JsonItem>>();
			foreach (JsonItem jsonItem in list_0)
			{
				*(byte*)ptr = ((jsonItem.note != null) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = ((!dictionary.ContainsKey(jsonItem.note)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						dictionary.Add(jsonItem.note, new List<JsonItem>());
					}
					dictionary[jsonItem.note].Add(jsonItem);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, List<JsonItem>> keyValuePair in dictionary)
			{
				stringBuilder.Append(string.Format(Pricing.getString_0(107463975), keyValuePair.Key));
				foreach (JsonItem jsonItem2 in keyValuePair.Value)
				{
					stringBuilder.Append(string.Format(Pricing.getString_0(107463410), new object[]
					{
						jsonItem2.inventoryId,
						Class255.class105_0.method_3(ConfigOptions.League),
						jsonItem2.x,
						jsonItem2.y
					}));
				}
				stringBuilder.Append(Pricing.getString_0(107463369));
			}
			return stringBuilder.ToString();
		}

		static Pricing()
		{
			Strings.CreateGetStringDelegate(typeof(Pricing));
		}

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class165
		{
			internal bool method_0(IGrouping<string, string> igrouping_0)
			{
				return igrouping_0.Count<string>() == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class166
		{
			internal bool method_0(FetchTradeResult fetchTradeResult_1)
			{
				return fetchTradeResult_1 != null && fetchTradeResult_1.listing.account.name == this.fetchTradeResult_0.listing.account.name;
			}

			public FetchTradeResult fetchTradeResult_0;
		}
	}
}
