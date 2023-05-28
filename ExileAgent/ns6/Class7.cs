using System;
using ImageDiff;
using ImageDiff.BoundingBoxes;
using ns1;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns6
{
	internal static class Class7
	{
		public static Interface2 smethod_0(BoundingBoxModes boundingBoxModes_0, int int_0)
		{
			Interface2 result;
			if (boundingBoxModes_0 != BoundingBoxModes.Single)
			{
				if (boundingBoxModes_0 != BoundingBoxModes.Multiple)
				{
					throw new ArgumentException(string.Format(Class7.getString_0(107396954), boundingBoxModes_0));
				}
				result = new MultipleBoundingBoxIdentifier(int_0);
			}
			else
			{
				result = new SingleBoundingBoxIdentifer(int_0);
			}
			return result;
		}

		static Class7()
		{
			Strings.CreateGetStringDelegate(typeof(Class7));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
