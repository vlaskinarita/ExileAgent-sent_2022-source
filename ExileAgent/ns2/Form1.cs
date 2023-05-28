using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ns0;
using ns15;
using ns32;
using PoEv2;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns2
{
	internal sealed partial class Form1 : Form
	{
		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr intptr_0, int int_0);

		[DllImport("user32.dll")]
		public static extern bool HideCaret(IntPtr intptr_0);

		public RichTextBox ChatBox
		{
			get
			{
				return this.richTextBox_0;
			}
		}

		public Form1()
		{
			this.method_4();
			int windowLong = Form1.GetWindowLong(base.Handle, -20);
			Form1.SetWindowLong(base.Handle, -20, windowLong | 524288 | 32);
			this.method_0(UI.struct2_0);
			Form1.HideCaret(this.richTextBox_0.Handle);
			base.Hide();
		}

		private void timer_0_Tick(object sender, EventArgs e)
		{
			base.Invoke(new Action(this.method_5));
			Struct2 struct2_;
			UI.GetWindowRect(UI.intptr_0, out struct2_);
			this.method_0(struct2_);
		}

		private void method_0(Struct2 struct2_0)
		{
			Form1.Class101 @class = new Form1.Class101();
			@class.form1_0 = this;
			@class.struct2_0 = struct2_0;
			base.Invoke(new Action(@class.method_0));
		}

		public void method_1()
		{
			base.Invoke(new Action(this.method_6));
		}

		public void method_2()
		{
			base.Invoke(new Action(this.method_7));
		}

		public void method_3()
		{
			base.Invoke(new Action(this.method_8));
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_4()
		{
			this.icontainer_0 = new Container();
			this.timer_0 = new Timer(this.icontainer_0);
			this.richTextBox_0 = new RichTextBox();
			base.SuspendLayout();
			this.timer_0.Tick += this.timer_0_Tick;
			this.richTextBox_0.BackColor = Color.Black;
			this.richTextBox_0.BorderStyle = BorderStyle.None;
			this.richTextBox_0.Font = new Font(Form1.getString_0(107397747), 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox_0.ForeColor = Color.White;
			this.richTextBox_0.Location = new Point(417, 2);
			this.richTextBox_0.Name = Form1.getString_0(107361032);
			this.richTextBox_0.ReadOnly = true;
			this.richTextBox_0.ScrollBars = RichTextBoxScrollBars.None;
			this.richTextBox_0.Size = new Size(380, 220);
			this.richTextBox_0.TabIndex = 0;
			this.richTextBox_0.Text = Form1.getString_0(107396762);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Wheat;
			base.ClientSize = new Size(800, 600);
			base.Controls.Add(this.richTextBox_0);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = Form1.getString_0(107360987);
			base.Opacity = 0.4;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.TransparencyKey = Color.Wheat;
			base.ResumeLayout(false);
		}

		[CompilerGenerated]
		private void method_5()
		{
			Form1.HideCaret(this.richTextBox_0.Handle);
			this.richTextBox_0.Visible = (UI.IsForeground || UI.IsFormForeground);
		}

		[CompilerGenerated]
		private void method_6()
		{
			base.Show();
			this.timer_0.Enabled = true;
		}

		[CompilerGenerated]
		private void method_7()
		{
			base.Hide();
			this.timer_0.Enabled = false;
		}

		[CompilerGenerated]
		private void method_8()
		{
			double opacity = (double)Class255.class105_0.method_5(ConfigOptions.ChatOpacity) / 10.0;
			base.Opacity = opacity;
		}

		static Form1()
		{
			Strings.CreateGetStringDelegate(typeof(Form1));
		}

		private IContainer icontainer_0 = null;

		private Timer timer_0;

		private RichTextBox richTextBox_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class101
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[16];
				this.form1_0.Size = new Size(this.struct2_0.int_2 - this.struct2_0.int_0, this.struct2_0.int_3 - this.struct2_0.int_1);
				this.form1_0.Top = this.struct2_0.int_1;
				this.form1_0.Left = this.struct2_0.int_0;
				*(double*)ptr = 0.52125;
				*(double*)((byte*)ptr + 8) = 0.0033333333333333335;
				this.form1_0.richTextBox_0.Location = new Point((int)Math.Round(*(double*)ptr * (double)this.struct2_0.width + Class251.WindowOffset.X / 2.0), (int)Math.Round(*(double*)((byte*)ptr + 8) * (double)this.struct2_0.height + Class251.WindowOffset.Y));
			}

			public Form1 form1_0;

			public Struct2 struct2_0;
		}
	}
}
