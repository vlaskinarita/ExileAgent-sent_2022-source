using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class OLVGroup
	{
		public OLVGroup() : this(OLVGroup.getString_0(107314534))
		{
		}

		public OLVGroup(string header)
		{
			this.Header = header;
			this.Id = OLVGroup.nextId++;
			this.TitleImage = -1;
			this.ExtendedImage = -1;
		}

		public string BottomDescription
		{
			get
			{
				return this.bottomDescription;
			}
			set
			{
				this.bottomDescription = value;
			}
		}

		public bool Collapsed
		{
			get
			{
				return this.GetOneState(GroupState.LVGS_COLLAPSED);
			}
			set
			{
				this.SetOneState(value, GroupState.LVGS_COLLAPSED);
			}
		}

		public bool Collapsible
		{
			get
			{
				return this.GetOneState(GroupState.LVGS_COLLAPSIBLE);
			}
			set
			{
				this.SetOneState(value, GroupState.LVGS_COLLAPSIBLE);
			}
		}

		public IList Contents
		{
			get
			{
				return this.contents;
			}
			set
			{
				this.contents = value;
			}
		}

		public bool Created
		{
			get
			{
				return this.ListView != null;
			}
		}

		public object ExtendedImage
		{
			get
			{
				return this.extendedImage;
			}
			set
			{
				this.extendedImage = value;
			}
		}

		public string Footer
		{
			get
			{
				return this.footer;
			}
			set
			{
				this.footer = value;
			}
		}

		public int GroupId
		{
			get
			{
				if (this.ListViewGroup == null)
				{
					return this.Id;
				}
				if (OLVGroup.groupIdPropInfo == null)
				{
					OLVGroup.groupIdPropInfo = typeof(ListViewGroup).GetProperty(OLVGroup.getString_0(107314505), BindingFlags.Instance | BindingFlags.NonPublic);
				}
				int? num = OLVGroup.groupIdPropInfo.GetValue(this.ListViewGroup, null) as int?;
				if (num == null)
				{
					return -1;
				}
				return num.Value;
			}
		}

		public string Header
		{
			get
			{
				return this.header;
			}
			set
			{
				this.header = value;
			}
		}

		public HorizontalAlignment HeaderAlignment
		{
			get
			{
				return this.headerAlignment;
			}
			set
			{
				this.headerAlignment = value;
			}
		}

		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public IList<OLVListItem> Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		public object Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			protected set
			{
				this.listView = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public bool Focused
		{
			get
			{
				return this.GetOneState(GroupState.LVGS_FOCUSED);
			}
			set
			{
				this.SetOneState(value, GroupState.LVGS_FOCUSED);
			}
		}

		public bool Selected
		{
			get
			{
				return this.GetOneState(GroupState.LVGS_SELECTED);
			}
			set
			{
				this.SetOneState(value, GroupState.LVGS_SELECTED);
			}
		}

		public string SubsetTitle
		{
			get
			{
				return this.subsetTitle;
			}
			set
			{
				this.subsetTitle = value;
			}
		}

		public string Subtitle
		{
			get
			{
				return this.subtitle;
			}
			set
			{
				this.subtitle = value;
			}
		}

		public IComparable SortValue
		{
			get
			{
				return this.sortValue;
			}
			set
			{
				this.sortValue = value;
			}
		}

		public GroupState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		public GroupState StateMask
		{
			get
			{
				return this.stateMask;
			}
			set
			{
				this.stateMask = value;
			}
		}

		public bool Subseted
		{
			get
			{
				return this.GetOneState(GroupState.LVGS_SUBSETED);
			}
			set
			{
				this.SetOneState(value, GroupState.LVGS_SUBSETED);
			}
		}

		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		public string Task
		{
			get
			{
				return this.task;
			}
			set
			{
				this.task = value;
			}
		}

		public object TitleImage
		{
			get
			{
				return this.titleImage;
			}
			set
			{
				this.titleImage = value;
			}
		}

		public string TopDescription
		{
			get
			{
				return this.topDescription;
			}
			set
			{
				this.topDescription = value;
			}
		}

		public int VirtualItemCount
		{
			get
			{
				return this.virtualItemCount;
			}
			set
			{
				this.virtualItemCount = value;
			}
		}

		protected ListViewGroup ListViewGroup
		{
			get
			{
				return this.listViewGroup;
			}
			set
			{
				this.listViewGroup = value;
			}
		}

		public int GetImageIndex(object imageSelector)
		{
			if (imageSelector == null || this.ListView == null || this.ListView.GroupImageList == null)
			{
				return -1;
			}
			if (imageSelector is int)
			{
				return (int)imageSelector;
			}
			string text = imageSelector as string;
			if (text != null)
			{
				return this.ListView.GroupImageList.Images.IndexOfKey(text);
			}
			return -1;
		}

		public override string ToString()
		{
			return this.Header;
		}

		public void InsertGroupNewStyle(ObjectListView olv)
		{
			this.ListView = olv;
			NativeMethods.InsertGroup(olv, this.AsNativeGroup(true));
		}

		public void InsertGroupOldStyle(ObjectListView olv)
		{
			this.ListView = olv;
			if (this.ListViewGroup == null)
			{
				this.ListViewGroup = new ListViewGroup();
			}
			this.ListViewGroup.Header = this.Header;
			this.ListViewGroup.HeaderAlignment = this.HeaderAlignment;
			this.ListViewGroup.Name = this.Name;
			this.ListViewGroup.Tag = this;
			olv.Groups.Add(this.ListViewGroup);
			NativeMethods.SetGroupInfo(olv, this.GroupId, this.AsNativeGroup(false));
		}

		public void SetItemsOldStyle()
		{
			List<OLVListItem> list = this.Items as List<OLVListItem>;
			if (list == null)
			{
				using (IEnumerator<OLVListItem> enumerator = this.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OLVListItem value = enumerator.Current;
						this.ListViewGroup.Items.Add(value);
					}
					return;
				}
			}
			this.ListViewGroup.Items.AddRange(list.ToArray());
		}

		internal NativeMethods.LVGROUP2 AsNativeGroup(bool withId)
		{
			NativeMethods.LVGROUP2 result = default(NativeMethods.LVGROUP2);
			result.cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.LVGROUP2));
			result.mask = 13U;
			result.pszHeader = this.Header;
			result.uAlign = (uint)this.HeaderAlignment;
			result.stateMask = (uint)this.StateMask;
			result.state = (uint)this.State;
			if (withId)
			{
				result.iGroupId = this.GroupId;
				result.mask ^= 16U;
			}
			if (!string.IsNullOrEmpty(this.Footer))
			{
				result.pszFooter = this.Footer;
				result.mask ^= 2U;
			}
			if (!string.IsNullOrEmpty(this.Subtitle))
			{
				result.pszSubtitle = this.Subtitle;
				result.mask ^= 256U;
			}
			if (!string.IsNullOrEmpty(this.Task))
			{
				result.pszTask = this.Task;
				result.mask ^= 512U;
			}
			if (!string.IsNullOrEmpty(this.TopDescription))
			{
				result.pszDescriptionTop = this.TopDescription;
				result.mask ^= 1024U;
			}
			if (!string.IsNullOrEmpty(this.BottomDescription))
			{
				result.pszDescriptionBottom = this.BottomDescription;
				result.mask ^= 2048U;
			}
			int imageIndex = this.GetImageIndex(this.TitleImage);
			if (imageIndex >= 0)
			{
				result.iTitleImage = imageIndex;
				result.mask ^= 4096U;
			}
			imageIndex = this.GetImageIndex(this.ExtendedImage);
			if (imageIndex >= 0)
			{
				result.iExtendedImage = imageIndex;
				result.mask ^= 8192U;
			}
			if (!string.IsNullOrEmpty(this.SubsetTitle))
			{
				result.pszSubsetTitle = this.SubsetTitle;
				result.mask ^= 32768U;
			}
			if (this.VirtualItemCount > 0)
			{
				result.cItems = this.VirtualItemCount;
				result.mask ^= 16384U;
			}
			return result;
		}

		private bool GetOneState(GroupState mask)
		{
			if (this.Created)
			{
				this.State = this.GetState();
			}
			return (this.State & mask) == mask;
		}

		protected GroupState GetState()
		{
			return NativeMethods.GetGroupState(this.ListView, this.GroupId, GroupState.LVGS_ALL);
		}

		protected int SetState(GroupState newState, GroupState mask)
		{
			NativeMethods.LVGROUP2 group = default(NativeMethods.LVGROUP2);
			group.cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.LVGROUP2));
			group.mask = 4U;
			group.state = (uint)newState;
			group.stateMask = (uint)mask;
			return NativeMethods.SetGroupInfo(this.ListView, this.GroupId, group);
		}

		private void SetOneState(bool value, GroupState mask)
		{
			this.StateMask ^= mask;
			if (value)
			{
				this.State ^= mask;
			}
			else
			{
				this.State &= ~mask;
			}
			if (this.Created)
			{
				this.SetState(this.State, mask);
			}
		}

		static OLVGroup()
		{
			Strings.CreateGetStringDelegate(typeof(OLVGroup));
		}

		private static int nextId;

		private string bottomDescription;

		private IList contents;

		private object extendedImage;

		private string footer;

		private static PropertyInfo groupIdPropInfo;

		private string header;

		private HorizontalAlignment headerAlignment;

		private int id;

		private IList<OLVListItem> items = new List<OLVListItem>();

		private object key;

		private ObjectListView listView;

		private string name;

		private string subsetTitle;

		private string subtitle;

		private IComparable sortValue;

		private GroupState state;

		private GroupState stateMask;

		private object tag;

		private string task;

		private object titleImage;

		private string topDescription;

		private int virtualItemCount;

		private ListViewGroup listViewGroup;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
