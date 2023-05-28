using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BrightIdeasSoftware.Properties;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class HeaderControl : NativeWindow
	{
		public HeaderControl(ObjectListView olv)
		{
			this.ListView = olv;
			base.AssignHandle(NativeMethods.GetHeaderControl(olv));
		}

		public int ColumnIndexUnderCursor
		{
			get
			{
				Point pt = this.ListView.PointToClient(Cursor.Position);
				pt.X += NativeMethods.GetScrollPosition(this.ListView, true);
				return NativeMethods.GetColumnUnderPoint(this.Handle, pt);
			}
		}

		public new IntPtr Handle
		{
			get
			{
				return NativeMethods.GetHeaderControl(this.ListView);
			}
		}

		[Obsolete("Use HeaderStyle.Hot.FontStyle instead")]
		public FontStyle HotFontStyle
		{
			get
			{
				return FontStyle.Regular;
			}
			set
			{
			}
		}

		protected bool IsCursorOverLockedDivider
		{
			get
			{
				Point pt = this.ListView.PointToClient(Cursor.Position);
				pt.X += NativeMethods.GetScrollPosition(this.ListView, true);
				int dividerUnderPoint = NativeMethods.GetDividerUnderPoint(this.Handle, pt);
				if (dividerUnderPoint >= 0 && dividerUnderPoint < this.ListView.Columns.Count)
				{
					OLVColumn column = this.ListView.GetColumn(dividerUnderPoint);
					return column.IsFixedWidth || column.FillsFreeSpace;
				}
				return false;
			}
		}

		protected ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			set
			{
				this.listView = value;
			}
		}

		public int MaximumHeight
		{
			get
			{
				return this.ListView.HeaderMaximumHeight;
			}
		}

		public ToolTipControl ToolTip
		{
			get
			{
				if (this.toolTip == null)
				{
					this.CreateToolTip();
				}
				return this.toolTip;
			}
			protected set
			{
				this.toolTip = value;
			}
		}

		public bool WordWrap
		{
			get
			{
				return this.wordWrap;
			}
			set
			{
				this.wordWrap = value;
			}
		}

		protected int CalculateHeight(Graphics g)
		{
			TextFormatFlags textFormatFlags = this.TextFormatFlags;
			int columnIndexUnderCursor = this.ColumnIndexUnderCursor;
			float num = 0f;
			for (int i = 0; i < this.ListView.Columns.Count; i++)
			{
				OLVColumn column = this.ListView.GetColumn(i);
				num = Math.Max(num, this.CalculateColumnHeight(g, column, textFormatFlags, columnIndexUnderCursor == i, i));
			}
			if (this.MaximumHeight != -1)
			{
				return Math.Min(this.MaximumHeight, (int)num);
			}
			return (int)num;
		}

		private float CalculateColumnHeight(Graphics g, OLVColumn column, TextFormatFlags flags, bool isHot, int i)
		{
			Font font = this.CalculateFont(column, isHot, false);
			if (column.IsHeaderVertical)
			{
				return (float)TextRenderer.MeasureText(g, column.Text, font, new Size(10000, 10000), flags).Width;
			}
			if (!this.WordWrap)
			{
				return (float)(font.Height + 9);
			}
			Rectangle itemRect = this.GetItemRect(i);
			itemRect.Width -= 6;
			if (this.HasNonThemedSortIndicator(column))
			{
				itemRect.Width -= 16;
			}
			if (column.HasHeaderImage)
			{
				itemRect.Width -= column.ImageList.ImageSize.Width + 3;
			}
			if (this.HasFilterIndicator(column))
			{
				itemRect.Width -= this.CalculateFilterIndicatorWidth(itemRect);
			}
			return TextRenderer.MeasureText(g, column.Text, font, new Size(itemRect.Width, 100), flags).Height + 9f;
		}

		protected bool HasSortIndicator(OLVColumn column)
		{
			return this.ListView.ShowSortIndicators && column == this.ListView.LastSortColumn && this.ListView.LastSortOrder != SortOrder.None;
		}

		protected bool HasFilterIndicator(OLVColumn column)
		{
			return this.ListView.UseFiltering && this.ListView.UseFilterIndicator && column.HasFilterIndicator;
		}

		protected bool HasNonThemedSortIndicator(OLVColumn column)
		{
			if (!this.ListView.ShowSortIndicators)
			{
				return false;
			}
			if (VisualStyleRenderer.IsSupported)
			{
				return !VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.SortArrow.SortedUp) && this.HasSortIndicator(column);
			}
			return this.HasSortIndicator(column);
		}

		public Rectangle GetItemRect(int itemIndex)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			NativeMethods.SendMessage_2(this.Handle, 4615, itemIndex, ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		public void Invalidate()
		{
			NativeMethods.InvalidateRect(this.Handle, 0, true);
		}

		protected void CreateToolTip()
		{
			this.ToolTip = new ToolTipControl();
			this.ToolTip.Create(this.Handle);
			this.ToolTip.AddTool(this);
			this.ToolTip.Showing += this.ListView.HeaderToolTipShowingCallback;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 32)
			{
				if (msg != 2)
				{
					if (msg == 32)
					{
						if (!this.HandleSetCursor(ref m))
						{
							return;
						}
					}
				}
				else if (!this.HandleDestroy(ref m))
				{
					return;
				}
			}
			else if (msg != 78)
			{
				if (msg != 512)
				{
					if (msg == 4613)
					{
						if (!this.HandleLayout(ref m))
						{
							return;
						}
					}
				}
				else if (!this.HandleMouseMove(ref m))
				{
					return;
				}
			}
			else if (!this.HandleNotify(ref m))
			{
				return;
			}
			base.WndProc(ref m);
		}

		protected bool HandleSetCursor(ref Message m)
		{
			if (this.IsCursorOverLockedDivider)
			{
				m.Result = (IntPtr)1;
				return false;
			}
			return true;
		}

		protected bool HandleMouseMove(ref Message m)
		{
			int columnIndexUnderCursor = this.ColumnIndexUnderCursor;
			if (columnIndexUnderCursor != this.columnShowingTip && !this.ListView.IsDesignMode)
			{
				this.ToolTip.PopToolTip(this);
				this.columnShowingTip = columnIndexUnderCursor;
			}
			return true;
		}

		protected bool HandleNotify(ref Message m)
		{
			if (m.LParam == IntPtr.Zero)
			{
				return false;
			}
			int code = ((NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR))).code;
			if (code == -530)
			{
				return this.ToolTip.HandleGetDispInfo(ref m);
			}
			switch (code)
			{
			case -522:
				return this.ToolTip.HandlePop(ref m);
			case -521:
				return this.ToolTip.HandleShow(ref m);
			default:
				return false;
			}
		}

		internal bool HandleHeaderCustomDraw(ref Message m)
		{
			NativeMethods.NMCUSTOMDRAW nmcustomdraw = (NativeMethods.NMCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMCUSTOMDRAW));
			int dwDrawStage = nmcustomdraw.dwDrawStage;
			if (dwDrawStage != 1)
			{
				switch (dwDrawStage)
				{
				case 65537:
				{
					int num = nmcustomdraw.dwItemSpec.ToInt32();
					OLVColumn column = this.ListView.GetColumn(num);
					if (this.cachedNeedsCustomDraw)
					{
						using (Graphics graphics = Graphics.FromHdc(nmcustomdraw.hdc))
						{
							graphics.TextRenderingHint = ObjectListView.TextRenderingHint;
							this.CustomDrawHeaderCell(graphics, num, nmcustomdraw.uItemState);
						}
						m.Result = (IntPtr)4;
					}
					else
					{
						bool isPressed = (nmcustomdraw.uItemState & 1) == 1;
						Font font = this.CalculateFont(column, num == this.ColumnIndexUnderCursor, isPressed);
						this.fontHandle = font.ToHfont();
						NativeMethods.SelectObject(nmcustomdraw.hdc, this.fontHandle);
						m.Result = (IntPtr)18;
					}
					return true;
				}
				case 65538:
					if (this.fontHandle != IntPtr.Zero)
					{
						NativeMethods.DeleteObject(this.fontHandle);
						this.fontHandle = IntPtr.Zero;
					}
					break;
				}
				return false;
			}
			this.cachedNeedsCustomDraw = this.NeedsCustomDraw();
			m.Result = (IntPtr)32;
			return true;
		}

		protected bool HandleLayout(ref Message m)
		{
			if (this.ListView.HeaderStyle == ColumnHeaderStyle.None)
			{
				return true;
			}
			NativeMethods.HDLAYOUT hdlayout = (NativeMethods.HDLAYOUT)m.GetLParam(typeof(NativeMethods.HDLAYOUT));
			NativeMethods.RECT structure = (NativeMethods.RECT)Marshal.PtrToStructure(hdlayout.prc, typeof(NativeMethods.RECT));
			NativeMethods.WINDOWPOS structure2 = (NativeMethods.WINDOWPOS)Marshal.PtrToStructure(hdlayout.pwpos, typeof(NativeMethods.WINDOWPOS));
			using (Graphics graphics = this.ListView.CreateGraphics())
			{
				graphics.TextRenderingHint = ObjectListView.TextRenderingHint;
				int num = this.CalculateHeight(graphics);
				structure2.hwnd = this.Handle;
				structure2.hwndInsertAfter = IntPtr.Zero;
				structure2.flags = 32;
				structure2.x = structure.left;
				structure2.y = structure.top;
				structure2.cx = structure.right - structure.left;
				structure2.cy = num;
				structure.top = num;
				Marshal.StructureToPtr<NativeMethods.RECT>(structure, hdlayout.prc, false);
				Marshal.StructureToPtr<NativeMethods.WINDOWPOS>(structure2, hdlayout.pwpos, false);
			}
			this.ListView.BeginInvoke(new MethodInvoker(delegate()
			{
				this.Invalidate();
				this.ListView.Invalidate();
			}));
			return false;
		}

		protected bool HandleDestroy(ref Message m)
		{
			if (this.ToolTip != null)
			{
				this.ToolTip.Showing -= this.ListView.HeaderToolTipShowingCallback;
			}
			return false;
		}

		protected bool NeedsCustomDraw()
		{
			if (this.WordWrap)
			{
				return true;
			}
			if (this.ListView.HeaderUsesThemes)
			{
				return false;
			}
			if (this.NeedsCustomDraw(this.ListView.HeaderFormatStyle))
			{
				return true;
			}
			foreach (object obj in this.ListView.Columns)
			{
				OLVColumn olvcolumn = (OLVColumn)obj;
				if (olvcolumn.HasHeaderImage || !olvcolumn.ShowTextInHeader || olvcolumn.IsHeaderVertical || this.HasFilterIndicator(olvcolumn) || olvcolumn.TextAlign != olvcolumn.HeaderTextAlign || this.NeedsCustomDraw(olvcolumn.HeaderFormatStyle))
				{
					return true;
				}
			}
			return false;
		}

		private bool NeedsCustomDraw(HeaderFormatStyle style)
		{
			return style != null && (this.NeedsCustomDraw(style.Normal) || this.NeedsCustomDraw(style.Hot) || this.NeedsCustomDraw(style.Pressed));
		}

		private bool NeedsCustomDraw(HeaderStateStyle style)
		{
			return style != null && (!style.BackColor.IsEmpty || (style.FrameWidth > 0f && !style.FrameColor.IsEmpty) || (!style.ForeColor.IsEmpty && style.ForeColor != Color.Black));
		}

		protected void CustomDrawHeaderCell(Graphics g, int columnIndex, int itemState)
		{
			Rectangle r = this.GetItemRect(columnIndex);
			OLVColumn column = this.ListView.GetColumn(columnIndex);
			bool flag = (itemState & 1) == 1;
			HeaderStateStyle stateStyle = this.CalculateStyle(column, columnIndex == this.ColumnIndexUnderCursor, flag);
			if (column.HeaderDrawing != null && !column.HeaderDrawing(g, r, columnIndex, column, flag, stateStyle))
			{
				return;
			}
			if (this.ListView.HeaderUsesThemes && VisualStyleRenderer.IsSupported && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Normal))
			{
				this.DrawThemedBackground(g, r, columnIndex, flag);
			}
			else
			{
				this.DrawUnthemedBackground(g, r, columnIndex, flag, stateStyle);
			}
			if (this.HasSortIndicator(column))
			{
				if (this.ListView.HeaderUsesThemes && VisualStyleRenderer.IsSupported && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.SortArrow.SortedUp))
				{
					this.DrawThemedSortIndicator(g, r);
				}
				else
				{
					r = this.DrawUnthemedSortIndicator(g, r);
				}
			}
			if (this.HasFilterIndicator(column))
			{
				r = this.DrawFilterIndicator(g, r);
			}
			this.DrawHeaderImageAndText(g, r, column, stateStyle);
		}

		protected void DrawUnthemedBackground(Graphics g, Rectangle r, int columnIndex, bool isSelected, HeaderStateStyle stateStyle)
		{
			if (stateStyle.BackColor.IsEmpty)
			{
				if (VisualStyleRenderer.IsSupported && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.Item.Normal))
				{
					this.DrawThemedBackground(g, r, columnIndex, isSelected);
				}
				else
				{
					ControlPaint.DrawBorder3D(g, r, Border3DStyle.RaisedInner);
				}
			}
			else
			{
				using (Brush brush = new SolidBrush(stateStyle.BackColor))
				{
					g.FillRectangle(brush, r);
				}
			}
			if (!stateStyle.FrameColor.IsEmpty && stateStyle.FrameWidth > 0f)
			{
				RectangleF rectangleF = r;
				rectangleF.Inflate(-stateStyle.FrameWidth, -stateStyle.FrameWidth);
				g.DrawRectangle(new Pen(stateStyle.FrameColor, stateStyle.FrameWidth), rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
			}
		}

		protected void DrawThemedBackground(Graphics g, Rectangle r, int columnIndex, bool isSelected)
		{
			int part = 1;
			if (columnIndex == 0 && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.ItemLeft.Normal))
			{
				part = 2;
			}
			if (columnIndex == this.ListView.Columns.Count - 1 && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Header.ItemRight.Normal))
			{
				part = 3;
			}
			int state = 1;
			if (isSelected)
			{
				state = 3;
			}
			else if (columnIndex == this.ColumnIndexUnderCursor)
			{
				state = 2;
			}
			VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(HeaderControl.getString_0(107314507), part, state);
			visualStyleRenderer.DrawBackground(g, r);
		}

		protected void DrawThemedSortIndicator(Graphics g, Rectangle r)
		{
			VisualStyleRenderer visualStyleRenderer = null;
			if (this.ListView.LastSortOrder == SortOrder.Ascending)
			{
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Header.SortArrow.SortedUp);
			}
			if (this.ListView.LastSortOrder == SortOrder.Descending)
			{
				visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Header.SortArrow.SortedDown);
			}
			if (visualStyleRenderer != null)
			{
				Size partSize = visualStyleRenderer.GetPartSize(g, ThemeSizeType.True);
				Point point = visualStyleRenderer.GetPoint(PointProperty.Offset);
				if (point.X == 0 && point.Y == 0)
				{
					point = new Point(r.X + r.Width / 2 - partSize.Width / 2, r.Y);
				}
				visualStyleRenderer.DrawBackground(g, new Rectangle(point, partSize));
			}
		}

		protected Rectangle DrawUnthemedSortIndicator(Graphics g, Rectangle r)
		{
			Point point = new Point(r.Right - 16 - 2, r.Top + (r.Height - 16) / 2);
			Point[] array = new Point[]
			{
				point,
				point,
				point
			};
			if (this.ListView.LastSortOrder == SortOrder.Ascending)
			{
				array[0].Offset(2, 10);
				array[1].Offset(8, 3);
				array[2].Offset(14, 10);
			}
			else
			{
				array[0].Offset(2, 4);
				array[1].Offset(8, 10);
				array[2].Offset(14, 4);
			}
			g.FillPolygon(Brushes.SlateGray, array);
			r.Width -= 16;
			return r;
		}

		protected Rectangle DrawFilterIndicator(Graphics g, Rectangle r)
		{
			int num = this.CalculateFilterIndicatorWidth(r);
			if (num <= 0)
			{
				return r;
			}
			Image columnFilterIndicator = Resources.ColumnFilterIndicator;
			int x = r.Right - num;
			int y = r.Top + (r.Height - columnFilterIndicator.Height) / 2;
			g.DrawImageUnscaled(columnFilterIndicator, x, y);
			r.Width -= num;
			return r;
		}

		private int CalculateFilterIndicatorWidth(Rectangle r)
		{
			if (Resources.ColumnFilterIndicator != null && r.Width >= 48)
			{
				return Resources.ColumnFilterIndicator.Width + 1;
			}
			return 0;
		}

		protected void DrawHeaderImageAndText(Graphics g, Rectangle r, OLVColumn column, HeaderStateStyle stateStyle)
		{
			TextFormatFlags textFormatFlags = this.TextFormatFlags;
			textFormatFlags |= TextFormatFlags.VerticalCenter;
			if (column.HeaderTextAlign == HorizontalAlignment.Center)
			{
				textFormatFlags |= TextFormatFlags.HorizontalCenter;
			}
			if (column.HeaderTextAlign == HorizontalAlignment.Right)
			{
				textFormatFlags |= TextFormatFlags.Right;
			}
			Font f = this.ListView.HeaderUsesThemes ? this.ListView.Font : (stateStyle.Font ?? this.ListView.Font);
			Color color = this.ListView.HeaderUsesThemes ? Color.Black : stateStyle.ForeColor;
			if (color.IsEmpty)
			{
				color = Color.Black;
			}
			r.Inflate(-3, 0);
			r.Y -= 2;
			if (column.IsHeaderVertical)
			{
				HeaderControl.DrawVerticalText(g, r, column, f, color);
				return;
			}
			if (column.HasHeaderImage && r.Width > column.ImageList.ImageSize.Width * 2)
			{
				this.DrawImageAndText(g, r, column, textFormatFlags, f, color, 3);
				return;
			}
			this.DrawText(g, r, column, textFormatFlags, f, color);
		}

		private void DrawText(Graphics g, Rectangle r, OLVColumn column, TextFormatFlags flags, Font f, Color color)
		{
			if (column.ShowTextInHeader)
			{
				TextRenderer.DrawText(g, column.Text, f, r, color, Color.Transparent, flags);
			}
		}

		private void DrawImageAndText(Graphics g, Rectangle r, OLVColumn column, TextFormatFlags flags, Font f, Color color, int imageTextGap)
		{
			Rectangle r2 = r;
			r2.X += column.ImageList.ImageSize.Width + imageTextGap;
			r2.Width -= column.ImageList.ImageSize.Width + imageTextGap;
			Size size = Size.Empty;
			if (column.ShowTextInHeader)
			{
				size = TextRenderer.MeasureText(g, column.Text, f, r2.Size, flags);
			}
			int y = r.Top + (r.Height - column.ImageList.ImageSize.Height) / 2;
			int num = r2.Left;
			if (column.HeaderTextAlign == HorizontalAlignment.Center)
			{
				num = r2.Left + (r2.Width - size.Width) / 2;
			}
			if (column.HeaderTextAlign == HorizontalAlignment.Right)
			{
				num = r2.Right - size.Width;
			}
			num -= column.ImageList.ImageSize.Width + imageTextGap;
			column.ImageList.Draw(g, num, y, column.ImageList.Images.IndexOfKey(column.HeaderImageKey));
			this.DrawText(g, r2, column, flags, f, color);
		}

		private static void DrawVerticalText(Graphics g, Rectangle r, OLVColumn column, Font f, Color color)
		{
			try
			{
				Matrix matrix = new Matrix();
				matrix.RotateAt(-90f, new Point(r.X, r.Bottom));
				matrix.Translate(0f, (float)r.Height);
				g.Transform = matrix;
				StringFormat stringFormat = new StringFormat(StringFormatFlags.NoWrap);
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.LineAlignment = column.HeaderTextAlignAsStringAlignment;
				Rectangle r2 = r;
				r2.Width = r.Height;
				r2.Height = r.Width;
				using (Brush brush = new SolidBrush(color))
				{
					g.DrawString(column.Text, f, brush, r2, stringFormat);
				}
			}
			finally
			{
				g.ResetTransform();
			}
		}

		protected HeaderStateStyle CalculateStyle(OLVColumn column, bool isHot, bool isPressed)
		{
			HeaderFormatStyle headerFormatStyle;
			if ((headerFormatStyle = column.HeaderFormatStyle) == null)
			{
				headerFormatStyle = (this.ListView.HeaderFormatStyle ?? new HeaderFormatStyle());
			}
			HeaderFormatStyle headerFormatStyle2 = headerFormatStyle;
			if (this.ListView.IsDesignMode)
			{
				return headerFormatStyle2.Normal;
			}
			if (isPressed)
			{
				return headerFormatStyle2.Pressed;
			}
			if (isHot)
			{
				return headerFormatStyle2.Hot;
			}
			return headerFormatStyle2.Normal;
		}

		protected Font CalculateFont(OLVColumn column, bool isHot, bool isPressed)
		{
			HeaderStateStyle headerStateStyle = this.CalculateStyle(column, isHot, isPressed);
			return headerStateStyle.Font ?? this.ListView.Font;
		}

		protected TextFormatFlags TextFormatFlags
		{
			get
			{
				TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.WordEllipsis | TextFormatFlags.PreserveGraphicsTranslateTransform;
				if (this.WordWrap)
				{
					textFormatFlags |= TextFormatFlags.WordBreak;
				}
				else
				{
					textFormatFlags |= TextFormatFlags.SingleLine;
				}
				if (this.ListView.RightToLeft == RightToLeft.Yes)
				{
					textFormatFlags |= TextFormatFlags.RightToLeft;
				}
				return textFormatFlags;
			}
		}

		static HeaderControl()
		{
			Strings.CreateGetStringDelegate(typeof(HeaderControl));
		}

		private ObjectListView listView;

		private ToolTipControl toolTip;

		private bool wordWrap;

		private int columnShowingTip = -1;

		private bool cachedNeedsCustomDraw;

		private IntPtr fontHandle;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
