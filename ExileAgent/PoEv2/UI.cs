using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using ImageDiff;
using Newtonsoft.Json.Linq;
using ns0;
using ns1;
using ns12;
using ns13;
using ns14;
using ns15;
using ns2;
using ns24;
using ns25;
using ns27;
using ns29;
using ns32;
using ns34;
using ns35;
using ns36;
using PoEv2.Classes;
using PoEv2.Handlers.Events.Trades;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Items;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2
{
	public static class UI
	{
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindowRect(IntPtr intptr_1, out Struct2 struct2_2);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetClientRect(IntPtr intptr_1, out Struct2 struct2_2);

		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowPos(IntPtr intptr_1, int int_4, int int_5, int int_6, int int_7, int int_8, int int_9);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr intptr_1, StringBuilder stringBuilder_0, int int_4);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr intptr_1);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr intptr_1);

		[DllImport("dwmapi.dll")]
		private static extern uint DwmEnableComposition(uint uint_2);

		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr intptr_1, int int_4);

		public static void smethod_0(MainForm mainForm_1)
		{
			UI.mainForm_0 = mainForm_1;
		}

		public static bool IsForeground
		{
			get
			{
				return UI.GetForegroundWindow() == UI.intptr_0;
			}
		}

		public static bool IsFormForeground
		{
			get
			{
				return UI.GetForegroundWindow() == UI.mainForm_0.Handle;
			}
		}

		public static bool SmallChat
		{
			get
			{
				if (!UI.bool_1)
				{
					ProcessHelper.smethod_13(true);
					UI.bool_1 = true;
				}
				return UI.int_2 <= 10 && UI.int_3 <= 10;
			}
		}

		public unsafe static Size BorderSize
		{
			get
			{
				void* ptr = stackalloc byte[9];
				*(int*)ptr = (UI.struct2_0.int_2 - UI.struct2_0.int_0 - (UI.struct2_1.int_2 - UI.struct2_1.int_0)) / 2;
				*(int*)((byte*)ptr + 4) = UI.struct2_1.int_1 - UI.struct2_1.int_3 - (UI.struct2_0.int_1 - UI.struct2_0.int_3) - *(int*)ptr;
				((byte*)ptr)[8] = ((*(int*)ptr == 0) ? 1 : 0);
				Size result;
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					result = new Size(0, 0);
				}
				else
				{
					result = new Size(*(int*)ptr, *(int*)((byte*)ptr + 4));
				}
				return result;
			}
		}

		public unsafe static Rectangle PoeDimensions
		{
			get
			{
				void* ptr = stackalloc byte[8];
				*(int*)ptr = UI.struct2_0.int_2 - UI.struct2_0.int_0 - (int)Class251.WindowOffset.X * 2;
				*(int*)((byte*)ptr + 4) = UI.struct2_0.int_3 - UI.struct2_0.int_1 - (int)Class251.WindowOffset.Y - (int)Class251.WindowOffset.X;
				return new Rectangle(UI.struct2_0.int_0, UI.struct2_0.int_1, *(int*)ptr, *(int*)((byte*)ptr + 4));
			}
		}

		public unsafe static void smethod_1()
		{
			void* ptr = stackalloc byte[2];
			Struct2 @struct;
			UI.GetClientRect(UI.intptr_0, out @struct);
			Struct2 struct2_;
			UI.GetWindowRect(UI.intptr_0, out struct2_);
			@struct = UI.smethod_82(@struct);
			struct2_ = UI.smethod_82(struct2_);
			Win32.smethod_13();
			if (!UI.bool_0 || !UI.IsForeground || !struct2_.Equals(UI.struct2_0) || !UI.struct2_1.Equals(@struct))
			{
				UI.struct2_0 = struct2_;
				UI.struct2_1 = @struct;
				ProcessHelper.smethod_9();
				*(byte*)ptr = ((UI.intptr_0 == IntPtr.Zero) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					UI.bool_1 = false;
				}
				else
				{
					Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107351524), Win32.smethod_24(UI.intptr_0).showCmd.ToString()));
					((byte*)ptr)[1] = ((Win32.smethod_24(UI.intptr_0).showCmd == Win32.ShowWindowCommands.Minimized) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						Win32.smethod_25(UI.intptr_0);
						UI.smethod_36(Enum2.const_22, 1.0);
						UI.smethod_1();
					}
					else
					{
						UI.list_2 = new List<Position>
						{
							UI.smethod_47(Enum10.const_3, 0, 0),
							UI.smethod_47(Enum10.const_3, 12, 0),
							UI.smethod_47(Enum10.const_3, 0, 5),
							UI.smethod_47(Enum10.const_3, 12, 5)
						};
						UI.SetForegroundWindow(UI.intptr_0);
						UI.smethod_36(Enum2.const_22, 1.0);
						UI.bool_0 = true;
					}
				}
			}
		}

		public unsafe static bool smethod_2(out Position position_1, Bitmap bitmap_3, string string_2 = "", double double_0 = 0.8)
		{
			void* ptr = stackalloc byte[2];
			try
			{
				using (Bitmap gameImage = UI.GameImage)
				{
					position_1 = Class196.smethod_0(gameImage, bitmap_3, double_0);
					*(byte*)ptr = (position_1.IsVisible ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						Class181.smethod_3(Enum11.const_3, UI.getString_0(107351491) + string_2);
						((byte*)ptr)[1] = 1;
					}
					else
					{
						Class181.smethod_3(Enum11.const_3, UI.getString_0(107350958) + string_2);
						((byte*)ptr)[1] = 0;
					}
				}
			}
			catch (ExternalException)
			{
				position_1 = new Position();
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_3(out Position position_1, Images images_0, string string_2 = "")
		{
			void* ptr = stackalloc byte[2];
			try
			{
				using (Bitmap gameImage = UI.GameImage)
				{
					Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
					position_1 = Class196.smethod_0(gameImage, tuple.Item1, tuple.Item2);
					*(byte*)ptr = (position_1.IsVisible ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						Class181.smethod_3(Enum11.const_3, UI.getString_0(107351491) + string_2);
						((byte*)ptr)[1] = 1;
					}
					else
					{
						Class181.smethod_3(Enum11.const_3, UI.getString_0(107350958) + string_2);
						((byte*)ptr)[1] = 0;
					}
				}
			}
			catch (ExternalException)
			{
				position_1 = new Position();
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_4(Bitmap bitmap_3, out Position position_1, Images images_0, string string_2 = "")
		{
			void* ptr = stackalloc byte[2];
			Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
			position_1 = Class196.smethod_0(bitmap_3, tuple.Item1, tuple.Item2);
			*(byte*)ptr = (position_1.IsVisible ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107351491) + string_2);
				((byte*)ptr)[1] = 1;
			}
			else
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107350958) + string_2);
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_5(Bitmap bitmap_3, out Position position_1, Bitmap bitmap_4, string string_2, double double_0)
		{
			void* ptr = stackalloc byte[2];
			position_1 = Class196.smethod_0(bitmap_3, bitmap_4, double_0);
			*(byte*)ptr = (position_1.IsVisible ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107351491) + string_2);
				((byte*)ptr)[1] = 1;
			}
			else
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107350958) + string_2);
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static bool smethod_6(Bitmap bitmap_3, Images images_0, double double_0)
		{
			Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
			return Class196.smethod_0(bitmap_3, tuple.Item1, double_0).IsVisible;
		}

		public static bool smethod_7(Bitmap bitmap_3, Bitmap bitmap_4, double double_0)
		{
			return Class196.smethod_0(bitmap_3, bitmap_4, double_0).IsVisible;
		}

		public static bool smethod_8(Rectangle rectangle_0, Images images_0)
		{
			bool result;
			try
			{
				using (Bitmap bitmap = Class197.smethod_1(rectangle_0, UI.getString_0(107396942)))
				{
					Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
					result = Class196.smethod_0(bitmap, tuple.Item1, tuple.Item2).IsVisible;
				}
			}
			catch (ExternalException)
			{
				result = false;
			}
			return result;
		}

		public static bool smethod_9(Bitmap bitmap_3, Images images_0)
		{
			Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
			return Class196.smethod_0(bitmap_3, tuple.Item1, tuple.Item2).IsVisible;
		}

		public static bool smethod_10(Bitmap bitmap_3, Images images_0, out Position position_1)
		{
			Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
			position_1 = Class196.smethod_0(bitmap_3, tuple.Item1, tuple.Item2);
			return position_1.IsVisible;
		}

		public static List<Position> smethod_11(Images images_0, string string_2 = "")
		{
			List<Position> result;
			try
			{
				using (Bitmap gameImage = UI.GameImage)
				{
					Tuple<Bitmap, double> tuple = Class308.smethod_0(images_0);
					List<Position> list = Class196.smethod_1(gameImage, tuple.Item1, tuple.Item2);
					Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107350925), list.Count, string_2));
					result = list;
				}
			}
			catch (ExternalException)
			{
				result = new List<Position>();
			}
			return result;
		}

		public static List<Position> smethod_12(Bitmap bitmap_3, Bitmap bitmap_4, double double_0, string string_2 = "")
		{
			List<Position> result;
			try
			{
				List<Position> list = Class196.smethod_1(bitmap_3, bitmap_4, double_0);
				Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107350925), list.Count, string_2));
				result = list;
			}
			catch (ExternalException)
			{
				result = new List<Position>();
			}
			return result;
		}

		public unsafe static bool smethod_13(int int_4 = 1)
		{
			void* ptr = stackalloc byte[10];
			UI.mainForm_0.thread_3 = Thread.CurrentThread;
			UI.smethod_1();
			Win32.smethod_4(-2, -2, 50, 90, false);
			Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107350892), int_4));
			*(byte*)ptr = ((int_4 > 5) ? 1 : 0);
			bool result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107350867));
				UI.mainForm_0.method_63();
				result = false;
			}
			else
			{
				Position position;
				bool flag = !UI.smethod_3(out position, Images.GuildStashOpen, UI.getString_0(107350838)) && (UI.smethod_3(out position, Images.StashOpen, UI.getString_0(107350817)) || UI.smethod_3(out position, Images.HighlightItems, UI.getString_0(107350804)));
				((byte*)ptr)[1] = (flag ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107350783));
					((byte*)ptr)[2] = ((UI.bitmap_1 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						UI.bitmap_1 = Class197.smethod_1(Class251.StashTabBar, UI.getString_0(107396942));
						UI.bitmap_2 = Class197.smethod_1(Class251.StashTabBar, UI.getString_0(107396942));
					}
					result = true;
				}
				else
				{
					((byte*)ptr)[3] = (UI.smethod_80() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						result = UI.smethod_13(++int_4);
					}
					else
					{
						List<Position> list = UI.smethod_11(Images.StashTitle, UI.getString_0(107350726));
						list.RemoveAll(new Predicate<Position>(UI.<>c.<>9.method_0));
						((byte*)ptr)[4] = (list.Any<Position>() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 4) != 0)
						{
							Class181.smethod_3(Enum11.const_3, UI.getString_0(107350741));
							Position position2 = list.First<Position>();
							Win32.smethod_5(position2.smethod_4(Class251.StashChestOffset), false);
							Thread.Sleep(400);
							Win32.smethod_2(true);
							Thread.Sleep(150);
							Win32.smethod_4(-2, -2, 50, 90, false);
							DateTime t = DateTime.Now + new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_32]);
							do
							{
								((byte*)ptr)[6] = ((!UI.smethod_3(out position, Images.StashOpen, UI.getString_0(107351180))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 6) == 0)
								{
									break;
								}
								UI.smethod_36(Enum2.const_22, 1.0);
								((byte*)ptr)[5] = ((DateTime.Now > t) ? 1 : 0);
							}
							while (*(sbyte*)((byte*)ptr + 5) == 0);
							((byte*)ptr)[7] = (UI.smethod_3(out position, Images.StashOpen, UI.getString_0(107351191)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								((byte*)ptr)[8] = ((UI.bitmap_1 == null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 8) != 0)
								{
									UI.bitmap_1 = Class197.smethod_1(Class251.StashTabBar, UI.getString_0(107396942));
									UI.bitmap_2 = Class197.smethod_1(Class251.StashTabBar, UI.getString_0(107396942));
								}
								result = true;
							}
							else
							{
								result = UI.smethod_13(++int_4);
							}
						}
						else
						{
							Class181.smethod_3(Enum11.const_2, UI.getString_0(107351142));
							((byte*)ptr)[9] = ((!UI.smethod_14()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								result = UI.smethod_13(10);
							}
							else
							{
								UI.smethod_36(Enum2.const_32, 1.0);
								Win32.smethod_4(-2, -2, 50, 90, false);
								result = UI.smethod_13(++int_4);
							}
						}
					}
				}
			}
			return result;
		}

		public unsafe static bool smethod_14()
		{
			void* ptr = stackalloc byte[7];
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107351117));
			((byte*)ptr)[4] = (Position.smethod_0(UI.position_0, null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107351096));
				((byte*)ptr)[5] = 0;
			}
			else
			{
				Position position;
				*(byte*)ptr = (UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)) ? 1 : 0);
				((byte*)ptr)[1] = (UI.smethod_3(out position, Images.ChatOpen, UI.getString_0(107351014)) ? 1 : 0);
				((byte*)ptr)[2] = (UI.smethod_3(out position, Images.StashOpen, UI.getString_0(107350817)) ? 1 : 0);
				((byte*)ptr)[3] = (UI.smethod_69() ? 1 : 0);
				((byte*)ptr)[6] = (byte)(*(sbyte*)((byte*)ptr + 1) | *(sbyte*)ptr | *(sbyte*)((byte*)ptr + 2) | *(sbyte*)((byte*)ptr + 3));
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					Win32.smethod_14(UI.getString_0(107393936), false);
				}
				UI.smethod_36(Enum2.const_22, 1.0);
				Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107351033), UI.position_0));
				Position position2 = new Position(UI.position_0.Left - UI.PoeDimensions.Width / 2, UI.position_0.Top - UI.PoeDimensions.Height / 2);
				Position position_ = new Position(UI.PoeDimensions.Width / 2 - position2.Left, UI.PoeDimensions.Height / 2 - position2.Top);
				Win32.smethod_5(position_, false);
				UI.smethod_36(Enum2.const_22, 1.0);
				Win32.smethod_2(true);
				UI.position_0 = null;
				UI.smethod_36(Enum2.const_32, 1.0);
				((byte*)ptr)[5] = 1;
			}
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		public unsafe static void smethod_15()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = (UI.smethod_69() ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107351000));
				UI.smethod_36(Enum2.const_22, 1.0);
			}
			else
			{
				Class181.smethod_3(Enum11.const_0, UI.getString_0(107350459));
				Win32.smethod_14(UI.mainForm_0.string_0, false);
				DateTime t = DateTime.Now.AddSeconds(3.0);
				for (;;)
				{
					((byte*)ptr)[2] = ((DateTime.Now < t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						break;
					}
					((byte*)ptr)[1] = (UI.smethod_69() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						return;
					}
					Thread.Sleep(50);
				}
				Win32.smethod_14(UI.mainForm_0.string_0, false);
				Thread.Sleep(2000);
			}
		}

		public unsafe static bool smethod_16()
		{
			void* ptr = stackalloc byte[2];
			UI.smethod_17();
			Position position;
			*(byte*)ptr = (UI.smethod_3(out position, Images.Menu, UI.getString_0(107350402)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_0, UI.getString_0(107350425));
				((byte*)ptr)[1] = 1;
			}
			else
			{
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public static void smethod_17()
		{
			Position position;
			if (UI.smethod_3(out position, Images.Later, UI.getString_0(107350352)))
			{
				UI.smethod_36(Enum2.const_26, 1.0);
				Win32.smethod_14(UI.getString_0(107393936), false);
				Class181.smethod_3(Enum11.const_0, UI.getString_0(107350343));
				UI.smethod_36(Enum2.const_34, 1.0);
			}
		}

		public static void smethod_18()
		{
			UI.smethod_15();
			Position position;
			while (UI.smethod_3(out position, Images.RequestAcceptDecline, UI.getString_0(107350282)))
			{
				Position position_ = position.smethod_4(Class251.DeclineOffset);
				Win32.smethod_5(position_, false);
				UI.smethod_36(Enum2.const_27, 1.0);
				Win32.smethod_2(true);
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107350293));
				Thread.Sleep(300);
			}
			UI.smethod_36(Enum2.const_22, 1.0);
		}

		public unsafe static bool smethod_19()
		{
			void* ptr = stackalloc byte[3];
			Rectangle rectangle_ = new Rectangle(Class251.TradeWindow.X, Class251.TradeWindow.Y, Class251.TradeWindow.Width / 2, Class251.TradeWindow.Height);
			using (Bitmap bitmap = Class197.smethod_1(rectangle_, UI.getString_0(107396942)))
			{
				Position position;
				*(byte*)ptr = (UI.smethod_4(bitmap, out position, Images.TradeAcceptDisabled, UI.getString_0(107350236)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[2] = (UI.smethod_4(bitmap, out position, Images.TradeAccept, UI.getString_0(107350715)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[1] = 1;
					}
					else
					{
						((byte*)ptr)[1] = 0;
					}
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_20()
		{
			void* ptr = stackalloc byte[20];
			Rectangle rectangle_ = new Rectangle(Class251.TradeWindow.X, Class251.TradeWindow.Y, Class251.TradeWindow.Width / 2, Class251.TradeWindow.Height);
			using (Bitmap bitmap = Class197.smethod_1(rectangle_, UI.getString_0(107396942)))
			{
				Position position;
				((byte*)ptr)[16] = (UI.smethod_4(bitmap, out position, Images.TradeAcceptDisabled, UI.getString_0(107350662)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					((byte*)ptr)[17] = 0;
					goto IL_1AF;
				}
				((byte*)ptr)[18] = (UI.smethod_4(bitmap, out position, Images.TradeAccept, UI.getString_0(107350633)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					*(double*)ptr = UI.GameScale;
					*(int*)((byte*)ptr + 8) = (int)Math.Round(90.0 * *(double*)ptr);
					*(int*)((byte*)ptr + 12) = (int)Math.Round(17.0 * *(double*)ptr);
					Position position_ = position.smethod_6(rectangle_.X, rectangle_.Y).smethod_6(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12));
					Win32.smethod_5(position_, false);
					UI.position_0 = position_;
					Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107350648), UI.position_0));
					UI.smethod_36(Enum2.const_14, 1.0);
					((byte*)ptr)[19] = (Class255.class105_0.method_4(ConfigOptions.ScreenshotTrades) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						UI.smethod_107();
					}
					Win32.smethod_2(true);
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107350575));
					((byte*)ptr)[17] = 1;
					goto IL_1AF;
				}
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107350530));
			}
			((byte*)ptr)[17] = 0;
			IL_1AF:
			return *(sbyte*)((byte*)ptr + 17) != 0;
		}

		public static void smethod_21()
		{
			if (UI.smethod_70() || UI.smethod_71())
			{
				Win32.smethod_14(UI.getString_0(107393936), false);
			}
			UI.smethod_36(Enum2.const_22, 1.0);
			Position position;
			if (UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)))
			{
				Win32.smethod_14(UI.getString_0(107393936), false);
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107350469));
			}
		}

		public unsafe static bool smethod_22()
		{
			void* ptr = stackalloc byte[2];
			Position position;
			*(byte*)ptr = (UI.smethod_3(out position, Images.RequestAcceptDecline, UI.getString_0(107349948)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107349919));
				Win32.smethod_5(position.smethod_4(Class251.AcceptTradeOffset), false);
				UI.smethod_36(Enum2.const_17, 1.0);
				Win32.smethod_2(true);
				UI.smethod_36(Enum2.const_22, 1.0);
				((byte*)ptr)[1] = 1;
			}
			else
			{
				UI.smethod_36(Enum2.const_22, 1.0);
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_23()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = 0;
			for (;;)
			{
				Position position;
				((byte*)ptr)[1] = (UI.smethod_3(out position, Images.RequestAcceptDecline, UI.getString_0(107349874)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					break;
				}
				Win32.smethod_5(position.smethod_4(Class251.AcceptTradeOffset), false);
				UI.smethod_36(Enum2.const_17, 1.0);
				Win32.smethod_2(true);
				UI.smethod_36(Enum2.const_22, 1.0);
				*(byte*)ptr = 1;
			}
			((byte*)ptr)[2] = (byte)(*(sbyte*)ptr);
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public static string smethod_24(IntPtr intptr_1)
		{
			int windowTextLength = UI.GetWindowTextLength(intptr_1);
			StringBuilder stringBuilder = new StringBuilder(windowTextLength + 1);
			UI.GetWindowText(intptr_1, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		public unsafe static void smethod_25()
		{
			void* ptr = stackalloc byte[26];
			((byte*)ptr)[12] = 0;
			((byte*)ptr)[13] = 0;
			((byte*)ptr)[14] = 0;
			ProcessHelper.smethod_9();
			UI.mainForm_0.Invoke(new Action(UI.<>c.<>9.method_1));
			for (;;)
			{
				((byte*)ptr)[22] = ((!UI.smethod_24(UI.intptr_0).Contains(UI.getString_0(107350100))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 22) == 0)
				{
					break;
				}
				UI.intptr_0 = IntPtr.Zero;
				ProcessHelper.smethod_9();
				string text = UI.smethod_24(UI.intptr_0);
				((byte*)ptr)[15] = (text.Contains(UI.getString_0(107349857)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					((byte*)ptr)[16] = ((*(sbyte*)((byte*)ptr + 12) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						Class181.smethod_3(Enum11.const_0, UI.getString_0(107349844));
					}
					((byte*)ptr)[12] = 1;
					UI.mainForm_0.Invoke(new Action(UI.<>c.<>9.method_2));
					Thread.Sleep(1000);
				}
				((byte*)ptr)[17] = (text.Contains(UI.getString_0(107349791)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					((byte*)ptr)[18] = ((*(sbyte*)((byte*)ptr + 13) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						Class181.smethod_3(Enum11.const_0, UI.getString_0(107349742));
					}
					((byte*)ptr)[13] = 1;
					UI.bool_2 = true;
					UI.mainForm_0.Invoke(new Action(UI.<>c.<>9.method_3));
					Thread.Sleep(1000);
				}
				((byte*)ptr)[19] = (text.Contains(UI.getString_0(107349721)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 19) != 0)
				{
					UI.Class110 @class = new UI.Class110();
					UI.mainForm_0.Invoke(new Action(UI.<>c.<>9.method_4));
					((byte*)ptr)[20] = ((*(sbyte*)((byte*)ptr + 14) == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) != 0)
					{
						Class181.smethod_3(Enum11.const_0, UI.getString_0(107350188));
					}
					((byte*)ptr)[14] = 1;
					*(int*)ptr = text.IndexOf(UI.getString_0(107349721));
					*(int*)((byte*)ptr + 4) = text.IndexOf(UI.getString_0(107350147));
					*(int*)((byte*)ptr + 8) = text.IndexOf(UI.getString_0(107414953));
					@class.string_0 = text.Substring(*(int*)ptr + 7, *(int*)((byte*)ptr + 4) - *(int*)ptr - 7);
					@class.string_1 = text.Substring(0, *(int*)((byte*)ptr + 8));
					@class.double_0 = double.Parse(@class.string_1.Replace('.', Class120.char_0)) * 100.0;
					UI.mainForm_0.Invoke(new Action(@class.method_0));
					Thread.Sleep(1000);
				}
				((byte*)ptr)[21] = (text.Contains(UI.getString_0(107350174)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 21) != 0)
				{
					goto IL_307;
				}
			}
			goto IL_346;
			IL_307:
			UI.mainForm_0.Invoke(new Action(UI.<>c.<>9.method_5));
			Class181.smethod_3(Enum11.const_0, UI.getString_0(107350165));
			IL_346:
			Position position;
			((byte*)ptr)[23] = (UI.smethod_2(out position, Class238.Launch, UI.getString_0(107350079), 0.8) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 23) != 0)
			{
				((byte*)ptr)[24] = ((!UI.smethod_27()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 24) != 0)
				{
					Win32.smethod_4(position.Left + 46, position.Top + 16, 50, 90, false);
					Thread.Sleep(1000);
					Win32.smethod_2(true);
					Class181.smethod_3(Enum11.const_0, UI.getString_0(107350070));
					Thread.Sleep(1000);
					for (;;)
					{
						((byte*)ptr)[25] = (UI.smethod_28() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 25) == 0)
						{
							break;
						}
						Thread.Sleep(100);
					}
					UI.intptr_0 = IntPtr.Zero;
					ProcessHelper.smethod_9();
					Thread.Sleep(1000);
					UI.smethod_1();
					UI.bool_2 = false;
				}
			}
		}

		public unsafe static bool smethod_26()
		{
			void* ptr = stackalloc byte[6];
			Process[] processes = Process.GetProcesses();
			Process[] array = processes;
			*(int*)ptr = 0;
			while (*(int*)ptr < array.Length)
			{
				Process process = array[*(int*)ptr];
				((byte*)ptr)[4] = (process.ProcessName.Contains(UI.getString_0(107350037)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					ProcessHelper.smethod_9();
					((byte*)ptr)[5] = 1;
					IL_5A:
					return *(sbyte*)((byte*)ptr + 5) != 0;
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			((byte*)ptr)[5] = 0;
			goto IL_5A;
		}

		public unsafe static bool smethod_27()
		{
			void* ptr = stackalloc byte[2];
			Position position;
			*(byte*)ptr = (UI.smethod_2(out position, Class238.Launch, UI.getString_0(107349988), 0.8) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Color pixel = UI.GameImage.GetPixel(position.Left + 46, position.Top + 16);
				if (pixel.R == pixel.G && pixel.G == pixel.B)
				{
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107350011));
					((byte*)ptr)[1] = 1;
				}
				else
				{
					((byte*)ptr)[1] = 0;
				}
			}
			else
			{
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_28()
		{
			void* ptr = stackalloc byte[2];
			UI.smethod_29();
			*(byte*)ptr = ((UI.dateTime_0 == default(DateTime)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				UI.dateTime_0 = DateTime.Now.AddMinutes((double)Class255.class105_0.method_6(ConfigOptions.MaxGameLoadTime));
				Class181.smethod_2(Enum11.const_3, UI.getString_0(107349982), new object[]
				{
					UI.dateTime_0
				});
			}
			if (UI.mainForm_0.gameProcessState_0 != GameProcessState.Patching && DateTime.Now > UI.dateTime_0)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107382197));
				UI.dateTime_0 = default(DateTime);
				UI.mainForm_0.method_64(true);
				((byte*)ptr)[1] = 0;
			}
			else
			{
				GameProcessState gameProcessState_ = UI.mainForm_0.gameProcessState_0;
				GameProcessState gameProcessState = gameProcessState_;
				switch (gameProcessState)
				{
				case GameProcessState.NotRunning:
				case GameProcessState.FixingConfig:
					goto IL_10B;
				case GameProcessState.Running:
					break;
				case GameProcessState.Patching:
					UI.smethod_25();
					((byte*)ptr)[1] = 1;
					goto IL_110;
				default:
					if (gameProcessState == GameProcessState.Loading)
					{
						goto IL_10B;
					}
					break;
				}
				UI.dateTime_0 = default(DateTime);
				((byte*)ptr)[1] = 0;
				goto IL_110;
				IL_10B:
				((byte*)ptr)[1] = 1;
			}
			IL_110:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static GameProcessState smethod_29()
		{
			void* ptr = stackalloc byte[6];
			ProcessHelper.smethod_9();
			UI.smethod_1();
			*(byte*)ptr = ((!UI.smethod_26()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				UI.mainForm_0.gameProcessState_0 = GameProcessState.NotRunning;
			}
			else
			{
				((byte*)ptr)[1] = (UI.smethod_27() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					UI.mainForm_0.gameProcessState_0 = GameProcessState.Patching;
				}
				else
				{
					Position position;
					((byte*)ptr)[2] = (UI.smethod_2(out position, Class238.LeagueInformation, UI.getString_0(107382164), 0.8) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						UI.mainForm_0.gameProcessState_0 = GameProcessState.FixingConfig;
					}
					else
					{
						((byte*)ptr)[3] = (UI.smethod_3(out position, Images.Exit, UI.getString_0(107382131)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							UI.mainForm_0.gameProcessState_0 = GameProcessState.Login;
						}
						else
						{
							((byte*)ptr)[4] = (UI.smethod_3(out position, Images.CharacterSelect, UI.getString_0(107382082)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) != 0)
							{
								UI.mainForm_0.gameProcessState_0 = GameProcessState.CharacterSelect;
							}
							else
							{
								((byte*)ptr)[5] = (UI.smethod_3(out position, Images.Menu, UI.getString_0(107382053)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 5) != 0)
								{
									UI.mainForm_0.gameProcessState_0 = GameProcessState.Online;
								}
								else
								{
									UI.mainForm_0.gameProcessState_0 = GameProcessState.Loading;
								}
							}
						}
					}
				}
			}
			Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107382068), UI.mainForm_0.gameProcessState_0));
			return UI.mainForm_0.gameProcessState_0;
		}

		public unsafe static bool smethod_30()
		{
			void* ptr = stackalloc byte[22];
			ProcessHelper.smethod_9();
			UI.smethod_1();
			((byte*)ptr)[8] = (UI.smethod_27() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				UI.smethod_25();
			}
			Position position;
			((byte*)ptr)[9] = (UI.smethod_3(out position, Images.Exit, UI.getString_0(107382043)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 9) != 0)
			{
				UI.mainForm_0.Invoke(new Action(UI.<>c.<>9.method_6));
				Class181.smethod_3(Enum11.const_0, UI.getString_0(107382034));
				if (!string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.Email)) && !string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.Password)))
				{
					((byte*)ptr)[10] = (UI.smethod_3(out position, Images.GGGLogin, UI.getString_0(107382005)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						((byte*)ptr)[11] = (UI.smethod_3(out position, Images.Ok, UI.getString_0(107381960)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							Thread.Sleep(100);
							Win32.smethod_14(UI.getString_0(107381975), false);
							Thread.Sleep(100);
						}
						Win32.smethod_4(0, 0, 50, 90, true);
						Thread.Sleep(100);
						Win32.smethod_2(true);
						Thread.Sleep(100);
						Win32.smethod_14(UI.getString_0(107382442), false);
						Win32.smethod_16(Class255.class105_0.method_3(ConfigOptions.Email), false, false, false, false);
						UI.smethod_36(Enum2.const_22, 1.0);
						Win32.smethod_14(UI.getString_0(107382442), false);
						UI.smethod_36(Enum2.const_22, 1.0);
						Win32.smethod_16(Class255.class105_0.method_3(ConfigOptions.Password), false, false, false, false);
					}
				}
				Class181.smethod_3(Enum11.const_0, UI.getString_0(107382465));
				Win32.smethod_14(UI.getString_0(107381975), false);
				for (;;)
				{
					((byte*)ptr)[14] = (UI.smethod_3(out position, Images.Exit, UI.getString_0(107382366)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) == 0)
					{
						break;
					}
					((byte*)ptr)[12] = (UI.smethod_3(out position, Images.LoginError, UI.getString_0(107382404)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						goto IL_239;
					}
					Thread.Sleep(100);
				}
				goto IL_2A0;
				IL_239:
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107382419));
				((byte*)ptr)[13] = 0;
				goto IL_44F;
			}
			((byte*)ptr)[15] = ((!UI.smethod_3(out position, Images.CharacterSelect, UI.getString_0(107382321))) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 15) != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107382324));
				((byte*)ptr)[13] = 0;
				goto IL_44F;
			}
			IL_2A0:
			Thread.Sleep(500);
			UI.mainForm_0.CharacterName = Characters.smethod_1(Class255.Cookies);
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107382303) + UI.mainForm_0.CharacterName);
			((byte*)ptr)[16] = ((Class255.class105_0.method_5(ConfigOptions.CharacterSelected) == -1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107382270));
				((byte*)ptr)[13] = 0;
			}
			else
			{
				Class181.smethod_3(Enum11.const_0, string.Format(UI.getString_0(107381649), UI.mainForm_0.comboBox_0.smethod_1()));
				((byte*)ptr)[17] = ((Characters.int_0 < Class255.class105_0.method_5(ConfigOptions.CharacterSelected)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					*(int*)ptr = Characters.int_0;
					for (;;)
					{
						((byte*)ptr)[18] = ((*(int*)ptr < Class255.class105_0.method_5(ConfigOptions.CharacterSelected)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 18) == 0)
						{
							break;
						}
						Win32.smethod_14(UI.getString_0(107381616), false);
						Thread.Sleep(100);
						*(int*)ptr = *(int*)ptr + 1;
					}
				}
				((byte*)ptr)[19] = ((Characters.int_0 > Class255.class105_0.method_5(ConfigOptions.CharacterSelected)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 19) != 0)
				{
					*(int*)((byte*)ptr + 4) = Class255.class105_0.method_5(ConfigOptions.CharacterSelected);
					for (;;)
					{
						((byte*)ptr)[20] = ((*(int*)((byte*)ptr + 4) < Characters.int_0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 20) == 0)
						{
							break;
						}
						Win32.smethod_14(UI.getString_0(107381607), false);
						Thread.Sleep(100);
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
				}
				Win32.smethod_14(UI.getString_0(107381975), false);
				for (;;)
				{
					((byte*)ptr)[21] = (UI.smethod_28() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 21) == 0)
					{
						break;
					}
					Thread.Sleep(100);
				}
				((byte*)ptr)[13] = 1;
			}
			IL_44F:
			return *(sbyte*)((byte*)ptr + 13) != 0;
		}

		public unsafe static List<Item> smethod_31(bool bool_3 = false, int int_4 = 60, int int_5 = 12, int int_6 = 5)
		{
			void* ptr = stackalloc byte[30];
			ProcessHelper.smethod_13(true);
			*(int*)ptr = 0;
			UI.mainForm_0.list_14.Clear();
			Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.EmptyCellInner);
			UI.smethod_15();
			Win32.smethod_4(-2, -2, 50, 90, false);
			((byte*)ptr)[16] = (UI.smethod_72() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				UI.smethod_18();
				UI.smethod_36(Enum2.const_22, 1.0);
			}
			Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107381630), new object[]
			{
				bool_3,
				int_4,
				int_5,
				int_6
			}));
			((byte*)ptr)[17] = ((!UI.SmallChat) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 17) != 0)
			{
				Win32.smethod_16(UI.getString_0(107381545), true, true, false, false);
			}
			UI.smethod_36(Enum2.const_30, 1.0);
			Bitmap gameImage = UI.GameImage;
			*(int*)((byte*)ptr + 4) = 0;
			UI.Class111 @class = new UI.Class111();
			@class.int_0 = 0;
			for (;;)
			{
				((byte*)ptr)[29] = ((@class.int_0 < int_5) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 29) == 0)
				{
					break;
				}
				UI.Class112 class2 = new UI.Class112();
				class2.class111_0 = @class;
				class2.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[27] = ((class2.int_0 < int_6) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 27) == 0)
					{
						break;
					}
					((byte*)ptr)[18] = ((*(int*)((byte*)ptr + 4) > 2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						goto IL_5B9;
					}
					Position position = UI.smethod_47(Enum10.const_2, class2.class111_0.int_0, class2.int_0);
					*(int*)((byte*)ptr + 8) = (int)UI.smethod_48(Enum10.const_2);
					Position position2;
					using (Bitmap bitmap = Class197.smethod_0(gameImage, position.Left, position.Top, *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 8), UI.getString_0(107396942)))
					{
						position2 = Class196.smethod_0(bitmap, tuple.Item1, tuple.Item2);
					}
					((byte*)ptr)[19] = ((!bool_3) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						((byte*)ptr)[20] = ((!position2.IsVisible) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 20) != 0)
						{
							UI.Class113 class3 = new UI.Class113();
							class3.class112_0 = class2;
							UI.smethod_32(class3.class112_0.class111_0.int_0, class3.class112_0.int_0, Enum2.const_3, true);
							class3.item_0 = new Item();
							class3.item_0.text = Win32.smethod_21();
							Win32.smethod_4(-2, -2, 50, 90, false);
							((byte*)ptr)[21] = ((class3.item_0.text != UI.getString_0(107381568)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 21) != 0)
							{
								class3.item_0.method_0();
								IEnumerable<Item> enumerable = UI.mainForm_0.list_14.Where(new Func<Item, bool>(class3.method_0));
								((byte*)ptr)[22] = (enumerable.Any<Item>() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 22) != 0)
								{
									using (IEnumerator<Item> enumerator = enumerable.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											Item item = enumerator.Current;
											item.smallestX = Math.Min(item.smallestX, class3.class112_0.class111_0.int_0);
											item.largestX = Math.Max(item.largestX, class3.class112_0.class111_0.int_0);
											item.smallestY = Math.Min(item.smallestY, class3.class112_0.int_0);
											item.largestY = Math.Max(item.largestY, class3.class112_0.int_0);
											item.Left = item.smallestX;
											item.Top = item.smallestY;
										}
										goto IL_430;
									}
									goto IL_377;
								}
								goto IL_377;
								IL_430:
								*(int*)ptr = *(int*)ptr + 1;
								((byte*)ptr)[23] = ((*(int*)ptr >= int_4) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 23) != 0)
								{
									break;
								}
								goto IL_540;
								IL_377:
								Size size = ItemData.smethod_2(class3.item_0.typeLine);
								class3.item_0.width = size.Width;
								class3.item_0.height = size.Height;
								class3.item_0.Left = class3.class112_0.class111_0.int_0;
								class3.item_0.Top = class3.class112_0.int_0;
								class3.item_0.smallestX = class3.class112_0.class111_0.int_0;
								class3.item_0.smallestY = class3.class112_0.int_0;
								UI.mainForm_0.list_14.Add(class3.item_0);
								goto IL_430;
							}
							((byte*)ptr)[24] = (UI.smethod_72() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 24) != 0)
							{
								Class181.smethod_3(Enum11.const_3, UI.getString_0(107381559));
								UI.smethod_18();
								UI.smethod_36(Enum2.const_22, 1.0);
								Win32.smethod_4(-2, -2, 50, 90, false);
								UI.smethod_36(Enum2.const_22, 1.0);
								gameImage = UI.GameImage;
								*(int*)((byte*)ptr + 12) = class3.class112_0.int_0;
								class3.class112_0.int_0 = *(int*)((byte*)ptr + 12) - 1;
							}
							else
							{
								Class181.smethod_3(Enum11.const_3, UI.getString_0(107381490));
								*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
							}
						}
					}
					else
					{
						((byte*)ptr)[25] = (position2.IsVisible ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 25) != 0)
						{
							UI.smethod_32(class2.class111_0.int_0, class2.int_0, Enum2.const_3, true);
							*(int*)ptr = *(int*)ptr + 1;
						}
						((byte*)ptr)[26] = ((*(int*)ptr >= int_4) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 26) != 0)
						{
							break;
						}
					}
					IL_540:
					*(int*)((byte*)ptr + 12) = class2.int_0;
					class2.int_0 = *(int*)((byte*)ptr + 12) + 1;
				}
				IL_573:
				((byte*)ptr)[28] = ((*(int*)ptr >= int_4) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 28) == 0)
				{
					*(int*)((byte*)ptr + 12) = @class.int_0;
					@class.int_0 = *(int*)((byte*)ptr + 12) + 1;
					continue;
				}
				break;
				goto IL_573;
			}
			goto IL_5C5;
			IL_5B9:
			return new List<Item>();
			IL_5C5:
			Class181.smethod_2(Enum11.const_3, UI.getString_0(107381929), new object[]
			{
				UI.mainForm_0.list_14.Count
			});
			foreach (Item item2 in UI.mainForm_0.list_14)
			{
				Class181.smethod_2(Enum11.const_3, UI.getString_0(107381940), new object[]
				{
					item2.Left,
					item2.Top,
					item2.Name,
					item2.stack,
					item2.width,
					item2.height
				});
			}
			gameImage.smethod_12();
			return UI.mainForm_0.list_14;
		}

		public unsafe static Position smethod_32(int int_4, int int_5, Enum2 enum2_0 = Enum2.const_3, bool bool_3 = true)
		{
			void* ptr = stackalloc byte[14];
			Position position = UI.smethod_46(Enum10.const_2, int_4, int_5);
			*(double*)ptr = 1.0;
			((byte*)ptr)[12] = ((enum2_0 == Enum2.const_4) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				*(int*)((byte*)ptr + 8) = (int)Class255.class105_0.method_6(ConfigOptions.ClickItemToTrade);
				Win32.smethod_4(position.X, position.Y, *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 8) + 50, false);
			}
			else
			{
				Win32.smethod_5(position, false);
			}
			if (bool_3 && int_4 >= 5)
			{
				using (Bitmap bitmap = Class197.smethod_1(Class251.TradeOpen, UI.getString_0(107396942)))
				{
					using (Bitmap bitmap2 = Class197.smethod_1(Class251.InventoryWindow, UI.getString_0(107396942)))
					{
						if (TradeProcessor.order_0 == null && UI.smethod_9(bitmap, Images.TradeOpen))
						{
							Win32.smethod_14(UI.getString_0(107393936), false);
							UI.smethod_36(Enum2.const_22, 1.0);
						}
						((byte*)ptr)[13] = (UI.smethod_9(bitmap2, Images.RequestAcceptDecline) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							UI.smethod_18();
							Thread.Sleep(400);
							Win32.smethod_5(position, false);
						}
					}
				}
				*(double*)ptr = 0.5;
			}
			UI.smethod_36(enum2_0, *(double*)ptr);
			return position;
		}

		public static Position smethod_33(int int_4, int int_5)
		{
			Position position = UI.smethod_46(Enum10.const_3, int_4, int_5);
			Win32.smethod_5(position, false);
			UI.smethod_36(Enum2.const_5, 1.0);
			return position;
		}

		public unsafe static Position smethod_34(string string_2, int int_4, int int_5, Enum2 enum2_0 = Enum2.const_2, bool bool_3 = false)
		{
			void* ptr = stackalloc byte[4];
			Position position;
			if (string_2 == UI.getString_0(107381795) || string_2 == UI.getString_0(107381810))
			{
				position = UI.smethod_46(Enum10.const_0, int_4, int_5);
				Win32.smethod_5(position, bool_3);
			}
			else
			{
				*(byte*)ptr = ((string_2 == UI.getString_0(107381793)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					position = UI.smethod_46(Enum10.const_1, int_4, int_5);
					Win32.smethod_5(position, bool_3);
				}
				else
				{
					JObject jobject = UI.smethod_62(string_2);
					((byte*)ptr)[1] = ((jobject == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						return null;
					}
					position = UI.smethod_49(jobject, int_4);
					((byte*)ptr)[2] = ((string_2 == UI.getString_0(107381780)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						UI.smethod_95(int_4, 0);
					}
					((byte*)ptr)[3] = ((string_2 == UI.getString_0(107393855)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						UI.smethod_96(int_4);
					}
					Win32.smethod_5(position, bool_3);
				}
			}
			UI.smethod_36(enum2_0, 1.0);
			return position;
		}

		public unsafe static void smethod_35(int int_4, bool bool_3 = false, int int_5 = 1)
		{
			void* ptr = stackalloc byte[25];
			Class181.smethod_2(Enum11.const_3, UI.getString_0(107381759), new object[]
			{
				int_4,
				int_5
			});
			((byte*)ptr)[12] = ((!UI.smethod_13(1)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) == 0)
			{
				((byte*)ptr)[13] = ((int_5 >= 5) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107381162));
				}
				else if (bool_3 && int_4 == Stashes.int_0)
				{
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107381101));
				}
				else
				{
					Position position;
					((byte*)ptr)[14] = (UI.smethod_3(out position, Images.ChatOpen, UI.getString_0(107351014)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						Class181.smethod_3(Enum11.const_3, UI.getString_0(107381076));
						Win32.smethod_14(UI.getString_0(107393936), false);
						UI.smethod_36(Enum2.const_22, 1.0);
					}
					Bitmap bitmap;
					if (Stashes.int_0 == int_4 && UI.bitmap_1 != null)
					{
						Bitmap bitmap2;
						bitmap = (bitmap2 = Class197.smethod_1(Class251.StashTabBar, UI.getString_0(107396942)));
						try
						{
							((byte*)ptr)[15] = ((!UI.smethod_59(UI.bitmap_1, bitmap, UI.getString_0(107396942), 0.4, 0).Any<Rectangle>()) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 15) != 0)
							{
								Class181.smethod_3(Enum11.const_3, UI.getString_0(107381019));
								return;
							}
						}
						finally
						{
							if (bitmap2 != null)
							{
								((IDisposable)bitmap2).Dispose();
							}
						}
					}
					Position position2;
					((byte*)ptr)[16] = (UI.smethod_3(out position2, Images.StashArrow, UI.getString_0(107381418)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						Position position3;
						((byte*)ptr)[17] = (UI.smethod_3(out position3, Images.Padlock, UI.getString_0(107381433)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							Bitmap item = Class308.smethod_0(Images.Padlock).Item1;
							Win32.smethod_4(position3.X + item.Width / 2, position3.Y + item.Height, 50, 90, false);
							Thread.Sleep(400);
							Win32.smethod_2(true);
							Thread.Sleep(400);
							Win32.smethod_4(position2.Left + 3, position2.Top + 3, 50, 90, false);
							UI.smethod_36(Enum2.const_0, 1.0);
							Win32.smethod_2(true);
							Thread.Sleep(500);
						}
						else
						{
							Win32.smethod_4(position2.Left + 3, position2.Top + 3, 50, 90, false);
							UI.smethod_36(Enum2.const_0, 1.0);
						}
						Win32.smethod_2(true);
						UI.smethod_36(Enum2.const_0, 1.0);
						((byte*)ptr)[18] = ((int_4 == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 18) != 0)
						{
							Win32.smethod_14(UI.getString_0(107381616), false);
						}
						else
						{
							JsonTab jsonTab = Stashes.smethod_11(int_4);
							bool flag = jsonTab == null || jsonTab.n.smethod_25() || jsonTab.n.StartsWith(UI.getString_0(107381388));
							((byte*)ptr)[19] = (flag ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) != 0)
							{
								*(int*)ptr = -1;
								for (;;)
								{
									((byte*)ptr)[20] = ((*(int*)ptr < int_4) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 20) == 0)
									{
										break;
									}
									Win32.smethod_14(UI.getString_0(107381616), false);
									*(int*)ptr = *(int*)ptr + 1;
								}
							}
							else
							{
								char c = jsonTab.n[0];
								foreach (JsonTab jsonTab2 in Stashes.Tabs.Skip(1))
								{
									((byte*)ptr)[21] = (jsonTab2.n.ToLower().StartsWith(c.ToString().ToLower()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 21) != 0)
									{
										Win32.smethod_14(string.Format(UI.getString_0(107381383), c.ToString()), false);
									}
									((byte*)ptr)[22] = ((jsonTab2 == jsonTab) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 22) != 0)
									{
										break;
									}
								}
							}
						}
						UI.smethod_36(Enum2.const_0, 1.0);
						Win32.smethod_14(UI.getString_0(107381975), false);
						UI.smethod_36(Enum2.const_0, 1.0);
						Win32.smethod_2(true);
						Win32.smethod_5(position2.smethod_6(0, -(int)Math.Round(18.0 * UI.GameScale)), false);
					}
					else
					{
						Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107381402), Stashes.Tabs.Count));
						*(int*)((byte*)ptr + 4) = 0;
						for (;;)
						{
							((byte*)ptr)[23] = ((*(int*)((byte*)ptr + 4) < Stashes.Tabs.Count) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 23) == 0)
							{
								break;
							}
							Win32.smethod_14(UI.getString_0(107381317), false);
							UI.smethod_36(Enum2.const_0, 1.0);
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						}
						*(int*)((byte*)ptr + 8) = 0;
						for (;;)
						{
							((byte*)ptr)[24] = ((*(int*)((byte*)ptr + 8) < int_4) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 24) == 0)
							{
								break;
							}
							Win32.smethod_14(UI.getString_0(107381340), false);
							UI.smethod_36(Enum2.const_0, 1.0);
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
						}
					}
					UI.smethod_36(Enum2.const_1, 1.0);
					Bitmap bitmap3;
					bitmap = (bitmap3 = Class197.smethod_1(Class251.StashTabBar, UI.getString_0(107396942)));
					try
					{
						if (UI.mainForm_0.genum0_0 == MainForm.GEnum0.const_2 && !UI.smethod_59(UI.bitmap_1, bitmap, UI.getString_0(107396942), 0.4, 0).Any<Rectangle>())
						{
							Class181.smethod_3(Enum11.const_3, UI.getString_0(107381295));
							UI.smethod_35(int_4, bool_3, ++int_5);
						}
						else
						{
							Stashes.int_0 = int_4;
							UI.bitmap_1 = new Bitmap(bitmap);
						}
					}
					finally
					{
						if (bitmap3 != null)
						{
							((IDisposable)bitmap3).Dispose();
						}
					}
				}
			}
		}

		public static Bitmap GameImage
		{
			get
			{
				return Class197.GameImage;
			}
		}

		public static void smethod_36(Enum2 enum2_0, double double_0 = 1.0)
		{
			if (Class255.class105_0.method_4(ConfigOptions.PredictServerLag))
			{
				Thread.Sleep((int)Math.Round((double)UI.mainForm_0.long_0 + (double)Class120.dictionary_0[enum2_0] * double_0));
			}
			else
			{
				Thread.Sleep((int)Math.Round((double)Class120.dictionary_0[enum2_0] * double_0));
			}
		}

		public unsafe static bool smethod_37(string string_2)
		{
			void* ptr = stackalloc byte[3];
			Win32.smethod_22(string_2, false);
			Win32.smethod_3();
			UI.smethod_36(Enum2.const_18, 1.0);
			DateTime t = DateTime.Now + new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_18]);
			Position position = null;
			for (;;)
			{
				((byte*)ptr)[2] = ((!UI.smethod_3(out position, Images.Note, UI.getString_0(107381218))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) == 0)
				{
					break;
				}
				Thread.Sleep(20);
				*(byte*)ptr = ((DateTime.Now > t) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					goto IL_E5;
				}
			}
			UI.smethod_36(Enum2.const_18, 1.0);
			Win32.smethod_14(UI.getString_0(107381237), false);
			UI.smethod_36(Enum2.const_18, 1.0);
			Win32.smethod_14(UI.getString_0(107381975), false);
			UI.smethod_36(Enum2.const_18, 1.0);
			((byte*)ptr)[1] = 1;
			goto IL_EA;
			IL_E5:
			((byte*)ptr)[1] = 0;
			IL_EA:
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static bool smethod_38(string string_2)
		{
			void* ptr = stackalloc byte[9];
			Class181.smethod_2(Enum11.const_3, UI.getString_0(107381196), new object[]
			{
				string_2
			});
			((byte*)ptr)[2] = ((!string.IsNullOrEmpty(string_2)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				Win32.smethod_22(string_2, false);
			}
			UI.smethod_36(Enum2.const_18, 1.0);
			Win32.smethod_3();
			UI.smethod_36(Enum2.const_18, 1.0);
			Position position = null;
			*(byte*)ptr = 0;
			((byte*)ptr)[1] = 0;
			DateTime t = DateTime.Now + new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_18] * 5);
			for (;;)
			{
				((byte*)ptr)[4] = ((DateTime.Now < t) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) == 0)
				{
					break;
				}
				if (UI.smethod_3(out position, Images.ExactPrice, UI.getString_0(107380659)) || UI.smethod_3(out position, Images.NegotiablePrice, UI.getString_0(107380610)))
				{
					goto IL_FF;
				}
				((byte*)ptr)[3] = (UI.smethod_3(out position, Images.Note, UI.getString_0(107380585)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					goto IL_104;
				}
			}
			goto IL_10C;
			IL_FF:
			*(byte*)ptr = 1;
			goto IL_10C;
			IL_104:
			*(byte*)ptr = 1;
			((byte*)ptr)[1] = 1;
			IL_10C:
			((byte*)ptr)[5] = ((*(sbyte*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				((byte*)ptr)[6] = 0;
			}
			else
			{
				Bitmap item = Class308.smethod_0(Images.ExactPrice).Item1;
				((byte*)ptr)[7] = ((*(sbyte*)((byte*)ptr + 1) == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					Win32.smethod_4(position.Left + item.Width / 2, position.Top + item.Height / 2, 50, 90, false);
					UI.smethod_36(Enum2.const_18, 1.0);
					Win32.smethod_2(true);
					Win32.smethod_14(UI.getString_0(107381607), false);
					Win32.smethod_14(UI.getString_0(107381607), false);
					UI.smethod_36(Enum2.const_18, 1.0);
					Win32.smethod_14(UI.getString_0(107381975), false);
					UI.smethod_36(Enum2.const_18, 1.0);
				}
				Win32.smethod_4(position.Left + item.Width + 20, position.Top + item.Height / 2, 50, 90, false);
				UI.smethod_36(Enum2.const_18, 1.0);
				Win32.smethod_2(true);
				UI.smethod_36(Enum2.const_18, 1.0);
				Win32.smethod_14(UI.getString_0(107380608), false);
				UI.smethod_36(Enum2.const_18, 1.0);
				((byte*)ptr)[8] = ((!string.IsNullOrEmpty(string_2)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					Win32.smethod_14(UI.getString_0(107381237), false);
				}
				else
				{
					Win32.smethod_14(UI.getString_0(107380599), false);
				}
				UI.smethod_36(Enum2.const_18, 1.0);
				Win32.smethod_14(UI.getString_0(107381975), false);
				UI.smethod_36(Enum2.const_18, 1.0);
				((byte*)ptr)[6] = 1;
			}
			return *(sbyte*)((byte*)ptr + 6) != 0;
		}

		public static void smethod_39(Player player_0, string string_2)
		{
			UI.smethod_36(Enum2.const_15, 1.0);
			Win32.smethod_16(string.Format(UI.getString_0(107397749), player_0.name, string_2), true, true, false, false);
		}

		public unsafe static void smethod_40(Player player_0)
		{
			void* ptr = stackalloc byte[5];
			UI.Class114 @class = new UI.Class114();
			@class.player_0 = player_0;
			UI.smethod_1();
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107380550) + @class.player_0.name);
			((byte*)ptr)[4] = (UI.mainForm_0.list_13.Any(new Func<Player, bool>(@class.method_0)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				Win32.smethod_16(UI.getString_0(107380569) + @class.player_0.name, true, true, false, false);
			}
			Enum11 enum11_ = Enum11.const_3;
			string str = UI.getString_0(107380528);
			*(int*)ptr = UI.mainForm_0.list_13.Count<Player>() + 1;
			Class181.smethod_3(enum11_, str + ((int*)ptr)->ToString());
			UI.mainForm_0.bool_6 = false;
			UI.mainForm_0.list_13.RemoveAll(new Predicate<Player>(@class.method_1));
			UI.smethod_36(Enum2.const_13, 1.0);
		}

		public static void smethod_41()
		{
			UI.smethod_1();
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107380550) + UI.mainForm_0.CharacterName);
			Win32.smethod_16(UI.getString_0(107380569) + UI.mainForm_0.CharacterName, true, true, false, false);
		}

		public static void smethod_42(Player player_0)
		{
			Win32.smethod_16(UI.getString_0(107397687) + player_0.name, true, true, false, false);
			UI.mainForm_0.list_13.Add(player_0);
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107380528) + (UI.mainForm_0.list_13.Count<Player>() + 1).ToString());
		}

		public unsafe static void smethod_43()
		{
			void* ptr = stackalloc byte[10];
			Win32.smethod_4(-2, -2, 50, 90, false);
			*(int*)ptr = UI.smethod_83(12);
			List<JsonItem> list = new List<JsonItem>();
			*(int*)((byte*)ptr + 4) = 0;
			for (;;)
			{
				((byte*)ptr)[8] = ((*(int*)((byte*)ptr + 4) < *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) == 0)
				{
					break;
				}
				JsonItem item = new JsonItem
				{
					w = 1,
					h = 1
				};
				list.Add(item);
				*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
			}
			list = InventoryManager.smethod_5(list);
			JsonItem jsonItem = TradeProcessor.Inventory.Last<JsonItem>();
			List<Position> source = InventoryManager.smethod_8(list, jsonItem);
			((byte*)ptr)[9] = (source.Any<Position>() ? 1 : 0);
			Position position;
			if (*(sbyte*)((byte*)ptr + 9) != 0)
			{
				position = source.First<Position>();
				jsonItem.x = position.x;
				jsonItem.y = position.y;
			}
			else
			{
				position = new Position(jsonItem.x, jsonItem.y);
			}
			jsonItem.x = position.x;
			jsonItem.y = position.y;
			UI.smethod_32(position.x, position.y, Enum2.const_3, true);
			Win32.smethod_2(true);
		}

		public unsafe static bool smethod_44(string string_2 = null)
		{
			void* ptr = stackalloc byte[13];
			((byte*)ptr)[4] = ((string_2 == null) ? 1 : 0);
			string text;
			string text2;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				text = UI.getString_0(107380535);
				text2 = UI.getString_0(107380498);
			}
			else
			{
				text = string.Format(UI.getString_0(107380453), string_2);
				text2 = string.Format(UI.getString_0(107380440), string_2);
			}
			Class181.smethod_3(Enum11.const_0, text);
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[11] = ((*(int*)ptr < 3) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) == 0)
				{
					break;
				}
				UI.smethod_1();
				Win32.smethod_18();
				Win32.smethod_20();
				Position position;
				((byte*)ptr)[5] = (UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					Win32.smethod_14(UI.getString_0(107393936), false);
					UI.smethod_36(Enum2.const_34, 1.0);
				}
				Win32.smethod_16(text2, true, true, true, false);
				Thread.Sleep(3000);
				for (;;)
				{
					((byte*)ptr)[6] = (UI.smethod_28() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) == 0)
					{
						break;
					}
					Thread.Sleep(100);
				}
				((byte*)ptr)[7] = ((UI.mainForm_0.enum13_0 == Enum13.const_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					goto IL_1A3;
				}
				if (UI.mainForm_0.enum13_0 == Enum13.const_1 || UI.mainForm_0.enum13_0 == Enum13.const_3)
				{
					goto IL_1E0;
				}
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107380828));
				*(int*)ptr = *(int*)ptr + 1;
			}
			((byte*)ptr)[12] = ((string_2 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107380711));
				UI.mainForm_0.method_63();
				((byte*)ptr)[9] = 0;
				goto IL_23F;
			}
			((byte*)ptr)[9] = 0;
			goto IL_23F;
			IL_1A3:
			((byte*)ptr)[8] = ((string_2 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107380903));
				UI.mainForm_0.method_63();
				((byte*)ptr)[9] = 0;
				goto IL_23F;
			}
			((byte*)ptr)[9] = 0;
			goto IL_23F;
			IL_1E0:
			UI.mainForm_0.enum13_0 = Enum13.const_0;
			((byte*)ptr)[10] = (UI.mainForm_0.bool_7 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 10) != 0)
			{
				UI.mainForm_0.bool_7 = false;
				Class181.smethod_3(Enum11.const_0, UI.getString_0(107380842));
				Win32.smethod_16(UI.getString_0(107380805), true, true, false, false);
			}
			((byte*)ptr)[9] = 1;
			IL_23F:
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		public static void smethod_45(JsonItem jsonItem_0, int int_4)
		{
			Win32.smethod_10();
			UI.smethod_36(Enum2.const_24, 1.0);
			Win32.smethod_15(int_4);
			UI.smethod_36(Enum2.const_24, 1.0);
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107380678) + jsonItem_0.Left.ToString() + UI.getString_0(107392566) + jsonItem_0.Top.ToString());
		}

		public static bool IsFullScreen
		{
			get
			{
				return UI.struct2_1.Equals(UI.struct2_0);
			}
		}

		public unsafe static Position smethod_46(Enum10 enum10_0, int int_4, int int_5)
		{
			void* ptr = stackalloc byte[56];
			Point point = default(Point);
			Position result;
			if (int_4 == -1 && int_5 == -1)
			{
				result = new Position();
			}
			else
			{
				switch (enum10_0)
				{
				case Enum10.const_0:
					point = Class251.StashOffset;
					break;
				case Enum10.const_1:
					point = Class251.StashOffset;
					break;
				case Enum10.const_2:
					point = Class251.InventoryOffset;
					break;
				case Enum10.const_3:
					point = Class251.TradeWindowItemStartOffset;
					break;
				}
				*(double*)ptr = UI.smethod_48(enum10_0);
				*(double*)((byte*)ptr + 8) = *(double*)ptr * (double)int_4;
				*(double*)((byte*)ptr + 16) = *(double*)ptr * (double)int_5;
				*(double*)((byte*)ptr + 24) = *(double*)ptr / 2.0;
				*(double*)((byte*)ptr + 32) = *(double*)ptr / 2.0;
				Point windowOffset = Class251.WindowOffset;
				*(double*)((byte*)ptr + 40) = windowOffset.X + point.X + *(double*)((byte*)ptr + 24) + *(double*)((byte*)ptr + 8);
				windowOffset = Class251.WindowOffset;
				*(double*)((byte*)ptr + 48) = windowOffset.Y + point.Y + *(double*)((byte*)ptr + 32) + *(double*)((byte*)ptr + 16);
				result = new Position((int)Math.Round(*(double*)((byte*)ptr + 40)), (int)Math.Round(*(double*)((byte*)ptr + 48)));
			}
			return result;
		}

		public unsafe static Position smethod_47(Enum10 enum10_0, int int_4, int int_5)
		{
			void* ptr = stackalloc byte[40];
			Point point = default(Point);
			switch (enum10_0)
			{
			case Enum10.const_0:
			case Enum10.const_1:
			case Enum10.const_4:
				point = Class251.StashOffset;
				break;
			case Enum10.const_2:
				point = Class251.InventoryOffset;
				break;
			case Enum10.const_3:
				point = Class251.TradeWindowItemStartOffset;
				break;
			}
			*(double*)ptr = UI.smethod_48(enum10_0);
			*(double*)((byte*)ptr + 8) = *(double*)ptr * (double)int_4;
			*(double*)((byte*)ptr + 16) = *(double*)ptr * (double)int_5;
			Point windowOffset = Class251.WindowOffset;
			*(double*)((byte*)ptr + 24) = windowOffset.X + point.X + *(double*)((byte*)ptr + 8);
			windowOffset = Class251.WindowOffset;
			*(double*)((byte*)ptr + 32) = windowOffset.Y + point.Y + *(double*)((byte*)ptr + 16);
			return new Position((int)Math.Round(*(double*)((byte*)ptr + 24)), (int)Math.Round(*(double*)((byte*)ptr + 32)));
		}

		public unsafe static double smethod_48(Enum10 enum10_0)
		{
			void* ptr = stackalloc byte[20];
			*(int*)((byte*)ptr + 16) = 12;
			*(double*)ptr = 632.0;
			switch (enum10_0)
			{
			case Enum10.const_1:
				*(int*)((byte*)ptr + 16) = 24;
				break;
			case Enum10.const_3:
				*(double*)ptr = 628.0;
				break;
			case Enum10.const_4:
				*(double*)((byte*)ptr + 8) = Class251.ItemOffset;
				goto IL_6A;
			}
			*(double*)((byte*)ptr + 8) = *(double*)ptr / (double)(*(int*)((byte*)ptr + 16)) * UI.GameScale;
			IL_6A:
			return *(double*)((byte*)ptr + 8);
		}

		public unsafe static double GameScale
		{
			get
			{
				void* ptr = stackalloc byte[9];
				((byte*)ptr)[8] = (UI.IsFullScreen ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					*(double*)ptr = 1.0;
				}
				else
				{
					*(double*)ptr = (double)UI.PoeDimensions.Height / 1080.0;
				}
				return *(double*)ptr;
			}
		}

		public unsafe static Position smethod_49(JObject jobject_0, int int_4)
		{
			void* ptr = stackalloc byte[51];
			((byte*)ptr)[48] = ((int_4 == -1) ? 1 : 0);
			Position result;
			if (*(sbyte*)((byte*)ptr + 48) != 0)
			{
				result = new Position();
			}
			else
			{
				((byte*)ptr)[49] = (jobject_0.ContainsKey(UI.getString_0(107380141)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 49) != 0)
				{
					jobject_0 = (JObject)jobject_0[UI.getString_0(107380141)];
				}
				*(double*)ptr = Class251.ItemOffset / 2.0;
				*(double*)((byte*)ptr + 8) = Class251.ItemOffset / 2.0;
				JToken jtoken = jobject_0[int_4.ToString()];
				((byte*)ptr)[50] = ((jtoken[UI.getString_0(107380132)] != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 50) != 0)
				{
					*(double*)((byte*)ptr + 32) = (double)jobject_0[UI.getString_0(107397904)][UI.getString_0(107380132)];
					*(double*)((byte*)ptr + 40) = (double)jtoken[UI.getString_0(107380132)] / *(double*)((byte*)ptr + 32);
					*(double*)ptr = *(double*)ptr * *(double*)((byte*)ptr + 40);
					*(double*)((byte*)ptr + 8) = *(double*)((byte*)ptr + 8) * *(double*)((byte*)ptr + 40);
				}
				*(double*)((byte*)ptr + 16) = (double)jtoken[UI.getString_0(107397007)] * Class251.SpecialTabItemWidth * UI.GameScale + Class251.StashOffset.X + Class251.WindowOffset.X + *(double*)ptr;
				*(double*)((byte*)ptr + 24) = (double)jtoken[UI.getString_0(107380155)] * Class251.SpecialTabItemHeight * UI.GameScale + Class251.StashOffset.Y + Class251.WindowOffset.Y + *(double*)((byte*)ptr + 8);
				result = new Position((int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
			return result;
		}

		public static bool smethod_50(string string_2 = "mouseover")
		{
			Position position;
			bool result;
			if (!UI.smethod_71() || !UI.smethod_3(out position, Images.TradeCancel, UI.getString_0(107380150)))
			{
				result = false;
			}
			else
			{
				Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.Mouseover);
				Rectangle rectangle_ = Class251.smethod_1(position);
				using (Bitmap bitmap = Class197.smethod_1(rectangle_, string_2))
				{
					Position position2 = Class196.smethod_0(bitmap, tuple.Item1, tuple.Item2);
					Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107380101), string_2, position2.IsVisible));
					result = position2.IsVisible;
				}
			}
			return result;
		}

		public static bool smethod_51()
		{
			InstanceZones instanceZones = (InstanceZones)Class255.class105_0.method_5(ConfigOptions.InstanceZone);
			InstanceZones instanceZones2 = instanceZones;
			InstanceZones instanceZones3 = instanceZones2;
			bool result;
			if (instanceZones3 != InstanceZones.Town)
			{
				if (instanceZones3 != InstanceZones.Menagerie)
				{
					result = UI.smethod_54(1);
				}
				else
				{
					result = UI.smethod_52();
				}
			}
			else
			{
				result = UI.smethod_54(1);
			}
			return result;
		}

		public unsafe static bool smethod_52()
		{
			void* ptr = stackalloc byte[4];
			UI.smethod_1();
			Win32.smethod_18();
			Win32.smethod_20();
			Win32.smethod_16(UI.getString_0(107380072), true, true, true, false);
			Thread.Sleep(3000);
			for (;;)
			{
				*(byte*)ptr = (UI.smethod_28() ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					break;
				}
				Thread.Sleep(100);
			}
			((byte*)ptr)[1] = ((UI.mainForm_0.enum13_0 == Enum13.const_2) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107380087));
				UI.mainForm_0.enum13_0 = Enum13.const_0;
				((byte*)ptr)[2] = (UI.smethod_54(1) ? 1 : 0);
			}
			else
			{
				((byte*)ptr)[3] = ((UI.mainForm_0.enum13_0 == Enum13.const_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					UI.mainForm_0.enum13_0 = Enum13.const_0;
					((byte*)ptr)[2] = 1;
				}
				else
				{
					((byte*)ptr)[2] = 0;
				}
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public unsafe static bool smethod_53()
		{
			void* ptr = stackalloc byte[4];
			UI.smethod_1();
			Win32.smethod_18();
			Win32.smethod_20();
			Win32.smethod_16(UI.getString_0(107379970), true, true, true, false);
			Thread.Sleep(3000);
			for (;;)
			{
				*(byte*)ptr = (UI.smethod_28() ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					break;
				}
				Thread.Sleep(100);
			}
			((byte*)ptr)[1] = ((UI.mainForm_0.enum13_0 == Enum13.const_2) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107379993));
				UI.mainForm_0.enum13_0 = Enum13.const_0;
				((byte*)ptr)[2] = (UI.smethod_54(1) ? 1 : 0);
			}
			else
			{
				((byte*)ptr)[3] = ((UI.mainForm_0.enum13_0 == Enum13.const_1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					UI.mainForm_0.enum13_0 = Enum13.const_0;
					((byte*)ptr)[2] = 1;
				}
				else
				{
					((byte*)ptr)[2] = 0;
				}
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public unsafe static bool smethod_54(int int_4 = 1)
		{
			void* ptr = stackalloc byte[22];
			Class181.smethod_2(Enum11.const_3, UI.getString_0(107380392), new object[]
			{
				int_4
			});
			((byte*)ptr)[12] = ((int_4 >= 5) ? 1 : 0);
			bool result;
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				UI.mainForm_0.method_63();
				result = false;
			}
			else
			{
				Position position;
				((byte*)ptr)[8] = (UI.smethod_3(out position, Images.ChatOpen, UI.getString_0(107351014)) ? 1 : 0);
				((byte*)ptr)[9] = (UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)) ? 1 : 0);
				((byte*)ptr)[10] = (UI.smethod_3(out position, Images.StashOpen, UI.getString_0(107350817)) ? 1 : 0);
				((byte*)ptr)[11] = (UI.smethod_69() ? 1 : 0);
				((byte*)ptr)[13] = (byte)(*(sbyte*)((byte*)ptr + 8));
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					Win32.smethod_14(UI.getString_0(107393936), false);
					UI.smethod_36(Enum2.const_34, 1.0);
				}
				((byte*)ptr)[14] = (byte)(*(sbyte*)((byte*)ptr + 9));
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					Win32.smethod_14(UI.getString_0(107393936), false);
					UI.smethod_36(Enum2.const_34, 1.0);
				}
				((byte*)ptr)[15] = (byte)(*(sbyte*)((byte*)ptr + 10) | *(sbyte*)((byte*)ptr + 11));
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					Win32.smethod_14(UI.getString_0(107393936), false);
					UI.smethod_36(Enum2.const_34, 1.0);
				}
				Position position2;
				UI.smethod_3(out position2, Images.WaypointTitle, UI.getString_0(107380359));
				((byte*)ptr)[16] = (position2.IsVisible ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					*(int*)ptr = Class308.smethod_0(Images.WaypointTitle).Item1.Width / 2;
					*(int*)((byte*)ptr + 4) = (int)Math.Round(-15.0 * UI.GameScale);
					Win32.smethod_4(position2.Left + *(int*)ptr, position2.Top + *(int*)((byte*)ptr + 4), 50, 90, false);
					Thread.Sleep(400);
					Win32.smethod_2(true);
					DateTime t = DateTime.Now + new TimeSpan(0, 0, 0, 0, Class120.dictionary_0[Enum2.const_32]);
					bool flag;
					while (!(flag = UI.smethod_3(out position, Images.World, UI.getString_0(107380370))))
					{
						UI.smethod_36(Enum2.const_22, 1.0);
						((byte*)ptr)[17] = ((DateTime.Now > t) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							break;
						}
					}
					((byte*)ptr)[18] = ((!flag) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 18) != 0)
					{
						Class181.smethod_3(Enum11.const_2, UI.getString_0(107380329));
						result = UI.smethod_54(++int_4);
					}
					else
					{
						Position position3 = UI.smethod_58();
						((byte*)ptr)[19] = (position3.IsVisible ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 19) != 0)
						{
							Win32.smethod_4(position3.Left, position3.Top, 50, 90, false);
							UI.smethod_36(Enum2.const_22, 1.0);
							Win32.smethod_2(true);
							Thread.Sleep(3000);
							for (;;)
							{
								((byte*)ptr)[20] = (UI.smethod_28() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 20) == 0)
								{
									break;
								}
								Thread.Sleep(100);
							}
							result = true;
						}
						else
						{
							Class181.smethod_3(Enum11.const_3, UI.getString_0(107380268));
							result = false;
						}
					}
				}
				else
				{
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107380235));
					((byte*)ptr)[21] = ((!UI.smethod_14()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 21) != 0)
					{
						Class181.smethod_3(Enum11.const_2, UI.getString_0(107380162));
						UI.mainForm_0.method_63();
						result = false;
					}
					else
					{
						result = UI.smethod_54(++int_4);
					}
				}
			}
			return result;
		}

		public static void smethod_55()
		{
			Class181.smethod_3(Enum11.const_0, UI.getString_0(107379649));
			UI.smethod_54(1);
			DateTime t = DateTime.Now + new TimeSpan(0, 16, 0);
			while (t > DateTime.Now)
			{
				Class181.smethod_3(Enum11.const_3, UI.getString_0(107379608));
				Thread.Sleep(60000);
			}
			Class181.smethod_3(Enum11.const_0, UI.getString_0(107379547));
			UI.smethod_44(null);
			UI.mainForm_0.bool_0 = false;
		}

		private static bool smethod_56(string string_2 = "GuildStashOpen")
		{
			Rectangle guildStashOpen = Class251.GuildStashOpen;
			Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.GuildStashOpen);
			bool isVisible;
			using (Bitmap bitmap = Class197.smethod_1(guildStashOpen, UI.getString_0(107350838)))
			{
				Position position = Class196.smethod_0(bitmap, tuple.Item1, tuple.Item2);
				Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107380101), string_2, position.IsVisible));
				isVisible = position.IsVisible;
			}
			return isVisible;
		}

		private static bool smethod_57(Position position_1)
		{
			Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.GuildStashTitle);
			Rectangle rectangle_ = Class251.smethod_0(position_1);
			bool isVisible;
			using (Bitmap bitmap = Class197.smethod_1(rectangle_, UI.getString_0(107379486)))
			{
				Position position = Class196.smethod_0(bitmap, tuple.Item1, tuple.Item2);
				Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107380101), UI.getString_0(107379437), position.IsVisible));
				isVisible = position.IsVisible;
			}
			return isVisible;
		}

		public static Position smethod_58()
		{
			Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.TownIcon);
			Position result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.TownIcon, UI.getString_0(107379448)))
			{
				Position position = Class196.smethod_0(bitmap, tuple.Item1, tuple.Item2);
				position = position.smethod_3(new Point(Class251.TownIcon.X, Class251.TownIcon.Y));
				Class181.smethod_3(Enum11.const_3, string.Format(UI.getString_0(107380101), UI.getString_0(107379448), position.IsVisible));
				result = position;
			}
			return result;
		}

		public static List<Rectangle> smethod_59(Bitmap bitmap_3, Bitmap bitmap_4, string string_2 = "", double double_0 = 0.4, int int_4 = 0)
		{
			List<Rectangle> result;
			if (bitmap_3 == null || bitmap_4 == null)
			{
				result = new List<Rectangle>();
			}
			else
			{
				Class2 class2_ = new Class2
				{
					AnalyzerType = AnalyzerTypes.CIE76,
					JustNoticeableDifference = double_0,
					BoundingBoxColor = Color.FromArgb(0, 255, 0),
					BoundingBoxPadding = 0,
					BoundingBoxMode = BoundingBoxModes.Multiple,
					DetectionPadding = int_4,
					Labeler = LabelerTypes.ConnectedComponentLabeling
				};
				using (Bitmap bitmap = new Bitmap(bitmap_3))
				{
					using (Bitmap bitmap2 = new Bitmap(bitmap_4))
					{
						Class1 @class = new Class1(class2_);
						Tuple<Bitmap, Rectangle[]> tuple = @class.method_1(bitmap, bitmap2);
						if (!string.IsNullOrEmpty(string_2))
						{
							tuple.Item1.Save(string.Format(UI.getString_0(107379403), Class120.string_5, string_2));
						}
						List<Rectangle> list = tuple.Item2.OrderBy(new Func<Rectangle, int>(UI.<>c.<>9.method_7)).ThenBy(new Func<Rectangle, int>(UI.<>c.<>9.method_8)).ToList<Rectangle>();
						list.RemoveAll(new Predicate<Rectangle>(UI.<>c.<>9.method_9));
						result = list;
					}
				}
			}
			return result;
		}

		public unsafe static Position smethod_60(Enum10 enum10_0, Rectangle rectangle_0)
		{
			void* ptr = stackalloc byte[33];
			*(double*)ptr = UI.smethod_48(enum10_0);
			*(int*)((byte*)ptr + 24) = rectangle_0.X;
			*(int*)((byte*)ptr + 28) = rectangle_0.Y;
			((byte*)ptr)[32] = ((enum10_0 == Enum10.const_2) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 32) != 0)
			{
				*(int*)((byte*)ptr + 24) = *(int*)((byte*)ptr + 24) + rectangle_0.Width / 2;
				*(int*)((byte*)ptr + 28) = *(int*)((byte*)ptr + 28) + rectangle_0.Height / 2;
			}
			*(double*)((byte*)ptr + 8) = (double)(*(int*)((byte*)ptr + 24)) / *(double*)ptr;
			*(double*)((byte*)ptr + 16) = (double)(*(int*)((byte*)ptr + 28)) / *(double*)ptr;
			return new Position((int)Math.Ceiling(*(double*)((byte*)ptr + 8)) - 1, (int)Math.Ceiling(*(double*)((byte*)ptr + 16)) - 1);
		}

		public unsafe static Position smethod_61(string string_2, Rectangle rectangle_0)
		{
			void* ptr = stackalloc byte[53];
			if (string_2 != null)
			{
				if (string_2 == UI.getString_0(107381793))
				{
					return UI.smethod_60(Enum10.const_1, rectangle_0);
				}
				if (string_2 == UI.getString_0(107381810) || string_2 == UI.getString_0(107381795))
				{
					return UI.smethod_60(Enum10.const_0, rectangle_0);
				}
			}
			JObject jobject = UI.smethod_62(string_2);
			((byte*)ptr)[48] = ((jobject == null) ? 1 : 0);
			Position result;
			if (*(sbyte*)((byte*)ptr + 48) != 0)
			{
				result = new Position();
			}
			else
			{
				*(int*)((byte*)ptr + 8) = (int)Math.Round((double)rectangle_0.X / (Class251.SpecialTabItemWidth * UI.GameScale));
				*(int*)((byte*)ptr + 12) = (int)Math.Round((double)rectangle_0.Y / (Class251.SpecialTabItemHeight * UI.GameScale));
				foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
				{
					*(double*)ptr = Class251.ItemOffset / UI.GameScale;
					if (!keyValuePair.Key.Contains(UI.getString_0(107392566)) && !keyValuePair.Key.Contains(UI.getString_0(107379410)))
					{
						((byte*)ptr)[49] = ((keyValuePair.Key == UI.getString_0(107380141)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 49) != 0)
						{
							using (IEnumerator<JToken> enumerator2 = ((IEnumerable<JToken>)keyValuePair.Value).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									JToken jtoken = enumerator2.Current;
									((byte*)ptr)[50] = (((JProperty)jtoken).Name.Contains(UI.getString_0(107392566)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 50) == 0)
									{
										foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
										{
											*(int*)((byte*)ptr + 16) = (int)Math.Round((double)jtoken2[UI.getString_0(107397007)]);
											*(int*)((byte*)ptr + 20) = (int)Math.Round((double)jtoken2[UI.getString_0(107380155)]);
											*(int*)((byte*)ptr + 24) = (int)Math.Round(*(double*)ptr * (double)((int)jtoken2[UI.getString_0(107379877)]));
											*(int*)((byte*)ptr + 28) = (int)Math.Round(*(double*)ptr * (double)((int)jtoken2[UI.getString_0(107395711)]));
											Rectangle rectangle = new Rectangle(*(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 20), *(int*)((byte*)ptr + 24), *(int*)((byte*)ptr + 28));
											((byte*)ptr)[51] = (rectangle.Contains(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 51) != 0)
											{
												return new Position(int.Parse(((JProperty)jtoken2.Parent).Name), 0);
											}
										}
									}
								}
								continue;
							}
						}
						*(int*)((byte*)ptr + 32) = (int)Math.Round((double)keyValuePair.Value[UI.getString_0(107397007)]);
						*(int*)((byte*)ptr + 36) = (int)Math.Round((double)keyValuePair.Value[UI.getString_0(107380155)]);
						*(int*)((byte*)ptr + 40) = (int)Math.Round(*(double*)ptr * (double)((int)keyValuePair.Value[UI.getString_0(107379877)]));
						*(int*)((byte*)ptr + 44) = (int)Math.Round(*(double*)ptr * (double)((int)keyValuePair.Value[UI.getString_0(107395711)]));
						Rectangle rectangle2 = new Rectangle(*(int*)((byte*)ptr + 32), *(int*)((byte*)ptr + 36), *(int*)((byte*)ptr + 40), *(int*)((byte*)ptr + 44));
						((byte*)ptr)[52] = (rectangle2.Contains(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 52) != 0)
						{
							return new Position(int.Parse(keyValuePair.Key), 0);
						}
					}
				}
				result = new Position();
			}
			return result;
		}

		public unsafe static JObject smethod_62(string string_2)
		{
			void* ptr = stackalloc byte[5];
			string text = null;
			if (string_2 != null)
			{
				*(int*)ptr = (int)Class396.smethod_0(string_2);
				if (*(uint*)ptr <= 611440075U)
				{
					if (*(uint*)ptr != 54800890U)
					{
						if (*(uint*)ptr != 599248347U)
						{
							if (*(uint*)ptr == 611440075U)
							{
								if (string_2 == UI.getString_0(107379870))
								{
									text = UI.getString_0(107379731);
								}
							}
						}
						else if (string_2 == UI.getString_0(107379817))
						{
							text = UI.getString_0(107379710);
						}
					}
					else if (string_2 == UI.getString_0(107381780))
					{
						text = UI.getString_0(107379758);
					}
				}
				else if (*(uint*)ptr <= 2977208620U)
				{
					if (*(uint*)ptr != 809384579U)
					{
						if (*(uint*)ptr == 2977208620U)
						{
							if (string_2 == UI.getString_0(107379855))
							{
								text = UI.getString_0(107379716);
							}
						}
					}
					else if (string_2 == UI.getString_0(107393855))
					{
						text = UI.getString_0(107379779);
					}
				}
				else if (*(uint*)ptr != 3568218232U)
				{
					if (*(uint*)ptr == 4294430810U)
					{
						if (string_2 == UI.getString_0(107379904))
						{
							text = UI.getString_0(107379769);
						}
					}
				}
				else if (string_2 == UI.getString_0(107379828))
				{
					text = UI.getString_0(107379657);
				}
			}
			((byte*)ptr)[4] = ((text == null) ? 1 : 0);
			JObject result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = null;
			}
			else
			{
				JObject jobject;
				Stashes.Layout.TryGetValue(text, out jobject);
				result = jobject;
			}
			return result;
		}

		public unsafe static void smethod_63()
		{
			void* ptr = stackalloc byte[27];
			((byte*)ptr)[20] = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 20) == 0)
			{
				Directory.CreateDirectory(UI.getString_0(107379672));
				Dictionary<string, Bitmap> dictionary = new Dictionary<string, Bitmap>();
				foreach (JsonTab jsonTab in Stashes.Tabs.Where(new Func<JsonTab, bool>(UI.<>c.<>9.method_10)))
				{
					((byte*)ptr)[21] = ((!Stashes.Items.ContainsKey(jsonTab.i)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 21) == 0)
					{
						List<JsonItem> list = Stashes.Items[jsonTab.i];
						Enum10 enum10_ = UI.smethod_64(jsonTab.type);
						*(int*)ptr = (int)Math.Round(UI.smethod_48(enum10_));
						Bitmap bitmap = new Bitmap(Class251.Stash.Width, Class251.Stash.Height);
						*(int*)((byte*)ptr + 4) = 0;
						for (;;)
						{
							((byte*)ptr)[23] = ((*(int*)((byte*)ptr + 4) < bitmap.Width / *(int*)ptr) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 23) == 0)
							{
								break;
							}
							*(int*)((byte*)ptr + 8) = 0;
							for (;;)
							{
								((byte*)ptr)[22] = ((*(int*)((byte*)ptr + 8) < bitmap.Height / *(int*)ptr) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 22) == 0)
								{
									break;
								}
								using (Graphics graphics = Graphics.FromImage(bitmap))
								{
									graphics.DrawImage(Class235.EmptyCell, *(int*)((byte*)ptr + 4) * *(int*)ptr, *(int*)((byte*)ptr + 8) * *(int*)ptr);
								}
								*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
							}
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						}
						foreach (JsonItem jsonItem in list)
						{
							((byte*)ptr)[24] = ((jsonItem.icon == null) ? 1 : 0);
							Bitmap bitmap3;
							if (*(sbyte*)((byte*)ptr + 24) != 0)
							{
								using (Bitmap bitmap2 = new Bitmap(Class238.x))
								{
									bitmap3 = new Bitmap(bitmap2, new Size(*(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h));
									goto IL_2B1;
								}
								goto IL_1E2;
							}
							goto IL_1E2;
							IL_2B1:
							using (Graphics graphics2 = Graphics.FromImage(bitmap))
							{
								if (jsonItem.BaseItemStackSize > 1 || jsonItem.stack > 1)
								{
									using (Graphics graphics3 = Graphics.FromImage(bitmap3))
									{
										using (Font font = new Font(UI.getString_0(107397927), 12.5f, FontStyle.Bold))
										{
											Graphics graphics4 = graphics3;
											*(int*)((byte*)ptr + 16) = jsonItem.stack;
											SizeF sizeF = graphics4.MeasureString(((int*)((byte*)ptr + 16))->ToString(), font);
											graphics3.FillRectangle(Brushes.Black, 3f, 0f, sizeF.Width, sizeF.Height);
											Graphics graphics5 = graphics3;
											*(int*)((byte*)ptr + 16) = jsonItem.stack;
											graphics5.DrawString(((int*)((byte*)ptr + 16))->ToString(), font, Brushes.White, 3f, 0f);
										}
									}
								}
								*(int*)((byte*)ptr + 12) = (int)Math.Round(UI.smethod_48(enum10_) / 2.0);
								((byte*)ptr)[26] = (jsonTab.IsSpecialTab ? 1 : 0);
								Position position;
								if (*(sbyte*)((byte*)ptr + 26) != 0)
								{
									position = UI.smethod_49(UI.smethod_62(jsonTab.type), jsonItem.x);
									position.Left -= (int)Math.Round(Class251.StashOffset.X - Class251.WindowOffset.X);
									position.Top -= (int)Math.Round(Class251.StashOffset.Y - Class251.WindowOffset.Y);
								}
								else
								{
									position = UI.smethod_46(enum10_, jsonItem.x, jsonItem.y);
									position.Left += -(int)Math.Round(Class251.StashOffset.X + Class251.WindowOffset.X);
									position.Top += -(int)Math.Round(Class251.StashOffset.Y + Class251.WindowOffset.Y);
								}
								position.Left -= *(int*)((byte*)ptr + 12);
								position.Top -= *(int*)((byte*)ptr + 12);
								graphics2.DrawImage(bitmap3, position.X, position.Y);
							}
							continue;
							IL_1E2:
							((byte*)ptr)[25] = (dictionary.ContainsKey(jsonItem.icon) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 25) != 0)
							{
								bitmap3 = dictionary[jsonItem.icon];
								goto IL_2B1;
							}
							try
							{
								Stream responseStream = WebRequest.Create(jsonItem.icon).GetResponse().GetResponseStream();
								using (Bitmap bitmap4 = new Bitmap(responseStream))
								{
									bitmap3 = new Bitmap(bitmap4, new Size(*(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h));
								}
							}
							catch
							{
								using (Bitmap bitmap5 = new Bitmap(Class238.x))
								{
									bitmap3 = new Bitmap(bitmap5, new Size(*(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h));
								}
							}
							dictionary.Add(jsonItem.icon, bitmap3);
							goto IL_2B1;
						}
						bitmap.Save(string.Format(UI.getString_0(107379111), Class120.string_5, jsonTab.n, jsonTab.i));
					}
				}
			}
		}

		public static Enum10 smethod_64(string string_2)
		{
			if (string_2 != null)
			{
				if (string_2 == UI.getString_0(107381810) || string_2 == UI.getString_0(107381795))
				{
					return Enum10.const_0;
				}
				if (string_2 == UI.getString_0(107381793))
				{
					return Enum10.const_1;
				}
			}
			return Enum10.const_4;
		}

		public unsafe static void smethod_65()
		{
			void* ptr = stackalloc byte[10];
			using (Bitmap bitmap = new Bitmap(Class308.smethod_0(Images.EmptyCell).Item1))
			{
				foreach (TradeWindowItem tradeWindowItem in UI.list_0)
				{
					tradeWindowItem.Image.Dispose();
					tradeWindowItem.CroppedImage.Dispose();
				}
				UI.list_0.Clear();
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[9] = ((*(int*)ptr < 12) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 4) = 0;
					for (;;)
					{
						((byte*)ptr)[8] = ((*(int*)((byte*)ptr + 4) < 5) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) == 0)
						{
							break;
						}
						UI.list_0.Add(new TradeWindowItem(bitmap, new Position(*(int*)ptr, *(int*)((byte*)ptr + 4))));
						*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
		}

		public unsafe static List<TradeWindowItem> smethod_66()
		{
			void* ptr = stackalloc byte[34];
			((byte*)ptr)[12] = ((!UI.smethod_71()) ? 1 : 0);
			List<TradeWindowItem> result;
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				result = UI.list_0.smethod_17().ToList<TradeWindowItem>();
			}
			else
			{
				Dictionary<Position, Bitmap> dictionary = new Dictionary<Position, Bitmap>();
				Position position = UI.smethod_46(Enum10.const_3, 0, 5);
				*(int*)ptr = Class308.smethod_0(Images.EmptyCell).Item1.Width;
				Position position2 = new Position(position.X, position.Y);
				UI.Class115 @class = new UI.Class115();
				@class.int_0 = 0;
				for (;;)
				{
					((byte*)ptr)[15] = ((@class.int_0 < 12) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) == 0)
					{
						break;
					}
					UI.Class116 class2 = new UI.Class116();
					class2.class115_0 = @class;
					class2.int_0 = 0;
					for (;;)
					{
						((byte*)ptr)[14] = ((class2.int_0 < 5) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 14) == 0)
						{
							break;
						}
						Position position3 = UI.smethod_47(Enum10.const_3, class2.class115_0.int_0, class2.int_0);
						Rectangle rectangle_ = new Rectangle(position3.X, position3.Y, *(int*)ptr, *(int*)ptr);
						using (Bitmap bitmap = Class197.smethod_1(rectangle_, UI.getString_0(107396942)))
						{
							((byte*)ptr)[13] = ((!UI.smethod_9(bitmap, Images.EmptyCellInner)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 13) != 0)
							{
								dictionary.Add(new Position(class2.class115_0.int_0, class2.int_0), new Bitmap(bitmap));
							}
							else
							{
								TradeWindowItem tradeWindowItem = UI.list_0.First(new Func<TradeWindowItem, bool>(class2.method_0));
								tradeWindowItem.method_0();
							}
						}
						*(int*)((byte*)ptr + 8) = class2.int_0;
						class2.int_0 = *(int*)((byte*)ptr + 8) + 1;
					}
					*(int*)((byte*)ptr + 8) = @class.int_0;
					@class.int_0 = *(int*)((byte*)ptr + 8) + 1;
				}
				foreach (KeyValuePair<Position, Bitmap> keyValuePair in dictionary)
				{
					UI.Class117 class3 = new UI.Class117();
					Position key = keyValuePair.Key;
					Bitmap value = keyValuePair.Value;
					class3.int_0 = key.X;
					class3.int_1 = key.Y;
					Position position4 = UI.smethod_47(Enum10.const_3, class3.int_0, class3.int_1);
					TradeWindowItem tradeWindowItem2 = UI.list_0.First(new Func<TradeWindowItem, bool>(class3.method_0));
					double double_ = tradeWindowItem2.MousedOverOnce ? 0.4 : 0.2;
					List<Rectangle> source = UI.smethod_59(tradeWindowItem2.Image, value, UI.getString_0(107396942), double_, 0);
					((byte*)ptr)[16] = ((!source.Any<Rectangle>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) == 0)
					{
						if (UI.smethod_90() || UI.smethod_91())
						{
							((byte*)ptr)[17] = (tradeWindowItem2.MousedOverOnce ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 17) != 0)
							{
								using (Bitmap bitmap2 = Class197.smethod_1(UI.smethod_93(class3.int_0, class3.int_1, false), UI.getString_0(107396942)))
								{
									((byte*)ptr)[18] = ((!UI.smethod_59(tradeWindowItem2.CroppedImage, bitmap2, UI.getString_0(107396942), 0.4, 0).Any<Rectangle>()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 18) != 0)
									{
										continue;
									}
									goto IL_3FA;
								}
							}
							((byte*)ptr)[19] = ((tradeWindowItem2.Item == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) != 0)
							{
								((byte*)ptr)[20] = ((class3.int_0 == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 20) != 0)
								{
									((byte*)ptr)[21] = (UI.smethod_9(value, Images.EmptyCellAcceptedLeft) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 21) != 0)
									{
										continue;
									}
								}
								((byte*)ptr)[22] = ((class3.int_0 == 11) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 22) != 0)
								{
									((byte*)ptr)[23] = (UI.smethod_9(value, Images.EmptyCellAcceptedRight) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 23) != 0)
									{
										continue;
									}
								}
								((byte*)ptr)[24] = ((class3.int_1 == 4) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 24) != 0)
								{
									((byte*)ptr)[25] = (UI.smethod_9(value, Images.EmptyCellAcceptedBot) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 25) != 0)
									{
										continue;
									}
								}
								((byte*)ptr)[26] = ((class3.int_1 == 0) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 26) != 0)
								{
									((byte*)ptr)[27] = (UI.smethod_9(value, Images.EmptyCellAcceptedTop) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 27) != 0)
									{
										continue;
									}
								}
							}
						}
						IL_3FA:
						if (UI.smethod_9(value, Images.EmptyCellInner) || UI.smethod_9(value, Images.EmptyCellAcceptedLeft) || UI.smethod_9(value, Images.EmptyCellAcceptedRight) || UI.smethod_9(value, Images.EmptyCellAcceptedTop) || UI.smethod_9(value, Images.EmptyCellAcceptedBot))
						{
							tradeWindowItem2.method_0();
						}
						else
						{
							Rectangle rectangle_2 = UI.smethod_93(class3.int_0, class3.int_1, false);
							position2 = UI.smethod_33(class3.int_0, class3.int_1);
							tradeWindowItem2.MousedOverOnce = true;
							class3.item_0 = new Item(Win32.smethod_21());
							if (class3.item_0.text != UI.getString_0(107381568) && !class3.item_0.Name.Replace(UI.getString_0(107396979), UI.getString_0(107396942)).smethod_25())
							{
								Class181.smethod_2(Enum11.const_3, UI.getString_0(107379102), new object[]
								{
									class3.item_0.Name,
									class3.item_0.stack,
									class3.int_0 + 1,
									class3.int_1 + 1
								});
								IEnumerable<Item> enumerable = UI.list_0.Where(new Func<TradeWindowItem, bool>(UI.<>c.<>9.method_11)).Select(new Func<TradeWindowItem, Item>(UI.<>c.<>9.method_12)).Where(new Func<Item, bool>(class3.method_1));
								((byte*)ptr)[28] = (enumerable.Any<Item>() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 28) != 0)
								{
									using (IEnumerator<Item> enumerator2 = enumerable.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											Item item = enumerator2.Current;
											item.smallestX = Math.Min(item.smallestX, class3.int_0);
											item.largestX = Math.Max(item.largestX, class3.int_0);
											item.smallestY = Math.Min(item.smallestY, class3.int_1);
											item.largestY = Math.Max(item.largestY, class3.int_1);
											item.Left = item.smallestX;
											item.Top = item.smallestY;
											UI.Class118 class4 = new UI.Class118();
											class4.int_0 = item.smallestX;
											for (;;)
											{
												((byte*)ptr)[32] = ((class4.int_0 < item.largestX + 1) ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 32) == 0)
												{
													break;
												}
												UI.Class119 class5 = new UI.Class119();
												class5.class118_0 = class4;
												class5.int_0 = item.smallestY;
												for (;;)
												{
													((byte*)ptr)[31] = ((class5.int_0 < item.largestY + 1) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 31) == 0)
													{
														break;
													}
													TradeWindowItem tradeWindowItem3 = UI.list_0.First(new Func<TradeWindowItem, bool>(class5.method_0));
													((byte*)ptr)[29] = ((tradeWindowItem3.Item != null) ? 1 : 0);
													if (*(sbyte*)((byte*)ptr + 29) == 0 || ((!tradeWindowItem3.Item.placeholder || (class5.class118_0.int_0 == item.smallestX && class5.int_0 == item.smallestY)) && (tradeWindowItem3.Item.placeholder || class5.class118_0.int_0 != item.smallestX || class5.int_0 != item.smallestY)))
													{
														Position position5 = UI.smethod_47(Enum10.const_3, class5.class118_0.int_0, class5.int_0);
														Rectangle rectangle_3 = new Rectangle(position5.X, position5.Y, *(int*)ptr, *(int*)ptr);
														Rectangle rectangle_4 = UI.smethod_93(class5.class118_0.int_0, class5.int_0, false);
														tradeWindowItem3.Item = new Item(class3.item_0.text)
														{
															smallestX = item.smallestX,
															smallestY = item.smallestY,
															largestX = item.largestX,
															largestY = item.largestY,
															Left = item.smallestX,
															Top = item.smallestY,
															width = item.width,
															height = item.height
														};
														if (class5.class118_0.int_0 == item.smallestX && class5.int_0 == item.smallestY)
														{
															tradeWindowItem3.Item.placeholder = false;
															((byte*)ptr)[30] = ((tradeWindowItem3.Item != item) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 30) != 0)
															{
																item.placeholder = true;
															}
														}
														else
														{
															tradeWindowItem3.Item.placeholder = true;
														}
														tradeWindowItem3.MousedOverOnce = true;
														tradeWindowItem3.Image = Class197.smethod_1(rectangle_3, UI.getString_0(107396942));
														tradeWindowItem3.CroppedImage = Class197.smethod_1(rectangle_4, UI.getString_0(107396942));
													}
													*(int*)((byte*)ptr + 8) = class5.int_0;
													class5.int_0 = *(int*)((byte*)ptr + 8) + 1;
												}
												*(int*)((byte*)ptr + 8) = class4.int_0;
												class4.int_0 = *(int*)((byte*)ptr + 8) + 1;
											}
										}
										continue;
									}
								}
								Size size = ItemData.smethod_2(class3.item_0.typeLine);
								class3.item_0.width = size.Width;
								class3.item_0.height = size.Height;
								class3.item_0.Left = class3.int_0;
								class3.item_0.Top = class3.int_1;
								class3.item_0.smallestX = class3.int_0;
								class3.item_0.smallestY = class3.int_1;
								tradeWindowItem2.Item = class3.item_0;
								tradeWindowItem2.Image = Class197.smethod_1(new Rectangle(position4.X, position4.Y, *(int*)ptr, *(int*)ptr), UI.getString_0(107396942));
								tradeWindowItem2.CroppedImage = Class197.smethod_1(rectangle_2, UI.getString_0(107396942));
							}
							else
							{
								Position position6;
								((byte*)ptr)[33] = ((!UI.smethod_3(out position6, Images.TradeCancel, UI.getString_0(107378985))) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 33) != 0)
								{
									Class181.smethod_3(Enum11.const_3, UI.getString_0(107379000));
									return UI.list_0.smethod_17().ToList<TradeWindowItem>();
								}
								Class181.smethod_2(Enum11.const_3, UI.getString_0(107378911), new object[]
								{
									class3.int_0,
									class3.int_1
								});
								tradeWindowItem2.method_0();
							}
						}
					}
				}
				foreach (KeyValuePair<Position, Bitmap> keyValuePair2 in dictionary)
				{
					keyValuePair2.Value.smethod_12();
				}
				*(int*)((byte*)ptr + 4) = (int)Util.smethod_4(position2.Y, (double)UI.smethod_46(Enum10.const_3, 0, -1).Y, (double)UI.smethod_46(Enum10.const_3, 0, 5).Y);
				Win32.smethod_4(position2.X, *(int*)((byte*)ptr + 4), 50, 90, false);
				result = UI.list_0.smethod_17().ToList<TradeWindowItem>();
			}
			return result;
		}

		public static Bitmap smethod_67()
		{
			Win32.smethod_4(-2, -2, 50, 90, false);
			UI.smethod_15();
			Bitmap bitmap = Class197.smethod_1(Class251.Inventory, UI.getString_0(107396942));
			Position position;
			while (UI.smethod_4(bitmap, out position, Images.RequestAccept, UI.getString_0(107379334)) || UI.smethod_4(bitmap, out position, Images.RequestIcon, UI.getString_0(107379313)))
			{
				UI.smethod_18();
				Thread.Sleep(400);
				bitmap.smethod_12();
				bitmap = Class197.smethod_1(Class251.Inventory, UI.getString_0(107396942));
			}
			return bitmap;
		}

		public unsafe static bool smethod_68()
		{
			void* ptr = stackalloc byte[3];
			UI.smethod_80();
			Position position;
			*(byte*)ptr = (UI.smethod_3(out position, Images.LillyTitle, UI.getString_0(107379328)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Bitmap item = Class308.smethod_0(Images.LillyTitle).Item1;
				Win32.smethod_5(position.smethod_6(item.Width / 2, item.Height / 2), false);
				Thread.Sleep(200);
				Win32.smethod_2(true);
				Thread.Sleep(200);
				Win32.smethod_4(-2, -2, 50, 90, false);
				for (;;)
				{
					Position position2;
					((byte*)ptr)[1] = ((!UI.smethod_3(out position2, Images.VendorSellItems, UI.getString_0(107379279))) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						break;
					}
					Thread.Sleep(100);
				}
				((byte*)ptr)[2] = 1;
			}
			else
			{
				Class181.smethod_3(Enum11.const_2, UI.getString_0(107379290));
				((byte*)ptr)[2] = 0;
			}
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public static bool smethod_69()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.InventoryOpen, UI.getString_0(107396942)))
			{
				Position position;
				result = UI.smethod_4(bitmap, out position, Images.InventoryOpen, UI.getString_0(107379213));
			}
			return result;
		}

		public static bool smethod_70()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.TradeWaiting, UI.getString_0(107396942)))
			{
				Position position;
				result = UI.smethod_4(bitmap, out position, Images.TradeWaiting, UI.getString_0(107379224));
			}
			return result;
		}

		public static bool smethod_71()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.TradeOpen, UI.getString_0(107396942)))
			{
				Position position;
				result = UI.smethod_4(bitmap, out position, Images.TradeOpen, UI.getString_0(107379199));
			}
			return result;
		}

		public static bool smethod_72()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.InventoryWindow, UI.getString_0(107396942)))
			{
				Position position;
				result = UI.smethod_4(bitmap, out position, Images.RequestAcceptDecline, UI.getString_0(107379150));
			}
			return result;
		}

		public static bool smethod_73()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.TradeWindow, UI.getString_0(107396942)))
			{
				Position position;
				result = (UI.smethod_4(bitmap, out position, Images.TradeCancelAcceptHighlighted, UI.getString_0(107378601)) || UI.smethod_4(bitmap, out position, Images.TradeCancelAccept, UI.getString_0(107378601)));
			}
			return result;
		}

		public static bool smethod_74()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.ProphecySeek, UI.getString_0(107396942)))
			{
				Position position;
				result = UI.smethod_4(bitmap, out position, Images.ProphecySeek, UI.getString_0(107378568));
			}
			return result;
		}

		public static bool smethod_75()
		{
			Position position;
			return UI.smethod_3(out position, Images.ProphecyPopup, UI.getString_0(107378543));
		}

		public static void smethod_76(string string_2)
		{
			UI.smethod_36(Enum2.const_22, 1.0);
			Win32.smethod_14(UI.getString_0(107378546), false);
			UI.smethod_36(Enum2.const_22, 1.0);
			Win32.smethod_16(string_2, false, true, false, false);
		}

		public static bool smethod_77()
		{
			Bitmap bitmap_ = Class197.smethod_1(Class251.Stash, UI.getString_0(107396942));
			Position position;
			return UI.smethod_4(bitmap_, out position, Images.CreateParty, UI.getString_0(107378505));
		}

		public static void smethod_78()
		{
			Win32.smethod_5(Class251.CreatePartyLocation, false);
			Thread.Sleep(50);
			Win32.smethod_2(true);
			UI.smethod_36(Enum2.const_22, 1.0);
		}

		public static bool smethod_79()
		{
			bool result;
			using (Bitmap bitmap = Class197.smethod_1(new Rectangle(0, 0, UI.PoeDimensions.Width / 3, UI.PoeDimensions.Height * 2 / 3), UI.getString_0(107396942)))
			{
				Position position;
				result = UI.smethod_4(bitmap, out position, Images.PartySwirl, UI.getString_0(107378472));
			}
			return result;
		}

		public unsafe static bool smethod_80()
		{
			void* ptr = stackalloc byte[2];
			Position position;
			if (UI.smethod_69() || UI.smethod_56(UI.getString_0(107350838)) || UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)) || UI.smethod_3(out position, Images.Social, UI.getString_0(107378491)) || UI.smethod_3(out position, Images.TradeCancel, UI.getString_0(107378985)) || UI.smethod_3(out position, Images.GwennenGamble, UI.getString_0(107378482)) || UI.smethod_70())
			{
				*(byte*)ptr = (UI.smethod_3(out position, Images.RerollOffer, UI.getString_0(107378441)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					Win32.smethod_14(UI.getString_0(107393936), false);
					Thread.Sleep(500);
				}
				Win32.smethod_14(UI.getString_0(107393936), false);
				UI.smethod_36(Enum2.const_34, 1.0);
				((byte*)ptr)[1] = 1;
			}
			else
			{
				((byte*)ptr)[1] = 0;
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		public unsafe static float smethod_81()
		{
			void* ptr = stackalloc byte[17];
			((byte*)ptr)[16] = (Class120.UsingWindows7 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				*(float*)((byte*)ptr + 12) = 1f;
			}
			else
			{
				Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
				IntPtr hdc = graphics.GetHdc();
				*(int*)ptr = UI.GetDeviceCaps(hdc, 10);
				*(int*)((byte*)ptr + 4) = UI.GetDeviceCaps(hdc, 117);
				*(float*)((byte*)ptr + 8) = (float)(*(int*)((byte*)ptr + 4)) / (float)(*(int*)ptr);
				*(float*)((byte*)ptr + 12) = *(float*)((byte*)ptr + 8);
			}
			return *(float*)((byte*)ptr + 12);
		}

		public unsafe static Struct2 smethod_82(Struct2 struct2_2)
		{
			void* ptr = stackalloc byte[20];
			*(float*)ptr = UI.smethod_81();
			*(int*)((byte*)ptr + 4) = (int)Math.Round((double)((float)struct2_2.int_1 * *(float*)ptr));
			*(int*)((byte*)ptr + 8) = (int)Math.Round((double)((float)struct2_2.int_3 * *(float*)ptr));
			*(int*)((byte*)ptr + 12) = (int)Math.Round((double)((float)struct2_2.int_0 * *(float*)ptr));
			*(int*)((byte*)ptr + 16) = (int)Math.Round((double)((float)struct2_2.int_2 * *(float*)ptr));
			return new Struct2
			{
				int_1 = *(int*)((byte*)ptr + 4),
				int_3 = *(int*)((byte*)ptr + 8),
				int_0 = *(int*)((byte*)ptr + 12),
				int_2 = *(int*)((byte*)ptr + 16)
			};
		}

		public unsafe static int smethod_83(int int_4 = 12)
		{
			void* ptr = stackalloc byte[27];
			Class181.smethod_3(Enum11.const_3, UI.getString_0(107378464));
			ProcessHelper.smethod_13(true);
			*(int*)ptr = 0;
			Win32.smethod_4(-2, -2, 50, 90, false);
			UI.smethod_15();
			((byte*)ptr)[20] = (UI.smethod_105() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 20) != 0)
			{
				Win32.smethod_14(UI.getString_0(107393936), false);
				Thread.Sleep(400);
			}
			((byte*)ptr)[21] = ((int_4 >= 5) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				((byte*)ptr)[22] = (UI.smethod_72() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 22) != 0)
				{
					UI.smethod_18();
					UI.smethod_36(Enum2.const_22, 1.0);
				}
			}
			((byte*)ptr)[23] = ((!UI.SmallChat) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 23) != 0)
			{
				Win32.smethod_16(UI.getString_0(107381545), true, true, false, false);
			}
			using (Bitmap gameImage = UI.GameImage)
			{
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[26] = ((*(int*)((byte*)ptr + 4) < int_4) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 26) == 0)
					{
						break;
					}
					*(int*)((byte*)ptr + 8) = 0;
					for (;;)
					{
						((byte*)ptr)[25] = ((*(int*)((byte*)ptr + 8) < 5) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 25) == 0)
						{
							break;
						}
						Position position = UI.smethod_47(Enum10.const_2, *(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 8));
						*(int*)((byte*)ptr + 12) = (int)UI.smethod_48(Enum10.const_2);
						using (Bitmap bitmap = Class197.smethod_0(gameImage, position.Left, position.Top, *(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 12), UI.getString_0(107396942)))
						{
							((byte*)ptr)[24] = ((!UI.smethod_9(bitmap, Images.EmptyCellInner)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 24) != 0)
							{
								*(int*)ptr = *(int*)ptr + 1;
							}
						}
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
			}
			Class181.smethod_2(Enum11.const_3, UI.getString_0(107378419), new object[]
			{
				*(int*)ptr
			});
			*(int*)((byte*)ptr + 16) = *(int*)ptr;
			return *(int*)((byte*)ptr + 16);
		}

		public unsafe static int[,] smethod_84()
		{
			void* ptr = stackalloc byte[17];
			ProcessHelper.smethod_13(true);
			Win32.smethod_4(-2, -2, 50, 90, false);
			UI.smethod_15();
			((byte*)ptr)[12] = (UI.smethod_72() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				UI.smethod_18();
				UI.smethod_36(Enum2.const_22, 1.0);
			}
			((byte*)ptr)[13] = ((!UI.SmallChat) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 13) != 0)
			{
				Win32.smethod_16(UI.getString_0(107381545), true, true, false, false);
			}
			int[,] array = new int[12, 5];
			Bitmap bitmap = new Bitmap(UI.GameImage);
			*(int*)ptr = 0;
			for (;;)
			{
				((byte*)ptr)[16] = ((*(int*)ptr < 12) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) == 0)
				{
					break;
				}
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[15] = ((*(int*)((byte*)ptr + 4) < 5) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) == 0)
					{
						break;
					}
					Position position = UI.smethod_47(Enum10.const_2, *(int*)ptr, *(int*)((byte*)ptr + 4));
					*(int*)((byte*)ptr + 8) = (int)UI.smethod_48(Enum10.const_2);
					Bitmap bitmap_ = Class197.smethod_0(bitmap, position.Left, position.Top, *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 8), UI.getString_0(107396942));
					((byte*)ptr)[14] = ((!UI.smethod_9(bitmap_, Images.EmptyCellInner)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 14) != 0)
					{
						array[*(int*)ptr, *(int*)((byte*)ptr + 4)] = 1;
					}
					else
					{
						array[*(int*)ptr, *(int*)((byte*)ptr + 4)] = 0;
					}
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			return array;
		}

		public unsafe static void smethod_85()
		{
			void* ptr = stackalloc byte[6];
			UI.smethod_1();
			UI.smethod_80();
			Thread.Sleep(300);
			Win32.smethod_14(UI.getString_0(107393936), false);
			DateTime t = DateTime.Now.AddSeconds(3.0);
			*(byte*)ptr = 0;
			Position position = new Position();
			for (;;)
			{
				((byte*)ptr)[2] = ((DateTime.Now < t) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) == 0)
				{
					break;
				}
				((byte*)ptr)[1] = (UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					goto IL_8B;
				}
			}
			goto IL_8E;
			IL_8B:
			*(byte*)ptr = 1;
			IL_8E:
			((byte*)ptr)[3] = ((*(sbyte*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				Win32.smethod_14(UI.getString_0(107393936), false);
				UI.smethod_85();
			}
			else
			{
				Position position_ = position.smethod_6(position.Width / 2, (int)Math.Round((double)position.Height - 30.0 * UI.GameScale));
				t = DateTime.Now.AddSeconds(3.0);
				for (;;)
				{
					((byte*)ptr)[5] = ((DateTime.Now < t) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) == 0)
					{
						break;
					}
					((byte*)ptr)[4] = (UI.smethod_3(out position, Images.EscMenu, UI.getString_0(107351059)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) == 0)
					{
						return;
					}
					Win32.smethod_5(position_, false);
					Thread.Sleep(500);
					Win32.smethod_2(true);
				}
				UI.smethod_85();
			}
		}

		public static void smethod_86()
		{
			UI.struct2_1 = default(Struct2);
			UI.struct2_0 = UI.struct2_1;
		}

		public unsafe static void smethod_87()
		{
			void* ptr = stackalloc byte[9];
			Position position;
			((byte*)ptr)[8] = (UI.smethod_3(out position, Images.Watchstone, UI.getString_0(107378390)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				*(int*)ptr = (int)Math.Round((double)(position.X + Class308.smethod_0(Images.Watchstone).Item1.Width) - 17.0 * UI.GameScale);
				*(int*)((byte*)ptr + 4) = (int)Math.Round((double)position.Y + 5.0 * UI.GameScale);
				Win32.smethod_4(*(int*)ptr, *(int*)((byte*)ptr + 4), 50, 90, false);
				UI.smethod_36(Enum2.const_22, 1.0);
				Win32.smethod_2(true);
			}
		}

		private static bool smethod_88(Color color_0)
		{
			return color_0.G > 50;
		}

		private static bool smethod_89(Color color_0)
		{
			return color_0.R > 50;
		}

		public unsafe static bool smethod_90()
		{
			void* ptr = stackalloc byte[6];
			Bitmap gameImage = UI.GameImage;
			*(int*)ptr = 0;
			foreach (Position position in UI.list_2)
			{
				((byte*)ptr)[4] = (UI.smethod_88(gameImage.GetPixel(position.X, position.Y)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
			((byte*)ptr)[5] = ((*(int*)ptr >= UI.list_2.Count / 2) ? 1 : 0);
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		public unsafe static bool smethod_91()
		{
			void* ptr = stackalloc byte[6];
			Bitmap gameImage = UI.GameImage;
			*(int*)ptr = 0;
			foreach (Position position in UI.list_2)
			{
				((byte*)ptr)[4] = (UI.smethod_89(gameImage.GetPixel(position.X, position.Y)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					*(int*)ptr = *(int*)ptr + 1;
				}
			}
			((byte*)ptr)[5] = ((*(int*)ptr >= UI.list_2.Count / 2) ? 1 : 0);
			return *(sbyte*)((byte*)ptr + 5) != 0;
		}

		public static void smethod_92()
		{
			if (!(UI.intptr_0 == IntPtr.Zero) && !UI.IsFullScreen && Class255.class105_0.method_5(ConfigOptions.WindowMode) == 0)
			{
				Point point = Class255.class105_0.method_2<Point>(ConfigOptions.GameLocation);
				if (point.X != 0 || point.Y != 0)
				{
					UI.SetWindowPos(UI.intptr_0, 0, point.X, point.Y, 0, 0, 5);
				}
			}
		}

		public unsafe static Rectangle smethod_93(int int_4, int int_5, bool bool_3 = false)
		{
			void* ptr = stackalloc byte[16];
			*(int*)ptr = Class308.smethod_0(Images.EmptyCell).Item1.Width;
			*(int*)((byte*)ptr + 4) = Util.smethod_22((double)(*(int*)ptr) * 0.53);
			*(int*)((byte*)ptr + 8) = *(int*)ptr - *(int*)((byte*)ptr + 4);
			Position position = bool_3 ? new Position(0, 0) : UI.smethod_47(Enum10.const_3, int_4, int_5);
			Rectangle result = new Rectangle(position.X + *(int*)((byte*)ptr + 4), position.Y + *(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 8));
			((byte*)ptr)[12] = ((int_4 == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				((byte*)ptr)[13] = ((int_5 == 4) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					result.Y = position.Y;
				}
			}
			((byte*)ptr)[14] = ((int_4 == 11) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 14) != 0)
			{
				((byte*)ptr)[15] = ((int_5 == 4) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 15) != 0)
				{
					result.X = position.X;
					result.Y = position.Y;
				}
				else
				{
					result.X = position.X;
				}
			}
			return result;
		}

		public static void smethod_94()
		{
			ProcessHelper.smethod_13(true);
			Win32.smethod_14(UI.mainForm_0.string_4, false);
			Thread.Sleep(200);
			Position position;
			if (!UI.smethod_3(out position, Images.Challenges, UI.getString_0(107378853)))
			{
				UI.smethod_94();
			}
		}

		public unsafe static void smethod_95(int int_4, int int_5 = 0)
		{
			void* ptr = stackalloc byte[3];
			JObject jobject = Stashes.Layout[UI.getString_0(107379758)];
			*(byte*)ptr = ((int_5 == 0) ? 1 : 0);
			string b;
			if (*(sbyte*)ptr != 0)
			{
				b = jobject[UI.getString_0(107380141)][int_4.ToString()][UI.getString_0(107379410)].ToString();
			}
			else
			{
				b = jobject[UI.getString_0(107380141)][string.Format(UI.getString_0(107378868), int_4, int_5)][UI.getString_0(107379410)].ToString();
			}
			((byte*)ptr)[1] = ((UI.string_0 == b) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) == 0)
			{
				Position position_ = Class251.smethod_2(b);
				Win32.smethod_5(position_, false);
				UI.smethod_36(Enum2.const_2, 1.0);
				Win32.smethod_2(true);
				((byte*)ptr)[2] = ((int_5 > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					position_ = Class251.smethod_3(b);
					Win32.smethod_5(position_, false);
					UI.smethod_36(Enum2.const_2, 1.0);
					Win32.smethod_2(true);
				}
				UI.string_0 = b;
			}
		}

		public static void smethod_96(int int_4)
		{
			JObject jobject = Stashes.Layout[UI.getString_0(107379779)];
			JToken jtoken = jobject[UI.getString_0(107380141)][int_4.ToString()];
			if (jtoken != null)
			{
				string b = jtoken[UI.getString_0(107379410)].smethod_10();
				if (!b.smethod_25() && !(UI.string_1 == b))
				{
					Position position_ = Class251.smethod_4(b);
					Win32.smethod_5(position_, false);
					UI.smethod_36(Enum2.const_2, 1.0);
					Win32.smethod_2(true);
					UI.string_1 = b;
				}
			}
		}

		public static void smethod_97(string string_2 = "general")
		{
			UI.string_1 = string_2;
			UI.string_0 = string_2;
		}

		public unsafe static Bitmap smethod_98()
		{
			void* ptr = stackalloc byte[20];
			*(int*)ptr = Util.smethod_22((double)Class308.smethod_0(Images.EmptyCell).Item1.Width * 0.53);
			Position position = UI.smethod_47(Enum10.const_3, 0, 0);
			Position position2 = UI.smethod_47(Enum10.const_3, 12, 5);
			*(int*)((byte*)ptr + 4) = position.X + *(int*)ptr;
			*(int*)((byte*)ptr + 8) = position.Y + *(int*)ptr;
			*(int*)((byte*)ptr + 12) = position2.X - *(int*)ptr - *(int*)((byte*)ptr + 4);
			*(int*)((byte*)ptr + 16) = position2.Y - *(int*)ptr - *(int*)((byte*)ptr + 8);
			Rectangle rectangle_ = new Rectangle(*(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12), *(int*)((byte*)ptr + 16));
			return Class197.smethod_1(rectangle_, UI.getString_0(107396942));
		}

		public static void smethod_99()
		{
			Win32.smethod_4(-2, -2, 50, 90, false);
			Position position;
			if (!UI.smethod_3(out position, Images.Social, UI.getString_0(107378491)))
			{
				Win32.smethod_14(UI.mainForm_0.string_3, false);
			}
			Thread.Sleep(50);
			Win32.smethod_5(Class251.CreatePartyLocation, false);
			Thread.Sleep(250);
			Win32.smethod_2(true);
		}

		public static List<Position> smethod_100()
		{
			List<Position> result;
			using (Bitmap bitmap = Class197.smethod_1(Class251.Stash, UI.getString_0(107396942)))
			{
				Tuple<Bitmap, double> tuple = Class308.smethod_0(Images.RequestAcceptDecline);
				result = Class196.smethod_1(bitmap, tuple.Item1, tuple.Item2).Select(new Func<Position, Position>(UI.<>c.<>9.method_13)).OrderBy(new Func<Position, int>(UI.<>c.<>9.method_14)).ToList<Position>();
			}
			return result;
		}

		public unsafe static string smethod_101(Position position_1)
		{
			void* ptr = stackalloc byte[2];
			string text = string.Empty;
			Bitmap item = Class308.smethod_0(Images.RequestAcceptDecline).Item1;
			Bitmap item2 = Class308.smethod_0(Images.WhisperPlayer).Item1;
			Win32.smethod_5(position_1.smethod_6(0, -item.Height / 2), false);
			Thread.Sleep(100);
			Win32.smethod_3();
			Thread.Sleep(200);
			Position position;
			*(byte*)ptr = ((!UI.smethod_3(out position, Images.WhisperPlayer, UI.getString_0(107397789))) ? 1 : 0);
			string result;
			if (*(sbyte*)ptr != 0)
			{
				result = null;
			}
			else
			{
				Clipboard.Clear();
				Win32.smethod_5(position.smethod_6(item2.Width / 2, item2.Height / 2), false);
				Thread.Sleep(400);
				Win32.smethod_2(true);
				Thread.Sleep(100);
				Win32.smethod_14(UI.getString_0(107378823), false);
				Win32.smethod_14(UI.getString_0(107378818), false);
				Thread.Sleep(200);
				Win32.smethod_14(UI.getString_0(107393936), false);
				string text2 = Clipboard.GetText();
				((byte*)ptr)[1] = ((text2 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_3(Enum11.const_3, UI.getString_0(107378845));
					result = null;
				}
				else
				{
					text = text2.Replace(UI.getString_0(107378808), string.Empty).Trim();
					Class181.smethod_2(Enum11.const_3, UI.getString_0(107378803), new object[]
					{
						text
					});
					result = text;
				}
			}
			return result;
		}

		public unsafe static void smethod_102(Position position_1)
		{
			void* ptr = stackalloc byte[2];
			Bitmap item = Class308.smethod_0(Images.RequestAccept).Item1;
			DateTime t = DateTime.Now.AddSeconds(3.0);
			for (;;)
			{
				((byte*)ptr)[1] = ((DateTime.Now < t) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					break;
				}
				*(byte*)ptr = (UI.smethod_8(Class251.Stash, Images.LeaveParty) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					break;
				}
				Win32.smethod_5(position_1.smethod_6(item.Width / 2, item.Height / 2), false);
				UI.smethod_36(Enum2.const_17, 1.0);
				Win32.smethod_2(true);
				Thread.Sleep(50);
			}
		}

		public static void smethod_103(Position position_1)
		{
			Bitmap item = Class308.smethod_0(Images.RequestAcceptDecline).Item1;
			Bitmap item2 = Class308.smethod_0(Images.RequestAccept).Item1;
			Win32.smethod_5(position_1.smethod_6(item.Width / 2 + item2.Width / 2, item.Height / 2), false);
			UI.smethod_36(Enum2.const_17, 1.5);
			Win32.smethod_2(true);
		}

		public static void smethod_104()
		{
			UI.DwmEnableComposition(UI.uint_0);
		}

		public static bool smethod_105()
		{
			Images images_ = UI.SmallChat ? Images.ChatOpen : Images.ChatLocal;
			Position position;
			return UI.smethod_3(out position, images_, UI.getString_0(107351014));
		}

		public unsafe static Position smethod_106()
		{
			void* ptr = stackalloc byte[3];
			Position result;
			*(byte*)ptr = (UI.smethod_3(out result, Images.RequestAcceptDecline, UI.getString_0(107378726)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Position position;
				((byte*)ptr)[1] = (UI.smethod_3(out position, Images.TradeRequest, UI.getString_0(107378693)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return result;
				}
				Win32.smethod_5(result.smethod_5(Class251.TradeRequestFullOffset), false);
				Thread.Sleep(200);
				((byte*)ptr)[2] = (UI.smethod_3(out position, Images.TradeRequestFull, UI.getString_0(107378668)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					return result;
				}
			}
			return new Position();
		}

		public unsafe static void smethod_107()
		{
			void* ptr = stackalloc byte[2];
			Order order_ = TradeProcessor.order_0;
			*(byte*)ptr = ((order_ == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = ((!Directory.Exists(UI.getString_0(107378639))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Directory.CreateDirectory(UI.getString_0(107378639));
				}
				string filename = string.Format(UI.getString_0(107378654), UI.getString_0(107378639), order_.ProcessedTime, order_.player.name);
				UI.GameImage.Save(filename);
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static UI()
		{
			Strings.CreateGetStringDelegate(typeof(UI));
			UI.uint_0 = 0U;
			UI.uint_1 = 1U;
			UI.intptr_0 = IntPtr.Zero;
			UI.list_0 = new List<TradeWindowItem>();
			UI.string_0 = UI.getString_0(107378101);
			UI.string_1 = UI.getString_0(107378101);
		}

		private const int int_0 = 1;

		private const int int_1 = 4;

		private static readonly uint uint_0;

		private static readonly uint uint_1;

		public static Position position_0;

		private static bool bool_0;

		public static Struct2 struct2_0;

		private static Struct2 struct2_1;

		private static MainForm mainForm_0;

		public static IntPtr intptr_0;

		private static bool bool_1;

		public static int int_2;

		public static int int_3;

		public static DateTime dateTime_0;

		public static Bitmap bitmap_0;

		public static List<TradeWindowItem> list_0;

		public static List<Item> list_1;

		public static Bitmap bitmap_1;

		public static Bitmap bitmap_2;

		public static List<Position> list_2;

		public static bool bool_2;

		private static string string_0;

		private static string string_1;

		[NonSerialized]
		internal static GetString getString_0;

		private enum Enum0
		{
			const_0 = 10,
			const_1 = 117
		}

		[CompilerGenerated]
		private sealed class Class110
		{
			internal void method_0()
			{
				UI.mainForm_0.toolStripLabel_2.Text = string.Format(UI.Class110.getString_0(107249677), this.string_0, this.string_1);
				if ((int)this.double_0 + 1 < UI.mainForm_0.toolStripProgressBar_0.Maximum)
				{
					UI.mainForm_0.toolStripProgressBar_0.Value = (int)this.double_0 + 1;
					UI.mainForm_0.toolStripProgressBar_0.Value--;
				}
				else
				{
					UI.mainForm_0.toolStripProgressBar_0.Value = UI.mainForm_0.toolStripProgressBar_0.Maximum;
				}
			}

			static Class110()
			{
				Strings.CreateGetStringDelegate(typeof(UI.Class110));
			}

			public string string_0;

			public string string_1;

			public double double_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class111
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class112
		{
			public int int_0;

			public UI.Class111 class111_0;
		}

		[CompilerGenerated]
		private sealed class Class113
		{
			internal bool method_0(Item item_1)
			{
				return item_1.method_1(this.item_0, this.class112_0.class111_0.int_0, this.class112_0.int_0);
			}

			public Item item_0;

			public UI.Class112 class112_0;
		}

		[CompilerGenerated]
		private sealed class Class114
		{
			internal bool method_0(Player player_1)
			{
				return player_1.name == this.player_0.name;
			}

			internal bool method_1(Player player_1)
			{
				return player_1.name == this.player_0.name;
			}

			public Player player_0;
		}

		[CompilerGenerated]
		private sealed class Class115
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class116
		{
			internal bool method_0(TradeWindowItem tradeWindowItem_0)
			{
				return tradeWindowItem_0.Position.X == this.class115_0.int_0 && tradeWindowItem_0.Position.Y == this.int_0;
			}

			public int int_0;

			public UI.Class115 class115_0;
		}

		[CompilerGenerated]
		private sealed class Class117
		{
			internal bool method_0(TradeWindowItem tradeWindowItem_0)
			{
				return tradeWindowItem_0.Position.X == this.int_0 && tradeWindowItem_0.Position.Y == this.int_1;
			}

			internal bool method_1(Item item_1)
			{
				return item_1.method_1(this.item_0, this.int_0, this.int_1);
			}

			public int int_0;

			public int int_1;

			public Item item_0;
		}

		[CompilerGenerated]
		private sealed class Class118
		{
			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class119
		{
			internal bool method_0(TradeWindowItem tradeWindowItem_0)
			{
				return tradeWindowItem_0.Position.X == this.class118_0.int_0 && tradeWindowItem_0.Position.Y == this.int_0;
			}

			public int int_0;

			public UI.Class118 class118_0;
		}
	}
}
