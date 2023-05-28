using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class ArraySliceFilter : PathFilter
	{
		public int? Start { get; set; }

		public int? End { get; set; }

		public int? Step { get; set; }

		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			ArraySliceFilter.<ExecuteFilter>d__12 <ExecuteFilter>d__ = new ArraySliceFilter.<ExecuteFilter>d__12(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			<ExecuteFilter>d__.<>3__errorWhenNoMatch = errorWhenNoMatch;
			return <ExecuteFilter>d__;
		}

		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
