using System;
using System.Diagnostics;

namespace System
{
	internal sealed class SpanDebugView<T>
	{
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		private readonly T[] _array;
	}
}
