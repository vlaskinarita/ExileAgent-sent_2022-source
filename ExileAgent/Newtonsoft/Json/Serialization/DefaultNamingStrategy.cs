using System;

namespace Newtonsoft.Json.Serialization
{
	public sealed class DefaultNamingStrategy : NamingStrategy
	{
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
