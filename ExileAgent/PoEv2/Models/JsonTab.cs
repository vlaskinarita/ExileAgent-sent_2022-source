using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models
{
	public sealed class JsonTab
	{
		public string id { get; set; }

		public string n { get; set; }

		public int i { get; set; }

		public string type { get; set; }

		[JsonIgnore]
		public bool IsExcluded { get; set; }

		[JsonIgnore]
		public bool ItemsLoaded { get; set; }

		public string ToString()
		{
			return this.n;
		}

		public static object smethod_0(object object_0)
		{
			return JsonTab.getString_0(107398027);
		}

		[JsonProperty("isSpecialTab")]
		public bool IsSpecialTab
		{
			get
			{
				string type = this.type;
				string text = type;
				return text == null || (!(text == JsonTab.getString_0(107382830)) && !(text == JsonTab.getString_0(107382815)) && !(text == JsonTab.getString_0(107382813)));
			}
		}

		[JsonProperty("isSupported")]
		public bool IsSupported
		{
			get
			{
				string type = this.type;
				string text = type;
				return text == null || (!(text == JsonTab.getString_0(107481350)) && !(text == JsonTab.getString_0(107481395)) && !(text == JsonTab.getString_0(107440519)));
			}
		}

		[JsonProperty("isCardTab")]
		public bool IsCardTab
		{
			get
			{
				return this.type == JsonTab.getString_0(107481350) || !this.IsSpecialTab;
			}
		}

		public bool ValidCraftingTab
		{
			get
			{
				return this.IsSupported && this.type != JsonTab.getString_0(107382800);
			}
		}

		public bool ValidCurrencyTab
		{
			get
			{
				return this.type == JsonTab.getString_0(107394875) || !this.IsSpecialTab;
			}
		}

		static JsonTab()
		{
			Strings.CreateGetStringDelegate(typeof(JsonTab));
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
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
