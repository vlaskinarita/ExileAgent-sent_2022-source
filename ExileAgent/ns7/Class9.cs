using System;
using ImageDiff;
using ns1;
using ns4;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns7
{
	internal static class Class9
	{
		public static Interface3 smethod_0(AnalyzerTypes analyzerTypes_0, double double_0)
		{
			Interface3 result;
			if (analyzerTypes_0 != AnalyzerTypes.ExactMatch)
			{
				if (analyzerTypes_0 != AnalyzerTypes.CIE76)
				{
					throw new ArgumentException(string.Format(Class9.getString_0(107396386), analyzerTypes_0));
				}
				result = new Class10(double_0);
			}
			else
			{
				result = new Class11();
			}
			return result;
		}

		static Class9()
		{
			Strings.CreateGetStringDelegate(typeof(Class9));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
