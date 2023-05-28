using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public interface IDropSink
	{
		ObjectListView ListView { get; set; }

		void DrawFeedback(Graphics g, Rectangle bounds);

		void Drop(DragEventArgs args);

		void Enter(DragEventArgs args);

		void GiveFeedback(GiveFeedbackEventArgs args);

		void Leave();

		void Over(DragEventArgs args);

		void QueryContinue(QueryContinueDragEventArgs args);
	}
}
