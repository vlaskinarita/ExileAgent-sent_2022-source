using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PoEv2.Models
{
	public sealed class Player
	{
		public string name { get; set; }

		public DateTime PartyTimeout { get; set; }

		public DateTime IgnoredUntil { get; set; }

		public bool PermanentBan { get; set; } = true;

		public DateTime AddedTime { get; set; }

		public Player()
		{
			this.AddedTime = DateTime.Now;
		}

		public Player(string name)
		{
			this.name = name;
		}

		public string ToString()
		{
			return this.name;
		}

		public bool Equals(object obj)
		{
			Player player = obj as Player;
			return player != null && player.name.ToLower() == this.name.ToLower();
		}

		public int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime dateTime_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime dateTime_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime dateTime_2;
	}
}
