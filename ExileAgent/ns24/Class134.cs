using System;
using ns12;
using ns29;
using ns35;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns24
{
	internal sealed class Class134 : Class129
	{
		public Class134(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected override void vmethod_0()
		{
			Class181.smethod_3(Enum11.const_3, Class134.getString_1(107369595));
			this.mainForm_0.method_119();
		}

		static Class134()
		{
			Strings.CreateGetStringDelegate(typeof(Class134));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
