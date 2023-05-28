using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ns22;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Query
{
	public sealed class FetchTradeResult
	{
		public string id { get; set; }

		public FetchTradeResult.GClass19 listing { get; set; }

		public JsonItem item { get; set; }

		public bool gone { get; set; }

		public string ToString()
		{
			return this.listing.account.name;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private FetchTradeResult.GClass19 gclass19_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonItem jsonItem_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		public sealed class GClass12
		{
			public string name { get; set; }

			public int x { get; set; }

			public int y { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_1;
		}

		public sealed class GClass13
		{
			public string league { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;
		}

		public sealed class GClass14
		{
			public string name { get; set; }

			public string lastCharacterName { get; set; }

			public FetchTradeResult.GClass13 online { get; set; }

			public string language { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass13 gclass13_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_2;
		}

		public sealed class GClass15
		{
			public string type { get; set; }

			public decimal amount { get; set; }

			public string currency { get; set; }

			public FetchTradeResult.GClass17 exchange { get; set; }

			public FetchTradeResult.GClass18 item { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private decimal decimal_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass17 gclass17_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass18 gclass18_0;
		}

		public sealed class GClass16
		{
			public FetchTradeResult.GClass17 exchange { get; set; }

			public FetchTradeResult.GClass18 item { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass17 gclass17_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass18 gclass18_0;
		}

		public sealed class GClass17
		{
			public decimal amount { get; set; }

			public string currency { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private decimal decimal_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;
		}

		public sealed class GClass18
		{
			public decimal amount { get; set; }

			public string currency { get; set; }

			public int stock { get; set; }

			public string id { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private decimal decimal_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;
		}

		public sealed class GClass19
		{
			public string method { get; set; }

			public DateTime indexed { get; set; }

			public FetchTradeResult.GClass12 stash { get; set; }

			public string whisper { get; set; }

			public FetchTradeResult.GClass14 account { get; set; }

			public FetchTradeResult.GClass15 price { get; set; }

			public List<FetchTradeResult.GClass16> offers { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private DateTime dateTime_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass12 gclass12_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass14 gclass14_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private FetchTradeResult.GClass15 gclass15_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<FetchTradeResult.GClass16> list_0;
		}

		public sealed class GClass20 : JsonConverter
		{
			public override bool CanConvert(Type objectType)
			{
				return objectType == typeof(Class266);
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				JObject jobject = JObject.Load(reader);
				Class266 @class = new Class266();
				@class.id = (string)jobject[FetchTradeResult.GClass20.getString_0(107452360)];
				@class.total = (int)jobject[FetchTradeResult.GClass20.getString_0(107249194)];
				@class.result = new List<FetchTradeResult>();
				JToken jtoken = jobject[FetchTradeResult.GClass20.getString_0(107229107)];
				foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
				{
					JProperty jproperty = (JProperty)jtoken2;
					FetchTradeResult fetchTradeResult = jproperty.Value.ToObject<FetchTradeResult>();
					JToken value = jproperty.Value;
					if (value[FetchTradeResult.GClass20.getString_0(107249217)][FetchTradeResult.GClass20.getString_0(107249172)] != null)
					{
						JToken jtoken3 = value[FetchTradeResult.GClass20.getString_0(107249217)][FetchTradeResult.GClass20.getString_0(107249172)].First<JToken>();
						FetchTradeResult.GClass15 gclass = new FetchTradeResult.GClass15();
						gclass.amount = (decimal)jtoken3[FetchTradeResult.GClass20.getString_0(107249163)][FetchTradeResult.GClass20.getString_0(107249182)] / (decimal)jtoken3[FetchTradeResult.GClass20.getString_0(107249141)][FetchTradeResult.GClass20.getString_0(107249182)];
						gclass.currency = (string)jtoken3[FetchTradeResult.GClass20.getString_0(107249163)][FetchTradeResult.GClass20.getString_0(107249132)];
						gclass.exchange = jtoken3[FetchTradeResult.GClass20.getString_0(107249163)].ToObject<FetchTradeResult.GClass17>();
						gclass.item = jtoken3[FetchTradeResult.GClass20.getString_0(107249141)].ToObject<FetchTradeResult.GClass18>();
						fetchTradeResult.listing.price = gclass;
					}
					@class.result.Add(fetchTradeResult);
				}
				return @class;
			}

			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}

			static GClass20()
			{
				Strings.CreateGetStringDelegate(typeof(FetchTradeResult.GClass20));
			}

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
