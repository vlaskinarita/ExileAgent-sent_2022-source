using System;
using ns12;
using ns25;
using ns29;
using ns35;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns21
{
	internal sealed class Class133 : Class129
	{
		public Class133(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected override void vmethod_0()
		{
			Class181.smethod_3(Enum11.const_3, Class133.getString_1(107369623));
			if (UI.smethod_26())
			{
				UI.smethod_1();
				Class123.smethod_6(Enum3.const_5, Class197.WebImage);
			}
		}

		static Class133()
		{
			Strings.CreateGetStringDelegate(typeof(Class133));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
