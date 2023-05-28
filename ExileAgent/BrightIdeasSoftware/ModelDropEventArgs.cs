using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class ModelDropEventArgs : OlvDropEventArgs
	{
		public IList SourceModels
		{
			get
			{
				return this.dragModels;
			}
			internal set
			{
				this.dragModels = value;
				TreeListView treeListView = this.SourceListView as TreeListView;
				if (treeListView != null)
				{
					foreach (object model in this.SourceModels)
					{
						object parent = treeListView.GetParent(model);
						if (!this.toBeRefreshed.Contains(parent))
						{
							this.toBeRefreshed.Add(parent);
						}
					}
				}
			}
		}

		public ObjectListView SourceListView
		{
			get
			{
				return this.sourceListView;
			}
			internal set
			{
				this.sourceListView = value;
			}
		}

		public object TargetModel
		{
			get
			{
				return this.targetModel;
			}
			internal set
			{
				this.targetModel = value;
			}
		}

		public void RefreshObjects()
		{
			this.toBeRefreshed.AddRange(this.SourceModels);
			TreeListView treeListView = this.SourceListView as TreeListView;
			if (treeListView == null)
			{
				this.SourceListView.RefreshObjects(this.toBeRefreshed);
			}
			else
			{
				treeListView.RebuildAll(true);
			}
			TreeListView treeListView2 = base.ListView as TreeListView;
			if (treeListView2 == null)
			{
				base.ListView.RefreshObject(this.TargetModel);
				return;
			}
			treeListView2.RebuildAll(true);
		}

		private IList dragModels;

		private ArrayList toBeRefreshed = new ArrayList();

		private ObjectListView sourceListView;

		private object targetModel;
	}
}
