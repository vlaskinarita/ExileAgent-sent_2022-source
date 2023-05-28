using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using ns0;
using ns22;
using ns32;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public sealed class Order
	{
		public string message { get; set; }

		public int OurMapTier { get; set; }

		public int PlayerMapTier { get; set; }

		public bool PlayerItemUnique { get; set; }

		public bool OurItemUnique { get; set; }

		public string my_item_name { get; set; }

		public decimal my_item_amount { get; set; }

		public Dictionary<JsonTab, List<JsonItem>> my_items { get; set; } = new Dictionary<JsonTab, List<JsonItem>>();

		public Dictionary<JsonTab, int> my_inventory_items { get; set; } = new Dictionary<JsonTab, int>();

		public List<string> BuyItem64 { get; set; } = new List<string>();

		public List<JsonItem> InventoryList { get; set; }

		public Player player { get; set; } = new Player();

		public string player_item_name { get; set; }

		public decimal player_item_amount { get; set; }

		public int left_pos { get; set; }

		public int top_pos { get; set; }

		public JsonTab stash { get; set; } = new JsonTab
		{
			i = -1
		};

		public int seconds_to_trade { get; set; }

		public DateTime ProcessedTime { get; set; }

		public string messageToSay { get; set; }

		public Order.Type OrderType { get; set; }

		public string OriginalNote { get; set; }

		public bool Flipping { get; set; }

		public decimal DecimalAmount { get; set; }

		public string DecimalCurrencyType { get; set; }

		public DateTime WhisperTime { get; set; }

		public TradeTypes BuyType { get; set; }

		public Class246 UpdateRequest { get; set; }

		public string BaseItemName { get; set; }

		public Dictionary<int, List<JsonItem>> StashBackup { get; set; }

		public Class240 BuySettings { get; set; }

		public bool IsSingleItem
		{
			get
			{
				return this.left_pos != 0 || this.top_pos != 0;
			}
		}

		public string Time
		{
			get
			{
				return this.dateTime_1.ToString().Replace(Order.getString_0(107370143), Order.getString_0(107373361));
			}
		}

		public int SoldCount
		{
			get
			{
				return (int)this.my_item_amount;
			}
		}

		public string SoldName
		{
			get
			{
				return this.my_item_name;
			}
		}

		public int ReceivedCount
		{
			get
			{
				return (int)this.player_item_amount;
			}
		}

		public string ReceivedName
		{
			get
			{
				return this.player_item_name;
			}
		}

		public string Buyer
		{
			get
			{
				return this.player.name;
			}
		}

		public string Stash { get; set; }

		public string TradeCompleted { get; set; }

		public Order()
		{
		}

		public Order(decimal myItemAmount, string myItemName)
		{
			this.my_item_amount = myItemAmount;
			this.my_item_name = myItemName;
		}

		public int my_item_amount_floor
		{
			get
			{
				return (int)Math.Floor(this.my_item_amount);
			}
		}

		public bool IsFlip
		{
			get
			{
				return Class255.FlippingList.Any(new Func<FlippingListItem, bool>(Order.<>c.<>9.method_0)) && Class255.class105_0.method_4(ConfigOptions.FlippingEnabled) && Class255.FlippingList.Any(new Func<FlippingListItem, bool>(this.method_0));
			}
		}

		[CompilerGenerated]
		private bool method_0(FlippingListItem flippingListItem_0)
		{
			return (flippingListItem_0.Enabled && flippingListItem_0.HaveName == this.my_item_name && flippingListItem_0.WantName == this.player_item_name) || (flippingListItem_0.HaveName == this.player_item_name && flippingListItem_0.WantName == this.my_item_name);
		}

		static Order()
		{
			Strings.CreateGetStringDelegate(typeof(Order));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<JsonTab, List<JsonItem>> dictionary_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<JsonTab, int> dictionary_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<string> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<JsonItem> list_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Player player_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonTab jsonTab_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_4;

		public bool bool_2 = false;

		public bool bool_3 = false;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime dateTime_0;

		public DateTime dateTime_1;

		public TimeSpan timeSpan_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		public Order.TradeStates tradeStates_0 = Order.TradeStates.NotStarted;

		public Order.GEnum2 genum2_0 = Order.GEnum2.const_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Order.Type type_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime dateTime_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TradeTypes tradeTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class246 class246_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<int, List<JsonItem>> dictionary_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class240 class240_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_7;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_8;

		[NonSerialized]
		internal static GetString getString_0;

		public enum TradeStates
		{
			NotStarted,
			ItemOnCursor,
			Pending,
			Cancelled,
			Complete,
			ClickedAccept,
			Sold,
			Offline,
			UserIgnored,
			DND
		}

		public enum GEnum2
		{
			const_0,
			const_1,
			const_2
		}

		public enum Type
		{
			Sell,
			Buy
		}
	}
}
