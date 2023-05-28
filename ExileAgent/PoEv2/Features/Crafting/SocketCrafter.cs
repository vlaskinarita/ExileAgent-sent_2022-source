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
	internal sealed class SocketCrafter : Crafter
	{
		private int SocketCount
		{
			get
			{
				return Class255.class105_0.method_5(ConfigOptions.SocketMinSockets);
			}
		}

		private int LinkCount
		{
			get
			{
				return Class255.class105_0.method_5(ConfigOptions.SocketMinLinks);
			}
		}

		public SocketCrafter(MainForm form, Class299 craftData) : base(form, craftData)
		{
			this.dictionary_0 = new Dictionary<JsonTab, List<JsonItem>>();
			this.dictionary_1 = new Dictionary<JsonTab, List<JsonItem>>();
		}

		protected unsafe override void vmethod_0()
		{
			void* ptr = stackalloc byte[14];
			Class181.smethod_2(Enum11.const_3, SocketCrafter.getString_1(107232591), new object[]
			{
				this.SocketCount,
				this.LinkCount
			});
			((byte*)ptr)[1] = ((this.SocketCount < this.LinkCount) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Class181.smethod_3(Enum11.const_2, SocketCrafter.getString_1(107232546));
			}
			else
			{
				this.dictionary_0 = StashManager.smethod_1(SocketCrafter.getString_1(107385784), true);
				this.dictionary_1 = StashManager.smethod_1(SocketCrafter.getString_1(107385127), true);
				decimal num = Math.Min(Class255.class105_0.method_6(ConfigOptions.SocketJewellersLimit), this.dictionary_0.smethod_16(false));
				decimal num2 = Math.Min(Class255.class105_0.method_6(ConfigOptions.SocketFusingLimit), this.dictionary_1.smethod_16(false));
				*(byte*)ptr = 0;
				Class181.smethod_2(Enum11.const_3, SocketCrafter.getString_1(107232485), new object[]
				{
					num,
					num2
				});
				Win32.smethod_4(-2, -2, 50, 90, false);
				this.vmethod_1();
				((byte*)ptr)[2] = ((!this.method_14(false, null)) ? 1 : 0);
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
						if (num > 0m && this.materials_0.Jewellers >= num)
						{
							goto IL_29A;
						}
						if (num2 > 0m && this.materials_0.Fusings >= num2)
						{
							goto IL_2A8;
						}
						((byte*)ptr)[4] = (this.ItemComplete ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							goto IL_2B6;
						}
						Win32.smethod_19();
						((byte*)ptr)[5] = ((this.class299_0.CraftedItem.SocketCount < this.SocketCount) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							((byte*)ptr)[6] = ((!this.method_12()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 6) != 0)
							{
								return;
							}
							((byte*)ptr)[7] = (this.ItemComplete ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								break;
							}
						}
						else
						{
							((byte*)ptr)[8] = ((this.class299_0.CraftedItem.LinkCount < this.LinkCount) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) != 0)
							{
								((byte*)ptr)[9] = ((this.materials_0.Fusings == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 9) != 0)
								{
									this.bool_1 = true;
								}
								((byte*)ptr)[10] = ((!this.method_13()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									return;
								}
								((byte*)ptr)[11] = (this.ItemComplete ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 11) != 0)
								{
									goto IL_2C0;
								}
							}
						}
					}
					*(byte*)ptr = 1;
					Block_14:
					goto IL_2C3;
					IL_29A:
					this.class299_0.OutOfMaterials = true;
					goto IL_2C3;
					IL_2A8:
					this.class299_0.OutOfMaterials = true;
					goto IL_2C3;
					IL_2B6:
					*(byte*)ptr = 1;
					goto IL_2C3;
					IL_2C0:
					*(byte*)ptr = 1;
					IL_2C3:
					Win32.smethod_20();
					((byte*)ptr)[13] = (byte)(*(sbyte*)ptr);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						Class181.smethod_3(Enum11.const_0, SocketCrafter.getString_1(107234491));
					}
					else
					{
						Class181.smethod_3(Enum11.const_0, SocketCrafter.getString_1(107232476));
					}
					Class181.smethod_3(Enum11.const_0, this.materials_0.ToString());
				}
			}
		}

		private unsafe bool method_12()
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.dictionary_0.smethod_16(false) == 0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, SocketCrafter.getString_1(107232854));
				((byte*)ptr)[1] = 0;
			}
			else if (this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.Sum(new Func<JsonItem, int>(SocketCrafter.<>c.<>9.method_0)) <= 0)
			{
				this.dictionary_0.Remove(this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key);
				((byte*)ptr)[1] = (this.method_12() ? 1 : 0);
			}
			else
			{
				JsonTab key = this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = this.dictionary_0.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				((byte*)ptr)[2] = (this.bool_1 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[3] = ((this.int_2 != key.i) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
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
				Materials materials_ = this.materials_0;
				int num = materials_.Jewellers;
				materials_.Jewellers = num + 1;
				JsonItem jsonItem2 = jsonItem;
				num = jsonItem2.stack;
				jsonItem2.stack = num - 1;
				((byte*)ptr)[1] = (this.method_14(true, new Func<bool>(this.method_12)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_13()
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.dictionary_1.smethod_16(false) == 0) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, SocketCrafter.getString_1(107232781));
				((byte*)ptr)[1] = 0;
			}
			else if (this.dictionary_1.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.Sum(new Func<JsonItem, int>(SocketCrafter.<>c.<>9.method_1)) <= 0)
			{
				this.dictionary_1.Remove(this.dictionary_1.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key);
				((byte*)ptr)[1] = (this.method_13() ? 1 : 0);
			}
			else
			{
				JsonTab key = this.dictionary_1.First<KeyValuePair<JsonTab, List<JsonItem>>>().Key;
				JsonItem jsonItem = this.dictionary_1.First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				((byte*)ptr)[2] = (this.bool_1 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[3] = ((this.int_2 != key.i) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
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
				Materials materials_ = this.materials_0;
				int num = materials_.Fusings;
				materials_.Fusings = num + 1;
				JsonItem jsonItem2 = jsonItem;
				num = jsonItem2.stack;
				jsonItem2.stack = num - 1;
				((byte*)ptr)[1] = (this.method_14(true, new Func<bool>(this.method_13)) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe bool method_14(bool bool_2, Func<bool> func_0)
		{
			void* ptr = stackalloc byte[4];
			((byte*)ptr)[1] = ((!base.method_8(bool_2, out *(bool*)ptr, func_0, 0, false)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				((byte*)ptr)[2] = 0;
			}
			else
			{
				((byte*)ptr)[3] = (byte)(*(sbyte*)ptr);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					Class181.smethod_2(Enum11.const_0, SocketCrafter.getString_1(107232740), new object[]
					{
						this.class299_0.CraftedItem.SocketCount,
						this.class299_0.CraftedItem.LinkCount
					});
				}
				((byte*)ptr)[2] = 1;
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		private bool ItemComplete
		{
			get
			{
				return this.class299_0.CraftedItem.SocketCount >= this.SocketCount && this.class299_0.CraftedItem.LinkCount >= this.LinkCount;
			}
		}

		static SocketCrafter()
		{
			Strings.CreateGetStringDelegate(typeof(SocketCrafter));
		}

		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		private Dictionary<JsonTab, List<JsonItem>> dictionary_1;

		private bool bool_1 = true;

		private int int_2;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
