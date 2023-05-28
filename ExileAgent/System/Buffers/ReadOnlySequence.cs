using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ns20;

namespace System.Buffers
{
	[IsReadOnlyAttribute1]
	[DebuggerTypeProxy(typeof(ReadOnlySequenceDebugView<>))]
	[DebuggerDisplay("{ToString(),raw}")]
	public struct ReadOnlySequence<T>
	{
		public long Length
		{
			get
			{
				return this.GetLength();
			}
		}

		public bool IsEmpty
		{
			get
			{
				return this.Length == 0L;
			}
		}

		public bool IsSingleSegment
		{
			get
			{
				return this._sequenceStart.GetObject() == this._sequenceEnd.GetObject();
			}
		}

		public ReadOnlyMemory<T> First
		{
			get
			{
				return this.GetFirstBuffer();
			}
		}

		public SequencePosition Start
		{
			get
			{
				return this._sequenceStart;
			}
		}

		public SequencePosition End
		{
			get
			{
				return this._sequenceEnd;
			}
		}

		private ReadOnlySequence(object startSegment, int startIndexAndFlags, object endSegment, int endIndexAndFlags)
		{
			this._sequenceStart = new SequencePosition(startSegment, startIndexAndFlags);
			this._sequenceEnd = new SequencePosition(endSegment, endIndexAndFlags);
		}

		public ReadOnlySequence(ReadOnlySequenceSegment<T> startSegment, int startIndex, ReadOnlySequenceSegment<T> endSegment, int endIndex)
		{
			if (startSegment == null || endSegment == null || (startSegment != endSegment && startSegment.RunningIndex > endSegment.RunningIndex) || startSegment.Memory.Length < startIndex || endSegment.Memory.Length < endIndex || (startSegment == endSegment && endIndex < startIndex))
			{
				ThrowHelper.ThrowArgumentValidationException<T>(startSegment, startIndex, endSegment);
			}
			this._sequenceStart = new SequencePosition(startSegment, ReadOnlySequence.SegmentToSequenceStart(startIndex));
			this._sequenceEnd = new SequencePosition(endSegment, ReadOnlySequence.SegmentToSequenceEnd(endIndex));
		}

