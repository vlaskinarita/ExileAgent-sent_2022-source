using System;
using System.Collections;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class OLVDataObject : DataObject
	{
		public OLVDataObject(ObjectListView olv) : this(olv, olv.SelectedObjects)
		{
		}

		public OLVDataObject(ObjectListView olv, IList modelObjects)
		{
			this.objectListView = olv;
			this.modelObjects = modelObjects;
			this.includeHiddenColumns = olv.IncludeHiddenColumnsInDataTransfer;
			this.includeColumnHeaders = olv.IncludeColumnHeadersInCopy;
			this.CreateTextFormats();
		}

		public bool IncludeHiddenColumns
		{
			get
			{
				return this.includeHiddenColumns;
			}
		}

		public bool IncludeColumnHeaders
		{
			get
			{
				return this.includeColumnHeaders;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.objectListView;
			}
		}

		public IList ModelObjects
		{
			get
			{
				return this.modelObjects;
			}
		}

		public void CreateTextFormats()
		{
			OLVExporter olvexporter = this.CreateExporter();
			this.SetData(olvexporter.ExportTo(OLVExporter.ExportFormat.TabSeparated));
			this.SetText(olvexporter.ExportTo(OLVExporter.ExportFormat.CSV), TextDataFormat.CommaSeparatedValue);
			this.SetText(this.ConvertToHtmlFragment(olvexporter.ExportTo(OLVExporter.ExportFormat.HTML)), TextDataFormat.Html);
		}

		protected OLVExporter CreateExporter()
		{
			return new OLVExporter(this.ListView)
			{
				IncludeColumnHeaders = this.IncludeColumnHeaders,
				IncludeHiddenColumns = this.IncludeHiddenColumns,
				ModelObjects = this.ModelObjects
			};
		}

		[Obsolete("Use OLVExporter directly instead", false)]
		public string CreateHtml()
		{
			OLVExporter olvexporter = this.CreateExporter();
			return olvexporter.ExportTo(OLVExporter.ExportFormat.HTML);
		}

		private string ConvertToHtmlFragment(string fragment)
		{
			int length = string.Format(OLVDataObject.getString_0(107315917), new object[]
			{
				0,
				0,
				0,
				0,
				OLVDataObject.getString_0(107315239),
				OLVDataObject.getString_0(107402840)
			}).Length;
			string text = string.Format(OLVDataObject.getString_0(107315654), fragment);
			int num = length + text.IndexOf(fragment, StringComparison.Ordinal);
			int num2 = num + fragment.Length;
			return string.Format(OLVDataObject.getString_0(107315917), new object[]
			{
				length,
				length + text.Length,
				num,
				num2,
				OLVDataObject.getString_0(107315239),
				text
			});
		}

		static OLVDataObject()
		{
			Strings.CreateGetStringDelegate(typeof(OLVDataObject));
		}

		private readonly bool includeHiddenColumns;

		private readonly bool includeColumnHeaders;

		private readonly ObjectListView objectListView;

		private readonly IList modelObjects;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
