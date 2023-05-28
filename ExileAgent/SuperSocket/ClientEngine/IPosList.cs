using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperSocket.ClientEngine
{
	public interface IPosList<T> : IEnumerable<!0>, IEnumerable, ICollection<T>, IList<T>
	{
		int Position { get; set; }
	}
}
