using System;

namespace BrightIdeasSoftware
{
	public sealed class IsHyperlinkEventArgs : EventArgs
	{
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

		public object Model
		{
			get
			{
				return this.model;
			}
			internal set
			{
				this.model = value;
			}
		}

		public OLVColumn Column
		{
			get
			{
				return this.column;
			}
			internal set
			{
				this.column = value;
			}
		}

		public string Text
		{
			get
			{
				return this.text;
			}
			internal set
			{
				this.text = value;
			}
		}

		private ObjectListView listView;

		private object model;

		private OLVColumn column;

		private string text;

		public string Url;
	}
}
