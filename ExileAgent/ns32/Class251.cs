using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using ns29;
using PoEv2;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns32
{
	internal static class Class251
	{
		private static double Scale
		{
			get
			{
				return UI.GameScale;
			}
		}

		public static Point WindowOffset
		{
			get
			{
				return new Point((double)UI.BorderSize.Width, (double)UI.BorderSize.Height);
			}
		}

		public unsafe static Point StashOffset
		{
			get
			{
				void* ptr = stackalloc byte[16];
				*(double*)ptr = 15.0 * Class251.Scale;
				*(double*)((byte*)ptr + 8) = 126.0 * Class251.Scale;
				return new Point(*(double*)ptr, *(double*)((byte*)ptr + 8));
			}
		}

		public unsafe static Point InventoryOffset
		{
			get
			{
				void* ptr = stackalloc byte[16];
				*(double*)ptr = (double)UI.PoeDimensions.Width - 648.0 * Class251.Scale;
				*(double*)((byte*)ptr + 8) = 588.0 * Class251.Scale;
				return new Point(*(double*)ptr, *(double*)((byte*)ptr + 8));
			}
		}

		public unsafe static Point TradeWindowItemStartOffset
		{
			get
			{
				void* ptr = stackalloc byte[16];
				*(double*)ptr = ((double)UI.PoeDimensions.Width - 690.0 * Class251.Scale - 662.0 * Class251.Scale) / 2.0 + 29.0 * Class251.Scale;
				*(double*)((byte*)ptr + 8) = 206.0 * Class251.Scale;
				return new Point(*(double*)ptr, *(double*)((byte*)ptr + 8));
			}
		}

		public unsafe static Rectangle GuildStashOpen
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = 260.0 * Class251.Scale + Class251.WindowOffset.X;
				*(double*)((byte*)ptr + 8) = 50.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 16) = 75.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 30.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle smethod_0(Position position_0)
		{
			void* ptr = stackalloc byte[24];
			Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.GuildStashTitle);
			*(double*)ptr = (double)(position_0.Left - tuple.Item1.Width) - 5.0 * Class251.Scale;
			*(int*)((byte*)ptr + 16) = position_0.Top;
			*(double*)((byte*)ptr + 8) = (double)tuple.Item1.Width + 14.0 * Class251.Scale;
			*(int*)((byte*)ptr + 20) = Class308.smethod_0(Images.StashTitle).Item1.Height;
			return new Rectangle((int)Math.Round(*(double*)ptr), *(int*)((byte*)ptr + 16), (int)Math.Round(*(double*)((byte*)ptr + 8)), *(int*)((byte*)ptr + 20));
		}

		public unsafe static Rectangle TownIcon
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = 30.0 * Class251.Scale + Class251.WindowOffset.X;
				*(double*)((byte*)ptr + 8) = 170.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 16) = 612.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 386.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle smethod_1(Position position_0)
		{
			void* ptr = stackalloc byte[40];
			*(double*)ptr = Class251.TradeWindowItemStartOffset.X;
			*(double*)((byte*)ptr + 8) = (double)Class308.smethod_0(Images.TradeAccept).Item1.Width * Class251.Scale;
			*(double*)((byte*)ptr + 16) = *(double*)ptr + *(double*)((byte*)ptr + 8);
			*(int*)((byte*)ptr + 24) = position_0.Left;
			*(int*)((byte*)ptr + 28) = position_0.Top;
			*(int*)((byte*)ptr + 32) = (int)Math.Round((double)(*(int*)((byte*)ptr + 24)) - *(double*)((byte*)ptr + 16));
			*(int*)((byte*)ptr + 36) = position_0.Height;
			return new Rectangle((int)Math.Round(*(double*)((byte*)ptr + 16)), *(int*)((byte*)ptr + 28), *(int*)((byte*)ptr + 32), *(int*)((byte*)ptr + 36));
		}

		public static Point DeclineOffset
		{
			get
			{
				return new Point(290.0 * Class251.Scale, 17.0 * Class251.Scale);
			}
		}

		public static Point StashChestOffset
		{
			get
			{
				return new Point(35.0 * Class251.Scale, 36.0 * Class251.Scale);
			}
		}

		public unsafe static Rectangle Stash
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = Class251.WindowOffset.X + Class251.StashOffset.X;
				Point windowOffset = Class251.WindowOffset;
				*(double*)((byte*)ptr + 8) = windowOffset.Y + Class251.StashOffset.Y;
				*(double*)((byte*)ptr + 16) = 636.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 636.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle Inventory
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = 633.0 * Class251.Scale;
				Point inventoryOffset = Class251.InventoryOffset;
				*(double*)((byte*)ptr + 8) = inventoryOffset.X + Class251.WindowOffset.X;
				inventoryOffset = Class251.InventoryOffset;
				*(double*)((byte*)ptr + 16) = inventoryOffset.Y + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 24) = 263.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle InventoryToEnd
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = 649.0 * Class251.Scale;
				Point inventoryOffset = Class251.InventoryOffset;
				*(double*)((byte*)ptr + 8) = inventoryOffset.X + Class251.WindowOffset.X;
				inventoryOffset = Class251.InventoryOffset;
				*(double*)((byte*)ptr + 16) = inventoryOffset.Y + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 24) = 263.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle TradeWindowItems
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = Class251.TradeWindowItemStartOffset.X + Class251.WindowOffset.X;
				Point tradeWindowItemStartOffset = Class251.TradeWindowItemStartOffset;
				*(double*)((byte*)ptr + 8) = tradeWindowItemStartOffset.Y + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 16) = 633.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 263.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public static double ItemOffset
		{
			get
			{
				return 44.0 * Class251.Scale;
			}
		}

		public static Dictionary<int, Rectangle> ProphecyDictionary
		{
			get
			{
				Dictionary<int, Position> dictionary = new Dictionary<int, Position>
				{
					{
						1,
						new Position(292, 211)
					},
					{
						2,
						new Position(147, 268)
					},
					{
						3,
						new Position(438, 268)
					},
					{
						4,
						new Position(112, 435)
					},
					{
						5,
						new Position(473, 435)
					},
					{
						6,
						new Position(200, 597)
					},
					{
						7,
						new Position(384, 597)
					}
				};
				Dictionary<int, Rectangle> dictionary2 = new Dictionary<int, Rectangle>();
				foreach (KeyValuePair<int, Position> keyValuePair in dictionary)
				{
					dictionary2.Add(keyValuePair.Key, new Rectangle((int)Math.Round((double)keyValuePair.Value.X * Class251.Scale + Class251.WindowOffset.X), (int)Math.Round((double)keyValuePair.Value.Y * Class251.Scale + Class251.WindowOffset.Y), Class308.smethod_0(Images.ProphecySeal).Item1.Width, Class308.smethod_0(Images.ProphecySeal).Item1.Height));
				}
				return dictionary2;
			}
		}

		public unsafe static Rectangle ProphecySeek
		{
			get
			{
				void* ptr = stackalloc byte[24];
				*(double*)ptr = 278.0 * Class251.Scale + Class251.WindowOffset.X;
				*(double*)((byte*)ptr + 8) = 756.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(int*)((byte*)ptr + 16) = Util.smethod_22(106.0 * Class251.Scale);
				*(int*)((byte*)ptr + 20) = Util.smethod_22(50.0 * Class251.Scale);
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), *(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 20));
			}
		}

		public unsafe static Rectangle InventoryOpen
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = (double)UI.PoeDimensions.Width - 455.0 * UI.GameScale + Class251.WindowOffset.X;
				Point windowOffset = Class251.WindowOffset;
				*(double*)((byte*)ptr + 8) = windowOffset.Y;
				*(double*)((byte*)ptr + 16) = 245.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 100.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle TradeWaiting
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = 500.0 * Class251.Scale;
				Rectangle poeDimensions = UI.PoeDimensions;
				*(double*)((byte*)ptr + 8) = (double)(poeDimensions.Width / 2) - *(double*)ptr / 2.0 + Class251.WindowOffset.X;
				*(double*)((byte*)ptr + 16) = 475.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 24) = 130.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle TradeOpen
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = (double)Class251.TradeWindow.X + 261.0 * Class251.Scale;
				*(double*)((byte*)ptr + 8) = 77.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 16) = 170.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 40.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle InventoryWindow
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = Class251.InventoryOffset.X + Class251.WindowOffset.X;
				Point windowOffset = Class251.WindowOffset;
				*(double*)((byte*)ptr + 8) = windowOffset.Y;
				*(double*)((byte*)ptr + 16) = 649.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 854.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle TradeWindow
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = ((double)UI.PoeDimensions.Width - 690.0 * Class251.Scale - 665.0 * Class251.Scale) / 2.0 + Class251.WindowOffset.X;
				*(double*)((byte*)ptr + 8) = 69.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 16) = 690.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 804.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public unsafe static Rectangle StashTabBar
		{
			get
			{
				void* ptr = stackalloc byte[32];
				*(double*)ptr = 14.0 * Class251.Scale + Class251.WindowOffset.X;
				*(double*)((byte*)ptr + 8) = 96.0 * Class251.Scale + Class251.WindowOffset.Y;
				*(double*)((byte*)ptr + 16) = 614.0 * Class251.Scale;
				*(double*)((byte*)ptr + 24) = 28.0 * Class251.Scale;
				return new Rectangle((int)Math.Round(*(double*)ptr), (int)Math.Round(*(double*)((byte*)ptr + 8)), (int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
		}

		public static Point NavaliSellItemsOffset
		{
			get
			{
				Bitmap item = Class308.smethod_0(Images.VendorSellItems).Item1;
				return new Point((double)(item.Width / 2), (double)(item.Height / 2) - 23.0 * Class251.Scale);
			}
		}

		public static Point TradeDivCardOffset
		{
			get
			{
				Bitmap item = Class308.smethod_0(Images.NavaliTradeDivCards).Item1;
				return new Point((double)(item.Width / 2), (double)(item.Height / 2));
			}
		}

		public static double SpecialTabItemWidth
		{
			get
			{
				return 1.110137306456325;
			}
		}

		public static double SpecialTabItemHeight
		{
			get
			{
				return 1.1101156006206159;
			}
		}

		public static Point AcceptTradeOffset
		{
			get
			{
				return new Point(88.0 * Class251.Scale, 24.0 * Class251.Scale);
			}
		}

		public static Point NavaliTradeOffset
		{
			get
			{
				Bitmap item = Class308.smethod_0(Images.NavaliTrade).Item1;
				return new Point((double)(item.Width / 2), (double)(item.Height / 2));
			}
		}

		public static Point NavaliDivCardOffset
		{
			get
			{
				Bitmap item = Class308.smethod_0(Images.NavaliDivCardSlotEmpty).Item1;
				return new Point((double)(item.Width / 2), (double)(item.Height / 2));
			}
		}

		public static Position SeekProphecy
		{
			get
			{
				return new Position(332.0 * Class251.Scale + Class251.WindowOffset.X, 778.0 * Class251.Scale + Class251.WindowOffset.Y);
			}
		}

		public static Position CreatePartyLocation
		{
			get
			{
				return new Position(338.0 * Class251.Scale + Class251.WindowOffset.X, 178.0 * Class251.Scale + Class251.WindowOffset.Y);
			}
		}

		public static Position PartyInviteOffset
		{
			get
			{
				return new Position(-160.0 * Class251.Scale, 124.0 * Class251.Scale);
			}
		}

		public static Position CapturedBeasts
		{
			get
			{
				return new Position(609.0 * Class251.Scale + Class251.WindowOffset.X, 668.0 * Class251.Scale + Class251.WindowOffset.Y);
			}
		}

		public static Position DefaultBeastMousePos
		{
			get
			{
				return new Position(540.0 * Class251.Scale + Class251.WindowOffset.X, 680.0 * Class251.Scale + Class251.WindowOffset.Y);
			}
		}

		public static Position TradeRequestFullOffset
		{
			get
			{
				return new Position(311.0 * Class251.Scale, -98.0 * Class251.Scale);
			}
		}

		public static Position smethod_2(string string_0)
		{
			if (string_0 != null)
			{
				if (string_0 == Class251.getString_0(107378975))
				{
					return new Position(90.0 * Class251.Scale + Class251.WindowOffset.X, 155.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
				if (string_0 == Class251.getString_0(107441847))
				{
					return new Position(250.0 * Class251.Scale + Class251.WindowOffset.X, 155.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
				if (string_0 == Class251.getString_0(107441838))
				{
					return new Position(410.0 * Class251.Scale + Class251.WindowOffset.X, 155.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
				if (string_0 == Class251.getString_0(107441861) || string_0 == Class251.getString_0(107441852))
				{
					return new Position(570.0 * Class251.Scale + Class251.WindowOffset.X, 155.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
			}
			return new Position();
		}

		public static Position smethod_3(string string_0)
		{
			if (string_0 != null)
			{
				if (string_0 == Class251.getString_0(107441861))
				{
					return new Position(270.0 * Class251.Scale + Class251.WindowOffset.X, 360.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
				if (string_0 == Class251.getString_0(107441852))
				{
					return new Position(400.0 * Class251.Scale + Class251.WindowOffset.X, 360.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
			}
			return new Position();
		}

		public static Position smethod_4(string string_0)
		{
			if (string_0 != null)
			{
				if (string_0 == Class251.getString_0(107378975))
				{
					return new Position(250.0 * Class251.Scale + Class251.WindowOffset.X, 155.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
				if (string_0 == Class251.getString_0(107441807))
				{
					return new Position(420.0 * Class251.Scale + Class251.WindowOffset.X, 155.0 * Class251.Scale + Class251.WindowOffset.Y);
				}
			}
			return new Position();
		}

		public static Rectangle ChatLocation
		{
			get
			{
				return new Rectangle
				{
					X = (int)Class251.WindowOffset.X,
					Y = (int)Class251.WindowOffset.Y,
					Width = UI.PoeDimensions.Width - Class251.InventoryToEnd.Width,
					Height = UI.PoeDimensions.Height
				};
			}
		}

		public static Rectangle GwennenVendor
		{
			get
			{
				return new Rectangle
				{
					X = (int)Class251.WindowOffset.X,
					Y = (int)Class251.WindowOffset.Y,
					Width = UI.PoeDimensions.Width - Class251.InventoryToEnd.Width,
					Height = UI.PoeDimensions.Height
				};
			}
		}

		static Class251()
		{
			Strings.CreateGetStringDelegate(typeof(Class251));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
