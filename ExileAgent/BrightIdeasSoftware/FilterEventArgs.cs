using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public sealed class FilterEventArgs : EventArgs
	{
		public FilterEventArgs(IEnumerable objects)
		{
			this.Objects = objects;
		}

		public IEnumerable Objects;

		public IEnumerable FilteredObjects;
	}
}
