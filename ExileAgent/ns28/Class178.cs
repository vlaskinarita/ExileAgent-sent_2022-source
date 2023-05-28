using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ns28
{
	internal static class Class178
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FlashWindowEx(ref Class178.Struct3 struct3_0);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowThreadProcessId(IntPtr intptr_0, out int int_0);

		public unsafe static bool smethod_0()
		{
			void* ptr = stackalloc byte[10];
			IntPtr foregroundWindow = Class178.GetForegroundWindow();
			((byte*)ptr)[8] = ((foregroundWindow == IntPtr.Zero) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				((byte*)ptr)[9] = 0;
			}
			else
			{
				*(int*)ptr = Process.GetCurrentProcess().Id;
				Class178.GetWindowThreadProcessId(foregroundWindow, out *(int*)((byte*)ptr + 4));
				((byte*)ptr)[9] = ((*(int*)((byte*)ptr + 4) == *(int*)ptr) ? 1 : 0);
			}
			return *(sbyte*)((byte*)ptr + 9) != 0;
		}

		public static bool smethod_1(IntPtr intptr_0)
		{
			bool result;
			if (Class178.Win2000OrLater && Class178.smethod_0())
			{
				Class178.Struct3 @struct = Class178.smethod_2(intptr_0, 15U, uint.MaxValue, 0U);
				result = Class178.FlashWindowEx(ref @struct);
			}
			else
			{
				result = false;
			}
			return result;
		}

		private static Class178.Struct3 smethod_2(IntPtr intptr_0, uint uint_6, uint uint_7, uint uint_8)
		{
			Class178.Struct3 @struct = default(Class178.Struct3);
			@struct.uint_0 = Convert.ToUInt32(Marshal.SizeOf<Class178.Struct3>(@struct));
			@struct.intptr_0 = intptr_0;
			@struct.uint_1 = uint_6;
			@struct.uint_2 = uint_7;
			@struct.uint_3 = uint_8;
			return @struct;
		}

		private static bool Win2000OrLater
		{
			get
			{
				return Environment.OSVersion.Version.Major >= 5;
			}
		}

		public const uint uint_0 = 0U;

		public const uint uint_1 = 1U;

		public const uint uint_2 = 2U;

		public const uint uint_3 = 3U;

		public const uint uint_4 = 4U;

		public const uint uint_5 = 12U;

		private struct Struct3
		{
			public uint uint_0;

			public IntPtr intptr_0;

			public uint uint_1;

			public uint uint_2;

			public uint uint_3;
		}
	}
}
