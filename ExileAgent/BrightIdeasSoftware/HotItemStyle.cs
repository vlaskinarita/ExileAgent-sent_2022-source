using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class HotItemStyle : Component, IItemStyle
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

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IOverlay Overlay
		{
			get
			{
				return this.overlay;
			}
			set
			{
				this.overlay = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IDecoration Decoration
		{
			get
			{
				return this.decoration;
			}
			set
			{
				this.decoration = value;
			}
		}

		private Font font;

		private FontStyle fontStyle;

		private Color foreColor;

		private Color backColor;

		private IOverlay overlay;

		private IDecoration decoration;
	}
}
