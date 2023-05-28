using System;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class AfterSortingEventArgs : EventArgs
	{
		public AfterSortingEventArgs(OLVColumn groupColumn, SortOrder groupOrder, OLVColumn column, SortOrder order, OLVColumn column2, SortOrder order2)
		{
			this.columnToGroupBy = groupColumn;
			this.groupByOrder = groupOrder;
			this.columnToSort = column;
			this.sortOrder = order;
			this.secondaryColumnToSort = column2;
			this.secondarySortOrder = order2;
		}

		public AfterSortingEventArgs(BeforeSortingEventArgs args)
		{
			this.columnToGroupBy = args.ColumnToGroupBy;
			this.groupByOrder = args.GroupByOrder;
			this.columnToSort = args.ColumnToSort;
			this.sortOrder = args.SortOrder;
			this.secondaryColumnToSort = args.SecondaryColumnToSort;
			this.secondarySortOrder = args.SecondarySortOrder;
		}

		public OLVColumn ColumnToGroupBy
		{
			get
			{
				return this.columnToGroupBy;
			}
		}

		public SortOrder GroupByOrder
		{
			get
			{
				return this.groupByOrder;
			}
		}

		public OLVColumn ColumnToSort
		{
			get
			{
				return this.columnToSort;
			}
		}

		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		public OLVColumn SecondaryColumnToSort
		{
			get
			{
				return this.secondaryColumnToSort;
			}
		}

		public SortOrder SecondarySortOrder
		{
			get
			{
				return this.secondarySortOrder;
			}
		}

		private OLVColumn columnToGroupBy;

		private SortOrder groupByOrder;

		private OLVColumn columnToSort;

		private SortOrder sortOrder;

		private OLVColumn secondaryColumnToSort;

		private SortOrder secondarySortOrder;
	}
}
