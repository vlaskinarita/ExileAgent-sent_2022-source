using System;
using ns29;
using ns35;
using PoEv2;
using PusherClient;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns12
{
	internal abstract class Class129
	{
		public Class129(MainForm mainForm_1)
		{
			this.mainForm_0 = mainForm_1;
		}

		public void method_0(PusherEvent pusherEvent_0)
		{
			this.string_0 = pusherEvent_0.Data;
			Class181.smethod_2(Enum11.const_3, Class129.getString_0(107370635), new object[]
			{
				pusherEvent_0.ChannelName,
				pusherEvent_0.EventName,
				this.string_0
			});
			this.vmethod_0();
		}

		public void method_1(string string_1, string string_2)
		{
			this.string_0 = string_2;
			Class181.smethod_2(Enum11.const_3, Class129.getString_0(107370566), new object[]
			{
				string_1,
				string_2
			});
			this.vmethod_0();
		}

		protected abstract void vmethod_0();

		static Class129()
		{
			Strings.CreateGetStringDelegate(typeof(Class129));
		}

		protected MainForm mainForm_0;

		protected string string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
