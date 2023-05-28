using System;
using System.Diagnostics;

namespace System
{
	internal sealed class MemoryDebugView<T>
	{
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._memory.ToArray();
			}
		}

		private readonly ReadOnlyMemory<T> _memory;
	}
}
