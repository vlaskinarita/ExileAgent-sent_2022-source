using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ns0;
using ns14;
using ns29;
using ns31;
using ns35;
using PoEv2.Classes;
using PoEv2.Handlers;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Api;
using PoEv2.Models.Flipping;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class Flipping
	{
		public unsafe static Class248 smethod_0(FlippingListItem flippingListItem_0)
		{
			void* ptr = stackalloc byte[2];
			ApiItem apiItem = API.smethod_7(flippingListItem_0.HaveName);
			ApiItem apiItem2 = API.smethod_7(flippingListItem_0.WantName);
			*(byte*)ptr = ((!flippingListItem_0.AutoPrice) ? 1 : 0);
			Class248 result;
			if (*(sbyte*)ptr != 0)
			{
				result = new Class248(flippingListItem_0.BuyHaveAmount, flippingListItem_0.BuyWantAmount);
			}
			else
			{
				KeyValuePair<string, decimal> keyValuePair = Pricing.smethod_6(apiItem.Id, apiItem2.Id, flippingListItem_0.HaveMinimumStock, flippingListItem_0.BuyListingsToSkip, flippingListItem_0.BuyListingsToTake).First<KeyValuePair<string, decimal>>();
				((byte*)ptr)[1] = ((keyValuePair.Key == Flipping.getString_0(107369334)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107270138), new object[]
					{
						flippingListItem_0.HaveName,
						flippingListItem_0.WantName
					});
					result = null;
				}
				else
				{
					Class248 @class = Class248.smethod_0(keyValuePair.Value, Math.Max(flippingListItem_0.MaxHavePerTrade, flippingListItem_0.MaxWantPerTrade));
					result = new Class248(@class.Denominator, @class.Numerator);
				}
			}
			return result;
		}

		public unsafe static Class248 smethod_1(FlippingListItem flippingListItem_0)
		{
			void* ptr = stackalloc byte[2];
			ApiItem apiItem = API.smethod_7(flippingListItem_0.HaveName);
			ApiItem apiItem2 = API.smethod_7(flippingListItem_0.WantName);
			*(byte*)ptr = ((!flippingListItem_0.AutoPrice) ? 1 : 0);
			Class248 result;
			if (*(sbyte*)ptr != 0)
			{
				result = new Class248(flippingListItem_0.SellHaveAmount, flippingListItem_0.SellWantAmount);
			}
			else
			{
				KeyValuePair<string, decimal> keyValuePair = Pricing.smethod_6(apiItem2.Id, apiItem.Id, flippingListItem_0.WantMinimumStock, flippingListItem_0.SellListingsToSkip, flippingListItem_0.SellListingsToTake).First<KeyValuePair<string, decimal>>();
				((byte*)ptr)[1] = ((keyValuePair.Key == Flipping.getString_0(107369334)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107270565), new object[]
					{
						flippingListItem_0.HaveName,
						flippingListItem_0.WantName
					});
					result = null;
				}
				else
				{
					result = Class248.smethod_0(keyValuePair.Value, Math.Max(flippingListItem_0.MaxHavePerTrade, flippingListItem_0.MaxWantPerTrade));
				}
			}
			return result;
		}

		public static Class248 smethod_2(Class248 class248_0, int int_0, double double_0)
		{
			return Class248.smethod_0(class248_0.Value * (1m + (decimal)double_0 / 100m), int_0);
		}

		public unsafe static int smethod_3(Class248 class248_0, FlippingListItem flippingListItem_0)
		{
			void* ptr = stackalloc byte[17];
			((byte*)ptr)[16] = ((!flippingListItem_0.AutoPrice) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				*(int*)((byte*)ptr + 12) = 1;
			}
			else
			{
				API.smethod_7(flippingListItem_0.HaveName);
				API.smethod_7(flippingListItem_0.WantName);
				*(int*)ptr = 1;
				*(int*)((byte*)ptr + 4) = (int)Math.Floor((double)flippingListItem_0.MaxHavePerTrade / (double)class248_0.Numerator);
				*(int*)((byte*)ptr + 8) = (int)Math.Floor((double)flippingListItem_0.MaxWantPerTrade / (double)class248_0.Denominator);
				*(int*)ptr = Math.Max(Math.Min(*(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 8)), 1);
				*(int*)((byte*)ptr + 12) = *(int*)ptr;
			}
			return *(int*)((byte*)ptr + 12);
		}

		public unsafe static Class248 smethod_4(Class248 class248_0, FlippingListItem flippingListItem_0)
		{
			void* ptr = stackalloc byte[18];
			((byte*)ptr)[16] = ((class248_0.Numerator < flippingListItem_0.MinHavePerTrade) ? 1 : 0);
			Class248 result;
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				*(double*)ptr = (double)flippingListItem_0.MinHavePerTrade / (double)class248_0.Numerator;
				result = new Class248(Util.smethod_22((double)class248_0.Numerator * *(double*)ptr), Util.smethod_22((double)class248_0.Denominator * *(double*)ptr));
			}
			else
			{
				((byte*)ptr)[17] = ((class248_0.Denominator < flippingListItem_0.MinWantPerTrade) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					*(double*)((byte*)ptr + 8) = (double)flippingListItem_0.MinWantPerTrade / (double)class248_0.Denominator;
					result = new Class248(Util.smethod_22((double)class248_0.Numerator * *(double*)((byte*)ptr + 8)), Util.smethod_22((double)class248_0.Denominator * *(double*)((byte*)ptr + 8)));
				}
				else
				{
					result = class248_0;
				}
			}
			return result;
		}

		public unsafe static Class248 smethod_5(Class248 class248_0, FlippingListItem flippingListItem_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((class248_0.Value < 1m) ? 1 : 0);
			decimal val;
			if (*(sbyte*)ptr != 0)
			{
				val = (int)Math.Ceiling(1m / class248_0.Value);
			}
			else
			{
				val = Math.Ceiling(class248_0.Value);
			}
			((byte*)ptr)[1] = ((class248_0.Numerator > flippingListItem_0.MaxHavePerTrade) ? 1 : 0);
			Class248 result;
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				result = Class248.smethod_0(class248_0.Value, (int)Math.Max(flippingListItem_0.MaxHavePerTrade, val));
			}
			else
			{
				((byte*)ptr)[2] = ((class248_0.Denominator > flippingListItem_0.MaxWantPerTrade) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					result = Class248.smethod_0(class248_0.Value, (int)Math.Max(flippingListItem_0.MaxWantPerTrade, val));
				}
				else
				{
					result = class248_0;
				}
			}
			return result;
		}

		public unsafe static bool smethod_6(MainForm mainForm_0, JsonTab jsonTab_0)
		{
			void* ptr = stackalloc byte[21];
			Flipping.Class362 @class = new Flipping.Class362();
			@class.mainForm_0 = mainForm_0;
			Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107270448), new object[]
			{
				jsonTab_0.n
			});
			((byte*)ptr)[8] = ((jsonTab_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				Class181.smethod_3(Enum11.const_2, Flipping.getString_0(107270411));
				((byte*)ptr)[9] = 0;
			}
			else
			{
				((byte*)ptr)[10] = ((!Stashes.Tabs.Contains(jsonTab_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107237074), new object[]
					{
						jsonTab_0.n
					});
					((byte*)ptr)[9] = 0;
				}
				else
				{
					((byte*)ptr)[11] = ((!UI.smethod_13(1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						((byte*)ptr)[9] = 0;
					}
					else
					{
						UI.smethod_35(jsonTab_0.i, false, 1);
						@class.list_0 = Stashes.Items[jsonTab_0.i].ToList<JsonItem>();
						@class.mainForm_0.Invoke(new Action(@class.method_0));
						using (IEnumerator<FlippingListItem> enumerator = Class255.FlippingList.Where(new Func<FlippingListItem, bool>(Flipping.<>c.<>9.method_0)).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Flipping.Class363 class2 = new Flipping.Class363();
								class2.flippingListItem_0 = enumerator.Current;
								Flipping.Class364 class3 = new Flipping.Class364();
								Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107236985), new object[]
								{
									class2.flippingListItem_0.HaveName,
									class2.flippingListItem_0.WantName
								});
								class3.jsonItem_0 = @class.list_0.FirstOrDefault(new Func<JsonItem, bool>(class2.method_0));
								((byte*)ptr)[12] = ((class3.jsonItem_0 == null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 12) != 0)
								{
									Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236924), new object[]
									{
										class2.flippingListItem_0.HaveName,
										class2.flippingListItem_0.WantName
									});
								}
								else
								{
									Class248 class4 = Flipping.smethod_0(class2.flippingListItem_0);
									((byte*)ptr)[13] = ((class4 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 13) == 0)
									{
										Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107236875), new object[]
										{
											class4.method_1(false),
											class4.Value
										});
										ApiItem apiItem = API.smethod_7(class2.flippingListItem_0.HaveName);
										ApiItem apiItem2 = API.smethod_7(class2.flippingListItem_0.WantName);
										((byte*)ptr)[14] = (class2.flippingListItem_0.AutoPrice ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 14) != 0)
										{
											class4.method_0(Flipping.smethod_3(class4, class2.flippingListItem_0));
											class4 = Flipping.smethod_4(class4, class2.flippingListItem_0);
											class4 = Flipping.smethod_5(class4, class2.flippingListItem_0);
										}
										string string_ = string.Format(Flipping.getString_0(107465953), class4.method_1(true), apiItem2.Id);
										JsonItem jsonItem_ = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class3.method_0));
										((byte*)ptr)[15] = ((!class2.flippingListItem_0.OnlyPriceHave) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 15) != 0)
										{
											Flipping.Class365 class5 = new Flipping.Class365();
											class5.jsonItem_0 = @class.list_0.FirstOrDefault(new Func<JsonItem, bool>(class2.method_1));
											((byte*)ptr)[16] = ((class5.jsonItem_0 == null) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 16) != 0)
											{
												Flipping.smethod_8(jsonTab_0, class2.flippingListItem_0, jsonItem_, string_);
												@class.list_0.Remove(class3.jsonItem_0);
												Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236838), new object[]
												{
													class2.flippingListItem_0.WantName
												});
												Control mainForm_ = @class.mainForm_0;
												Action method;
												if ((method = @class.action_0) == null)
												{
													method = (@class.action_0 = new Action(@class.method_1));
												}
												mainForm_.Invoke(method);
											}
											else
											{
												Class248 class6 = new Class248();
												((byte*)ptr)[17] = (class2.flippingListItem_0.AutoPrice ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 17) != 0)
												{
													((byte*)ptr)[18] = (class2.flippingListItem_0.AutoDetermineMargin ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 18) != 0)
													{
														class6 = Flipping.smethod_1(class2.flippingListItem_0);
													}
													else
													{
														class6 = Flipping.smethod_2(class4, Math.Max(class2.flippingListItem_0.MaxHavePerTrade, class2.flippingListItem_0.MaxWantPerTrade), class2.flippingListItem_0.ResellMargin);
													}
													((byte*)ptr)[19] = ((class6 == null) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 19) != 0)
													{
														Thread.Sleep(1000);
														continue;
													}
													class6.method_0(Flipping.smethod_3(class6, class2.flippingListItem_0));
													class6 = Flipping.smethod_4(class6, class2.flippingListItem_0);
													class6 = Flipping.smethod_5(class6, class2.flippingListItem_0);
												}
												else
												{
													class6 = new Class248(class2.flippingListItem_0.SellHaveAmount, class2.flippingListItem_0.SellWantAmount);
												}
												Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107237273), new object[]
												{
													class6.method_1(false),
													class6.Value,
													class2.flippingListItem_0.AutoDetermineMargin
												});
												decimal d = Class248.smethod_1(class4, class6);
												if (class2.flippingListItem_0.AutoDetermineMargin && d < (decimal)class2.flippingListItem_0.IgnoreBelowMargin)
												{
													Enum11 enum11_ = Enum11.const_1;
													string string_2 = Flipping.getString_0(107237260);
													object[] array = new object[4];
													array[0] = class2.flippingListItem_0.HaveName;
													array[1] = class2.flippingListItem_0.WantName;
													array[2] = d.ToString(Flipping.getString_0(107355723));
													int num = 3;
													*(double*)ptr = class2.flippingListItem_0.IgnoreBelowMargin;
													array[num] = ((double*)ptr)->ToString(Flipping.getString_0(107355723));
													Class181.smethod_2(enum11_, string_2, array);
												}
												else
												{
													string string_3 = string.Format(Flipping.getString_0(107465953), class6.method_1(false), apiItem.Id);
													JsonItem jsonItem_2 = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class5.method_0));
													Flipping.smethod_8(jsonTab_0, class2.flippingListItem_0, jsonItem_, string_);
													@class.list_0.Remove(class3.jsonItem_0);
													Flipping.smethod_8(jsonTab_0, class2.flippingListItem_0, jsonItem_2, string_3);
													@class.list_0.Remove(class5.jsonItem_0);
													Control mainForm_2 = @class.mainForm_0;
													Action method2;
													if ((method2 = @class.action_1) == null)
													{
														method2 = (@class.action_1 = new Action(@class.method_2));
													}
													mainForm_2.Invoke(method2);
												}
											}
										}
										else
										{
											Flipping.smethod_8(jsonTab_0, class2.flippingListItem_0, jsonItem_, string_);
											@class.list_0.Remove(class3.jsonItem_0);
											Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107237078), new object[]
											{
												class2.flippingListItem_0.WantName,
												class2.flippingListItem_0.HaveName
											});
											Control mainForm_3 = @class.mainForm_0;
											Action method3;
											if ((method3 = @class.action_2) == null)
											{
												method3 = (@class.action_2 = new Action(@class.method_3));
											}
											mainForm_3.Invoke(method3);
										}
									}
								}
							}
						}
						using (List<JsonItem>.Enumerator enumerator2 = @class.list_0.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Flipping.Class366 class7 = new Flipping.Class366();
								class7.jsonItem_0 = enumerator2.Current;
								((byte*)ptr)[20] = ((class7.jsonItem_0.note != null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 20) != 0)
								{
									JsonItem jsonItem = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class7.method_0));
									jsonItem.note = null;
									UI.smethod_34(jsonTab_0.type, class7.jsonItem_0.x, class7.jsonItem_0.y, Enum2.const_2, false);
									UI.smethod_38(string.Empty);
									Class181.smethod_2(Enum11.const_1, Flipping.getString_0(107236497), new object[]
									{
										class7.jsonItem_0.Name
									});
								}
								Control mainForm_4 = @class.mainForm_0;
								Action method4;
								if ((method4 = @class.action_3) == null)
								{
									method4 = (@class.action_3 = new Action(@class.method_4));
								}
								mainForm_4.Invoke(method4);
							}
						}
						@class.mainForm_0.Invoke(new Action(@class.method_5));
						((byte*)ptr)[9] = 1;
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		public unsafe static bool smethod_7(MainForm mainForm_0, JsonTab jsonTab_0)
		{
			void* ptr = stackalloc byte[24];
			Flipping.Class367 @class = new Flipping.Class367();
			@class.mainForm_0 = mainForm_0;
			Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107270448), new object[]
			{
				jsonTab_0.n
			});
			((byte*)ptr)[8] = ((jsonTab_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				Class181.smethod_3(Enum11.const_2, Flipping.getString_0(107270411));
				((byte*)ptr)[9] = 0;
			}
			else
			{
				((byte*)ptr)[10] = ((!Stashes.Tabs.Contains(jsonTab_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107237074), new object[]
					{
						jsonTab_0.n
					});
					((byte*)ptr)[9] = 0;
				}
				else
				{
					((byte*)ptr)[11] = ((!UI.smethod_13(1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						((byte*)ptr)[9] = 0;
					}
					else
					{
						UI.smethod_35(jsonTab_0.i, false, 1);
						@class.list_0 = Stashes.Items[jsonTab_0.i].ToList<JsonItem>();
						@class.mainForm_0.Invoke(new Action(@class.method_0));
						List<FlippingListJsonItem> list = Web.smethod_11(Class255.FlippingList.Where(new Func<FlippingListItem, bool>(Flipping.<>c.<>9.method_1)));
						((byte*)ptr)[12] = ((list == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 12) != 0)
						{
							Class181.smethod_3(Enum11.const_2, Flipping.getString_0(107236412));
							((byte*)ptr)[9] = 0;
						}
						else
						{
							List<FlippingListItem> list2 = new List<FlippingListItem>();
							using (IEnumerator<FlippingListJsonItem> enumerator = list.Where(new Func<FlippingListJsonItem, bool>(Flipping.<>c.<>9.method_2)).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Flipping.Class368 class2 = new Flipping.Class368();
									class2.flippingListJsonItem_0 = enumerator.Current;
									Flipping.Class369 class3 = new Flipping.Class369();
									Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107236379), new object[]
									{
										class2.flippingListJsonItem_0
									});
									class3.flippingListItem_0 = Flipping.smethod_10(class2.flippingListJsonItem_0);
									((byte*)ptr)[13] = ((class3.flippingListItem_0 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 13) != 0)
									{
										Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236390), new object[]
										{
											class2.flippingListJsonItem_0
										});
									}
									else
									{
										((byte*)ptr)[14] = (list2.Contains(class3.flippingListItem_0) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 14) == 0)
										{
											FlippingListJsonItem flippingListJsonItem = list.FirstOrDefault(new Func<FlippingListJsonItem, bool>(class2.method_0));
											Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107236793), new object[]
											{
												class3.flippingListItem_0.HaveName,
												class3.flippingListItem_0.WantName
											});
											((byte*)ptr)[15] = ((class2.flippingListJsonItem_0.Average == -1m) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 15) != 0)
											{
												Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236736), new object[]
												{
													class3.flippingListItem_0.HaveName,
													class3.flippingListItem_0.WantName
												});
											}
											else
											{
												((byte*)ptr)[16] = ((class2.flippingListJsonItem_0.Average == -2m) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 16) != 0)
												{
													Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236626), new object[]
													{
														class3.flippingListItem_0.HaveName,
														class3.flippingListItem_0.WantName
													});
												}
												else
												{
													class3.jsonItem_0 = @class.list_0.FirstOrDefault(new Func<JsonItem, bool>(class3.method_0));
													((byte*)ptr)[17] = ((class3.jsonItem_0 == null) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 17) != 0)
													{
														Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236924), new object[]
														{
															class3.flippingListItem_0.HaveName,
															class3.flippingListItem_0.WantName
														});
													}
													else
													{
														Class248 class4 = Flipping.smethod_11(class3.flippingListItem_0, class2.flippingListJsonItem_0.Average);
														((byte*)ptr)[18] = ((class4 == null) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 18) != 0)
														{
															Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107235993), new object[]
															{
																class3.flippingListItem_0.HaveName,
																class3.flippingListItem_0.WantName
															});
														}
														else
														{
															Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107236875), new object[]
															{
																class4.method_1(false),
																class4.Value
															});
															ApiItem apiItem = API.smethod_7(class3.flippingListItem_0.HaveName);
															ApiItem apiItem2 = API.smethod_7(class3.flippingListItem_0.WantName);
															class4.method_0(Flipping.smethod_3(class4, class3.flippingListItem_0));
															class4 = Flipping.smethod_4(class4, class3.flippingListItem_0);
															class4 = Flipping.smethod_5(class4, class3.flippingListItem_0);
															string string_ = string.Format(Flipping.getString_0(107465953), class4.method_1(true), apiItem2.Id);
															JsonItem jsonItem_ = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class3.method_1));
															if (!class3.flippingListItem_0.OnlyPriceHave && flippingListJsonItem != null)
															{
																Flipping.Class370 class5 = new Flipping.Class370();
																class5.jsonItem_0 = @class.list_0.FirstOrDefault(new Func<JsonItem, bool>(class3.method_2));
																((byte*)ptr)[19] = ((class5.jsonItem_0 == null) ? 1 : 0);
																if (*(sbyte*)((byte*)ptr + 19) != 0)
																{
																	Flipping.smethod_8(jsonTab_0, class3.flippingListItem_0, jsonItem_, string_);
																	@class.list_0.Remove(class3.jsonItem_0);
																	Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236838), new object[]
																	{
																		class3.flippingListItem_0.WantName
																	});
																	Control mainForm_ = @class.mainForm_0;
																	Action method;
																	if ((method = @class.action_0) == null)
																	{
																		method = (@class.action_0 = new Action(@class.method_1));
																	}
																	mainForm_.Invoke(method);
																	Thread.Sleep(1000);
																}
																else
																{
																	Class248 class6 = new Class248();
																	((byte*)ptr)[20] = (class3.flippingListItem_0.AutoPrice ? 1 : 0);
																	if (*(sbyte*)((byte*)ptr + 20) != 0)
																	{
																		((byte*)ptr)[21] = ((flippingListJsonItem.Average == -1m) ? 1 : 0);
																		if (*(sbyte*)((byte*)ptr + 21) != 0)
																		{
																			Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107235956), new object[]
																			{
																				class3.flippingListItem_0.HaveName,
																				class3.flippingListItem_0.WantName
																			});
																			continue;
																		}
																		((byte*)ptr)[22] = (class3.flippingListItem_0.AutoDetermineMargin ? 1 : 0);
																		if (*(sbyte*)((byte*)ptr + 22) != 0)
																		{
																			class6 = Flipping.smethod_12(class3.flippingListItem_0, flippingListJsonItem.Average);
																		}
																		else
																		{
																			class6 = Flipping.smethod_2(class4, Math.Max(class3.flippingListItem_0.MaxHavePerTrade, class3.flippingListItem_0.MaxWantPerTrade), class3.flippingListItem_0.ResellMargin);
																		}
																		class6.method_0(Flipping.smethod_3(class6, class3.flippingListItem_0));
																		class6 = Flipping.smethod_4(class6, class3.flippingListItem_0);
																		class6 = Flipping.smethod_5(class6, class3.flippingListItem_0);
																	}
																	else
																	{
																		class6 = new Class248(class3.flippingListItem_0.SellHaveAmount, class3.flippingListItem_0.SellWantAmount);
																	}
																	Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107235887), new object[]
																	{
																		class6.method_1(false),
																		class6.Value
																	});
																	decimal d = Class248.smethod_1(class4, class6);
																	if (class3.flippingListItem_0.AutoDetermineMargin && d < (decimal)class3.flippingListItem_0.IgnoreBelowMargin)
																	{
																		Enum11 enum11_ = Enum11.const_1;
																		string string_2 = Flipping.getString_0(107237260);
																		object[] array = new object[4];
																		array[0] = class3.flippingListItem_0.HaveName;
																		array[1] = class3.flippingListItem_0.WantName;
																		array[2] = d.ToString(Flipping.getString_0(107355723));
																		int num = 3;
																		*(double*)ptr = class3.flippingListItem_0.IgnoreBelowMargin;
																		array[num] = ((double*)ptr)->ToString(Flipping.getString_0(107355723));
																		Class181.smethod_2(enum11_, string_2, array);
																	}
																	else
																	{
																		string string_3 = string.Format(Flipping.getString_0(107465953), class6.method_1(false), apiItem.Id);
																		JsonItem jsonItem_2 = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class5.method_0));
																		Flipping.smethod_8(jsonTab_0, class3.flippingListItem_0, jsonItem_, string_);
																		@class.list_0.Remove(class3.jsonItem_0);
																		Flipping.smethod_8(jsonTab_0, class3.flippingListItem_0, jsonItem_2, string_3);
																		@class.list_0.Remove(class5.jsonItem_0);
																		Control mainForm_2 = @class.mainForm_0;
																		Action method2;
																		if ((method2 = @class.action_1) == null)
																		{
																			method2 = (@class.action_1 = new Action(@class.method_2));
																		}
																		mainForm_2.Invoke(method2);
																	}
																}
															}
															else
															{
																Flipping.smethod_8(jsonTab_0, class3.flippingListItem_0, jsonItem_, string_);
																@class.list_0.Remove(class3.jsonItem_0);
																Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107237078), new object[]
																{
																	class3.flippingListItem_0.WantName,
																	class3.flippingListItem_0.HaveName
																});
																Control mainForm_3 = @class.mainForm_0;
																Action method3;
																if ((method3 = @class.action_2) == null)
																{
																	method3 = (@class.action_2 = new Action(@class.method_3));
																}
																mainForm_3.Invoke(method3);
															}
														}
													}
												}
											}
										}
									}
								}
							}
							using (List<JsonItem>.Enumerator enumerator2 = @class.list_0.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Flipping.Class371 class7 = new Flipping.Class371();
									class7.jsonItem_0 = enumerator2.Current;
									((byte*)ptr)[23] = ((class7.jsonItem_0.note != null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 23) != 0)
									{
										JsonItem jsonItem = Stashes.Items[jsonTab_0.i].FirstOrDefault(new Func<JsonItem, bool>(class7.method_0));
										jsonItem.note = null;
										UI.smethod_34(jsonTab_0.type, class7.jsonItem_0.x, class7.jsonItem_0.y, Enum2.const_2, false);
										UI.smethod_38(string.Empty);
										Class181.smethod_2(Enum11.const_1, Flipping.getString_0(107236497), new object[]
										{
											class7.jsonItem_0.Name
										});
									}
									Control mainForm_4 = @class.mainForm_0;
									Action method4;
									if ((method4 = @class.action_3) == null)
									{
										method4 = (@class.action_3 = new Action(@class.method_4));
									}
									mainForm_4.Invoke(method4);
								}
							}
							@class.mainForm_0.Invoke(new Action(@class.method_5));
							((byte*)ptr)[9] = 1;
						}
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		private unsafe static void smethod_8(JsonTab jsonTab_0, FlippingListItem flippingListItem_0, JsonItem jsonItem_0, string string_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((jsonItem_0 != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = ((jsonItem_0.note == string_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_2(Enum11.const_1, Flipping.getString_0(107235850), new object[]
					{
						jsonItem_0.Name,
						string_0
					});
					Thread.Sleep(750);
				}
				else
				{
					jsonItem_0.note = string_0;
					UI.smethod_34(jsonTab_0.type, jsonItem_0.x, jsonItem_0.y, Enum2.const_2, false);
					((byte*)ptr)[2] = (UI.smethod_38(string_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						if (string_0.smethod_9(Flipping.getString_0(107369404)) || string_0.smethod_9(Flipping.getString_0(107442864)))
						{
							Class181.smethod_2(Enum11.const_1, Flipping.getString_0(107236293), new object[]
							{
								jsonItem_0.Name,
								string_0,
								string_0.Replace(Flipping.getString_0(107369404), Flipping.getString_0(107399265)).Replace(Flipping.getString_0(107442864), Flipping.getString_0(107399265))
							});
						}
						else
						{
							Class181.smethod_2(Enum11.const_1, Flipping.getString_0(107236232), new object[]
							{
								jsonItem_0.Name,
								string_0
							});
						}
					}
					else
					{
						Class181.smethod_2(Enum11.const_2, Flipping.getString_0(107236199), new object[]
						{
							jsonItem_0.Name
						});
					}
				}
			}
		}

		public unsafe static void smethod_9(JsonTab jsonTab_0)
		{
			void* ptr = stackalloc byte[18];
			Class181.smethod_3(Enum11.const_0, Flipping.getString_0(107236094));
			((byte*)ptr)[1] = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) == 0)
			{
				List<JsonItem> list = Stashes.Items[jsonTab_0.i].ToList<JsonItem>();
				List<string> list2 = new List<string>();
				List<JsonItem> list3 = new List<JsonItem>();
				*(byte*)ptr = 0;
				using (IEnumerator<FlippingListItem> enumerator = Class255.FlippingList.Where(new Func<FlippingListItem, bool>(Flipping.<>c.<>9.method_3)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Flipping.Class372 @class = new Flipping.Class372();
						@class.flippingListItem_0 = enumerator.Current;
						((byte*)ptr)[2] = (list.Any(new Func<JsonItem, bool>(@class.method_0)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							list.smethod_13(new Func<JsonItem, bool>(@class.method_1));
						}
						else
						{
							list2.Add(@class.flippingListItem_0.HaveName);
						}
						((byte*)ptr)[3] = ((!@class.flippingListItem_0.OnlyPriceHave) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							((byte*)ptr)[4] = (list.Any(new Func<JsonItem, bool>(@class.method_2)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								list.smethod_13(new Func<JsonItem, bool>(@class.method_3));
							}
							else
							{
								list2.Add(@class.flippingListItem_0.WantName);
							}
						}
					}
				}
				((byte*)ptr)[5] = (list2.Any<string>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107235513), new object[]
					{
						string.Join(Flipping.getString_0(107399348), list2)
					});
					Class181.smethod_3(Enum11.const_0, Flipping.getString_0(107235488));
					foreach (string text in list2)
					{
						string text2;
						Dictionary<JsonTab, List<JsonItem>> source = StashManager.smethod_2(text, 1, 0, false, false, string.Empty, out text2, out *(bool*)((byte*)ptr + 6), true);
						((byte*)ptr)[7] = (source.Any<KeyValuePair<JsonTab, List<JsonItem>>>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							KeyValuePair<JsonTab, List<JsonItem>> keyValuePair = source.First<KeyValuePair<JsonTab, List<JsonItem>>>();
							JsonTab key = keyValuePair.Key;
							JsonItem jsonItem = keyValuePair.Value.First<JsonItem>();
							List<Position> source2 = InventoryManager.smethod_8(list3, jsonItem);
							((byte*)ptr)[8] = (source2.Any<Position>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) == 0)
							{
								*(byte*)ptr = 1;
								break;
							}
							UI.smethod_35(key.i, false, 1);
							UI.smethod_34(key.type, jsonItem.x, jsonItem.y, Enum2.const_2, false);
							((byte*)ptr)[9] = ((jsonItem.stack > 1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								((byte*)ptr)[10] = ((API.smethod_7(jsonItem.Name).Stack == 1) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									Win32.smethod_2(true);
								}
								else
								{
									UI.smethod_45(jsonItem, 1);
								}
								jsonItem.stack--;
							}
							else
							{
								Win32.smethod_2(true);
								Stashes.Items[key.i].Remove(jsonItem);
							}
							Position position = source2.First<Position>();
							((byte*)ptr)[11] = ((jsonItem.w > 1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 11) != 0)
							{
								Position position2 = position;
								int num = position2.Left;
								position2.Left = num + 1;
							}
							((byte*)ptr)[12] = ((jsonItem.h > 2) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								Position position3 = position;
								int num = position3.Top;
								position3.Top = num + 1;
							}
							UI.smethod_32(position.x, position.y, Enum2.const_3, true);
							Thread.Sleep(200);
							Win32.smethod_2(true);
							JsonItem jsonItem2 = jsonItem.method_2();
							jsonItem2.stack = 1;
							jsonItem2.x = position.x;
							jsonItem2.y = position.y;
							list3.Add(jsonItem2);
						}
						else
						{
							Class181.smethod_2(Enum11.const_0, Flipping.getString_0(107235451), new object[]
							{
								text
							});
						}
					}
					((byte*)ptr)[13] = (list3.Any<JsonItem>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						UI.smethod_35(jsonTab_0.i, false, 1);
						foreach (JsonItem jsonItem3 in list3)
						{
							Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107235394), new object[]
							{
								jsonItem3.Name,
								jsonItem3.x,
								jsonItem3.y
							});
							UI.smethod_32(jsonItem3.x, jsonItem3.y, Enum2.const_3, true);
							Thread.Sleep(100);
							Win32.smethod_2(true);
							Position position4 = StashManager.smethod_7(jsonTab_0.i, jsonItem3.w, jsonItem3.h);
							((byte*)ptr)[14] = (Position.smethod_1(position4, null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								Class181.smethod_2(Enum11.const_3, Flipping.getString_0(107235377), new object[]
								{
									position4.x,
									position4.y
								});
								((byte*)ptr)[15] = ((jsonItem3.w > 1) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 15) != 0)
								{
									Position position5 = position4;
									int num = position5.Left;
									position5.Left = num + 1;
								}
								((byte*)ptr)[16] = ((jsonItem3.h >= 2) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 16) != 0)
								{
									Position position6 = position4;
									int num = position6.Top;
									position6.Top = num + 1;
								}
								UI.smethod_34(jsonTab_0.type, position4.x, position4.y, Enum2.const_2, false);
								Thread.Sleep(150);
								Win32.smethod_2(true);
								jsonItem3.x = position4.x;
								jsonItem3.y = position4.y;
								Stashes.Items[jsonTab_0.i].Add(jsonItem3);
							}
						}
					}
					((byte*)ptr)[17] = (byte)(*(sbyte*)ptr);
					if (*(sbyte*)((byte*)ptr + 17) != 0)
					{
						Flipping.smethod_9(jsonTab_0);
					}
				}
				else
				{
					Class181.smethod_3(Enum11.const_0, Flipping.getString_0(107235340));
				}
			}
		}

		private static FlippingListItem smethod_10(FlippingListJsonItem flippingListJsonItem_0)
		{
			Flipping.Class373 @class = new Flipping.Class373();
			@class.string_0 = API.smethod_5(flippingListJsonItem_0.have);
			@class.string_1 = API.smethod_5(flippingListJsonItem_0.want);
			return Class255.FlippingList.FirstOrDefault(new Func<FlippingListItem, bool>(@class.method_0));
		}

		public static Class248 smethod_11(FlippingListItem flippingListItem_0, decimal decimal_0)
		{
			Class248 @class = Class248.smethod_0(decimal_0, Math.Max(flippingListItem_0.MaxHavePerTrade, flippingListItem_0.MaxWantPerTrade));
			return new Class248(@class.Denominator, @class.Numerator);
		}

		public static Class248 smethod_12(FlippingListItem flippingListItem_0, decimal decimal_0)
		{
			return Class248.smethod_0(decimal_0, Math.Max(flippingListItem_0.MaxHavePerTrade, flippingListItem_0.MaxWantPerTrade));
		}

		static Flipping()
		{
			Strings.CreateGetStringDelegate(typeof(Flipping));
		}

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class362
		{
			internal void method_0()
			{
				this.mainForm_0.toolStripProgressBar_0.Value = 0;
				this.mainForm_0.toolStripProgressBar_0.Maximum = this.list_0.Count;
			}

			internal void method_1()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			internal void method_2()
			{
				this.mainForm_0.toolStripProgressBar_0.Value += 2;
			}

			internal void method_3()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			internal void method_4()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			internal void method_5()
			{
				this.mainForm_0.toolStripProgressBar_0.Value = 0;
			}

			public MainForm mainForm_0;

			public List<JsonItem> list_0;

			public Action action_0;

			public Action action_1;

			public Action action_2;

			public Action action_3;
		}

		[CompilerGenerated]
		private sealed class Class363
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.flippingListItem_0.HaveName;
			}

			internal bool method_1(JsonItem jsonItem_0)
			{
				return jsonItem_0.Name == this.flippingListItem_0.WantName;
			}

			public FlippingListItem flippingListItem_0;
		}

		[CompilerGenerated]
		private sealed class Class364
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class365
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class366
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class367
		{
			internal void method_0()
			{
				this.mainForm_0.toolStripProgressBar_0.Value = 0;
				this.mainForm_0.toolStripProgressBar_0.Maximum = this.list_0.Count;
			}

			internal void method_1()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			internal void method_2()
			{
				this.mainForm_0.toolStripProgressBar_0.Value += 2;
			}

			internal void method_3()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			internal void method_4()
			{
				ToolStripProgressBar toolStripProgressBar_ = this.mainForm_0.toolStripProgressBar_0;
				int value = toolStripProgressBar_.Value;
				toolStripProgressBar_.Value = value + 1;
			}

			internal void method_5()
			{
				this.mainForm_0.toolStripProgressBar_0.Value = 0;
			}

			public MainForm mainForm_0;

			public List<JsonItem> list_0;

			public Action action_0;

			public Action action_1;

			public Action action_2;

			public Action action_3;
		}

		[CompilerGenerated]
		private sealed class Class368
		{
			internal bool method_0(FlippingListJsonItem flippingListJsonItem_1)
			{
				return flippingListJsonItem_1.have == this.flippingListJsonItem_0.want && flippingListJsonItem_1.want == this.flippingListJsonItem_0.have && flippingListJsonItem_1.type == FlippingTypes.Sell;
			}

			public FlippingListJsonItem flippingListJsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class369
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name == this.flippingListItem_0.HaveName;
			}

			internal bool method_1(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			internal bool method_2(JsonItem jsonItem_1)
			{
				return jsonItem_1.Name == this.flippingListItem_0.WantName;
			}

			public FlippingListItem flippingListItem_0;

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class370
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class371
		{
			internal bool method_0(JsonItem jsonItem_1)
			{
				return jsonItem_1.x == this.jsonItem_0.x && jsonItem_1.y == this.jsonItem_0.y;
			}

			public JsonItem jsonItem_0;
		}

		[CompilerGenerated]
		private sealed class Class372
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine == this.flippingListItem_0.HaveName;
			}

			internal bool method_1(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine == this.flippingListItem_0.HaveName;
			}

			internal bool method_2(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine == this.flippingListItem_0.WantName;
			}

			internal bool method_3(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine == this.flippingListItem_0.WantName;
			}

			public FlippingListItem flippingListItem_0;
		}

		[CompilerGenerated]
		private sealed class Class373
		{
			internal bool method_0(FlippingListItem flippingListItem_0)
			{
				return (flippingListItem_0.HaveName == this.string_0 && flippingListItem_0.WantName == this.string_1) || (flippingListItem_0.HaveName == this.string_1 && flippingListItem_0.WantName == this.string_0);
			}

			public string string_0;

			public string string_1;
		}
	}
}
