using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperSocket.ClientEngine
{
	public sealed class PosList<T> : List<T>, IEnumerable<!0>, IEnumerable, ICollection<T>, IList<T>, IPosList<T>
	{
		public int Position { get; set; }
	}
}
