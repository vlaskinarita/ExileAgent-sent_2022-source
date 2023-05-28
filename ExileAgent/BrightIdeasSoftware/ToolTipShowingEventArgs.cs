using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class ToolTipShowingEventArgs : CellEventArgs
	{
		public ToolTipControl ToolTipControl
		{
			get
			{
				return this.toolTipControl;
			}
			internal set
			{
				this.toolTipControl = value;
			}
		}

		private ToolTipControl toolTipControl;

		public string Text;

		public RightToLeft RightToLeft;

		public bool? IsBalloon;

		public Color? BackColor;

		public Color? ForeColor;

		public string Title;

		public ToolTipControl.StandardIcons? StandardIcon;

		public int? AutoPopDelay;

		public Font Font;
	}
}
