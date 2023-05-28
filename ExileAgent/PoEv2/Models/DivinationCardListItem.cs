using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PoEv2.Models
{
	public sealed class DivinationCardListItem
	{
		public string Name { get; set; }

		public int Quantity { get; set; }

		public double ChaosValue { get; set; }

		public DivinationCardListItem(string name, int quantity, double chaosValue)
		{
			this.Name = name;
			this.Quantity = quantity;
			this.ChaosValue = chaosValue;
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double double_0;
	}
}
