using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns7
{
	internal sealed class Class179
	{
		public List<Keys> HookedKeys { get; set; }

		public Class179()
		{
			this.intptr_0 = IntPtr.Zero;
			this.delegate1_0 = new Class179.Delegate1(this.method_1);
			this.HookedKeys = new List<Keys>();
		}

		[DllImport("user32.dll")]
		private static extern int CallNextHookEx(IntPtr intptr_1, int int_0, int int_1, ref Class179.Struct4 struct4_0);

		~Class179()
		{
			this.method_2();
		}

		public void method_0()
		{
			IntPtr intptr_ = Class179.LoadLibrary(Class179.getString_0(107452483));
			this.intptr_0 = Class179.SetWindowsHookEx(13, this.delegate1_0, intptr_, 0U);
		}

		public unsafe int method_1(int int_0, int int_1, ref Class179.Struct4 struct4_0)
		{
			void* ptr = stackalloc byte[7];
			((byte*)ptr)[4] = ((int_0 >= 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				Keys int_2 = (Keys)struct4_0.int_0;
				((byte*)ptr)[5] = (this.HookedKeys.Contains(int_2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					KeyEventArgs keyEventArgs = new KeyEventArgs(int_2);
					bool flag;
					if (int_1 != 256)
					{
						if (int_1 != 260)
						{
							flag = false;
							goto IL_5B;
						}
					}
					flag = (this.keyEventHandler_0 != null);
					IL_5B:
					if (flag)
					{
						this.keyEventHandler_0(this, keyEventArgs);
					}
					else
					{
						bool flag2;
						if (int_1 != 257)
						{
							if (int_1 != 261)
							{
								flag2 = false;
								goto IL_8A;
							}
						}
						flag2 = (this.keyEventHandler_1 != null);
						IL_8A:
						if (flag2)
						{
							this.keyEventHandler_1(this, keyEventArgs);
						}
					}
					((byte*)ptr)[6] = (keyEventArgs.Handled ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						*(int*)ptr = 1;
						goto IL_BE;
					}
				}
			}
			*(int*)ptr = Class179.CallNextHookEx(this.intptr_0, int_0, int_1, ref struct4_0);
			IL_BE:
			return *(int*)ptr;
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string string_0);

		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int int_0, Class179.Delegate1 delegate1_1, IntPtr intptr_1, uint uint_0);

		public void method_2()
		{
			Class179.UnhookWindowsHookEx(this.intptr_0);
		}

		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(IntPtr intptr_1);

		public event KeyEventHandler KeyDown
		{
			[CompilerGenerated]
			add
			{
				KeyEventHandler keyEventHandler = this.keyEventHandler_0;
				KeyEventHandler keyEventHandler2;
				do
				{
					keyEventHandler2 = keyEventHandler;
					KeyEventHandler value2 = (KeyEventHandler)Delegate.Combine(keyEventHandler2, value);
					keyEventHandler = Interlocked.CompareExchange<KeyEventHandler>(ref this.keyEventHandler_0, value2, keyEventHandler2);
				}
				while (keyEventHandler != keyEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				KeyEventHandler keyEventHandler = this.keyEventHandler_0;
				KeyEventHandler keyEventHandler2;
				do
				{
					keyEventHandler2 = keyEventHandler;
					KeyEventHandler value2 = (KeyEventHandler)Delegate.Remove(keyEventHandler2, value);
					keyEventHandler = Interlocked.CompareExchange<KeyEventHandler>(ref this.keyEventHandler_0, value2, keyEventHandler2);
				}
				while (keyEventHandler != keyEventHandler2);
			}
		}

		public event KeyEventHandler KeyUp
		{
			[CompilerGenerated]
			add
			{
				KeyEventHandler keyEventHandler = this.keyEventHandler_1;
				KeyEventHandler keyEventHandler2;
				do
				{
					keyEventHandler2 = keyEventHandler;
					KeyEventHandler value2 = (KeyEventHandler)Delegate.Combine(keyEventHandler2, value);
					keyEventHandler = Interlocked.CompareExchange<KeyEventHandler>(ref this.keyEventHandler_1, value2, keyEventHandler2);
				}
				while (keyEventHandler != keyEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				KeyEventHandler keyEventHandler = this.keyEventHandler_1;
				KeyEventHandler keyEventHandler2;
				do
				{
					keyEventHandler2 = keyEventHandler;
					KeyEventHandler value2 = (KeyEventHandler)Delegate.Remove(keyEventHandler2, value);
					keyEventHandler = Interlocked.CompareExchange<KeyEventHandler>(ref this.keyEventHandler_1, value2, keyEventHandler2);
				}
				while (keyEventHandler != keyEventHandler2);
			}
		}

		static Class179()
		{
			Strings.CreateGetStringDelegate(typeof(Class179));
		}

		private Class179.Delegate1 delegate1_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Keys> list_0;

		private IntPtr intptr_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private KeyEventHandler keyEventHandler_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private KeyEventHandler keyEventHandler_1;

		[NonSerialized]
		internal static GetString getString_0;

		public struct Struct4
		{
			public int int_0;

			public int int_1;

			public int int_2;

			public int int_3;

			public int int_4;
		}

		public delegate int Delegate1(int Code, int wParam, ref Class179.Struct4 lParam);
	}
}
