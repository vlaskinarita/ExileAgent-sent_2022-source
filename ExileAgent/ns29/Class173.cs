using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ns0;
using ns12;
using ns35;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns29
{
	internal static class Class173
	{
		public unsafe static bool smethod_0(string string_0, MainForm mainForm_0)
		{
			void* ptr = stackalloc byte[71];
			Class173.Class174 @class = new Class173.Class174();
			@class.order_0 = new Order
			{
				OrderType = Order.Type.Sell,
				WhisperTime = DateTime.Now
			};
			ApiItem apiItem = null;
			Match match = Class173.regex_0.Match(string_0);
			((byte*)ptr)[32] = ((match.Groups.Count == 1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 32) != 0)
			{
				((byte*)ptr)[33] = 0;
			}
			else
			{
				Match match2 = Class173.regex_1.Match(string_0);
				string value = match2.Groups[1].Value;
				@class.order_0.player.name = match.Groups[1].Value;
				@class.order_0.message = string_0;
				@class.order_0.OrderType = Order.Type.Sell;
				((byte*)ptr)[34] = (mainForm_0.list_17.Any(new Func<string, bool>(@class.method_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 34) != 0)
				{
					Class181.smethod_2(Enum11.const_3, Class173.getString_0(107455551), new object[]
					{
						@class.order_0.player
					});
					((byte*)ptr)[33] = 0;
				}
				else
				{
					((byte*)ptr)[35] = (mainForm_0.list_15.Any(new Func<Player, bool>(@class.method_1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 35) != 0)
					{
						Class181.smethod_2(Enum11.const_0, Class173.getString_0(107455490), new object[]
						{
							@class.order_0.player
						});
						((byte*)ptr)[33] = 0;
					}
					else
					{
						((byte*)ptr)[36] = (Class255.class105_0.method_4(ConfigOptions.IgnoreRepeatPlayerEnabled) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 36) != 0)
						{
							((byte*)ptr)[37] = (mainForm_0.LastPlayerWhispers.ContainsKey(@class.order_0.player.name) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 37) != 0)
							{
								*(double*)ptr = DateTime.Now.Subtract(mainForm_0.LastPlayerWhispers[@class.order_0.player.name]).TotalMinutes;
								((byte*)ptr)[38] = ((*(double*)ptr < (double)Class255.class105_0.method_5(ConfigOptions.IgnoreRepeatPlayerTime)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 38) != 0)
								{
									Class181.smethod_2(Enum11.const_0, Class173.getString_0(107453183), new object[]
									{
										@class.order_0.player,
										Math.Round(*(double*)ptr)
									});
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								mainForm_0.LastPlayerWhispers[@class.order_0.player.name] = DateTime.Now;
							}
							else
							{
								mainForm_0.LastPlayerWhispers.Add(@class.order_0.player.name, DateTime.Now);
							}
						}
						((byte*)ptr)[39] = (match.Groups[7].Value.Contains(Class173.getString_0(107453042)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 39) != 0)
						{
							string text = match.Groups[7].Value.Replace(Class173.getString_0(107369381), Class173.getString_0(107453065));
							Match match3 = Class173.regex_2.Match(text);
							((byte*)ptr)[40] = ((match3.Groups.Count == 4) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 40) != 0)
							{
								text = match3.Groups[1].Value.Replace(Class173.getString_0(107453065), Class173.getString_0(107397368));
								((byte*)ptr)[41] = ((!int.TryParse(match3.Groups[2].Value, out *(int*)((byte*)ptr + 16))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 41) != 0)
								{
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								((byte*)ptr)[42] = ((!int.TryParse(match3.Groups[3].Value, out *(int*)((byte*)ptr + 20))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 42) != 0)
								{
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								@class.order_0.left_pos = *(int*)((byte*)ptr + 16);
								@class.order_0.top_pos = *(int*)((byte*)ptr + 20);
							}
							((byte*)ptr)[43] = (match.Groups[3].Value.Contains(Class173.getString_0(107453060)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 43) != 0)
							{
								Match match4 = Class173.regex_3.Match(match.Groups[3].Value);
								((byte*)ptr)[44] = ((!int.TryParse(match4.Groups[1].Value.Replace(Class173.getString_0(107453055), Class173.getString_0(107397368)), out *(int*)((byte*)ptr + 24))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 44) != 0)
								{
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								@class.order_0.OurMapTier = *(int*)((byte*)ptr + 24);
								string string_ = match.Groups[3].Value.Split(new string[]
								{
									Class173.getString_0(107453530)
								}, StringSplitOptions.None)[0];
								apiItem = API.smethod_7(string_);
								((byte*)ptr)[45] = ((apiItem.Type == Class173.getString_0(107453525)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 45) != 0)
								{
									@class.order_0.OurItemUnique = true;
									apiItem.Type = Class173.getString_0(107401231);
								}
							}
							else
							{
								apiItem = API.smethod_7(match.Groups[3].Value);
							}
							@class.order_0.my_item_name = apiItem.Text;
							decimal player_item_amount;
							((byte*)ptr)[46] = ((!decimal.TryParse(match.Groups[4].Value.Replace('.', Class120.char_0), out player_item_amount)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 46) != 0)
							{
								((byte*)ptr)[33] = 0;
								goto IL_11FE;
							}
							@class.order_0.player_item_amount = player_item_amount;
							apiItem = API.smethod_7(match.Groups[5].Value);
							@class.order_0.player_item_name = apiItem.Text;
							List<JsonTab> list = Stashes.smethod_6(text);
							((byte*)ptr)[47] = ((list.Count == 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 47) != 0)
							{
								Class181.smethod_3(Enum11.const_2, string.Format(Class173.getString_0(107453544), text));
								((byte*)ptr)[33] = 0;
								goto IL_11FE;
							}
							((byte*)ptr)[48] = ((list.Count > 1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 48) != 0)
							{
								@class.order_0.stash = null;
								foreach (JsonTab jsonTab in list)
								{
									JsonItem jsonItem = StashManager.smethod_10(jsonTab.i, @class.order_0.left_pos - 1, @class.order_0.top_pos - 1);
									if (jsonItem != null && (jsonItem.Name.ToLower().Contains(@class.order_0.my_item_name.ToLower()) || @class.order_0.my_item_name.ToLower().Contains(jsonItem.Name.ToLower())))
									{
										@class.order_0.stash = jsonTab;
										break;
									}
								}
								((byte*)ptr)[49] = ((@class.order_0.stash == null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 49) != 0)
								{
									Class181.smethod_3(Enum11.const_2, string.Format(Class173.getString_0(107453471), @class.order_0.my_item_name, @class.order_0.left_pos, @class.order_0.top_pos));
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
							}
							else
							{
								@class.order_0.stash = list.First<JsonTab>();
							}
							if (@class.order_0.stash.IsExcluded || @class.order_0.stash == Stashes.smethod_13())
							{
								((byte*)ptr)[50] = ((API.smethod_7(@class.order_0.my_item_name).Class == Class173.getString_0(107453414)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 50) == 0)
								{
									Class181.smethod_2(Enum11.const_0, Class173.getString_0(107452703), new object[]
									{
										@class.order_0.stash.n
									});
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								string text2;
								((byte*)ptr)[51] = (StashManager.smethod_2(@class.order_0.my_item_name, 0, 0, false, false, string.Empty, out text2, out *(bool*)((byte*)ptr + 52), true).Any<KeyValuePair<JsonTab, List<JsonItem>>>() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 51) == 0)
								{
									Class181.smethod_2(Enum11.const_0, Class173.getString_0(107453312), new object[]
									{
										@class.order_0.stash.n
									});
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								Class181.smethod_3(Enum11.const_3, Class173.getString_0(107453357));
								@class.order_0.top_pos = 0;
								@class.order_0.left_pos = 0;
							}
							JsonItem jsonItem2 = StashManager.smethod_10(@class.order_0.stash.i, @class.order_0.left_pos - 1, @class.order_0.top_pos - 1);
							((byte*)ptr)[53] = ((jsonItem2 != null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 53) != 0)
							{
								@class.order_0.OurItemUnique = jsonItem2.IsUnique;
							}
						}
						if (@class.order_0.left_pos == 0 && @class.order_0.top_pos == 0)
						{
							string arg = Class173.getString_0(107397368);
							((byte*)ptr)[54] = (match.Groups[3].Value.Contains(Class173.getString_0(107453060)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 54) != 0)
							{
								if (!match.Groups[3].Value.Contains(Class173.getString_0(107452598)) && (match.Groups[3].Value.Contains(Class173.getString_0(107392992)) || match.Groups[3].Value.Contains(Class173.getString_0(107452613))))
								{
									Class181.smethod_3(Enum11.const_3, Class173.getString_0(107452608));
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								Match match5 = Class173.regex_3.Match(match.Groups[3].Value);
								((byte*)ptr)[55] = ((!int.TryParse(match5.Groups[1].Value.Replace(Class173.getString_0(107453055), Class173.getString_0(107397368)), out *(int*)((byte*)ptr + 28))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 55) != 0)
								{
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								@class.order_0.OurMapTier = *(int*)((byte*)ptr + 28);
								arg = match.Groups[3].Value.Split(new string[]
								{
									Class173.getString_0(107453530)
								}, StringSplitOptions.None)[0];
								apiItem = API.smethod_7(string.Format(Class173.getString_0(107452527), arg));
							}
							else
							{
								((byte*)ptr)[56] = (match.Groups[3].Value.Contains(Class173.getString_0(107452613)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 56) != 0)
								{
									Class181.smethod_3(Enum11.const_3, Class173.getString_0(107452554));
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								apiItem = API.smethod_7(match.Groups[3].Value);
							}
							if (@class.order_0.left_pos == 0 && @class.order_0.top_pos == 0 && apiItem.Type == Class173.getString_0(107371635))
							{
								((byte*)ptr)[57] = (Class255.class105_0.method_4(ConfigOptions.ForumThreadEnabled) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 57) == 0)
								{
									Class181.smethod_3(Enum11.const_3, string.Format(Class173.getString_0(107452912), apiItem.Text));
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
								((byte*)ptr)[58] = 0;
								foreach (KeyValuePair<int, List<string>> keyValuePair in Stashes.ForumTheadItemIds)
								{
									foreach (string string_2 in keyValuePair.Value)
									{
										Tuple<int, JsonItem> tuple = Stashes.smethod_5(string_2);
										((byte*)ptr)[59] = ((tuple == null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 59) == 0)
										{
											JsonItem item = tuple.Item2;
											if (!item.note.smethod_25() && (item.Name.ToLower().Contains(apiItem.Text.ToLower()) || apiItem.Text.ToLower().Contains(item.Name.ToLower())))
											{
												@class.order_0.left_pos = item.x;
												@class.order_0.top_pos = item.y;
												@class.order_0.OurItemUnique = item.IsUnique;
												((byte*)ptr)[58] = 1;
												break;
											}
										}
									}
									((byte*)ptr)[60] = (byte)(*(sbyte*)((byte*)ptr + 58));
									if (*(sbyte*)((byte*)ptr + 60) != 0)
									{
										break;
									}
								}
								((byte*)ptr)[61] = ((*(sbyte*)((byte*)ptr + 58) == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 61) != 0)
								{
									Class181.smethod_2(Enum11.const_2, Class173.getString_0(107452981), new object[]
									{
										apiItem.Text
									});
									((byte*)ptr)[33] = 0;
									goto IL_11FE;
								}
							}
							@class.order_0.my_item_name = apiItem.Text;
							decimal player_item_amount2;
							((byte*)ptr)[62] = ((!decimal.TryParse(match.Groups[4].Value.Replace('.', Class120.char_0), out player_item_amount2)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 62) != 0)
							{
								((byte*)ptr)[33] = 0;
								goto IL_11FE;
							}
							@class.order_0.player_item_amount = player_item_amount2;
							((byte*)ptr)[63] = ((apiItem.Type == Class173.getString_0(107453525)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 63) != 0)
							{
								apiItem = API.smethod_7(string.Format(Class173.getString_0(107452527), arg));
								@class.order_0.my_item_name = apiItem.Text;
								@class.order_0.OurItemUnique = true;
							}
							apiItem = API.smethod_7(match.Groups[5].Value);
							@class.order_0.player_item_name = apiItem.Text;
						}
						((byte*)ptr)[64] = (apiItem.Text.Contains(Class173.getString_0(107370069)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 64) != 0)
						{
							@class.order_0.my_item_name = Class173.getString_0(107370069);
						}
						((byte*)ptr)[65] = ((match.Groups[2].Value != Class173.getString_0(107397368)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 65) != 0)
						{
							decimal my_item_amount;
							((byte*)ptr)[66] = ((!decimal.TryParse(match.Groups[2].Value.Replace('.', Class120.char_0), out my_item_amount)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 66) != 0)
							{
								((byte*)ptr)[33] = 0;
								goto IL_11FE;
							}
							@class.order_0.my_item_amount = my_item_amount;
						}
						else
						{
							@class.order_0.my_item_amount = 1m;
						}
						if (@class.order_0.left_pos != 0 && @class.order_0.top_pos != 0 && (@class.order_0.my_item_amount != 1m && API.smethod_7(@class.order_0.my_item_name).Type == Class173.getString_0(107371635)))
						{
							@class.order_0.player_item_amount /= @class.order_0.my_item_amount;
							@class.order_0.my_item_amount = 1m;
						}
						((byte*)ptr)[67] = (mainForm_0.list_7.Any(new Func<Order, bool>(@class.method_2)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 67) != 0)
						{
							Class181.smethod_2(Enum11.const_0, Class173.getString_0(107452851), new object[]
							{
								@class.order_0.player
							});
							((byte*)ptr)[33] = 0;
						}
						else
						{
							*(int*)((byte*)ptr + 8) = API.smethod_6(@class.order_0.my_item_name);
							*(int*)((byte*)ptr + 12) = API.smethod_6(@class.order_0.player_item_name);
							decimal val = Math.Ceiling(@class.order_0.my_item_amount / *(int*)((byte*)ptr + 8) / 60m);
							decimal val2 = Math.Ceiling(@class.order_0.player_item_amount / *(int*)((byte*)ptr + 12) / 60m);
							((byte*)ptr)[68] = (@class.order_0.my_item_name.Contains(Class173.getString_0(107454336)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 68) != 0)
							{
								((byte*)ptr)[69] = (@class.order_0.my_item_name.Contains(Class173.getString_0(107452250)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 69) != 0)
								{
									val = Math.Ceiling(@class.order_0.my_item_amount / *(int*)((byte*)ptr + 8) / 24m);
								}
								else if (@class.order_0.my_item_name.Contains(Class173.getString_0(107452241)) || @class.order_0.my_item_name.Contains(Class173.getString_0(107452260)))
								{
									val = Math.Ceiling(@class.order_0.my_item_amount / *(int*)((byte*)ptr + 8) / 12m);
								}
							}
							decimal d = Math.Max(val, val2);
							Class181.smethod_3(Enum11.const_3, Class173.getString_0(107452219) + d.ToString());
							((byte*)ptr)[70] = ((d == 1m) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 70) != 0)
							{
								@class.order_0.ProcessedTime = DateTime.Now;
								mainForm_0.list_7.Add(@class.order_0);
								mainForm_0.method_65();
								mainForm_0.list_5.Add(@class.order_0.player.name);
								Class181.smethod_3(Enum11.const_0, Class173.getString_0(107452186) + value);
								Class307.smethod_0(ConfigOptions.OnTradeRequest, Class173.getString_0(107452189), value);
								((byte*)ptr)[33] = 1;
							}
							else
							{
								((byte*)ptr)[33] = 0;
							}
						}
					}
				}
			}
			IL_11FE:
			return *(sbyte*)((byte*)ptr + 33) != 0;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class173()
		{
			Strings.CreateGetStringDelegate(typeof(Class173));
			Class173.regex_0 = new Regex(Class173.getString_0(107452168));
			Class173.regex_1 = new Regex(Class173.getString_0(107452522));
			Class173.regex_2 = new Regex(Class173.getString_0(107452509));
			Class173.regex_3 = new Regex(Class173.getString_0(107452432));
		}

		private static Regex regex_0;

		private static Regex regex_1;

		private static Regex regex_2;

		private static Regex regex_3;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class174
		{
			internal bool method_0(string string_0)
			{
				return string_0.ToLower() == this.order_0.player.name.ToLower();
			}

			internal bool method_1(Player player_0)
			{
				return player_0.name.ToLower() == this.order_0.player.name.ToLower();
			}

			internal bool method_2(Order order_1)
			{
				return order_1.player.name == this.order_0.player.name;
			}

			public Order order_0;
		}
	}
}
