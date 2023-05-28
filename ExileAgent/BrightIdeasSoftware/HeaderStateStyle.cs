using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public sealed class HeaderStateStyle
	{
		[DefaultValue(null)]
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				this.foreColor = value;
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color FrameColor
		{
			get
			{
				return this.frameColor;
			}
			set
			{
				this.frameColor = value;
			}
		}

		[DefaultValue(0f)]
		public float FrameWidth
		{
			get
			{
				return this.frameWidth;
			}
			set
			{
				this.frameWidth = value;
			}
		}

		private Font font;

		private Color foreColor;

		private Color backColor;

		private Color frameColor;

		private float frameWidth;
	}
}
