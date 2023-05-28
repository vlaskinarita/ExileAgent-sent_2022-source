using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class HyperlinkStyle : Component
	{
		public HyperlinkStyle()
		{
			this.Normal = new CellStyle();
			this.Normal.ForeColor = Color.Blue;
			this.Over = new CellStyle();
			this.Over.FontStyle = FontStyle.Underline;
			this.Visited = new CellStyle();
			this.Visited.ForeColor = Color.Purple;
			this.OverCursor = Cursors.Hand;
		}

		[Description("How should hyperlinks be drawn")]
		[Category("Appearance")]
		public CellStyle Normal
		{
			get
			{
				return this.normalStyle;
			}
			set
			{
				this.normalStyle = value;
			}
		}

		[Category("Appearance")]
		[Description("How should hyperlinks be drawn when the mouse is over them?")]
		public CellStyle Over
		{
			get
			{
				return this.overStyle;
			}
			set
			{
				this.overStyle = value;
			}
		}

		[Description("How should hyperlinks be drawn after they have been clicked")]
		[Category("Appearance")]
		public CellStyle Visited
		{
			get
			{
				return this.visitedStyle;
			}
			set
			{
				this.visitedStyle = value;
			}
		}

		[Category("Appearance")]
		[Description("What cursor should be shown when the mouse is over a link?")]
		public Cursor OverCursor
		{
			get
			{
				return this.overCursor;
			}
			set
			{
				this.overCursor = value;
			}
		}

		private CellStyle normalStyle;

		private CellStyle overStyle;

		private CellStyle visitedStyle;

		private Cursor overCursor;
	}
}
