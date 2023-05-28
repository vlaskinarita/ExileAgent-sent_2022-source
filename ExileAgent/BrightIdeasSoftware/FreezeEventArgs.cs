using System;

namespace BrightIdeasSoftware
{
	public sealed class FreezeEventArgs : EventArgs
	{
		public FreezeEventArgs(int freeze)
		{
			this.FreezeLevel = freeze;
		}

		public int FreezeLevel
		{
			get
			{
				return this.freezeLevel;
			}
			set
			{
				this.freezeLevel = value;
			}
		}

		private int freezeLevel;
	}
}
