using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class AbstractDropSink : IDropSink
	{
		public virtual ObjectListView ListView
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

		public virtual void DrawFeedback(Graphics g, Rectangle bounds)
		{
		}

		public virtual void Drop(DragEventArgs args)
		{
			this.Cleanup();
		}

		public virtual void Enter(DragEventArgs args)
		{
		}

		public virtual void Leave()
		{
			this.Cleanup();
		}

		public virtual void Over(DragEventArgs args)
		{
		}

		public virtual void GiveFeedback(GiveFeedbackEventArgs args)
		{
			args.UseDefaultCursors = true;
		}

		public virtual void QueryContinue(QueryContinueDragEventArgs args)
		{
		}

		protected virtual void Cleanup()
		{
		}

		private ObjectListView listView;
	}
}
