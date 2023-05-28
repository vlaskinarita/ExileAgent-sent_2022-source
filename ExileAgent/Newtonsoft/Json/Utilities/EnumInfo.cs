using System;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class EnumInfo
	{
		public EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		public readonly bool IsFlags;

		public readonly ulong[] Values;

		public readonly string[] Names;

		public readonly string[] ResolvedNames;
	}
}
