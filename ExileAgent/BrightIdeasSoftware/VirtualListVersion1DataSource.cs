using System;

namespace BrightIdeasSoftware
{
	public sealed class VirtualListVersion1DataSource : AbstractVirtualListDataSource
	{
		public VirtualListVersion1DataSource(VirtualObjectListView listView) : base(listView)
		{
		}

		public RowGetterDelegate RowGetter
		{
			get
			{
				return this.rowGetter;
			}
			set
			{
				this.rowGetter = value;
			}
		}

		public override object GetNthObject(int n)
		{
			if (this.RowGetter == null)
			{
				return null;
			}
			return this.RowGetter(n);
		}

		public override int SearchText(string value, int first, int last, OLVColumn column)
		{
			return AbstractVirtualListDataSource.DefaultSearchText(value, first, last, column, this);
		}

		private RowGetterDelegate rowGetter;
	}
}
