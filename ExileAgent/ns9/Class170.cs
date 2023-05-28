using System;
using System.Linq;
using ns29;
using ns35;
using PoEv2;
using PoEv2.Handlers.Events.Orders;
using PoEv2.Managers;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns9
{
	internal static class Class170
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			Class170.mainForm_0 = mainForm_1;
		}

		public unsafe static void smethod_1(Order order_0)
		{
			void* ptr = stackalloc byte[5];
			((byte*)ptr)[1] = ((order_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) == 0)
			{
				Class181.smethod_2(Enum11.const_3, Class170.getString_0(107455852), new object[]
				{
					order_0.OrderType,
					order_0.player.name
				});
				string string_;
				((byte*)ptr)[2] = (Class170.smethod_2(out string_) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_0, string_);
					Class170.mainForm_0.list_7.Remove(order_0);
					Class170.mainForm_0.list_9.Remove(order_0);
					Class170.mainForm_0.method_65();
				}
				else
				{
					UI.smethod_1();
					((byte*)ptr)[3] = ((order_0.OrderType == Order.Type.Sell) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						*(byte*)ptr = (SellOrderProcessor.smethod_0(order_0, Class170.mainForm_0) ? 1 : 0);
					}
					else
					{
						*(byte*)ptr = (BuyOrderProcessor.smethod_0(order_0, Class170.mainForm_0) ? 1 : 0);
					}
					Class170.mainForm_0.method_65();
					((byte*)ptr)[4] = ((*(sbyte*)ptr == 0) ? 1 : 0);
					Position position;
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						Class170.mainForm_0.list_7.Remove(order_0);
						Class170.mainForm_0.list_9.Remove(order_0);
						BuyOrderProcessor.order_0 = null;
						Class170.mainForm_0.method_65();
						if (order_0.OrderType == Order.Type.Buy && !Class170.mainForm_0.list_9.Any<Order>() && !Class170.mainForm_0.list_10.Any<BuyingTradeData>())
						{
							UI.smethod_13(1);
						}
					}
					else if (order_0.OrderType == Order.Type.Sell && UI.smethod_3(out position, Images.Social, Class170.getString_0(107378903)))
					{
						UI.smethod_13(1);
					}
				}
			}
		}

		private unsafe static bool smethod_2(out string string_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = (Class170.mainForm_0.PastFlipUpdatingTime ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				string_0 = Class170.getString_0(107455323);
				((byte*)ptr)[1] = 1;
			}
			else
			{
				((byte*)ptr)[2] = (Class170.mainForm_0.PastAFKTime ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					string_0 = Class170.getString_0(107455258);
					((byte*)ptr)[1] = 1;
				}
				else
				{
					((byte*)ptr)[3] = (Class170.mainForm_0.bool_19 ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						string_0 = Class170.getString_0(107455177);
						((byte*)ptr)[1] = 1;
					}
					else
					{
						string_0 = Class170.getString_0(107397354);
						((byte*)ptr)[1] = 0;
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		static Class170()
		{
			Strings.CreateGetStringDelegate(typeof(Class170));
		}

		private static MainForm mainForm_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
