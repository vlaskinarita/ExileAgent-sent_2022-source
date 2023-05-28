using System;

namespace BrightIdeasSoftware
{
	public sealed class HyperlinkEventArgs : EventArgs
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

		public object Model
		{
			get
			{
				return this.model;
			}
			internal set
			{
				this.model = value;
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

		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
			internal set
			{
				this.columnIndex = value;
			}
		}

		public OLVColumn Column
		{
			get
			{
				return this.column;
			}
			internal set
			{
				this.column = value;
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

		public OLVListSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
			internal set
			{
				this.subItem = value;
			}
		}

		public string Url
		{
			get
			{
				return this.url;
			}
			internal set
			{
				this.url = value;
			}
		}

		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		private ObjectListView listView;

		private object model;

		private int rowIndex = -1;

		private int columnIndex = -1;

		private OLVColumn column;

		private OLVListItem item;

		private OLVListSubItem subItem;

		private string url;

		private bool handled;
	}
}
