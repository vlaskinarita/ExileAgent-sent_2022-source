using System;
using ns29;
using PoEv2;

namespace ns25
{
	internal static class Class175
	{
		public static void smethod_0(MainForm mainForm_1)
		{
			Class175.mainForm_0 = mainForm_1;
		}

		public static void smethod_1(string string_0)
		{
			if (Class175.mainForm_0.genum0_0 == MainForm.GEnum0.const_2)
			{
				Class173.smethod_0(string_0, Class175.mainForm_0);
			}
		}

		private static MainForm mainForm_0;
	}
}
