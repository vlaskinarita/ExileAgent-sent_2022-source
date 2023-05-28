using System;

namespace BrightIdeasSoftware
{
	public sealed class BeforeSearchingEventArgs : CancellableEventArgs
	{
		public BeforeSearchingEventArgs(string stringToFind, int startSearchFrom)
		{
			this.StringToFind = stringToFind;
			this.StartSearchFrom = startSearchFrom;
		}

		public string StringToFind;

		public int StartSearchFrom;
	}
}
