using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using ns0;
using ns12;
using ns14;
using ns23;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers.Events.Trades
{
	public sealed class BuyProcessor : TradeProcessor
	{
		public BuyProcessor(MainForm form)
		{
			TradeProcessor.mainForm_0 = form;
		}

		public unsafe void method_0()
		{
			void* ptr = stackalloc byte[39];
			TradeProcessor.TradeStartTime = DateTime.Now;
			TradeProcessor.TradeExpireTime = DateTime.Now + new TimeSpan(0, 0, Class255.class105_0.method_7(ConfigOptions.BuyMaxTradeTime));
			((byte*)ptr)[8] = 0;
			((byte*)ptr)[9] = 0;
			((byte*)ptr)[10] = 0;
			((byte*)ptr)[11] = 0;
			List<JsonItem> list_ = null;
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			DateTime dateTime = DateTime.Now;
			Class181.smethod_2(Enum11.const_3, BuyProcessor.getString_1(107462288), new object[]
			{
				TradeProcessor.order_1.player_item_amount
			});
			using (List<string>.Enumerator enumerator = TradeProcessor.order_1.BuyItem64.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string string_ = enumerator.Current;
					Class181.smethod_3(Enum11.const_3, string_);
				}
				goto IL_723;
			}
			IL_CE:
			((byte*)ptr)[13] = ((*(sbyte*)((byte*)ptr + 10) == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 13) != 0)
			{
				UI.smethod_65();
				list_ = TradeProcessor.Inventory.ToList<JsonItem>();
				((byte*)ptr)[8] = 0;
				((byte*)ptr)[9] = 0;
			}
			((byte*)ptr)[14] = (TradeProcessor.mainForm_0.list_3.Contains(TradeProcessor.order_1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 14) != 0)
			{
				TradeProcessor.mainForm_0.list_3.Remove(TradeProcessor.order_1);
				return;
			}
			TimeSpan timeSpan = DateTime.Now - now;
			((byte*)ptr)[15] = ((timeSpan.TotalSeconds > 1.0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 15) != 0)
			{
				Class181.smethod_3(Enum11.const_0, string.Format(BuyProcessor.getString_1(107462255), TradeProcessor.TotalSecondsRemaining, TradeProcessor.order_1.player.name));
				now = DateTime.Now;
			}
			((byte*)ptr)[10] = (UI.smethod_71() ? 1 : 0);
			((byte*)ptr)[12] = ((!UI.smethod_50(BuyProcessor.getString_1(107462682))) ? 1 : 0);
			((byte*)ptr)[16] = ((*(sbyte*)((byte*)ptr + 10) == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				((byte*)ptr)[18] = ((this.int_2 >= Class255.class105_0.method_6(ConfigOptions.MaxBuyTradeAttempts)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					((byte*)ptr)[19] = (Class255.class105_0.method_4(ConfigOptions.IgnoreFailedTraders) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						TradeProcessor.mainForm_0.method_56(TradeProcessor.order_1.player, false);
					}
					Class181.smethod_2(Enum11.const_0, BuyProcessor.getString_1(107462633), new object[]
					{
						this.int_2,
						TradeProcessor.order_1.player.name
					});
					return;
				}
				((byte*)ptr)[17] = (UI.smethod_22() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) == 0 && !UI.smethod_79())
				{
					((byte*)ptr)[20] = ((*(sbyte*)((byte*)ptr + 11) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) != 0)
					{
						((byte*)ptr)[11] = 1;
						DateTime now3 = DateTime.Now;
						dateTime = DateTime.Now.AddSeconds((double)Class255.class105_0.method_6(ConfigOptions.TimeBeforePartyClosed));
					}
					*(int*)ptr = dateTime.Subtract(DateTime.Now).Seconds;
					((byte*)ptr)[21] = ((*(int*)ptr >= 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 21) == 0)
					{
						Class181.smethod_3(Enum11.const_0, BuyProcessor.getString_1(107462479));
						return;
					}
					timeSpan = DateTime.Now - now;
					((byte*)ptr)[22] = ((timeSpan.TotalSeconds > 1.0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 22) != 0)
					{
						Class181.smethod_2(Enum11.const_0, BuyProcessor.getString_1(107462584), new object[]
						{
							*(int*)ptr
						});
						now = DateTime.Now;
						goto IL_723;
					}
					goto IL_723;
				}
				else
				{
					((byte*)ptr)[23] = (byte)(*(sbyte*)((byte*)ptr + 17));
					if (*(sbyte*)((byte*)ptr + 23) != 0)
					{
						this.int_2++;
						Class181.smethod_2(Enum11.const_0, BuyProcessor.getString_1(107461862), new object[]
						{
							this.int_2,
							TradeProcessor.order_1.player.name
						});
					}
				}
			}
			if (*(sbyte*)((byte*)ptr + 10) != 0 && *(sbyte*)((byte*)ptr + 8) == 0)
			{
				using (Bitmap bitmap = UI.smethod_67())
				{
					TradeProcessor.TradeExpireTime = DateTime.Now + new TimeSpan(0, 0, (int)Class255.class105_0.method_6(ConfigOptions.BuyMaxTimeAcceptTrade)) + TradeProcessor.order_1.timeSpan_0;
					((byte*)ptr)[24] = (TradeProcessor.smethod_5(bitmap, list_, *(sbyte*)((byte*)ptr + 10) != 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 24) != 0)
					{
						((byte*)ptr)[8] = 1;
					}
				}
			}
			((byte*)ptr)[25] = (byte)(((*(sbyte*)((byte*)ptr + 12) == 0) ? 1 : 0) & *(sbyte*)((byte*)ptr + 10));
			if (*(sbyte*)((byte*)ptr + 25) != 0)
			{
				TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
				this.method_2();
				((byte*)ptr)[12] = ((!UI.smethod_50(BuyProcessor.getString_1(107461845))) ? 1 : 0);
				((byte*)ptr)[9] = 1;
			}
			((byte*)ptr)[26] = (byte)(*(sbyte*)((byte*)ptr + 10) & *(sbyte*)((byte*)ptr + 8) & *(sbyte*)((byte*)ptr + 12) & *(sbyte*)((byte*)ptr + 9));
			if (*(sbyte*)((byte*)ptr + 26) != 0)
			{
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[36] = ((TradeProcessor.TradeExpireTime > DateTime.Now) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 36) == 0)
					{
						break;
					}
					Class181.smethod_3(Enum11.const_3, BuyProcessor.getString_1(107461800) + TradeProcessor.order_1.tradeStates_0.ToString());
					timeSpan = DateTime.Now - now;
					((byte*)ptr)[27] = ((timeSpan.TotalSeconds > 1.0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 27) != 0)
					{
						Class181.smethod_3(Enum11.const_0, string.Format(BuyProcessor.getString_1(107461811), TradeProcessor.TotalSecondsRemaining));
						now = DateTime.Now;
					}
					((byte*)ptr)[28] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Complete) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 28) != 0)
					{
						goto IL_656;
					}
					((byte*)ptr)[29] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Cancelled) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 29) != 0)
					{
						goto IL_681;
					}
					((byte*)ptr)[30] = ((!UI.smethod_71()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 30) == 0)
					{
						((byte*)ptr)[31] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Pending) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 31) != 0)
						{
							this.method_2();
						}
						else
						{
							((byte*)ptr)[32] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.ClickedAccept) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 32) != 0)
							{
								if (UI.smethod_50(BuyProcessor.getString_1(107461845)) || !UI.smethod_73())
								{
									Class181.smethod_3(Enum11.const_0, BuyProcessor.getString_1(107462132));
									this.method_2();
									((byte*)ptr)[33] = ((*(int*)((byte*)ptr + 4) == 0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 33) != 0)
									{
										TradeProcessor.TradeExpireTime += new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_28]);
									}
									*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
								}
							}
							else
							{
								((byte*)ptr)[34] = (UI.smethod_50(BuyProcessor.getString_1(107461845)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 34) != 0)
								{
									Class181.smethod_3(Enum11.const_0, BuyProcessor.getString_1(107462132));
									this.method_2();
									*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
								}
							}
						}
						((byte*)ptr)[35] = ((TradeProcessor.TradeExpireTime < DateTime.Now) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 35) != 0)
						{
							goto IL_6B7;
						}
						Thread.Sleep(100);
					}
				}
				goto IL_6EA;
				IL_656:
				Class181.smethod_3(Enum11.const_0, string.Format(BuyProcessor.getString_1(107461698), TradeProcessor.order_1.player.name));
				goto IL_6EA;
				IL_681:
				Class181.smethod_3(Enum11.const_0, string.Format(BuyProcessor.getString_1(107462157), TradeProcessor.order_1.player.name));
				TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
				goto IL_6EA;
				IL_6B7:
				Class181.smethod_3(Enum11.const_0, string.Format(BuyProcessor.getString_1(107462027), TradeProcessor.TotalSeconds, TradeProcessor.order_1.player.name));
				IL_6EA:
				if (TradeProcessor.TradeExpireTime < DateTime.Now || TradeProcessor.TradeCompleted)
				{
					return;
				}
			}
			((byte*)ptr)[37] = (TradeProcessor.TradeCompleted ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 37) != 0)
			{
				return;
			}
			Thread.Sleep(100);
			IL_723:
			((byte*)ptr)[38] = ((TradeProcessor.TradeExpireTime > DateTime.Now) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 38) != 0)
			{
				goto IL_CE;
			}
		}

		private unsafe MatchTradeContentResults method_1(decimal decimal_0, string string_0)
		{
			void* ptr = stackalloc byte[16];
			((byte*)ptr)[4] = ((!UI.smethod_71()) ? 1 : 0);
			MatchTradeContentResults result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = MatchTradeContentResults.InvalidPrice;
			}
			else
			{
				List<TradeWindowItem> list = UI.smethod_66();
				((byte*)ptr)[5] = ((list == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					result = MatchTradeContentResults.InvalidPrice;
				}
				else
				{
					Class181.smethod_2(Enum11.const_0, BuyProcessor.getString_1(107461978), new object[]
					{
						decimal_0,
						string_0
					});
					*(int*)ptr = 0;
					List<string> list2 = new List<string>();
					foreach (string item in TradeProcessor.order_1.BuyItem64)
					{
						((byte*)ptr)[6] = (list2.Contains(item) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) == 0)
						{
							list2.Add(item);
						}
					}
					foreach (TradeWindowItem tradeWindowItem in list.Where(new Func<TradeWindowItem, bool>(BuyProcessor.<>c.<>9.method_0)))
					{
						Item item2 = tradeWindowItem.Item;
						if (item2.HasStack || item2.IsIncubator)
						{
							Item item3 = new Item
							{
								text = TradeProcessor.order_1.BuyItem64.First<string>()
							};
							item3.method_0();
							((byte*)ptr)[7] = ((item3.Name == item2.Name) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								*(int*)ptr = *(int*)ptr + item2.stack;
							}
						}
						else
						{
							foreach (string text in list2.ToList<string>())
							{
								string a = this.method_3(item2.text);
								ApiItem apiItem = API.smethod_7(item2.typeLine);
								Item item4 = new Item
								{
									text = text
								};
								item4.method_0();
								((byte*)ptr)[8] = ((apiItem.Type != BuyProcessor.getString_1(107371557)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 8) != 0)
								{
									((byte*)ptr)[9] = (apiItem.IsMap ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 9) != 0)
									{
										((byte*)ptr)[10] = ((API.smethod_2(item2.typeLine) != API.smethod_2(item4.typeLine)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 10) != 0)
										{
											continue;
										}
										((byte*)ptr)[11] = ((item2.tier != item4.tier) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 11) != 0 || (item2.corrupted != item4.corrupted && !item4.isUnique))
										{
											continue;
										}
										if (!this.method_4(item2, item4) || !this.method_5(item2, item4))
										{
											continue;
										}
										*(int*)ptr = *(int*)ptr + 1;
									}
									else
									{
										((byte*)ptr)[12] = ((apiItem.Type == BuyProcessor.getString_1(107361334)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 12) != 0)
										{
											((byte*)ptr)[13] = ((API.smethod_3(item2.typeLine) != API.smethod_3(item4.typeLine)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 13) != 0)
											{
												continue;
											}
											*(int*)ptr = *(int*)ptr + 1;
										}
										else
										{
											((byte*)ptr)[14] = ((a == text) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 14) == 0)
											{
												continue;
											}
											*(int*)ptr = *(int*)ptr + 1;
										}
									}
								}
								else
								{
									((byte*)ptr)[15] = ((a == text) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 15) == 0)
									{
										continue;
									}
									*(int*)ptr = *(int*)ptr + 1;
								}
								break;
							}
						}
					}
					if (*(int*)ptr == 0 && !this.bool_1)
					{
						Class307.smethod_1(ConfigOptions.OnBuyScamAttempt, BuyProcessor.getString_1(107411613), BuyProcessor.getString_1(107461381), new object[]
						{
							TradeProcessor.order_1.player_item_name,
							TradeProcessor.order_1.player_item_amount
						});
						this.bool_1 = true;
					}
					Class181.smethod_3(Enum11.const_0, string.Format(BuyProcessor.getString_1(107461320), *(int*)ptr, TradeProcessor.order_1.player_item_amount));
					result = ((*(int*)ptr >= TradeProcessor.order_1.player_item_amount) ? MatchTradeContentResults.ValidPrice : MatchTradeContentResults.InvalidPrice);
				}
			}
			return result;
		}

		private void method_2()
		{
			MatchTradeContentResults matchTradeContentResults = this.method_1(TradeProcessor.order_1.player_item_amount, TradeProcessor.order_1.player_item_name);
			MatchTradeContentResults matchTradeContentResults2 = matchTradeContentResults;
			MatchTradeContentResults matchTradeContentResults3 = matchTradeContentResults2;
			if (matchTradeContentResults3 != MatchTradeContentResults.InvalidPrice)
			{
				if (matchTradeContentResults3 == MatchTradeContentResults.ValidPrice)
				{
					Class181.smethod_3(Enum11.const_3, BuyProcessor.getString_1(107461251));
					TradeProcessor.smethod_3();
				}
			}
			else
			{
				Class181.smethod_3(Enum11.const_3, BuyProcessor.getString_1(107461270));
				int num = (int)Math.Ceiling(TradeProcessor.order_1.player_item_amount / API.smethod_6(TradeProcessor.order_1.player_item_name));
				if (num <= UI.list_0.smethod_17().Count<TradeWindowItem>() && this.int_1 < 2 && UI.smethod_71())
				{
					if (UI.list_0.smethod_17().Any(new Func<TradeWindowItem, bool>(BuyProcessor.<>c.<>9.method_1)))
					{
						Class181.smethod_3(Enum11.const_3, BuyProcessor.getString_1(107461225));
						this.int_1++;
						UI.smethod_65();
						this.method_2();
						return;
					}
				}
			}
			if (matchTradeContentResults != MatchTradeContentResults.ValidPrice && UI.smethod_71() && TradeProcessor.order_1.tradeStates_0 != Order.TradeStates.Cancelled)
			{
				TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
			}
		}

		private string method_3(string string_0)
		{
			string text = string_0.Replace(BuyProcessor.getString_1(107461660), BuyProcessor.getString_1(107397290)).Replace(BuyProcessor.getString_1(107461655), BuyProcessor.getString_1(107397290));
			foreach (object obj in Class145.regex_0.Matches(text))
			{
				Match match = (Match)obj;
				string value = match.Value;
				text = text.Replace(value, Util.smethod_9(value));
			}
			return text;
		}

		private unsafe bool method_4(Item item_0, Item item_1)
		{
			void* ptr = stackalloc byte[8];
			if (item_0.implicits == null && item_1.implicits == null)
			{
				((byte*)ptr)[4] = 1;
			}
			else if (item_0.implicits == null || item_1.implicits == null)
			{
				((byte*)ptr)[4] = 0;
			}
			else
			{
				((byte*)ptr)[5] = ((item_0.implicits.Count != item_1.implicits.Count) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					((byte*)ptr)[4] = 0;
				}
				else
				{
					*(int*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[7] = ((*(int*)ptr < item_0.implicits.Count) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) == 0)
						{
							break;
						}
						((byte*)ptr)[6] = ((!item_0.implicits[*(int*)ptr].Equals(item_1.implicits[*(int*)ptr])) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							goto IL_C8;
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
					((byte*)ptr)[4] = 1;
					goto IL_CD;
					IL_C8:
					((byte*)ptr)[4] = 0;
				}
			}
			IL_CD:
			return *(sbyte*)((byte*)ptr + 4) != 0;
		}

		private unsafe bool method_5(Item item_0, Item item_1)
		{
			void* ptr = stackalloc byte[8];
			if (item_0.enchants == null && item_1.enchants == null)
			{
				((byte*)ptr)[4] = 1;
			}
			else if (item_0.enchants == null || item_1.enchants == null)
			{
				((byte*)ptr)[4] = 0;
			}
			else
			{
				((byte*)ptr)[5] = ((item_0.enchants.Count != item_1.enchants.Count) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					((byte*)ptr)[4] = 0;
				}
				else
				{
					*(int*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[7] = ((*(int*)ptr < item_0.enchants.Count) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) == 0)
						{
							break;
						}
						((byte*)ptr)[6] = ((!item_0.enchants[*(int*)ptr].Equals(item_1.enchants[*(int*)ptr])) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							goto IL_C8;
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
					((byte*)ptr)[4] = 1;
					goto IL_CD;
					IL_C8:
					((byte*)ptr)[4] = 0;
				}
			}
			IL_CD:
			return *(sbyte*)((byte*)ptr + 4) != 0;
		}

		static BuyProcessor()
		{
			Strings.CreateGetStringDelegate(typeof(BuyProcessor));
		}

		private bool bool_1 = false;

		private int int_1 = 0;

		private int int_2 = 0;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
