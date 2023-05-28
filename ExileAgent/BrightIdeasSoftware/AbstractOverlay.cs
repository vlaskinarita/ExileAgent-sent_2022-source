using System;
using System.ComponentModel;
using System.Drawing;

namespace BrightIdeasSoftware
{
	public sealed class AbstractOverlay : IOverlay, ITransparentOverlay
	{
		public void Draw(ObjectListView olv, Graphics g, Rectangle r)
		{
		}

		[Category("ObjectListView")]
		[Description("How transparent should this overlay be")]
		[DefaultValue(128)]
		[NotifyParentProperty(true)]
		public int Transparency
		{
			get
			{
				return this.transparency;
			}
			set
			{
				this.transparency = Math.Min(255, Math.Max(0, value));
			}
		}

		private int transparency = 128;
	}
}
