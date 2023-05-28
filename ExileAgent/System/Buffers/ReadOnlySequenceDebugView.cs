using System;
using System.Diagnostics;

namespace System.Buffers
{
	internal sealed class ReadOnlySequenceDebugView<T>
	{
		public ReadOnlySequenceDebugView(ReadOnlySequence<T> sequence)
		{
			this._array = ref sequence.ToArray<T>();
			int num = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory in sequence)
			{
				num++;
			}
			ReadOnlyMemory<T>[] array = new ReadOnlyMemory<T>[num];
			int num2 = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory2 in sequence)
			{
				array[num2] = readOnlyMemory2;
				num2++;
			}
			this._segments = new ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments
			{
				Segments = array
			};
		}

		public ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments BufferSegments
		{
			get
			{
				return this._segments;
			}
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

		private readonly ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments _segments;

		[DebuggerDisplay("Count: {Segments.Length}", Name = "Segments")]
		public struct ReadOnlySequenceDebugViewSegments
		{
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ReadOnlyMemory<T>[] Segments { get; set; }
		}
	}
}
