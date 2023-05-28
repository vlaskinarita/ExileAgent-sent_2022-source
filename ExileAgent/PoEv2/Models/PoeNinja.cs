using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace PoEv2.Models
{
	public sealed class PoeNinja
	{
		[JsonProperty("lines")]
		public PoeNinja.Line[] Lines { get; set; }

		[JsonProperty("currencyDetails")]
		public PoeNinja.GClass1[] CurrencyDetails { get; set; }

		[JsonProperty("language")]
		public PoeNinja.GClass2 Language { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PoeNinja.Line[] line_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PoeNinja.GClass1[] gclass1_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PoeNinja.GClass2 gclass2_0;

		public sealed class GClass1
		{
			[JsonProperty("id")]
			public long Id { get; set; }

			[JsonProperty("icon")]
			public Uri Icon { get; set; }

			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("poeTradeId")]
			public long PoeTradeId { get; set; }

			[JsonProperty("tradeId")]
			public string TradeId { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private Uri uri_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;
		}

		public sealed class GClass2
		{
			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("translations")]
			public PoeNinja.GClass3 Translations { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass3 gclass3_0;
		}

		public sealed class GClass3
		{
		}

		public sealed class Line
		{
			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("currencyTypeName")]
			public string CurrencyTypeName { get; set; }

			[JsonProperty("pay")]
			public PoeNinja.GClass5 Pay { get; set; }

			[JsonProperty("receive")]
			public PoeNinja.GClass5 Receive { get; set; }

			[JsonProperty("paySparkLine")]
			public PoeNinja.GClass6 PaySparkLine { get; set; }

			[JsonProperty("receiveSparkLine")]
			public PoeNinja.GClass4 ReceiveSparkLine { get; set; }

			[JsonProperty("chaosEquivalent")]
			public double ChaosEquivalent { get; set; }

			[JsonProperty("lowConfidencePaySparkLine")]
			public PoeNinja.GClass4 LowConfidencePaySparkLine { get; set; }

			[JsonProperty("lowConfidenceReceiveSparkLine")]
			public PoeNinja.GClass4 LowConfidenceReceiveSparkLine { get; set; }

			[JsonProperty("detailsId")]
			public string DetailsId { get; set; }

			[JsonProperty("chaosValue")]
			public double ChaosValue { get; set; }

			[JsonProperty("exaltedValue")]
			public double ExaltedValue { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass5 gclass5_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass5 gclass5_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass6 gclass6_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass4 gclass4_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double double_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass4 gclass4_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private PoeNinja.GClass4 gclass4_2;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string string_2;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double double_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double double_2;
		}

		public sealed class GClass4
		{
			[JsonProperty("data")]
			public double[] Data { get; set; }

			[JsonProperty("totalChange")]
			public double TotalChange { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double[] double_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double double_1;
		}

		public sealed class GClass5
		{
			[JsonProperty("id")]
			public long Id { get; set; }

			[JsonProperty("league_id")]
			public long LeagueId { get; set; }

			[JsonProperty("pay_currency_id")]
			public long PayCurrencyId { get; set; }

			[JsonProperty("get_currency_id")]
			public long GetCurrencyId { get; set; }

			[JsonProperty("sample_time_utc")]
			public DateTimeOffset SampleTimeUtc { get; set; }

			[JsonProperty("count")]
			public long Count { get; set; }

			[JsonProperty("value")]
			public double Value { get; set; }

			[JsonProperty("data_point_count")]
			public long DataPointCount { get; set; }

			[JsonProperty("includes_secondary")]
			public bool IncludesSecondary { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_2;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_3;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private DateTimeOffset dateTimeOffset_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_4;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double double_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private long long_5;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool bool_0;
		}

		public sealed class GClass6
		{
			[JsonProperty("data")]
			public double?[] Data { get; set; }

			[JsonProperty("totalChange")]
			public double TotalChange { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double?[] nullable_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private double double_0;
		}
	}
}
