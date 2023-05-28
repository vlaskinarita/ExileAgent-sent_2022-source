using System;
using Newtonsoft.Json;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class DecimalCurrencyListItem
	{
		public string Currency { get; set; }

		public string DecimalType { get; set; }

		[JsonIgnore]
		public decimal Value { get; set; }

		public DecimalCurrencyListItem()
		{
		}

		public DecimalCurrencyListItem(string currency, string decimalType)
		{
			this.Currency = currency;
			this.DecimalType = decimalType;
		}

		public static object ImageGetter(object obj)
		{
			DecimalCurrencyListItem decimalCurrencyListItem = (DecimalCurrencyListItem)obj;
			return decimalCurrencyListItem.Image;
		}

		public override string ToString()
		{
			return string.Format(DecimalCurrencyListItem.getString_0(107284404), this.Currency, this.DecimalType);
		}

		static DecimalCurrencyListItem()
		{
			Strings.CreateGetStringDelegate(typeof(DecimalCurrencyListItem));
		}

		[JsonIgnore]
		public object Image = DecimalCurrencyListItem.getString_0(107398662);

		[NonSerialized]
		internal static GetString getString_0;
	}
}
