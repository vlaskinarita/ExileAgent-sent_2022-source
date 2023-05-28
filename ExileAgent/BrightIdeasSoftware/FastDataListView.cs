using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace BrightIdeasSoftware
{
	public sealed class FastDataListView : FastObjectListView
	{
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
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", typeof(UITypeEditor))]
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
				if (this.adapter == null)
				{
					this.adapter = this.CreateDataSourceAdapter();
				}
				return this.adapter;
			}
			set
			{
				this.adapter = value;
			}
		}

		protected DataSourceAdapter CreateDataSourceAdapter()
		{
			return new DataSourceAdapter(this);
		}

		private DataSourceAdapter adapter;
	}
}
