using System;
using System.Collections.Generic;
using System.Linq;
using ns12;
using ns14;
using ns17;
using ns27;
using ns29;
using ns35;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Features.Crafting;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns0
{
	internal sealed class Class384 : Crafter
	{
		private bool UsesScourTransmute
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.AltUseScourTransmute);
			}
		}

		private bool UsesRegal
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.AltUseScourTransmute) && Class255.class105_0.method_4(ConfigOptions.AltUseRegal);
			}
		}

		private bool UsesAug
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.AltUseAug);
			}
		}

		public Class384(MainForm mainForm_1, Class299 class299_1) : base(mainForm_1, class299_1)
		{
			this.dictionary_0 = new Dictionary<JsonTab, List<JsonItem>>();
		}

		protected unsafe override void vmethod_0()
		{
			void* ptr = stackalloc byte[19];
			Class181.smethod_2(Enum11.const_3, Class384.getString_1(107233915), new object[]
			{
				this.class299_0
			});
			this.dictionary_0 = StashManager.smethod_1(Class384.getString_1(107385705), true);
			decimal num = Math.Min(Class255.class105_0.method_6(ConfigOptions.AltLimit), this.dictionary_0.smethod_16(false));
			*(int*)ptr = this.dictionary_0.smethod_16(false);
			((byte*)ptr)[4] = 0;
			Class181.smethod_2(Enum11.const_3, Class384.getString_1(107234390), new object[]
			{
				num
			});
			Win32.smethod_4(-2, -2, 50, 90, false);
			this.vmethod_1();
			((byte*)ptr)[5] = ((!this.method_19(false, null)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) == 0)
			{
				for (;;)
				{
					((byte*)ptr)[17] = 1;
					if (this.class299_0.OutOfMaterials || this.materials_0.Alts > *(int*)ptr)
					{
						goto IL_2A1;
					}
					if (num > 0m && this.materials_0.Alts >= num)
					{
						goto IL_2AF;
					}
					if (!this.UsesScourTransmute && !this.class299_0.CraftedItem.isMagic)
					{
						goto IL_2BD;
					}
					if ((this.class299_0.CraftedItem.isRare && !this.method_12()) || (this.class299_0.CraftedItem.isNormal && !this.method_13()))
					{
						goto IL_2EE;
					}
					((byte*)ptr)[6] = (this.class300_0.Success ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						goto IL_2D4;
					}
					((byte*)ptr)[7] = (this.UsesAug ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						((byte*)ptr)[8] = (this.method_17(this.class300_0, this.class299_0.CraftedItem) ? 1 : 0);
						((byte*)ptr)[9] = (byte)(*(sbyte*)((byte*)ptr + 8));
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							this.bool_1 = true;
							((byte*)ptr)[10] = (this.class300_0.Success ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 10) != 0)
							{
								goto IL_2DB;
							}
						}
					}
					((byte*)ptr)[11] = (this.UsesRegal ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						((byte*)ptr)[12] = (this.method_18(this.class300_0) ? 1 : 0);
						((byte*)ptr)[13] = (byte)(*(sbyte*)((byte*)ptr + 12));
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							this.bool_1 = true;
							((byte*)ptr)[14] = (this.class300_0.Success ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								break;
							}
							continue;
						}
					}
					Win32.smethod_19();
					((byte*)ptr)[15] = ((!this.method_16()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						return;
					}
					this.bool_1 = false;
					((byte*)ptr)[16] = (this.class300_0.Success ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						goto Block_18;
					}
				}
				((byte*)ptr)[4] = 1;
				goto IL_2EE;
				Block_18:
				((byte*)ptr)[4] = 1;
				goto IL_2EE;
				IL_2A1:
				this.class299_0.OutOfMaterials = true;
				goto IL_2EE;
				IL_2AF:
				this.class299_0.OutOfMaterials = true;
				goto IL_2EE;
				IL_2BD:
				Class181.smethod_3(Enum11.const_2, Class384.getString_1(107234361));
				return;
				IL_2D4:
				((byte*)ptr)[4] = 1;
				goto IL_2EE;
				IL_2DB:
				((byte*)ptr)[4] = 1;
				IL_2EE:
				Win32.smethod_20();
				((byte*)ptr)[18] = (byte)(*(sbyte*)((byte*)ptr + 4));
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					Class181.smethod_3(Enum11.const_0, Class384.getString_1(107234486));
				}
				else
				{
					Class181.smethod_3(Enum11.const_0, Class384.getString_1(107234260));
				}
				Class181.smethod_3(Enum11.const_0, this.materials_0.ToString());
			}
		}

		private unsafe bool method_12()
		{
			void* ptr = stackalloc byte[2];
			string string_ = this.class299_0.CraftedItem.IsFractured ? Class384.getString_1(107441772) : Class384.getString_1(107399460);
			*(byte*)ptr = ((!base.method_7(Class384.getString_1(107385112), string_, false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_19(true, new Func<bool>(this.method_12)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_13()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!base.method_7(Class384.getString_1(107408743), Class384.getString_1(107441772), false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_19(true, new Func<bool>(this.method_13)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_14()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!base.method_7(Class384.getString_1(107408772), Class384.getString_1(107441772), false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_19(true, new Func<bool>(this.method_14)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_15()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!base.method_7(Class384.getString_1(107385049), Class384.getString_1(107404802), false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_19(true, new Func<bool>(this.method_15)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_16()
		{
			void* ptr = stackalloc byte[7];
			*(byte*)ptr = (this.bool_0 ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.bool_1 = true;
				this.bool_0 = false;
			}
			((byte*)ptr)[1] = ((this.dictionary_0.smethod_16(false) == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Class181.smethod_3(Enum11.const_2, Class384.getString_1(107233574));
				this.class299_0.OutOfMaterials = true;
				((byte*)ptr)[2] = 0;
			}
			else
			{
				JsonTab key = this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				((byte*)ptr)[3] = (this.bool_1 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					((byte*)ptr)[4] = ((this.int_2 != key.i) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						UI.smethod_35(key.i, false, 1);
						this.int_2 = key.i;
					}
					UI.smethod_34(key.type, jsonItem.x, jsonItem.y, Enum2.const_19, false);
					Win32.smethod_3();
					this.bool_1 = false;
				}
				this.vmethod_1();
				Win32.smethod_2(true);
				((byte*)ptr)[5] = ((jsonItem.stack == 1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					this.dictionary_0[key].Remove(jsonItem);
					((byte*)ptr)[6] = ((!this.dictionary_0[key].Any<JsonItem>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						this.dictionary_0.Remove(key);
					}
				}
				Materials materials_ = this.materials_0;
				int num = materials_.Alts;
				materials_.Alts = num + 1;
				JsonItem jsonItem2 = jsonItem;
				num = jsonItem2.stack;
				jsonItem2.stack = num - 1;
				this.bool_1 = false;
				((byte*)ptr)[2] = (this.method_19(true, new Func<bool>(this.method_16)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		private unsafe bool method_17(Class300 class300_1, Item item_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!this.UsesAug) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else if (item_0.HasPrefix && item_0.HasSuffix)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = ((class300_1.CraftingOption == Enum17.const_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0 && ((class300_1.RequiredPrefixes > 0 && !item_0.HasPrefix) || (class300_1.RequiredSuffixes > 0 && !item_0.HasSuffix)))
				{
					Win32.smethod_3();
					((byte*)ptr)[1] = (this.method_14() ? 1 : 0);
				}
				else if ((class300_1.PrefixSuccess || class300_1.HitPrefixes > 0) && class300_1.RequiredSuffixes > 0 && !item_0.HasSuffix)
				{
					Win32.smethod_3();
					((byte*)ptr)[1] = (this.method_14() ? 1 : 0);
				}
				else if ((class300_1.SuffixSuccess || class300_1.HitSuffixes > 0) && class300_1.RequiredPrefixes > 0 && !item_0.HasPrefix)
				{
					Win32.smethod_3();
					((byte*)ptr)[1] = (this.method_14() ? 1 : 0);
				}
				else
				{
					((byte*)ptr)[1] = 0;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_18(Class300 class300_1)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!this.UsesRegal) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else if ((class300_1.PrefixSuccess && (class300_1.RequiredSuffixes - 1 == class300_1.HitSuffixes || class300_1.RequiredSuffixes == 0)) || (class300_1.SuffixSuccess && (class300_1.RequiredPrefixes - 1 == class300_1.HitPrefixes || class300_1.RequiredPrefixes == 0)))
			{
				this.method_15();
				((byte*)ptr)[1] = 1;
			}
			else
			{
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_19(bool bool_2, Func<bool> func_0)
		{
			void* ptr = stackalloc byte[3];
			((byte*)ptr)[1] = ((!base.method_8(bool_2, out *(bool*)ptr, func_0, 0, false)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				((byte*)ptr)[2] = 0;
			}
			else
			{
				if (*(sbyte*)ptr != 0 && !this.class299_0.CraftedItem.isNormal)
				{
					Class181.smethod_2(Enum11.const_0, Class384.getString_1(107233940), new object[]
					{
						this.class299_0.CraftedItem.ExplicitMods
					});
				}
				((byte*)ptr)[2] = 1;
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		static Class384()
		{
			Strings.CreateGetStringDelegate(typeof(Class384));
		}

		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		private int int_2 = -1;

		private bool bool_1 = true;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
