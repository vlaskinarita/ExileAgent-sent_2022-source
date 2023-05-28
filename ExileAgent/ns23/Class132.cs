using System;
using ns12;
using ns19;
using ns21;
using ns29;
using ns35;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns23
{
	internal sealed class Class132 : Class129
	{
		public Class132(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected override void vmethod_0()
		{
			Class181.smethod_3(Enum11.const_3, Class132.getString_1(107370333));
			if (Stashes.Tabs == null)
			{
				Class181.smethod_3(Enum11.const_2, Class132.getString_1(107370296));
			}
			else
			{
				JsonTab jsonTab = Stashes.smethod_14(Class132.getString_1(107394273));
				if (jsonTab == null || !Stashes.Items.ContainsKey(jsonTab.i))
				{
					Class181.smethod_3(Enum11.const_2, Class132.getString_1(107370199));
				}
				else
				{
					Form4 form = new Form4(jsonTab.i, false);
					Class123.smethod_6(Enum3.const_6, form.method_0());
				}
			}
		}

		static Class132()
		{
			Strings.CreateGetStringDelegate(typeof(Class132));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
