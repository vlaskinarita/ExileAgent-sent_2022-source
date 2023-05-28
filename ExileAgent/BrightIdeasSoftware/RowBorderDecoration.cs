using System;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class RowBorderDecoration : BorderDecoration
	{
		public int LeftColumn
		{
			get
			{
				return this.leftColumn;
			}
			set
			{
				this.leftColumn = value;
			}
		}

		public int RightColumn
		{
			get
			{
				return this.rightColumn;
			}
			set
			{
				this.rightColumn = value;
			}
		}

		protected override Rectangle CalculateBounds()
		{
			Rectangle rowBounds = base.RowBounds;
			if (base.ListItem == null)
			{
				return rowBounds;
			}
			if (this.LeftColumn >= 0)
			{
				Rectangle subItemBounds = base.ListItem.GetSubItemBounds(this.LeftColumn);
				if (!subItemBounds.IsEmpty)
				{
					rowBounds.Width = rowBounds.Right - subItemBounds.Left;
					rowBounds.X = subItemBounds.Left;
				}
			}
			if (this.RightColumn >= 0)
			{
				Rectangle subItemBounds2 = base.ListItem.GetSubItemBounds(this.RightColumn);
				if (!subItemBounds2.IsEmpty)
				{
					rowBounds.Width = subItemBounds2.Right - rowBounds.Left;
				}
			}
			return rowBounds;
		}

		private int leftColumn = -1;

		private int rightColumn = -1;
	}
}
