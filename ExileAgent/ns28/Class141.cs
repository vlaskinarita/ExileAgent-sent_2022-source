using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ns29;

namespace ns28
{
	internal sealed class Class141
	{
		[JsonProperty("type")]
		public int Type { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		public Class141(Enum11 enum11_0, string string_2)
		{
			this.Type = (int)enum11_0;
			this.Text = string_2.Replace(Environment.NewLine, string.Empty);
		}

		public Class141(Enum11 enum11_0, string string_2, string string_3)
		{
			this.Type = (int)enum11_0;
			this.Text = string_2.Replace(Environment.NewLine, string.Empty);
			this.Username = string_3;
		}

		public bool Equals(object obj)
		{
			bool result;
			if (!(obj is Class141))
			{
				result = false;
			}
			else
			{
				Class141 @class = (Class141)obj;
				result = (this.Text == @class.Text && this.Username == @class.Username);
			}
			return result;
		}

		public int GetHashCode()
		{
			return this.Text.GetHashCode() + this.Username.GetHashCode();
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int int_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;
	}
}
