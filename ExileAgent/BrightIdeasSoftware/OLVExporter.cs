using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class OLVExporter
	{
		public OLVExporter()
		{
		}

		public OLVExporter(ObjectListView olv) : this(olv, olv.Objects)
		{
		}

		public OLVExporter(ObjectListView olv, IEnumerable objectsToExport)
		{
			if (olv == null)
			{
				throw new ArgumentNullException(OLVExporter.getString_0(107316097));
			}
			if (objectsToExport == null)
			{
				throw new ArgumentNullException(OLVExporter.getString_0(107312837));
			}
			this.ListView = olv;
			this.ModelObjects = ObjectListView.EnumerableToArray(objectsToExport, true);
		}

		public bool IncludeHiddenColumns
		{
			get
			{
				return this.includeHiddenColumns;
			}
			set
			{
				this.includeHiddenColumns = value;
			}
		}

		public bool IncludeColumnHeaders
		{
			get
			{
				return this.includeColumnHeaders;
			}
			set
			{
				this.includeColumnHeaders = value;
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.objectListView;
			}
			set
			{
				this.objectListView = value;
			}
		}

		public IList ModelObjects
		{
			get
			{
				return this.modelObjects;
			}
			set
			{
				this.modelObjects = value;
			}
		}

		public string ExportTo(OLVExporter.ExportFormat format)
		{
			if (this.results == null)
			{
				this.Convert();
			}
			return this.results[format];
		}

		public void Convert()
		{
			IList<OLVColumn> list = this.IncludeHiddenColumns ? this.ListView.AllColumns : this.ListView.ColumnsInDisplayOrder;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder(OLVExporter.getString_0(107312848));
			if (this.IncludeColumnHeaders)
			{
				List<string> list2 = new List<string>();
				foreach (OLVColumn olvcolumn in list)
				{
					list2.Add(olvcolumn.Text);
				}
				this.WriteOneRow(stringBuilder, list2, OLVExporter.getString_0(107403943), OLVExporter.getString_0(107247112), OLVExporter.getString_0(107403943), null);
				this.WriteOneRow(stringBuilder3, list2, OLVExporter.getString_0(107312803), OLVExporter.getString_0(107312822), OLVExporter.getString_0(107312777), new OLVExporter.StringToString(OLVExporter.HtmlEncode));
				this.WriteOneRow(stringBuilder2, list2, OLVExporter.getString_0(107403943), OLVExporter.getString_0(107399567), OLVExporter.getString_0(107403943), new OLVExporter.StringToString(OLVExporter.CsvEncode));
			}
			foreach (object rowObject in this.ModelObjects)
			{
				List<string> list3 = new List<string>();
				foreach (OLVColumn olvcolumn2 in list)
				{
					list3.Add(olvcolumn2.GetStringValue(rowObject));
				}
				this.WriteOneRow(stringBuilder, list3, OLVExporter.getString_0(107403943), OLVExporter.getString_0(107247112), OLVExporter.getString_0(107403943), null);
				this.WriteOneRow(stringBuilder3, list3, OLVExporter.getString_0(107312803), OLVExporter.getString_0(107312822), OLVExporter.getString_0(107312777), new OLVExporter.StringToString(OLVExporter.HtmlEncode));
				this.WriteOneRow(stringBuilder2, list3, OLVExporter.getString_0(107403943), OLVExporter.getString_0(107399567), OLVExporter.getString_0(107403943), new OLVExporter.StringToString(OLVExporter.CsvEncode));
			}
			stringBuilder3.AppendLine(OLVExporter.getString_0(107312792));
			this.results = new Dictionary<OLVExporter.ExportFormat, string>();
			this.results[OLVExporter.ExportFormat.TabSeparated] = stringBuilder.ToString();
			this.results[OLVExporter.ExportFormat.CSV] = stringBuilder2.ToString();
			this.results[OLVExporter.ExportFormat.HTML] = stringBuilder3.ToString();
		}

		private void WriteOneRow(StringBuilder sb, IEnumerable<string> strings, string startRow, string betweenCells, string endRow, OLVExporter.StringToString encoder)
		{
			sb.Append(startRow);
			bool flag = true;
			foreach (string text in strings)
			{
				if (!flag)
				{
					sb.Append(betweenCells);
				}
				sb.Append((encoder == null) ? text : encoder(text));
				flag = false;
			}
			sb.AppendLine(endRow);
		}

		private static string CsvEncode(string text)
		{
			if (text == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(OLVExporter.getString_0(107375956));
			stringBuilder.Append(text.Replace(OLVExporter.getString_0(107375956), OLVExporter.getString_0(107312779)));
			stringBuilder.Append(OLVExporter.getString_0(107375956));
			return stringBuilder.ToString();
		}

		private static string HtmlEncode(string text)
		{
			if (text == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c != '"')
				{
					if (c != '&')
					{
						switch (c)
						{
						case '<':
							stringBuilder.Append(OLVExporter.getString_0(107249811));
							goto IL_117;
						case '>':
							stringBuilder.Append(OLVExporter.getString_0(107249770));
							goto IL_117;
						}
						if (text[i] > '\u009f')
						{
							stringBuilder.Append(OLVExporter.getString_0(107246870));
							stringBuilder.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
							stringBuilder.Append(OLVExporter.getString_0(107246865));
						}
						else
						{
							stringBuilder.Append(text[i]);
						}
					}
					else
					{
						stringBuilder.Append(OLVExporter.getString_0(107249793));
					}
				}
				else
				{
					stringBuilder.Append(OLVExporter.getString_0(107249761));
				}
				IL_117:;
			}
			return stringBuilder.ToString();
		}

		static OLVExporter()
		{
			Strings.CreateGetStringDelegate(typeof(OLVExporter));
		}

		private bool includeHiddenColumns;

		private bool includeColumnHeaders = true;

		private ObjectListView objectListView;

		private IList modelObjects = new ArrayList();

		private Dictionary<OLVExporter.ExportFormat, string> results;

		[NonSerialized]
		internal static GetString getString_0;

		public enum ExportFormat
		{
			TabSeparated = 1,
			TSV = 1,
			CSV,
			HTML
		}

		private delegate string StringToString(string str);
	}
}
