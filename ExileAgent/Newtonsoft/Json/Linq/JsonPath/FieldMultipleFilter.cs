using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class FieldMultipleFilter : PathFilter
	{
		public List<string> Names { get; set; }

		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			FieldMultipleFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__ = new FieldMultipleFilter.<ExecuteFilter>d__4(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			<ExecuteFilter>d__.<>3__errorWhenNoMatch = errorWhenNoMatch;
			return <ExecuteFilter>d__;
		}
	}
}
