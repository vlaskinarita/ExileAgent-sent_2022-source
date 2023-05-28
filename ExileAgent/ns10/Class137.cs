using System;
using ns12;
using ns29;
using ns35;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns10
{
	internal sealed class Class137 : Class129
	{
		public Class137(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected override void vmethod_0()
		{
			Class181.smethod_3(Enum11.const_3, Class137.getString_1(107369543));
			this.mainForm_0.method_64(true);
		}

		static Class137()
		{
			Strings.CreateGetStringDelegate(typeof(Class137));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
