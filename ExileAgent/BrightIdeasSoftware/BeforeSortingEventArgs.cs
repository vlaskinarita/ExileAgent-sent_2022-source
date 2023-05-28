using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class BeforeSortingEventArgs : CancellableEventArgs
	{
		public BeforeSortingEventArgs(OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2)
		{
			this.ColumnToGroupBy = column;
			this.GroupByOrder = order;
			this.ColumnToSort = column;
			this.SortOrder = order;
			this.SecondaryColumnToSort = column2;
			this.SecondarySortOrder = order2;
		}

		public BeforeSortingEventArgs(OLVColumn groupColumn, SortOrder groupOrder, OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2)
		{
			this.ColumnToGroupBy = groupColumn;
			this.GroupByOrder = groupOrder;
			this.ColumnToSort = column;
			this.SortOrder = order;
			this.SecondaryColumnToSort = column2;
			this.SecondarySortOrder = order2;
		}

		public bool Handled;

		public OLVColumn ColumnToGroupBy;

		public SortOrder GroupByOrder;

		public OLVColumn ColumnToSort;

		public SortOrder SortOrder;

		public OLVColumn SecondaryColumnToSort;

		public SortOrder SecondarySortOrder;
	}
}
