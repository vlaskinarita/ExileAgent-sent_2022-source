using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using ns0;
using ns14;
using ns2;
using ns29;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features.Crafting
{
	public sealed class VaalCrafter
	{
		private string QualityCurrency
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.VaalCraftCurrency);
			}
		}

		private bool UsingQualityCurrency
		{
			get
			{
				return this.QualityCurrency != VaalCrafter.getString_0(107399525);
			}
		}

		public VaalCrafter(MainForm form, JsonTab tab)
		{
			this.mainForm_0 = form;
			this.jsonTab_0 = tab;
			this.materials_0 = new Materials();
			this.list_0 = new List<JsonItem>();
		}

		[DebuggerStepThrough]
		public void method_0()
		{
			VaalCrafter.Class387 @class = new VaalCrafter.Class387();
			@class.vaalCrafter_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<VaalCrafter.Class387>(ref @class);
		}

		private unsafe bool method_1()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, VaalCrafter.getString_0(107273568));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, VaalCrafter.getString_0(107273430));
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[3] = ((this.jsonTab_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						Class181.smethod_3(Enum11.const_2, VaalCrafter.getString_0(107232721));
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
			void* ptr = stackalloc byte[28];
			Class181.smethod_3(Enum11.const_0, VaalCrafter.getString_0(107232664));
			this.list_0 = Stashes.Items[this.jsonTab_0.i].Where(new Func<JsonItem, bool>(this.method_13)).OrderBy(new Func<JsonItem, int>(VaalCrafter.<>c.<>9.method_0)).ThenBy(new Func<JsonItem, int>(VaalCrafter.<>c.<>9.method_1)).ToList<JsonItem>();
			*(int*)ptr = this.list_0.Count;
			((byte*)ptr)[16] = 1;
			*(int*)((byte*)ptr + 4) = 0;
			((byte*)ptr)[17] = ((*(int*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 17) != 0)
			{
				Class181.smethod_2(Enum11.const_2, VaalCrafter.getString_0(107232635), new object[]
				{
					this.jsonTab_0.n
				});
				this.method_2();
			}
			else
			{
				((byte*)ptr)[18] = (this.UsingQualityCurrency ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					this.dictionary_0 = StashManager.smethod_1(this.QualityCurrency, true);
				}
				this.dictionary_1 = StashManager.smethod_1(VaalCrafter.getString_0(107384985), true);
				Class181.smethod_2(Enum11.const_0, VaalCrafter.getString_0(107232098), new object[]
				{
					*(int*)ptr
				});
				this.mainForm_0.method_122(*(int*)ptr);
				for (;;)
				{
					List<JsonItem> list = this.method_4();
					*(int*)((byte*)ptr + 4) = list.Count;
					((byte*)ptr)[19] = ((*(int*)((byte*)ptr + 4) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						break;
					}
					foreach (JsonItem jsonItem in list)
					{
						((byte*)ptr)[20] = ((!this.method_5(jsonItem.x, jsonItem.y)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 20) != 0)
						{
							((byte*)ptr)[16] = 0;
							break;
						}
						this.mainForm_0.method_123(1);
					}
					((byte*)ptr)[21] = ((*(sbyte*)((byte*)ptr + 16) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 21) != 0)
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
						goto IL_2E2;
					}
					goto IL_258;
					IL_2E2:
					((byte*)ptr)[25] = ((UI.smethod_83(12) > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 25) == 0)
					{
						Win32.smethod_20();
						Win32.smethod_18();
						((byte*)ptr)[26] = (byte)(((*(int*)((byte*)ptr + 4) > 0) ? 1 : 0) & *(sbyte*)((byte*)ptr + 16));
						if (*(sbyte*)((byte*)ptr + 26) != 0)
						{
							continue;
						}
						break;
					}
					IL_258:
					int[,] array = UI.smethod_84();
					*(int*)((byte*)ptr + 8) = 0;
					for (;;)
					{
						((byte*)ptr)[24] = ((*(int*)((byte*)ptr + 8) < 12) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 24) == 0)
						{
							goto IL_2E2;
						}
						*(int*)((byte*)ptr + 12) = 0;
						for (;;)
						{
							((byte*)ptr)[23] = ((*(int*)((byte*)ptr + 12) < 5) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 23) == 0)
							{
								break;
							}
							((byte*)ptr)[22] = ((array[*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)] != 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 22) != 0)
							{
								UI.smethod_32(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12), Enum2.const_3, true);
								Win32.smethod_2(true);
							}
							*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
						}
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
				}
				((byte*)ptr)[27] = (byte)(*(sbyte*)((byte*)ptr + 16));
				if (*(sbyte*)((byte*)ptr + 27) != 0)
				{
					Class181.smethod_2(Enum11.const_0, VaalCrafter.getString_0(107232021), new object[]
					{
						*(int*)ptr
					});
				}
				this.mainForm_0.method_121();
				Class181.smethod_3(Enum11.const_0, this.materials_0.ToString());
			}
		}

		private unsafe List<JsonItem> method_4()
		{
			void* ptr = stackalloc byte[2];
			List<JsonItem> list = new List<JsonItem>();
			*(byte*)ptr = ((this.int_0 != this.jsonTab_0.i) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				UI.smethod_35(this.jsonTab_0.i, false, 1);
				this.int_0 = this.jsonTab_0.i;
			}
			foreach (JsonItem jsonItem in this.list_0.ToList<JsonItem>())
			{
				List<Position> source = InventoryManager.smethod_8(list, jsonItem);
				((byte*)ptr)[1] = (source.Any<Position>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					break;
				}
				Position position = source.First<Position>();
				JsonItem jsonItem2 = jsonItem.method_2();
				jsonItem2.x = position.x;
				jsonItem2.y = position.y;
				list.Add(jsonItem2);
				this.list_0.Remove(jsonItem);
				UI.smethod_34(this.jsonTab_0.type, jsonItem.x, jsonItem.y, Enum2.const_19, false);
				Win32.smethod_9();
			}
			return list;
		}

		private unsafe bool method_5(int int_1, int int_2)
		{
			void* ptr = stackalloc byte[13];
			UI.smethod_32(int_1, int_2, Enum2.const_19, true);
			((byte*)ptr)[8] = ((!this.method_7(false, int_1, int_2, 0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				((byte*)ptr)[9] = 0;
			}
			else
			{
				if (this.UsingQualityCurrency && !this.method_9(this.item_0))
				{
					Win32.smethod_19();
					*(int*)ptr = 20 - this.method_10(this.item_0);
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[11] = ((*(int*)((byte*)ptr + 4) < *(int*)ptr) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) == 0)
						{
							break;
						}
						((byte*)ptr)[10] = ((!this.method_6(this.dictionary_0, int_1, int_2, *(int*)((byte*)ptr + 4) != 0)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							goto IL_B8;
						}
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					Win32.smethod_20();
					goto IL_E4;
					IL_B8:
					Class181.smethod_2(Enum11.const_2, VaalCrafter.getString_0(107232012), new object[]
					{
						this.QualityCurrency
					});
					((byte*)ptr)[9] = 0;
					goto IL_125;
				}
				IL_E4:
				((byte*)ptr)[12] = ((!this.method_6(this.dictionary_1, int_1, int_2, false)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					Class181.smethod_3(Enum11.const_2, VaalCrafter.getString_0(107231947));
					((byte*)ptr)[9] = 0;
				}
				else
				{
					((byte*)ptr)[9] = 1;
				}
			}
			IL_125:
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		private unsafe bool method_6(Dictionary<JsonTab, List<JsonItem>> dictionary_2, int int_1, int int_2, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[8];
			*(byte*)ptr = ((dictionary_2.smethod_16(false) <= 0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				JsonTab key = dictionary_2.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = dictionary_2.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
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
				((byte*)ptr)[4] = ((!this.method_7(true, int_1, int_2, 0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					((byte*)ptr)[1] = (this.method_6(dictionary_2, int_1, int_2, bool_0) ? 1 : 0);
				}
				else
				{
					((byte*)ptr)[5] = ((jsonItem.stack == 1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						dictionary_2[key].Remove(jsonItem);
					}
					else
					{
						JsonItem jsonItem2 = jsonItem;
						int num = jsonItem2.stack;
						jsonItem2.stack = num - 1;
					}
					((byte*)ptr)[6] = ((!dictionary_2[key].Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						dictionary_2.Remove(key);
					}
					((byte*)ptr)[7] = ((dictionary_2 == this.dictionary_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						this.materials_0.method_0(this.QualityCurrency);
					}
					else
					{
						Materials materials = this.materials_0;
						int num = materials.Vaals;
						materials.Vaals = num + 1;
					}
					((byte*)ptr)[1] = 1;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_7(bool bool_0, int int_1, int int_2, int int_3 = 0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((int_3 >= 3) ? 1 : 0);
			bool result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, VaalCrafter.getString_0(107233442));
				result = false;
			}
			else
			{
				Thread.Sleep(50);
				string text = Win32.smethod_21();
				if (string.IsNullOrEmpty(text) || text == VaalCrafter.getString_0(107384045))
				{
					UI.smethod_1();
					UI.smethod_32(int_1, int_2, Enum2.const_19, true);
					result = this.method_7(bool_0, int_1, int_2, ++int_3);
				}
				else
				{
					((byte*)ptr)[1] = ((text == this.string_0 && bool_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Thread.Sleep(50);
						this.method_7(bool_0, int_1, int_2, int_3);
					}
					this.item_0 = new Item(text);
					((byte*)ptr)[2] = ((this.string_0 != text) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_2(Enum11.const_0, VaalCrafter.getString_0(107233959), new object[]
						{
							this.item_0.text
						});
					}
					this.string_0 = text;
					result = true;
				}
			}
			return result;
		}

		private unsafe bool method_8(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[8];
			*(byte*)ptr = (jsonItem_0.corrupted ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = (jsonItem_0.IsFlask ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[3] = (this.UsingQualityCurrency ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						string qualityCurrency = this.QualityCurrency;
						string text = qualityCurrency;
						if (text != null)
						{
							if (!(text == VaalCrafter.getString_0(107408931)))
							{
								if (!(text == VaalCrafter.getString_0(107408906)))
								{
									if (text == VaalCrafter.getString_0(107385823))
									{
										((byte*)ptr)[6] = ((!jsonItem_0.IsGem) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 6) != 0)
										{
											((byte*)ptr)[1] = 0;
											goto IL_109;
										}
										goto IL_104;
									}
								}
								else
								{
									((byte*)ptr)[5] = ((!jsonItem_0.IsWeapon) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 5) != 0)
									{
										((byte*)ptr)[1] = 0;
										goto IL_109;
									}
									goto IL_104;
								}
							}
							else
							{
								((byte*)ptr)[4] = ((!jsonItem_0.IsArmor) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 4) != 0)
								{
									((byte*)ptr)[1] = 0;
									goto IL_109;
								}
								goto IL_104;
							}
						}
						((byte*)ptr)[7] = ((!jsonItem_0.IsJewelry) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							((byte*)ptr)[1] = 0;
							goto IL_109;
						}
					}
					IL_104:
					((byte*)ptr)[1] = 1;
				}
			}
			IL_109:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private bool method_9(Item item_1)
		{
			return this.method_10(item_1) >= 20;
		}

		private unsafe int method_10(Item item_1)
		{
			void* ptr = stackalloc byte[6];
			foreach (string text in item_1.Properties)
			{
				((byte*)ptr)[4] = (text.Contains(VaalCrafter.getString_0(107443303)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					Match match = Regex.Match(text, VaalCrafter.getString_0(107231874));
					((byte*)ptr)[5] = (match.Success ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						*(int*)ptr = int.Parse(match.Groups[1].Value);
						goto IL_97;
					}
				}
			}
			*(int*)ptr = 0;
			IL_97:
			return *(int*)ptr;
		}

		[DebuggerStepThrough]
		[CompilerGenerated]
		private void method_11()
		{
			VaalCrafter.Class388 @class = new VaalCrafter.Class388();
			@class.vaalCrafter_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<VaalCrafter.Class388>(ref @class);
		}

		[CompilerGenerated]
		private void method_12()
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
		private bool method_13(JsonItem jsonItem_0)
		{
			return this.method_8(jsonItem_0);
		}

		static VaalCrafter()
		{
			Strings.CreateGetStringDelegate(typeof(VaalCrafter));
		}

		private MainForm mainForm_0;

		private JsonTab jsonTab_0;

		private Materials materials_0;

		private int int_0 = -1;

		private string string_0;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_1;

		private List<JsonItem> list_0;

		private Item item_0;

		private const string string_1 = "\\+(\\d+)% \\(augmented\\)";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
