using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ns0;
using ns14;
using ns2;
using ns25;
using ns27;
using ns29;
using ns32;
using ns34;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class BeastCapture
	{
		[DebuggerStepThrough]
		public static void smethod_0(MainForm mainForm_1)
		{
			BeastCapture.Class343 @class = new BeastCapture.Class343();
			@class.mainForm_0 = mainForm_1;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<BeastCapture.Class343>(ref @class);
		}

		private unsafe static bool smethod_1()
		{
			void* ptr = stackalloc byte[6];
			UI.smethod_80();
			UI.smethod_15();
			UI.smethod_94();
			DateTime t = DateTime.Now.AddMilliseconds(750.0);
			for (;;)
			{
				((byte*)ptr)[2] = ((!UI.smethod_9(UI.GameImage, Images.Challenges)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) == 0)
				{
					break;
				}
				Thread.Sleep(50);
				*(byte*)ptr = ((DateTime.Now > t) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					goto IL_166;
				}
			}
			Position position_;
			((byte*)ptr)[3] = ((!UI.smethod_3(out position_, Images.BestiaryTab, BeastCapture.getString_0(107448882))) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				Class181.smethod_3(Enum11.const_2, BeastCapture.getString_0(107273813));
				((byte*)ptr)[1] = (BeastCapture.smethod_1() ? 1 : 0);
				goto IL_16F;
			}
			Bitmap item = Class308.smethod_0(Images.BestiaryTab).Item1;
			Win32.smethod_5(position_.smethod_6(item.Width / 2, item.Height / 2), false);
			Thread.Sleep(BeastCapture.BestiarySleep);
			Win32.smethod_2(true);
			Thread.Sleep(BeastCapture.BestiarySleep);
			Win32.smethod_5(Class251.CapturedBeasts, false);
			Thread.Sleep(BeastCapture.BestiarySleep);
			Win32.smethod_2(true);
			t = DateTime.Now.AddSeconds(5.0);
			for (;;)
			{
				((byte*)ptr)[5] = ((DateTime.Now < t) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					break;
				}
				((byte*)ptr)[4] = (UI.smethod_9(UI.GameImage, Images.FilterBeasts) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					goto IL_15F;
				}
				Thread.Sleep(250);
			}
			((byte*)ptr)[1] = 0;
			goto IL_16F;
			IL_15F:
			((byte*)ptr)[1] = 1;
			goto IL_16F;
			IL_166:
			((byte*)ptr)[1] = (BeastCapture.smethod_1() ? 1 : 0);
			IL_16F:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe static bool smethod_2()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, BeastCapture.getString_0(107273276));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = ((!Class255.BeastStashIds.Any<int>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, BeastCapture.getString_0(107273175));
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[3] = ((!UI.smethod_13(1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						BeastCapture.smethod_3(true);
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[4] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							Class181.smethod_3(Enum11.const_2, BeastCapture.getString_0(107273138));
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

		private static void smethod_3(bool bool_1 = true)
		{
			Win32.smethod_18();
			Win32.smethod_20();
			BeastCapture.mainForm_0.method_121();
			BeastCapture.mainForm_0.enum8_0 = Enum8.const_1;
			if (bool_1 && BeastCapture.mainForm_0.thread_5 != null)
			{
				BeastCapture.mainForm_0.thread_5.Abort();
				BeastCapture.mainForm_0.thread_5 = null;
			}
		}

		private unsafe static bool smethod_4()
		{
			void* ptr = stackalloc byte[8];
			*(byte*)ptr = ((!BeastCapture.list_1.Any<string>()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 1;
			}
			else
			{
				string text = BeastCapture.list_1.First<string>();
				Images images_ = BeastCapture.smethod_6(text);
				Class181.smethod_2(Enum11.const_0, BeastCapture.getString_0(107273117), new object[]
				{
					text
				});
				UI.smethod_76(text);
				for (;;)
				{
					((byte*)ptr)[7] = ((BeastCapture.int_1 > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) == 0)
					{
						break;
					}
					((byte*)ptr)[2] = ((!BeastCapture.OrbsInInventory) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						goto IL_340;
					}
					using (Bitmap bitmap = Class197.smethod_1(Class251.Stash, BeastCapture.getString_0(107399127)))
					{
						List<Position> source = UI.smethod_11(images_, text);
						((byte*)ptr)[3] = (source.Any<Position>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							Position position_ = source.OrderBy(new Func<Position, int>(BeastCapture.<>c.<>9.method_0)).ThenBy(new Func<Position, int>(BeastCapture.<>c.<>9.method_1)).First<Position>();
							JsonItem jsonItem = BeastCapture.list_2.First<JsonItem>();
							UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_3();
							Win32.smethod_5(position_, false);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_2(true);
							Win32.smethod_5(BeastCapture.DefaultPos, false);
							((byte*)ptr)[4] = ((jsonItem.stack == 1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								BeastCapture.list_2.Remove(jsonItem);
							}
							JsonItem jsonItem2 = new JsonItem
							{
								w = 1,
								h = 1,
								stack = 1
							};
							Position position = InventoryManager.smethod_8(BeastCapture.list_2, jsonItem2).First<Position>();
							UI.smethod_32(position.x, position.y, Enum2.const_3, true);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_2(true);
							jsonItem2.x = position.x;
							jsonItem2.y = position.y;
							BeastCapture.list_2.Add(jsonItem2);
							BeastCapture.int_1--;
							JsonItem jsonItem3 = jsonItem;
							int stack = jsonItem3.stack;
							jsonItem3.stack = stack - 1;
							BeastCapture.bool_0 = true;
							BeastCapture.mainForm_0.method_123(1);
						}
						else
						{
							((byte*)ptr)[5] = ((!BeastCapture.bool_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								BeastCapture.int_2++;
							}
							int num = BeastCapture.bool_0 ? BeastCapture.int_2 : 1;
							BeastCapture.bool_0 = false;
							Win32.smethod_5(BeastCapture.DefaultPos, false);
							Thread.Sleep(200);
							Win32.smethod_12(Enum6.const_1, 4 * num);
							Thread.Sleep(300);
							using (Bitmap bitmap2 = Class197.smethod_1(Class251.Stash, BeastCapture.getString_0(107399127)))
							{
								((byte*)ptr)[6] = ((!UI.smethod_59(bitmap, bitmap2, BeastCapture.getString_0(107399127), 0.4, 0).Any<Rectangle>()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 6) != 0)
								{
									Class181.smethod_2(Enum11.const_0, BeastCapture.getString_0(107273096), new object[]
									{
										text
									});
									BeastCapture.list_1.Remove(text);
									BeastCapture.int_2 = 1;
									((byte*)ptr)[1] = (BeastCapture.smethod_4() ? 1 : 0);
									goto IL_345;
								}
							}
						}
					}
				}
				((byte*)ptr)[1] = 1;
				goto IL_345;
				IL_340:
				((byte*)ptr)[1] = 1;
			}
			IL_345:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe static bool smethod_5()
		{
			void* ptr = stackalloc byte[6];
			((byte*)ptr)[4] = (BeastCapture.OrbsInInventory ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				((byte*)ptr)[5] = 1;
			}
			else
			{
				*(int*)ptr = Math.Min(BeastCapture.int_1, 60);
				Order order_ = new Order
				{
					my_item_amount = *(int*)ptr,
					my_item_name = BeastCapture.getString_0(107273039),
					my_items = StashManager.smethod_1(BeastCapture.getString_0(107273039), true)
				};
				BeastCapture.list_2 = InventoryManager.smethod_4(order_, new List<JsonItem>());
				((byte*)ptr)[5] = (BeastCapture.smethod_1() ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		private static Images smethod_6(string string_0)
		{
			if (string_0 != null)
			{
				uint num = Class396.smethod_0(string_0);
				if (num <= 1836601437U)
				{
					if (num <= 1383133494U)
					{
						if (num != 1036292173U)
						{
							if (num != 1159014565U)
							{
								if (num == 1383133494U)
								{
									if (string_0 == BeastCapture.getString_0(107273479))
									{
										return Images.BeastInsect;
									}
								}
							}
							else if (string_0 == BeastCapture.getString_0(107273460))
							{
								return Images.BeastReptile;
							}
						}
						else if (string_0 == BeastCapture.getString_0(107273436))
						{
							return Images.BeastCrustacean;
						}
					}
					else if (num != 1739076260U)
					{
						if (num != 1811790319U)
						{
							if (num == 1836601437U)
							{
								if (string_0 == BeastCapture.getString_0(107273495))
								{
									return Images.BeastUrsae;
								}
							}
						}
						else if (string_0 == BeastCapture.getString_0(107273054))
						{
							return Images.BeastFeline;
						}
					}
					else if (string_0 == BeastCapture.getString_0(107273486))
					{
						return Images.BeastUnnatural;
					}
				}
				else if (num <= 2259480748U)
				{
					if (num != 2238197078U)
					{
						if (num != 2241442646U)
						{
							if (num == 2259480748U)
							{
								if (string_0 == BeastCapture.getString_0(107273521))
								{
									return Images.BeastPrimate;
								}
							}
						}
						else if (string_0 == BeastCapture.getString_0(107273540))
						{
							return Images.BeastCanine;
						}
					}
					else if (string_0 == BeastCapture.getString_0(107273434))
					{
						return Images.BeastArachnid;
					}
				}
				else if (num != 2444247301U)
				{
					if (num != 2622592047U)
					{
						if (num == 3467658981U)
						{
							if (string_0 == BeastCapture.getString_0(107273387))
							{
								return Images.BeastAmphibian;
							}
						}
					}
					else if (string_0 == BeastCapture.getString_0(107273501))
					{
						return Images.BeastAvian;
					}
				}
				else if (string_0 == BeastCapture.getString_0(107273421))
				{
					return Images.BeastCephalopod;
				}
			}
			return Images.BeastFeline;
		}

		private unsafe static bool smethod_7()
		{
			void* ptr = stackalloc byte[20];
			((byte*)ptr)[12] = ((!BeastCapture.list_2.Any<JsonItem>()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				((byte*)ptr)[13] = 1;
			}
			else
			{
				UI.smethod_80();
				Thread.Sleep(1000);
				((byte*)ptr)[14] = ((!UI.smethod_13(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					((byte*)ptr)[13] = 0;
				}
				else
				{
					List<JsonItem> list = BeastCapture.list_2.Where(new Func<JsonItem, bool>(BeastCapture.<>c.<>9.method_2)).ToList<JsonItem>();
					((byte*)ptr)[15] = (list.Any<JsonItem>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						Class181.smethod_3(Enum11.const_0, BeastCapture.getString_0(107273370));
						UI.smethod_35(BeastCapture.jsonTab_0.i, false, 1);
						foreach (JsonItem jsonItem in list)
						{
							UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_9();
						}
						BeastCapture.smethod_9(list);
						BeastCapture.list_2.RemoveAll(new Predicate<JsonItem>(BeastCapture.<>c.<>9.method_3));
					}
					((byte*)ptr)[16] = ((!BeastCapture.list_2.Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						((byte*)ptr)[13] = 1;
					}
					else
					{
						*(int*)ptr = -1;
						Class181.smethod_2(Enum11.const_0, BeastCapture.getString_0(107273325), new object[]
						{
							BeastCapture.list_2.Count
						});
						foreach (JsonItem jsonItem2 in BeastCapture.list_2.OrderBy(new Func<JsonItem, int>(BeastCapture.<>c.<>9.method_4)).ThenBy(new Func<JsonItem, int>(BeastCapture.<>c.<>9.method_5)))
						{
							*(int*)((byte*)ptr + 4) = -1;
							foreach (int num in Class255.BeastStashIds)
							{
								*(int*)((byte*)ptr + 8) = num;
								Position position = StashManager.smethod_7(*(int*)((byte*)ptr + 8), jsonItem2.w, jsonItem2.h);
								((byte*)ptr)[17] = (Position.smethod_1(position, null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 17) != 0)
								{
									*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 8);
									JsonItem jsonItem3 = jsonItem2.method_2();
									jsonItem3.x = position.x;
									Stashes.Items[*(int*)((byte*)ptr + 4)].Add(jsonItem3);
									jsonItem3.y = position.y;
									break;
								}
							}
							((byte*)ptr)[18] = ((*(int*)((byte*)ptr + 4) == -1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 18) != 0)
							{
								Class181.smethod_3(Enum11.const_2, BeastCapture.getString_0(107273292));
								((byte*)ptr)[13] = 0;
								goto IL_363;
							}
							((byte*)ptr)[19] = ((*(int*)ptr != *(int*)((byte*)ptr + 4)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) != 0)
							{
								UI.smethod_35(*(int*)((byte*)ptr + 4), false, 1);
								*(int*)ptr = *(int*)((byte*)ptr + 4);
							}
							UI.smethod_32(jsonItem2.x, jsonItem2.y, Enum2.const_3, true);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_9();
						}
						BeastCapture.smethod_8();
						UI.smethod_32(0, -1, Enum2.const_3, true);
						Class181.smethod_3(Enum11.const_0, BeastCapture.getString_0(107272719));
						((byte*)ptr)[13] = 1;
					}
				}
			}
			IL_363:
			return *(sbyte*)((byte*)ptr + 13) != 0;
		}

		private unsafe static void smethod_8()
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[8] = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				int[,] array = UI.smethod_84();
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[11] = ((*(int*)ptr < array.GetLength(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[10] = ((*(int*)((byte*)ptr + 4) < array.GetLength(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) == 0)
						{
							break;
						}
						((byte*)ptr)[9] = ((array[*(int*)ptr, *(int*)((byte*)ptr + 4)] == 1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							UI.smethod_32(*(int*)ptr, *(int*)((byte*)ptr + 4), Enum2.const_3, true);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_9();
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
		}

		private unsafe static void smethod_9(List<JsonItem> list_3)
		{
			void* ptr = stackalloc byte[12];
			if (list_3.Any<JsonItem>() && UI.smethod_13(1))
			{
				int[,] array = UI.smethod_84();
				int[,] array2 = InventoryManager.smethod_7(list_3);
				((byte*)ptr)[8] = 0;
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[10] = ((*(int*)ptr < 12) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[9] = ((*(int*)((byte*)ptr + 4) < 5) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) == 0)
						{
							break;
						}
						if (array2[*(int*)ptr, *(int*)((byte*)ptr + 4)] == 1 && array[*(int*)ptr, *(int*)((byte*)ptr + 4)] == 1)
						{
							UI.smethod_32(*(int*)ptr, *(int*)((byte*)ptr + 4), Enum2.const_3, true);
							Thread.Sleep(BeastCapture.InventorySleep);
							Win32.smethod_9();
							((byte*)ptr)[8] = 1;
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				((byte*)ptr)[11] = (byte)(*(sbyte*)((byte*)ptr + 8));
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					BeastCapture.smethod_9(list_3);
				}
			}
		}

		private static int BeastLimit
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.BeastOrbLimit);
			}
		}

		private static bool OrbsInInventory
		{
			get
			{
				return BeastCapture.list_2.Any(new Func<JsonItem, bool>(BeastCapture.<>c.<>9.method_6));
			}
		}

		private static Position DefaultPos
		{
			get
			{
				return Class251.DefaultBeastMousePos;
			}
		}

		private static int InventorySleep
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.BeastInventoryClickTiming);
			}
		}

		private static int BestiarySleep
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.BeastOpenBestiaryTiming);
			}
		}

		private static bool StartAfterFinished
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.BeastStartAfterFinished);
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static BeastCapture()
		{
			Strings.CreateGetStringDelegate(typeof(BeastCapture));
			BeastCapture.list_0 = new List<string>
			{
				BeastCapture.getString_0(107273054),
				BeastCapture.getString_0(107273521),
				BeastCapture.getString_0(107273540),
				BeastCapture.getString_0(107273495),
				BeastCapture.getString_0(107273486),
				BeastCapture.getString_0(107273501),
				BeastCapture.getString_0(107273460),
				BeastCapture.getString_0(107273479),
				BeastCapture.getString_0(107273434),
				BeastCapture.getString_0(107273421),
				BeastCapture.getString_0(107273436),
				BeastCapture.getString_0(107273387)
			};
			BeastCapture.int_1 = 0;
			BeastCapture.list_2 = new List<JsonItem>();
			BeastCapture.int_2 = 1;
			BeastCapture.bool_0 = false;
		}

		private const int int_0 = 4;

		private static readonly List<string> list_0;

		private static MainForm mainForm_0;

		private static int int_1;

		private static List<string> list_1;

		private static List<JsonItem> list_2;

		private static JsonTab jsonTab_0;

		private static int int_2;

		private static bool bool_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class341
		{
			[DebuggerStepThrough]
			internal void method_0()
			{
				BeastCapture.Class341.Class342 @class = new BeastCapture.Class341.Class342();
				@class.class341_0 = this;
				@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
				asyncVoidMethodBuilder_.Start<BeastCapture.Class341.Class342>(ref @class);
			}

			internal unsafe void method_1()
			{
				void* ptr = stackalloc byte[6];
				BeastCapture.mainForm_0 = this.mainForm_0;
				BeastCapture.mainForm_0.thread_5 = Thread.CurrentThread;
				BeastCapture.mainForm_0.method_116();
				UI.smethod_1();
				*(byte*)ptr = ((!BeastCapture.smethod_2()) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					BeastCapture.smethod_3(true);
				}
				else
				{
					BeastCapture.list_1 = BeastCapture.list_0.ToList<string>();
					Dictionary<JsonTab, List<JsonItem>> dictionary = StashManager.smethod_1(BeastCapture.Class341.getString_0(107273042), true);
					BeastCapture.list_2 = new List<JsonItem>();
					BeastCapture.int_1 = dictionary.smethod_16(false);
					((byte*)ptr)[1] = ((BeastCapture.int_1 == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Class181.smethod_3(Enum11.const_2, BeastCapture.Class341.getString_0(107250490));
						BeastCapture.smethod_3(true);
					}
					else
					{
						BeastCapture.jsonTab_0 = dictionary.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
						((byte*)ptr)[2] = ((BeastCapture.BeastLimit > 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							Class181.smethod_2(Enum11.const_0, BeastCapture.Class341.getString_0(107250409), new object[]
							{
								BeastCapture.int_1,
								BeastCapture.BeastLimit
							});
							BeastCapture.int_1 = Math.Min(BeastCapture.int_1, BeastCapture.BeastLimit);
						}
						else
						{
							Class181.smethod_2(Enum11.const_0, BeastCapture.Class341.getString_0(107250320), new object[]
							{
								BeastCapture.int_1
							});
						}
						BeastCapture.mainForm_0.method_122(BeastCapture.int_1);
						while (BeastCapture.int_1 > 0 && BeastCapture.list_1.Any<string>())
						{
							((byte*)ptr)[3] = ((!BeastCapture.smethod_5()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								BeastCapture.smethod_3(true);
								return;
							}
							((byte*)ptr)[4] = ((!BeastCapture.smethod_4()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								BeastCapture.smethod_3(true);
								return;
							}
							((byte*)ptr)[5] = ((!BeastCapture.smethod_7()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								BeastCapture.smethod_3(true);
								return;
							}
						}
						Class181.smethod_3(Enum11.const_0, BeastCapture.Class341.getString_0(107250287));
						if (BeastCapture.StartAfterFinished && !BeastCapture.mainForm_0.bool_12)
						{
							BeastCapture.smethod_3(false);
							BeastCapture.mainForm_0.method_58(false, MainForm.GEnum1.const_0, false);
						}
						else
						{
							BeastCapture.smethod_3(true);
						}
					}
				}
			}

			static Class341()
			{
				Strings.CreateGetStringDelegate(typeof(BeastCapture.Class341));
			}

			public MainForm mainForm_0;

			public Action action_0;

			[NonSerialized]
			internal static GetString getString_0;

			private sealed class Class342 : IAsyncStateMachine
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
							if ((action = this.class341_0.action_0) == null)
							{
								action = (this.class341_0.action_0 = new Action(this.class341_0.method_1));
							}
							awaiter = Task.Run(action).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.int_0 = 0;
								this.configuredTaskAwaiter_0 = awaiter;
								BeastCapture.Class341.Class342 @class = this;
								this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BeastCapture.Class341.Class342>(ref awaiter, ref @class);
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

				public BeastCapture.Class341 class341_0;

				private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter_0;
			}
		}
	}
}
