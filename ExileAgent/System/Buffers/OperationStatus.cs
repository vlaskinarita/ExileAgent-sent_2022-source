using System;

namespace System.Buffers
{
	public enum OperationStatus
	{
		Done,
		DestinationTooSmall,
		NeedMoreData,
		InvalidData
	}
}
