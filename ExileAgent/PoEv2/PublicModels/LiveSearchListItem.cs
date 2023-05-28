using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using ns0;
using ns34;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class LiveSearchListItem
	{
		public bool Enabled { get; set; }

		public string Priority { get; set; }

		public string Id { get; set; }

		public string Description { get; set; }

		public decimal MaxPriceAmount { get; set; }

		public string MaxPriceCurrency { get; set; }

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

		public string Stash { get; set; }

		public bool TurnInCard { get; set; }

		[JsonIgnore]
		public int ReconnectCount { get; set; }

		[JsonIgnore]
		public Class249 MaxPrice
		{
			get
			{
				return new Class249(this.MaxPriceAmount, this.MaxPriceCurrency);
			}
		}

		public LiveSearchListItem()
		{
		}

		public LiveSearchListItem(string id, string description)
		{
			this.Id = id;
			this.Description = description;
			this.Enabled = true;
			this.SetDefaults();
		}

		public LiveSearchListItem(string id, string description, bool enabled)
		{
			this.Id = id;
			this.Description = description;
			this.Enabled = enabled;
			this.SetDefaults();
		}

		private void SetDefaults()
		{
			this.Stash = LiveSearchListItem.getString_0(107353542);
			this.MaxPriceCurrency = LiveSearchListItem.getString_0(107408427);
			this.PriceCurrency = LiveSearchListItem.getString_0(107393548);
			this.Priority = LiveSearchListItem.getString_0(107371090);
			this.PriceSkip = 4;
			this.PriceTake = 2;
			this.MaxListingSize = 100;
			this.AutoPriceCurrency = LiveSearchListItem.getString_0(107393548);
		}

		public unsafe object[] ToDataGrid()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = (string.IsNullOrEmpty(this.Priority) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.Priority = LiveSearchListItem.getString_0(107371090);
			}
			((byte*)ptr)[1] = (string.IsNullOrEmpty(this.MaxPriceCurrency) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.MaxPriceCurrency = LiveSearchListItem.getString_0(107393548);
			}
			((byte*)ptr)[2] = (string.IsNullOrEmpty(this.PriceCurrency) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				this.PriceCurrency = LiveSearchListItem.getString_0(107393548);
			}
			return new object[]
			{
				this.Enabled,
				this.Priority,
				this.Description,
				this.StockLimit,
				this.MaxPriceAmount,
				this.MaxPriceCurrency,
				this.PriceAmount,
				this.PriceCurrency
			};
		}

		public unsafe static LiveSearchListItem SaveFromDataGrid(DataGridViewRow row)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = row.Index;
			((byte*)ptr)[4] = ((Class255.LiveSearchList.Count <= *(int*)ptr) ? 1 : 0);
			LiveSearchListItem liveSearchListItem;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				liveSearchListItem = new LiveSearchListItem(LiveSearchListItem.getString_0(107398901), LiveSearchListItem.getString_0(107398901));
			}
			else
			{
				liveSearchListItem = Class255.LiveSearchList[*(int*)ptr];
			}
			liveSearchListItem.Enabled = (bool)row.Cells[LiveSearchListItem.getString_0(107408061)].Value;
			liveSearchListItem.Priority = row.Cells[LiveSearchListItem.getString_0(107393183)].Value.smethod_10();
			liveSearchListItem.Description = row.Cells[LiveSearchListItem.getString_0(107408006)].Value.smethod_10();
			liveSearchListItem.StockLimit = Util.smethod_33(row.Cells[LiveSearchListItem.getString_0(107398538)].Value.smethod_10());
			liveSearchListItem.MaxPriceAmount = Util.smethod_6(row.Cells[LiveSearchListItem.getString_0(107398632)].Value.smethod_10());
			liveSearchListItem.MaxPriceCurrency = row.Cells[LiveSearchListItem.getString_0(107398575)].Value.smethod_10();
			liveSearchListItem.PriceAmount = row.Cells[LiveSearchListItem.getString_0(107398509)].Value.smethod_10();
			liveSearchListItem.PriceCurrency = row.Cells[LiveSearchListItem.getString_0(107407657)].Value.smethod_10();
			row.Cells[LiveSearchListItem.getString_0(107398509)].smethod_26(liveSearchListItem.PriceAfterBuying && !liveSearchListItem.AutoPrice);
			row.Cells[LiveSearchListItem.getString_0(107407657)].smethod_26(liveSearchListItem.PriceAfterBuying && !liveSearchListItem.AutoPrice);
			return liveSearchListItem;
		}

		static LiveSearchListItem()
		{
			Strings.CreateGetStringDelegate(typeof(LiveSearchListItem));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
