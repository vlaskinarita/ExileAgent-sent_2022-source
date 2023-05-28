using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PoEv2.Models;

namespace ns25
{
	internal sealed class Class245
	{
		public string Time { get; set; }

		public string Type { get; set; }

		public decimal SoldCount { get; set; }

		public string SoldName { get; set; }

		public decimal BoughtCount { get; set; }

		public string BoughtName { get; set; }

		public Dictionary<string, decimal> Tabs { get; set; }

		public string Buyer { get; set; }

		public string Status { get; set; }

		public Class245()
		{
		}

		public Class245(Order order_0)
		{
			this.Time = order_0.Time;
			this.Type = order_0.OrderType.ToString();
			this.SoldCount = order_0.my_item_amount;
			this.SoldName = order_0.my_item_name;
			this.BoughtCount = order_0.player_item_amount;
			this.BoughtName = order_0.player_item_name;
			this.Buyer = order_0.player.name;
			this.Status = order_0.TradeCompleted;
			this.Tabs = new Dictionary<string, decimal>();
			foreach (KeyValuePair<JsonTab, int> keyValuePair in order_0.my_inventory_items)
			{
				this.Tabs.Add(keyValuePair.Key.n, keyValuePair.Value);
			}
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, decimal> dictionary_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;
	}
}
