using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ns12;
using ns17;

namespace ns27
{
	internal sealed class Class300
	{
		public Enum17 CraftingOption { get; private set; }

		public int RequiredPrefixes { get; set; }

		public int HitPrefixes { get; set; }

		public int RequiredSuffixes { get; set; }

		public int HitSuffixes { get; set; }

		public Class300(Class299 class299_0, Enum17 enum17_1)
		{
			this.CraftingOption = enum17_1;
			this.RequiredPrefixes = class299_0.RequiredPrefixes;
			this.RequiredSuffixes = class299_0.RequiredSuffixes;
		}

		public bool Success
		{
			get
			{
				bool result;
				if (this.CraftingOption == Enum17.const_0)
				{
					result = (this.HitPrefixes >= this.RequiredPrefixes && this.HitSuffixes >= this.RequiredSuffixes);
				}
				else
				{
					result = ((this.RequiredPrefixes > 0 && this.HitPrefixes >= this.RequiredPrefixes) || (this.RequiredSuffixes > 0 && this.HitSuffixes >= this.RequiredSuffixes));
				}
				return result;
			}
		}

		public bool PrefixSuccess
		{
			get
			{
				return this.HitPrefixes >= this.RequiredPrefixes;
			}
		}

		public bool SuffixSuccess
		{
			get
			{
				return this.HitSuffixes >= this.RequiredSuffixes;
			}
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Enum17 enum17_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_3;
	}
}
