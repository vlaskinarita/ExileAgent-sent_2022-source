using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ns0;
using ns12;
using ns14;
using ns17;
using ns2;
using ns25;
using ns27;
using ns29;
using ns35;
using ns6;
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
	public abstract class Crafter
	{
		public Crafter(MainForm form, Class299 craftData)
		{
			this.mainForm_0 = form;
			this.class299_0 = craftData;
			this.materials_0 = new Materials();
			this.class299_0.OutOfMaterials = false;
		}

		protected abstract void vmethod_0();

		public void method_0()
		{
			try
			{
				Util.smethod_29(new ThreadStart(this.method_10)).Start();
			}
			catch (ThreadAbortException)
			{
				this.method_6();
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_2, Crafter.getString_0(107233540), new object[]
				{
					ex
				});
				this.method_6();
			}
		}

		private unsafe bool method_1()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107273528));
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = (UI.smethod_31(false, 1, 12, 5).Any<Item>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107273390));
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[1] = 1;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		protected void method_2()
		{
			UI.smethod_35(this.class299_0.Tab.i, false, 1);
			UI.smethod_34(this.class299_0.Tab.type, this.class299_0.InitialItem.x, this.class299_0.InitialItem.y, Enum2.const_19, false);
			Win32.smethod_9();
			Stashes.Items[this.class299_0.Tab.i].Remove(this.class299_0.InitialItem);
		}

		protected void method_3()
		{
			UI.smethod_35(this.CurrencyTab.i, false, 1);
			UI.smethod_32(0, 0, Enum2.const_3, true);
			Win32.smethod_9();
		}

		protected bool UsingCraftingSlot
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.UseCraftingSlot);
			}
		}

		protected bool ItemInCenterSlot
		{
			get
			{
				return this.CurrencyTab != null && this.class299_0.InitialItem.x == 28 && this.class299_0.InitialItem.StashId == this.CurrencyTab.i;
			}
		}

		protected bool CenterSlotOccupied
		{
			get
			{
				return this.CurrencyTab != null && StashManager.smethod_10(this.CurrencyTab.i, 28, 0) != null;
			}
		}

		private JsonTab CurrencyTab
		{
			get
			{
				return Stashes.Tabs.FirstOrDefault(new Func<JsonTab, bool>(Crafter.<>c.<>9.method_2));
			}
		}

		private bool CraftMoreItems
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.CraftMoreItems);
			}
		}

		private JsonTab CraftMoreItemsTab
		{
			get
			{
				return Stashes.Tabs.FirstOrDefault(new Func<JsonTab, bool>(Crafter.<>c.<>9.method_3));
			}
		}

		private unsafe Enum17 CraftingOption
		{
			get
			{
				void* ptr = stackalloc byte[2];
				*(byte*)ptr = ((this is Class384) ? 1 : 0);
				Enum17 result;
				if (*(sbyte*)ptr != 0)
				{
					result = ((Class255.class105_0.method_3(ConfigOptions.AltAndOr) == Crafter.getString_0(107365988)) ? Enum17.const_0 : Enum17.const_1);
				}
				else
				{
					((byte*)ptr)[1] = ((this is RareCrafter) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						result = ((Class255.class105_0.method_3(ConfigOptions.RareAndOr) == Crafter.getString_0(107365988)) ? Enum17.const_0 : Enum17.const_1);
					}
					else
					{
						result = Enum17.const_0;
					}
				}
				return result;
			}
		}

		protected unsafe Class300 method_4(string string_2)
		{
			void* ptr = stackalloc byte[16];
			Class300 @class = new Class300(this.class299_0, this.CraftingOption);
			((byte*)ptr)[8] = ((this.class299_0.Prefixes != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[11] = ((*(int*)ptr < this.class299_0.Prefixes.Count) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) == 0)
					{
						break;
					}
					Mod mod_ = this.class299_0.Prefixes[*(int*)ptr];
					Tier tier_ = this.class299_0.PrefixTiers[*(int*)ptr];
					((byte*)ptr)[9] = (ModUtilities.smethod_5(mod_, string_2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						((byte*)ptr)[10] = (ModUtilities.smethod_6(mod_, tier_, string_2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							Class300 class2 = @class;
							int num = class2.HitPrefixes;
							class2.HitPrefixes = num + 1;
						}
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
			((byte*)ptr)[12] = ((this.class299_0.Suffixes != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[15] = ((*(int*)((byte*)ptr + 4) < this.class299_0.Suffixes.Count) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) == 0)
					{
						break;
					}
					Mod mod_2 = this.class299_0.Suffixes[*(int*)((byte*)ptr + 4)];
					Tier tier_2 = this.class299_0.SuffixTiers[*(int*)((byte*)ptr + 4)];
					((byte*)ptr)[13] = (ModUtilities.smethod_5(mod_2, string_2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						((byte*)ptr)[14] = (ModUtilities.smethod_6(mod_2, tier_2, string_2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 14) != 0)
						{
							Class300 class3 = @class;
							int num = class3.HitSuffixes;
							class3.HitSuffixes = num + 1;
						}
					}
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
			}
			return @class;
		}

		private unsafe bool method_5(string string_2)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this is Class383) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Item item = new Item(string_2);
				((byte*)ptr)[1] = (item.isUnique ? 1 : 0);
			}
			else
			{
				((byte*)ptr)[1] = (this.method_4(string_2).Success ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private void method_6()
		{
			Win32.smethod_18();
			Win32.smethod_20();
			this.mainForm_0.method_12(true);
			this.mainForm_0.enum8_0 = Enum8.const_1;
			if (Class255.class105_0.method_4(ConfigOptions.StartAfterCrafting) && !this.mainForm_0.bool_12)
			{
				this.mainForm_0.method_58(false, MainForm.GEnum1.const_0, false);
			}
			else if (this.mainForm_0.thread_5 != null)
			{
				this.mainForm_0.thread_5.Abort();
				this.mainForm_0.thread_5 = null;
			}
		}

		protected unsafe bool method_7(string string_2, string string_3, bool bool_1 = false)
		{
			void* ptr = stackalloc byte[6];
			Dictionary<JsonTab, List<JsonItem>> source = StashManager.smethod_1(string_2, true);
			*(byte*)ptr = ((!source.Any<KeyValuePair<JsonTab, List<JsonItem>>>()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_2(Enum11.const_2, Crafter.getString_0(107233479), new object[]
				{
					string_2
				});
				this.class299_0.OutOfMaterials = true;
				((byte*)ptr)[1] = 0;
			}
			else
			{
				JsonTab key = source.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = source.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				Win32.smethod_20();
				((byte*)ptr)[2] = ((this.int_1 != key.i) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					UI.smethod_35(key.i, false, 1);
					this.int_1 = key.i;
				}
				UI.smethod_34(key.type, jsonItem.x, jsonItem.y, Enum2.const_19, false);
				Win32.smethod_3();
				this.vmethod_1();
				Win32.smethod_2(true);
				string text = Win32.smethod_21();
				DateTime t = DateTime.Now.AddSeconds(2.0);
				for (;;)
				{
					((byte*)ptr)[4] = ((text == this.class299_0.CraftedItem.text) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) == 0)
					{
						break;
					}
					text = Win32.smethod_21();
					((byte*)ptr)[3] = ((DateTime.Now > t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						goto IL_1D1;
					}
				}
				Item item = new Item(text);
				if ((item.rarity == string_3 && !bool_1) || (item.rarity != string_3 && bool_1))
				{
					((byte*)ptr)[5] = ((jsonItem.stack == 1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						Stashes.Items[key.i].Remove(jsonItem);
					}
					else
					{
						JsonItem jsonItem2 = jsonItem;
						int stack = jsonItem2.stack;
						jsonItem2.stack = stack - 1;
					}
					this.materials_0.method_0(string_2);
					((byte*)ptr)[1] = 1;
					goto IL_1DE;
				}
				((byte*)ptr)[1] = (this.method_7(string_2, string_3, bool_1) ? 1 : 0);
				goto IL_1DE;
				IL_1D1:
				((byte*)ptr)[1] = (this.method_7(string_2, string_3, bool_1) ? 1 : 0);
			}
			IL_1DE:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		protected virtual void vmethod_1()
		{
			if (this.UsingCraftingSlot)
			{
				UI.smethod_34(Crafter.getString_0(107396292), 28, 0, Enum2.const_19, false);
			}
			else
			{
				UI.smethod_32(0, 0, Enum2.const_3, true);
			}
		}

		protected unsafe bool method_8(bool bool_1, out bool bool_2, Func<bool> func_0, int int_2 = 0, bool bool_3 = false)
		{
			void* ptr = stackalloc byte[5];
			bool_2 = false;
			*(byte*)ptr = ((int_2 >= 3 && bool_3) ? 1 : 0);
			bool result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107233402));
				result = false;
			}
			else if (this.dateTime_0.AddSeconds(2.0) < DateTime.Now && int_2 > 0)
			{
				((byte*)ptr)[1] = ((func_0 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					result = false;
				}
				else
				{
					Class181.smethod_3(Enum11.const_0, Crafter.getString_0(107233809));
					this.bool_0 = true;
					result = func_0();
				}
			}
			else
			{
				Thread.Sleep(50);
				this.string_0 = Win32.smethod_21();
				((byte*)ptr)[2] = ((int_2 == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					this.dateTime_0 = DateTime.Now;
				}
				if (string.IsNullOrEmpty(this.string_0) || this.string_0 == Crafter.getString_0(107384005))
				{
					UI.smethod_1();
					this.vmethod_1();
					result = this.method_8(bool_1, out bool_2, func_0, ++int_2, true);
				}
				else
				{
					this.class299_0.CraftedItem = new Item(this.string_0);
					this.class300_0 = this.method_4(this.string_0);
					((byte*)ptr)[3] = ((this.string_0 == this.string_1 && bool_1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						Thread.Sleep(50);
						this.method_8(bool_1, out bool_2, func_0, ++int_2, false);
					}
					((byte*)ptr)[4] = ((this.string_1 != this.string_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						bool_2 = true;
					}
					this.string_1 = this.string_0;
					result = true;
				}
			}
			return result;
		}

		private unsafe bool method_9()
		{
			void* ptr = stackalloc byte[3];
			if (!this.CraftMoreItems || this.CraftMoreItemsTab == null)
			{
				*(byte*)ptr = 0;
			}
			else
			{
				((byte*)ptr)[1] = (Position.smethod_0(StashManager.smethod_7(this.CraftMoreItemsTab.i, this.class299_0.InitialItem.w, this.class299_0.InitialItem.h), null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_2(Enum11.const_2, Crafter.getString_0(107233740), new object[]
					{
						this.CraftMoreItemsTab.n
					});
					*(byte*)ptr = 0;
				}
				else
				{
					((byte*)ptr)[2] = (this.UsingCraftingSlot ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						UI.smethod_35(this.CurrencyTab.i, false, 1);
						this.vmethod_1();
						Thread.Sleep(500);
						Win32.smethod_9();
						Stashes.Items[this.CurrencyTab.i].RemoveAll(new Predicate<JsonItem>(Crafter.<>c.<>9.method_4));
					}
					UI.smethod_35(this.CraftMoreItemsTab.i, false, 1);
					UI.smethod_32(0, 0, Enum2.const_3, true);
					Thread.Sleep(500);
					Win32.smethod_9();
					*(byte*)ptr = 1;
				}
			}
			return *(sbyte*)ptr != 0;
		}

		[DebuggerStepThrough]
		[CompilerGenerated]
		private void method_10()
		{
			Crafter.Class386 @class = new Crafter.Class386();
			@class.crafter_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<Crafter.Class386>(ref @class);
		}

		[CompilerGenerated]
		private unsafe void method_11()
		{
			void* ptr = stackalloc byte[11];
			this.mainForm_0.thread_5 = Thread.CurrentThread;
			this.mainForm_0.method_116();
			UI.smethod_1();
			*(byte*)ptr = ((!this.method_1()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.method_6();
			}
			else
			{
				((byte*)ptr)[1] = ((!UI.smethod_13(1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.method_6();
				}
				else
				{
					JsonTab jsonTab = Stashes.smethod_11(this.class299_0.InitialItem.StashId);
					((byte*)ptr)[2] = ((jsonTab == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_2(Enum11.const_2, Crafter.getString_0(107233639), new object[]
						{
							this.class299_0.InitialItem.Name
						});
						this.method_6();
					}
					else
					{
						this.class299_0.Tab = jsonTab;
						((byte*)ptr)[3] = (this.UsingCraftingSlot ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							((byte*)ptr)[4] = ((this.CurrencyTab == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107233074));
								this.method_6();
								return;
							}
							if (this.CurrencyTab != null && Web.smethod_15(this.CurrencyTab))
							{
								Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107232993));
								this.method_6();
								return;
							}
							((byte*)ptr)[5] = (this.CenterSlotOccupied ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) != 0)
							{
								((byte*)ptr)[6] = ((!this.ItemInCenterSlot) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 6) != 0)
								{
									Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107232896));
									this.method_6();
									return;
								}
								UI.smethod_35(this.CurrencyTab.i, false, 1);
							}
							else
							{
								this.method_2();
								this.method_3();
								Win32.smethod_4(-2, -2, 50, 90, false);
							}
						}
						else
						{
							this.method_2();
							Win32.smethod_4(-2, -2, 50, 90, false);
						}
						this.vmethod_0();
						string str = Crafter.getString_0(107233335);
						((byte*)ptr)[7] = ((this.class299_0.CraftedItem != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							str += string.Format(Crafter.getString_0(107233266), this.class299_0.CraftedItem.Name, this.class299_0.CraftedItem.ExplicitMods);
						}
						Class307.smethod_0(ConfigOptions.OnCraftingItemComplete, Crafter.getString_0(107233237), str);
						if (this.CraftMoreItems && !this.class299_0.OutOfMaterials)
						{
							Crafter.Class385 @class = new Crafter.Class385();
							@class.crafter_0 = this;
							((byte*)ptr)[8] = ((this.CraftMoreItemsTab == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) != 0)
							{
								Class181.smethod_3(Enum11.const_2, Crafter.getString_0(107233244));
								this.method_6();
							}
							else
							{
								@class.string_0 = API.smethod_15(this.class299_0.InitialItem);
								IEnumerable<JsonItem> source = Stashes.Items[this.CraftMoreItemsTab.i].Where(new Func<JsonItem, bool>(@class.method_0));
								((byte*)ptr)[9] = ((!this.method_9()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 9) != 0)
								{
									this.method_6();
								}
								else
								{
									((byte*)ptr)[10] = ((source.Count<JsonItem>() == 0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 10) != 0)
									{
										Class181.smethod_2(Enum11.const_0, Crafter.getString_0(107233187), new object[]
										{
											@class.string_0
										});
										Class181.smethod_3(Enum11.const_0, Crafter.getString_0(107233237));
										this.method_6();
									}
									else
									{
										Class181.smethod_2(Enum11.const_0, Crafter.getString_0(107233150), new object[]
										{
											source.Count<JsonItem>(),
											@class.string_0
										});
										JsonItem initialItem = source.OrderBy(new Func<JsonItem, int>(Crafter.<>c.<>9.method_0)).ThenBy(new Func<JsonItem, int>(Crafter.<>c.<>9.method_1)).First<JsonItem>();
										this.class299_0.InitialItem = initialItem;
										this.materials_0 = new Materials();
										this.class299_0.OutOfMaterials = false;
										this.method_0();
									}
								}
							}
						}
						else
						{
							this.method_6();
						}
					}
				}
			}
		}

		static Crafter()
		{
			Strings.CreateGetStringDelegate(typeof(Crafter));
		}

		protected MainForm mainForm_0;

		protected Class299 class299_0;

		protected Materials materials_0;

		protected Class300 class300_0;

		protected const int int_0 = 28;

		private int int_1;

		private DateTime dateTime_0;

		protected string string_0;

		protected string string_1;

		protected bool bool_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class385
		{
			internal bool method_0(JsonItem jsonItem_0)
			{
				return jsonItem_0.typeLine.Contains(this.string_0) && Form5.smethod_0(jsonItem_0) && !this.crafter_0.method_5(jsonItem_0.method_3());
			}

			public string string_0;

			public Crafter crafter_0;
		}
	}
}
