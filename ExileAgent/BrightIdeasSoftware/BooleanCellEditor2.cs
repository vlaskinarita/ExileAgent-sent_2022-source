using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	public sealed class BooleanCellEditor2 : CheckBox
	{
		public bool? Value
		{
			get
			{
				switch (base.CheckState)
				{
				default:
					return new bool?(false);
				case CheckState.Checked:
					return new bool?(true);
				case CheckState.Indeterminate:
					return null;
				}
			}
			set
			{
				if (value != null)
				{
					base.CheckState = (value.Value ? CheckState.Checked : CheckState.Unchecked);
					return;
				}
				base.CheckState = CheckState.Indeterminate;
			}
		}

		public new HorizontalAlignment TextAlign
		{
			get
			{
				ContentAlignment checkAlign = base.CheckAlign;
				if (checkAlign != ContentAlignment.MiddleLeft)
				{
					if (checkAlign == ContentAlignment.MiddleCenter)
					{
						return HorizontalAlignment.Center;
					}
					if (checkAlign == ContentAlignment.MiddleRight)
					{
						return HorizontalAlignment.Right;
					}
				}
				return HorizontalAlignment.Left;
			}
			set
			{
				switch (value)
				{
				case HorizontalAlignment.Left:
					base.CheckAlign = ContentAlignment.MiddleLeft;
					return;
				case HorizontalAlignment.Right:
					base.CheckAlign = ContentAlignment.MiddleRight;
					return;
				case HorizontalAlignment.Center:
					base.CheckAlign = ContentAlignment.MiddleCenter;
					return;
				default:
					return;
				}
			}
		}
	}
}
