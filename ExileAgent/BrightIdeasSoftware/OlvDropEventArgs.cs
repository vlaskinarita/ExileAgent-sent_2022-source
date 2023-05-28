using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class OlvDropEventArgs : EventArgs
	{
		public DragEventArgs DragEventArgs
		{
			get
			{
				return this.dragEventArgs;
			}
			internal set
			{
				this.dragEventArgs = value;
			}
		}

		public object DataObject
		{
			get
			{
				return this.dataObject;
			}
			internal set
			{
				this.dataObject = value;
			}
		}

		public SimpleDropSink DropSink
		{
			get
			{
				return this.dropSink;
			}
			internal set
			{
				this.dropSink = value;
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
				this.dropTargetIndex = value;
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
				this.dropTargetLocation = value;
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
				this.dropTargetSubItemIndex = value;
			}
		}

		public OLVListItem DropTargetItem
		{
			get
			{
				return this.ListView.GetItem(this.DropTargetIndex);
			}
			set
			{
				if (value == null)
				{
					this.DropTargetIndex = -1;
					return;
				}
				this.DropTargetIndex = value.Index;
			}
		}

		public DragDropEffects Effect
		{
			get
			{
				return this.effect;
			}
			set
			{
				this.effect = value;
			}
		}

		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		public string InfoMessage
		{
			get
			{
				return this.infoMessage;
			}
			set
			{
				this.infoMessage = value;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			internal set
			{
				this.listView = value;
			}
		}

		public Point MouseLocation
		{
			get
			{
				return this.mouseLocation;
			}
			internal set
			{
				this.mouseLocation = value;
			}
		}

		public DragDropEffects StandardDropActionFromKeys
		{
			get
			{
				return this.DropSink.CalculateStandardDropActionFromKeys();
			}
		}

		private DragEventArgs dragEventArgs;

		private object dataObject;

		private SimpleDropSink dropSink;

		private int dropTargetIndex = -1;

		private DropTargetLocation dropTargetLocation;

		private int dropTargetSubItemIndex = -1;

		private DragDropEffects effect;

		private bool handled;

		private string infoMessage;

		private ObjectListView listView;

		private Point mouseLocation;
	}
}
