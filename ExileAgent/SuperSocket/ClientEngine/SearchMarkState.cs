using System;

namespace SuperSocket.ClientEngine
{
	public sealed class SearchMarkState<T> where T : IEquatable<T>
	{
		public SearchMarkState(T[] mark)
		{
			this.Mark = mark;
		}

		public T[] Mark { get; private set; }

		public int Matched { get; set; }
	}
}
