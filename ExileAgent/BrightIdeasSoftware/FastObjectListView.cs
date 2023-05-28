using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class FastObjectListView : VirtualObjectListView
	{
		public FastObjectListView()
		{
			this.VirtualListDataSource = new FastObjectListDataSource(this);
			base.GroupingStrategy = new FastListGroupingStrategy();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override IEnumerable FilteredObjects
		{
			get
			{
				return ((FastObjectListDataSource)this.VirtualListDataSource).FilteredObjectList;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override IEnumerable Objects
		{
			get
			{
				return ((FastObjectListDataSource)this.VirtualListDataSource).ObjectList;
			}
			set
			{
				base.Objects = value;
			}
		}

		public override void Unsort()
		{
			this.ShowGroups = false;
			this.PrimarySortColumn = null;
			this.PrimarySortOrder = SortOrder.None;
			this.SetObjects(this.Objects);
		}
	}
}
