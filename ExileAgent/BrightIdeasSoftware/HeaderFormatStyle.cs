using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class HeaderFormatStyle : Component
	{
		public HeaderFormatStyle()
		{
			this.Hot = new HeaderStateStyle();
			this.Normal = new HeaderStateStyle();
			this.Pressed = new HeaderStateStyle();
		}

		[Description("How should the header be drawn when the mouse is over it?")]
		[Category("Appearance")]
		public HeaderStateStyle Hot
		{
			get
			{
				return this.hotStyle;
			}
			set
			{
				this.hotStyle = value;
			}
		}

		[Description("How should a column header normally be drawn")]
		[Category("Appearance")]
		public HeaderStateStyle Normal
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
		[Description("How should a column header be drawn when it is pressed")]
		public HeaderStateStyle Pressed
		{
			get
			{
				return this.pressedStyle;
			}
			set
			{
				this.pressedStyle = value;
			}
		}

		public void SetFont(Font font)
		{
			this.Normal.Font = font;
			this.Hot.Font = font;
			this.Pressed.Font = font;
		}

		public void SetForeColor(Color color)
		{
			this.Normal.ForeColor = color;
			this.Hot.ForeColor = color;
			this.Pressed.ForeColor = color;
		}

		public void SetBackColor(Color color)
		{
			this.Normal.BackColor = color;
			this.Hot.BackColor = color;
			this.Pressed.BackColor = color;
		}

		private HeaderStateStyle hotStyle;

		private HeaderStateStyle normalStyle;

		private HeaderStateStyle pressedStyle;
	}
}
