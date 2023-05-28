using System;
using ImageDiff;
using ns1;
using ns3;
using ns4;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns5
{
	internal static class Class6
	{
		public static Interface1 smethod_0(LabelerTypes labelerTypes_0, int int_0)
		{
			Interface1 result;
			if (labelerTypes_0 != LabelerTypes.Basic)
			{
				if (labelerTypes_0 != LabelerTypes.ConnectedComponentLabeling)
				{
					throw new ArgumentException(string.Format(Class6.getString_0(107396966), labelerTypes_0));
				}
				result = new Class4(int_0);
			}
			else
			{
				result = new Class3();
			}
			return result;
		}

		static Class6()
		{
			Strings.CreateGetStringDelegate(typeof(Class6));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
