using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ns0;
using ns14;
using ns24;
using ns27;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Items;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class VendorRecipe
	{
		private static string RecipeType
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.VendorRecipeRecipeType);
			}
		}

		private static string MaximumPrice
		{
			get
			{
				return string.Format(VendorRecipe.getString_0(107465872), Class255.class105_0.method_6(ConfigOptions.VendorBelowPriceValue), API.smethod_4(Class255.class105_0.method_3(ConfigOptions.VendorBelowPriceCurrency)));
			}
		}

		[DebuggerStepThrough]
		public static void smethod_0(MainForm mainForm_0)
		{
			VendorRecipe.Class354 @class = new VendorRecipe.Class354();
			@class.mainForm_0 = mainForm_0;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<VendorRecipe.Class354>(ref @class);
		}

		private static Images smethod_1()
		{
			string text = Class255.class105_0.method_3(ConfigOptions.VendorRecipeVendor);
			string text2 = text;
			if (text2 != null)
			{
				uint num = Class396.smethod_0(text2);
				if (num <= 1157362297U)
				{
					if (num != 1016565922U)
					{
						if (num != 1102783620U)
						{
							if (num == 1157362297U)
							{
								if (text2 == VendorRecipe.getString_0(107367735))
								{
									return Images.AlvaTitle;
								}
							}
						}
						else if (text2 == VendorRecipe.getString_0(107367685))
						{
							return Images.HelenaTitle;
						}
					}
					else if (text2 == VendorRecipe.getString_0(107353812))
					{
						return Images.NavaliTitle;
					}
				}
				else if (num <= 2622125602U)
				{
					if (num != 1187066338U)
					{
						if (num == 2622125602U)
						{
							if (text2 == VendorRecipe.getString_0(107367662))
							{
								return Images.NikoTitle;
							}
						}
					}
					else if (text2 == VendorRecipe.getString_0(107367708))
					{
						return Images.JunTitle;
					}
				}
				else if (num != 2701558794U)
				{
					if (num == 3456607725U)
					{
						if (text2 == VendorRecipe.getString_0(107367653))
						{
							return Images.ZanaTitle;
						}
					}
				}
				else if (text2 == VendorRecipe.getString_0(107367694))
				{
					return Images.EinharTitle;
				}
			}
			return Images.NavaliTitle;
		}

		private unsafe static Dictionary<int, List<JsonItem>> smethod_2(Dictionary<int, List<JsonItem>> dictionary_0)
		{
			void* ptr = stackalloc byte[28];
			*(byte*)ptr = ((VendorRecipe.RecipeType == VendorRecipe.getString_0(107393059)) ? 1 : 0);
			Dictionary<int, List<JsonItem>> result;
			if (*(sbyte*)ptr != 0)
			{
				result = VendorRecipe.smethod_3(dictionary_0);
			}
			else if (VendorRecipe.RecipeType == VendorRecipe.getString_0(107393054) || VendorRecipe.RecipeType == VendorRecipe.getString_0(107393009))
			{
				result = VendorRecipe.smethod_4(dictionary_0);
			}
			else
			{
				((byte*)ptr)[1] = ((VendorRecipe.RecipeType == VendorRecipe.getString_0(107367007)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Dictionary<int, List<JsonItem>> dictionary = new Dictionary<int, List<JsonItem>>();
					foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in dictionary_0)
					{
						((byte*)ptr)[2] = ((!dictionary.ContainsKey(keyValuePair.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value.OrderBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_7)).ToList<JsonItem>());
						}
					}
					dictionary_0 = dictionary;
				}
				Dictionary<int, List<JsonItem>> dictionary2 = new Dictionary<int, List<JsonItem>>();
				JsonItem jsonItem = null;
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair2 in dictionary_0)
				{
					jsonItem = keyValuePair2.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_8));
					((byte*)ptr)[3] = ((jsonItem != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						((byte*)ptr)[4] = ((!dictionary2.ContainsKey(keyValuePair2.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							dictionary2.Add(keyValuePair2.Key, new List<JsonItem>());
						}
						dictionary2[keyValuePair2.Key].Add(jsonItem);
						break;
					}
				}
				((byte*)ptr)[5] = ((jsonItem == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271578), new object[]
					{
						VendorRecipe.RecipeType,
						Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
					});
					result = null;
				}
				else
				{
					JsonItem jsonItem2 = null;
					foreach (KeyValuePair<int, List<JsonItem>> keyValuePair3 in dictionary_0)
					{
						jsonItem2 = keyValuePair3.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_9));
						((byte*)ptr)[6] = ((jsonItem2 != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							((byte*)ptr)[7] = ((!dictionary2.ContainsKey(keyValuePair3.Key)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								dictionary2.Add(keyValuePair3.Key, new List<JsonItem>());
							}
							dictionary2[keyValuePair3.Key].Add(jsonItem2);
							break;
						}
					}
					((byte*)ptr)[8] = ((jsonItem2 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107272005), new object[]
						{
							VendorRecipe.RecipeType,
							Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
						});
						result = null;
					}
					else
					{
						JsonItem jsonItem3 = null;
						foreach (KeyValuePair<int, List<JsonItem>> keyValuePair4 in dictionary_0)
						{
							jsonItem3 = keyValuePair4.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_10));
							((byte*)ptr)[9] = ((jsonItem3 != null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								((byte*)ptr)[10] = ((!dictionary2.ContainsKey(keyValuePair4.Key)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									dictionary2.Add(keyValuePair4.Key, new List<JsonItem>());
								}
								dictionary2[keyValuePair4.Key].Add(jsonItem3);
								break;
							}
						}
						((byte*)ptr)[11] = ((jsonItem3 == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271988), new object[]
							{
								VendorRecipe.RecipeType,
								Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
							});
							result = null;
						}
						else
						{
							JsonItem jsonItem4 = null;
							foreach (KeyValuePair<int, List<JsonItem>> keyValuePair5 in dictionary_0)
							{
								jsonItem4 = keyValuePair5.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_11));
								((byte*)ptr)[12] = ((jsonItem4 != null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 12) != 0)
								{
									((byte*)ptr)[13] = ((!dictionary2.ContainsKey(keyValuePair5.Key)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 13) != 0)
									{
										dictionary2.Add(keyValuePair5.Key, new List<JsonItem>());
									}
									dictionary2[keyValuePair5.Key].Add(jsonItem4);
									break;
								}
							}
							((byte*)ptr)[14] = ((jsonItem4 == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271935), new object[]
								{
									VendorRecipe.RecipeType,
									Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
								});
								result = null;
							}
							else
							{
								JsonItem jsonItem5 = null;
								foreach (KeyValuePair<int, List<JsonItem>> keyValuePair6 in dictionary_0)
								{
									jsonItem5 = keyValuePair6.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_12));
									((byte*)ptr)[15] = ((jsonItem5 != null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 15) != 0)
									{
										((byte*)ptr)[16] = ((!dictionary2.ContainsKey(keyValuePair6.Key)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 16) != 0)
										{
											dictionary2.Add(keyValuePair6.Key, new List<JsonItem>());
										}
										dictionary2[keyValuePair6.Key].Add(jsonItem5);
										break;
									}
								}
								((byte*)ptr)[17] = ((jsonItem5 == null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 17) != 0)
								{
									Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271854), new object[]
									{
										VendorRecipe.RecipeType,
										Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
									});
									result = null;
								}
								else
								{
									JsonItem jsonItem6 = null;
									foreach (KeyValuePair<int, List<JsonItem>> keyValuePair7 in dictionary_0)
									{
										jsonItem6 = keyValuePair7.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_13));
										((byte*)ptr)[18] = ((jsonItem6 != null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 18) != 0)
										{
											((byte*)ptr)[19] = ((!dictionary2.ContainsKey(keyValuePair7.Key)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 19) != 0)
											{
												dictionary2.Add(keyValuePair7.Key, new List<JsonItem>());
											}
											dictionary2[keyValuePair7.Key].Add(jsonItem6);
											break;
										}
									}
									((byte*)ptr)[20] = ((jsonItem6 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 20) != 0)
									{
										Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271837), new object[]
										{
											VendorRecipe.RecipeType,
											Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
										});
										result = null;
									}
									else
									{
										Dictionary<int, List<JsonItem>> dictionary3 = new Dictionary<int, List<JsonItem>>();
										foreach (KeyValuePair<int, List<JsonItem>> keyValuePair8 in dictionary_0)
										{
											IEnumerable<JsonItem> enumerable = keyValuePair8.Value.Where(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_14));
											foreach (JsonItem item in enumerable)
											{
												if (dictionary3.Sum(new Func<KeyValuePair<int, List<JsonItem>>, int>(VendorRecipe.<>c.<>9.method_15)) == 2)
												{
													break;
												}
												((byte*)ptr)[21] = ((!dictionary3.ContainsKey(keyValuePair8.Key)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 21) != 0)
												{
													dictionary3.Add(keyValuePair8.Key, new List<JsonItem>());
												}
												((byte*)ptr)[22] = ((!dictionary2.ContainsKey(keyValuePair8.Key)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 22) != 0)
												{
													dictionary2.Add(keyValuePair8.Key, new List<JsonItem>());
												}
												dictionary3[keyValuePair8.Key].Add(item);
												dictionary2[keyValuePair8.Key].Add(item);
											}
											if (dictionary3.Sum(new Func<KeyValuePair<int, List<JsonItem>>, int>(VendorRecipe.<>c.<>9.method_16)) == 2)
											{
												break;
											}
										}
										if (dictionary3.Sum(new Func<KeyValuePair<int, List<JsonItem>>, int>(VendorRecipe.<>c.<>9.method_17)) < 2)
										{
											Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271240), new object[]
											{
												VendorRecipe.RecipeType,
												Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
											});
											result = null;
										}
										else
										{
											Dictionary<int, List<JsonItem>> dictionary4 = new Dictionary<int, List<JsonItem>>();
											foreach (KeyValuePair<int, List<JsonItem>> keyValuePair9 in dictionary_0)
											{
												JsonItem jsonItem7 = keyValuePair9.Value.FirstOrDefault(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_18));
												((byte*)ptr)[23] = ((jsonItem7 != null) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 23) != 0)
												{
													((byte*)ptr)[24] = ((!dictionary2.ContainsKey(keyValuePair9.Key)) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 24) != 0)
													{
														dictionary2.Add(keyValuePair9.Key, new List<JsonItem>());
													}
													dictionary2[keyValuePair9.Key].Add(jsonItem7);
													dictionary4.Add(keyValuePair9.Key, new List<JsonItem>
													{
														jsonItem7
													});
													break;
												}
											}
											((byte*)ptr)[25] = ((!dictionary4.Any<KeyValuePair<int, List<JsonItem>>>()) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 25) != 0)
											{
												foreach (KeyValuePair<int, List<JsonItem>> keyValuePair10 in dictionary_0)
												{
													VendorRecipe.Class355 @class = new VendorRecipe.Class355();
													@class.list_0 = ItemData.Weapons1H;
													@class.list_0.AddRange(ItemData.Shields);
													IEnumerable<JsonItem> enumerable2 = keyValuePair10.Value.Where(new Func<JsonItem, bool>(@class.method_0));
													foreach (JsonItem item2 in enumerable2)
													{
														if (dictionary4.Sum(new Func<KeyValuePair<int, List<JsonItem>>, int>(VendorRecipe.<>c.<>9.method_19)) == 2)
														{
															break;
														}
														((byte*)ptr)[26] = ((!dictionary4.ContainsKey(keyValuePair10.Key)) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 26) != 0)
														{
															dictionary4.Add(keyValuePair10.Key, new List<JsonItem>());
														}
														((byte*)ptr)[27] = ((!dictionary2.ContainsKey(keyValuePair10.Key)) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 27) != 0)
														{
															dictionary2.Add(keyValuePair10.Key, new List<JsonItem>());
														}
														dictionary4[keyValuePair10.Key].Add(item2);
														dictionary2[keyValuePair10.Key].Add(item2);
													}
													if (dictionary4.Sum(new Func<KeyValuePair<int, List<JsonItem>>, int>(VendorRecipe.<>c.<>9.method_20)) == 2)
													{
														break;
													}
												}
												if (dictionary4.Sum(new Func<KeyValuePair<int, List<JsonItem>>, int>(VendorRecipe.<>c.<>9.method_21)) < 2)
												{
													Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271175), new object[]
													{
														VendorRecipe.RecipeType,
														Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType)
													});
													return null;
												}
											}
											result = dictionary2;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		public unsafe static Dictionary<int, List<JsonItem>> smethod_3(Dictionary<int, List<JsonItem>> dictionary_0)
		{
			void* ptr = stackalloc byte[17];
			Dictionary<int, List<JsonItem>> dictionary = new Dictionary<int, List<JsonItem>>();
			foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in dictionary_0)
			{
				dictionary.Add(keyValuePair.Key, new List<JsonItem>());
				dictionary[keyValuePair.Key].AddRange(keyValuePair.Value.Where(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_22)));
			}
			List<JsonItem> list = dictionary_0.smethod_15<int>().Where(new Func<JsonItem, bool>(VendorRecipe.<>c.<>9.method_23)).Take(60 - dictionary.smethod_15<int>().Count).ToList<JsonItem>();
			int num = list.Sum(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_24));
			*(int*)((byte*)ptr + 8) = (int)Math.Floor((double)num / 40.0);
			*(double*)ptr = Math.Round(((double)num / 40.0 - (double)(*(int*)((byte*)ptr + 8))) * 40.0);
			foreach (JsonItem jsonItem in list.OrderByDescending(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_25)).ToList<JsonItem>())
			{
				((byte*)ptr)[12] = (((double)jsonItem.Quality <= *(double*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					*(double*)ptr = *(double*)ptr - (double)jsonItem.Quality;
					list.Remove(jsonItem);
				}
				((byte*)ptr)[13] = ((*(double*)ptr == 0.0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					break;
				}
			}
			using (List<JsonItem>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					VendorRecipe.Class356 @class = new VendorRecipe.Class356();
					@class.jsonItem_0 = enumerator3.Current;
					((byte*)ptr)[14] = ((!dictionary.ContainsKey(@class.jsonItem_0.StashId)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						dictionary.Add(@class.jsonItem_0.StashId, new List<JsonItem>());
					}
					JsonItem jsonItem2 = dictionary_0[@class.jsonItem_0.StashId].FirstOrDefault(new Func<JsonItem, bool>(@class.method_0));
					((byte*)ptr)[15] = ((jsonItem2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						dictionary[@class.jsonItem_0.StashId].Add(jsonItem2);
					}
				}
			}
			((byte*)ptr)[16] = ((dictionary.smethod_15<int>().Count == 0) ? 1 : 0);
			Dictionary<int, List<JsonItem>> result;
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				result = null;
			}
			else
			{
				result = dictionary;
			}
			return result;
		}

		public unsafe static Dictionary<int, List<JsonItem>> smethod_4(Dictionary<int, List<JsonItem>> dictionary_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!dictionary_0.Any<KeyValuePair<int, List<JsonItem>>>()) ? 1 : 0);
			Dictionary<int, List<JsonItem>> result;
			if (*(sbyte*)ptr != 0)
			{
				result = null;
			}
			else
			{
				Dictionary<int, List<JsonItem>> dictionary = new Dictionary<int, List<JsonItem>>();
				List<JsonItem> list = new List<JsonItem>();
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in dictionary_0)
				{
					foreach (JsonItem jsonItem in keyValuePair.Value.OrderBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_26)).ThenBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_27)))
					{
						List<Position> source = InventoryManager.smethod_8(list, jsonItem);
						((byte*)ptr)[1] = (source.Any<Position>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) == 0)
						{
							return dictionary;
						}
						Position position = source.First<Position>();
						JsonItem jsonItem2 = jsonItem.method_2();
						jsonItem2.x = position.x;
						jsonItem2.y = position.y;
						list.Add(jsonItem2);
						((byte*)ptr)[2] = ((!dictionary.ContainsKey(keyValuePair.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							dictionary.Add(keyValuePair.Key, new List<JsonItem>());
						}
						dictionary[keyValuePair.Key].Add(jsonItem);
					}
				}
				result = dictionary;
			}
			return result;
		}

		private unsafe static Dictionary<int, List<JsonItem>> smethod_5()
		{
			void* ptr = stackalloc byte[6];
			Dictionary<int, List<JsonItem>> dictionary = new Dictionary<int, List<JsonItem>>();
			foreach (int num in Class255.class105_0.method_8<int>(ConfigOptions.VendorRecipeStashList))
			{
				*(int*)ptr = num;
				dictionary.Add(*(int*)ptr, new List<JsonItem>());
				dictionary[*(int*)ptr].AddRange(Stashes.Items[*(int*)ptr]);
				string recipeType = VendorRecipe.RecipeType;
				string text = recipeType;
				if (text != null)
				{
					if (!(text == VendorRecipe.getString_0(107367007)))
					{
						if (!(text == VendorRecipe.getString_0(107366998)))
						{
							if (!(text == VendorRecipe.getString_0(107393059)))
							{
								if (!(text == VendorRecipe.getString_0(107385577)))
								{
									if (text == VendorRecipe.getString_0(107393009))
									{
										dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_32));
									}
								}
								else
								{
									dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_31));
								}
							}
							else
							{
								dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_30));
							}
						}
						else
						{
							dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_29));
						}
					}
					else
					{
						dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_28));
					}
				}
				((byte*)ptr)[4] = (VendorRecipe.UsingIdentifedType ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					string text2 = Class255.class105_0.method_3(ConfigOptions.VendorRecipeIdentifiedType);
					string text3 = text2;
					if (text3 != null)
					{
						if (!(text3 == VendorRecipe.getString_0(107367637)))
						{
							if (text3 == VendorRecipe.getString_0(107367612))
							{
								dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_34));
							}
						}
						else
						{
							dictionary[*(int*)ptr].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_33));
						}
					}
				}
				((byte*)ptr)[5] = ((!dictionary[*(int*)ptr].Any<JsonItem>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					dictionary.Remove(*(int*)ptr);
				}
			}
			return dictionary;
		}

		private unsafe static bool smethod_6()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, VendorRecipe.getString_0(107271110));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				UI.smethod_1();
				((byte*)ptr)[2] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, VendorRecipe.getString_0(107271077));
					((byte*)ptr)[1] = 0;
				}
				else
				{
					UI.smethod_80();
					Images images_ = VendorRecipe.smethod_1();
					Position position;
					((byte*)ptr)[3] = ((!UI.smethod_3(out position, images_, VendorRecipe.getString_0(107401885))) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						Class181.smethod_2(Enum11.const_2, VendorRecipe.getString_0(107271528), new object[]
						{
							Class255.class105_0.method_3(ConfigOptions.VendorRecipeVendor)
						});
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[4] = ((Class255.class105_0.method_8<int>(ConfigOptions.VendorRecipeStashList).Count == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							Class181.smethod_3(Enum11.const_2, VendorRecipe.getString_0(107271503));
							((byte*)ptr)[1] = 0;
						}
						else
						{
							((byte*)ptr)[1] = 1;
						}
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private static bool UsingExaltedShardRecipe
		{
			get
			{
				return VendorRecipe.RecipeType == VendorRecipe.getString_0(107385577);
			}
		}

		private static bool UsingIdentifedType
		{
			get
			{
				return VendorRecipe.RecipeType != VendorRecipe.getString_0(107393059) && VendorRecipe.RecipeType != VendorRecipe.getString_0(107393054);
			}
		}

		private unsafe static void smethod_7()
		{
			void* ptr = stackalloc byte[9];
			JsonTab jsonTab = Stashes.smethod_14(VendorRecipe.getString_0(107396243));
			UI.smethod_35(jsonTab.i, false, 1);
			if (VendorRecipe.RecipeType != VendorRecipe.getString_0(107393054) && VendorRecipe.RecipeType != VendorRecipe.getString_0(107393009))
			{
				Thread.Sleep(200);
				UI.smethod_32(0, 0, Enum2.const_3, true);
				Thread.Sleep(100);
				Win32.smethod_9();
			}
			else
			{
				*(int*)ptr = UI.smethod_83(12);
				List<JsonItem> list = new List<JsonItem>();
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[8] = ((*(int*)((byte*)ptr + 4) < *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) == 0)
					{
						break;
					}
					JsonItem jsonItem = new JsonItem
					{
						w = 1,
						h = 1
					};
					Position position = InventoryManager.smethod_8(list, jsonItem).First<Position>();
					jsonItem.x = position.x;
					jsonItem.y = position.y;
					list.Add(jsonItem);
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				foreach (JsonItem jsonItem2 in list)
				{
					UI.smethod_32(jsonItem2.x, jsonItem2.y, Enum2.const_3, true);
					UI.smethod_36(Enum2.const_3, 1.0);
					Win32.smethod_9();
				}
			}
		}

		private static bool smethod_8(JsonItem jsonItem_0)
		{
			Class252 @class = Class252.smethod_0(VendorRecipe.MaximumPrice, false);
			Class252 class2 = Class252.smethod_0(jsonItem_0.note, false);
			return @class.CurrencyId == class2.CurrencyId && @class.Amount >= class2.Amount;
		}

		public static Position smethod_9()
		{
			Position position_;
			Position result;
			if (!UI.smethod_3(out position_, Images.TradeCancel, VendorRecipe.getString_0(107381227)))
			{
				result = new Position();
			}
			else
			{
				Bitmap item = Class308.smethod_0(Images.TradeCancel).Item1;
				result = position_.smethod_6(Util.smethod_22(-450.0 * UI.GameScale + (double)(item.Width / 2)), item.Height / 2);
			}
			return result;
		}

		static VendorRecipe()
		{
			Strings.CreateGetStringDelegate(typeof(VendorRecipe));
		}

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class351
		{
			[DebuggerStepThrough]
			internal void method_0()
			{
				VendorRecipe.Class351.Class352 @class = new VendorRecipe.Class351.Class352();
				@class.class351_0 = this;
				@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
				asyncVoidMethodBuilder_.Start<VendorRecipe.Class351.Class352>(ref @class);
			}

			internal unsafe void method_1()
			{
				void* ptr = stackalloc byte[19];
				try
				{
					VendorRecipe.Class353 @class = new VendorRecipe.Class353();
					Control control = this.mainForm_0;
					Action method;
					if ((method = this.action_0) == null)
					{
						method = (this.action_0 = new Action(this.method_2));
					}
					control.Invoke(method);
					((byte*)ptr)[2] = ((!VendorRecipe.smethod_6()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						this.mainForm_0.method_109();
					}
					else
					{
						Dictionary<int, List<JsonItem>> dictionary = null;
						Dictionary<int, List<JsonItem>> dictionary2 = VendorRecipe.smethod_5();
						@class.list_0 = new List<JsonItem>();
						*(byte*)ptr = 1;
						((byte*)ptr)[1] = 0;
						for (;;)
						{
							((byte*)ptr)[18] = 1;
							((byte*)ptr)[3] = (VendorRecipe.UsingExaltedShardRecipe ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								dictionary2 = VendorRecipe.smethod_5();
								using (Dictionary<int, List<JsonItem>>.Enumerator enumerator = dictionary2.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										KeyValuePair<int, List<JsonItem>> keyValuePair = enumerator.Current;
										List<JsonItem> list = dictionary2[keyValuePair.Key];
										Predicate<JsonItem> match;
										if ((match = @class.predicate_0) == null)
										{
											match = (@class.predicate_0 = new Predicate<JsonItem>(@class.method_0));
										}
										list.RemoveAll(match);
										((byte*)ptr)[4] = (byte)(*(sbyte*)ptr);
										if (*(sbyte*)((byte*)ptr + 4) != 0)
										{
											dictionary2[keyValuePair.Key].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_0));
										}
										else
										{
											dictionary2[keyValuePair.Key].RemoveAll(new Predicate<JsonItem>(VendorRecipe.<>c.<>9.method_1));
										}
									}
									goto IL_7A;
								}
								continue;
							}
							IL_7A:
							dictionary = VendorRecipe.smethod_2(dictionary2);
							((byte*)ptr)[5] = ((dictionary == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								((byte*)ptr)[6] = (VendorRecipe.UsingExaltedShardRecipe ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 6) == 0)
								{
									break;
								}
								((byte*)ptr)[7] = (byte)(*(sbyte*)ptr);
								if (*(sbyte*)((byte*)ptr + 7) != 0)
								{
									*(byte*)ptr = 0;
									((byte*)ptr)[1] = 1;
									continue;
								}
								((byte*)ptr)[8] = (byte)(*(sbyte*)((byte*)ptr + 1));
								if (*(sbyte*)((byte*)ptr + 8) != 0)
								{
									break;
								}
							}
							List<JsonItem> list2 = new List<JsonItem>();
							((byte*)ptr)[9] = ((!UI.smethod_13(1)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								goto IL_6A2;
							}
							foreach (KeyValuePair<int, List<JsonItem>> keyValuePair2 in dictionary)
							{
								UI.smethod_35(keyValuePair2.Key, false, 1);
								JsonTab jsonTab = Stashes.smethod_11(keyValuePair2.Key);
								Enum11 enum11_ = Enum11.const_3;
								string string_ = VendorRecipe.Class351.getString_0(107249575);
								object[] array = new object[1];
								array[0] = string.Join(VendorRecipe.Class351.getString_0(107399271), dictionary.smethod_15<int>().Select(new Func<JsonItem, string>(VendorRecipe.<>c.<>9.method_2)));
								Class181.smethod_2(enum11_, string_, array);
								foreach (JsonItem jsonItem in keyValuePair2.Value.OrderBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_3)).ThenBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_4)))
								{
									UI.smethod_34(jsonTab.type, jsonItem.x, jsonItem.y, Enum2.const_2, false);
									Win32.smethod_9();
									Thread.Sleep(100);
									Position position = InventoryManager.smethod_8(list2, jsonItem).First<Position>();
									JsonItem jsonItem2 = jsonItem.method_2();
									jsonItem2.x = position.x;
									jsonItem2.y = position.y;
									list2.Add(jsonItem2);
									dictionary2[keyValuePair2.Key].Remove(jsonItem);
									@class.list_0.Add(jsonItem);
									((byte*)ptr)[10] = ((!dictionary2[keyValuePair2.Key].Any<JsonItem>()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 10) != 0)
									{
										dictionary2.Remove(keyValuePair2.Key);
									}
								}
							}
							Win32.smethod_14(VendorRecipe.Class351.getString_0(107396182), false);
							Thread.Sleep(500);
							Images images_ = VendorRecipe.smethod_1();
							Bitmap item = Class308.smethod_0(images_).Item1;
							Position position_;
							((byte*)ptr)[11] = ((!UI.smethod_3(out position_, images_, VendorRecipe.Class351.getString_0(107401889))) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 11) != 0)
							{
								goto IL_6C7;
							}
							Win32.smethod_5(position_.smethod_6(item.Width / 2, item.Height / 2), false);
							Thread.Sleep(200);
							Win32.smethod_2(true);
							DateTime t = DateTime.Now.AddSeconds(4.0);
							Position position2 = null;
							item = Class308.smethod_0(Images.VendorSellItems).Item1;
							for (;;)
							{
								((byte*)ptr)[13] = ((DateTime.Now < t) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 13) == 0)
								{
									break;
								}
								((byte*)ptr)[12] = (UI.smethod_3(out position2, Images.VendorSellItems, VendorRecipe.Class351.getString_0(107235131)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 12) != 0)
								{
									break;
								}
								Thread.Sleep(50);
							}
							if (Position.smethod_0(position2, null) || !position2.IsVisible)
							{
								goto IL_704;
							}
							Win32.smethod_5(position2.smethod_6(item.Width / 2, item.Height / 2), false);
							Thread.Sleep(200);
							Win32.smethod_2(true);
							t = DateTime.Now.AddSeconds(3.0);
							do
							{
								((byte*)ptr)[15] = ((DateTime.Now < t) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 15) == 0)
								{
									break;
								}
								((byte*)ptr)[14] = (UI.smethod_69() ? 1 : 0);
							}
							while (*(sbyte*)((byte*)ptr + 14) == 0);
							((byte*)ptr)[16] = ((!UI.smethod_69()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 16) != 0)
							{
								goto IL_726;
							}
							foreach (JsonItem jsonItem3 in list2.OrderBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_5)).ThenBy(new Func<JsonItem, int>(VendorRecipe.<>c.<>9.method_6)))
							{
								UI.smethod_32(jsonItem3.x, jsonItem3.y, Enum2.const_4, true);
								Win32.smethod_9();
							}
							Position position3;
							((byte*)ptr)[17] = ((!UI.smethod_3(out position3, Images.TradeCancel, VendorRecipe.Class351.getString_0(107381231))) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 17) != 0)
							{
								goto IL_748;
							}
							Win32.smethod_5(VendorRecipe.smethod_9(), false);
							Thread.Sleep(200);
							Win32.smethod_2(true);
							Thread.Sleep(200);
							Win32.smethod_14(VendorRecipe.Class351.getString_0(107396182), false);
							Thread.Sleep(200);
							VendorRecipe.smethod_7();
						}
						Class181.smethod_3(Enum11.const_0, VendorRecipe.Class351.getString_0(107249923));
						this.mainForm_0.method_109();
						return;
						IL_6A2:
						Class181.smethod_3(Enum11.const_2, VendorRecipe.Class351.getString_0(107249604));
						this.mainForm_0.method_109();
						return;
						IL_6C7:
						Class181.smethod_2(Enum11.const_2, VendorRecipe.Class351.getString_0(107271532), new object[]
						{
							Class255.class105_0.method_3(ConfigOptions.VendorRecipeVendor)
						});
						this.mainForm_0.method_109();
						return;
						IL_704:
						Class181.smethod_3(Enum11.const_2, VendorRecipe.Class351.getString_0(107250034));
						this.mainForm_0.method_109();
						return;
						IL_726:
						Class181.smethod_3(Enum11.const_2, VendorRecipe.Class351.getString_0(107250021));
						this.mainForm_0.method_109();
						return;
						IL_748:
						Class181.smethod_3(Enum11.const_2, VendorRecipe.Class351.getString_0(107249940));
						this.mainForm_0.method_109();
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception ex)
				{
					Class181.smethod_2(Enum11.const_2, VendorRecipe.Class351.getString_0(107249890), new object[]
					{
						ex
					});
				}
			}

			internal void method_2()
			{
				this.mainForm_0.tabControl_4.SelectedIndex = 0;
			}

			static Class351()
			{
				Strings.CreateGetStringDelegate(typeof(VendorRecipe.Class351));
			}

			public MainForm mainForm_0;

			public Action action_0;

			public Action action_1;

			[NonSerialized]
			internal static GetString getString_0;

			private sealed class Class352 : IAsyncStateMachine
			{
				void IAsyncStateMachine.MoveNext()
				{
					int num = this.int_0;
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							Action action;
							if ((action = this.class351_0.action_1) == null)
							{
								action = (this.class351_0.action_1 = new Action(this.class351_0.method_1));
							}
							awaiter = Task.Run(action).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.int_0 = 0;
								this.configuredTaskAwaiter_0 = awaiter;
								VendorRecipe.Class351.Class352 @class = this;
								this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, VendorRecipe.Class351.Class352>(ref awaiter, ref @class);
								return;
							}
						}
						else
						{
							awaiter = this.configuredTaskAwaiter_0;
							this.configuredTaskAwaiter_0 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.int_0 = -1;
						}
						awaiter.GetResult();
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					this.int_0 = -2;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				public int int_0;

				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				public VendorRecipe.Class351 class351_0;

				private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter_0;
			}
		}

		[CompilerGenerated]
		private sealed class Class353
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return this.list_0.Contains(jsonItem_0);
			}

			public List<JsonItem> list_0;

			public Predicate<JsonItem> predicate_0;
		}

		[CompilerGenerated]
		private sealed class Class355
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return this.list_0.Contains(jsonItem_0.CleanedTypeLine);
			}

			public List<string> list_0;
		}

		[CompilerGenerated]
		private sealed class Class356
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}
	}
}
