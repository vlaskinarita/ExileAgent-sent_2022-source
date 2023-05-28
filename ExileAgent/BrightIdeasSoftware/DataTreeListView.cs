using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace BrightIdeasSoftware
{
	public sealed class DataTreeListView : TreeListView
	{
		[Category("Data")]
		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
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

		[DefaultValue("")]
		[Category("Data")]
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

		[Description("The name of the property/column that holds the key of a row")]
		[DefaultValue(null)]
		[Category("Data")]
		public string KeyAspectName
		{
			get
			{
				return this.Adapter.KeyAspectName;
			}
			set
			{
				this.Adapter.KeyAspectName = value;
			}
		}

		[Description("The name of the property/column that holds the key of the parent of a row")]
		[Category("Data")]
		[DefaultValue(null)]
		public string ParentKeyAspectName
		{
			get
			{
				return this.Adapter.ParentKeyAspectName;
			}
			set
			{
				this.Adapter.ParentKeyAspectName = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object RootKeyValue
		{
			get
			{
				return this.Adapter.RootKeyValue;
			}
			set
			{
				this.Adapter.RootKeyValue = value;
			}
		}

		[Category("Data")]
		[DefaultValue(null)]
		[Description("The parent id value that identifies a row as a root object")]
		public string RootKeyValueString
		{
			get
			{
				return Convert.ToString(this.Adapter.RootKeyValue);
			}
			set
			{
				this.Adapter.RootKeyValue = value;
			}
		}

		[Category("Data")]
		[Description("Should the keys columns (id and parent id) be shown to the user?")]
		[DefaultValue(true)]
		public bool ShowKeyColumns
		{
			get
			{
				return this.Adapter.ShowKeyColumns;
			}
			set
			{
				this.Adapter.ShowKeyColumns = value;
			}
		}

		protected TreeDataSourceAdapter Adapter
		{
			get
			{
				if (this.adapter == null)
				{
					this.adapter = new TreeDataSourceAdapter(this);
				}
				return this.adapter;
			}
			set
			{
				this.adapter = value;
			}
		}

		private TreeDataSourceAdapter adapter;
	}
}
