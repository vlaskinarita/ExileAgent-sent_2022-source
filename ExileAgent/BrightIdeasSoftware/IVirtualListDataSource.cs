using System;
using System.Collections;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public interface IVirtualListDataSource
	{
		object GetNthObject(int n);

		int GetObjectCount();

		int GetObjectIndex(object model);

		void PrepareCache(int first, int last);

		int SearchText(string value, int first, int last, OLVColumn column);

		void Sort(OLVColumn column, SortOrder order);

		void AddObjects(ICollection modelObjects);

		void RemoveObjects(ICollection modelObjects);

		void SetObjects(IEnumerable collection);

		void UpdateObject(int index, object modelObject);
	}
}
