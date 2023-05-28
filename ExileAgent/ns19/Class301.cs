using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using PoEv2.Models;
using PoEv2.PublicModels;

namespace ns19
{
	internal sealed class Class301
	{
		public string ItemBaseType { get; set; }

		public string Influences { get; set; }

		[JsonProperty]
		public Class301.Class302 PrefixSettings { get; private set; }

		[JsonProperty]
		public Class301.Class302 SuffixSettings { get; private set; }

		public Class301()
		{
		}

		public Class301(CraftingBoxData craftingBoxData_0, CraftingBoxData craftingBoxData_1)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			foreach (object obj in craftingBoxData_0.Box.Objects)
			{
				AffixItemViewModel affixItemViewModel = (AffixItemViewModel)obj;
				list.Add(craftingBoxData_0.AffixBox.Items.IndexOf(affixItemViewModel.Affix));
				list3.Add((affixItemViewModel.Tier != null) ? affixItemViewModel.Tier.Rank : -1);
			}
			foreach (object obj2 in craftingBoxData_1.Box.Objects)
			{
				AffixItemViewModel affixItemViewModel2 = (AffixItemViewModel)obj2;
				list2.Add(craftingBoxData_1.AffixBox.Items.IndexOf(affixItemViewModel2.Affix));
				list4.Add((affixItemViewModel2.Tier != null) ? affixItemViewModel2.Tier.Rank : -1);
			}
			Class301.Class302 prefixSettings = new Class301.Class302
			{
				Affixes = list,
				Tiers = list3,
				Count = craftingBoxData_0.CountBox.SelectedIndex
			};
			Class301.Class302 suffixSettings = new Class301.Class302
			{
				Affixes = list2,
				Tiers = list4,
				Count = craftingBoxData_1.CountBox.SelectedIndex
			};
			this.PrefixSettings = prefixSettings;
			this.SuffixSettings = suffixSettings;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class301.Class302 class302_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class301.Class302 class302_1;

		public sealed class Class302
		{
			public List<int> Affixes { get; set; }

			public List<int> Tiers { get; set; }

			public int Count { get; set; }

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<int> list_0;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private List<int> list_1;

			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private int int_0;
		}
	}
}
