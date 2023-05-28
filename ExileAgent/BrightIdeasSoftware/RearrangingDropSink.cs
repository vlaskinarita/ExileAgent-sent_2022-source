using System;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class RearrangingDropSink : SimpleDropSink
	{
		public RearrangingDropSink()
		{
			base.CanDropBetween = true;
			base.CanDropOnBackground = true;
			base.CanDropOnItem = false;
		}

		public RearrangingDropSink(bool acceptDropsFromOtherLists) : this()
		{
			base.AcceptExternal = acceptDropsFromOtherLists;
		}

		protected override void OnModelCanDrop(ModelDropEventArgs args)
		{
			base.OnModelCanDrop(args);
			if (args.Handled)
			{
				return;
			}
			args.Effect = DragDropEffects.Move;
			if (!base.AcceptExternal && args.SourceListView != this.ListView)
			{
				args.Effect = DragDropEffects.None;
				args.DropTargetLocation = DropTargetLocation.None;
				args.InfoMessage = RearrangingDropSink.getString_1(107314487);
			}
			if (args.DropTargetLocation == DropTargetLocation.Background && args.SourceListView == this.ListView)
			{
				args.Effect = DragDropEffects.None;
				args.DropTargetLocation = DropTargetLocation.None;
			}
		}

		protected override void OnModelDropped(ModelDropEventArgs args)
		{
			base.OnModelDropped(args);
			if (!args.Handled)
			{
				this.RearrangeModels(args);
			}
		}

		public void RearrangeModels(ModelDropEventArgs args)
		{
			DropTargetLocation dropTargetLocation = args.DropTargetLocation;
			if (dropTargetLocation != DropTargetLocation.Background)
			{
				if (dropTargetLocation != DropTargetLocation.AboveItem)
				{
					if (dropTargetLocation != DropTargetLocation.BelowItem)
					{
						return;
					}
					this.ListView.MoveObjects(args.DropTargetIndex + 1, args.SourceModels);
				}
				else
				{
					this.ListView.MoveObjects(args.DropTargetIndex, args.SourceModels);
				}
			}
			else
			{
				this.ListView.AddObjects(args.SourceModels);
			}
			if (args.SourceListView != this.ListView)
			{
				args.SourceListView.RemoveObjects(args.SourceModels);
			}
		}

		static RearrangingDropSink()
		{
			Strings.CreateGetStringDelegate(typeof(RearrangingDropSink));
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
