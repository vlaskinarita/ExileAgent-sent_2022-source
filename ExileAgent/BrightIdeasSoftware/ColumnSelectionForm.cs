using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed partial class ColumnSelectionForm : Form
	{
		public ColumnSelectionForm()
		{
			this.InitializeComponent();
		}

		public void OpenOn(ObjectListView olv)
		{
			this.OpenOn(olv, olv.View);
		}

		public void OpenOn(ObjectListView olv, View view)
		{
			if (view != View.Details && view != View.Tile)
			{
				return;
			}
			this.InitializeForm(olv, view);
			if (base.ShowDialog() == DialogResult.OK)
			{
				this.Apply(olv, view);
			}
		}

		protected void InitializeForm(ObjectListView olv, View view)
		{
			this.AllColumns = olv.AllColumns;
			this.RearrangableColumns = new List<OLVColumn>(this.AllColumns);
			foreach (OLVColumn olvcolumn in this.RearrangableColumns)
			{
				if (view == View.Details)
				{
					this.MapColumnToVisible[olvcolumn] = olvcolumn.IsVisible;
				}
				else
				{
					this.MapColumnToVisible[olvcolumn] = olvcolumn.IsTileViewColumn;
				}
			}
			this.RearrangableColumns.Sort(new ColumnSelectionForm.SortByDisplayOrder(this));
			this.objectListView1.BooleanCheckStateGetter = ((object rowObject) => this.MapColumnToVisible[(OLVColumn)rowObject]);
			this.objectListView1.BooleanCheckStatePutter = delegate(object rowObject, bool newValue)
			{
				OLVColumn olvcolumn2 = (OLVColumn)rowObject;
				if (!olvcolumn2.CanBeHidden)
				{
					return true;
				}
				this.MapColumnToVisible[olvcolumn2] = newValue;
				this.EnableControls();
				return newValue;
			};
			this.objectListView1.SetObjects(this.RearrangableColumns);
			this.EnableControls();
		}

		protected void Apply(ObjectListView olv, View view)
		{
			olv.Freeze();
			if (view == View.Details)
			{
				using (List<OLVColumn>.Enumerator enumerator = olv.AllColumns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OLVColumn olvcolumn = enumerator.Current;
						olvcolumn.IsVisible = this.MapColumnToVisible[olvcolumn];
					}
					goto IL_90;
				}
			}
			foreach (OLVColumn olvcolumn2 in olv.AllColumns)
			{
				olvcolumn2.IsTileViewColumn = this.MapColumnToVisible[olvcolumn2];
			}
			IL_90:
			List<OLVColumn> list = this.RearrangableColumns.FindAll((OLVColumn x) => this.MapColumnToVisible[x]);
			if (view == View.Details)
			{
				olv.ChangeToFilteredColumns(view);
				using (List<OLVColumn>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						ColumnHeader columnHeader = enumerator3.Current;
						columnHeader.DisplayIndex = list.IndexOf((OLVColumn)columnHeader);
					}
					goto IL_138;
				}
			}
			OLVColumn olvcolumn3 = this.AllColumns[0];
			list.Remove(olvcolumn3);
			olv.Columns.Clear();
			olv.Columns.Add(olvcolumn3);
			olv.Columns.AddRange(list.ToArray());
			olv.CalculateReasonableTileSize();
			IL_138:
			olv.Unfreeze();
		}

		private void buttonMoveUp_Click(object sender, EventArgs e)
		{
			int num = this.objectListView1.SelectedIndices[0];
			OLVColumn item = this.RearrangableColumns[num];
			this.RearrangableColumns.RemoveAt(num);
			this.RearrangableColumns.Insert(num - 1, item);
			this.objectListView1.BuildList();
			this.EnableControls();
		}

		private void buttonMoveDown_Click(object sender, EventArgs e)
		{
			int num = this.objectListView1.SelectedIndices[0];
			OLVColumn item = this.RearrangableColumns[num];
			this.RearrangableColumns.RemoveAt(num);
			this.RearrangableColumns.Insert(num + 1, item);
			this.objectListView1.BuildList();
			this.EnableControls();
		}

		private void buttonShow_Click(object sender, EventArgs e)
		{
			this.objectListView1.SelectedItem.Checked = true;
		}

		private void buttonHide_Click(object sender, EventArgs e)
		{
			this.objectListView1.SelectedItem.Checked = false;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void objectListView1_SelectionChanged(object sender, EventArgs e)
		{
			this.EnableControls();
		}

		protected void EnableControls()
		{
			if (this.objectListView1.SelectedIndices.Count == 0)
			{
				this.buttonMoveUp.Enabled = false;
				this.buttonMoveDown.Enabled = false;
				this.buttonShow.Enabled = false;
				this.buttonHide.Enabled = false;
				return;
			}
			this.buttonMoveUp.Enabled = (this.objectListView1.SelectedIndices[0] != 0);
			this.buttonMoveDown.Enabled = (this.objectListView1.SelectedIndices[0] < this.objectListView1.GetItemCount() - 1);
			OLVColumn olvcolumn = (OLVColumn)this.objectListView1.SelectedObject;
			this.buttonShow.Enabled = (!this.MapColumnToVisible[olvcolumn] && olvcolumn.CanBeHidden);
			this.buttonHide.Enabled = (this.MapColumnToVisible[olvcolumn] && olvcolumn.CanBeHidden);
		}

		static ColumnSelectionForm()
		{
			Strings.CreateGetStringDelegate(typeof(ColumnSelectionForm));
		}

		private List<OLVColumn> AllColumns;

		private List<OLVColumn> RearrangableColumns = new List<OLVColumn>();

		private Dictionary<OLVColumn, bool> MapColumnToVisible = new Dictionary<OLVColumn, bool>();

		private sealed class SortByDisplayOrder : IComparer<OLVColumn>
		{
			public SortByDisplayOrder(ColumnSelectionForm form)
			{
				this.Form = form;
			}

			int IComparer<OLVColumn>.Compare(OLVColumn x, OLVColumn y)
			{
				if (this.Form.MapColumnToVisible[x] && !this.Form.MapColumnToVisible[y])
				{
					return -1;
				}
				if (!this.Form.MapColumnToVisible[x] && this.Form.MapColumnToVisible[y])
				{
					return 1;
				}
				if (x.DisplayIndex == y.DisplayIndex)
				{
					return x.Text.CompareTo(y.Text);
				}
				return x.DisplayIndex - y.DisplayIndex;
			}

			private ColumnSelectionForm Form;
		}
	}
}
