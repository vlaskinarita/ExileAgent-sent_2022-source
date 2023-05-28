using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using ns0;
using ns34;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class ItemBuyingListItem
	{
		public bool Enabled { get; set; }

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
		public string Query { get; set; }

		[JsonIgnore]
		public List<string> FetchList { get; set; }

		[JsonIgnore]
		public Class249 MaxPrice
		{
			get
			{
				return new Class249(this.MaxPriceAmount, this.MaxPriceCurrency);
			}
		}

		public ItemBuyingListItem(string id, string description)
		{
			this.Id = id;
			this.Description = description;
			this.Enabled = true;
			this.SetDefaults();
		}

		public override bool Equals(object obj)
		{
			bool result;
			if (!(obj is ItemBuyingListItem))
			{
				result = false;
			}
			else
			{
				ItemBuyingListItem itemBuyingListItem = (ItemBuyingListItem)obj;
				result = (itemBuyingListItem != null && this.Id == itemBuyingListItem.Id);
			}
			return result;
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		private void SetDefaults()
		{
			this.Stash = ItemBuyingListItem.getString_0(107353206);
			this.MaxPriceCurrency = ItemBuyingListItem.getString_0(107408091);
			this.PriceCurrency = ItemBuyingListItem.getString_0(107393212);
			this.PriceSkip = 4;
			this.PriceSkip = 2;
			this.MaxListingSize = 100;
			this.AutoPriceCurrency = ItemBuyingListItem.getString_0(107393212);
		}

		public unsafe object[] ToDataGrid()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (string.IsNullOrEmpty(this.MaxPriceCurrency) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.MaxPriceCurrency = ItemBuyingListItem.getString_0(107408091);
			}
			((byte*)ptr)[1] = (string.IsNullOrEmpty(this.PriceCurrency) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.PriceCurrency = ItemBuyingListItem.getString_0(107393212);
			}
			return new object[]
			{
				this.Enabled,
				this.Description,
				this.StockLimit,
				this.MaxPriceAmount,
				this.MaxPriceCurrency,
				this.PriceAmount,
				this.PriceCurrency
			};
		}

		public unsafe static ItemBuyingListItem FromDataGrid(DataGridViewRow row)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = row.Index;
			((byte*)ptr)[4] = ((Class255.ItemBuyingList.Count <= *(int*)ptr) ? 1 : 0);
			ItemBuyingListItem itemBuyingListItem;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				itemBuyingListItem = new ItemBuyingListItem(ItemBuyingListItem.getString_0(107398565), ItemBuyingListItem.getString_0(107398565));
			}
			else
			{
				itemBuyingListItem = Class255.ItemBuyingList[*(int*)ptr];
			}
			itemBuyingListItem.Enabled = (bool)row.Cells[ItemBuyingListItem.getString_0(107406861)].Value;
			itemBuyingListItem.Description = row.Cells[ItemBuyingListItem.getString_0(107406836)].Value.smethod_10();
			itemBuyingListItem.StockLimit = Util.smethod_33(row.Cells[ItemBuyingListItem.getString_0(107398810)].Value.smethod_10());
			itemBuyingListItem.MaxPriceAmount = Util.smethod_6(row.Cells[ItemBuyingListItem.getString_0(107398781)].Value.smethod_10());
			itemBuyingListItem.MaxPriceCurrency = row.Cells[ItemBuyingListItem.getString_0(107398051)].Value.smethod_10();
			itemBuyingListItem.PriceAmount = row.Cells[ItemBuyingListItem.getString_0(107398756)].Value.smethod_10();
			itemBuyingListItem.PriceCurrency = row.Cells[ItemBuyingListItem.getString_0(107406807)].Value.smethod_10();
			row.Cells[ItemBuyingListItem.getString_0(107398756)].smethod_26(itemBuyingListItem.PriceAfterBuying && !itemBuyingListItem.AutoPrice);
			row.Cells[ItemBuyingListItem.getString_0(107406807)].smethod_26(itemBuyingListItem.PriceAfterBuying && !itemBuyingListItem.AutoPrice);
			return itemBuyingListItem;
		}

		static ItemBuyingListItem()
		{
			Strings.CreateGetStringDelegate(typeof(ItemBuyingListItem));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
