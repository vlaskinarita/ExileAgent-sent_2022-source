using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ns23
{
	internal sealed class Class239
	{
		[JsonProperty("expire")]
		public string ExpirationDate { get; set; }

		[JsonProperty("flipping")]
		public bool UsingFlipping { get; set; }

		[JsonProperty("token")]
		public string Token { get; set; }

		[JsonProperty("error")]
		public bool Error { get; set; }

		[JsonProperty("web_error")]
		public string WebErrorMessage { get; set; }

		[JsonProperty("message")]
		public string ErrorMessage { get; set; }

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool bool_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;
	}
}
