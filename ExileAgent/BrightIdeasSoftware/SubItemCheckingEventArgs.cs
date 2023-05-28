using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class SubItemCheckingEventArgs : CancellableEventArgs
	{
		public SubItemCheckingEventArgs(OLVColumn column, OLVListItem item, int subItemIndex, CheckState currentValue, CheckState newValue)
		{
			this.column = column;
			this.listViewItem = item;
			this.subItemIndex = subItemIndex;
			this.currentValue = currentValue;
			this.newValue = newValue;
		}

		public OLVColumn Column
		{
			get
			{
				return this.column;
			}
		}

		public object RowObject
		{
			get
			{
				return this.listViewItem.RowObject;
			}
		}

		public OLVListItem ListViewItem
		{
			get
			{
				return this.listViewItem;
			}
		}

		public CheckState CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		public CheckState NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		public int SubItemIndex
		{
			get
			{
				return this.subItemIndex;
			}
		}

		private OLVColumn column;

		private OLVListItem listViewItem;

		private CheckState currentValue;

		private CheckState newValue;

		private int subItemIndex;
	}
}
