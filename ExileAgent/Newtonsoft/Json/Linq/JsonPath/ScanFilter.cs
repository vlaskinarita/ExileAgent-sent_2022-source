using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class ScanFilter : PathFilter
	{
		public string Name { get; set; }

		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			ScanFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__ = new ScanFilter.<ExecuteFilter>d__4(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			return <ExecuteFilter>d__;
		}
	}
}
