using System;

namespace BrightIdeasSoftware
{
	public sealed class ComboBoxItem
	{
		public ComboBoxItem(object key, string description)
		{
			this.key = key;
			this.description = description;
		}

		public object Key
		{
			get
			{
				return this.key;
			}
		}

		public override string ToString()
		{
			return this.description;
		}

		private readonly string description;

		private readonly object key;
	}
}
