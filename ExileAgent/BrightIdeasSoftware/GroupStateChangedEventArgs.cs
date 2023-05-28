using System;

namespace BrightIdeasSoftware
{
	public sealed class GroupStateChangedEventArgs : EventArgs
	{
		public GroupStateChangedEventArgs(OLVGroup group, GroupState oldState, GroupState newState)
		{
			this.group = group;
			this.oldState = oldState;
			this.newState = newState;
		}

		public bool Collapsed
		{
			get
			{
				return (this.oldState & GroupState.LVGS_COLLAPSED) != GroupState.LVGS_COLLAPSED && (this.newState & GroupState.LVGS_COLLAPSED) == GroupState.LVGS_COLLAPSED;
			}
		}

		public bool Focused
		{
			get
			{
				return (this.oldState & GroupState.LVGS_FOCUSED) != GroupState.LVGS_FOCUSED && (this.newState & GroupState.LVGS_FOCUSED) == GroupState.LVGS_FOCUSED;
			}
		}

		public bool Selected
		{
			get
			{
				return (this.oldState & GroupState.LVGS_SELECTED) != GroupState.LVGS_SELECTED && (this.newState & GroupState.LVGS_SELECTED) == GroupState.LVGS_SELECTED;
			}
		}

		public bool Uncollapsed
		{
			get
			{
				return (this.oldState & GroupState.LVGS_COLLAPSED) == GroupState.LVGS_COLLAPSED && (this.newState & GroupState.LVGS_COLLAPSED) != GroupState.LVGS_COLLAPSED;
			}
		}

		public bool Unfocused
		{
			get
			{
				return (this.oldState & GroupState.LVGS_FOCUSED) == GroupState.LVGS_FOCUSED && (this.newState & GroupState.LVGS_FOCUSED) != GroupState.LVGS_FOCUSED;
			}
		}

		public bool Unselected
		{
			get
			{
				return (this.oldState & GroupState.LVGS_SELECTED) == GroupState.LVGS_SELECTED && (this.newState & GroupState.LVGS_SELECTED) != GroupState.LVGS_SELECTED;
			}
		}

		public OLVGroup Group
		{
			get
			{
				return this.group;
			}
		}

		public GroupState OldState
		{
			get
			{
				return this.oldState;
			}
		}

		public GroupState NewState
		{
			get
			{
				return this.newState;
			}
		}

		private readonly OLVGroup group;

		private readonly GroupState oldState;

		private readonly GroupState newState;
	}
}
