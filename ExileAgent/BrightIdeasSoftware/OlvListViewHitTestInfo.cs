using System;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class OlvListViewHitTestInfo
	{
		public OlvListViewHitTestInfo(OLVListItem olvListItem, OLVListSubItem subItem, int flags, OLVGroup group)
		{
			this.item = olvListItem;
			this.subItem = subItem;
			this.location = OlvListViewHitTestInfo.ConvertNativeFlagsToDotNetLocation(olvListItem, flags);
			this.HitTestLocationEx = (HitTestLocationEx)flags;
			this.Group = group;
			ListViewHitTestLocations listViewHitTestLocations = this.location;
			switch (listViewHitTestLocations)
			{
			case ListViewHitTestLocations.Image:
				this.HitTestLocation = HitTestLocation.Image;
				return;
			case ListViewHitTestLocations.None | ListViewHitTestLocations.Image:
				break;
			case ListViewHitTestLocations.Label:
				this.HitTestLocation = HitTestLocation.Text;
				return;
			default:
				if (listViewHitTestLocations == ListViewHitTestLocations.StateImage)
				{
					this.HitTestLocation = HitTestLocation.CheckBox;
					return;
				}
				break;
			}
			if ((this.HitTestLocationEx & HitTestLocationEx.LVHT_EX_GROUP_COLLAPSE) == HitTestLocationEx.LVHT_EX_GROUP_COLLAPSE)
			{
				this.HitTestLocation = HitTestLocation.GroupExpander;
				return;
			}
			if ((this.HitTestLocationEx & HitTestLocationEx.LVHT_EX_GROUP_MINUS_FOOTER_AND_BKGRD) != (HitTestLocationEx)0)
			{
				this.HitTestLocation = HitTestLocation.Group;
				return;
			}
			this.HitTestLocation = HitTestLocation.Nothing;
		}

		private static ListViewHitTestLocations ConvertNativeFlagsToDotNetLocation(OLVListItem hitItem, int flags)
		{
			if ((8 & flags) == 8)
			{
				return (ListViewHitTestLocations)((247 & flags) | ((hitItem == null) ? 256 : 512));
			}
			return (ListViewHitTestLocations)(flags & 65535);
		}

		public OLVListItem Item
		{
			get
			{
				return this.item;
			}
			internal set
			{
				this.item = value;
			}
		}

		public OLVListSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
			internal set
			{
				this.subItem = value;
			}
		}

		public ListViewHitTestLocations Location
		{
			get
			{
				return this.location;
			}
			internal set
			{
				this.location = value;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				if (this.Item != null)
				{
					return (ObjectListView)this.Item.ListView;
				}
				return null;
			}
		}

		public object RowObject
		{
			get
			{
				if (this.Item != null)
				{
					return this.Item.RowObject;
				}
				return null;
			}
		}

		public int RowIndex
		{
			get
			{
				if (this.Item != null)
				{
					return this.Item.Index;
				}
				return -1;
			}
		}

		public int ColumnIndex
		{
			get
			{
				if (this.Item != null && this.SubItem != null)
				{
					return this.Item.SubItems.IndexOf(this.SubItem);
				}
				return -1;
			}
		}

		public OLVColumn Column
		{
			get
			{
				int columnIndex = this.ColumnIndex;
				if (columnIndex >= 0)
				{
					return this.ListView.GetColumn(columnIndex);
				}
				return null;
			}
		}

		public override string ToString()
		{
			return string.Format(OlvListViewHitTestInfo.getString_0(107315315), new object[]
			{
				this.HitTestLocation,
				this.HitTestLocationEx,
				this.item,
				this.subItem,
				this.location,
				this.Group
			});
		}

		static OlvListViewHitTestInfo()
		{
			Strings.CreateGetStringDelegate(typeof(OlvListViewHitTestInfo));
		}

		public HitTestLocation HitTestLocation;

		public HitTestLocationEx HitTestLocationEx;

		public OLVGroup Group;

		public object UserData;

		private OLVListItem item;

		private OLVListSubItem subItem;

		private ListViewHitTestLocations location;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
