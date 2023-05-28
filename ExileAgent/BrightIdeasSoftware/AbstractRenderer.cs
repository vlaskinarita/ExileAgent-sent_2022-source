using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	[Browsable(true)]
	public class AbstractRenderer : Component, IRenderer
	{
		public virtual bool RenderItem(DrawListViewItemEventArgs e, Graphics g, Rectangle itemBounds, object rowObject)
		{
			return true;
		}

		public virtual bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle cellBounds, object rowObject)
		{
			return false;
		}

		public virtual void HitTest(OlvListViewHitTestInfo hti, int x, int y)
		{
		}

		public virtual Rectangle GetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			return cellBounds;
		}
	}
}
