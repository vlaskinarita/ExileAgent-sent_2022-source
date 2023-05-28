using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns17
{
	internal static class Class121
	{
		[STAThread]
		private static void Main()
		{
			List<string> list = Environment.GetCommandLineArgs().ToList<string>();
			list.RemoveAt(0);
			if (!list.Any<string>() && !Debugger.IsAttached)
			{
				MessageBox.Show(Class121.getString_0(107372207));
				Process.GetCurrentProcess().Kill();
			}
			else
			{
				try
				{
					list.Add(Class121.getString_0(107398036));
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new MainForm(list[0]));
				}
				catch (Exception arg)
				{
					MessageBox.Show(string.Format(Class121.getString_0(107372150), arg));
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		static Class121()
		{
			Strings.CreateGetStringDelegate(typeof(Class121));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
