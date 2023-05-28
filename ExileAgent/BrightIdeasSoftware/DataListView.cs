using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace BrightIdeasSoftware
{
	public sealed class DataListView : ObjectListView
	{
		public DataListView()
		{
			this.Adapter = new DataSourceAdapter(this);
		}

		[Category("Data")]
		[Description("Should the control automatically generate columns from the DataSource")]
		[DefaultValue(true)]
		public bool AutoGenerateColumns
		{
			get
			{
				return this.Adapter.AutoGenerateColumns;
			}
			set
			{
				this.Adapter.AutoGenerateColumns = value;
			}
		}

		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
		[Category("Data")]
		public object DataSource
		{
			get
			{
				return this.Adapter.DataSource;
			}
			set
			{
				this.Adapter.DataSource = value;
			}
		}

		[Category("Data")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return this.Adapter.DataMember;
			}
			set
			{
				this.Adapter.DataMember = value;
			}
		}

		protected DataSourceAdapter Adapter
		{
			get
			{
				return this.adapter;
			}
			set
			{
				this.adapter = value;
			}
		}

		public override void AddObjects(ICollection modelObjects)
		{
		}

		public override void RemoveObjects(ICollection modelObjects)
		{
		}

		protected override void OnParentBindingContextChanged(EventArgs e)
		{
			base.OnParentBindingContextChanged(e);
		}

		private DataSourceAdapter adapter;
	}
}
