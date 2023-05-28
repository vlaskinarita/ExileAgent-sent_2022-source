using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class OLVGroupComparer : IComparer<OLVGroup>
	{
		public OLVGroupComparer(SortOrder order)
		{
			this.sortOrder = order;
		}

		public int Compare(OLVGroup x, OLVGroup y)
		{
			int num;
			if (x.SortValue != null && y.SortValue != null)
			{
				num = x.SortValue.CompareTo(y.SortValue);
			}
			else
			{
				num = string.Compare(x.Header, y.Header, StringComparison.CurrentCultureIgnoreCase);
			}
			if (this.sortOrder == SortOrder.Descending)
			{
				num = -num;
			}
			return num;
		}

		private SortOrder sortOrder;
	}
}
