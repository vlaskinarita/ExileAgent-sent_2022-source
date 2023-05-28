using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns10
{
	internal sealed partial class Form0 : Form
	{
		public Form0(IEnumerable<DivinationCardListItem> ienumerable_0)
		{
			this.method_0();
			this.fastObjectListView_0.SetObjects(ienumerable_0);
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
			this.fastObjectListView_0.Font = new Font(Form0.getString_0(107396320), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.fastObjectListView_0.FullRowSelect = true;
			this.fastObjectListView_0.HideSelection = false;
			this.fastObjectListView_0.Location = new Point(0, 0);
			this.fastObjectListView_0.MultiSelect = false;
			this.fastObjectListView_0.Name = Form0.getString_0(107396558);
			this.fastObjectListView_0.ShowGroups = false;
			this.fastObjectListView_0.Size = new Size(301, 408);
			this.fastObjectListView_0.TabIndex = 1;
			this.fastObjectListView_0.UseAlternatingBackColors = true;
			this.fastObjectListView_0.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_0.View = View.Details;
			this.fastObjectListView_0.VirtualMode = true;
			this.olvcolumn_0.AspectName = Form0.getString_0(107396509);
			this.olvcolumn_0.CellPadding = null;
			this.olvcolumn_0.MaximumWidth = 21;
			this.olvcolumn_0.MinimumWidth = 21;
			this.olvcolumn_0.Text = Form0.getString_0(107396528);
			this.olvcolumn_0.Width = 21;
			this.olvcolumn_1.AspectName = Form0.getString_0(107396523);
			this.olvcolumn_1.CellPadding = null;
			this.olvcolumn_1.MaximumWidth = 170;
			this.olvcolumn_1.MinimumWidth = 170;
			this.olvcolumn_1.Text = Form0.getString_0(107396523);
			this.olvcolumn_1.Width = 170;
			this.olvcolumn_2.AspectName = Form0.getString_0(107396482);
			this.olvcolumn_2.CellPadding = null;
			this.olvcolumn_2.FillsFreeSpace = true;
			this.olvcolumn_2.MaximumWidth = 89;
			this.olvcolumn_2.MinimumWidth = 89;
			this.olvcolumn_2.Text = Form0.getString_0(107396497);
			this.olvcolumn_2.Width = 89;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(301, 409);
			base.Controls.Add(this.fastObjectListView_0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = Form0.getString_0(107395936);
			this.Text = Form0.getString_0(107395947);
			((ISupportInitialize)this.fastObjectListView_0).EndInit();
			base.ResumeLayout(false);
		}

		static Form0()
		{
			Strings.CreateGetStringDelegate(typeof(Form0));
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
