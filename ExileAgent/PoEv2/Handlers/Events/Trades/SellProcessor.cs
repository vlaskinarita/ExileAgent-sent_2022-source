using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using ns0;
using ns12;
using ns14;
using ns29;
using ns31;
using ns32;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers.Events.Trades
{
	public sealed class SellProcessor : TradeProcessor
	{
		public SellProcessor(MainForm form)
		{
			TradeProcessor.mainForm_0 = form;
		}

		public unsafe void method_0()
		{
			void* ptr = stackalloc byte[36];
			TradeProcessor.TradeStartTime = DateTime.Now;
			TradeProcessor.TradeExpireTime = DateTime.Now + new TimeSpan(0, 0, Class255.class105_0.method_7(ConfigOptions.MaxTradeTime));
			((byte*)ptr)[4] = 0;
			((byte*)ptr)[5] = 0;
			((byte*)ptr)[6] = 1;
			DateTime now = DateTime.Now;
			for (;;)
			{
				((byte*)ptr)[35] = ((TradeProcessor.TradeExpireTime > DateTime.Now) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 35) == 0)
				{
					break;
				}
				if (TradeProcessor.mainForm_0.list_12.Count(new Func<Player, bool>(SellProcessor.<>c.<>9.method_0)) == 0)
				{
					goto IL_801;
				}
				TimeSpan timeSpan = DateTime.Now - now;
				((byte*)ptr)[10] = ((timeSpan.TotalSeconds > 1.0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107462282), TradeProcessor.TotalSecondsRemaining, TradeProcessor.order_1.player.name));
					now = DateTime.Now;
				}
				((byte*)ptr)[7] = 0;
				((byte*)ptr)[8] = 0;
				((byte*)ptr)[9] = 0;
				((byte*)ptr)[11] = ((*(sbyte*)((byte*)ptr + 6) == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					((byte*)ptr)[7] = (UI.smethod_70() ? 1 : 0);
					((byte*)ptr)[8] = (UI.smethod_71() ? 1 : 0);
					((byte*)ptr)[9] = ((!UI.smethod_50(SellProcessor.getString_1(107462709))) ? 1 : 0);
				}
				else
				{
					((byte*)ptr)[12] = (SellProcessor.smethod_14() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						Thread.Sleep(400);
						((byte*)ptr)[6] = 0;
						this.method_3();
						continue;
					}
				}
				if (*(sbyte*)((byte*)ptr + 7) == 0 && *(sbyte*)((byte*)ptr + 8) == 0)
				{
					((byte*)ptr)[5] = 0;
					((byte*)ptr)[4] = 0;
					this.int_1++;
					((byte*)ptr)[13] = ((this.int_1 > Class255.class105_0.method_7(ConfigOptions.MaxTradeAttempts)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						goto IL_82F;
					}
					if (TradeProcessor.mainForm_0.list_12.Count(new Func<Player, bool>(SellProcessor.<>c.<>9.method_1)) == 0)
					{
						goto IL_892;
					}
					UI.smethod_13(1);
					((byte*)ptr)[15] = ((*(sbyte*)((byte*)ptr + 6) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						UI.smethod_36(Enum2.const_11, 1.0);
					}
					this.method_3();
					((byte*)ptr)[16] = (SellProcessor.smethod_14() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						Thread.Sleep(400);
						((byte*)ptr)[6] = 0;
						continue;
					}
					Position position;
					((byte*)ptr)[17] = (UI.smethod_3(out position, Images.KeepDestroyWindow, SellProcessor.getString_1(107461544)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) != 0)
					{
						Win32.smethod_14(SellProcessor.getString_1(107394311), false);
						UI.smethod_36(Enum2.const_34, 1.0);
						UI.smethod_43();
					}
					Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107461889), new object[]
					{
						this.int_1,
						TradeProcessor.order_1.player.name
					});
					Win32.smethod_16(SellProcessor.getString_1(107461551) + TradeProcessor.order_1.player.name, true, true, false, false);
					UI.smethod_32(0, -1, Enum2.const_3, true);
					Thread.Sleep(1000);
					((byte*)ptr)[18] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.ItemOnCursor) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						Class181.smethod_3(Enum11.const_2, SellProcessor.getString_1(107461502));
						UI.smethod_43();
						this.int_1--;
						continue;
					}
					((byte*)ptr)[19] = (byte)(*(sbyte*)((byte*)ptr + 6));
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						((byte*)ptr)[6] = 0;
						continue;
					}
				}
				if (*(sbyte*)((byte*)ptr + 8) != 0 && *(sbyte*)((byte*)ptr + 4) == 0)
				{
					Win32.smethod_4((int)Math.Round(Class251.InventoryOffset.X - 5.0), (int)Math.Round(Class251.InventoryOffset.Y - 5.0), 50, 90, false);
					using (Bitmap bitmap = UI.smethod_67())
					{
						TradeProcessor.TradeExpireTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_10]) + TradeProcessor.order_1.timeSpan_0;
						((byte*)ptr)[20] = (TradeProcessor.smethod_5(bitmap, this.list_3, *(sbyte*)((byte*)ptr + 8) != 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 20) != 0)
						{
							((byte*)ptr)[4] = 1;
						}
					}
				}
				((byte*)ptr)[21] = (byte)(((*(sbyte*)((byte*)ptr + 9) == 0) ? 1 : 0) & *(sbyte*)((byte*)ptr + 8));
				if (*(sbyte*)((byte*)ptr + 21) != 0)
				{
					((byte*)ptr)[5] = 1;
					MatchTradeContentResults matchTradeContentResults = this.method_2();
					((byte*)ptr)[9] = ((!UI.smethod_50(SellProcessor.getString_1(107461872))) ? 1 : 0);
					Class181.smethod_3(Enum11.const_3, string.Format(SellProcessor.getString_1(107461441), matchTradeContentResults.ToString()));
				}
				((byte*)ptr)[22] = (byte)(*(sbyte*)((byte*)ptr + 8) & *(sbyte*)((byte*)ptr + 4) & *(sbyte*)((byte*)ptr + 9) & *(sbyte*)((byte*)ptr + 5));
				if (*(sbyte*)((byte*)ptr + 22) != 0)
				{
					*(int*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[33] = ((TradeProcessor.TradeExpireTime > DateTime.Now) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 33) == 0)
						{
							break;
						}
						Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107461827) + TradeProcessor.order_1.tradeStates_0.ToString());
						timeSpan = DateTime.Now - now;
						((byte*)ptr)[23] = ((timeSpan.TotalSeconds > 1.0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 23) != 0)
						{
							Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107461838), TradeProcessor.TotalSecondsRemaining));
							now = DateTime.Now;
						}
						((byte*)ptr)[24] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Complete) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 24) != 0)
						{
							goto IL_711;
						}
						((byte*)ptr)[25] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Cancelled) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 25) != 0)
						{
							goto IL_73C;
						}
						((byte*)ptr)[26] = (UI.smethod_71() ? 1 : 0);
						((byte*)ptr)[27] = ((*(sbyte*)((byte*)ptr + 26) == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 27) == 0)
						{
							((byte*)ptr)[28] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Pending) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 28) != 0)
							{
								this.method_2();
							}
							else
							{
								((byte*)ptr)[29] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.ClickedAccept) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 29) != 0)
								{
									if (UI.smethod_50(SellProcessor.getString_1(107461872)) || !UI.smethod_73())
									{
										Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107461452), TradeProcessor.order_1.player.name));
										this.method_2();
										((byte*)ptr)[30] = ((*(int*)ptr == 0) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 30) != 0)
										{
											TradeProcessor.TradeExpireTime += new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_28]);
										}
										*(int*)ptr = *(int*)ptr + 1;
									}
								}
								else
								{
									((byte*)ptr)[31] = (UI.smethod_50(SellProcessor.getString_1(107461872)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 31) != 0)
									{
										Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107461452), TradeProcessor.order_1.player.name));
										this.method_2();
										*(int*)ptr = *(int*)ptr + 1;
									}
								}
							}
							((byte*)ptr)[32] = ((TradeProcessor.TradeExpireTime < DateTime.Now) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 32) != 0)
							{
								goto IL_772;
							}
							Thread.Sleep(100);
						}
					}
					IL_7A5:
					if (!(TradeProcessor.TradeExpireTime < DateTime.Now) && !TradeProcessor.TradeCompleted)
					{
						goto IL_7C3;
					}
					break;
					IL_772:
					Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107462054), TradeProcessor.TotalSeconds, TradeProcessor.order_1.player.name));
					goto IL_7A5;
					IL_73C:
					Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107462184), TradeProcessor.order_1.player.name));
					TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
					goto IL_7A5;
					IL_711:
					Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107461725), TradeProcessor.order_1.player.name));
					goto IL_7A5;
				}
				IL_7C3:
				((byte*)ptr)[34] = (TradeProcessor.TradeCompleted ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 34) != 0)
				{
					break;
				}
				Thread.Sleep(100);
			}
			return;
			IL_801:
			Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107461637), TradeProcessor.order_1.player.name));
			return;
			IL_82F:
			((byte*)ptr)[14] = (Class255.class105_0.method_4(ConfigOptions.IgnoreFailedTraders) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 14) != 0)
			{
				TradeProcessor.mainForm_0.method_56(TradeProcessor.order_1.player, false);
			}
			Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107462660), this.int_1, TradeProcessor.order_1.player.name));
			return;
			IL_892:
			Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107461637), TradeProcessor.order_1.player.name));
		}

		private unsafe Tuple<MatchTradeContentResults, int> method_1(decimal decimal_0, string string_0)
		{
			void* ptr = stackalloc byte[12];
			*(byte*)ptr = ((!UI.smethod_71()) ? 1 : 0);
			Tuple<MatchTradeContentResults, int> result;
			if (*(sbyte*)ptr != 0)
			{
				result = Tuple.Create<MatchTradeContentResults, int>(MatchTradeContentResults.InvalidPrice, 0);
			}
			else
			{
				this.list_1 = UI.smethod_66();
				((byte*)ptr)[1] = ((this.list_1 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = Tuple.Create<MatchTradeContentResults, int>(MatchTradeContentResults.InvalidPrice, 0);
				}
				else
				{
					new Dictionary<string, double>();
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					foreach (TradeWindowItem tradeWindowItem in this.list_1.Where(new Func<TradeWindowItem, bool>(SellProcessor.<>c.<>9.method_2)))
					{
						((byte*)ptr)[2] = ((!dictionary.ContainsKey(tradeWindowItem.Item.Name)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							dictionary.Add(tradeWindowItem.Item.Name, 0);
						}
						Dictionary<string, int> dictionary2 = dictionary;
						string name = tradeWindowItem.Item.Name;
						dictionary2[name] += tradeWindowItem.Item.stack;
					}
					foreach (KeyValuePair<string, int> keyValuePair in dictionary)
					{
						Class181.smethod_2(Enum11.const_3, SellProcessor.getString_1(107460831), new object[]
						{
							keyValuePair.Value,
							keyValuePair.Key
						});
					}
					Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107462005), new object[]
					{
						decimal_0,
						string_0
					});
					foreach (KeyValuePair<string, int> keyValuePair2 in dictionary)
					{
						((byte*)ptr)[3] = ((keyValuePair2.Key != string_0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							((byte*)ptr)[4] = (Class255.class105_0.method_4(ConfigOptions.AcceptOtherCurrency) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								((byte*)ptr)[5] = (Class255.AcceptedCurrencyList.Contains(keyValuePair2.Key) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 5) != 0)
								{
									string key = API.smethod_4(keyValuePair2.Key);
									decimal num = 0m;
									((byte*)ptr)[6] = (this.dictionary_0.ContainsKey(key) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 6) != 0)
									{
										num = this.dictionary_0[key];
									}
									else if (Class255.class105_0.method_4(ConfigOptions.UseCustomExaltPrice) && ((keyValuePair2.Key == SellProcessor.getString_1(107391964) && string_0 == SellProcessor.getString_1(107383759)) || (keyValuePair2.Key == SellProcessor.getString_1(107383759) && string_0 == SellProcessor.getString_1(107391964))))
									{
										decimal num2 = Class255.class105_0.method_6(ConfigOptions.ExaltedOrbValue);
										((byte*)ptr)[7] = ((keyValuePair2.Key == SellProcessor.getString_1(107391964)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 7) != 0)
										{
											num = 1m / num2;
										}
										else
										{
											num = num2;
										}
										Class181.smethod_2(Enum11.const_3, SellProcessor.getString_1(107460846), new object[]
										{
											num2
										});
									}
									else
									{
										Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107460741), new object[]
										{
											keyValuePair2.Key,
											string_0
										});
										((byte*)ptr)[8] = ((keyValuePair2.Key != SellProcessor.getString_1(107391964)) ? 1 : 0);
										Dictionary<string, decimal> dictionary3;
										if (*(sbyte*)((byte*)ptr + 8) != 0)
										{
											dictionary3 = Pricing.smethod_5(API.smethod_4(keyValuePair2.Key), API.smethod_4(SellProcessor.getString_1(107391964)), 1, true);
										}
										else
										{
											dictionary3 = new Dictionary<string, decimal>
											{
												{
													SellProcessor.getString_1(107362636),
													1m
												}
											};
										}
										((byte*)ptr)[9] = ((string_0 != SellProcessor.getString_1(107391964)) ? 1 : 0);
										Dictionary<string, decimal> dictionary4;
										if (*(sbyte*)((byte*)ptr + 9) != 0)
										{
											dictionary4 = Pricing.smethod_5(API.smethod_4(string_0), API.smethod_4(SellProcessor.getString_1(107391964)), 1, true);
										}
										else
										{
											dictionary4 = new Dictionary<string, decimal>
											{
												{
													SellProcessor.getString_1(107362636),
													1m
												}
											};
										}
										if (!dictionary3.ContainsKey(SellProcessor.getString_1(107362636)) || !dictionary4.ContainsKey(SellProcessor.getString_1(107362636)))
										{
											Class181.smethod_3(Enum11.const_2, SellProcessor.getString_1(107460687) + keyValuePair2.Key);
											continue;
										}
										num = dictionary3.First<KeyValuePair<string, decimal>>().Value / dictionary4.First<KeyValuePair<string, decimal>>().Value;
										Class181.smethod_2(Enum11.const_3, SellProcessor.getString_1(107460700), new object[]
										{
											keyValuePair2.Key,
											num,
											string_0
										});
										this.dictionary_0.Add(key, num);
									}
									decimal_0 -= Math.Round(keyValuePair2.Value * num * (1m + Class255.class105_0.method_6(ConfigOptions.UnexpectedPercentageDiscount) / 100m), 2);
									Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107461162), new object[]
									{
										Math.Round(keyValuePair2.Value * num, 2),
										string_0
									});
								}
								else
								{
									Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107461105), new object[]
									{
										keyValuePair2.Key
									});
								}
							}
							else
							{
								Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107460971), new object[]
								{
									keyValuePair2.Key
								});
							}
						}
						else
						{
							decimal_0 -= keyValuePair2.Value;
							Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107460329), new object[]
							{
								keyValuePair2.Value,
								keyValuePair2.Key
							});
						}
						((byte*)ptr)[10] = ((decimal_0 == 0m) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							return Tuple.Create<MatchTradeContentResults, int>(MatchTradeContentResults.ValidPrice, 0);
						}
						((byte*)ptr)[11] = ((decimal_0 < 0m) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							return Tuple.Create<MatchTradeContentResults, int>(MatchTradeContentResults.ExpectingMoreItems, (int)Math.Abs(decimal_0));
						}
						Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107460252), new object[]
						{
							decimal_0,
							string_0
						});
					}
					result = Tuple.Create<MatchTradeContentResults, int>(MatchTradeContentResults.InvalidPrice, (int)decimal_0);
				}
			}
			return result;
		}

		private unsafe MatchTradeContentResults method_2()
		{
			void* ptr = stackalloc byte[13];
			Tuple<MatchTradeContentResults, int> tuple = this.method_1(TradeProcessor.order_1.player_item_amount, TradeProcessor.order_1.player_item_name);
			switch (tuple.Item1)
			{
			case MatchTradeContentResults.InvalidPrice:
				Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107460204));
				((byte*)ptr)[12] = ((tuple.Item2 == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) == 0)
				{
					*(int*)((byte*)ptr + 4) = (int)Math.Ceiling(TradeProcessor.order_1.player_item_amount / API.smethod_6(TradeProcessor.order_1.player_item_name));
					if (*(int*)((byte*)ptr + 4) <= UI.list_0.smethod_17().Count<TradeWindowItem>() && this.int_2 < 2)
					{
						if (UI.list_0.smethod_17().Any(new Func<TradeWindowItem, bool>(SellProcessor.<>c.<>9.method_3)))
						{
							Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107461252));
							this.int_2++;
							UI.smethod_65();
							return this.method_2();
						}
					}
				}
				break;
			case MatchTradeContentResults.ValidPrice:
				Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107460243));
				TradeProcessor.smethod_3();
				break;
			case MatchTradeContentResults.ExpectingMoreItems:
				Class181.smethod_2(Enum11.const_3, SellProcessor.getString_1(107460234), new object[]
				{
					tuple.Item2
				});
				((byte*)ptr)[8] = ((this.int_1 > 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107460213));
					tuple = Tuple.Create<MatchTradeContentResults, int>(MatchTradeContentResults.ValidPrice, 0);
					TradeProcessor.smethod_3();
				}
				else
				{
					((byte*)ptr)[9] = ((this.list_2.Count == this.list_1.Count) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						this.int_3++;
					}
					this.list_2 = new List<TradeWindowItem>(this.list_1);
					((byte*)ptr)[10] = ((this.int_3 < 2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						return this.method_2();
					}
					*(int*)ptr = (int)Math.Floor(tuple.Item2 / TradeProcessor.order_1.player_item_amount);
					((byte*)ptr)[11] = ((!SellProcessor.smethod_12(TradeProcessor.order_1, *(int*)ptr, true)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						TradeProcessor.smethod_3();
					}
				}
				break;
			}
			if (tuple.Item1 != MatchTradeContentResults.ValidPrice && UI.smethod_71() && TradeProcessor.order_1.tradeStates_0 != Order.TradeStates.Cancelled)
			{
				TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
			}
			return tuple.Item1;
		}

		public unsafe static bool smethod_12(Order order_2, int int_4, bool bool_1)
		{
			void* ptr = stackalloc byte[13];
			((byte*)ptr)[4] = ((!Class148.smethod_0(order_2)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107460163));
				((byte*)ptr)[5] = 0;
			}
			else
			{
				((byte*)ptr)[6] = ((int_4 == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107460154));
					((byte*)ptr)[5] = 0;
				}
				else
				{
					decimal my_item_amount = order_2.my_item_amount;
					((byte*)ptr)[7] = (((my_item_amount + int_4) / 60m / API.smethod_6(order_2.my_item_name) > 1m) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						Class181.smethod_3(Enum11.const_3, SellProcessor.getString_1(107460204));
						((byte*)ptr)[5] = 0;
					}
					else
					{
						if (order_2.left_pos != 0 && order_2.top_pos != 0)
						{
							((byte*)ptr)[8] = (order_2.my_items.Any<KeyValuePair<JsonTab, List<JsonItem>>>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) != 0)
							{
								order_2.my_items.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>().x = order_2.left_pos - 1;
								order_2.my_items.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>().y = order_2.top_pos - 1;
							}
							order_2.left_pos = 0;
							order_2.top_pos = 0;
						}
						Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107460177), new object[]
						{
							order_2.player.name,
							int_4,
							order_2.my_item_name
						});
						Dictionary<JsonTab, List<JsonItem>> dictionary;
						InventoryManager.smethod_1(0, order_2, order_2.OriginalNote, TradeProcessor.mainForm_0.list_7, out dictionary, out *(int*)ptr, false);
						((byte*)ptr)[9] = ((dictionary.smethod_16(false) < *(int*)ptr + int_4) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							Class181.smethod_3(Enum11.const_0, string.Format(SellProcessor.getString_1(107460576), new object[]
							{
								order_2.my_item_name,
								dictionary.smethod_16(false),
								*(int*)ptr,
								int_4
							}));
							order_2.my_item_amount = my_item_amount;
							((byte*)ptr)[5] = 0;
						}
						else
						{
							Class181.smethod_3(Enum11.const_3, string.Format(SellProcessor.getString_1(107460475), order_2.my_item_name, dictionary.smethod_16(false), *(int*)ptr));
							order_2.my_item_amount = int_4;
							((byte*)ptr)[10] = (order_2.my_items.Any<KeyValuePair<JsonTab, List<JsonItem>>>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 10) != 0)
							{
								Dictionary<JsonTab, List<JsonItem>> dictionary2 = new Dictionary<JsonTab, List<JsonItem>>(order_2.my_items);
								order_2.my_items.Clear();
								((byte*)ptr)[11] = (bool_1 ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 11) != 0)
								{
									SellProcessor.smethod_13(order_2);
								}
								foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair in order_2.my_items)
								{
									((byte*)ptr)[12] = ((!dictionary2.ContainsKey(keyValuePair.Key)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 12) != 0)
									{
										dictionary2.Add(keyValuePair.Key, new List<JsonItem>());
									}
									foreach (JsonItem item in keyValuePair.Value)
									{
										dictionary2[keyValuePair.Key].Add(item);
									}
								}
								order_2.my_items = dictionary2;
								List<JsonItem> list = new List<JsonItem>();
								foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair2 in order_2.my_items)
								{
									list.AddRange(keyValuePair2.Value);
								}
							}
							Class181.smethod_2(Enum11.const_0, SellProcessor.getString_1(107460438), new object[]
							{
								int_4,
								order_2.my_item_name
							});
							order_2.my_item_amount = int_4 + my_item_amount;
							order_2.player_item_amount = order_2.player_item_amount / my_item_amount * order_2.my_item_amount;
							TradeProcessor.mainForm_0.method_85(order_2);
							((byte*)ptr)[5] = 1;
						}
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		private static void smethod_13(Order order_2)
		{
			Win32.smethod_14(SellProcessor.getString_1(107394311), false);
			UI.smethod_36(Enum2.const_22, 1.0);
			TradeProcessor.Inventory = InventoryManager.smethod_3(order_2, order_2.OriginalNote, false, TradeProcessor.Inventory);
		}

		private unsafe static bool smethod_14()
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((!UI.smethod_72()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = ((TradeProcessor.mainForm_0.list_12.Count == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Position position = UI.smethod_106();
					((byte*)ptr)[3] = (position.IsVisible ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						UI.smethod_22();
						((byte*)ptr)[1] = 1;
						goto IL_63;
					}
				}
				UI.smethod_18();
				((byte*)ptr)[1] = 0;
			}
			IL_63:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private void method_3()
		{
			TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
			UI.smethod_65();
			this.int_2 = 0;
			this.list_3 = TradeProcessor.Inventory.ToList<JsonItem>();
		}

		static SellProcessor()
		{
			Strings.CreateGetStringDelegate(typeof(SellProcessor));
		}

		private int int_1 = 0;

		private int int_2 = 0;

		private int int_3 = 0;

		private List<TradeWindowItem> list_1 = new List<TradeWindowItem>();

		private List<TradeWindowItem> list_2 = new List<TradeWindowItem>();

		private Dictionary<string, decimal> dictionary_0 = new Dictionary<string, decimal>();

		private List<JsonItem> list_3 = null;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
