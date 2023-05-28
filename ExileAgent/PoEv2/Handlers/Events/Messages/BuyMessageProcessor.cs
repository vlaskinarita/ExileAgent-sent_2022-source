using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using ns0;
using ns23;
using ns29;
using ns32;
using ns34;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers.Events.Messages
{
	public static class BuyMessageProcessor
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			BuyMessageProcessor.mainForm_0 = mainForm_1;
		}

		public unsafe static void smethod_1(BuyingTradeData buyingTradeData_0, Class240 class240_0)
		{
			void* ptr = stackalloc byte[35];
			BuyMessageProcessor.Class171 @class = new BuyMessageProcessor.Class171();
			@class.buyingTradeData_0 = buyingTradeData_0;
			Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107455107), new object[]
			{
				@class.buyingTradeData_0.Query,
				@class.buyingTradeData_0.Id,
				@class.buyingTradeData_0.CharacterName,
				@class.buyingTradeData_0.TradeType
			});
			Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107455582), new object[]
			{
				class240_0
			});
			Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107455521), new object[]
			{
				@class.buyingTradeData_0.OurItemType,
				@class.buyingTradeData_0.Item.typeLine
			});
			((byte*)ptr)[16] = (BuyMessageProcessor.mainForm_0.list_17.Any(new Func<string, bool>(@class.method_0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107455540), new object[]
				{
					@class.buyingTradeData_0.CharacterName
				});
			}
			else
			{
				((byte*)ptr)[17] = (BuyMessageProcessor.mainForm_0.list_15.Any(new Func<Player, bool>(@class.method_1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107455479), new object[]
					{
						@class.buyingTradeData_0.CharacterName
					});
				}
				else
				{
					Order order = new Order
					{
						BuyType = @class.buyingTradeData_0.TradeType,
						player = new Player(@class.buyingTradeData_0.CharacterName),
						OrderType = Order.Type.Buy,
						BuySettings = class240_0
					};
					((byte*)ptr)[18] = (BuyMessageProcessor.mainForm_0.list_16.Contains(@class.buyingTradeData_0.CharacterName) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107455366), new object[]
						{
							@class.buyingTradeData_0.CharacterName
						});
					}
					else
					{
						Class249 maxPrice = class240_0.MaxPrice;
						if (maxPrice == null || maxPrice.Amount == 0m)
						{
							Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107454805), new object[]
							{
								@class.buyingTradeData_0.Query,
								order.player
							});
						}
						else
						{
							@class.string_0 = API.smethod_7(@class.buyingTradeData_0.OurItemType).Text;
							if (maxPrice.Currency != BuyMessageProcessor.getString_0(107406883) && maxPrice.Currency != @class.string_0)
							{
								Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107454704), new object[]
								{
									@class.buyingTradeData_0.Item.Name,
									maxPrice.Currency,
									@class.string_0
								});
							}
							else
							{
								if (maxPrice.Currency == BuyMessageProcessor.getString_0(107406883) && @class.string_0 != BuyMessageProcessor.getString_0(107392004))
								{
									Dictionary<string, double> dictionary_ = BuyMessageProcessor.mainForm_0.dictionary_5;
									((byte*)ptr)[19] = ((!dictionary_.ContainsKey(@class.string_0)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 19) != 0)
									{
										Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107454603), new object[]
										{
											@class.string_0
										});
										return;
									}
									decimal num = (decimal)dictionary_[@class.string_0];
									Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107455054), new object[]
									{
										@class.string_0,
										num
									});
									decimal d = Math.Round(@class.buyingTradeData_0.OurItemsPerTrade * num, 2);
									((byte*)ptr)[20] = ((d > maxPrice.Amount) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 20) != 0)
									{
										Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107455061), new object[]
										{
											@class.buyingTradeData_0.OurItemsPerTrade,
											@class.string_0,
											maxPrice.Amount
										});
										return;
									}
								}
								else
								{
									((byte*)ptr)[21] = ((maxPrice.Amount < @class.buyingTradeData_0.PricePerItem) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 21) != 0)
									{
										Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107454920), new object[]
										{
											@class.buyingTradeData_0.Item.Name,
											Math.Round(maxPrice.Amount, 3),
											Math.Round(@class.buyingTradeData_0.PricePerItem, 3)
										});
										return;
									}
								}
								*(int*)ptr = BuyMessageProcessor.smethod_3(@class.buyingTradeData_0);
								*(int*)((byte*)ptr + 4) = BuyMessageProcessor.smethod_4(@class.buyingTradeData_0, class240_0, *(int*)ptr);
								decimal num2 = *(int*)((byte*)ptr + 4) / @class.buyingTradeData_0.PlayerItemsPerTrade * @class.buyingTradeData_0.OurItemsPerTrade;
								Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107454831), new object[]
								{
									*(int*)ptr,
									*(int*)((byte*)ptr + 4),
									num2,
									@class.buyingTradeData_0.PricePerItem
								});
								((byte*)ptr)[22] = ((*(int*)ptr == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 22) == 0)
								{
									((byte*)ptr)[23] = ((*(int*)((byte*)ptr + 4) == 0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 23) != 0)
									{
										Class181.smethod_3(Enum11.const_0, BuyMessageProcessor.getString_0(107454230));
									}
									else
									{
										((byte*)ptr)[24] = ((num2 != (int)num2) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 24) != 0)
										{
											if (*(int*)((byte*)ptr + 4) > 1 && @class.buyingTradeData_0.OurItemType == BuyMessageProcessor.getString_0(107454105))
											{
												num2 = Math.Ceiling(num2);
												Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107454064), new object[]
												{
													num2,
													@class.buyingTradeData_0.OurItemType
												});
												((byte*)ptr)[25] = ((num2 > maxPrice.Amount) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 25) != 0)
												{
													Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107454920), new object[]
													{
														@class.buyingTradeData_0.Item.Name,
														Math.Round(maxPrice.Amount, 3),
														Math.Round(num2, 3)
													});
													return;
												}
											}
											else
											{
												((byte*)ptr)[26] = ((!Class255.DecimalCurrencyList.Any(new Func<DecimalCurrencyListItem, bool>(@class.method_2))) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 26) != 0)
												{
													Class181.smethod_2(Enum11.const_2, BuyMessageProcessor.getString_0(107454543), new object[]
													{
														num2,
														@class.buyingTradeData_0.OurItemType
													});
													return;
												}
												DecimalCurrencyListItem decimalCurrencyListItem = Class255.DecimalCurrencyList.FirstOrDefault(new Func<DecimalCurrencyListItem, bool>(@class.method_3));
												((byte*)ptr)[27] = ((decimalCurrencyListItem == null) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 27) != 0)
												{
													return;
												}
												if (decimalCurrencyListItem.Currency == BuyMessageProcessor.getString_0(107383799) && decimalCurrencyListItem.DecimalType == BuyMessageProcessor.getString_0(107392004) && Class255.class105_0.method_4(ConfigOptions.UseCustomExaltPrice))
												{
													*(int*)((byte*)ptr + 8) = (int)Math.Round(Class255.class105_0.method_6(ConfigOptions.ExaltedOrbValue) * (num2 - (int)num2));
													order.DecimalAmount = *(int*)((byte*)ptr + 8);
													order.DecimalCurrencyType = decimalCurrencyListItem.DecimalType;
												}
												else
												{
													((byte*)ptr)[28] = ((decimalCurrencyListItem.Value == 0m) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 28) != 0)
													{
														Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107454482), new object[]
														{
															decimalCurrencyListItem
														});
														return;
													}
													*(int*)((byte*)ptr + 12) = (int)Math.Round(1m / decimalCurrencyListItem.Value * (num2 - (int)num2));
													string decimalType = decimalCurrencyListItem.DecimalType;
													order.DecimalAmount = *(int*)((byte*)ptr + 12);
													order.DecimalCurrencyType = decimalType;
												}
												Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107454405), new object[]
												{
													num2,
													@class.buyingTradeData_0.OurItemType,
													order.DecimalAmount,
													order.DecimalCurrencyType
												});
											}
										}
										string name = API.smethod_7(@class.buyingTradeData_0.OurItemType).Name;
										string player_item_name = API.smethod_7(@class.buyingTradeData_0.Item.CleanedTypeLine).Text;
										((byte*)ptr)[29] = ((@class.buyingTradeData_0.Item.typeLine == BuyMessageProcessor.getString_0(107454348)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 29) != 0)
										{
											((byte*)ptr)[30] = ((!@class.buyingTradeData_0.Item.typeLine.Contains(BuyMessageProcessor.getString_0(107454359))) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 30) != 0)
											{
												player_item_name = API.smethod_7(BuyMessageProcessor.getString_0(107454314)).Name;
											}
										}
										if (@class.buyingTradeData_0.Item.typeLine.Contains(BuyMessageProcessor.getString_0(107454325)) && @class.buyingTradeData_0.Item.explicitMods.Count > 1)
										{
											Class181.smethod_2(Enum11.const_0, BuyMessageProcessor.getString_0(107453768), new object[]
											{
												@class.buyingTradeData_0.CharacterName,
												@class.buyingTradeData_0.Item.typeLine
											});
										}
										else
										{
											order.player_item_amount = *(int*)((byte*)ptr + 4);
											order.player_item_name = player_item_name;
											order.PlayerItemUnique = @class.buyingTradeData_0.Item.IsUnique;
											order.my_item_name = name;
											order.my_item_amount = num2;
											order.OurItemUnique = false;
											order.OurMapTier = 0;
											order.PlayerMapTier = @class.buyingTradeData_0.Item.MapTier;
											order.message = BuyMessageProcessor.smethod_2(@class.buyingTradeData_0, order, *(int*)((byte*)ptr + 4));
											order.BaseItemName = @class.buyingTradeData_0.Item.BaseItemName;
											foreach (string s in BuyMessageProcessor.smethod_5(@class.buyingTradeData_0).Take(*(int*)ptr).Select(new Func<BuyingTradeData, string>(BuyMessageProcessor.<>c.<>9.method_0)))
											{
												string text = Util.smethod_5(Encoding.UTF8.GetString(Convert.FromBase64String(s))).Replace(BuyMessageProcessor.getString_0(107461727), BuyMessageProcessor.getString_0(107397357));
												((byte*)ptr)[31] = (text.Contains(BuyMessageProcessor.getString_0(107453715)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 31) != 0)
												{
													text = text.Replace(BuyMessageProcessor.getString_0(107453666), BuyMessageProcessor.getString_0(107397357));
												}
												foreach (object obj in Class145.regex_0.Matches(text))
												{
													Match match = (Match)obj;
													string value = match.Value;
													text = text.Replace(value, Util.smethod_9(value));
												}
												order.BuyItem64.Add(text);
											}
											order.ProcessedTime = DateTime.Now;
											BuyMessageProcessor.mainForm_0.list_9.Add(order);
											string text2 = null;
											((byte*)ptr)[32] = ((@class.buyingTradeData_0.TradeType == TradeTypes.LiveSearch) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 32) != 0)
											{
												text2 = BuyMessageProcessor.getString_0(107453633);
											}
											else
											{
												((byte*)ptr)[33] = ((@class.buyingTradeData_0.TradeType == TradeTypes.ItemBuying) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 33) != 0)
												{
													text2 = BuyMessageProcessor.getString_0(107453616);
												}
												else
												{
													((byte*)ptr)[34] = ((@class.buyingTradeData_0.TradeType == TradeTypes.BulkBuying) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 34) != 0)
													{
														text2 = BuyMessageProcessor.getString_0(107453603);
													}
												}
											}
											Class181.smethod_3(Enum11.const_0, string.Format(BuyMessageProcessor.getString_0(107453622), new object[]
											{
												order.player_item_amount,
												@class.buyingTradeData_0.Item.Name,
												order.my_item_amount,
												order.my_item_name,
												order.player.name,
												text2
											}));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private unsafe static string smethod_2(BuyingTradeData buyingTradeData_0, Order order_0, int int_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((int_0 == 1) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = ((order_0.BuyType != TradeTypes.BulkBuying) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = buyingTradeData_0.WhisperMessage;
				}
				else
				{
					result = string.Format(buyingTradeData_0.WhisperMessage, string.Format(BuyMessageProcessor.getString_0(107395771), order_0.player_item_amount, order_0.player_item_name), string.Format(BuyMessageProcessor.getString_0(107395771), order_0.my_item_amount, order_0.my_item_name));
				}
			}
			else
			{
				result = string.Format(BuyMessageProcessor.getString_0(107453561), new object[]
				{
					order_0.player.name,
					order_0.player_item_amount,
					order_0.player_item_name,
					order_0.my_item_amount,
					order_0.my_item_name,
					Class255.LeagueClean
				});
			}
			return result;
		}

		public unsafe static int smethod_3(BuyingTradeData buyingTradeData_0)
		{
			void* ptr = stackalloc byte[16];
			decimal pricePerItem = buyingTradeData_0.PricePerItem;
			string type = API.smethod_7(buyingTradeData_0.Item.Name).Type;
			*(int*)ptr = API.smethod_7(buyingTradeData_0.OurItemType).Stack * 60;
			*(int*)((byte*)ptr + 4) = API.smethod_7(buyingTradeData_0.PlayerItemType).Stack * 60;
			if (pricePerItem == 0m || pricePerItem > *(int*)ptr)
			{
				*(int*)((byte*)ptr + 8) = 0;
			}
			else
			{
				((byte*)ptr)[12] = ((type == BuyMessageProcessor.getString_0(107371624)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					*(int*)((byte*)ptr + 8) = 1;
				}
				else
				{
					((byte*)ptr)[13] = ((pricePerItem > 1m) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						((byte*)ptr)[14] = ((*(int*)ptr > *(int*)((byte*)ptr + 4)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 14) != 0)
						{
							*(int*)((byte*)ptr + 8) = (int)Math.Min(*(int*)ptr / pricePerItem, *(int*)((byte*)ptr + 4));
						}
						else
						{
							*(int*)((byte*)ptr + 8) = (int)(*(int*)ptr / pricePerItem);
						}
					}
					else
					{
						((byte*)ptr)[15] = ((*(int*)ptr > *(int*)((byte*)ptr + 4)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 15) != 0)
						{
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 4);
						}
						else
						{
							*(int*)((byte*)ptr + 8) = (int)Math.Min(*(int*)ptr / pricePerItem, *(int*)((byte*)ptr + 4));
						}
					}
				}
			}
			return *(int*)((byte*)ptr + 8);
		}

		public unsafe static int smethod_4(BuyingTradeData buyingTradeData_0, Class240 class240_0, int int_0)
		{
			void* ptr = stackalloc byte[49];
			int value = buyingTradeData_0.PlayerTotalStock;
			((byte*)ptr)[40] = ((buyingTradeData_0.TradeType != TradeTypes.BulkBuying) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 40) != 0)
			{
				value = BuyMessageProcessor.smethod_5(buyingTradeData_0).Sum(new Func<BuyingTradeData, int>(BuyMessageProcessor.<>c.<>9.method_1));
			}
			string text = API.smethod_7(buyingTradeData_0.OurItemType).Text;
			*(int*)ptr = StashManager.smethod_1(text, false).smethod_16(false);
			*(int*)((byte*)ptr + 4) = (int)Math.Floor(int_0 / buyingTradeData_0.PlayerItemsPerTrade);
			*(int*)((byte*)ptr + 8) = (int)Math.Floor(*(int*)ptr / buyingTradeData_0.OurItemsPerTrade);
			*(int*)((byte*)ptr + 12) = (int)Math.Floor(value / buyingTradeData_0.PlayerItemsPerTrade);
			*(int*)((byte*)ptr + 16) = 0;
			int num = Util.smethod_23(new int[]
			{
				*(int*)((byte*)ptr + 4),
				*(int*)((byte*)ptr + 8),
				*(int*)((byte*)ptr + 12)
			});
			string text2 = null;
			((byte*)ptr)[41] = ((class240_0.StockLimit > 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 41) != 0)
			{
				((byte*)ptr)[42] = (class240_0.TurnInCard ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 42) != 0)
				{
					ApiItem apiItem = API.smethod_18(buyingTradeData_0.Item.typeLine);
					((byte*)ptr)[43] = ((apiItem == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 43) != 0)
					{
						Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107459432), new object[]
						{
							buyingTradeData_0.Item.typeLine
						});
						string text3;
						*(int*)((byte*)ptr + 20) = class240_0.StockLimit - StashManager.smethod_2(buyingTradeData_0.Item.BaseItemName, 0, buyingTradeData_0.Item.MapTier, buyingTradeData_0.Item.IsUnique, true, string.Empty, out text3, out *(bool*)((byte*)ptr + 44), true).smethod_16(Class255.class105_0.method_4(ConfigOptions.StockLimitExcludePriced));
						((byte*)ptr)[45] = ((*(int*)((byte*)ptr + 20) < 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 45) != 0)
						{
							*(int*)((byte*)ptr + 20) = 0;
						}
					}
					else
					{
						string cardReward = apiItem.CardReward;
						string text3;
						*(int*)((byte*)ptr + 24) = StashManager.smethod_2(cardReward, 0, 0, apiItem.UniqueReward, true, string.Empty, out text3, out *(bool*)((byte*)ptr + 44), true).smethod_16(Class255.class105_0.method_4(ConfigOptions.StockLimitExcludePriced));
						((byte*)ptr)[46] = ((*(int*)((byte*)ptr + 24) + 1 > class240_0.StockLimit) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 46) != 0)
						{
							*(int*)((byte*)ptr + 28) = 0;
							goto IL_484;
						}
						*(int*)((byte*)ptr + 20) = class240_0.StockLimit - *(int*)((byte*)ptr + 24);
						*(int*)((byte*)ptr + 32) = StashManager.smethod_2(buyingTradeData_0.Item.BaseItemName, 0, buyingTradeData_0.Item.MapTier, buyingTradeData_0.Item.IsUnique, true, string.Empty, out text3, out *(bool*)((byte*)ptr + 44), true).smethod_16(Class255.class105_0.method_4(ConfigOptions.StockLimitExcludePriced));
						*(int*)((byte*)ptr + 36) = *(int*)((byte*)ptr + 20) * apiItem.Stack - *(int*)((byte*)ptr + 32);
						text2 = string.Format(BuyMessageProcessor.getString_0(107453984), new object[]
						{
							cardReward,
							class240_0.StockLimit,
							*(int*)((byte*)ptr + 36),
							buyingTradeData_0.Item.BaseItemName
						});
					}
				}
				else
				{
					string text3;
					*(int*)((byte*)ptr + 20) = class240_0.StockLimit - StashManager.smethod_2(buyingTradeData_0.Item.BaseItemName, 0, buyingTradeData_0.Item.MapTier, buyingTradeData_0.Item.IsUnique, true, string.Empty, out text3, out *(bool*)((byte*)ptr + 44), true).smethod_16(Class255.class105_0.method_4(ConfigOptions.StockLimitExcludePriced));
					((byte*)ptr)[47] = ((*(int*)((byte*)ptr + 20) < 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 47) != 0)
					{
						*(int*)((byte*)ptr + 20) = 0;
					}
				}
				*(int*)((byte*)ptr + 16) = (int)Math.Floor(*(int*)((byte*)ptr + 20) / buyingTradeData_0.PlayerItemsPerTrade);
				num = Util.smethod_23(new int[]
				{
					num,
					*(int*)((byte*)ptr + 16)
				});
				((byte*)ptr)[48] = ((text2 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 48) != 0)
				{
					text2 = string.Format(BuyMessageProcessor.getString_0(107453859), buyingTradeData_0.Item.BaseItemName, class240_0.StockLimit, *(int*)((byte*)ptr + 20));
				}
				Class181.smethod_3(Enum11.const_0, text2);
			}
			Class181.smethod_2(Enum11.const_3, BuyMessageProcessor.getString_0(107453254), new object[]
			{
				*(int*)((byte*)ptr + 4),
				*(int*)((byte*)ptr + 8),
				*(int*)((byte*)ptr + 12),
				*(int*)((byte*)ptr + 16),
				buyingTradeData_0.PlayerItemsPerTrade,
				buyingTradeData_0.OurItemsPerTrade
			});
			*(int*)((byte*)ptr + 28) = (int)(num * buyingTradeData_0.PlayerItemsPerTrade);
			IL_484:
			return *(int*)((byte*)ptr + 28);
		}

		public static IEnumerable<BuyingTradeData> smethod_5(BuyingTradeData buyingTradeData_0)
		{
			BuyMessageProcessor.Class172 @class = new BuyMessageProcessor.Class172();
			@class.buyingTradeData_0 = buyingTradeData_0;
			return BuyMessageProcessor.mainForm_0.list_10.ToList<BuyingTradeData>().Where(new Func<BuyingTradeData, bool>(@class.method_0));
		}

		// Note: this type is marked as 'beforefieldinit'.
		static BuyMessageProcessor()
		{
			Strings.CreateGetStringDelegate(typeof(BuyMessageProcessor));
			BuyMessageProcessor.regex_0 = new Regex(BuyMessageProcessor.getString_0(107453209));
		}

		private static MainForm mainForm_0;

		private static Regex regex_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class171
		{
			internal bool method_0(string string_1)
			{
				return string_1.ToLower() == this.buyingTradeData_0.CharacterName.ToLower();
			}

			internal bool method_1(Player player_0)
			{
				return player_0.name.ToLower() == this.buyingTradeData_0.CharacterName.ToLower();
			}

			internal bool method_2(DecimalCurrencyListItem decimalCurrencyListItem_0)
			{
				return decimalCurrencyListItem_0.Currency == this.string_0;
			}

			internal bool method_3(DecimalCurrencyListItem decimalCurrencyListItem_0)
			{
				return decimalCurrencyListItem_0.Currency == this.string_0;
			}

			public BuyingTradeData buyingTradeData_0;

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class172
		{
			internal bool method_0(BuyingTradeData buyingTradeData_1)
			{
				return buyingTradeData_1.AccountName == this.buyingTradeData_0.AccountName && buyingTradeData_1.Item.Name == this.buyingTradeData_0.Item.Name;
			}

			public BuyingTradeData buyingTradeData_0;
		}
	}
}
