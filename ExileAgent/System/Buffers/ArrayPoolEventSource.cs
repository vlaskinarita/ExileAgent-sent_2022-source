using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	[EventSource(Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		[Event(1, Level = EventLevel.Verbose)]
		internal unsafe void BufferRented(int bufferId, int bufferSize, int poolId, int bucketId)
		{
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
			ptr->Size = 4;
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			base.WriteEventCore(1, 4, ptr);
		}

		[Event(2, Level = EventLevel.Informational)]
		internal unsafe void BufferAllocated(int bufferId, int bufferSize, int poolId, int bucketId, ArrayPoolEventSource.BufferAllocatedReason reason)
		{
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
			ptr->Size = 4;
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			ptr[4].Size = 4;
			ptr[4].DataPointer = (IntPtr)((void*)(&reason));
			base.WriteEventCore(2, 5, ptr);
		}

		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		internal enum BufferAllocatedReason
		{
			Pooled,
			OverMaximumSize,
			PoolExhausted
		}
	}
}
