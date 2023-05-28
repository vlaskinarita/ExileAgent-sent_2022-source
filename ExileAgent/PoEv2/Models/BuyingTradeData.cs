using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public sealed class BuyingTradeData
	{
		public DateTime Time { get; set; }

		public string Id { get; set; }

		public string Query { get; set; }

		public TradeTypes TradeType { get; set; }

		public int Priority { get; set; }

		public string AccountName { get; set; }

		public string CharacterName { get; set; }

		public string WhisperMessage { get; set; }

		public JsonItem Item { get; set; }

		public string PlayerItemType { get; set; }

		public int PlayerTotalStock { get; set; }

		public decimal PlayerItemsPerTrade { get; set; }

		public string OurItemType { get; set; }

		public decimal OurItemsPerTrade { get; set; }

		public decimal PricePerItem
		{
			get
			{
				return this.OurItemsPerTrade / this.PlayerItemsPerTrade;
			}
		}

		public string ToString()
		{
			return string.Format(BuyingTradeData.getString_0(107442452), new object[]
			{
				this.Id,
				this.Query,
				this.AccountName,
				this.CharacterName,
				this.PlayerItemType,
				this.OurItemType,
				this.PricePerItem,
				this.TradeType
			});
		}

		static BuyingTradeData()
		{
			Strings.CreateGetStringDelegate(typeof(BuyingTradeData));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime dateTime_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TradeTypes tradeTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonItem jsonItem_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
