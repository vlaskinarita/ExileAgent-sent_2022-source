using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ns1;
using ns34;
using ns41;
using ns8;

namespace ns3
{
	internal sealed class Class273
	{
		public Class287 status { get; set; }

		[JsonIgnore]
		public string name { get; set; }

		[JsonIgnore]
		public string type { get; set; }

		[JsonIgnore]
		public Class269 discriminatorType { get; set; }

		[JsonIgnore]
		public Class269 discriminatorName { get; set; }

		public List<Class293> stats { get; set; }

		public Class288 filters { get; set; }

		public string term { get; set; }

		[JsonProperty("name")]
		public object serializedName
		{
			get
			{
				return (this.discriminatorName == null) ? this.name : this.discriminatorName;
			}
		}

		[JsonProperty("type")]
		public object serializedType
		{
			get
			{
				return (this.discriminatorType == null) ? this.type : this.discriminatorType;
			}
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class287 class287_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class269 class269_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class269 class269_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private List<Class293> list_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Class288 class288_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;
	}
}
