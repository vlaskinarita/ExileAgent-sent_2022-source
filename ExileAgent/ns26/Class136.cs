using System;
using ns12;
using ns29;
using ns35;
using PoEv2;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns26
{
	internal sealed class Class136 : Class129
	{
		public Class136(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected override void vmethod_0()
		{
			Class181.smethod_3(Enum11.const_3, Class136.getString_1(107369563));
			this.mainForm_0.method_128();
		}

		static Class136()
		{
			Strings.CreateGetStringDelegate(typeof(Class136));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
