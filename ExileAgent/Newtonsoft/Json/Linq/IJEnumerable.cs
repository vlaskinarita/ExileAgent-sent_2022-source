using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq
{
	public interface IJEnumerable<out T> : IEnumerable<!0>, IEnumerable where T : JToken
	{
		IJEnumerable<JToken> this[object key]
		{
			get;
		}
	}
}
