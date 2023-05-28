using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	public struct MemoryHandle : IDisposable
	{
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		private unsafe void* _pointer;

		private GCHandle _handle;

		private IPinnable _pinnable;
	}
}
