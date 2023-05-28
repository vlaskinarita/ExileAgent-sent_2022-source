using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns17
{
	internal sealed class Class299
	{
		public Mods Mods { get; set; }

		public JsonItem InitialItem { get; set; }

		public Item CraftedItem { get; set; } = null;

		public JsonTab Tab { get; set; }

		public ToolTip ToolTip { get; set; }

		public int RequiredPrefixes { get; set; }

		public List<Mod> Prefixes { get; set; }

		public List<Tier> PrefixTiers { get; set; }

		public int RequiredSuffixes { get; set; }

		public List<Mod> Suffixes { get; set; }

		public List<Tier> SuffixTiers { get; set; }

		public bool OutOfMaterials { get; set; }

		public unsafe string ToString()
		{
			void* ptr = stackalloc byte[2];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(Class299.getString_0(107285181), this.InitialItem.method_3()));
			stringBuilder.AppendLine(string.Format(Class299.getString_0(107285168), this.Tab.n, this.Tab.i));
			*(byte*)ptr = ((this.Prefixes != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				stringBuilder.AppendLine(string.Format(Class299.getString_0(107285147), this.RequiredPrefixes));
				stringBuilder.AppendLine(string.Format(Class299.getString_0(107285114), string.Join<Mod>(Environment.NewLine, this.Prefixes)));
				stringBuilder.AppendLine(string.Format(Class299.getString_0(107285057), string.Join<Tier>(Environment.NewLine, this.PrefixTiers)));
			}
			((byte*)ptr)[1] = ((this.Suffixes != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				stringBuilder.AppendLine(string.Format(Class299.getString_0(107285032), this.RequiredSuffixes));
				stringBuilder.AppendLine(string.Format(Class299.getString_0(107284999), string.Join<Mod>(Environment.NewLine, this.Suffixes)));
				stringBuilder.AppendLine(string.Format(Class299.getString_0(107284974), string.Join<Tier>(Environment.NewLine, this.SuffixTiers)));
			}
			return stringBuilder.ToString();
		}

		static Class299()
		{
			Strings.CreateGetStringDelegate(typeof(Class299));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Mods mods_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonItem jsonItem_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Item item_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private JsonTab jsonTab_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ToolTip toolTip_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Tier> list_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Mod> list_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Tier> list_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
