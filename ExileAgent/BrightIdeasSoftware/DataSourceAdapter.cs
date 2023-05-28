using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public class DataSourceAdapter : IDisposable
	{
		public DataSourceAdapter(ObjectListView olv)
		{
			if (olv == null)
			{
				throw new ArgumentNullException(DataSourceAdapter.getString_0(107315193));
			}
			this.ListView = olv;
			this.BindListView(this.ListView);
		}

		~DataSourceAdapter()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual void Dispose(bool fromUser)
		{
			this.UnbindListView(this.ListView);
			this.UnbindDataSource();
		}

		public bool AutoGenerateColumns
		{
			get
			{
				return this.autoGenerateColumns;
			}
			set
			{
				this.autoGenerateColumns = value;
			}
		}

		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
				this.RebindDataSource(true);
			}
		}

		public virtual string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (this.dataMember != value)
				{
					this.dataMember = value;
					this.RebindDataSource();
				}
			}
		}

		public ObjectListView ListView
		{
			get
			{
				return this.listView;
			}
			internal set
			{
				this.listView = value;
			}
		}

		protected CurrencyManager CurrencyManager
		{
			get
			{
				return this.currencyManager;
			}
			set
			{
				this.currencyManager = value;
			}
		}

		protected virtual void BindListView(ObjectListView olv)
		{
			if (olv == null)
			{
				return;
			}
			olv.Freezing += this.HandleListViewFreezing;
			olv.SelectedIndexChanged += this.HandleListViewSelectedIndexChanged;
			olv.BindingContextChanged += this.HandleListViewBindingContextChanged;
		}

		protected virtual void UnbindListView(ObjectListView olv)
		{
			if (olv == null)
			{
				return;
			}
			olv.Freezing -= this.HandleListViewFreezing;
			olv.SelectedIndexChanged -= this.HandleListViewSelectedIndexChanged;
			olv.BindingContextChanged -= this.HandleListViewBindingContextChanged;
		}

		protected virtual void BindDataSource()
		{
			if (this.CurrencyManager == null)
			{
				return;
			}
			this.CurrencyManager.MetaDataChanged += this.HandleCurrencyManagerMetaDataChanged;
			this.CurrencyManager.PositionChanged += this.HandleCurrencyManagerPositionChanged;
			this.CurrencyManager.ListChanged += this.CurrencyManagerListChanged;
		}

		protected virtual void UnbindDataSource()
		{
			if (this.CurrencyManager == null)
			{
				return;
			}
			this.CurrencyManager.MetaDataChanged -= this.HandleCurrencyManagerMetaDataChanged;
			this.CurrencyManager.PositionChanged -= this.HandleCurrencyManagerPositionChanged;
			this.CurrencyManager.ListChanged -= this.CurrencyManagerListChanged;
		}

		protected virtual void RebindDataSource()
		{
			this.RebindDataSource(false);
		}

		protected virtual void RebindDataSource(bool forceDataInitialization)
		{
			CurrencyManager currencyManager = null;
			if (this.ListView != null && this.ListView.BindingContext != null && this.DataSource != null)
			{
				currencyManager = (this.ListView.BindingContext[this.DataSource, this.DataMember] as CurrencyManager);
			}
			if (this.CurrencyManager != currencyManager)
			{
				this.UnbindDataSource();
				this.CurrencyManager = currencyManager;
				this.BindDataSource();
				forceDataInitialization = true;
			}
			if (forceDataInitialization)
			{
				this.InitializeDataSource();
			}
		}

		protected virtual void InitializeDataSource()
		{
			if (!this.ListView.Frozen && this.CurrencyManager != null)
			{
				this.CreateColumnsFromSource();
				this.CreateMissingAspectGettersAndPutters();
				this.SetListContents();
				this.ListView.AutoSizeColumns();
				return;
			}
		}

		protected virtual void SetListContents()
		{
			this.ListView.Objects = this.CurrencyManager.List;
		}

		protected virtual void CreateColumnsFromSource()
		{
			if (this.CurrencyManager == null)
			{
				return;
			}
			if (this.ListView.IsDesignMode)
			{
				return;
			}
			if (!this.AutoGenerateColumns)
			{
				return;
			}
			Generator generator = (Generator.Instance as Generator) ?? new Generator();
			PropertyDescriptorCollection itemProperties = this.CurrencyManager.GetItemProperties();
			if (itemProperties.Count == 0)
			{
				return;
			}
			foreach (object obj in itemProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (this.ShouldCreateColumn(propertyDescriptor))
				{
					OLVColumn olvcolumn = generator.MakeColumnFromPropertyDescriptor(propertyDescriptor);
					this.ConfigureColumn(olvcolumn, propertyDescriptor);
					this.ListView.AllColumns.Add(olvcolumn);
				}
			}
			generator.PostCreateColumns(this.ListView);
		}

		protected virtual bool ShouldCreateColumn(PropertyDescriptor property)
		{
			return !this.ListView.AllColumns.Exists((OLVColumn x) => x.AspectName == property.Name) && !(property.PropertyType == typeof(IBindingList)) && property.Attributes[typeof(OLVIgnoreAttribute)] == null;
		}

		protected virtual void ConfigureColumn(OLVColumn column, PropertyDescriptor property)
		{
			column.LastDisplayIndex = this.ListView.AllColumns.Count;
			if (property.PropertyType == typeof(byte[]))
			{
				column.Renderer = new ImageRenderer();
			}
		}

		protected virtual void CreateMissingAspectGettersAndPutters()
		{
			foreach (OLVColumn column2 in this.ListView.AllColumns)
			{
				OLVColumn column = column2;
				if (column.AspectGetter == null && !string.IsNullOrEmpty(column.AspectName))
				{
					column.AspectGetter = delegate(object row)
					{
						DataRowView dataRowView = row as DataRowView;
						if (dataRowView == null)
						{
							return column.GetAspectByName(row);
						}
						if (dataRowView.Row.RowState != DataRowState.Detached)
						{
							return dataRowView[column.AspectName];
						}
						return null;
					};
				}
				if (column.IsEditable && column.AspectPutter == null && !string.IsNullOrEmpty(column.AspectName))
				{
					column.AspectPutter = delegate(object row, object newValue)
					{
						DataRowView dataRowView = row as DataRowView;
						if (dataRowView == null)
						{
							column.PutAspectByName(row, newValue);
							return;
						}
						if (dataRowView.Row.RowState != DataRowState.Detached)
						{
							dataRowView[column.AspectName] = newValue;
						}
					};
				}
			}
		}

		protected virtual void CurrencyManagerListChanged(object sender, ListChangedEventArgs e)
		{
			if (this.ListView.Frozen)
			{
				return;
			}
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				this.HandleListChangedReset(e);
				return;
			case ListChangedType.ItemAdded:
				this.HandleListChangedItemAdded(e);
				return;
			case ListChangedType.ItemDeleted:
				this.HandleListChangedItemDeleted(e);
				return;
			case ListChangedType.ItemMoved:
				this.HandleListChangedItemMoved(e);
				return;
			case ListChangedType.ItemChanged:
				this.HandleListChangedItemChanged(e);
				return;
			case ListChangedType.PropertyDescriptorAdded:
			case ListChangedType.PropertyDescriptorDeleted:
			case ListChangedType.PropertyDescriptorChanged:
				this.HandleListChangedMetadataChanged(e);
				return;
			default:
				return;
			}
		}

		protected virtual void HandleListChangedMetadataChanged(ListChangedEventArgs e)
		{
			this.InitializeDataSource();
		}

		protected virtual void HandleListChangedItemMoved(ListChangedEventArgs e)
		{
			this.InitializeDataSource();
		}

		protected virtual void HandleListChangedItemDeleted(ListChangedEventArgs e)
		{
			this.InitializeDataSource();
		}

		protected virtual void HandleListChangedItemAdded(ListChangedEventArgs e)
		{
			object obj = this.CurrencyManager.List[e.NewIndex];
			DataRowView dataRowView = obj as DataRowView;
			if (dataRowView == null || !dataRowView.IsNew)
			{
				this.InitializeDataSource();
			}
		}

		protected virtual void HandleListChangedReset(ListChangedEventArgs e)
		{
			this.InitializeDataSource();
		}

		protected virtual void HandleListChangedItemChanged(ListChangedEventArgs e)
		{
			object modelObject = this.CurrencyManager.List[e.NewIndex];
			this.ListView.RefreshObject(modelObject);
		}

		protected virtual void HandleCurrencyManagerMetaDataChanged(object sender, EventArgs e)
		{
			this.InitializeDataSource();
		}

		protected virtual void HandleCurrencyManagerPositionChanged(object sender, EventArgs e)
		{
			int position = this.CurrencyManager.Position;
			if (position < 0 || position >= this.ListView.GetItemCount())
			{
				return;
			}
			if (this.isChangingIndex)
			{
				return;
			}
			try
			{
				this.isChangingIndex = true;
				this.ChangePosition(position);
			}
			finally
			{
				this.isChangingIndex = false;
			}
		}

		protected virtual void ChangePosition(int index)
		{
			this.ListView.SelectedObject = this.CurrencyManager.List[index];
			if (this.ListView.SelectedIndices.Count > 0)
			{
				this.ListView.EnsureVisible(this.ListView.SelectedIndices[0]);
			}
		}

		protected virtual void HandleListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.isChangingIndex)
			{
				return;
			}
			if (this.ListView.SelectedIndices.Count == 1 && this.CurrencyManager != null)
			{
				try
				{
					this.isChangingIndex = true;
					this.CurrencyManager.Position = this.CurrencyManager.List.IndexOf(this.ListView.SelectedObject);
				}
				finally
				{
					this.isChangingIndex = false;
				}
			}
		}

		protected virtual void HandleListViewFreezing(object sender, FreezeEventArgs e)
		{
			if (!this.alreadyFreezing && e.FreezeLevel == 0)
			{
				try
				{
					this.alreadyFreezing = true;
					this.RebindDataSource(true);
				}
				finally
				{
					this.alreadyFreezing = false;
				}
			}
		}

		protected virtual void HandleListViewBindingContextChanged(object sender, EventArgs e)
		{
			this.RebindDataSource(false);
		}

		static DataSourceAdapter()
		{
			Strings.CreateGetStringDelegate(typeof(DataSourceAdapter));
		}

		private bool autoGenerateColumns = true;

		private object dataSource;

		private string dataMember = DataSourceAdapter.getString_0(107403039);

		private ObjectListView listView;

		private CurrencyManager currencyManager;

		private bool isChangingIndex;

		private bool alreadyFreezing;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
