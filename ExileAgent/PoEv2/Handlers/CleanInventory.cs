using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ns0;
using ns13;
using ns14;
using ns2;
using ns24;
using ns25;
using ns29;
using ns31;
using ns32;
using ns35;
using ns6;
using ns8;
using PoEv2.Classes;
using PoEv2.Handlers.Events.Trades;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.Models.Items;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers
{
	public static class CleanInventory
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			CleanInventory.mainForm_0 = mainForm_1;
		}

		public unsafe static bool smethod_1(Order order_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[9];
			*(byte*)ptr = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107368509), list_0.Count));
				CleanInventory.dictionary_0.Clear();
				((byte*)ptr)[2] = ((!list_0.Any<JsonItem>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[3] = ((UI.smethod_83(12) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						((byte*)ptr)[1] = 1;
						goto IL_404;
					}
					list_0 = UI.smethod_31(false, 60, 12, 5).smethod_11();
				}
				Dictionary<JsonTab, List<JsonItem>> dictionary = CleanInventory.smethod_2(list_0);
				UI.smethod_15();
				foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair in dictionary)
				{
					((byte*)ptr)[4] = ((!CleanInventory.smethod_3(order_0, keyValuePair.Key, keyValuePair.Value)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						CleanInventory.mainForm_0.bool_3 = true;
						CleanInventory.mainForm_0.method_64(true);
						((byte*)ptr)[1] = 0;
						goto IL_404;
					}
				}
				((byte*)ptr)[5] = ((UI.smethod_83(12) > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					List<JsonItem> list = UI.smethod_31(false, 60, 12, 5).smethod_11();
					((byte*)ptr)[6] = (list.Any<JsonItem>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368464), new object[]
						{
							list.Count
						});
						CleanInventory.smethod_1(order_0, list);
						((byte*)ptr)[1] = 1;
						goto IL_404;
					}
				}
				((byte*)ptr)[7] = ((order_0 != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					using (IEnumerator<Order> enumerator2 = CleanInventory.mainForm_0.list_2.Where(new Func<Order, bool>(CleanInventory.<>c.<>9.method_0)).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							CleanInventory.Class152 @class = new CleanInventory.Class152();
							@class.order_0 = enumerator2.Current;
							foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair2 in order_0.my_items)
							{
								Enum11 enum11_ = Enum11.const_3;
								string format = CleanInventory.getString_0(107368459);
								object arg = @class.order_0.my_item_name.Contains(CleanInventory.getString_0(107368890)) ? string.Format(CleanInventory.getString_0(107368881), @class.order_0.my_item_name, @class.order_0.OurMapTier) : @class.order_0.my_item_name;
								IEnumerable<JsonItem> source = Stashes.Items[keyValuePair2.Key.i];
								Func<JsonItem, bool> predicate;
								if ((predicate = @class.func_0) == null)
								{
									predicate = (@class.func_0 = new Func<JsonItem, bool>(@class.method_0));
								}
								object arg2 = source.Count(predicate);
								IEnumerable<JsonItem> source2 = Stashes.Items[keyValuePair2.Key.i];
								Func<JsonItem, bool> predicate2;
								if ((predicate2 = @class.func_1) == null)
								{
									predicate2 = (@class.func_1 = new Func<JsonItem, bool>(@class.method_1));
								}
								Class181.smethod_3(enum11_, string.Format(format, arg, arg2, source2.Where(predicate2).Sum(new Func<JsonItem, int>(CleanInventory.<>c.<>9.method_1))));
								@class.order_0.bool_3 = true;
							}
						}
					}
					((byte*)ptr)[8] = ((!CleanInventory.mainForm_0.list_3.Any<Order>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0 && (order_0.OrderType == Order.Type.Buy && order_0.BuySettings.DisableAfterPurchase))
					{
						Class181.smethod_3(Enum11.const_0, CleanInventory.getString_0(107368896));
						CleanInventory.smethod_11(order_0);
					}
				}
				CleanInventory.mainForm_0.list_3.Clear();
				CleanInventory.mainForm_0.list_14.Clear();
				Class181.smethod_3(Enum11.const_0, CleanInventory.getString_0(107368807));
				Win32.smethod_4(-2, -2, 50, 90, false);
				CleanInventory.mainForm_0.method_144();
				((byte*)ptr)[1] = 1;
			}
			IL_404:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe static Dictionary<JsonTab, List<JsonItem>> smethod_2(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[15];
			Dictionary<JsonTab, List<JsonItem>> dictionary = new Dictionary<JsonTab, List<JsonItem>>();
			((byte*)ptr)[4] = ((!CleanInventory.mainForm_0.list_3.Any<Order>()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				using (List<JsonItem>.Enumerator enumerator = list_0.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonItem jsonItem = enumerator.Current;
						string string_ = API.smethod_9(jsonItem);
						JsonTab jsonTab = null;
						((byte*)ptr)[5] = (Class255.class105_0.method_4(ConfigOptions.PutCurrencyBack) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							if (CleanInventory.mainForm_0.list_2.Any(new Func<Order, bool>(CleanInventory.<>c.<>9.method_2)))
							{
								Order order = CleanInventory.mainForm_0.list_2.FirstOrDefault(new Func<Order, bool>(CleanInventory.<>c.<>9.method_3));
								((byte*)ptr)[6] = ((order.my_items.Count == 1) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 6) != 0)
								{
									jsonTab = order.my_items.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
									((byte*)ptr)[7] = (jsonTab.IsSpecialTab ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 7) != 0)
									{
										jsonTab = null;
									}
								}
							}
						}
						Order order2 = CleanInventory.mainForm_0.list_2.LastOrDefault<Order>();
						if (order2 != null && order2.OrderType == Order.Type.Buy)
						{
							((byte*)ptr)[8] = ((order2.BuySettings.DumpStash != CleanInventory.getString_0(107351884)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) != 0)
							{
								Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368730), new object[]
								{
									order2.BuySettings.DumpStash
								});
								jsonTab = Stashes.smethod_7(order2.BuySettings.DumpStash, false);
								((byte*)ptr)[9] = ((jsonTab.n != order2.BuySettings.DumpStash) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 9) != 0)
								{
									jsonTab = null;
								}
							}
						}
						((byte*)ptr)[10] = ((jsonTab == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							jsonTab = Stashes.smethod_14(string_);
						}
						((byte*)ptr)[11] = ((!dictionary.ContainsKey(jsonTab)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							dictionary.Add(jsonTab, new List<JsonItem>());
						}
						dictionary[jsonTab].Add(jsonItem);
					}
					return dictionary;
				}
			}
			Order order3 = CleanInventory.mainForm_0.list_3.First<Order>();
			*(int*)ptr = 0;
			using (Dictionary<JsonTab, List<JsonItem>>.Enumerator enumerator2 = order3.my_items.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<JsonTab, List<JsonItem>> keyValuePair = enumerator2.Current;
					foreach (JsonItem jsonItem2 in keyValuePair.Value)
					{
						((byte*)ptr)[12] = ((list_0.Count - 1 < *(int*)ptr) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 12) != 0)
						{
							break;
						}
						JsonItem item = new JsonItem
						{
							x = list_0[*(int*)ptr].x,
							y = list_0[*(int*)ptr].y,
							typeLine = list_0[*(int*)ptr].typeLine,
							name = list_0[*(int*)ptr].name,
							w = list_0[*(int*)ptr].w,
							h = list_0[*(int*)ptr].h,
							stack = list_0[*(int*)ptr].stack
						};
						*(int*)ptr = *(int*)ptr + 1;
						((byte*)ptr)[13] = ((!dictionary.ContainsKey(keyValuePair.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							dictionary.Add(keyValuePair.Key, new List<JsonItem>());
						}
						dictionary[keyValuePair.Key].Add(item);
					}
				}
				goto IL_3CB;
			}
			IL_39D:
			dictionary[order3.my_items.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key].Add(list_0[*(int*)ptr]);
			*(int*)ptr = *(int*)ptr + 1;
			IL_3CB:
			((byte*)ptr)[14] = ((*(int*)ptr < list_0.Count) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 14) != 0)
			{
				goto IL_39D;
			}
			return dictionary;
		}

		private unsafe static bool smethod_3(Order order_0, JsonTab jsonTab_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[35];
			((byte*)ptr)[4] = ((!CleanInventory.smethod_10(ref jsonTab_0, order_0, list_0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				((byte*)ptr)[5] = 0;
			}
			else
			{
				UI.smethod_87();
				UI.smethod_35(jsonTab_0.i, true, 1);
				Bitmap bitmap = Class197.smethod_1(Class251.Stash, CleanInventory.getString_0(107397243));
				DateTime t = DateTime.Now.AddSeconds(3.0);
				for (;;)
				{
					((byte*)ptr)[8] = ((DateTime.Now < t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) == 0)
					{
						break;
					}
					Position position;
					((byte*)ptr)[6] = ((!UI.smethod_4(bitmap, out position, Images.StashLoadingPage, CleanInventory.getString_0(107368697))) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						break;
					}
					((byte*)ptr)[7] = ((!Stashes.Items[jsonTab_0.i].Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						break;
					}
					Thread.Sleep(300);
					bitmap.Dispose();
					bitmap = Class197.smethod_1(Class251.Stash, CleanInventory.getString_0(107397243));
				}
				CleanInventory.smethod_4(list_0);
				((byte*)ptr)[9] = ((!jsonTab_0.IsSupported) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					((byte*)ptr)[5] = 1;
				}
				else
				{
					Win32.smethod_4(-2, -2, 50, 90, false);
					Thread.Sleep(300);
					Bitmap bitmap2 = Class197.smethod_1(Class251.Stash, CleanInventory.getString_0(107397243));
					List<Rectangle> list = UI.smethod_59(bitmap, bitmap2, CleanInventory.getString_0(107397243), 1.0, 0);
					Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368704), new object[]
					{
						list.Count
					});
					List<Position> list2 = new List<Position>();
					List<Item> list3 = new List<Item>();
					int[,] array = null;
					((byte*)ptr)[10] = ((!jsonTab_0.IsSpecialTab) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						int int_ = (jsonTab_0.type == CleanInventory.getString_0(107382094)) ? 24 : 12;
						array = StashManager.smethod_5(jsonTab_0.i, int_);
					}
					((byte*)ptr)[11] = ((jsonTab_0.type == CleanInventory.getString_0(107382081)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						using (List<JsonItem>.Enumerator enumerator = list_0.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								JsonItem jsonItem = enumerator.Current;
								CleanInventory.Class153 @class = new CleanInventory.Class153();
								@class.position_0 = Class394.smethod_0(jsonItem.typeLine);
								((byte*)ptr)[12] = ((@class.position_0.X == -1) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 12) == 0)
								{
									((byte*)ptr)[13] = ((!list2.Any(new Func<Position, bool>(@class.method_0))) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 13) != 0)
									{
										list2.Add(@class.position_0);
									}
								}
							}
							goto IL_857;
						}
					}
					((byte*)ptr)[14] = ((jsonTab_0.type == CleanInventory.getString_0(107394156)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						using (List<JsonItem>.Enumerator enumerator2 = list_0.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								JsonItem jsonItem2 = enumerator2.Current;
								foreach (Rectangle rectangle_ in list)
								{
									CleanInventory.Class154 class2 = new CleanInventory.Class154();
									class2.position_0 = UI.smethod_61(jsonTab_0.type, rectangle_);
									Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368171), new object[]
									{
										rectangle_.X,
										rectangle_.Y,
										rectangle_.Width,
										rectangle_.Height,
										class2.position_0.X,
										class2.position_0.Y
									});
									if (class2.position_0.X >= 0 && class2.position_0.Y >= 0)
									{
										JsonItem jsonItem3 = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class2.method_0));
										if (jsonItem3 == null || jsonItem3.influences == null)
										{
											((byte*)ptr)[15] = (Class392.smethod_1(class2.position_0.X) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 15) != 0)
											{
												((byte*)ptr)[16] = ((!list2.Any(new Func<Position, bool>(class2.method_1))) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 16) != 0)
												{
													Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368098), new object[]
													{
														rectangle_.X,
														rectangle_.Y,
														rectangle_.Width,
														rectangle_.Height,
														class2.position_0.X,
														class2.position_0.Y
													});
													list2.Add(class2.position_0);
												}
											}
											else
											{
												CleanInventory.Class155 class3 = new CleanInventory.Class155();
												class3.position_0 = Class392.smethod_0(jsonItem2.typeLine);
												((byte*)ptr)[17] = ((!list2.Any(new Func<Position, bool>(class3.method_0))) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 17) != 0)
												{
													Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107367993), new object[]
													{
														rectangle_.X,
														rectangle_.Y,
														rectangle_.Width,
														rectangle_.Height,
														class2.position_0.X,
														class2.position_0.Y
													});
													list2.Add(class3.position_0);
												}
											}
										}
									}
								}
							}
							goto IL_857;
						}
					}
					foreach (Rectangle rectangle_2 in list)
					{
						CleanInventory.Class156 class4 = new CleanInventory.Class156();
						class4.position_0 = UI.smethod_61(jsonTab_0.type, rectangle_2);
						Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368171), new object[]
						{
							rectangle_2.X,
							rectangle_2.Y,
							rectangle_2.Width,
							rectangle_2.Height,
							class4.position_0.X,
							class4.position_0.Y
						});
						if (class4.position_0.X >= 0 && class4.position_0.Y >= 0)
						{
							JsonItem jsonItem4 = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class4.method_0));
							if (jsonItem4 == null || jsonItem4.influences == null)
							{
								((byte*)ptr)[18] = ((!jsonTab_0.IsSpecialTab) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 18) != 0)
								{
									if (jsonItem4 != null && jsonItem4.stack_size == 1)
									{
										continue;
									}
									((byte*)ptr)[19] = ((jsonItem4 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 19) != 0 && (array.GetLength(0) <= class4.position_0.x || array.GetLength(1) <= class4.position_0.y || array[class4.position_0.x, class4.position_0.y] == 1))
									{
										continue;
									}
								}
								((byte*)ptr)[20] = ((!list2.Any(new Func<Position, bool>(class4.method_1))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 20) != 0)
								{
									Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368098), new object[]
									{
										rectangle_2.X,
										rectangle_2.Y,
										rectangle_2.Width,
										rectangle_2.Height,
										class4.position_0.X,
										class4.position_0.Y
									});
									list2.Add(class4.position_0);
								}
							}
						}
					}
					IL_857:
					List<string> list4 = list_0.Select(new Func<JsonItem, string>(CleanInventory.<>c.<>9.method_4)).ToList<string>();
					((byte*)ptr)[21] = ((!UI.smethod_13(1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 21) != 0)
					{
						((byte*)ptr)[5] = 0;
					}
					else
					{
						if (!list.Any<Rectangle>() && CleanInventory.mainForm_0.list_3.Any<Order>())
						{
							Class181.smethod_3(Enum11.const_3, CleanInventory.getString_0(107367948));
							Order order = CleanInventory.mainForm_0.list_3.First<Order>();
							Stashes.Items = order.StashBackup;
						}
						using (IEnumerator<Position> enumerator5 = list2.OrderBy(new Func<Position, int>(CleanInventory.<>c.<>9.method_5)).ThenBy(new Func<Position, int>(CleanInventory.<>c.<>9.method_6)).GetEnumerator())
						{
							while (enumerator5.MoveNext())
							{
								CleanInventory.Class157 class5 = new CleanInventory.Class157();
								class5.position_0 = enumerator5.Current;
								CleanInventory.Class158 class6 = new CleanInventory.Class158();
								class6.class157_0 = class5;
								class6.jsonItem_0 = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class6.class157_0.method_0));
								class6.item_0 = null;
								if (class6.jsonItem_0 != null && list2.Count == 1 && CleanInventory.mainForm_0.list_8.Any<Order>())
								{
									class6.item_0 = new Item(class6.jsonItem_0);
									class6.item_0.stack = list_0.Where(new Func<JsonItem, bool>(class6.method_0)).Sum(new Func<JsonItem, int>(CleanInventory.<>c.<>9.method_7)) + class6.jsonItem_0.stack;
								}
								else
								{
									UI.smethod_34(jsonTab_0.type, class6.class157_0.position_0.x, class6.class157_0.position_0.y, Enum2.const_2, false);
									class6.item_0 = new Item
									{
										text = Win32.smethod_21()
									};
									*(int*)ptr = 0;
									while (class6.item_0.text == null || class6.item_0.text == CleanInventory.getString_0(107381869))
									{
										((byte*)ptr)[22] = ((*(int*)ptr > 2) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 22) != 0)
										{
											break;
										}
										Class181.smethod_3(Enum11.const_3, CleanInventory.getString_0(107368343));
										class6.item_0.text = Win32.smethod_21();
										Thread.Sleep(25);
										*(int*)ptr = *(int*)ptr + 1;
									}
									class6.item_0.method_0();
									if (class6.item_0.text == null || class6.item_0.text == CleanInventory.getString_0(107381869))
									{
										Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107368330), new object[]
										{
											class6.class157_0.position_0.ToString()
										});
										continue;
									}
									class6.item_0.stack_size = Util.smethod_2(jsonTab_0, class6.item_0);
									((byte*)ptr)[23] = (list3.Any(new Func<Item, bool>(class6.method_1)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 23) != 0)
									{
										continue;
									}
									list3.Add(class6.item_0);
								}
								JsonItem jsonItem5 = new JsonItem(class6.item_0, class6.class157_0.position_0.x, class6.class157_0.position_0.y);
								((byte*)ptr)[24] = ((class6.jsonItem_0 != null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 24) != 0)
								{
									((byte*)ptr)[25] = ((class6.jsonItem_0.typeLine != class6.item_0.typeLine) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 25) != 0)
									{
										Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107368257), class6.jsonItem_0.typeLine, class6.item_0.typeLine));
										continue;
									}
									((byte*)ptr)[26] = ((!class6.item_0.HasStack) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 26) != 0)
									{
										if (jsonTab_0.type == CleanInventory.getString_0(107382081) && API.smethod_7(class6.jsonItem_0.Name).Stack == 1)
										{
											class6.jsonItem_0.stackSize += list_0.Count(new Func<JsonItem, bool>(class6.method_2));
										}
										else
										{
											class6.jsonItem_0.stackSize += list_0.Count(new Func<JsonItem, bool>(class6.method_3)) / list.Count;
										}
									}
									else
									{
										class6.jsonItem_0.stackSize = class6.item_0.stack;
									}
									Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107368204), new object[]
									{
										jsonItem5.Name,
										jsonItem5.x,
										jsonItem5.y,
										jsonTab_0.n,
										jsonTab_0.i,
										class6.jsonItem_0.stack
									}));
								}
								else
								{
									((byte*)ptr)[27] = 0;
									foreach (string text in list4)
									{
										((byte*)ptr)[28] = (text.Contains(jsonItem5.typeLine.Replace(CleanInventory.getString_0(107367567), CleanInventory.getString_0(107397243))) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 28) != 0)
										{
											((byte*)ptr)[27] = 1;
											break;
										}
									}
									((byte*)ptr)[29] = ((*(sbyte*)((byte*)ptr + 27) == 0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 29) != 0)
									{
										continue;
									}
									((byte*)ptr)[30] = ((!jsonTab_0.IsSpecialTab) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 30) != 0)
									{
										Size size = ItemData.smethod_2(jsonItem5.typeLine);
										Position position2 = StashManager.smethod_7(jsonTab_0.i, size.Width, size.Height);
										((byte*)ptr)[31] = (Position.smethod_0(position2, null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 31) != 0 || array.GetLength(0) <= class6.class157_0.position_0.x || array.GetLength(1) <= class6.class157_0.position_0.y)
										{
											continue;
										}
										((byte*)ptr)[32] = ((array[class6.class157_0.position_0.x, class6.class157_0.position_0.y] == 1) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 32) != 0)
										{
											continue;
										}
										Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107367586), new object[]
										{
											jsonItem5.Name,
											jsonItem5.x,
											jsonItem5.y
										});
										jsonItem5.x = position2.x;
										jsonItem5.y = position2.y;
										jsonItem5.w = size.Width;
										jsonItem5.h = size.Height;
									}
									((byte*)ptr)[33] = ((!class6.item_0.HasStack) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 33) != 0)
									{
										if (jsonTab_0.type == CleanInventory.getString_0(107382081) && API.smethod_7(jsonItem5.Name).Stack == 1)
										{
											jsonItem5.stackSize = list_0.Count(new Func<JsonItem, bool>(class6.method_4));
										}
										else
										{
											jsonItem5.stackSize = list_0.Count(new Func<JsonItem, bool>(class6.method_5)) / list.Count;
										}
									}
									else
									{
										jsonItem5.stackSize = class6.item_0.stack;
									}
									list4.Remove(jsonItem5.Name);
									Stashes.Items[jsonTab_0.i].Add(jsonItem5);
									Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107367505), new object[]
									{
										jsonItem5.Name,
										jsonItem5.x,
										jsonItem5.y,
										jsonTab_0.n,
										jsonTab_0.i,
										jsonItem5.stack,
										jsonItem5.w,
										jsonItem5.h
									}));
								}
								if (CleanInventory.mainForm_0.list_3.Any<Order>() && (class6.jsonItem_0 == null || class6.jsonItem_0.note == null))
								{
									CleanInventory.smethod_5(jsonTab_0, jsonItem5);
								}
								if (order_0 != null && order_0.OrderType == Order.Type.Buy && (order_0.player_item_name.Contains(jsonItem5.CleanedTypeLine) || (order_0.player_item_name.Contains(CleanInventory.getString_0(107369944)) && jsonItem5.typeLine.Contains(CleanInventory.getString_0(107369944)))))
								{
									if (order_0.BuySettings.PriceAfterBuying && !CleanInventory.mainForm_0.list_3.Any<Order>())
									{
										JsonItem jsonItem_ = class6.jsonItem_0 ?? jsonItem5;
										CleanInventory.smethod_6(order_0, jsonTab_0, class6.class157_0.position_0, class6.jsonItem_0, jsonItem_);
									}
									if (order_0.BuySettings.TurnInCard && CleanInventory.smethod_9(jsonItem5))
									{
										bitmap.smethod_12();
										bitmap2.smethod_12();
										Class181.smethod_2(Enum11.const_0, CleanInventory.getString_0(107367424), new object[]
										{
											jsonItem5.Name
										});
										((byte*)ptr)[34] = ((!CleanInventory.smethod_8(jsonTab_0, jsonItem5)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 34) != 0)
										{
											Class181.smethod_3(Enum11.const_2, CleanInventory.getString_0(107367827));
											((byte*)ptr)[5] = 0;
											goto IL_127D;
										}
									}
								}
								Win32.smethod_4(-2, -2, 50, 90, false);
							}
						}
						bitmap.smethod_12();
						bitmap2.smethod_12();
						((byte*)ptr)[5] = 1;
					}
				}
			}
			IL_127D:
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		private unsafe static void smethod_4(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[8] = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				Bitmap bitmap = UI.smethod_67();
				List<JsonItem> list = list_0.ToList<JsonItem>();
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[9] = ((*(int*)ptr < list_0.Count) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						break;
					}
					JsonItem jsonItem = list_0[*(int*)ptr];
					UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
					Win32.smethod_9();
					Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107367790), jsonItem.stack, jsonItem.Name));
					*(int*)ptr = *(int*)ptr + 1;
				}
				Win32.smethod_4(-2, -2, 50, 90, false);
				Thread.Sleep(200);
				Bitmap bitmap2 = UI.smethod_67();
				List<Rectangle> list2 = UI.smethod_59(bitmap, bitmap2, CleanInventory.getString_0(107397243), 0.4, 0);
				foreach (Rectangle rectangle_ in list2)
				{
					CleanInventory.Class159 @class = new CleanInventory.Class159();
					@class.position_0 = UI.smethod_60(Enum10.const_2, rectangle_);
					list.RemoveAll(new Predicate<JsonItem>(@class.method_0));
				}
				using (List<JsonItem>.Enumerator enumerator2 = list.ToList<JsonItem>().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CleanInventory.Class160 class2 = new CleanInventory.Class160();
						class2.jsonItem_0 = enumerator2.Current;
						*(int*)((byte*)ptr + 4) = Class308.smethod_0(Images.EmptyCell).Item1.Width;
						Position position = UI.smethod_47(Enum10.const_2, class2.jsonItem_0.x, class2.jsonItem_0.y);
						Rectangle rectangle_2 = new Rectangle(position.X, position.Y, *(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 4));
						using (Bitmap bitmap3 = Class197.smethod_1(rectangle_2, CleanInventory.getString_0(107397243)))
						{
							((byte*)ptr)[10] = (UI.smethod_9(bitmap3, Images.EmptyCellInner) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 10) != 0)
							{
								list.RemoveAll(new Predicate<JsonItem>(class2.method_0));
							}
						}
					}
				}
				bitmap.Dispose();
				bitmap2.Dispose();
				((byte*)ptr)[11] = (list.Any<JsonItem>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					CleanInventory.smethod_4(list);
				}
			}
		}

		private unsafe static void smethod_5(JsonTab jsonTab_0, JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[2];
			Order order = CleanInventory.mainForm_0.list_3.First<Order>();
			if (!order.Flipping && order.OrderType != Order.Type.Buy)
			{
				*(byte*)ptr = ((!order.my_item_name.Contains(jsonItem_0.CleanedTypeLine)) ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					string originalNote = order.OriginalNote;
					((byte*)ptr)[1] = ((!Class252.smethod_3(jsonTab_0.n, originalNote)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						UI.smethod_37(originalNote);
						jsonItem_0.note = originalNote;
						Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107367733), order.my_item_name, jsonTab_0.n, originalNote));
						Win32.smethod_4(-2, -2, 50, 90, false);
					}
					else
					{
						Class181.smethod_3(Enum11.const_3, string.Format(CleanInventory.getString_0(107367712), order.my_item_name, jsonTab_0.n));
					}
				}
			}
		}

		private unsafe static void smethod_6(Order order_0, JsonTab jsonTab_0, Position position_0, JsonItem jsonItem_0, JsonItem jsonItem_1)
		{
			void* ptr = stackalloc byte[4];
			Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107367679), new object[]
			{
				order_0.player_item_name,
				jsonItem_1.typeLine
			});
			*(byte*)ptr = ((!order_0.player_item_name.Contains(jsonItem_1.typeLine)) ? 1 : 0);
			if (*(sbyte*)ptr == 0 || (order_0.player_item_name.Contains(CleanInventory.getString_0(107369944)) && jsonItem_1.typeLine.Contains(CleanInventory.getString_0(107369944))))
			{
				((byte*)ptr)[1] = ((order_0 == TradeProcessor.order_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0 || !jsonItem_1.IsCard || !order_0.BuySettings.TurnInCard)
				{
					string text = CleanInventory.smethod_7(order_0.BuySettings, jsonItem_1);
					Class252 @class = Class252.smethod_0(text, false);
					Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107367102), new object[]
					{
						text
					});
					if (jsonItem_0 != null && jsonItem_0.note == text)
					{
						Class181.smethod_2(Enum11.const_3, CleanInventory.getString_0(107367085), new object[]
						{
							jsonItem_0.Name,
							text
						});
					}
					else
					{
						((byte*)ptr)[2] = ((@class.Amount != 0m) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							UI.smethod_34(jsonTab_0.type, position_0.x, position_0.y, Enum2.const_2, false);
							((byte*)ptr)[3] = (UI.smethod_38(text) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								Class181.smethod_2(Enum11.const_1, CleanInventory.getString_0(107367016), new object[]
								{
									TradeProcessor.order_0.player_item_name,
									text
								});
								jsonItem_1.note = text;
							}
							else
							{
								Class181.smethod_2(Enum11.const_2, CleanInventory.getString_0(107366975), new object[]
								{
									TradeProcessor.order_0.player_item_name
								});
							}
						}
					}
				}
			}
		}

		private unsafe static string smethod_7(Class240 class240_0, JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[8];
			*(byte*)ptr = ((!class240_0.PriceAfterBuying) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = string.Empty;
			}
			else
			{
				((byte*)ptr)[1] = ((!class240_0.AutoPrice) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					ApiItem apiItem = API.smethod_7(class240_0.PriceCurrency);
					((byte*)ptr)[2] = ((apiItem.Type == CleanInventory.getString_0(107371510)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = string.Empty;
					}
					else
					{
						result = CleanInventory.getString_0(107367382) + class240_0.PriceAmount + CleanInventory.getString_0(107397280) + apiItem.Id;
					}
				}
				else
				{
					((byte*)ptr)[3] = (CleanInventory.dictionary_0.ContainsKey(jsonItem_0.UniqueIdentifiers) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						result = CleanInventory.dictionary_0[jsonItem_0.UniqueIdentifiers];
					}
					else
					{
						((byte*)ptr)[4] = (class240_0.PriceAsBulk ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							string text = API.smethod_4(class240_0.AutoPriceCurrency);
							string id = API.smethod_7(jsonItem_0.CleanedTypeLine).Id;
							((byte*)ptr)[5] = ((id == CleanInventory.getString_0(107371510)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								Class181.smethod_2(Enum11.const_2, CleanInventory.getString_0(107367401), new object[]
								{
									jsonItem_0.CleanedTypeLine
								});
								result = string.Empty;
							}
							else
							{
								KeyValuePair<string, decimal> keyValuePair = Pricing.smethod_6(id, text, 1, class240_0.PriceSkip, class240_0.PriceTake).First<KeyValuePair<string, decimal>>();
								((byte*)ptr)[6] = ((keyValuePair.Key == CleanInventory.getString_0(107367312)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 6) != 0)
								{
									Class181.smethod_2(Enum11.const_2, CleanInventory.getString_0(107367335), new object[]
									{
										text,
										id
									});
									result = string.Empty;
								}
								else
								{
									Class248 @class = Class248.smethod_0(keyValuePair.Value, class240_0.MaxListingSize);
									string text2 = CleanInventory.getString_0(107367382) + @class.method_1(true) + CleanInventory.getString_0(107397280) + text;
									CleanInventory.dictionary_0.Add(jsonItem_0.UniqueIdentifiers, text2);
									result = text2;
								}
							}
						}
						else
						{
							string text3 = Pricing.smethod_8(jsonItem_0, class240_0.PriceSkip, class240_0.PriceTake, class240_0.AutoPriceCurrency);
							((byte*)ptr)[7] = ((text3 == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								result = string.Empty;
							}
							else
							{
								CleanInventory.dictionary_0.Add(jsonItem_0.UniqueIdentifiers, text3);
								result = text3;
							}
						}
					}
				}
			}
			return result;
		}

		private unsafe static bool smethod_8(JsonTab jsonTab_0, JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[3];
			CleanInventory.Class161 @class = new CleanInventory.Class161();
			@class.jsonItem_0 = jsonItem_0;
			Stashes.Items[jsonTab_0.i].RemoveAll(new Predicate<JsonItem>(@class.method_0));
			UI.smethod_34(jsonTab_0.type, @class.jsonItem_0.x, @class.jsonItem_0.y, Enum2.const_2, false);
			Win32.smethod_9();
			@class.jsonItem_0.x = 0;
			@class.jsonItem_0.y = 0;
			List<JsonItem> list = new List<JsonItem>
			{
				@class.jsonItem_0
			};
			list = Class361.smethod_1(list);
			*(byte*)ptr = ((!list.Any<JsonItem>()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				Order order = new Order();
				order.player_item_name = list.First<JsonItem>().Name;
				order.OrderType = Order.Type.Buy;
				order.BuySettings = TradeProcessor.order_0.BuySettings;
				((byte*)ptr)[2] = ((!CleanInventory.smethod_3(order, jsonTab_0, list)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[1] = 1;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe static bool smethod_9(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!jsonItem_0.IsCard) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = ((jsonItem_0.stackSize == jsonItem_0.maxStackSize) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe static bool smethod_10(ref JsonTab jsonTab_0, Order order_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[9];
			((byte*)ptr)[4] = ((!StashManager.smethod_9(jsonTab_0.i, list_0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				Class181.smethod_3(Enum11.const_2, CleanInventory.getString_0(107367190));
				((byte*)ptr)[5] = 0;
				foreach (int num in Class255.class105_0.method_8<int>(ConfigOptions.DumpTabList))
				{
					*(int*)ptr = num;
					((byte*)ptr)[6] = (StashManager.smethod_9(*(int*)ptr, list_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						Class181.smethod_2(Enum11.const_0, CleanInventory.getString_0(107366544), new object[]
						{
							Stashes.smethod_11(*(int*)ptr).n
						});
						jsonTab_0 = Stashes.smethod_11(*(int*)ptr);
						((byte*)ptr)[5] = 1;
						break;
					}
				}
				if (order_0 != null && order_0.OrderType == Order.Type.Buy && order_0.BuySettings.DisableAfterStashFull)
				{
					Class181.smethod_3(Enum11.const_0, CleanInventory.getString_0(107366535));
					CleanInventory.smethod_11(order_0);
				}
				((byte*)ptr)[7] = ((*(sbyte*)((byte*)ptr + 5) == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					Class181.smethod_3(Enum11.const_2, CleanInventory.getString_0(107366442));
					((byte*)ptr)[8] = 0;
					goto IL_129;
				}
			}
			((byte*)ptr)[8] = 1;
			IL_129:
			return *(sbyte*)((byte*)ptr + 8) != 0;
		}

		private static void smethod_11(Order order_0)
		{
			CleanInventory.Class162 @class = new CleanInventory.Class162();
			@class.order_0 = order_0;
			if (@class.order_0.OrderType == Order.Type.Buy)
			{
				CleanInventory.mainForm_0.Invoke(new Action(@class.method_0));
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static CleanInventory()
		{
			Strings.CreateGetStringDelegate(typeof(CleanInventory));
			CleanInventory.dictionary_0 = new Dictionary<string, string>();
		}

		private static MainForm mainForm_0;

		private static Dictionary<string, string> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class152
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.order_0.my_item_name;
			}

			internal bool method_1(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.order_0.my_item_name;
			}

			public Order order_0;

			public Func<JsonItem, bool> func_0;

			public Func<JsonItem, bool> func_1;
		}

		[CompilerGenerated]
		private sealed class Class153
		{
			internal bool method_0(Position position_1)
			{
				return position_1.X == this.position_0.X && position_1.Y == this.position_0.Y;
			}

			public Position position_0;
		}

		[CompilerGenerated]
		private sealed class Class154
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.position_0.x && jsonItem_0.y == this.position_0.y;
			}

			internal bool method_1(Position position_1)
			{
				return position_1.X == this.position_0.X && position_1.Y == this.position_0.Y;
			}

			public Position position_0;
		}

		[CompilerGenerated]
		private sealed class Class155
		{
			internal bool method_0(Position position_1)
			{
				return position_1.X == this.position_0.X && position_1.Y == this.position_0.Y;
			}

			public Position position_0;
		}

		[CompilerGenerated]
		private sealed class Class156
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.position_0.x && jsonItem_0.y == this.position_0.y;
			}

			internal bool method_1(Position position_1)
			{
				return position_1.X == this.position_0.X && position_1.Y == this.position_0.Y;
			}

			public Position position_0;
		}

		[CompilerGenerated]
		private sealed class Class157
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.position_0.x && jsonItem_0.y == this.position_0.y;
			}

			public Position position_0;
		}

		[CompilerGenerated]
		private sealed class Class158
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.UniqueIdentifiers == this.jsonItem_0.UniqueIdentifiers;
			}

			internal bool method_1(Item item_1)
			{
				return item_1.method_1(this.item_0, this.class157_0.position_0.x, this.class157_0.position_0.y);
			}

			internal bool method_2(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name == this.item_0.Name;
			}

			internal bool method_3(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name == this.item_0.Name;
			}

			internal bool method_4(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name == this.item_0.Name;
			}

			internal bool method_5(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name == this.item_0.Name;
			}

			public JsonItem jsonItem_0;

			public Item item_0;

			public CleanInventory.Class157 class157_0;
		}

		[CompilerGenerated]
		private sealed class Class159
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.position_0.X && jsonItem_0.y == this.position_0.Y;
			}

			public Position position_0;
		}

		[CompilerGenerated]
		private sealed class Class160
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class161
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class162
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[15];
				switch (this.order_0.BuyType)
				{
				case TradeTypes.LiveSearch:
				{
					List<LiveSearchListItem> list = Class255.LiveSearchList.ToList<LiveSearchListItem>();
					IEnumerable<LiveSearchListItem> liveSearchList = Class255.LiveSearchList;
					Func<LiveSearchListItem, bool> predicate;
					if ((predicate = this.func_0) == null)
					{
						predicate = (this.func_0 = new Func<LiveSearchListItem, bool>(this.method_1));
					}
					LiveSearchListItem liveSearchListItem = liveSearchList.FirstOrDefault(predicate);
					*(int*)ptr = list.IndexOf(liveSearchListItem);
					((byte*)ptr)[12] = ((liveSearchListItem != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						liveSearchListItem.Enabled = false;
					}
					CleanInventory.mainForm_0.dataGridView_1.Rows[*(int*)ptr].Cells[0].Value = false;
					list[*(int*)ptr] = liveSearchListItem;
					Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, true);
					break;
				}
				case TradeTypes.ItemBuying:
				{
					List<ItemBuyingListItem> list2 = Class255.ItemBuyingList.ToList<ItemBuyingListItem>();
					IEnumerable<ItemBuyingListItem> itemBuyingList = Class255.ItemBuyingList;
					Func<ItemBuyingListItem, bool> predicate2;
					if ((predicate2 = this.func_1) == null)
					{
						predicate2 = (this.func_1 = new Func<ItemBuyingListItem, bool>(this.method_2));
					}
					ItemBuyingListItem itemBuyingListItem = itemBuyingList.FirstOrDefault(predicate2);
					*(int*)((byte*)ptr + 4) = list2.IndexOf(itemBuyingListItem);
					((byte*)ptr)[13] = ((itemBuyingListItem != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						itemBuyingListItem.Enabled = false;
					}
					CleanInventory.mainForm_0.dataGridView_3.Rows[*(int*)((byte*)ptr + 4)].Cells[0].Value = false;
					list2[*(int*)((byte*)ptr + 4)] = itemBuyingListItem;
					Class255.class105_0.method_9(ConfigOptions.ItemBuyingList, list2, true);
					break;
				}
				case TradeTypes.BulkBuying:
				{
					List<BulkBuyingListItem> list3 = Class255.BulkBuyingList.ToList<BulkBuyingListItem>();
					IEnumerable<BulkBuyingListItem> bulkBuyingList = Class255.BulkBuyingList;
					Func<BulkBuyingListItem, bool> predicate3;
					if ((predicate3 = this.func_2) == null)
					{
						predicate3 = (this.func_2 = new Func<BulkBuyingListItem, bool>(this.method_3));
					}
					BulkBuyingListItem bulkBuyingListItem = bulkBuyingList.FirstOrDefault(predicate3);
					*(int*)((byte*)ptr + 8) = list3.IndexOf(bulkBuyingListItem);
					((byte*)ptr)[14] = ((bulkBuyingListItem != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						bulkBuyingListItem.Enabled = false;
					}
					CleanInventory.mainForm_0.dataGridView_2.Rows[*(int*)((byte*)ptr + 8)].Cells[0].Value = false;
					list3[*(int*)((byte*)ptr + 8)] = bulkBuyingListItem;
					Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, list3, true);
					break;
				}
				}
			}

			internal bool method_1(LiveSearchListItem liveSearchListItem_0)
			{
				return liveSearchListItem_0.Id == this.order_0.BuySettings.Id;
			}

			internal bool method_2(ItemBuyingListItem itemBuyingListItem_0)
			{
				return itemBuyingListItem_0.Id == this.order_0.BuySettings.Id;
			}

			internal bool method_3(BulkBuyingListItem bulkBuyingListItem_0)
			{
				return bulkBuyingListItem_0.QueryId == this.order_0.BuySettings.Id;
			}

			public Order order_0;

			public Func<LiveSearchListItem, bool> func_0;

			public Func<ItemBuyingListItem, bool> func_1;

			public Func<BulkBuyingListItem, bool> func_2;
		}
	}
}
