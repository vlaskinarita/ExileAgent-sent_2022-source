using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ns0;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns16
{
	internal sealed partial class Form2 : Form
	{
		public Form2()
		{
			this.method_1();
		}

		private void Form2_Move(object sender, EventArgs e)
		{
			Class255.class105_0.method_9(ConfigOptions.PopoutChatLocation, base.Location, false);
		}

		public void method_0()
		{
			base.Move += this.Form2_Move;
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_1()
		{
			this.richTextBox_0 = new RichTextBox();
			base.SuspendLayout();
			this.richTextBox_0.BackColor = Color.Black;
			this.richTextBox_0.BorderStyle = BorderStyle.FixedSingle;
			this.richTextBox_0.Dock = DockStyle.Fill;
			this.richTextBox_0.Font = new Font(Form2.getString_0(107398080), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox_0.ForeColor = SystemColors.ActiveCaptionText;
			this.richTextBox_0.HideSelection = false;
			this.richTextBox_0.Location = new Point(0, 0);
			this.richTextBox_0.Name = Form2.getString_0(107372229);
			this.richTextBox_0.ReadOnly = true;
			this.richTextBox_0.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.richTextBox_0.Size = new Size(563, 265);
			this.richTextBox_0.TabIndex = 8;
			this.richTextBox_0.Text = Form2.getString_0(107397095);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(563, 265);
			base.Controls.Add(this.richTextBox_0);
			base.Name = Form2.getString_0(107372248);
			this.Text = Form2.getString_0(107372199);
			base.ResumeLayout(false);
		}

		static Form2()
		{
			Strings.CreateGetStringDelegate(typeof(Form2));
		}

		private IContainer icontainer_0 = null;

		public RichTextBox richTextBox_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
