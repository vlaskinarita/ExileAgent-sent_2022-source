using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BrightIdeasSoftware
{
	public class TreeListView : VirtualObjectListView
	{
		[Category("ObjectListView")]
		[Description("This event is triggered when a branch is about to expand.")]
		public event EventHandler<TreeBranchExpandingEventArgs> Expanding;

		[Description("This event is triggered when a branch is about to collapsed.")]
		[Category("ObjectListView")]
		public event EventHandler<TreeBranchCollapsingEventArgs> Collapsing;

		[Description("This event is triggered when a branch has been expanded.")]
		[Category("ObjectListView")]
		public event EventHandler<TreeBranchExpandedEventArgs> Expanded;

		[Description("This event is triggered when a branch has been collapsed.")]
		[Category("ObjectListView")]
		public event EventHandler<TreeBranchCollapsedEventArgs> Collapsed;

		protected virtual void OnExpanding(TreeBranchExpandingEventArgs e)
		{
			if (this.Expanding != null)
			{
				this.Expanding(this, e);
			}
		}

		protected virtual void OnCollapsing(TreeBranchCollapsingEventArgs e)
		{
			if (this.Collapsing != null)
			{
				this.Collapsing(this, e);
			}
		}

		protected virtual void OnExpanded(TreeBranchExpandedEventArgs e)
		{
			if (this.Expanded != null)
			{
				this.Expanded(this, e);
			}
		}

		protected virtual void OnCollapsed(TreeBranchCollapsedEventArgs e)
		{
			if (this.Collapsed != null)
			{
				this.Collapsed(this, e);
			}
		}

		public TreeListView()
		{
			base.OwnerDraw = true;
			base.View = View.Details;
			base.CheckedObjectsMustStillExistInList = false;
			this.RegenerateTree();
			this.TreeColumnRenderer = new TreeListView.TreeRenderer();
			base.SmallImageList = new ImageList();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual TreeListView.CanExpandGetterDelegate CanExpandGetter
		{
			get
			{
				return this.TreeModel.CanExpandGetter;
			}
			set
			{
				this.TreeModel.CanExpandGetter = value;
			}
		}

		[Browsable(false)]
		public override bool CanShowGroups
		{
			get
			{
				return false;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual TreeListView.ChildrenGetterDelegate ChildrenGetter
		{
			get
			{
				return this.TreeModel.ChildrenGetter;
			}
			set
			{
				this.TreeModel.ChildrenGetter = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TreeListView.ParentGetterDelegate ParentGetter
		{
			get
			{
				return this.parentGetter;
			}
			set
			{
				this.parentGetter = value;
			}
		}

		public override IList CheckedObjects
		{
			get
			{
				return base.CheckedObjects;
			}
			set
			{
				ArrayList arrayList = new ArrayList(this.CheckedObjects);
				if (value != null)
				{
					arrayList.AddRange(value);
				}
				base.CheckedObjects = value;
				if (this.HierarchicalCheckboxes)
				{
					this.RecalculateHierarchicalCheckBoxGraph(arrayList);
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IEnumerable ExpandedObjects
		{
			get
			{
				return this.TreeModel.mapObjectToExpanded.Keys;
			}
			set
			{
				this.TreeModel.mapObjectToExpanded.Clear();
				if (value != null)
				{
					foreach (object model in value)
					{
						this.TreeModel.SetModelExpanded(model, true);
					}
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override IListFilter ListFilter
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		[Category("ObjectListView")]
		[Description("Show hierarchical checkboxes be enabled?")]
		[DefaultValue(false)]
		public virtual bool HierarchicalCheckboxes
		{
			get
			{
				return this.hierarchicalCheckboxes;
			}
			set
			{
				if (this.hierarchicalCheckboxes == value)
				{
					return;
				}
				this.hierarchicalCheckboxes = value;
				base.CheckBoxes = value;
				if (value)
				{
					this.TriStateCheckBoxes = false;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override IEnumerable Objects
		{
			get
			{
				return this.Roots;
			}
			set
			{
				this.Roots = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override IEnumerable ObjectsForClustering
		{
			get
			{
				TreeListView.<get_ObjectsForClustering>d__6e <get_ObjectsForClustering>d__6e = new TreeListView.<get_ObjectsForClustering>d__6e(-2);
				<get_ObjectsForClustering>d__6e.<>4__this = this;
				return <get_ObjectsForClustering>d__6e;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(true)]
		[Description("Should the parent of an expand subtree be scrolled to the top revealing the children?")]
		public bool RevealAfterExpand
		{
			get
			{
				return this.revealAfterExpand;
			}
			set
			{
				this.revealAfterExpand = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IEnumerable Roots
		{
			get
			{
				return this.TreeModel.RootObjects;
			}
			set
			{
				this.TreeColumnRenderer = this.TreeColumnRenderer;
				this.TreeModel.RootObjects = (value ?? new ArrayList());
				this.UpdateVirtualListSize();
			}
		}

		protected virtual void EnsureTreeRendererPresent(TreeListView.TreeRenderer renderer)
		{
			if (base.Columns.Count == 0)
			{
				return;
			}
			foreach (object obj in base.Columns)
			{
				OLVColumn olvcolumn = (OLVColumn)obj;
				if (olvcolumn.Renderer is TreeListView.TreeRenderer)
				{
					olvcolumn.Renderer = renderer;
					return;
				}
			}
			OLVColumn column = this.GetColumn(0);
			column.Renderer = renderer;
			column.WordWrap = column.WordWrap;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual TreeListView.TreeRenderer TreeColumnRenderer
		{
			get
			{
				TreeListView.TreeRenderer result;
				if ((result = this.treeRenderer) == null)
				{
					result = (this.treeRenderer = new TreeListView.TreeRenderer());
				}
				return result;
			}
			set
			{
				this.treeRenderer = (value ?? new TreeListView.TreeRenderer());
				this.EnsureTreeRendererPresent(this.treeRenderer);
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListView.TreeFactoryDelegate TreeFactory
		{
			get
			{
				return this.treeFactory;
			}
			set
			{
				this.treeFactory = value;
			}
		}

		[DefaultValue(true)]
		[Category("ObjectListView")]
		[Description("Should a wait cursor be shown when a branch is being expaned?")]
		public virtual bool UseWaitCursorWhenExpanding
		{
			get
			{
				return this.useWaitCursorWhenExpanding;
			}
			set
			{
				this.useWaitCursorWhenExpanding = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeListView.Tree TreeModel
		{
			get
			{
				return this.treeModel;
			}
			protected set
			{
				this.treeModel = value;
			}
		}

		public virtual bool IsExpanded(object model)
		{
			TreeListView.Branch branch = this.TreeModel.GetBranch(model);
			return branch != null && branch.IsExpanded;
		}

		public virtual void Collapse(object model)
		{
			if (this.GetItemCount() == 0)
			{
				return;
			}
			OLVListItem item = this.ModelToItem(model);
			TreeBranchCollapsingEventArgs treeBranchCollapsingEventArgs = new TreeBranchCollapsingEventArgs(model, item);
			this.OnCollapsing(treeBranchCollapsingEventArgs);
			if (treeBranchCollapsingEventArgs.Canceled)
			{
				return;
			}
			IList selectedObjects = this.SelectedObjects;
			int num = this.TreeModel.Collapse(model);
			if (num >= 0)
			{
				this.UpdateVirtualListSize();
				this.SelectedObjects = selectedObjects;
				if (num < this.GetItemCount())
				{
					base.RedrawItems(num, this.GetItemCount() - 1, false);
				}
				this.OnCollapsed(new TreeBranchCollapsedEventArgs(model, item));
			}
		}

		public virtual void CollapseAll()
		{
			if (this.GetItemCount() == 0)
			{
				return;
			}
			TreeBranchCollapsingEventArgs treeBranchCollapsingEventArgs = new TreeBranchCollapsingEventArgs(null, null);
			this.OnCollapsing(treeBranchCollapsingEventArgs);
			if (treeBranchCollapsingEventArgs.Canceled)
			{
				return;
			}
			IList selectedObjects = this.SelectedObjects;
			int num = this.TreeModel.CollapseAll();
			if (num >= 0)
			{
				this.UpdateVirtualListSize();
				this.SelectedObjects = selectedObjects;
				base.RedrawItems(num, this.GetItemCount() - 1, false);
				this.OnCollapsed(new TreeBranchCollapsedEventArgs(null, null));
			}
		}

		public override void ClearObjects()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ClearObjects));
				return;
			}
			this.Roots = null;
			this.DiscardAllState();
		}

		public virtual void DiscardAllState()
		{
			base.CheckStateMap.Clear();
			this.RebuildAll(false);
		}

		public virtual void Expand(object model)
		{
			if (this.GetItemCount() == 0)
			{
				return;
			}
			OLVListItem item = this.ModelToItem(model);
			TreeBranchExpandingEventArgs treeBranchExpandingEventArgs = new TreeBranchExpandingEventArgs(model, item);
			this.OnExpanding(treeBranchExpandingEventArgs);
			if (treeBranchExpandingEventArgs.Canceled)
			{
				return;
			}
			IList selectedObjects = this.SelectedObjects;
			int num = this.TreeModel.Expand(model);
			if (num < 0)
			{
				return;
			}
			this.UpdateVirtualListSize();
			using (base.SuspendSelectionEventsDuring())
			{
				this.SelectedObjects = selectedObjects;
			}
			base.RedrawItems(num, this.GetItemCount() - 1, false);
			this.OnExpanded(new TreeBranchExpandedEventArgs(model, item));
			if (this.RevealAfterExpand && num > 0)
			{
				base.BeginUpdate();
				try
				{
					int countPerPage = NativeMethods.GetCountPerPage(this);
					int visibleDescendentCount = this.TreeModel.GetVisibleDescendentCount(model);
					if (visibleDescendentCount < countPerPage)
					{
						base.EnsureVisible(num + visibleDescendentCount);
					}
					else
					{
						this.TopItemIndex = num;
					}
				}
				finally
				{
					base.EndUpdate();
				}
			}
		}

		public virtual void ExpandAll()
		{
			if (this.GetItemCount() == 0)
			{
				return;
			}
			TreeBranchExpandingEventArgs treeBranchExpandingEventArgs = new TreeBranchExpandingEventArgs(null, null);
			this.OnExpanding(treeBranchExpandingEventArgs);
			if (treeBranchExpandingEventArgs.Canceled)
			{
				return;
			}
			IList selectedObjects = this.SelectedObjects;
			int num = this.TreeModel.ExpandAll();
			if (num < 0)
			{
				return;
			}
			this.UpdateVirtualListSize();
			using (base.SuspendSelectionEventsDuring())
			{
				this.SelectedObjects = selectedObjects;
			}
			base.RedrawItems(num, this.GetItemCount() - 1, false);
			this.OnExpanded(new TreeBranchExpandedEventArgs(null, null));
		}

		public virtual void RebuildAll(bool preserveState)
		{
			int topItemIndex = preserveState ? this.TopItemIndex : -1;
			this.RebuildAll(preserveState ? this.SelectedObjects : null, preserveState ? this.ExpandedObjects : null, preserveState ? this.CheckedObjects : null);
			if (preserveState)
			{
				this.TopItemIndex = topItemIndex;
			}
		}

		protected virtual void RebuildAll(IList selected, IEnumerable expanded, IList checkedObjects)
		{
			IEnumerable roots = this.Roots;
			TreeListView.CanExpandGetterDelegate canExpandGetter = this.CanExpandGetter;
			TreeListView.ChildrenGetterDelegate childrenGetter = this.ChildrenGetter;
			try
			{
				base.BeginUpdate();
				this.RegenerateTree();
				this.CanExpandGetter = canExpandGetter;
				this.ChildrenGetter = childrenGetter;
				if (expanded != null)
				{
					this.ExpandedObjects = expanded;
				}
				this.Roots = roots;
				if (selected != null)
				{
					this.SelectedObjects = selected;
				}
				if (checkedObjects != null)
				{
					this.CheckedObjects = checkedObjects;
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public virtual void Reveal(object modelToReveal, bool selectAfterReveal)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object value in this.GetAncestors(modelToReveal))
			{
				arrayList.Add(value);
			}
			arrayList.Reverse();
			try
			{
				base.BeginUpdate();
				foreach (object model in arrayList)
				{
					this.Expand(model);
				}
				this.EnsureModelVisible(modelToReveal);
				if (selectAfterReveal)
				{
					this.SelectObject(modelToReveal, true);
				}
			}
			finally
			{
				base.EndUpdate();
			}
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
			if (this.GetItemCount() == 0)
			{
				return;
			}
			IList selectedObjects = this.SelectedObjects;
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable = new Hashtable();
			foreach (object obj in modelObjects)
			{
				if (obj != null)
				{
					object parent = this.GetParent(obj);
					if (parent == null)
					{
						arrayList.Add(obj);
					}
					else
					{
						hashtable[obj] = true;
						hashtable[parent] = true;
					}
				}
			}
			if (arrayList.Count > 0)
			{
				base.RefreshObjects(arrayList);
			}
			int num = int.MaxValue;
			foreach (object obj2 in hashtable.Keys)
			{
				if (obj2 != null)
				{
					int num2 = this.TreeModel.RebuildChildren(obj2);
					if (num2 >= 0)
					{
						num = Math.Min(num, num2);
					}
				}
			}
			if (num >= this.GetItemCount())
			{
				return;
			}
			this.ClearCachedInfo();
			this.UpdateVirtualListSize();
			this.SelectedObjects = selectedObjects;
			base.RedrawItems(num, this.GetItemCount() - 1, false);
		}

		protected override bool SetObjectCheckedness(object modelObject, CheckState state)
		{
			if (!base.SetObjectCheckedness(modelObject, state))
			{
				return false;
			}
			if (!this.HierarchicalCheckboxes)
			{
				return true;
			}
			CheckState? checkState = this.GetCheckState(modelObject);
			if (checkState != null)
			{
				if (checkState.Value != CheckState.Indeterminate)
				{
					foreach (object modelObject2 in this.GetChildrenWithoutExpanding(modelObject))
					{
						this.SetObjectCheckedness(modelObject2, checkState.Value);
					}
					this.RecalculateHierarchicalCheckBoxGraph(new ArrayList
					{
						modelObject
					});
					return true;
				}
			}
			return true;
		}

		private IEnumerable GetChildrenWithoutExpanding(object model)
		{
			TreeListView.Branch branch = this.TreeModel.GetBranch(model);
			if (branch != null && branch.CanExpand)
			{
				return branch.Children;
			}
			return new ArrayList();
		}

		public virtual void ToggleExpansion(object model)
		{
			this.ModelToItem(model);
			if (this.IsExpanded(model))
			{
				this.Collapse(model);
				return;
			}
			this.Expand(model);
		}

		public virtual bool CanExpand(object model)
		{
			TreeListView.Branch branch = this.TreeModel.GetBranch(model);
			return branch != null && branch.CanExpand;
		}

		public virtual object GetParent(object model)
		{
			TreeListView.Branch branch = this.TreeModel.GetBranch(model);
			if (branch != null && branch.ParentBranch != null)
			{
				return branch.ParentBranch.Model;
			}
			return null;
		}

		public virtual IEnumerable GetChildren(object model)
		{
			TreeListView.Branch branch = this.TreeModel.GetBranch(model);
			if (branch != null && branch.CanExpand)
			{
				branch.FetchChildren();
				return branch.Children;
			}
			return new ArrayList();
		}

		protected override bool ProcessLButtonDown(OlvListViewHitTestInfo hti)
		{
			if (hti.HitTestLocation == HitTestLocation.ExpandButton)
			{
				this.PossibleFinishCellEditing();
				this.ToggleExpansion(hti.RowObject);
				return true;
			}
			return base.ProcessLButtonDown(hti);
		}

		public override OLVListItem MakeListViewItem(int itemIndex)
		{
			OLVListItem olvlistItem = base.MakeListViewItem(itemIndex);
			TreeListView.Branch branch = this.TreeModel.GetBranch(olvlistItem.RowObject);
			if (branch != null)
			{
				olvlistItem.IndentCount = branch.Level;
			}
			return olvlistItem;
		}

		protected virtual void RegenerateTree()
		{
			this.TreeModel = ((this.TreeFactory == null) ? new TreeListView.Tree(this) : this.TreeFactory(this));
			Trace.Assert(this.TreeModel != null);
			this.VirtualListDataSource = this.TreeModel;
		}

		protected virtual void RecalculateHierarchicalCheckBoxGraph(IList toCheck)
		{
			if (toCheck == null || toCheck.Count == 0)
			{
				return;
			}
			if (this.isRecalculatingHierarchicalCheckBox)
			{
				return;
			}
			try
			{
				this.isRecalculatingHierarchicalCheckBox = true;
				foreach (object modelObject in this.CalculateDistinctAncestors(toCheck))
				{
					this.RecalculateSingleHierarchicalCheckBox(modelObject);
				}
			}
			finally
			{
				this.isRecalculatingHierarchicalCheckBox = false;
			}
		}

		protected virtual void RecalculateSingleHierarchicalCheckBox(object modelObject)
		{
			if (modelObject == null)
			{
				return;
			}
			if (!this.CanExpand(modelObject))
			{
				return;
			}
			CheckState? checkState = null;
			foreach (object modelObject2 in this.GetChildren(modelObject))
			{
				CheckState? checkState2 = this.GetCheckState(modelObject2);
				if (checkState2 != null)
				{
					if (checkState != null)
					{
						if (checkState.Value != checkState2.Value)
						{
							checkState = new CheckState?(CheckState.Indeterminate);
							break;
						}
					}
					else
					{
						checkState = checkState2;
					}
				}
			}
			base.SetObjectCheckedness(modelObject, checkState ?? CheckState.Indeterminate);
		}

		protected virtual IEnumerable CalculateDistinctAncestors(IList toCheck)
		{
			TreeListView.<CalculateDistinctAncestors>d__76 <CalculateDistinctAncestors>d__ = new TreeListView.<CalculateDistinctAncestors>d__76(-2);
			<CalculateDistinctAncestors>d__.<>4__this = this;
			<CalculateDistinctAncestors>d__.<>3__toCheck = toCheck;
			return <CalculateDistinctAncestors>d__;
		}

		protected virtual IEnumerable GetAncestors(object model)
		{
			TreeListView.<GetAncestors>d__84 <GetAncestors>d__ = new TreeListView.<GetAncestors>d__84(-2);
			<GetAncestors>d__.<>4__this = this;
			<GetAncestors>d__.<>3__model = model;
			return <GetAncestors>d__;
		}

		protected override void HandleApplicationIdle(object sender, EventArgs e)
		{
			base.HandleApplicationIdle(sender, e);
			base.Invalidate();
		}

		protected override bool IsInputKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys != Keys.Left)
			{
				if (keys != Keys.Right)
				{
					return base.IsInputKey(keyData);
				}
			}
			return true;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			OLVListItem olvlistItem = base.FocusedItem as OLVListItem;
			if (olvlistItem == null)
			{
				base.OnKeyDown(e);
				return;
			}
			object rowObject = olvlistItem.RowObject;
			TreeListView.Branch branch = this.TreeModel.GetBranch(rowObject);
			switch (e.KeyCode)
			{
			case Keys.Left:
				if (branch.IsExpanded)
				{
					this.Collapse(rowObject);
				}
				else if (branch.ParentBranch != null && branch.ParentBranch.Model != null)
				{
					this.SelectObject(branch.ParentBranch.Model, true);
				}
				e.Handled = true;
				break;
			case Keys.Right:
				if (branch.IsExpanded)
				{
					List<TreeListView.Branch> filteredChildBranches = branch.FilteredChildBranches;
					if (filteredChildBranches.Count > 0)
					{
						this.SelectObject(filteredChildBranches[0].Model, true);
					}
				}
				else if (branch.CanExpand)
				{
					this.Expand(rowObject);
				}
				e.Handled = true;
				break;
			}
			base.OnKeyDown(e);
		}

		private TreeListView.ParentGetterDelegate parentGetter;

		private bool hierarchicalCheckboxes;

		private bool revealAfterExpand = true;

		private TreeListView.TreeRenderer treeRenderer;

		private TreeListView.TreeFactoryDelegate treeFactory;

		private bool useWaitCursorWhenExpanding = true;

		private TreeListView.Tree treeModel;

		private bool isRecalculatingHierarchicalCheckBox;

		public sealed class TreeRenderer : HighlightTextRenderer
		{
			public TreeRenderer()
			{
				this.LinePen = new Pen(Color.Blue, 1f);
				this.LinePen.DashStyle = DashStyle.Dot;
			}

			private TreeListView.Branch Branch
			{
				get
				{
					return this.TreeListView.TreeModel.GetBranch(base.RowObject);
				}
			}

			public Pen LinePen
			{
				get
				{
					return this.linePen;
				}
				set
				{
					this.linePen = value;
				}
			}

			public TreeListView TreeListView
			{
				get
				{
					return (TreeListView)base.ListView;
				}
			}

			public bool IsShowLines
			{
				get
				{
					return this.isShowLines;
				}
				set
				{
					this.isShowLines = value;
				}
			}

			public override void Render(Graphics g, Rectangle r)
			{
				this.DrawBackground(g, r);
				TreeListView.Branch branch = this.Branch;
				Rectangle rectangle = this.ApplyCellPadding(r);
				Rectangle rectangle2 = rectangle;
				rectangle2.Offset((branch.Level - 1) * TreeListView.TreeRenderer.PIXELS_PER_LEVEL, 0);
				rectangle2.Width = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
				rectangle2.Height = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
				rectangle2.Y = base.AlignVertically(rectangle, rectangle2);
				int glyphMidVertical = rectangle2.Y + rectangle2.Height / 2;
				if (this.IsShowLines)
				{
					this.DrawLines(g, r, this.LinePen, branch, glyphMidVertical);
				}
				if (branch.CanExpand)
				{
					this.DrawExpansionGlyph(g, rectangle2, branch.IsExpanded);
				}
				int num = branch.Level * TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
				rectangle.Offset(num, 0);
				rectangle.Width -= num;
				this.DrawImageAndText(g, rectangle);
			}

			protected void DrawExpansionGlyph(Graphics g, Rectangle r, bool isExpanded)
			{
				if (this.UseStyles)
				{
					this.DrawExpansionGlyphStyled(g, r, isExpanded);
					return;
				}
				this.DrawExpansionGlyphManual(g, r, isExpanded);
			}

			protected bool UseStyles
			{
				get
				{
					return !base.IsPrinting && Application.RenderWithVisualStyles;
				}
			}

			protected void DrawExpansionGlyphStyled(Graphics g, Rectangle r, bool isExpanded)
			{
				VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Closed;
				if (isExpanded)
				{
					element = VisualStyleElement.TreeView.Glyph.Opened;
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				visualStyleRenderer.DrawBackground(g, r);
			}

			protected void DrawExpansionGlyphManual(Graphics g, Rectangle r, bool isExpanded)
			{
				int num = 8;
				int num2 = r.X + 4;
				int num3 = r.Y + r.Height / 2 - 4;
				g.DrawRectangle(new Pen(SystemBrushes.ControlDark), num2, num3, 8, 8);
				g.FillRectangle(Brushes.White, num2 + 1, num3 + 1, 7, 7);
				g.DrawLine(Pens.Black, num2 + 2, num3 + 4, num2 + 8 - 2, num3 + 4);
				if (!isExpanded)
				{
					g.DrawLine(Pens.Black, num2 + 4, num3 + 2, num2 + 4, num3 + num - 2);
				}
			}

			protected void DrawLines(Graphics g, Rectangle r, Pen p, TreeListView.Branch br, int glyphMidVertical)
			{
				Rectangle rectangle = r;
				rectangle.Width = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
				int top = rectangle.Top;
				IList<TreeListView.Branch> ancestors = br.Ancestors;
				int num;
				foreach (TreeListView.Branch branch in ancestors)
				{
					if (!branch.IsLastChild && !branch.IsOnlyBranch)
					{
						num = rectangle.Left + rectangle.Width / 2;
						g.DrawLine(p, num, top, num, rectangle.Bottom);
					}
					rectangle.Offset(TreeListView.TreeRenderer.PIXELS_PER_LEVEL, 0);
				}
				num = rectangle.Left + rectangle.Width / 2;
				g.DrawLine(p, num, glyphMidVertical, rectangle.Right, glyphMidVertical);
				if (br.IsFirstBranch)
				{
					if (!br.IsLastChild && !br.IsOnlyBranch)
					{
						g.DrawLine(p, num, glyphMidVertical, num, rectangle.Bottom);
						return;
					}
				}
				else
				{
					if (br.IsLastChild)
					{
						g.DrawLine(p, num, top, num, glyphMidVertical);
						return;
					}
					g.DrawLine(p, num, top, num, rectangle.Bottom);
				}
			}

			protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
			{
				TreeListView.Branch branch = this.Branch;
				Rectangle bounds = base.Bounds;
				if (branch.CanExpand)
				{
					bounds.Offset((branch.Level - 1) * TreeListView.TreeRenderer.PIXELS_PER_LEVEL, 0);
					bounds.Width = TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
					if (bounds.Contains(x, y))
					{
						hti.HitTestLocation = HitTestLocation.ExpandButton;
						return;
					}
				}
				bounds = base.Bounds;
				int num = branch.Level * TreeListView.TreeRenderer.PIXELS_PER_LEVEL;
				bounds.X += num;
				bounds.Width -= num;
				if (x < bounds.Left)
				{
					hti.HitTestLocation = HitTestLocation.Nothing;
					return;
				}
				base.StandardHitTest(g, hti, bounds, x, y);
			}

			protected override Rectangle HandleGetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
			{
				return base.StandardGetEditRectangle(g, cellBounds, preferredSize);
			}

			private Pen linePen;

			private bool isShowLines = true;

			public static int PIXELS_PER_LEVEL = 17;
		}

		public delegate bool CanExpandGetterDelegate(object model);

		public delegate IEnumerable ChildrenGetterDelegate(object model);

		public delegate object ParentGetterDelegate(object model);

		public delegate TreeListView.Tree TreeFactoryDelegate(TreeListView view);

		public sealed class Tree : IVirtualListDataSource, IFilterableDataSource
		{
			public Tree(TreeListView treeView)
			{
				this.treeView = treeView;
				this.trunk = new TreeListView.Branch(null, this, null);
				this.trunk.IsExpanded = true;
			}

			public TreeListView.CanExpandGetterDelegate CanExpandGetter
			{
				get
				{
					return this.canExpandGetter;
				}
				set
				{
					this.canExpandGetter = value;
				}
			}

			public TreeListView.ChildrenGetterDelegate ChildrenGetter
			{
				get
				{
					return this.childrenGetter;
				}
				set
				{
					this.childrenGetter = value;
				}
			}

			public IEnumerable RootObjects
			{
				get
				{
					return this.trunk.Children;
				}
				set
				{
					this.trunk.Children = value;
					foreach (TreeListView.Branch branch in this.trunk.ChildBranches)
					{
						branch.RefreshChildren();
					}
					this.RebuildList();
				}
			}

			public TreeListView TreeView
			{
				get
				{
					return this.treeView;
				}
			}

			public int Collapse(object model)
			{
				TreeListView.Branch branch = this.GetBranch(model);
				if (branch == null || !branch.IsExpanded)
				{
					return -1;
				}
				if (!branch.Visible)
				{
					branch.Collapse();
					return -1;
				}
				int numberVisibleDescendents = branch.NumberVisibleDescendents;
				branch.Collapse();
				int objectIndex = this.GetObjectIndex(model);
				this.objectList.RemoveRange(objectIndex + 1, numberVisibleDescendents);
				this.RebuildObjectMap(objectIndex + 1);
				return objectIndex;
			}

			public int CollapseAll()
			{
				foreach (TreeListView.Branch branch in this.trunk.ChildBranches)
				{
					if (branch.IsExpanded)
					{
						branch.Collapse();
					}
				}
				this.RebuildList();
				return 0;
			}

			public int Expand(object model)
			{
				TreeListView.Branch branch = this.GetBranch(model);
				if (branch == null || !branch.CanExpand || branch.IsExpanded)
				{
					return -1;
				}
				if (!branch.Visible)
				{
					branch.Expand();
					return -1;
				}
				int objectIndex = this.GetObjectIndex(model);
				this.InsertChildren(branch, objectIndex + 1);
				return objectIndex;
			}

			public int ExpandAll()
			{
				this.trunk.ExpandAll();
				this.Sort(this.lastSortColumn, this.lastSortOrder);
				return 0;
			}

			public TreeListView.Branch GetBranch(object model)
			{
				if (model == null)
				{
					return null;
				}
				TreeListView.Branch result;
				this.mapObjectToBranch.TryGetValue(model, out result);
				return result;
			}

			public int GetVisibleDescendentCount(object model)
			{
				TreeListView.Branch branch = this.GetBranch(model);
				if (branch != null && branch.IsExpanded)
				{
					return branch.NumberVisibleDescendents;
				}
				return 0;
			}

			public int RebuildChildren(object model)
			{
				TreeListView.Branch branch = this.GetBranch(model);
				if (branch != null && branch.Visible)
				{
					int numberVisibleDescendents = branch.NumberVisibleDescendents;
					int objectIndex = this.GetObjectIndex(model);
					if (numberVisibleDescendents > 0)
					{
						this.objectList.RemoveRange(objectIndex + 1, numberVisibleDescendents);
					}
					if (branch.CanExpand && branch.IsExpanded)
					{
						branch.RefreshChildren();
						this.InsertChildren(branch, objectIndex + 1);
					}
					return objectIndex;
				}
				return -1;
			}

			internal bool IsModelExpanded(object model)
			{
				if (model == null)
				{
					return true;
				}
				bool result = false;
				this.mapObjectToExpanded.TryGetValue(model, out result);
				return result;
			}

			internal void SetModelExpanded(object model, bool isExpanded)
			{
				if (model == null)
				{
					return;
				}
				if (isExpanded)
				{
					this.mapObjectToExpanded[model] = true;
					return;
				}
				this.mapObjectToExpanded.Remove(model);
			}

			protected void InsertChildren(TreeListView.Branch br, int index)
			{
				br.Expand();
				br.Sort(this.GetBranchComparer());
				this.objectList.InsertRange(index, br.Flatten());
				this.RebuildObjectMap(index);
			}

			protected void RebuildList()
			{
				this.objectList = ArrayList.Adapter(this.trunk.Flatten());
				List<TreeListView.Branch> filteredChildBranches = this.trunk.FilteredChildBranches;
				if (filteredChildBranches.Count > 0)
				{
					filteredChildBranches[0].IsFirstBranch = true;
					filteredChildBranches[0].IsOnlyBranch = (filteredChildBranches.Count == 1);
				}
				this.RebuildObjectMap(0);
			}

			protected void RebuildObjectMap(int startIndex)
			{
				if (startIndex == 0)
				{
					this.mapObjectToIndex.Clear();
				}
				for (int i = startIndex; i < this.objectList.Count; i++)
				{
					this.mapObjectToIndex[this.objectList[i]] = i;
				}
			}

			internal TreeListView.Branch MakeBranch(TreeListView.Branch parent, object model)
			{
				TreeListView.Branch branch = new TreeListView.Branch(parent, this, model);
				this.mapObjectToBranch[model] = branch;
				return branch;
			}

			public object GetNthObject(int n)
			{
				return this.objectList[n];
			}

			public int GetObjectCount()
			{
				return this.trunk.NumberVisibleDescendents;
			}

			public int GetObjectIndex(object model)
			{
				int result;
				if (model != null && this.mapObjectToIndex.TryGetValue(model, out result))
				{
					return result;
				}
				return -1;
			}

			public void PrepareCache(int first, int last)
			{
			}

			public int SearchText(string value, int first, int last, OLVColumn column)
			{
				return AbstractVirtualListDataSource.DefaultSearchText(value, first, last, column, this);
			}

			public void Sort(OLVColumn column, SortOrder order)
			{
				this.lastSortColumn = column;
				this.lastSortOrder = order;
				foreach (TreeListView.Branch branch in this.trunk.ChildBranches)
				{
					branch.IsFirstBranch = false;
				}
				this.trunk.Sort(this.GetBranchComparer());
				this.RebuildList();
			}

			protected TreeListView.BranchComparer GetBranchComparer()
			{
				if (this.lastSortColumn == null)
				{
					return null;
				}
				return new TreeListView.BranchComparer(new ModelObjectComparer(this.lastSortColumn, this.lastSortOrder, this.treeView.SecondarySortColumn ?? this.treeView.GetColumn(0), (this.treeView.SecondarySortColumn == null) ? this.lastSortOrder : this.treeView.SecondarySortOrder));
			}

			public void AddObjects(ICollection modelObjects)
			{
				ArrayList arrayList = ObjectListView.EnumerableToArray(this.treeView.Roots, true);
				foreach (object value in modelObjects)
				{
					arrayList.Add(value);
				}
				this.SetObjects(arrayList);
			}

			public void RemoveObjects(ICollection modelObjects)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object value in this.treeView.Roots)
				{
					arrayList.Add(value);
				}
				foreach (object obj in modelObjects)
				{
					arrayList.Remove(obj);
					this.mapObjectToIndex.Remove(obj);
				}
				this.SetObjects(arrayList);
			}

			public void SetObjects(IEnumerable collection)
			{
				this.treeView.Roots = collection;
			}

			public void UpdateObject(int index, object modelObject)
			{
				ArrayList arrayList = ObjectListView.EnumerableToArray(this.treeView.Roots, false);
				if (index < arrayList.Count)
				{
					arrayList[index] = modelObject;
				}
				this.SetObjects(arrayList);
			}

			public void ApplyFilters(IModelFilter modelFilter, IListFilter listFilter)
			{
				this.modelFilter = modelFilter;
				this.listFilter = listFilter;
				this.RebuildList();
			}

			internal bool IsFiltering
			{
				get
				{
					return this.treeView.UseFiltering && (this.modelFilter != null || this.listFilter != null);
				}
			}

			internal bool IncludeModel(object model)
			{
				return !this.treeView.UseFiltering || this.modelFilter == null || this.modelFilter.Filter(model);
			}

			private TreeListView.CanExpandGetterDelegate canExpandGetter;

			private TreeListView.ChildrenGetterDelegate childrenGetter;

			private OLVColumn lastSortColumn;

			private SortOrder lastSortOrder;

			private Dictionary<object, TreeListView.Branch> mapObjectToBranch = new Dictionary<object, TreeListView.Branch>();

			internal Dictionary<object, bool> mapObjectToExpanded = new Dictionary<object, bool>();

			private Dictionary<object, int> mapObjectToIndex = new Dictionary<object, int>();

			private ArrayList objectList = new ArrayList();

			private TreeListView treeView;

			private TreeListView.Branch trunk;

			protected IModelFilter modelFilter;

			protected IListFilter listFilter;
		}

		public sealed class Branch
		{
			public Branch(TreeListView.Branch parent, TreeListView.Tree tree, object model)
			{
				this.ParentBranch = parent;
				this.Tree = tree;
				this.Model = model;
			}

			public IList<TreeListView.Branch> Ancestors
			{
				get
				{
					List<TreeListView.Branch> list = new List<TreeListView.Branch>();
					if (this.ParentBranch != null)
					{
						this.ParentBranch.PushAncestors(list);
					}
					return list;
				}
			}

			private void PushAncestors(IList<TreeListView.Branch> list)
			{
				if (this.ParentBranch != null)
				{
					this.ParentBranch.PushAncestors(list);
					list.Add(this);
				}
			}

			public bool CanExpand
			{
				get
				{
					return this.Tree.CanExpandGetter != null && this.Model != null && this.Tree.CanExpandGetter(this.Model);
				}
			}

			public List<TreeListView.Branch> ChildBranches
			{
				get
				{
					return this.childBranches;
				}
				set
				{
					this.childBranches = value;
				}
			}

			public IEnumerable Children
			{
				get
				{
					ArrayList arrayList = new ArrayList();
					foreach (TreeListView.Branch branch in this.ChildBranches)
					{
						arrayList.Add(branch.Model);
					}
					return arrayList;
				}
				set
				{
					this.ChildBranches.Clear();
					TreeListView treeView = this.Tree.TreeView;
					CheckState? checkState = null;
					if (treeView != null && treeView.HierarchicalCheckboxes)
					{
						checkState = treeView.GetCheckState(this.Model);
					}
					foreach (object modelObject in value)
					{
						this.AddChild(modelObject);
						if (checkState != null && checkState.Value == CheckState.Checked)
						{
							treeView.SetObjectCheckedness(modelObject, checkState.Value);
						}
					}
				}
			}

			private void AddChild(object model)
			{
				TreeListView.Branch branch = this.Tree.GetBranch(model);
				if (branch == null)
				{
					branch = this.Tree.MakeBranch(this, model);
				}
				else
				{
					branch.ParentBranch = this;
					branch.Model = model;
					branch.ClearCachedInfo();
				}
				this.ChildBranches.Add(branch);
			}

			public List<TreeListView.Branch> FilteredChildBranches
			{
				get
				{
					if (!this.IsExpanded)
					{
						return new List<TreeListView.Branch>();
					}
					if (!this.Tree.IsFiltering)
					{
						return this.ChildBranches;
					}
					List<TreeListView.Branch> list = new List<TreeListView.Branch>();
					foreach (TreeListView.Branch branch in this.ChildBranches)
					{
						if (this.Tree.IncludeModel(branch.Model))
						{
							list.Add(branch);
						}
						else if (branch.FilteredChildBranches.Count > 0)
						{
							list.Add(branch);
						}
					}
					return list;
				}
			}

			public bool IsExpanded
			{
				get
				{
					return this.Tree.IsModelExpanded(this.Model);
				}
				set
				{
					this.Tree.SetModelExpanded(this.Model, value);
				}
			}

			public bool IsFirstBranch
			{
				get
				{
					return (this.flags & TreeListView.Branch.BranchFlags.FirstBranch) != (TreeListView.Branch.BranchFlags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= TreeListView.Branch.BranchFlags.FirstBranch;
						return;
					}
					this.flags &= ~TreeListView.Branch.BranchFlags.FirstBranch;
				}
			}

			public bool IsLastChild
			{
				get
				{
					return (this.flags & TreeListView.Branch.BranchFlags.LastChild) != (TreeListView.Branch.BranchFlags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= TreeListView.Branch.BranchFlags.LastChild;
						return;
					}
					this.flags &= ~TreeListView.Branch.BranchFlags.LastChild;
				}
			}

			public bool IsOnlyBranch
			{
				get
				{
					return (this.flags & TreeListView.Branch.BranchFlags.OnlyBranch) != (TreeListView.Branch.BranchFlags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= TreeListView.Branch.BranchFlags.OnlyBranch;
						return;
					}
					this.flags &= ~TreeListView.Branch.BranchFlags.OnlyBranch;
				}
			}

			public int Level
			{
				get
				{
					if (this.ParentBranch == null)
					{
						return 0;
					}
					return this.ParentBranch.Level + 1;
				}
			}

			public object Model
			{
				get
				{
					return this.model;
				}
				set
				{
					this.model = value;
				}
			}

			public int NumberVisibleDescendents
			{
				get
				{
					if (!this.IsExpanded)
					{
						return 0;
					}
					List<TreeListView.Branch> filteredChildBranches = this.FilteredChildBranches;
					int num = filteredChildBranches.Count;
					foreach (TreeListView.Branch branch in filteredChildBranches)
					{
						num += branch.NumberVisibleDescendents;
					}
					return num;
				}
			}

			public TreeListView.Branch ParentBranch
			{
				get
				{
					return this.parentBranch;
				}
				set
				{
					this.parentBranch = value;
				}
			}

			public TreeListView.Tree Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = value;
				}
			}

			public bool Visible
			{
				get
				{
					return this.ParentBranch == null || (this.ParentBranch.IsExpanded && this.ParentBranch.Visible);
				}
			}

			public void ClearCachedInfo()
			{
				this.Children = new ArrayList();
				this.alreadyHasChildren = false;
			}

			public void Collapse()
			{
				this.IsExpanded = false;
			}

			public void Expand()
			{
				if (this.CanExpand)
				{
					this.IsExpanded = true;
					this.FetchChildren();
				}
			}

			public void ExpandAll()
			{
				this.Expand();
				foreach (TreeListView.Branch branch in this.ChildBranches)
				{
					if (branch.CanExpand)
					{
						branch.ExpandAll();
					}
				}
			}

			public void FetchChildren()
			{
				if (this.alreadyHasChildren)
				{
					return;
				}
				this.alreadyHasChildren = true;
				if (this.Tree.ChildrenGetter == null)
				{
					return;
				}
				Cursor value = Cursor.Current;
				try
				{
					if (this.Tree.TreeView.UseWaitCursorWhenExpanding)
					{
						Cursor.Current = Cursors.WaitCursor;
					}
					this.Children = this.Tree.ChildrenGetter(this.Model);
				}
				finally
				{
					Cursor.Current = value;
				}
			}

			public IList Flatten()
			{
				ArrayList arrayList = new ArrayList();
				if (this.IsExpanded)
				{
					this.FlattenOnto(arrayList);
				}
				return arrayList;
			}

			public void FlattenOnto(IList flatList)
			{
				TreeListView.Branch branch = null;
				foreach (TreeListView.Branch branch2 in this.FilteredChildBranches)
				{
					branch = branch2;
					branch2.IsLastChild = false;
					flatList.Add(branch2.Model);
					if (branch2.IsExpanded)
					{
						branch2.FlattenOnto(flatList);
					}
				}
				if (branch != null)
				{
					branch.IsLastChild = true;
				}
			}

			public void RefreshChildren()
			{
				if (this.IsExpanded && this.CanExpand)
				{
					this.ClearCachedInfo();
					this.FetchChildren();
					foreach (TreeListView.Branch branch in this.ChildBranches)
					{
						branch.RefreshChildren();
					}
					return;
				}
			}

			public void Sort(TreeListView.BranchComparer comparer)
			{
				if (this.ChildBranches.Count == 0)
				{
					return;
				}
				if (comparer != null)
				{
					this.ChildBranches.Sort(comparer);
				}
				foreach (TreeListView.Branch branch in this.ChildBranches)
				{
					branch.Sort(comparer);
				}
			}

			private List<TreeListView.Branch> childBranches = new List<TreeListView.Branch>();

			private object model;

			private TreeListView.Branch parentBranch;

			private TreeListView.Tree tree;

			private bool alreadyHasChildren;

			private TreeListView.Branch.BranchFlags flags;

			[Flags]
			public enum BranchFlags
			{
				FirstBranch = 1,
				LastChild = 2,
				OnlyBranch = 4
			}
		}

		public sealed class BranchComparer : IComparer<TreeListView.Branch>
		{
			public BranchComparer(IComparer actualComparer)
			{
				this.actualComparer = actualComparer;
			}

			public int Compare(TreeListView.Branch x, TreeListView.Branch y)
			{
				return this.actualComparer.Compare(x.Model, y.Model);
			}

			private IComparer actualComparer;
		}
	}
}
