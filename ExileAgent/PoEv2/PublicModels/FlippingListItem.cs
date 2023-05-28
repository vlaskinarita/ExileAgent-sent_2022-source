using System;
using PoEv2.Classes;
using PoEv2.Models.Flipping;

namespace PoEv2.PublicModels
{
	public sealed class FlippingListItem
	{
		public bool Enabled { get; set; }

		public string HaveName { get; set; }

		public string WantName { get; set; }

		public int HaveMinimumStock { get; set; }

		public int WantMinimumStock { get; set; }

		public int SellHaveAmount { get; set; }

		public int SellWantAmount { get; set; }

		public int BuyHaveAmount { get; set; }

		public int BuyWantAmount { get; set; }

		public int BuyListingsToSkip { get; set; }

		public int BuyListingsToTake { get; set; }

		public int SellListingsToSkip { get; set; }

		public int SellListingsToTake { get; set; }

		public bool AutoPrice { get; set; }

		public bool OnlyPriceHave { get; set; }

		public bool AutoDetermineMargin { get; set; }

		public double ResellMargin { get; set; }

		public double IgnoreBelowMargin { get; set; }

		public int MinHavePerTrade { get; set; }

		public int MinWantPerTrade { get; set; }

		public int MaxHavePerTrade { get; set; }

		public int MaxWantPerTrade { get; set; }

		public int MinHaveStock { get; set; }

		public int MaxHaveStock { get; set; }

		public int MinWantStock { get; set; }

		public int MaxWantStock { get; set; }

		public FlippingListItem(string have, string want)
		{
			this.Enabled = true;
			this.HaveName = have;
			this.WantName = want;
			this.AutoPrice = true;
			this.AutoDetermineMargin = true;
			this.HaveMinimumStock = 1;
			this.WantMinimumStock = 1;
			this.BuyListingsToSkip = 6;
			this.BuyListingsToTake = 2;
			this.SellListingsToSkip = 6;
			this.SellListingsToTake = 2;
			this.ResellMargin = 2.0;
			this.IgnoreBelowMargin = 1.0;
		}

		public override bool Equals(object obj)
		{
			bool result;
			if (!(obj is FlippingListItem))
			{
				result = false;
			}
			else
			{
				FlippingListItem flippingListItem = (FlippingListItem)obj;
				result = (flippingListItem != null && this.HaveName == flippingListItem.HaveName && this.WantName == flippingListItem.WantName);
			}
			return result;
		}

		public override int GetHashCode()
		{
			return this.HaveName.GetHashCode() + this.WantName.GetHashCode();
		}

		public object[] ToDataGrid()
		{
			return new object[]
			{
				this.Enabled,
				this.HaveName,
				this.WantName
			};
		}

		public FlippingListJsonItem ToJsonItem(FlippingTypes type)
		{
			FlippingListJsonItem flippingListJsonItem = new FlippingListJsonItem();
			flippingListJsonItem.stock = -9999;
			flippingListJsonItem.type = type;
			if (type != FlippingTypes.Buy)
			{
				if (type == FlippingTypes.Sell)
				{
					flippingListJsonItem.have = API.smethod_4(this.HaveName);
					flippingListJsonItem.want = API.smethod_4(this.WantName);
					flippingListJsonItem.skip = this.SellListingsToSkip;
					flippingListJsonItem.take = this.SellListingsToTake;
				}
			}
			else
			{
				flippingListJsonItem.have = API.smethod_4(this.WantName);
				flippingListJsonItem.want = API.smethod_4(this.HaveName);
				flippingListJsonItem.skip = this.BuyListingsToSkip;
				flippingListJsonItem.take = this.BuyListingsToTake;
			}
			return flippingListJsonItem;
		}
	}
}
