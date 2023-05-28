using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace BrightIdeasSoftware
{
	public sealed class FlagRenderer : BaseRenderer
	{
		public void Add(object key, object imageSelector)
		{
			int num = ((IConvertible)key).ToInt32(NumberFormatInfo.InvariantInfo);
			this.imageMap[num] = imageSelector;
			this.keysInOrder.Remove(num);
			this.keysInOrder.Add(num);
		}

		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			IConvertible convertible = base.Aspect as IConvertible;
			if (convertible == null)
			{
				return;
			}
			r = this.ApplyCellPadding(r);
			int num = convertible.ToInt32(NumberFormatInfo.InvariantInfo);
			ArrayList arrayList = new ArrayList();
			foreach (int num2 in this.keysInOrder)
			{
				if ((num & num2) == num2)
				{
					Image image = this.GetImage(this.imageMap[num2]);
					if (image != null)
					{
						arrayList.Add(image);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				this.DrawImages(g, r, arrayList);
			}
		}

		protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
		{
			IConvertible convertible = base.Aspect as IConvertible;
			if (convertible == null)
			{
				return;
			}
			int num = convertible.ToInt32(NumberFormatInfo.InvariantInfo);
			Point location = base.Bounds.Location;
			foreach (int num2 in this.keysInOrder)
			{
				if ((num & num2) == num2)
				{
					Image image = this.GetImage(this.imageMap[num2]);
					if (image != null)
					{
						Rectangle rectangle = new Rectangle(location, image.Size);
						if (rectangle.Contains(x, y))
						{
							hti.UserData = num2;
							break;
						}
						location.X += image.Width + base.Spacing;
					}
				}
			}
		}

		private List<int> keysInOrder = new List<int>();

		private Dictionary<int, object> imageMap = new Dictionary<int, object>();
	}
}
