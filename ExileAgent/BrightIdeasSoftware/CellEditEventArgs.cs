using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class CellEditEventArgs : EventArgs
	{
		public CellEditEventArgs(OLVColumn column, Control control, Rectangle r, OLVListItem item, int subItemIndex)
		{
			this.Control = control;
			this.column = column;
			this.cellBounds = r;
			this.listViewItem = item;
			this.rowObject = item.RowObject;
			this.subItemIndex = subItemIndex;
			this.value = column.GetValue(item.RowObject);
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
				return this.rowObject;
			}
		}

		public OLVListItem ListViewItem
		{
			get
			{
				return this.listViewItem;
			}
		}

		public object NewValue
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

		public object Value
		{
			get
			{
				return this.value;
			}
		}

		public Rectangle CellBounds
		{
			get
			{
				return this.cellBounds;
			}
		}

		public bool AutoDispose
		{
			get
			{
				return this.autoDispose;
			}
			set
			{
				this.autoDispose = value;
			}
		}

		public bool Cancel;

		public Control Control;

		private OLVColumn column;

		private object rowObject;

		private OLVListItem listViewItem;

		private object newValue;

		private int subItemIndex;

		private object value;

		private Rectangle cellBounds;

		private bool autoDispose = true;
	}
}
