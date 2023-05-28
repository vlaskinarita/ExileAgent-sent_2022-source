using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns18
{
	internal sealed partial class Form3 : Form
	{
		public Form3(List<FlippingListItem> list_0)
		{
			this.method_0();
			this.fastObjectListView_0.AddObjects(list_0);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_0()
		{
			this.fastObjectListView_0 = new FastObjectListView();
			this.olvcolumn_0 = new OLVColumn();
			this.olvcolumn_1 = new OLVColumn();
			this.olvcolumn_2 = new OLVColumn();
			((ISupportInitialize)this.fastObjectListView_0).BeginInit();
			base.SuspendLayout();
			this.fastObjectListView_0.AllColumns.Add(this.olvcolumn_0);
			this.fastObjectListView_0.AllColumns.Add(this.olvcolumn_1);
			this.fastObjectListView_0.AllColumns.Add(this.olvcolumn_2);
			this.fastObjectListView_0.AlternateRowBackColor = Color.Gainsboro;
			this.fastObjectListView_0.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_0,
				this.olvcolumn_1,
				this.olvcolumn_2
			});
			this.fastObjectListView_0.FullRowSelect = true;
			this.fastObjectListView_0.HideSelection = false;
			this.fastObjectListView_0.Location = new Point(6, 6);
			this.fastObjectListView_0.MultiSelect = false;
			this.fastObjectListView_0.Name = Form3.getString_0(107371611);
			this.fastObjectListView_0.ShowGroups = false;
			this.fastObjectListView_0.Size = new Size(300, 402);
			this.fastObjectListView_0.TabIndex = 0;
			this.fastObjectListView_0.UseAlternatingBackColors = true;
			this.fastObjectListView_0.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_0.View = View.Details;
			this.fastObjectListView_0.VirtualMode = true;
			this.olvcolumn_0.AspectName = Form3.getString_0(107371582);
			this.olvcolumn_0.CellPadding = null;
			this.olvcolumn_0.IsEditable = false;
			this.olvcolumn_0.MaximumWidth = 67;
			this.olvcolumn_0.MinimumWidth = 67;
			this.olvcolumn_0.Text = Form3.getString_0(107408084);
			this.olvcolumn_0.Width = 67;
			this.olvcolumn_1.AspectName = Form3.getString_0(107371537);
			this.olvcolumn_1.CellPadding = null;
			this.olvcolumn_1.FillsFreeSpace = true;
			this.olvcolumn_1.IsEditable = false;
			this.olvcolumn_1.MaximumWidth = 150;
			this.olvcolumn_1.MinimumWidth = 150;
			this.olvcolumn_1.Text = Form3.getString_0(107408043);
			this.olvcolumn_1.Width = 150;
			this.olvcolumn_2.AspectName = Form3.getString_0(107371524);
			this.olvcolumn_2.CellPadding = null;
			this.olvcolumn_2.FillsFreeSpace = true;
			this.olvcolumn_2.IsEditable = false;
			this.olvcolumn_2.MaximumWidth = 60;
			this.olvcolumn_2.MinimumWidth = 60;
			this.olvcolumn_2.Text = Form3.getString_0(107353840);
			base.AutoScaleDimensions = new SizeF(7f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(307, 411);
			base.Controls.Add(this.fastObjectListView_0);
			this.Font = new Font(Form3.getString_0(107398087), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = Form3.getString_0(107371539);
			this.Text = Form3.getString_0(107371510);
			((ISupportInitialize)this.fastObjectListView_0).EndInit();
			base.ResumeLayout(false);
		}

		static Form3()
		{
			Strings.CreateGetStringDelegate(typeof(Form3));
		}

		private IContainer icontainer_0 = null;

		private FastObjectListView fastObjectListView_0;

		private OLVColumn olvcolumn_0;

		private OLVColumn olvcolumn_1;

		private OLVColumn olvcolumn_2;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
