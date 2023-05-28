using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ns0;
using ns27;
using ns29;
using ns35;
using ns8;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Classes
{
	public static class Stashes
	{
		public static List<JsonTab> Tabs { get; set; }

		public static Dictionary<string, JObject> Layout { get; set; }

		public static Dictionary<int, List<JsonItem>> Items { get; set; }

		public static Dictionary<int, List<string>> ForumTheadItemIds { get; set; }

		public static Enum9 LoadState { get; set; }

		public static void smethod_0(MainForm mainForm_1)
		{
			Stashes.mainForm_0 = mainForm_1;
			StashManager.smethod_0(mainForm_1);
			Stashes.LoadState = Enum9.const_0;
		}

		[DebuggerStepThrough]
		public static Task<bool> smethod_1()
		{
			Stashes.Class204 @class = new Stashes.Class204();
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder<bool>.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<Stashes.Class204>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		[DebuggerStepThrough]
		public static Task smethod_2(int int_1 = 0)
		{
			Stashes.Class208 @class = new Stashes.Class208();
			@class.int_1 = int_1;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<Stashes.Class208>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		public unsafe static void smethod_3(int int_1, Dictionary<string, object> dictionary_3)
		{
			void* ptr = stackalloc byte[3];
			Stashes.Class209 @class = new Stashes.Class209();
			@class.int_0 = int_1;
			Stashes.Items.Remove(@class.int_0);
			*(byte*)ptr = ((dictionary_3.Count == 2) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				KeyValuePair<string, object> keyValuePair = dictionary_3.ElementAt(1);
				Stashes.Items.Add(@class.int_0, ((JArray)keyValuePair.Value).ToObject<List<JsonItem>>());
			}
			else
			{
				KeyValuePair<string, object> keyValuePair2 = dictionary_3.ElementAt(1);
				KeyValuePair<string, object> keyValuePair = dictionary_3.ElementAt(2);
				((byte*)ptr)[1] = ((!Stashes.Layout.ContainsKey(keyValuePair2.Key)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((byte*)ptr)[2] = ((!(keyValuePair2.Value is bool)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Stashes.Layout.Add(keyValuePair2.Key, (JObject)keyValuePair2.Value);
					}
				}
				Stashes.Items.Add(@class.int_0, ((JArray)keyValuePair.Value).ToObject<List<JsonItem>>());
			}
			Stashes.Tabs.First(new Func<JsonTab, bool>(@class.method_0)).ItemsLoaded = true;
		}

		public unsafe static void smethod_4()
		{
			void* ptr = stackalloc byte[11];
			((byte*)ptr)[8] = (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.ForumThreadId)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				Class181.smethod_3(Enum11.const_2, Stashes.getString_0(107448924));
			}
			else
			{
				Dictionary<string, string> dictionary = Web.smethod_1(Class255.class105_0.method_3(ConfigOptions.ForumThreadId), Class255.Cookies);
				*(int*)ptr = 0;
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					Tuple<int, JsonItem> tuple = Stashes.smethod_5(keyValuePair.Key);
					((byte*)ptr)[9] = ((tuple != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						*(int*)((byte*)ptr + 4) = tuple.Item1;
						JsonItem item = tuple.Item2;
						item.note = keyValuePair.Value;
						Class181.smethod_3(Enum11.const_0, string.Format(Stashes.getString_0(107448891), new object[]
						{
							item.Name,
							item.x,
							item.y,
							keyValuePair.Value
						}));
						((byte*)ptr)[10] = ((!Stashes.ForumTheadItemIds.ContainsKey(*(int*)((byte*)ptr + 4))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							Stashes.ForumTheadItemIds.Add(*(int*)((byte*)ptr + 4), new List<string>());
						}
						Stashes.ForumTheadItemIds[*(int*)((byte*)ptr + 4)].Add(keyValuePair.Key);
						*(int*)ptr = *(int*)ptr + 1;
					}
				}
				Class181.smethod_3(Enum11.const_0, Stashes.getString_0(107481058) + ((int*)ptr)->ToString() + Stashes.getString_0(107481029));
			}
		}

		public static Tuple<int, JsonItem> smethod_5(string string_1)
		{
			Stashes.Class210 @class = new Stashes.Class210();
			@class.string_0 = string_1;
			foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in Stashes.Items)
			{
				IEnumerable<JsonItem> value = keyValuePair.Value;
				Func<JsonItem, bool> predicate;
				if ((predicate = @class.func_0) == null)
				{
					predicate = (@class.func_0 = new Func<JsonItem, bool>(@class.method_0));
				}
				JsonItem jsonItem = value.FirstOrDefault(predicate);
				if (jsonItem != null)
				{
					return Tuple.Create<int, JsonItem>(keyValuePair.Key, jsonItem);
				}
			}
			return null;
		}

		public static List<JsonTab> smethod_6(string string_1)
		{
			Stashes.Class211 @class = new Stashes.Class211();
			@class.string_0 = string_1;
			List<JsonTab> result;
			if (Stashes.Tabs == null)
			{
				result = new List<JsonTab>();
			}
			else
			{
				result = Stashes.Tabs.Where(new Func<JsonTab, bool>(@class.method_0)).ToList<JsonTab>();
			}
			return result;
		}

		public unsafe static JsonTab smethod_7(string string_1, bool bool_1 = false)
		{
			void* ptr = stackalloc byte[3];
			Stashes.Class212 @class = new Stashes.Class212();
			@class.string_0 = string_1;
			*(byte*)ptr = (bool_1 ? 1 : 0);
			JsonTab jsonTab;
			if (*(sbyte*)ptr != 0)
			{
				jsonTab = Stashes.Tabs.Where(new Func<JsonTab, bool>(Stashes.<>c.<>9.method_5)).FirstOrDefault(new Func<JsonTab, bool>(@class.method_0));
			}
			else
			{
				jsonTab = Stashes.Tabs.FirstOrDefault(new Func<JsonTab, bool>(@class.method_1));
			}
			((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
			JsonTab result;
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				((byte*)ptr)[2] = (bool_1 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					result = Stashes.Tabs.Where(new Func<JsonTab, bool>(Stashes.<>c.<>9.method_6)).First<JsonTab>();
				}
				else
				{
					result = Stashes.Tabs.First<JsonTab>();
				}
			}
			else
			{
				result = jsonTab;
			}
			return result;
		}

		public static List<string> smethod_8()
		{
			List<string> result;
			if (Stashes.Tabs == null)
			{
				result = new List<string>();
			}
			else
			{
				result = Stashes.Tabs.Select(new Func<JsonTab, string>(Stashes.<>c.<>9.method_7)).ToList<string>();
			}
			return result;
		}

		public static List<JsonTab> smethod_9(string string_1)
		{
			Stashes.Class213 @class = new Stashes.Class213();
			@class.string_0 = string_1;
			List<JsonTab> result;
			if (Stashes.Tabs == null)
			{
				result = new List<JsonTab>();
			}
			else
			{
				result = Stashes.Tabs.Where(new Func<JsonTab, bool>(@class.method_0)).ToList<JsonTab>();
			}
			return result;
		}

		public static List<int> smethod_10(string string_1)
		{
			Stashes.Class214 @class = new Stashes.Class214();
			@class.string_0 = string_1;
			List<int> result;
			if (Stashes.Tabs == null)
			{
				result = new List<int>();
			}
			else
			{
				result = Stashes.Tabs.Where(new Func<JsonTab, bool>(@class.method_0)).Select(new Func<JsonTab, int>(Stashes.<>c.<>9.method_8)).ToList<int>();
			}
			return result;
		}

		public static JsonTab smethod_11(int int_1)
		{
			Stashes.Class215 @class = new Stashes.Class215();
			@class.int_0 = int_1;
			JsonTab result;
			if (Stashes.Tabs == null)
			{
				result = null;
			}
			else
			{
				result = Stashes.Tabs.FirstOrDefault(new Func<JsonTab, bool>(@class.method_0));
			}
			return result;
		}

		public unsafe static bool smethod_12(int int_1)
		{
			void* ptr = stackalloc byte[2];
			JsonTab jsonTab = Stashes.smethod_11(int_1);
			*(byte*)ptr = ((jsonTab == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (jsonTab.IsSupported ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static JsonTab smethod_13()
		{
			JsonTab jsonTab = Stashes.Tabs.FirstOrDefault(new Func<JsonTab, bool>(Stashes.<>c.<>9.method_9));
			JsonTab result;
			if (jsonTab != null && Class255.class105_0.method_4(ConfigOptions.FlippingEnabled))
			{
				result = jsonTab;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public unsafe static JsonTab smethod_14(string string_1)
		{
			void* ptr = stackalloc byte[5];
			((byte*)ptr)[4] = (Class255.class105_0.method_4(ConfigOptions.UseDumpTabs) ? 1 : 0);
			JsonTab result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				if (string_1 != null)
				{
					*(int*)ptr = (int)Class396.smethod_0(string_1);
					if (*(uint*)ptr <= 1699492079U)
					{
						if (*(uint*)ptr <= 754411619U)
						{
							if (*(uint*)ptr <= 662684201U)
							{
								if (*(uint*)ptr != 342827606U)
								{
									if (*(uint*)ptr != 436641912U)
									{
										if (*(uint*)ptr != 662684201U)
										{
											goto IL_600;
										}
										if (string_1 == Stashes.getString_0(107361572))
										{
											return Stashes.mainForm_0.class256_0.class257_0.jsonTab_3 ?? Stashes.smethod_15(string_1);
										}
										goto IL_600;
									}
									else
									{
										if (!(string_1 == Stashes.getString_0(107362043)))
										{
											goto IL_600;
										}
										goto IL_5A7;
									}
								}
								else
								{
									if (!(string_1 == Stashes.getString_0(107394576)))
									{
										goto IL_600;
									}
									goto IL_354;
								}
							}
							else if (*(uint*)ptr != 668369568U)
							{
								if (*(uint*)ptr != 707214052U)
								{
									if (*(uint*)ptr != 754411619U)
									{
										goto IL_600;
									}
									if (string_1 == Stashes.getString_0(107361979))
									{
										return Stashes.mainForm_0.class256_0.jsonTab_3 ?? Stashes.smethod_15(string_1);
									}
									goto IL_600;
								}
								else
								{
									if (string_1 == Stashes.getString_0(107362020))
									{
										return Stashes.mainForm_0.class256_0.jsonTab_6 ?? Stashes.smethod_15(string_1);
									}
									goto IL_600;
								}
							}
							else if (!(string_1 == Stashes.getString_0(107453674)))
							{
								goto IL_600;
							}
						}
						else if (*(uint*)ptr <= 912196998U)
						{
							if (*(uint*)ptr != 768477233U)
							{
								if (*(uint*)ptr != 840341850U)
								{
									if (*(uint*)ptr != 912196998U)
									{
										goto IL_600;
									}
									if (!(string_1 == Stashes.getString_0(107401380)))
									{
										goto IL_600;
									}
								}
								else
								{
									if (string_1 == Stashes.getString_0(107361538))
									{
										return Stashes.mainForm_0.class256_0.class257_0.jsonTab_4 ?? Stashes.smethod_15(string_1);
									}
									goto IL_600;
								}
							}
							else
							{
								if (!(string_1 == Stashes.getString_0(107371833)))
								{
									goto IL_600;
								}
								goto IL_5A7;
							}
						}
						else if (*(uint*)ptr != 974961241U)
						{
							if (*(uint*)ptr != 1326941322U)
							{
								if (*(uint*)ptr != 1699492079U)
								{
									goto IL_600;
								}
								if (!(string_1 == Stashes.getString_0(107481015)))
								{
									goto IL_600;
								}
								goto IL_5A7;
							}
							else
							{
								if (!(string_1 == Stashes.getString_0(107361679)))
								{
									goto IL_600;
								}
								goto IL_4F1;
							}
						}
						else
						{
							if (!(string_1 == Stashes.getString_0(107361617)))
							{
								goto IL_600;
							}
							goto IL_4F1;
						}
						return Stashes.mainForm_0.class256_0.jsonTab_1 ?? Stashes.smethod_15(string_1);
					}
					if (*(uint*)ptr > 2899014759U)
					{
						if (*(uint*)ptr <= 3644247934U)
						{
							if (*(uint*)ptr != 2975789710U)
							{
								if (*(uint*)ptr != 3442707768U)
								{
									if (*(uint*)ptr != 3644247934U)
									{
										goto IL_600;
									}
									if (string_1 == Stashes.getString_0(107361989))
									{
										return Stashes.mainForm_0.class256_0.class257_0.jsonTab_0 ?? Stashes.smethod_15(string_1);
									}
									goto IL_600;
								}
								else
								{
									if (string_1 == Stashes.getString_0(107480936))
									{
										goto IL_4F1;
									}
									goto IL_600;
								}
							}
							else if (!(string_1 == Stashes.getString_0(107450948)))
							{
								goto IL_600;
							}
						}
						else if (*(uint*)ptr != 3645813303U)
						{
							if (*(uint*)ptr != 3744739325U)
							{
								if (*(uint*)ptr != 4107304114U)
								{
									goto IL_600;
								}
								if (string_1 == Stashes.getString_0(107480996))
								{
									return Stashes.mainForm_0.class256_0.class257_0.jsonTab_6 ?? Stashes.smethod_15(string_1);
								}
								goto IL_600;
							}
							else
							{
								if (string_1 == Stashes.getString_0(107480974))
								{
									goto IL_5A7;
								}
								goto IL_600;
							}
						}
						else if (!(string_1 == Stashes.getString_0(107362034)))
						{
							goto IL_600;
						}
						return Stashes.mainForm_0.class256_0.class257_0.jsonTab_1 ?? Stashes.smethod_15(string_1);
					}
					if (*(uint*)ptr <= 1997917396U)
					{
						if (*(uint*)ptr != 1701187425U)
						{
							if (*(uint*)ptr != 1906751595U)
							{
								if (*(uint*)ptr != 1997917396U)
								{
									goto IL_600;
								}
								if (!(string_1 == Stashes.getString_0(107361666)))
								{
									goto IL_600;
								}
								goto IL_4F1;
							}
							else if (!(string_1 == Stashes.getString_0(107352712)))
							{
								goto IL_600;
							}
						}
						else
						{
							if (string_1 == Stashes.getString_0(107480981))
							{
								return Stashes.mainForm_0.class256_0.class257_0.jsonTab_7 ?? Stashes.smethod_15(string_1);
							}
							goto IL_600;
						}
					}
					else if (*(uint*)ptr != 2056346661U)
					{
						if (*(uint*)ptr != 2176199594U)
						{
							if (*(uint*)ptr != 2899014759U)
							{
								goto IL_600;
							}
							if (string_1 == Stashes.getString_0(107361587))
							{
								return Stashes.mainForm_0.class256_0.class257_0.jsonTab_5 ?? Stashes.smethod_15(string_1);
							}
							goto IL_600;
						}
						else
						{
							if (string_1 == Stashes.getString_0(107361970))
							{
								return Stashes.mainForm_0.class256_0.jsonTab_4 ?? Stashes.smethod_15(string_1);
							}
							goto IL_600;
						}
					}
					else
					{
						if (!(string_1 == Stashes.getString_0(107361664)))
						{
							goto IL_600;
						}
						goto IL_4F1;
					}
					IL_354:
					return Stashes.mainForm_0.class256_0.jsonTab_0 ?? Stashes.smethod_15(string_1);
					IL_4F1:
					return Stashes.mainForm_0.class256_0.jsonTab_2 ?? Stashes.smethod_15(string_1);
					IL_5A7:
					return Stashes.mainForm_0.class256_0.class257_0.jsonTab_2 ?? Stashes.smethod_15(string_1);
				}
				IL_600:
				result = (Stashes.mainForm_0.class256_0.jsonTab_5 ?? Stashes.smethod_15(string_1));
			}
			else
			{
				result = Stashes.smethod_15(string_1);
			}
			return result;
		}

		private static JsonTab smethod_15(string string_1)
		{
			if (string_1 != null)
			{
				uint num = Class396.smethod_0(string_1);
				if (num <= 1699492079U)
				{
					if (num <= 754411619U)
					{
						if (num <= 662684201U)
						{
							if (num != 342827606U)
							{
								if (num != 436641912U)
								{
									if (num != 662684201U)
									{
										goto IL_510;
									}
									if (!(string_1 == Stashes.getString_0(107361572)))
									{
										goto IL_510;
									}
									goto IL_4F9;
								}
								else
								{
									if (!(string_1 == Stashes.getString_0(107362043)))
									{
										goto IL_510;
									}
									goto IL_46F;
								}
							}
							else
							{
								if (!(string_1 == Stashes.getString_0(107394576)))
								{
									goto IL_510;
								}
								goto IL_36F;
							}
						}
						else if (num != 668369568U)
						{
							if (num != 707214052U)
							{
								if (num != 754411619U)
								{
									goto IL_510;
								}
								if (string_1 == Stashes.getString_0(107361979))
								{
									return Stashes.smethod_16(Stashes.getString_0(107380430));
								}
								goto IL_510;
							}
							else
							{
								if (string_1 == Stashes.getString_0(107362020))
								{
									return Stashes.smethod_16(Stashes.getString_0(107480905));
								}
								goto IL_510;
							}
						}
						else if (!(string_1 == Stashes.getString_0(107453674)))
						{
							goto IL_510;
						}
					}
					else if (num <= 912196998U)
					{
						if (num != 768477233U)
						{
							if (num != 840341850U)
							{
								if (num != 912196998U)
								{
									goto IL_510;
								}
								if (!(string_1 == Stashes.getString_0(107401380)))
								{
									goto IL_510;
								}
							}
							else
							{
								if (!(string_1 == Stashes.getString_0(107361538)))
								{
									goto IL_510;
								}
								goto IL_4F9;
							}
						}
						else
						{
							if (!(string_1 == Stashes.getString_0(107371833)))
							{
								goto IL_510;
							}
							goto IL_46F;
						}
					}
					else if (num != 974961241U)
					{
						if (num != 1326941322U)
						{
							if (num != 1699492079U)
							{
								goto IL_510;
							}
							if (!(string_1 == Stashes.getString_0(107481015)))
							{
								goto IL_510;
							}
							goto IL_46F;
						}
						else
						{
							if (!(string_1 == Stashes.getString_0(107361679)))
							{
								goto IL_510;
							}
							goto IL_33B;
						}
					}
					else
					{
						if (!(string_1 == Stashes.getString_0(107361617)))
						{
							goto IL_510;
						}
						goto IL_33B;
					}
					return Stashes.smethod_16(Stashes.getString_0(107480950));
				}
				if (num > 2056346661U)
				{
					if (num <= 3644247934U)
					{
						if (num != 2176199594U)
						{
							if (num != 2975789710U)
							{
								if (num != 3644247934U)
								{
									goto IL_510;
								}
								if (string_1 == Stashes.getString_0(107361989))
								{
									return Stashes.smethod_16(Stashes.getString_0(107380445));
								}
								goto IL_510;
							}
							else if (!(string_1 == Stashes.getString_0(107450948)))
							{
								goto IL_510;
							}
						}
						else
						{
							if (string_1 == Stashes.getString_0(107361970))
							{
								return Stashes.smethod_16(Stashes.getString_0(107380479));
							}
							goto IL_510;
						}
					}
					else if (num <= 3744739325U)
					{
						if (num != 3645813303U)
						{
							if (num != 3744739325U)
							{
								goto IL_510;
							}
							if (string_1 == Stashes.getString_0(107480974))
							{
								goto IL_46F;
							}
							goto IL_510;
						}
						else if (!(string_1 == Stashes.getString_0(107362034)))
						{
							goto IL_510;
						}
					}
					else if (num != 4107304114U)
					{
						if (num != 4212465135U)
						{
							goto IL_510;
						}
						if (!(string_1 == Stashes.getString_0(107361561)))
						{
							goto IL_510;
						}
						goto IL_4F9;
					}
					else
					{
						if (string_1 == Stashes.getString_0(107480996))
						{
							goto IL_4F9;
						}
						goto IL_510;
					}
					return Stashes.smethod_16(Stashes.getString_0(107380392));
				}
				if (num <= 1849229205U)
				{
					if (num != 1701187425U)
					{
						if (num != 1809394226U)
						{
							if (num != 1849229205U)
							{
								goto IL_510;
							}
							if (!(string_1 == Stashes.getString_0(107480959)))
							{
								goto IL_510;
							}
							goto IL_4F9;
						}
						else if (!(string_1 == Stashes.getString_0(107361925)))
						{
							goto IL_510;
						}
					}
					else
					{
						if (!(string_1 == Stashes.getString_0(107480981)))
						{
							goto IL_510;
						}
						goto IL_4F9;
					}
				}
				else if (num != 1906751595U)
				{
					if (num != 1997917396U)
					{
						if (num != 2056346661U)
						{
							goto IL_510;
						}
						if (!(string_1 == Stashes.getString_0(107361664)))
						{
							goto IL_510;
						}
					}
					else if (!(string_1 == Stashes.getString_0(107361666)))
					{
						goto IL_510;
					}
				}
				else
				{
					if (string_1 == Stashes.getString_0(107352712))
					{
						goto IL_36F;
					}
					goto IL_510;
				}
				IL_33B:
				return Stashes.smethod_16(Stashes.getString_0(107382355));
				IL_36F:
				return Stashes.smethod_16(Stashes.getString_0(107394430));
				IL_46F:
				return Stashes.smethod_16(Stashes.getString_0(107380403));
				IL_4F9:
				return Stashes.smethod_16(Stashes.getString_0(107382368));
			}
			IL_510:
			return Stashes.smethod_16(Stashes.getString_0(107382368));
		}

		private unsafe static JsonTab smethod_16(string string_1)
		{
			void* ptr = stackalloc byte[5];
			Stashes.Class216 @class = new Stashes.Class216();
			@class.string_0 = string_1;
			*(byte*)ptr = (string.IsNullOrEmpty(@class.string_0) ? 1 : 0);
			JsonTab result;
			if (*(sbyte*)ptr != 0)
			{
				result = null;
			}
			else
			{
				JsonTab jsonTab = Stashes.Tabs.Where(new Func<JsonTab, bool>(@class.method_0)).FirstOrDefault<JsonTab>();
				((byte*)ptr)[1] = ((jsonTab != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = jsonTab;
				}
				else
				{
					jsonTab = Stashes.Tabs.Where(new Func<JsonTab, bool>(Stashes.<>c.<>9.method_10)).FirstOrDefault<JsonTab>();
					((byte*)ptr)[2] = ((jsonTab != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						result = jsonTab;
					}
					else
					{
						jsonTab = Stashes.Tabs.Where(new Func<JsonTab, bool>(Stashes.<>c.<>9.method_11)).FirstOrDefault<JsonTab>();
						((byte*)ptr)[3] = ((jsonTab != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							result = jsonTab;
						}
						else
						{
							jsonTab = Stashes.Tabs.Where(new Func<JsonTab, bool>(Stashes.<>c.<>9.method_12)).FirstOrDefault<JsonTab>();
							((byte*)ptr)[4] = ((jsonTab != null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								result = jsonTab;
							}
							else
							{
								result = null;
							}
						}
					}
				}
			}
			return result;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Stashes()
		{
			Strings.CreateGetStringDelegate(typeof(Stashes));
			Stashes.Layout = new Dictionary<string, JObject>();
			Stashes.Items = new Dictionary<int, List<JsonItem>>();
			Stashes.ForumTheadItemIds = new Dictionary<int, List<string>>();
			Stashes.bool_0 = false;
		}

		private static MainForm mainForm_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static List<JsonTab> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Dictionary<string, JObject> dictionary_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Dictionary<int, List<JsonItem>> dictionary_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Dictionary<int, List<string>> dictionary_2;

		public static int int_0;

		public static string[] string_0;

		public static bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Enum9 enum9_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class203
		{
			internal string method_0()
			{
				return this.string_0 = Web.smethod_2(this.string_1, Encoding.UTF8, Class255.Cookies);
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class205
		{
			[DebuggerStepThrough]
			internal void method_0()
			{
				Stashes.Class205.Class206 @class = new Stashes.Class205.Class206();
				@class.class205_0 = this;
				@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
				asyncVoidMethodBuilder_.Start<Stashes.Class205.Class206>(ref @class);
			}

			internal void method_1()
			{
				Stashes.mainForm_0.toolStripProgressBar_0.Maximum = Class255.CurrentStashProfile.Count + 1;
				Stashes.mainForm_0.toolStripProgressBar_0.Value = this.int_0;
			}

			public int int_0;

			public Action action_0;

			private sealed class Class206 : IAsyncStateMachine
			{
				unsafe void IAsyncStateMachine.MoveNext()
				{
					void* ptr = stackalloc byte[13];
					int num = this.int_0;
					try
					{
						if (num <= 1)
						{
						}
						try
						{
							ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
							TaskAwaiter<bool> awaiter2;
							if (num != 0)
							{
								if (num == 1)
								{
									awaiter = this.configuredTaskAwaiter_0;
									this.configuredTaskAwaiter_0 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
									this.int_0 = -1;
									goto IL_2B9;
								}
								if (Stashes.LoadState == Enum9.const_1 && this.class205_0.int_0 == 0)
								{
									goto IL_582;
								}
								Stashes.mainForm_0.thread_2 = Thread.CurrentThread;
								Stashes.LoadState = Enum9.const_1;
								((byte*)ptr)[4] = ((Stashes.Tabs == null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 4) == 0)
								{
									goto IL_111;
								}
								awaiter2 = Stashes.smethod_1().GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.int_0 = 0;
									this.taskAwaiter_0 = awaiter2;
									Stashes.Class205.Class206 @class = this;
									this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, Stashes.Class205.Class206>(ref awaiter2, ref @class);
									return;
								}
							}
							else
							{
								awaiter2 = this.taskAwaiter_0;
								this.taskAwaiter_0 = default(TaskAwaiter<bool>);
								this.int_0 = -1;
							}
							this.bool_0 = awaiter2.GetResult();
							((byte*)ptr)[5] = ((!this.bool_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								Stashes.mainForm_0.method_64(true);
								goto IL_582;
							}
							IL_111:
							Control mainForm_ = Stashes.mainForm_0;
							Action method;
							if ((method = this.class205_0.action_0) == null)
							{
								method = (this.class205_0.action_0 = new Action(this.class205_0.method_1));
							}
							mainForm_.Invoke(method);
							this.int_1 = 0;
							this.int_2 = 0;
							((byte*)ptr)[6] = ((this.class205_0.int_0 != 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 6) != 0)
							{
								Class181.smethod_3(Enum11.const_0, Stashes.Class205.Class206.getString_0(107244027));
							}
							else
							{
								Class181.smethod_3(Enum11.const_0, Stashes.Class205.Class206.getString_0(107243950));
							}
							this.int_2 = this.class205_0.int_0;
							goto IL_3A8;
							IL_2B9:
							awaiter.GetResult();
							Stashes.mainForm_0.thread_2 = Thread.CurrentThread;
							this.dictionary_0 = JsonConvert.DeserializeObject<Dictionary<string, object>>(this.class207_0.string_0);
							((byte*)ptr)[9] = (this.dictionary_0.ContainsKey(Stashes.Class205.Class206.getString_0(107396098)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								Class181.smethod_3(Enum11.const_0, string.Format(Stashes.Class205.Class206.getString_0(107243900), Stashes.Tabs[this.int_2].n));
								this.int_1 = this.int_2;
								Thread.Sleep(60000);
								goto IL_469;
							}
							Stashes.mainForm_0.Invoke(new Action(Stashes.<>c.<>9.method_2));
							Stashes.smethod_3(Stashes.Tabs[this.int_2].i, this.dictionary_0);
							Class181.smethod_3(Enum11.const_0, string.Format(Stashes.Class205.Class206.getString_0(107244307), Stashes.Tabs[this.int_2].n));
							this.class207_0 = null;
							this.dictionary_0 = null;
							IL_396:
							*(int*)ptr = this.int_2;
							this.int_2 = *(int*)ptr + 1;
							IL_3A8:
							((byte*)ptr)[10] = ((this.int_2 < Stashes.Tabs.Count) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 10) != 0)
							{
								this.class207_0 = new Stashes.Class207();
								((byte*)ptr)[7] = ((!Class255.CurrentStashProfile.Contains(Stashes.Tabs[this.int_2].i)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 7) != 0)
								{
									goto IL_396;
								}
								((byte*)ptr)[8] = (Stashes.mainForm_0.bool_12 ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 8) != 0)
								{
									Stashes.mainForm_0.Invoke(new Action(Stashes.<>c.<>9.method_1));
									goto IL_582;
								}
								this.class207_0.string_0 = Stashes.Class205.Class206.getString_0(107397554);
								this.class207_0.string_1 = string.Format(Stashes.Class205.Class206.getString_0(107243941), new object[]
								{
									Class103.string_8,
									Class255.class105_0.method_3(ConfigOptions.AccountName),
									Class255.class105_0.method_3(ConfigOptions.League),
									Stashes.Tabs[this.int_2].i
								});
								awaiter = Task.Run<string>(new Func<string>(this.class207_0.method_0)).ConfigureAwait(false).GetAwaiter();
								if (awaiter.IsCompleted)
								{
									goto IL_2B9;
								}
								this.int_0 = 1;
								this.configuredTaskAwaiter_0 = awaiter;
								Stashes.Class205.Class206 @class = this;
								this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, Stashes.Class205.Class206>(ref awaiter, ref @class);
								return;
							}
							IL_469:
							((byte*)ptr)[11] = ((this.int_1 != 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 11) != 0)
							{
								Stashes.smethod_2(this.int_1);
							}
							((byte*)ptr)[12] = ((this.int_2 == Stashes.Tabs.Count) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								Class181.smethod_3(Enum11.const_0, Stashes.Class205.Class206.getString_0(107244270));
								Stashes.mainForm_0.Invoke(new Action(Stashes.<>c.<>9.method_3));
								Stashes.mainForm_0.Invoke(new Action(Stashes.<>c.<>9.method_4));
								Stashes.LoadState = Enum9.const_2;
								Stashes.bool_0 = true;
								Stashes.mainForm_0.thread_2 = null;
							}
						}
						catch (ThreadAbortException)
						{
						}
						catch (Exception exception)
						{
							this.exception_0 = exception;
							Stashes.LoadState = Enum9.const_0;
							Class181.smethod_3(Enum11.const_2, string.Format(Stashes.Class205.Class206.getString_0(107244249), this.exception_0.Message));
						}
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					IL_582:
					this.int_0 = -2;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				static Class206()
				{
					Strings.CreateGetStringDelegate(typeof(Stashes.Class205.Class206));
				}

				public int int_0;

				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				public Stashes.Class205 class205_0;

				private int int_1;

				private int int_2;

				private bool bool_0;

				private Stashes.Class207 class207_0;

				private Dictionary<string, object> dictionary_0;

				private Exception exception_0;

				private TaskAwaiter<bool> taskAwaiter_0;

				private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter_0;

				[NonSerialized]
				internal static GetString getString_0;
			}
		}

		[CompilerGenerated]
		private sealed class Class207
		{
			internal string method_0()
			{
				return this.string_0 = Web.smethod_2(this.string_1, Encoding.UTF8, Class255.Cookies);
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class209
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.i == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class210
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.id == this.string_0;
			}

			public string string_0;

			public Func<JsonItem, bool> func_0;
		}

		[CompilerGenerated]
		private sealed class Class211
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.n.Replace(Stashes.Class211.getString_0(107248650), Stashes.Class211.getString_0(107397603)) == this.string_0;
			}

			static Class211()
			{
				Strings.CreateGetStringDelegate(typeof(Stashes.Class211));
			}

			public string string_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class212
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.n == this.string_0;
			}

			internal bool method_1(JsonTab jsonTab_0)
			{
				return jsonTab_0.n == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class213
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.type == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class214
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.n == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class215
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.i == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class216
		{
			internal bool method_0(JsonTab jsonTab_0)
			{
				return jsonTab_0.type == this.string_0;
			}

			public string string_0;
		}
	}
}
