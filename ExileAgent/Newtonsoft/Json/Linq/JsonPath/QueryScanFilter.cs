using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class QueryScanFilter : PathFilter
	{
		public QueryExpression Expression { get; set; }

		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			QueryScanFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__ = new QueryScanFilter.<ExecuteFilter>d__4(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__root = root;
			<ExecuteFilter>d__.<>3__current = current;
			return <ExecuteFilter>d__;
		}
	}
}
