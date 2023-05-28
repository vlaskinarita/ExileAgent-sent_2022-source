using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ns0;
using ns13;
using ns14;
using ns25;
using ns29;
using ns34;
using ns35;
using ns9;
using PoEv2.Classes;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Handlers.Events.Trades
{
	public class TradeProcessor
	{
		public static bool TradeCompleted { get; set; }

		public static List<JsonItem> Inventory { get; set; }

		protected static DateTime TradeStartTime { get; set; }

		public static DateTime TradeExpireTime { get; set; }

		protected static int TotalSeconds
		{
			get
			{
				return DateTime.Now.Subtract(TradeProcessor.TradeStartTime).Seconds;
			}
		}

		protected static int TotalSecondsRemaining
		{
			get
			{
				return TradeProcessor.TradeExpireTime.Subtract(DateTime.Now).Seconds;
			}
		}

		public static void smethod_0(MainForm mainForm_1)
		{
			TradeProcessor.mainForm_0 = mainForm_1;
		}

		public unsafe static void smethod_1(Order order_2, int int_1 = 1)
		{
			void* ptr = stackalloc byte[17];
			Class181.smethod_2(Enum11.const_3, TradeProcessor.getString_0(107459854), new object[]
			{
				order_2.player.name,
				order_2.my_item_amount,
				order_2.my_item_name,
				order_2.player_item_amount,
				order_2.player_item_name,
				int_1
			});
			try
			{
				UI.smethod_1();
				TradeProcessor.order_1 = order_2;
				TradeProcessor.order_0 = TradeProcessor.order_1;
				TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
				Dictionary<int, List<JsonItem>> dictionary = new Dictionary<int, List<JsonItem>>();
				foreach (KeyValuePair<int, List<JsonItem>> keyValuePair in Stashes.Items)
				{
					dictionary.Add(keyValuePair.Key, Stashes.Items[keyValuePair.Key].ToList<JsonItem>());
				}
				TradeProcessor.order_1.StashBackup = dictionary;
				TradeProcessor.smethod_2();
				TradeProcessor.Inventory = InventoryManager.smethod_3(TradeProcessor.order_1, TradeProcessor.order_1.OriginalNote, TradeProcessor.order_1.OrderType == Order.Type.Buy, new List<JsonItem>());
				*(byte*)ptr = ((TradeProcessor.order_1.DecimalAmount > 0m) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					Order order = new Order(TradeProcessor.order_1.DecimalAmount, TradeProcessor.order_1.DecimalCurrencyType);
					TradeProcessor.Inventory = InventoryManager.smethod_3(order, string.Empty, true, TradeProcessor.Inventory);
					foreach (KeyValuePair<JsonTab, List<JsonItem>> keyValuePair2 in order.my_items)
					{
						((byte*)ptr)[1] = ((!TradeProcessor.order_1.my_items.ContainsKey(keyValuePair2.Key)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) != 0)
						{
							TradeProcessor.order_1.my_items.Add(keyValuePair2.Key, new List<JsonItem>());
						}
						TradeProcessor.order_1.my_items[keyValuePair2.Key].AddRange(keyValuePair2.Value);
					}
				}
				((byte*)ptr)[2] = ((!TradeProcessor.Inventory.Any<JsonItem>()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					TradeProcessor.smethod_4();
					((byte*)ptr)[3] = ((TradeProcessor.order_1.OrderType == Order.Type.Sell) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						UI.smethod_39(TradeProcessor.order_1.player, TradeProcessor.getString_0(107361669));
						UI.smethod_40(TradeProcessor.order_1.player);
						TradeProcessor.mainForm_0.method_63();
					}
					else
					{
						UI.smethod_41();
					}
				}
				else
				{
					((byte*)ptr)[4] = ((UI.smethod_83(4) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						((byte*)ptr)[5] = ((int_1 <= 3) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							Class181.smethod_3(Enum11.const_2, TradeProcessor.getString_0(107459769));
							Stashes.Items = dictionary;
							UI.smethod_97(TradeProcessor.getString_0(107459704));
							TradeProcessor.order_1.my_items.Clear();
							TradeProcessor.smethod_1(order_2, ++int_1);
							return;
						}
					}
					((byte*)ptr)[6] = ((TradeProcessor.order_1.OrderType == Order.Type.Sell) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						((byte*)ptr)[7] = ((int_1 <= 3) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							new SellProcessor(TradeProcessor.mainForm_0).method_0();
						}
						((byte*)ptr)[8] = ((!TradeProcessor.TradeCompleted) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							Class181.smethod_3(Enum11.const_3, TradeProcessor.getString_0(107459695) + TradeProcessor.order_1.player.name);
						}
						UI.smethod_36(Enum2.const_22, 1.0);
						UI.smethod_21();
						TradeProcessor.smethod_8();
					}
					else if (!UI.smethod_44(TradeProcessor.order_1.player.name) || TradeProcessor.mainForm_0.enum13_0 == Enum13.const_2)
					{
						Class181.smethod_3(Enum11.const_2, TradeProcessor.getString_0(107459666));
						UI.smethod_41();
					}
					else
					{
						((byte*)ptr)[9] = ((int_1 <= 3) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							UI.smethod_32(7, 5, Enum2.const_3, false);
							new BuyProcessor(TradeProcessor.mainForm_0).method_0();
						}
						((byte*)ptr)[10] = ((!TradeProcessor.TradeCompleted) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							Class181.smethod_3(Enum11.const_3, TradeProcessor.getString_0(107459695) + TradeProcessor.order_1.player.name);
						}
						UI.smethod_36(Enum2.const_22, 1.0);
						UI.smethod_21();
						TradeProcessor.smethod_9();
					}
					TradeProcessor.smethod_4();
					string string_ = TradeProcessor.getString_0(107397302);
					((byte*)ptr)[11] = (TradeProcessor.TradeCompleted ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						Class181.smethod_3(Enum11.const_0, string.Format(TradeProcessor.getString_0(107460113), TradeProcessor.order_1.player.name, TradeProcessor.TotalSeconds));
						TradeProcessor.mainForm_0.list_2.Add(TradeProcessor.order_1);
						TradeProcessor.mainForm_0.LastPlayerWhispers.Remove(TradeProcessor.order_1.player.name);
						string_ = string.Format(TradeProcessor.getString_0(107460052), new object[]
						{
							TradeProcessor.order_1.player.name,
							TradeProcessor.order_1.my_item_amount,
							TradeProcessor.order_1.my_item_name,
							TradeProcessor.order_1.player_item_amount,
							TradeProcessor.order_1.player_item_name
						});
					}
					else
					{
						Class181.smethod_3(Enum11.const_0, string.Format(TradeProcessor.getString_0(107459987), TradeProcessor.order_1.player.name, TradeProcessor.TotalSeconds));
						((byte*)ptr)[12] = (Class255.class105_0.method_4(ConfigOptions.IgnoreFailedTraders) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 12) != 0)
						{
							TradeProcessor.mainForm_0.method_56(TradeProcessor.order_1.player, false);
						}
						TradeProcessor.mainForm_0.list_3.Add(TradeProcessor.order_1);
						TradeProcessor.order_1.seconds_to_trade = TradeProcessor.TotalSeconds;
						string_ = string.Format(TradeProcessor.getString_0(107459966), new object[]
						{
							TradeProcessor.order_1.player.name,
							TradeProcessor.order_1.my_item_amount,
							TradeProcessor.order_1.my_item_name,
							TradeProcessor.order_1.player_item_amount,
							TradeProcessor.order_1.player_item_name
						});
					}
					((byte*)ptr)[13] = (TradeProcessor.TradeCompleted ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						Class307.smethod_0(ConfigOptions.OnSuccessfulTrade, TradeProcessor.getString_0(107459901), string_);
						TradeProcessor.Inventory = InventoryManager.smethod_6(UI.list_0.smethod_17().Select(new Func<TradeWindowItem, Item>(TradeProcessor.<>c.<>9.method_0)).ToList<Item>());
					}
					else
					{
						Class307.smethod_0(ConfigOptions.OnFailedTrade, TradeProcessor.getString_0(107459368), string_);
					}
					((byte*)ptr)[14] = ((TradeProcessor.order_1.OrderType == Order.Type.Sell) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						((byte*)ptr)[15] = ((!TradeProcessor.mainForm_0.list_18.Contains(TradeProcessor.order_1.my_item_name)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 15) != 0)
						{
							TradeProcessor.mainForm_0.list_18.Add(TradeProcessor.order_1.my_item_name);
						}
						foreach (Order order2 in TradeProcessor.mainForm_0.list_7.Where(new Func<Order, bool>(TradeProcessor.<>c.<>9.method_1)).ToList<Order>())
						{
							((byte*)ptr)[16] = ((!TradeProcessor.mainForm_0.bool_6) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 16) != 0)
							{
								Class181.smethod_3(Enum11.const_3, TradeProcessor.getString_0(107459319) + order2.player.name);
								Class170.smethod_1(order2);
							}
						}
					}
					TradeProcessor.mainForm_0.method_67(TradeProcessor.TradeCompleted, TradeProcessor.order_1);
					CleanInventory.smethod_1(TradeProcessor.order_0, TradeProcessor.Inventory);
					TradeProcessor.order_0 = null;
					TradeProcessor.Inventory = new List<JsonItem>();
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception arg)
			{
				Class181.smethod_3(Enum11.const_2, string.Format(TradeProcessor.getString_0(107459322), arg));
				TradeProcessor.mainForm_0.method_63();
			}
		}

		private static void smethod_2()
		{
			UI.smethod_65();
			TradeProcessor.Inventory = new List<JsonItem>();
			TradeProcessor.TradeCompleted = false;
		}

		protected unsafe static void smethod_3()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (UI.smethod_19() ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = ((!TradeProcessor.smethod_7()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.ClickedAccept;
					UI.smethod_20();
					Class181.smethod_3(Enum11.const_3, string.Format(TradeProcessor.getString_0(107459305), TradeProcessor.order_1.player.name));
					UI.smethod_36(Enum2.const_22, 1.0);
				}
			}
		}

		private static void smethod_4()
		{
			TradeProcessor.mainForm_0.list_8.Remove(TradeProcessor.order_1);
			TradeProcessor.mainForm_0.list_7.Remove(TradeProcessor.order_1);
			TradeProcessor.mainForm_0.list_9.Remove(TradeProcessor.order_1);
			TradeProcessor.mainForm_0.method_65();
		}

		protected unsafe static bool smethod_5(Bitmap bitmap_0, List<JsonItem> list_1, bool bool_1)
		{
			void* ptr = stackalloc byte[17];
			foreach (JsonItem jsonItem in list_1)
			{
				((byte*)ptr)[8] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Cancelled) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					Thread.Sleep(500);
					((byte*)ptr)[9] = (UI.smethod_71() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						break;
					}
					TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
				}
				Position position_ = UI.smethod_32(jsonItem.Left, jsonItem.Top, Enum2.const_4, true);
				UI.position_0 = position_;
				Class181.smethod_3(Enum11.const_3, string.Format(TradeProcessor.getString_0(107351008), UI.position_0));
				Win32.smethod_9();
				TradeProcessor.smethod_6(jsonItem);
				((byte*)ptr)[10] = ((jsonItem.stack == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					Class181.smethod_3(Enum11.const_3, string.Format(TradeProcessor.getString_0(107459272), jsonItem.Name));
				}
				else
				{
					Class181.smethod_3(Enum11.const_3, string.Format(TradeProcessor.getString_0(107459227), jsonItem.stackSize, jsonItem.Name));
				}
			}
			*(int*)ptr = 0;
			Position position_2 = UI.smethod_46(Enum10.const_2, 0, -1);
			for (;;)
			{
				Func<JsonItem, bool> predicate;
				if ((predicate = TradeProcessor.<>c.<>9__29_1) == null)
				{
					goto IL_35C;
				}
				IL_136:
				if (!list_1.Any(predicate))
				{
					break;
				}
				((byte*)ptr)[11] = ((*(int*)ptr > 10) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					break;
				}
				((byte*)ptr)[12] = ((*(int*)ptr > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					TradeProcessor.TradeExpireTime.AddSeconds(2.0);
				}
				*(int*)ptr = *(int*)ptr + 1;
				Win32.smethod_5(position_2, false);
				((byte*)ptr)[13] = ((*(int*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					Thread.Sleep(100);
				}
				((byte*)ptr)[14] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Cancelled) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					goto IL_38E;
				}
				using (Bitmap bitmap = UI.smethod_67())
				{
					List<Rectangle> list = UI.smethod_59(bitmap_0, bitmap, TradeProcessor.getString_0(107397302), 0.4, 0);
					int[,] array = InventoryManager.smethod_12(TradeProcessor.Inventory);
					foreach (Rectangle rectangle_ in list)
					{
						Position position = UI.smethod_60(Enum10.const_2, rectangle_);
						if (position.X >= 0 && position.Y >= 0 && array.GetLength(0) > position.X && array.GetLength(1) > position.Y)
						{
							*(int*)((byte*)ptr + 4) = array[position.X, position.Y];
							if (*(int*)((byte*)ptr + 4) != 0 && list_1.Count<JsonItem>() >= *(int*)((byte*)ptr + 4) - 1)
							{
								list_1[*(int*)((byte*)ptr + 4) - 1] = null;
							}
						}
					}
				}
				using (IEnumerator<JsonItem> enumerator3 = list_1.Where(new Func<JsonItem, bool>(TradeProcessor.<>c.<>9.method_2)).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						JsonItem jsonItem2 = enumerator3.Current;
						((byte*)ptr)[16] = ((TradeProcessor.order_1.tradeStates_0 == Order.TradeStates.Cancelled) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) != 0)
						{
							((byte*)ptr)[15] = 0;
							goto IL_397;
						}
						UI.smethod_32(jsonItem2.Left, jsonItem2.Top, Enum2.const_4, true);
						Win32.smethod_9();
						TradeProcessor.smethod_6(jsonItem2);
					}
					continue;
				}
				IL_35C:
				predicate = (TradeProcessor.<>c.<>9__29_1 = new Func<JsonItem, bool>(TradeProcessor.<>c.<>9.method_3));
				goto IL_136;
			}
			Win32.smethod_5(UI.smethod_46(Enum10.const_3, 0, 5), false);
			((byte*)ptr)[15] = 1;
			goto IL_397;
			IL_38E:
			((byte*)ptr)[15] = 0;
			IL_397:
			return *(sbyte*)((byte*)ptr + 15) != 0;
		}

		private unsafe static void smethod_6(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((jsonItem_0.incubatedItem != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				DateTime t = DateTime.Now.AddSeconds(3.0);
				for (;;)
				{
					((byte*)ptr)[2] = ((DateTime.Now < t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						break;
					}
					Position position;
					((byte*)ptr)[1] = (UI.smethod_3(out position, Images.IncubatorYes, TradeProcessor.getString_0(107459162)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Win32.smethod_14(TradeProcessor.getString_0(107382335), false);
					}
				}
			}
		}

		public unsafe static bool smethod_7()
		{
			void* ptr = stackalloc byte[2];
			foreach (TradeWindowItem tradeWindowItem in UI.list_0.smethod_17())
			{
				*(byte*)ptr = (tradeWindowItem.Item.IsInfluenced ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					Rectangle rectangle_ = UI.smethod_93(tradeWindowItem.Position.X, tradeWindowItem.Position.Y, false);
					using (Bitmap bitmap = Class197.smethod_1(rectangle_, TradeProcessor.getString_0(107397302)))
					{
						if (UI.smethod_59(bitmap, tradeWindowItem.CroppedImage, TradeProcessor.getString_0(107397302), 0.4, 0).Any<Rectangle>() && !UI.smethod_7(tradeWindowItem.Image, bitmap, 0.7))
						{
							Class181.smethod_2(Enum11.const_3, TradeProcessor.getString_0(107459145), new object[]
							{
								tradeWindowItem.Position
							});
							TradeProcessor.order_1.tradeStates_0 = Order.TradeStates.Pending;
							((byte*)ptr)[1] = 0;
							goto IL_115;
						}
					}
				}
			}
			((byte*)ptr)[1] = 1;
			IL_115:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private static void smethod_8()
		{
			List<Action> list = new List<Action>();
			list.Add(new Action(TradeProcessor.<>c.<>9.method_4));
			list.Add(new Action(TradeProcessor.smethod_10));
			List<Action> list2 = list;
			list2.smethod_24<Action>();
			foreach (Action action in list2)
			{
				action();
			}
		}

		private static void smethod_9()
		{
			List<Action> list = new List<Action>();
			list.Add(new Action(UI.smethod_41));
			list.Add(new Action(TradeProcessor.<>c.<>9.method_5));
			list.Add(new Action(TradeProcessor.smethod_10));
			List<Action> list2 = list;
			list2.smethod_24<Action>();
			foreach (Action action in list2)
			{
				action();
			}
		}

		private unsafe static void smethod_10()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((!TradeProcessor.TradeCompleted) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string text = TradeProcessor.smethod_11();
				((byte*)ptr)[1] = ((text == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					UI.smethod_39(TradeProcessor.order_1.player, text);
				}
			}
		}

		private static string smethod_11()
		{
			List<string> list = Class255.class105_0.method_8<string>(ConfigOptions.MessagesAfterTradeList);
			string result;
			if (Class255.class105_0.method_4(ConfigOptions.MessagesAfterTradeEnabled) && list.Count > 0)
			{
				Random random = new Random();
				string text = list[random.Next(0, list.Count)];
				result = text;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static TradeProcessor()
		{
			Strings.CreateGetStringDelegate(typeof(TradeProcessor));
			TradeProcessor.TradeCompleted = false;
			TradeProcessor.Inventory = new List<JsonItem>();
		}

		private const int int_0 = 3;

		protected static MainForm mainForm_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool bool_0;

		public static Order order_0;

		protected static Order order_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static List<JsonItem> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static DateTime dateTime_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static DateTime dateTime_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
