using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Api
{
	public sealed class ApiItem
	{
		public string Name { get; set; }

		public string Id { get; set; }

		public string Text { get; set; }

		public int Stack { get; set; }

		public string Type { get; set; }

		public int Cost { get; set; }

		public string Class { get; set; }

		public string CardReward { get; set; }

		public bool UniqueReward { get; set; }

		public ApiItem()
		{
		}

		public ApiItem(string name, string id, string text, int stack, string type, string itemClass)
		{
			this.Name = name;
			this.Id = id;
			this.Text = text;
			this.Stack = stack;
			this.Type = type;
			this.Class = itemClass;
		}

		public string ToString()
		{
			return string.Format(ApiItem.getString_0(107439613), this.Text, this.Type, this.Stack);
		}

		public bool IsMap
		{
			get
			{
				return this.Type == ApiItem.getString_0(107402020) || this.Type == ApiItem.getString_0(107372473) || this.Type == ApiItem.getString_0(107481614);
			}
		}

		static ApiItem()
		{
			Strings.CreateGetStringDelegate(typeof(ApiItem));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_4;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_5;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
