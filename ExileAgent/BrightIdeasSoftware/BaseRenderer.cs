using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BrightIdeasSoftware
{
	[Browsable(true)]
	[ToolboxItem(true)]
	public class BaseRenderer : AbstractRenderer
	{
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Can the renderer wrap text that does not fit completely within the cell")]
		public bool CanWrap
		{
			get
			{
				return this.canWrap;
			}
			set
			{
				this.canWrap = value;
				if (this.canWrap)
				{
					this.UseGdiTextRendering = false;
				}
			}
		}

		[Description("The number of pixels that renderer will leave empty around the edge of the cell")]
		[Category("ObjectListView")]
		[DefaultValue(null)]
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
		[DefaultValue(null)]
		public virtual StringAlignment? CellVerticalAlignment
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

		[Browsable(false)]
		protected virtual Rectangle? EffectiveCellPadding
		{
			get
			{
				if (this.cellPadding != null)
				{
					return new Rectangle?(this.cellPadding.Value);
				}
				if (this.OLVSubItem != null && this.OLVSubItem.CellPadding != null)
				{
					return new Rectangle?(this.OLVSubItem.CellPadding.Value);
				}
				if (this.ListItem != null && this.ListItem.CellPadding != null)
				{
					return new Rectangle?(this.ListItem.CellPadding.Value);
				}
				if (this.Column != null && this.Column.CellPadding != null)
				{
					return new Rectangle?(this.Column.CellPadding.Value);
				}
				if (this.ListView != null && this.ListView.CellPadding != null)
				{
					return new Rectangle?(this.ListView.CellPadding.Value);
				}
				return null;
			}
		}

		[Browsable(false)]
		protected virtual StringAlignment EffectiveCellVerticalAlignment
		{
			get
			{
				if (this.cellVerticalAlignment != null)
				{
					return this.cellVerticalAlignment.Value;
				}
				if (this.OLVSubItem != null && this.OLVSubItem.CellVerticalAlignment != null)
				{
					return this.OLVSubItem.CellVerticalAlignment.Value;
				}
				if (this.ListItem != null && this.ListItem.CellVerticalAlignment != null)
				{
					return this.ListItem.CellVerticalAlignment.Value;
				}
				if (this.Column != null && this.Column.CellVerticalAlignment != null)
				{
					return this.Column.CellVerticalAlignment.Value;
				}
				if (this.ListView != null)
				{
					return this.ListView.CellVerticalAlignment;
				}
				return StringAlignment.Center;
			}
		}

		[Category("Appearance")]
		[DefaultValue(null)]
		[Description("The image list from which keyed images will be fetched for drawing.")]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				this.imageList = value;
			}
		}

		[DefaultValue(1)]
		[Category("Appearance")]
		[Description("When rendering multiple images, how many pixels should be between each image?")]
		public int Spacing
		{
			get
			{
				return this.spacing;
			}
			set
			{
				this.spacing = value;
			}
		}

		[Description("Should text be rendered using GDI routines?")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool UseGdiTextRendering
		{
			get
			{
				return !this.IsPrinting && this.useGdiTextRendering;
			}
			set
			{
				this.useGdiTextRendering = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Aspect
		{
			get
			{
				if (this.aspect == null)
				{
					this.aspect = this.column.GetValue(this.rowObject);
				}
				return this.aspect;
			}
			set
			{
				this.aspect = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
			set
			{
				this.bounds = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OLVColumn Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DrawListViewItemEventArgs DrawItemEvent
		{
			get
			{
				return this.drawItemEventArgs;
			}
			set
			{
				this.drawItemEventArgs = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DrawListViewSubItemEventArgs Event
		{
			get
			{
				return this.eventArgs;
			}
			set
			{
				this.eventArgs = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Font Font
		{
			get
			{
				if (this.font != null || this.ListItem == null)
				{
					return this.font;
				}
				if (this.SubItem != null && !this.ListItem.UseItemStyleForSubItems)
				{
					return this.SubItem.Font;
				}
				return this.ListItem.Font;
			}
			set
			{
				this.font = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ImageList ImageListOrDefault
		{
			get
			{
				return this.ImageList ?? this.ListView.SmallImageList;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDrawBackground
		{
			get
			{
				return !this.IsPrinting;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsItemSelected
		{
			get
			{
				return this.isItemSelected;
			}
			set
			{
				this.isItemSelected = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPrinting
		{
			get
			{
				return this.isPrinting;
			}
			set
			{
				this.isPrinting = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OLVListItem ListItem
		{
			get
			{
				return this.listItem;
			}
			set
			{
				this.listItem = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ObjectListView ListView
		{
			get
			{
				return this.objectListView;
			}
			set
			{
				this.objectListView = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OLVListSubItem OLVSubItem
		{
			get
			{
				return this.listSubItem;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object RowObject
		{
			get
			{
				return this.rowObject;
			}
			set
			{
				this.rowObject = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OLVListSubItem SubItem
		{
			get
			{
				return this.listSubItem;
			}
			set
			{
				this.listSubItem = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush TextBrush
		{
			get
			{
				if (this.textBrush == null)
				{
					return new SolidBrush(this.GetForegroundColor());
				}
				return this.textBrush;
			}
			set
			{
				this.textBrush = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool UseCustomCheckboxImages
		{
			get
			{
				return this.useCustomCheckboxImages;
			}
			set
			{
				this.useCustomCheckboxImages = value;
			}
		}

		private void ClearState()
		{
			this.Event = null;
			this.DrawItemEvent = null;
			this.Aspect = null;
			this.Font = null;
			this.TextBrush = null;
		}

		protected virtual Rectangle AlignRectangle(Rectangle outer, Rectangle inner)
		{
			Rectangle result = new Rectangle(outer.Location, inner.Size);
			if (inner.Width < outer.Width)
			{
				result.X = this.AlignHorizontally(outer, inner);
			}
			if (inner.Height < outer.Height)
			{
				result.Y = this.AlignVertically(outer, inner);
			}
			return result;
		}

		protected int AlignHorizontally(Rectangle outer, Rectangle inner)
		{
			switch ((this.Column == null) ? HorizontalAlignment.Left : this.Column.TextAlign)
			{
			case HorizontalAlignment.Left:
				return outer.Left + 1;
			case HorizontalAlignment.Right:
				return outer.Right - inner.Width - 1;
			case HorizontalAlignment.Center:
				return outer.Left + (outer.Width - inner.Width) / 2;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		protected int AlignVertically(Rectangle outer, Rectangle inner)
		{
			return this.AlignVertically(outer, inner.Height);
		}

		protected int AlignVertically(Rectangle outer, int innerHeight)
		{
			switch (this.EffectiveCellVerticalAlignment)
			{
			case StringAlignment.Near:
				return outer.Top + 1;
			case StringAlignment.Center:
				return outer.Top + (outer.Height - innerHeight) / 2;
			case StringAlignment.Far:
				return outer.Bottom - innerHeight - 1;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		protected virtual Rectangle CalculateAlignedRectangle(Graphics g, Rectangle r)
		{
			if (this.Column == null || this.Column.TextAlign == HorizontalAlignment.Left)
			{
				return r;
			}
			int num = this.CalculateCheckBoxWidth(g);
			num += this.CalculateImageWidth(g, this.GetImageSelector());
			num += this.CalculateTextWidth(g, this.GetText());
			if (num >= r.Width)
			{
				return r;
			}
			return this.AlignRectangle(r, new Rectangle(0, 0, num, r.Height));
		}

		protected Rectangle CalculateCheckBoxBounds(Graphics g, Rectangle cellBounds)
		{
			Size size = (!this.UseCustomCheckboxImages || this.ListView.StateImageList == null) ? CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.CheckedNormal) : this.ListView.StateImageList.ImageSize;
			return this.AlignRectangle(cellBounds, new Rectangle(0, 0, size.Width, size.Height));
		}

		protected virtual int CalculateCheckBoxWidth(Graphics g)
		{
			if (!this.ListView.CheckBoxes || !this.ColumnIsPrimary)
			{
				return 0;
			}
			if (this.UseCustomCheckboxImages && this.ListView.StateImageList != null)
			{
				return this.ListView.StateImageList.ImageSize.Width;
			}
			return CheckBoxRenderer.GetGlyphSize(g, CheckBoxState.UncheckedNormal).Width + 6;
		}

		protected virtual int CalculateImageWidth(Graphics g, object imageSelector)
		{
			if (imageSelector != null)
			{
				if (imageSelector != DBNull.Value)
				{
					ImageList imageListOrDefault = this.ImageListOrDefault;
					if (imageListOrDefault != null)
					{
						int num = -1;
						if (imageSelector is int)
						{
							num = (int)imageSelector;
						}
						else
						{
							string text = imageSelector as string;
							if (text != null)
							{
								num = imageListOrDefault.Images.IndexOfKey(text);
							}
						}
						if (num >= 0)
						{
							return imageListOrDefault.ImageSize.Width;
						}
					}
					Image image = imageSelector as Image;
					if (image != null)
					{
						return image.Width;
					}
					return 0;
				}
			}
			return 0;
		}

		protected virtual int CalculateTextWidth(Graphics g, string txt)
		{
			if (string.IsNullOrEmpty(txt))
			{
				return 0;
			}
			if (this.UseGdiTextRendering)
			{
				Size proposedSize = new Size(int.MaxValue, int.MaxValue);
				return TextRenderer.MeasureText(g, txt, this.Font, proposedSize, TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix).Width;
			}
			int result;
			using (StringFormat stringFormat = new StringFormat())
			{
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
				result = 1 + (int)g.MeasureString(txt, this.Font, int.MaxValue, stringFormat).Width;
			}
			return result;
		}

		public virtual Color GetBackgroundColor()
		{
			if (!this.ListView.Enabled)
			{
				return SystemColors.Control;
			}
			if (this.IsItemSelected && !this.ListView.UseTranslucentSelection && this.ListView.FullRowSelect)
			{
				if (this.ListView.Focused)
				{
					return this.ListView.HighlightBackgroundColorOrDefault;
				}
				if (!this.ListView.HideSelection)
				{
					return this.ListView.UnfocusedHighlightBackgroundColorOrDefault;
				}
			}
			if (this.SubItem != null && !this.ListItem.UseItemStyleForSubItems)
			{
				return this.SubItem.BackColor;
			}
			return this.ListItem.BackColor;
		}

		public virtual Color GetForegroundColor()
		{
			if (this.IsItemSelected && !this.ListView.UseTranslucentSelection && (this.ColumnIsPrimary || this.ListView.FullRowSelect))
			{
				if (this.ListView.Focused)
				{
					return this.ListView.HighlightForegroundColorOrDefault;
				}
				if (!this.ListView.HideSelection)
				{
					return this.ListView.UnfocusedHighlightForegroundColorOrDefault;
				}
			}
			if (this.SubItem != null && !this.ListItem.UseItemStyleForSubItems)
			{
				return this.SubItem.ForeColor;
			}
			return this.ListItem.ForeColor;
		}

		protected virtual Image GetImage()
		{
			return this.GetImage(this.GetImageSelector());
		}

		protected virtual Image GetImage(object imageSelector)
		{
			if (imageSelector != null)
			{
				if (imageSelector != DBNull.Value)
				{
					ImageList imageListOrDefault = this.ImageListOrDefault;
					if (imageListOrDefault != null)
					{
						if (imageSelector is int)
						{
							int num = (int)imageSelector;
							if (num >= 0 && num < imageListOrDefault.Images.Count)
							{
								return imageListOrDefault.Images[num];
							}
							return null;
						}
						else
						{
							string text = imageSelector as string;
							if (text != null)
							{
								if (imageListOrDefault.Images.ContainsKey(text))
								{
									return imageListOrDefault.Images[text];
								}
								return null;
							}
						}
					}
					return imageSelector as Image;
				}
			}
			return null;
		}

		protected virtual object GetImageSelector()
		{
			if (!this.ColumnIsPrimary)
			{
				return this.OLVSubItem.ImageSelector;
			}
			return this.ListItem.ImageSelector;
		}

		protected virtual string GetText()
		{
			if (this.SubItem != null)
			{
				return this.SubItem.Text;
			}
			return this.ListItem.Text;
		}

		protected virtual Color GetTextBackgroundColor()
		{
			if (this.IsItemSelected && !this.ListView.UseTranslucentSelection && (this.ColumnIsPrimary || this.ListView.FullRowSelect))
			{
				if (this.ListView.Focused)
				{
					return this.ListView.HighlightBackgroundColorOrDefault;
				}
				if (!this.ListView.HideSelection)
				{
					return this.ListView.UnfocusedHighlightBackgroundColorOrDefault;
				}
			}
			if (this.SubItem != null && !this.ListItem.UseItemStyleForSubItems)
			{
				return this.SubItem.BackColor;
			}
			return this.ListItem.BackColor;
		}

		public override bool RenderItem(DrawListViewItemEventArgs e, Graphics g, Rectangle itemBounds, object rowObject)
		{
			this.ClearState();
			this.DrawItemEvent = e;
			this.ListItem = (OLVListItem)e.Item;
			this.SubItem = null;
			this.ListView = (ObjectListView)this.ListItem.ListView;
			this.Column = this.ListView.GetColumn(0);
			this.RowObject = rowObject;
			this.Bounds = itemBounds;
			this.IsItemSelected = this.ListItem.Selected;
			return this.OptionalRender(g, itemBounds);
		}

		public override bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle cellBounds, object rowObject)
		{
			this.ClearState();
			this.Event = e;
			this.ListItem = (OLVListItem)e.Item;
			this.SubItem = (OLVListSubItem)e.SubItem;
			this.ListView = (ObjectListView)this.ListItem.ListView;
			this.Column = (OLVColumn)e.Header;
			this.RowObject = rowObject;
			this.Bounds = cellBounds;
			this.IsItemSelected = this.ListItem.Selected;
			return this.OptionalRender(g, cellBounds);
		}

		public override void HitTest(OlvListViewHitTestInfo hti, int x, int y)
		{
			this.ClearState();
			this.ListView = hti.ListView;
			this.ListItem = hti.Item;
			this.SubItem = hti.SubItem;
			this.Column = hti.Column;
			this.RowObject = hti.RowObject;
			this.IsItemSelected = this.ListItem.Selected;
			if (this.SubItem == null)
			{
				this.Bounds = this.ListItem.Bounds;
			}
			else
			{
				this.Bounds = this.ListItem.GetSubItemBounds(this.Column.Index);
			}
			using (Graphics graphics = this.ListView.CreateGraphics())
			{
				this.HandleHitTest(graphics, hti, x, y);
			}
		}

		public override Rectangle GetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			this.ClearState();
			this.ListView = (ObjectListView)item.ListView;
			this.ListItem = item;
			this.SubItem = item.GetSubItem(subItemIndex);
			this.Column = this.ListView.GetColumn(subItemIndex);
			this.RowObject = item.RowObject;
			this.IsItemSelected = this.ListItem.Selected;
			this.Bounds = cellBounds;
			return this.HandleGetEditRectangle(g, cellBounds, item, subItemIndex, preferredSize);
		}

		public virtual bool OptionalRender(Graphics g, Rectangle r)
		{
			if (this.ListView.View == View.Details)
			{
				this.Render(g, r);
				return true;
			}
			return false;
		}

		public virtual void Render(Graphics g, Rectangle r)
		{
			this.StandardRender(g, r);
		}

		protected virtual void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
		{
			Rectangle rectangle = this.CalculateAlignedRectangle(g, this.Bounds);
			this.StandardHitTest(g, hti, rectangle, x, y);
		}

		protected virtual Rectangle HandleGetEditRectangle(Graphics g, Rectangle cellBounds, OLVListItem item, int subItemIndex, Size preferredSize)
		{
			if (base.GetType() == typeof(BaseRenderer))
			{
				return this.StandardGetEditRectangle(g, cellBounds, preferredSize);
			}
			return cellBounds;
		}

		protected void StandardRender(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			if (this.ColumnIsPrimary)
			{
				r.X += 3;
				r.Width--;
			}
			r = this.ApplyCellPadding(r);
			this.DrawAlignedImageAndText(g, r);
			if (ObjectListView.ShowCellPaddingBounds)
			{
				g.DrawRectangle(Pens.Purple, r);
			}
		}

		public virtual Rectangle ApplyCellPadding(Rectangle r)
		{
			Rectangle? effectiveCellPadding = this.EffectiveCellPadding;
			if (effectiveCellPadding == null)
			{
				return r;
			}
			Rectangle value = effectiveCellPadding.Value;
			r.Width -= value.Right;
			r.Height -= value.Bottom;
			r.Offset(value.Location);
			return r;
		}

		protected void StandardHitTest(Graphics g, OlvListViewHitTestInfo hti, Rectangle bounds, int x, int y)
		{
			Rectangle rectangle = bounds;
			if (this.ColumnIsPrimary && !(this is TreeListView.TreeRenderer))
			{
				rectangle.X += 3;
				rectangle.Width--;
			}
			rectangle = this.ApplyCellPadding(rectangle);
			int num = 0;
			if (this.ColumnIsPrimary && this.ListView.CheckBoxes)
			{
				Rectangle rectangle2 = this.CalculateCheckBoxBounds(g, rectangle);
				Rectangle rectangle3 = rectangle2;
				rectangle3.Inflate(2, 2);
				if (rectangle3.Contains(x, y))
				{
					hti.HitTestLocation = HitTestLocation.CheckBox;
					return;
				}
				num = rectangle3.Width;
			}
			rectangle.X += num;
			rectangle.Width -= num;
			num = this.CalculateImageWidth(g, this.GetImageSelector());
			Rectangle rectangle4 = rectangle;
			rectangle4.Width = num;
			if (rectangle4.Contains(x, y))
			{
				if (this.Column != null && this.Column.Index > 0 && this.Column.CheckBoxes)
				{
					hti.HitTestLocation = HitTestLocation.CheckBox;
					return;
				}
				hti.HitTestLocation = HitTestLocation.Image;
				return;
			}
			else
			{
				rectangle.X += num;
				rectangle.Width -= num;
				num = this.CalculateTextWidth(g, this.GetText());
				rectangle4 = rectangle;
				rectangle4.Width = num;
				if (rectangle4.Contains(x, y))
				{
					hti.HitTestLocation = HitTestLocation.Text;
					return;
				}
				hti.HitTestLocation = HitTestLocation.InCell;
				return;
			}
		}

		protected Rectangle StandardGetEditRectangle(Graphics g, Rectangle cellBounds, Size preferredSize)
		{
			Rectangle rectangle = this.CalculateAlignedRectangle(g, cellBounds);
			rectangle = this.CalculatePaddedAlignedBounds(g, rectangle, preferredSize);
			int num = this.CalculateCheckBoxWidth(g);
			num += this.CalculateImageWidth(g, this.GetImageSelector());
			if (this.ColumnIsPrimary && this.ListItem.IndentCount > 0)
			{
				int width = this.ListView.SmallImageSize.Width;
				num += width * this.ListItem.IndentCount;
			}
			if (num > 0)
			{
				rectangle.X += num;
				rectangle.Width = Math.Max(rectangle.Width - num, 40);
			}
			return rectangle;
		}

		protected Rectangle CalculatePaddedAlignedBounds(Graphics g, Rectangle cellBounds, Size preferredSize)
		{
			Rectangle outer = this.ApplyCellPadding(cellBounds);
			return this.AlignRectangle(outer, new Rectangle(0, 0, outer.Width, preferredSize.Height));
		}

		protected virtual void DrawAlignedImage(Graphics g, Rectangle r, Image image)
		{
			if (image == null)
			{
				return;
			}
			Rectangle inner = new Rectangle(r.Location, image.Size);
			if (image.Height > r.Height)
			{
				float num = (float)r.Height / (float)image.Height;
				inner.Width = (int)((float)image.Width * num);
				inner.Height = r.Height - 1;
			}
			g.DrawImage(image, this.AlignRectangle(r, inner));
		}

		protected virtual void DrawAlignedImageAndText(Graphics g, Rectangle r)
		{
			this.DrawImageAndText(g, this.CalculateAlignedRectangle(g, r));
		}

		protected virtual void DrawBackground(Graphics g, Rectangle r)
		{
			if (!this.IsDrawBackground)
			{
				return;
			}
			Color backgroundColor = this.GetBackgroundColor();
			using (Brush brush = new SolidBrush(backgroundColor))
			{
				g.FillRectangle(brush, r.X - 1, r.Y - 1, r.Width + 2, r.Height + 2);
			}
		}

		protected virtual int DrawCheckBox(Graphics g, Rectangle r)
		{
			int stateImageIndex = this.ListItem.StateImageIndex;
			if (!this.IsPrinting && !this.UseCustomCheckboxImages)
			{
				r = this.CalculateCheckBoxBounds(g, r);
				CheckBoxState checkBoxState = this.GetCheckBoxState(this.ListItem.CheckState);
				CheckBoxRenderer.DrawCheckBox(g, r.Location, checkBoxState);
				return CheckBoxRenderer.GetGlyphSize(g, checkBoxState).Width + 6;
			}
			if (this.ListView.StateImageList != null && stateImageIndex >= 0)
			{
				return this.DrawImage(g, r, this.ListView.StateImageList.Images[stateImageIndex]) + 4;
			}
			return 0;
		}

		protected virtual CheckBoxState GetCheckBoxState(CheckState checkState)
		{
			if (this.IsCheckBoxDisabled)
			{
				switch (checkState)
				{
				case CheckState.Unchecked:
					return CheckBoxState.UncheckedDisabled;
				case CheckState.Checked:
					return CheckBoxState.CheckedDisabled;
				default:
					return CheckBoxState.MixedDisabled;
				}
			}
			else if (this.IsItemHot)
			{
				switch (checkState)
				{
				case CheckState.Unchecked:
					return CheckBoxState.UncheckedHot;
				case CheckState.Checked:
					return CheckBoxState.CheckedHot;
				default:
					return CheckBoxState.MixedHot;
				}
			}
			else
			{
				switch (checkState)
				{
				case CheckState.Unchecked:
					return CheckBoxState.UncheckedNormal;
				case CheckState.Checked:
					return CheckBoxState.CheckedNormal;
				default:
					return CheckBoxState.MixedNormal;
				}
			}
		}

		protected virtual bool IsCheckBoxDisabled
		{
			get
			{
				return this.ListView.RenderNonEditableCheckboxesAsDisabled && (this.ListView.CellEditActivation == ObjectListView.CellEditActivateMode.None || (this.Column != null && !this.Column.IsEditable));
			}
		}

		protected bool IsItemHot
		{
			get
			{
				return this.ListView != null && this.ListItem != null && this.ListView.HotRowIndex == this.ListItem.Index && this.ListView.HotColumnIndex == ((this.Column == null) ? 0 : this.Column.Index) && this.ListView.HotCellHitLocation == HitTestLocation.CheckBox;
			}
		}

		protected virtual int DrawImage(Graphics g, Rectangle r, object imageSelector)
		{
			if (imageSelector != null)
			{
				if (imageSelector != DBNull.Value)
				{
					ImageList smallImageList = this.ListView.SmallImageList;
					if (smallImageList != null)
					{
						int num = -1;
						if (imageSelector is int)
						{
							num = (int)imageSelector;
							if (num >= smallImageList.Images.Count)
							{
								num = -1;
							}
						}
						else
						{
							string text = imageSelector as string;
							if (text != null)
							{
								num = smallImageList.Images.IndexOfKey(text);
							}
						}
						if (num >= 0)
						{
							if (!this.IsPrinting)
							{
								if (smallImageList.ImageSize.Height < r.Height)
								{
									r.Y = this.AlignVertically(r, new Rectangle(Point.Empty, smallImageList.ImageSize));
								}
								Rectangle rectangle = new Rectangle(r.X - this.Bounds.X, r.Y - this.Bounds.Y, r.Width, r.Height);
								smallImageList.Draw(g, rectangle.Location, num);
								return smallImageList.ImageSize.Width;
							}
							imageSelector = smallImageList.Images[num];
						}
					}
					Image image = imageSelector as Image;
					if (image != null)
					{
						if (image.Size.Height < r.Height)
						{
							r.Y = this.AlignVertically(r, new Rectangle(Point.Empty, image.Size));
						}
						g.DrawImageUnscaled(image, r.X, r.Y);
						return image.Width;
					}
					return 0;
				}
			}
			return 0;
		}

		protected virtual void DrawImageAndText(Graphics g, Rectangle r)
		{
			int num;
			if (this.ListView.CheckBoxes && this.ColumnIsPrimary)
			{
				num = this.DrawCheckBox(g, r);
				r.X += num;
				r.Width -= num;
			}
			num = this.DrawImage(g, r, this.GetImageSelector());
			r.X += num;
			r.Width -= num;
			this.DrawText(g, r, this.GetText());
		}

		protected virtual int DrawImages(Graphics g, Rectangle r, ICollection imageSelectors)
		{
			List<Image> list = new List<Image>();
			foreach (object imageSelector in imageSelectors)
			{
				Image image = this.GetImage(imageSelector);
				if (image != null)
				{
					list.Add(image);
				}
			}
			int num = 0;
			int num2 = 0;
			foreach (Image image2 in list)
			{
				num += image2.Width + this.Spacing;
				num2 = Math.Max(num2, image2.Height);
			}
			Point location = this.AlignRectangle(r, new Rectangle(0, 0, num, num2)).Location;
			foreach (Image image3 in list)
			{
				g.DrawImage(image3, location);
				location.X += image3.Width + this.Spacing;
			}
			return num;
		}

		public virtual void DrawText(Graphics g, Rectangle r, string txt)
		{
			if (string.IsNullOrEmpty(txt))
			{
				return;
			}
			if (this.UseGdiTextRendering)
			{
				this.DrawTextGdi(g, r, txt);
				return;
			}
			this.DrawTextGdiPlus(g, r, txt);
		}

		protected virtual void DrawTextGdi(Graphics g, Rectangle r, string txt)
		{
			Color backColor = Color.Transparent;
			if (this.IsDrawBackground && this.IsItemSelected && this.ColumnIsPrimary && !this.ListView.FullRowSelect)
			{
				backColor = this.GetTextBackgroundColor();
			}
			TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.PreserveGraphicsTranslateTransform | this.CellVerticalAlignmentAsTextFormatFlag;
			if (!this.CanWrap)
			{
				textFormatFlags |= TextFormatFlags.SingleLine;
			}
			TextRenderer.DrawText(g, txt, this.Font, r, this.GetForegroundColor(), backColor, textFormatFlags);
		}

		private bool ColumnIsPrimary
		{
			get
			{
				return this.Column != null && this.Column.Index == 0;
			}
		}

		protected TextFormatFlags CellVerticalAlignmentAsTextFormatFlag
		{
			get
			{
				switch (this.EffectiveCellVerticalAlignment)
				{
				case StringAlignment.Near:
					return TextFormatFlags.Default;
				case StringAlignment.Center:
					return TextFormatFlags.VerticalCenter;
				case StringAlignment.Far:
					return TextFormatFlags.Bottom;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		protected virtual StringFormat StringFormatForGdiPlus
		{
			get
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.LineAlignment = this.EffectiveCellVerticalAlignment;
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
				stringFormat.Alignment = ((this.Column == null) ? StringAlignment.Near : this.Column.TextStringAlign);
				if (!this.CanWrap)
				{
					stringFormat.FormatFlags = StringFormatFlags.NoWrap;
				}
				return stringFormat;
			}
		}

		protected virtual void DrawTextGdiPlus(Graphics g, Rectangle r, string txt)
		{
			using (StringFormat stringFormatForGdiPlus = this.StringFormatForGdiPlus)
			{
				Font font = this.Font;
				if (this.IsDrawBackground && this.IsItemSelected && this.ColumnIsPrimary && !this.ListView.FullRowSelect)
				{
					SizeF sizeF = g.MeasureString(txt, font, r.Width, stringFormatForGdiPlus);
					Rectangle rect = r;
					rect.Width = (int)sizeF.Width + 1;
					using (Brush brush = new SolidBrush(this.ListView.HighlightBackgroundColorOrDefault))
					{
						g.FillRectangle(brush, rect);
					}
				}
				RectangleF layoutRectangle = r;
				g.DrawString(txt, font, this.TextBrush, layoutRectangle, stringFormatForGdiPlus);
			}
		}

		private bool canWrap;

		private Rectangle? cellPadding;

		private StringAlignment? cellVerticalAlignment;

		private ImageList imageList;

		private int spacing = 1;

		private bool useGdiTextRendering = true;

		private object aspect;

		private Rectangle bounds;

		private OLVColumn column;

		private DrawListViewItemEventArgs drawItemEventArgs;

		private DrawListViewSubItemEventArgs eventArgs;

		private Font font;

		private bool isItemSelected;

		private bool isPrinting;

		private OLVListItem listItem;

		private ObjectListView objectListView;

		private object rowObject;

		private OLVListSubItem listSubItem;

		private Brush textBrush;

		private bool useCustomCheckboxImages;
	}
}
