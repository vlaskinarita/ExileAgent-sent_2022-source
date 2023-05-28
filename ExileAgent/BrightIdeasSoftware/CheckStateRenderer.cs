using System;
using System.Drawing;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class CheckStateRenderer : BaseRenderer
	{
		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			if (base.Column == null)
			{
				return;
			}
			r = this.ApplyCellPadding(r);
			CheckState checkState = base.Column.GetCheckState(base.RowObject);
			if (base.IsPrinting)
			{
				string key = CheckStateRenderer.getString_0(107317020);
				if (checkState == CheckState.Unchecked)
				{
					key = CheckStateRenderer.getString_0(107317045);
				}
				if (checkState == CheckState.Indeterminate)
				{
					key = CheckStateRenderer.getString_0(107316963);
				}
				this.DrawAlignedImage(g, r, base.ListView.SmallImageList.Images[key]);
				return;
			}
			r = base.CalculateCheckBoxBounds(g, r);
			CheckBoxRenderer.DrawCheckBox(g, r.Location, this.GetCheckBoxState(checkState));
		}

		protected override Rectangle HandleGetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			return base.CalculatePaddedAlignedBounds(g, cellBounds, preferredSize);
		}

		protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
		{
			if (base.CalculateCheckBoxBounds(g, base.Bounds).Contains(x, y))
			{
				hti.HitTestLocation = HitTestLocation.CheckBox;
			}
		}

		static CheckStateRenderer()
		{
			Strings.CreateGetStringDelegate(typeof(CheckStateRenderer));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
