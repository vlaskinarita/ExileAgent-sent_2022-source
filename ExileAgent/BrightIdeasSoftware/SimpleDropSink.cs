using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public class SimpleDropSink : AbstractDropSink
	{
		public SimpleDropSink()
		{
			this.timer = new Timer();
			this.timer.Interval = 250;
			this.timer.Tick += this.timer_Tick;
			this.CanDropOnItem = true;
			this.FeedbackColor = Color.FromArgb(180, Color.MediumBlue);
			this.billboard = new BillboardOverlay();
		}

		public DropTargetLocation AcceptableLocations
		{
			get
			{
				return this.acceptableLocations;
			}
			set
			{
				this.acceptableLocations = value;
			}
		}

		public bool AcceptExternal
		{
			get
			{
				return this.acceptExternal;
			}
			set
			{
				this.acceptExternal = value;
			}
		}

		public bool AutoScroll
		{
			get
			{
				return this.autoScroll;
			}
			set
			{
				this.autoScroll = value;
			}
		}

		public BillboardOverlay Billboard
		{
			get
			{
				return this.billboard;
			}
			set
			{
				this.billboard = value;
			}
		}

		public bool CanDropBetween
		{
			get
			{
				return (this.AcceptableLocations & DropTargetLocation.BetweenItems) == DropTargetLocation.BetweenItems;
			}
			set
			{
				if (value)
				{
					this.AcceptableLocations |= DropTargetLocation.BetweenItems;
					return;
				}
				this.AcceptableLocations &= ~DropTargetLocation.BetweenItems;
			}
		}

		public bool CanDropOnBackground
		{
			get
			{
				return (this.AcceptableLocations & DropTargetLocation.Background) == DropTargetLocation.Background;
			}
			set
			{
				if (value)
				{
					this.AcceptableLocations |= DropTargetLocation.Background;
					return;
				}
				this.AcceptableLocations &= ~DropTargetLocation.Background;
			}
		}

		public bool CanDropOnItem
		{
			get
			{
				return (this.AcceptableLocations & DropTargetLocation.Item) == DropTargetLocation.Item;
			}
			set
			{
				if (value)
				{
					this.AcceptableLocations |= DropTargetLocation.Item;
					return;
				}
				this.AcceptableLocations &= ~DropTargetLocation.Item;
			}
		}

		public bool CanDropOnSubItem
		{
			get
			{
				return (this.AcceptableLocations & DropTargetLocation.SubItem) == DropTargetLocation.SubItem;
			}
			set
			{
				if (value)
				{
					this.AcceptableLocations |= DropTargetLocation.SubItem;
					return;
				}
				this.AcceptableLocations &= ~DropTargetLocation.SubItem;
			}
		}

		public int DropTargetIndex
		{
			get
			{
				return this.dropTargetIndex;
			}
			set
			{
				if (this.dropTargetIndex != value)
				{
					this.dropTargetIndex = value;
					this.ListView.Invalidate();
				}
			}
		}

		public OLVListItem DropTargetItem
		{
			get
			{
				return this.ListView.GetItem(this.DropTargetIndex);
			}
		}

		public DropTargetLocation DropTargetLocation
		{
			get
			{
				return this.dropTargetLocation;
			}
			set
			{
				if (this.dropTargetLocation != value)
				{
					this.dropTargetLocation = value;
					this.ListView.Invalidate();
				}
			}
		}

		public int DropTargetSubItemIndex
		{
			get
			{
				return this.dropTargetSubItemIndex;
			}
			set
			{
				if (this.dropTargetSubItemIndex != value)
				{
					this.dropTargetSubItemIndex = value;
					this.ListView.Invalidate();
				}
			}
		}

		public Color FeedbackColor
		{
			get
			{
				return this.feedbackColor;
			}
			set
			{
				this.feedbackColor = value;
			}
		}

		public bool IsAltDown
		{
			get
			{
				return (this.KeyState & 32) == 32;
			}
		}

		public bool IsAnyModifierDown
		{
			get
			{
				return (this.KeyState & 44) != 0;
			}
		}

		public bool IsControlDown
		{
			get
			{
				return (this.KeyState & 8) == 8;
			}
		}

		public bool IsLeftMouseButtonDown
		{
			get
			{
				return (this.KeyState & 1) == 1;
			}
		}

		public bool IsMiddleMouseButtonDown
		{
			get
			{
				return (this.KeyState & 16) == 16;
			}
		}

		public bool IsRightMouseButtonDown
		{
			get
			{
				return (this.KeyState & 2) == 2;
			}
		}

		public bool IsShiftDown
		{
			get
			{
				return (this.KeyState & 4) == 4;
			}
		}

		public int KeyState
		{
			get
			{
				return this.keyState;
			}
			set
			{
				this.keyState = value;
			}
		}

		public bool UseDefaultCursors
		{
			get
			{
				return this.useDefaultCursors;
			}
			set
			{
				this.useDefaultCursors = value;
			}
		}

		public event EventHandler<OlvDropEventArgs> CanDrop;

		public event EventHandler<OlvDropEventArgs> Dropped;

		public event EventHandler<ModelDropEventArgs> ModelCanDrop;

		public event EventHandler<ModelDropEventArgs> ModelDropped;

		protected override void Cleanup()
		{
			this.DropTargetLocation = DropTargetLocation.None;
			this.ListView.FullRowSelect = this.originalFullRowSelect;
			this.Billboard.Text = null;
		}

		public override void DrawFeedback(Graphics g, Rectangle bounds)
		{
			g.SmoothingMode = ObjectListView.SmoothingMode;
			DropTargetLocation dropTargetLocation = this.DropTargetLocation;
			switch (dropTargetLocation)
			{
			case DropTargetLocation.Background:
				this.DrawFeedbackBackgroundTarget(g, bounds);
				break;
			case DropTargetLocation.Item:
				this.DrawFeedbackItemTarget(g, bounds);
				break;
			default:
				if (dropTargetLocation != DropTargetLocation.AboveItem)
				{
					if (dropTargetLocation == DropTargetLocation.BelowItem)
					{
						this.DrawFeedbackBelowItemTarget(g, bounds);
					}
				}
				else
				{
					this.DrawFeedbackAboveItemTarget(g, bounds);
				}
				break;
			}
			if (this.Billboard != null)
			{
				this.Billboard.Draw(this.ListView, g, bounds);
			}
		}

		public override void Drop(DragEventArgs args)
		{
			this.dropEventArgs.DragEventArgs = args;
			this.TriggerDroppedEvent(args);
			this.timer.Stop();
			this.Cleanup();
		}

		public override void Enter(DragEventArgs args)
		{
			this.originalFullRowSelect = this.ListView.FullRowSelect;
			this.ListView.FullRowSelect = false;
			this.dropEventArgs = new ModelDropEventArgs();
			this.dropEventArgs.DropSink = this;
			this.dropEventArgs.ListView = this.ListView;
			this.dropEventArgs.DragEventArgs = args;
			this.dropEventArgs.DataObject = args.Data;
			OLVDataObject olvdataObject = args.Data as OLVDataObject;
			if (olvdataObject != null)
			{
				this.dropEventArgs.SourceListView = olvdataObject.ListView;
				this.dropEventArgs.SourceModels = olvdataObject.ModelObjects;
			}
			this.Over(args);
		}

		public override void GiveFeedback(GiveFeedbackEventArgs args)
		{
			args.UseDefaultCursors = this.UseDefaultCursors;
		}

		public override void Over(DragEventArgs args)
		{
			this.dropEventArgs.DragEventArgs = args;
			this.KeyState = args.KeyState;
			Point pt = this.ListView.PointToClient(new Point(args.X, args.Y));
			args.Effect = this.CalculateDropAction(args, pt);
			this.CheckScrolling(pt);
		}

		protected virtual void TriggerDroppedEvent(DragEventArgs args)
		{
			this.dropEventArgs.Handled = false;
			if (this.dropEventArgs.SourceListView != null)
			{
				this.OnModelDropped(this.dropEventArgs);
			}
			if (!this.dropEventArgs.Handled)
			{
				this.OnDropped(this.dropEventArgs);
			}
		}

		protected virtual void OnCanDrop(OlvDropEventArgs args)
		{
			if (this.CanDrop != null)
			{
				this.CanDrop(this, args);
			}
		}

		protected virtual void OnDropped(OlvDropEventArgs args)
		{
			if (this.Dropped != null)
			{
				this.Dropped(this, args);
			}
		}

		protected virtual void OnModelCanDrop(ModelDropEventArgs args)
		{
			if (!this.AcceptExternal && args.SourceListView != null && args.SourceListView != this.ListView)
			{
				args.Effect = DragDropEffects.None;
				args.DropTargetLocation = DropTargetLocation.None;
				args.InfoMessage = SimpleDropSink.getString_0(107314486);
				return;
			}
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

		private void timer_Tick(object sender, EventArgs e)
		{
			this.HandleTimerTick();
		}

		protected virtual void HandleTimerTick()
		{
			if ((this.IsLeftMouseButtonDown && (Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left) || (this.IsMiddleMouseButtonDown && (Control.MouseButtons & MouseButtons.Middle) != MouseButtons.Middle) || (this.IsRightMouseButtonDown && (Control.MouseButtons & MouseButtons.Right) != MouseButtons.Right))
			{
				this.timer.Stop();
				this.Cleanup();
				return;
			}
			Point pt = this.ListView.PointToClient(Cursor.Position);
			Rectangle clientRectangle = this.ListView.ClientRectangle;
			clientRectangle.Inflate(30, 30);
			if (clientRectangle.Contains(pt))
			{
				this.ListView.LowLevelScroll(0, this.scrollAmount);
			}
		}

		protected virtual void CalculateDropTarget(OlvDropEventArgs args, Point pt)
		{
			DropTargetLocation dropTargetLocation = DropTargetLocation.None;
			int num = -1;
			int num2 = 0;
			if (this.CanDropOnBackground)
			{
				dropTargetLocation = DropTargetLocation.Background;
			}
			OlvListViewHitTestInfo olvListViewHitTestInfo = this.ListView.OlvHitTest(pt.X, pt.Y);
			if (olvListViewHitTestInfo.Item != null && this.CanDropOnItem)
			{
				dropTargetLocation = DropTargetLocation.Item;
				num = olvListViewHitTestInfo.Item.Index;
				if (olvListViewHitTestInfo.SubItem != null && this.CanDropOnSubItem)
				{
					num2 = olvListViewHitTestInfo.Item.SubItems.IndexOf(olvListViewHitTestInfo.SubItem);
				}
			}
			if (this.CanDropBetween && this.ListView.GetItemCount() > 0)
			{
				if (dropTargetLocation == DropTargetLocation.Item)
				{
					if (pt.Y - 3 <= olvListViewHitTestInfo.Item.Bounds.Top)
					{
						dropTargetLocation = DropTargetLocation.AboveItem;
					}
					if (pt.Y + 3 >= olvListViewHitTestInfo.Item.Bounds.Bottom)
					{
						dropTargetLocation = DropTargetLocation.BelowItem;
					}
				}
				else
				{
					olvListViewHitTestInfo = this.ListView.OlvHitTest(pt.X, pt.Y + 3);
					if (olvListViewHitTestInfo.Item != null)
					{
						num = olvListViewHitTestInfo.Item.Index;
						dropTargetLocation = DropTargetLocation.AboveItem;
					}
					else
					{
						olvListViewHitTestInfo = this.ListView.OlvHitTest(pt.X, pt.Y - 3);
						if (olvListViewHitTestInfo.Item != null)
						{
							num = olvListViewHitTestInfo.Item.Index;
							dropTargetLocation = DropTargetLocation.BelowItem;
						}
					}
				}
			}
			args.DropTargetLocation = dropTargetLocation;
			args.DropTargetIndex = num;
			args.DropTargetSubItemIndex = num2;
		}

		public virtual DragDropEffects CalculateDropAction(DragEventArgs args, Point pt)
		{
			this.CalculateDropTarget(this.dropEventArgs, pt);
			this.dropEventArgs.MouseLocation = pt;
			this.dropEventArgs.InfoMessage = null;
			this.dropEventArgs.Handled = false;
			if (this.dropEventArgs.SourceListView != null)
			{
				this.dropEventArgs.TargetModel = this.ListView.GetModelObject(this.dropEventArgs.DropTargetIndex);
				this.OnModelCanDrop(this.dropEventArgs);
			}
			if (!this.dropEventArgs.Handled)
			{
				this.OnCanDrop(this.dropEventArgs);
			}
			this.UpdateAfterCanDropEvent(this.dropEventArgs);
			return this.dropEventArgs.Effect;
		}

		public DragDropEffects CalculateStandardDropActionFromKeys()
		{
			if (!this.IsControlDown)
			{
				return DragDropEffects.Move;
			}
			if (this.IsShiftDown)
			{
				return DragDropEffects.Link;
			}
			return DragDropEffects.Copy;
		}

		protected virtual void CheckScrolling(Point pt)
		{
			if (!this.AutoScroll)
			{
				return;
			}
			Rectangle contentRectangle = this.ListView.ContentRectangle;
			int rowHeightEffective = this.ListView.RowHeightEffective;
			int num = rowHeightEffective;
			if (this.ListView.View == View.Tile)
			{
				num /= 2;
			}
			if (pt.Y <= contentRectangle.Top + num)
			{
				this.timer.Interval = ((pt.Y <= contentRectangle.Top + num / 2) ? 100 : 350);
				this.timer.Start();
				this.scrollAmount = -rowHeightEffective;
				return;
			}
			if (pt.Y >= contentRectangle.Bottom - num)
			{
				this.timer.Interval = ((pt.Y >= contentRectangle.Bottom - num / 2) ? 100 : 350);
				this.timer.Start();
				this.scrollAmount = rowHeightEffective;
				return;
			}
			this.timer.Stop();
		}

		protected virtual void UpdateAfterCanDropEvent(OlvDropEventArgs args)
		{
			this.DropTargetIndex = args.DropTargetIndex;
			this.DropTargetLocation = args.DropTargetLocation;
			this.DropTargetSubItemIndex = args.DropTargetSubItemIndex;
			if (this.Billboard != null)
			{
				Point mouseLocation = args.MouseLocation;
				mouseLocation.Offset(5, 5);
				if (this.Billboard.Text != this.dropEventArgs.InfoMessage || this.Billboard.Location != mouseLocation)
				{
					this.Billboard.Text = this.dropEventArgs.InfoMessage;
					this.Billboard.Location = mouseLocation;
					this.ListView.Invalidate();
				}
			}
		}

		protected virtual void DrawFeedbackBackgroundTarget(Graphics g, Rectangle bounds)
		{
			float width = 12f;
			Rectangle rect = bounds;
			rect.Inflate(-6, -6);
			using (Pen pen = new Pen(Color.FromArgb(128, this.FeedbackColor), width))
			{
				using (GraphicsPath roundedRect = this.GetRoundedRect(rect, 30f))
				{
					g.DrawPath(pen, roundedRect);
				}
			}
		}

		protected virtual void DrawFeedbackItemTarget(Graphics g, Rectangle bounds)
		{
			if (this.DropTargetItem == null)
			{
				return;
			}
			Rectangle rect = this.CalculateDropTargetRectangle(this.DropTargetItem, this.DropTargetSubItemIndex);
			rect.Inflate(1, 1);
			float diameter = (float)(rect.Height / 3);
			using (GraphicsPath roundedRect = this.GetRoundedRect(rect, diameter))
			{
				using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(48, this.FeedbackColor)))
				{
					g.FillPath(solidBrush, roundedRect);
				}
				using (Pen pen = new Pen(this.FeedbackColor, 3f))
				{
					g.DrawPath(pen, roundedRect);
				}
			}
		}

		protected virtual void DrawFeedbackAboveItemTarget(Graphics g, Rectangle bounds)
		{
			if (this.DropTargetItem == null)
			{
				return;
			}
			Rectangle rectangle = this.CalculateDropTargetRectangle(this.DropTargetItem, this.DropTargetSubItemIndex);
			this.DrawBetweenLine(g, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
		}

		protected virtual void DrawFeedbackBelowItemTarget(Graphics g, Rectangle bounds)
		{
			if (this.DropTargetItem == null)
			{
				return;
			}
			Rectangle rectangle = this.CalculateDropTargetRectangle(this.DropTargetItem, this.DropTargetSubItemIndex);
			this.DrawBetweenLine(g, rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
		}

		protected GraphicsPath GetRoundedRect(Rectangle rect, float diameter)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			RectangleF rect2 = new RectangleF((float)rect.X, (float)rect.Y, diameter, diameter);
			graphicsPath.AddArc(rect2, 180f, 90f);
			rect2.X = (float)rect.Right - diameter;
			graphicsPath.AddArc(rect2, 270f, 90f);
			rect2.Y = (float)rect.Bottom - diameter;
			graphicsPath.AddArc(rect2, 0f, 90f);
			rect2.X = (float)rect.Left;
			graphicsPath.AddArc(rect2, 90f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		protected virtual Rectangle CalculateDropTargetRectangle(OLVListItem item, int subItem)
		{
			if (subItem > 0)
			{
				return item.SubItems[subItem].Bounds;
			}
			Rectangle result = this.ListView.CalculateCellTextBounds(item, subItem);
			if (item.IndentCount > 0)
			{
				int width = this.ListView.SmallImageSize.Width;
				result.X += width * item.IndentCount;
				result.Width -= width * item.IndentCount;
			}
			return result;
		}

		protected virtual void DrawBetweenLine(Graphics g, int x1, int y1, int x2, int y2)
		{
			using (Brush brush = new SolidBrush(this.FeedbackColor))
			{
				using (GraphicsPath graphicsPath = new GraphicsPath())
				{
					graphicsPath.AddLine(x1, y1 + 5, x1, y1 - 5);
					graphicsPath.AddBezier(x1, y1 - 6, x1 + 3, y1 - 2, x1 + 6, y1 - 1, x1 + 11, y1);
					graphicsPath.AddBezier(x1 + 11, y1, x1 + 6, y1 + 1, x1 + 3, y1 + 2, x1, y1 + 6);
					graphicsPath.CloseFigure();
					g.FillPath(brush, graphicsPath);
				}
				using (GraphicsPath graphicsPath2 = new GraphicsPath())
				{
					graphicsPath2.AddLine(x2, y2 + 6, x2, y2 - 6);
					graphicsPath2.AddBezier(x2, y2 - 7, x2 - 3, y2 - 2, x2 - 6, y2 - 1, x2 - 11, y2);
					graphicsPath2.AddBezier(x2 - 11, y2, x2 - 6, y2 + 1, x2 - 3, y2 + 2, x2, y2 + 7);
					graphicsPath2.CloseFigure();
					g.FillPath(brush, graphicsPath2);
				}
			}
			using (Pen pen = new Pen(this.FeedbackColor, 3f))
			{
				g.DrawLine(pen, x1, y1, x2, y2);
			}
		}

		static SimpleDropSink()
		{
			Strings.CreateGetStringDelegate(typeof(SimpleDropSink));
		}

		private DropTargetLocation acceptableLocations;

		private bool acceptExternal = true;

		private bool autoScroll = true;

		private BillboardOverlay billboard;

		private int dropTargetIndex = -1;

		private DropTargetLocation dropTargetLocation;

		private int dropTargetSubItemIndex = -1;

		private Color feedbackColor;

		private int keyState;

		private bool useDefaultCursors = true;

		private Timer timer;

		private int scrollAmount;

		private bool originalFullRowSelect;

		private ModelDropEventArgs dropEventArgs;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
