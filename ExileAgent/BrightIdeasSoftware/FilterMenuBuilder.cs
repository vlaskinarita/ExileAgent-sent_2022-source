using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware.Properties;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class FilterMenuBuilder
	{
		public bool TreatNullAsDataValue
		{
			get
			{
				return this.treatNullAsDataValue;
			}
			set
			{
				this.treatNullAsDataValue = value;
			}
		}

		public int MaxObjectsToConsider
		{
			get
			{
				return this.maxObjectsToConsider;
			}
			set
			{
				this.maxObjectsToConsider = value;
			}
		}

		public ToolStripDropDown MakeFilterMenu(ToolStripDropDown strip, ObjectListView listView, OLVColumn column)
		{
			if (strip == null)
			{
				throw new ArgumentNullException(FilterMenuBuilder.getString_0(107314925));
			}
			if (listView == null)
			{
				throw new ArgumentNullException(FilterMenuBuilder.getString_0(107314884));
			}
			if (column == null)
			{
				throw new ArgumentNullException(FilterMenuBuilder.getString_0(107314903));
			}
			if (column.UseFiltering && column.ClusteringStrategy != null && listView.Objects != null)
			{
				List<ICluster> list = this.Cluster(column.ClusteringStrategy, listView, column);
				if (list.Count > 0)
				{
					this.SortClusters(column.ClusteringStrategy, list);
					strip.Items.Add(this.CreateFilteringMenuItem(column, list));
				}
				return strip;
			}
			return strip;
		}

		protected List<ICluster> Cluster(IClusteringStrategy strategy, ObjectListView listView, OLVColumn column)
		{
			NullableDictionary<object, ICluster> nullableDictionary = new NullableDictionary<object, ICluster>();
			int num = 0;
			foreach (object model in listView.ObjectsForClustering)
			{
				this.ClusterOneModel(strategy, nullableDictionary, model);
				if (num++ > this.MaxObjectsToConsider)
				{
					break;
				}
			}
			foreach (ICluster cluster in nullableDictionary.Values)
			{
				cluster.DisplayLabel = strategy.GetClusterDisplayLabel(cluster);
			}
			return new List<ICluster>(nullableDictionary.Values);
		}

		private void ClusterOneModel(IClusteringStrategy strategy, NullableDictionary<object, ICluster> map, object model)
		{
			object clusterKey = strategy.GetClusterKey(model);
			IEnumerable enumerable = clusterKey as IEnumerable;
			if (clusterKey is string || enumerable == null)
			{
				enumerable = new object[]
				{
					clusterKey
				};
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in enumerable)
			{
				if (obj != null)
				{
					if (obj != DBNull.Value)
					{
						arrayList.Add(obj);
						continue;
					}
				}
				if (this.TreatNullAsDataValue)
				{
					arrayList.Add(null);
				}
			}
			foreach (object obj2 in arrayList)
			{
				if (map.ContainsKey(obj2))
				{
					map[obj2].Count++;
				}
				else
				{
					map[obj2] = strategy.CreateCluster(obj2);
				}
			}
		}

		protected void SortClusters(IClusteringStrategy strategy, List<ICluster> clusters)
		{
			clusters.Sort();
		}

		protected ToolStripMenuItem CreateFilteringMenuItem(OLVColumn column, List<ICluster> clusters)
		{
			ToolStripCheckedListBox checkedList = new ToolStripCheckedListBox();
			checkedList.Tag = column;
			foreach (ICluster cluster in clusters)
			{
				checkedList.AddItem(cluster, column.ValuesChosenForFiltering.Contains(cluster.ClusterKey));
			}
			if (!string.IsNullOrEmpty(FilterMenuBuilder.SELECT_ALL_LABEL))
			{
				int count = checkedList.CheckedItems.Count;
				if (count == 0)
				{
					checkedList.AddItem(FilterMenuBuilder.SELECT_ALL_LABEL, CheckState.Unchecked);
				}
				else
				{
					checkedList.AddItem(FilterMenuBuilder.SELECT_ALL_LABEL, (count == clusters.Count) ? CheckState.Checked : CheckState.Indeterminate);
				}
			}
			checkedList.ItemCheck += this.HandleItemCheckedWrapped;
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(FilterMenuBuilder.CLEAR_ALL_FILTERS_LABEL, FilterMenuBuilder.ClearFilteringImage, delegate(object sender, EventArgs e)
			{
				this.ClearAllFilters(column);
			});
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(FilterMenuBuilder.APPLY_LABEL, FilterMenuBuilder.FilteringImage, delegate(object sender, EventArgs e)
			{
				this.EnactFilter(checkedList, column);
			});
			return new ToolStripMenuItem(FilterMenuBuilder.FILTERING_LABEL, null, new ToolStripItem[]
			{
				toolStripMenuItem,
				new ToolStripSeparator(),
				checkedList,
				toolStripMenuItem2
			});
		}

		private void HandleItemCheckedWrapped(object sender, ItemCheckEventArgs e)
		{
			if (this.alreadyInHandleItemChecked)
			{
				return;
			}
			try
			{
				this.alreadyInHandleItemChecked = true;
				this.HandleItemChecked(sender, e);
			}
			finally
			{
				this.alreadyInHandleItemChecked = false;
			}
		}

		protected void HandleItemChecked(object sender, ItemCheckEventArgs e)
		{
			ToolStripCheckedListBox toolStripCheckedListBox = sender as ToolStripCheckedListBox;
			if (toolStripCheckedListBox == null)
			{
				return;
			}
			OLVColumn olvcolumn = toolStripCheckedListBox.Tag as OLVColumn;
			if (olvcolumn == null)
			{
				return;
			}
			if (!(olvcolumn.ListView is ObjectListView))
			{
				return;
			}
			int num = toolStripCheckedListBox.Items.IndexOf(FilterMenuBuilder.SELECT_ALL_LABEL);
			if (num >= 0)
			{
				this.HandleSelectAllItem(e, toolStripCheckedListBox, num);
			}
		}

		protected void HandleSelectAllItem(ItemCheckEventArgs e, ToolStripCheckedListBox checkedList, int selectAllIndex)
		{
			if (e.Index == selectAllIndex)
			{
				if (e.NewValue == CheckState.Checked)
				{
					checkedList.CheckAll();
				}
				if (e.NewValue == CheckState.Unchecked)
				{
					checkedList.UncheckAll();
				}
				return;
			}
			int num = checkedList.CheckedItems.Count;
			if (checkedList.GetItemCheckState(selectAllIndex) != CheckState.Unchecked)
			{
				num--;
			}
			if (e.NewValue != e.CurrentValue)
			{
				if (e.NewValue == CheckState.Checked)
				{
					num++;
				}
				else
				{
					num--;
				}
			}
			if (num == 0)
			{
				checkedList.SetItemState(selectAllIndex, CheckState.Unchecked);
				return;
			}
			if (num == checkedList.Items.Count - 1)
			{
				checkedList.SetItemState(selectAllIndex, CheckState.Checked);
				return;
			}
			checkedList.SetItemState(selectAllIndex, CheckState.Indeterminate);
		}

		protected void ClearAllFilters(OLVColumn column)
		{
			ObjectListView objectListView = column.ListView as ObjectListView;
			if (objectListView != null && !objectListView.IsDisposed)
			{
				objectListView.ResetColumnFiltering();
				return;
			}
		}

		protected void EnactFilter(ToolStripCheckedListBox checkedList, OLVColumn column)
		{
			ObjectListView objectListView = column.ListView as ObjectListView;
			if (objectListView != null && !objectListView.IsDisposed)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in checkedList.CheckedItems)
				{
					ICluster cluster = obj as ICluster;
					if (cluster != null)
					{
						arrayList.Add(cluster.ClusterKey);
					}
				}
				column.ValuesChosenForFiltering = arrayList;
				objectListView.UpdateColumnFiltering();
				return;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static FilterMenuBuilder()
		{
			Strings.CreateGetStringDelegate(typeof(FilterMenuBuilder));
			FilterMenuBuilder.APPLY_LABEL = FilterMenuBuilder.getString_0(107314894);
			FilterMenuBuilder.CLEAR_ALL_FILTERS_LABEL = FilterMenuBuilder.getString_0(107314341);
			FilterMenuBuilder.FILTERING_LABEL = FilterMenuBuilder.getString_0(107314714);
			FilterMenuBuilder.SELECT_ALL_LABEL = FilterMenuBuilder.getString_0(107314348);
			FilterMenuBuilder.ClearFilteringImage = Resources.ClearFiltering;
			FilterMenuBuilder.FilteringImage = Resources.Filtering;
		}

		public static string APPLY_LABEL;

		public static string CLEAR_ALL_FILTERS_LABEL;

		public static string FILTERING_LABEL;

		public static string SELECT_ALL_LABEL;

		public static Bitmap ClearFilteringImage;

		public static Bitmap FilteringImage;

		private bool treatNullAsDataValue = true;

		private int maxObjectsToConsider = 10000;

		private bool alreadyInHandleItemChecked;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
