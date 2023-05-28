using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public class VirtualObjectListView : ObjectListView
	{
		public VirtualObjectListView()
		{
			base.VirtualMode = true;
			base.CacheVirtualItems += this.HandleCacheVirtualItems;
			base.RetrieveVirtualItem += this.HandleRetrieveVirtualItem;
			base.SearchForVirtualItem += this.HandleSearchForVirtualItem;
			this.VirtualListDataSource = new VirtualListVersion1DataSource(this);
			this.PersistentCheckBoxes = true;
		}

		[Browsable(false)]
		public override bool CanShowGroups
		{
			get
			{
				return ObjectListView.IsVistaOrLater && this.GroupingStrategy != null;
			}
		}

		[DefaultValue(false)]
		[Category("Appearance")]
		[Description("Should the list view show checkboxes?")]
		public new bool CheckBoxes
		{
			get
			{
				return base.CheckBoxes;
			}
			set
			{
				try
				{
					base.CheckBoxes = value;
				}
				catch (InvalidOperationException)
				{
					base.StateImageList = null;
					base.VirtualMode = false;
					base.CheckBoxes = value;
					base.VirtualMode = true;
					this.ShowGroups = this.ShowGroups;
					this.BuildList(true);
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override IList CheckedObjects
		{
			get
			{
				if (!this.CheckBoxes)
				{
					return new ArrayList();
				}
				if (this.VirtualListDataSource == null)
				{
					return new ArrayList();
				}
				if (this.CheckStateGetter != null)
				{
					return base.CheckedObjects;
				}
				ArrayList arrayList = new ArrayList();
				foreach (KeyValuePair<object, CheckState> keyValuePair in base.CheckStateMap)
				{
					if (keyValuePair.Value == CheckState.Checked && (!this.CheckedObjectsMustStillExistInList || this.VirtualListDataSource.GetObjectIndex(keyValuePair.Key) >= 0))
					{
						arrayList.Add(keyValuePair.Key);
					}
				}
				return arrayList;
			}
			set
			{
				if (!this.CheckBoxes)
				{
					return;
				}
				Hashtable hashtable = new Hashtable(this.GetItemCount());
				if (value != null)
				{
					foreach (object key in value)
					{
						hashtable[key] = true;
					}
				}
				object[] array = new object[base.CheckStateMap.Count];
				base.CheckStateMap.Keys.CopyTo(array, 0);
				foreach (object obj in array)
				{
					if (!hashtable.Contains(obj))
					{
						this.SetObjectCheckedness(obj, CheckState.Unchecked);
					}
				}
				foreach (object modelObject in hashtable.Keys)
				{
					this.SetObjectCheckedness(modelObject, CheckState.Checked);
				}
			}
		}

		protected internal bool CheckedObjectsMustStillExistInList
		{
			get
			{
				return this.checkedObjectsMustStillExistInList;
			}
			set
			{
				this.checkedObjectsMustStillExistInList = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override IEnumerable FilteredObjects
		{
			get
			{
				VirtualObjectListView.<get_FilteredObjects>d__61 <get_FilteredObjects>d__ = new VirtualObjectListView.<get_FilteredObjects>d__61(-2);
				<get_FilteredObjects>d__.<>4__this = this;
				return <get_FilteredObjects>d__;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IVirtualGroups GroupingStrategy
		{
			get
			{
				return this.groupingStrategy;
			}
			set
			{
				this.groupingStrategy = value;
			}
		}

		public override bool IsFiltering
		{
			get
			{
				return base.IsFiltering && this.VirtualListDataSource is IFilterableDataSource;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override IEnumerable Objects
		{
			get
			{
				IEnumerable filteredObjects;
				try
				{
					if (this.IsFiltering)
					{
						((IFilterableDataSource)this.VirtualListDataSource).ApplyFilters(null, null);
					}
					filteredObjects = this.FilteredObjects;
				}
				finally
				{
					if (this.IsFiltering)
					{
						((IFilterableDataSource)this.VirtualListDataSource).ApplyFilters(this.ModelFilter, this.ListFilter);
					}
				}
				return filteredObjects;
			}
			set
			{
				base.Objects = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RowGetterDelegate RowGetter
		{
			get
			{
				return ((VirtualListVersion1DataSource)this.virtualListDataSource).RowGetter;
			}
			set
			{
				((VirtualListVersion1DataSource)this.virtualListDataSource).RowGetter = value;
			}
		}

		[Description("Should the list view show items in groups?")]
		[DefaultValue(true)]
		[Category("Appearance")]
		public override bool ShowGroups
		{
			get
			{
				return ObjectListView.IsVistaOrLater && this.showGroups;
			}
			set
			{
				this.showGroups = value;
				if (base.Created && !value)
				{
					this.DisableVirtualGroups();
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IVirtualListDataSource VirtualListDataSource
		{
			get
			{
				return this.virtualListDataSource;
			}
			set
			{
				this.virtualListDataSource = value;
				this.CustomSorter = delegate(OLVColumn column, SortOrder sortOrder)
				{
					this.ClearCachedInfo();
					this.virtualListDataSource.Sort(column, sortOrder);
				};
				this.BuildList(false);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected new virtual int VirtualListSize
		{
			get
			{
				return base.VirtualListSize;
			}
			set
			{
				if (value != this.VirtualListSize && value >= 0)
				{
					if (VirtualObjectListView.virtualListSizeFieldInfo == null)
					{
						VirtualObjectListView.virtualListSizeFieldInfo = typeof(ListView).GetField(VirtualObjectListView.getString_1(107315861), BindingFlags.Instance | BindingFlags.NonPublic);
					}
					VirtualObjectListView.virtualListSizeFieldInfo.SetValue(this, value);
					if (base.IsHandleCreated && !base.DesignMode)
					{
						NativeMethods.SetItemCount(this, value);
					}
					return;
				}
			}
		}

		public override int GetItemCount()
		{
			return this.VirtualListSize;
		}

		public override object GetModelObject(int index)
		{
			if (this.VirtualListDataSource != null && index >= 0 && index < this.GetItemCount())
			{
				return this.VirtualListDataSource.GetNthObject(index);
			}
			return null;
		}

		public override int IndexOf(object modelObject)
		{
			if (this.VirtualListDataSource != null && modelObject != null)
			{
				return this.VirtualListDataSource.GetObjectIndex(modelObject);
			}
			return -1;
		}

		public override OLVListItem ModelToItem(object modelObject)
		{
			if (this.VirtualListDataSource == null || modelObject == null)
			{
				return null;
			}
			int objectIndex = this.VirtualListDataSource.GetObjectIndex(modelObject);
			if (objectIndex < 0)
			{
				return null;
			}
			return this.GetItem(objectIndex);
		}

		public override void AddObjects(ICollection modelObjects)
		{
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			ItemsAddingEventArgs itemsAddingEventArgs = new ItemsAddingEventArgs(modelObjects);
			this.OnItemsAdding(itemsAddingEventArgs);
			if (itemsAddingEventArgs.Canceled)
			{
				return;
			}
			try
			{
				base.BeginUpdate();
				this.VirtualListDataSource.AddObjects(itemsAddingEventArgs.ObjectsToAdd);
				this.BuildList();
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public override void ClearObjects()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ClearObjects));
				return;
			}
			base.CheckStateMap.Clear();
			this.SetObjects(new ArrayList());
		}

		public virtual void EnsureNthGroupVisible(int groupIndex)
		{
			if (!this.ShowGroups)
			{
				return;
			}
			if (groupIndex > 0 && groupIndex < base.OLVGroups.Count)
			{
				OLVGroup olvgroup = base.OLVGroups[groupIndex - 1];
				int groupMember = this.GroupingStrategy.GetGroupMember(olvgroup, olvgroup.VirtualItemCount - 1);
				Rectangle itemRect = base.GetItemRect(groupMember);
				int dy = itemRect.Y + itemRect.Height / 2;
				NativeMethods.Scroll(this, 0, dy);
				return;
			}
			int dy2 = -NativeMethods.GetScrollPosition(this, false);
			NativeMethods.Scroll(this, 0, dy2);
		}

		public override void RefreshObjects(IList modelObjects)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.RefreshObjects(modelObjects);
				}));
				return;
			}
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			try
			{
				base.BeginUpdate();
				this.ClearCachedInfo();
				foreach (object obj in modelObjects)
				{
					int objectIndex = this.VirtualListDataSource.GetObjectIndex(obj);
					if (objectIndex >= 0)
					{
						this.VirtualListDataSource.UpdateObject(objectIndex, obj);
						base.RedrawItems(objectIndex, objectIndex, true);
					}
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public override void RefreshSelectedObjects()
		{
			foreach (object obj in base.SelectedIndices)
			{
				int num = (int)obj;
				base.RedrawItems(num, num, true);
			}
		}

		public override void RemoveObjects(ICollection modelObjects)
		{
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			ItemsRemovingEventArgs itemsRemovingEventArgs = new ItemsRemovingEventArgs(modelObjects);
			this.OnItemsRemoving(itemsRemovingEventArgs);
			if (itemsRemovingEventArgs.Canceled)
			{
				return;
			}
			try
			{
				base.BeginUpdate();
				this.VirtualListDataSource.RemoveObjects(itemsRemovingEventArgs.ObjectsToRemove);
				this.BuildList();
				base.UnsubscribeNotifications(itemsRemovingEventArgs.ObjectsToRemove);
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public override void SelectObject(object modelObject, bool setFocus)
		{
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			int objectIndex = this.VirtualListDataSource.GetObjectIndex(modelObject);
			if (objectIndex < 0 || objectIndex >= this.VirtualListSize)
			{
				return;
			}
			if (base.SelectedIndices.Count == 1 && base.SelectedIndices[0] == objectIndex)
			{
				return;
			}
			base.SelectedIndices.Clear();
			base.SelectedIndices.Add(objectIndex);
			if (setFocus)
			{
				this.SelectedItem.Focused = true;
			}
		}

		public override void SelectObjects(IList modelObjects)
		{
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			base.SelectedIndices.Clear();
			if (modelObjects == null)
			{
				return;
			}
			foreach (object model in modelObjects)
			{
				int objectIndex = this.VirtualListDataSource.GetObjectIndex(model);
				if (objectIndex >= 0 && objectIndex < this.VirtualListSize)
				{
					base.SelectedIndices.Add(objectIndex);
				}
			}
		}

		public override void SetObjects(IEnumerable collection, bool preserveState)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.SetObjects(collection, preserveState);
				}));
				return;
			}
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			ItemsChangingEventArgs itemsChangingEventArgs = new ItemsChangingEventArgs(null, collection);
			this.OnItemsChanging(itemsChangingEventArgs);
			if (itemsChangingEventArgs.Canceled)
			{
				return;
			}
			base.BeginUpdate();
			try
			{
				this.VirtualListDataSource.SetObjects(itemsChangingEventArgs.NewObjects);
				this.BuildList();
				this.UpdateNotificationSubscriptions(itemsChangingEventArgs.NewObjects);
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public override void BuildList(bool shouldPreserveSelection)
		{
			this.UpdateVirtualListSize();
			this.ClearCachedInfo();
			if (this.ShowGroups)
			{
				this.BuildGroups();
			}
			else
			{
				base.Sort();
			}
			base.Invalidate();
		}

		public virtual void ClearCachedInfo()
		{
			this.lastRetrieveVirtualItemIndex = -1;
		}

		protected override void CreateGroups(IEnumerable<OLVGroup> groups)
		{
			NativeMethods.ClearGroups(this);
			this.EnableVirtualGroups();
			foreach (OLVGroup olvgroup in groups)
			{
				olvgroup.InsertGroupNewStyle(this);
			}
		}

		protected void DisableVirtualGroups()
		{
			NativeMethods.ClearGroups(this);
			NativeMethods.SendMessage(base.Handle, 4253, 0, 0);
			NativeMethods.SendMessage(base.Handle, 4283, 0, 0);
		}

		protected void EnableVirtualGroups()
		{
			if (this.ownerDataCallbackImpl == null)
			{
				this.ownerDataCallbackImpl = new OwnerDataCallbackImpl(this);
			}
			IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(this.ownerDataCallbackImpl, typeof(IOwnerDataCallback));
			NativeMethods.SendMessage(base.Handle, 4283, comInterfaceForObject, 0);
			Marshal.Release(comInterfaceForObject);
			NativeMethods.SendMessage(base.Handle, 4253, 1, 0);
		}

		protected override CheckState? GetCheckState(object modelObject)
		{
			if (this.CheckStateGetter != null)
			{
				return base.GetCheckState(modelObject);
			}
			CheckState value = CheckState.Unchecked;
			if (modelObject != null)
			{
				base.CheckStateMap.TryGetValue(modelObject, out value);
			}
			return new CheckState?(value);
		}

		public override int GetDisplayOrderOfItemIndex(int itemIndex)
		{
			if (!this.ShowGroups)
			{
				return itemIndex;
			}
			int group = this.GroupingStrategy.GetGroup(itemIndex);
			int num = 0;
			for (int i = 0; i < group - 1; i++)
			{
				num += base.OLVGroups[i].VirtualItemCount;
			}
			return num + this.GroupingStrategy.GetIndexWithinGroup(base.OLVGroups[group], itemIndex);
		}

		public override OLVListItem GetLastItemInDisplayOrder()
		{
			if (!this.ShowGroups)
			{
				return base.GetLastItemInDisplayOrder();
			}
			if (base.OLVGroups.Count > 0)
			{
				OLVGroup olvgroup = base.OLVGroups[base.OLVGroups.Count - 1];
				if (olvgroup.VirtualItemCount > 0)
				{
					return this.GetItem(this.GroupingStrategy.GetGroupMember(olvgroup, olvgroup.VirtualItemCount - 1));
				}
			}
			return null;
		}

		public override OLVListItem GetNthItemInDisplayOrder(int n)
		{
			if (!this.ShowGroups)
			{
				return this.GetItem(n);
			}
			foreach (OLVGroup olvgroup in base.OLVGroups)
			{
				if (n < olvgroup.VirtualItemCount)
				{
					return this.GetItem(this.GroupingStrategy.GetGroupMember(olvgroup, n));
				}
				n -= olvgroup.VirtualItemCount;
			}
			return null;
		}

		public override OLVListItem GetNextItem(OLVListItem itemToFind)
		{
			if (!this.ShowGroups)
			{
				return base.GetNextItem(itemToFind);
			}
			if (base.OLVGroups == null || base.OLVGroups.Count == 0)
			{
				return null;
			}
			if (itemToFind == null)
			{
				return this.GetItem(this.GroupingStrategy.GetGroupMember(base.OLVGroups[0], 0));
			}
			int group = this.GroupingStrategy.GetGroup(itemToFind.Index);
			int indexWithinGroup = this.GroupingStrategy.GetIndexWithinGroup(base.OLVGroups[group], itemToFind.Index);
			if (indexWithinGroup < base.OLVGroups[group].VirtualItemCount - 1)
			{
				return this.GetItem(this.GroupingStrategy.GetGroupMember(base.OLVGroups[group], indexWithinGroup + 1));
			}
			if (group < base.OLVGroups.Count - 1)
			{
				return this.GetItem(this.GroupingStrategy.GetGroupMember(base.OLVGroups[group + 1], 0));
			}
			return null;
		}

		public override OLVListItem GetPreviousItem(OLVListItem itemToFind)
		{
			if (!this.ShowGroups)
			{
				return base.GetPreviousItem(itemToFind);
			}
			if (base.OLVGroups == null || base.OLVGroups.Count == 0)
			{
				return null;
			}
			if (itemToFind == null)
			{
				OLVGroup olvgroup = base.OLVGroups[base.OLVGroups.Count - 1];
				return this.GetItem(this.GroupingStrategy.GetGroupMember(olvgroup, olvgroup.VirtualItemCount - 1));
			}
			int group = this.GroupingStrategy.GetGroup(itemToFind.Index);
			int indexWithinGroup = this.GroupingStrategy.GetIndexWithinGroup(base.OLVGroups[group], itemToFind.Index);
			if (indexWithinGroup > 0)
			{
				return this.GetItem(this.GroupingStrategy.GetGroupMember(base.OLVGroups[group], indexWithinGroup - 1));
			}
			if (group > 0)
			{
				OLVGroup olvgroup2 = base.OLVGroups[group - 1];
				return this.GetItem(this.GroupingStrategy.GetGroupMember(olvgroup2, olvgroup2.VirtualItemCount - 1));
			}
			return null;
		}

		protected override IList<OLVGroup> MakeGroups(GroupingParameters parms)
		{
			if (this.GroupingStrategy == null)
			{
				return new List<OLVGroup>();
			}
			return this.GroupingStrategy.GetGroups(parms);
		}

		public virtual OLVListItem MakeListViewItem(int itemIndex)
		{
			OLVListItem olvlistItem = new OLVListItem(this.GetModelObject(itemIndex));
			this.FillInValues(olvlistItem, olvlistItem.RowObject);
			this.PostProcessOneRow(itemIndex, this.GetDisplayOrderOfItemIndex(itemIndex), olvlistItem);
			if (this.HotRowIndex == itemIndex)
			{
				this.UpdateHotRow(olvlistItem);
			}
			return olvlistItem;
		}

		protected override void PostProcessRows()
		{
		}

		protected override CheckState PutCheckState(object modelObject, CheckState state)
		{
			state = base.PutCheckState(modelObject, state);
			base.CheckStateMap[modelObject] = state;
			return state;
		}

		public override void RefreshItem(OLVListItem olvi)
		{
			this.ClearCachedInfo();
			base.RedrawItems(olvi.Index, olvi.Index, false);
		}

		protected virtual void SetVirtualListSize(int newSize)
		{
			if (newSize >= 0)
			{
				if (this.VirtualListSize != newSize)
				{
					int virtualListSize = this.VirtualListSize;
					this.ClearCachedInfo();
					try
					{
						if (newSize == 0 && this.TopItemIndex > 0)
						{
							this.TopItemIndex = 0;
						}
					}
					catch (Exception)
					{
					}
					try
					{
						this.VirtualListSize = newSize;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					catch (NullReferenceException)
					{
					}
					this.OnItemsChanged(new ItemsChangedEventArgs(virtualListSize, this.VirtualListSize));
					return;
				}
			}
		}

		protected override void TakeOwnershipOfObjects()
		{
		}

		protected override void UpdateFiltering()
		{
			IFilterableDataSource filterableDataSource = this.VirtualListDataSource as IFilterableDataSource;
			if (filterableDataSource == null)
			{
				return;
			}
			base.BeginUpdate();
			try
			{
				int virtualListSize = this.VirtualListSize;
				filterableDataSource.ApplyFilters(this.ModelFilter, this.ListFilter);
				this.BuildList();
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public virtual void UpdateVirtualListSize()
		{
			if (this.VirtualListDataSource != null)
			{
				this.SetVirtualListSize(this.VirtualListDataSource.GetObjectCount());
			}
		}

		protected virtual void HandleCacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
		{
			if (this.VirtualListDataSource != null)
			{
				this.VirtualListDataSource.PrepareCache(e.StartIndex, e.EndIndex);
			}
		}

		protected virtual void HandleRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if (this.lastRetrieveVirtualItemIndex != e.ItemIndex)
			{
				this.lastRetrieveVirtualItemIndex = e.ItemIndex;
				this.lastRetrieveVirtualItem = this.MakeListViewItem(e.ItemIndex);
			}
			e.Item = this.lastRetrieveVirtualItem;
		}

		protected virtual void HandleSearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
		{
			if (this.VirtualListDataSource == null)
			{
				return;
			}
			int startSearchFrom = Math.Min(e.StartIndex, this.VirtualListDataSource.GetObjectCount() - 1);
			BeforeSearchingEventArgs beforeSearchingEventArgs = new BeforeSearchingEventArgs(e.Text, startSearchFrom);
			this.OnBeforeSearching(beforeSearchingEventArgs);
			if (beforeSearchingEventArgs.Canceled)
			{
				return;
			}
			int num = this.FindMatchingRow(beforeSearchingEventArgs.StringToFind, beforeSearchingEventArgs.StartSearchFrom, e.Direction);
			AfterSearchingEventArgs e2 = new AfterSearchingEventArgs(beforeSearchingEventArgs.StringToFind, num);
			this.OnAfterSearching(e2);
			if (num != -1)
			{
				e.Index = num;
			}
		}

		protected override int FindMatchInRange(string text, int first, int last, OLVColumn column)
		{
			return this.VirtualListDataSource.SearchText(text, first, last, column);
		}

		static VirtualObjectListView()
		{
			Strings.CreateGetStringDelegate(typeof(VirtualObjectListView));
		}

		private bool checkedObjectsMustStillExistInList = true;

		private IVirtualGroups groupingStrategy;

		private bool showGroups;

		private IVirtualListDataSource virtualListDataSource;

		private static FieldInfo virtualListSizeFieldInfo;

		private OwnerDataCallbackImpl ownerDataCallbackImpl;

		private OLVListItem lastRetrieveVirtualItem;

		private int lastRetrieveVirtualItemIndex = -1;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
