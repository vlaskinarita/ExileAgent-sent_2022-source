using System;
using System.Collections.Generic;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns1
{
	internal sealed class Class391
	{
		// Note: this type is marked as 'beforefieldinit'.
		static Class391()
		{
			Strings.CreateGetStringDelegate(typeof(Class391));
			Class391.list_0 = new List<DecimalCurrencyListItem>
			{
				new DecimalCurrencyListItem(Class391.getString_0(107385909), Class391.getString_0(107394114))
			};
			Class391.list_1 = new List<string>
			{
				Class391.getString_0(107408790),
				Class391.getString_0(107408060),
				Class391.getString_0(107407521)
			};
			Class391.string_0 = new string[]
			{
				Class391.getString_0(107408993),
				Class391.getString_0(107385961),
				Class391.getString_0(107385976),
				Class391.getString_0(107394114),
				Class391.getString_0(107385947),
				Class391.getString_0(107385894),
				Class391.getString_0(107385909),
				Class391.getString_0(107385871),
				Class391.getString_0(107385846),
				Class391.getString_0(107385179),
				Class391.getString_0(107385200),
				Class391.getString_0(107385189),
				Class391.getString_0(107385242),
				Class391.getString_0(107385772),
				Class391.getString_0(107385793),
				Class391.getString_0(107385116),
				Class391.getString_0(107385033)
			};
		}

		public static List<DecimalCurrencyListItem> list_0;

		public static List<string> list_1;

		public static string[] string_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
