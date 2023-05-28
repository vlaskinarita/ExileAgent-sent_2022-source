using System;
using PoEv2;
using PoEv2.Models;
using PoEv2.Models.Query;

namespace ns33
{
	internal sealed class Class151
	{
		public unsafe static BuyingTradeData smethod_0(FetchTradeResult fetchTradeResult_0, string string_0, TradeTypes tradeTypes_0, int int_0)
		{
			void* ptr = stackalloc byte[3];
			BuyingTradeData result;
			if (fetchTradeResult_0 == null || fetchTradeResult_0.gone || fetchTradeResult_0.listing.price == null)
			{
				result = null;
			}
			else
			{
				BuyingTradeData buyingTradeData = new BuyingTradeData();
				buyingTradeData.Time = DateTime.Now;
				buyingTradeData.Id = fetchTradeResult_0.id;
				buyingTradeData.Query = string_0;
				buyingTradeData.TradeType = tradeTypes_0;
				buyingTradeData.Priority = int_0;
				buyingTradeData.WhisperMessage = fetchTradeResult_0.listing.whisper;
				buyingTradeData.AccountName = fetchTradeResult_0.listing.account.name;
				buyingTradeData.CharacterName = fetchTradeResult_0.listing.account.lastCharacterName;
				buyingTradeData.Item = fetchTradeResult_0.item;
				if (tradeTypes_0 == TradeTypes.LiveSearch || tradeTypes_0 == TradeTypes.ItemBuying)
				{
					*(byte*)ptr = ((fetchTradeResult_0.listing.price.amount == 0m) ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						return null;
					}
					buyingTradeData.PlayerItemType = fetchTradeResult_0.item.typeLine;
					buyingTradeData.PlayerTotalStock = fetchTradeResult_0.item.stack;
					buyingTradeData.PlayerItemsPerTrade = 1m;
					buyingTradeData.OurItemsPerTrade = fetchTradeResult_0.listing.price.amount;
					buyingTradeData.OurItemType = fetchTradeResult_0.listing.price.currency;
				}
				((byte*)ptr)[1] = ((tradeTypes_0 == TradeTypes.BulkBuying) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					if (fetchTradeResult_0.listing.price.exchange == null || fetchTradeResult_0.listing.price.item == null || fetchTradeResult_0.listing.price.item.amount < 1m || fetchTradeResult_0.listing.price.exchange.amount == 0m)
					{
						return null;
					}
					buyingTradeData.PlayerItemType = fetchTradeResult_0.listing.price.item.currency;
					buyingTradeData.PlayerTotalStock = fetchTradeResult_0.listing.price.item.stock;
					buyingTradeData.PlayerItemsPerTrade = fetchTradeResult_0.listing.price.item.amount;
					buyingTradeData.OurItemType = fetchTradeResult_0.listing.price.exchange.currency;
					buyingTradeData.OurItemsPerTrade = fetchTradeResult_0.listing.price.exchange.amount;
				}
				((byte*)ptr)[2] = (buyingTradeData.OurItemType.smethod_25() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					result = null;
				}
				else
				{
					result = buyingTradeData;
				}
			}
			return result;
		}
	}
}
