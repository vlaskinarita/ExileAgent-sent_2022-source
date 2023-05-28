using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class ColumnComparer : IComparer<OLVListItem>, IComparer
	{
		public ColumnComparer(OLVColumn col, SortOrder order)
		{
			this.column = col;
			this.sortOrder = order;
		}

		public ColumnComparer(OLVColumn col, SortOrder order, OLVColumn col2, SortOrder order2) : this(col, order)
		{
			if (col != col2)
			{
				this.secondComparer = new ColumnComparer(col2, order2);
			}
		}

		public int Compare(object x, object y)
		{
			return this.Compare((OLVListItem)x, (OLVListItem)y);
		}

		public int Compare(OLVListItem x, OLVListItem y)
		{
			if (this.sortOrder == SortOrder.None)
			{
				return 0;
			}
			object value = this.column.GetValue(x.RowObject);
			object value2 = this.column.GetValue(y.RowObject);
			bool flag = value == null || value == DBNull.Value;
			bool flag2 = value2 == null || value2 == DBNull.Value;
			int num;
			if (!flag && !flag2)
			{
				num = this.CompareValues(value, value2);
			}
			else if (flag && flag2)
			{
				num = 0;
			}
			else
			{
				num = (flag ? -1 : 1);
			}
			if (this.sortOrder == SortOrder.Descending)
			{
				num = -num;
			}
			if (num == 0 && this.secondComparer != null)
			{
				num = this.secondComparer.Compare(x, y);
			}
			return num;
		}

		public int CompareValues(object x, object y)
		{
			string text = x as string;
			if (text != null)
			{
				return string.Compare(text, (string)y, StringComparison.CurrentCultureIgnoreCase);
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			return 0;
		}

		private OLVColumn column;

		private SortOrder sortOrder;

		private ColumnComparer secondComparer;
	}
}
