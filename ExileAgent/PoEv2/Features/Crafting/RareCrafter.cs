using System;
using System.Collections.Generic;
using System.Linq;
using ns0;
using ns14;
using ns17;
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
	public sealed class RareCrafter : Crafter
	{
		private bool UsesFirstScourAlch
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.RareUseScourAlch);
			}
		}

		private bool UsesChaosOrb
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.RareCraftType) == RareCrafter.getString_1(107403946);
			}
		}

		private bool UsesScourAlch
		{
			get
			{
				return Class255.class105_0.method_3(ConfigOptions.RareCraftType) == RareCrafter.getString_1(107403988);
			}
		}

		public RareCrafter(MainForm form, Class299 craftData) : base(form, craftData)
		{
			this.dictionary_0 = new Dictionary<JsonTab, List<JsonItem>>();
		}

		protected unsafe override void vmethod_0()
		{
			void* ptr = stackalloc byte[14];
			Class181.smethod_2(Enum11.const_3, RareCrafter.getString_1(107234800), new object[]
			{
				this.class299_0
			});
			this.dictionary_0 = StashManager.smethod_1(RareCrafter.getString_1(107394041), true);
			((byte*)ptr)[1] = (this.UsesChaosOrb ? 1 : 0);
			decimal num;
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				num = Math.Min(Class255.class105_0.method_6(ConfigOptions.RareChaosLimit), this.dictionary_0.smethod_16(false));
			}
			else
			{
				num = Class255.class105_0.method_6(ConfigOptions.RareChaosLimit);
			}
			*(byte*)ptr = 0;
			Class181.smethod_2(Enum11.const_3, RareCrafter.getString_1(107234763), new object[]
			{
				this.UsesChaosOrb
			});
			Class181.smethod_2(Enum11.const_3, RareCrafter.getString_1(107234738), new object[]
			{
				num
			});
			Win32.smethod_4(-2, -2, 50, 90, false);
			this.vmethod_1();
			((byte*)ptr)[2] = ((!this.method_15(false, null)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) == 0)
			{
				for (;;)
				{
					((byte*)ptr)[12] = 1;
					((byte*)ptr)[3] = (this.class299_0.OutOfMaterials ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						goto Block_14;
					}
					((byte*)ptr)[4] = (this.UsesChaosOrb ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						if (num > 0m && this.materials_0.Chaos >= num)
						{
							break;
						}
					}
					else if (num > 0m && this.materials_0.Alchs >= num)
					{
						goto IL_294;
					}
					if (!this.UsesFirstScourAlch && !this.class299_0.CraftedItem.isRare)
					{
						goto IL_2A2;
					}
					((byte*)ptr)[5] = ((!this.class299_0.CraftedItem.isRare) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						((byte*)ptr)[6] = ((!this.class299_0.CraftedItem.isNormal) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							((byte*)ptr)[7] = ((!this.method_12()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								goto IL_2C1;
							}
						}
						((byte*)ptr)[8] = ((!this.method_13()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							goto IL_2C1;
						}
					}
					((byte*)ptr)[9] = (this.class300_0.Success ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						goto IL_2B9;
					}
					((byte*)ptr)[10] = (this.UsesChaosOrb ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						Win32.smethod_19();
						this.method_14();
					}
					else if (!this.method_12() || !this.method_13())
					{
						goto IL_2C1;
					}
					((byte*)ptr)[11] = (this.class300_0.Success ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						goto IL_2BE;
					}
				}
				this.class299_0.OutOfMaterials = true;
				Block_14:
				goto IL_2C1;
				IL_294:
				this.class299_0.OutOfMaterials = true;
				goto IL_2C1;
				IL_2A2:
				Class181.smethod_3(Enum11.const_2, RareCrafter.getString_1(107234697));
				return;
				IL_2B9:
				*(byte*)ptr = 1;
				goto IL_2C1;
				IL_2BE:
				*(byte*)ptr = 1;
				IL_2C1:
				Win32.smethod_20();
				((byte*)ptr)[13] = (byte)(*(sbyte*)ptr);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					Class181.smethod_3(Enum11.const_0, RareCrafter.getString_1(107234480));
				}
				else
				{
					Class181.smethod_3(Enum11.const_0, RareCrafter.getString_1(107234092));
				}
				Class181.smethod_3(Enum11.const_0, this.materials_0.ToString());
			}
		}

		private unsafe bool method_12()
		{
			void* ptr = stackalloc byte[2];
			string string_ = this.class299_0.CraftedItem.IsFractured ? RareCrafter.getString_1(107441766) : RareCrafter.getString_1(107399454);
			*(byte*)ptr = ((!base.method_7(RareCrafter.getString_1(107385106), string_, false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_15(true, new Func<bool>(this.method_12)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_13()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!base.method_7(RareCrafter.getString_1(107385720), RareCrafter.getString_1(107404796), false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_15(true, new Func<bool>(this.method_13)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_14()
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((this.dictionary_0.smethod_16(false) == 0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, RareCrafter.getString_1(107233967));
				((byte*)ptr)[1] = 0;
			}
			else if (this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.Sum(new Func<JsonItem, int>(RareCrafter.<>c.<>9.method_0)) <= 0)
			{
				this.dictionary_0.Remove(this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key);
				this.method_14();
				((byte*)ptr)[1] = 0;
			}
			else
			{
				JsonTab key = this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				((byte*)ptr)[2] = ((this.int_2 != key.i) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					UI.smethod_35(key.i, false, 1);
					UI.smethod_34(key.type, jsonItem.x, jsonItem.y, Enum2.const_19, false);
					Win32.smethod_3();
					this.int_2 = key.i;
				}
				this.vmethod_1();
				Win32.smethod_2(true);
				((byte*)ptr)[3] = ((jsonItem.stack == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					this.dictionary_0[key].Remove(jsonItem);
					((byte*)ptr)[4] = ((!this.dictionary_0[key].Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						this.dictionary_0.Remove(key);
					}
				}
				Materials materials_ = this.materials_0;
				int num = materials_.Chaos;
				materials_.Chaos = num + 1;
				JsonItem jsonItem2 = jsonItem;
				num = jsonItem2.stack;
				jsonItem2.stack = num - 1;
				((byte*)ptr)[1] = (this.method_15(true, new Func<bool>(this.method_14)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_15(bool bool_1, Func<bool> func_0)
		{
			void* ptr = stackalloc byte[3];
			((byte*)ptr)[1] = ((!base.method_8(bool_1, out *(bool*)ptr, func_0, 0, false)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				((byte*)ptr)[2] = 0;
			}
			else
			{
				if (*(sbyte*)ptr != 0 && !this.class299_0.CraftedItem.isNormal)
				{
					Class181.smethod_2(Enum11.const_0, RareCrafter.getString_1(107233934), new object[]
					{
						this.class299_0.CraftedItem.ExplicitMods
					});
				}
				((byte*)ptr)[2] = 1;
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		static RareCrafter()
		{
			Strings.CreateGetStringDelegate(typeof(RareCrafter));
		}

		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		private int int_2 = -1;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
