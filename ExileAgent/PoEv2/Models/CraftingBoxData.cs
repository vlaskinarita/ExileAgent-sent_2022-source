using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ns36;

namespace PoEv2.Models
{
	public sealed class CraftingBoxData
	{
		public Enum18 Type { get; set; }

		public FastObjectListView Box { get; set; }

		public ComboBox AffixBox { get; set; }

		public ComboBox TierBox { get; set; }

		public ComboBox CountBox { get; set; }

		public Button AddButton { get; set; }

		public Button LoadButton { get; set; }

		public Button SaveButton { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Enum18 enum18_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private FastObjectListView fastObjectListView_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ComboBox comboBox_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ComboBox comboBox_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ComboBox comboBox_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Button button_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Button button_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Button button_2;
	}
}
