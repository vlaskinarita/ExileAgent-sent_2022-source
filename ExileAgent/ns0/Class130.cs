using System;
using Newtonsoft.Json;
using ns12;
using ns17;
using ns21;
using ns29;
using ns35;
using PoEv2;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns0
{
	internal sealed class Class130 : Class129
	{
		public Class130(MainForm mainForm_1) : base(mainForm_1)
		{
		}

		protected unsafe override void vmethod_0()
		{
			void* ptr = stackalloc byte[4];
			Class143 @class = JsonConvert.DeserializeObject<Class143>(this.string_0, Util.smethod_12());
			*(byte*)ptr = ((@class == null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_2, Class130.getString_1(107370130));
				Class123.smethod_14();
				Class130.int_0 = 0;
			}
			else
			{
				((byte*)ptr)[1] = (@class.Success ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class123.bool_5 = true;
					Class123.smethod_14();
					Class130.int_0 = 0;
				}
				else
				{
					((byte*)ptr)[2] = ((@class.Reason != Enum4.const_1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_2(Enum11.const_2, Class130.getString_1(107370125), new object[]
						{
							Class143.smethod_0(@class.Reason)
						});
						Class123.smethod_12(true, false);
					}
					else
					{
						Class130.int_0++;
						((byte*)ptr)[3] = ((Class130.int_0 > 2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							Class181.smethod_2(Enum11.const_2, Class130.getString_1(107370125), new object[]
							{
								Class143.smethod_0(@class.Reason)
							});
							Class123.smethod_12(true, false);
						}
						else
						{
							Class181.smethod_2(Enum11.const_3, Class130.getString_1(107370068), new object[]
							{
								Class130.int_0
							});
						}
					}
				}
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class130()
		{
			Strings.CreateGetStringDelegate(typeof(Class130));
			Class130.int_0 = 0;
		}

		private static int int_0;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
