using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class OLVListItem : ListViewItem
	{
		public OLVListItem(object rowObject)
		{
			this.rowObject = rowObject;
		}

		public OLVListItem(object rowObject, string text, object image) : base(text, -1)
		{
			this.rowObject = rowObject;
			this.imageSelector = image;
		}

		public new Rectangle Bounds
		{
			get
			{
				Rectangle result;
				try
				{
					result = base.Bounds;
				}
				catch (ArgumentException)
				{
					result = Rectangle.Empty;
				}
				return result;
			}
		}

		public Rectangle? CellPadding
		{
			get
			{
				return this.cellPadding;
			}
			set
			{
				this.cellPadding = value;
			}
		}

		public StringAlignment? CellVerticalAlignment
		{
			get
			{
				return this.cellVerticalAlignment;
			}
			set
			{
				this.cellVerticalAlignment = value;
			}
		}

		public new bool Checked
		{
			get
			{
				return base.Checked;
			}
			set
			{
				if (this.Checked != value)
				{
					if (value)
					{
						((ObjectListView)base.ListView).CheckObject(this.RowObject);
						return;
					}
					((ObjectListView)base.ListView).UncheckObject(this.RowObject);
				}
			}
		}

		public CheckState CheckState
		{
			get
			{
				switch (base.StateImageIndex)
				{
				case 0:
					return CheckState.Unchecked;
				case 1:
					return CheckState.Checked;
				case 2:
					return CheckState.Indeterminate;
				default:
					return CheckState.Unchecked;
				}
			}
			set
			{
				if (this.checkState == value)
				{
					return;
				}
				this.checkState = value;
				switch (value)
				{
				case CheckState.Unchecked:
					base.StateImageIndex = 0;
					return;
				case CheckState.Checked:
					base.StateImageIndex = 1;
					return;
				case CheckState.Indeterminate:
					base.StateImageIndex = 2;
					return;
				default:
					return;
				}
			}
		}

		public bool HasDecoration
		{
			get
			{
				return this.decorations != null && this.decorations.Count > 0;
			}
		}

		public IDecoration Decoration
		{
			get
			{
				if (this.HasDecoration)
				{
					return this.Decorations[0];
				}
				return null;
			}
			set
			{
				this.Decorations.Clear();
				if (value != null)
				{
					this.Decorations.Add(value);
				}
			}
		}

		public IList<IDecoration> Decorations
		{
			get
			{
				if (this.decorations == null)
				{
					this.decorations = new List<IDecoration>();
				}
				return this.decorations;
			}
		}

		public object ImageSelector
		{
			get
			{
				return this.imageSelector;
			}
			set
			{
				this.imageSelector = value;
				if (value is int)
				{
					base.ImageIndex = (int)value;
					return;
				}
				if (value is string)
				{
					base.ImageKey = (string)value;
					return;
				}
				base.ImageIndex = -1;
			}
		}

		public object RowObject
		{
			get
			{
				return this.rowObject;
			}
			set
			{
				this.rowObject = value;
			}
		}

		public OLVListSubItem GetSubItem(int index)
		{
			if (index >= 0 && index < base.SubItems.Count)
			{
				return (OLVListSubItem)base.SubItems[index];
			}
			return null;
		}

		public Rectangle GetSubItemBounds(int subItemIndex)
		{
			if (subItemIndex == 0)
			{
				Rectangle bounds = this.Bounds;
				Point scrolledColumnSides = NativeMethods.GetScrolledColumnSides(base.ListView, subItemIndex);
				bounds.X = scrolledColumnSides.X + 1;
				bounds.Width = scrolledColumnSides.Y - scrolledColumnSides.X;
				return bounds;
			}
			OLVListSubItem subItem = this.GetSubItem(subItemIndex);
			if (subItem != null)
			{
				return subItem.Bounds;
			}
			return default(Rectangle);
		}

		private Rectangle? cellPadding;

		private StringAlignment? cellVerticalAlignment;

		private CheckState checkState;

		private IList<IDecoration> decorations;

		private object imageSelector;

		private object rowObject;
	}
}
