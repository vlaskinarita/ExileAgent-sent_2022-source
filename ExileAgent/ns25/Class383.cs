using System;
using System.Collections.Generic;
using ns0;
using ns17;
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

namespace ns25
{
	internal sealed class Class383 : Crafter
	{
		public Class383(MainForm mainForm_1, Class299 class299_1) : base(mainForm_1, class299_1)
		{
			this.dictionary_0 = new Dictionary<JsonTab, List<JsonItem>>();
			this.class299_0.Prefixes = new List<Mod>();
			this.class299_0.Suffixes = new List<Mod>();
			this.class299_0.PrefixTiers = new List<Tier>();
			this.class299_0.SuffixTiers = new List<Tier>();
		}

		protected unsafe override void vmethod_0()
		{
			void* ptr = stackalloc byte[10];
			Class181.smethod_2(Enum11.const_3, Class383.getString_1(107234561), new object[]
			{
				this.class299_0
			});
			this.dictionary_0 = StashManager.smethod_1(Class383.getString_1(107385143), true);
			decimal num = Math.Min(Class255.class105_0.method_6(ConfigOptions.ChanceChanceLimit), this.dictionary_0.smethod_16(false));
			*(byte*)ptr = 0;
			Class181.smethod_2(Enum11.const_3, Class383.getString_1(107234552), new object[]
			{
				num
			});
			Win32.smethod_4(-2, -2, 50, 90, false);
			this.vmethod_1();
			((byte*)ptr)[1] = ((!this.method_14(false, null)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) == 0)
			{
				for (;;)
				{
					((byte*)ptr)[8] = 1;
					((byte*)ptr)[2] = (this.class299_0.OutOfMaterials ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						break;
					}
					if (num > 0m && this.materials_0.Chances >= num)
					{
						goto IL_187;
					}
					((byte*)ptr)[3] = (this.class299_0.CraftedItem.isUnique ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						goto IL_195;
					}
					((byte*)ptr)[4] = ((!this.class299_0.CraftedItem.isNormal) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						((byte*)ptr)[5] = ((!this.method_12()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							break;
						}
					}
					((byte*)ptr)[6] = ((!this.method_13()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						break;
					}
					((byte*)ptr)[7] = (this.class299_0.CraftedItem.isUnique ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) != 0)
					{
						goto IL_1AC;
					}
				}
				goto IL_1AF;
				IL_187:
				this.class299_0.OutOfMaterials = true;
				goto IL_1AF;
				IL_195:
				Class181.smethod_3(Enum11.const_2, Class383.getString_1(107234475));
				return;
				IL_1AC:
				*(byte*)ptr = 1;
				IL_1AF:
				Win32.smethod_20();
				((byte*)ptr)[9] = (byte)(*(sbyte*)ptr);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					Class181.smethod_3(Enum11.const_0, Class383.getString_1(107234454));
				}
				else
				{
					Class181.smethod_3(Enum11.const_0, Class383.getString_1(107234417));
				}
				Class181.smethod_3(Enum11.const_0, this.materials_0.ToString());
			}
		}

		private unsafe bool method_12()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!base.method_7(Class383.getString_1(107385080), Class383.getString_1(107399428), false)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_14(true, new Func<bool>(this.method_12)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_13()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!base.method_7(Class383.getString_1(107385143), Class383.getString_1(107399428), true)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[1] = (this.method_14(true, new Func<bool>(this.method_13)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_14(bool bool_1, Func<bool> func_0)
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
					Class181.smethod_2(Enum11.const_0, Class383.getString_1(107234799), new object[]
					{
						this.class299_0.CraftedItem.Name
					});
				}
				((byte*)ptr)[2] = 1;
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		static Class383()
		{
			Strings.CreateGetStringDelegate(typeof(Class383));
		}

		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
