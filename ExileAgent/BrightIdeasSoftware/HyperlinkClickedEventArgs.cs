using System;

namespace BrightIdeasSoftware
{
	public sealed class HyperlinkClickedEventArgs : CellEventArgs
	{
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		private string url;
	}
}
