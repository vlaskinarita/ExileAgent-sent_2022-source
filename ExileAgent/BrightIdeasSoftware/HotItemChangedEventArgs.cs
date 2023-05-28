using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class HotItemChangedEventArgs : EventArgs
	{
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		public HitTestLocation HotCellHitLocation
		{
			get
			{
				return this.newHotCellHitLocation;
			}
			internal set
			{
				this.newHotCellHitLocation = value;
			}
		}

		public HitTestLocationEx HotCellHitLocationEx
		{
			get
			{
				return this.hotCellHitLocationEx;
			}
			internal set
			{
				this.hotCellHitLocationEx = value;
			}
		}

		public int HotColumnIndex
		{
			get
			{
				return this.newHotColumnIndex;
			}
			internal set
			{
				this.newHotColumnIndex = value;
			}
		}

		public int HotRowIndex
		{
			get
			{
				return this.newHotRowIndex;
			}
			internal set
			{
				this.newHotRowIndex = value;
			}
		}

		public OLVGroup HotGroup
		{
			get
			{
				return this.hotGroup;
			}
			internal set
			{
				this.hotGroup = value;
			}
		}

		public HitTestLocation OldHotCellHitLocation
		{
			get
			{
				return this.oldHotCellHitLocation;
			}
			internal set
			{
				this.oldHotCellHitLocation = value;
			}
		}

		public HitTestLocationEx OldHotCellHitLocationEx
		{
			get
			{
				return this.oldHotCellHitLocationEx;
			}
			internal set
			{
				this.oldHotCellHitLocationEx = value;
			}
		}

		public int OldHotColumnIndex
		{
			get
			{
				return this.oldHotColumnIndex;
			}
			internal set
			{
				this.oldHotColumnIndex = value;
			}
		}

		public int OldHotRowIndex
		{
			get
			{
				return this.oldHotRowIndex;
			}
			internal set
			{
				this.oldHotRowIndex = value;
			}
		}

		public OLVGroup OldHotGroup
		{
			get
			{
				return this.oldHotGroup;
			}
			internal set
			{
				this.oldHotGroup = value;
			}
		}

		public override string ToString()
		{
			return string.Format(HotItemChangedEventArgs.getString_0(107315045), new object[]
			{
				this.newHotCellHitLocation,
				this.hotCellHitLocationEx,
				this.newHotColumnIndex,
				this.newHotRowIndex,
				this.hotGroup
			});
		}

		static HotItemChangedEventArgs()
		{
			Strings.CreateGetStringDelegate(typeof(HotItemChangedEventArgs));
		}

		private bool handled;

		private HitTestLocation newHotCellHitLocation;

		private HitTestLocationEx hotCellHitLocationEx;

		private int newHotColumnIndex;

		private int newHotRowIndex;

		private OLVGroup hotGroup;

		private HitTestLocation oldHotCellHitLocation;

		private HitTestLocationEx oldHotCellHitLocationEx;

		private int oldHotColumnIndex;

		private int oldHotRowIndex;

		private OLVGroup oldHotGroup;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
