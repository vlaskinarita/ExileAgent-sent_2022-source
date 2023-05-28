using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ns0;
using ns13;
using ns14;
using ns2;
using ns25;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features.Crafting
{
	public sealed class MapCrafter
	{
		public MapCrafter(MainForm form, JsonTab tab)
		{
			this.mainForm_0 = form;
			this.jsonTab_0 = tab;
			this.materials_0 = new Materials();
			this.list_0 = new List<JsonItem>();
		}

		[DebuggerStepThrough]
		public void method_0()
		{
			MapCrafter.Class389 @class = new MapCrafter.Class389();
			@class.mapCrafter_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MapCrafter.Class389>(ref @class);
		}

		private unsafe bool method_1()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, MapCrafter.getString_0(107273595));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, MapCrafter.getString_0(107273457));
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[3] = ((this.jsonTab_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						Class181.smethod_3(Enum11.const_2, MapCrafter.getString_0(107232380));
						((byte*)ptr)[1] = 0;
					}
					else if (this.CurrencyTab != null && Web.smethod_15(this.CurrencyTab))
					{
						Class181.smethod_3(Enum11.const_2, MapCrafter.getString_0(107233060));
						((byte*)ptr)[1] = 0;
					}
					else
					{
						((byte*)ptr)[4] = ((!UI.smethod_13(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
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

		private void method_2()
		{
			Win32.smethod_18();
			Win32.smethod_20();
			this.mainForm_0.method_12(true);
			this.mainForm_0.enum8_0 = Enum8.const_1;
			if (this.mainForm_0.thread_5 != null)
			{
				this.mainForm_0.thread_5.Abort();
				this.mainForm_0.thread_5 = null;
			}
		}

		private unsafe void method_3()
		{
			void* ptr = stackalloc byte[31];
			Class181.smethod_3(Enum11.const_0, MapCrafter.getString_0(107232323));
			this.method_13();
			this.dictionary_0 = StashManager.smethod_1(MapCrafter.getString_0(107408259), true);
			this.dictionary_1 = StashManager.smethod_1(MapCrafter.getString_0(107385955), true);
			this.dictionary_2 = StashManager.smethod_1(MapCrafter.getString_0(107385772), true);
			this.dictionary_3 = StashManager.smethod_1(MapCrafter.getString_0(107385158), true);
			this.dictionary_4 = StashManager.smethod_1(MapCrafter.getString_0(107394093), true);
			this.dictionary_5 = StashManager.smethod_1(MapCrafter.getString_0(107385012), true);
			this.dictionary_6 = StashManager.smethod_1(MapCrafter.getString_0(107363746), true);
			this.list_0 = Stashes.Items[this.jsonTab_0.i].Where(new Func<JsonItem, bool>(this.method_16)).ToList<JsonItem>();
			*(int*)ptr = this.list_0.Count;
			((byte*)ptr)[20] = 1;
			*(int*)((byte*)ptr + 4) = 0;
			((byte*)ptr)[21] = ((*(int*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				Class181.smethod_2(Enum11.const_2, MapCrafter.getString_0(107232294), new object[]
				{
					this.jsonTab_0.n
				});
				this.method_2();
			}
			else
			{
				Class181.smethod_2(Enum11.const_0, MapCrafter.getString_0(107232233), new object[]
				{
					*(int*)ptr
				});
				this.mainForm_0.method_122(*(int*)ptr);
				for (;;)
				{
					*(int*)((byte*)ptr + 4) = this.method_4();
					List<JsonItem> list = new List<JsonItem>();
					*(int*)((byte*)ptr + 8) = 0;
					for (;;)
					{
						((byte*)ptr)[22] = ((*(int*)((byte*)ptr + 8) < *(int*)((byte*)ptr + 4)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 22) == 0)
						{
							break;
						}
						JsonItem item = new JsonItem
						{
							w = 1,
							h = 1
						};
						list.Add(item);
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
					list = InventoryManager.smethod_5(list);
					foreach (JsonItem jsonItem in list)
					{
						((byte*)ptr)[23] = ((!this.method_5(jsonItem.x, jsonItem.y)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 23) != 0)
						{
							((byte*)ptr)[20] = 0;
							break;
						}
						this.mainForm_0.method_123(1);
					}
					((byte*)ptr)[24] = ((*(sbyte*)((byte*)ptr + 20) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 24) != 0)
					{
						break;
					}
					UI.smethod_35(this.jsonTab_0.i, false, 1);
					this.int_0 = this.jsonTab_0.i;
					Win32.smethod_19();
					Win32.smethod_17();
					using (List<JsonItem>.Enumerator enumerator2 = list.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							JsonItem jsonItem2 = enumerator2.Current;
							UI.smethod_32(jsonItem2.x, jsonItem2.y, Enum2.const_19, true);
							Win32.smethod_2(true);
						}
						goto IL_36D;
					}
					goto IL_2D9;
					IL_36D:
					((byte*)ptr)[28] = ((UI.smethod_83(12) > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 28) == 0)
					{
						Win32.smethod_20();
						Win32.smethod_18();
						((byte*)ptr)[29] = (byte)(((*(int*)((byte*)ptr + 4) > 0) ? 1 : 0) & *(sbyte*)((byte*)ptr + 20));
						if (*(sbyte*)((byte*)ptr + 29) != 0)
						{
							continue;
						}
						break;
					}
					IL_2D9:
					int[,] array = UI.smethod_84();
					*(int*)((byte*)ptr + 12) = 0;
					for (;;)
					{
						((byte*)ptr)[27] = ((*(int*)((byte*)ptr + 12) < 12) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 27) == 0)
						{
							goto IL_36D;
						}
						*(int*)((byte*)ptr + 16) = 0;
						for (;;)
						{
							((byte*)ptr)[26] = ((*(int*)((byte*)ptr + 16) < 5) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 26) == 0)
							{
								break;
							}
							((byte*)ptr)[25] = ((array[*(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 16)] != 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 25) != 0)
							{
								UI.smethod_32(*(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 16), Enum2.const_19, true);
								Win32.smethod_2(true);
							}
							*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + 1;
						}
						*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
					}
				}
				((byte*)ptr)[30] = (byte)(*(sbyte*)((byte*)ptr + 20));
				if (*(sbyte*)((byte*)ptr + 30) != 0)
				{
					Class181.smethod_2(Enum11.const_0, MapCrafter.getString_0(107232188), new object[]
					{
						*(int*)ptr
					});
				}
				this.mainForm_0.method_121();
				Class181.smethod_3(Enum11.const_0, this.materials_0.ToString());
			}
		}

		private unsafe int method_4()
		{
			void* ptr = stackalloc byte[18];
			IEnumerable<JsonItem> enumerable = this.list_0.OrderBy(new Func<JsonItem, int>(MapCrafter.<>c.<>9.method_0)).ThenBy(new Func<JsonItem, int>(MapCrafter.<>c.<>9.method_1)).Take(60);
			((byte*)ptr)[12] = ((!enumerable.Any<JsonItem>()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				*(int*)((byte*)ptr + 4) = 0;
			}
			else
			{
				((byte*)ptr)[13] = ((this.int_0 != this.jsonTab_0.i) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					UI.smethod_35(this.jsonTab_0.i, false, 1);
					this.int_0 = this.jsonTab_0.i;
				}
				foreach (JsonItem jsonItem in enumerable)
				{
					UI.smethod_34(this.jsonTab_0.type, jsonItem.x, jsonItem.y, Enum2.const_19, false);
					Win32.smethod_9();
				}
				Thread.Sleep(400);
				for (;;)
				{
					((byte*)ptr)[17] = ((UI.smethod_83(12) < enumerable.Count<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) == 0)
					{
						break;
					}
					List<JsonItem> list = new List<JsonItem>();
					Enum10 enum10_ = UI.smethod_64(this.jsonTab_0.type);
					Bitmap gameImage = UI.GameImage;
					Bitmap item = Class308.smethod_0(Images.EmptyCellInner).Item1;
					Bitmap bitmap_ = Util.smethod_1(item, item.Width / 2, item.Height / 2);
					*(int*)((byte*)ptr + 8) = (int)UI.smethod_48(enum10_);
					foreach (JsonItem jsonItem2 in enumerable)
					{
						Position position = UI.smethod_47(enum10_, jsonItem2.x, jsonItem2.y);
						using (Bitmap bitmap = Class197.smethod_0(gameImage, position.Left, position.Top, *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 8), MapCrafter.getString_0(107399446)))
						{
							((byte*)ptr)[14] = ((this.jsonTab_0.type == MapCrafter.getString_0(107384297)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								((byte*)ptr)[15] = ((!UI.smethod_7(bitmap, bitmap_, 0.6)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 15) != 0)
								{
									list.Add(jsonItem2);
								}
							}
							else
							{
								((byte*)ptr)[16] = ((!UI.smethod_9(bitmap, Images.EmptyCellInner)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 16) != 0)
								{
									list.Add(jsonItem2);
								}
							}
						}
					}
					foreach (JsonItem jsonItem3 in list)
					{
						UI.smethod_34(this.jsonTab_0.type, jsonItem3.x, jsonItem3.y, Enum2.const_19, false);
						Win32.smethod_9();
					}
				}
				*(int*)ptr = enumerable.Count<JsonItem>();
				foreach (JsonItem item2 in enumerable)
				{
					this.list_0.Remove(item2);
				}
				*(int*)((byte*)ptr + 4) = *(int*)ptr;
			}
			return *(int*)((byte*)ptr + 4);
		}

		private JsonTab CurrencyTab
		{
			get
			{
				return Stashes.Tabs.FirstOrDefault(new Func<JsonTab, bool>(MapCrafter.<>c.<>9.method_2));
			}
		}

		private IEnumerable<Mod> PreventedMods
		{
			get
			{
				return this.mainForm_0.fastObjectListView_14.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Mod>(MapCrafter.<>c.<>9.method_3));
			}
		}

		private IEnumerable<Mod> ForcedMods
		{
			get
			{
				return this.mainForm_0.fastObjectListView_13.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Mod>(MapCrafter.<>c.<>9.method_4));
			}
		}

		private bool UsingChaos
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.MapCraftChoice) == MapCrafter.getString_0(107403998);
			}
		}

		private bool UsingAlchemy
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.MapCraftChoice) == MapCrafter.getString_0(107404040);
			}
		}

		private bool CorruptMap
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.MapCorrupt);
			}
		}

		private bool PackSizeAsMod
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.MapPacksizeForced);
			}
		}

		private int ForcedModCount
		{
			get
			{
				return Math.Min(this.ForcedMods.Count<Mod>(), int.Parse(Class255.class105_0.method_3(ConfigOptions.MapForcedCount).Replace(MapCrafter.getString_0(107231603), MapCrafter.getString_0(107399446))));
			}
		}

		private int MinQuality
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.MapMinQuality);
			}
		}

		private int MinQuantity
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.MapMinQuantity);
			}
		}

		private int MinPackSize
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.MapMinPacksize);
			}
		}

		private int ScourLimit
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.MapScourLimit);
			}
		}

		private int AlchLimit
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.MapAlchLimit);
			}
		}

		private int ChaosLimit
		{
			get
			{
				return (int)Class255.class105_0.method_6(ConfigOptions.MapChaosLimit);
			}
		}

		private unsafe bool method_5(int int_1, int int_2)
		{
			void* ptr = stackalloc byte[34];
			UI.smethod_32(int_1, int_2, Enum2.const_19, true);
			((byte*)ptr)[8] = ((!this.method_10(false, int_1, int_2, 0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				((byte*)ptr)[9] = 0;
			}
			else
			{
				((byte*)ptr)[10] = ((!this.item_0.identified) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					((byte*)ptr)[11] = ((!this.method_6(this.dictionary_0, int_1, int_2, false)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						this.method_12();
						((byte*)ptr)[9] = 0;
						goto IL_408;
					}
				}
				((byte*)ptr)[12] = (this.item_0.isMagic ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					((byte*)ptr)[13] = ((!this.method_6(this.dictionary_3, int_1, int_2, false)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						this.method_12();
						((byte*)ptr)[9] = 0;
						goto IL_408;
					}
				}
				if (this.item_0.quality < this.MinQuality && this.item_0.IsMap)
				{
					((byte*)ptr)[14] = ((!this.item_0.isNormal) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						((byte*)ptr)[15] = ((!this.method_6(this.dictionary_3, int_1, int_2, false)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 15) != 0)
						{
							this.method_12();
							((byte*)ptr)[9] = 0;
							goto IL_408;
						}
					}
					*(int*)ptr = (int)Math.Ceiling((double)(this.MinQuality - this.item_0.quality) / 5.0);
					Win32.smethod_19();
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[17] = ((*(int*)((byte*)ptr + 4) < *(int*)ptr) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) == 0)
						{
							break;
						}
						((byte*)ptr)[16] = ((!this.method_6(this.dictionary_1, int_1, int_2, *(int*)((byte*)ptr + 4) != 0)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) != 0)
						{
							goto IL_1AB;
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					Win32.smethod_20();
					goto IL_1BC;
					IL_1AB:
					this.method_12();
					((byte*)ptr)[9] = 0;
					goto IL_408;
				}
				IL_1BC:
				((byte*)ptr)[18] = ((!this.item_0.isRare) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					((byte*)ptr)[19] = (this.UsingAlchemy ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						((byte*)ptr)[20] = ((!this.method_6(this.dictionary_2, int_1, int_2, false)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 20) != 0)
						{
							this.method_12();
							((byte*)ptr)[9] = 0;
							goto IL_408;
						}
					}
					else
					{
						((byte*)ptr)[21] = ((!this.method_6(this.dictionary_6, int_1, int_2, false)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 21) != 0)
						{
							this.method_12();
							((byte*)ptr)[9] = 0;
							goto IL_408;
						}
					}
				}
				for (;;)
				{
					((byte*)ptr)[31] = ((!this.method_8(this.item_0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 31) == 0)
					{
						goto Block_22;
					}
					((byte*)ptr)[22] = (this.UsingChaos ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 22) != 0)
					{
						Win32.smethod_19();
						((byte*)ptr)[23] = ((!this.method_6(this.dictionary_4, int_1, int_2, false)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 23) != 0)
						{
							break;
						}
						for (;;)
						{
							((byte*)ptr)[25] = ((!this.method_8(this.item_0)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 25) == 0)
							{
								break;
							}
							((byte*)ptr)[24] = ((!this.method_6(this.dictionary_4, int_1, int_2, true)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 24) != 0)
							{
								goto IL_3D2;
							}
						}
						Win32.smethod_20();
					}
					else
					{
						((byte*)ptr)[26] = ((!this.item_0.isNormal) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 26) != 0)
						{
							((byte*)ptr)[27] = ((!this.method_6(this.dictionary_3, int_1, int_2, false)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 27) != 0)
							{
								goto IL_3E0;
							}
						}
						((byte*)ptr)[28] = (this.UsingAlchemy ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 28) != 0)
						{
							((byte*)ptr)[29] = ((!this.method_6(this.dictionary_2, int_1, int_2, false)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 29) != 0)
							{
								goto Block_21;
							}
						}
						else
						{
							((byte*)ptr)[30] = ((!this.method_6(this.dictionary_6, int_1, int_2, false)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 30) != 0)
							{
								goto IL_3FC;
							}
						}
					}
				}
				this.method_12();
				((byte*)ptr)[9] = 0;
				goto IL_408;
				Block_21:
				this.method_12();
				((byte*)ptr)[9] = 0;
				goto IL_408;
				Block_22:
				((byte*)ptr)[32] = (this.CorruptMap ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 32) != 0)
				{
					((byte*)ptr)[33] = ((!this.method_6(this.dictionary_5, int_1, int_2, false)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 33) != 0)
					{
						this.method_12();
						((byte*)ptr)[9] = 0;
						goto IL_408;
					}
				}
				((byte*)ptr)[9] = 1;
				goto IL_408;
				IL_3D2:
				this.method_12();
				((byte*)ptr)[9] = 0;
				goto IL_408;
				IL_3E0:
				this.method_12();
				((byte*)ptr)[9] = 0;
				goto IL_408;
				IL_3FC:
				this.method_12();
				((byte*)ptr)[9] = 0;
			}
			IL_408:
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		private unsafe bool method_6(Dictionary<JsonTab, List<JsonItem>> dictionary_7, int int_1, int int_2, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[22];
			*(byte*)ptr = ((dictionary_7.smethod_16(false) <= 0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				JsonTab key = dictionary_7.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = dictionary_7.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				((byte*)ptr)[2] = ((this.int_0 != key.i) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					UI.smethod_35(key.i, false, 1);
					this.int_0 = key.i;
				}
				((byte*)ptr)[3] = ((!bool_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					UI.smethod_34(key.type, jsonItem.x, jsonItem.y, Enum2.const_19, false);
					Win32.smethod_3();
				}
				UI.smethod_32(int_1, int_2, Enum2.const_19, true);
				Win32.smethod_2(true);
				Thread.Sleep(50);
				((byte*)ptr)[4] = ((!this.method_10(true, int_1, int_2, 0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					((byte*)ptr)[1] = (this.method_6(dictionary_7, int_1, int_2, bool_0) ? 1 : 0);
				}
				else
				{
					((byte*)ptr)[5] = ((jsonItem.stack == 1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						dictionary_7[key].Remove(jsonItem);
					}
					else
					{
						JsonItem jsonItem2 = jsonItem;
						int stack = jsonItem2.stack;
						jsonItem2.stack = stack - 1;
					}
					((byte*)ptr)[6] = ((!dictionary_7[key].Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						dictionary_7.Remove(key);
					}
					((byte*)ptr)[7] = ((dictionary_7 == this.dictionary_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						this.materials_0.method_0(MapCrafter.getString_0(107408259));
					}
					else
					{
						((byte*)ptr)[8] = ((dictionary_7 == this.dictionary_1) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							this.materials_0.method_0(MapCrafter.getString_0(107385955));
						}
						else
						{
							((byte*)ptr)[9] = ((dictionary_7 == this.dictionary_2) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								this.materials_0.method_0(MapCrafter.getString_0(107385772));
							}
							else
							{
								((byte*)ptr)[10] = ((dictionary_7 == this.dictionary_3) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									this.materials_0.method_0(MapCrafter.getString_0(107385158));
								}
								else
								{
									((byte*)ptr)[11] = ((dictionary_7 == this.dictionary_4) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 11) != 0)
									{
										this.materials_0.method_0(MapCrafter.getString_0(107394093));
									}
									else
									{
										((byte*)ptr)[12] = ((dictionary_7 == this.dictionary_5) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 12) != 0)
										{
											this.materials_0.method_0(MapCrafter.getString_0(107385012));
										}
										else
										{
											((byte*)ptr)[13] = ((dictionary_7 == this.dictionary_6) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 13) != 0)
											{
												this.materials_0.method_0(MapCrafter.getString_0(107363746));
											}
										}
									}
								}
							}
						}
					}
					((byte*)ptr)[14] = ((this.ScourLimit > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						((byte*)ptr)[15] = ((this.materials_0.Scours >= this.ScourLimit) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 15) != 0)
						{
							((byte*)ptr)[1] = 0;
							goto IL_39C;
						}
					}
					((byte*)ptr)[16] = ((this.AlchLimit > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						((byte*)ptr)[17] = (this.UsingAlchemy ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							((byte*)ptr)[18] = ((this.materials_0.Alchs >= this.AlchLimit) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 18) != 0)
							{
								((byte*)ptr)[1] = 0;
								goto IL_39C;
							}
						}
						else
						{
							((byte*)ptr)[19] = ((this.materials_0.Bindings >= this.AlchLimit) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) != 0)
							{
								((byte*)ptr)[1] = 0;
								goto IL_39C;
							}
						}
					}
					((byte*)ptr)[20] = ((this.ChaosLimit > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) != 0)
					{
						((byte*)ptr)[21] = ((this.materials_0.Chaos >= this.ChaosLimit) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 21) != 0)
						{
							((byte*)ptr)[1] = 0;
							goto IL_39C;
						}
					}
					((byte*)ptr)[1] = 1;
				}
			}
			IL_39C:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private bool method_7(JsonItem jsonItem_0)
		{
			return this.method_8(new Item(jsonItem_0.method_3()));
		}

		public unsafe bool method_8(Item item_1)
		{
			void* ptr = stackalloc byte[2];
			if (!item_1.isRare || !item_1.identified)
			{
				*(byte*)ptr = 0;
			}
			else if (item_1.quality < this.MinQuality && item_1.IsMap)
			{
				*(byte*)ptr = 0;
			}
			else
			{
				((byte*)ptr)[1] = ((item_1.ItemQuantity < this.MinQuantity) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					*(byte*)ptr = 0;
				}
				else if (item_1.PackSize < this.MinPackSize && !this.PackSizeAsMod)
				{
					*(byte*)ptr = 0;
				}
				else
				{
					*(byte*)ptr = (this.method_9(item_1.text) ? 1 : 0);
				}
			}
			return *(sbyte*)ptr != 0;
		}

		private unsafe bool method_9(string string_1)
		{
			void* ptr = stackalloc byte[9];
			foreach (Mod mod_ in this.PreventedMods)
			{
				((byte*)ptr)[4] = (ModUtilities.smethod_5(mod_, string_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					((byte*)ptr)[5] = 0;
					goto IL_FF;
				}
			}
			*(int*)ptr = 0;
			if (this.ForcedMods.Count<Mod>() > 0 || this.PackSizeAsMod)
			{
				foreach (Mod mod_2 in this.ForcedMods)
				{
					((byte*)ptr)[6] = (ModUtilities.smethod_5(mod_2, string_1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						*(int*)ptr = *(int*)ptr + 1;
					}
				}
				((byte*)ptr)[7] = (this.PackSizeAsMod ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					Item item = new Item(string_1);
					((byte*)ptr)[8] = ((item.PackSize >= this.MinPackSize) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						*(int*)ptr = *(int*)ptr + 1;
					}
				}
				((byte*)ptr)[5] = ((*(int*)ptr >= this.ForcedModCount) ? 1 : 0);
			}
			else
			{
				((byte*)ptr)[5] = 1;
			}
			IL_FF:
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		private unsafe bool method_10(bool bool_0, int int_1, int int_2, int int_3 = 0)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((int_3 >= 3) ? 1 : 0);
			bool result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, MapCrafter.getString_0(107233469));
				result = false;
			}
			else
			{
				Thread.Sleep(50);
				string text = Win32.smethod_21();
				if (string.IsNullOrEmpty(text) || text == MapCrafter.getString_0(107384072))
				{
					UI.smethod_1();
					UI.smethod_32(int_1, int_2, Enum2.const_19, true);
					result = this.method_10(bool_0, int_1, int_2, ++int_3);
				}
				else
				{
					((byte*)ptr)[1] = ((text == this.string_0 && bool_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Thread.Sleep(50);
						this.method_10(bool_0, int_1, int_2, int_3);
					}
					this.item_0 = new Item(text);
					if (this.string_0 != text && !this.item_0.isNormal && this.item_0.identified)
					{
						Class181.smethod_2(Enum11.const_0, MapCrafter.getString_0(107233986), new object[]
						{
							this.method_11()
						});
					}
					this.string_0 = text;
					result = true;
				}
			}
			return result;
		}

		private string method_11()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this.item_0.name);
			stringBuilder.AppendLine(this.item_0.typeLine);
			stringBuilder.AppendLine(MapCrafter.getString_0(107442788));
			stringBuilder.AppendLine(string.Format(MapCrafter.getString_0(107231594), this.item_0.quality));
			stringBuilder.AppendLine(string.Format(MapCrafter.getString_0(107231573), this.item_0.ItemQuantity));
			stringBuilder.AppendLine(string.Format(MapCrafter.getString_0(107231544), this.item_0.PackSize));
			stringBuilder.AppendLine(MapCrafter.getString_0(107442788));
			if (!this.item_0.isNormal && this.item_0.identified)
			{
				stringBuilder.AppendLine(this.item_0.ExplicitMods);
				stringBuilder.Append(MapCrafter.getString_0(107442788));
			}
			return stringBuilder.ToString();
		}

		private void method_12()
		{
			Class181.smethod_3(Enum11.const_0, MapCrafter.getString_0(107231511));
		}

		private void method_13()
		{
			Enum11 enum11_ = Enum11.const_3;
			string text = MapCrafter.getString_0(107231422);
			object[] array = new object[1];
			array[0] = string.Join(Environment.NewLine, this.PreventedMods.Select(new Func<Mod, string>(MapCrafter.<>c.<>9.method_5)));
			Class181.smethod_2(enum11_, text, array);
			Enum11 enum11_2 = Enum11.const_3;
			string text2 = MapCrafter.getString_0(107231393);
			object[] array2 = new object[1];
			array2[0] = string.Join(Environment.NewLine, this.ForcedMods.Select(new Func<Mod, string>(MapCrafter.<>c.<>9.method_6)));
			Class181.smethod_2(enum11_2, text2, array2);
			Class181.smethod_2(Enum11.const_3, MapCrafter.getString_0(107231880), new object[]
			{
				this.UsingChaos,
				this.CorruptMap,
				this.MinQuality,
				this.MinQuantity,
				this.MinPackSize,
				this.ScourLimit,
				this.AlchLimit,
				this.ChaosLimit,
				this.ForcedModCount
			});
		}

		[DebuggerStepThrough]
		[CompilerGenerated]
		private void method_14()
		{
			MapCrafter.Class390 @class = new MapCrafter.Class390();
			@class.mapCrafter_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MapCrafter.Class390>(ref @class);
		}

		[CompilerGenerated]
		private void method_15()
		{
			this.mainForm_0.thread_5 = Thread.CurrentThread;
			this.mainForm_0.method_116();
			UI.smethod_1();
			if (!this.method_1())
			{
				this.method_2();
			}
			else
			{
				this.method_3();
				this.method_2();
			}
		}

		[CompilerGenerated]
		private bool method_16(JsonItem jsonItem_0)
		{
			return (jsonItem_0.IsMap || jsonItem_0.IsMavenInvitation) && !jsonItem_0.corrupted && !jsonItem_0.IsFractured && !jsonItem_0.IsUnique && !this.method_7(jsonItem_0);
		}

		static MapCrafter()
		{
			Strings.CreateGetStringDelegate(typeof(MapCrafter));
		}

		private MainForm mainForm_0;

		private JsonTab jsonTab_0;

		private Materials materials_0;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_1;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_2;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_3;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_4;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_5;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_6;

		private List<JsonItem> list_0;

		private int int_0 = -1;

		private string string_0;

		private Item item_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
