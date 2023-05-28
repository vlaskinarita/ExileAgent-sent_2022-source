using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ns0;
using ns26;
using ns29;
using ns35;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Flipping
{
	public sealed class FlippingListJsonItem
	{
		public string have { get; set; }

		public string want { get; set; }

		public int stock { get; set; }

		public int skip { get; set; }

		public int take { get; set; }

		public FlippingTypes type { get; set; }

		[JsonProperty("avg")]
		public decimal Average { get; set; }

		public unsafe static NameValueCollection smethod_0(IEnumerable<FlippingListItem> ienumerable_0)
		{
			void* ptr = stackalloc byte[5];
			NameValueCollection nameValueCollection = new NameValueCollection();
			List<FlippingListJsonItem> list = new List<FlippingListJsonItem>();
			foreach (FlippingListItem flippingListItem in ienumerable_0)
			{
				list.Add(flippingListItem.ToJsonItem(FlippingTypes.Buy));
				((byte*)ptr)[4] = ((!flippingListItem.OnlyPriceHave) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					list.Add(flippingListItem.ToJsonItem(FlippingTypes.Sell));
				}
			}
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107450231), Class255.class105_0.method_3(ConfigOptions.AuthKey));
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107439544), Class255.class105_0.method_3(ConfigOptions.League));
			NameValueCollection nameValueCollection2 = nameValueCollection;
			string name = FlippingListJsonItem.getString_0(107439535);
			*(int*)ptr = 180;
			nameValueCollection2.Add(name, ((int*)ptr)->ToString());
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107439554), string.Format(FlippingListJsonItem.getString_0(107439545), string.Join(FlippingListJsonItem.getString_0(107439500), list.Select(new Func<FlippingListJsonItem, string>(FlippingListJsonItem.<>c.<>9.method_0)))));
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107439523), string.Format(FlippingListJsonItem.getString_0(107439545), string.Join(FlippingListJsonItem.getString_0(107439500), list.Select(new Func<FlippingListJsonItem, string>(FlippingListJsonItem.<>c.<>9.method_1)))));
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107439514), string.Format(FlippingListJsonItem.getString_0(107439985), string.Join<int>(FlippingListJsonItem.getString_0(107398248), list.Select(new Func<FlippingListJsonItem, int>(FlippingListJsonItem.<>c.<>9.method_2)))));
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107440008), string.Format(FlippingListJsonItem.getString_0(107439985), string.Join<int>(FlippingListJsonItem.getString_0(107398248), list.Select(new Func<FlippingListJsonItem, int>(FlippingListJsonItem.<>c.<>9.method_3)))));
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107439999), string.Format(FlippingListJsonItem.getString_0(107439985), string.Join<int>(FlippingListJsonItem.getString_0(107398248), list.Select(new Func<FlippingListJsonItem, int>(FlippingListJsonItem.<>c.<>9.method_4)))));
			nameValueCollection.Add(FlippingListJsonItem.getString_0(107370828), string.Format(FlippingListJsonItem.getString_0(107439985), string.Join<int>(FlippingListJsonItem.getString_0(107398248), list.Select(new Func<FlippingListJsonItem, int>(FlippingListJsonItem.<>c.<>9.method_5)))));
			return nameValueCollection;
		}

		public static List<FlippingListJsonItem> smethod_1(string string_2)
		{
			Class265 @class = JsonConvert.DeserializeObject<Class265>(string_2);
			List<FlippingListJsonItem> result;
			if (@class.Error)
			{
				result = null;
			}
			else
			{
				Class181.smethod_2(Enum11.const_3, FlippingListJsonItem.getString_0(107439958), new object[]
				{
					@class.BytesUsed / 1024,
					@class.BadProxies,
					@class.ProxiesUsed,
					@class.TotalTime
				});
				result = @class.FlippingList;
			}
			return result;
		}

		public string ToString()
		{
			return string.Format(FlippingListJsonItem.getString_0(107439849), new object[]
			{
				this.have,
				this.want,
				this.Average,
				this.type
			});
		}

		static FlippingListJsonItem()
		{
			Strings.CreateGetStringDelegate(typeof(FlippingListJsonItem));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

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
		private FlippingTypes flippingTypes_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal decimal_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
