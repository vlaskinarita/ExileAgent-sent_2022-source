using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ns0;
using ns7;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns29
{
	internal sealed class Class242
	{
		public bool SettingHotkeyActive { get; set; }

		public List<string> KeysPressed { get; set; }

		public Class179 KeyHook { get; set; }

		public Class242(ConfigOptions configOptions_1, TextBox textBox_1, Action<object, KeyEventArgs, Class242, Action> action_0, Action action_1)
		{
			this.configOptions_0 = configOptions_1;
			this.textBox_0 = textBox_1;
			this.KeysPressed = this.SetHotkeys;
			this.method_0(action_0, action_1);
		}

		private void method_0(Action<object, KeyEventArgs, Class242, Action> action_0, Action action_1)
		{
			Class242.Class243 @class = new Class242.Class243();
			@class.action_0 = action_0;
			@class.class242_0 = this;
			@class.action_1 = action_1;
			this.KeyHook = new Class179();
			this.KeyHook.KeyDown += @class.method_0;
			this.KeyHook.HookedKeys = KeyToStringHelper.smethod_4();
			this.KeyHook.method_0();
		}

		public void ResetKeysPressed()
		{
			this.KeysPressed = this.SetHotkeys;
		}

		public void method_1()
		{
			this.SettingHotkeyActive = !this.SettingHotkeyActive;
		}

		public List<string> SetHotkeys
		{
			get
			{
				return Class255.class105_0.method_8<string>(this.configOptions_0).ToList<string>();
			}
		}

		public void method_2()
		{
			this.textBox_0.Invoke(new Action(this.method_7));
		}

		public void method_3()
		{
			this.method_6(new List<string>());
		}

		public void method_4(string string_0)
		{
			this.method_6(this.SetHotkeys.Append(string_0));
		}

		public void method_5()
		{
			this.textBox_0.Invoke(new Action(this.method_8));
		}

		public void method_6(IEnumerable<string> ienumerable_0)
		{
			Class255.class105_0.method_9(this.configOptions_0, ienumerable_0.ToList<string>(), true);
		}

		[CompilerGenerated]
		private void method_7()
		{
			this.textBox_0.Text = string.Join(Class242.getString_0(107352296), this.SetHotkeys);
		}

		[CompilerGenerated]
		private void method_8()
		{
			this.textBox_0.Focus();
		}

		static Class242()
		{
			Strings.CreateGetStringDelegate(typeof(Class242));
		}

		private ConfigOptions configOptions_0;

		private TextBox textBox_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class179 class179_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class243
		{
			internal void method_0(object sender, KeyEventArgs e)
			{
				this.action_0(sender, e, this.class242_0, this.action_1);
			}

			public Action<object, KeyEventArgs, Class242, Action> action_0;

			public Class242 class242_0;

			public Action action_1;
		}
	}
}
