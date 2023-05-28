using System;
using System.Runtime.InteropServices;

namespace ns25
{
	internal sealed class Class244
	{
		[DllImport("user32.dll")]
		public static extern IntPtr GetMessageExtraInfo();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint uint_4, Class244.Struct5[] struct5_0, int int_3);

		public const int int_0 = 0;

		public const int int_1 = 1;

		public const int int_2 = 2;

		public const uint uint_0 = 1U;

		public const uint uint_1 = 2U;

		public const uint uint_2 = 4U;

		public const uint uint_3 = 8U;

		public struct Struct5
		{
			public int int_0;

			public Class244.Struct6 struct6_0;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct Struct6
		{
			[FieldOffset(0)]
			public Class244.Struct7 struct7_0;

			[FieldOffset(0)]
			public Class244.Struct8 struct8_0;

			[FieldOffset(0)]
			public Class244.Struct9 struct9_0;
		}

		public struct Struct7
		{
			public int int_0;

			public int int_1;

			public uint uint_0;

			public uint uint_1;

			public uint uint_2;

			public IntPtr intptr_0;
		}

		public struct Struct8
		{
			public ushort ushort_0;

			public ushort ushort_1;

			public uint uint_0;

			public uint uint_1;

			public IntPtr intptr_0;
		}

		public struct Struct9
		{
			public uint uint_0;

			public ushort ushort_0;

			public ushort ushort_1;
		}
	}
}
