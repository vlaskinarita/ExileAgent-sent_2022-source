using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class RootFilter : PathFilter
	{
		private RootFilter()
		{
		}

		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			return new JToken[]
			{
				root
			};
		}

		public static readonly RootFilter Instance = new RootFilter();
	}
}
