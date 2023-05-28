namespace BrightIdeasSoftware
{
	public sealed partial class ColumnSelectionForm : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.buttonMoveUp = new global::System.Windows.Forms.Button();
			this.buttonMoveDown = new global::System.Windows.Forms.Button();
			this.buttonShow = new global::System.Windows.Forms.Button();
			this.buttonHide = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.objectListView1 = new global::BrightIdeasSoftware.ObjectListView();
			this.olvColumn1 = new global::BrightIdeasSoftware.OLVColumn();
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).BeginInit();
			base.SuspendLayout();
			this.buttonMoveUp.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonMoveUp.Location = new global::System.Drawing.Point(295, 31);
			this.buttonMoveUp.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314356);
			this.buttonMoveUp.Size = new global::System.Drawing.Size(87, 23);
			this.buttonMoveUp.TabIndex = 1;
			this.buttonMoveUp.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314307);
			this.buttonMoveUp.UseVisualStyleBackColor = true;
			this.buttonMoveUp.Click += new global::System.EventHandler(this.buttonMoveUp_Click);
			this.buttonMoveDown.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonMoveDown.Location = new global::System.Drawing.Point(295, 60);
			this.buttonMoveDown.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314294);
			this.buttonMoveDown.Size = new global::System.Drawing.Size(87, 23);
			this.buttonMoveDown.TabIndex = 2;
			this.buttonMoveDown.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314273);
			this.buttonMoveDown.UseVisualStyleBackColor = true;
			this.buttonMoveDown.Click += new global::System.EventHandler(this.buttonMoveDown_Click);
			this.buttonShow.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonShow.Location = new global::System.Drawing.Point(295, 89);
			this.buttonShow.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314288);
			this.buttonShow.Size = new global::System.Drawing.Size(87, 23);
			this.buttonShow.TabIndex = 3;
			this.buttonShow.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314239);
			this.buttonShow.UseVisualStyleBackColor = true;
			this.buttonShow.Click += new global::System.EventHandler(this.buttonShow_Click);
			this.buttonHide.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonHide.Location = new global::System.Drawing.Point(295, 118);
			this.buttonHide.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314230);
			this.buttonHide.Size = new global::System.Drawing.Size(87, 23);
			this.buttonHide.TabIndex = 4;
			this.buttonHide.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314245);
			this.buttonHide.UseVisualStyleBackColor = true;
			this.buttonHide.Click += new global::System.EventHandler(this.buttonHide_Click);
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.label1.BackColor = global::System.Drawing.SystemColors.Control;
			this.label1.Location = new global::System.Drawing.Point(13, 9);
			this.label1.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107392565);
			this.label1.Size = new global::System.Drawing.Size(366, 19);
			this.label1.TabIndex = 5;
			this.label1.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314204);
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.Location = new global::System.Drawing.Point(198, 304);
			this.buttonOK.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314135);
			this.buttonOK.Size = new global::System.Drawing.Size(87, 23);
			this.buttonOK.TabIndex = 6;
			this.buttonOK.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314154);
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(295, 304);
			this.buttonCancel.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314149);
			this.buttonCancel.Size = new global::System.Drawing.Size(87, 23);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314644);
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.buttonCancel_Click);
			this.objectListView1.AllColumns.Add(this.olvColumn1);
			this.objectListView1.AlternateRowBackColor = global::System.Drawing.Color.FromArgb(192, 255, 192);
			this.objectListView1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.objectListView1.CellEditActivation = global::BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
			this.objectListView1.CheckBoxes = true;
			this.objectListView1.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.olvColumn1
			});
			this.objectListView1.FullRowSelect = true;
			this.objectListView1.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.objectListView1.HideSelection = false;
			this.objectListView1.Location = new global::System.Drawing.Point(12, 31);
			this.objectListView1.MultiSelect = false;
			this.objectListView1.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314631);
			this.objectListView1.ShowGroups = false;
			this.objectListView1.ShowSortIndicators = false;
			this.objectListView1.Size = new global::System.Drawing.Size(273, 259);
			this.objectListView1.TabIndex = 0;
			this.objectListView1.UseCompatibleStateImageBehavior = false;
			this.objectListView1.View = global::System.Windows.Forms.View.Details;
			this.objectListView1.SelectionChanged += new global::System.EventHandler(this.objectListView1_SelectionChanged);
			this.olvColumn1.AspectName = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107392151);
			this.olvColumn1.IsVisible = true;
			this.olvColumn1.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314610);
			this.olvColumn1.Width = 267;
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(391, 339);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.buttonHide);
			base.Controls.Add(this.buttonShow);
			base.Controls.Add(this.buttonMoveDown);
			base.Controls.Add(this.buttonMoveUp);
			base.Controls.Add(this.objectListView1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314601);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = global::BrightIdeasSoftware.ColumnSelectionForm.getString_0(107314572);
			((global::System.ComponentModel.ISupportInitialize)this.objectListView1).EndInit();
			base.ResumeLayout(false);
		}

		private global::System.ComponentModel.IContainer components;

		private global::BrightIdeasSoftware.ObjectListView objectListView1;

		private global::System.Windows.Forms.Button buttonMoveUp;

		private global::System.Windows.Forms.Button buttonMoveDown;

		private global::System.Windows.Forms.Button buttonShow;

		private global::System.Windows.Forms.Button buttonHide;

		private global::BrightIdeasSoftware.OLVColumn olvColumn1;

		private global::System.Windows.Forms.Label label1;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		[global::System.NonSerialized]
		internal static global::SmartAssembly.Delegates.GetString getString_0;
	}
}
