using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ns0;
using ns24;
using ns29;
using ns30;
using ns35;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Managers
{
	public static class StashManager
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			StashManager.mainForm_0 = mainForm_1;
		}

		public static Dictionary<JsonTab, List<JsonItem>> smethod_1(string string_0, bool bool_0 = true)
		{
			string text;
			bool flag;
			return StashManager.smethod_2(string_0, 0, 0, false, bool_0, string.Empty, out text, out flag, true);
		}

		public unsafe static Dictionary<JsonTab, List<JsonItem>> smethod_2(string string_0, int int_0, int int_1, bool bool_0, bool bool_1, string string_1, out string string_2, out bool bool_2, bool bool_3 = false)
		{
			void* ptr = stackalloc byte[21];
			StashManager.Class309 @class = new StashManager.Class309();
			@class.string_0 = string_0;
			@class.bool_0 = bool_0;
			@class.string_1 = string_1;
			Dictionary<JsonTab, List<JsonItem>> dictionary = new Dictionary<JsonTab, List<JsonItem>>();
			Dictionary<int, List<JsonItem>> dictionary2 = Stashes.Items.Where(new Func<KeyValuePair<int, List<JsonItem>>, bool>(StashManager.<>c.<>9.method_0)).ToDictionary(new Func<KeyValuePair<int, List<JsonItem>>, int>(StashManager.<>c.<>9.method_1), new Func<KeyValuePair<int, List<JsonItem>>, List<JsonItem>>(StashManager.<>c.<>9.method_2));
			Dictionary<int, List<JsonItem>> dictionary3 = new Dictionary<int, List<JsonItem>>();
			@class.jsonItem_0 = null;
			bool_2 = false;
			string_2 = string.Empty;
			@class.string_0 = @class.string_0.Replace(StashManager.getString_0(107369333), StashManager.getString_0(107399009));
			Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107275015), new object[]
			{
				(int_0 == 0) ? StashManager.getString_0(107275466) : int_0.ToString(),
				@class.string_0,
				int_1,
				@class.bool_0,
				bool_1,
				@class.string_1,
				bool_3
			});
			((byte*)ptr)[4] = ((int_1 > 0) ? 1 : 0);
			Dictionary<JsonTab, List<JsonItem>> result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = StashManager.smethod_3(@class.string_0, int_0, int_1, @class.bool_0, bool_1, @class.string_1, out string_2, bool_3);
			}
			else if (@class.string_0.Contains(StashManager.getString_0(107370656)) && int_1 == 0)
			{
				result = StashManager.smethod_4(@class.string_0, int_0, @class.bool_0, bool_1, @class.string_1, out string_2, bool_3);
			}
			else
			{
				ApiItem apiItem = API.smethod_7(@class.string_0);
				Predicate<JsonItem> predicate;
				if (apiItem.Type != StashManager.getString_0(107373276) && !apiItem.Text.Contains(StashManager.getString_0(107371710)))
				{
					((byte*)ptr)[5] = (@class.string_0.Contains(StashManager.getString_0(107453015)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						predicate = new Predicate<JsonItem>(@class.method_0);
					}
					else
					{
						predicate = new Predicate<JsonItem>(@class.method_1);
					}
				}
				else
				{
					predicate = new Predicate<JsonItem>(@class.method_2);
				}
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in dictionary2)
				{
					StashManager.Class310 class2 = new StashManager.Class310();
					class2.class309_0 = @class;
					class2.jsonTab_0 = Stashes.smethod_11(keyValuePair.Key);
					((byte*)ptr)[6] = ((!bool_3) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						class2.class309_0.jsonItem_0 = dictionary2[class2.jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(Util.smethod_13<JsonItem>(new Predicate<JsonItem>[]
						{
							predicate,
							new Predicate<JsonItem>(class2, ldftn(method_0))
						}).Invoke));
						if (class2.jsonTab_0 == Stashes.smethod_13() || class2.jsonTab_0.IsExcluded)
						{
							bool_2 = true;
						}
						((byte*)ptr)[7] = ((class2.class309_0.jsonItem_0 != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							break;
						}
					}
					else
					{
						IEnumerable<JsonItem> source = dictionary2[class2.jsonTab_0.i];
						Predicate<JsonItem>[] array = new Predicate<JsonItem>[2];
						array[0] = predicate;
						array[1] = new Predicate<JsonItem>(StashManager.<>c.<>9.method_3);
						List<JsonItem> list = source.Where(new Func<JsonItem, bool>(Util.smethod_13<JsonItem>(array).Invoke)).ToList<JsonItem>();
						((byte*)ptr)[8] = (list.Any<JsonItem>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							dictionary3.Add(class2.jsonTab_0.i, list);
						}
					}
				}
				((byte*)ptr)[9] = ((!bool_3) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					((byte*)ptr)[10] = ((@class.jsonItem_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107275461), new object[]
						{
							@class.string_0
						});
						return new Dictionary<JsonTab, List<JsonItem>>();
					}
					string_2 = @class.jsonItem_0.note;
					foreach (KeyValuePair<int, List<JsonItem>> keyValuePair2 in dictionary2)
					{
						((byte*)ptr)[11] = (dictionary2.ContainsKey(keyValuePair2.Key) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							IEnumerable<JsonItem> source2 = dictionary2[keyValuePair2.Key];
							Predicate<JsonItem>[] array2 = new Predicate<JsonItem>[2];
							array2[0] = predicate;
							int num = 1;
							Predicate<JsonItem> predicate2;
							if ((predicate2 = @class.predicate_0) == null)
							{
								predicate2 = (@class.predicate_0 = new Predicate<JsonItem>(@class.method_3));
							}
							array2[num] = predicate2;
							IEnumerable<JsonItem> enumerable = source2.Where(new Func<JsonItem, bool>(Util.smethod_13<JsonItem>(array2).Invoke));
							((byte*)ptr)[12] = (enumerable.Any<JsonItem>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								((byte*)ptr)[13] = ((!dictionary3.ContainsKey(keyValuePair2.Key)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 13) != 0)
								{
									dictionary3.Add(keyValuePair2.Key, new List<JsonItem>());
								}
								dictionary3[keyValuePair2.Key].AddRange(enumerable);
							}
						}
					}
				}
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair3 in dictionary3.ToList<KeyValuePair<int, List<JsonItem>>>())
				{
					foreach (JsonItem item in keyValuePair3.Value)
					{
						JsonTab key = Stashes.smethod_11(keyValuePair3.Key);
						((byte*)ptr)[14] = ((!dictionary.ContainsKey(key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 14) != 0)
						{
							dictionary.Add(key, new List<JsonItem>());
						}
						dictionary[key].Add(item);
					}
				}
				((byte*)ptr)[15] = ((!bool_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair4 in dictionary.ToList<KeyValuePair<JsonTab, List<JsonItem>>>())
					{
						((byte*)ptr)[16] = (keyValuePair4.Key.IsExcluded ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) != 0)
						{
							dictionary.Remove(keyValuePair4.Key);
						}
					}
				}
				if (API.smethod_7(@class.string_0).Type == StashManager.getString_0(107396068) && Class255.class105_0.method_4(ConfigOptions.CurrencyOnlyFromTab))
				{
					JsonTab jsonTab = Stashes.smethod_14(StashManager.getString_0(107396068));
					Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275436));
					foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair5 in dictionary.ToList<KeyValuePair<JsonTab, List<JsonItem>>>())
					{
						JsonTab key2 = keyValuePair5.Key;
						((byte*)ptr)[17] = ((key2 != jsonTab) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							dictionary.Remove(key2);
						}
					}
				}
				bool flag;
				if (dictionary.Any<KeyValuePair<JsonTab, List<JsonItem>>>())
				{
					flag = (dictionary.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(StashManager.<>c.<>9.method_4)) < int_0);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107275299), new object[]
					{
						@class.string_0
					});
					result = new Dictionary<JsonTab, List<JsonItem>>();
				}
				else
				{
					((byte*)ptr)[18] = ((int_0 == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						Enum11 enum11_ = Enum11.const_3;
						string string_3 = StashManager.getString_0(107275306);
						object[] array3 = new object[2];
						array3[0] = dictionary.Sum(new Func<KeyValuePair<JsonTab, List<JsonItem>>, int>(StashManager.<>c.<>9.method_6));
						array3[1] = @class.string_0;
						Class181.smethod_2(enum11_, string_3, array3);
						result = dictionary;
					}
					else
					{
						int num2 = 0;
						Dictionary<JsonTab, List<JsonItem>> dictionary4 = new Dictionary<JsonTab, List<JsonItem>>();
						foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair6 in dictionary)
						{
							int num3 = keyValuePair6.Value.Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_8));
							Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275253) + num3.ToString());
							((byte*)ptr)[19] = ((num2 + num3 < int_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) != 0)
							{
								num2 += num3;
								dictionary4.Add(keyValuePair6.Key, keyValuePair6.Value);
								Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275232) + num2.ToString() + StashManager.getString_0(107275243) + keyValuePair6.Key.n);
							}
							else
							{
								List<JsonItem> list2 = keyValuePair6.Value.Take(int_0 - num2).ToList<JsonItem>();
								num2 += list2.Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_9));
								Enum11 enum11_2 = Enum11.const_3;
								string str = StashManager.getString_0(107274690);
								*(int*)ptr = list2.Count<JsonItem>();
								Class181.smethod_3(enum11_2, str + ((int*)ptr)->ToString());
								Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274701) + num2.ToString() + StashManager.getString_0(107275243) + keyValuePair6.Key.n);
								dictionary4.Add(keyValuePair6.Key, list2);
								((byte*)ptr)[20] = ((num2 >= int_0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 20) != 0)
								{
									break;
								}
							}
						}
						result = dictionary4;
					}
				}
			}
			return result;
		}

		private unsafe static Dictionary<JsonTab, List<JsonItem>> smethod_3(string string_0, int int_0, int int_1, bool bool_0, bool bool_1, string string_1, out string string_2, bool bool_2 = false)
		{
			void* ptr = stackalloc byte[20];
			StashManager.Class311 @class = new StashManager.Class311();
			@class.string_0 = string_0;
			@class.bool_0 = bool_0;
			@class.int_0 = int_1;
			@class.string_1 = string_1;
			Dictionary<JsonTab, List<JsonItem>> dictionary = new Dictionary<JsonTab, List<JsonItem>>();
			Dictionary<int, List<JsonItem>> dictionary2 = Stashes.Items.Where(new Func<KeyValuePair<int, List<JsonItem>>, bool>(StashManager.<>c.<>9.method_10)).ToDictionary(new Func<KeyValuePair<int, List<JsonItem>>, int>(StashManager.<>c.<>9.method_11), new Func<KeyValuePair<int, List<JsonItem>>, List<JsonItem>>(StashManager.<>c.<>9.method_12));
			string_2 = string.Empty;
			Dictionary<int, List<JsonItem>> dictionary3 = new Dictionary<int, List<JsonItem>>();
			foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in dictionary2)
			{
				((byte*)ptr)[4] = ((!dictionary3.ContainsKey(keyValuePair.Key)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					dictionary3.Add(keyValuePair.Key, new List<JsonItem>());
				}
				dictionary3[keyValuePair.Key] = keyValuePair.Value.ToList<JsonItem>();
			}
			((byte*)ptr)[5] = (@class.bool_0 ? 1 : 0);
			Predicate<JsonItem> predicate;
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				predicate = new Predicate<JsonItem>(@class.method_0);
			}
			else
			{
				predicate = new Predicate<JsonItem>(@class.method_1);
			}
			@class.jsonItem_0 = null;
			using (Dictionary<int, List<JsonItem>>.Enumerator enumerator2 = new Dictionary<int, List<JsonItem>>(dictionary3).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					StashManager.Class312 class2 = new StashManager.Class312();
					class2.class311_0 = @class;
					class2.keyValuePair_0 = enumerator2.Current;
					((byte*)ptr)[6] = (bool_2 ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						class2.class311_0.jsonItem_0 = dictionary3[class2.keyValuePair_0.Key].FirstOrDefault(new Func<JsonItem, bool>(predicate.Invoke));
					}
					else
					{
						class2.class311_0.jsonItem_0 = dictionary3[class2.keyValuePair_0.Key].FirstOrDefault(new Func<JsonItem, bool>(Util.smethod_13<JsonItem>(new Predicate<JsonItem>[]
						{
							predicate,
							new Predicate<JsonItem>(class2, ldftn(method_0))
						}).Invoke));
					}
					((byte*)ptr)[7] = ((class2.class311_0.jsonItem_0 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						string_2 = Class252.smethod_1(class2.class311_0.jsonItem_0, Stashes.smethod_11(class2.keyValuePair_0.Key));
						break;
					}
				}
			}
			((byte*)ptr)[8] = ((@class.jsonItem_0 == null) ? 1 : 0);
			Dictionary<JsonTab, List<JsonItem>> result;
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				string_2 = string.Empty;
				result = new Dictionary<JsonTab, List<JsonItem>>();
			}
			else
			{
				Dictionary<int, List<JsonItem>> dictionary4 = new Dictionary<int, List<JsonItem>>();
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair2 in new Dictionary<int, List<JsonItem>>(dictionary3))
				{
					IEnumerable<JsonItem> source = Stashes.Items[keyValuePair2.Key];
					Func<JsonItem, bool> predicate2;
					if ((predicate2 = @class.func_0) == null)
					{
						predicate2 = (@class.func_0 = new Func<JsonItem, bool>(@class.method_2));
					}
					IEnumerable<JsonItem> enumerable = source.Where(predicate2);
					((byte*)ptr)[9] = ((!enumerable.Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						((byte*)ptr)[10] = ((!dictionary4.ContainsKey(keyValuePair2.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							dictionary4.Add(keyValuePair2.Key, new List<JsonItem>());
						}
						dictionary4[keyValuePair2.Key].AddRange(enumerable);
					}
				}
				dictionary2 = dictionary4;
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair3 in dictionary2)
				{
					StashManager.Class313 class3 = new StashManager.Class313();
					class3.jsonTab_0 = Stashes.smethod_11(keyValuePair3.Key);
					((byte*)ptr)[11] = (@class.bool_0 ? 1 : 0);
					List<JsonItem> list;
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						list = dictionary2[class3.jsonTab_0.i].Where(new Func<JsonItem, bool>(StashManager.<>c.<>9.method_13)).ToList<JsonItem>();
					}
					else
					{
						list = dictionary2[class3.jsonTab_0.i].Where(new Func<JsonItem, bool>(StashManager.<>c.<>9.method_14)).ToList<JsonItem>();
					}
					((byte*)ptr)[12] = (list.Any<JsonItem>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						((byte*)ptr)[13] = ((!dictionary.ContainsKey(class3.jsonTab_0)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							dictionary.Add(class3.jsonTab_0, new List<JsonItem>());
						}
						dictionary[class3.jsonTab_0].AddRange(list);
					}
					IEnumerable<Order> enumerable2 = StashManager.mainForm_0.list_7.Where(new Func<Order, bool>(class3.method_0));
					using (IEnumerator<Order> enumerator5 = enumerable2.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							StashManager.Class314 class4 = new StashManager.Class314();
							class4.order_0 = enumerator5.Current;
							((byte*)ptr)[14] = (dictionary.ContainsKey(class3.jsonTab_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								dictionary[class3.jsonTab_0].RemoveAll(new Predicate<JsonItem>(class4.method_0));
							}
						}
					}
				}
				((byte*)ptr)[15] = ((!bool_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair4 in dictionary.ToList<KeyValuePair<JsonTab, List<JsonItem>>>())
					{
						((byte*)ptr)[16] = (keyValuePair4.Key.IsExcluded ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) != 0)
						{
							dictionary.Remove(keyValuePair4.Key);
						}
					}
				}
				if (dictionary.Values.Sum(new Func<List<JsonItem>, int>(StashManager.<>c.<>9.method_15)) < int_0)
				{
					string_2 = string.Empty;
					Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107274648), new object[]
					{
						dictionary.smethod_16(false),
						@class.string_0
					});
					result = new Dictionary<JsonTab, List<JsonItem>>();
				}
				else
				{
					((byte*)ptr)[17] = ((int_0 == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) != 0)
					{
						Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107274615), new object[]
						{
							dictionary.smethod_16(false),
							@class.string_0
						});
						result = dictionary;
					}
					else
					{
						int num = 0;
						Dictionary<JsonTab, List<JsonItem>> dictionary5 = new Dictionary<JsonTab, List<JsonItem>>();
						foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair5 in dictionary)
						{
							int num2 = keyValuePair5.Value.Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_16));
							Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275253) + num2.ToString());
							((byte*)ptr)[18] = ((num + num2 < int_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 18) != 0)
							{
								num += num2;
								dictionary5.Add(keyValuePair5.Key, keyValuePair5.Value);
								Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275232) + num.ToString() + StashManager.getString_0(107275243) + keyValuePair5.Key.n);
							}
							else
							{
								List<JsonItem> list2 = keyValuePair5.Value.Take(int_0 - num).ToList<JsonItem>();
								num += list2.Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_17));
								Enum11 enum11_ = Enum11.const_3;
								string str = StashManager.getString_0(107274690);
								*(int*)ptr = list2.Count<JsonItem>();
								Class181.smethod_3(enum11_, str + ((int*)ptr)->ToString());
								Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274701) + num.ToString() + StashManager.getString_0(107275243) + keyValuePair5.Key.n);
								dictionary5.Add(keyValuePair5.Key, list2);
								((byte*)ptr)[19] = ((num >= int_0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 19) != 0)
								{
									break;
								}
							}
						}
						result = dictionary5;
					}
				}
			}
			return result;
		}

		private unsafe static Dictionary<JsonTab, List<JsonItem>> smethod_4(string string_0, int int_0, bool bool_0, bool bool_1, string string_1, out string string_2, bool bool_2 = false)
		{
			void* ptr = stackalloc byte[20];
			StashManager.Class315 @class = new StashManager.Class315();
			@class.string_0 = string_0;
			@class.bool_0 = bool_0;
			@class.string_1 = string_1;
			Dictionary<JsonTab, List<JsonItem>> dictionary = new Dictionary<JsonTab, List<JsonItem>>();
			Dictionary<int, List<JsonItem>> dictionary2 = Stashes.Items.Where(new Func<KeyValuePair<int, List<JsonItem>>, bool>(StashManager.<>c.<>9.method_18)).ToDictionary(new Func<KeyValuePair<int, List<JsonItem>>, int>(StashManager.<>c.<>9.method_19), new Func<KeyValuePair<int, List<JsonItem>>, List<JsonItem>>(StashManager.<>c.<>9.method_20));
			string_2 = string.Empty;
			Dictionary<int, List<JsonItem>> dictionary3 = new Dictionary<int, List<JsonItem>>();
			foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in dictionary2)
			{
				((byte*)ptr)[4] = ((!dictionary3.ContainsKey(keyValuePair.Key)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					dictionary3.Add(keyValuePair.Key, new List<JsonItem>());
				}
				dictionary3[keyValuePair.Key] = keyValuePair.Value.ToList<JsonItem>();
			}
			((byte*)ptr)[5] = (@class.bool_0 ? 1 : 0);
			Predicate<JsonItem> predicate;
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				predicate = new Predicate<JsonItem>(@class.method_0);
			}
			else
			{
				predicate = new Predicate<JsonItem>(@class.method_1);
			}
			@class.jsonItem_0 = null;
			using (Dictionary<int, List<JsonItem>>.Enumerator enumerator2 = new Dictionary<int, List<JsonItem>>(dictionary3).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					StashManager.Class316 class2 = new StashManager.Class316();
					class2.class315_0 = @class;
					class2.keyValuePair_0 = enumerator2.Current;
					((byte*)ptr)[6] = (bool_2 ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						class2.class315_0.jsonItem_0 = dictionary3[class2.keyValuePair_0.Key].FirstOrDefault(new Func<JsonItem, bool>(predicate.Invoke));
					}
					else
					{
						class2.class315_0.jsonItem_0 = dictionary3[class2.keyValuePair_0.Key].FirstOrDefault(new Func<JsonItem, bool>(Util.smethod_13<JsonItem>(new Predicate<JsonItem>[]
						{
							predicate,
							new Predicate<JsonItem>(class2, ldftn(method_0))
						}).Invoke));
					}
					((byte*)ptr)[7] = ((class2.class315_0.jsonItem_0 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						string_2 = Class252.smethod_1(class2.class315_0.jsonItem_0, Stashes.smethod_11(class2.keyValuePair_0.Key));
						break;
					}
				}
			}
			((byte*)ptr)[8] = ((@class.jsonItem_0 == null) ? 1 : 0);
			Dictionary<JsonTab, List<JsonItem>> result;
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				string_2 = string.Empty;
				result = new Dictionary<JsonTab, List<JsonItem>>();
			}
			else
			{
				Dictionary<int, List<JsonItem>> dictionary4 = new Dictionary<int, List<JsonItem>>();
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair2 in new Dictionary<int, List<JsonItem>>(dictionary3))
				{
					IEnumerable<JsonItem> source = Stashes.Items[keyValuePair2.Key];
					Func<JsonItem, bool> predicate2;
					if ((predicate2 = @class.func_0) == null)
					{
						predicate2 = (@class.func_0 = new Func<JsonItem, bool>(@class.method_2));
					}
					IEnumerable<JsonItem> enumerable = source.Where(predicate2);
					((byte*)ptr)[9] = ((!enumerable.Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						((byte*)ptr)[10] = ((!dictionary4.ContainsKey(keyValuePair2.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							dictionary4.Add(keyValuePair2.Key, new List<JsonItem>());
						}
						dictionary4[keyValuePair2.Key].AddRange(enumerable);
					}
				}
				dictionary2 = dictionary4;
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair3 in dictionary2)
				{
					StashManager.Class317 class3 = new StashManager.Class317();
					class3.jsonTab_0 = Stashes.smethod_11(keyValuePair3.Key);
					((byte*)ptr)[11] = (@class.bool_0 ? 1 : 0);
					List<JsonItem> list;
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						list = dictionary2[class3.jsonTab_0.i].Where(new Func<JsonItem, bool>(StashManager.<>c.<>9.method_21)).ToList<JsonItem>();
					}
					else
					{
						list = dictionary2[class3.jsonTab_0.i].Where(new Func<JsonItem, bool>(StashManager.<>c.<>9.method_22)).ToList<JsonItem>();
					}
					((byte*)ptr)[12] = (list.Any<JsonItem>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						((byte*)ptr)[13] = ((!dictionary.ContainsKey(class3.jsonTab_0)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							dictionary.Add(class3.jsonTab_0, new List<JsonItem>());
						}
						dictionary[class3.jsonTab_0].AddRange(list);
					}
					IEnumerable<Order> enumerable2 = StashManager.mainForm_0.list_7.Where(new Func<Order, bool>(class3.method_0));
					using (IEnumerator<Order> enumerator5 = enumerable2.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							StashManager.Class318 class4 = new StashManager.Class318();
							class4.order_0 = enumerator5.Current;
							((byte*)ptr)[14] = (dictionary.ContainsKey(class3.jsonTab_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								dictionary[class3.jsonTab_0].RemoveAll(new Predicate<JsonItem>(class4.method_0));
							}
						}
					}
				}
				((byte*)ptr)[15] = ((!bool_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair4 in dictionary.ToList<KeyValuePair<JsonTab, List<JsonItem>>>())
					{
						((byte*)ptr)[16] = (keyValuePair4.Key.IsExcluded ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) != 0)
						{
							dictionary.Remove(keyValuePair4.Key);
						}
					}
				}
				if (dictionary.Values.Sum(new Func<List<JsonItem>, int>(StashManager.<>c.<>9.method_23)) < int_0)
				{
					string_2 = string.Empty;
					Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107274648), new object[]
					{
						dictionary.smethod_16(false),
						@class.string_0
					});
					result = new Dictionary<JsonTab, List<JsonItem>>();
				}
				else
				{
					((byte*)ptr)[17] = ((int_0 == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) != 0)
					{
						Class181.smethod_2(Enum11.const_3, StashManager.getString_0(107274615), new object[]
						{
							dictionary.smethod_16(false),
							@class.string_0
						});
						result = dictionary;
					}
					else
					{
						int num = 0;
						Dictionary<JsonTab, List<JsonItem>> dictionary5 = new Dictionary<JsonTab, List<JsonItem>>();
						foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair5 in dictionary)
						{
							int num2 = keyValuePair5.Value.Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_24));
							Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275253) + num2.ToString());
							((byte*)ptr)[18] = ((num + num2 < int_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 18) != 0)
							{
								num += num2;
								dictionary5.Add(keyValuePair5.Key, keyValuePair5.Value);
								Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107275232) + num.ToString() + StashManager.getString_0(107275243) + keyValuePair5.Key.n);
							}
							else
							{
								List<JsonItem> list2 = keyValuePair5.Value.Take(int_0 - num).ToList<JsonItem>();
								num += list2.Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_25));
								Enum11 enum11_ = Enum11.const_3;
								string str = StashManager.getString_0(107274690);
								*(int*)ptr = list2.Count<JsonItem>();
								Class181.smethod_3(enum11_, str + ((int*)ptr)->ToString());
								Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274701) + num.ToString() + StashManager.getString_0(107275243) + keyValuePair5.Key.n);
								dictionary5.Add(keyValuePair5.Key, list2);
								((byte*)ptr)[19] = ((num >= int_0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 19) != 0)
								{
									break;
								}
							}
						}
						result = dictionary5;
					}
				}
			}
			return result;
		}

		public unsafe static int[,] smethod_5(int int_0, int int_1)
		{
			void* ptr = stackalloc byte[18];
			int[,] array = new int[int_1, int_1];
			StashManager.Class319 @class = new StashManager.Class319();
			@class.int_0 = 0;
			for (;;)
			{
				((byte*)ptr)[17] = ((@class.int_0 < array.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) == 0)
				{
					break;
				}
				StashManager.Class320 class2 = new StashManager.Class320();
				class2.class319_0 = @class;
				class2.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[16] = ((class2.int_0 < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) == 0)
					{
						break;
					}
					((byte*)ptr)[12] = ((array[class2.int_0, class2.class319_0.int_0] != 1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						JsonItem jsonItem = Stashes.Items[int_0].FirstOrDefault(new Func<JsonItem, bool>(class2.method_0));
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
									array[*(int*)((byte*)ptr + 4) + class2.int_0, *(int*)ptr + class2.class319_0.int_0] = 1;
									*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
								}
								*(int*)ptr = *(int*)ptr + 1;
							}
						}
						else
						{
							array[class2.int_0, class2.class319_0.int_0] = 0;
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

		public unsafe static int[,] smethod_6(List<JsonItem> list_0, int int_0)
		{
			void* ptr = stackalloc byte[18];
			int[,] array = new int[int_0, int_0];
			StashManager.Class321 @class = new StashManager.Class321();
			@class.int_0 = 0;
			for (;;)
			{
				((byte*)ptr)[17] = ((@class.int_0 < array.GetLength(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) == 0)
				{
					break;
				}
				StashManager.Class322 class2 = new StashManager.Class322();
				class2.class321_0 = @class;
				class2.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[16] = ((class2.int_0 < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) == 0)
					{
						break;
					}
					((byte*)ptr)[12] = ((array[class2.int_0, class2.class321_0.int_0] != 1) ? 1 : 0);
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
									array[*(int*)((byte*)ptr + 4) + class2.int_0, *(int*)ptr + class2.class321_0.int_0] = 1;
									*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
								}
								*(int*)ptr = *(int*)ptr + 1;
							}
						}
						else
						{
							array[class2.int_0, class2.class321_0.int_0] = 0;
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

		public unsafe static Position smethod_7(int int_0, int int_1, int int_2)
		{
			void* ptr = stackalloc byte[32];
			JsonTab jsonTab = Stashes.smethod_11(int_0);
			((byte*)ptr)[24] = ((jsonTab == null) ? 1 : 0);
			Position result;
			if (*(sbyte*)((byte*)ptr + 24) != 0)
			{
				result = null;
			}
			else
			{
				*(int*)ptr = 0;
				string type = jsonTab.type;
				string text = type;
				if (text != null)
				{
					if (!(text == StashManager.getString_0(107383877)) && !(text == StashManager.getString_0(107383862)))
					{
						if (!(text == StashManager.getString_0(107383860)))
						{
							goto IL_259;
						}
						*(int*)ptr = 24;
					}
					else
					{
						*(int*)ptr = 12;
					}
					int[,] array = StashManager.smethod_5(int_0, *(int*)ptr);
					List<Position> list = new List<Position>();
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[30] = ((*(int*)((byte*)ptr + 4) < array.GetLength(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 30) == 0)
						{
							break;
						}
						*(int*)((byte*)ptr + 8) = 0;
						for (;;)
						{
							((byte*)ptr)[29] = ((*(int*)((byte*)ptr + 8) < array.GetLength(0)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 29) == 0)
							{
								break;
							}
							((byte*)ptr)[25] = ((array[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 4)] == 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 25) != 0 && *(int*)((byte*)ptr + 8) + int_1 <= array.GetLength(0) && *(int*)((byte*)ptr + 4) + int_2 <= array.GetLength(1))
							{
								*(int*)((byte*)ptr + 12) = 0;
								*(int*)((byte*)ptr + 16) = 0;
								for (;;)
								{
									((byte*)ptr)[27] = ((*(int*)((byte*)ptr + 16) < int_2) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 27) == 0)
									{
										break;
									}
									*(int*)((byte*)ptr + 20) = 0;
									for (;;)
									{
										((byte*)ptr)[26] = ((*(int*)((byte*)ptr + 20) < int_1) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 26) == 0)
										{
											break;
										}
										*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + array[*(int*)((byte*)ptr + 20) + *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 16) + *(int*)((byte*)ptr + 4)];
										*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
									}
									*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + 1;
								}
								((byte*)ptr)[28] = ((*(int*)((byte*)ptr + 12) == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 28) != 0)
								{
									list.Add(new Position(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 4)));
								}
							}
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					((byte*)ptr)[31] = ((!list.Any<Position>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 31) != 0)
					{
						return null;
					}
					return list.OrderBy(new Func<Position, int>(StashManager.<>c.<>9.method_26)).ThenBy(new Func<Position, int>(StashManager.<>c.<>9.method_27)).First<Position>();
				}
				IL_259:
				result = null;
			}
			return result;
		}

		public static JsonTab smethod_8(List<JsonTab> list_0, int int_0, int int_1)
		{
			foreach (JsonTab jsonTab in list_0)
			{
				if (jsonTab != null && !jsonTab.IsSpecialTab)
				{
					Position position_ = StashManager.smethod_7(jsonTab.i, int_0, int_1);
					if (Position.smethod_1(position_, null))
					{
						return jsonTab;
					}
				}
			}
			return null;
		}

		public unsafe static bool smethod_9(int int_0, List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[112];
			JsonTab jsonTab = Stashes.smethod_11(int_0);
			((byte*)ptr)[80] = ((jsonTab == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 80) != 0)
			{
				Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274586));
				((byte*)ptr)[81] = 0;
			}
			else
			{
				((byte*)ptr)[82] = ((!Stashes.Items.ContainsKey(int_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 82) != 0)
				{
					Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274605));
					((byte*)ptr)[81] = 0;
				}
				else
				{
					((byte*)ptr)[83] = ((!jsonTab.IsSupported) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 83) != 0)
					{
						((byte*)ptr)[81] = 1;
					}
					else
					{
						List<JsonItem> list = Stashes.Items[int_0].ToList<JsonItem>();
						((byte*)ptr)[84] = ((!jsonTab.IsSpecialTab) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 84) != 0)
						{
							int int_ = (jsonTab.type == StashManager.getString_0(107383860)) ? 24 : 12;
							foreach (JsonItem jsonItem in list_0)
							{
								int[,] array = StashManager.smethod_6(list, int_);
								((byte*)ptr)[85] = 0;
								*(int*)((byte*)ptr + 32) = 0;
								for (;;)
								{
									((byte*)ptr)[93] = ((*(int*)((byte*)ptr + 32) < array.GetLength(1)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 93) == 0)
									{
										break;
									}
									*(int*)((byte*)ptr + 36) = 0;
									for (;;)
									{
										((byte*)ptr)[91] = ((*(int*)((byte*)ptr + 36) < array.GetLength(0)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 91) == 0)
										{
											break;
										}
										((byte*)ptr)[86] = ((array[*(int*)((byte*)ptr + 36), *(int*)((byte*)ptr + 32)] == 0) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 86) == 0)
										{
											goto IL_245;
										}
										if (*(int*)((byte*)ptr + 36) + jsonItem.w <= array.GetLength(0) && *(int*)((byte*)ptr + 32) + jsonItem.h <= array.GetLength(1))
										{
											*(int*)((byte*)ptr + 40) = 0;
											*(int*)((byte*)ptr + 44) = 0;
											for (;;)
											{
												((byte*)ptr)[88] = ((*(int*)((byte*)ptr + 44) < jsonItem.h) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 88) == 0)
												{
													break;
												}
												*(int*)((byte*)ptr + 48) = 0;
												for (;;)
												{
													((byte*)ptr)[87] = ((*(int*)((byte*)ptr + 48) < jsonItem.w) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 87) == 0)
													{
														break;
													}
													*(int*)((byte*)ptr + 40) = *(int*)((byte*)ptr + 40) + array[*(int*)((byte*)ptr + 48) + *(int*)((byte*)ptr + 36), *(int*)((byte*)ptr + 44) + *(int*)((byte*)ptr + 32)];
													*(int*)((byte*)ptr + 48) = *(int*)((byte*)ptr + 48) + 1;
												}
												*(int*)((byte*)ptr + 44) = *(int*)((byte*)ptr + 44) + 1;
											}
											((byte*)ptr)[89] = ((*(int*)((byte*)ptr + 40) == 0) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 89) != 0)
											{
												JsonItem jsonItem2 = jsonItem.method_2();
												jsonItem2.x = *(int*)((byte*)ptr + 36);
												jsonItem2.y = *(int*)((byte*)ptr + 32);
												list.Add(jsonItem2);
												((byte*)ptr)[85] = 1;
												goto IL_245;
											}
											goto IL_245;
										}
										IL_256:
										*(int*)((byte*)ptr + 36) = *(int*)((byte*)ptr + 36) + 1;
										continue;
										IL_245:
										((byte*)ptr)[90] = (byte)(*(sbyte*)((byte*)ptr + 85));
										if (*(sbyte*)((byte*)ptr + 90) == 0)
										{
											goto IL_256;
										}
										break;
									}
									((byte*)ptr)[92] = (byte)(*(sbyte*)((byte*)ptr + 85));
									if (*(sbyte*)((byte*)ptr + 92) != 0)
									{
										break;
									}
									*(int*)((byte*)ptr + 32) = *(int*)((byte*)ptr + 32) + 1;
								}
								((byte*)ptr)[94] = ((*(sbyte*)((byte*)ptr + 85) == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 94) != 0)
								{
									((byte*)ptr)[81] = 0;
									goto IL_D79;
								}
							}
							((byte*)ptr)[81] = 1;
						}
						else
						{
							StashManager.Class323 @class = new StashManager.Class323();
							Dictionary<string, Tuple<int, string>> dictionary = new Dictionary<string, Tuple<int, string>>();
							foreach (JsonItem jsonItem3 in list_0)
							{
								((byte*)ptr)[95] = ((!dictionary.ContainsKey(jsonItem3.Name)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 95) != 0)
								{
									dictionary.Add(jsonItem3.Name, Tuple.Create<int, string>(0, API.smethod_9(jsonItem3)));
								}
								Tuple<int, string> tuple = dictionary[jsonItem3.Name];
								dictionary[jsonItem3.Name] = Tuple.Create<int, string>(tuple.Item1 + jsonItem3.stack, tuple.Item2);
							}
							@class.list_0 = new List<string>
							{
								StashManager.getString_0(107363171),
								StashManager.getString_0(107363158),
								StashManager.getString_0(107363220),
								StashManager.getString_0(107363512),
								StashManager.getString_0(107402872),
								StashManager.getString_0(107455166),
								StashManager.getString_0(107373325),
								StashManager.getString_0(107373276)
							};
							StashManager.Class324 class2 = new StashManager.Class324();
							string type = jsonTab.type;
							string text = type;
							if (text != null)
							{
								if (!(text == StashManager.getString_0(107395922)))
								{
									if (!(text == StashManager.getString_0(107381971)))
									{
										if (!(text == StashManager.getString_0(107381922)))
										{
											if (!(text == StashManager.getString_0(107383847)))
											{
												goto IL_D73;
											}
											class2.list_0 = new List<string>
											{
												StashManager.getString_0(107363171),
												StashManager.getString_0(107363158),
												StashManager.getString_0(107363156)
											};
											((byte*)ptr)[110] = (dictionary.Any(new Func<KeyValuePair<string, Tuple<int, string>>, bool>(class2.method_0)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 110) != 0)
											{
												((byte*)ptr)[81] = 0;
												goto IL_D79;
											}
											using (Dictionary<string, Tuple<int, string>>.Enumerator enumerator3 = dictionary.GetEnumerator())
											{
												while (enumerator3.MoveNext())
												{
													StashManager.Class333 class3 = new StashManager.Class333();
													class3.keyValuePair_0 = enumerator3.Current;
													*(double*)((byte*)ptr + 24) = (double)Util.smethod_3(class3.keyValuePair_0.Key);
													int num = list.Where(new Func<JsonItem, bool>(class3.method_0)).Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_34));
													((byte*)ptr)[111] = (((int)Math.Ceiling(((double)num + (double)class3.keyValuePair_0.Value.Item1) / *(double*)((byte*)ptr + 24)) > 1) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 111) != 0)
													{
														((byte*)ptr)[81] = 0;
														goto IL_D79;
													}
												}
												goto IL_D73;
											}
										}
										if (dictionary.Any(new Func<KeyValuePair<string, Tuple<int, string>>, bool>(StashManager.<>c.<>9.method_32)))
										{
											((byte*)ptr)[81] = 0;
											goto IL_D79;
										}
										using (Dictionary<string, Tuple<int, string>>.Enumerator enumerator4 = dictionary.GetEnumerator())
										{
											while (enumerator4.MoveNext())
											{
												StashManager.Class332 class4 = new StashManager.Class332();
												class4.keyValuePair_0 = enumerator4.Current;
												*(double*)((byte*)ptr + 16) = (double)Util.smethod_3(class4.keyValuePair_0.Key);
												int num2 = list.Where(new Func<JsonItem, bool>(class4.method_0)).Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_33));
												((byte*)ptr)[109] = (((int)Math.Ceiling(((double)num2 + (double)class4.keyValuePair_0.Value.Item1) / *(double*)((byte*)ptr + 16)) > 1) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 109) != 0)
												{
													((byte*)ptr)[81] = 0;
													goto IL_D79;
												}
											}
											goto IL_D73;
										}
									}
									StashManager.Class329 class5 = new StashManager.Class329();
									((byte*)ptr)[104] = (dictionary.Any(new Func<KeyValuePair<string, Tuple<int, string>>, bool>(@class.method_1)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 104) != 0)
									{
										((byte*)ptr)[81] = 0;
										goto IL_D79;
									}
									class5.list_0 = new List<int>
									{
										105,
										106,
										107
									};
									*(int*)((byte*)ptr + 68) = 0;
									using (List<int>.Enumerator enumerator5 = class5.list_0.GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											StashManager.Class330 class6 = new StashManager.Class330();
											class6.int_0 = enumerator5.Current;
											JsonItem jsonItem4 = list.FirstOrDefault(new Func<JsonItem, bool>(class6.method_0));
											((byte*)ptr)[105] = ((jsonItem4 == null) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 105) != 0)
											{
												*(int*)((byte*)ptr + 68) = *(int*)((byte*)ptr + 68) + 1;
											}
										}
									}
									if (dictionary.Count(new Func<KeyValuePair<string, Tuple<int, string>>, bool>(StashManager.<>c.<>9.method_30)) > *(int*)((byte*)ptr + 68))
									{
										((byte*)ptr)[81] = 0;
										goto IL_D79;
									}
									using (Dictionary<string, Tuple<int, string>>.Enumerator enumerator6 = dictionary.GetEnumerator())
									{
										while (enumerator6.MoveNext())
										{
											StashManager.Class331 class7 = new StashManager.Class331();
											class7.class329_0 = class5;
											class7.keyValuePair_0 = enumerator6.Current;
											*(double*)((byte*)ptr + 8) = (double)Util.smethod_3(class7.keyValuePair_0.Key);
											int num3 = list.Where(new Func<JsonItem, bool>(class7.method_0)).Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_31));
											*(int*)((byte*)ptr + 72) = (int)Math.Ceiling(((double)num3 + (double)class7.keyValuePair_0.Value.Item1) / *(double*)((byte*)ptr + 8));
											((byte*)ptr)[106] = ((class7.keyValuePair_0.Value.Item2 == StashManager.getString_0(107363462)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 106) != 0)
											{
												*(int*)((byte*)ptr + 72) = *(int*)((byte*)ptr + 72) - 1;
											}
											((byte*)ptr)[107] = ((*(int*)((byte*)ptr + 72) > 0) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 107) != 0)
											{
												*(int*)((byte*)ptr + 76) = list.Count(new Func<JsonItem, bool>(class7.method_1));
												((byte*)ptr)[108] = ((*(int*)((byte*)ptr + 72) - *(int*)((byte*)ptr + 76) > *(int*)((byte*)ptr + 68)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 108) != 0)
												{
													((byte*)ptr)[81] = 0;
													goto IL_D79;
												}
												*(int*)((byte*)ptr + 68) = *(int*)((byte*)ptr + 68) - (*(int*)((byte*)ptr + 72) - *(int*)((byte*)ptr + 76));
											}
										}
										goto IL_D73;
									}
								}
								StashManager.Class325 class8 = new StashManager.Class325();
								((byte*)ptr)[96] = (dictionary.Any(new Func<KeyValuePair<string, Tuple<int, string>>, bool>(@class.method_0)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 96) != 0)
								{
									((byte*)ptr)[81] = 0;
									goto IL_D79;
								}
								class8.list_0 = new List<int>
								{
									30,
									31,
									32,
									33,
									34,
									40,
									41,
									42,
									43,
									44,
									45,
									46,
									47,
									48
								};
								*(int*)((byte*)ptr + 52) = 0;
								using (List<int>.Enumerator enumerator7 = class8.list_0.GetEnumerator())
								{
									while (enumerator7.MoveNext())
									{
										StashManager.Class326 class9 = new StashManager.Class326();
										class9.int_0 = enumerator7.Current;
										JsonItem jsonItem5 = list.FirstOrDefault(new Func<JsonItem, bool>(class9.method_0));
										((byte*)ptr)[97] = ((jsonItem5 == null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 97) != 0)
										{
											*(int*)((byte*)ptr + 52) = *(int*)((byte*)ptr + 52) + 1;
										}
									}
								}
								if (dictionary.Count(new Func<KeyValuePair<string, Tuple<int, string>>, bool>(StashManager.<>c.<>9.method_28)) > *(int*)((byte*)ptr + 52))
								{
									*(int*)((byte*)ptr + 56) = 0;
									foreach (KeyValuePair<string, Tuple<int, string>> keyValuePair in dictionary)
									{
										using (List<int>.Enumerator enumerator9 = class8.list_0.GetEnumerator())
										{
											while (enumerator9.MoveNext())
											{
												StashManager.Class327 class10 = new StashManager.Class327();
												class10.int_0 = enumerator9.Current;
												JsonItem jsonItem6 = list.FirstOrDefault(new Func<JsonItem, bool>(class10.method_0));
												((byte*)ptr)[98] = ((jsonItem6 != null) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 98) != 0)
												{
													((byte*)ptr)[99] = ((jsonItem6.Name == keyValuePair.Key) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 99) != 0)
													{
														((byte*)ptr)[100] = ((jsonItem6.stack + keyValuePair.Value.Item1 <= Util.smethod_3(keyValuePair.Key)) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 100) != 0)
														{
															*(int*)((byte*)ptr + 56) = *(int*)((byte*)ptr + 56) + 1;
														}
													}
												}
											}
										}
									}
									((byte*)ptr)[81] = ((*(int*)((byte*)ptr + 56) >= dictionary.Count) ? 1 : 0);
									goto IL_D79;
								}
								using (Dictionary<string, Tuple<int, string>>.Enumerator enumerator10 = dictionary.GetEnumerator())
								{
									while (enumerator10.MoveNext())
									{
										StashManager.Class328 class11 = new StashManager.Class328();
										class11.class325_0 = class8;
										class11.keyValuePair_0 = enumerator10.Current;
										*(double*)ptr = (double)Util.smethod_3(class11.keyValuePair_0.Key);
										int num4 = list.Where(new Func<JsonItem, bool>(class11.method_0)).Sum(new Func<JsonItem, int>(StashManager.<>c.<>9.method_29));
										*(int*)((byte*)ptr + 60) = (int)Math.Ceiling(((double)num4 + (double)class11.keyValuePair_0.Value.Item1) / *(double*)ptr);
										((byte*)ptr)[101] = ((class11.keyValuePair_0.Value.Item2 == StashManager.getString_0(107396068)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 101) != 0)
										{
											*(int*)((byte*)ptr + 60) = *(int*)((byte*)ptr + 60) - 1;
										}
										((byte*)ptr)[102] = ((*(int*)((byte*)ptr + 60) > 0) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 102) != 0)
										{
											*(int*)((byte*)ptr + 64) = list.Count(new Func<JsonItem, bool>(class11.method_1));
											((byte*)ptr)[103] = ((*(int*)((byte*)ptr + 60) - *(int*)((byte*)ptr + 64) > *(int*)((byte*)ptr + 52)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 103) != 0)
											{
												((byte*)ptr)[81] = 0;
												goto IL_D79;
											}
											*(int*)((byte*)ptr + 52) = *(int*)((byte*)ptr + 52) - (*(int*)((byte*)ptr + 60) - *(int*)((byte*)ptr + 64));
										}
									}
								}
							}
							IL_D73:
							((byte*)ptr)[81] = 1;
						}
					}
				}
			}
			IL_D79:
			return *(sbyte*)((byte*)ptr + 81) != 0;
		}

		public unsafe static JsonItem smethod_10(int int_0, int int_1, int int_2)
		{
			void* ptr = stackalloc byte[4];
			StashManager.Class334 @class = new StashManager.Class334();
			@class.int_0 = int_1;
			@class.int_1 = int_2;
			JsonTab jsonTab = Stashes.smethod_11(int_0);
			*(byte*)ptr = ((jsonTab == null) ? 1 : 0);
			JsonItem result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274560));
				result = null;
			}
			else if (!jsonTab.IsSupported || jsonTab.IsExcluded)
			{
				Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274579));
				result = null;
			}
			else
			{
				((byte*)ptr)[1] = ((!Stashes.Items.ContainsKey(int_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274566));
					result = null;
				}
				else
				{
					JsonItem jsonItem = Stashes.Items[int_0].FirstOrDefault(new Func<JsonItem, bool>(@class.method_0));
					((byte*)ptr)[2] = ((jsonItem == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = null;
					}
					else
					{
						((byte*)ptr)[3] = (jsonItem.HasSocketedItemsInside ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							Class181.smethod_3(Enum11.const_3, StashManager.getString_0(107274521));
							result = null;
						}
						else
						{
							result = jsonItem;
						}
					}
				}
			}
			return result;
		}

		static StashManager()
		{
			Strings.CreateGetStringDelegate(typeof(StashManager));
		}

		private static MainForm mainForm_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class309
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.typeLine.ToLower().Contains(this.string_0.ToLower());
			}

			internal bool method_1(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name.ToLower() == this.string_0.ToLower() && jsonItem_1.IsUnique == this.bool_0;
			}

			internal bool method_2(JsonItem jsonItem_1)
			{
				return (jsonItem_1.Name.ToLower().Contains(this.string_0.ToLower()) || this.string_0.ToLower().Contains(jsonItem_1.Name.ToLower())) && jsonItem_1.IsUnique == this.bool_0;
			}

			internal bool method_3(JsonItem jsonItem_1)
			{
				return !jsonItem_1.note.smethod_9(StashManager.Class309.getString_0(107251180)) && jsonItem_1.UniqueIdentifiers == this.jsonItem_0.UniqueIdentifiers && !jsonItem_1.HasSocketedItemsInside;
			}

			static Class309()
			{
				Strings.CreateGetStringDelegate(typeof(StashManager.Class309));
			}

			public string string_0;

			public bool bool_0;

			public string string_1;

			public JsonItem jsonItem_0;

			public Predicate<JsonItem> predicate_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class310
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return Class252.smethod_3(Class252.smethod_1(jsonItem_0, this.jsonTab_0), this.class309_0.string_1) && !jsonItem_0.HasSocketedItemsInside;
			}

			public JsonTab jsonTab_0;

			public StashManager.Class309 class309_0;
		}

		[CompilerGenerated]
		private sealed class Class311
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				bool flag = !Class146.smethod_0(this.string_0);
				return jsonItem_1.Name.Replace(StashManager.Class311.getString_0(107369385), StashManager.Class311.getString_0(107399061)).smethod_20(this.string_0.Replace(StashManager.Class311.getString_0(107369385), StashManager.Class311.getString_0(107399061)), StringComparison.OrdinalIgnoreCase) && jsonItem_1.IsUnique == this.bool_0 && (!flag || jsonItem_1.MapTier == this.int_0);
			}

			internal bool method_1(JsonItem jsonItem_1)
			{
				return API.smethod_2(jsonItem_1.typeLine).ToLower() == this.string_0.ToLower() && jsonItem_1.MapTier == this.int_0 && jsonItem_1.IsUnique == this.bool_0;
			}

			internal bool method_2(JsonItem jsonItem_1)
			{
				return !jsonItem_1.note.smethod_9(StashManager.Class311.getString_0(107251226)) && API.smethod_2(this.string_0).Contains(API.smethod_2(jsonItem_1.typeLine)) && (Class146.smethod_0(jsonItem_1.Name) || jsonItem_1.MapTier == this.int_0) && jsonItem_1.UniqueIdentifiers == this.jsonItem_0.UniqueIdentifiers;
			}

			static Class311()
			{
				Strings.CreateGetStringDelegate(typeof(StashManager.Class311));
			}

			public string string_0;

			public bool bool_0;

			public int int_0;

			public string string_1;

			public JsonItem jsonItem_0;

			public Func<JsonItem, bool> func_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class312
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return Class252.smethod_3(Class252.smethod_1(jsonItem_0, Stashes.smethod_11(this.keyValuePair_0.Key)), this.class311_0.string_1);
			}

			public KeyValuePair<int, List<JsonItem>> keyValuePair_0;

			public StashManager.Class311 class311_0;
		}

		[CompilerGenerated]
		private sealed class Class313
		{
			internal bool method_0(Order order_0)
			{
				return order_0.bool_2 && order_0.stash.i == this.jsonTab_0.i && order_0.top_pos != 0 && order_0.left_pos != 0;
			}

			public JsonTab jsonTab_0;
		}

		[CompilerGenerated]
		private sealed class Class314
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.order_0.left_pos - 1 && jsonItem_0.y == this.order_0.top_pos - 1;
			}

			public Order order_0;
		}

		[CompilerGenerated]
		private sealed class Class315
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name.Replace(StashManager.Class315.getString_0(107369395), StashManager.Class315.getString_0(107399071)).smethod_20(this.string_0.Replace(StashManager.Class315.getString_0(107369395), StashManager.Class315.getString_0(107399071)), StringComparison.OrdinalIgnoreCase) && jsonItem_1.IsUnique == this.bool_0;
			}

			internal bool method_1(JsonItem jsonItem_1)
			{
				return API.smethod_2(jsonItem_1.typeLine).ToLower() == this.string_0.ToLower() && jsonItem_1.IsUnique == this.bool_0;
			}

			internal bool method_2(JsonItem jsonItem_1)
			{
				return !jsonItem_1.note.smethod_9(StashManager.Class315.getString_0(107251236)) && API.smethod_2(this.string_0).Contains(API.smethod_2(jsonItem_1.typeLine)) && jsonItem_1.UniqueIdentifiers == this.jsonItem_0.UniqueIdentifiers;
			}

			static Class315()
			{
				Strings.CreateGetStringDelegate(typeof(StashManager.Class315));
			}

			public string string_0;

			public bool bool_0;

			public string string_1;

			public JsonItem jsonItem_0;

			public Func<JsonItem, bool> func_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class316
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return Class252.smethod_3(Class252.smethod_1(jsonItem_0, Stashes.smethod_11(this.keyValuePair_0.Key)), this.class315_0.string_1);
			}

			public KeyValuePair<int, List<JsonItem>> keyValuePair_0;

			public StashManager.Class315 class315_0;
		}

		[CompilerGenerated]
		private sealed class Class317
		{
			internal bool method_0(Order order_0)
			{
				return order_0.bool_2 && order_0.stash.i == this.jsonTab_0.i && order_0.top_pos != 0 && order_0.left_pos != 0;
			}

			public JsonTab jsonTab_0;
		}

		[CompilerGenerated]
		private sealed class Class318
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.order_0.left_pos - 1 && jsonItem_0.y == this.order_0.top_pos - 1;
			}

			public Order order_0;
		}

		[CompilerGenerated]
		private sealed class Class319
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class320
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0 && jsonItem_0.y == this.class319_0.int_0;
			}

			public int int_0;

			public StashManager.Class319 class319_0;
		}

		[CompilerGenerated]
		private sealed class Class321
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class322
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0 && jsonItem_0.y == this.class321_0.int_0;
			}

			public int int_0;

			public StashManager.Class321 class321_0;
		}

		[CompilerGenerated]
		private sealed class Class323
		{
			internal bool method_0(KeyValuePair<string, Tuple<int, string>> keyValuePair_0)
			{
				return this.list_0.Contains(keyValuePair_0.Value.Item2) && API.smethod_7(keyValuePair_0.Key).Class != StashManager.Class323.getString_0(107455129);
			}

			internal bool method_1(KeyValuePair<string, Tuple<int, string>> keyValuePair_0)
			{
				return this.list_0.Contains(keyValuePair_0.Value.Item2) && API.smethod_7(keyValuePair_0.Key).Class != StashManager.Class323.getString_0(107455129);
			}

			static Class323()
			{
				Strings.CreateGetStringDelegate(typeof(StashManager.Class323));
			}

			public List<string> list_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class324
		{
			internal bool method_0(KeyValuePair<string, Tuple<int, string>> keyValuePair_0)
			{
				return !this.list_0.Contains(keyValuePair_0.Value.Item2);
			}

			public List<string> list_0;
		}

		[CompilerGenerated]
		private sealed class Class325
		{
			public List<int> list_0;
		}

		[CompilerGenerated]
		private sealed class Class326
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class327
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class328
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Key;
			}

			internal bool method_1(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Key && this.class325_0.list_0.Contains(jsonItem_0.x);
			}

			public KeyValuePair<string, Tuple<int, string>> keyValuePair_0;

			public StashManager.Class325 class325_0;
		}

		[CompilerGenerated]
		private sealed class Class329
		{
			public List<int> list_0;
		}

		[CompilerGenerated]
		private sealed class Class330
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class331
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Key;
			}

			internal bool method_1(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Key && this.class329_0.list_0.Contains(jsonItem_0.x);
			}

			public KeyValuePair<string, Tuple<int, string>> keyValuePair_0;

			public StashManager.Class329 class329_0;
		}

		[CompilerGenerated]
		private sealed class Class332
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Key;
			}

			public KeyValuePair<string, Tuple<int, string>> keyValuePair_0;
		}

		[CompilerGenerated]
		private sealed class Class333
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Key;
			}

			public KeyValuePair<string, Tuple<int, string>> keyValuePair_0;
		}

		[CompilerGenerated]
		private sealed class Class334
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.x == this.int_0 && jsonItem_0.y == this.int_1;
			}

			public int int_0;

			public int int_1;
		}
	}
}
