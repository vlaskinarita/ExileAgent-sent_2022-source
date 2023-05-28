using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ns0;
using ns12;
using ns14;
using ns15;
using ns24;
using ns29;
using ns31;
using ns35;
using ns6;
using PoEv2.Classes;
using PoEv2.Handlers;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class Pricer
	{
		[DebuggerStepThrough]
		public static Task smethod_0(JsonTab jsonTab_0, MainForm mainForm_0)
		{
			Pricer.Class377 @class = new Pricer.Class377();
			@class.jsonTab_0 = jsonTab_0;
			@class.mainForm_0 = mainForm_0;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<Pricer.Class377>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		private unsafe static bool smethod_1(string string_1)
		{
			void* ptr = stackalloc byte[3];
			foreach (string key in Class255.BulkTypesList)
			{
				*(byte*)ptr = ((!Class102.PricingTypes.ContainsKey(key)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
					goto IL_76;
				}
				((byte*)ptr)[2] = (Class102.PricingTypes[key].Contains(string_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[1] = 1;
					goto IL_76;
				}
			}
			((byte*)ptr)[1] = 0;
			IL_76:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static string string_0 = null;

		[CompilerGenerated]
		private sealed class Class374
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[44];
				Pricer.Class375 @class = new Pricer.Class375();
				@class.class374_0 = this;
				this.mainForm_0.thread_5 = Thread.CurrentThread;
				if (!Stashes.bool_0 || Stashes.Tabs == null)
				{
					this.mainForm_0.method_74();
					Class181.smethod_3(Enum11.const_2, Pricer.Class374.getString_0(107249968));
				}
				else
				{
					((byte*)ptr)[8] = ((Stashes.smethod_11(this.jsonTab_0.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						this.mainForm_0.method_74();
						Class181.smethod_2(Enum11.const_2, Pricer.Class374.getString_0(107249391), new object[]
						{
							this.jsonTab_0.n
						});
					}
					else
					{
						Class181.smethod_2(Enum11.const_0, Pricer.Class374.getString_0(107249326), new object[]
						{
							this.jsonTab_0.n
						});
						((byte*)ptr)[9] = ((!Class255.class105_0.method_4(ConfigOptions.ForumThreadEnabled)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							UI.smethod_1();
							UI.smethod_35(this.jsonTab_0.i, false, 1);
						}
						((byte*)ptr)[10] = ((!Class255.class105_0.method_4(ConfigOptions.RepriceItems)) ? 1 : 0);
						IEnumerable<JsonItem> enumerable;
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							enumerable = Stashes.Items[this.jsonTab_0.i].Where(new Func<JsonItem, bool>(Pricer.<>c.<>9.method_0));
						}
						else
						{
							enumerable = Stashes.Items[this.jsonTab_0.i];
						}
						enumerable = enumerable.Where(new Func<JsonItem, bool>(Pricer.<>c.<>9.method_1));
						((byte*)ptr)[11] = ((!enumerable.Any<JsonItem>()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							Class181.smethod_2(Enum11.const_2, Pricer.Class374.getString_0(107249273), new object[]
							{
								this.jsonTab_0.n
							});
							this.mainForm_0.method_74();
						}
						else
						{
							@class.string_0 = new string[]
							{
								Pricer.Class374.getString_0(107452764)
							};
							IEnumerable<JsonItem> enumerable2 = enumerable.Where(new Func<JsonItem, bool>(@class.method_0));
							@class.list_0 = enumerable.Except(enumerable2).ToList<JsonItem>();
							Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
							Dictionary<Tuple<string, string>, JsonItem> dictionary2 = new Dictionary<Tuple<string, string>, JsonItem>();
							foreach (JsonItem jsonItem in enumerable.Except(enumerable2))
							{
								string text = API.smethod_8(jsonItem).Text;
								((byte*)ptr)[12] = ((!dictionary2.ContainsKey(Tuple.Create<string, string>(text, jsonItem.UniqueIdentifiers))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 12) != 0)
								{
									dictionary2.Add(Tuple.Create<string, string>(text, jsonItem.UniqueIdentifiers), jsonItem);
								}
							}
							this.mainForm_0.Invoke(new Action(@class.method_1));
							using (Dictionary<Tuple<string, string>, JsonItem>.Enumerator enumerator2 = dictionary2.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Pricer.Class376 class2 = new Pricer.Class376();
									class2.keyValuePair_0 = enumerator2.Current;
									Class181.smethod_3(Enum11.const_3, Pricer.Class374.getString_0(107249639) + class2.keyValuePair_0.Value.Name);
									ApiItem apiItem = API.smethod_7(class2.keyValuePair_0.Value.Name);
									((byte*)ptr)[13] = (Pricer.smethod_1(apiItem.Type) ? 1 : 0);
									string text4;
									if (*(sbyte*)((byte*)ptr + 13) != 0)
									{
										string text2 = (class2.keyValuePair_0.Value.typeLine == Pricer.Class374.getString_0(107393946)) ? API.smethod_4(Pricer.Class374.getString_0(107385741)) : API.smethod_4(Pricer.Class374.getString_0(107393946));
										string text3;
										int num = StashManager.smethod_2(class2.keyValuePair_0.Value.BaseItemName, 0, class2.keyValuePair_0.Value.MapTier, class2.keyValuePair_0.Value.ItemRarity == ItemRarity.Unique, true, string.Empty, out text3, out *(bool*)((byte*)ptr + 14), true).Values.Sum(new Func<List<JsonItem>, int>(Pricer.<>c.<>9.method_2));
										int num2 = (num >= class2.keyValuePair_0.Value.BaseItemStackSize) ? class2.keyValuePair_0.Value.BaseItemStackSize : num;
										if (apiItem.Type == Pricer.Class374.getString_0(107363510) || apiItem.Type == Pricer.Class374.getString_0(107363448) || apiItem.Type == Pricer.Class374.getString_0(107363461))
										{
											num2 = Math.Min(num2, 15);
										}
										Dictionary<string, decimal> dictionary3 = Pricing.smethod_5(apiItem.Id, text2, num2, false);
										((byte*)ptr)[15] = (dictionary3.ContainsKey(Pricer.Class374.getString_0(107369368)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 15) != 0)
										{
											continue;
										}
										decimal value = dictionary3.First(new Func<KeyValuePair<string, decimal>, bool>(Pricer.<>c.<>9.method_4)).Value;
										((byte*)ptr)[16] = ((value > 1m) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 16) != 0)
										{
											text4 = string.Format(Pricer.Class374.getString_0(107249594), Math.Round(value));
										}
										else
										{
											*(int*)ptr = API.smethod_6(API.smethod_7(class2.keyValuePair_0.Value.Name).Name);
											*(int*)((byte*)ptr + 4) = Math.Min(num, *(int*)ptr);
											if (apiItem.Type == Pricer.Class374.getString_0(107363510) || apiItem.Type == Pricer.Class374.getString_0(107363448) || apiItem.Type == Pricer.Class374.getString_0(107363461))
											{
												*(int*)((byte*)ptr + 4) = Math.Min(num, 15);
											}
											((byte*)ptr)[17] = ((apiItem.Type == Pricer.Class374.getString_0(107396358)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 17) != 0)
											{
												string typeLine = class2.keyValuePair_0.Value.typeLine;
												string text5 = typeLine;
												if (text5 != null)
												{
													if (text5 == Pricer.Class374.getString_0(107393946))
													{
														*(int*)((byte*)ptr + 4) = 220;
														goto IL_683;
													}
													if (text5 == Pricer.Class374.getString_0(107363311))
													{
														*(int*)((byte*)ptr + 4) = 50000;
														goto IL_683;
													}
												}
												*(int*)((byte*)ptr + 4) = 100;
											}
											IL_683:
											Class248 class3 = Class248.smethod_0(value, *(int*)((byte*)ptr + 4));
											((byte*)ptr)[18] = ((class3.Denominator != 1) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 18) != 0)
											{
												((byte*)ptr)[19] = ((class3.Denominator > num) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 19) != 0)
												{
													text4 = Pricing.smethod_7(class2.keyValuePair_0.Value, Class255.class105_0.method_3(ConfigOptions.PricingCurrencyType));
												}
												else
												{
													text4 = string.Format(Pricer.Class374.getString_0(107443548), class3.Numerator, class3.Denominator, text2);
												}
											}
											else
											{
												((byte*)ptr)[20] = ((class2.keyValuePair_0.Value.stackSize > class2.keyValuePair_0.Value.BaseItemStackSize) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 20) != 0)
												{
													text4 = string.Format(Pricer.Class374.getString_0(107249569), class2.keyValuePair_0.Value.BaseItemStackSize, text2);
												}
												else
												{
													text4 = string.Format(Pricer.Class374.getString_0(107249576), text2);
												}
											}
										}
									}
									else if (class2.keyValuePair_0.Value.UsePoePrices && Class255.class105_0.method_4(ConfigOptions.UsePoePrices))
									{
										Class250 class4 = Web.smethod_10(class2.keyValuePair_0.Value);
										if (class4 != null && class4.Median != 0.0)
										{
											ApiItem apiItem2 = API.smethod_7(class4.Currency);
											string id = apiItem2.Id;
											double num3 = (apiItem2.Text == Pricer.Class374.getString_0(107385741)) ? class4.Median : Math.Ceiling(class4.Median);
											text4 = string.Format(Pricer.Class374.getString_0(107465987), num3, id);
										}
										else
										{
											text4 = null;
										}
									}
									else
									{
										text4 = Pricing.smethod_7(class2.keyValuePair_0.Value, Class255.class105_0.method_3(ConfigOptions.PricingCurrencyType));
									}
									((byte*)ptr)[21] = (class2.keyValuePair_0.Value.IsMap ? 1 : 0);
									IEnumerable<JsonItem> enumerable3;
									if (*(sbyte*)((byte*)ptr + 21) != 0)
									{
										((byte*)ptr)[22] = ((class2.keyValuePair_0.Value.ItemRarity == ItemRarity.Unique) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 22) != 0)
										{
											enumerable3 = enumerable.Where(new Func<JsonItem, bool>(class2.method_0));
										}
										else
										{
											enumerable3 = enumerable.Where(new Func<JsonItem, bool>(class2.method_1));
										}
									}
									else
									{
										((byte*)ptr)[23] = (class2.keyValuePair_0.Value.IsGem ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 23) != 0)
										{
											enumerable3 = enumerable.Where(new Func<JsonItem, bool>(class2.method_2));
										}
										else
										{
											((byte*)ptr)[24] = (class2.keyValuePair_0.Value.IsHeist ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 24) != 0)
											{
												enumerable3 = enumerable.Where(new Func<JsonItem, bool>(class2.method_3));
											}
											else
											{
												enumerable3 = enumerable.Where(new Func<JsonItem, bool>(class2.method_4));
											}
										}
									}
									((byte*)ptr)[25] = ((text4 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 25) == 0)
									{
										text4 = text4.Replace(Pricer.Class374.getString_0(107394923), Pricer.Class374.getString_0(107371353)).Replace(Class120.char_0, '.');
										if (Class255.class105_0.method_4(ConfigOptions.PriceWithDiscount) && !Pricer.smethod_1(apiItem.Type))
										{
											Class252 class5 = Class252.smethod_0(text4, false);
											string text6 = API.smethod_4(Class255.class105_0.method_3(ConfigOptions.DiscountCurrency));
											decimal num4 = 1m - Class255.class105_0.method_6(ConfigOptions.DiscountAmount) / 100m;
											((byte*)ptr)[26] = 0;
											((byte*)ptr)[27] = 0;
											Class181.smethod_2(Enum11.const_3, Pricer.Class374.getString_0(107249527), new object[]
											{
												num4,
												text6
											});
											((byte*)ptr)[28] = ((text6 != class5.CurrencyId) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 28) != 0)
											{
												((byte*)ptr)[29] = ((!dictionary.ContainsKey(class5.CurrencyId)) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 29) != 0)
												{
													Dictionary<string, decimal> dictionary4 = Pricing.smethod_5(class5.CurrencyId, text6, 1, false);
													((byte*)ptr)[30] = (dictionary4.ContainsKey(Pricer.Class374.getString_0(107364618)) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 30) != 0)
													{
														KeyValuePair<string, decimal> keyValuePair = dictionary4.First<KeyValuePair<string, decimal>>();
														((byte*)ptr)[31] = ((keyValuePair.Value > 1m) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 31) != 0)
														{
															dictionary.Add(class5.CurrencyId, dictionary4.First<KeyValuePair<string, decimal>>().Value);
															((byte*)ptr)[26] = 1;
															Class181.smethod_2(Enum11.const_3, Pricer.Class374.getString_0(107249474), new object[]
															{
																API.smethod_7(class5.CurrencyId).Text,
																Math.Round(dictionary[class5.CurrencyId], 2),
																API.smethod_7(text6).Text
															});
														}
														else
														{
															((byte*)ptr)[27] = 1;
														}
													}
												}
												else
												{
													((byte*)ptr)[26] = 1;
												}
												((byte*)ptr)[32] = (byte)(*(sbyte*)((byte*)ptr + 26));
												if (*(sbyte*)((byte*)ptr + 32) != 0)
												{
													decimal d = dictionary[class5.CurrencyId];
													class5.Amount *= d;
												}
											}
											else
											{
												((byte*)ptr)[26] = 1;
											}
											((byte*)ptr)[33] = (byte)(*(sbyte*)((byte*)ptr + 26));
											if (*(sbyte*)((byte*)ptr + 33) != 0)
											{
												Class181.smethod_2(Enum11.const_3, Pricer.Class374.getString_0(107249433), new object[]
												{
													class5.Amount,
													class5.Amount * num4
												});
												class5.Amount = ((API.smethod_7(text6).Text == Pricer.Class374.getString_0(107385741)) ? Math.Round(class5.Amount * num4, 1) : Math.Round(class5.Amount * num4));
												class5.CurrencyId = text6;
												((byte*)ptr)[34] = ((class5.Amount < 1m) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 34) != 0)
												{
													Class181.smethod_2(Enum11.const_0, Pricer.Class374.getString_0(107249424), new object[]
													{
														text6
													});
													class5.Amount = 1m;
												}
												text4 = class5.ToString();
											}
											else
											{
												((byte*)ptr)[35] = (byte)(*(sbyte*)((byte*)ptr + 27));
												if (*(sbyte*)((byte*)ptr + 35) != 0)
												{
													Class181.smethod_2(Enum11.const_2, Pricer.Class374.getString_0(107248819), new object[]
													{
														API.smethod_7(text6).Text,
														API.smethod_7(class5.CurrencyId).Text
													});
												}
												else
												{
													Class181.smethod_2(Enum11.const_2, Pricer.Class374.getString_0(107248653), new object[]
													{
														API.smethod_7(class5.CurrencyId).Text,
														API.smethod_7(text6).Text
													});
												}
											}
										}
										foreach (JsonItem jsonItem2 in enumerable3)
										{
											((byte*)ptr)[36] = ((!Class255.class105_0.method_4(ConfigOptions.ForumThreadEnabled)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 36) != 0)
											{
												((byte*)ptr)[37] = ((jsonItem2.note == null) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 37) != 0)
												{
													UI.smethod_34(this.jsonTab_0.type, jsonItem2.x, jsonItem2.y, Enum2.const_2, false);
													((byte*)ptr)[38] = ((!UI.smethod_37(text4)) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 38) != 0)
													{
														Class181.smethod_3(Enum11.const_2, Pricer.Class374.getString_0(107249035));
														return;
													}
													Class181.smethod_3(Enum11.const_1, string.Format(Pricer.Class374.getString_0(107248974), new object[]
													{
														jsonItem2.Name,
														jsonItem2.x,
														jsonItem2.y,
														text4.Replace(Pricer.Class374.getString_0(107369438), Pricer.Class374.getString_0(107399299))
													}));
												}
												else
												{
													((byte*)ptr)[39] = ((text4.Trim() == Pricer.Class374.getString_0(107251464)) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 39) != 0)
													{
														Class181.smethod_3(Enum11.const_0, string.Format(Pricer.Class374.getString_0(107248897), jsonItem2.Name, jsonItem2.x, jsonItem2.y));
													}
													else
													{
														((byte*)ptr)[40] = ((text4 != jsonItem2.note) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 40) != 0)
														{
															UI.smethod_34(this.jsonTab_0.type, jsonItem2.x, jsonItem2.y, Enum2.const_2, false);
															((byte*)ptr)[41] = ((!UI.smethod_38(text4)) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 41) != 0)
															{
																Class181.smethod_3(Enum11.const_2, Pricer.Class374.getString_0(107248324));
																return;
															}
															Class181.smethod_3(Enum11.const_1, string.Format(Pricer.Class374.getString_0(107248295), new object[]
															{
																jsonItem2.Name,
																jsonItem2.x,
																jsonItem2.y,
																jsonItem2.note.Replace(Pricer.Class374.getString_0(107369438), Pricer.Class374.getString_0(107399299)).Replace(Pricer.Class374.getString_0(107442898), Pricer.Class374.getString_0(107399299)),
																text4.Replace(Pricer.Class374.getString_0(107369438), Pricer.Class374.getString_0(107399299)).Replace(Pricer.Class374.getString_0(107442898), Pricer.Class374.getString_0(107399299))
															}));
														}
														else
														{
															Class181.smethod_3(Enum11.const_0, string.Format(Pricer.Class374.getString_0(107248198), jsonItem2.Name));
														}
													}
												}
											}
											jsonItem2.note = text4;
											Control control = this.mainForm_0;
											Action method;
											if ((method = this.action_0) == null)
											{
												method = (this.action_0 = new Action(this.method_1));
											}
											control.Invoke(method);
										}
									}
								}
							}
							((byte*)ptr)[42] = (Class255.class105_0.method_4(ConfigOptions.ForumThreadEnabled) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 42) != 0)
							{
								Pricer.string_0 = Pricing.smethod_10(@class.list_0);
								Class181.smethod_3(Enum11.const_3, Pricer.string_0);
								Class181.smethod_2(Enum11.const_3, Pricer.Class374.getString_0(107248133), new object[]
								{
									@class.list_0.Count<JsonItem>(),
									this.jsonTab_0.n
								});
							}
							((byte*)ptr)[43] = (enumerable2.Any<JsonItem>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 43) != 0)
							{
								Class181.smethod_3(Enum11.const_0, string.Format(Pricer.Class374.getString_0(107248624), string.Join(Pricer.Class374.getString_0(107399382), enumerable2.Select(new Func<JsonItem, string>(Pricer.<>c.<>9.method_5)))));
							}
							Win32.smethod_4(-2, -2, 50, 90, false);
						}
					}
				}
			}

			internal void method_1()
			{
				if (this.mainForm_0.toolStripProgressBar_0.Value + 2 < this.mainForm_0.toolStripProgressBar_0.Maximum)
				{
					this.mainForm_0.toolStripProgressBar_0.Value += 2;
					this.mainForm_0.toolStripProgressBar_0.Value--;
				}
				else
				{
					this.mainForm_0.toolStripProgressBar_0.Value = this.mainForm_0.toolStripProgressBar_0.Maximum;
				}
			}

			static Class374()
			{
				Strings.CreateGetStringDelegate(typeof(Pricer.Class374));
			}

			public MainForm mainForm_0;

			public JsonTab jsonTab_0;

			public Action action_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class375
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return this.string_0.Contains(API.smethod_8(jsonItem_0).Type) || jsonItem_0.note.smethod_9(Pricer.Class375.getString_0(107251468));
			}

			internal void method_1()
			{
				this.class374_0.mainForm_0.toolStripProgressBar_0.Maximum = this.list_0.Count + 1;
				this.class374_0.mainForm_0.toolStripProgressBar_0.Value = 0;
			}

			static Class375()
			{
				Strings.CreateGetStringDelegate(typeof(Pricer.Class375));
			}

			public string[] string_0;

			public List<JsonItem> list_0;

			public Pricer.Class374 class374_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class376
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return API.smethod_8(jsonItem_0).Text == API.smethod_15(this.keyValuePair_0.Value) && jsonItem_0.UniqueIdentifiers == this.keyValuePair_0.Value.UniqueIdentifiers;
			}

			internal bool method_1(JsonItem jsonItem_0)
			{
				return API.smethod_8(jsonItem_0).Text == API.smethod_15(this.keyValuePair_0.Value) && jsonItem_0.UniqueIdentifiers == this.keyValuePair_0.Value.UniqueIdentifiers;
			}

			internal bool method_2(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Value.Name && jsonItem_0.UniqueIdentifiers == this.keyValuePair_0.Value.UniqueIdentifiers;
			}

			internal bool method_3(JsonItem jsonItem_0)
			{
				return API.smethod_15(jsonItem_0) == API.smethod_15(this.keyValuePair_0.Value) && jsonItem_0.UniqueIdentifiers == this.keyValuePair_0.Value.UniqueIdentifiers;
			}

			internal bool method_4(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.keyValuePair_0.Value.Name && jsonItem_0.ItemRarity == this.keyValuePair_0.Value.ItemRarity && jsonItem_0.UniqueIdentifiers == this.keyValuePair_0.Value.UniqueIdentifiers;
			}

			public KeyValuePair<Tuple<string, string>, JsonItem> keyValuePair_0;
		}
	}
}
