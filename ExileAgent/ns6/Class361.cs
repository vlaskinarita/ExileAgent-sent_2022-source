using System;
using System.Collections.Generic;
using System.Threading;
using ns14;
using ns29;
using ns32;
using ns35;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns6
{
	internal static class Class361
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			Class361.mainForm_0 = mainForm_1;
		}

		public unsafe static List<JsonItem> smethod_1(List<JsonItem> list_0)
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[1] = ((!UI.smethod_68()) ? 1 : 0);
			List<JsonItem> result;
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				result = new List<JsonItem>();
			}
			else
			{
				Position position_;
				((byte*)ptr)[2] = (UI.smethod_3(out position_, Images.NavaliTradeDivCards, Class361.getString_0(107270930)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Win32.smethod_5(position_.smethod_4(Class251.TradeDivCardOffset), false);
					Thread.Sleep(400);
					Win32.smethod_2(true);
					Thread.Sleep(1000);
					Win32.smethod_4(-2, -2, 50, 90, false);
					DateTime dateTime = DateTime.Now.AddSeconds(3.0);
					*(byte*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[4] = ((DateTime.Now < dateTime) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) == 0)
						{
							break;
						}
						Position position;
						((byte*)ptr)[3] = (UI.smethod_3(out position, Images.NavaliCardTradeTitle, Class361.getString_0(107448507)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							goto IL_D8;
						}
					}
					goto IL_DB;
					IL_D8:
					*(byte*)ptr = 1;
					IL_DB:
					((byte*)ptr)[5] = ((*(sbyte*)ptr == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						Class181.smethod_3(Enum11.const_2, Class361.getString_0(107270300));
						result = new List<JsonItem>();
					}
					else
					{
						Position position_2;
						((byte*)ptr)[6] = ((!UI.smethod_3(out position_2, Images.NavaliDivCardSlotEmpty, Class361.getString_0(107448478))) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							Class181.smethod_3(Enum11.const_2, Class361.getString_0(107270283));
							result = new List<JsonItem>();
						}
						else
						{
							foreach (JsonItem jsonItem in list_0)
							{
								dateTime = DateTime.Now.AddSeconds(3.0);
								for (;;)
								{
									((byte*)ptr)[8] = ((dateTime > DateTime.Now) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 8) == 0)
									{
										break;
									}
									((byte*)ptr)[7] = (UI.smethod_9(UI.GameImage, Images.NavaliDivCardSlotEmpty) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 7) == 0)
									{
										break;
									}
									UI.smethod_32(jsonItem.x, jsonItem.y, Enum2.const_3, true);
									Win32.smethod_9();
									Thread.Sleep(200);
								}
								((byte*)ptr)[9] = ((!UI.smethod_3(out position_, Images.NavaliTrade, Class361.getString_0(107448411))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 9) != 0)
								{
									Class181.smethod_3(Enum11.const_2, Class361.getString_0(107270214));
									return new List<JsonItem>();
								}
								Thread.Sleep(600);
								Win32.smethod_5(position_.smethod_4(Class251.NavaliTradeOffset), false);
								Thread.Sleep(400);
								Win32.smethod_2(true);
								dateTime = DateTime.Now.AddSeconds(3.0);
								for (;;)
								{
									((byte*)ptr)[11] = ((dateTime > DateTime.Now) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 11) == 0)
									{
										break;
									}
									((byte*)ptr)[10] = ((!UI.smethod_9(UI.GameImage, Images.NavaliDivCardSlotEmpty)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 10) == 0)
									{
										break;
									}
									Win32.smethod_5(position_2.smethod_4(Class251.NavaliDivCardOffset), false);
									Thread.Sleep(400);
									Win32.smethod_9();
									Thread.Sleep(400);
									Thread.Sleep(200);
								}
							}
							Win32.smethod_14(Class361.getString_0(107396258), false);
							Thread.Sleep(1000);
							result = UI.smethod_31(false, 60, 12, 5).smethod_11();
						}
					}
				}
				else
				{
					Class181.smethod_3(Enum11.const_2, Class361.getString_0(107270877));
					result = new List<JsonItem>();
				}
			}
			return result;
		}

		static Class361()
		{
			Strings.CreateGetStringDelegate(typeof(Class361));
		}

		private static MainForm mainForm_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
