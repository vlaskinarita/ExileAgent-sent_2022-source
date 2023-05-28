using System;

namespace BrightIdeasSoftware
{
	public sealed class CellClickEventArgs : CellEventArgs
	{
		public int ClickCount
		{
			get
			{
				return this.clickCount;
			}
			set
			{
				this.clickCount = value;
			}
		}

		private int clickCount;
	}
}
