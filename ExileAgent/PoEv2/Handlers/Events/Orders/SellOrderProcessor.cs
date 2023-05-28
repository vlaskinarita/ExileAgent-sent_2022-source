using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using ns0;
using ns12;
using ns14;
using ns24;
using ns29;
using ns31;
using ns35;
using ns8;
using PoEv2.Classes;
using PoEv2.Handlers.Events.Trades;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers.Events.Orders
{
	public static class SellOrderProcessor
	{
		public unsafe static bool smethod_0(Order order_0, MainForm mainForm_0)
		{
			void* ptr = stackalloc byte[42];
			SellOrderProcessor.Class169 @class = new SellOrderProcessor.Class169();
			@class.order_0 = order_0;
			SellOrderProcessor.GClass0 gclass = SellOrderProcessor.smethod_3(@class.order_0);
			((byte*)ptr)[17] = ((!gclass.bool_0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 17) != 0)
			{
				Class181.smethod_3(Enum11.const_2, gclass.string_0);
				((byte*)ptr)[18] = 0;
			}
			else
			{
				if (@class.order_0.UpdateRequest != null && @class.order_0.UpdateRequest.AdditionalItemCount > 0)
				{
					((byte*)ptr)[19] = ((!Class148.smethod_0(@class.order_0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						Class181.smethod_3(Enum11.const_3, SellOrderProcessor.getString_0(107457832));
						((byte*)ptr)[18] = 0;
						goto IL_13E2;
					}
					Class181.smethod_2(Enum11.const_3, SellOrderProcessor.getString_0(107457775), new object[]
					{
						@class.order_0.UpdateRequest.AdditionalItemCount,
						@class.order_0.my_item_name
					});
					decimal num = @class.order_0.my_item_amount + @class.order_0.UpdateRequest.AdditionalItemCount;
					decimal num2 = @class.order_0.player_item_amount / @class.order_0.my_item_amount * num;
					if (num / 60m / API.smethod_6(@class.order_0.my_item_name) > 1m || num2 / 60m / API.smethod_6(@class.order_0.player_item_name) > 1m)
					{
						Class181.smethod_3(Enum11.const_1, SellOrderProcessor.getString_0(107457766));
						((byte*)ptr)[18] = 0;
						goto IL_13E2;
					}
					@class.order_0.my_item_amount = num;
					@class.order_0.player_item_amount = num2;
					@class.order_0.top_pos = 0;
					@class.order_0.left_pos = 0;
					Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457689), new object[]
					{
						@class.order_0.player.name,
						@class.order_0.my_item_amount,
						@class.order_0.my_item_name,
						@class.order_0.player_item_amount,
						@class.order_0.player_item_name
					});
				}
				((byte*)ptr)[16] = 0;
				bool flag;
				if (Class255.class105_0.method_6(ConfigOptions.MaxPendingTrades) > 0m)
				{
					flag = (mainForm_0.list_7.Count(new Func<Order, bool>(SellOrderProcessor.<>c.<>9.method_0)) + mainForm_0.list_8.Count > Class255.class105_0.method_6(ConfigOptions.MaxPendingTrades));
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					Class181.smethod_2(Enum11.const_3, SellOrderProcessor.getString_0(107458100), new object[]
					{
						Class255.class105_0.method_6(ConfigOptions.MaxPendingTrades),
						@class.order_0.player
					});
					((byte*)ptr)[18] = 0;
				}
				else if (Class255.class105_0.method_6(ConfigOptions.MaxTimeBeforeOrderExpires) > 0m && DateTime.Now.Subtract(@class.order_0.WhisperTime).Minutes >= Class255.class105_0.method_6(ConfigOptions.MaxTimeBeforeOrderExpires))
				{
					Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107458015), new object[]
					{
						(int)Class255.class105_0.method_6(ConfigOptions.MaxTimeBeforeOrderExpires),
						@class.order_0.player
					});
					mainForm_0.list_7.Remove(@class.order_0);
					mainForm_0.list_9.Remove(@class.order_0);
					mainForm_0.method_65();
					((byte*)ptr)[18] = 0;
				}
				else
				{
					((byte*)ptr)[20] = ((@class.order_0.my_item_amount != (int)@class.order_0.my_item_amount) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) != 0)
					{
						Class181.smethod_3(Enum11.const_3, SellOrderProcessor.getString_0(107457918));
						((byte*)ptr)[18] = 0;
					}
					else
					{
						if (Class255.FlippingList.Any<FlippingListItem>() && Class255.class105_0.method_4(ConfigOptions.FlippingEnabled) && API.smethod_7(@class.order_0.my_item_name).Type != SellOrderProcessor.getString_0(107371599))
						{
							FlippingListItem flippingListItem = Class255.FlippingList.FirstOrDefault(new Func<FlippingListItem, bool>(@class.method_0));
							((byte*)ptr)[21] = ((flippingListItem != null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 21) != 0)
							{
								if (Class255.class105_0.method_4(ConfigOptions.IgnoreBotsWhileFlipping) && Util.smethod_15(@class.order_0.player.name))
								{
									Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457329), new object[]
									{
										@class.order_0.player
									});
									((byte*)ptr)[18] = 0;
									goto IL_13E2;
								}
								((byte*)ptr)[22] = (Class255.class105_0.method_4(ConfigOptions.IgnorePrivateAccounts) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 22) != 0)
								{
									string text = Web.smethod_2(Class103.smethod_3(@class.order_0.player.name), Encoding.UTF8, null);
									if (text == null || text.Contains(SellOrderProcessor.getString_0(107395876)))
									{
										Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457256), new object[]
										{
											@class.order_0.player
										});
										((byte*)ptr)[18] = 0;
										goto IL_13E2;
									}
									AccountInfo accountInfo = JsonConvert.DeserializeObject<AccountInfo>(text);
									((byte*)ptr)[23] = (accountInfo.IsPrivate ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 23) != 0)
									{
										Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457256), new object[]
										{
											@class.order_0.player
										});
										((byte*)ptr)[18] = 0;
										goto IL_13E2;
									}
									((byte*)ptr)[24] = (Class255.class105_0.method_4(ConfigOptions.IgnoreNewAccounts) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 24) != 0)
									{
										*(int*)((byte*)ptr + 8) = accountInfo.AccountAge;
										decimal num3 = Class255.class105_0.method_6(ConfigOptions.IgnoreNewAccountDays);
										if (*(int*)((byte*)ptr + 8) > 0 && *(int*)((byte*)ptr + 8) < num3)
										{
											Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457211), new object[]
											{
												@class.order_0.player,
												*(int*)((byte*)ptr + 8),
												num3
											});
											((byte*)ptr)[18] = 0;
											goto IL_13E2;
										}
									}
								}
								string text2;
								int num4 = StashManager.smethod_2(@class.order_0.my_item_name, 0, 0, false, false, string.Empty, out text2, out *(bool*)((byte*)ptr + 25), true).Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(SellOrderProcessor.<>c.<>9.method_1));
								int num5 = StashManager.smethod_2(@class.order_0.player_item_name, 0, 0, false, false, string.Empty, out text2, out *(bool*)((byte*)ptr + 25), true).Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(SellOrderProcessor.<>c.<>9.method_3));
								((byte*)ptr)[26] = ((flippingListItem.HaveName == @class.order_0.my_item_name) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 26) != 0)
								{
									*(int*)ptr = flippingListItem.MinHaveStock;
									*(int*)((byte*)ptr + 4) = flippingListItem.MaxWantStock;
								}
								else
								{
									*(int*)ptr = flippingListItem.MinWantStock;
									*(int*)((byte*)ptr + 4) = flippingListItem.MaxHaveStock;
								}
								((byte*)ptr)[27] = ((*(int*)ptr > 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 27) != 0)
								{
									decimal num6 = mainForm_0.list_7.Where(new Func<Order, bool>(@class.method_1)).Sum(new Func<Order, decimal>(SellOrderProcessor.<>c.<>9.method_5));
									int value = 0;
									((byte*)ptr)[28] = (TradeProcessor.Inventory.Any<JsonItem>() ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 28) != 0)
									{
										value = TradeProcessor.Inventory.Where(new Func<JsonItem, bool>(@class.method_2)).Sum(new Func<JsonItem, int>(SellOrderProcessor.<>c.<>9.method_6));
									}
									((byte*)ptr)[29] = ((num4 - num6 - value - @class.order_0.my_item_amount < *(int*)ptr) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 29) != 0)
									{
										Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457586), new object[]
										{
											num4,
											@class.order_0.my_item_amount,
											*(int*)ptr,
											@class.order_0.player_item_name,
											num6
										});
										((byte*)ptr)[18] = 0;
										goto IL_13E2;
									}
								}
								((byte*)ptr)[30] = ((*(int*)((byte*)ptr + 4) > 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 30) != 0)
								{
									decimal num7 = mainForm_0.list_7.Where(new Func<Order, bool>(@class.method_3)).Sum(new Func<Order, decimal>(SellOrderProcessor.<>c.<>9.method_7));
									int num8 = 0;
									((byte*)ptr)[31] = (TradeProcessor.Inventory.Any<JsonItem>() ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 31) != 0)
									{
										num8 = TradeProcessor.Inventory.Where(new Func<JsonItem, bool>(@class.method_4)).Sum(new Func<JsonItem, int>(SellOrderProcessor.<>c.<>9.method_8));
									}
									((byte*)ptr)[32] = ((num5 + num7 + num8 + @class.order_0.player_item_amount > *(int*)((byte*)ptr + 4)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 32) != 0)
									{
										Class181.smethod_2(Enum11.const_0, SellOrderProcessor.getString_0(107457440), new object[]
										{
											num5,
											@class.order_0.player_item_amount,
											*(int*)((byte*)ptr + 4),
											@class.order_0.player_item_name,
											num7,
											num8
										});
										((byte*)ptr)[18] = 0;
										goto IL_13E2;
									}
								}
							}
						}
						if (@class.order_0.left_pos != 0 && @class.order_0.top_pos != 0)
						{
							JsonItem jsonItem;
							((byte*)ptr)[33] = ((!InventoryManager.smethod_0(@class.order_0, mainForm_0.list_7, out jsonItem)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 33) != 0)
							{
								decimal d = mainForm_0.list_7.Where(new Func<Order, bool>(@class.method_5)).Sum(new Func<Order, decimal>(SellOrderProcessor.<>c.<>9.method_9));
								string text2;
								Dictionary<JsonTab, List<JsonItem>> source = StashManager.smethod_2(@class.order_0.my_item_name, 0, @class.order_0.OurMapTier, @class.order_0.OurItemUnique, false, Class252.smethod_4(@class.order_0), out text2, out *(bool*)((byte*)ptr + 25), true);
								bool flag2;
								if (jsonItem != null && !(API.smethod_7(jsonItem.Name).Type == SellOrderProcessor.getString_0(107371599)))
								{
									flag2 = (source.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(SellOrderProcessor.<>c.<>9.method_10)) < d + (int)@class.order_0.my_item_amount);
								}
								else
								{
									flag2 = true;
								}
								if (flag2)
								{
									Class181.smethod_3(Enum11.const_0, string.Format(SellOrderProcessor.getString_0(107456694), new object[]
									{
										@class.order_0.my_item_name,
										@class.order_0.stash.n,
										@class.order_0.left_pos,
										@class.order_0.top_pos
									}));
									SellOrderProcessor.smethod_2(mainForm_0, @class.order_0);
									((byte*)ptr)[18] = 0;
									goto IL_13E2;
								}
							}
							else
							{
								Class181.smethod_3(Enum11.const_0, string.Format(SellOrderProcessor.getString_0(107456617), new object[]
								{
									jsonItem.Name,
									@class.order_0.left_pos,
									@class.order_0.top_pos,
									@class.order_0.stash.n
								}));
							}
							string text3 = Class252.smethod_1(jsonItem, @class.order_0.stash);
							Class252 class2 = Class252.smethod_0(text3, false);
							decimal d2 = Math.Round(class2.Amount * @class.order_0.my_item_amount, 2);
							if (d2 <= @class.order_0.player_item_amount && API.smethod_7(@class.order_0.player_item_name).Id == class2.CurrencyId)
							{
								@class.order_0.OriginalNote = text3;
								((byte*)ptr)[16] = 1;
							}
						}
						else
						{
							Dictionary<JsonTab, List<JsonItem>> source2;
							((byte*)ptr)[35] = ((!InventoryManager.smethod_1(0, @class.order_0, Class252.smethod_4(@class.order_0), mainForm_0.list_7, out source2, out *(int*)((byte*)ptr + 12), false)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 35) != 0)
							{
								string text2;
								Dictionary<JsonTab, List<JsonItem>> source3 = StashManager.smethod_2(@class.order_0.my_item_name, 0, @class.order_0.OurMapTier, @class.order_0.OurItemUnique, false, string.Empty, out text2, out *(bool*)((byte*)ptr + 25), true);
								if (source3.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(SellOrderProcessor.<>c.<>9.method_12)) < *(int*)((byte*)ptr + 12) + (int)@class.order_0.my_item_amount)
								{
									Enum11 enum11_ = Enum11.const_0;
									string format = SellOrderProcessor.getString_0(107457072);
									object[] array = new object[4];
									array[0] = @class.order_0.my_item_name;
									array[1] = source2.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(SellOrderProcessor.<>c.<>9.method_14));
									array[2] = *(int*)((byte*)ptr + 12);
									array[3] = @class.order_0.my_item_amount;
									Class181.smethod_3(enum11_, string.Format(format, array));
									SellOrderProcessor.smethod_2(mainForm_0, @class.order_0);
									((byte*)ptr)[18] = 0;
									goto IL_13E2;
								}
							}
							string string_;
							Dictionary<JsonTab, List<JsonItem>> dictionary = StashManager.smethod_2(@class.order_0.my_item_name, 0, @class.order_0.OurMapTier, @class.order_0.OurItemUnique, true, Class252.smethod_4(@class.order_0), out string_, out *(bool*)((byte*)ptr + 34), false);
							@class.order_0.Flipping = (*(sbyte*)((byte*)ptr + 34) != 0);
							foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair in dictionary)
							{
								foreach (JsonItem jsonItem2 in keyValuePair.Value)
								{
									string text4 = Class252.smethod_2(string_, keyValuePair.Key.n);
									((byte*)ptr)[36] = ((text4 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 36) == 0)
									{
										@class.order_0.OriginalNote = text4;
										Class252 class3 = Class252.smethod_0(text4, false);
										decimal d3 = Math.Round(class3.Amount * @class.order_0.my_item_amount, 2);
										Class181.smethod_3(Enum11.const_3, string.Format(SellOrderProcessor.getString_0(107456951), class3.Amount, class3.CurrencyId));
										if (d3 <= @class.order_0.player_item_amount && API.smethod_7(@class.order_0.player_item_name).Id == class3.CurrencyId)
										{
											((byte*)ptr)[16] = 1;
											break;
										}
									}
								}
								((byte*)ptr)[37] = (byte)(*(sbyte*)((byte*)ptr + 16));
								if (*(sbyte*)((byte*)ptr + 37) != 0)
								{
									break;
								}
							}
							Class181.smethod_3(Enum11.const_0, string.Format(SellOrderProcessor.getString_0(107456914), @class.order_0.my_item_name, *(int*)((byte*)ptr + 12), (int)@class.order_0.my_item_amount));
						}
						((byte*)ptr)[38] = ((*(sbyte*)((byte*)ptr + 16) == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 38) != 0)
						{
							((byte*)ptr)[39] = (Class255.class105_0.method_4(ConfigOptions.IgnoreScamTraders) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 39) != 0)
							{
								mainForm_0.method_56(@class.order_0.player, false);
							}
							Class181.smethod_3(Enum11.const_0, SellOrderProcessor.getString_0(107456845));
							string text5 = string.Format(SellOrderProcessor.getString_0(107456860), new object[]
							{
								@class.order_0.player.name,
								@class.order_0.my_item_amount,
								@class.order_0.my_item_name,
								@class.order_0.player_item_amount,
								@class.order_0.player_item_name
							});
							Class181.smethod_3(Enum11.const_0, text5);
							mainForm_0.list_7.Remove(@class.order_0);
							mainForm_0.method_65();
							Class307.smethod_0(ConfigOptions.OnScamAttempt, SellOrderProcessor.getString_0(107411263), text5);
							((byte*)ptr)[18] = 0;
						}
						else
						{
							mainForm_0.method_66();
							@class.order_0.timeSpan_0 = SellOrderProcessor.smethod_1(@class.order_0);
							@class.order_0.player.PartyTimeout = DateTime.Now + new TimeSpan(0, 0, Class255.class105_0.method_7(ConfigOptions.TimeBeforeSaleExpires));
							new Player();
							Player player = @class.order_0.player;
							@class.order_0.bool_2 = true;
							((byte*)ptr)[40] = (mainForm_0.list_12.Any(new Func<Player, bool>(@class.method_6)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 40) != 0)
							{
								Class181.smethod_3(Enum11.const_0, SellOrderProcessor.getString_0(107456267) + @class.order_0.player.name + SellOrderProcessor.getString_0(107456286));
								Class181.smethod_3(Enum11.const_0, string.Concat(new string[]
								{
									SellOrderProcessor.getString_0(107456261),
									@class.order_0.my_item_amount.ToString(),
									SellOrderProcessor.getString_0(107397369),
									@class.order_0.my_item_name,
									SellOrderProcessor.getString_0(107456248),
									@class.order_0.player_item_amount.ToString(),
									SellOrderProcessor.getString_0(107397369),
									@class.order_0.player_item_name,
									SellOrderProcessor.getString_0(107456207)
								}));
								mainForm_0.list_8.Add(@class.order_0);
							}
							else
							{
								((byte*)ptr)[41] = (mainForm_0.list_13.Any(new Func<Player, bool>(@class.method_7)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 41) != 0)
								{
									Class181.smethod_3(Enum11.const_3, SellOrderProcessor.getString_0(107456267) + @class.order_0.player.name + SellOrderProcessor.getString_0(107456198));
								}
								else
								{
									Thread.Sleep(new Random().Next(1, 4) * 1000);
									UI.smethod_1();
									Class181.smethod_3(Enum11.const_0, string.Concat(new string[]
									{
										SellOrderProcessor.getString_0(107456153),
										@class.order_0.player.name,
										SellOrderProcessor.getString_0(107456108),
										@class.order_0.my_item_amount.ToString(),
										SellOrderProcessor.getString_0(107397369),
										@class.order_0.my_item_name,
										SellOrderProcessor.getString_0(107456248),
										@class.order_0.player_item_amount.ToString(),
										SellOrderProcessor.getString_0(107397369),
										@class.order_0.player_item_name
									}));
									UI.smethod_36(Enum2.const_12, 1.0);
									UI.smethod_42(@class.order_0.player);
								}
							}
							((byte*)ptr)[18] = 1;
						}
					}
				}
			}
			IL_13E2:
			return *(sbyte*)((byte*)ptr + 18) != 0;
		}

		private unsafe static TimeSpan smethod_1(Order order_0)
		{
			void* ptr = stackalloc byte[24];
			*(int*)ptr = (int)Math.Ceiling(order_0.player_item_amount / API.smethod_6(order_0.player_item_name));
			*(int*)((byte*)ptr + 4) = Class120.dictionary_0[Enum2.const_29];
			*(int*)((byte*)ptr + 8) = (int)Math.Floor((double)(*(int*)ptr) / 15.0) * *(int*)((byte*)ptr + 4);
			*(int*)((byte*)ptr + 12) = (int)Math.Ceiling(order_0.my_item_amount / API.smethod_6(order_0.my_item_name));
			*(int*)((byte*)ptr + 16) = Class120.dictionary_0[Enum2.const_29];
			*(int*)((byte*)ptr + 20) = (int)Math.Floor((double)(*(int*)((byte*)ptr + 12)) / 15.0) * *(int*)((byte*)ptr + 16);
			return new TimeSpan(0, 0, 0, 0, Math.Max(*(int*)((byte*)ptr + 20), *(int*)((byte*)ptr + 8)));
		}

		private unsafe static void smethod_2(MainForm mainForm_0, Order order_0)
		{
			void* ptr = stackalloc byte[5];
			if (Class255.class105_0.method_4(ConfigOptions.SoldMessageEnabled) && Class255.SoldMessages.Any<string>())
			{
				((byte*)ptr)[4] = ((API.smethod_7(order_0.my_item_name).Type == SellOrderProcessor.getString_0(107394391)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) == 0 && !mainForm_0.list_19.Contains(order_0.player.name) && mainForm_0.list_18.Contains(order_0.my_item_name))
				{
					*(int*)ptr = new Random().Next(0, Class255.SoldMessages.Count);
					mainForm_0.list_19.Add(order_0.player.name);
					UI.smethod_1();
					Thread.Sleep(new Random().Next(1, 5) * 500);
					Win32.smethod_16(string.Format(SellOrderProcessor.getString_0(107398139), order_0.player, Class255.SoldMessages[*(int*)ptr]), true, true, false, false);
				}
			}
		}

		public unsafe static SellOrderProcessor.GClass0 smethod_3(Order order_0)
		{
			void* ptr = stackalloc byte[12];
			*(byte*)ptr = ((!Class255.class105_0.method_4(ConfigOptions.UseTradeSafety)) ? 1 : 0);
			SellOrderProcessor.GClass0 result;
			if (*(sbyte*)ptr != 0)
			{
				result = new SellOrderProcessor.GClass0(true, SellOrderProcessor.getString_0(107397332));
			}
			else
			{
				((byte*)ptr)[1] = (Class255.class105_0.method_4(ConfigOptions.DisableSameCurrency) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((byte*)ptr)[2] = ((order_0.my_item_name == order_0.player_item_name) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						return new SellOrderProcessor.GClass0(false, SellOrderProcessor.getString_0(107456127));
					}
				}
				((byte*)ptr)[3] = (Class255.class105_0.method_4(ConfigOptions.DisableSellingEngineer) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					((byte*)ptr)[4] = ((order_0.player_item_name == SellOrderProcessor.getString_0(107406786)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						return new SellOrderProcessor.GClass0(false, string.Format(SellOrderProcessor.getString_0(107456505), order_0.player_item_amount, order_0.player_item_name));
					}
				}
				((byte*)ptr)[5] = (Class255.class105_0.method_4(ConfigOptions.DisableSellingFacetors) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					((byte*)ptr)[6] = ((order_0.player_item_name == SellOrderProcessor.getString_0(107361333)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						return new SellOrderProcessor.GClass0(false, string.Format(SellOrderProcessor.getString_0(107456388), order_0.player_item_amount, order_0.player_item_name));
					}
				}
				((byte*)ptr)[7] = (Class255.class105_0.method_4(ConfigOptions.DisableSellingDecimalChaos) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0 && (order_0.player_item_name == SellOrderProcessor.getString_0(107391979) && order_0.player_item_amount != (int)order_0.player_item_amount && order_0.IsSingleItem))
				{
					result = new SellOrderProcessor.GClass0(false, string.Format(SellOrderProcessor.getString_0(107455727), order_0.player_item_amount, order_0.player_item_name));
				}
				else
				{
					((byte*)ptr)[8] = (Class255.class105_0.method_4(ConfigOptions.DisableSellingCheapExalted) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0 && (order_0.my_item_name == SellOrderProcessor.getString_0(107383774) && order_0.player_item_name == SellOrderProcessor.getString_0(107391979)))
					{
						decimal num = Class255.class105_0.method_6(ConfigOptions.CheapExaltedValue);
						((byte*)ptr)[9] = ((order_0.player_item_amount / order_0.my_item_amount < num) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							return new SellOrderProcessor.GClass0(false, string.Format(SellOrderProcessor.getString_0(107455597), Math.Round(order_0.player_item_amount / order_0.my_item_amount), num));
						}
					}
					((byte*)ptr)[10] = (Class255.class105_0.method_4(ConfigOptions.DisableSellingCheapChaos) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0 && (Class255.CheapChaosList.Contains(order_0.player_item_name) && order_0.my_item_name == SellOrderProcessor.getString_0(107391979)))
					{
						decimal num2 = order_0.player_item_amount / order_0.my_item_amount;
						((byte*)ptr)[11] = ((num2 < 1m) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							return new SellOrderProcessor.GClass0(false, string.Format(SellOrderProcessor.getString_0(107455971), order_0.player_item_name, Math.Round(1m / num2, 2)));
						}
					}
					result = new SellOrderProcessor.GClass0(true, SellOrderProcessor.getString_0(107397332));
				}
			}
			return result;
		}

		static SellOrderProcessor()
		{
			Strings.CreateGetStringDelegate(typeof(SellOrderProcessor));
		}

		[NonSerialized]
		internal static GetString getString_0;

		public sealed class GClass0
		{
			public GClass0(bool bool_1, string string_1)
			{
				this.bool_0 = bool_1;
				this.string_0 = string_1;
			}

			public bool bool_0;

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class169
		{
			internal bool method_0(FlippingListItem flippingListItem_0)
			{
				return (flippingListItem_0.Enabled && flippingListItem_0.HaveName == this.order_0.my_item_name && flippingListItem_0.WantName == this.order_0.player_item_name) || (flippingListItem_0.HaveName == this.order_0.player_item_name && flippingListItem_0.WantName == this.order_0.my_item_name);
			}

			internal bool method_1(Order order_1)
			{
				return order_1.bool_2 && order_1.my_item_name == this.order_0.my_item_name;
			}

			internal bool method_2(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine == this.order_0.my_item_name;
			}

			internal bool method_3(Order order_1)
			{
				return order_1.bool_2 && order_1.player_item_name == this.order_0.player_item_name;
			}

			internal bool method_4(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine == this.order_0.player_item_name;
			}

			internal bool method_5(Order order_1)
			{
				return order_1.bool_2 && order_1.my_item_name == this.order_0.my_item_name;
			}

			internal bool method_6(Player player_0)
			{
				return player_0.name == this.order_0.player.name;
			}

			internal bool method_7(Player player_0)
			{
				return player_0.name == this.order_0.player.name;
			}

			public Order order_0;
		}
	}
}
