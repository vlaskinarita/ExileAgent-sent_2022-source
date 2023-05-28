using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ns14;
using ns24;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Managers
{
	public static class InventoryManager
	{
		public unsafe static bool smethod_0(Order order_0, List<Order> list_0, out JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[5];
			InventoryManager.Class335 @class = new InventoryManager.Class335();
			@class.order_0 = order_0;
			jsonItem_0 = null;
			if (@class.order_0.left_pos == 0 || @class.order_0.top_pos == 0)
			{
				Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107274629), @class.order_0.left_pos, @class.order_0.top_pos));
				*(byte*)ptr = 0;
			}
			else
			{
				jsonItem_0 = StashManager.smethod_10(@class.order_0.stash.i, @class.order_0.left_pos - 1, @class.order_0.top_pos - 1);
				if (jsonItem_0 == null || (!jsonItem_0.Name.ToLower().Contains(@class.order_0.my_item_name.ToLower()) && !@class.order_0.my_item_name.ToLower().Contains(jsonItem_0.Name.ToLower())))
				{
					((byte*)ptr)[1] = ((jsonItem_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Class181.smethod_3(Enum11.const_3, InventoryManager.getString_0(107274604));
						*(byte*)ptr = 0;
					}
					else if (jsonItem_0.IsUnique && @class.order_0.my_item_name.ToLower().Contains(jsonItem_0.Name.Replace(InventoryManager.getString_0(107369422), InventoryManager.getString_0(107399098)).ToLower()))
					{
						*(byte*)ptr = 1;
					}
					else
					{
						Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107274591), jsonItem_0.Name, @class.order_0.my_item_name));
						*(byte*)ptr = 0;
					}
				}
				else
				{
					((byte*)ptr)[2] = (list_0.Where(new Func<Order, bool>(@class.method_0)).Any<Order>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_3(Enum11.const_3, InventoryManager.getString_0(107274566));
						*(byte*)ptr = 0;
					}
					else
					{
						((byte*)ptr)[3] = ((API.smethod_7(@class.order_0.my_item_name).Type != InventoryManager.getString_0(107373365)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							int num = list_0.Where(new Func<Order, bool>(@class.method_1)).Sum(new Func<Order, int>(InventoryManager.<>c.<>9.method_0));
							string text;
							Dictionary<JsonTab, List<JsonItem>> source = StashManager.smethod_2(@class.order_0.my_item_name, 0, @class.order_0.OurMapTier, @class.order_0.OurItemUnique, false, Class252.smethod_4(@class.order_0), out text, out *(bool*)((byte*)ptr + 4), false);
							if (source.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(InventoryManager.<>c.<>9.method_1)) < num + @class.order_0.my_item_amount_floor)
							{
								Class181.smethod_3(Enum11.const_3, InventoryManager.getString_0(107275033));
								*(byte*)ptr = 0;
								goto IL_2F2;
							}
						}
						*(byte*)ptr = 1;
					}
				}
			}
			IL_2F2:
			return *(sbyte*)ptr != 0;
		}

		public unsafe static bool smethod_1(int int_0, Order order_0, string string_0, List<Order> list_0, out Dictionary<JsonTab, List<JsonItem>> dictionary_0, out int int_1, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[3];
			InventoryManager.Class336 @class = new InventoryManager.Class336();
			@class.order_0 = order_0;
			int_1 = list_0.Where(new Func<Order, bool>(@class.method_0)).Sum(new Func<Order, int>(InventoryManager.<>c.<>9.method_3));
			string text;
			dictionary_0 = StashManager.smethod_2(@class.order_0.my_item_name, int_0, @class.order_0.OurMapTier, @class.order_0.OurItemUnique, false, string_0, out text, out *(bool*)ptr, bool_0);
			((byte*)ptr)[1] = ((dictionary_0.smethod_16(false) < int_1 + @class.order_0.my_item_amount_floor) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				((byte*)ptr)[2] = 0;
			}
			else
			{
				((byte*)ptr)[2] = 1;
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		private unsafe static bool smethod_2(Order order_0, string string_0, bool bool_0)
		{
			void* ptr = stackalloc byte[5];
			if (order_0.left_pos != 0 && order_0.top_pos != 0)
			{
				JsonItem jsonItem = StashManager.smethod_10(order_0.stash.i, order_0.left_pos - 1, order_0.top_pos - 1);
				*(byte*)ptr = ((jsonItem == null) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					string text;
					Dictionary<JsonTab, List<JsonItem>> dictionary = StashManager.smethod_2(order_0.my_item_name, 1, order_0.OurMapTier, order_0.OurItemUnique, false, string_0, out text, out *(bool*)((byte*)ptr + 1), false);
					((byte*)ptr)[2] = (dictionary.Any<KeyValuePair<JsonTab, List<JsonItem>>>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						Class181.smethod_3(Enum11.const_2, string.Format(InventoryManager.getString_0(107274935), new object[]
						{
							order_0.my_item_name,
							order_0.OurMapTier,
							order_0.OurItemUnique,
							order_0.left_pos,
							order_0.top_pos,
							string_0
						}));
						Class181.smethod_3(Enum11.const_2, InventoryManager.getString_0(107274850));
						((byte*)ptr)[3] = 0;
						goto IL_27C;
					}
					order_0.my_items = dictionary;
					order_0.stash = dictionary.Keys.First<JsonTab>();
					order_0.left_pos = dictionary.Values.First<List<JsonItem>>().First<JsonItem>().x + 1;
					order_0.top_pos = dictionary.Values.First<List<JsonItem>>().First<JsonItem>().y + 1;
					Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107275052), order_0.left_pos, order_0.top_pos, dictionary.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>()));
				}
				else
				{
					order_0.my_items.Add(order_0.stash, new List<JsonItem>
					{
						jsonItem
					});
				}
			}
			else
			{
				string text;
				Dictionary<JsonTab, List<JsonItem>> dictionary2 = StashManager.smethod_2(order_0.my_item_name, order_0.my_item_amount_floor, order_0.OurMapTier, order_0.OurItemUnique, false, string_0, out text, out *(bool*)((byte*)ptr + 1), bool_0);
				((byte*)ptr)[4] = ((dictionary2.Count == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					Class181.smethod_3(Enum11.const_2, string.Format(InventoryManager.getString_0(107274245), new object[]
					{
						order_0.my_item_name,
						order_0.my_item_amount_floor,
						order_0.OurMapTier,
						order_0.OurItemUnique,
						string_0
					}));
					Class181.smethod_3(Enum11.const_2, InventoryManager.getString_0(107274850));
					((byte*)ptr)[3] = 0;
					goto IL_27C;
				}
				order_0.my_items = dictionary2;
			}
			((byte*)ptr)[3] = 1;
			IL_27C:
			return *(sbyte*)((byte*)ptr + 3) != 0;
		}

		public unsafe static List<JsonItem> smethod_3(Order order_0, string string_0, bool bool_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!UI.smethod_13(1)) ? 1 : 0);
			List<JsonItem> result;
			if (*(sbyte*)ptr != 0)
			{
				result = new List<JsonItem>();
			}
			else
			{
				((byte*)ptr)[1] = ((!InventoryManager.smethod_2(order_0, string_0, bool_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = new List<JsonItem>();
				}
				else
				{
					((byte*)ptr)[2] = ((!string.IsNullOrEmpty(order_0.messageToSay)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						UI.smethod_39(order_0.player, order_0.messageToSay);
					}
					result = InventoryManager.smethod_4(order_0, list_0);
				}
			}
			return result;
		}

		public unsafe static List<JsonItem> smethod_4(Order order_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[59];
			List<JsonItem> list = new List<JsonItem>();
			*(int*)ptr = order_0.my_item_amount_floor;
			List<JsonItem> list2 = new List<JsonItem>(list_0);
			Position position = null;
			foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair in order_0.my_items)
			{
				UI.smethod_35(keyValuePair.Key.i, false, 1);
				((byte*)ptr)[40] = (keyValuePair.Key.IsSpecialTab ? 1 : 0);
				List<JsonItem> list3;
				if (*(sbyte*)((byte*)ptr + 40) != 0)
				{
					list3 = keyValuePair.Value.OrderByDescending(new Func<JsonItem, int>(InventoryManager.<>c.<>9.method_4)).ToList<JsonItem>();
				}
				else
				{
					list3 = keyValuePair.Value.OrderBy(new Func<JsonItem, int>(InventoryManager.<>c.<>9.method_5)).ToList<JsonItem>();
				}
				for (;;)
				{
					((byte*)ptr)[56] = ((*(int*)ptr > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 56) == 0)
					{
						break;
					}
					((byte*)ptr)[41] = ((!list3.Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 41) != 0)
					{
						break;
					}
					JsonItem jsonItem = list3.First<JsonItem>();
					((byte*)ptr)[42] = ((!order_0.my_inventory_items.ContainsKey(keyValuePair.Key)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 42) != 0)
					{
						order_0.my_inventory_items.Add(keyValuePair.Key, 0);
					}
					((byte*)ptr)[43] = ((jsonItem.stack == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 43) != 0)
					{
						jsonItem.stack = 1;
					}
					((byte*)ptr)[44] = ((jsonItem.stack_size == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 44) != 0)
					{
						jsonItem.stack_size = 1;
					}
					Position position2 = InventoryManager.smethod_15(keyValuePair.Key, jsonItem.Left, jsonItem.Top);
					if (jsonItem.stack_size == 1 || jsonItem.stack == 1)
					{
						Win32.smethod_9();
						Enum11 enum11_ = Enum11.const_3;
						string str = InventoryManager.getString_0(107274124);
						*(int*)((byte*)ptr + 4) = jsonItem.Left;
						string str2 = ((int*)((byte*)ptr + 4))->ToString();
						string str3 = InventoryManager.getString_0(107394722);
						*(int*)((byte*)ptr + 4) = jsonItem.Top;
						Class181.smethod_3(enum11_, str + str2 + str3 + ((int*)((byte*)ptr + 4))->ToString());
						*(int*)ptr = *(int*)ptr - 1;
						Dictionary<JsonTab, int> my_inventory_items = order_0.my_inventory_items;
						JsonTab key = keyValuePair.Key;
						my_inventory_items[key]++;
						list3.RemoveAt(0);
						Stashes.Items[keyValuePair.Key.i].Remove(jsonItem);
						list2.Add(jsonItem.method_2());
						Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107274063), new object[]
						{
							jsonItem.Name,
							jsonItem.x,
							jsonItem.y,
							keyValuePair.Key.n
						}));
					}
					else
					{
						*(int*)((byte*)ptr + 8) = jsonItem.stack;
						((byte*)ptr)[45] = ((jsonItem.stack > *(int*)ptr) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 45) != 0)
						{
							*(int*)((byte*)ptr + 12) = (int)Math.Floor((double)(*(int*)ptr) / (double)jsonItem.BaseItemStackSize);
							*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 12) * jsonItem.BaseItemStackSize;
							*(int*)((byte*)ptr + 20) = *(int*)ptr - *(int*)((byte*)ptr + 16);
							JsonItem jsonItem2 = jsonItem.method_2();
							jsonItem2.stack = *(int*)((byte*)ptr + 16) + *(int*)((byte*)ptr + 20);
							list2.Add(jsonItem2);
							((byte*)ptr)[46] = ((*(int*)((byte*)ptr + 12) > 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 46) != 0)
							{
								Class181.smethod_2(Enum11.const_3, InventoryManager.getString_0(107274534), new object[]
								{
									*(int*)((byte*)ptr + 16),
									jsonItem.Name,
									jsonItem.Left,
									jsonItem.Top,
									*(int*)((byte*)ptr + 12)
								});
								InventoryManager.smethod_11(position2, jsonItem.stack, *(int*)((byte*)ptr + 12), jsonItem, list_0);
								JsonItem jsonItem3 = StashManager.smethod_10(keyValuePair.Key.i, jsonItem.x, jsonItem.y);
								jsonItem3.stack -= *(int*)((byte*)ptr + 16);
								*(int*)ptr = *(int*)ptr - *(int*)((byte*)ptr + 16);
								Dictionary<JsonTab, int> my_inventory_items = order_0.my_inventory_items;
								JsonTab key = keyValuePair.Key;
								my_inventory_items[key] += *(int*)((byte*)ptr + 16);
							}
							((byte*)ptr)[47] = ((*(int*)((byte*)ptr + 20) > 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 47) != 0)
							{
								UI.smethod_45(jsonItem, *(int*)ptr);
								Class181.smethod_2(Enum11.const_3, InventoryManager.getString_0(107274429), new object[]
								{
									list2.Count
								});
								list = InventoryManager.smethod_5(list2);
								((byte*)ptr)[48] = ((order_0.my_item_amount_floor > jsonItem.BaseItemStackSize) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 48) != 0)
								{
									((byte*)ptr)[49] = (list.Any<JsonItem>() ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 49) != 0)
									{
										*(int*)((byte*)ptr + 24) = list.Count - 1;
										JsonItem jsonItem4 = list[*(int*)((byte*)ptr + 24) - 1];
										JsonItem jsonItem5 = list[*(int*)((byte*)ptr + 24)];
										((byte*)ptr)[50] = ((jsonItem5.stack < *(int*)ptr) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 50) != 0)
										{
											UI.smethod_32(jsonItem4.x, jsonItem4.y, Enum2.const_3, true);
											Win32.smethod_2(true);
										}
									}
								}
								position2 = new Position();
								((byte*)ptr)[51] = (list.Any<JsonItem>() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 51) != 0)
								{
									position2.Left = list.Last<JsonItem>().x;
									position2.Top = list.Last<JsonItem>().y;
								}
								else
								{
									position2.Left = 0;
									position2.Top = 0;
								}
								UI.smethod_32(position2.Left, position2.Top, Enum2.const_3, true);
								Thread.Sleep(100);
								Win32.smethod_2(true);
								UI.smethod_32(0, -2, Enum2.const_3, true);
								Thread.Sleep(1000);
								for (;;)
								{
									Position position3;
									((byte*)ptr)[54] = ((!UI.smethod_3(out position3, Images.InventoryPatch, InventoryManager.getString_0(107396054))) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 54) == 0)
									{
										break;
									}
									((byte*)ptr)[52] = (UI.smethod_3(out position3, Images.KeepDestroyWindow, InventoryManager.getString_0(107396137)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 52) != 0)
									{
										Win32.smethod_14(InventoryManager.getString_0(107396092), false);
										Thread.Sleep(200);
									}
									UI.smethod_32(0, -2, Enum2.const_3, true);
									*(int*)((byte*)ptr + 28) = UI.smethod_83(12);
									List<JsonItem> list4 = new List<JsonItem>();
									*(int*)((byte*)ptr + 32) = 0;
									for (;;)
									{
										((byte*)ptr)[53] = ((*(int*)((byte*)ptr + 32) < *(int*)((byte*)ptr + 28)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 53) == 0)
										{
											break;
										}
										JsonItem item = new JsonItem
										{
											w = 1,
											h = 1
										};
										list4.Add(item);
										*(int*)((byte*)ptr + 32) = *(int*)((byte*)ptr + 32) + 1;
									}
									list4 = InventoryManager.smethod_5(list4);
									position = InventoryManager.smethod_8(list4, list.Last<JsonItem>()).First<Position>();
									UI.smethod_32(position.Left, position.Top, Enum2.const_3, true);
									Thread.Sleep(100);
									Win32.smethod_2(true);
								}
								JsonItem jsonItem6 = StashManager.smethod_10(keyValuePair.Key.i, jsonItem.x, jsonItem.y);
								((byte*)ptr)[55] = ((jsonItem6.stack == *(int*)ptr) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 55) != 0)
								{
									Dictionary<JsonTab, int> my_inventory_items = order_0.my_inventory_items;
									JsonTab key = keyValuePair.Key;
									my_inventory_items[key] += *(int*)ptr;
									Stashes.Items[keyValuePair.Key.i].Remove(jsonItem6);
									Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107274444), new object[]
									{
										jsonItem6.Name,
										jsonItem6.x,
										jsonItem6.y,
										keyValuePair.Key.n
									}));
								}
								else
								{
									jsonItem6.stack -= *(int*)ptr;
									Dictionary<JsonTab, int> my_inventory_items = order_0.my_inventory_items;
									JsonTab key = keyValuePair.Key;
									my_inventory_items[key] += *(int*)ptr;
									Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107274375), new object[]
									{
										jsonItem6.Name,
										jsonItem6.x,
										jsonItem6.y,
										jsonItem6.stack + *(int*)ptr,
										jsonItem6.stack,
										keyValuePair.Key.n
									}));
								}
							}
							*(int*)ptr = 0;
						}
						else
						{
							*(int*)ptr = *(int*)ptr - jsonItem.stack;
							Dictionary<JsonTab, int> my_inventory_items = order_0.my_inventory_items;
							JsonTab key = keyValuePair.Key;
							my_inventory_items[key] += jsonItem.stack;
							*(int*)((byte*)ptr + 36) = (int)Math.Ceiling((double)jsonItem.stack / (double)jsonItem.BaseItemStackSize);
							Class181.smethod_2(Enum11.const_3, InventoryManager.getString_0(107273770), new object[]
							{
								jsonItem.Left,
								jsonItem.Top,
								*(int*)((byte*)ptr + 36)
							});
							InventoryManager.smethod_10(position2, jsonItem.stack, *(int*)((byte*)ptr + 36), jsonItem.BaseItemStackSize);
							list3.RemoveAt(0);
							Stashes.Items[keyValuePair.Key.i].Remove(jsonItem);
							list2.Add(jsonItem);
							Class181.smethod_3(Enum11.const_3, string.Format(InventoryManager.getString_0(107273681), new object[]
							{
								jsonItem.Name,
								jsonItem.x,
								jsonItem.y,
								keyValuePair.Key.n,
								jsonItem.stack
							}));
						}
					}
				}
				((byte*)ptr)[57] = ((*(int*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 57) != 0)
				{
					break;
				}
			}
			list = InventoryManager.smethod_5(list2);
			((byte*)ptr)[58] = (Position.smethod_1(position, null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 58) != 0)
			{
				list.Last<JsonItem>().x = position.x;
				list.Last<JsonItem>().y = position.y;
			}
			Win32.smethod_4(-2, -2, 50, 90, false);
			return list;
		}

		public unsafe static List<JsonItem> smethod_5(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[15];
			List<JsonItem> list = new List<JsonItem>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (JsonItem jsonItem in list_0)
			{
				((byte*)ptr)[8] = ((!dictionary.ContainsKey(InventoryManager.smethod_14(jsonItem))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					dictionary.Add(InventoryManager.smethod_14(jsonItem), 0);
				}
				Dictionary<string, int> dictionary2 = dictionary;
				string key = InventoryManager.smethod_14(jsonItem);
				dictionary2[key] += jsonItem.stack;
			}
			foreach (JsonItem jsonItem2 in list_0)
			{
				for (;;)
				{
					((byte*)ptr)[12] = (dictionary.ContainsKey(InventoryManager.smethod_14(jsonItem2)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) == 0)
					{
						break;
					}
					JsonItem jsonItem3 = jsonItem2.method_2();
					List<Position> source = InventoryManager.smethod_8(list, jsonItem3);
					((byte*)ptr)[9] = (source.Any<Position>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						Position position = source.First<Position>();
						jsonItem3.x = position.Left;
						jsonItem3.y = position.Top;
						((byte*)ptr)[10] = ((dictionary[InventoryManager.smethod_14(jsonItem2)] >= jsonItem2.BaseItemStackSize) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							jsonItem3.stack = jsonItem2.BaseItemStackSize;
						}
						else
						{
							jsonItem3.stack = dictionary[InventoryManager.smethod_14(jsonItem2)];
						}
						list.Add(jsonItem3);
					}
					Dictionary<string, int> dictionary2 = dictionary;
					string key = InventoryManager.smethod_14(jsonItem2);
					dictionary2[key] -= jsonItem2.BaseItemStackSize;
					((byte*)ptr)[11] = ((dictionary[InventoryManager.smethod_14(jsonItem2)] <= 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						dictionary.Remove(InventoryManager.smethod_14(jsonItem2));
					}
				}
			}
			int[,] array = InventoryManager.smethod_7(list);
			string text = InventoryManager.getString_0(107399098);
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[14] = ((*(int*)ptr < 5) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) == 0)
				{
					break;
				}
				text += InventoryManager.getString_0(107371089);
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[13] = ((*(int*)((byte*)ptr + 4) < 12) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) == 0)
					{
						break;
					}
					text = text + array[*(int*)((byte*)ptr + 4), *(int*)ptr].ToString() + InventoryManager.getString_0(107394722);
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				text = text.Substring(0, text.Length - 1);
				text += InventoryManager.getString_0(107371116);
				Class181.smethod_3(Enum11.const_3, text);
				text = InventoryManager.getString_0(107399098);
				*(int*)ptr = *(int*)ptr + 1;
			}
			return list;
		}

		public unsafe static List<JsonItem> smethod_6(List<Item> list_0)
		{
			void* ptr = stackalloc byte[5];
			List<JsonItem> list = new List<JsonItem>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (Item item in list_0)
			{
				*(byte*)ptr = ((!dictionary.ContainsKey(InventoryManager.smethod_13(item))) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					dictionary.Add(InventoryManager.smethod_13(item), 0);
				}
				Dictionary<string, int> dictionary2 = dictionary;
				string key = InventoryManager.smethod_13(item);
				dictionary2[key] += item.stack;
			}
			foreach (Item item2 in list_0)
			{
				((byte*)ptr)[1] = ((!dictionary.ContainsKey(InventoryManager.smethod_13(item2))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					JsonItem jsonItem = new JsonItem(item2, 0, 0);
					List<Position> source = InventoryManager.smethod_8(list, jsonItem);
					((byte*)ptr)[2] = (source.Any<Position>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Position position = source.First<Position>();
						jsonItem.x = position.Left;
						jsonItem.y = position.Top;
						((byte*)ptr)[3] = ((dictionary[InventoryManager.smethod_13(item2)] >= item2.stack_size) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							jsonItem.stack = item2.stack_size;
						}
						else
						{
							jsonItem.stack = dictionary[InventoryManager.smethod_13(item2)];
						}
						list.Add(jsonItem);
					}
					Dictionary<string, int> dictionary2 = dictionary;
					string key = InventoryManager.smethod_13(item2);
					dictionary2[key] -= item2.stack_size;
					((byte*)ptr)[4] = ((dictionary[InventoryManager.smethod_13(item2)] <= 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						dictionary.Remove(InventoryManager.smethod_13(item2));
					}
				}
			}
			int[,] int_ = InventoryManager.smethod_7(list);
			Util.smethod_17(int_, true);
			return list;
		}

		public unsafe static int[,] smethod_7(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[18];
			int[,] array = new int[12, 5];
			InventoryManager.Class337 @class = new InventoryManager.Class337();
			@class.int_0 = 0;
			for (;;)
			{
				((byte*)ptr)[17] = ((@class.int_0 < array.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) == 0)
				{
					break;
				}
				InventoryManager.Class338 class2 = new InventoryManager.Class338();
				class2.class337_0 = @class;
				class2.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[16] = ((class2.int_0 < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) == 0)
					{
						break;
					}
					((byte*)ptr)[12] = ((array[class2.int_0, class2.class337_0.int_0] != 1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						JsonItem jsonItem = list_0.FirstOrDefault(new Func<JsonItem, bool>(class2.method_0));
						((byte*)ptr)[13] = ((jsonItem != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							*(int*)ptr = 0;
							for (;;)
							{
								((byte*)ptr)[15] = ((*(int*)ptr < jsonItem.h) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 15) == 0)
								{
									break;
								}
								*(int*)((byte*)ptr + 4) = 0;
								for (;;)
								{
									((byte*)ptr)[14] = ((*(int*)((byte*)ptr + 4) < jsonItem.w) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 14) == 0)
									{
										break;
									}
									array[*(int*)((byte*)ptr + 4) + class2.int_0, *(int*)ptr + class2.class337_0.int_0] = 1;
									*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
								}
								*(int*)ptr = *(int*)ptr + 1;
							}
						}
						else
						{
							array[class2.int_0, class2.class337_0.int_0] = 0;
						}
					}
					*(int*)((byte*)ptr + 8) = class2.int_0;
					class2.int_0 = *(int*)((byte*)ptr + 8) + 1;
				}
				*(int*)((byte*)ptr + 8) = @class.int_0;
				@class.int_0 = *(int*)((byte*)ptr + 8) + 1;
			}
			return array;
		}

		public unsafe static List<Position> smethod_8(List<JsonItem> list_0, JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[34];
			int[,] array = InventoryManager.smethod_7(list_0);
			List<Position> list = new List<Position>();
			*(int*)ptr = jsonItem_0.w;
			*(int*)((byte*)ptr + 4) = jsonItem_0.h;
			*(int*)((byte*)ptr + 8) = 0;
			for (;;)
			{
				((byte*)ptr)[33] = ((*(int*)((byte*)ptr + 8) < array.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 33) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = 0;
				for (;;)
				{
					((byte*)ptr)[32] = ((*(int*)((byte*)ptr + 12) < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 32) == 0)
					{
						break;
					}
					((byte*)ptr)[28] = ((array[*(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 8)] == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 28) != 0 && *(int*)((byte*)ptr + 12) + *(int*)ptr <= array.GetLength(0) && *(int*)((byte*)ptr + 8) + *(int*)((byte*)ptr + 4) <= array.GetLength(1))
					{
						*(int*)((byte*)ptr + 16) = 0;
						*(int*)((byte*)ptr + 20) = 0;
						for (;;)
						{
							((byte*)ptr)[30] = ((*(int*)((byte*)ptr + 20) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 30) == 0)
							{
								break;
							}
							*(int*)((byte*)ptr + 24) = 0;
							for (;;)
							{
								((byte*)ptr)[29] = ((*(int*)((byte*)ptr + 24) < *(int*)ptr) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 29) == 0)
								{
									break;
								}
								*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + array[*(int*)((byte*)ptr + 24) + *(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 20) + *(int*)((byte*)ptr + 8)];
								*(int*)((byte*)ptr + 24) = *(int*)((byte*)ptr + 24) + 1;
							}
							*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
						}
						((byte*)ptr)[31] = ((*(int*)((byte*)ptr + 16) == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 31) != 0)
						{
							list.Add(new Position(*(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 8)));
						}
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return list.OrderBy(new Func<Position, int>(InventoryManager.<>c.<>9.method_6)).ThenBy(new Func<Position, int>(InventoryManager.<>c.<>9.method_7)).ToList<Position>();
		}

		public unsafe static List<Position> smethod_9(List<Item> list_0, Item item_0)
		{
			void* ptr = stackalloc byte[34];
			List<JsonItem> list_ = InventoryManager.smethod_6(list_0);
			int[,] array = InventoryManager.smethod_7(list_);
			List<Position> list = new List<Position>();
			*(int*)ptr = item_0.width;
			*(int*)((byte*)ptr + 4) = item_0.height;
			*(int*)((byte*)ptr + 8) = 0;
			for (;;)
			{
				((byte*)ptr)[33] = ((*(int*)((byte*)ptr + 8) < array.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 33) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 12) = 0;
				for (;;)
				{
					((byte*)ptr)[32] = ((*(int*)((byte*)ptr + 12) < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 32) == 0)
					{
						break;
					}
					((byte*)ptr)[28] = ((array[*(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 8)] == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 28) != 0 && *(int*)((byte*)ptr + 12) + *(int*)ptr <= array.GetLength(0) && *(int*)((byte*)ptr + 8) + *(int*)((byte*)ptr + 4) <= array.GetLength(1))
					{
						*(int*)((byte*)ptr + 16) = 0;
						*(int*)((byte*)ptr + 20) = 0;
						for (;;)
						{
							((byte*)ptr)[30] = ((*(int*)((byte*)ptr + 20) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 30) == 0)
							{
								break;
							}
							*(int*)((byte*)ptr + 24) = 0;
							for (;;)
							{
								((byte*)ptr)[29] = ((*(int*)((byte*)ptr + 24) < *(int*)ptr) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 29) == 0)
								{
									break;
								}
								*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + array[*(int*)((byte*)ptr + 24) + *(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 20) + *(int*)((byte*)ptr + 8)];
								*(int*)((byte*)ptr + 24) = *(int*)((byte*)ptr + 24) + 1;
							}
							*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
						}
						((byte*)ptr)[31] = ((*(int*)((byte*)ptr + 16) == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 31) != 0)
						{
							list.Add(new Position(*(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 8)));
						}
					}
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
			}
			return list.OrderBy(new Func<Position, int>(InventoryManager.<>c.<>9.method_8)).ThenBy(new Func<Position, int>(InventoryManager.<>c.<>9.method_9)).ToList<Position>();
		}

		private unsafe static void smethod_10(Position position_0, int int_0, int int_1, int int_2)
		{
			void* ptr = stackalloc byte[14];
			((byte*)ptr)[8] = 0;
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[13] = ((*(int*)ptr < int_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) == 0)
				{
					break;
				}
				Win32.smethod_9();
				UI.smethod_36(Enum2.const_2, (*(sbyte*)((byte*)ptr + 8) != 0) ? 1.0 : 0.25);
				((byte*)ptr)[9] = ((int_1 == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					break;
				}
				((byte*)ptr)[10] = ((*(int*)ptr == int_1 - 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					((byte*)ptr)[8] = 1;
					Win32.smethod_4(-2, -2, 50, 90, false);
					UI.smethod_36(Enum2.const_2, 1.5);
					Win32.smethod_5(position_0, false);
					UI.smethod_36(Enum2.const_2, 1.5);
					Item item = new Item
					{
						text = Win32.smethod_21()
					};
					item.method_0();
					((byte*)ptr)[11] = ((item.text != InventoryManager.getString_0(107383724)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 4) = (int_0 - item.stack) / int_2;
					Class181.smethod_2(Enum11.const_3, InventoryManager.getString_0(107273592), new object[]
					{
						int_0,
						item.stack,
						int_1 - *(int*)((byte*)ptr + 4)
					});
					((byte*)ptr)[12] = ((*(int*)((byte*)ptr + 4) < int_1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						*(int*)ptr = *(int*)((byte*)ptr + 4) - 1;
					}
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
		}

		private unsafe static void smethod_11(Position position_0, int int_0, int int_1, JsonItem jsonItem_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[24];
			((byte*)ptr)[16] = 0;
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[23] = ((*(int*)ptr < int_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 23) == 0)
				{
					break;
				}
				Win32.smethod_9();
				UI.smethod_36(Enum2.const_2, (*(sbyte*)((byte*)ptr + 16) != 0) ? 1.0 : 0.25);
				((byte*)ptr)[17] = ((*(int*)ptr == int_1 - 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					((byte*)ptr)[16] = 1;
					Win32.smethod_4(-2, -2, 50, 90, false);
					UI.smethod_36(Enum2.const_2, 1.5);
					Win32.smethod_5(position_0, false);
					UI.smethod_36(Enum2.const_2, 1.5);
					Item item = new Item
					{
						text = Win32.smethod_21()
					};
					item.method_0();
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[19] = ((*(int*)((byte*)ptr + 4) < 2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 19) == 0)
						{
							break;
						}
						((byte*)ptr)[18] = ((item.text == InventoryManager.getString_0(107383724)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 18) == 0)
						{
							break;
						}
						Class181.smethod_3(Enum11.const_3, InventoryManager.getString_0(107274003));
						item.text = Win32.smethod_21();
						item.method_0();
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					((byte*)ptr)[20] = ((item.text == InventoryManager.getString_0(107383724)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) != 0)
					{
						goto IL_26B;
					}
					if (jsonItem_0.stackSize > 1 && API.smethod_7(jsonItem_0.Name).Stack == 1)
					{
						*(int*)((byte*)ptr + 12) = UI.smethod_83(12);
						*(int*)((byte*)ptr + 8) = int_1 - *(int*)((byte*)ptr + 12) + list_0.Count;
						Class181.smethod_2(Enum11.const_3, InventoryManager.getString_0(107273925), new object[]
						{
							int_0,
							*(int*)((byte*)ptr + 12),
							*(int*)((byte*)ptr + 8)
						});
						((byte*)ptr)[21] = ((*(int*)((byte*)ptr + 8) > 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 21) != 0)
						{
							Win32.smethod_5(position_0, false);
							*(int*)ptr = *(int*)ptr - *(int*)((byte*)ptr + 8);
						}
					}
					else
					{
						*(int*)((byte*)ptr + 8) = (int_0 - item.stack) / jsonItem_0.BaseItemStackSize;
						Class181.smethod_2(Enum11.const_3, InventoryManager.getString_0(107273592), new object[]
						{
							int_0,
							item.stack,
							int_1 - *(int*)((byte*)ptr + 8)
						});
						((byte*)ptr)[22] = ((*(int*)((byte*)ptr + 8) < int_1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 22) != 0)
						{
							*(int*)ptr = *(int*)((byte*)ptr + 8) - 1;
						}
					}
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			return;
			IL_26B:
			Class181.smethod_3(Enum11.const_3, InventoryManager.getString_0(107273990));
		}

		public unsafe static int[,] smethod_12(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[24];
			int[,] array = new int[12, 5];
			InventoryManager.Class339 @class = new InventoryManager.Class339();
			@class.int_0 = 0;
			for (;;)
			{
				((byte*)ptr)[23] = ((@class.int_0 < array.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 23) == 0)
				{
					break;
				}
				InventoryManager.Class340 class2 = new InventoryManager.Class340();
				class2.class339_0 = @class;
				class2.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[22] = ((class2.int_0 < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 22) == 0)
					{
						break;
					}
					((byte*)ptr)[16] = ((array[class2.int_0, class2.class339_0.int_0] == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						JsonItem jsonItem = list_0.FirstOrDefault(new Func<JsonItem, bool>(class2.method_0));
						((byte*)ptr)[17] = ((jsonItem != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							*(int*)ptr = 0;
							for (;;)
							{
								((byte*)ptr)[21] = ((*(int*)ptr < list_0.Count) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 21) == 0)
								{
									break;
								}
								((byte*)ptr)[18] = ((list_0[*(int*)ptr] == jsonItem) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 18) != 0)
								{
									*(int*)((byte*)ptr + 4) = 0;
									for (;;)
									{
										((byte*)ptr)[20] = ((*(int*)((byte*)ptr + 4) < jsonItem.h) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 20) == 0)
										{
											break;
										}
										*(int*)((byte*)ptr + 8) = 0;
										for (;;)
										{
											((byte*)ptr)[19] = ((*(int*)((byte*)ptr + 8) < jsonItem.w) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 19) == 0)
											{
												break;
											}
											array[*(int*)((byte*)ptr + 8) + class2.int_0, *(int*)((byte*)ptr + 4) + class2.class339_0.int_0] = *(int*)ptr + 1;
											*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
										}
										*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
									}
								}
								*(int*)ptr = *(int*)ptr + 1;
							}
						}
						else
						{
							array[class2.int_0, class2.class339_0.int_0] = 0;
						}
					}
					*(int*)((byte*)ptr + 12) = class2.int_0;
					class2.int_0 = *(int*)((byte*)ptr + 12) + 1;
				}
				*(int*)((byte*)ptr + 12) = @class.int_0;
				@class.int_0 = *(int*)((byte*)ptr + 12) + 1;
			}
			return array;
		}

		private static string smethod_13(Item item_0)
		{
			return item_0.Name + item_0.method_2().UniqueIdentifiers;
		}

		private static string smethod_14(JsonItem jsonItem_0)
		{
			return jsonItem_0.Name + jsonItem_0.UniqueIdentifiers;
		}

		private static Position smethod_15(JsonTab jsonTab_0, int int_0, int int_1)
		{
			Position position = UI.smethod_34(jsonTab_0.type, int_0, int_1, Enum2.const_2, false);
			JsonItem jsonItem = StashManager.smethod_10(jsonTab_0.i, int_0, int_1);
			Position result;
			if (jsonItem.BaseItemStackSize == 1)
			{
				result = position;
			}
			else
			{
				Item item = new Item(Win32.smethod_21());
				if (item.text == null || item.text == InventoryManager.getString_0(107383724))
				{
					result = position;
				}
				else
				{
					jsonItem.stack = item.stack;
					result = position;
				}
			}
			return result;
		}

		static InventoryManager()
		{
			Strings.CreateGetStringDelegate(typeof(InventoryManager));
		}

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class335
		{
			internal bool method_0(Order order_1)
			{
				return order_1.bool_2 && order_1.stash.i == this.order_0.stash.i && order_1.left_pos == this.order_0.left_pos && order_1.top_pos == this.order_0.top_pos;
			}

			internal bool method_1(Order order_1)
			{
				return order_1.bool_2 && order_1.my_item_name == this.order_0.my_item_name;
			}

			public Order order_0;
		}

		[CompilerGenerated]
		private sealed class Class336
		{
			internal bool method_0(Order order_1)
			{
				return order_1.bool_2 && order_1.my_item_name == this.order_0.my_item_name;
			}

			public Order order_0;
		}

		[CompilerGenerated]
		private sealed class Class337
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class338
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0 && jsonItem_0.y == this.class337_0.int_0;
			}

			public int int_0;

			public InventoryManager.Class337 class337_0;
		}

		[CompilerGenerated]
		private sealed class Class339
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class340
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0 && jsonItem_0.y == this.class339_0.int_0;
			}

			public int int_0;

			public InventoryManager.Class339 class339_0;
		}
	}
}
