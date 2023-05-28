using System;
using System.Runtime.InteropServices;

namespace BrightIdeasSoftware
{
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("44C09D56-8D3B-419D-A462-7B956B105B47")]
	[ComImport]
	internal interface IOwnerDataCallback
	{
		void GetItemPosition(int i, out NativeMethods.POINT pt);

		void SetItemPosition(int t, NativeMethods.POINT pt);

		void GetItemInGroup(int groupIndex, int n, out int itemIndex);

		void GetItemGroup(int itemIndex, int occurrenceCount, out int groupIndex);

		void GetItemGroupCount(int itemIndex, out int occurrenceCount);

		void OnCacheHint(NativeMethods.LVITEMINDEX i, NativeMethods.LVITEMINDEX j);
	}
}
