﻿using System;

namespace WebSocket4Net.Common
{
	internal sealed class ArraySegmentEx<T>
	{
		public ArraySegmentEx(T[] array, int offset, int count)
		{
			this.Array = array;
			this.Offset = offset;
			this.Count = count;
		}

		public T[] Array { get; private set; }

		public int Count { get; private set; }

		public int Offset { get; private set; }

		public int From { get; set; }

		public int To { get; set; }
	}
}
