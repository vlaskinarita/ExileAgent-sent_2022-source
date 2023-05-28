using System;
using System.Windows;
using Newtonsoft.Json.Linq;
using ns13;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns32
{
	internal static class Class149
	{
		private static double ItemOffset
		{
			get
			{
				return 44.0;
			}
		}

		private static double SpecialTabItemWidth
		{
			get
			{
				return 1.110137306456325;
			}
		}

		private static double SpecialTabItemHeight
		{
			get
			{
				return 1.1101156006206159;
			}
		}

		public static Point WindowOffset
		{
			get
			{
				return new Point(0.0, 0.0);
			}
		}

		public unsafe static double smethod_0(Enum10 enum10_0)
		{
			void* ptr = stackalloc byte[20];
			*(int*)((byte*)ptr + 16) = 12;
			*(double*)ptr = 632.0;
			switch (enum10_0)
			{
			case Enum10.const_1:
				*(int*)((byte*)ptr + 16) = 24;
				break;
			case Enum10.const_3:
				*(double*)ptr = 628.0;
				break;
			case Enum10.const_4:
				*(double*)((byte*)ptr + 8) = Class149.ItemOffset;
				goto IL_6E;
			}
			*(double*)((byte*)ptr + 8) = *(double*)ptr / (double)(*(int*)((byte*)ptr + 16)) * 1.0;
			IL_6E:
			return *(double*)((byte*)ptr + 8);
		}

		public unsafe static Position smethod_1(JObject jobject_0, int int_0)
		{
			void* ptr = stackalloc byte[51];
			((byte*)ptr)[48] = ((int_0 == -1) ? 1 : 0);
			Position result;
			if (*(sbyte*)((byte*)ptr + 48) != 0)
			{
				result = new Position();
			}
			else
			{
				((byte*)ptr)[49] = (jobject_0.ContainsKey(Class149.getString_0(107380434)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 49) != 0)
				{
					jobject_0 = (JObject)jobject_0[Class149.getString_0(107380434)];
				}
				*(double*)ptr = Class149.ItemOffset / 2.0;
				*(double*)((byte*)ptr + 8) = Class149.ItemOffset / 2.0;
				JToken jtoken = jobject_0[int_0.ToString()];
				((byte*)ptr)[50] = ((jtoken[Class149.getString_0(107380425)] != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 50) != 0)
				{
					*(double*)((byte*)ptr + 32) = (double)jobject_0[Class149.getString_0(107398197)][Class149.getString_0(107380425)];
					*(double*)((byte*)ptr + 40) = (double)jtoken[Class149.getString_0(107380425)] / *(double*)((byte*)ptr + 32);
					*(double*)ptr = *(double*)ptr * *(double*)((byte*)ptr + 40);
					*(double*)((byte*)ptr + 8) = *(double*)((byte*)ptr + 8) * *(double*)((byte*)ptr + 40);
				}
				*(double*)((byte*)ptr + 16) = (double)jtoken[Class149.getString_0(107397300)] * Class149.SpecialTabItemWidth * 1.0 + Class149.StashOffset.X + Class149.WindowOffset.X + *(double*)ptr;
				*(double*)((byte*)ptr + 24) = (double)jtoken[Class149.getString_0(107380448)] * Class149.SpecialTabItemHeight * 1.0 + Class149.StashOffset.Y + Class149.WindowOffset.Y + *(double*)((byte*)ptr + 8);
				result = new Position((int)Math.Round(*(double*)((byte*)ptr + 16)), (int)Math.Round(*(double*)((byte*)ptr + 24)));
			}
			return result;
		}

		public unsafe static Position smethod_2(Enum10 enum10_0, int int_0, int int_1)
		{
			void* ptr = stackalloc byte[56];
			Point point = default(Point);
			Position result;
			if (int_0 == -1 && int_1 == -1)
			{
				result = new Position();
			}
			else
			{
				if (enum10_0 != Enum10.const_0)
				{
					if (enum10_0 == Enum10.const_1)
					{
						point = Class149.StashOffset;
					}
				}
				else
				{
					point = Class149.StashOffset;
				}
				*(double*)ptr = Class149.smethod_0(enum10_0);
				*(double*)((byte*)ptr + 8) = *(double*)ptr * (double)int_0;
				*(double*)((byte*)ptr + 16) = *(double*)ptr * (double)int_1;
				*(double*)((byte*)ptr + 24) = *(double*)ptr / 2.0;
				*(double*)((byte*)ptr + 32) = *(double*)ptr / 2.0;
				Point windowOffset = Class149.WindowOffset;
				*(double*)((byte*)ptr + 40) = windowOffset.X + point.X + *(double*)((byte*)ptr + 24) + *(double*)((byte*)ptr + 8);
				windowOffset = Class149.WindowOffset;
				*(double*)((byte*)ptr + 48) = windowOffset.Y + point.Y + *(double*)((byte*)ptr + 32) + *(double*)((byte*)ptr + 16);
				result = new Position((int)Math.Round(*(double*)((byte*)ptr + 40)), (int)Math.Round(*(double*)((byte*)ptr + 48)));
			}
			return result;
		}

		public unsafe static Point StashOffset
		{
			get
			{
				void* ptr = stackalloc byte[16];
				*(double*)ptr = 15.0;
				*(double*)((byte*)ptr + 8) = 126.0;
				return new Point(*(double*)ptr, *(double*)((byte*)ptr + 8));
			}
		}

		static Class149()
		{
			Strings.CreateGetStringDelegate(typeof(Class149));
		}

		private const double double_0 = 1.0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
