using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using ns0;
using ns12;
using ns34;
using ns40;
using PoEv2.Classes;
using PoEv2.Models.Query;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class BulkBuyingListItem
	{
		public bool Enabled { get; set; }

		public string HaveName { get; set; }

		public string WantName { get; set; }

		public int MinStock { get; set; }

		public decimal MaxPriceAmount { get; set; }

		public bool PriceAfterBuying { get; set; }

		public bool AutoPrice { get; set; }

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
		public string QueryId { get; set; }

		[JsonIgnore]
		public List<FetchTradeResult> FetchList { get; set; }

		public BulkBuyingListItem(string have, string want)
		{
			this.HaveName = have;
			this.WantName = want;
			this.SetDefaults();
		}

		public override bool Equals(object obj)
		{
			bool result;
			if (!(obj is BulkBuyingListItem))
			{
				result = false;
			}
			else
			{
				BulkBuyingListItem bulkBuyingListItem = (BulkBuyingListItem)obj;
				result = (bulkBuyingListItem != null && this.HaveName == bulkBuyingListItem.HaveName && this.WantName == bulkBuyingListItem.WantName);
			}
			return result;
		}

		public override int GetHashCode()
		{
			return this.HaveName.GetHashCode() + this.WantName.GetHashCode();
		}

		private void SetDefaults()
		{
			this.Enabled = true;
			this.MinStock = 1;
			this.PriceCurrency = BulkBuyingListItem.getString_0(107393233);
			this.Stash = BulkBuyingListItem.getString_0(107353227);
			this.PriceSkip = 4;
			this.PriceTake = 2;
			this.AutoPriceCurrency = BulkBuyingListItem.getString_0(107393233);
			this.MaxListingSize = 100;
		}

		public object[] ToDataGrid()
		{
			return new object[]
			{
				this.Enabled,
				this.HaveName,
				this.WantName,
				this.StockLimit,
				this.MaxPriceAmount,
				this.HaveName,
				this.PriceAmount,
				this.PriceCurrency
			};
		}

		public static BulkBuyingListItem FromDataGrid(DataGridViewRow row)
		{
			string have = row.Cells[BulkBuyingListItem.getString_0(107399133)].Value.smethod_10();
			string want = row.Cells[BulkBuyingListItem.getString_0(107399144)].Value.smethod_10();
			BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList.FirstOrDefault((BulkBuyingListItem f) => f.HaveName == have && f.WantName == want);
			if (bulkBuyingListItem == null)
			{
				bulkBuyingListItem = new BulkBuyingListItem(have, want);
			}
			bulkBuyingListItem.Enabled = (bool)row.Cells[BulkBuyingListItem.getString_0(107406379)].Value;
			bulkBuyingListItem.HaveName = row.Cells[BulkBuyingListItem.getString_0(107399133)].Value.smethod_10();
			bulkBuyingListItem.WantName = row.Cells[BulkBuyingListItem.getString_0(107399144)].Value.smethod_10();
			bulkBuyingListItem.StockLimit = Util.smethod_33(row.Cells[BulkBuyingListItem.getString_0(107399023)].Value.smethod_10());
			try
			{
				bulkBuyingListItem.MaxPriceAmount = Util.smethod_6(row.Cells[BulkBuyingListItem.getString_0(107399080)].Value.smethod_10());
			}
			catch
			{
				bulkBuyingListItem.MaxPriceAmount = 0m;
			}
			bulkBuyingListItem.PriceAmount = row.Cells[BulkBuyingListItem.getString_0(107398994)].Value.smethod_10();
			bulkBuyingListItem.PriceCurrency = row.Cells[BulkBuyingListItem.getString_0(107406317)].Value.smethod_10();
			row.Cells[BulkBuyingListItem.getString_0(107398994)].smethod_26(bulkBuyingListItem.PriceAfterBuying && !bulkBuyingListItem.AutoPrice);
			row.Cells[BulkBuyingListItem.getString_0(107406317)].smethod_26(bulkBuyingListItem.PriceAfterBuying && !bulkBuyingListItem.AutoPrice);
			return bulkBuyingListItem;
		}

		[JsonIgnore]
		public string Query
		{
			get
			{
				string result;
				if (!API.DataLoaded)
				{
					result = null;
				}
				else
				{
					result = JsonConvert.SerializeObject(new Class272(new Class270(API.smethod_4(this.WantName), API.smethod_4(this.HaveName), this.MinStock)), Util.smethod_10());
				}
				return result;
			}
		}

		[JsonIgnore]
		public string QueryURL
		{
			get
			{
				string result;
				if (!API.DataLoaded)
				{
					result = null;
				}
				else
				{
					result = JsonConvert.SerializeObject(new Class270(API.smethod_4(this.WantName), API.smethod_4(this.HaveName), this.MinStock), Util.smethod_10());
				}
				return result;
			}
		}

		public override string ToString()
		{
			return string.Format(BulkBuyingListItem.getString_0(107284422), this.HaveName, this.WantName);
		}

		[JsonIgnore]
		public Class249 MaxPrice
		{
			get
			{
				Class249 result;
				if (!API.DataLoaded)
				{
					result = new Class249();
				}
				else
				{
					result = new Class249(this.MaxPriceAmount, API.smethod_7(this.HaveName).Text);
				}
				return result;
			}
		}

		static BulkBuyingListItem()
		{
			Strings.CreateGetStringDelegate(typeof(BulkBuyingListItem));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
