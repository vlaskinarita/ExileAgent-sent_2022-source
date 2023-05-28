using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ns0;
using ns13;
using ns14;
using ns25;
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

namespace PoEv2.Features.Expedition
{
	public static class Gwennen
	{
		private static Gwennen.Enum20 ItemDecision
		{
			get
			{
				return (Gwennen.Enum20)Class255.class105_0.method_5(ConfigOptions.GwennenNonUniqueAction);
			}
		}

		public static void smethod_0(MainForm mainForm_1)
		{
			Gwennen.Class381 @class = new Gwennen.Class381();
			@class.mainForm_0 = mainForm_1;
			Util.smethod_29(new ThreadStart(@class.method_0)).Start();
		}

		private unsafe static bool smethod_1()
		{
			void* ptr = stackalloc byte[20];
			Dictionary<string, List<Position>> dictionary = new Dictionary<string, List<Position>>();
			List<Position> list = new List<Position>();
			foreach (string text in Class255.class105_0.method_8<string>(ConfigOptions.GwennenItemList))
			{
				((byte*)ptr)[8] = ((!dictionary.ContainsKey(text)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					dictionary.Add(text, new List<Position>());
				}
				ItemDatum itemDatum = ItemData.smethod_5(text);
				if (itemDatum == null || itemDatum.ItemImage == null)
				{
					Class181.smethod_2(Enum11.const_2, Gwennen.getString_0(107235872), new object[]
					{
						itemDatum.Name
					});
					((byte*)ptr)[9] = 0;
					goto IL_5A8;
				}
				Win32.smethod_4(-2, -2, 50, 90, false);
				Thread.Sleep(200);
				dictionary[text] = UI.smethod_12(Class197.smethod_1(Gwennen.rectangle_0, Gwennen.getString_0(107399346)), Gwennen.dictionary_0[itemDatum.Name], 0.5, text);
			}
			foreach (KeyValuePair<string, List<Position>> keyValuePair in dictionary)
			{
				*(int*)ptr = 0;
				((byte*)ptr)[10] = ((keyValuePair.Value.Count == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					Class181.smethod_2(Enum11.const_0, Gwennen.getString_0(107235807), new object[]
					{
						keyValuePair.Key
					});
				}
				else
				{
					foreach (Position position_ in keyValuePair.Value)
					{
						Position position = Gwennen.smethod_11(position_);
						((byte*)ptr)[11] = (list.Contains(position) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) == 0)
						{
							list.Add(position);
							ItemDatum itemDatum2 = ItemData.smethod_5(keyValuePair.Key);
							if (itemDatum2.Dimensions.Width > 1 && itemDatum2.Dimensions.Height > 1)
							{
								position.smethod_5(position.smethod_6(1, 0));
								position.smethod_5(position.smethod_6(0, 1));
								position.smethod_5(position.smethod_6(1, 1));
								position.smethod_5(position.smethod_6(-1, 0));
								position.smethod_5(position.smethod_6(0, -1));
								position.smethod_5(position.smethod_6(-1, -1));
							}
							Bitmap bitmap = Gwennen.dictionary_0[keyValuePair.Key];
							Position position_2 = position_.smethod_6(Gwennen.rectangle_0.X, Gwennen.rectangle_0.Y).smethod_6(bitmap.Width / 2, bitmap.Height / 2);
							Win32.smethod_5(position_2, false);
							Thread.Sleep(200);
							Item item = new Item(Win32.smethod_21());
							((byte*)ptr)[12] = ((item.typeLine != keyValuePair.Key) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								Class181.smethod_2(Enum11.const_3, Gwennen.getString_0(107235758), new object[]
								{
									item.typeLine,
									keyValuePair.Key
								});
							}
							else
							{
								*(int*)ptr = *(int*)ptr + 1;
								Size size = ItemData.smethod_2(item.typeLine);
								item.width = size.Width;
								item.height = size.Height;
								Class181.smethod_2(Enum11.const_0, Gwennen.getString_0(107235693), new object[]
								{
									keyValuePair.Key
								});
								((byte*)ptr)[13] = ((!Gwennen.smethod_6()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 13) != 0)
								{
									Class181.smethod_2(Enum11.const_2, Gwennen.getString_0(107235712), new object[]
									{
										keyValuePair.Key
									});
									((byte*)ptr)[9] = 0;
									goto IL_5A8;
								}
								Position position2 = Gwennen.smethod_4(item);
								((byte*)ptr)[14] = (Position.smethod_1(position2, null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 14) != 0)
								{
									Win32.smethod_9();
									for (;;)
									{
										((byte*)ptr)[15] = (UI.smethod_9(UI.GameImage, Images.Artifact) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 15) == 0)
										{
											break;
										}
										Win32.smethod_9();
										Thread.Sleep(75);
									}
									UI.smethod_32(position2.Left, position2.Top, Enum2.const_3, true);
									Thread.Sleep(50);
									Item item2 = new Item(Win32.smethod_21());
									item2.Left = position2.Left;
									item2.Top = position2.Top;
									item2.width = item.width;
									item2.height = item.height;
									*(int*)((byte*)ptr + 4) = Gwennen.list_0.IndexOf(item);
									Gwennen.list_0[*(int*)((byte*)ptr + 4)] = item2;
								}
								else
								{
									Class181.smethod_2(Enum11.const_0, Gwennen.getString_0(107235647), new object[]
									{
										item.Name
									});
									UI.smethod_80();
									((byte*)ptr)[16] = ((!Gwennen.smethod_12()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 16) != 0)
									{
										((byte*)ptr)[9] = 0;
										goto IL_5A8;
									}
									UI.smethod_80();
									((byte*)ptr)[17] = ((!Gwennen.smethod_9()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 17) != 0)
									{
										((byte*)ptr)[9] = 0;
										goto IL_5A8;
									}
								}
							}
						}
					}
					((byte*)ptr)[18] = ((*(int*)ptr == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						Class181.smethod_2(Enum11.const_0, Gwennen.getString_0(107235807), new object[]
						{
							keyValuePair.Key
						});
					}
				}
			}
			Class181.smethod_3(Enum11.const_0, Gwennen.getString_0(107235046));
			((byte*)ptr)[19] = ((!Gwennen.smethod_5()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 19) != 0)
			{
				Class181.smethod_3(Enum11.const_2, Gwennen.getString_0(107235005));
				((byte*)ptr)[9] = 0;
			}
			else
			{
				((byte*)ptr)[9] = 1;
			}
			IL_5A8:
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		private static void smethod_2()
		{
			foreach (string text in Class255.class105_0.method_8<string>(ConfigOptions.GwennenItemList))
			{
				ItemDatum itemDatum = ItemData.smethod_5(text);
				if (itemDatum != null && itemDatum.ItemImage != null)
				{
					Gwennen.dictionary_0.Add(itemDatum.Name, Gwennen.smethod_3(itemDatum.ItemImage));
				}
			}
		}

		private static Bitmap smethod_3(Bitmap bitmap_0)
		{
			double num = (double)UI.PoeDimensions.Height / 1080.0;
			return Util.smethod_1(bitmap_0, (int)Math.Round((double)bitmap_0.Width * num), (int)Math.Round((double)bitmap_0.Height * num));
		}

		private static Position smethod_4(Item item_0)
		{
			Position position = InventoryManager.smethod_9(Gwennen.list_0, item_0).FirstOrDefault<Position>();
			Position result;
			if (Position.smethod_0(position, null))
			{
				result = null;
			}
			else
			{
				item_0.Left = position.Left;
				item_0.Top = position.Top;
				Gwennen.list_0.Add(item_0);
				Util.smethod_17(InventoryManager.smethod_7(Gwennen.list_0.smethod_11()), true);
				result = position;
			}
			return result;
		}

		private unsafe static bool smethod_5()
		{
			void* ptr = stackalloc byte[3];
			Position position;
			*(byte*)ptr = ((!UI.smethod_3(out position, Images.RerollOffer, Gwennen.getString_0(107380845))) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				Color pixel = UI.GameImage.GetPixel(Util.smethod_22((double)position.X + 9.0 * UI.GameScale), Util.smethod_22((double)position.Y + 9.0 * UI.GameScale));
				((byte*)ptr)[2] = ((pixel.B < 15) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Bitmap item = Class308.smethod_0(Images.RerollOffer).Item1;
					Win32.smethod_5(position.smethod_6(item.Width / 2, item.Height / 2), false);
					Thread.Sleep(400);
					Win32.smethod_2(true);
					Win32.smethod_5(position.smethod_6(item.Width / 2, Util.smethod_22((double)item.Height * 1.5)), false);
					((byte*)ptr)[1] = 1;
				}
				else
				{
					((byte*)ptr)[1] = 0;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe static bool smethod_6()
		{
			void* ptr = stackalloc byte[21];
			Position position;
			((byte*)ptr)[16] = ((!UI.smethod_3(out position, Images.Artifact, Gwennen.getString_0(107449882))) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				((byte*)ptr)[17] = 0;
			}
			else
			{
				Bitmap item = Class308.smethod_0(Images.Artifact).Item1;
				*(int*)ptr = Util.smethod_22((double)item.Width * 0.1);
				*(int*)((byte*)ptr + 4) = item.Height / 2;
				*(int*)((byte*)ptr + 8) = 0;
				for (;;)
				{
					((byte*)ptr)[20] = ((*(int*)((byte*)ptr + 8) < *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 12) = 0;
					for (;;)
					{
						((byte*)ptr)[19] = ((*(int*)((byte*)ptr + 12) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 19) == 0)
						{
							break;
						}
						Color pixel = UI.GameImage.GetPixel(position.X + *(int*)((byte*)ptr + 8), position.Y + *(int*)((byte*)ptr + 12));
						((byte*)ptr)[18] = ((pixel.R > 200) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 18) != 0)
						{
							goto IL_100;
						}
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
					}
					*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
				}
				((byte*)ptr)[17] = 1;
				goto IL_106;
				IL_100:
				((byte*)ptr)[17] = 0;
			}
			IL_106:
			return *(sbyte*)((byte*)ptr + 17) != 0;
		}

		private unsafe static bool smethod_7()
		{
			void* ptr = stackalloc byte[4];
			if (!Stashes.bool_0 || Stashes.Tabs == null)
			{
				Class181.smethod_3(Enum11.const_2, Gwennen.getString_0(107234920));
				*(byte*)ptr = 0;
			}
			else
			{
				((byte*)ptr)[1] = ((Class255.class105_0.method_8<int>(ConfigOptions.GwennenStashList).Count == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_3(Enum11.const_2, Gwennen.getString_0(107234875));
					*(byte*)ptr = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((Class255.class105_0.method_8<string>(ConfigOptions.GwennenItemList).Count == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_3(Enum11.const_2, Gwennen.getString_0(107235310));
						*(byte*)ptr = 0;
					}
					else
					{
						UI.smethod_1();
						((byte*)ptr)[3] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							Class181.smethod_3(Enum11.const_2, Gwennen.getString_0(107271239));
							*(byte*)ptr = 0;
						}
						else
						{
							*(byte*)ptr = 1;
						}
					}
				}
			}
			return *(sbyte*)ptr != 0;
		}

		private static bool smethod_8()
		{
			UI.smethod_80();
			Thread.Sleep(1000);
			Position position;
			return UI.smethod_3(out position, Images.GwennenTitle, Gwennen.getString_0(107449279));
		}

		private unsafe static bool smethod_9()
		{
			void* ptr = stackalloc byte[25];
			Position position_;
			((byte*)ptr)[17] = ((!UI.smethod_3(out position_, Images.GwennenTitle, Gwennen.getString_0(107449279))) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 17) != 0)
			{
				((byte*)ptr)[18] = 0;
			}
			else
			{
				Bitmap item = Class308.smethod_0(Images.GwennenTitle).Item1;
				Position position_2 = position_.smethod_6(item.Width / 2, item.Height / 2);
				Win32.smethod_5(position_2, false);
				Thread.Sleep(500);
				Win32.smethod_2(true);
				Thread.Sleep(500);
				DateTime t = DateTime.Now.AddSeconds(4.0);
				Position position_3 = null;
				((byte*)ptr)[16] = 0;
				for (;;)
				{
					((byte*)ptr)[20] = ((DateTime.Now < t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) == 0)
					{
						break;
					}
					((byte*)ptr)[19] = (UI.smethod_3(out position_3, Images.GwennenGamble, Gwennen.getString_0(107449268)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						goto IL_E9;
					}
					Thread.Sleep(150);
				}
				goto IL_EF;
				IL_E9:
				((byte*)ptr)[16] = 1;
				IL_EF:
				((byte*)ptr)[21] = ((*(sbyte*)((byte*)ptr + 16) == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 21) != 0)
				{
					((byte*)ptr)[18] = 0;
				}
				else
				{
					item = Class308.smethod_0(Images.GwennenGamble).Item1;
					position_2 = position_3.smethod_6(item.Width / 2, item.Height / 2);
					Win32.smethod_5(position_2, false);
					Thread.Sleep(500);
					Win32.smethod_2(true);
					Thread.Sleep(500);
					UI.smethod_32(-1, 0, Enum2.const_3, true);
					t = DateTime.Now.AddSeconds(4.0);
					((byte*)ptr)[16] = 0;
					Position position;
					for (;;)
					{
						((byte*)ptr)[23] = ((DateTime.Now < t) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 23) == 0)
						{
							break;
						}
						((byte*)ptr)[22] = (UI.smethod_3(out position, Images.RerollOffer, Gwennen.getString_0(107380845)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 22) != 0)
						{
							goto IL_1CA;
						}
						Thread.Sleep(150);
					}
					goto IL_262;
					IL_1CA:
					*(int*)ptr = position.X - Util.smethod_22(610.0 * UI.GameScale);
					*(int*)((byte*)ptr + 4) = position.Y - Util.smethod_22(589.0 * UI.GameScale);
					*(int*)((byte*)ptr + 8) = Util.smethod_22(636.0 * UI.GameScale);
					*(int*)((byte*)ptr + 12) = Util.smethod_22(581.0 * UI.GameScale);
					Gwennen.rectangle_0 = new Rectangle(*(int*)ptr, *(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12));
					UI.smethod_32(-1, 0, Enum2.const_3, true);
					((byte*)ptr)[16] = 1;
					IL_262:
					((byte*)ptr)[24] = ((*(sbyte*)((byte*)ptr + 16) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 24) != 0)
					{
						((byte*)ptr)[18] = 0;
					}
					else
					{
						((byte*)ptr)[18] = 1;
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 18) != 0;
		}

		public unsafe static bool smethod_10()
		{
			void* ptr = stackalloc byte[9];
			Position position_;
			((byte*)ptr)[1] = ((!UI.smethod_3(out position_, Images.GwennenTitle, Gwennen.getString_0(107449279))) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				((byte*)ptr)[2] = 0;
			}
			else
			{
				Bitmap item = Class308.smethod_0(Images.GwennenTitle).Item1;
				Position position_2 = position_.smethod_6(item.Width / 2, item.Height / 2);
				Win32.smethod_5(position_2, false);
				Thread.Sleep(500);
				Win32.smethod_2(true);
				Thread.Sleep(500);
				DateTime t = DateTime.Now.AddSeconds(4.0);
				Position position_3 = null;
				*(byte*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[4] = ((DateTime.Now < t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) == 0)
					{
						break;
					}
					((byte*)ptr)[3] = (UI.smethod_3(out position_3, Images.VendorSellItems, Gwennen.getString_0(107235289)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						goto IL_DF;
					}
					Thread.Sleep(150);
				}
				goto IL_E2;
				IL_DF:
				*(byte*)ptr = 1;
				IL_E2:
				((byte*)ptr)[5] = ((*(sbyte*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					((byte*)ptr)[2] = 0;
				}
				else
				{
					item = Class308.smethod_0(Images.VendorSellItems).Item1;
					position_2 = position_3.smethod_6(item.Width / 2, item.Height / 2);
					Win32.smethod_5(position_2, false);
					Thread.Sleep(250);
					Win32.smethod_2(true);
					t = DateTime.Now.AddSeconds(4.0);
					*(byte*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[7] = ((DateTime.Now < t) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) == 0)
						{
							break;
						}
						Position position;
						((byte*)ptr)[6] = (UI.smethod_3(out position, Images.TradeCancel, Gwennen.getString_0(107381389)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							goto IL_199;
						}
						Thread.Sleep(150);
					}
					goto IL_19C;
					IL_199:
					*(byte*)ptr = 1;
					IL_19C:
					((byte*)ptr)[8] = ((*(sbyte*)ptr == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						((byte*)ptr)[2] = 0;
					}
					else
					{
						((byte*)ptr)[2] = 1;
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public static Position smethod_11(Position position_0)
		{
			double num = UI.smethod_48(Enum10.const_2);
			return new Position(Math.Floor((double)position_0.X / num), Math.Floor((double)position_0.Y / num));
		}

		private unsafe static bool smethod_12()
		{
			void* ptr = stackalloc byte[20];
			((byte*)ptr)[8] = ((Gwennen.ItemDecision == Gwennen.Enum20.const_1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				((byte*)ptr)[9] = ((!Gwennen.smethod_10()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					((byte*)ptr)[10] = 0;
					goto IL_396;
				}
				((byte*)ptr)[11] = ((!Gwennen.smethod_15()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					((byte*)ptr)[10] = 0;
					goto IL_396;
				}
				Gwennen.list_0.RemoveAll(new Predicate<Item>(Gwennen.<>c.<>9.method_0));
				Thread.Sleep(200);
				Win32.smethod_14(Gwennen.getString_0(107396340), false);
				Thread.Sleep(200);
			}
			else
			{
				((byte*)ptr)[12] = ((Gwennen.ItemDecision == Gwennen.Enum20.const_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					UI.smethod_15();
					foreach (Item item in Gwennen.list_0.Where(new Func<Item, bool>(Gwennen.<>c.<>9.method_1)).ToList<Item>())
					{
						UI.smethod_32(item.Left, item.Top, Enum2.const_3, true);
						Thread.Sleep(250);
						Win32.smethod_2(true);
						Win32.smethod_16(Gwennen.getString_0(107235244), true, true, false, false);
					}
					Gwennen.list_0.RemoveAll(new Predicate<Item>(Gwennen.<>c.<>9.method_2));
					((byte*)ptr)[13] = ((!Gwennen.list_0.Any<Item>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						((byte*)ptr)[10] = 1;
						goto IL_396;
					}
				}
			}
			UI.smethod_80();
			((byte*)ptr)[14] = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 14) != 0)
			{
				((byte*)ptr)[10] = 0;
			}
			else if (!Gwennen.smethod_17(Class255.GwennenStashIds, Gwennen.list_0.Where(new Func<Item, bool>(Gwennen.<>c.<>9.method_3)).ToList<Item>().smethod_11()))
			{
				((byte*)ptr)[10] = 0;
			}
			else
			{
				Gwennen.list_0.RemoveAll(new Predicate<Item>(Gwennen.<>c.<>9.method_4));
				((byte*)ptr)[15] = (Gwennen.list_0.Any<Item>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					UI.smethod_35(Gwennen.mainForm_0.class256_0.jsonTab_0.i, false, 1);
					List<int> list_ = new List<int>
					{
						Gwennen.mainForm_0.class256_0.jsonTab_0.i
					};
					int[,] array = UI.smethod_84();
					List<Item> list = new List<Item>();
					*(int*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[18] = ((*(int*)ptr < array.GetLength(0)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 18) == 0)
						{
							break;
						}
						*(int*)((byte*)ptr + 4) = 0;
						for (;;)
						{
							((byte*)ptr)[17] = ((*(int*)((byte*)ptr + 4) < array.GetLength(1)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 17) == 0)
							{
								break;
							}
							((byte*)ptr)[16] = ((array[*(int*)ptr, *(int*)((byte*)ptr + 4)] == 1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 16) != 0)
							{
								list.Add(new Item
								{
									typeLine = Gwennen.getString_0(107393993),
									width = 1,
									height = 1,
									Left = *(int*)ptr,
									Top = *(int*)((byte*)ptr + 4)
								});
							}
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
					((byte*)ptr)[19] = ((!Gwennen.smethod_17(list_, list.smethod_11())) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						((byte*)ptr)[10] = 0;
						goto IL_396;
					}
				}
				Gwennen.list_0.Clear();
				((byte*)ptr)[10] = 1;
			}
			IL_396:
			return *(sbyte*)((byte*)ptr + 10) != 0;
		}

		public static Position smethod_13()
		{
			Position position_;
			Position result;
			if (!UI.smethod_3(out position_, Images.TradeCancel, Gwennen.getString_0(107381389)))
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

		private static bool smethod_14(Item item_0)
		{
			return item_0.stack_size > 1;
		}

		private static bool smethod_15()
		{
			bool result;
			if (!Gwennen.smethod_16(Gwennen.list_0.Where(new Func<Item, bool>(Gwennen.<>c.<>9.method_5)).ToList<Item>().smethod_11()))
			{
				result = false;
			}
			else
			{
				Win32.smethod_5(Gwennen.smethod_13(), false);
				Thread.Sleep(300);
				Win32.smethod_2(true);
				result = true;
			}
			return result;
		}

		private unsafe static bool smethod_16(List<JsonItem> list_1)
		{
			void* ptr = stackalloc byte[12];
			Bitmap bitmap_ = UI.smethod_67();
			foreach (JsonItem jsonItem in list_1)
			{
				Position position_ = UI.smethod_32(jsonItem.Left, jsonItem.Top, Enum2.const_4, true);
				UI.position_0 = position_;
				Class181.smethod_3(Enum11.const_3, string.Format(Gwennen.getString_0(107353052), UI.position_0));
				Win32.smethod_9();
				((byte*)ptr)[8] = ((jsonItem.stack == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					Class181.smethod_3(Enum11.const_3, string.Format(Gwennen.getString_0(107235263), jsonItem.Name));
				}
				else
				{
					Class181.smethod_3(Enum11.const_3, string.Format(Gwennen.getString_0(107235234), jsonItem.stackSize, jsonItem.Name));
				}
			}
			*(int*)ptr = 0;
			Position position_2 = UI.smethod_46(Enum10.const_2, 0, -1);
			for (;;)
			{
				Func<JsonItem, bool> predicate;
				if ((predicate = Gwennen.<>c.<>9__25_1) == null)
				{
					goto IL_2B1;
				}
				IL_F6:
				if (!list_1.Any(predicate))
				{
					break;
				}
				((byte*)ptr)[9] = ((*(int*)ptr > 10) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					break;
				}
				*(int*)ptr = *(int*)ptr + 1;
				Win32.smethod_5(position_2, false);
				((byte*)ptr)[10] = ((*(int*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					Thread.Sleep(100);
				}
				using (Bitmap bitmap = UI.smethod_67())
				{
					List<Rectangle> list = UI.smethod_59(bitmap_, bitmap, Gwennen.getString_0(107399346), 0.4, 0);
					int[,] array = InventoryManager.smethod_12(Gwennen.list_0.smethod_11());
					foreach (Rectangle rectangle in list)
					{
						Position position = UI.smethod_60(Enum10.const_2, rectangle);
						if (position.X >= 0 && position.Y >= 0 && array.GetLength(0) > position.X && array.GetLength(1) > position.Y)
						{
							*(int*)((byte*)ptr + 4) = array[position.X, position.Y];
							if (*(int*)((byte*)ptr + 4) != 0 && list_1.Count<JsonItem>() >= *(int*)((byte*)ptr + 4) - 1)
							{
								list_1[*(int*)((byte*)ptr + 4) - 1] = null;
							}
						}
					}
				}
				using (IEnumerator<JsonItem> enumerator3 = list_1.Where(new Func<JsonItem, bool>(Gwennen.<>c.<>9.method_6)).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						JsonItem jsonItem2 = enumerator3.Current;
						UI.smethod_32(jsonItem2.Left, jsonItem2.Top, Enum2.const_4, true);
						Win32.smethod_9();
					}
					continue;
				}
				IL_2B1:
				predicate = (Gwennen.<>c.<>9__25_1 = new Func<JsonItem, bool>(Gwennen.<>c.<>9.method_7));
				goto IL_F6;
			}
			Win32.smethod_5(UI.smethod_46(Enum10.const_3, 0, 5), false);
			((byte*)ptr)[11] = 1;
			return *(sbyte*)((byte*)ptr + 11) != 0;
		}

		private unsafe static bool smethod_17(List<int> list_1, List<JsonItem> list_2)
		{
			void* ptr = stackalloc byte[17];
			*(int*)ptr = -1;
			List<JsonItem> list = new List<JsonItem>();
			Class181.smethod_3(Enum11.const_0, Gwennen.getString_0(107235153));
			foreach (JsonItem jsonItem in list_2)
			{
				*(int*)((byte*)ptr + 4) = -1;
				foreach (int num in list_1)
				{
					*(int*)((byte*)ptr + 8) = num;
					((byte*)ptr)[12] = (Stashes.smethod_11(*(int*)((byte*)ptr + 8)).IsSpecialTab ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						if (!StashManager.smethod_9(*(int*)((byte*)ptr + 8), new List<JsonItem>
						{
							jsonItem
						}))
						{
							continue;
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 8);
					}
					else
					{
						Position position = StashManager.smethod_7(*(int*)((byte*)ptr + 8), jsonItem.w, jsonItem.h);
						((byte*)ptr)[13] = (Position.smethod_1(position, null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) == 0)
						{
							continue;
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 8);
						JsonItem jsonItem2 = jsonItem.method_2();
						jsonItem2.x = position.x;
						Stashes.Items[*(int*)((byte*)ptr + 4)].Add(jsonItem2);
						jsonItem2.y = position.y;
					}
					break;
				}
				((byte*)ptr)[14] = ((*(int*)((byte*)ptr + 4) == -1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Gwennen.getString_0(107235116), new object[]
					{
						jsonItem.Name
					});
					((byte*)ptr)[15] = 0;
					goto IL_1F9;
				}
				((byte*)ptr)[16] = ((*(int*)ptr != *(int*)((byte*)ptr + 4)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					Gwennen.smethod_18(list);
					UI.smethod_35(*(int*)((byte*)ptr + 4), true, 1);
					list.Clear();
					*(int*)ptr = *(int*)((byte*)ptr + 4);
				}
				list.Add(jsonItem);
				UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
				Win32.smethod_9();
				Thread.Sleep(50);
			}
			Gwennen.smethod_18(list);
			UI.smethod_32(0, -1, Enum2.const_3, true);
			Class181.smethod_3(Enum11.const_0, Gwennen.getString_0(107272938));
			((byte*)ptr)[15] = 1;
			IL_1F9:
			return *(sbyte*)((byte*)ptr + 15) != 0;
		}

		private unsafe static void smethod_18(List<JsonItem> list_1)
		{
			void* ptr = stackalloc byte[12];
			if (list_1.Any<JsonItem>() && UI.smethod_13(1))
			{
				int[,] array = UI.smethod_84();
				int[,] array2 = InventoryManager.smethod_7(list_1);
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
							Thread.Sleep(50);
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
					Gwennen.smethod_18(list_1);
				}
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Gwennen()
		{
			Strings.CreateGetStringDelegate(typeof(Gwennen));
			Gwennen.string_0 = new string[]
			{
				Gwennen.getString_0(107266060),
				Gwennen.getString_0(107437880),
				Gwennen.getString_0(107437319)
			};
		}

		private const double double_0 = 0.5;

		public static string[] string_0;

		private static MainForm mainForm_0;

		private static List<Item> list_0;

		private static Dictionary<string, Bitmap> dictionary_0;

		private static Rectangle rectangle_0;

		[NonSerialized]
		internal static GetString getString_0;

		private enum Enum20
		{
			const_0,
			const_1,
			const_2
		}

		[CompilerGenerated]
		private sealed class Class381
		{
			[DebuggerStepThrough]
			internal void method_0()
			{
				Gwennen.Class381.Class382 @class = new Gwennen.Class381.Class382();
				@class.class381_0 = this;
				@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
				asyncVoidMethodBuilder_.Start<Gwennen.Class381.Class382>(ref @class);
			}

			internal unsafe void method_1()
			{
				void* ptr = stackalloc byte[5];
				try
				{
					Gwennen.list_0 = new List<Item>();
					Gwennen.dictionary_0 = new Dictionary<string, Bitmap>();
					Gwennen.mainForm_0 = this.mainForm_0;
					Gwennen.mainForm_0.thread_5 = Thread.CurrentThread;
					Gwennen.mainForm_0.method_116();
					*(byte*)ptr = ((!Gwennen.smethod_7()) ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						Gwennen.mainForm_0.method_148();
					}
					else
					{
						((byte*)ptr)[1] = ((!UI.smethod_13(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) != 0)
						{
							Gwennen.mainForm_0.method_148();
						}
						else
						{
							((byte*)ptr)[2] = ((!Gwennen.smethod_8()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								Class181.smethod_3(Enum11.const_2, Gwennen.Class381.getString_0(107248003));
								Gwennen.mainForm_0.method_148();
							}
							else
							{
								((byte*)ptr)[3] = ((!Gwennen.smethod_9()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 3) != 0)
								{
									Class181.smethod_3(Enum11.const_2, Gwennen.Class381.getString_0(107247970));
									Gwennen.mainForm_0.method_148();
								}
								else
								{
									Gwennen.smethod_2();
									do
									{
										((byte*)ptr)[4] = (Gwennen.smethod_1() ? 1 : 0);
									}
									while (*(sbyte*)((byte*)ptr + 4) != 0);
									Gwennen.mainForm_0.method_148();
								}
							}
						}
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception ex)
				{
					Class181.smethod_2(Enum11.const_2, Gwennen.Class381.getString_0(107247381), new object[]
					{
						ex
					});
				}
			}

			static Class381()
			{
				Strings.CreateGetStringDelegate(typeof(Gwennen.Class381));
			}

			public MainForm mainForm_0;

			public Action action_0;

			[NonSerialized]
			internal static GetString getString_0;

			private sealed class Class382 : IAsyncStateMachine
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
							if ((action = this.class381_0.action_0) == null)
							{
								action = (this.class381_0.action_0 = new Action(this.class381_0.method_1));
							}
							awaiter = Task.Run(action).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.int_0 = 0;
								this.configuredTaskAwaiter_0 = awaiter;
								Gwennen.Class381.Class382 @class = this;
								this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Gwennen.Class381.Class382>(ref awaiter, ref @class);
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

				public Gwennen.Class381 class381_0;

				private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter_0;
			}
		}
	}
}
