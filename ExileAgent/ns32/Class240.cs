using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns34;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns32
{
	internal sealed class Class240
	{
		public string Id { get; set; }

		public Class249 MaxPrice { get; set; }

		public bool PriceAfterBuying { get; set; }

		public bool AutoPrice { get; set; }

		public bool PriceAsBulk { get; set; }

		public string PriceAmount { get; set; }

		public string PriceCurrency { get; set; }

		public int PriceSkip { get; set; }

		public int PriceTake { get; set; }

		public int MaxListingSize { get; set; }

		public string AutoPriceCurrency { get; set; }

		public bool DisableAfterPurchase { get; set; }

		public bool DisableAfterStashFull { get; set; }

		public int StockLimit { get; set; }

		public string DumpStash { get; set; }

		public bool TurnInCard { get; set; }

		public bool OnlyPriceDivResult { get; set; }

		public Class240(LiveSearchListItem liveSearchListItem_0)
		{
			this.Id = liveSearchListItem_0.Id;
			this.MaxPrice = liveSearchListItem_0.MaxPrice;
			this.PriceAfterBuying = liveSearchListItem_0.PriceAfterBuying;
			this.AutoPrice = liveSearchListItem_0.AutoPrice;
			this.PriceAmount = liveSearchListItem_0.PriceAmount;
			this.PriceCurrency = liveSearchListItem_0.PriceCurrency;
			this.PriceSkip = liveSearchListItem_0.PriceSkip;
			this.PriceTake = liveSearchListItem_0.PriceTake;
			this.MaxListingSize = liveSearchListItem_0.MaxListingSize;
			this.AutoPriceCurrency = liveSearchListItem_0.AutoPriceCurrency;
			this.DisableAfterPurchase = liveSearchListItem_0.DisableAfterPurchase;
			this.DisableAfterStashFull = liveSearchListItem_0.DisableAfterStashFull;
			this.StockLimit = liveSearchListItem_0.StockLimit;
			this.DumpStash = liveSearchListItem_0.Stash;
			this.TurnInCard = liveSearchListItem_0.TurnInCard;
			this.OnlyPriceDivResult = liveSearchListItem_0.TurnInCard;
		}

		public Class240(ItemBuyingListItem itemBuyingListItem_0)
		{
			this.Id = itemBuyingListItem_0.Id;
			this.MaxPrice = itemBuyingListItem_0.MaxPrice;
			this.PriceAfterBuying = itemBuyingListItem_0.PriceAfterBuying;
			this.AutoPrice = itemBuyingListItem_0.AutoPrice;
			this.PriceAsBulk = itemBuyingListItem_0.PriceAsBulk;
			this.PriceAmount = itemBuyingListItem_0.PriceAmount;
			this.PriceCurrency = itemBuyingListItem_0.PriceCurrency;
			this.PriceSkip = itemBuyingListItem_0.PriceSkip;
			this.PriceTake = itemBuyingListItem_0.PriceTake;
			this.MaxListingSize = itemBuyingListItem_0.MaxListingSize;
			this.AutoPriceCurrency = itemBuyingListItem_0.AutoPriceCurrency;
			this.DisableAfterPurchase = itemBuyingListItem_0.DisableAfterPurchase;
			this.DisableAfterStashFull = itemBuyingListItem_0.DisableAfterStashFull;
			this.StockLimit = itemBuyingListItem_0.StockLimit;
			this.DumpStash = itemBuyingListItem_0.Stash;
			this.TurnInCard = itemBuyingListItem_0.TurnInCard;
			this.OnlyPriceDivResult = itemBuyingListItem_0.TurnInCard;
		}

		public Class240(BulkBuyingListItem bulkBuyingListItem_0)
		{
			this.Id = bulkBuyingListItem_0.QueryId;
			this.MaxPrice = bulkBuyingListItem_0.MaxPrice;
			this.PriceAfterBuying = bulkBuyingListItem_0.PriceAfterBuying;
			this.AutoPrice = bulkBuyingListItem_0.AutoPrice;
			this.PriceAsBulk = true;
			this.PriceAmount = bulkBuyingListItem_0.PriceAmount;
			this.PriceCurrency = bulkBuyingListItem_0.PriceCurrency;
			this.PriceSkip = bulkBuyingListItem_0.PriceSkip;
			this.PriceTake = bulkBuyingListItem_0.PriceTake;
			this.MaxListingSize = bulkBuyingListItem_0.MaxListingSize;
			this.AutoPriceCurrency = bulkBuyingListItem_0.AutoPriceCurrency;
			this.DisableAfterPurchase = bulkBuyingListItem_0.DisableAfterPurchase;
			this.DisableAfterStashFull = bulkBuyingListItem_0.DisableAfterStashFull;
			this.StockLimit = bulkBuyingListItem_0.StockLimit;
			this.DumpStash = bulkBuyingListItem_0.Stash;
			this.TurnInCard = bulkBuyingListItem_0.TurnInCard;
			this.OnlyPriceDivResult = bulkBuyingListItem_0.TurnInCard;
		}

		public string ToString()
		{
			return string.Format(Class240.getString_0(107442097), new object[]
			{
				this.MaxPrice,
				this.PriceAfterBuying,
				this.AutoPrice,
				this.PriceAsBulk,
				this.PriceAmount,
				this.PriceCurrency
			}) + string.Format(Class240.getString_0(107442572), new object[]
			{
				this.PriceSkip,
				this.PriceTake,
				this.MaxListingSize,
				this.AutoPriceCurrency
			}) + string.Format(Class240.getString_0(107442487), this.DisableAfterPurchase, this.DisableAfterStashFull) + string.Format(Class240.getString_0(107442458), new object[]
			{
				this.StockLimit,
				this.DumpStash,
				this.TurnInCard,
				this.OnlyPriceDivResult
			});
		}

		static Class240()
		{
			Strings.CreateGetStringDelegate(typeof(Class240));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class249 class249_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_6;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
