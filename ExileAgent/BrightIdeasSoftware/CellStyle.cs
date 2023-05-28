using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public sealed class CellStyle : IItemStyle
	{
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

		[DefaultValue(FontStyle.Regular)]
		public FontStyle FontStyle
		{
			get
			{
				return this.fontStyle;
			}
			set
			{
				this.fontStyle = value;
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

		private Font font;

		private FontStyle fontStyle;

		private Color foreColor;

		private Color backColor;
	}
}
