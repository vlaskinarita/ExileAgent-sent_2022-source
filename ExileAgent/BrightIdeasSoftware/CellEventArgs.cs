using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class CellEventArgs : EventArgs
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

		public Point Location
		{
			get
			{
				return this.location;
			}
			internal set
			{
				this.location = value;
			}
		}

		public Keys ModifierKeys
		{
			get
			{
				return this.modifierKeys;
			}
			internal set
			{
				this.modifierKeys = value;
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

		public OlvListViewHitTestInfo HitTest
		{
			get
			{
				return this.hitTest;
			}
			internal set
			{
				this.hitTest = value;
			}
		}

		private ObjectListView listView;

		private object model;

		private int rowIndex = -1;

		private int columnIndex = -1;

		private OLVColumn column;

		private Point location;

		private Keys modifierKeys;

		private OLVListItem item;

		private OLVListSubItem subItem;

		private OlvListViewHitTestInfo hitTest;

		public bool Handled;
	}
}
