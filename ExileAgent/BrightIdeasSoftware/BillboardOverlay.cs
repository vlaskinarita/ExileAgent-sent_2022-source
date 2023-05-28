using System;
using System.Drawing;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class BillboardOverlay : TextOverlay
	{
		public BillboardOverlay()
		{
			base.Transparency = 255;
			base.BackColor = Color.PeachPuff;
			base.TextColor = Color.Black;
			base.BorderColor = Color.Empty;
			base.Font = new Font(BillboardOverlay.getString_1(107316712), 10f);
		}

		public Point Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
			if (string.IsNullOrEmpty(base.Text))
			{
				return;
			}
			Rectangle textRect = this.CalculateTextBounds(g, r, base.Text);
			textRect.Location = this.Location;
			if (textRect.Right > r.Width)
			{
				textRect.X = Math.Max(r.Left, r.Width - textRect.Width);
			}
			if (textRect.Bottom > r.Height)
			{
				textRect.Y = Math.Max(r.Top, r.Height - textRect.Height);
			}
			this.DrawBorderedText(g, textRect, base.Text, 255);
		}

		static BillboardOverlay()
		{
			Strings.CreateGetStringDelegate(typeof(BillboardOverlay));
		}

		private Point location;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
