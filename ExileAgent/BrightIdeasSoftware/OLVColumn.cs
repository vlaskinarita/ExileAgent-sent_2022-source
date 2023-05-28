﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using ns20;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	[Browsable(false)]
	public sealed class OLVColumn : ColumnHeader
	{
		public OLVColumn()
		{
		}

		public OLVColumn(string title, string aspect) : this()
		{
			base.Text = title;
			this.AspectName = aspect;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AspectGetterDelegate AspectGetter
		{
			get
			{
				return this.aspectGetter;
			}
			set
			{
				this.aspectGetter = value;
			}
		}

		[Obsolete("This property is no longer maintained", true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AspectGetterAutoGenerated
		{
			get
			{
				return this.aspectGetterAutoGenerated;
			}
			set
			{
				this.aspectGetterAutoGenerated = value;
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("The name of the property or method that should be called to get the aspect to display in this column")]
		public string AspectName
		{
			get
			{
				return this.aspectName;
			}
			set
			{
				this.aspectName = value;
				this.aspectMunger = null;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AspectPutterDelegate AspectPutter
		{
			get
			{
				return this.aspectPutter;
			}
			set
			{
				this.aspectPutter = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AspectToStringConverterDelegate AspectToStringConverter
		{
			get
			{
				return this.aspectToStringConverter;
			}
			set
			{
				this.aspectToStringConverter = value;
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("The format string that will be used to convert an aspect to its string representation")]
		public string AspectToStringFormat
		{
			get
			{
				return this.aspectToStringFormat;
			}
			set
			{
				this.aspectToStringFormat = value;
			}
		}

		[DefaultValue(true)]
		[Description("Should the editor for cells of this column use AutoComplete")]
		[Category("ObjectListView")]
		public bool AutoCompleteEditor
		{
			get
			{
				return this.AutoCompleteEditorMode != AutoCompleteMode.None;
			}
			set
			{
				if (value)
				{
					if (this.AutoCompleteEditorMode == AutoCompleteMode.None)
					{
						this.AutoCompleteEditorMode = AutoCompleteMode.Append;
						return;
					}
				}
				else
				{
					this.AutoCompleteEditorMode = AutoCompleteMode.None;
				}
			}
		}

		[DefaultValue(AutoCompleteMode.Append)]
		[Description("Should the editor for cells of this column use AutoComplete")]
		[Category("ObjectListView")]
		public AutoCompleteMode AutoCompleteEditorMode
		{
			get
			{
				return this.autoCompleteEditorMode;
			}
			set
			{
				this.autoCompleteEditorMode = value;
			}
		}

		[Browsable(false)]
		public bool CanBeHidden
		{
			get
			{
				return this.Hideable && base.Index != 0;
			}
		}

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

		[Description("How will cell values be vertically aligned?")]
		[DefaultValue(null)]
		[Category("ObjectListView")]
		public StringAlignment? CellVerticalAlignment
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

		[Description("Should values in this column be treated as a checkbox, rather than a string?")]
		[Category("ObjectListView")]
		[DefaultValue(false)]
		public bool CheckBoxes
		{
			get
			{
				return this.checkBoxes;
			}
			set
			{
				if (this.checkBoxes == value)
				{
					return;
				}
				this.checkBoxes = value;
				if (this.checkBoxes)
				{
					if (this.Renderer == null)
					{
						this.Renderer = new CheckStateRenderer();
						return;
					}
				}
				else if (this.Renderer is CheckStateRenderer)
				{
					this.Renderer = null;
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IClusteringStrategy ClusteringStrategy
		{
			get
			{
				if (this.clusteringStrategy == null)
				{
					this.ClusteringStrategy = this.DecideDefaultClusteringStrategy();
				}
				return this.clusteringStrategy;
			}
			set
			{
				this.clusteringStrategy = value;
				if (this.clusteringStrategy != null)
				{
					this.clusteringStrategy.Column = this;
				}
			}
		}

		[DefaultValue(false)]
		[Category("ObjectListView")]
		[Description("Will this column resize to fill unoccupied horizontal space in the listview?")]
		public bool FillsFreeSpace
		{
			get
			{
				return this.FreeSpaceProportion > 0;
			}
			set
			{
				this.FreeSpaceProportion = (value ? 1 : 0);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int FreeSpaceProportion
		{
			get
			{
				return this.freeSpaceProportion;
			}
			set
			{
				this.freeSpaceProportion = Math.Max(0, value);
			}
		}

		[Description("Will the list create groups when this header is clicked?")]
		[Category("ObjectListView")]
		[DefaultValue(true)]
		public bool Groupable
		{
			get
			{
				return this.groupable;
			}
			set
			{
				this.groupable = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GroupFormatterDelegate GroupFormatter
		{
			get
			{
				return this.groupFormatter;
			}
			set
			{
				this.groupFormatter = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GroupKeyGetterDelegate GroupKeyGetter
		{
			get
			{
				return this.groupKeyGetter;
			}
			set
			{
				this.groupKeyGetter = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GroupKeyToTitleConverterDelegate GroupKeyToTitleConverter
		{
			get
			{
				return this.groupKeyToTitleConverter;
			}
			set
			{
				this.groupKeyToTitleConverter = value;
			}
		}

		[Category("ObjectListView")]
		[Description("The format to use when suffixing item counts to group titles")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string GroupWithItemCountFormat
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
		public string GroupWithItemCountFormatOrDefault
		{
			get
			{
				if (!string.IsNullOrEmpty(this.GroupWithItemCountFormat))
				{
					return this.GroupWithItemCountFormat;
				}
				if (base.ListView != null)
				{
					this.cachedGroupWithItemCountFormat = ((ObjectListView)base.ListView).GroupWithItemCountFormatOrDefault;
					return this.cachedGroupWithItemCountFormat;
				}
				return this.cachedGroupWithItemCountFormat ?? OLVColumn.getString_0(107315843);
			}
		}

		[Description("The format to use when suffixing item counts to group titles")]
		[Category("ObjectListView")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string GroupWithItemCountSingularFormat
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
		public string GroupWithItemCountSingularFormatOrDefault
		{
			get
			{
				if (!string.IsNullOrEmpty(this.GroupWithItemCountSingularFormat))
				{
					return this.GroupWithItemCountSingularFormat;
				}
				if (base.ListView != null)
				{
					this.cachedGroupWithItemCountSingularFormat = ((ObjectListView)base.ListView).GroupWithItemCountSingularFormatOrDefault;
					return this.cachedGroupWithItemCountSingularFormat;
				}
				return this.cachedGroupWithItemCountSingularFormat ?? OLVColumn.getString_0(107315854);
			}
		}

		[Browsable(false)]
		public bool HasFilterIndicator
		{
			get
			{
				return this.UseFiltering && this.ValuesChosenForFiltering != null && this.ValuesChosenForFiltering.Count > 0;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HeaderDrawingDelegate HeaderDrawing
		{
			get
			{
				return this.headerDrawing;
			}
			set
			{
				this.headerDrawing = value;
			}
		}

		[DefaultValue(null)]
		[Description("What style will be used to draw the header of this column")]
		[Category("ObjectListView")]
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

		[Browsable(false)]
		[DefaultValue(null)]
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

		[Browsable(false)]
		[DefaultValue(typeof(Color), "")]
		public Color HeaderForeColor
		{
			get
			{
				if (this.HeaderFormatStyle != null)
				{
					return this.HeaderFormatStyle.Normal.ForeColor;
				}
				return Color.Empty;
			}
			set
			{
				if (value.IsEmpty && this.HeaderFormatStyle == null)
				{
					return;
				}
				if (this.HeaderFormatStyle == null)
				{
					this.HeaderFormatStyle = new HeaderFormatStyle();
				}
				this.HeaderFormatStyle.SetForeColor(value);
			}
		}

		[Category("ObjectListView")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Description("Name of the image that will be shown in the column header.")]
		[DefaultValue(null)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string HeaderImageKey
		{
			get
			{
				return this.headerImageKey;
			}
			set
			{
				this.headerImageKey = value;
			}
		}

		[DefaultValue(HorizontalAlignment.Left)]
		[Category("ObjectListView")]
		[Description("How will the header text be aligned?")]
		public HorizontalAlignment HeaderTextAlign
		{
			get
			{
				if (this.headerTextAlign == null)
				{
					return this.TextAlign;
				}
				return this.headerTextAlign.Value;
			}
			set
			{
				this.headerTextAlign = new HorizontalAlignment?(value);
			}
		}

		[Browsable(false)]
		public StringAlignment HeaderTextAlignAsStringAlignment
		{
			get
			{
				switch (this.HeaderTextAlign)
				{
				case HorizontalAlignment.Left:
					return StringAlignment.Near;
				case HorizontalAlignment.Right:
					return StringAlignment.Far;
				case HorizontalAlignment.Center:
					return StringAlignment.Center;
				default:
					return StringAlignment.Near;
				}
			}
		}

		[Browsable(false)]
		public bool HasHeaderImage
		{
			get
			{
				return base.ListView != null && base.ListView.SmallImageList != null && base.ListView.SmallImageList.Images.ContainsKey(this.HeaderImageKey);
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(true)]
		[Description("Will the user be able to choose to hide this column?")]
		public bool Hideable
		{
			get
			{
				return this.hideable;
			}
			set
			{
				this.hideable = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Will the text values of this column act like hyperlinks?")]
		[DefaultValue(false)]
		public bool Hyperlink
		{
			get
			{
				return this.hyperlink;
			}
			set
			{
				this.hyperlink = value;
			}
		}

		[Category("ObjectListView")]
		[Description("The name of the property that holds the image selector")]
		[DefaultValue(null)]
		public string ImageAspectName
		{
			get
			{
				return this.imageAspectName;
			}
			set
			{
				this.imageAspectName = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ImageGetterDelegate ImageGetter
		{
			get
			{
				return this.imageGetter;
			}
			set
			{
				this.imageGetter = value;
			}
		}

		[DefaultValue(true)]
		[Category("ObjectListView")]
		[Description("Can the value in this column be edited?")]
		public bool IsEditable
		{
			get
			{
				return this.isEditable;
			}
			set
			{
				this.isEditable = value;
			}
		}

		[Browsable(false)]
		public bool IsFixedWidth
		{
			get
			{
				return this.MinimumWidth != -1 && this.MaximumWidth != -1 && this.MinimumWidth >= this.MaximumWidth;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(false)]
		[Description("Will this column be used when the view is switched to tile view")]
		public bool IsTileViewColumn
		{
			get
			{
				return this.isTileViewColumn;
			}
			set
			{
				this.isTileViewColumn = value;
			}
		}

		[Description("Will the header for this column be drawn vertically?")]
		[DefaultValue(false)]
		[Category("ObjectListView")]
		public bool IsHeaderVertical
		{
			get
			{
				return this.isHeaderVertical;
			}
			set
			{
				this.isHeaderVertical = value;
			}
		}

		[Description("Can this column be seen by the user?")]
		[DefaultValue(true)]
		[Category("ObjectListView")]
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int LastDisplayIndex
		{
			get
			{
				return this.lastDisplayIndex;
			}
			set
			{
				this.lastDisplayIndex = value;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(-1)]
		[Description("What is the maximum width to which the user can resize this column?")]
		public int MaximumWidth
		{
			get
			{
				return this.maxWidth;
			}
			set
			{
				this.maxWidth = value;
				if (this.maxWidth != -1 && this.Width > this.maxWidth)
				{
					this.Width = this.maxWidth;
				}
			}
		}

		[DefaultValue(-1)]
		[Category("ObjectListView")]
		[Description("What is the minimum width to which the user can resize this column?")]
		public int MinimumWidth
		{
			get
			{
				return this.minWidth;
			}
			set
			{
				this.minWidth = value;
				if (this.Width < this.minWidth)
				{
					this.Width = this.minWidth;
				}
			}
		}

		[DefaultValue(null)]
		[Category("ObjectListView")]
		[Description("The renderer will draw this column when the ListView is owner drawn")]
		public IRenderer Renderer
		{
			get
			{
				return this.renderer;
			}
			set
			{
				this.renderer = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RenderDelegate RendererDelegate
		{
			get
			{
				if (this.Renderer is Version1Renderer)
				{
					return ((Version1Renderer)this.Renderer).RenderDelegate;
				}
				return null;
			}
			set
			{
				this.Renderer = ((value == null) ? null : new Version1Renderer(value));
			}
		}

		[DefaultValue(true)]
		[Description("Will the text of the cells in this column be considered when searching?")]
		[Category("ObjectListView")]
		public bool Searchable
		{
			get
			{
				return this.searchable;
			}
			set
			{
				this.searchable = value;
			}
		}

		[Category("ObjectListView")]
		[Description("Will the header for this column include text?")]
		[DefaultValue(true)]
		public bool ShowTextInHeader
		{
			get
			{
				return this.showTextInHeader;
			}
			set
			{
				this.showTextInHeader = value;
			}
		}

		[DefaultValue(true)]
		[Description("Will clicking this columns header resort the list?")]
		[Category("ObjectListView")]
		public bool Sortable
		{
			get
			{
				return this.sortable;
			}
			set
			{
				this.sortable = value;
			}
		}

		public new HorizontalAlignment TextAlign
		{
			get
			{
				if (this.textAlign == null)
				{
					return base.TextAlign;
				}
				return this.textAlign.Value;
			}
			set
			{
				this.textAlign = new HorizontalAlignment?(value);
				base.TextAlign = value;
			}
		}

		[Browsable(false)]
		public StringAlignment TextStringAlign
		{
			get
			{
				switch (this.TextAlign)
				{
				case HorizontalAlignment.Left:
					return StringAlignment.Near;
				case HorizontalAlignment.Right:
					return StringAlignment.Far;
				case HorizontalAlignment.Center:
					return StringAlignment.Center;
				default:
					return StringAlignment.Near;
				}
			}
		}

		[Description("The tooltip to show when the mouse is hovered over the header of this column")]
		[Category("ObjectListView")]
		[Localizable(true)]
		[DefaultValue(null)]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(false)]
		[Description("Should values in this column be treated as a tri-state checkbox?")]
		public bool TriStateCheckBoxes
		{
			get
			{
				return this.triStateCheckBoxes;
			}
			set
			{
				this.triStateCheckBoxes = value;
				if (value && !this.CheckBoxes)
				{
					this.CheckBoxes = true;
				}
			}
		}

		[Category("ObjectListView")]
		[DefaultValue(false)]
		[Description("The name of the property or method that should be called to get the aspect to display in this column")]
		public bool UseInitialLetterForGroup
		{
			get
			{
				return this.useInitialLetterForGroup;
			}
			set
			{
				this.useInitialLetterForGroup = value;
			}
		}

		[DefaultValue(true)]
		[Category("ObjectListView")]
		[Description("Does this column want to show a Filter menu item when its header is right clicked")]
		public bool UseFiltering
		{
			get
			{
				return this.useFiltering;
			}
			set
			{
				this.useFiltering = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IModelFilter ValueBasedFilter
		{
			get
			{
				if (!this.UseFiltering)
				{
					return null;
				}
				if (this.valueBasedFilter != null)
				{
					return this.valueBasedFilter;
				}
				if (this.ClusteringStrategy == null)
				{
					return null;
				}
				if (this.ValuesChosenForFiltering != null && this.ValuesChosenForFiltering.Count != 0)
				{
					return this.ClusteringStrategy.CreateFilter(this.ValuesChosenForFiltering);
				}
				return null;
			}
			set
			{
				this.valueBasedFilter = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IList ValuesChosenForFiltering
		{
			get
			{
				return this.valuesChosenForFiltering;
			}
			set
			{
				this.valuesChosenForFiltering = value;
			}
		}

		[DefaultValue(60)]
		[Category("ObjectListView")]
		[Description("The width in pixels of this column")]
		public new int Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				if (this.MaximumWidth != -1 && value > this.MaximumWidth)
				{
					base.Width = this.MaximumWidth;
					return;
				}
				base.Width = Math.Max(this.MinimumWidth, value);
			}
		}

		[Description("Draw this column cell's word wrapped")]
		[Category("ObjectListView")]
		[DefaultValue(false)]
		public bool WordWrap
		{
			get
			{
				return this.wordWrap;
			}
			set
			{
				this.wordWrap = value;
				if (this.Renderer == null && !this.wordWrap)
				{
					return;
				}
				if (this.Renderer == null)
				{
					this.Renderer = new HighlightTextRenderer();
				}
				BaseRenderer baseRenderer = this.Renderer as BaseRenderer;
				if (baseRenderer == null)
				{
					return;
				}
				baseRenderer.CanWrap = this.wordWrap;
			}
		}

		public string ConvertGroupKeyToTitle(object value)
		{
			if (this.groupKeyToTitleConverter != null)
			{
				return this.groupKeyToTitleConverter(value);
			}
			if (value != null)
			{
				return this.ValueToString(value);
			}
			return ObjectListView.GroupTitleDefault;
		}

		public CheckState GetCheckState(object rowObject)
		{
			if (!this.CheckBoxes)
			{
				return CheckState.Unchecked;
			}
			bool? flag = this.GetValue(rowObject) as bool?;
			if (flag == null)
			{
				return CheckState.Indeterminate;
			}
			if (flag.Value)
			{
				return CheckState.Checked;
			}
			return CheckState.Unchecked;
		}

		public void PutCheckState(object rowObject, CheckState newState)
		{
			if (newState == CheckState.Checked)
			{
				this.PutValue(rowObject, true);
				return;
			}
			if (newState == CheckState.Unchecked)
			{
				this.PutValue(rowObject, false);
				return;
			}
			this.PutValue(rowObject, null);
		}

		public object GetAspectByName(object rowObject)
		{
			if (this.aspectMunger == null)
			{
				this.aspectMunger = new Munger(this.AspectName);
			}
			return this.aspectMunger.GetValue(rowObject);
		}

		public object GetGroupKey(object rowObject)
		{
			if (this.groupKeyGetter == null)
			{
				object obj = this.GetValue(rowObject);
				string text = obj as string;
				if (text != null && this.UseInitialLetterForGroup && text.Length > 0)
				{
					obj = text.Substring(0, 1).ToUpper();
				}
				return obj;
			}
			return this.groupKeyGetter(rowObject);
		}

		public object GetImage(object rowObject)
		{
			if (this.CheckBoxes)
			{
				return this.GetCheckStateImage(rowObject);
			}
			if (this.ImageGetter != null)
			{
				return this.ImageGetter(rowObject);
			}
			if (!string.IsNullOrEmpty(this.ImageAspectName))
			{
				if (this.imageAspectMunger == null)
				{
					this.imageAspectMunger = new Munger(this.ImageAspectName);
				}
				return this.imageAspectMunger.GetValue(rowObject);
			}
			if (!string.IsNullOrEmpty(base.ImageKey))
			{
				return base.ImageKey;
			}
			return base.ImageIndex;
		}

		public string GetCheckStateImage(object rowObject)
		{
			CheckState checkState = this.GetCheckState(rowObject);
			if (checkState == CheckState.Checked)
			{
				return OLVColumn.getString_0(107316155);
			}
			if (checkState == CheckState.Unchecked)
			{
				return OLVColumn.getString_0(107316180);
			}
			return OLVColumn.getString_0(107316098);
		}

		public string GetStringValue(object rowObject)
		{
			return this.ValueToString(this.GetValue(rowObject));
		}

		public object GetValue(object rowObject)
		{
			if (this.AspectGetter == null)
			{
				return this.GetAspectByName(rowObject);
			}
			return this.AspectGetter(rowObject);
		}

		public void PutAspectByName(object rowObject, object newValue)
		{
			if (this.aspectMunger == null)
			{
				this.aspectMunger = new Munger(this.AspectName);
			}
			this.aspectMunger.PutValue(rowObject, newValue);
		}

		public void PutValue(object rowObject, object newValue)
		{
			if (this.aspectPutter == null)
			{
				this.PutAspectByName(rowObject, newValue);
				return;
			}
			this.aspectPutter(rowObject, newValue);
		}

		public string ValueToString(object value)
		{
			if (this.AspectToStringConverter != null)
			{
				return this.AspectToStringConverter(value) ?? string.Empty;
			}
			if (value == null)
			{
				return string.Empty;
			}
			string text = this.AspectToStringFormat;
			if (string.IsNullOrEmpty(text))
			{
				return value.ToString();
			}
			return string.Format(text, value);
		}

		private IClusteringStrategy DecideDefaultClusteringStrategy()
		{
			if (!this.UseFiltering)
			{
				return null;
			}
			if (this.DataType == typeof(DateTime))
			{
				return new DateTimeClusteringStrategy();
			}
			return new ClustersFromGroupsStrategy();
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type DataType
		{
			get
			{
				if (this.dataType == null)
				{
					ObjectListView objectListView = base.ListView as ObjectListView;
					if (objectListView != null)
					{
						object firstNonNullValue = objectListView.GetFirstNonNullValue(this);
						if (firstNonNullValue != null)
						{
							return firstNonNullValue.GetType();
						}
					}
				}
				return this.dataType;
			}
			set
			{
				this.dataType = value;
			}
		}

		public void MakeGroupies(object[] values, string[] descriptions)
		{
			this.MakeGroupies<object>(values, descriptions, null, null, null);
		}

		public void MakeGroupies<T>(T[] values, string[] descriptions)
		{
			this.MakeGroupies<T>(values, descriptions, null, null, null);
		}

		public void MakeGroupies<T>(T[] values, string[] descriptions, object[] images)
		{
			this.MakeGroupies<T>(values, descriptions, images, null, null);
		}

		public void MakeGroupies<T>(T[] values, string[] descriptions, object[] images, string[] subtitles)
		{
			this.MakeGroupies<T>(values, descriptions, images, subtitles, null);
		}

		public void MakeGroupies<T>(T[] values, string[] descriptions, object[] images, string[] subtitles, string[] tasks)
		{
			if (values == null)
			{
				throw new ArgumentNullException(OLVColumn.getString_0(107315103));
			}
			if (descriptions == null)
			{
				throw new ArgumentNullException(OLVColumn.getString_0(107315126));
			}
			if (values.Length + 1 != descriptions.Length)
			{
				throw new ArgumentException(OLVColumn.getString_0(107315077));
			}
			this.GroupKeyGetter = delegate(object row)
			{
				object value = this.GetValue(row);
				if (value != null)
				{
					if (value != DBNull.Value)
					{
						IComparable comparable = (IComparable)value;
						for (int i = 0; i < values.Length; i++)
						{
							if (comparable.CompareTo(values[i]) < 0)
							{
								return i;
							}
						}
						return descriptions.Length - 1;
					}
				}
				return -1;
			};
			this.GroupKeyToTitleConverter = delegate(object key)
			{
				if ((int)key < 0)
				{
					return Class401.smethod_0(107395212);
				}
				return descriptions[(int)key];
			};
			this.GroupFormatter = delegate(OLVGroup group, GroupingParameters parms)
			{
				int num = (int)group.Key;
				if (num >= 0)
				{
					if (images != null && num < images.Length)
					{
						group.TitleImage = images[num];
					}
					if (subtitles != null && num < subtitles.Length)
					{
						group.Subtitle = subtitles[num];
					}
					if (tasks != null && num < tasks.Length)
					{
						group.Task = tasks[num];
					}
				}
			};
		}

		public void MakeEqualGroupies<T>(T[] values, string[] descriptions, object[] images, string[] subtitles, string[] tasks)
		{
			if (values == null)
			{
				throw new ArgumentNullException(OLVColumn.getString_0(107315103));
			}
			if (descriptions == null)
			{
				throw new ArgumentNullException(OLVColumn.getString_0(107315126));
			}
			if (values.Length != descriptions.Length)
			{
				throw new ArgumentException(OLVColumn.getString_0(107315036));
			}
			ArrayList valuesArray = new ArrayList(values);
			this.GroupKeyGetter = ((object row) => valuesArray.IndexOf(this.GetValue(row)));
			this.GroupKeyToTitleConverter = delegate(object key)
			{
				int num = (int)key;
				if (num >= 0)
				{
					return descriptions[num];
				}
				return Class401.smethod_0(107304011);
			};
			this.GroupFormatter = delegate(OLVGroup group, GroupingParameters parms)
			{
				int num = (int)group.Key;
				if (num >= 0)
				{
					if (images != null && num < images.Length)
					{
						group.TitleImage = images[num];
					}
					if (subtitles != null && num < subtitles.Length)
					{
						group.Subtitle = subtitles[num];
					}
					if (tasks != null && num < tasks.Length)
					{
						group.Task = tasks[num];
					}
				}
			};
		}

		static OLVColumn()
		{
			Strings.CreateGetStringDelegate(typeof(OLVColumn));
		}

		private AspectGetterDelegate aspectGetter;

		private bool aspectGetterAutoGenerated;

		private string aspectName;

		private AspectPutterDelegate aspectPutter;

		private AspectToStringConverterDelegate aspectToStringConverter;

		private string aspectToStringFormat;

		private AutoCompleteMode autoCompleteEditorMode = AutoCompleteMode.Append;

		private Rectangle? cellPadding;

		private StringAlignment? cellVerticalAlignment;

		private bool checkBoxes;

		private IClusteringStrategy clusteringStrategy;

		private int freeSpaceProportion;

		private bool groupable = true;

		private GroupFormatterDelegate groupFormatter;

		private GroupKeyGetterDelegate groupKeyGetter;

		private GroupKeyToTitleConverterDelegate groupKeyToTitleConverter;

		private string groupWithItemCountFormat;

		private string cachedGroupWithItemCountFormat;

		private string groupWithItemCountSingularFormat;

		private string cachedGroupWithItemCountSingularFormat;

		private HeaderDrawingDelegate headerDrawing;

		private HeaderFormatStyle headerFormatStyle;

		private string headerImageKey;

		private HorizontalAlignment? headerTextAlign;

		private bool hideable = true;

		private bool hyperlink;

		private string imageAspectName;

		private ImageGetterDelegate imageGetter;

		private bool isEditable = true;

		private bool isTileViewColumn;

		private bool isHeaderVertical;

		private bool isVisible = true;

		private int lastDisplayIndex = -1;

		private int maxWidth = -1;

		private int minWidth = -1;

		private IRenderer renderer;

		private bool searchable = true;

		private bool showTextInHeader = true;

		private bool sortable = true;

		private HorizontalAlignment? textAlign;

		private string toolTipText;

		private bool triStateCheckBoxes;

		private bool useInitialLetterForGroup;

		private bool useFiltering = true;

		private IModelFilter valueBasedFilter;

		private IList valuesChosenForFiltering = new ArrayList();

		private bool wordWrap;

		private Munger aspectMunger;

		private Munger imageAspectMunger;

		private Type dataType;

		[NonSerialized]
		internal static GetString getString_0;
	}
}