using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class Generator : IGenerator
	{
		public static IGenerator Instance
		{
			get
			{
				IGenerator result;
				if ((result = Generator.instance) == null)
				{
					result = (Generator.instance = new Generator());
				}
				return result;
			}
			set
			{
				Generator.instance = value;
			}
		}

		public static void GenerateColumns(ObjectListView olv, IEnumerable enumerable)
		{
			Generator.GenerateColumns(olv, enumerable, false);
		}

		public static void GenerateColumns(ObjectListView olv, IEnumerable enumerable, bool allProperties)
		{
			if (enumerable != null)
			{
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Generator.Instance.GenerateAndReplaceColumns(olv, obj.GetType(), allProperties);
						return;
					}
				}
			}
			Generator.Instance.GenerateAndReplaceColumns(olv, null, allProperties);
		}

		public static void GenerateColumns(ObjectListView olv, Type type)
		{
			Generator.Instance.GenerateAndReplaceColumns(olv, type, false);
		}

		public static void GenerateColumns(ObjectListView olv, Type type, bool allProperties)
		{
			Generator.Instance.GenerateAndReplaceColumns(olv, type, allProperties);
		}

		public static IList<OLVColumn> GenerateColumns(Type type)
		{
			return Generator.Instance.GenerateColumns(type, false);
		}

		public void GenerateAndReplaceColumns(ObjectListView olv, Type type, bool allProperties)
		{
			IList<OLVColumn> columns = this.GenerateColumns(type, allProperties);
			TreeListView treeListView = olv as TreeListView;
			if (treeListView != null)
			{
				this.TryGenerateChildrenDelegates(treeListView, type);
			}
			this.ReplaceColumns(olv, columns);
		}

		public IList<OLVColumn> GenerateColumns(Type type, bool allProperties)
		{
			List<OLVColumn> list = new List<OLVColumn>();
			if (type == null)
			{
				return list;
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (Attribute.GetCustomAttribute(propertyInfo, typeof(OLVIgnoreAttribute)) == null)
				{
					OLVColumnAttribute olvcolumnAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(OLVColumnAttribute)) as OLVColumnAttribute;
					if (olvcolumnAttribute == null)
					{
						if (allProperties)
						{
							list.Add(this.MakeColumnFromPropertyInfo(propertyInfo));
						}
					}
					else
					{
						list.Add(this.MakeColumnFromAttribute(propertyInfo, olvcolumnAttribute));
					}
				}
			}
			int num = 0;
			foreach (OLVColumn olvcolumn in list)
			{
				if (olvcolumn.DisplayIndex >= 0)
				{
					num++;
				}
			}
			int num2 = num;
			foreach (OLVColumn olvcolumn2 in list)
			{
				if (olvcolumn2.DisplayIndex < 0)
				{
					olvcolumn2.DisplayIndex = num2++;
				}
			}
			list.Sort((OLVColumn x, OLVColumn y) => x.DisplayIndex.CompareTo(y.DisplayIndex));
			return list;
		}

		protected void ReplaceColumns(ObjectListView olv, IList<OLVColumn> columns)
		{
			olv.Reset();
			if (columns != null && columns.Count != 0)
			{
				olv.AllColumns.AddRange(columns);
				this.PostCreateColumns(olv);
				return;
			}
		}

		public void PostCreateColumns(ObjectListView olv)
		{
			if (olv.AllColumns.Exists((OLVColumn x) => x.CheckBoxes))
			{
				olv.UseSubItemCheckBoxes = true;
			}
			if (olv.AllColumns.Exists((OLVColumn x) => x.Index > 0 && (x.ImageGetter != null || !string.IsNullOrEmpty(x.ImageAspectName))))
			{
				olv.ShowImagesOnSubItems = true;
			}
			olv.RebuildColumns();
			olv.AutoSizeColumns();
		}

		protected OLVColumn MakeColumnFromAttribute(PropertyInfo pinfo, OLVColumnAttribute attr)
		{
			return this.MakeColumn(pinfo.Name, this.DisplayNameToColumnTitle(pinfo.Name), pinfo.CanWrite, pinfo.PropertyType, attr);
		}

		protected OLVColumn MakeColumnFromPropertyInfo(PropertyInfo pinfo)
		{
			return this.MakeColumn(pinfo.Name, this.DisplayNameToColumnTitle(pinfo.Name), pinfo.CanWrite, pinfo.PropertyType, null);
		}

		public OLVColumn MakeColumnFromPropertyDescriptor(PropertyDescriptor pd)
		{
			OLVColumnAttribute attr = pd.Attributes[typeof(OLVColumnAttribute)] as OLVColumnAttribute;
			return this.MakeColumn(pd.Name, this.DisplayNameToColumnTitle(pd.DisplayName), !pd.IsReadOnly, pd.PropertyType, attr);
		}

		protected OLVColumn MakeColumn(string aspectName, string title, bool editable, Type propertyType, OLVColumnAttribute attr)
		{
			OLVColumn olvcolumn = this.MakeColumn(aspectName, title, attr);
			olvcolumn.Name = ((attr == null || string.IsNullOrEmpty(attr.Name)) ? aspectName : attr.Name);
			this.ConfigurePossibleBooleanColumn(olvcolumn, propertyType);
			if (attr == null)
			{
				olvcolumn.IsEditable = editable;
				return olvcolumn;
			}
			olvcolumn.AspectToStringFormat = attr.AspectToStringFormat;
			if (attr.IsCheckBoxesSet)
			{
				olvcolumn.CheckBoxes = attr.CheckBoxes;
			}
			olvcolumn.DisplayIndex = attr.DisplayIndex;
			olvcolumn.FillsFreeSpace = attr.FillsFreeSpace;
			if (attr.IsFreeSpaceProportionSet)
			{
				olvcolumn.FreeSpaceProportion = attr.FreeSpaceProportion;
			}
			olvcolumn.GroupWithItemCountFormat = attr.GroupWithItemCountFormat;
			olvcolumn.GroupWithItemCountSingularFormat = attr.GroupWithItemCountSingularFormat;
			olvcolumn.Hyperlink = attr.Hyperlink;
			olvcolumn.ImageAspectName = attr.ImageAspectName;
			olvcolumn.IsEditable = (attr.IsEditableSet ? attr.IsEditable : editable);
			olvcolumn.IsTileViewColumn = attr.IsTileViewColumn;
			olvcolumn.IsVisible = attr.IsVisible;
			olvcolumn.MaximumWidth = attr.MaximumWidth;
			olvcolumn.MinimumWidth = attr.MinimumWidth;
			olvcolumn.Tag = attr.Tag;
			if (attr.IsTextAlignSet)
			{
				olvcolumn.TextAlign = attr.TextAlign;
			}
			olvcolumn.ToolTipText = attr.ToolTipText;
			if (attr.IsTriStateCheckBoxesSet)
			{
				olvcolumn.TriStateCheckBoxes = attr.TriStateCheckBoxes;
			}
			olvcolumn.UseInitialLetterForGroup = attr.UseInitialLetterForGroup;
			olvcolumn.Width = attr.Width;
			if (attr.GroupCutoffs != null && attr.GroupDescriptions != null)
			{
				olvcolumn.MakeGroupies(attr.GroupCutoffs, attr.GroupDescriptions);
			}
			return olvcolumn;
		}

		protected OLVColumn MakeColumn(string aspectName, string title, OLVColumnAttribute attr)
		{
			string title2 = (attr == null || string.IsNullOrEmpty(attr.Title)) ? title : attr.Title;
			return new OLVColumn(title2, aspectName);
		}

		protected string DisplayNameToColumnTitle(string displayName)
		{
			string text = displayName.Replace(Generator.getString_0(107251304), Generator.getString_0(107403404));
			text = Regex.Replace(text, Generator.getString_0(107314553), Generator.getString_0(107314496));
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
		}

		protected void ConfigurePossibleBooleanColumn(OLVColumn column, Type propertyType)
		{
			if (propertyType != typeof(bool) && propertyType != typeof(bool?) && propertyType != typeof(CheckState))
			{
				return;
			}
			column.CheckBoxes = true;
			column.TextAlign = HorizontalAlignment.Center;
			column.Width = 32;
			column.TriStateCheckBoxes = (propertyType == typeof(bool?) || propertyType == typeof(CheckState));
		}

		protected void TryGenerateChildrenDelegates(TreeListView tlv, Type type)
		{
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (Attribute.GetCustomAttribute(propertyInfo, typeof(OLVChildrenAttribute)) is OLVChildrenAttribute)
				{
					this.GenerateChildrenDelegates(tlv, propertyInfo);
					return;
				}
			}
		}

		protected void GenerateChildrenDelegates(TreeListView tlv, PropertyInfo pinfo)
		{
			Munger childrenGetter = new Munger(pinfo.Name);
			tlv.CanExpandGetter = delegate(object x)
			{
				bool result;
				try
				{
					IEnumerable collection = childrenGetter.GetValueEx(x) as IEnumerable;
					result = !ObjectListView.IsEnumerableEmpty(collection);
				}
				catch (MungerException)
				{
					result = false;
				}
				return result;
			};
			tlv.ChildrenGetter = delegate(object x)
			{
				IEnumerable result;
				try
				{
					result = (childrenGetter.GetValueEx(x) as IEnumerable);
				}
				catch (MungerException)
				{
					result = null;
				}
				return result;
			};
		}

		static Generator()
		{
			Strings.CreateGetStringDelegate(typeof(Generator));
		}

		private static IGenerator instance;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
