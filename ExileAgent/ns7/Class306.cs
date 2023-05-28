using System;
using ns21;
using ns29;
using ns35;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns7
{
	internal static class Class306
	{
		public unsafe static bool smethod_0(int int_0 = 0)
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((int_0 > 3) ? 1 : 0);
			bool result;
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_0, Class306.getString_0(107284635));
				result = false;
			}
			else
			{
				((byte*)ptr)[1] = (Class123.bool_5 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((byte*)ptr)[2] = ((!Class123.IsConnected) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((byte*)ptr)[3] = ((!Class123.smethod_19()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							Class181.smethod_3(Enum11.const_2, Class306.getString_0(107284614));
						}
					}
					result = true;
				}
				else
				{
					((byte*)ptr)[4] = ((!Class123.bool_5) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						Class123.smethod_12(false, true);
						Class123.smethod_19();
						result = Class306.smethod_0(++int_0);
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		static Class306()
		{
			Strings.CreateGetStringDelegate(typeof(Class306));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
