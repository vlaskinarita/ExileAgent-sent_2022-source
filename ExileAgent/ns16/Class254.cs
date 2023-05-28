using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ns16
{
	internal sealed class Class254
	{
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool PostMessage(IntPtr intptr_0, int int_3, Keys keys_0, int int_4);

		[DllImport("user32.dll", EntryPoint = "PostMessage")]
		private static extern bool PostMessage_1(IntPtr intptr_0, uint uint_2, int int_3, int int_4);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindowEx(IntPtr intptr_0, IntPtr intptr_1, string string_0, string string_1);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr intptr_0, uint uint_2, IntPtr intptr_1, IntPtr intptr_2);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string string_0, string string_1);

		public static IntPtr smethod_0(string string_0)
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.MainWindowHandle != IntPtr.Zero && process.MainWindowTitle.ToLower() == string_0.ToLower())
				{
					return process.MainWindowHandle;
				}
			}
			return IntPtr.Zero;
		}

		public static void smethod_1(IntPtr intptr_0, Keys keys_0)
		{
			Class254.PostMessage(intptr_0, Class254.int_0, keys_0, 0);
		}

		public static IntPtr smethod_2(int int_3, int int_4)
		{
			return (IntPtr)(int_4 << 16 | int_3);
		}

		public static void smethod_3(IntPtr intptr_0, int int_3, int int_4)
		{
			Class254.SendMessage(intptr_0, Class254.uint_0, (IntPtr)1, Class254.smethod_2(int_3, int_4));
		}

		private static int int_0 = 256;

		private static int int_1 = 257;

		private static uint uint_0 = 513U;

		private static uint uint_1 = 514U;

		private static int int_2 = 1;
	}
}
