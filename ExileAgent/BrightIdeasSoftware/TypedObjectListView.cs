using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class TypedObjectListView<T> where T : class
	{
		public TypedObjectListView(ObjectListView olv)
		{
			this.olv = olv;
		}

		public T CheckedObject
		{
			get
			{
				return (T)((object)this.olv.CheckedObject);
			}
		}

		public IList<T> CheckedObjects
		{
			get
			{
				IList checkedObjects = this.olv.CheckedObjects;
				List<T> list = new List<T>(checkedObjects.Count);
				foreach (object obj in checkedObjects)
				{
					list.Add((T)((object)obj));
				}
				return list;
			}
			set
			{
				this.olv.CheckedObjects = (IList)value;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.olv;
			}
			set
			{
				this.olv = value;
			}
		}

		public IList<T> Objects
		{
			get
			{
				List<T> list = new List<T>(this.olv.GetItemCount());
				for (int i = 0; i < this.olv.GetItemCount(); i++)
				{
					list.Add(this.GetModelObject(i));
				}
				return list;
			}
			set
			{
				this.olv.SetObjects(value);
			}
		}

		public T SelectedObject
		{
			get
			{
				return (T)((object)this.olv.SelectedObject);
			}
			set
			{
				this.olv.SelectedObject = value;
			}
		}

		public IList<T> SelectedObjects
		{
			get
			{
				List<T> list = new List<T>(this.olv.SelectedIndices.Count);
				foreach (object obj in this.olv.SelectedIndices)
				{
					int index = (int)obj;
					list.Add((T)((object)this.olv.GetModelObject(index)));
				}
				return list;
			}
			set
			{
				this.olv.SelectedObjects = (IList)value;
			}
		}

		public TypedColumn<T> GetColumn(int i)
		{
			return new TypedColumn<T>(this.olv.GetColumn(i));
		}

		public TypedColumn<T> GetColumn(string name)
		{
			return new TypedColumn<T>(this.olv.GetColumn(name));
		}

		public T GetModelObject(int index)
		{
			return (T)((object)this.olv.GetModelObject(index));
		}

		public TypedObjectListView<T>.TypedCheckStateGetterDelegate CheckStateGetter
		{
			get
			{
				return this.checkStateGetter;
			}
			set
			{
				this.checkStateGetter = value;
				if (value == null)
				{
					this.olv.CheckStateGetter = null;
					return;
				}
				this.olv.CheckStateGetter = ((object x) => this.checkStateGetter((T)((object)x)));
			}
		}

		public TypedObjectListView<T>.TypedBooleanCheckStateGetterDelegate BooleanCheckStateGetter
		{
			set
			{
				if (value == null)
				{
					this.olv.BooleanCheckStateGetter = null;
					return;
				}
				this.olv.BooleanCheckStateGetter = ((object x) => value((T)((object)x)));
			}
		}

		public TypedObjectListView<T>.TypedCheckStatePutterDelegate CheckStatePutter
		{
			get
			{
				return this.checkStatePutter;
			}
			set
			{
				this.checkStatePutter = value;
				if (value == null)
				{
					this.olv.CheckStatePutter = null;
					return;
				}
				this.olv.CheckStatePutter = ((object x, CheckState newValue) => this.checkStatePutter((T)((object)x), newValue));
			}
		}

		public TypedObjectListView<T>.TypedBooleanCheckStatePutterDelegate BooleanCheckStatePutter
		{
			set
			{
				if (value == null)
				{
					this.olv.BooleanCheckStatePutter = null;
					return;
				}
				this.olv.BooleanCheckStatePutter = ((object x, bool newValue) => value((T)((object)x), newValue));
			}
		}

		public TypedObjectListView<T>.TypedCellToolTipGetterDelegate CellToolTipGetter
		{
			set
			{
				if (value == null)
				{
					this.olv.CellToolTipGetter = null;
					return;
				}
				this.olv.CellToolTipGetter = ((OLVColumn col, object x) => value(col, (T)((object)x)));
			}
		}

		public HeaderToolTipGetterDelegate HeaderToolTipGetter
		{
			get
			{
				return this.olv.HeaderToolTipGetter;
			}
			set
			{
				this.olv.HeaderToolTipGetter = value;
			}
		}

		public void GenerateAspectGetters()
		{
			for (int i = 0; i < this.ListView.Columns.Count; i++)
			{
				this.GetColumn(i).GenerateAspectGetter();
			}
		}

		private ObjectListView olv;

		private TypedObjectListView<T>.TypedCheckStateGetterDelegate checkStateGetter;

		private TypedObjectListView<T>.TypedCheckStatePutterDelegate checkStatePutter;

		public delegate CheckState TypedCheckStateGetterDelegate(T rowObject);

		public delegate bool TypedBooleanCheckStateGetterDelegate(T rowObject);

		public delegate CheckState TypedCheckStatePutterDelegate(T rowObject, CheckState newValue);

		public delegate bool TypedBooleanCheckStatePutterDelegate(T rowObject, bool newValue);

		public delegate string TypedCellToolTipGetterDelegate(OLVColumn column, T modelObject);
	}
}
