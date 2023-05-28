using System;
using Newtonsoft.Json;
using ns12;
using ns21;
using ns29;
using ns35;
using PoEv2;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns22
{
	internal sealed class Class131 : Class129
	{
		public Class131(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected unsafe override void vmethod_0()
		{
			void* ptr = stackalloc byte[6];
			Class143 @class = JsonConvert.DeserializeObject<Class143>(this.string_0, Util.smethod_12());
			*(byte*)ptr = ((@class == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = (this.string_0.Contains(Class131.getString_1(107371127)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_3(Enum11.const_2, Class131.getString_1(107370040));
				}
				else
				{
					Class181.smethod_3(Enum11.const_2, Class131.getString_1(107369959));
				}
				Class123.smethod_12(true, false);
			}
			else
			{
				((byte*)ptr)[2] = (@class.Success ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_2(Enum11.const_3, Class131.getString_1(107370414), new object[]
					{
						@class.Features
					});
					((byte*)ptr)[3] = (Util.smethod_28(@class.Features, 15) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) == 0)
					{
					}
					((byte*)ptr)[4] = (Util.smethod_28(@class.Features, 14) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) == 0)
					{
					}
					((byte*)ptr)[5] = (Util.smethod_28(@class.Features, 13) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) == 0)
					{
					}
					Class123.bool_5 = true;
					Class123.smethod_13();
					Class123.smethod_16();
					Class123.smethod_15();
				}
				else
				{
					Class181.smethod_2(Enum11.const_2, Class131.getString_1(107370361), new object[]
					{
						Class143.smethod_0(@class.Reason)
					});
					Class123.smethod_12(true, false);
				}
			}
		}

		static Class131()
		{
			Strings.CreateGetStringDelegate(typeof(Class131));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
