using System;
using System.Collections;
using System.ComponentModel;

namespace BrightIdeasSoftware
{
	public sealed class TreeDataSourceAdapter : DataSourceAdapter
	{
		public TreeDataSourceAdapter(DataTreeListView tlv) : base(tlv)
		{
			this.treeListView = tlv;
			TreeListView treeListView = this.treeListView;
			TreeListView.CanExpandGetterDelegate canExpandGetter = (object model) => this.CalculateHasChildren(model);
			treeListView.CanExpandGetter = canExpandGetter;
			TreeListView treeListView2 = this.treeListView;
			TreeListView.ChildrenGetterDelegate childrenGetter = (object model) => this.CalculateChildren(model);
			treeListView2.ChildrenGetter = childrenGetter;
		}

		public string KeyAspectName
		{
			get
			{
				return this.keyAspectName;
			}
			set
			{
				if (this.keyAspectName == value)
				{
					return;
				}
				this.keyAspectName = value;
				this.keyMunger = new Munger(this.KeyAspectName);
				this.InitializeDataSource();
			}
		}

		public string ParentKeyAspectName
		{
			get
			{
				return this.parentKeyAspectName;
			}
			set
			{
				if (this.parentKeyAspectName == value)
				{
					return;
				}
				this.parentKeyAspectName = value;
				this.parentKeyMunger = new Munger(this.ParentKeyAspectName);
				this.InitializeDataSource();
			}
		}

		public object RootKeyValue
		{
			get
			{
				return this.rootKeyValue;
			}
			set
			{
				if (object.Equals(this.rootKeyValue, value))
				{
					return;
				}
				this.rootKeyValue = value;
				this.InitializeDataSource();
			}
		}

		public bool ShowKeyColumns
		{
			get
			{
				return this.showKeyColumns;
			}
			set
			{
				this.showKeyColumns = value;
			}
		}

		protected DataTreeListView TreeListView
		{
			get
			{
				return this.treeListView;
			}
		}

		protected override void InitializeDataSource()
		{
			base.InitializeDataSource();
			this.TreeListView.RebuildAll(true);
		}

		protected override void SetListContents()
		{
			this.TreeListView.Roots = this.CalculateRoots();
		}

		protected override bool ShouldCreateColumn(PropertyDescriptor property)
		{
			return (this.ShowKeyColumns || (!(property.Name == this.KeyAspectName) && !(property.Name == this.ParentKeyAspectName))) && base.ShouldCreateColumn(property);
		}

		protected override void HandleListChangedItemChanged(ListChangedEventArgs e)
		{
			if (e.PropertyDescriptor != null && (e.PropertyDescriptor.Name == this.KeyAspectName || e.PropertyDescriptor.Name == this.ParentKeyAspectName))
			{
				this.InitializeDataSource();
				return;
			}
			base.HandleListChangedItemChanged(e);
		}

		protected override void ChangePosition(int index)
		{
			object model = base.CurrencyManager.List[index];
			object obj = this.CalculateParent(model);
			while (obj != null && !this.TreeListView.IsExpanded(obj))
			{
				this.TreeListView.Expand(obj);
				obj = this.CalculateParent(obj);
			}
			base.ChangePosition(index);
		}

		private IEnumerable CalculateRoots()
		{
			TreeDataSourceAdapter.<CalculateRoots>d__d <CalculateRoots>d__d = new TreeDataSourceAdapter.<CalculateRoots>d__d(-2);
			<CalculateRoots>d__d.<>4__this = this;
			return <CalculateRoots>d__d;
		}

		private bool CalculateHasChildren(object model)
		{
			object keyValue = this.GetKeyValue(model);
			if (keyValue == null)
			{
				return false;
			}
			foreach (object model2 in base.CurrencyManager.List)
			{
				object parentValue = this.GetParentValue(model2);
				if (object.Equals(keyValue, parentValue))
				{
					return true;
				}
			}
			return false;
		}

		private IEnumerable CalculateChildren(object model)
		{
			TreeDataSourceAdapter.<CalculateChildren>d__15 <CalculateChildren>d__ = new TreeDataSourceAdapter.<CalculateChildren>d__15(-2);
			<CalculateChildren>d__.<>4__this = this;
			<CalculateChildren>d__.<>3__model = model;
			return <CalculateChildren>d__;
		}

		private object CalculateParent(object model)
		{
			object parentValue = this.GetParentValue(model);
			if (parentValue == null)
			{
				return null;
			}
			foreach (object obj in base.CurrencyManager.List)
			{
				object keyValue = this.GetKeyValue(obj);
				if (object.Equals(parentValue, keyValue))
				{
					return obj;
				}
			}
			return null;
		}

		private object GetKeyValue(object model)
		{
			if (this.keyMunger != null)
			{
				return this.keyMunger.GetValue(model);
			}
			return null;
		}

		private object GetParentValue(object model)
		{
			if (this.parentKeyMunger != null)
			{
				return this.parentKeyMunger.GetValue(model);
			}
			return null;
		}

		private string keyAspectName;

		private string parentKeyAspectName;

		private object rootKeyValue;

		private bool showKeyColumns = true;

		private readonly DataTreeListView treeListView;

		private Munger keyMunger;

		private Munger parentKeyMunger;
	}
}
