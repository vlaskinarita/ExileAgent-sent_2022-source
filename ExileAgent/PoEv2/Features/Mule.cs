using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using ns0;
using ns14;
using ns25;
using ns29;
using ns32;
using ns35;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Features
{
	public static class Mule
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			Mule.mainForm_0 = mainForm_1;
		}

		public unsafe static void smethod_1()
		{
			void* ptr = stackalloc byte[6];
			Mule.mainForm_0.method_117();
			Class181.smethod_5(Enum11.const_0, Mule.getString_0(107272142));
			UI.smethod_1();
			for (;;)
			{
				((byte*)ptr)[5] = 1;
				Thread.Sleep(150);
				*(byte*)ptr = (Mule.smethod_4() ? 1 : 0);
				((byte*)ptr)[1] = ((*(sbyte*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					UI.smethod_65();
					Mule.smethod_2();
					((byte*)ptr)[2] = (Mule.smethod_3() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_5(Enum11.const_0, Mule.getString_0(107272113));
						UI.smethod_22();
						Thread.Sleep(500);
					}
				}
				else if (!UI.smethod_90() || UI.smethod_50(Mule.getString_0(107463711)))
				{
					UI.smethod_66();
					Thread.Sleep(250);
				}
				else
				{
					UI.smethod_66();
					UI.smethod_20();
					Class181.smethod_5(Enum11.const_0, Mule.getString_0(107272084));
					List<JsonItem> list_ = InventoryManager.smethod_6(UI.list_0.smethod_17().Select(new Func<TradeWindowItem, Item>(Mule.<>c.<>9.method_0)).ToList<Item>());
					((byte*)ptr)[3] = ((!UI.smethod_13(1)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						goto IL_14D;
					}
					((byte*)ptr)[4] = ((!Mule.smethod_5(list_)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						break;
					}
				}
			}
			goto IL_162;
			IL_14D:
			Class181.smethod_5(Enum11.const_2, Mule.getString_0(107272043));
			IL_162:
			Mule.mainForm_0.method_118();
		}

		private unsafe static void smethod_2()
		{
			void* ptr = stackalloc byte[2];
			Class181.smethod_5(Enum11.const_0, Mule.getString_0(107272526));
			UI.smethod_32(0, -1, Enum2.const_3, true);
			do
			{
				((byte*)ptr)[1] = 1;
				Thread.Sleep(200);
				*(byte*)ptr = (Mule.smethod_3() ? 1 : 0);
			}
			while (*(sbyte*)ptr == 0);
		}

		private static bool smethod_3()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.InventoryWindow, Mule.getString_0(107399156)))
			{
				result = UI.smethod_9(bitmap, Images.RequestAcceptDecline);
			}
			return result;
		}

		private static bool smethod_4()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.TradeOpen, Mule.getString_0(107399156)))
			{
				result = UI.smethod_9(bitmap, Images.TradeOpen);
			}
			return result;
		}

		private unsafe static bool smethod_5(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[17];
			*(int*)ptr = -1;
			List<JsonItem> list = new List<JsonItem>();
			Class181.smethod_4(Enum11.const_0, Mule.getString_0(107272517), new object[]
			{
				list_0.Count
			});
			foreach (JsonItem jsonItem in list_0)
			{
				*(int*)((byte*)ptr + 4) = -1;
				foreach (int num in Class255.MuleStashIds)
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
					Class181.smethod_4(Enum11.const_2, Mule.getString_0(107272476), new object[]
					{
						jsonItem.Name
					});
					((byte*)ptr)[15] = 0;
					goto IL_239;
				}
				((byte*)ptr)[16] = ((*(int*)ptr != *(int*)((byte*)ptr + 4)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					Mule.smethod_6(list);
					UI.smethod_35(*(int*)((byte*)ptr + 4), true, 1);
					list.Clear();
					*(int*)ptr = *(int*)((byte*)ptr + 4);
				}
				list.Add(jsonItem);
				UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
				Win32.smethod_9();
				Thread.Sleep(50);
			}
			Mule.smethod_6(list);
			UI.smethod_32(0, -1, Enum2.const_3, true);
			Class181.smethod_5(Enum11.const_0, Mule.getString_0(107272748));
			Class307.smethod_0(ConfigOptions.OnMuleInventoryEmpty, Mule.getString_0(107272411), Mule.getString_0(107272382));
			((byte*)ptr)[15] = 1;
			IL_239:
			return *(sbyte*)((byte*)ptr + 15) != 0;
		}

		private unsafe static void smethod_6(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[12];
			if (list_0.Any<JsonItem>() && UI.smethod_13(1))
			{
				int[,] array = UI.smethod_84();
				int[,] array2 = InventoryManager.smethod_7(list_0);
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
					Mule.smethod_6(list_0);
				}
			}
		}

		static Mule()
		{
			Strings.CreateGetStringDelegate(typeof(Mule));
		}

		private static MainForm mainForm_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