		public ReadOnlySequence(T[] array)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(0));
			this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(array.Length));
		}

		public ReadOnlySequence(T[] array, int start, int length)
		{
			if (array == null || start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentValidationException(array, start);
			}
			this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(start));
			this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(start + length));
		}

		public ReadOnlySequence(ReadOnlyMemory<T> memory)
		{
			MemoryManager<T> @object;
			int startIndex;
			int num;
			if (MemoryMarshal.TryGetMemoryManager<T, MemoryManager<T>>(memory, out @object, out startIndex, out num))
			{
				this._sequenceStart = new SequencePosition(@object, ReadOnlySequence.MemoryManagerToSequenceStart(startIndex));
				this._sequenceEnd = new SequencePosition(@object, ReadOnlySequence.MemoryManagerToSequenceEnd(num));
				return;
			}
			ArraySegment<T> arraySegment;
			if (MemoryMarshal.TryGetArray<T>(memory, out arraySegment))
			{
				T[] array = arraySegment.Array;
				int offset = arraySegment.Offset;
				this._sequenceStart = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceStart(offset));
				this._sequenceEnd = new SequencePosition(array, ReadOnlySequence.ArrayToSequenceEnd(offset + arraySegment.Count));
				return;
			}
			if (typeof(T) == typeof(char))
			{
				string object2;
				int num2;
				if (!MemoryMarshal.TryGetString((ReadOnlyMemory<char>)memory, out object2, out num2, out num))
				{
					ThrowHelper.ThrowInvalidOperationException();
				}
				this._sequenceStart = new SequencePosition(object2, ReadOnlySequence.StringToSequenceStart(num2));
				this._sequenceEnd = new SequencePosition(object2, ReadOnlySequence.StringToSequenceEnd(num2 + num));
				return;
			}
			ThrowHelper.ThrowInvalidOperationException();
			this._sequenceStart = default(SequencePosition);
			this._sequenceEnd = default(SequencePosition);
		}

		public ReadOnlySequence<T> Slice(long start, long length)
		{
			if (start < 0L || length < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			int num = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			SequencePosition sequencePosition;
			SequencePosition endPosition;
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num2 = readOnlySequenceSegment.Memory.Length - num;
				if ((long)num2 > start)
				{
					num += (int)start;
					sequencePosition = new SequencePosition(@object, num);
					endPosition = ReadOnlySequence<T>.GetEndPosition(readOnlySequenceSegment, @object, num, object2, index, length);
				}
				else
				{
					if (num2 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object2, index, start - (long)num2, ExceptionArgument.start);
					int index2 = ReadOnlySequence<T>.GetIndex(ref sequencePosition);
					object object3 = sequencePosition.GetObject();
					if (object3 != object2)
					{
						endPosition = ReadOnlySequence<T>.GetEndPosition((ReadOnlySequenceSegment<T>)object3, object3, index2, object2, index, length);
					}
					else
					{
						if ((long)(index - index2) < length)
						{
							ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
						}
						endPosition = new SequencePosition(object3, index2 + (int)length);
					}
				}
			}
			else
			{
				if ((long)(index - num) < start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
				num += (int)start;
				sequencePosition = new SequencePosition(@object, num);
				if ((long)(index - num) < length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				endPosition = new SequencePosition(@object, num + (int)length);
			}
			return this.SliceImpl(ref sequencePosition, ref endPosition);
		}

		public ReadOnlySequence<T> Slice(long start, SequencePosition end)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref end);
			object @object = end.GetObject();
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			object object2 = this._sequenceStart.GetObject();
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object object3 = this._sequenceEnd.GetObject();
			if (object2 == object3)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if ((ulong)(index - index2) < (ulong)start)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(-1L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)object2;
				ulong num = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index2));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index));
				if (!ReadOnlySequence<T>.InRange(num2, num, (ulong)(((ReadOnlySequenceSegment<T>)object3).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num + (ulong)start > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				int num3 = readOnlySequenceSegment.Memory.Length - (int)index2;
				if ((long)num3 <= start)
				{
					if (num3 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, @object, (int)index, start - (long)num3, ExceptionArgument.start);
					return this.SliceImpl(ref sequencePosition, ref end);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(object2, (int)(index2 + (uint)((int)start)));
			return this.SliceImpl(ref sequencePosition2, ref end);
		}

		public ReadOnlySequence<T> Slice(SequencePosition start, long length)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref start);
			object @object = start.GetObject();
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			object object2 = this._sequenceStart.GetObject();
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object object3 = this._sequenceEnd.GetObject();
			if (object2 == object3)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if ((ulong)(index3 - index) < (ulong)length)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
			}
			else
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				ulong num = (ulong)(readOnlySequenceSegment.RunningIndex + (long)((ulong)index));
				ulong start2 = (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index2));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)object3).RunningIndex + (long)((ulong)index3));
				if (!ReadOnlySequence<T>.InRange(num, start2, num2))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (length < 0L)
				{
					ThrowHelper.ThrowStartOrEndArgumentValidationException(0L);
				}
				if (num + (ulong)length > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
				}
				int num3 = readOnlySequenceSegment.Memory.Length - (int)index;
				if ((long)num3 < length)
				{
					if (num3 < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					SequencePosition sequencePosition = ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object3, (int)index3, length - (long)num3, ExceptionArgument.length);
					return this.SliceImpl(ref start, ref sequencePosition);
				}
			}
			SequencePosition sequencePosition2 = new SequencePosition(@object, (int)(index + (uint)((int)length)));
			return this.SliceImpl(ref start, ref sequencePosition2);
		}

		public ReadOnlySequence<T> Slice(int start, int length)
		{
			return this.Slice((long)start, (long)length);
		}

		public ReadOnlySequence<T> Slice(int start, SequencePosition end)
		{
			return this.Slice((long)start, end);
		}

		public ReadOnlySequence<T> Slice(SequencePosition start, int length)
		{
			return this.Slice(start, (long)length);
		}

		public ReadOnlySequence<T> Slice(SequencePosition start, SequencePosition end)
		{
			this.BoundsCheck((uint)ReadOnlySequence<T>.GetIndex(ref start), start.GetObject(), (uint)ReadOnlySequence<T>.GetIndex(ref end), end.GetObject());
			return this.SliceImpl(ref start, ref end);
		}

		public ReadOnlySequence<T> Slice(SequencePosition start)
		{
			this.BoundsCheck(ref start);
			return this.SliceImpl(ref start, ref this._sequenceEnd);
		}

		public ReadOnlySequence<T> Slice(long start)
		{
			if (start < 0L)
			{
				ThrowHelper.ThrowStartOrEndArgumentValidationException(start);
			}
			if (start == 0L)
			{
				return this;
			}
			SequencePosition sequencePosition = this.Seek(ref this._sequenceStart, ref this._sequenceEnd, start, ExceptionArgument.start);
			return this.SliceImpl(ref sequencePosition, ref this._sequenceEnd);
		}

		public unsafe override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				ReadOnlySequence<T> readOnlySequence = this;
				ReadOnlySequence<char> sequence = *Unsafe.As<ReadOnlySequence<T>, ReadOnlySequence<char>>(ref readOnlySequence);
				string text;
				int startIndex;
				int length;
				if (SequenceMarshal.TryGetString(sequence, out text, out startIndex, out length))
				{
					return text.Substring(startIndex, length);
				}
				if (this.Length < 2147483647L)
				{
					return new string(ref sequence.ToArray<char>());
				}
			}
			return string.Format(Class401.smethod_0(107133478), typeof(T).Name, this.Length);
		}

		public ReadOnlySequence<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySequence<T>.Enumerator(ref this);
		}

		public SequencePosition GetPosition(long offset)
		{
			return this.GetPosition(offset, this._sequenceStart);
		}

		public SequencePosition GetPosition(long offset, SequencePosition origin)
		{
			if (offset < 0L)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_OffsetOutOfRange();
			}
			return this.Seek(ref origin, ref this._sequenceEnd, offset, ExceptionArgument.offset);
		}

		public bool TryGet(ref SequencePosition position, out ReadOnlyMemory<T> memory, bool advance = true)
		{
			SequencePosition sequencePosition;
			bool result = this.TryGetBuffer(ref position, out memory, out sequencePosition);
			if (advance)
			{
				position = sequencePosition;
			}
			return result;
		}

		internal bool TryGetBuffer([IsReadOnlyAttribute1] [In] ref SequencePosition position, out ReadOnlyMemory<T> memory, out SequencePosition next)
		{
			object @object = position.GetObject();
			next = default(SequencePosition);
			if (@object == null)
			{
				memory = default(ReadOnlyMemory<T>);
				return false;
			}
			ReadOnlySequence<T>.SequenceType sequenceType = this.GetSequenceType();
			object object2 = this._sequenceEnd.GetObject();
			int index = ReadOnlySequence<T>.GetIndex(ref position);
			int index2 = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			if (sequenceType == ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				if (readOnlySequenceSegment != object2)
				{
					ReadOnlySequenceSegment<T> next2 = readOnlySequenceSegment.Next;
					if (next2 == null)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					next = new SequencePosition(next2, 0);
					memory = readOnlySequenceSegment.Memory.Slice(index);
				}
				else
				{
					memory = readOnlySequenceSegment.Memory.Slice(index, index2 - index);
				}
			}
			else
			{
				if (@object != object2)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (sequenceType == ReadOnlySequence<T>.SequenceType.Array)
				{
					memory = new ReadOnlyMemory<T>((T[])@object, index, index2 - index);
				}
				else if (typeof(T) == typeof(char) && sequenceType == ReadOnlySequence<T>.SequenceType.String)
				{
					memory = (ReadOnlyMemory<T>)((string)@object).AsMemory(index, index2 - index);
				}
				else
				{
					memory = ((MemoryManager<T>)@object).Memory.Slice(index, index2 - index);
				}
			}
			return true;
		}

		private ReadOnlyMemory<T> GetFirstBuffer()
		{
			object @object = this._sequenceStart.GetObject();
			if (@object == null)
			{
				return default(ReadOnlyMemory<T>);
			}
			int num = this._sequenceStart.GetInteger();
			int integer = this._sequenceEnd.GetInteger();
			bool flag = @object != this._sequenceEnd.GetObject();
			if (num >= 0)
			{
				if (integer < 0)
				{
					if (flag)
					{
						ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
					}
					return new ReadOnlyMemory<T>((T[])@object, num, (integer & int.MaxValue) - num);
				}
				ReadOnlyMemory<T> memory = ((ReadOnlySequenceSegment<T>)@object).Memory;
				if (flag)
				{
					return memory.Slice(num);
				}
				return memory.Slice(num, integer - num);
			}
			else
			{
				if (flag)
				{
					ThrowHelper.ThrowInvalidOperationException_EndPositionNotReached();
				}
				if (typeof(T) == typeof(char) && integer < 0)
				{
					return (ReadOnlyMemory<T>)((string)@object).AsMemory(num & int.MaxValue, integer - num);
				}
				num &= int.MaxValue;
				return ((MemoryManager<T>)@object).Memory.Slice(num, integer - num);
			}
		}

		private SequencePosition Seek([IsReadOnlyAttribute1] [In] ref SequencePosition start, [IsReadOnlyAttribute1] [In] ref SequencePosition end, long offset, ExceptionArgument argument)
		{
			int index = ReadOnlySequence<T>.GetIndex(ref start);
			int index2 = ReadOnlySequence<T>.GetIndex(ref end);
			object @object = start.GetObject();
			object object2 = end.GetObject();
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				int num = readOnlySequenceSegment.Memory.Length - index;
				if ((long)num <= offset)
				{
					if (num < 0)
					{
						ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					}
					return ReadOnlySequence<T>.SeekMultiSegment(readOnlySequenceSegment.Next, object2, index2, offset - (long)num, argument);
				}
			}
			else if ((long)(index2 - index) < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
			}
			return new SequencePosition(@object, index + (int)offset);
		}

		private static SequencePosition SeekMultiSegment(ReadOnlySequenceSegment<T> currentSegment, object endObject, int endIndex, long offset, ExceptionArgument argument)
		{
			while (currentSegment != null)
			{
				if (currentSegment == endObject)
				{
					break;
				}
				int length = currentSegment.Memory.Length;
				if ((long)length > offset)
				{
					IL_3D:
					return new SequencePosition(currentSegment, (int)offset);
				}
				offset -= (long)length;
				currentSegment = currentSegment.Next;
			}
			if (currentSegment == null || (long)endIndex < offset)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(argument);
				goto IL_3D;
			}
			goto IL_3D;
		}

		private void BoundsCheck([IsReadOnlyAttribute1] [In] ref SequencePosition position)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref position);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			uint index3 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object == object2)
			{
				if (!ReadOnlySequence<T>.InRange(index, index2, index3))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong start = (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index2));
				if (!ReadOnlySequence<T>.InRange((ulong)(((ReadOnlySequenceSegment<T>)position.GetObject()).RunningIndex + (long)((ulong)index)), start, (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index3))))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		private void BoundsCheck(uint sliceStartIndex, object sliceStartObject, uint sliceEndIndex, object sliceEndObject)
		{
			uint index = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			uint index2 = (uint)ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object == object2)
			{
				if (sliceStartObject != sliceEndObject || sliceStartObject != @object || sliceStartIndex > sliceEndIndex || sliceStartIndex < index || sliceEndIndex > index2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
					return;
				}
			}
			else
			{
				ulong num = (ulong)(((ReadOnlySequenceSegment<T>)sliceStartObject).RunningIndex + (long)((ulong)sliceStartIndex));
				ulong num2 = (ulong)(((ReadOnlySequenceSegment<T>)sliceEndObject).RunningIndex + (long)((ulong)sliceEndIndex));
				if (num > num2)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
				if (num < (ulong)(((ReadOnlySequenceSegment<T>)@object).RunningIndex + (long)((ulong)index)) || num2 > (ulong)(((ReadOnlySequenceSegment<T>)object2).RunningIndex + (long)((ulong)index2)))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
				}
			}
		}

		private static SequencePosition GetEndPosition(ReadOnlySequenceSegment<T> startSegment, object startObject, int startIndex, object endObject, int endIndex, long length)
		{
			int num = startSegment.Memory.Length - startIndex;
			if ((long)num > length)
			{
				return new SequencePosition(startObject, startIndex + (int)length);
			}
			if (num < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PositionOutOfRange();
			}
			return ReadOnlySequence<T>.SeekMultiSegment(startSegment.Next, endObject, endIndex, length - (long)num, ExceptionArgument.length);
		}

		private ReadOnlySequence<T>.SequenceType GetSequenceType()
		{
			return (ReadOnlySequence<T>.SequenceType)(-(ReadOnlySequence<T>.SequenceType)(2 * (this._sequenceStart.GetInteger() >> 31) + (this._sequenceEnd.GetInteger() >> 31)));
		}

		private static int GetIndex([IsReadOnlyAttribute1] [In] ref SequencePosition position)
		{
			return position.GetInteger() & int.MaxValue;
		}

		private ReadOnlySequence<T> SliceImpl([IsReadOnlyAttribute1] [In] ref SequencePosition start, [IsReadOnlyAttribute1] [In] ref SequencePosition end)
		{
			return new ReadOnlySequence<T>(start.GetObject(), ReadOnlySequence<T>.GetIndex(ref start) | (this._sequenceStart.GetInteger() & int.MinValue), end.GetObject(), ReadOnlySequence<T>.GetIndex(ref end) | (this._sequenceEnd.GetInteger() & int.MinValue));
		}

		private long GetLength()
		{
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			int index2 = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
			object @object = this._sequenceStart.GetObject();
			object object2 = this._sequenceEnd.GetObject();
			if (@object != object2)
			{
				ReadOnlySequenceSegment<T> readOnlySequenceSegment = (ReadOnlySequenceSegment<T>)@object;
				ReadOnlySequenceSegment<T> readOnlySequenceSegment2 = (ReadOnlySequenceSegment<T>)object2;
				return readOnlySequenceSegment2.RunningIndex + (long)index2 - (readOnlySequenceSegment.RunningIndex + (long)index);
			}
			return (long)(index2 - index);
		}

		internal bool TryGetReadOnlySequenceSegment(out ReadOnlySequenceSegment<T> startSegment, out int startIndex, out ReadOnlySequenceSegment<T> endSegment, out int endIndex)
		{
			object @object = this._sequenceStart.GetObject();
			if (@object != null && this.GetSequenceType() == ReadOnlySequence<T>.SequenceType.MultiSegment)
			{
				startSegment = (ReadOnlySequenceSegment<T>)@object;
				startIndex = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
				endSegment = (ReadOnlySequenceSegment<T>)this._sequenceEnd.GetObject();
				endIndex = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd);
				return true;
			}
			startSegment = null;
			startIndex = 0;
			endSegment = null;
			endIndex = 0;
			return false;
		}

		internal bool TryGetArray(out ArraySegment<T> segment)
		{
			if (this.GetSequenceType() != ReadOnlySequence<T>.SequenceType.Array)
			{
				segment = default(ArraySegment<T>);
				return false;
			}
			int index = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
			segment = new ArraySegment<T>((T[])this._sequenceStart.GetObject(), index, ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd) - index);
			return true;
		}

		internal bool TryGetString(out string text, out int start, out int length)
		{
			if (!(typeof(T) != typeof(char)))
			{
				if (this.GetSequenceType() == ReadOnlySequence<T>.SequenceType.String)
				{
					start = ReadOnlySequence<T>.GetIndex(ref this._sequenceStart);
					length = ReadOnlySequence<T>.GetIndex(ref this._sequenceEnd) - start;
					text = (string)this._sequenceStart.GetObject();
					return true;
				}
			}
			start = 0;
			length = 0;
			text = null;
			return false;
		}

		private static bool InRange(uint value, uint start, uint end)
		{
			return value - start <= end - start;
		}

		private static bool InRange(ulong value, ulong start, ulong end)
		{
			return value - start <= end - start;
		}

		private readonly SequencePosition _sequenceStart;

		private readonly SequencePosition _sequenceEnd;

		public static readonly ReadOnlySequence<T> Empty = new ReadOnlySequence<T>(SpanHelpers.PerTypeValues<T>.EmptyArray);

		public struct Enumerator
		{
			public Enumerator([IsReadOnlyAttribute1] [In] ref ReadOnlySequence<T> sequence)
			{
				this._currentMemory = default(ReadOnlyMemory<T>);
				this._next = sequence.Start;
				this._sequence = sequence;
			}

			public ReadOnlyMemory<T> Current
			{
				get
				{
					return this._currentMemory;
				}
			}

			public bool MoveNext()
			{
				return this._next.GetObject() != null && this._sequence.TryGet(ref this._next, out this._currentMemory, true);
			}

			private readonly ReadOnlySequence<T> _sequence;

			private SequencePosition _next;

			private ReadOnlyMemory<T> _currentMemory;
		}

		private enum SequenceType
		{
			MultiSegment,
			Array,
			MemoryManager,
			String,
			Empty
		}
	}
}
