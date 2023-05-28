using System;
using ns12;
using ns29;
using ns35;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns4
{
	internal sealed class Class138 : Class129
	{
		public Class138(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected override void vmethod_0()
		{
			Class181.smethod_3(Enum11.const_3, Class138.getString_1(107369555));
			this.mainForm_0.method_58(false, MainForm.GEnum1.const_0, false);
		}

		static Class138()
		{
			Strings.CreateGetStringDelegate(typeof(Class138));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
