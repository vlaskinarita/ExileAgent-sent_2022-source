using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ns0;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers.Events.Orders
{
	public static class BuyOrderProcessor
	{
		public unsafe static bool smethod_0(Order order_1, MainForm mainForm_0)
		{
			void* ptr = stackalloc byte[47];
			BuyOrderProcessor.Class168 @class = new BuyOrderProcessor.Class168();
			@class.order_0 = order_1;
			((byte*)ptr)[21] = ((@class.order_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				mainForm_0.list_9.RemoveAll(new Predicate<Order>(BuyOrderProcessor.<>c.<>9.method_0));
				((byte*)ptr)[22] = 0;
			}
			else
			{
				switch (@class.order_0.BuyType)
				{
				case TradeTypes.LiveSearch:
				{
					LiveSearchListItem liveSearchListItem = Class255.LiveSearchList.FirstOrDefault(new Func<LiveSearchListItem, bool>(@class.method_0));
					((byte*)ptr)[23] = ((!liveSearchListItem.Enabled) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 23) != 0)
					{
						Class181.smethod_2(Enum11.const_3, BuyOrderProcessor.getString_0(107459598), new object[]
						{
							@class.order_0.player.name
						});
						((byte*)ptr)[22] = 0;
						goto IL_B92;
					}
					break;
				}
				case TradeTypes.ItemBuying:
				{
					ItemBuyingListItem itemBuyingListItem = Class255.ItemBuyingList.FirstOrDefault(new Func<ItemBuyingListItem, bool>(@class.method_1));
					((byte*)ptr)[24] = ((!itemBuyingListItem.Enabled) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 24) != 0)
					{
						Class181.smethod_2(Enum11.const_3, BuyOrderProcessor.getString_0(107459598), new object[]
						{
							@class.order_0.player.name
						});
						((byte*)ptr)[22] = 0;
						goto IL_B92;
					}
					break;
				}
				case TradeTypes.BulkBuying:
				{
					BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList.FirstOrDefault(new Func<BulkBuyingListItem, bool>(@class.method_2));
					((byte*)ptr)[25] = ((!bulkBuyingListItem.Enabled) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 25) != 0)
					{
						Class181.smethod_2(Enum11.const_3, BuyOrderProcessor.getString_0(107459598), new object[]
						{
							@class.order_0.player.name
						});
						((byte*)ptr)[22] = 0;
						goto IL_B92;
					}
					break;
				}
				}
				Class181.smethod_2(Enum11.const_3, BuyOrderProcessor.getString_0(107459533), new object[]
				{
					@class.order_0.player.name
				});
				BuyOrderProcessor.order_0 = @class.order_0;
				((byte*)ptr)[26] = (mainForm_0.list_16.Contains(@class.order_0.player.name) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 26) != 0)
				{
					Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107459492), new object[]
					{
						@class.order_0.player.name
					});
					((byte*)ptr)[22] = 0;
				}
				else
				{
					((byte*)ptr)[27] = ((@class.order_0.BuySettings.StockLimit > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 27) != 0)
					{
						((byte*)ptr)[28] = (@class.order_0.BuySettings.TurnInCard ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 28) != 0)
						{
							ApiItem apiItem = API.smethod_18(@class.order_0.BaseItemName);
							((byte*)ptr)[29] = ((apiItem == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 29) != 0)
							{
								Class181.smethod_2(Enum11.const_3, BuyOrderProcessor.getString_0(107459399), new object[]
								{
									@class.order_0.BaseItemName
								});
								string text;
								*(int*)((byte*)ptr + 4) = StashManager.smethod_2(@class.order_0.BaseItemName, 0, @class.order_0.PlayerMapTier, @class.order_0.PlayerItemUnique, true, string.Empty, out text, out *(bool*)((byte*)ptr + 30), true).smethod_16(false);
								((byte*)ptr)[31] = ((*(int*)((byte*)ptr + 4) >= @class.order_0.BuySettings.StockLimit) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 31) != 0)
								{
									Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458818), new object[]
									{
										*(int*)((byte*)ptr + 4),
										@class.order_0.BaseItemName,
										@class.order_0.BuySettings.StockLimit
									});
									((byte*)ptr)[22] = 0;
									goto IL_B92;
								}
							}
							else
							{
								string cardReward = apiItem.CardReward;
								string text;
								*(int*)((byte*)ptr + 8) = StashManager.smethod_2(cardReward, 0, 0, apiItem.UniqueReward, true, string.Empty, out text, out *(bool*)((byte*)ptr + 30), true).smethod_16(false);
								((byte*)ptr)[32] = ((*(int*)((byte*)ptr + 8) + 1 > @class.order_0.BuySettings.StockLimit) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 32) != 0)
								{
									Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458818), new object[]
									{
										*(int*)((byte*)ptr + 8),
										cardReward,
										@class.order_0.BuySettings.StockLimit
									});
									((byte*)ptr)[22] = 0;
									goto IL_B92;
								}
							}
						}
						else
						{
							string text;
							*(int*)((byte*)ptr + 12) = StashManager.smethod_2(@class.order_0.BaseItemName, 0, @class.order_0.PlayerMapTier, @class.order_0.PlayerItemUnique, true, string.Empty, out text, out *(bool*)((byte*)ptr + 30), true).smethod_16(false);
							((byte*)ptr)[33] = ((*(int*)((byte*)ptr + 12) >= @class.order_0.BuySettings.StockLimit) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 33) != 0)
							{
								Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458818), new object[]
								{
									*(int*)((byte*)ptr + 12),
									@class.order_0.BaseItemName,
									@class.order_0.BuySettings.StockLimit
								});
								((byte*)ptr)[22] = 0;
								goto IL_B92;
							}
						}
					}
					((byte*)ptr)[34] = ((@class.order_0.my_item_amount != (int)@class.order_0.my_item_amount) ? 1 : 0);
					Dictionary<JsonTab, List<JsonItem>> dictionary;
					if (*(sbyte*)((byte*)ptr + 34) != 0)
					{
						Order order = new Order(@class.order_0.DecimalAmount, @class.order_0.DecimalCurrencyType);
						((byte*)ptr)[35] = ((!InventoryManager.smethod_1((int)@class.order_0.DecimalAmount, order, string.Empty, mainForm_0.list_7, out dictionary, out *(int*)((byte*)ptr + 16), true)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 35) != 0)
						{
							string text;
							Dictionary<JsonTab, List<JsonItem>> source = StashManager.smethod_2(@class.order_0.DecimalCurrencyType, 0, 0, @class.order_0.OurItemUnique, false, string.Empty, out text, out *(bool*)((byte*)ptr + 30), true);
							Enum11 enum11_ = Enum11.const_2;
							string format = BuyOrderProcessor.getString_0(107458729);
							object[] array = new object[5];
							array[0] = @class.order_0.player_item_amount;
							array[1] = @class.order_0.player_item_name;
							array[2] = @class.order_0.DecimalAmount;
							array[3] = @class.order_0.DecimalCurrencyType;
							array[4] = source.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(BuyOrderProcessor.<>c.<>9.method_1));
							Class181.smethod_3(enum11_, string.Format(format, array));
							((byte*)ptr)[22] = 0;
							goto IL_B92;
						}
					}
					((byte*)ptr)[36] = ((!InventoryManager.smethod_1(@class.order_0.my_item_amount_floor, @class.order_0, string.Empty, mainForm_0.list_7, out dictionary, out *(int*)((byte*)ptr + 16), true)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 36) != 0)
					{
						string text;
						Dictionary<JsonTab, List<JsonItem>> source2 = StashManager.smethod_2(@class.order_0.my_item_name, 0, @class.order_0.OurMapTier, @class.order_0.OurItemUnique, false, string.Empty, out text, out *(bool*)((byte*)ptr + 30), true);
						Enum11 enum11_2 = Enum11.const_2;
						string format2 = BuyOrderProcessor.getString_0(107458648);
						object[] array2 = new object[5];
						array2[0] = @class.order_0.player_item_amount;
						array2[1] = @class.order_0.player_item_name;
						array2[2] = @class.order_0.my_item_amount_floor;
						array2[3] = @class.order_0.my_item_name;
						array2[4] = source2.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(BuyOrderProcessor.<>c.<>9.method_3));
						Class181.smethod_3(enum11_2, string.Format(format2, array2));
						((byte*)ptr)[22] = 0;
					}
					else
					{
						UI.smethod_1();
						Win32.smethod_16(@class.order_0.message, true, true, false, false);
						@class.order_0.WhisperTime = DateTime.Now;
						UI.smethod_99();
						mainForm_0.method_7(@class.order_0.player.name);
						Class307.smethod_1(ConfigOptions.OnBuyWhisper, BuyOrderProcessor.getString_0(107459055), BuyOrderProcessor.getString_0(107459062), new object[]
						{
							@class.order_0.player.name,
							@class.order_0.player_item_amount,
							@class.order_0.player_item_name,
							@class.order_0.my_item_amount,
							@class.order_0.my_item_name
						});
						mainForm_0.method_66();
						mainForm_0.method_85(@class.order_0);
						*(int*)ptr = Class255.class105_0.method_7(ConfigOptions.TimeBeforeBuyInviteExpires) + 2;
						DateTime dateTime = DateTime.Now + new TimeSpan(0, 0, 0, *(int*)ptr);
						DateTime now = DateTime.Now;
						((byte*)ptr)[20] = 0;
						for (;;)
						{
							((byte*)ptr)[46] = ((dateTime > DateTime.Now) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 46) == 0)
							{
								break;
							}
							((byte*)ptr)[37] = ((@class.order_0.tradeStates_0 == Order.TradeStates.UserIgnored) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 37) != 0)
							{
								goto IL_AA4;
							}
							((byte*)ptr)[38] = ((@class.order_0.tradeStates_0 == Order.TradeStates.Offline) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 38) != 0)
							{
								goto IL_ADD;
							}
							((byte*)ptr)[39] = ((@class.order_0.tradeStates_0 == Order.TradeStates.Sold) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 39) != 0)
							{
								goto IL_B13;
							}
							((byte*)ptr)[40] = ((@class.order_0.tradeStates_0 == Order.TradeStates.DND) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 40) != 0)
							{
								goto IL_B44;
							}
							List<Position> list = UI.smethod_100();
							((byte*)ptr)[41] = ((!list.Any<Position>()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 41) != 0)
							{
								TimeSpan timeSpan = dateTime - DateTime.Now;
								((byte*)ptr)[42] = ((timeSpan.TotalSeconds == 0.0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 42) != 0)
								{
									break;
								}
								Thread.Sleep(100);
								timeSpan = DateTime.Now - now;
								((byte*)ptr)[43] = ((timeSpan.TotalSeconds > 1.0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 43) != 0)
								{
									Class181.smethod_3(Enum11.const_0, string.Format(BuyOrderProcessor.getString_0(107458234), (int)(dateTime - DateTime.Now).TotalSeconds));
									now = DateTime.Now;
								}
							}
							else
							{
								foreach (Position position_ in list)
								{
									string text2 = UI.smethod_101(position_);
									((byte*)ptr)[44] = ((text2 == @class.order_0.player.name) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 44) != 0)
									{
										UI.smethod_102(position_);
										Win32.smethod_14(BuyOrderProcessor.getString_0(107394318), false);
										BuyOrderProcessor.smethod_1(@class.order_0, mainForm_0);
										UI.smethod_13(1);
										((byte*)ptr)[22] = 1;
										goto IL_B92;
									}
									((byte*)ptr)[45] = ((!text2.smethod_25()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 45) != 0)
									{
										Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458161), new object[]
										{
											text2
										});
										UI.smethod_103(position_);
									}
								}
							}
						}
						goto IL_B78;
						IL_AA4:
						Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107459492), new object[]
						{
							@class.order_0.player.name
						});
						((byte*)ptr)[20] = 1;
						goto IL_B78;
						IL_ADD:
						Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107459001), new object[]
						{
							@class.order_0.player.name
						});
						((byte*)ptr)[20] = 1;
						goto IL_B78;
						IL_B13:
						Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458892), new object[]
						{
							@class.order_0.player_item_name
						});
						((byte*)ptr)[20] = 1;
						goto IL_B78;
						IL_B44:
						Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458331), new object[]
						{
							@class.order_0.player.name
						});
						((byte*)ptr)[20] = 1;
						IL_B78:
						BuyOrderProcessor.smethod_2(@class.order_0, mainForm_0, *(sbyte*)((byte*)ptr + 20) == 0);
						((byte*)ptr)[22] = 0;
					}
				}
			}
			IL_B92:
			return *(sbyte*)((byte*)ptr + 22) != 0;
		}

		private static void smethod_1(Order order_1, MainForm mainForm_0)
		{
			Class181.smethod_2(Enum11.const_0, BuyOrderProcessor.getString_0(107458616), new object[]
			{
				order_1.player.name
			});
			mainForm_0.list_8.Add(order_1);
			order_1.bool_2 = true;
			Class307.smethod_1(ConfigOptions.OnBuyPartyAccepted, BuyOrderProcessor.getString_0(107458523), BuyOrderProcessor.getString_0(107458470), new object[]
			{
				order_1.player_item_amount,
				order_1.player_item_name,
				order_1.my_item_amount,
				order_1.my_item_name
			});
		}

		private static void smethod_2(Order order_1, MainForm mainForm_0, bool bool_0)
		{
			mainForm_0.method_85(null);
			mainForm_0.list_16.Add(order_1.player.name);
			if (bool_0)
			{
				Class181.smethod_3(Enum11.const_0, BuyOrderProcessor.getString_0(107458409));
			}
		}

		static BuyOrderProcessor()
		{
			Strings.CreateGetStringDelegate(typeof(BuyOrderProcessor));
		}

		public static Order order_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class168
		{
			internal bool method_0(LiveSearchListItem liveSearchListItem_0)
			{
				return liveSearchListItem_0.Id == this.order_0.BuySettings.Id;
			}

			internal bool method_1(ItemBuyingListItem itemBuyingListItem_0)
			{
				return itemBuyingListItem_0.Id == this.order_0.BuySettings.Id;
			}

			internal bool method_2(BulkBuyingListItem bulkBuyingListItem_0)
			{
				return bulkBuyingListItem_0.QueryId == this.order_0.BuySettings.Id;
			}

			public Order order_0;
		}
	}
}
