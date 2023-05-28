using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class GroupingParameters
	{
		public GroupingParameters(ObjectListView olv, OLVColumn groupByColumn, SortOrder groupByOrder, OLVColumn column, SortOrder order, OLVColumn secondaryColumn, SortOrder secondaryOrder, string titleFormat, string titleSingularFormat, bool sortItemsByPrimaryColumn)
		{
			this.ListView = olv;
			this.GroupByColumn = groupByColumn;
			this.GroupByOrder = groupByOrder;
			this.PrimarySort = column;
			this.PrimarySortOrder = order;
			this.SecondarySort = secondaryColumn;
			this.SecondarySortOrder = secondaryOrder;
			this.SortItemsByPrimaryColumn = sortItemsByPrimaryColumn;
			this.TitleFormat = titleFormat;
			this.TitleSingularFormat = titleSingularFormat;
		}

		public ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			set
			{
				this.listView = value;
			}
		}

		public OLVColumn GroupByColumn
		{
			get
			{
				return this.groupByColumn;
			}
			set
			{
				this.groupByColumn = value;
			}
		}

		public SortOrder GroupByOrder
		{
			get
			{
				return this.groupByOrder;
			}
			set
			{
				this.groupByOrder = value;
			}
		}

		public IComparer<OLVGroup> GroupComparer
		{
			get
			{
				return this.groupComparer;
			}
			set
			{
				this.groupComparer = value;
			}
		}

		public IComparer<OLVListItem> ItemComparer
		{
			get
			{
				return this.itemComparer;
			}
			set
			{
				this.itemComparer = value;
			}
		}

		public OLVColumn PrimarySort
		{
			get
			{
				return this.primarySort;
			}
			set
			{
				this.primarySort = value;
			}
		}

		public SortOrder PrimarySortOrder
		{
			get
			{
				return this.primarySortOrder;
			}
			set
			{
				this.primarySortOrder = value;
			}
		}

		public OLVColumn SecondarySort
		{
			get
			{
				return this.secondarySort;
			}
			set
			{
				this.secondarySort = value;
			}
		}

		public SortOrder SecondarySortOrder
		{
			get
			{
				return this.secondarySortOrder;
			}
			set
			{
				this.secondarySortOrder = value;
			}
		}

		public string TitleFormat
		{
			get
			{
				return this.titleFormat;
			}
			set
			{
				this.titleFormat = value;
			}
		}

		public string TitleSingularFormat
		{
			get
			{
				return this.titleSingularFormat;
			}
			set
			{
				this.titleSingularFormat = value;
			}
		}

		public bool SortItemsByPrimaryColumn
		{
			get
			{
				return this.sortItemsByPrimaryColumn;
			}
			set
			{
				this.sortItemsByPrimaryColumn = value;
			}
		}

		private ObjectListView listView;

		private OLVColumn groupByColumn;

		private SortOrder groupByOrder;

		private IComparer<OLVGroup> groupComparer;

		private IComparer<OLVListItem> itemComparer;

		private OLVColumn primarySort;

		private SortOrder primarySortOrder;

		private OLVColumn secondarySort;

		private SortOrder secondarySortOrder;

		private string titleFormat;

		private string titleSingularFormat;

		private bool sortItemsByPrimaryColumn;
	}
}
