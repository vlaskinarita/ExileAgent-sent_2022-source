using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.Models.Items;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Classes
{
	public static class API
	{
		public static bool DataLoaded { get; private set; }

		public unsafe static bool smethod_0(string string_0)
		{
			void* ptr = stackalloc byte[9];
			JObject jobject = JsonConvert.DeserializeObject<JObject>(string_0);
			((byte*)ptr)[4] = ((!jobject.ContainsKey(API.getString_0(107451058))) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				((byte*)ptr)[5] = 0;
			}
			else
			{
				API.list_0 = new List<ApiItem>();
				API.list_1 = jobject[API.getString_0(107451058)].ToObject<List<string>>();
				API.list_2 = jobject[API.getString_0(107451045)].ToObject<List<string>>();
				API.list_3 = jobject[API.getString_0(107451064)].ToObject<List<string>>();
				foreach (JToken jtoken in ((IEnumerable<JToken>)jobject[API.getString_0(107451535)]))
				{
					JProperty jproperty = (JProperty)jtoken;
					ApiItem apiItem = new ApiItem
					{
						Name = jproperty.Name,
						Id = jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451526)].ToString(),
						Text = jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107398467)].ToString(),
						Stack = int.Parse(jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451553)].ToString()),
						Type = jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107370087)].ToString(),
						Class = jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451544)].ToString()
					};
					*(int*)ptr = 0;
					((byte*)ptr)[7] = ((jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451503)] != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						int.TryParse(jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451503)].ToString(), out *(int*)ptr);
					}
					apiItem.Cost = *(int*)ptr;
					string cardReward = null;
					((byte*)ptr)[6] = 0;
					((byte*)ptr)[8] = ((jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451494)] != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						cardReward = jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451494)].ToString();
						((byte*)ptr)[6] = (((bool)jobject[API.getString_0(107451535)][jproperty.Name][API.getString_0(107451517)]) ? 1 : 0);
					}
					apiItem.CardReward = cardReward;
					apiItem.UniqueReward = (*(sbyte*)((byte*)ptr + 6) != 0);
					API.list_0.Add(apiItem);
				}
				API.DataLoaded = true;
				((byte*)ptr)[5] = 1;
			}
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		public static List<string> smethod_1()
		{
			return API.list_1;
		}

		public unsafe static string smethod_2(string string_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!string_0.Contains(API.getString_0(107369071))) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = string_0;
			}
			else
			{
				string text = string_0.Replace(API.getString_0(107367748), API.getString_0(107397424));
				((byte*)ptr)[1] = ((!text.EndsWith(API.getString_0(107369071))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					text = text.Substring(0, text.IndexOf(API.getString_0(107369071)) + 4);
				}
				foreach (string text2 in API.list_2)
				{
					((byte*)ptr)[2] = ((text2 == API.getString_0(107402238)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0 || (!string_0.ToLower().Contains(API.getString_0(107451464)) && !string_0.ToLower().Contains(API.getString_0(107451479))))
					{
						text = text.Replace(text2, API.getString_0(107397424));
					}
				}
				result = text.Trim();
			}
			return result;
		}

		public unsafe static string smethod_3(string string_0)
		{
			void* ptr = stackalloc byte[7];
			((byte*)ptr)[4] = ((!string_0.Contains(API.getString_0(107451430))) ? 1 : 0);
			string result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = string_0;
			}
			else
			{
				string text = string_0.Replace(API.getString_0(107367748), API.getString_0(107397424));
				((byte*)ptr)[5] = ((!text.StartsWith(API.getString_0(107451430))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					text = text.Substring(text.IndexOf(API.getString_0(107451430)));
				}
				*(int*)ptr = text.IndexOf(API.getString_0(107451405));
				((byte*)ptr)[6] = ((*(int*)ptr != -1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					text = text.Substring(0, *(int*)ptr);
				}
				result = text.Trim();
			}
			return result;
		}

		public static string smethod_4(string string_0)
		{
			API.Class186 @class = new API.Class186();
			@class.string_0 = string_0;
			return API.list_0.FirstOrDefault(new Func<ApiItem, bool>(@class.method_0)).Id;
		}

		public static string smethod_5(string string_0)
		{
			API.Class187 @class = new API.Class187();
			@class.string_0 = string_0;
			return API.list_0.FirstOrDefault(new Func<ApiItem, bool>(@class.method_0)).Text;
		}

		public static int smethod_6(string string_0)
		{
			API.Class188 @class = new API.Class188();
			@class.string_0 = string_0;
			ApiItem apiItem = API.list_0.FirstOrDefault(new Func<ApiItem, bool>(@class.method_0));
			return (apiItem != null) ? apiItem.Stack : 1;
		}

		public unsafe static ApiItem smethod_7(string string_0)
		{
			void* ptr = stackalloc byte[22];
			List<ApiItem> list = API.list_0.ToList<ApiItem>();
			List<ApiItem> list2 = new List<ApiItem>();
			string_0 = string_0.Replace(API.getString_0(107367748), API.getString_0(107397424));
			*(byte*)ptr = ((string_0.ToLower() == API.getString_0(107451396)) ? 1 : 0);
			ApiItem result;
			if (*(sbyte*)ptr != 0)
			{
				result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_0));
			}
			else if (string_0.ToLower() == API.getString_0(107451415) || string_0.ToLower() == API.getString_0(107451370))
			{
				result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_1));
			}
			else if (string_0.ToLower() == API.getString_0(107451385) || string_0.ToLower() == API.getString_0(107451380))
			{
				result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_2));
			}
			else
			{
				((byte*)ptr)[1] = ((string_0.ToLower() == API.getString_0(107451363)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_3));
				}
				else if (string_0.ToLower() == API.getString_0(107451354) || string_0.ToLower() == API.getString_0(107451309) || string_0.ToLower() == API.getString_0(107451300))
				{
					result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_4));
				}
				else
				{
					((byte*)ptr)[2] = ((string_0.ToLower() == API.getString_0(107451327)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_5));
					}
					else
					{
						((byte*)ptr)[3] = ((string_0.ToLower() == API.getString_0(107450758)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_6));
						}
						else
						{
							((byte*)ptr)[4] = ((string_0.ToLower() == API.getString_0(107450733)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_7));
							}
							else if (string_0.ToLower() == API.getString_0(107450744) || string_0.ToLower() == API.getString_0(107450699))
							{
								result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_8));
							}
							else
							{
								((byte*)ptr)[5] = ((string_0.ToLower() == API.getString_0(107450722)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 5) != 0)
								{
									result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_9));
								}
								else
								{
									((byte*)ptr)[6] = ((string_0.ToLower() == API.getString_0(107450713)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 6) != 0)
									{
										result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_10));
									}
									else
									{
										((byte*)ptr)[7] = ((string_0.ToLower() == API.getString_0(107450672)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 7) != 0)
										{
											result = list.First(new Func<ApiItem, bool>(API.<>c.<>9.method_11));
										}
										else
										{
											((byte*)ptr)[8] = (string_0.EndsWith(API.getString_0(107450691)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 8) != 0)
											{
												result = new ApiItem(string_0, API.getString_0(107398386), string_0 + API.getString_0(107450642), 1, API.getString_0(107371691), API.getString_0(107450629));
											}
											else
											{
												((byte*)ptr)[9] = ((!string_0.ToLower().StartsWith(API.getString_0(107450604))) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 9) != 0)
												{
													list.RemoveAll(new Predicate<ApiItem>(API.<>c.<>9.method_12));
												}
												((byte*)ptr)[10] = (string_0.Contains(API.getString_0(107450627)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 10) != 0)
												{
													result = new ApiItem(string_0, API.getString_0(107398386), string_0, API.smethod_6(string_0), API.getString_0(107371691), string.Empty);
												}
												else
												{
													((byte*)ptr)[11] = (string_0.Contains(API.getString_0(107450574)) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 11) != 0)
													{
														result = new ApiItem(string_0, API.getString_0(107398386), string_0, 1, API.getString_0(107371691), string.Empty);
													}
													else if (string_0 == API.getString_0(107450585) || string_0 == API.getString_0(107450544) || string_0 == API.getString_0(107450535) || string_0 == API.getString_0(107450558))
													{
														result = new ApiItem(string_0, API.getString_0(107398386), string_0, 1, API.getString_0(107371691), string.Empty);
													}
													else
													{
														foreach (ApiItem apiItem in list)
														{
															((byte*)ptr)[12] = ((string_0.ToLower() == apiItem.Text.ToLower()) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 12) != 0)
															{
																list2.Add(apiItem);
															}
															((byte*)ptr)[13] = ((string_0.ToLower() == apiItem.Id.ToLower()) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 13) != 0)
															{
																return apiItem;
															}
														}
														((byte*)ptr)[14] = (list2.Any<ApiItem>() ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 14) != 0)
														{
															ApiItem apiItem2 = list2.First<ApiItem>();
															((byte*)ptr)[15] = ((list2.First<ApiItem>().Type == API.getString_0(107453581)) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 15) != 0)
															{
																result = new ApiItem(apiItem2.Name, apiItem2.Id, apiItem2.Text.Replace(apiItem2.Id, API.getString_0(107397424)).Trim(), apiItem2.Stack, API.getString_0(107453581), API.getString_0(107401287));
															}
															else
															{
																result = apiItem2;
															}
														}
														else
														{
															foreach (ApiItem apiItem3 in list)
															{
																((byte*)ptr)[16] = (apiItem3.Text.ToLower().Contains(string_0.ToLower()) ? 1 : 0);
																if (*(sbyte*)((byte*)ptr + 16) != 0)
																{
																	return apiItem3;
																}
																((byte*)ptr)[17] = (apiItem3.Id.ToLower().Contains(string_0.ToLower()) ? 1 : 0);
																if (*(sbyte*)((byte*)ptr + 17) != 0)
																{
																	return apiItem3;
																}
															}
															foreach (ApiItem apiItem4 in list)
															{
																if (string_0.ToLower().Contains(apiItem4.Text.ToLower()) || apiItem4.Text.ToLower().Contains(string_0.ToLower()))
																{
																	list2.Add(apiItem4);
																}
															}
															((byte*)ptr)[18] = ((list2.Count == 1) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 18) != 0)
															{
																ApiItem apiItem5 = list2.First<ApiItem>();
																((byte*)ptr)[19] = ((apiItem5.Type == API.getString_0(107361927)) ? 1 : 0);
																if (*(sbyte*)((byte*)ptr + 19) != 0)
																{
																	((byte*)ptr)[20] = ((string_0.ToLower() != apiItem5.Name.ToLower()) ? 1 : 0);
																	if (*(sbyte*)((byte*)ptr + 20) != 0)
																	{
																		return new ApiItem(string_0, API.getString_0(107398386), string_0, 1, API.getString_0(107371691), API.getString_0(107450549));
																	}
																}
																result = apiItem5;
															}
															else
															{
																((byte*)ptr)[21] = ((list2.Count > 0) ? 1 : 0);
																if (*(sbyte*)((byte*)ptr + 21) != 0)
																{
																	ApiItem apiItem6 = list2.OrderByDescending(new Func<ApiItem, int>(API.<>c.<>9.method_13)).First<ApiItem>();
																	result = apiItem6;
																}
																else
																{
																	ApiItem apiItem7 = new ApiItem(string_0, API.getString_0(107371691), string_0, API.smethod_6(string_0), API.getString_0(107371691), string.Empty);
																	result = apiItem7;
																}
															}
														}
													}
												}
											}
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

		public unsafe static ApiItem smethod_8(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[3];
			API.Class189 @class = new API.Class189();
			@class.jsonItem_0 = jsonItem_0;
			string typeLine = @class.jsonItem_0.typeLine;
			string text = typeLine;
			if (text != null)
			{
				if (text == API.getString_0(107451040))
				{
					API.Class190 class2 = new API.Class190();
					class2.class189_0 = @class;
					class2.string_0 = ((class2.class189_0.jsonItem_0.ItemRarity == ItemRarity.Divination) ? API.getString_0(107361927) : API.getString_0(107352653));
					ApiItem apiItem = API.list_0.FirstOrDefault(new Func<ApiItem, bool>(class2.method_0));
					return apiItem ?? new ApiItem(class2.class189_0.jsonItem_0.typeLine, API.getString_0(107398386), class2.class189_0.jsonItem_0.typeLine, API.smethod_6(class2.class189_0.jsonItem_0.typeLine), API.getString_0(107371691), API.getString_0(107397424));
				}
				if (text == API.getString_0(107450995))
				{
					API.Class191 class3 = new API.Class191();
					class3.class189_0 = @class;
					class3.string_0 = ((class3.class189_0.jsonItem_0.ItemRarity == ItemRarity.Divination) ? API.getString_0(107361927) : API.getString_0(107352653));
					ApiItem apiItem2 = API.list_0.FirstOrDefault(new Func<ApiItem, bool>(class3.method_0));
					return apiItem2 ?? new ApiItem(class3.class189_0.jsonItem_0.typeLine, API.getString_0(107398386), class3.class189_0.jsonItem_0.typeLine, API.smethod_6(class3.class189_0.jsonItem_0.typeLine), API.getString_0(107371691), API.getString_0(107397424));
				}
			}
			ApiItem result;
			if (@class.jsonItem_0.descrText != null && @class.jsonItem_0.descrText.Contains(API.getString_0(107450982)))
			{
				result = new ApiItem(@class.jsonItem_0.Name, API.getString_0(107398386), @class.jsonItem_0.typeLine, 1, API.getString_0(107450889), API.getString_0(107450912));
			}
			else
			{
				*(byte*)ptr = ((@class.jsonItem_0.ItemRarity == ItemRarity.Unique) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					string type = API.smethod_9(@class.jsonItem_0);
					ApiItem apiItem3 = new ApiItem(@class.jsonItem_0.Name, API.getString_0(107398386), @class.jsonItem_0.typeLine.Replace(API.getString_0(107367748), API.getString_0(107397424)), API.smethod_6(@class.jsonItem_0.Name), type, API.getString_0(107397424));
					result = apiItem3;
				}
				else
				{
					if (@class.jsonItem_0.ItemRarity == ItemRarity.Rare || @class.jsonItem_0.ItemRarity == ItemRarity.Magic || @class.jsonItem_0.ItemRarity == ItemRarity.Normal)
					{
						string type2 = API.smethod_9(@class.jsonItem_0);
						((byte*)ptr)[1] = ((!@class.jsonItem_0.IsMap) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) != 0)
						{
							if (@class.jsonItem_0.influences != null || @class.jsonItem_0.Links == 6)
							{
								type2 = API.getString_0(107402826);
							}
							return new ApiItem(@class.jsonItem_0.Name, API.getString_0(107398386), @class.jsonItem_0.typeLine.Replace(API.getString_0(107367748), API.getString_0(107397424)), API.smethod_6(@class.jsonItem_0.Name), type2, API.getString_0(107397424));
						}
						((byte*)ptr)[2] = ((@class.jsonItem_0.ItemRarity == ItemRarity.Magic) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							return new ApiItem(@class.jsonItem_0.Name, API.getString_0(107398386), API.smethod_2(@class.jsonItem_0.typeLine), API.smethod_6(@class.jsonItem_0.Name), type2, API.getString_0(107397424));
						}
					}
					ApiItem apiItem4 = new ApiItem(@class.jsonItem_0.Name, API.getString_0(107398386), @class.jsonItem_0.typeLine.Replace(API.getString_0(107367748), API.getString_0(107397424)), API.smethod_6(@class.jsonItem_0.Name), API.smethod_9(@class.jsonItem_0), API.getString_0(107397424));
					result = apiItem4;
				}
			}
			return result;
		}

		public unsafe static string smethod_9(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[4];
			string result;
			if (jsonItem_0.descrText != null && jsonItem_0.descrText.Contains(API.getString_0(107450982)))
			{
				result = API.getString_0(107450855);
			}
			else
			{
				string typeLine = jsonItem_0.typeLine;
				string text = typeLine;
				if (text != null && (text == API.getString_0(107451040) || text == API.getString_0(107450995)))
				{
					result = ((jsonItem_0.ItemRarity == ItemRarity.Divination) ? API.getString_0(107361927) : API.getString_0(107352653));
				}
				else
				{
					string string_ = jsonItem_0.Name.Replace(API.getString_0(107367748), API.getString_0(107397424));
					*(byte*)ptr = (jsonItem_0.IsMap ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						((byte*)ptr)[1] = ((jsonItem_0.ItemRarity == ItemRarity.Magic) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) != 0)
						{
							string_ = API.smethod_2(jsonItem_0.typeLine);
						}
						else
						{
							((byte*)ptr)[2] = ((jsonItem_0.ItemRarity != ItemRarity.Unique) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								string_ = jsonItem_0.typeLine;
							}
						}
					}
					ApiItem apiItem = API.smethod_10(string_);
					((byte*)ptr)[3] = ((apiItem == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						result = API.getString_0(107371691);
					}
					else
					{
						result = apiItem.Type;
					}
				}
			}
			return result;
		}

		private static ApiItem smethod_10(string string_0)
		{
			API.Class192 @class = new API.Class192();
			@class.string_0 = string_0;
			return API.list_0.FirstOrDefault(new Func<ApiItem, bool>(@class.method_0));
		}

		public static string smethod_11()
		{
			string result;
			if (!API.list_3.Any<string>())
			{
				result = string.Empty;
			}
			else
			{
				result = API.list_3[new Random().Next(API.list_3.Count)];
			}
			return result;
		}

		public static IEnumerable<ApiItem> smethod_12()
		{
			return API.list_0.Where(new Func<ApiItem, bool>(API.<>c.<>9.method_14));
		}

		public static string[] smethod_13()
		{
			return API.list_0.Where(new Func<ApiItem, bool>(API.<>c.<>9.method_15)).Select(new Func<ApiItem, string>(API.<>c.<>9.method_16)).ToArray<string>();
		}

		public unsafe static int smethod_14(Item item_0)
		{
			void* ptr = stackalloc byte[7];
			API.Class193 @class = new API.Class193();
			@class.item_0 = item_0;
			((byte*)ptr)[4] = ((@class.item_0.Name == API.getString_0(107370125)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				((byte*)ptr)[5] = (@class.item_0.text.Contains(API.getString_0(107365893)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					*(int*)ptr = 3;
				}
				else
				{
					*(int*)ptr = 2;
				}
			}
			else
			{
				ApiItem apiItem = API.list_0.FirstOrDefault(new Func<ApiItem, bool>(@class.method_0));
				((byte*)ptr)[6] = ((apiItem == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					*(int*)ptr = 0;
				}
				else
				{
					*(int*)ptr = apiItem.Cost;
				}
			}
			return *(int*)ptr;
		}

		public unsafe static string smethod_15(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((jsonItem_0 == null) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = string.Empty;
			}
			else
			{
				((byte*)ptr)[1] = ((jsonItem_0.ItemRarity != ItemRarity.Magic) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = jsonItem_0.typeLine.Replace(API.getString_0(107367748), API.getString_0(107397424)).Replace(API.getString_0(107452021), API.getString_0(107397424));
				}
				else
				{
					((byte*)ptr)[2] = (jsonItem_0.IsMap ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = API.smethod_2(jsonItem_0.typeLine);
					}
					else
					{
						((byte*)ptr)[3] = (jsonItem_0.IsMavenInvitation ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = API.smethod_3(jsonItem_0.typeLine);
						}
						else
						{
							result = API.smethod_16(jsonItem_0.typeLine);
						}
					}
				}
			}
			return result;
		}

		public unsafe static string smethod_16(string string_0)
		{
			void* ptr = stackalloc byte[8];
			((byte*)ptr)[4] = ((string_0 == null) ? 1 : 0);
			string result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = string.Empty;
			}
			else
			{
				string text = string_0.Replace(API.getString_0(107367748), API.getString_0(107397424)).Replace(API.getString_0(107452021), API.getString_0(107397424));
				*(int*)ptr = text.IndexOf(API.getString_0(107451405));
				if (*(int*)ptr != -1 && !text.StartsWith(API.getString_0(107450874)))
				{
					text = text.Substring(0, *(int*)ptr);
				}
				((byte*)ptr)[5] = (ItemData.smethod_1(text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					result = text;
				}
				else
				{
					IEnumerable<string> enumerable = text.Split(new char[]
					{
						' '
					});
					for (;;)
					{
						((byte*)ptr)[7] = ((enumerable.Count<string>() > 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) == 0)
						{
							break;
						}
						enumerable = enumerable.Skip(1);
						text = string.Join(API.getString_0(107397461), enumerable);
						((byte*)ptr)[6] = (ItemData.smethod_1(text) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							goto IL_116;
						}
					}
					return text;
					IL_116:
					result = text;
				}
			}
			return result;
		}

		public static List<string> smethod_17(string string_0)
		{
			API.Class194 @class = new API.Class194();
			@class.string_0 = string_0;
			return API.list_0.Where(new Func<ApiItem, bool>(@class.method_0)).Select(new Func<ApiItem, string>(API.<>c.<>9.method_17)).ToList<string>();
		}

		public static ApiItem smethod_18(string string_0)
		{
			API.Class195 @class = new API.Class195();
			@class.string_0 = string_0;
			return API.list_0.FirstOrDefault(new Func<ApiItem, bool>(@class.method_0));
		}

		// Note: this type is marked as 'beforefieldinit'.
		static API()
		{
			Strings.CreateGetStringDelegate(typeof(API));
			API.DataLoaded = false;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool bool_0;

		private static List<ApiItem> list_0;

		private static List<string> list_1;

		private static List<string> list_2;

		private static List<string> list_3;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class186
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Name.ToLower() == this.string_0.ToLower();
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class187
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Id == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class188
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Name.ToLower() == this.string_0.ToLower();
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class189
		{
			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class190
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Text == this.class189_0.jsonItem_0.typeLine && apiItem_0.Type == this.string_0;
			}

			public string string_0;

			public API.Class189 class189_0;
		}

		[CompilerGenerated]
		private sealed class Class191
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Text == this.class189_0.jsonItem_0.typeLine && apiItem_0.Type == this.string_0;
			}

			public string string_0;

			public API.Class189 class189_0;
		}

		[CompilerGenerated]
		private sealed class Class192
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Text == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class193
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Text == this.item_0.Name && apiItem_0.Type == API.Class193.getString_0(107352684);
			}

			static Class193()
			{
				Strings.CreateGetStringDelegate(typeof(API.Class193));
			}

			public Item item_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class194
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Type == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class195
		{
			internal bool method_0(ApiItem apiItem_0)
			{
				return apiItem_0.Type == API.Class195.getString_0(107361961) && apiItem_0.Text == this.string_0;
			}

			static Class195()
			{
				Strings.CreateGetStringDelegate(typeof(API.Class195));
			}

			public string string_0;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
