using System;

namespace BrightIdeasSoftware
{
	public sealed class AfterSearchingEventArgs : EventArgs
	{
		public AfterSearchingEventArgs(string stringToFind, int indexSelected)
		{
			this.stringToFind = stringToFind;
			this.indexSelected = indexSelected;
		}

		public string StringToFind
		{
			get
			{
				return this.stringToFind;
			}
		}

		public int IndexSelected
		{
			get
			{
				return this.indexSelected;
			}
		}

		private string stringToFind;

		public bool Handled;

		private int indexSelected;
	}
}
