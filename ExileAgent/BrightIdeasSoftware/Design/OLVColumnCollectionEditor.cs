using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware.Design
{
	public sealed class OLVColumnCollectionEditor : CollectionEditor
	{
		public OLVColumnCollectionEditor(Type t) : base(t)
		{
		}

		protected override Type CreateCollectionItemType()
		{
			return typeof(OLVColumn);
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context == null)
			{
				throw new ArgumentNullException(OLVColumnCollectionEditor.getString_0(107313464));
			}
			if (provider == null)
			{
				throw new ArgumentNullException(OLVColumnCollectionEditor.getString_0(107313483));
			}
			ObjectListView objectListView = context.Instance as ObjectListView;
			base.EditValue(context, provider, objectListView.AllColumns);
			List<OLVColumn> filteredColumns = objectListView.GetFilteredColumns(View.Details);
			objectListView.Columns.Clear();
			objectListView.Columns.AddRange(filteredColumns.ToArray());
			return objectListView.Columns;
		}

		protected override string GetDisplayText(object value)
		{
			OLVColumn olvcolumn = value as OLVColumn;
			if (olvcolumn != null && !string.IsNullOrEmpty(olvcolumn.AspectName))
			{
				return string.Format(OLVColumnCollectionEditor.getString_0(107457644), base.GetDisplayText(value), olvcolumn.AspectName);
			}
			return base.GetDisplayText(value);
		}

		static OLVColumnCollectionEditor()
		{
			Strings.CreateGetStringDelegate(typeof(OLVColumnCollectionEditor));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
