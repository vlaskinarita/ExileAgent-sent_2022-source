using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ns0;
using ns12;
using ns14;
using ns25;
using ns29;
using ns32;
using ns34;
using ns35;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Classes
{
	public sealed class Win32
	{
		private static int MaxMouseSpeed { get; set; }

		public static void smethod_0(int int_11)
		{
			Win32.MaxMouseSpeed = int_11;
		}

		private static void smethod_1()
		{
			while (MainForm.IsPaused)
			{
				Thread.Sleep(50);
			}
		}

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		private static extern void mouse_event(uint uint_1, uint uint_2, uint uint_3, int int_11, uint uint_4);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string string_0, string string_1);

		[DllImport("User32.Dll")]
		private static extern long SetCursorPos(int int_11, int int_12);

		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern int GetWindowText(IntPtr intptr_0, StringBuilder stringBuilder_0, int int_11);

		[DllImport("user32.dll")]
		private static extern bool SetClipboardData(uint uint_1, IntPtr intptr_0);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsClipboardFormatAvailable(uint uint_1);

		[DllImport("User32.dll", SetLastError = true)]
		private static extern IntPtr GetClipboardData(uint uint_1);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool OpenClipboard(IntPtr intptr_0);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseClipboard();

		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern IntPtr GlobalLock(IntPtr intptr_0);

		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GlobalUnlock(IntPtr intptr_0);

		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern int GlobalSize(IntPtr intptr_0);

		[DllImport("user32.dll")]
		private static extern IntPtr GetOpenClipboardWindow();

		[DllImport("user32.dll", SetLastError = true)]
		private static extern void keybd_event(byte byte_0, byte byte_1, int int_11, int int_12);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowPlacement(IntPtr intptr_0, ref Win32.WINDOWPLACEMENT windowplacement_0);

		[DllImport("user32.dll")]
		private static extern bool SetWindowPlacement(IntPtr intptr_0, [In] ref Win32.WINDOWPLACEMENT windowplacement_0);

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr intptr_0, int int_11);

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out Point point_0);

		public static void smethod_2(bool bool_0 = true)
		{
			Win32.smethod_1();
			if (bool_0)
			{
				Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107449431), new Point(Cursor.Position.X - UI.struct2_0.int_0, Cursor.Position.Y - UI.struct2_0.int_1)));
			}
			Win32.mouse_event(2U, 0U, 0U, 0, 0U);
			Win32.mouse_event(4U, 0U, 0U, 0, 0U);
			Thread.Sleep(20);
			Win32.dateTime_0 = DateTime.Now;
		}

		public static void smethod_3()
		{
			Win32.smethod_1();
			Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107449406), new Point(Cursor.Position.X - UI.struct2_0.int_0, Cursor.Position.Y - UI.struct2_0.int_1)));
			Win32.mouse_event(8U, 0U, 0U, 0, 0U);
			Win32.mouse_event(16U, 0U, 0U, 0, 0U);
			Thread.Sleep(20);
			Win32.dateTime_0 = DateTime.Now;
		}

		public unsafe static void smethod_4(int int_11 = -2, int int_12 = -2, int int_13 = 50, int int_14 = 90, bool bool_0 = false)
		{
			void* ptr = stackalloc byte[119];
			Win32.smethod_1();
			Point point;
			Win32.GetCursorPos(out point);
			Point point2 = new Point(int_11, int_12);
			Point point3 = new Point(UI.struct2_0.int_2 - (int)Class251.WindowOffset.X * 2, UI.struct2_0.int_1 + (int)Class251.WindowOffset.Y);
			if (int_11 == -2 && int_12 == -2)
			{
				*(double*)ptr = 6.0 * UI.GameScale;
				Win32.GetCursorPos(out point2);
				point2.X -= UI.struct2_0.int_0;
				point2.Y -= UI.struct2_0.int_1;
				Rectangle rectangle = Class251.Stash;
				((byte*)ptr)[112] = (rectangle.Contains(point2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 112) != 0)
				{
					*(double*)((byte*)ptr + 8) = Util.smethod_4(point2.X, (double)Class251.Stash.Left - *(double*)ptr, (double)Class251.Stash.Right + *(double*)ptr);
					*(double*)((byte*)ptr + 16) = Util.smethod_4(point2.Y, (double)Class251.StashTabBar.Top - *(double*)ptr, (double)Class251.Stash.Bottom + *(double*)ptr);
					*(double*)((byte*)ptr + 24) = Math.Abs(*(double*)((byte*)ptr + 8) - (double)point2.X);
					*(double*)((byte*)ptr + 32) = Math.Abs(*(double*)((byte*)ptr + 16) - (double)point2.Y);
					((byte*)ptr)[113] = ((*(double*)((byte*)ptr + 24) < *(double*)((byte*)ptr + 32)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 113) != 0)
					{
						point2 = new Point((int)Math.Round(*(double*)((byte*)ptr + 8)), point2.Y);
					}
					else
					{
						point2 = new Point(point2.X, (int)Math.Round(*(double*)((byte*)ptr + 16)));
					}
				}
				else
				{
					rectangle = Class251.Inventory;
					((byte*)ptr)[114] = (rectangle.Contains(point2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 114) != 0)
					{
						*(double*)((byte*)ptr + 40) = Util.smethod_4(point2.X, (double)Class251.Inventory.Left - *(double*)ptr, (double)Class251.Inventory.Right + *(double*)ptr);
						*(double*)((byte*)ptr + 48) = Util.smethod_4(point2.Y, (double)Class251.Inventory.Top - *(double*)ptr, (double)Class251.Inventory.Bottom + *(double*)ptr);
						*(double*)((byte*)ptr + 56) = Math.Abs(*(double*)((byte*)ptr + 40) - (double)point2.X);
						*(double*)((byte*)ptr + 64) = Math.Abs(*(double*)((byte*)ptr + 48) - (double)point2.Y);
						((byte*)ptr)[115] = ((*(double*)((byte*)ptr + 56) < *(double*)((byte*)ptr + 64)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 115) != 0)
						{
							point2 = new Point((int)Math.Round(*(double*)((byte*)ptr + 40)), point2.Y);
						}
						else
						{
							point2 = new Point(point2.X, (int)Math.Round(*(double*)((byte*)ptr + 48)));
						}
					}
					else
					{
						rectangle = Class251.InventoryWindow;
						((byte*)ptr)[116] = (rectangle.Contains(point2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 116) != 0)
						{
							*(double*)((byte*)ptr + 72) = Util.smethod_4(point2.X, (double)Class251.Inventory.Left - *(double*)ptr, (double)Class251.Inventory.Right + *(double*)ptr);
							*(double*)((byte*)ptr + 80) = Util.smethod_4(point2.Y, (double)Class251.InventoryWindow.Top + 90.0 * UI.GameScale - *(double*)ptr, (double)Class251.Inventory.Top - *(double*)ptr);
							*(double*)((byte*)ptr + 88) = Math.Abs(*(double*)((byte*)ptr + 72) - (double)point2.X);
							*(double*)((byte*)ptr + 96) = Math.Abs(*(double*)((byte*)ptr + 80) - (double)point2.Y);
							((byte*)ptr)[117] = ((*(double*)((byte*)ptr + 88) < *(double*)((byte*)ptr + 96)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 117) != 0)
							{
								point2 = new Point((int)Math.Round(*(double*)((byte*)ptr + 72)), point2.Y);
							}
							else
							{
								point2 = new Point(point2.X, (int)Math.Round(*(double*)((byte*)ptr + 80)));
							}
						}
					}
				}
			}
			else if (int_11 == 0 && int_12 == 0)
			{
				point2 = point3;
			}
			if (int_11 != 0 && int_12 != 0 && point2 != point3)
			{
				point2.X += UI.struct2_0.int_0;
				point2.Y += UI.struct2_0.int_1;
			}
			*(float*)((byte*)ptr + 104) = UI.smethod_81();
			point2.X = (int)Math.Round((double)((float)point2.X / *(float*)((byte*)ptr + 104)));
			point2.Y = (int)Math.Round((double)((float)point2.Y / *(float*)((byte*)ptr + 104)));
			Win32.position_0 = new Position(point2.X, point2.Y);
			*(int*)((byte*)ptr + 108) = Win32.random_0.Next(5, 35);
			((byte*)ptr)[118] = (bool_0 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 118) != 0)
			{
				Win32.SetCursorPos(point2.X, point2.Y);
				Thread.Sleep(*(int*)((byte*)ptr + 108));
			}
			else
			{
				*(int*)((byte*)ptr + 108) = Win32.smethod_6(point2, int_13, int_14);
			}
			Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107449381), new object[]
			{
				point.X - UI.struct2_0.int_0,
				point.Y - UI.struct2_0.int_1,
				point2.X - UI.struct2_0.int_0,
				point2.Y - UI.struct2_0.int_1,
				bool_0 ? Win32.getString_0(107448743) : Win32.getString_0(107448748),
				*(int*)((byte*)ptr + 108),
				int_11,
				int_12
			}));
		}

		public static void smethod_5(Position position_1, bool bool_0 = false)
		{
			Win32.smethod_1();
			Win32.position_0 = new Position(position_1.X, position_1.Y);
			Win32.smethod_4(position_1.Left, position_1.Top, 50, 90, bool_0);
		}

		private unsafe static int smethod_6(Point point_0, int int_11, int int_12)
		{
			void* ptr = stackalloc byte[42];
			Point point;
			Win32.GetCursorPos(out point);
			((byte*)ptr)[40] = (point_0.Equals(point) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 40) != 0)
			{
				*(int*)((byte*)ptr + 32) = 0;
			}
			else
			{
				PointF value = point;
				PointF pointF = new PointF((float)(point_0.X - point.X), (float)(point_0.Y - point.Y));
				*(int*)((byte*)ptr + 8) = Win32.MaxMouseSpeed + 1;
				*(int*)((byte*)ptr + 12) = (*(int*)((byte*)ptr + 8) - Class255.class105_0.method_5(ConfigOptions.MouseMoveSpeed)) * 66;
				*(int*)((byte*)ptr + 16) = (*(int*)((byte*)ptr + 8) - Class255.class105_0.method_5(ConfigOptions.MouseMoveSpeed)) * 100;
				*(double*)ptr = Math.Sqrt(Math.Pow((double)pointF.X, 2.0) + Math.Pow((double)pointF.Y, 2.0));
				*(int*)((byte*)ptr + 20) = (int)Math.Round(*(double*)ptr / 350.0 * (double)(*(int*)((byte*)ptr + 12))) + 20;
				*(int*)((byte*)ptr + 24) = (int)Math.Round(*(double*)ptr / 350.0 * (double)(*(int*)((byte*)ptr + 16))) + 20;
				*(int*)((byte*)ptr + 28) = (int)Math.Min((double)Win32.random_0.Next(*(int*)((byte*)ptr + 20), *(int*)((byte*)ptr + 24)), 1.5 * (double)(*(int*)((byte*)ptr + 16))) + Win32.random_0.Next(int_11, int_12);
				pointF.X /= (float)(*(int*)((byte*)ptr + 28));
				pointF.Y /= (float)(*(int*)((byte*)ptr + 28));
				*(int*)((byte*)ptr + 36) = 0;
				for (;;)
				{
					((byte*)ptr)[41] = ((*(int*)((byte*)ptr + 36) < *(int*)((byte*)ptr + 28)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 41) == 0)
					{
						break;
					}
					value = new PointF(value.X + pointF.X, value.Y + pointF.Y);
					Win32.smethod_8(Point.Round(value));
					Win32.smethod_7(1);
					*(int*)((byte*)ptr + 36) = *(int*)((byte*)ptr + 36) + 1;
				}
				Win32.smethod_8(point_0);
				*(int*)((byte*)ptr + 32) = *(int*)((byte*)ptr + 28);
			}
			return *(int*)((byte*)ptr + 32);
		}

		private static void smethod_7(int int_11)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (stopwatch.ElapsedMilliseconds < (long)int_11)
			{
			}
		}

		private static void smethod_8(Point point_0)
		{
			Win32.SetCursorPos(point_0.X, point_0.Y);
		}

		public static void smethod_9()
		{
			Win32.smethod_1();
			Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107448770), new Point(Cursor.Position.X - UI.struct2_0.int_0, Cursor.Position.Y - UI.struct2_0.int_1)));
			Win32.keybd_event(17, 69, 1, 0);
			Win32.smethod_2(false);
			Thread.Sleep(30);
			Win32.keybd_event(17, 69, 3, 0);
			Win32.dateTime_0 = DateTime.Now;
		}

		public static void smethod_10()
		{
			Win32.smethod_1();
			Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107448713), new Point(Cursor.Position.X - UI.struct2_0.int_0, Cursor.Position.Y - UI.struct2_0.int_1)));
			Win32.keybd_event(16, 69, 1, 0);
			Win32.smethod_2(false);
			Thread.Sleep(30);
			Win32.keybd_event(16, 69, 3, 0);
			Win32.dateTime_0 = DateTime.Now;
		}

		public static void smethod_11()
		{
			Win32.smethod_1();
			if (!Position.smethod_0(Win32.position_0, null))
			{
				Class181.smethod_2(Enum11.const_3, Win32.getString_0(107448688), new object[]
				{
					Win32.position_0
				});
				Win32.smethod_5(Win32.position_0, false);
			}
		}

		public static void smethod_12(Enum6 enum6_0, int int_11)
		{
			Win32.smethod_1();
			Class181.smethod_2(Enum11.const_3, Win32.getString_0(107448655), new object[]
			{
				int_11,
				enum6_0
			});
			int num = (enum6_0 == Enum6.const_0) ? 1 : -1;
			Win32.mouse_event(2048U, 0U, 0U, 120 * int_11 * num, 0U);
		}

		public static void smethod_13()
		{
			foreach (object obj in InputLanguage.InstalledInputLanguages)
			{
				InputLanguage inputLanguage = (InputLanguage)obj;
				if (inputLanguage.Culture.Name.StartsWith(Win32.getString_0(107363861), StringComparison.OrdinalIgnoreCase))
				{
					InputLanguage.CurrentInputLanguage = inputLanguage;
					break;
				}
			}
		}

		public static void smethod_14(string string_0, bool bool_0 = false)
		{
			if (!bool_0)
			{
				Win32.smethod_1();
			}
			Class181.smethod_2(Enum11.const_3, Win32.getString_0(107448626), new object[]
			{
				string_0
			});
			SendKeys.SendWait(string_0);
			Win32.dateTime_0 = DateTime.Now;
		}

		public static void smethod_15(int int_11)
		{
			Win32.smethod_1();
			Class181.smethod_2(Enum11.const_3, Win32.getString_0(107448633), new object[]
			{
				int_11
			});
			string text = int_11.ToString();
			for (int i = 0; i < text.Length; i++)
			{
				SendKeys.SendWait(text[i].ToString());
			}
			Thread.Sleep(2 * Class120.dictionary_0[Enum2.const_2]);
			SendKeys.SendWait(Win32.getString_0(107382524));
			Win32.dateTime_0 = DateTime.Now;
		}

		public unsafe static void smethod_16(string string_0, bool bool_0 = true, bool bool_1 = true, bool bool_2 = false, bool bool_3 = false)
		{
			void* ptr = stackalloc byte[13];
			((byte*)ptr)[5] = ((!bool_3) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				Win32.smethod_1();
			}
			UI.smethod_1();
			Class181.smethod_2(Enum11.const_3, Win32.getString_0(107448608), new object[]
			{
				string_0
			});
			Images images_ = UI.SmallChat ? Images.ChatOpen : Images.ChatLocal;
			Position position;
			((byte*)ptr)[4] = (UI.smethod_3(out position, images_, Win32.getString_0(107351563)) ? 1 : 0);
			((byte*)ptr)[6] = (UI.smethod_105() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 6) != 0)
			{
				Class181.smethod_3(Enum11.const_3, Win32.getString_0(107448559));
				Win32.smethod_14(Win32.getString_0(107394485), bool_3);
				Thread.Sleep(200);
				Win32.smethod_16(string_0, bool_0, bool_1, bool_2, bool_3);
			}
			else
			{
				((byte*)ptr)[7] = (UI.smethod_3(out position, Images.EscMenu, Win32.getString_0(107351608)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) != 0)
				{
					Class181.smethod_3(Enum11.const_3, Win32.getString_0(107449050));
					Win32.smethod_14(Win32.getString_0(107394485), bool_3);
					Thread.Sleep(300);
					Win32.smethod_16(string_0, bool_0, bool_1, bool_2, bool_3);
				}
				else
				{
					((byte*)ptr)[8] = (UI.smethod_3(out position, Images.KeepDestroyWindow, Win32.getString_0(107394530)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						Class181.smethod_3(Enum11.const_3, Win32.getString_0(107448969));
						Win32.smethod_14(Win32.getString_0(107394485), bool_3);
						Thread.Sleep(1000);
						Win32.smethod_16(string_0, bool_0, bool_1, bool_2, bool_3);
					}
					else
					{
						Win32.smethod_22(string_0, bool_3);
						((byte*)ptr)[9] = (bool_0 ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							Win32.smethod_14(Win32.getString_0(107382524), bool_3);
							*(int*)ptr = 0;
							for (;;)
							{
								((byte*)ptr)[11] = ((*(int*)ptr < 12) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 11) == 0)
								{
									break;
								}
								using (Bitmap bitmap = Class197.smethod_1(Class251.ChatLocation, Win32.getString_0(107397491)))
								{
									((byte*)ptr)[10] = (UI.smethod_9(bitmap, images_) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 10) != 0)
									{
										break;
									}
									Thread.Sleep(100);
									*(int*)ptr = *(int*)ptr + 1;
								}
							}
						}
						if (bool_2 || ((bool_0 ? 1 : 0) & *(sbyte*)((byte*)ptr + 4)) != 0)
						{
							Win32.smethod_14(Win32.getString_0(107381157), bool_3);
							Thread.Sleep(200);
						}
						Win32.smethod_14(Win32.getString_0(107381786), bool_3);
						((byte*)ptr)[12] = (bool_1 ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 12) != 0)
						{
							Thread.Sleep(200);
							Win32.smethod_14(Win32.getString_0(107382524), bool_3);
							Thread.Sleep(200);
						}
					}
				}
			}
		}

		public static void smethod_17()
		{
			Win32.smethod_1();
			Win32.keybd_event(17, 69, 1, 0);
			Thread.Sleep(50);
		}

		public static void smethod_18()
		{
			Win32.smethod_1();
			Win32.keybd_event(17, 69, 3, 0);
			Thread.Sleep(50);
		}

		public static void smethod_19()
		{
			Win32.smethod_1();
			Win32.keybd_event(16, 69, 1, 0);
			Thread.Sleep(50);
		}

		public static void smethod_20()
		{
			Win32.smethod_1();
			Win32.keybd_event(16, 42, 3, 0);
			Thread.Sleep(50);
		}

		public static string smethod_21()
		{
			Win32.Class201 @class = new Win32.Class201();
			Win32.smethod_1();
			Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107448952), new Point(Cursor.Position.X - UI.struct2_0.int_0, Cursor.Position.Y - UI.struct2_0.int_1)));
			@class.string_0 = null;
			@class.dateTime_0 = DateTime.Now;
			Thread thread = new Thread(new ThreadStart(@class.method_0));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			Win32.dateTime_0 = DateTime.Now;
			Class181.smethod_3(Enum11.const_3, string.Format(Win32.getString_0(107448927), (Win32.dateTime_0 - @class.dateTime_0).TotalMilliseconds.ToString()));
			return @class.string_0;
		}

		public static void smethod_22(string string_0, bool bool_0 = false)
		{
			Win32.Class202 @class = new Win32.Class202();
			@class.string_0 = string_0;
			if (!bool_0)
			{
				Win32.smethod_1();
			}
			Thread thread = new Thread(new ThreadStart(@class.method_0));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			Win32.dateTime_0 = DateTime.Now;
		}

		private static string smethod_23()
		{
			string result;
			if (Win32.OpenClipboard(Win32.GetOpenClipboardWindow()))
			{
				IntPtr clipboardData = Win32.GetClipboardData(13U);
				Win32.CloseClipboard();
				result = Marshal.PtrToStringAuto(clipboardData);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static Win32.WINDOWPLACEMENT smethod_24(IntPtr intptr_0)
		{
			Win32.WINDOWPLACEMENT windowplacement = default(Win32.WINDOWPLACEMENT);
			windowplacement.length = Marshal.SizeOf<Win32.WINDOWPLACEMENT>(windowplacement);
			Win32.GetWindowPlacement(intptr_0, ref windowplacement);
			return windowplacement;
		}

		public static void smethod_25(IntPtr intptr_0)
		{
			Win32.ShowWindow(intptr_0, 1);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Win32()
		{
			Strings.CreateGetStringDelegate(typeof(Win32));
			Win32.random_0 = new Random();
			Win32.position_0 = new Position();
		}

		private const int int_0 = 2;

		private const int int_1 = 4;

		private const int int_2 = 8;

		private const int int_3 = 16;

		private const int int_4 = 2048;

		public const int int_5 = 1;

		public const int int_6 = 2;

		public const int int_7 = 17;

		public const int int_8 = 16;

		private const uint uint_0 = 13U;

		public static DateTime dateTime_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static int int_9;

		public const int int_10 = 1;

		private static Random random_0;

		private static Position position_0;

		[NonSerialized]
		internal static GetString getString_0;

		[Serializable]
		public struct WINDOWPLACEMENT
		{
			public int length;

			public int flags;

			public Win32.ShowWindowCommands showCmd;

			public Point ptMinPosition;

			public Point ptMaxPosition;

			public Rectangle rcNormalPosition;
		}

		public enum ShowWindowCommands
		{
			Hide,
			Normal,
			Minimized,
			Maximized
		}

		[CompilerGenerated]
		private sealed class Class201
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[10];
				try
				{
					Clipboard.Clear();
					DateTime t = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)Class255.class105_0.method_6(ConfigOptions.ClipboardTiming));
					for (;;)
					{
						((byte*)ptr)[9] = ((this.string_0 == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) == 0)
						{
							break;
						}
						Win32.smethod_14(Win32.Class201.getString_0(107249080), false);
						this.string_0 = Win32.smethod_23();
						((byte*)ptr)[8] = ((t < DateTime.Now) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							this.string_0 = Win32.Class201.getString_0(107382131);
						}
						Thread.Sleep(10);
					}
					Clipboard.Clear();
				}
				catch (Exception)
				{
					Enum11 enum11_ = Enum11.const_2;
					string format = Win32.Class201.getString_0(107249107);
					*(double*)ptr = (DateTime.Now - this.dateTime_0).TotalMilliseconds;
					Class181.smethod_3(enum11_, string.Format(format, ((double*)ptr)->ToString()));
				}
			}

			static Class201()
			{
				Strings.CreateGetStringDelegate(typeof(Win32.Class201));
			}

			public string string_0;

			public DateTime dateTime_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class202
		{
			internal void method_0()
			{
				try
				{
					Clipboard.Clear();
					if (this.string_0 != null)
					{
						Class181.smethod_2(Enum11.const_3, Win32.Class202.getString_0(107249064), new object[]
						{
							this.string_0
						});
						Clipboard.SetText(this.string_0);
					}
				}
				catch (ExternalException)
				{
					Class181.smethod_3(Enum11.const_2, Win32.Class202.getString_0(107248991));
				}
				catch (Exception arg)
				{
					Class181.smethod_3(Enum11.const_2, string.Format(Win32.Class202.getString_0(107248890), arg));
				}
			}

			static Class202()
			{
				Strings.CreateGetStringDelegate(typeof(Win32.Class202));
			}

			public string string_0;

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
