using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BrightIdeasSoftware.Design;
using BrightIdeasSoftware.Properties;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	[Designer(typeof(ObjectListViewDesigner))]
	public class ObjectListView : ListView, ISupportInitialize
	{
		[Description("This event is triggered after the control has done a search-by-typing action.")]
		[Category("ObjectListView")]
		public event EventHandler<AfterSearchingEventArgs> AfterSearching;

		[Category("ObjectListView")]
		[Description("This event is triggered after the items in the list have been sorted.")]
		public event EventHandler<AfterSortingEventArgs> AfterSorting;

		[Category("ObjectListView")]
		[Description("This event is triggered before the control does a search-by-typing action.")]
		public event EventHandler<BeforeSearchingEventArgs> BeforeSearching;

		[Description("This event is triggered before the items in the list are sorted.")]
		[Category("ObjectListView")]
		public event EventHandler<BeforeSortingEventArgs> BeforeSorting;

		[Description("This event is triggered after the groups are created.")]
		[Category("ObjectListView")]
		public event EventHandler<CreateGroupsEventArgs> AfterCreatingGroups;

		[Description("This event is triggered before the groups are created.")]
		[Category("ObjectListView")]
		public event EventHandler<CreateGroupsEventArgs> BeforeCreatingGroups;

		[Category("ObjectListView")]
		[Description("This event is triggered when the groups are just about to be created.")]
		public event EventHandler<CreateGroupsEventArgs> AboutToCreateGroups;

		[Category("ObjectListView")]
		[Description("Can the user drop the currently dragged items at the current mouse location?")]
		public event EventHandler<OlvDropEventArgs> CanDrop;

		[Description("This event is triggered cell edit operation is finishing.")]
		[Category("ObjectListView")]
		public event CellEditEventHandler CellEditFinishing;

		[Category("ObjectListView")]
		[Description("This event is triggered when cell edit is about to begin.")]
		public event CellEditEventHandler CellEditStarting;

		[Category("ObjectListView")]
		[Description("This event is triggered when a cell editor is about to lose focus and its new contents need to be validated.")]
		public event CellEditEventHandler CellEditValidating;

		[Category("ObjectListView")]
		[Description("This event is triggered when the user left clicks a cell.")]
		public event EventHandler<CellClickEventArgs> CellClick;

		[Category("ObjectListView")]
		[Description("This event is triggered when the mouse is over a cell.")]
		public event EventHandler<CellOverEventArgs> CellOver;

		[Category("ObjectListView")]
		[Description("This event is triggered when the user right clicks a cell.")]
		public event EventHandler<CellRightClickEventArgs> CellRightClick;

		[Description("This event is triggered when a cell needs a tool tip.")]
		[Category("ObjectListView")]
		public event EventHandler<ToolTipShowingEventArgs> CellToolTipShowing;

		[Description("This event is triggered when a checkbox is checked/unchecked on a subitem.")]
		[Category("ObjectListView")]
		public event EventHandler<SubItemCheckingEventArgs> SubItemChecking;

		[Category("ObjectListView")]
		[Description("This event is triggered when the user right clicks a column header.")]
		public event ColumnRightClickEventHandler ColumnRightClick;

		[Description("This event is triggered when the user dropped items onto the control.")]
		[Category("ObjectListView")]
		public event EventHandler<OlvDropEventArgs> Dropped;

		[Description("This event is triggered when the control needs to filter its collection of objects.")]
		[Category("ObjectListView")]
		public event EventHandler<FilterEventArgs> Filter;

		[Description("This event is triggered when a cell needs to be formatted.")]
		[Category("ObjectListView")]
		public event EventHandler<FormatCellEventArgs> FormatCell;

		[Description("This event is triggered when frozeness of the control changes.")]
		[Category("ObjectListView")]
		public event EventHandler<FreezeEventArgs> Freezing;

		[Category("ObjectListView")]
		[Description("This event is triggered when a row needs to be formatted.")]
		public event EventHandler<FormatRowEventArgs> FormatRow;

		[Category("ObjectListView")]
		[Description("This event is triggered when a group is about to collapse or expand.")]
		public event EventHandler<GroupExpandingCollapsingEventArgs> GroupExpandingCollapsing;

		[Category("ObjectListView")]
		[Description("This event is triggered when a group changes state.")]
		public event EventHandler<GroupStateChangedEventArgs> GroupStateChanged;

		[Category("ObjectListView")]
		[Description("This event is triggered when a header needs a tool tip.")]
		public event EventHandler<ToolTipShowingEventArgs> HeaderToolTipShowing;

		[Category("ObjectListView")]
		[Description("This event is triggered when the hot item changed.")]
		public event EventHandler<HotItemChangedEventArgs> HotItemChanged;

		[Description("This event is triggered when a hyperlink cell is clicked.")]
		[Category("ObjectListView")]
		public event EventHandler<HyperlinkClickedEventArgs> HyperlinkClicked;

		[Category("ObjectListView")]
		[Description("This event is triggered when the task text of a group is clicked.")]
		public event EventHandler<GroupTaskClickedEventArgs> GroupTaskClicked;

		[Description("This event is triggered when the control needs to know if a given cell contains a hyperlink.")]
		[Category("ObjectListView")]
		public event EventHandler<IsHyperlinkEventArgs> IsHyperlink;

		[Description("This event is triggered when objects are about to be added to the control")]
		[Category("ObjectListView")]
		public event EventHandler<ItemsAddingEventArgs> ItemsAdding;

		[Description("This event is triggered when the contents of the control have changed.")]
		[Category("ObjectListView")]
		public event EventHandler<ItemsChangedEventArgs> ItemsChanged;

		[Category("ObjectListView")]
		[Description("This event is triggered when the contents of the control changes.")]
		public event EventHandler<ItemsChangingEventArgs> ItemsChanging;

		[Category("ObjectListView")]
		[Description("This event is triggered when objects are removed from the control.")]
		public event EventHandler<ItemsRemovingEventArgs> ItemsRemoving;

		[Description("Can the dragged collection of model objects be dropped at the current mouse location")]
		[Category("ObjectListView")]
		public event EventHandler<ModelDropEventArgs> ModelCanDrop;

		[Description("A collection of model objects from a ObjectListView has been dropped on this control")]
		[Category("ObjectListView")]
		public event EventHandler<ModelDropEventArgs> ModelDropped;

		[Description("This event is triggered once per user action that changes the selection state of one or more rows.")]
		[Category("ObjectListView")]
		public event EventHandler SelectionChanged;

		[Category("ObjectListView")]
		[Description("This event is triggered when the contents of the ObjectListView has scrolled.")]
		public event EventHandler<ScrollEventArgs> Scroll;

		protected virtual void OnAboutToCreateGroups(CreateGroupsEventArgs e)
		{
			if (this.AboutToCreateGroups != null)
			{
				this.AboutToCreateGroups(this, e);
			}
		}

		protected virtual void OnBeforeCreatingGroups(CreateGroupsEventArgs e)
		{
			if (this.BeforeCreatingGroups != null)
			{
				this.BeforeCreatingGroups(this, e);
			}
		}

		protected virtual void OnAfterCreatingGroups(CreateGroupsEventArgs e)
		{
			if (this.AfterCreatingGroups != null)
			{
				this.AfterCreatingGroups(this, e);
			}
		}

		protected virtual void OnAfterSearching(AfterSearchingEventArgs e)
		{
			if (this.AfterSearching != null)
			{
				this.AfterSearching(this, e);
			}
		}

		protected virtual void OnAfterSorting(AfterSortingEventArgs e)
		{
			if (this.AfterSorting != null)
			{
				this.AfterSorting(this, e);
			}
		}

		protected virtual void OnBeforeSearching(BeforeSearchingEventArgs e)
		{
			if (this.BeforeSearching != null)
			{
				this.BeforeSearching(this, e);
			}
		}

		protected virtual void OnBeforeSorting(BeforeSortingEventArgs e)
		{
			if (this.BeforeSorting != null)
			{
				this.BeforeSorting(this, e);
			}
		}

		protected virtual void OnCanDrop(OlvDropEventArgs args)
		{
			if (this.CanDrop != null)
			{
				this.CanDrop(this, args);
			}
		}

		protected virtual void OnCellClick(CellClickEventArgs args)
		{
			if (this.CellClick != null)
			{
				this.CellClick(this, args);
			}
		}

		protected virtual void OnCellOver(CellOverEventArgs args)
		{
			if (this.CellOver != null)
			{
				this.CellOver(this, args);
			}
		}

		protected virtual void OnCellRightClick(CellRightClickEventArgs args)
		{
			if (this.CellRightClick != null)
			{
				this.CellRightClick(this, args);
			}
		}

		protected virtual void OnCellToolTip(ToolTipShowingEventArgs args)
		{
			if (this.CellToolTipShowing != null)
			{
				this.CellToolTipShowing(this, args);
			}
		}

		protected virtual void OnSubItemChecking(SubItemCheckingEventArgs args)
		{
			if (this.SubItemChecking != null)
			{
				this.SubItemChecking(this, args);
			}
		}

		protected virtual void OnColumnRightClick(ColumnClickEventArgs e)
		{
			if (this.ColumnRightClick != null)
			{
				this.ColumnRightClick(this, e);
			}
		}

		protected virtual void OnDropped(OlvDropEventArgs args)
		{
			if (this.Dropped != null)
			{
				this.Dropped(this, args);
			}
		}

		protected virtual void OnFilter(FilterEventArgs e)
		{
			if (this.Filter != null)
			{
				this.Filter(this, e);
			}
		}

		protected virtual void OnFormatCell(FormatCellEventArgs args)
		{
			if (this.FormatCell != null)
			{
				this.FormatCell(this, args);
			}
		}

		protected virtual void OnFormatRow(FormatRowEventArgs args)
		{
			if (this.FormatRow != null)
			{
				this.FormatRow(this, args);
			}
		}

		protected virtual void OnFreezing(FreezeEventArgs args)
		{
			if (this.Freezing != null)
			{
				this.Freezing(this, args);
			}
		}

		protected virtual void OnGroupExpandingCollapsing(GroupExpandingCollapsingEventArgs args)
		{
			if (this.GroupExpandingCollapsing != null)
			{
				this.GroupExpandingCollapsing(this, args);
			}
		}

		protected virtual void OnGroupStateChanged(GroupStateChangedEventArgs args)
		{
			if (this.GroupStateChanged != null)
			{
				this.GroupStateChanged(this, args);
			}
		}

		protected virtual void OnHeaderToolTip(ToolTipShowingEventArgs args)
		{
			if (this.HeaderToolTipShowing != null)
			{
				this.HeaderToolTipShowing(this, args);
			}
		}

		protected virtual void OnHotItemChanged(HotItemChangedEventArgs e)
		{
			if (this.HotItemChanged != null)
			{
				this.HotItemChanged(this, e);
			}
		}

		protected virtual void OnHyperlinkClicked(HyperlinkClickedEventArgs e)
		{
			if (this.HyperlinkClicked != null)
			{
				this.HyperlinkClicked(this, e);
			}
		}

		protected virtual void OnGroupTaskClicked(GroupTaskClickedEventArgs e)
		{
			if (this.GroupTaskClicked != null)
			{
				this.GroupTaskClicked(this, e);
			}
		}

		protected virtual void OnIsHyperlink(IsHyperlinkEventArgs e)
		{
			if (this.IsHyperlink != null)
			{
				this.IsHyperlink(this, e);
			}
		}

		protected virtual void OnItemsAdding(ItemsAddingEventArgs e)
		{
			if (this.ItemsAdding != null)
			{
				this.ItemsAdding(this, e);
			}
		}

		protected virtual void OnItemsChanged(ItemsChangedEventArgs e)
		{
			if (this.ItemsChanged != null)
			{
				this.ItemsChanged(this, e);
			}
		}

		protected virtual void OnItemsChanging(ItemsChangingEventArgs e)
		{
			if (this.ItemsChanging != null)
			{
				this.ItemsChanging(this, e);
			}
		}

		protected virtual void OnItemsRemoving(ItemsRemovingEventArgs e)
		{
			if (this.ItemsRemoving != null)
			{
				this.ItemsRemoving(this, e);
			}
		}

		protected virtual void OnModelCanDrop(ModelDropEventArgs args)
		{
			if (this.ModelCanDrop != null)
			{
				this.ModelCanDrop(this, args);
			}
		}

		protected virtual void OnModelDropped(ModelDropEventArgs args)
		{
			if (this.ModelDropped != null)
			{
				this.ModelDropped(this, args);
			}
		}

		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, e);
			}
		}

		protected virtual void OnScroll(ScrollEventArgs e)
		{
			if (this.Scroll != null)
			{
				this.Scroll(this, e);
			}
		}

		protected virtual void OnCellEditStarting(CellEditEventArgs e)
		{
			if (this.CellEditStarting != null)
			{
				this.CellEditStarting(this, e);
			}
		}

		protected virtual void OnCellEditorValidating(CellEditEventArgs e)
		{
			if (Environment.TickCount - this.lastValidatingEvent < 100)
			{
				e.Cancel = true;
			}
			else
			{
				this.lastValidatingEvent = Environment.TickCount;
				if (this.CellEditValidating != null)
				{
					this.CellEditValidating(this, e);
				}
			}
			this.lastValidatingEvent = Environment.TickCount;
		}

		protected virtual void OnCellEditFinishing(CellEditEventArgs e)
		{
			if (this.CellEditFinishing != null)
			{
				this.CellEditFinishing(this, e);
			}
		}

		public ObjectListView()
		{
			base.ColumnClick += this.HandleColumnClick;
			base.Layout += this.HandleLayout;
			base.ColumnWidthChanging += this.HandleColumnWidthChanging;
			base.ColumnWidthChanged += this.HandleColumnWidthChanged;
			base.View = View.Details;
			this.DoubleBuffered = true;
			this.ShowSortIndicators = true;
			this.InitializeStandardOverlays();
			this.InitializeEmptyListMsgOverlay();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!disposing)
			{
				return;
			}
			foreach (GlassPanelForm glassPanelForm in this.glassPanels)
			{
				glassPanelForm.Unbind();
				glassPanelForm.Dispose();
			}
			this.glassPanels.Clear();
			this.UnsubscribeNotifications(null);
		}

		public static bool IsVistaOrLater
		{
			get
			{
				if (ObjectListView.sIsVistaOrLater == null)
				{
					ObjectListView.sIsVistaOrLater = new bool?(Environment.OSVersion.Version.Major >= 6);
				}
				return ObjectListView.sIsVistaOrLater.Value;
			}
		}

		public static bool IsWin7OrLater
		{
			get
			{
				if (ObjectListView.sIsWin7OrLater == null)
				{
					Version version = Environment.OSVersion.Version;
					ObjectListView.sIsWin7OrLater = new bool?(version.Major > 6 || (version.Major == 6 && version.Minor > 0));
				}
				return ObjectListView.sIsWin7OrLater.Value;
			}
		}

		public static SmoothingMode SmoothingMode
		{
			get
			{
				return ObjectListView.sSmoothingMode;
			}
			set
			{
				ObjectListView.sSmoothingMode = value;
			}
		}

		public static TextRenderingHint TextRenderingHint
		{
			get
			{
				return ObjectListView.sTextRendereringHint;
			}
			set
			{
				ObjectListView.sTextRendereringHint = value;
			}
		}

		public static string GroupTitleDefault
		{
			get
			{
				return ObjectListView.sGroupTitleDefault;
			}
			set
			{
				ObjectListView.sGroupTitleDefault = ((value == null) ? ObjectListView.getString_0(107346616) : value);
			}
		}

		public static ArrayList EnumerableToArray(IEnumerable collection, bool alwaysCreate)
		{
			if (collection == null)
			{
				return new ArrayList();
			}
			if (!alwaysCreate)
			{
				ArrayList arrayList = collection as ArrayList;
				if (arrayList != null)
				{
					return arrayList;
				}
				IList list = collection as IList;
				if (list != null)
				{
					return ArrayList.Adapter(list);
				}
			}
			ICollection collection2 = collection as ICollection;
			if (collection2 != null)
			{
				return new ArrayList(collection2);
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (object value in collection)
			{
				arrayList2.Add(value);
			}
			return arrayList2;
		}

		public static int EnumerableCount(IEnumerable collection)
		{
			if (collection == null)
			{
				return 0;
			}
			ICollection collection2 = collection as ICollection;
			if (collection2 != null)
			{
				return collection2.Count;
			}
			int num = 0;
			foreach (object obj in collection)
			{
				num++;
			}
			return num;
		}

		public static bool IsEnumerableEmpty(IEnumerable collection)
		{
			return collection == null || collection is string || !collection.GetEnumerator().MoveNext();
		}

		public static bool IgnoreMissingAspects
		{
			get
			{
				return Munger.IgnoreMissingAspects;
			}
			set
			{
				Munger.IgnoreMissingAspects = value;
			}
		}

		public static bool ShowCellPaddingBounds
		{
			get
			{
				return ObjectListView.showCellPaddingBounds;
			}
			set
			{
				ObjectListView.showCellPaddingBounds = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IModelFilter AdditionalFilter
		{
			get
			{
				return this.additionalFilter;
			}
			set
			{
				if (this.additionalFilter == value)
				{
					return;
				}
				this.additionalFilter = value;
				this.UpdateColumnFiltering();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public virtual List<OLVColumn> AllColumns
		{
			get
			{
				return this.allColumns;
			}
			set
			{
				this.allColumns = (value ?? new List<OLVColumn>());
			}
		}

		[DefaultValue(typeof(Color), "")]
		[Category("ObjectListView")]
		[Description("If using alternate colors, what color should the background of alterate rows be?")]
		public Color AlternateRowBackColor
		{
			get
			{
				return this.alternateRowBackColor;
			}
			set
			{
				this.alternateRowBackColor = value;
			}
		}

		[Browsable(false)]
		public virtual Color AlternateRowBackColorOrDefault
		{
			get
			{
				if (!(this.alternateRowBackColor == Color.Empty))
				{
					return this.alternateRowBackColor;
				}
				return Color.LemonChiffon;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual OLVColumn AlwaysGroupByColumn
		{
			get
			{
				return this.alwaysGroupByColumn;
			}
			set
			{
				this.alwaysGroupByColumn = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual SortOrder AlwaysGroupBySortOrder
		{
			get
			{
				return this.alwaysGroupBySortOrder;
			}
			set
			{
				this.alwaysGroupBySortOrder = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ImageList BaseSmallImageList
		{
			get
			{
				return base.SmallImageList;
			}
			set
			{
				base.SmallImageList = value;
			}
		}

		[DefaultValue(ObjectListView.CellEditActivateMode.None)]
		[Category("ObjectListView")]
		[Description("How does the user indicate that they want to edit a cell?")]
		public virtual ObjectListView.CellEditActivateMode CellEditActivation
		{
			get
			{
				return this.cellEditActivation;
			}
			set
			{
				this.cellEditActivation = value;
				if (base.Created)
				{
					base.Invalidate();
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CellEditKeyEngine CellEditKeyEngine
		{
			get
			{
				CellEditKeyEngine result;
				if ((result = this.cellEditKeyEngine) == null)
				{
					result = (this.cellEditKeyEngine = new CellEditKeyEngine());
				}
				return result;
			}
			set
			{
				this.cellEditKeyEngine = value;
			}
		}

		[Browsable(false)]
		public Control CellEditor
		{
			get
			{
				return this.cellEditor;
			}
		}

		[Description("Should Tab/Shift-Tab change rows while cell editing?")]
		[DefaultValue(false)]
		[Category("ObjectListView")]
		public virtual bool CellEditTabChangesRows
		{
			get
			{
				return this.cellEditTabChangesRows;
			}
			set
			{
				this.cellEditTabChangesRows = value;
				if (this.cellEditTabChangesRows)
				{
					this.CellEditKeyEngine.SetKeyBehaviour(Keys.Tab, CellEditCharacterBehaviour.ChangeColumnRight, CellEditAtEdgeBehaviour.ChangeRow);
					this.CellEditKeyEngine.SetKeyBehaviour(Keys.LButton | Keys.Back | Keys.Shift, CellEditCharacterBehaviour.ChangeColumnLeft, CellEditAtEdgeBehaviour.ChangeRow);
					return;
				}
				this.CellEditKeyEngine.SetKeyBehaviour(Keys.Tab, CellEditCharacterBehaviour.ChangeColumnRight, CellEditAtEdgeBehaviour.Wrap);
				this.CellEditKeyEngine.SetKeyBehaviour(Keys.LButton | Keys.Back | Keys.Shift, CellEditCharacterBehaviour.ChangeColumnLeft, CellEditAtEdgeBehaviour.Wrap);
			}
		}

		[Category("ObjectListView")]
		[Description("Should Enter change rows while cell editing?")]
		[DefaultValue(false)]
		public virtual bool CellEditEnterChangesRows
		{
			get
			{
				return this.cellEditEnterChangesRows;
			}
			set
			{
				this.cellEditEnterChangesRows = value;
				if (this.cellEditEnterChangesRows)
				{
					this.CellEditKeyEngine.SetKeyBehaviour(Keys.Return, CellEditCharacterBehaviour.ChangeRowDown, CellEditAtEdgeBehaviour.ChangeColumn);
					this.CellEditKeyEngine.SetKeyBehaviour(Keys.LButton | Keys.MButton | Keys.Back | Keys.Shift, CellEditCharacterBehaviour.ChangeRowUp, CellEditAtEdgeBehaviour.ChangeColumn);
					return;
				}
				this.CellEditKeyEngine.SetKeyBehaviour(Keys.Return, CellEditCharacterBehaviour.EndEdit, CellEditAtEdgeBehaviour.EndEdit);
				this.CellEditKeyEngine.SetKeyBehaviour(Keys.LButton | Keys.MButton | Keys.Back | Keys.Shift, CellEditCharacterBehaviour.EndEdit, CellEditAtEdgeBehaviour.EndEdit);
			}
		}

		[Browsable(false)]
		public ToolTipControl CellToolTip
		{
			get
			{
				if (this.cellToolTip == null)
				{
					this.CreateCellToolTip();
				}
				return this.cellToolTip;
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("How much padding will be applied to each cell in this control?")]
		public Rectangle? CellPadding
		{
			get
			{
				return this.cellPadding;
			}
			set
			{
				this.cellPadding = value;
			}
		}

		[Category("ObjectListView")]
		[Description("How will cell values be vertically aligned?")]
		[DefaultValue(StringAlignment.Center)]
		public virtual StringAlignment CellVerticalAlignment
		{
			get
			{
				return this.cellVerticalAlignment;
			}
			set
			{
				this.cellVerticalAlignment = value;
			}
		}

		public new bool CheckBoxes
		{
			get
			{
				return base.CheckBoxes;
			}
			set
			{
				base.CheckBoxes = value;
				this.InitializeStateImageList();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual object CheckedObject
		{
			get
			{
				IList checkedObjects = this.CheckedObjects;
				if (checkedObjects.Count != 1)
				{
					return null;
				}
				return checkedObjects[0];
			}
			set
			{
				this.CheckedObjects = new ArrayList(new object[]
				{
					value
				});
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IList CheckedObjects
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				if (this.CheckBoxes)
				{
					for (int i = 0; i < this.GetItemCount(); i++)
					{
						OLVListItem item = this.GetItem(i);
						if (item.CheckState == CheckState.Checked)
						{
							arrayList.Add(item.RowObject);
						}
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
				foreach (object obj in this.Objects)
				{
					this.SetObjectCheckedness(obj, hashtable.ContainsKey(obj) ? CheckState.Checked : CheckState.Unchecked);
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IEnumerable CheckedObjectsEnumerable
		{
			get
			{
				return this.CheckedObjects;
			}
			set
			{
				this.CheckedObjects = ObjectListView.EnumerableToArray(value, true);
			}
		}

		[Editor("BrightIdeasSoftware.Design.OLVColumnCollectionEditor", "System.Drawing.Design.UITypeEditor")]
		public new ListView.ColumnHeaderCollection Columns
		{
			get
			{
				return base.Columns;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use GetFilteredColumns() and OLVColumn.IsTileViewColumn instead")]
		public List<OLVColumn> ColumnsForTileView
		{
			get
			{
				return this.GetFilteredColumns(View.Tile);
			}
		}

		[Browsable(false)]
		public virtual List<OLVColumn> ColumnsInDisplayOrder
		{
			get
			{
				OLVColumn[] array = new OLVColumn[this.Columns.Count];
				foreach (object obj in this.Columns)
				{
					OLVColumn olvcolumn = (OLVColumn)obj;
					array[olvcolumn.DisplayIndex] = olvcolumn;
				}
				return new List<OLVColumn>(array);
			}
		}

		[Browsable(false)]
		public Rectangle ContentRectangle
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				if ((this.View == View.Details || this.ShowHeaderInAllViews) && this.HeaderControl != null)
				{
					Rectangle rectangle = default(Rectangle);
					NativeMethods.GetClientRect(this.HeaderControl.Handle, ref rectangle);
					clientRectangle.Y = rectangle.Height;
					clientRectangle.Height -= rectangle.Height;
				}
				return clientRectangle;
			}
		}

		[DefaultValue(true)]
		[Description("Should the control copy the selection to the clipboard when the user presses Ctrl-C?")]
		[Category("ObjectListView")]
		public virtual bool CopySelectionOnControlC
		{
			get
			{
				return this.copySelectionOnControlC;
			}
			set
			{
				this.copySelectionOnControlC = value;
			}
		}

		[Description("Should the Ctrl-C copy process use the DragSource to create the Clipboard data object?")]
		[Category("ObjectListView")]
		[DefaultValue(true)]
		public bool CopySelectionOnControlCUsesDragSource
		{
			get
			{
				return this.copySelectionOnControlCUsesDragSource;
			}
			set
			{
				this.copySelectionOnControlCUsesDragSource = value;
			}
		}

		[Browsable(false)]
		protected IList<IDecoration> Decorations
		{
			get
			{
				return this.decorations;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IRenderer DefaultRenderer
		{
			get
			{
				return this.defaultRenderer;
			}
			set
			{
				this.defaultRenderer = (value ?? new BaseRenderer());
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDragSource DragSource
		{
			get
			{
				return this.dragSource;
			}
			set
			{
				this.dragSource = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IDropSink DropSink
		{
			get
			{
				return this.dropSink;
			}
			set
			{
				if (this.dropSink == value)
				{
					return;
				}
				SimpleDropSink simpleDropSink = this.dropSink as SimpleDropSink;
				if (simpleDropSink != null)
				{
					simpleDropSink.CanDrop -= this.DropSinkCanDrop;
					simpleDropSink.Dropped -= this.DropSinkDropped;
					simpleDropSink.ModelCanDrop -= this.DropSinkModelCanDrop;
					simpleDropSink.ModelDropped -= this.DropSinkModelDropped;
				}
				this.dropSink = value;
				this.AllowDrop = (value != null);
				if (this.dropSink != null)
				{
					this.dropSink.ListView = this;
				}
				SimpleDropSink simpleDropSink2 = value as SimpleDropSink;
				if (simpleDropSink2 != null)
				{
					simpleDropSink2.CanDrop += this.DropSinkCanDrop;
					simpleDropSink2.Dropped += this.DropSinkDropped;
					simpleDropSink2.ModelCanDrop += this.DropSinkModelCanDrop;
					simpleDropSink2.ModelDropped += this.DropSinkModelDropped;
				}
			}
		}

		private void DropSinkCanDrop(object sender, OlvDropEventArgs e)
		{
			this.OnCanDrop(e);
		}

		private void DropSinkDropped(object sender, OlvDropEventArgs e)
		{
			this.OnDropped(e);
		}

		private void DropSinkModelCanDrop(object sender, ModelDropEventArgs e)
		{
			this.OnModelCanDrop(e);
		}

		private void DropSinkModelDropped(object sender, ModelDropEventArgs e)
		{
			this.OnModelDropped(e);
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Localizable(true)]
		[Description("When the list has no items, show this message in the control")]
		public virtual string EmptyListMsg
		{
			get
			{
				TextOverlay textOverlay = this.EmptyListMsgOverlay as TextOverlay;
				if (textOverlay != null)
				{
					return textOverlay.Text;
				}
				return null;
			}
			set
			{
				TextOverlay textOverlay = this.EmptyListMsgOverlay as TextOverlay;
				if (textOverlay != null)
				{
					textOverlay.Text = value;
					base.Invalidate();
				}
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(null)]
		[Description("What font should the 'list empty' message be drawn in?")]
		public virtual Font EmptyListMsgFont
		{
			get
			{
				TextOverlay textOverlay = this.EmptyListMsgOverlay as TextOverlay;
				if (textOverlay != null)
				{
					return textOverlay.Font;
				}
				return null;
			}
			set
			{
				TextOverlay textOverlay = this.EmptyListMsgOverlay as TextOverlay;
				if (textOverlay != null)
				{
					textOverlay.Font = value;
				}
			}
		}

		[Browsable(false)]
		public virtual Font EmptyListMsgFontOrDefault
		{
			get
			{
				return this.EmptyListMsgFont ?? new Font(ObjectListView.getString_0(107315560), 14f);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IOverlay EmptyListMsgOverlay
		{
			get
			{
				return this.emptyListMsgOverlay;
			}
			set
			{
				if (this.emptyListMsgOverlay != value)
				{
					this.emptyListMsgOverlay = value;
					base.Invalidate();
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IEnumerable FilteredObjects
		{
			get
			{
				if (this.IsFiltering)
				{
					return this.FilterObjects(this.Objects, this.ModelFilter, this.ListFilter);
				}
				return this.Objects;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FilterMenuBuilder FilterMenuBuildStrategy
		{
			get
			{
				return this.filterMenuBuilder;
			}
			set
			{
				this.filterMenuBuilder = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new ListViewGroupCollection Groups
		{
			get
			{
				return base.Groups;
			}
		}

		[Description("The image list from which group header will take their images")]
		[DefaultValue(null)]
		[Category("ObjectListView")]
		public ImageList GroupImageList
		{
			get
			{
				return this.groupImageList;
			}
			set
			{
				this.groupImageList = value;
				if (base.Created)
				{
					NativeMethods.SetGroupImageList(this, value);
				}
			}
		}

		[Localizable(true)]
		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("The format to use when suffixing item counts to group titles")]
		public virtual string GroupWithItemCountFormat
		{
			get
			{
				return this.groupWithItemCountFormat;
			}
			set
			{
				this.groupWithItemCountFormat = value;
			}
		}

		[Browsable(false)]
		public virtual string GroupWithItemCountFormatOrDefault
		{
			get
			{
				if (!string.IsNullOrEmpty(this.GroupWithItemCountFormat))
				{
					return this.GroupWithItemCountFormat;
				}
				return ObjectListView.getString_0(107315551);
			}
		}

		[Localizable(true)]
		[Description("The format to use when suffixing item counts to group titles")]
		[DefaultValue(null)]
		[Category("ObjectListView")]
		public virtual string GroupWithItemCountSingularFormat
		{
			get
			{
				return this.groupWithItemCountSingularFormat;
			}
			set
			{
				this.groupWithItemCountSingularFormat = value;
			}
		}

		[Browsable(false)]
		public virtual string GroupWithItemCountSingularFormatOrDefault
		{
			get
			{
				if (!string.IsNullOrEmpty(this.GroupWithItemCountSingularFormat))
				{
					return this.GroupWithItemCountSingularFormat;
				}
				return ObjectListView.getString_0(107315562);
			}
		}

		[DefaultValue(true)]
		[Browsable(true)]
		[Category("ObjectListView")]
		[Description("Should the groups in this control be collapsible (Vista and later only).")]
		public bool HasCollapsibleGroups
		{
			get
			{
				return this.hasCollapsibleGroups;
			}
			set
			{
				this.hasCollapsibleGroups = value;
			}
		}

		[Browsable(false)]
		public virtual bool HasEmptyListMsg
		{
			get
			{
				return !string.IsNullOrEmpty(this.EmptyListMsg);
			}
		}

		[Browsable(false)]
		public bool HasOverlays
		{
			get
			{
				return this.Overlays.Count > 2 || this.imageOverlay.Image != null || !string.IsNullOrEmpty(this.textOverlay.Text);
			}
		}

		[Browsable(false)]
		public HeaderControl HeaderControl
		{
			get
			{
				HeaderControl result;
				if ((result = this.headerControl) == null)
				{
					result = (this.headerControl = new HeaderControl(this));
				}
				return result;
			}
		}

		[Obsolete("Use a HeaderFormatStyle instead", false)]
		[DefaultValue(null)]
		[Browsable(false)]
		public Font HeaderFont
		{
			get
			{
				if (this.HeaderFormatStyle != null)
				{
					return this.HeaderFormatStyle.Normal.Font;
				}
				return null;
			}
			set
			{
				if (value == null && this.HeaderFormatStyle == null)
				{
					return;
				}
				if (this.HeaderFormatStyle == null)
				{
					this.HeaderFormatStyle = new HeaderFormatStyle();
				}
				this.HeaderFormatStyle.SetFont(value);
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(null)]
		[Description("What style will be used to draw the control's header")]
		public HeaderFormatStyle HeaderFormatStyle
		{
			get
			{
				return this.headerFormatStyle;
			}
			set
			{
				this.headerFormatStyle = value;
			}
		}

		[DefaultValue(-1)]
		[Category("ObjectListView")]
		[Description("What is the maximum height of the header? -1 means no maximum")]
		public int HeaderMaximumHeight
		{
			get
			{
				return this.headerMaximumHeight;
			}
			set
			{
				this.headerMaximumHeight = value;
			}
		}

		[Description("Will the column headers be drawn strictly according to OS theme?")]
		[Category("ObjectListView")]
		[DefaultValue(true)]
		public bool HeaderUsesThemes
		{
			get
			{
				return this.headerUsesThemes;
			}
			set
			{
				this.headerUsesThemes = value;
			}
		}

		[Description("Will the text of the column headers be word wrapped?")]
		[DefaultValue(false)]
		[Category("ObjectListView")]
		public bool HeaderWordWrap
		{
			get
			{
				return this.headerWordWrap;
			}
			set
			{
				this.headerWordWrap = value;
				if (this.headerControl != null)
				{
					this.headerControl.WordWrap = value;
				}
			}
		}

		[Browsable(false)]
		public ToolTipControl HeaderToolTip
		{
			get
			{
				return this.HeaderControl.ToolTip;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int HotRowIndex
		{
			get
			{
				return this.hotRowIndex;
			}
			protected set
			{
				this.hotRowIndex = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int HotColumnIndex
		{
			get
			{
				return this.hotColumnIndex;
			}
			protected set
			{
				this.hotColumnIndex = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual HitTestLocation HotCellHitLocation
		{
			get
			{
				return this.hotCellHitLocation;
			}
			protected set
			{
				this.hotCellHitLocation = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HitTestLocationEx HotCellHitLocationEx
		{
			get
			{
				return this.hotCellHitLocationEx;
			}
			protected set
			{
				this.hotCellHitLocationEx = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OLVGroup HotGroup
		{
			get
			{
				return this.hotGroup;
			}
			internal set
			{
				this.hotGroup = value;
			}
		}

		[Obsolete("Use HotRowIndex instead", false)]
		[Browsable(false)]
		public virtual int HotItemIndex
		{
			get
			{
				return this.HotRowIndex;
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("How should the row under the cursor be highlighted")]
		public virtual HotItemStyle HotItemStyle
		{
			get
			{
				return this.hotItemStyle;
			}
			set
			{
				if (this.HotItemStyle != null)
				{
					this.RemoveOverlay(this.HotItemStyle.Overlay);
				}
				this.hotItemStyle = value;
				if (this.HotItemStyle != null)
				{
					this.AddOverlay(this.HotItemStyle.Overlay);
				}
			}
		}

		[Category("ObjectListView")]
		[Description("How should hyperlinks be drawn")]
		[DefaultValue(null)]
		public virtual HyperlinkStyle HyperlinkStyle
		{
			get
			{
				return this.hyperlinkStyle;
			}
			set
			{
				this.hyperlinkStyle = value;
			}
		}

		[Description("The background foregroundColor of selected rows when the control is owner drawn")]
		[Category("ObjectListView")]
		[DefaultValue(typeof(Color), "")]
		public virtual Color HighlightBackgroundColor
		{
			get
			{
				return this.highlightBackgroundColor;
			}
			set
			{
				this.highlightBackgroundColor = value;
			}
		}

		[Browsable(false)]
		public virtual Color HighlightBackgroundColorOrDefault
		{
			get
			{
				if (!this.HighlightBackgroundColor.IsEmpty)
				{
					return this.HighlightBackgroundColor;
				}
				return SystemColors.Highlight;
			}
		}

		[DefaultValue(typeof(Color), "")]
		[Category("ObjectListView")]
		[Description("The foreground foregroundColor of selected rows when the control is owner drawn")]
		public virtual Color HighlightForegroundColor
		{
			get
			{
				return this.highlightForegroundColor;
			}
			set
			{
				this.highlightForegroundColor = value;
			}
		}

		[Browsable(false)]
		public virtual Color HighlightForegroundColorOrDefault
		{
			get
			{
				if (!this.HighlightForegroundColor.IsEmpty)
				{
					return this.HighlightForegroundColor;
				}
				return SystemColors.HighlightText;
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("When rows are copied or dragged, will data in hidden columns be included in the text? If this is false, only visible columns will be included.")]
		public virtual bool IncludeHiddenColumnsInDataTransfer
		{
			get
			{
				return this.includeHiddenColumnsInDataTransfer;
			}
			set
			{
				this.includeHiddenColumnsInDataTransfer = value;
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("When rows are copied, will column headers be in the text?.")]
		public virtual bool IncludeColumnHeadersInCopy
		{
			get
			{
				return this.includeColumnHeadersInCopy;
			}
			set
			{
				this.includeColumnHeadersInCopy = value;
			}
		}

		[Browsable(false)]
		public virtual bool IsCellEditing
		{
			get
			{
				return this.cellEditor != null;
			}
		}

		[Browsable(false)]
		public virtual bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsFiltering
		{
			get
			{
				return this.UseFiltering && (this.ModelFilter != null || this.ListFilter != null);
			}
		}

		[Description("When the user types into a list, should the values in the current sort column be searched to find a match?")]
		[DefaultValue(true)]
		[Category("ObjectListView")]
		public virtual bool IsSearchOnSortColumn
		{
			get
			{
				return this.isSearchOnSortColumn;
			}
			set
			{
				this.isSearchOnSortColumn = value;
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("Should this control will use a SimpleDropSink to receive drops.")]
		public virtual bool IsSimpleDropSink
		{
			get
			{
				return this.DropSink != null;
			}
			set
			{
				this.DropSink = (value ? new SimpleDropSink() : null);
			}
		}

		[Description("Should this control use a SimpleDragSource to initiate drags out from this control")]
		[DefaultValue(false)]
		[Category("ObjectListView")]
		public virtual bool IsSimpleDragSource
		{
			get
			{
				return this.DragSource != null;
			}
			set
			{
				this.DragSource = (value ? new SimpleDragSource() : null);
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ListView.ListViewItemCollection Items
		{
			get
			{
				return base.Items;
			}
		}

		[Description("The owner drawn renderer that draws items when the list is in non-Details view.")]
		[Category("ObjectListView")]
		[DefaultValue(null)]
		public IRenderer ItemRenderer
		{
			get
			{
				return this.itemRenderer;
			}
			set
			{
				this.itemRenderer = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual OLVColumn LastSortColumn
		{
			get
			{
				return this.PrimarySortColumn;
			}
			set
			{
				this.PrimarySortColumn = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual SortOrder LastSortOrder
		{
			get
			{
				return this.PrimarySortOrder;
			}
			set
			{
				this.PrimarySortOrder = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IListFilter ListFilter
		{
			get
			{
				return this.listFilter;
			}
			set
			{
				this.listFilter = value;
				if (this.UseFiltering)
				{
					this.UpdateFiltering();
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IModelFilter ModelFilter
		{
			get
			{
				return this.modelFilter;
			}
			set
			{
				this.modelFilter = value;
				if (this.UseFiltering)
				{
					this.UpdateFiltering();
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual OlvListViewHitTestInfo MouseMoveHitTest
		{
			get
			{
				return this.mouseMoveHitTest;
			}
			private set
			{
				this.mouseMoveHitTest = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IList<OLVGroup> OLVGroups
		{
			get
			{
				return this.olvGroups;
			}
			set
			{
				this.olvGroups = value;
			}
		}

		[DefaultValue(false)]
		[Description("Should the DrawColumnHeader event be triggered")]
		[Category("ObjectListView")]
		public bool OwnerDrawnHeader
		{
			get
			{
				return this.ownerDrawnHeader;
			}
			set
			{
				this.ownerDrawnHeader = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IEnumerable Objects
		{
			get
			{
				return this.objects;
			}
			set
			{
				this.SetObjects(value, true);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IEnumerable ObjectsForClustering
		{
			get
			{
				return this.Objects;
			}
		}

		[Category("ObjectListView")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The image that will be drawn over the top of the ListView")]
		public ImageOverlay OverlayImage
		{
			get
			{
				return this.imageOverlay;
			}
			set
			{
				if (this.imageOverlay == value)
				{
					return;
				}
				this.RemoveOverlay(this.imageOverlay);
				this.imageOverlay = value;
				this.AddOverlay(this.imageOverlay);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("ObjectListView")]
		[Description("The text that will be drawn over the top of the ListView")]
		public TextOverlay OverlayText
		{
			get
			{
				return this.textOverlay;
			}
			set
			{
				if (this.textOverlay == value)
				{
					return;
				}
				this.RemoveOverlay(this.textOverlay);
				this.textOverlay = value;
				this.AddOverlay(this.textOverlay);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int OverlayTransparency
		{
			get
			{
				return this.overlayTransparency;
			}
			set
			{
				this.overlayTransparency = Math.Min(255, Math.Max(0, value));
			}
		}

		[Browsable(false)]
		protected IList<IOverlay> Overlays
		{
			get
			{
				return this.overlays;
			}
		}

		[Description("Will primary checkboxes persistent their values across list rebuilds")]
		[Category("ObjectListView")]
		[DefaultValue(true)]
		public virtual bool PersistentCheckBoxes
		{
			get
			{
				return this.persistentCheckBoxes;
			}
			set
			{
				if (this.persistentCheckBoxes == value)
				{
					return;
				}
				this.persistentCheckBoxes = value;
				this.ClearPersistentCheckState();
			}
		}

		protected Dictionary<object, CheckState> CheckStateMap
		{
			get
			{
				Dictionary<object, CheckState> result;
				if ((result = this.checkStateMap) == null)
				{
					result = (this.checkStateMap = new Dictionary<object, CheckState>());
				}
				return result;
			}
			set
			{
				this.checkStateMap = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual OLVColumn PrimarySortColumn
		{
			get
			{
				return this.primarySortColumn;
			}
			set
			{
				this.primarySortColumn = value;
				if (this.TintSortColumn)
				{
					this.SelectedColumn = value;
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual SortOrder PrimarySortOrder
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

		[DefaultValue(false)]
		[Description("Should non-editable checkboxes be drawn as disabled?")]
		[Category("ObjectListView")]
		public virtual bool RenderNonEditableCheckboxesAsDisabled
		{
			get
			{
				return this.renderNonEditableCheckboxesAsDisabled;
			}
			set
			{
				this.renderNonEditableCheckboxesAsDisabled = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Specify the height of each row in pixels. -1 indicates default height")]
		[DefaultValue(-1)]
		public virtual int RowHeight
		{
			get
			{
				return this.rowHeight;
			}
			set
			{
				if (value < 1)
				{
					this.rowHeight = -1;
				}
				else
				{
					this.rowHeight = value;
				}
				if (base.DesignMode)
				{
					return;
				}
				this.SetupBaseImageList();
				if (this.CheckBoxes)
				{
					this.InitializeStateImageList();
				}
			}
		}

		[Browsable(false)]
		public virtual int RowHeightEffective
		{
			get
			{
				switch (this.View)
				{
				case View.LargeIcon:
					if (base.LargeImageList == null)
					{
						return this.Font.Height;
					}
					return Math.Max(base.LargeImageList.ImageSize.Height, this.Font.Height);
				case View.Details:
				case View.SmallIcon:
				case View.List:
					return Math.Max(this.SmallImageSize.Height, this.Font.Height);
				case View.Tile:
					return base.TileSize.Height;
				default:
					return 0;
				}
			}
		}

		[Browsable(false)]
		public virtual int RowsPerPage
		{
			get
			{
				return NativeMethods.GetCountPerPage(this);
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual OLVColumn SecondarySortColumn
		{
			get
			{
				return this.secondarySortColumn;
			}
			set
			{
				this.secondarySortColumn = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual SortOrder SecondarySortOrder
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

		[Category("ObjectListView")]
		[Description("Should the control select all rows when the user presses Ctrl-A?")]
		[DefaultValue(true)]
		public virtual bool SelectAllOnControlA
		{
			get
			{
				return this.selectAllOnControlA;
			}
			set
			{
				this.selectAllOnControlA = value;
			}
		}

		[DefaultValue(true)]
		[Category("ObjectListView")]
		[Description("When the user right clicks on the column headers, should a menu be presented which will allow them to choose which columns will be shown in the view?")]
		public virtual bool SelectColumnsOnRightClick
		{
			get
			{
				return this.SelectColumnsOnRightClickBehaviour != ObjectListView.ColumnSelectBehaviour.None;
			}
			set
			{
				if (value)
				{
					if (this.SelectColumnsOnRightClickBehaviour == ObjectListView.ColumnSelectBehaviour.None)
					{
						this.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.InlineMenu;
						return;
					}
				}
				else
				{
					this.SelectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.None;
				}
			}
		}

		[Description("When the user right clicks on the column headers, how will the user be able to select columns?")]
		[Category("ObjectListView")]
		[DefaultValue(ObjectListView.ColumnSelectBehaviour.InlineMenu)]
		public virtual ObjectListView.ColumnSelectBehaviour SelectColumnsOnRightClickBehaviour
		{
			get
			{
				return this.selectColumnsOnRightClickBehaviour;
			}
			set
			{
				this.selectColumnsOnRightClickBehaviour = value;
			}
		}

		[Category("ObjectListView")]
		[Description("When the column select inline menu is open, should it stay open after an item is selected?")]
		[DefaultValue(true)]
		public virtual bool SelectColumnsMenuStaysOpen
		{
			get
			{
				return this.selectColumnsMenuStaysOpen;
			}
			set
			{
				this.selectColumnsMenuStaysOpen = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OLVColumn SelectedColumn
		{
			get
			{
				return this.selectedColumn;
			}
			set
			{
				this.selectedColumn = value;
				if (value == null)
				{
					this.RemoveDecoration(this.selectedColumnDecoration);
					return;
				}
				if (!this.HasDecoration(this.selectedColumnDecoration))
				{
					this.AddDecoration(this.selectedColumnDecoration);
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IDecoration SelectedRowDecoration
		{
			get
			{
				return this.selectedRowDecoration;
			}
			set
			{
				this.selectedRowDecoration = value;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(typeof(Color), "")]
		[Description("The color that will be used to tint the selected column")]
		public virtual Color SelectedColumnTint
		{
			get
			{
				return this.selectedColumnTint;
			}
			set
			{
				this.selectedColumnTint = ((value.A == byte.MaxValue) ? Color.FromArgb(15, value) : value);
				this.selectedColumnDecoration.Tint = this.selectedColumnTint;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int SelectedIndex
		{
			get
			{
				if (base.SelectedIndices.Count != 1)
				{
					return -1;
				}
				return base.SelectedIndices[0];
			}
			set
			{
				base.SelectedIndices.Clear();
				if (value >= 0 && value < this.Items.Count)
				{
					base.SelectedIndices.Add(value);
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual OLVListItem SelectedItem
		{
			get
			{
				if (base.SelectedIndices.Count != 1)
				{
					return null;
				}
				return this.GetItem(base.SelectedIndices[0]);
			}
			set
			{
				base.SelectedIndices.Clear();
				if (value != null)
				{
					base.SelectedIndices.Add(value.Index);
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual object SelectedObject
		{
			get
			{
				if (base.SelectedIndices.Count != 1)
				{
					return null;
				}
				return this.GetModelObject(base.SelectedIndices[0]);
			}
			set
			{
				object selectedObject = this.SelectedObject;
				if (selectedObject != null && selectedObject.Equals(value))
				{
					return;
				}
				base.SelectedIndices.Clear();
				this.SelectObject(value, true);
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IList SelectedObjects
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in base.SelectedIndices)
				{
					int index = (int)obj;
					arrayList.Add(this.GetModelObject(index));
				}
				return arrayList;
			}
			set
			{
				base.SelectedIndices.Clear();
				this.SelectObjects(value);
			}
		}

		[Category("ObjectListView")]
		[Description("When the user right clicks on the column headers, should a menu be presented which will allow them to perform common tasks on the listview?")]
		[DefaultValue(false)]
		public virtual bool ShowCommandMenuOnRightClick
		{
			get
			{
				return this.showCommandMenuOnRightClick;
			}
			set
			{
				this.showCommandMenuOnRightClick = value;
			}
		}

		[DefaultValue(true)]
		[Description("If this is true, right clicking on a column header will show a Filter menu option")]
		[Category("ObjectListView")]
		public bool ShowFilterMenuOnRightClick
		{
			get
			{
				return this.showFilterMenuOnRightClick;
			}
			set
			{
				this.showFilterMenuOnRightClick = value;
			}
		}

		[Description("Should the list view show items in groups?")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public new virtual bool ShowGroups
		{
			get
			{
				return base.ShowGroups;
			}
			set
			{
				this.GroupImageList = this.GroupImageList;
				base.ShowGroups = value;
			}
		}

		[DefaultValue(true)]
		[Category("ObjectListView")]
		[Description("Should the list view show sort indicators in the column headers?")]
		public virtual bool ShowSortIndicators
		{
			get
			{
				return this.showSortIndicators;
			}
			set
			{
				this.showSortIndicators = value;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(false)]
		[Description("Should the list view show images on subitems?")]
		public virtual bool ShowImagesOnSubItems
		{
			get
			{
				return this.showImagesOnSubItems;
			}
			set
			{
				this.showImagesOnSubItems = value;
				if (base.Created)
				{
					this.ApplyExtendedStyles();
				}
				if (value && base.VirtualMode)
				{
					base.OwnerDraw = true;
				}
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("Will group titles be suffixed with a count of the items in the group?")]
		public virtual bool ShowItemCountOnGroups
		{
			get
			{
				return this.showItemCountOnGroups;
			}
			set
			{
				this.showItemCountOnGroups = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Will the control will show column headers in all views?")]
		[DefaultValue(true)]
		public bool ShowHeaderInAllViews
		{
			get
			{
				return this.showHeaderInAllViews;
			}
			set
			{
				if (this.showHeaderInAllViews == value)
				{
					return;
				}
				this.showHeaderInAllViews = value;
				if (!base.Created)
				{
					return;
				}
				if (this.showHeaderInAllViews)
				{
					this.ApplyExtendedStyles();
				}
				else
				{
					base.RecreateHandle();
				}
				if (this.View != View.Details)
				{
					View view = this.View;
					this.View = View.Details;
					this.View = view;
				}
			}
		}

		public new ImageList SmallImageList
		{
			get
			{
				return this.shadowedImageList;
			}
			set
			{
				this.shadowedImageList = value;
				if (this.UseSubItemCheckBoxes)
				{
					this.SetupSubItemCheckBoxes();
				}
				this.SetupBaseImageList();
			}
		}

		[Browsable(false)]
		public virtual Size SmallImageSize
		{
			get
			{
				if (this.BaseSmallImageList != null)
				{
					return this.BaseSmallImageList.ImageSize;
				}
				return new Size(16, 16);
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(true)]
		[Description("When the listview is grouped, should the items be sorted by the primary column? If this is false, the items will be sorted by the same column as they are grouped.")]
		public virtual bool SortGroupItemsByPrimaryColumn
		{
			get
			{
				return this.sortGroupItemsByPrimaryColumn;
			}
			set
			{
				this.sortGroupItemsByPrimaryColumn = value;
			}
		}

		[DefaultValue(0)]
		[Category("ObjectListView")]
		[Description("How many pixels of space will be between groups")]
		public virtual int SpaceBetweenGroups
		{
			get
			{
				return this.spaceBetweenGroups;
			}
			set
			{
				if (this.spaceBetweenGroups == value)
				{
					return;
				}
				this.spaceBetweenGroups = value;
				this.SetGroupSpacing();
			}
		}

		private void SetGroupSpacing()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			NativeMethods.SetGroupMetrics(this, new NativeMethods.LVGROUPMETRICS
			{
				cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.LVGROUPMETRICS)),
				mask = 1U,
				Bottom = (uint)this.SpaceBetweenGroups
			});
		}

		[Category("ObjectListView")]
		[DefaultValue(false)]
		[Description("Should the sort column show a slight tinting?")]
		public virtual bool TintSortColumn
		{
			get
			{
				return this.tintSortColumn;
			}
			set
			{
				this.tintSortColumn = value;
				if (value && this.PrimarySortColumn != null)
				{
					this.SelectedColumn = this.PrimarySortColumn;
					return;
				}
				this.SelectedColumn = null;
			}
		}

		[DefaultValue(false)]
		[Description("Should the primary column have a checkbox that behaves as a tri-state checkbox?")]
		[Category("ObjectListView")]
		public virtual bool TriStateCheckBoxes
		{
			get
			{
				return this.triStateCheckBoxes;
			}
			set
			{
				if (this.triStateCheckBoxes == value)
				{
					return;
				}
				this.triStateCheckBoxes = value;
				if (value && !this.CheckBoxes)
				{
					this.CheckBoxes = true;
				}
				this.InitializeStateImageList();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int TopItemIndex
		{
			get
			{
				if (this.View == View.Details && base.IsHandleCreated)
				{
					return NativeMethods.GetTopIndex(this);
				}
				return -1;
			}
			set
			{
				int num = Math.Min(value, this.GetItemCount() - 1);
				if (this.View == View.Details && num >= 0)
				{
					try
					{
						base.TopItem = this.Items[num];
						if (base.TopItem != null && base.TopItem.Index != num)
						{
							base.TopItem = this.GetItem(num);
						}
					}
					catch (NullReferenceException)
					{
					}
					return;
				}
			}
		}

		[Category("ObjectListView")]
		[Description("When resizing a column by dragging its divider, should any space filling columns be resized at each mouse move?")]
		[DefaultValue(true)]
		public virtual bool UpdateSpaceFillingColumnsWhenDraggingColumnDivider
		{
			get
			{
				return this.updateSpaceFillingColumnsWhenDraggingColumnDivider;
			}
			set
			{
				this.updateSpaceFillingColumnsWhenDraggingColumnDivider = value;
			}
		}

		[DefaultValue(typeof(Color), "")]
		[Category("ObjectListView")]
		[Description("The background color of selected rows when the control is owner drawn and doesn't have the focus")]
		public virtual Color UnfocusedHighlightBackgroundColor
		{
			get
			{
				return this.unfocusedHighlightBackgroundColor;
			}
			set
			{
				this.unfocusedHighlightBackgroundColor = value;
			}
		}

		[Browsable(false)]
		public virtual Color UnfocusedHighlightBackgroundColorOrDefault
		{
			get
			{
				if (!this.UnfocusedHighlightBackgroundColor.IsEmpty)
				{
					return this.UnfocusedHighlightBackgroundColor;
				}
				return SystemColors.Control;
			}
		}

		[DefaultValue(typeof(Color), "")]
		[Description("The foreground color of selected rows when the control is owner drawn and doesn't have the focus")]
		[Category("ObjectListView")]
		public virtual Color UnfocusedHighlightForegroundColor
		{
			get
			{
				return this.unfocusedHighlightForegroundColor;
			}
			set
			{
				this.unfocusedHighlightForegroundColor = value;
			}
		}

		[Browsable(false)]
		public virtual Color UnfocusedHighlightForegroundColorOrDefault
		{
			get
			{
				if (!this.UnfocusedHighlightForegroundColor.IsEmpty)
				{
					return this.UnfocusedHighlightForegroundColor;
				}
				return SystemColors.ControlText;
			}
		}

		[Description("Should the list view use a different backcolor to alternate rows?")]
		[DefaultValue(false)]
		[Category("ObjectListView")]
		public virtual bool UseAlternatingBackColors
		{
			get
			{
				return this.useAlternatingBackColors;
			}
			set
			{
				this.useAlternatingBackColors = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Should FormatCell events be triggered to every cell that is built?")]
		[DefaultValue(false)]
		public bool UseCellFormatEvents
		{
			get
			{
				return this.useCellFormatEvents;
			}
			set
			{
				this.useCellFormatEvents = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Should the selected row be drawn with non-standard foreground and background colors?")]
		[DefaultValue(false)]
		public bool UseCustomSelectionColors
		{
			get
			{
				return this.useCustomSelectionColors;
			}
			set
			{
				this.useCustomSelectionColors = value;
				if (!base.DesignMode && value)
				{
					base.OwnerDraw = true;
				}
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("Should the list use the same hot item and selection mechanism as Vista?")]
		public bool UseExplorerTheme
		{
			get
			{
				return this.useExplorerTheme;
			}
			set
			{
				this.useExplorerTheme = value;
				if (base.Created)
				{
					NativeMethods.SetWindowTheme(base.Handle, value ? ObjectListView.getString_0(107315541) : ObjectListView.getString_0(107402662), null);
				}
			}
		}

		[Category("ObjectListView")]
		[Description("Should the list enable filtering?")]
		[DefaultValue(false)]
		public virtual bool UseFiltering
		{
			get
			{
				return this.useFiltering;
			}
			set
			{
				if (this.useFiltering == value)
				{
					return;
				}
				this.useFiltering = value;
				this.UpdateFiltering();
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(false)]
		[Description("Should an image be drawn in a column's header when that column is being used for filtering?")]
		public virtual bool UseFilterIndicator
		{
			get
			{
				return this.useFilterIndicator;
			}
			set
			{
				if (this.useFilterIndicator == value)
				{
					return;
				}
				this.useFilterIndicator = value;
				if (this.useFilterIndicator)
				{
					this.HeaderUsesThemes = false;
				}
				base.Invalidate();
			}
		}

		[Category("ObjectListView")]
		[Description("Should HotTracking be used? Hot tracking applies special formatting to the row under the cursor")]
		[DefaultValue(false)]
		public bool UseHotItem
		{
			get
			{
				return this.useHotItem;
			}
			set
			{
				this.useHotItem = value;
				if (this.HotItemStyle != null)
				{
					if (value)
					{
						this.AddOverlay(this.HotItemStyle.Overlay);
						return;
					}
					this.RemoveOverlay(this.HotItemStyle.Overlay);
				}
			}
		}

		[DefaultValue(false)]
		[Description("Should hyperlinks be shown on this control?")]
		[Category("ObjectListView")]
		public bool UseHyperlinks
		{
			get
			{
				return this.useHyperlinks;
			}
			set
			{
				this.useHyperlinks = value;
				if (value && this.HyperlinkStyle == null)
				{
					this.HyperlinkStyle = new HyperlinkStyle();
				}
			}
		}

		[Category("ObjectListView")]
		[Description("Should this control show overlays")]
		[DefaultValue(true)]
		public bool UseOverlays
		{
			get
			{
				return this.useOverlays;
			}
			set
			{
				this.useOverlays = value;
			}
		}

		[DefaultValue(false)]
		[Description("Should this control be configured to show check boxes on subitems.")]
		[Category("ObjectListView")]
		public bool UseSubItemCheckBoxes
		{
			get
			{
				return this.useSubItemCheckBoxes;
			}
			set
			{
				this.useSubItemCheckBoxes = value;
				if (value)
				{
					this.SetupSubItemCheckBoxes();
				}
			}
		}

		[DefaultValue(false)]
		[Description("Should the list use a translucent selection mechanism (like Vista)")]
		[Category("ObjectListView")]
		public bool UseTranslucentSelection
		{
			get
			{
				return this.useTranslucentSelection;
			}
			set
			{
				this.useTranslucentSelection = value;
				if (value)
				{
					this.SelectedRowDecoration = new RowBorderDecoration
					{
						BorderPen = new Pen(Color.FromArgb(154, 223, 251)),
						FillBrush = new SolidBrush(Color.FromArgb(48, 163, 217, 225)),
						BoundsPadding = new Size(0, 0),
						CornerRounding = 6f
					};
					return;
				}
				this.SelectedRowDecoration = null;
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("Should the list use a translucent hot row highlighting mechanism (like Vista)")]
		public bool UseTranslucentHotItem
		{
			get
			{
				return this.useTranslucentHotItem;
			}
			set
			{
				this.useTranslucentHotItem = value;
				if (value)
				{
					this.HotItemStyle = new HotItemStyle();
					RowBorderDecoration rowBorderDecoration = new RowBorderDecoration();
					rowBorderDecoration.BorderPen = new Pen(Color.FromArgb(154, 223, 251));
					rowBorderDecoration.BoundsPadding = new Size(0, 0);
					rowBorderDecoration.CornerRounding = 6f;
					rowBorderDecoration.FillGradientFrom = new Color?(Color.FromArgb(0, 255, 255, 255));
					rowBorderDecoration.FillGradientTo = new Color?(Color.FromArgb(64, 183, 237, 240));
					this.HotItemStyle.Decoration = rowBorderDecoration;
				}
				else
				{
					this.HotItemStyle = null;
				}
				this.UseHotItem = value;
			}
		}

		public new View View
		{
			get
			{
				return base.View;
			}
			set
			{
				if (base.View == value)
				{
					return;
				}
				if (this.Frozen)
				{
					base.View = value;
					this.SetupBaseImageList();
					return;
				}
				this.Freeze();
				if (value == View.Tile)
				{
					this.CalculateReasonableTileSize();
				}
				base.View = value;
				this.SetupBaseImageList();
				this.Unfreeze();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual BooleanCheckStateGetterDelegate BooleanCheckStateGetter
		{
			set
			{
				if (value == null)
				{
					this.CheckStateGetter = null;
					return;
				}
				this.CheckStateGetter = delegate(object x)
				{
					if (!value(x))
					{
						return CheckState.Unchecked;
					}
					return CheckState.Checked;
				};
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual BooleanCheckStatePutterDelegate BooleanCheckStatePutter
		{
			set
			{
				if (value == null)
				{
					this.CheckStatePutter = null;
					return;
				}
				this.CheckStatePutter = delegate(object x, CheckState state)
				{
					bool newValue = state == CheckState.Checked;
					if (!value(x, newValue))
					{
						return CheckState.Unchecked;
					}
					return CheckState.Checked;
				};
			}
		}

		[Browsable(false)]
		public virtual bool CanShowGroups
		{
			get
			{
				return true;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool CanUseApplicationIdle
		{
			get
			{
				return this.canUseApplicationIdle;
			}
			set
			{
				this.canUseApplicationIdle = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual CellToolTipGetterDelegate CellToolTipGetter
		{
			get
			{
				return this.cellToolTipGetter;
			}
			set
			{
				this.cellToolTipGetter = value;
			}
		}

		[DefaultValue(null)]
		[Description("The name of the property or field that holds the 'checkedness' of the model")]
		[Category("ObjectListView")]
		public virtual string CheckedAspectName
		{
			get
			{
				return this.checkedAspectName;
			}
			set
			{
				this.checkedAspectName = value;
				if (string.IsNullOrEmpty(this.checkedAspectName))
				{
					this.checkedAspectMunger = null;
					this.CheckStateGetter = null;
					this.CheckStatePutter = null;
					return;
				}
				this.checkedAspectMunger = new Munger(this.checkedAspectName);
				this.CheckStateGetter = delegate(object modelObject)
				{
					bool? flag = this.checkedAspectMunger.GetValue(modelObject) as bool?;
					if (flag != null)
					{
						if (!flag.Value)
						{
							return CheckState.Unchecked;
						}
						return CheckState.Checked;
					}
					else
					{
						if (!this.TriStateCheckBoxes)
						{
							return CheckState.Unchecked;
						}
						return CheckState.Indeterminate;
					}
				};
				this.CheckStatePutter = delegate(object modelObject, CheckState newValue)
				{
					if (this.TriStateCheckBoxes && newValue == CheckState.Indeterminate)
					{
						this.checkedAspectMunger.PutValue(modelObject, null);
					}
					else
					{
						this.checkedAspectMunger.PutValue(modelObject, newValue == CheckState.Checked);
					}
					return this.CheckStateGetter(modelObject);
				};
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual CheckStateGetterDelegate CheckStateGetter
		{
			get
			{
				return this.checkStateGetter;
			}
			set
			{
				this.checkStateGetter = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual CheckStatePutterDelegate CheckStatePutter
		{
			get
			{
				return this.checkStatePutter;
			}
			set
			{
				this.checkStatePutter = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual SortDelegate CustomSorter
		{
			get
			{
				return this.customSorter;
			}
			set
			{
				this.customSorter = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HeaderToolTipGetterDelegate HeaderToolTipGetter
		{
			get
			{
				return this.headerToolTipGetter;
			}
			set
			{
				this.headerToolTipGetter = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RowFormatterDelegate RowFormatter
		{
			get
			{
				return this.rowFormatter;
			}
			set
			{
				this.rowFormatter = value;
			}
		}

		public virtual void AddObject(object modelObject)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.AddObject(modelObject);
				}));
				return;
			}
			this.AddObjects(new object[]
			{
				modelObject
			});
		}

		public virtual void AddObjects(ICollection modelObjects)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.AddObjects(modelObjects);
				}));
				return;
			}
			this.InsertObjects(ObjectListView.EnumerableCount(this.Objects), modelObjects);
			this.Sort(this.PrimarySortColumn, this.PrimarySortOrder);
		}

		public virtual void AutoResizeColumns()
		{
			foreach (object obj in this.Columns)
			{
				OLVColumn olvcolumn = (OLVColumn)obj;
				olvcolumn.Width = -2;
			}
		}

		public virtual void AutoSizeColumns()
		{
			ColumnHeaderAutoResizeStyle headerAutoResize = ColumnHeaderAutoResizeStyle.ColumnContent;
			if (this.GetItemCount() == 0)
			{
				headerAutoResize = ColumnHeaderAutoResizeStyle.HeaderSize;
			}
			foreach (object obj in this.Columns)
			{
				ColumnHeader columnHeader = (ColumnHeader)obj;
				switch (columnHeader.Width)
				{
				case -1:
					base.AutoResizeColumn(columnHeader.Index, ColumnHeaderAutoResizeStyle.HeaderSize);
					break;
				case 0:
					base.AutoResizeColumn(columnHeader.Index, headerAutoResize);
					break;
				}
			}
		}

		public virtual void BuildGroups()
		{
			this.BuildGroups(this.PrimarySortColumn, (this.PrimarySortOrder == SortOrder.None) ? SortOrder.Ascending : this.PrimarySortOrder);
		}

		public virtual void BuildGroups(OLVColumn column, SortOrder order)
		{
			BeforeSortingEventArgs beforeSortingEventArgs = this.BuildBeforeSortingEventArgs(column, order);
			this.OnBeforeSorting(beforeSortingEventArgs);
			if (beforeSortingEventArgs.Canceled)
			{
				return;
			}
			this.BuildGroups(beforeSortingEventArgs.ColumnToGroupBy, beforeSortingEventArgs.GroupByOrder, beforeSortingEventArgs.ColumnToSort, beforeSortingEventArgs.SortOrder, beforeSortingEventArgs.SecondaryColumnToSort, beforeSortingEventArgs.SecondarySortOrder);
			this.OnAfterSorting(new AfterSortingEventArgs(beforeSortingEventArgs));
		}

		private BeforeSortingEventArgs BuildBeforeSortingEventArgs(OLVColumn column, SortOrder order)
		{
			OLVColumn groupColumn = this.AlwaysGroupByColumn ?? (column ?? this.GetColumn(0));
			SortOrder sortOrder = this.AlwaysGroupBySortOrder;
			if (order == SortOrder.None)
			{
				order = base.Sorting;
				if (order == SortOrder.None)
				{
					order = SortOrder.Ascending;
				}
			}
			if (sortOrder == SortOrder.None)
			{
				sortOrder = order;
			}
			BeforeSortingEventArgs beforeSortingEventArgs = new BeforeSortingEventArgs(groupColumn, sortOrder, column, order, this.SecondarySortColumn ?? this.GetColumn(0), (this.SecondarySortOrder == SortOrder.None) ? order : this.SecondarySortOrder);
			if (column != null)
			{
				beforeSortingEventArgs.Canceled = !column.Sortable;
			}
			return beforeSortingEventArgs;
		}

		public virtual void BuildGroups(OLVColumn groupByColumn, SortOrder groupByOrder, OLVColumn column, SortOrder order, OLVColumn secondaryColumn, SortOrder secondaryOrder)
		{
			if (groupByColumn == null)
			{
				return;
			}
			int count = this.Items.Count;
			GroupingParameters groupingParameters = this.CollectGroupingParameters(groupByColumn, groupByOrder, column, order, secondaryColumn, secondaryOrder);
			CreateGroupsEventArgs createGroupsEventArgs = new CreateGroupsEventArgs(groupingParameters);
			if (groupingParameters.GroupByColumn != null)
			{
				createGroupsEventArgs.Canceled = !groupingParameters.GroupByColumn.Groupable;
			}
			this.OnBeforeCreatingGroups(createGroupsEventArgs);
			if (createGroupsEventArgs.Canceled)
			{
				return;
			}
			if (createGroupsEventArgs.Groups == null)
			{
				createGroupsEventArgs.Groups = this.MakeGroups(groupingParameters);
			}
			this.OnAboutToCreateGroups(createGroupsEventArgs);
			if (createGroupsEventArgs.Canceled)
			{
				return;
			}
			this.OLVGroups = createGroupsEventArgs.Groups;
			this.CreateGroups(createGroupsEventArgs.Groups);
			this.OnAfterCreatingGroups(createGroupsEventArgs);
			this.lastGroupingParameters = createGroupsEventArgs.Parameters;
		}

		protected virtual GroupingParameters CollectGroupingParameters(OLVColumn groupByColumn, SortOrder groupByOrder, OLVColumn sortByColumn, SortOrder sortByOrder, OLVColumn secondaryColumn, SortOrder secondaryOrder)
		{
			if (!groupByColumn.Groupable && this.lastGroupingParameters != null)
			{
				sortByColumn = groupByColumn;
				sortByOrder = groupByOrder;
				groupByColumn = this.lastGroupingParameters.GroupByColumn;
				groupByOrder = this.lastGroupingParameters.GroupByOrder;
			}
			string titleFormat = this.ShowItemCountOnGroups ? groupByColumn.GroupWithItemCountFormatOrDefault : null;
			string titleSingularFormat = this.ShowItemCountOnGroups ? groupByColumn.GroupWithItemCountSingularFormatOrDefault : null;
			return new GroupingParameters(this, groupByColumn, groupByOrder, sortByColumn, sortByOrder, secondaryColumn, secondaryOrder, titleFormat, titleSingularFormat, this.SortGroupItemsByPrimaryColumn);
		}

		protected virtual IList<OLVGroup> MakeGroups(GroupingParameters parms)
		{
			NullableDictionary<object, List<OLVListItem>> nullableDictionary = new NullableDictionary<object, List<OLVListItem>>();
			foreach (object obj in parms.ListView.Items)
			{
				OLVListItem olvlistItem = (OLVListItem)obj;
				object groupKey = parms.GroupByColumn.GetGroupKey(olvlistItem.RowObject);
				if (!nullableDictionary.ContainsKey(groupKey))
				{
					nullableDictionary[groupKey] = new List<OLVListItem>();
				}
				nullableDictionary[groupKey].Add(olvlistItem);
			}
			OLVColumn olvcolumn = parms.SortItemsByPrimaryColumn ? parms.ListView.GetColumn(0) : parms.PrimarySort;
			if (olvcolumn != null && parms.PrimarySortOrder != SortOrder.None)
			{
				IComparer<OLVListItem> comparer = parms.ItemComparer ?? new ColumnComparer(olvcolumn, parms.PrimarySortOrder, parms.SecondarySort, parms.SecondarySortOrder);
				foreach (object key in nullableDictionary.Keys)
				{
					nullableDictionary[key].Sort(comparer);
				}
			}
			List<OLVGroup> list = new List<OLVGroup>();
			foreach (object obj2 in nullableDictionary.Keys)
			{
				string text = parms.GroupByColumn.ConvertGroupKeyToTitle(obj2);
				if (!string.IsNullOrEmpty(parms.TitleFormat))
				{
					int count = nullableDictionary[obj2].Count;
					string text2 = (count == 1) ? parms.TitleSingularFormat : parms.TitleFormat;
					try
					{
						text = string.Format(text2, text, count);
						goto IL_1CA;
					}
					catch (FormatException)
					{
						text = ObjectListView.getString_0(107315496) + text2;
						goto IL_1CA;
					}
					goto IL_1A9;
				}
				goto IL_1CA;
				IL_1BC:
				OLVGroup olvgroup;
				list.Add(olvgroup);
				continue;
				IL_1A9:
				parms.GroupByColumn.GroupFormatter(olvgroup, parms);
				goto IL_1BC;
				IL_1CA:
				olvgroup = new OLVGroup(text);
				olvgroup.Collapsible = this.HasCollapsibleGroups;
				olvgroup.Key = obj2;
				olvgroup.SortValue = (obj2 as IComparable);
				olvgroup.Items = nullableDictionary[obj2];
				if (parms.GroupByColumn.GroupFormatter != null)
				{
					goto IL_1A9;
				}
				goto IL_1BC;
			}
			if (parms.GroupByOrder != SortOrder.None)
			{
				list.Sort(parms.GroupComparer ?? new OLVGroupComparer(parms.GroupByOrder));
			}
			return list;
		}

		public virtual void BuildList()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.BuildList));
				return;
			}
			this.BuildList(true);
		}

		public virtual void BuildList(bool shouldPreserveState)
		{
			if (this.Frozen)
			{
				return;
			}
			this.ApplyExtendedStyles();
			this.ClearHotItem();
			int topItemIndex = this.TopItemIndex;
			Point lowLevelScrollPosition = this.LowLevelScrollPosition;
			IList selectedObjects = new ArrayList();
			object modelObject = null;
			if (shouldPreserveState && this.objects != null)
			{
				selectedObjects = this.SelectedObjects;
				OLVListItem olvlistItem = base.FocusedItem as OLVListItem;
				if (olvlistItem != null)
				{
					modelObject = olvlistItem.RowObject;
				}
			}
			IEnumerable filteredObjects = this.FilteredObjects;
			base.BeginUpdate();
			try
			{
				this.Items.Clear();
				base.ListViewItemSorter = null;
				if (filteredObjects != null)
				{
					List<ListViewItem> list = new List<ListViewItem>();
					foreach (object rowObject in filteredObjects)
					{
						OLVListItem olvlistItem2 = new OLVListItem(rowObject);
						this.FillInValues(olvlistItem2, rowObject);
						list.Add(olvlistItem2);
					}
					this.Items.AddRange(list.ToArray());
					this.Sort();
					if (shouldPreserveState)
					{
						this.SelectedObjects = selectedObjects;
						base.FocusedItem = this.ModelToItem(modelObject);
					}
					this.RefreshHotItem();
				}
			}
			finally
			{
				base.EndUpdate();
			}
			if (shouldPreserveState)
			{
				this.RefreshHotItem();
				if (this.ShowGroups)
				{
					this.LowLevelScroll(lowLevelScrollPosition.X, lowLevelScrollPosition.Y);
					return;
				}
				this.TopItemIndex = topItemIndex;
			}
		}

		protected virtual void ApplyExtendedStyles()
		{
			int num = 0;
			if (this.ShowImagesOnSubItems && !base.VirtualMode)
			{
				num ^= 2;
			}
			if (this.ShowHeaderInAllViews)
			{
				num ^= 33554432;
			}
			NativeMethods.SetExtendedStyle(this, num, 33554434);
		}

		public virtual void CalculateReasonableTileSize()
		{
			if (this.Columns.Count <= 0)
			{
				return;
			}
			List<OLVColumn> list = this.AllColumns.FindAll((OLVColumn x) => x.Index == 0 || x.IsTileViewColumn);
			int val = (base.LargeImageList == null) ? 16 : base.LargeImageList.ImageSize.Height;
			int val2 = (this.Font.Height + 1) * list.Count;
			int width = (base.TileSize.Width == 0) ? 200 : base.TileSize.Width;
			int height = Math.Max(base.TileSize.Height, Math.Max(val, val2));
			base.TileSize = new Size(width, height);
		}

		public virtual void ChangeToFilteredColumns(View view)
		{
			this.SuspendSelectionEvents();
			IList selectedObjects = this.SelectedObjects;
			int topItemIndex = this.TopItemIndex;
			this.Freeze();
			base.Clear();
			List<OLVColumn> filteredColumns = this.GetFilteredColumns(view);
			if (view == View.Details || this.ShowHeaderInAllViews)
			{
				for (int i = 0; i < filteredColumns.Count; i++)
				{
					if (filteredColumns[i].LastDisplayIndex == -1)
					{
						filteredColumns[i].LastDisplayIndex = i;
					}
				}
				List<OLVColumn> list = new List<OLVColumn>(filteredColumns);
				list.Sort((OLVColumn x, OLVColumn y) => x.LastDisplayIndex - y.LastDisplayIndex);
				int num = 0;
				foreach (OLVColumn olvcolumn in list)
				{
					olvcolumn.DisplayIndex = num++;
				}
			}
			this.Columns.AddRange(filteredColumns.ToArray());
			if (view == View.Details || this.ShowHeaderInAllViews)
			{
				this.ShowSortIndicator();
			}
			this.UpdateFiltering();
			this.Unfreeze();
			this.SelectedObjects = selectedObjects;
			this.TopItemIndex = topItemIndex;
			this.ResumeSelectionEvents();
		}

		public virtual void ClearObjects()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ClearObjects));
				return;
			}
			this.SetObjects(null);
		}

		public virtual void ClearUrlVisited()
		{
			this.visitedUrlMap = new Dictionary<string, bool>();
		}

		public virtual void CopySelectionToClipboard()
		{
			IList selectedObjects = this.SelectedObjects;
			if (selectedObjects.Count == 0)
			{
				return;
			}
			object obj = null;
			if (this.CopySelectionOnControlCUsesDragSource && this.DragSource != null)
			{
				obj = this.DragSource.StartDrag(this, MouseButtons.Left, this.ModelToItem(selectedObjects[0]));
			}
			Clipboard.SetDataObject(obj ?? new OLVDataObject(this, selectedObjects));
		}

		public virtual void CopyObjectsToClipboard(IList objectsToCopy)
		{
			if (objectsToCopy.Count == 0)
			{
				return;
			}
			OLVDataObject olvdataObject = new OLVDataObject(this, objectsToCopy);
			olvdataObject.CreateTextFormats();
			Clipboard.SetDataObject(olvdataObject);
		}

		public virtual string ObjectsToHtml(IList objectsToConvert)
		{
			if (objectsToConvert.Count == 0)
			{
				return string.Empty;
			}
			OLVExporter olvexporter = new OLVExporter(this, objectsToConvert);
			return olvexporter.ExportTo(OLVExporter.ExportFormat.HTML);
		}

		public virtual void DeselectAll()
		{
			NativeMethods.DeselectAllItems(this);
		}

		public virtual void EnableCustomSelectionColors()
		{
			this.UseCustomSelectionColors = true;
		}

		public virtual OLVListItem GetNextItem(OLVListItem itemToFind)
		{
			if (this.ShowGroups)
			{
				bool flag = itemToFind == null;
				foreach (object obj in this.Groups)
				{
					ListViewGroup listViewGroup = (ListViewGroup)obj;
					foreach (object obj2 in listViewGroup.Items)
					{
						OLVListItem olvlistItem = (OLVListItem)obj2;
						if (flag)
						{
							return olvlistItem;
						}
						flag = (itemToFind == olvlistItem);
					}
				}
				return null;
			}
			if (this.GetItemCount() == 0)
			{
				return null;
			}
			if (itemToFind == null)
			{
				return this.GetItem(0);
			}
			if (itemToFind.Index == this.GetItemCount() - 1)
			{
				return null;
			}
			return this.GetItem(itemToFind.Index + 1);
		}

		public virtual OLVListItem GetLastItemInDisplayOrder()
		{
			if (!this.ShowGroups)
			{
				return this.GetItem(this.GetItemCount() - 1);
			}
			if (this.Groups.Count > 0)
			{
				ListViewGroup listViewGroup = this.Groups[this.Groups.Count - 1];
				if (listViewGroup.Items.Count > 0)
				{
					return (OLVListItem)listViewGroup.Items[listViewGroup.Items.Count - 1];
				}
			}
			return null;
		}

		public virtual OLVListItem GetNthItemInDisplayOrder(int n)
		{
			if (!this.ShowGroups)
			{
				return this.GetItem(n);
			}
			foreach (object obj in this.Groups)
			{
				ListViewGroup listViewGroup = (ListViewGroup)obj;
				if (n < listViewGroup.Items.Count)
				{
					return (OLVListItem)listViewGroup.Items[n];
				}
				n -= listViewGroup.Items.Count;
			}
			return null;
		}

		public virtual int GetDisplayOrderOfItemIndex(int itemIndex)
		{
			if (!this.ShowGroups)
			{
				return itemIndex;
			}
			int num = 0;
			foreach (object obj in this.Groups)
			{
				ListViewGroup listViewGroup = (ListViewGroup)obj;
				foreach (object obj2 in listViewGroup.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj2;
					if (listViewItem.Index == itemIndex)
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		public virtual OLVListItem GetPreviousItem(OLVListItem itemToFind)
		{
			if (this.ShowGroups)
			{
				OLVListItem result = null;
				foreach (object obj in this.Groups)
				{
					ListViewGroup listViewGroup = (ListViewGroup)obj;
					foreach (object obj2 in listViewGroup.Items)
					{
						OLVListItem olvlistItem = (OLVListItem)obj2;
						if (olvlistItem == itemToFind)
						{
							return result;
						}
						result = olvlistItem;
					}
				}
				if (itemToFind != null)
				{
					return null;
				}
				return result;
			}
			if (this.GetItemCount() == 0)
			{
				return null;
			}
			if (itemToFind == null)
			{
				return this.GetItem(this.GetItemCount() - 1);
			}
			if (itemToFind.Index == 0)
			{
				return null;
			}
			return this.GetItem(itemToFind.Index - 1);
		}

		public virtual void InsertObjects(int index, ICollection modelObjects)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.InsertObjects(index, modelObjects);
				}));
				return;
			}
			if (modelObjects == null)
			{
				return;
			}
			base.BeginUpdate();
			try
			{
				ItemsAddingEventArgs itemsAddingEventArgs = new ItemsAddingEventArgs(modelObjects);
				this.OnItemsAdding(itemsAddingEventArgs);
				if (!itemsAddingEventArgs.Canceled)
				{
					modelObjects = itemsAddingEventArgs.ObjectsToAdd;
					this.TakeOwnershipOfObjects();
					ArrayList arrayList = ObjectListView.EnumerableToArray(this.Objects, false);
					if (this.IsFiltering)
					{
						index = Math.Max(0, Math.Min(index, arrayList.Count));
						arrayList.InsertRange(index, modelObjects);
						this.BuildList(true);
					}
					else
					{
						base.ListViewItemSorter = null;
						index = Math.Max(0, Math.Min(index, this.GetItemCount()));
						int i = index;
						foreach (object obj in modelObjects)
						{
							if (obj != null)
							{
								arrayList.Insert(i, obj);
								OLVListItem olvlistItem = new OLVListItem(obj);
								this.FillInValues(olvlistItem, obj);
								this.Items.Insert(i, olvlistItem);
								i++;
							}
						}
						for (i = index; i < this.GetItemCount(); i++)
						{
							OLVListItem item = this.GetItem(i);
							this.SetSubItemImages(item.Index, item);
						}
						this.PostProcessRows();
					}
					this.SubscribeNotifications(modelObjects);
					this.OnItemsChanged(new ItemsChangedEventArgs());
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public bool IsSelected(object model)
		{
			OLVListItem olvlistItem = this.ModelToItem(model);
			return olvlistItem != null && olvlistItem.Selected;
		}

		public virtual bool IsUrlVisited(string url)
		{
			return this.visitedUrlMap.ContainsKey(url);
		}

		public void LowLevelScroll(int dx, int dy)
		{
			NativeMethods.Scroll(this, dx, dy);
		}

		public Point LowLevelScrollPosition
		{
			get
			{
				return new Point(NativeMethods.GetScrollPosition(this, true), NativeMethods.GetScrollPosition(this, false));
			}
		}

		public virtual void MarkUrlVisited(string url)
		{
			this.visitedUrlMap[url] = true;
		}

		public virtual void MoveObjects(int index, ICollection modelObjects)
		{
			this.TakeOwnershipOfObjects();
			ArrayList arrayList = ObjectListView.EnumerableToArray(this.Objects, false);
			List<int> list = new List<int>();
			foreach (object obj in modelObjects)
			{
				if (obj != null)
				{
					int num = this.IndexOf(obj);
					if (num >= 0)
					{
						list.Add(num);
						arrayList.Remove(obj);
						if (num <= index)
						{
							index--;
						}
					}
				}
			}
			list.Sort();
			list.Reverse();
			try
			{
				base.BeginUpdate();
				foreach (int index2 in list)
				{
					this.Items.RemoveAt(index2);
				}
				this.InsertObjects(index, modelObjects);
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public new ListViewHitTestInfo HitTest(int x, int y)
		{
			ListViewHitTestInfo result;
			try
			{
				result = base.HitTest(x, y);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = new ListViewHitTestInfo(null, null, ListViewHitTestLocations.None);
			}
			return result;
		}

		protected OlvListViewHitTestInfo LowLevelHitTest(int x, int y)
		{
			if (!base.ClientRectangle.Contains(x, y))
			{
				return new OlvListViewHitTestInfo(null, null, 0, null);
			}
			NativeMethods.LVHITTESTINFO lvhittestinfo = default(NativeMethods.LVHITTESTINFO);
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num = NativeMethods.HitTest(this, ref lvhittestinfo);
			bool flag;
			OLVListItem olvlistItem = ((flag = ((lvhittestinfo.flags & -218103808) != 0)) || num == -1) ? null : this.GetItem(num);
			OLVListSubItem subItem = (this.View != View.Details || olvlistItem == null) ? null : olvlistItem.GetSubItem(lvhittestinfo.iSubItem);
			OLVGroup group = null;
			if (this.ShowGroups && this.OLVGroups != null)
			{
				if (base.VirtualMode)
				{
					group = ((lvhittestinfo.iGroup < 0 || lvhittestinfo.iGroup >= this.OLVGroups.Count) ? null : this.OLVGroups[lvhittestinfo.iGroup]);
				}
				else if (flag)
				{
					foreach (OLVGroup olvgroup in this.OLVGroups)
					{
						if (olvgroup.GroupId == num)
						{
							group = olvgroup;
							break;
						}
					}
				}
			}
			return new OlvListViewHitTestInfo(olvlistItem, subItem, lvhittestinfo.flags, group);
		}

		public virtual OlvListViewHitTestInfo OlvHitTest(int x, int y)
		{
			OlvListViewHitTestInfo olvListViewHitTestInfo = this.LowLevelHitTest(x, y);
			if (olvListViewHitTestInfo.Item == null && !base.FullRowSelect && this.View == View.Details)
			{
				Point scrolledColumnSides = NativeMethods.GetScrolledColumnSides(this, 0);
				if (x >= scrolledColumnSides.X && x <= scrolledColumnSides.Y)
				{
					olvListViewHitTestInfo = this.LowLevelHitTest(scrolledColumnSides.Y + 4, y);
					if (olvListViewHitTestInfo.Item == null)
					{
						olvListViewHitTestInfo = this.LowLevelHitTest(scrolledColumnSides.X - 4, y);
					}
					if (olvListViewHitTestInfo.Item == null)
					{
						olvListViewHitTestInfo = this.LowLevelHitTest(4, y);
					}
					if (olvListViewHitTestInfo.Item != null)
					{
						olvListViewHitTestInfo.SubItem = olvListViewHitTestInfo.Item.GetSubItem(0);
						olvListViewHitTestInfo.Location = ListViewHitTestLocations.None;
						olvListViewHitTestInfo.HitTestLocation = HitTestLocation.InCell;
					}
				}
			}
			if (base.OwnerDraw)
			{
				this.CalculateOwnerDrawnHitTest(olvListViewHitTestInfo, x, y);
			}
			else
			{
				this.CalculateStandardHitTest(olvListViewHitTestInfo, x, y);
			}
			return olvListViewHitTestInfo;
		}

		protected virtual void CalculateStandardHitTest(OlvListViewHitTestInfo hti, int x, int y)
		{
			if (this.View == View.Details && hti.ColumnIndex != 0 && hti.SubItem != null && hti.Column != null)
			{
				Rectangle bounds = hti.SubItem.Bounds;
				bool flag = this.GetActualImageIndex(hti.SubItem.ImageSelector) != -1;
				hti.HitTestLocation = HitTestLocation.InCell;
				Rectangle rectangle = bounds;
				rectangle.Width = this.SmallImageSize.Width;
				if (rectangle.Contains(x, y))
				{
					if (hti.Column.CheckBoxes)
					{
						hti.HitTestLocation = HitTestLocation.CheckBox;
						return;
					}
					if (flag)
					{
						hti.HitTestLocation = HitTestLocation.Image;
						return;
					}
				}
				Rectangle rectangle2 = bounds;
				rectangle2.X += 4;
				if (flag)
				{
					rectangle2.X += this.SmallImageSize.Width;
				}
				Size proposedSize = new Size(rectangle2.Width, rectangle2.Height);
				Size size = TextRenderer.MeasureText(hti.SubItem.Text, this.Font, proposedSize, TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine);
				rectangle2.Width = size.Width;
				switch (hti.Column.TextAlign)
				{
				case HorizontalAlignment.Right:
					rectangle2.X = bounds.Right - size.Width;
					break;
				case HorizontalAlignment.Center:
					rectangle2.X += (bounds.Right - bounds.Left - size.Width) / 2;
					break;
				}
				if (rectangle2.Contains(x, y))
				{
					hti.HitTestLocation = HitTestLocation.Text;
				}
				return;
			}
		}

		protected virtual void CalculateOwnerDrawnHitTest(OlvListViewHitTestInfo hti, int x, int y)
		{
			if (hti.Item == null)
			{
				return;
			}
			if (this.View == View.Details && hti.Column == null)
			{
				return;
			}
			IRenderer renderer = (this.View == View.Details) ? (hti.Column.Renderer ?? this.DefaultRenderer) : this.ItemRenderer;
			if (renderer == null)
			{
				return;
			}
			renderer.HitTest(hti, x, y);
		}

		public virtual void PauseAnimations(bool isPause)
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				OLVColumn column = this.GetColumn(i);
				ImageRenderer imageRenderer = column.Renderer as ImageRenderer;
				if (imageRenderer != null)
				{
					imageRenderer.Paused = isPause;
				}
			}
		}

		public virtual void RebuildColumns()
		{
			this.ChangeToFilteredColumns(this.View);
		}

		public virtual void RemoveObject(object modelObject)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.RemoveObject(modelObject);
				}));
				return;
			}
			this.RemoveObjects(new object[]
			{
				modelObject
			});
		}

		public virtual void RemoveObjects(ICollection modelObjects)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.RemoveObjects(modelObjects);
				}));
				return;
			}
			if (modelObjects == null)
			{
				return;
			}
			base.BeginUpdate();
			try
			{
				ItemsRemovingEventArgs itemsRemovingEventArgs = new ItemsRemovingEventArgs(modelObjects);
				this.OnItemsRemoving(itemsRemovingEventArgs);
				if (!itemsRemovingEventArgs.Canceled)
				{
					modelObjects = itemsRemovingEventArgs.ObjectsToRemove;
					this.TakeOwnershipOfObjects();
					ArrayList arrayList = ObjectListView.EnumerableToArray(this.Objects, false);
					foreach (object obj in modelObjects)
					{
						if (obj != null)
						{
							int num = arrayList.IndexOf(obj);
							if (num >= 0)
							{
								arrayList.RemoveAt(num);
							}
							num = this.IndexOf(obj);
							if (num >= 0)
							{
								this.Items.RemoveAt(num);
							}
						}
					}
					this.PostProcessRows();
					this.UnsubscribeNotifications(modelObjects);
					this.OnItemsChanged(new ItemsChangedEventArgs());
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public virtual void SelectAll()
		{
			NativeMethods.SelectAllItems(this);
		}

		public void SetNativeBackgroundWatermark(Image image)
		{
			NativeMethods.SetBackgroundImage(this, image, true, false, 0, 0);
		}

		public void SetNativeBackgroundImage(Image image, int xOffset, int yOffset)
		{
			NativeMethods.SetBackgroundImage(this, image, false, false, xOffset, yOffset);
		}

		public void SetNativeBackgroundTiledImage(Image image)
		{
			NativeMethods.SetBackgroundImage(this, image, false, true, 0, 0);
		}

		public virtual void SetObjects(IEnumerable collection)
		{
			this.SetObjects(collection, false);
		}

		public virtual void SetObjects(IEnumerable collection, bool preserveState)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.SetObjects(collection, preserveState);
				}));
				return;
			}
			ItemsChangingEventArgs itemsChangingEventArgs = new ItemsChangingEventArgs(this.objects, collection);
			this.OnItemsChanging(itemsChangingEventArgs);
			if (itemsChangingEventArgs.Canceled)
			{
				return;
			}
			collection = itemsChangingEventArgs.NewObjects;
			if (this.isOwnerOfObjects && this.objects != collection)
			{
				this.isOwnerOfObjects = false;
			}
			this.objects = collection;
			this.BuildList(preserveState);
			this.UpdateNotificationSubscriptions(this.objects);
			this.OnItemsChanged(new ItemsChangedEventArgs());
		}

		public virtual void UpdateObject(object modelObject)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.UpdateObject(modelObject);
				}));
				return;
			}
			this.UpdateObjects(new object[]
			{
				modelObject
			});
		}

		public virtual void UpdateObjects(ICollection modelObjects)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.UpdateObjects(modelObjects);
				}));
				return;
			}
			if (modelObjects != null && modelObjects.Count != 0)
			{
				base.BeginUpdate();
				try
				{
					this.UnsubscribeNotifications(modelObjects);
					ArrayList arrayList = new ArrayList();
					this.TakeOwnershipOfObjects();
					ArrayList arrayList2 = ObjectListView.EnumerableToArray(this.Objects, false);
					foreach (object obj in modelObjects)
					{
						if (obj != null)
						{
							int num = arrayList2.IndexOf(obj);
							if (num < 0)
							{
								arrayList.Add(obj);
							}
							else
							{
								arrayList2[num] = obj;
								OLVListItem olvlistItem = this.ModelToItem(obj);
								if (olvlistItem != null)
								{
									olvlistItem.RowObject = obj;
									this.RefreshItem(olvlistItem);
								}
							}
						}
					}
					this.PostProcessRows();
					this.AddObjects(arrayList);
					this.SubscribeNotifications(modelObjects);
					this.OnItemsChanged(new ItemsChangedEventArgs());
				}
				finally
				{
					base.EndUpdate();
				}
				return;
			}
		}

		protected virtual void UpdateNotificationSubscriptions(IEnumerable collection)
		{
			if (!this.UseNotifyPropertyChanged)
			{
				return;
			}
			this.UnsubscribeNotifications(null);
			this.SubscribeNotifications(collection ?? this.Objects);
		}

		[Category("ObjectListView")]
		[Description("Should ObjectListView listen for property changed events on the model objects?")]
		[DefaultValue(false)]
		public bool UseNotifyPropertyChanged
		{
			get
			{
				return this.useNotifyPropertyChanged;
			}
			set
			{
				if (this.useNotifyPropertyChanged == value)
				{
					return;
				}
				this.useNotifyPropertyChanged = value;
				if (value)
				{
					this.SubscribeNotifications(this.Objects);
					return;
				}
				this.UnsubscribeNotifications(null);
			}
		}

		protected void SubscribeNotifications(IEnumerable models)
		{
			if (this.UseNotifyPropertyChanged && models != null)
			{
				foreach (object obj in models)
				{
					INotifyPropertyChanged notifyPropertyChanged = obj as INotifyPropertyChanged;
					if (notifyPropertyChanged != null && !this.subscribedModels.ContainsKey(notifyPropertyChanged))
					{
						notifyPropertyChanged.PropertyChanged += this.HandleModelOnPropertyChanged;
						this.subscribedModels[notifyPropertyChanged] = notifyPropertyChanged;
					}
				}
				return;
			}
		}

		protected void UnsubscribeNotifications(IEnumerable models)
		{
			if (models == null)
			{
				foreach (object obj in this.subscribedModels.Keys)
				{
					INotifyPropertyChanged notifyPropertyChanged = (INotifyPropertyChanged)obj;
					notifyPropertyChanged.PropertyChanged -= this.HandleModelOnPropertyChanged;
				}
				this.subscribedModels = new Hashtable();
				return;
			}
			foreach (object obj2 in models)
			{
				INotifyPropertyChanged notifyPropertyChanged2 = obj2 as INotifyPropertyChanged;
				if (notifyPropertyChanged2 != null)
				{
					notifyPropertyChanged2.PropertyChanged -= this.HandleModelOnPropertyChanged;
					this.subscribedModels.Remove(notifyPropertyChanged2);
				}
			}
		}

		private void HandleModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.RefreshObject(sender);
		}

		public virtual byte[] SaveState()
		{
			ObjectListView.ObjectListViewState objectListViewState = new ObjectListView.ObjectListViewState();
			objectListViewState.VersionNumber = 1;
			objectListViewState.NumberOfColumns = this.AllColumns.Count;
			objectListViewState.CurrentView = this.View;
			if (this.PrimarySortColumn != null)
			{
				objectListViewState.SortColumn = this.AllColumns.IndexOf(this.PrimarySortColumn);
			}
			objectListViewState.LastSortOrder = this.PrimarySortOrder;
			objectListViewState.IsShowingGroups = this.ShowGroups;
			if (this.AllColumns.Count > 0 && this.AllColumns[0].LastDisplayIndex == -1)
			{
				this.RememberDisplayIndicies();
			}
			foreach (OLVColumn olvcolumn in this.AllColumns)
			{
				objectListViewState.ColumnIsVisible.Add(olvcolumn.IsVisible);
				objectListViewState.ColumnDisplayIndicies.Add(olvcolumn.LastDisplayIndex);
				objectListViewState.ColumnWidths.Add(olvcolumn.Width);
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter
				{
					AssemblyFormat = FormatterAssemblyStyle.Simple
				}.Serialize(memoryStream, objectListViewState);
				result = memoryStream.ToArray();
			}
			return result;
		}

		public virtual bool RestoreState(byte[] state)
		{
			using (MemoryStream memoryStream = new MemoryStream(state))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				ObjectListView.ObjectListViewState objectListViewState;
				try
				{
					objectListViewState = (binaryFormatter.Deserialize(memoryStream) as ObjectListView.ObjectListViewState);
				}
				catch (SerializationException)
				{
					return false;
				}
				if (objectListViewState != null)
				{
					if (objectListViewState.NumberOfColumns == this.AllColumns.Count)
					{
						if (objectListViewState.SortColumn == -1)
						{
							this.PrimarySortColumn = null;
							this.PrimarySortOrder = SortOrder.None;
						}
						else
						{
							this.PrimarySortColumn = this.AllColumns[objectListViewState.SortColumn];
							this.PrimarySortOrder = objectListViewState.LastSortOrder;
						}
						for (int i = 0; i < objectListViewState.NumberOfColumns; i++)
						{
							OLVColumn olvcolumn = this.AllColumns[i];
							olvcolumn.Width = (int)objectListViewState.ColumnWidths[i];
							olvcolumn.IsVisible = (bool)objectListViewState.ColumnIsVisible[i];
							olvcolumn.LastDisplayIndex = (int)objectListViewState.ColumnDisplayIndicies[i];
						}
						if (objectListViewState.IsShowingGroups != this.ShowGroups)
						{
							this.ShowGroups = objectListViewState.IsShowingGroups;
						}
						if (this.View == objectListViewState.CurrentView)
						{
							this.RebuildColumns();
						}
						else
						{
							this.View = objectListViewState.CurrentView;
						}
						return true;
					}
				}
				return false;
			}
			return true;
		}

		protected virtual void HandleApplicationIdle(object sender, EventArgs e)
		{
			Application.Idle -= this.HandleApplicationIdle;
			this.hasIdleHandler = false;
			this.OnSelectionChanged(new EventArgs());
		}

		protected virtual void HandleApplicationIdleResizeColumns(object sender, EventArgs e)
		{
			Application.Idle -= this.HandleApplicationIdleResizeColumns;
			this.hasResizeColumnsHandler = false;
			this.ResizeFreeSpaceFillingColumns();
		}

		protected virtual bool HandleBeginScroll(ref Message m)
		{
			NativeMethods.NMLVSCROLL nmlvscroll = (NativeMethods.NMLVSCROLL)m.GetLParam(typeof(NativeMethods.NMLVSCROLL));
			if (nmlvscroll.dx != 0)
			{
				int scrollPosition = NativeMethods.GetScrollPosition(this, true);
				ScrollEventArgs e = new ScrollEventArgs(ScrollEventType.EndScroll, scrollPosition - nmlvscroll.dx, scrollPosition, ScrollOrientation.HorizontalScroll);
				this.OnScroll(e);
				if (this.GetItemCount() == 0)
				{
					base.Invalidate();
				}
			}
			if (nmlvscroll.dy != 0)
			{
				int scrollPosition2 = NativeMethods.GetScrollPosition(this, false);
				ScrollEventArgs e2 = new ScrollEventArgs(ScrollEventType.EndScroll, scrollPosition2 - nmlvscroll.dy, scrollPosition2, ScrollOrientation.VerticalScroll);
				this.OnScroll(e2);
			}
			return false;
		}

		protected virtual bool HandleEndScroll(ref Message m)
		{
			if (!ObjectListView.IsVistaOrLater && Control.MouseButtons == MouseButtons.Left && base.GridLines)
			{
				base.Invalidate();
				base.Update();
			}
			return false;
		}

		protected virtual bool HandleLinkClick(ref Message m)
		{
			NativeMethods.NMLVLINK nmlvlink = (NativeMethods.NMLVLINK)m.GetLParam(typeof(NativeMethods.NMLVLINK));
			foreach (OLVGroup olvgroup in this.OLVGroups)
			{
				if (olvgroup.GroupId == nmlvlink.iSubItem)
				{
					this.OnGroupTaskClicked(new GroupTaskClickedEventArgs(olvgroup));
					return true;
				}
			}
			return false;
		}

		protected virtual void HandleCellToolTipShowing(object sender, ToolTipShowingEventArgs e)
		{
			this.BuildCellEvent(e, base.PointToClient(Cursor.Position));
			if (e.Item != null)
			{
				e.Text = this.GetCellToolTip(e.ColumnIndex, e.RowIndex);
				this.OnCellToolTip(e);
			}
		}

		internal void HeaderToolTipShowingCallback(object sender, ToolTipShowingEventArgs e)
		{
			this.HandleHeaderToolTipShowing(sender, e);
		}

		protected virtual void HandleHeaderToolTipShowing(object sender, ToolTipShowingEventArgs e)
		{
			e.ColumnIndex = this.HeaderControl.ColumnIndexUnderCursor;
			if (e.ColumnIndex < 0)
			{
				return;
			}
			e.RowIndex = -1;
			e.Model = null;
			e.Column = this.GetColumn(e.ColumnIndex);
			e.Text = this.GetHeaderToolTip(e.ColumnIndex);
			e.ListView = this;
			this.OnHeaderToolTip(e);
		}

		protected virtual void HandleColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (!this.PossibleFinishCellEditing())
			{
				return;
			}
			if (this.PrimarySortColumn != null && e.Column == this.PrimarySortColumn.Index)
			{
				this.PrimarySortOrder = ((this.PrimarySortOrder == SortOrder.Descending) ? SortOrder.Ascending : SortOrder.Descending);
			}
			else
			{
				this.PrimarySortOrder = SortOrder.Ascending;
			}
			base.BeginUpdate();
			try
			{
				this.Sort(e.Column);
			}
			finally
			{
				base.EndUpdate();
			}
		}

		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 78)
			{
				if (msg <= 15)
				{
					if (msg != 2)
					{
						if (msg == 15)
						{
							if (!this.HandlePaint(ref m))
							{
								base.WndProc(ref m);
								return;
							}
							return;
						}
					}
					else
					{
						if (!this.HandleDestroy(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					}
				}
				else if (msg != 70)
				{
					if (msg == 78)
					{
						if (!this.HandleNotify(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					}
				}
				else
				{
					if (this.PossibleFinishCellEditing() && !this.HandleWindowPosChanging(ref m))
					{
						base.WndProc(ref m);
						return;
					}
					return;
				}
			}
			else if (msg <= 258)
			{
				if (msg != 123)
				{
					switch (msg)
					{
					case 256:
						if (!this.HandleKeyDown(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 258:
						if (!this.HandleChar(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					}
				}
				else
				{
					if (!this.HandleContextMenu(ref m))
					{
						base.WndProc(ref m);
						return;
					}
					return;
				}
			}
			else
			{
				switch (msg)
				{
				case 276:
				case 277:
					if (this.PossibleFinishCellEditing())
					{
						base.WndProc(ref m);
						return;
					}
					return;
				default:
					switch (msg)
					{
					case 512:
						if (!this.HandleMouseMove(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 513:
						if (this.PossibleFinishCellEditing() && !this.HandleLButtonDown(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 514:
						if (this.PossibleFinishCellEditing() && !this.HandleLButtonUp(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 515:
						if (this.PossibleFinishCellEditing() && !this.HandleLButtonDoubleClick(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 516:
						if (this.PossibleFinishCellEditing() && !this.HandleRButtonDown(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 517:
					case 519:
					case 520:
					case 521:
					case 523:
					case 524:
					case 525:
						break;
					case 518:
						if (this.PossibleFinishCellEditing() && !this.HandleRButtonDoubleClick(ref m))
						{
							base.WndProc(ref m);
							return;
						}
						return;
					case 522:
					case 526:
						if (this.PossibleFinishCellEditing())
						{
							base.WndProc(ref m);
							return;
						}
						return;
					default:
						if (msg == 8270)
						{
							if (!this.HandleReflectNotify(ref m))
							{
								base.WndProc(ref m);
								return;
							}
							return;
						}
						break;
					}
					break;
				}
			}
			base.WndProc(ref m);
		}

		protected virtual bool HandleChar(ref Message m)
		{
			if (this.ProcessKeyEventArgs(ref m))
			{
				return true;
			}
			char c = (char)m.WParam.ToInt32();
			if (c == '\b')
			{
				this.timeLastCharEvent = 0;
				return true;
			}
			if (Environment.TickCount < this.timeLastCharEvent + 1000)
			{
				this.lastSearchString += c;
			}
			else
			{
				this.lastSearchString = c.ToString(CultureInfo.InvariantCulture);
			}
			if (this.CheckBoxes && this.lastSearchString == ObjectListView.getString_0(107402699))
			{
				this.timeLastCharEvent = 0;
				return true;
			}
			int num = 0;
			ListViewItem focusedItem = base.FocusedItem;
			if (focusedItem != null)
			{
				num = this.GetDisplayOrderOfItemIndex(focusedItem.Index);
				if (this.lastSearchString.Length == 1)
				{
					num++;
					if (num == this.GetItemCount())
					{
						num = 0;
					}
				}
			}
			BeforeSearchingEventArgs beforeSearchingEventArgs = new BeforeSearchingEventArgs(this.lastSearchString, num);
			this.OnBeforeSearching(beforeSearchingEventArgs);
			if (beforeSearchingEventArgs.Canceled)
			{
				return true;
			}
			string stringToFind = beforeSearchingEventArgs.StringToFind;
			num = beforeSearchingEventArgs.StartSearchFrom;
			int num2 = this.FindMatchingRow(stringToFind, num, SearchDirectionHint.Down);
			if (num2 >= 0)
			{
				base.BeginUpdate();
				try
				{
					base.SelectedIndices.Clear();
					ListViewItem nthItemInDisplayOrder = this.GetNthItemInDisplayOrder(num2);
					if (nthItemInDisplayOrder != null)
					{
						nthItemInDisplayOrder.Selected = true;
						nthItemInDisplayOrder.Focused = true;
						base.EnsureVisible(nthItemInDisplayOrder.Index);
					}
				}
				finally
				{
					base.EndUpdate();
				}
			}
			AfterSearchingEventArgs afterSearchingEventArgs = new AfterSearchingEventArgs(stringToFind, num2);
			this.OnAfterSearching(afterSearchingEventArgs);
			if (!afterSearchingEventArgs.Handled && num2 < 0)
			{
				SystemSounds.Beep.Play();
			}
			this.timeLastCharEvent = Environment.TickCount;
			return true;
		}

		protected virtual bool HandleContextMenu(ref Message m)
		{
			if (base.DesignMode)
			{
				return false;
			}
			if (m.LParam == this.minusOne)
			{
				return false;
			}
			if (m.WParam != this.HeaderControl.Handle)
			{
				return false;
			}
			if (!this.PossibleFinishCellEditing())
			{
				return true;
			}
			int columnIndexUnderCursor = this.HeaderControl.ColumnIndexUnderCursor;
			return this.HandleHeaderRightClick(columnIndexUnderCursor);
		}

		protected virtual bool HandleHitTest(ref Message m)
		{
			if (!base.VirtualMode || !this.CheckBoxes)
			{
				return false;
			}
			base.DefWndProc(ref m);
			NativeMethods.LVHITTESTINFO structure = (NativeMethods.LVHITTESTINFO)m.GetLParam(typeof(NativeMethods.LVHITTESTINFO));
			if (structure.iItem != -1)
			{
				return true;
			}
			int pt_x = structure.pt_x;
			int width = base.StateImageList.ImageSize.Width;
			structure.pt_x -= width;
			Marshal.StructureToPtr<NativeMethods.LVHITTESTINFO>(structure, m.LParam, false);
			base.DefWndProc(ref m);
			structure = (NativeMethods.LVHITTESTINFO)m.GetLParam(typeof(NativeMethods.LVHITTESTINFO));
			structure.pt_x = pt_x;
			Marshal.StructureToPtr<NativeMethods.LVHITTESTINFO>(structure, m.LParam, false);
			return true;
		}

		protected virtual bool HandleCustomDraw(ref Message m)
		{
			if (!this.isInWmPaintEvent)
			{
				return true;
			}
			if (!this.shouldDoCustomDrawing)
			{
				return true;
			}
			NativeMethods.NMLVCUSTOMDRAW nmlvcustomdraw = (NativeMethods.NMLVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMLVCUSTOMDRAW));
			if (nmlvcustomdraw.dwItemType == 1)
			{
				return true;
			}
			int dwDrawStage = nmlvcustomdraw.nmcd.dwDrawStage;
			switch (dwDrawStage)
			{
			case 1:
				if (this.prePaintLevel == 0)
				{
					this.drawnItems = new List<OLVListItem>();
				}
				this.isAfterItemPaint = (this.GetItemCount() == 0);
				this.prePaintLevel++;
				base.WndProc(ref m);
				m.Result = (IntPtr)((int)m.Result | 16 | 64);
				return true;
			case 2:
				this.prePaintLevel--;
				if (this.prePaintLevel == 0 && (this.isMarqueSelecting || this.isAfterItemPaint))
				{
					this.shouldDoCustomDrawing = false;
					using (Graphics graphics = Graphics.FromHdc(nmlvcustomdraw.nmcd.hdc))
					{
						this.DrawAllDecorations(graphics, this.drawnItems);
					}
				}
				break;
			case 3:
			case 4:
				break;
			default:
				switch (dwDrawStage)
				{
				case 65537:
					this.isAfterItemPaint = true;
					if (this.View == View.Tile)
					{
						if (base.OwnerDraw && this.ItemRenderer != null)
						{
							base.WndProc(ref m);
						}
					}
					else
					{
						base.WndProc(ref m);
					}
					m.Result = (IntPtr)((int)m.Result | 16 | 64);
					return true;
				case 65538:
					if (this.Columns.Count > 0)
					{
						OLVListItem item = this.GetItem((int)nmlvcustomdraw.nmcd.dwItemSpec);
						if (item != null)
						{
							this.drawnItems.Add(item);
						}
					}
					break;
				case 65539:
				case 65540:
					break;
				default:
					switch (dwDrawStage)
					{
					case 196609:
					{
						if (!base.OwnerDraw)
						{
							return false;
						}
						int iSubItem = nmlvcustomdraw.iSubItem;
						if (iSubItem != 0)
						{
							return false;
						}
						if (this.Columns[0].DisplayIndex == 0)
						{
							return false;
						}
						int num = (int)nmlvcustomdraw.nmcd.dwItemSpec;
						OLVListItem item2 = this.GetItem(num);
						if (item2 == null)
						{
							return false;
						}
						using (Graphics graphics2 = Graphics.FromHdc(nmlvcustomdraw.nmcd.hdc))
						{
							Rectangle subItemBounds = item2.GetSubItemBounds(0);
							DrawListViewSubItemEventArgs drawListViewSubItemEventArgs = new DrawListViewSubItemEventArgs(graphics2, subItemBounds, item2, item2.SubItems[0], num, 0, this.Columns[0], (ListViewItemStates)nmlvcustomdraw.nmcd.uItemState);
							this.OnDrawSubItem(drawListViewSubItemEventArgs);
							Trace.Assert(!drawListViewSubItemEventArgs.DrawDefault, ObjectListView.getString_0(107315975));
						}
						m.Result = (IntPtr)4;
						return true;
					}
					}
					break;
				}
				break;
			}
			return false;
		}

		protected virtual bool HandleDestroy(ref Message m)
		{
			base.BeginInvoke(new MethodInvoker(delegate()
			{
				this.headerControl = null;
				this.HeaderControl.WordWrap = this.HeaderWordWrap;
			}));
			if (this.cellToolTip == null)
			{
				return false;
			}
			this.cellToolTip.PushSettings();
			base.WndProc(ref m);
			base.BeginInvoke(new MethodInvoker(delegate()
			{
				this.UpdateCellToolTipHandle();
				this.cellToolTip.PopSettings();
			}));
			return true;
		}

		protected virtual bool HandleFindItem(ref Message m)
		{
			NativeMethods.LVFINDINFO lvfindinfo = (NativeMethods.LVFINDINFO)m.GetLParam(typeof(NativeMethods.LVFINDINFO));
			if ((lvfindinfo.flags & 2) != 2)
			{
				return false;
			}
			int start = m.WParam.ToInt32();
			m.Result = (IntPtr)this.FindMatchingRow(lvfindinfo.psz, start, SearchDirectionHint.Down);
			return true;
		}

		public virtual int FindMatchingRow(string text, int start, SearchDirectionHint direction)
		{
			int itemCount = this.GetItemCount();
			if (itemCount == 0)
			{
				return -1;
			}
			OLVColumn column = this.GetColumn(0);
			if (this.IsSearchOnSortColumn && this.View == View.Details && this.PrimarySortColumn != null)
			{
				column = this.PrimarySortColumn;
			}
			int num;
			if (direction == SearchDirectionHint.Down)
			{
				num = this.FindMatchInRange(text, start, itemCount - 1, column);
				if (num == -1 && start > 0)
				{
					num = this.FindMatchInRange(text, 0, start - 1, column);
				}
			}
			else
			{
				num = this.FindMatchInRange(text, start, 0, column);
				if (num == -1 && start != itemCount)
				{
					num = this.FindMatchInRange(text, itemCount - 1, start + 1, column);
				}
			}
			return num;
		}

		protected virtual int FindMatchInRange(string text, int first, int last, OLVColumn column)
		{
			if (first <= last)
			{
				for (int i = first; i <= last; i++)
				{
					string stringValue = column.GetStringValue(this.GetNthItemInDisplayOrder(i).RowObject);
					if (stringValue.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = first; j >= last; j--)
				{
					string stringValue2 = column.GetStringValue(this.GetNthItemInDisplayOrder(j).RowObject);
					if (stringValue2.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
					{
						return j;
					}
				}
			}
			return -1;
		}

		protected virtual bool HandleGroupInfo(ref Message m)
		{
			NativeMethods.NMLVGROUP nmlvgroup = (NativeMethods.NMLVGROUP)m.GetLParam(typeof(NativeMethods.NMLVGROUP));
			if ((nmlvgroup.uOldState & 49U) == (nmlvgroup.uNewState & 49U))
			{
				return false;
			}
			foreach (OLVGroup olvgroup in this.OLVGroups)
			{
				if (olvgroup.GroupId == nmlvgroup.iGroupId)
				{
					GroupStateChangedEventArgs args = new GroupStateChangedEventArgs(olvgroup, (GroupState)nmlvgroup.uOldState, (GroupState)nmlvgroup.uNewState);
					this.OnGroupStateChanged(args);
					break;
				}
			}
			return false;
		}

		protected virtual bool HandleKeyDown(ref Message m)
		{
			if (this.CheckBoxes && m.WParam.ToInt32() == 32 && base.SelectedIndices.Count > 0)
			{
				this.ToggleSelectedRowCheckBoxes();
				return true;
			}
			int scrollPosition = NativeMethods.GetScrollPosition(this, true);
			int scrollPosition2 = NativeMethods.GetScrollPosition(this, false);
			base.WndProc(ref m);
			if (base.IsDisposed)
			{
				return true;
			}
			int scrollPosition3 = NativeMethods.GetScrollPosition(this, true);
			int scrollPosition4 = NativeMethods.GetScrollPosition(this, false);
			if (scrollPosition != scrollPosition3)
			{
				ScrollEventArgs e = new ScrollEventArgs(ScrollEventType.EndScroll, scrollPosition, scrollPosition3, ScrollOrientation.HorizontalScroll);
				this.OnScroll(e);
				this.RefreshHotItem();
			}
			if (scrollPosition2 != scrollPosition4)
			{
				ScrollEventArgs e2 = new ScrollEventArgs(ScrollEventType.EndScroll, scrollPosition2, scrollPosition4, ScrollOrientation.VerticalScroll);
				this.OnScroll(e2);
				this.RefreshHotItem();
			}
			return true;
		}

		private void ToggleSelectedRowCheckBoxes()
		{
			object rowObject = this.GetItem(base.SelectedIndices[0]).RowObject;
			this.ToggleCheckObject(rowObject);
			CheckState? checkState = this.GetCheckState(rowObject);
			if (checkState != null)
			{
				foreach (object modelObject in this.SelectedObjects)
				{
					this.SetObjectCheckedness(modelObject, checkState.Value);
				}
			}
		}

		protected virtual bool HandleLButtonDown(ref Message m)
		{
			int x = m.LParam.ToInt32() & 65535;
			int y = m.LParam.ToInt32() >> 16 & 65535;
			return this.ProcessLButtonDown(this.OlvHitTest(x, y));
		}

		protected virtual bool ProcessLButtonDown(OlvListViewHitTestInfo hti)
		{
			if (hti.Item == null)
			{
				return false;
			}
			if (this.View == View.Details)
			{
				if (hti.HitTestLocation == HitTestLocation.CheckBox)
				{
					if (hti.Column.Index > 0)
					{
						if (hti.Column.IsEditable)
						{
							this.ToggleSubItemCheckBox(hti.RowObject, hti.Column);
						}
						return true;
					}
					this.ToggleCheckObject(hti.RowObject);
					if (hti.Item.Selected)
					{
						CheckState? checkState = this.GetCheckState(hti.RowObject);
						if (checkState != null)
						{
							foreach (object modelObject in this.SelectedObjects)
							{
								this.SetObjectCheckedness(modelObject, checkState.Value);
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		protected virtual bool HandleLButtonUp(ref Message m)
		{
			if (this.MouseMoveHitTest == null)
			{
				return false;
			}
			if (this.MouseMoveHitTest.HitTestLocation == HitTestLocation.GroupExpander && this.TriggerGroupExpandCollapse(this.MouseMoveHitTest.Group))
			{
				return true;
			}
			if (ObjectListView.IsVistaOrLater && this.HasCollapsibleGroups)
			{
				base.DefWndProc(ref m);
			}
			return false;
		}

		protected virtual bool TriggerGroupExpandCollapse(OLVGroup group)
		{
			GroupExpandingCollapsingEventArgs groupExpandingCollapsingEventArgs = new GroupExpandingCollapsingEventArgs(group);
			this.OnGroupExpandingCollapsing(groupExpandingCollapsingEventArgs);
			return groupExpandingCollapsingEventArgs.Canceled;
		}

		protected virtual bool HandleRButtonDown(ref Message m)
		{
			int x = m.LParam.ToInt32() & 65535;
			int y = m.LParam.ToInt32() >> 16 & 65535;
			return this.ProcessRButtonDown(this.OlvHitTest(x, y));
		}

		protected virtual bool ProcessRButtonDown(OlvListViewHitTestInfo hti)
		{
			return hti.Item != null && this.View == View.Details && hti.HitTestLocation == HitTestLocation.CheckBox;
		}

		protected virtual bool HandleLButtonDoubleClick(ref Message m)
		{
			int x = m.LParam.ToInt32() & 65535;
			int y = m.LParam.ToInt32() >> 16 & 65535;
			return this.ProcessLButtonDoubleClick(this.OlvHitTest(x, y));
		}

		protected virtual bool ProcessLButtonDoubleClick(OlvListViewHitTestInfo hti)
		{
			return hti.HitTestLocation == HitTestLocation.CheckBox;
		}

		protected virtual bool HandleRButtonDoubleClick(ref Message m)
		{
			int x = m.LParam.ToInt32() & 65535;
			int y = m.LParam.ToInt32() >> 16 & 65535;
			return this.ProcessRButtonDoubleClick(this.OlvHitTest(x, y));
		}

		protected virtual bool ProcessRButtonDoubleClick(OlvListViewHitTestInfo hti)
		{
			return hti.HitTestLocation == HitTestLocation.CheckBox;
		}

		protected virtual bool HandleMouseMove(ref Message m)
		{
			return false;
		}

		protected virtual bool HandleReflectNotify(ref Message m)
		{
			bool result = false;
			NativeMethods.NMHDR structure = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
			int code = structure.code;
			if (code <= -156)
			{
				if (code <= -180)
				{
					if (code != -188)
					{
						switch (code)
						{
						case -184:
							result = this.HandleLinkClick(ref m);
							break;
						case -181:
							result = this.HandleEndScroll(ref m);
							break;
						case -180:
							result = this.HandleBeginScroll(ref m);
							break;
						}
					}
					else
					{
						result = this.HandleGroupInfo(ref m);
					}
				}
				else if (code != -177)
				{
					switch (code)
					{
					case -158:
						result = (((NativeMethods.NMLVGETINFOTIP)m.GetLParam(typeof(NativeMethods.NMLVGETINFOTIP))).iItem >= this.GetItemCount());
						break;
					case -156:
						this.isMarqueSelecting = true;
						break;
					}
				}
			}
			else if (code <= -100)
			{
				if (code != -121)
				{
					switch (code)
					{
					case -101:
					{
						NativeMethods.NMLISTVIEW structure2 = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
						if ((structure2.uChanged & 8) != 0)
						{
							CheckState checkState = this.CalculateCheckState(structure2.uOldState);
							CheckState checkState2 = this.CalculateCheckState(structure2.uNewState);
							if (checkState != checkState2)
							{
								structure2.uOldState &= 4095;
								structure2.uNewState &= 4095;
								Marshal.StructureToPtr<NativeMethods.NMLISTVIEW>(structure2, m.LParam, false);
							}
						}
						break;
					}
					case -100:
					{
						NativeMethods.NMLISTVIEW structure3 = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
						if ((structure3.uChanged & 8) != 0)
						{
							CheckState checkState3 = this.CalculateCheckState(structure3.uOldState);
							CheckState checkState4 = this.CalculateCheckState(structure3.uNewState);
							if (checkState3 != checkState4)
							{
								structure3.uChanged &= -9;
								Marshal.StructureToPtr<NativeMethods.NMLISTVIEW>(structure3, m.LParam, false);
							}
						}
						break;
					}
					}
				}
			}
			else if (code != -16)
			{
				if (code != -12)
				{
					switch (code)
					{
					case -3:
						if (this.CheckBoxes)
						{
							structure.code = -6;
							Marshal.StructureToPtr<NativeMethods.NMHDR>(structure, m.LParam, false);
						}
						break;
					case -2:
						result = true;
						this.OnClick(EventArgs.Empty);
						break;
					}
				}
				else
				{
					result = this.HandleCustomDraw(ref m);
				}
			}
			else
			{
				this.isMarqueSelecting = false;
				base.Invalidate();
			}
			return result;
		}

		private CheckState CalculateCheckState(int state)
		{
			switch ((state & 61440) >> 12)
			{
			case 1:
				return CheckState.Unchecked;
			case 2:
				return CheckState.Checked;
			case 3:
				return CheckState.Indeterminate;
			default:
				return CheckState.Checked;
			}
		}

		protected bool HandleNotify(ref Message m)
		{
			bool result = false;
			NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
			int code = nmheader.nhdr.code;
			if (code <= -521)
			{
				if (code != -530)
				{
					switch (code)
					{
					case -522:
						if (this.CellToolTip.Handle == nmheader.nhdr.hwndFrom)
						{
							result = this.CellToolTip.HandlePop(ref m);
						}
						break;
					case -521:
						if (this.CellToolTip.Handle == nmheader.nhdr.hwndFrom)
						{
							result = this.CellToolTip.HandleShow(ref m);
						}
						break;
					}
				}
				else if (this.CellToolTip.Handle == nmheader.nhdr.hwndFrom)
				{
					result = this.CellToolTip.HandleGetDispInfo(ref m);
				}
			}
			else
			{
				switch (code)
				{
				case -328:
					break;
				case -327:
				case -324:
				case -323:
				case -321:
					return result;
				case -326:
				case -325:
					goto IL_223;
				case -322:
					goto IL_28E;
				case -320:
					goto IL_2AC;
				default:
					switch (code)
					{
					case -308:
						break;
					case -307:
					case -304:
					case -303:
					case -301:
						return result;
					case -306:
					case -305:
						goto IL_223;
					case -302:
						goto IL_28E;
					case -300:
						goto IL_2AC;
					default:
						if (code == -12 && !this.OwnerDrawnHeader)
						{
							return this.HeaderControl.HandleHeaderCustomDraw(ref m);
						}
						return result;
					}
					break;
				}
				if (nmheader.iItem >= 0 && nmheader.iItem < this.Columns.Count)
				{
					NativeMethods.HDITEM structure = (NativeMethods.HDITEM)Marshal.PtrToStructure(nmheader.pHDITEM, typeof(NativeMethods.HDITEM));
					OLVColumn column = this.GetColumn(nmheader.iItem);
					if (structure.cxy < column.MinimumWidth)
					{
						structure.cxy = column.MinimumWidth;
					}
					else if (column.MaximumWidth != -1 && structure.cxy > column.MaximumWidth)
					{
						structure.cxy = column.MaximumWidth;
					}
					Marshal.StructureToPtr<NativeMethods.HDITEM>(structure, nmheader.pHDITEM, false);
					return result;
				}
				return result;
				IL_223:
				if (!this.PossibleFinishCellEditing())
				{
					m.Result = (IntPtr)1;
					return true;
				}
				if (nmheader.iItem < 0 || nmheader.iItem >= this.Columns.Count)
				{
					return result;
				}
				OLVColumn column2 = this.GetColumn(nmheader.iItem);
				if (column2.FillsFreeSpace)
				{
					m.Result = (IntPtr)1;
					return true;
				}
				return result;
				IL_28E:
				if (!this.PossibleFinishCellEditing())
				{
					m.Result = (IntPtr)1;
					return true;
				}
				return result;
				IL_2AC:
				nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (nmheader.iItem >= 0 && nmheader.iItem < this.Columns.Count)
				{
					NativeMethods.HDITEM hditem = (NativeMethods.HDITEM)Marshal.PtrToStructure(nmheader.pHDITEM, typeof(NativeMethods.HDITEM));
					OLVColumn column3 = this.GetColumn(nmheader.iItem);
					if ((hditem.mask & 1) == 1 && (hditem.cxy < column3.MinimumWidth || (column3.MaximumWidth != -1 && hditem.cxy > column3.MaximumWidth)))
					{
						m.Result = (IntPtr)1;
						result = true;
					}
				}
			}
			return result;
		}

		protected virtual void CreateCellToolTip()
		{
			this.cellToolTip = new ToolTipControl();
			this.cellToolTip.AssignHandle(NativeMethods.GetTooltipControl(this));
			this.cellToolTip.Showing += this.HandleCellToolTipShowing;
			this.cellToolTip.SetMaxWidth();
			NativeMethods.MakeTopMost(this.cellToolTip);
		}

		protected virtual void UpdateCellToolTipHandle()
		{
			if (this.cellToolTip != null && this.cellToolTip.Handle == IntPtr.Zero)
			{
				this.cellToolTip.AssignHandle(NativeMethods.GetTooltipControl(this));
			}
		}

		protected virtual bool HandlePaint(ref Message m)
		{
			this.isInWmPaintEvent = true;
			this.shouldDoCustomDrawing = true;
			this.prePaintLevel = 0;
			this.ShowOverlays();
			this.HandlePrePaint();
			base.WndProc(ref m);
			this.HandlePostPaint();
			this.isInWmPaintEvent = false;
			return true;
		}

		protected virtual void HandlePrePaint()
		{
		}

		protected virtual void HandlePostPaint()
		{
		}

		protected virtual bool HandleWindowPosChanging(ref Message m)
		{
			NativeMethods.WINDOWPOS windowpos = (NativeMethods.WINDOWPOS)m.GetLParam(typeof(NativeMethods.WINDOWPOS));
			if ((windowpos.flags & 1) == 0 && windowpos.cx < base.Bounds.Width)
			{
				this.ResizeFreeSpaceFillingColumns(windowpos.cx - (base.Bounds.Width - base.ClientSize.Width));
			}
			return false;
		}

		protected virtual bool HandleHeaderRightClick(int columnIndex)
		{
			ColumnClickEventArgs e = new ColumnClickEventArgs(columnIndex);
			this.OnColumnRightClick(e);
			return this.ShowHeaderRightClickMenu(columnIndex, Cursor.Position);
		}

		protected virtual bool ShowHeaderRightClickMenu(int columnIndex, Point pt)
		{
			ToolStripDropDown toolStripDropDown = this.MakeHeaderRightClickMenu(columnIndex);
			if (toolStripDropDown.Items.Count > 0)
			{
				toolStripDropDown.Show(pt);
				return true;
			}
			return false;
		}

		protected virtual ToolStripDropDown MakeHeaderRightClickMenu(int columnIndex)
		{
			ToolStripDropDown toolStripDropDown = new ContextMenuStrip();
			if (columnIndex >= 0 && this.UseFiltering && this.ShowFilterMenuOnRightClick)
			{
				toolStripDropDown = this.MakeFilteringMenu(toolStripDropDown, columnIndex);
			}
			if (columnIndex >= 0 && this.ShowCommandMenuOnRightClick)
			{
				toolStripDropDown = this.MakeColumnCommandMenu(toolStripDropDown, columnIndex);
			}
			if (this.SelectColumnsOnRightClickBehaviour != ObjectListView.ColumnSelectBehaviour.None)
			{
				toolStripDropDown = this.MakeColumnSelectMenu(toolStripDropDown);
			}
			return toolStripDropDown;
		}

		[Obsolete("Use HandleHeaderRightClick(int) instead")]
		protected virtual bool HandleHeaderRightClick()
		{
			return false;
		}

		[Obsolete("Use ShowHeaderRightClickMenu instead")]
		protected virtual void ShowColumnSelectMenu(Point pt)
		{
			ToolStripDropDown toolStripDropDown = this.MakeColumnSelectMenu(new ContextMenuStrip());
			toolStripDropDown.Show(pt);
		}

		[Obsolete("Use ShowHeaderRightClickMenu instead")]
		protected virtual void ShowColumnCommandMenu(int columnIndex, Point pt)
		{
			ToolStripDropDown toolStripDropDown = this.MakeColumnCommandMenu(new ContextMenuStrip(), columnIndex);
			if (this.SelectColumnsOnRightClick)
			{
				if (toolStripDropDown.Items.Count > 0)
				{
					toolStripDropDown.Items.Add(new ToolStripSeparator());
				}
				this.MakeColumnSelectMenu(toolStripDropDown);
			}
			toolStripDropDown.Show(pt);
		}

		[Localizable(true)]
		[Category("Labels - ObjectListView")]
		[DefaultValue("Sort ascending by '{0}'")]
		public string MenuLabelSortAscending
		{
			get
			{
				return this.menuLabelSortAscending;
			}
			set
			{
				this.menuLabelSortAscending = value;
			}
		}

		[Category("Labels - ObjectListView")]
		[Localizable(true)]
		[DefaultValue("Sort descending by '{0}'")]
		public string MenuLabelSortDescending
		{
			get
			{
				return this.menuLabelSortDescending;
			}
			set
			{
				this.menuLabelSortDescending = value;
			}
		}

		[Category("Labels - ObjectListView")]
		[Localizable(true)]
		[DefaultValue("Group by '{0}'")]
		public string MenuLabelGroupBy
		{
			get
			{
				return this.menuLabelGroupBy;
			}
			set
			{
				this.menuLabelGroupBy = value;
			}
		}

		[Category("Labels - ObjectListView")]
		[DefaultValue("Lock grouping on '{0}'")]
		[Localizable(true)]
		public string MenuLabelLockGroupingOn
		{
			get
			{
				return this.menuLabelLockGroupingOn;
			}
			set
			{
				this.menuLabelLockGroupingOn = value;
			}
		}

		[DefaultValue("Unlock grouping from '{0}'")]
		[Category("Labels - ObjectListView")]
		[Localizable(true)]
		public string MenuLabelUnlockGroupingOn
		{
			get
			{
				return this.menuLabelUnlockGroupingOn;
			}
			set
			{
				this.menuLabelUnlockGroupingOn = value;
			}
		}

		[Localizable(true)]
		[DefaultValue("Turn off groups")]
		[Category("Labels - ObjectListView")]
		public string MenuLabelTurnOffGroups
		{
			get
			{
				return this.menuLabelTurnOffGroups;
			}
			set
			{
				this.menuLabelTurnOffGroups = value;
			}
		}

		[DefaultValue("Unsort")]
		[Localizable(true)]
		[Category("Labels - ObjectListView")]
		public string MenuLabelUnsort
		{
			get
			{
				return this.menuLabelUnsort;
			}
			set
			{
				this.menuLabelUnsort = value;
			}
		}

		[Localizable(true)]
		[DefaultValue("Columns")]
		[Category("Labels - ObjectListView")]
		public string MenuLabelColumns
		{
			get
			{
				return this.menuLabelColumns;
			}
			set
			{
				this.menuLabelColumns = value;
			}
		}

		[Category("Labels - ObjectListView")]
		[Localizable(true)]
		[DefaultValue("Select Columns...")]
		public string MenuLabelSelectColumns
		{
			get
			{
				return this.menuLabelSelectColumns;
			}
			set
			{
				this.menuLabelSelectColumns = value;
			}
		}

		public virtual ToolStripDropDown MakeColumnCommandMenu(ToolStripDropDown strip, int columnIndex)
		{
			OLVColumn column = this.GetColumn(columnIndex);
			if (column == null)
			{
				return strip;
			}
			if (strip.Items.Count > 0)
			{
				strip.Items.Add(new ToolStripSeparator());
			}
			string text = string.Format(this.MenuLabelSortAscending, column.Text);
			if (column.Sortable && !string.IsNullOrEmpty(text))
			{
				strip.Items.Add(text, ObjectListView.SortAscendingImage, delegate(object sender, EventArgs e)
				{
					this.Sort(column, SortOrder.Ascending);
				});
			}
			text = string.Format(this.MenuLabelSortDescending, column.Text);
			if (column.Sortable && !string.IsNullOrEmpty(text))
			{
				strip.Items.Add(text, ObjectListView.SortDescendingImage, delegate(object sender, EventArgs e)
				{
					this.Sort(column, SortOrder.Descending);
				});
			}
			if (this.CanShowGroups)
			{
				text = string.Format(this.MenuLabelGroupBy, column.Text);
				if (column.Groupable && !string.IsNullOrEmpty(text))
				{
					strip.Items.Add(text, null, delegate(object sender, EventArgs e)
					{
						this.ShowGroups = true;
						this.PrimarySortColumn = column;
						this.PrimarySortOrder = SortOrder.Ascending;
						this.BuildList();
					});
				}
			}
			if (this.ShowGroups)
			{
				if (this.AlwaysGroupByColumn == column)
				{
					text = string.Format(this.MenuLabelUnlockGroupingOn, column.Text);
					if (!string.IsNullOrEmpty(text))
					{
						strip.Items.Add(text, null, delegate(object sender, EventArgs e)
						{
							this.AlwaysGroupByColumn = null;
							this.AlwaysGroupBySortOrder = SortOrder.None;
							this.BuildList();
						});
					}
				}
				else
				{
					text = string.Format(this.MenuLabelLockGroupingOn, column.Text);
					if (column.Groupable && !string.IsNullOrEmpty(text))
					{
						strip.Items.Add(text, null, delegate(object sender, EventArgs e)
						{
							this.ShowGroups = true;
							this.AlwaysGroupByColumn = column;
							this.AlwaysGroupBySortOrder = SortOrder.Ascending;
							this.BuildList();
						});
					}
				}
				text = string.Format(this.MenuLabelTurnOffGroups, column.Text);
				if (!string.IsNullOrEmpty(text))
				{
					strip.Items.Add(text, null, delegate(object sender, EventArgs e)
					{
						this.ShowGroups = false;
						this.BuildList();
					});
				}
			}
			else
			{
				text = string.Format(this.MenuLabelUnsort, column.Text);
				if (column.Sortable && !string.IsNullOrEmpty(text) && this.PrimarySortOrder != SortOrder.None)
				{
					strip.Items.Add(text, null, delegate(object sender, EventArgs e)
					{
						this.Unsort();
					});
				}
			}
			return strip;
		}

		public virtual ToolStripDropDown MakeColumnSelectMenu(ToolStripDropDown strip)
		{
			if (strip.Items.Count > 0 && !(strip.Items[strip.Items.Count - 1] is ToolStripSeparator))
			{
				strip.Items.Add(new ToolStripSeparator());
			}
			if (this.AllColumns.Count > 0 && this.AllColumns[0].LastDisplayIndex == -1)
			{
				this.RememberDisplayIndicies();
			}
			if (this.SelectColumnsOnRightClickBehaviour == ObjectListView.ColumnSelectBehaviour.ModelDialog)
			{
				strip.Items.Add(this.MenuLabelSelectColumns, null, delegate(object sender, EventArgs e)
				{
					new ColumnSelectionForm().OpenOn(this);
				});
			}
			if (this.SelectColumnsOnRightClickBehaviour == ObjectListView.ColumnSelectBehaviour.Submenu)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(this.MenuLabelColumns);
				toolStripMenuItem.DropDownItemClicked += this.ColumnSelectMenuItemClicked;
				strip.Items.Add(toolStripMenuItem);
				this.AddItemsToColumnSelectMenu(toolStripMenuItem.DropDownItems);
			}
			if (this.SelectColumnsOnRightClickBehaviour == ObjectListView.ColumnSelectBehaviour.InlineMenu)
			{
				strip.ItemClicked += this.ColumnSelectMenuItemClicked;
				strip.Closing += this.ColumnSelectMenuClosing;
				this.AddItemsToColumnSelectMenu(strip.Items);
			}
			return strip;
		}

		protected void AddItemsToColumnSelectMenu(ToolStripItemCollection items)
		{
			List<OLVColumn> list = new List<OLVColumn>(this.AllColumns);
			list.Sort((OLVColumn x, OLVColumn y) => x.LastDisplayIndex - y.LastDisplayIndex);
			foreach (OLVColumn olvcolumn in list)
			{
				items.Add(new ToolStripMenuItem(olvcolumn.Text)
				{
					Checked = olvcolumn.IsVisible,
					Tag = olvcolumn,
					Enabled = (!olvcolumn.IsVisible || olvcolumn.CanBeHidden)
				});
			}
		}

		private void ColumnSelectMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			this.contextMenuStaysOpen = false;
			ToolStripMenuItem toolStripMenuItem = e.ClickedItem as ToolStripMenuItem;
			if (toolStripMenuItem == null)
			{
				return;
			}
			OLVColumn olvcolumn = toolStripMenuItem.Tag as OLVColumn;
			if (olvcolumn == null)
			{
				return;
			}
			toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
			olvcolumn.IsVisible = toolStripMenuItem.Checked;
			this.contextMenuStaysOpen = this.SelectColumnsMenuStaysOpen;
			base.BeginInvoke(new MethodInvoker(this.RebuildColumns));
		}

		private void ColumnSelectMenuClosing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			e.Cancel = (this.contextMenuStaysOpen && e.CloseReason == ToolStripDropDownCloseReason.ItemClicked);
			this.contextMenuStaysOpen = false;
		}

		public virtual ToolStripDropDown MakeFilteringMenu(ToolStripDropDown strip, int columnIndex)
		{
			OLVColumn column = this.GetColumn(columnIndex);
			if (column == null)
			{
				return strip;
			}
			FilterMenuBuilder filterMenuBuildStrategy = this.FilterMenuBuildStrategy;
			if (filterMenuBuildStrategy == null)
			{
				return strip;
			}
			return filterMenuBuildStrategy.MakeFilterMenu(strip, this, column);
		}

		protected override void OnColumnReordered(ColumnReorderedEventArgs e)
		{
			base.OnColumnReordered(e);
			base.BeginInvoke(new MethodInvoker(this.RememberDisplayIndicies));
		}

		private void RememberDisplayIndicies()
		{
			foreach (OLVColumn olvcolumn in this.AllColumns)
			{
				olvcolumn.LastDisplayIndex = olvcolumn.DisplayIndex;
			}
		}

		protected virtual void HandleColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			if (this.UpdateSpaceFillingColumnsWhenDraggingColumnDivider && !this.GetColumn(e.ColumnIndex).FillsFreeSpace)
			{
				int width = this.GetColumn(e.ColumnIndex).Width;
				if (e.NewWidth > width)
				{
					this.ResizeFreeSpaceFillingColumns(base.ClientSize.Width - (e.NewWidth - width));
					return;
				}
				this.ResizeFreeSpaceFillingColumns();
			}
		}

		protected virtual void HandleColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
		{
			if (!this.GetColumn(e.ColumnIndex).FillsFreeSpace)
			{
				this.ResizeFreeSpaceFillingColumns();
			}
		}

		protected virtual void HandleLayout(object sender, LayoutEventArgs e)
		{
			if (!this.hasResizeColumnsHandler)
			{
				this.hasResizeColumnsHandler = true;
				this.RunWhenIdle(new EventHandler(this.HandleApplicationIdleResizeColumns));
			}
		}

		private void RunWhenIdle(EventHandler eventHandler)
		{
			Application.Idle += eventHandler;
			if (!this.CanUseApplicationIdle)
			{
				SynchronizationContext.Current.Post(delegate(object x)
				{
					Application.RaiseIdle(EventArgs.Empty);
				}, null);
			}
		}

		protected virtual void ResizeFreeSpaceFillingColumns()
		{
			this.ResizeFreeSpaceFillingColumns(base.ClientSize.Width);
		}

		protected virtual void ResizeFreeSpaceFillingColumns(int freeSpace)
		{
			if (base.DesignMode)
			{
				return;
			}
			if (this.Frozen)
			{
				return;
			}
			int num = 0;
			List<OLVColumn> list = new List<OLVColumn>();
			for (int i = 0; i < this.Columns.Count; i++)
			{
				OLVColumn column = this.GetColumn(i);
				if (column.FillsFreeSpace)
				{
					list.Add(column);
					num += column.FreeSpaceProportion;
				}
				else
				{
					freeSpace -= column.Width;
				}
			}
			freeSpace = Math.Max(0, freeSpace);
			foreach (OLVColumn olvcolumn in list.ToArray())
			{
				int num2 = freeSpace * olvcolumn.FreeSpaceProportion / num;
				if (olvcolumn.MinimumWidth != -1 && num2 < olvcolumn.MinimumWidth)
				{
					num2 = olvcolumn.MinimumWidth;
				}
				else if (olvcolumn.MaximumWidth != -1 && num2 > olvcolumn.MaximumWidth)
				{
					num2 = olvcolumn.MaximumWidth;
				}
				else
				{
					num2 = 0;
				}
				if (num2 > 0)
				{
					olvcolumn.Width = num2;
					freeSpace -= num2;
					num -= olvcolumn.FreeSpaceProportion;
					list.Remove(olvcolumn);
				}
			}
			try
			{
				base.BeginUpdate();
				foreach (OLVColumn olvcolumn2 in list)
				{
					olvcolumn2.Width = freeSpace * olvcolumn2.FreeSpaceProportion / num;
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		public virtual void CheckIndeterminateObject(object modelObject)
		{
			this.SetObjectCheckedness(modelObject, CheckState.Indeterminate);
		}

		public virtual void CheckObject(object modelObject)
		{
			this.SetObjectCheckedness(modelObject, CheckState.Checked);
		}

		public virtual void CheckObjects(IEnumerable modelObjects)
		{
			foreach (object modelObject in modelObjects)
			{
				this.CheckObject(modelObject);
			}
		}

		public virtual void CheckSubItem(object rowObject, OLVColumn column)
		{
			if (column != null && rowObject != null && column.CheckBoxes)
			{
				column.PutCheckState(rowObject, CheckState.Checked);
				this.RefreshObject(rowObject);
				return;
			}
		}

		public virtual void CheckIndeterminateSubItem(object rowObject, OLVColumn column)
		{
			if (column != null && rowObject != null && column.CheckBoxes)
			{
				column.PutCheckState(rowObject, CheckState.Indeterminate);
				this.RefreshObject(rowObject);
				return;
			}
		}

		public virtual bool IsChecked(object modelObject)
		{
			return this.GetCheckState(modelObject) == CheckState.Checked;
		}

		public virtual bool IsCheckedIndeterminate(object modelObject)
		{
			return this.GetCheckState(modelObject) == CheckState.Indeterminate;
		}

		public virtual bool IsSubItemChecked(object rowObject, OLVColumn column)
		{
			return column != null && rowObject != null && column.CheckBoxes && column.GetCheckState(rowObject) == CheckState.Checked;
		}

		protected virtual CheckState? GetCheckState(object modelObject)
		{
			if (this.CheckStateGetter != null)
			{
				return new CheckState?(this.CheckStateGetter(modelObject));
			}
			if (!this.PersistentCheckBoxes)
			{
				return null;
			}
			return new CheckState?(this.GetPersistentCheckState(modelObject));
		}

		protected virtual CheckState PutCheckState(object modelObject, CheckState state)
		{
			if (this.CheckStatePutter != null)
			{
				return this.CheckStatePutter(modelObject, state);
			}
			if (!this.PersistentCheckBoxes)
			{
				return state;
			}
			return this.SetPersistentCheckState(modelObject, state);
		}

		protected virtual bool SetObjectCheckedness(object modelObject, CheckState state)
		{
			if (this.GetCheckState(modelObject) == state)
			{
				return false;
			}
			OLVListItem olvlistItem = this.ModelToItem(modelObject);
			if (olvlistItem == null)
			{
				this.PutCheckState(modelObject, state);
				return true;
			}
			ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(olvlistItem.Index, state, olvlistItem.CheckState);
			this.OnItemCheck(itemCheckEventArgs);
			if (itemCheckEventArgs.NewValue == olvlistItem.CheckState)
			{
				return false;
			}
			olvlistItem.CheckState = this.PutCheckState(modelObject, state);
			this.RefreshItem(olvlistItem);
			this.OnItemChecked(new ItemCheckedEventArgs(olvlistItem));
			return true;
		}

		public virtual void ToggleCheckObject(object modelObject)
		{
			OLVListItem olvlistItem = this.ModelToItem(modelObject);
			if (olvlistItem == null)
			{
				return;
			}
			CheckState state = CheckState.Checked;
			if (olvlistItem.CheckState == CheckState.Checked)
			{
				state = (this.TriStateCheckBoxes ? CheckState.Indeterminate : CheckState.Unchecked);
			}
			else if (olvlistItem.CheckState == CheckState.Indeterminate && this.TriStateCheckBoxes)
			{
				state = CheckState.Unchecked;
			}
			this.SetObjectCheckedness(modelObject, state);
		}

		public virtual void ToggleSubItemCheckBox(object rowObject, OLVColumn column)
		{
			CheckState checkState = column.GetCheckState(rowObject);
			CheckState newValue = this.CalculateToggledCheckState(column, checkState);
			SubItemCheckingEventArgs subItemCheckingEventArgs = new SubItemCheckingEventArgs(column, this.ModelToItem(rowObject), column.Index, checkState, newValue);
			this.OnSubItemChecking(subItemCheckingEventArgs);
			if (subItemCheckingEventArgs.Canceled)
			{
				return;
			}
			switch (subItemCheckingEventArgs.NewValue)
			{
			case CheckState.Unchecked:
				this.UncheckSubItem(rowObject, column);
				return;
			case CheckState.Checked:
				this.CheckSubItem(rowObject, column);
				return;
			case CheckState.Indeterminate:
				this.CheckIndeterminateSubItem(rowObject, column);
				return;
			default:
				return;
			}
		}

		private CheckState CalculateToggledCheckState(OLVColumn column, CheckState currentState)
		{
			switch (currentState)
			{
			case CheckState.Checked:
				if (!column.TriStateCheckBoxes)
				{
					return CheckState.Unchecked;
				}
				return CheckState.Indeterminate;
			case CheckState.Indeterminate:
				return CheckState.Unchecked;
			default:
				return CheckState.Checked;
			}
		}

		public virtual void UncheckObject(object modelObject)
		{
			this.SetObjectCheckedness(modelObject, CheckState.Unchecked);
		}

		public virtual void UncheckObjects(IEnumerable modelObjects)
		{
			foreach (object modelObject in modelObjects)
			{
				this.UncheckObject(modelObject);
			}
		}

		public virtual void UncheckSubItem(object rowObject, OLVColumn column)
		{
			if (column != null && rowObject != null && column.CheckBoxes)
			{
				column.PutCheckState(rowObject, CheckState.Unchecked);
				this.RefreshObject(rowObject);
				return;
			}
		}

		public virtual OLVColumn GetColumn(int index)
		{
			return (OLVColumn)this.Columns[index];
		}

		public virtual OLVColumn GetColumn(string name)
		{
			foreach (object obj in this.Columns)
			{
				ColumnHeader columnHeader = (ColumnHeader)obj;
				if (columnHeader.Text == name)
				{
					return (OLVColumn)columnHeader;
				}
			}
			return null;
		}

		public virtual List<OLVColumn> GetFilteredColumns(View view)
		{
			int index = 0;
			return this.AllColumns.FindAll((OLVColumn x) => index++ == 0 || x.IsVisible);
		}

		public virtual int GetItemCount()
		{
			return this.Items.Count;
		}

		public virtual OLVListItem GetItem(int index)
		{
			if (index >= 0 && index < this.GetItemCount())
			{
				return (OLVListItem)this.Items[index];
			}
			return null;
		}

		public virtual object GetModelObject(int index)
		{
			OLVListItem item = this.GetItem(index);
			if (item != null)
			{
				return item.RowObject;
			}
			return null;
		}

		public virtual OLVListItem GetItemAt(int x, int y, out OLVColumn hitColumn)
		{
			hitColumn = null;
			ListViewHitTestInfo listViewHitTestInfo = this.HitTest(x, y);
			if (listViewHitTestInfo.Item == null)
			{
				return null;
			}
			if (listViewHitTestInfo.SubItem != null)
			{
				int index = listViewHitTestInfo.Item.SubItems.IndexOf(listViewHitTestInfo.SubItem);
				hitColumn = this.GetColumn(index);
			}
			return (OLVListItem)listViewHitTestInfo.Item;
		}

		public virtual OLVListSubItem GetSubItem(int index, int columnIndex)
		{
			OLVListItem item = this.GetItem(index);
			if (item != null)
			{
				return item.GetSubItem(columnIndex);
			}
			return null;
		}

		public virtual void EnsureGroupVisible(ListViewGroup lvg)
		{
			if (!this.ShowGroups || lvg == null)
			{
				return;
			}
			int num = this.Groups.IndexOf(lvg);
			if (num <= 0)
			{
				int dy = -NativeMethods.GetScrollPosition(this, false);
				NativeMethods.Scroll(this, 0, dy);
				return;
			}
			ListViewGroup listViewGroup = this.Groups[num - 1];
			ListViewItem listViewItem = listViewGroup.Items[listViewGroup.Items.Count - 1];
			Rectangle itemRect = base.GetItemRect(listViewItem.Index);
			int dy2 = itemRect.Y + itemRect.Height / 2;
			NativeMethods.Scroll(this, 0, dy2);
		}

		public virtual void EnsureModelVisible(object modelObject)
		{
			int num = this.IndexOf(modelObject);
			if (num >= 0)
			{
				base.EnsureVisible(num);
			}
		}

		[Obsolete("Use SelectedObject property instead of this method")]
		public virtual object GetSelectedObject()
		{
			return this.SelectedObject;
		}

		[Obsolete("Use SelectedObjects property instead of this method")]
		public virtual ArrayList GetSelectedObjects()
		{
			return ObjectListView.EnumerableToArray(this.SelectedObjects, false);
		}

		[Obsolete("Use CheckedObject property instead of this method")]
		public virtual object GetCheckedObject()
		{
			return this.CheckedObject;
		}

		[Obsolete("Use CheckedObjects property instead of this method")]
		public virtual ArrayList GetCheckedObjects()
		{
			return ObjectListView.EnumerableToArray(this.CheckedObjects, false);
		}

		public virtual int IndexOf(object modelObject)
		{
			for (int i = 0; i < this.GetItemCount(); i++)
			{
				if (this.GetModelObject(i).Equals(modelObject))
				{
					return i;
				}
			}
			return -1;
		}

		public virtual void RefreshItem(OLVListItem olvi)
		{
			olvi.UseItemStyleForSubItems = true;
			olvi.SubItems.Clear();
			this.FillInValues(olvi, olvi.RowObject);
			this.PostProcessOneRow(olvi.Index, this.GetDisplayOrderOfItemIndex(olvi.Index), olvi);
		}

		public virtual void RefreshObject(object modelObject)
		{
			this.RefreshObjects(new object[]
			{
				modelObject
			});
		}

		public virtual void RefreshObjects(IList modelObjects)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.RefreshObjects(modelObjects);
				}));
				return;
			}
			foreach (object obj in modelObjects)
			{
				OLVListItem olvlistItem = this.ModelToItem(obj);
				if (olvlistItem != null)
				{
					this.ReplaceModel(olvlistItem, obj);
					this.RefreshItem(olvlistItem);
				}
			}
		}

		private void ReplaceModel(OLVListItem olvi, object newModel)
		{
			if (object.ReferenceEquals(olvi.RowObject, newModel))
			{
				return;
			}
			this.TakeOwnershipOfObjects();
			ArrayList arrayList = ObjectListView.EnumerableToArray(this.Objects, false);
			int num = arrayList.IndexOf(olvi.RowObject);
			if (num >= 0)
			{
				arrayList[num] = newModel;
			}
			olvi.RowObject = newModel;
		}

		public virtual void RefreshSelectedObjects()
		{
			foreach (object obj in base.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				this.RefreshItem((OLVListItem)listViewItem);
			}
		}

		public virtual void SelectObject(object modelObject)
		{
			this.SelectObject(modelObject, false);
		}

		public virtual void SelectObject(object modelObject, bool setFocus)
		{
			OLVListItem olvlistItem = this.ModelToItem(modelObject);
			if (olvlistItem != null)
			{
				olvlistItem.Selected = true;
				if (setFocus)
				{
					olvlistItem.Focused = true;
				}
			}
		}

		public virtual void SelectObjects(IList modelObjects)
		{
			base.SelectedIndices.Clear();
			if (modelObjects == null)
			{
				return;
			}
			foreach (object modelObject in modelObjects)
			{
				OLVListItem olvlistItem = this.ModelToItem(modelObject);
				if (olvlistItem != null)
				{
					olvlistItem.Selected = true;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool Frozen
		{
			get
			{
				return this.freezeCount > 0;
			}
			set
			{
				if (value)
				{
					this.Freeze();
					return;
				}
				if (this.freezeCount > 0)
				{
					this.freezeCount = 1;
					this.Unfreeze();
				}
			}
		}

		public virtual void Freeze()
		{
			this.freezeCount++;
			this.OnFreezing(new FreezeEventArgs(this.freezeCount));
		}

		public virtual void Unfreeze()
		{
			if (this.freezeCount <= 0)
			{
				return;
			}
			this.freezeCount--;
			if (this.freezeCount == 0)
			{
				this.DoUnfreeze();
			}
			this.OnFreezing(new FreezeEventArgs(this.freezeCount));
		}

		protected virtual void DoUnfreeze()
		{
			this.ResizeFreeSpaceFillingColumns();
			this.BuildList();
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool SelectionEventsSuspended
		{
			get
			{
				return this.suspendSelectionEventCount > 0;
			}
		}

		protected void SuspendSelectionEvents()
		{
			this.suspendSelectionEventCount++;
		}

		protected void ResumeSelectionEvents()
		{
			this.suspendSelectionEventCount--;
		}

		protected IDisposable SuspendSelectionEventsDuring()
		{
			return new ObjectListView.SuspendSelectionDisposable(this);
		}

		public new void Sort()
		{
			this.Sort(this.PrimarySortColumn, this.PrimarySortOrder);
		}

		public virtual void Sort(string columnToSortName)
		{
			this.Sort(this.GetColumn(columnToSortName), this.PrimarySortOrder);
		}

		public virtual void Sort(int columnToSortIndex)
		{
			if (columnToSortIndex >= 0 && columnToSortIndex < this.Columns.Count)
			{
				this.Sort(this.GetColumn(columnToSortIndex), this.PrimarySortOrder);
			}
		}

		public virtual void Sort(OLVColumn columnToSort)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.Sort(columnToSort);
				}));
				return;
			}
			this.Sort(columnToSort, this.PrimarySortOrder);
		}

		public virtual void Sort(OLVColumn columnToSort, SortOrder order)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(delegate()
				{
					this.Sort(columnToSort, order);
				}));
				return;
			}
			this.DoSort(columnToSort, order);
			this.PostProcessRows();
		}

		private void DoSort(OLVColumn columnToSort, SortOrder order)
		{
			if (this.GetItemCount() == 0 || this.Columns.Count == 0)
			{
				return;
			}
			if (this.ShowGroups)
			{
				columnToSort = (columnToSort ?? this.GetColumn(0));
				if (order == SortOrder.None)
				{
					order = base.Sorting;
					if (order == SortOrder.None)
					{
						order = SortOrder.Ascending;
					}
				}
			}
			BeforeSortingEventArgs beforeSortingEventArgs = this.BuildBeforeSortingEventArgs(columnToSort, order);
			this.OnBeforeSorting(beforeSortingEventArgs);
			if (beforeSortingEventArgs.Canceled)
			{
				return;
			}
			IList list = base.VirtualMode ? this.SelectedObjects : null;
			this.SuspendSelectionEvents();
			this.ClearHotItem();
			if (!beforeSortingEventArgs.Handled && beforeSortingEventArgs.ColumnToSort != null && beforeSortingEventArgs.SortOrder != SortOrder.None)
			{
				if (this.ShowGroups)
				{
					this.BuildGroups(beforeSortingEventArgs.ColumnToGroupBy, beforeSortingEventArgs.GroupByOrder, beforeSortingEventArgs.ColumnToSort, beforeSortingEventArgs.SortOrder, beforeSortingEventArgs.SecondaryColumnToSort, beforeSortingEventArgs.SecondarySortOrder);
				}
				else if (this.CustomSorter != null)
				{
					this.CustomSorter(beforeSortingEventArgs.ColumnToSort, beforeSortingEventArgs.SortOrder);
				}
				else
				{
					base.ListViewItemSorter = new ColumnComparer(beforeSortingEventArgs.ColumnToSort, beforeSortingEventArgs.SortOrder, beforeSortingEventArgs.SecondaryColumnToSort, beforeSortingEventArgs.SecondarySortOrder);
				}
			}
			if (this.ShowSortIndicators)
			{
				this.ShowSortIndicator(beforeSortingEventArgs.ColumnToSort, beforeSortingEventArgs.SortOrder);
			}
			this.PrimarySortColumn = beforeSortingEventArgs.ColumnToSort;
			this.PrimarySortOrder = beforeSortingEventArgs.SortOrder;
			if (list != null && list.Count > 0)
			{
				this.SelectedObjects = list;
			}
			this.ResumeSelectionEvents();
			this.RefreshHotItem();
			this.OnAfterSorting(new AfterSortingEventArgs(beforeSortingEventArgs));
		}

		public virtual void ShowSortIndicator()
		{
			if (this.ShowSortIndicators && this.PrimarySortOrder != SortOrder.None)
			{
				this.ShowSortIndicator(this.PrimarySortColumn, this.PrimarySortOrder);
			}
		}

		protected virtual void ShowSortIndicator(OLVColumn columnToSort, SortOrder sortOrder)
		{
			int imageIndex = -1;
			if (!NativeMethods.HasBuiltinSortIndicators())
			{
				if (this.SmallImageList == null || !this.SmallImageList.Images.ContainsKey(ObjectListView.getString_0(107315910)))
				{
					this.MakeSortIndicatorImages();
				}
				if (this.SmallImageList != null)
				{
					string key = (sortOrder == SortOrder.Ascending) ? ObjectListView.getString_0(107315910) : ObjectListView.getString_0(107315917);
					imageIndex = this.SmallImageList.Images.IndexOfKey(key);
				}
			}
			for (int i = 0; i < this.Columns.Count; i++)
			{
				if (columnToSort != null && i == columnToSort.Index)
				{
					NativeMethods.SetColumnImage(this, i, sortOrder, imageIndex);
				}
				else
				{
					NativeMethods.SetColumnImage(this, i, SortOrder.None, -1);
				}
			}
		}

		protected virtual void MakeSortIndicatorImages()
		{
			if (base.DesignMode)
			{
				return;
			}
			ImageList imageList = this.SmallImageList;
			if (imageList == null)
			{
				imageList = new ImageList();
				imageList.ImageSize = new Size(16, 16);
				imageList.ColorDepth = ColorDepth.Depth32Bit;
			}
			int num = imageList.ImageSize.Width / 2;
			int num2 = imageList.ImageSize.Height / 2 - 1;
			int num3 = num - 2;
			int num4 = num3 / 2;
			if (imageList.Images.IndexOfKey(ObjectListView.getString_0(107315910)) == -1)
			{
				Point point = new Point(num - num3, num2 + num4);
				Point point2 = new Point(num, num2 - num4 - 1);
				Point point3 = new Point(num + num3, num2 + num4);
				imageList.Images.Add(ObjectListView.getString_0(107315910), this.MakeTriangleBitmap(imageList.ImageSize, new Point[]
				{
					point,
					point2,
					point3
				}));
			}
			if (imageList.Images.IndexOfKey(ObjectListView.getString_0(107315917)) == -1)
			{
				Point point4 = new Point(num - num3, num2 - num4);
				Point point5 = new Point(num, num2 + num4);
				Point point6 = new Point(num + num3, num2 - num4);
				imageList.Images.Add(ObjectListView.getString_0(107315917), this.MakeTriangleBitmap(imageList.ImageSize, new Point[]
				{
					point4,
					point5,
					point6
				}));
			}
			this.SmallImageList = imageList;
		}

		private Bitmap MakeTriangleBitmap(Size sz, Point[] pts)
		{
			Bitmap bitmap = new Bitmap(sz.Width, sz.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.FillPolygon(new SolidBrush(Color.Gray), pts);
			return bitmap;
		}

		public virtual void Unsort()
		{
			this.ShowGroups = false;
			this.PrimarySortColumn = null;
			this.PrimarySortOrder = SortOrder.None;
			this.BuildList();
		}

		protected virtual void CreateGroups(IEnumerable<OLVGroup> groups)
		{
			this.Groups.Clear();
			foreach (OLVGroup olvgroup in groups)
			{
				olvgroup.InsertGroupOldStyle(this);
				olvgroup.SetItemsOldStyle();
			}
		}

		protected virtual void CorrectSubItemColors(ListViewItem olvi)
		{
		}

		protected virtual void FillInValues(OLVListItem lvi, object rowObject)
		{
			if (this.Columns.Count == 0)
			{
				return;
			}
			OLVListSubItem olvlistSubItem = this.MakeSubItem(rowObject, this.GetColumn(0));
			lvi.SubItems[0] = olvlistSubItem;
			lvi.ImageSelector = olvlistSubItem.ImageSelector;
			View view = this.View;
			if (view != View.Details)
			{
				if (view == View.Tile)
				{
					for (int i = 1; i < this.Columns.Count; i++)
					{
						OLVColumn column = this.GetColumn(i);
						if (column.IsTileViewColumn)
						{
							lvi.SubItems.Add(this.MakeSubItem(rowObject, column));
						}
					}
				}
			}
			else
			{
				for (int j = 1; j < this.Columns.Count; j++)
				{
					lvi.SubItems.Add(this.MakeSubItem(rowObject, this.GetColumn(j)));
				}
			}
			lvi.Font = this.Font;
			lvi.BackColor = this.BackColor;
			lvi.ForeColor = this.ForeColor;
			if (this.CheckBoxes)
			{
				CheckState? checkState = this.GetCheckState(lvi.RowObject);
				if (checkState != null)
				{
					lvi.CheckState = checkState.Value;
				}
			}
			if (this.RowFormatter != null)
			{
				this.RowFormatter(lvi);
			}
		}

		private OLVListSubItem MakeSubItem(object rowObject, OLVColumn column)
		{
			object value = column.GetValue(rowObject);
			OLVListSubItem olvlistSubItem = new OLVListSubItem(value, column.ValueToString(value), column.GetImage(rowObject));
			if (this.UseHyperlinks && column.Hyperlink)
			{
				IsHyperlinkEventArgs isHyperlinkEventArgs = new IsHyperlinkEventArgs();
				isHyperlinkEventArgs.ListView = this;
				isHyperlinkEventArgs.Model = rowObject;
				isHyperlinkEventArgs.Column = column;
				isHyperlinkEventArgs.Text = olvlistSubItem.Text;
				isHyperlinkEventArgs.Url = olvlistSubItem.Text;
				this.OnIsHyperlink(isHyperlinkEventArgs);
				olvlistSubItem.Url = isHyperlinkEventArgs.Url;
			}
			return olvlistSubItem;
		}

		private void ApplyHyperlinkStyle(int rowIndex, OLVListItem olvi)
		{
			olvi.UseItemStyleForSubItems = false;
			Color backColor = olvi.BackColor;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				OLVListSubItem subItem = olvi.GetSubItem(i);
				if (subItem != null)
				{
					OLVColumn column = this.GetColumn(i);
					subItem.BackColor = backColor;
					if (column.Hyperlink && !string.IsNullOrEmpty(subItem.Url))
					{
						this.ApplyCellStyle(olvi, i, this.IsUrlVisited(subItem.Url) ? this.HyperlinkStyle.Visited : this.HyperlinkStyle.Normal);
					}
				}
			}
		}

		protected virtual void ForceSubItemImagesExStyle()
		{
			if (!base.VirtualMode)
			{
				NativeMethods.ForceSubItemImagesExStyle(this);
			}
		}

		protected virtual int GetActualImageIndex(object imageSelector)
		{
			if (imageSelector == null)
			{
				return -1;
			}
			if (imageSelector is int)
			{
				return (int)imageSelector;
			}
			string text = imageSelector as string;
			if (text != null && this.SmallImageList != null)
			{
				return this.SmallImageList.Images.IndexOfKey(text);
			}
			return -1;
		}

		public virtual string GetHeaderToolTip(int columnIndex)
		{
			OLVColumn column = this.GetColumn(columnIndex);
			if (column == null)
			{
				return null;
			}
			string result = column.ToolTipText;
			if (this.HeaderToolTipGetter != null)
			{
				result = this.HeaderToolTipGetter(column);
			}
			return result;
		}

		public virtual string GetCellToolTip(int columnIndex, int rowIndex)
		{
			if (this.CellToolTipGetter != null)
			{
				return this.CellToolTipGetter(this.GetColumn(columnIndex), this.GetModelObject(rowIndex));
			}
			if (columnIndex >= 0)
			{
				OLVListSubItem subItem = this.GetSubItem(rowIndex, columnIndex);
				if (subItem != null && !string.IsNullOrEmpty(subItem.Url) && subItem.Url != subItem.Text && this.HotCellHitLocation == HitTestLocation.Text)
				{
					return subItem.Url;
				}
			}
			return null;
		}

		public virtual OLVListItem ModelToItem(object modelObject)
		{
			if (modelObject == null)
			{
				return null;
			}
			foreach (object obj in this.Items)
			{
				OLVListItem olvlistItem = (OLVListItem)obj;
				if (olvlistItem.RowObject != null && olvlistItem.RowObject.Equals(modelObject))
				{
					return olvlistItem;
				}
			}
			return null;
		}

		protected virtual void PostProcessRows()
		{
			int count = this.Items.Count;
			int num = 0;
			if (this.ShowGroups)
			{
				using (IEnumerator enumerator = this.Groups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ListViewGroup listViewGroup = (ListViewGroup)obj;
						foreach (object obj2 in listViewGroup.Items)
						{
							OLVListItem olvlistItem = (OLVListItem)obj2;
							this.PostProcessOneRow(olvlistItem.Index, num, olvlistItem);
							num++;
						}
					}
					return;
				}
			}
			foreach (object obj3 in this.Items)
			{
				OLVListItem olvlistItem2 = (OLVListItem)obj3;
				this.PostProcessOneRow(olvlistItem2.Index, num, olvlistItem2);
				num++;
			}
		}

		protected virtual void PostProcessOneRow(int rowIndex, int displayIndex, OLVListItem olvi)
		{
			if (this.UseAlternatingBackColors && this.View == View.Details)
			{
				olvi.BackColor = ((displayIndex % 2 == 1) ? this.AlternateRowBackColorOrDefault : this.BackColor);
			}
			if (this.ShowImagesOnSubItems && !base.VirtualMode)
			{
				this.SetSubItemImages(rowIndex, olvi);
			}
			if (this.UseHyperlinks)
			{
				this.ApplyHyperlinkStyle(rowIndex, olvi);
			}
			this.TriggerFormatRowEvent(rowIndex, displayIndex, olvi);
		}

		[Obsolete("This method is no longer used. Override PostProcessOneRow() to achieve a similar result")]
		protected virtual void PrepareAlternateBackColors()
		{
		}

		[Obsolete("This method is not longer maintained and will be removed", false)]
		protected virtual void SetAllSubItemImages()
		{
		}

		protected virtual void SetSubItemImages(int rowIndex, OLVListItem item)
		{
			this.SetSubItemImages(rowIndex, item, false);
		}

		protected virtual void SetSubItemImages(int rowIndex, OLVListItem item, bool shouldClearImages)
		{
			if (this.ShowImagesOnSubItems && !base.OwnerDraw)
			{
				for (int i = 1; i < item.SubItems.Count; i++)
				{
					this.SetSubItemImage(rowIndex, i, item.GetSubItem(i), shouldClearImages);
				}
				return;
			}
		}

		public virtual void SetSubItemImage(int rowIndex, int subItemIndex, OLVListSubItem subItem, bool shouldClearImages)
		{
			int actualImageIndex = this.GetActualImageIndex(subItem.ImageSelector);
			if (shouldClearImages || actualImageIndex != -1)
			{
				NativeMethods.SetSubItemImage(this, rowIndex, subItemIndex, actualImageIndex);
			}
		}

		protected virtual void TakeOwnershipOfObjects()
		{
			if (this.isOwnerOfObjects)
			{
				return;
			}
			this.isOwnerOfObjects = true;
			this.objects = ObjectListView.EnumerableToArray(this.objects, true);
		}

		protected virtual void TriggerFormatRowEvent(int rowIndex, int displayIndex, OLVListItem olvi)
		{
			FormatRowEventArgs formatRowEventArgs = new FormatRowEventArgs();
			formatRowEventArgs.ListView = this;
			formatRowEventArgs.RowIndex = rowIndex;
			formatRowEventArgs.DisplayIndex = displayIndex;
			formatRowEventArgs.Item = olvi;
			formatRowEventArgs.UseCellFormatEvents = this.UseCellFormatEvents;
			this.OnFormatRow(formatRowEventArgs);
			if (formatRowEventArgs.UseCellFormatEvents && this.View == View.Details)
			{
				olvi.UseItemStyleForSubItems = false;
				Color backColor = olvi.BackColor;
				for (int i = 0; i < this.Columns.Count; i++)
				{
					olvi.SubItems[i].BackColor = backColor;
				}
				FormatCellEventArgs formatCellEventArgs = new FormatCellEventArgs();
				formatCellEventArgs.ListView = this;
				formatCellEventArgs.RowIndex = rowIndex;
				formatCellEventArgs.DisplayIndex = displayIndex;
				formatCellEventArgs.Item = olvi;
				for (int j = 0; j < this.Columns.Count; j++)
				{
					formatCellEventArgs.ColumnIndex = j;
					formatCellEventArgs.Column = this.GetColumn(j);
					formatCellEventArgs.SubItem = olvi.GetSubItem(j);
					this.OnFormatCell(formatCellEventArgs);
				}
			}
		}

		public virtual void Reset()
		{
			base.Clear();
			this.AllColumns.Clear();
			this.ClearObjects();
			this.PrimarySortColumn = null;
			this.SecondarySortColumn = null;
			this.ClearPersistentCheckState();
			this.ClearUrlVisited();
			this.ClearHotItem();
		}

		void ISupportInitialize.BeginInit()
		{
			this.Frozen = true;
		}

		void ISupportInitialize.EndInit()
		{
			if (this.RowHeight != -1)
			{
				this.SmallImageList = this.SmallImageList;
				if (this.CheckBoxes)
				{
					this.InitializeStateImageList();
				}
			}
			if (this.UseCustomSelectionColors)
			{
				this.EnableCustomSelectionColors();
			}
			if (this.UseSubItemCheckBoxes || (base.VirtualMode && this.CheckBoxes))
			{
				this.SetupSubItemCheckBoxes();
			}
			this.Frozen = false;
		}

		private void SetupBaseImageList()
		{
			if (this.rowHeight != -1 && this.View == View.Details)
			{
				if (this.shadowedImageList == null || this.shadowedImageList.ImageSize.Height != this.rowHeight)
				{
					int width = (this.shadowedImageList == null) ? 16 : this.shadowedImageList.ImageSize.Width;
					this.BaseSmallImageList = this.MakeResizedImageList(width, this.rowHeight, this.shadowedImageList);
					return;
				}
			}
			this.BaseSmallImageList = this.shadowedImageList;
		}

		private ImageList MakeResizedImageList(int width, int height, ImageList source)
		{
			ImageList imageList = new ImageList();
			imageList.ImageSize = new Size(width, height);
			if (source == null)
			{
				return imageList;
			}
			imageList.TransparentColor = source.TransparentColor;
			imageList.ColorDepth = source.ColorDepth;
			for (int i = 0; i < source.Images.Count; i++)
			{
				Bitmap value = this.MakeResizedImage(width, height, source.Images[i], source.TransparentColor);
				imageList.Images.Add(value);
			}
			foreach (string text in source.Images.Keys)
			{
				imageList.Images.SetKeyName(source.Images.IndexOfKey(text), text);
			}
			return imageList;
		}

		private Bitmap MakeResizedImage(int width, int height, Image image, Color transparent)
		{
			Bitmap bitmap = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(transparent);
			int x = Math.Max(0, (bitmap.Size.Width - image.Size.Width) / 2);
			int y = Math.Max(0, (bitmap.Size.Height - image.Size.Height) / 2);
			graphics.DrawImage(image, x, y, image.Size.Width, image.Size.Height);
			return bitmap;
		}

		protected virtual void InitializeStateImageList()
		{
			if (base.DesignMode)
			{
				return;
			}
			if (!this.CheckBoxes)
			{
				return;
			}
			if (base.StateImageList == null)
			{
				base.StateImageList = new ImageList();
				base.StateImageList.ImageSize = new Size(16, (this.RowHeight == -1) ? 16 : this.RowHeight);
				base.StateImageList.ColorDepth = ColorDepth.Depth32Bit;
			}
			if (this.RowHeight != -1 && this.View == View.Details && base.StateImageList.ImageSize.Height != this.RowHeight)
			{
				base.StateImageList = new ImageList();
				base.StateImageList.ImageSize = new Size(16, this.RowHeight);
				base.StateImageList.ColorDepth = ColorDepth.Depth32Bit;
			}
			if (base.StateImageList.Images.Count == 0)
			{
				this.AddCheckStateBitmap(base.StateImageList, ObjectListView.getString_0(107315888), CheckBoxState.UncheckedNormal);
			}
			if (base.StateImageList.Images.Count <= 1)
			{
				this.AddCheckStateBitmap(base.StateImageList, ObjectListView.getString_0(107315863), CheckBoxState.CheckedNormal);
			}
			if (this.TriStateCheckBoxes && base.StateImageList.Images.Count <= 2)
			{
				this.AddCheckStateBitmap(base.StateImageList, ObjectListView.getString_0(107315806), CheckBoxState.MixedNormal);
				return;
			}
			if (base.StateImageList.Images.ContainsKey(ObjectListView.getString_0(107315806)))
			{
				base.StateImageList.Images.RemoveByKey(ObjectListView.getString_0(107315806));
			}
		}

		public virtual void SetupSubItemCheckBoxes()
		{
			this.ShowImagesOnSubItems = true;
			if (this.SmallImageList == null || !this.SmallImageList.Images.ContainsKey(ObjectListView.getString_0(107315863)))
			{
				this.InitializeSubItemCheckBoxImages();
			}
		}

		protected virtual void InitializeSubItemCheckBoxImages()
		{
			if (base.DesignMode)
			{
				return;
			}
			ImageList imageList = this.SmallImageList;
			if (imageList == null)
			{
				imageList = new ImageList();
				imageList.ImageSize = new Size(16, 16);
				imageList.ColorDepth = ColorDepth.Depth32Bit;
			}
			this.AddCheckStateBitmap(imageList, ObjectListView.getString_0(107315863), CheckBoxState.CheckedNormal);
			this.AddCheckStateBitmap(imageList, ObjectListView.getString_0(107315888), CheckBoxState.UncheckedNormal);
			this.AddCheckStateBitmap(imageList, ObjectListView.getString_0(107315806), CheckBoxState.MixedNormal);
			this.SmallImageList = imageList;
		}

		private void AddCheckStateBitmap(ImageList il, string key, CheckBoxState boxState)
		{
			Bitmap bitmap = new Bitmap(il.ImageSize.Width, il.ImageSize.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(il.TransparentColor);
			Point glyphLocation = new Point(bitmap.Width / 2 - 5, bitmap.Height / 2 - 6);
			CheckBoxRenderer.DrawCheckBox(graphics, glyphLocation, boxState);
			il.Images.Add(key, bitmap);
		}

		protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
			base.OnDrawColumnHeader(e);
		}

		protected override void OnDrawItem(DrawListViewItemEventArgs e)
		{
			if (this.View == View.Details)
			{
				e.DrawDefault = false;
			}
			else if (this.ItemRenderer == null)
			{
				e.DrawDefault = true;
			}
			else
			{
				object rowObject = ((OLVListItem)e.Item).RowObject;
				e.DrawDefault = !this.ItemRenderer.RenderItem(e, e.Graphics, e.Bounds, rowObject);
			}
			if (e.DrawDefault)
			{
				base.OnDrawItem(e);
			}
		}

		protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			if (base.DesignMode)
			{
				e.DrawDefault = true;
				return;
			}
			Rectangle bounds = e.Bounds;
			OLVColumn column = this.GetColumn(e.ColumnIndex);
			IRenderer renderer = column.Renderer ?? this.DefaultRenderer;
			Graphics graphics = e.Graphics;
			BufferedGraphics bufferedGraphics = BufferedGraphicsManager.Current.Allocate(e.Graphics, bounds);
			graphics = bufferedGraphics.Graphics;
			graphics.TextRenderingHint = ObjectListView.TextRenderingHint;
			graphics.SmoothingMode = ObjectListView.SmoothingMode;
			e.DrawDefault = !renderer.RenderSubItem(e, graphics, bounds, ((OLVListItem)e.Item).RowObject);
			if (bufferedGraphics != null)
			{
				if (!e.DrawDefault)
				{
					bufferedGraphics.Render();
				}
				bufferedGraphics.Dispose();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.lastMouseDownClickCount = e.Clicks;
			base.OnMouseDown(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (!base.Created)
			{
				return;
			}
			this.UpdateHotItem(new Point(-1, -1));
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!base.Created)
			{
				return;
			}
			CellOverEventArgs cellOverEventArgs = new CellOverEventArgs();
			this.BuildCellEvent(cellOverEventArgs, e.Location);
			this.OnCellOver(cellOverEventArgs);
			this.MouseMoveHitTest = cellOverEventArgs.HitTest;
			if (!cellOverEventArgs.Handled)
			{
				this.UpdateHotItem(cellOverEventArgs.HitTest);
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (!base.Created)
			{
				return;
			}
			if (e.Button == MouseButtons.Right)
			{
				this.OnRightMouseUp(e);
				return;
			}
			CellClickEventArgs args = new CellClickEventArgs();
			this.BuildCellEvent(args, e.Location);
			args.ClickCount = this.lastMouseDownClickCount;
			this.OnCellClick(args);
			if (args.Handled)
			{
				return;
			}
			if (this.UseHyperlinks && args.HitTest.HitTestLocation == HitTestLocation.Text && args.SubItem != null && !string.IsNullOrEmpty(args.SubItem.Url))
			{
				base.BeginInvoke(new MethodInvoker(delegate()
				{
					this.ProcessHyperlinkClicked(args);
				}));
			}
			if (!this.ShouldStartCellEdit(e))
			{
				return;
			}
			if (args.HitTest.HitTestLocation == HitTestLocation.Nothing)
			{
				return;
			}
			if (this.CellEditActivation == ObjectListView.CellEditActivateMode.SingleClick && args.ColumnIndex <= 0)
			{
				return;
			}
			if (args.Column != null && args.Column.CheckBoxes)
			{
				return;
			}
			this.EditSubItem(args.Item, args.ColumnIndex);
		}

		protected virtual void ProcessHyperlinkClicked(CellClickEventArgs e)
		{
			HyperlinkClickedEventArgs hyperlinkClickedEventArgs = new HyperlinkClickedEventArgs();
			hyperlinkClickedEventArgs.HitTest = e.HitTest;
			hyperlinkClickedEventArgs.ListView = this;
			hyperlinkClickedEventArgs.Location = new Point(-1, -1);
			hyperlinkClickedEventArgs.Item = e.Item;
			hyperlinkClickedEventArgs.SubItem = e.SubItem;
			hyperlinkClickedEventArgs.Model = e.Model;
			hyperlinkClickedEventArgs.ColumnIndex = e.ColumnIndex;
			hyperlinkClickedEventArgs.Column = e.Column;
			hyperlinkClickedEventArgs.RowIndex = e.RowIndex;
			hyperlinkClickedEventArgs.ModifierKeys = Control.ModifierKeys;
			hyperlinkClickedEventArgs.Url = e.SubItem.Url;
			this.OnHyperlinkClicked(hyperlinkClickedEventArgs);
			if (!hyperlinkClickedEventArgs.Handled)
			{
				this.StandardHyperlinkClickedProcessing(hyperlinkClickedEventArgs);
			}
		}

		protected virtual void StandardHyperlinkClickedProcessing(HyperlinkClickedEventArgs args)
		{
			Cursor cursor = this.Cursor;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				Process.Start(args.Url);
			}
			catch (Win32Exception)
			{
				SystemSounds.Beep.Play();
			}
			finally
			{
				this.Cursor = cursor;
			}
			this.MarkUrlVisited(args.Url);
			this.RefreshHotItem();
		}

		protected virtual void OnRightMouseUp(MouseEventArgs e)
		{
			CellRightClickEventArgs cellRightClickEventArgs = new CellRightClickEventArgs();
			this.BuildCellEvent(cellRightClickEventArgs, e.Location);
			this.OnCellRightClick(cellRightClickEventArgs);
			if (!cellRightClickEventArgs.Handled && cellRightClickEventArgs.MenuStrip != null)
			{
				cellRightClickEventArgs.MenuStrip.Show(this, cellRightClickEventArgs.Location);
			}
		}

		private void BuildCellEvent(CellEventArgs args, Point location)
		{
			OlvListViewHitTestInfo olvListViewHitTestInfo = this.OlvHitTest(location.X, location.Y);
			args.HitTest = olvListViewHitTestInfo;
			args.ListView = this;
			args.Location = location;
			args.Item = olvListViewHitTestInfo.Item;
			args.SubItem = olvListViewHitTestInfo.SubItem;
			args.Model = olvListViewHitTestInfo.RowObject;
			args.ColumnIndex = olvListViewHitTestInfo.ColumnIndex;
			args.Column = olvListViewHitTestInfo.Column;
			if (olvListViewHitTestInfo.Item != null)
			{
				args.RowIndex = olvListViewHitTestInfo.Item.Index;
			}
			args.ModifierKeys = Control.ModifierKeys;
			if (args.Item != null && args.ListView.View != View.Details)
			{
				args.ColumnIndex = 0;
				args.Column = args.ListView.GetColumn(0);
				args.SubItem = args.Item.GetSubItem(0);
			}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.SelectionEventsSuspended)
			{
				return;
			}
			base.OnSelectedIndexChanged(e);
			if (!this.hasIdleHandler)
			{
				this.hasIdleHandler = true;
				this.RunWhenIdle(new EventHandler(this.HandleApplicationIdle));
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.Invoke(new MethodInvoker(this.OnControlCreated));
		}

		protected virtual void OnControlCreated()
		{
			HeaderControl headerControl = this.HeaderControl;
			headerControl.WordWrap = this.HeaderWordWrap;
			this.HotItemStyle = this.HotItemStyle;
			NativeMethods.SetGroupImageList(this, this.GroupImageList);
			this.UseExplorerTheme = this.UseExplorerTheme;
			this.RememberDisplayIndicies();
			this.SetGroupSpacing();
			if (base.VirtualMode)
			{
				this.ApplyExtendedStyles();
			}
		}

		protected virtual bool ShouldStartCellEdit(MouseEventArgs e)
		{
			return !this.IsCellEditing && e.Button == MouseButtons.Left && (Control.ModifierKeys & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.None && ((this.lastMouseDownClickCount == 1 && this.CellEditActivation == ObjectListView.CellEditActivateMode.SingleClick) || (this.lastMouseDownClickCount == 2 && this.CellEditActivation == ObjectListView.CellEditActivateMode.DoubleClick));
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.IsCellEditing)
			{
				return this.CellEditKeyEngine.HandleKey(this, keyData);
			}
			if (keyData == Keys.F2)
			{
				this.EditSubItem((OLVListItem)base.FocusedItem, 0);
				return base.ProcessDialogKey(keyData);
			}
			if (this.CopySelectionOnControlC && keyData == (Keys)131139)
			{
				this.CopySelectionToClipboard();
				return true;
			}
			if (this.SelectAllOnControlA && keyData == (Keys)131137)
			{
				this.SelectAll();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		public virtual void EditModel(object rowModel)
		{
			OLVListItem olvlistItem = this.ModelToItem(rowModel);
			if (olvlistItem == null)
			{
				return;
			}
			for (int i = 0; i < olvlistItem.SubItems.Count; i++)
			{
				if (this.GetColumn(i).IsEditable)
				{
					this.StartCellEdit(olvlistItem, i);
					return;
				}
			}
		}

		public virtual void EditSubItem(OLVListItem item, int subItemIndex)
		{
			if (item == null)
			{
				return;
			}
			if (subItemIndex < 0 && subItemIndex >= item.SubItems.Count)
			{
				return;
			}
			if (this.CellEditActivation == ObjectListView.CellEditActivateMode.None)
			{
				return;
			}
			if (!this.GetColumn(subItemIndex).IsEditable)
			{
				return;
			}
			this.StartCellEdit(item, subItemIndex);
		}

		public virtual void StartCellEdit(OLVListItem item, int subItemIndex)
		{
			OLVColumn column = this.GetColumn(subItemIndex);
			Control control = this.GetCellEditor(item, subItemIndex);
			Rectangle rectangle = this.CalculateCellEditorBounds(item, subItemIndex, control.PreferredSize);
			control.Bounds = rectangle;
			Munger.PutProperty(control, ObjectListView.getString_0(107315773), column.TextAlign);
			this.SetControlValue(control, column.GetValue(item.RowObject), column.GetStringValue(item.RowObject));
			this.cellEditEventArgs = new CellEditEventArgs(column, control, rectangle, item, subItemIndex);
			this.OnCellEditStarting(this.cellEditEventArgs);
			if (this.cellEditEventArgs.Cancel)
			{
				return;
			}
			this.cellEditor = this.cellEditEventArgs.Control;
			if (this.View != View.Tile && !base.OwnerDraw && this.cellEditor.Height != rectangle.Height)
			{
				this.cellEditor.Top += (rectangle.Height - this.cellEditor.Height) / 2;
			}
			base.Invalidate();
			base.Controls.Add(this.cellEditor);
			this.ConfigureControl();
			this.PauseAnimations(true);
		}

		public Rectangle CalculateCellEditorBounds(OLVListItem item, int subItemIndex, Size preferredSize)
		{
			Rectangle rectangle;
			if (this.View == View.Details)
			{
				rectangle = item.GetSubItemBounds(subItemIndex);
			}
			else
			{
				rectangle = base.GetItemRect(item.Index, ItemBoundsPortion.Label);
			}
			if (base.OwnerDraw)
			{
				return this.CalculateCellEditorBoundsOwnerDrawn(item, subItemIndex, rectangle, preferredSize);
			}
			return this.CalculateCellEditorBoundsStandard(item, subItemIndex, rectangle, preferredSize);
		}

		protected Rectangle CalculateCellEditorBoundsOwnerDrawn(OLVListItem item, int subItemIndex, Rectangle r, Size preferredSize)
		{
			IRenderer renderer = (this.View == View.Details) ? (this.GetColumn(subItemIndex).Renderer ?? this.DefaultRenderer) : this.ItemRenderer;
			if (renderer == null)
			{
				return r;
			}
			Rectangle editRectangle;
			using (Graphics graphics = base.CreateGraphics())
			{
				editRectangle = renderer.GetEditRectangle(graphics, r, item, subItemIndex, preferredSize);
			}
			return editRectangle;
		}

		protected Rectangle CalculateCellEditorBoundsStandard(OLVListItem item, int subItemIndex, Rectangle cellBounds, Size preferredSize)
		{
			if (this.View != View.Details)
			{
				return cellBounds;
			}
			int num = 0;
			object imageSelector = null;
			if (subItemIndex == 0)
			{
				imageSelector = item.ImageSelector;
			}
			else if (base.OwnerDraw || this.ShowImagesOnSubItems)
			{
				imageSelector = item.GetSubItem(subItemIndex).ImageSelector;
			}
			if (this.GetActualImageIndex(imageSelector) != -1)
			{
				num += this.SmallImageSize.Width + 2;
			}
			if (this.CheckBoxes && base.StateImageList != null && subItemIndex == 0)
			{
				num += base.StateImageList.ImageSize.Width + 2;
			}
			if (subItemIndex == 0 && item.IndentCount > 0)
			{
				num += this.SmallImageSize.Width * item.IndentCount;
			}
			if (num > 0)
			{
				cellBounds.X += num;
				cellBounds.Width -= num;
			}
			return cellBounds;
		}

		protected virtual void SetControlValue(Control control, object value, string stringValue)
		{
			if (control is ComboBox)
			{
				ComboBox cb = (ComboBox)control;
				if (cb.Created)
				{
					cb.SelectedValue = value;
					return;
				}
				base.BeginInvoke(new MethodInvoker(delegate()
				{
					cb.SelectedValue = value;
				}));
				return;
			}
			else
			{
				if (Munger.PutProperty(control, ObjectListView.getString_0(107322634), value))
				{
					return;
				}
				try
				{
					string text = value as string;
					if (text == null)
					{
						control.Text = stringValue;
					}
					else
					{
						control.Text = text;
					}
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				return;
			}
		}

		protected virtual void ConfigureControl()
		{
			this.cellEditor.Validating += this.CellEditor_Validating;
			this.cellEditor.Select();
		}

		protected virtual object GetControlValue(Control control)
		{
			if (control == null)
			{
				return null;
			}
			if (control is TextBox)
			{
				return ((TextBox)control).Text;
			}
			if (control is ComboBox)
			{
				return ((ComboBox)control).SelectedValue;
			}
			if (control is CheckBox)
			{
				return ((CheckBox)control).Checked;
			}
			object result;
			try
			{
				result = control.GetType().InvokeMember(ObjectListView.getString_0(107322634), BindingFlags.GetProperty, null, control, null);
			}
			catch (MissingMethodException)
			{
				result = control.Text;
			}
			catch (MissingFieldException)
			{
				result = control.Text;
			}
			return result;
		}

		protected virtual void CellEditor_Validating(object sender, CancelEventArgs e)
		{
			this.cellEditEventArgs.Cancel = false;
			this.cellEditEventArgs.NewValue = this.GetControlValue(this.cellEditor);
			this.OnCellEditorValidating(this.cellEditEventArgs);
			if (this.cellEditEventArgs.Cancel)
			{
				this.cellEditEventArgs.Control.Select();
				e.Cancel = true;
				return;
			}
			this.FinishCellEdit();
		}

		public virtual Rectangle CalculateCellBounds(OLVListItem item, int subItemIndex)
		{
			return this.CalculateCellBounds(item, subItemIndex, ItemBoundsPortion.Label);
		}

		public virtual Rectangle CalculateCellTextBounds(OLVListItem item, int subItemIndex)
		{
			return this.CalculateCellBounds(item, subItemIndex, ItemBoundsPortion.ItemOnly);
		}

		private Rectangle CalculateCellBounds(OLVListItem item, int subItemIndex, ItemBoundsPortion portion)
		{
			if (subItemIndex > 0)
			{
				return item.SubItems[subItemIndex].Bounds;
			}
			Rectangle itemRect = base.GetItemRect(item.Index, portion);
			if (itemRect.Y < -10000000 || itemRect.Y > 10000000)
			{
				itemRect.Y = item.Bounds.Y;
			}
			if (this.View != View.Details)
			{
				return itemRect;
			}
			Point scrolledColumnSides = NativeMethods.GetScrolledColumnSides(this, 0);
			itemRect.X = scrolledColumnSides.X + 4;
			itemRect.Width = scrolledColumnSides.Y - scrolledColumnSides.X - 5;
			return itemRect;
		}

		protected virtual Control GetCellEditor(OLVListItem item, int subItemIndex)
		{
			OLVColumn column = this.GetColumn(subItemIndex);
			object value = column.GetValue(item.RowObject) ?? this.GetFirstNonNullValue(column);
			return ObjectListView.EditorRegistry.GetEditor(item.RowObject, column, value) ?? this.MakeDefaultCellEditor(column);
		}

		internal object GetFirstNonNullValue(OLVColumn column)
		{
			for (int i = 0; i < Math.Min(this.GetItemCount(), 1000); i++)
			{
				object value = column.GetValue(this.GetModelObject(i));
				if (value != null)
				{
					return value;
				}
			}
			return null;
		}

		protected virtual Control MakeDefaultCellEditor(OLVColumn column)
		{
			TextBox textBox = new TextBox();
			if (column.AutoCompleteEditor)
			{
				this.ConfigureAutoComplete(textBox, column);
			}
			return textBox;
		}

		public void ConfigureAutoComplete(TextBox tb, OLVColumn column)
		{
			this.ConfigureAutoComplete(tb, column, 1000);
		}

		public void ConfigureAutoComplete(TextBox tb, OLVColumn column, int maxRows)
		{
			maxRows = Math.Min(this.GetItemCount(), maxRows);
			tb.AutoCompleteCustomSource.Clear();
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			List<string> list = new List<string>();
			for (int i = 0; i < maxRows; i++)
			{
				string stringValue = column.GetStringValue(this.GetModelObject(i));
				if (!string.IsNullOrEmpty(stringValue) && !dictionary.ContainsKey(stringValue))
				{
					list.Add(stringValue);
					dictionary[stringValue] = true;
				}
			}
			tb.AutoCompleteCustomSource.AddRange(list.ToArray());
			tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
			tb.AutoCompleteMode = column.AutoCompleteEditorMode;
		}

		public virtual void CancelCellEdit()
		{
			if (!this.IsCellEditing)
			{
				return;
			}
			this.cellEditEventArgs.Cancel = true;
			this.cellEditEventArgs.NewValue = this.GetControlValue(this.cellEditor);
			this.OnCellEditFinishing(this.cellEditEventArgs);
			this.CleanupCellEdit(false, this.cellEditEventArgs.AutoDispose);
		}

		public virtual bool PossibleFinishCellEditing()
		{
			return this.PossibleFinishCellEditing(false);
		}

		public virtual bool PossibleFinishCellEditing(bool expectingCellEdit)
		{
			if (!this.IsCellEditing)
			{
				return true;
			}
			this.cellEditEventArgs.Cancel = false;
			this.cellEditEventArgs.NewValue = this.GetControlValue(this.cellEditor);
			this.OnCellEditorValidating(this.cellEditEventArgs);
			if (this.cellEditEventArgs.Cancel)
			{
				return false;
			}
			this.FinishCellEdit(expectingCellEdit);
			return true;
		}

		public virtual void FinishCellEdit()
		{
			this.FinishCellEdit(false);
		}

		public virtual void FinishCellEdit(bool expectingCellEdit)
		{
			if (!this.IsCellEditing)
			{
				return;
			}
			this.cellEditEventArgs.Cancel = false;
			this.cellEditEventArgs.NewValue = this.GetControlValue(this.cellEditor);
			this.OnCellEditFinishing(this.cellEditEventArgs);
			if (!this.cellEditEventArgs.Cancel)
			{
				this.cellEditEventArgs.Column.PutValue(this.cellEditEventArgs.RowObject, this.cellEditEventArgs.NewValue);
				this.RefreshItem(this.cellEditEventArgs.ListViewItem);
			}
			this.CleanupCellEdit(expectingCellEdit, this.cellEditEventArgs.AutoDispose);
		}

		protected virtual void CleanupCellEdit(bool expectingCellEdit, bool disposeOfCellEditor)
		{
			if (this.cellEditor == null)
			{
				return;
			}
			this.cellEditor.Validating -= this.CellEditor_Validating;
			Control soonToBeOldCellEditor = this.cellEditor;
			this.cellEditor = null;
			EventHandler toBeRun = null;
			toBeRun = delegate(object sender, EventArgs e)
			{
				Application.Idle -= toBeRun;
				this.Controls.Remove(soonToBeOldCellEditor);
				if (disposeOfCellEditor)
				{
					soonToBeOldCellEditor.Dispose();
				}
				this.Invalidate();
				if (!this.IsCellEditing)
				{
					if (this.Focused)
					{
						this.Select();
					}
					this.PauseAnimations(false);
				}
			};
			if (expectingCellEdit)
			{
				this.RunWhenIdle(toBeRun);
				return;
			}
			toBeRun(null, null);
		}

		public virtual void ClearHotItem()
		{
			this.UpdateHotItem(new Point(-1, -1));
		}

		public virtual void RefreshHotItem()
		{
			this.UpdateHotItem(base.PointToClient(Cursor.Position));
		}

		protected virtual void UpdateHotItem(Point pt)
		{
			this.UpdateHotItem(this.OlvHitTest(pt.X, pt.Y));
		}

		protected virtual void UpdateHotItem(OlvListViewHitTestInfo hti)
		{
			if (!this.UseHotItem && !this.UseHyperlinks)
			{
				return;
			}
			int rowIndex = hti.RowIndex;
			int num = hti.ColumnIndex;
			HitTestLocation hitTestLocation = hti.HitTestLocation;
			HitTestLocationEx hitTestLocationEx = hti.HitTestLocationEx;
			OLVGroup group = hti.Group;
			if (rowIndex >= 0 && this.View != View.Details)
			{
				num = 0;
			}
			if (this.HotRowIndex == rowIndex && this.HotColumnIndex == num && this.HotCellHitLocation == hitTestLocation && this.HotCellHitLocationEx == hitTestLocationEx && this.HotGroup == group)
			{
				return;
			}
			HotItemChangedEventArgs hotItemChangedEventArgs = new HotItemChangedEventArgs();
			hotItemChangedEventArgs.HotCellHitLocation = hitTestLocation;
			hotItemChangedEventArgs.HotCellHitLocationEx = hitTestLocationEx;
			hotItemChangedEventArgs.HotColumnIndex = num;
			hotItemChangedEventArgs.HotRowIndex = rowIndex;
			hotItemChangedEventArgs.HotGroup = group;
			hotItemChangedEventArgs.OldHotCellHitLocation = this.HotCellHitLocation;
			hotItemChangedEventArgs.OldHotCellHitLocationEx = this.HotCellHitLocationEx;
			hotItemChangedEventArgs.OldHotColumnIndex = this.HotColumnIndex;
			hotItemChangedEventArgs.OldHotRowIndex = this.HotRowIndex;
			hotItemChangedEventArgs.OldHotGroup = this.HotGroup;
			this.OnHotItemChanged(hotItemChangedEventArgs);
			this.HotRowIndex = rowIndex;
			this.HotColumnIndex = num;
			this.HotCellHitLocation = hitTestLocation;
			this.HotCellHitLocationEx = hitTestLocationEx;
			this.HotGroup = group;
			if (hotItemChangedEventArgs.Handled)
			{
				return;
			}
			base.BeginUpdate();
			try
			{
				base.Invalidate();
				if (hotItemChangedEventArgs.OldHotRowIndex != -1)
				{
					this.UnapplyHotItem(hotItemChangedEventArgs.OldHotRowIndex);
				}
				if (this.HotRowIndex != -1)
				{
					if (base.VirtualMode)
					{
						base.RedrawItems(this.HotRowIndex, this.HotRowIndex, true);
					}
					else
					{
						this.UpdateHotRow(this.HotRowIndex, this.HotColumnIndex, this.HotCellHitLocation, hti.Item);
					}
				}
				if (this.UseHotItem && this.HotItemStyle != null && this.HotItemStyle.Overlay != null)
				{
					this.RefreshOverlays();
				}
			}
			finally
			{
				base.EndUpdate();
			}
		}

		protected virtual void UpdateHotRow(OLVListItem olvi)
		{
			this.UpdateHotRow(this.HotRowIndex, this.HotColumnIndex, this.HotCellHitLocation, olvi);
		}

		protected virtual void UpdateHotRow(int rowIndex, int columnIndex, HitTestLocation hitLocation, OLVListItem olvi)
		{
			if (rowIndex >= 0 && columnIndex >= 0)
			{
				if (this.UseHyperlinks)
				{
					OLVColumn column = this.GetColumn(columnIndex);
					OLVListSubItem subItem = olvi.GetSubItem(columnIndex);
					if (column.Hyperlink && hitLocation == HitTestLocation.Text && !string.IsNullOrEmpty(subItem.Url))
					{
						this.ApplyCellStyle(olvi, columnIndex, this.HyperlinkStyle.Over);
						this.Cursor = (this.HyperlinkStyle.OverCursor ?? Cursors.Default);
					}
					else
					{
						this.Cursor = Cursors.Default;
					}
				}
				if (this.UseHotItem && !olvi.Selected)
				{
					this.ApplyRowStyle(olvi, this.HotItemStyle);
				}
				return;
			}
		}

		protected virtual void ApplyRowStyle(OLVListItem olvi, IItemStyle style)
		{
			if (style == null)
			{
				return;
			}
			if (!base.FullRowSelect)
			{
				if (this.View == View.Details)
				{
					olvi.UseItemStyleForSubItems = false;
					foreach (object obj in olvi.SubItems)
					{
						ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)obj;
						if (style.BackColor.IsEmpty)
						{
							listViewSubItem.BackColor = olvi.BackColor;
						}
						else
						{
							listViewSubItem.BackColor = style.BackColor;
						}
					}
					this.ApplyCellStyle(olvi, 0, style);
					return;
				}
			}
			if (style.Font != null)
			{
				olvi.Font = style.Font;
			}
			if (style.FontStyle != FontStyle.Regular)
			{
				olvi.Font = new Font(olvi.Font ?? this.Font, style.FontStyle);
			}
			if (!style.ForeColor.IsEmpty)
			{
				if (olvi.UseItemStyleForSubItems)
				{
					olvi.ForeColor = style.ForeColor;
				}
				else
				{
					foreach (object obj2 in olvi.SubItems)
					{
						ListViewItem.ListViewSubItem listViewSubItem2 = (ListViewItem.ListViewSubItem)obj2;
						listViewSubItem2.ForeColor = style.ForeColor;
					}
				}
			}
			if (!style.BackColor.IsEmpty)
			{
				if (olvi.UseItemStyleForSubItems)
				{
					olvi.BackColor = style.BackColor;
					return;
				}
				foreach (object obj3 in olvi.SubItems)
				{
					ListViewItem.ListViewSubItem listViewSubItem3 = (ListViewItem.ListViewSubItem)obj3;
					listViewSubItem3.BackColor = style.BackColor;
				}
			}
		}

		protected virtual void ApplyCellStyle(OLVListItem olvi, int columnIndex, IItemStyle style)
		{
			if (style == null)
			{
				return;
			}
			if (this.View != View.Details && columnIndex > 0)
			{
				return;
			}
			olvi.UseItemStyleForSubItems = false;
			ListViewItem.ListViewSubItem listViewSubItem = olvi.SubItems[columnIndex];
			if (style.Font != null)
			{
				listViewSubItem.Font = style.Font;
			}
			if (style.FontStyle != FontStyle.Regular)
			{
				ListViewItem.ListViewSubItem listViewSubItem2 = listViewSubItem;
				Font prototype;
				if ((prototype = listViewSubItem.Font) == null)
				{
					prototype = (olvi.Font ?? this.Font);
				}
				listViewSubItem2.Font = new Font(prototype, style.FontStyle);
			}
			if (!style.ForeColor.IsEmpty)
			{
				listViewSubItem.ForeColor = style.ForeColor;
			}
			if (!style.BackColor.IsEmpty)
			{
				listViewSubItem.BackColor = style.BackColor;
			}
		}

		protected virtual void UnapplyHotItem(int index)
		{
			this.Cursor = Cursors.Default;
			if (base.VirtualMode)
			{
				if (index < base.VirtualListSize)
				{
					base.RedrawItems(index, index, true);
					return;
				}
			}
			else
			{
				OLVListItem item = this.GetItem(index);
				if (item != null)
				{
					this.RefreshItem(item);
				}
			}
		}

		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			base.OnItemDrag(e);
			if (this.DragSource == null)
			{
				return;
			}
			object obj = this.DragSource.StartDrag(this, e.Button, (OLVListItem)e.Item);
			if (obj != null)
			{
				DragDropEffects effect = base.DoDragDrop(obj, this.DragSource.GetAllowedEffects(obj));
				this.DragSource.EndDrag(obj, effect);
			}
		}

		protected override void OnDragEnter(DragEventArgs args)
		{
			base.OnDragEnter(args);
			if (this.DropSink != null)
			{
				this.DropSink.Enter(args);
			}
		}

		protected override void OnDragOver(DragEventArgs args)
		{
			base.OnDragOver(args);
			if (this.DropSink != null)
			{
				this.DropSink.Over(args);
			}
		}

		protected override void OnDragDrop(DragEventArgs args)
		{
			base.OnDragDrop(args);
			if (this.DropSink != null)
			{
				this.DropSink.Drop(args);
			}
		}

		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
			if (this.DropSink != null)
			{
				this.DropSink.Leave();
			}
		}

		protected override void OnGiveFeedback(GiveFeedbackEventArgs args)
		{
			base.OnGiveFeedback(args);
			if (this.DropSink != null)
			{
				this.DropSink.GiveFeedback(args);
			}
		}

		protected override void OnQueryContinueDrag(QueryContinueDragEventArgs args)
		{
			base.OnQueryContinueDrag(args);
			if (this.DropSink != null)
			{
				this.DropSink.QueryContinue(args);
			}
		}

		public virtual void AddDecoration(IDecoration decoration)
		{
			if (decoration == null)
			{
				return;
			}
			this.Decorations.Add(decoration);
			base.Invalidate();
		}

		public virtual void AddOverlay(IOverlay overlay)
		{
			if (overlay == null)
			{
				return;
			}
			this.Overlays.Add(overlay);
			base.Invalidate();
		}

		protected virtual void DrawAllDecorations(Graphics g, List<OLVListItem> itemsThatWereRedrawn)
		{
			g.TextRenderingHint = ObjectListView.TextRenderingHint;
			g.SmoothingMode = ObjectListView.SmoothingMode;
			Rectangle contentRectangle = this.ContentRectangle;
			if (this.HasEmptyListMsg && this.GetItemCount() == 0)
			{
				this.EmptyListMsgOverlay.Draw(this, g, contentRectangle);
			}
			if (this.DropSink != null)
			{
				this.DropSink.DrawFeedback(g, contentRectangle);
			}
			foreach (OLVListItem olvlistItem in itemsThatWereRedrawn)
			{
				if (olvlistItem.HasDecoration)
				{
					foreach (IDecoration decoration in olvlistItem.Decorations)
					{
						decoration.ListItem = olvlistItem;
						decoration.SubItem = null;
						decoration.Draw(this, g, contentRectangle);
					}
				}
				foreach (object obj in olvlistItem.SubItems)
				{
					OLVListSubItem olvlistSubItem = (OLVListSubItem)obj;
					if (olvlistSubItem.HasDecoration)
					{
						foreach (IDecoration decoration2 in olvlistSubItem.Decorations)
						{
							decoration2.ListItem = olvlistItem;
							decoration2.SubItem = olvlistSubItem;
							decoration2.Draw(this, g, contentRectangle);
						}
					}
				}
				if (this.SelectedRowDecoration != null && olvlistItem.Selected)
				{
					this.SelectedRowDecoration.ListItem = olvlistItem;
					this.SelectedRowDecoration.SubItem = null;
					this.SelectedRowDecoration.Draw(this, g, contentRectangle);
				}
			}
			foreach (IDecoration decoration3 in this.Decorations)
			{
				decoration3.ListItem = null;
				decoration3.SubItem = null;
				decoration3.Draw(this, g, contentRectangle);
			}
			if (this.UseHotItem && this.HotItemStyle != null && this.HotItemStyle.Decoration != null)
			{
				IDecoration decoration4 = this.HotItemStyle.Decoration;
				decoration4.ListItem = this.GetItem(this.HotRowIndex);
				if (decoration4.ListItem == null)
				{
					decoration4.SubItem = null;
				}
				else
				{
					decoration4.SubItem = decoration4.ListItem.GetSubItem(this.HotColumnIndex);
				}
				decoration4.Draw(this, g, contentRectangle);
			}
			if (base.DesignMode)
			{
				foreach (IOverlay overlay in this.Overlays)
				{
					overlay.Draw(this, g, contentRectangle);
				}
			}
		}

		public virtual bool HasDecoration(IDecoration decoration)
		{
			return this.Decorations.Contains(decoration);
		}

		public virtual bool HasOverlay(IOverlay overlay)
		{
			return this.Overlays.Contains(overlay);
		}

		public virtual void HideOverlays()
		{
			foreach (GlassPanelForm glassPanelForm in this.glassPanels)
			{
				glassPanelForm.HideGlass();
			}
		}

		protected virtual void InitializeEmptyListMsgOverlay()
		{
			this.EmptyListMsgOverlay = new TextOverlay
			{
				Alignment = ContentAlignment.MiddleCenter,
				TextColor = SystemColors.ControlDarkDark,
				BackColor = Color.BlanchedAlmond,
				BorderColor = SystemColors.ControlDark,
				BorderWidth = 2f
			};
		}

		protected virtual void InitializeStandardOverlays()
		{
			this.OverlayImage = new ImageOverlay();
			this.AddOverlay(this.OverlayImage);
			this.OverlayText = new TextOverlay();
			this.AddOverlay(this.OverlayText);
		}

		public virtual void ShowOverlays()
		{
			if (!this.ShouldShowOverlays())
			{
				return;
			}
			if (this.Overlays.Count != this.glassPanels.Count)
			{
				foreach (IOverlay overlay in this.Overlays)
				{
					if (this.FindGlassPanelForOverlay(overlay) == null)
					{
						GlassPanelForm glassPanelForm = new GlassPanelForm();
						glassPanelForm.Bind(this, overlay);
						this.glassPanels.Add(glassPanelForm);
					}
				}
			}
			foreach (GlassPanelForm glassPanelForm2 in this.glassPanels)
			{
				glassPanelForm2.ShowGlass();
			}
		}

		private bool ShouldShowOverlays()
		{
			return !base.DesignMode && this.UseOverlays && this.HasOverlays && Screen.PrimaryScreen.BitsPerPixel >= 32;
		}

		private GlassPanelForm FindGlassPanelForOverlay(IOverlay overlay)
		{
			return this.glassPanels.Find((GlassPanelForm x) => x.Overlay == overlay);
		}

		public virtual void RefreshOverlays()
		{
			foreach (GlassPanelForm glassPanelForm in this.glassPanels)
			{
				glassPanelForm.Invalidate();
			}
		}

		public virtual void RefreshOverlay(IOverlay overlay)
		{
			GlassPanelForm glassPanelForm = this.FindGlassPanelForOverlay(overlay);
			if (glassPanelForm != null)
			{
				glassPanelForm.Invalidate();
			}
		}

		public virtual void RemoveDecoration(IDecoration decoration)
		{
			if (decoration == null)
			{
				return;
			}
			this.Decorations.Remove(decoration);
			base.Invalidate();
		}

		public virtual void RemoveOverlay(IOverlay overlay)
		{
			if (overlay == null)
			{
				return;
			}
			this.Overlays.Remove(overlay);
			GlassPanelForm glassPanelForm = this.FindGlassPanelForOverlay(overlay);
			if (glassPanelForm != null)
			{
				this.glassPanels.Remove(glassPanelForm);
				glassPanelForm.Unbind();
				glassPanelForm.Dispose();
			}
		}

		public virtual IModelFilter CreateColumnFilter()
		{
			List<IModelFilter> list = new List<IModelFilter>();
			foreach (object obj in this.Columns)
			{
				OLVColumn olvcolumn = (OLVColumn)obj;
				IModelFilter valueBasedFilter = olvcolumn.ValueBasedFilter;
				if (valueBasedFilter != null)
				{
					list.Add(valueBasedFilter);
				}
			}
			if (list.Count != 0)
			{
				return new CompositeAllFilter(list);
			}
			return null;
		}

		protected virtual IEnumerable FilterObjects(IEnumerable originalObjects, IModelFilter aModelFilter, IListFilter aListFilter)
		{
			originalObjects = (originalObjects ?? new ArrayList());
			FilterEventArgs filterEventArgs = new FilterEventArgs(originalObjects);
			this.OnFilter(filterEventArgs);
			if (filterEventArgs.FilteredObjects != null)
			{
				return filterEventArgs.FilteredObjects;
			}
			if (aListFilter != null)
			{
				originalObjects = aListFilter.Filter(originalObjects);
			}
			if (aModelFilter != null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in originalObjects)
				{
					if (aModelFilter.Filter(obj))
					{
						arrayList.Add(obj);
					}
				}
				originalObjects = arrayList;
			}
			return originalObjects;
		}

		public virtual void ResetColumnFiltering()
		{
			foreach (object obj in this.Columns)
			{
				OLVColumn olvcolumn = (OLVColumn)obj;
				olvcolumn.ValuesChosenForFiltering.Clear();
			}
			this.UpdateColumnFiltering();
		}

		public virtual void UpdateColumnFiltering()
		{
			if (this.AdditionalFilter == null)
			{
				this.ModelFilter = this.CreateColumnFilter();
				return;
			}
			IModelFilter modelFilter = this.CreateColumnFilter();
			if (modelFilter == null)
			{
				this.ModelFilter = this.AdditionalFilter;
				return;
			}
			this.ModelFilter = new CompositeAllFilter(new List<IModelFilter>
			{
				modelFilter,
				this.AdditionalFilter
			});
		}

		protected virtual void UpdateFiltering()
		{
			this.BuildList(true);
		}

		protected virtual CheckState GetPersistentCheckState(object model)
		{
			CheckState result = CheckState.Unchecked;
			if (model != null)
			{
				this.CheckStateMap.TryGetValue(model, out result);
			}
			return result;
		}

		protected virtual CheckState SetPersistentCheckState(object model, CheckState state)
		{
			if (model == null)
			{
				return CheckState.Unchecked;
			}
			this.CheckStateMap[model] = state;
			return state;
		}

		protected virtual void ClearPersistentCheckState()
		{
			this.CheckStateMap = null;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ObjectListView()
		{
			Strings.CreateGetStringDelegate(typeof(ObjectListView));
			ObjectListView.sSmoothingMode = SmoothingMode.HighQuality;
			ObjectListView.sTextRendereringHint = TextRenderingHint.SystemDefault;
			ObjectListView.sGroupTitleDefault = ObjectListView.getString_0(107346616);
			ObjectListView.EditorRegistry = new EditorRegistry();
			ObjectListView.SortAscendingImage = Resources.SortAscending;
			ObjectListView.SortDescendingImage = Resources.SortDescending;
		}

		public const string SORT_INDICATOR_UP_KEY = "sort-indicator-up";

		public const string SORT_INDICATOR_DOWN_KEY = "sort-indicator-down";

		public const string CHECKED_KEY = "checkbox-checked";

		public const string UNCHECKED_KEY = "checkbox-unchecked";

		public const string INDETERMINATE_KEY = "checkbox-indeterminate";

		private int lastValidatingEvent;

		private static bool? sIsVistaOrLater;

		private static bool? sIsWin7OrLater;

		private static SmoothingMode sSmoothingMode;

		private static TextRenderingHint sTextRendereringHint;

		private static string sGroupTitleDefault;

		private static bool showCellPaddingBounds;

		private IModelFilter additionalFilter;

		private List<OLVColumn> allColumns = new List<OLVColumn>();

		private Color alternateRowBackColor = Color.Empty;

		private OLVColumn alwaysGroupByColumn;

		private SortOrder alwaysGroupBySortOrder;

		private ObjectListView.CellEditActivateMode cellEditActivation;

		private CellEditKeyEngine cellEditKeyEngine;

		private bool cellEditTabChangesRows;

		private bool cellEditEnterChangesRows;

		private ToolTipControl cellToolTip;

		private Rectangle? cellPadding;

		private StringAlignment cellVerticalAlignment = StringAlignment.Center;

		private bool copySelectionOnControlC = true;

		private bool copySelectionOnControlCUsesDragSource = true;

		private readonly List<IDecoration> decorations = new List<IDecoration>();

		private IRenderer defaultRenderer = new BaseRenderer();

		private IDragSource dragSource;

		private IDropSink dropSink;

		public static EditorRegistry EditorRegistry;

		private IOverlay emptyListMsgOverlay;

		private FilterMenuBuilder filterMenuBuilder = new FilterMenuBuilder();

		private ImageList groupImageList;

		private string groupWithItemCountFormat;

		private string groupWithItemCountSingularFormat;

		private bool hasCollapsibleGroups = true;

		private HeaderControl headerControl;

		private HeaderFormatStyle headerFormatStyle;

		private int headerMaximumHeight = -1;

		private bool headerUsesThemes = true;

		private bool headerWordWrap;

		private int hotRowIndex;

		private int hotColumnIndex;

		private HitTestLocation hotCellHitLocation;

		private HitTestLocationEx hotCellHitLocationEx;

		private OLVGroup hotGroup;

		private HotItemStyle hotItemStyle;

		private HyperlinkStyle hyperlinkStyle;

		private Color highlightBackgroundColor = Color.Empty;

		private Color highlightForegroundColor = Color.Empty;

		private bool includeHiddenColumnsInDataTransfer;

		private bool includeColumnHeadersInCopy;

		private bool isSearchOnSortColumn = true;

		private IRenderer itemRenderer;

		private IListFilter listFilter;

		private IModelFilter modelFilter;

		private OlvListViewHitTestInfo mouseMoveHitTest;

		private IList<OLVGroup> olvGroups;

		private bool ownerDrawnHeader;

		private IEnumerable objects;

		private ImageOverlay imageOverlay;

		private TextOverlay textOverlay;

		private int overlayTransparency = 128;

		private readonly List<IOverlay> overlays = new List<IOverlay>();

		private bool persistentCheckBoxes = true;

		private Dictionary<object, CheckState> checkStateMap;

		private OLVColumn primarySortColumn;

		private SortOrder primarySortOrder;

		private bool renderNonEditableCheckboxesAsDisabled;

		private int rowHeight = -1;

		private OLVColumn secondarySortColumn;

		private SortOrder secondarySortOrder;

		private bool selectAllOnControlA = true;

		private ObjectListView.ColumnSelectBehaviour selectColumnsOnRightClickBehaviour = ObjectListView.ColumnSelectBehaviour.InlineMenu;

		private bool selectColumnsMenuStaysOpen = true;

		private OLVColumn selectedColumn;

		private readonly TintedColumnDecoration selectedColumnDecoration = new TintedColumnDecoration();

		private IDecoration selectedRowDecoration;

		private Color selectedColumnTint = Color.Empty;

		private bool showCommandMenuOnRightClick;

		private bool showFilterMenuOnRightClick = true;

		private bool showSortIndicators;

		private bool showImagesOnSubItems;

		private bool showItemCountOnGroups;

		private bool showHeaderInAllViews = true;

		private ImageList shadowedImageList;

		private bool sortGroupItemsByPrimaryColumn = true;

		private int spaceBetweenGroups;

		private bool tintSortColumn;

		private bool triStateCheckBoxes;

		private bool updateSpaceFillingColumnsWhenDraggingColumnDivider = true;

		private Color unfocusedHighlightBackgroundColor = Color.Empty;

		private Color unfocusedHighlightForegroundColor = Color.Empty;

		private bool useAlternatingBackColors;

		private bool useCellFormatEvents;

		private bool useCustomSelectionColors;

		private bool useExplorerTheme;

		private bool useFiltering;

		private bool useFilterIndicator;

		private bool useHotItem;

		private bool useHyperlinks;

		private bool useOverlays = true;

		private bool useSubItemCheckBoxes;

		private bool useTranslucentSelection;

		private bool useTranslucentHotItem;

		private bool canUseApplicationIdle = true;

		private CellToolTipGetterDelegate cellToolTipGetter;

		private string checkedAspectName;

		private Munger checkedAspectMunger;

		private CheckStateGetterDelegate checkStateGetter;

		private CheckStatePutterDelegate checkStatePutter;

		private SortDelegate customSorter;

		private HeaderToolTipGetterDelegate headerToolTipGetter;

		private RowFormatterDelegate rowFormatter;

		private GroupingParameters lastGroupingParameters;

		private bool useNotifyPropertyChanged;

		private Hashtable subscribedModels = new Hashtable();

		private int timeLastCharEvent;

		private string lastSearchString;

		private readonly IntPtr minusOne = new IntPtr(-1);

		private bool isAfterItemPaint;

		private List<OLVListItem> drawnItems;

		private int prePaintLevel;

		private string menuLabelSortAscending = ObjectListView.getString_0(107316297);

		private string menuLabelSortDescending = ObjectListView.getString_0(107316264);

		private string menuLabelGroupBy = ObjectListView.getString_0(107315719);

		private string menuLabelLockGroupingOn = ObjectListView.getString_0(107315730);

		private string menuLabelUnlockGroupingOn = ObjectListView.getString_0(107315697);

		private string menuLabelTurnOffGroups = ObjectListView.getString_0(107315660);

		private string menuLabelUnsort = ObjectListView.getString_0(107315639);

		private string menuLabelColumns = ObjectListView.getString_0(107315630);

		private string menuLabelSelectColumns = ObjectListView.getString_0(107315585);

		public static Bitmap SortAscendingImage;

		public static Bitmap SortDescendingImage;

		private bool contextMenuStaysOpen;

		private int freezeCount;

		private int lastMouseDownClickCount;

		private Control cellEditor;

		internal CellEditEventArgs cellEditEventArgs;

		private bool isOwnerOfObjects;

		private bool hasIdleHandler;

		private bool hasResizeColumnsHandler;

		private bool isInWmPaintEvent;

		private bool shouldDoCustomDrawing;

		private bool isMarqueSelecting;

		private int suspendSelectionEventCount;

		private List<GlassPanelForm> glassPanels = new List<GlassPanelForm>();

		private Dictionary<string, bool> visitedUrlMap = new Dictionary<string, bool>();

		[NonSerialized]
		internal static GetString getString_0;

		public enum CellEditActivateMode
		{
			None,
			SingleClick,
			DoubleClick,
			F2Only
		}

		public enum ColumnSelectBehaviour
		{
			None,
			InlineMenu,
			Submenu,
			ModelDialog
		}

		[Serializable]
		internal sealed class ObjectListViewState
		{
			public int VersionNumber = 1;

			public int NumberOfColumns = 1;

			public View CurrentView;

			public int SortColumn = -1;

			public bool IsShowingGroups;

			public SortOrder LastSortOrder;

			public ArrayList ColumnIsVisible = new ArrayList();

			public ArrayList ColumnDisplayIndicies = new ArrayList();

			public ArrayList ColumnWidths = new ArrayList();
		}

		private sealed class SuspendSelectionDisposable : IDisposable
		{
			public SuspendSelectionDisposable(ObjectListView objectListView)
			{
				this.objectListView = objectListView;
				this.objectListView.SuspendSelectionEvents();
			}

			public void Dispose()
			{
				this.objectListView.ResumeSelectionEvents();
			}

			private readonly ObjectListView objectListView;
		}
	}
}
