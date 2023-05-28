using System;

namespace BrightIdeasSoftware
{
	public class FormatRowEventArgs : EventArgs
	{
		public ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			internal set
			{
				this.listView = value;
			}
		}

		public OLVListItem Item
		{
			get
			{
				return this.item;
			}
			internal set
			{
				this.item = value;
			}
		}

		public object Model
		{
			get
			{
				return this.Item.RowObject;
			}
		}

		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
			internal set
			{
				this.rowIndex = value;
			}
		}

		public int DisplayIndex
		{
			get
			{
				return this.displayIndex;
			}
			internal set
			{
				this.displayIndex = value;
			}
		}

		public bool UseCellFormatEvents
		{
			get
			{
				return this.useCellFormatEvents;
			}
			set
			{
				this.useCellFormatEvents = value;
			}
		}

		private ObjectListView listView;

		private OLVListItem item;

		private int rowIndex = -1;

		private int displayIndex = -1;

		private bool useCellFormatEvents;
	}
}
