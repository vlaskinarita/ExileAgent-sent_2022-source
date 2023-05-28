using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ns0;
using ns1;
using ns6;
using ns8;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.BuyingForms
{
	public sealed partial class LiveSearchBuyForm : Form
	{
		public LiveSearchBuyForm(MainForm form, LiveSearchListItem buyData)
		{
			this.mainForm_0 = form;
			this.liveSearchListItem_0 = buyData;
			this.method_6();
			this.textBox_1.KeyPress += Util.smethod_32;
		}

		public void method_0()
		{
			this.Text = this.liveSearchListItem_0.Description;
			this.method_1();
			this.method_4();
			this.method_2();
			this.method_3();
			this.numericUpDown_3.Focus();
			this.numericUpDown_3.Select(10, 0);
		}

		private void method_1()
		{
			int num = Class255.LiveSearchList.IndexOf(this.liveSearchListItem_0);
			this.button_2.Visible = (num > 0);
			this.button_1.Visible = (Class255.LiveSearchList.Count - 1 > num);
		}

		private unsafe void method_2()
		{
			void* ptr = stackalloc byte[6];
			*(byte*)ptr = (this.liveSearchListItem_0.PriceCurrency.smethod_25() ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.liveSearchListItem_0.PriceCurrency = LiveSearchBuyForm.getString_0(107394247);
			}
			((byte*)ptr)[1] = (this.liveSearchListItem_0.AutoPriceCurrency.smethod_25() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.liveSearchListItem_0.AutoPriceCurrency = LiveSearchBuyForm.getString_0(107394247);
			}
			((byte*)ptr)[2] = (this.liveSearchListItem_0.Stash.smethod_25() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				this.liveSearchListItem_0.Stash = LiveSearchBuyForm.getString_0(107354241);
			}
			((byte*)ptr)[3] = ((this.liveSearchListItem_0.MaxListingSize == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				this.liveSearchListItem_0.MaxListingSize = 100;
			}
			((byte*)ptr)[4] = ((this.liveSearchListItem_0.PriceSkip == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				this.liveSearchListItem_0.PriceSkip = 4;
			}
			((byte*)ptr)[5] = ((this.liveSearchListItem_0.PriceTake == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				this.liveSearchListItem_0.PriceTake = 2;
			}
			ComboBox.ObjectCollection items = this.comboBox_1.Items;
			object[] items2 = Class391.string_0;
			items.AddRange(items2);
			ComboBox.ObjectCollection items3 = this.comboBox_0.Items;
			items2 = Class102.string_7;
			items3.AddRange(items2);
			ComboBox.ObjectCollection items4 = this.comboBox_3.Items;
			items2 = Class102.string_7;
			items4.AddRange(items2);
			this.linkLabel_0.Text = this.liveSearchListItem_0.Id;
			this.textBox_0.Text = this.liveSearchListItem_0.Description;
			this.numericUpDown_3.smethod_2(this.liveSearchListItem_0.MaxPriceAmount);
			this.comboBox_1.SelectedItem = this.liveSearchListItem_0.MaxPriceCurrency;
			this.checkBox_1.Checked = this.liveSearchListItem_0.PriceAfterBuying;
			this.checkBox_0.Checked = this.liveSearchListItem_0.AutoPrice;
			this.checkBox_3.Checked = this.liveSearchListItem_0.PriceAsBulk;
			this.textBox_1.Text = this.liveSearchListItem_0.PriceAmount;
			this.comboBox_0.SelectedItem = this.liveSearchListItem_0.PriceCurrency;
			this.numericUpDown_2.smethod_2(this.liveSearchListItem_0.PriceSkip);
			this.numericUpDown_1.smethod_2(this.liveSearchListItem_0.PriceTake);
			this.numericUpDown_4.smethod_2(this.liveSearchListItem_0.MaxListingSize);
			this.comboBox_3.SelectedItem = this.liveSearchListItem_0.AutoPriceCurrency;
			this.checkBox_5.Checked = this.liveSearchListItem_0.DisableAfterPurchase;
			this.checkBox_4.Checked = this.liveSearchListItem_0.DisableAfterStashFull;
			this.numericUpDown_0.smethod_2(this.liveSearchListItem_0.StockLimit);
			this.comboBox_2.SelectedItem = this.liveSearchListItem_0.Stash;
			this.checkBox_2.Checked = this.liveSearchListItem_0.TurnInCard;
		}

		private void method_3()
		{
			this.checkBox_1_CheckedChanged(null, null);
		}

		private void method_4()
		{
			this.comboBox_2.Items.Clear();
			this.comboBox_2.Items.Add(LiveSearchBuyForm.getString_0(107354241));
			if (Stashes.Tabs != null && Stashes.Tabs.Any<JsonTab>())
			{
				this.comboBox_2.Items.smethod_22(Stashes.Tabs.Where(new Func<JsonTab, bool>(LiveSearchBuyForm.<>c.<>9.method_0)).Select(new Func<JsonTab, string>(LiveSearchBuyForm.<>c.<>9.method_1)));
			}
		}

		private void button_0_Click(object sender, EventArgs e)
		{
			this.method_5();
			base.Close();
		}

		private unsafe void method_5()
		{
			void* ptr = stackalloc byte[5];
			this.liveSearchListItem_0.Description = this.textBox_0.Text;
			this.liveSearchListItem_0.MaxPriceAmount = this.numericUpDown_3.Value;
			this.liveSearchListItem_0.MaxPriceCurrency = this.comboBox_1.smethod_1();
			this.liveSearchListItem_0.PriceAfterBuying = this.checkBox_1.Checked;
			this.liveSearchListItem_0.AutoPrice = this.checkBox_0.Checked;
			this.liveSearchListItem_0.PriceAsBulk = this.checkBox_3.Checked;
			this.liveSearchListItem_0.PriceAmount = this.textBox_1.Text;
			this.liveSearchListItem_0.PriceCurrency = this.comboBox_0.smethod_1();
			this.liveSearchListItem_0.PriceSkip = (int)this.numericUpDown_2.Value;
			this.liveSearchListItem_0.PriceTake = (int)this.numericUpDown_1.Value;
			this.liveSearchListItem_0.MaxListingSize = (int)this.numericUpDown_4.Value;
			this.liveSearchListItem_0.AutoPriceCurrency = this.comboBox_3.smethod_1();
			this.liveSearchListItem_0.DisableAfterPurchase = this.checkBox_5.Checked;
			this.liveSearchListItem_0.DisableAfterStashFull = this.checkBox_4.Checked;
			this.liveSearchListItem_0.StockLimit = (int)this.numericUpDown_0.Value;
			this.liveSearchListItem_0.Stash = this.comboBox_2.smethod_1();
			this.liveSearchListItem_0.TurnInCard = this.checkBox_2.Checked;
			List<LiveSearchListItem> list = Class255.LiveSearchList.ToList<LiveSearchListItem>();
			LiveSearchListItem liveSearchListItem = list.FirstOrDefault(new Func<LiveSearchListItem, bool>(this.method_7));
			((byte*)ptr)[4] = ((liveSearchListItem == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				*(int*)ptr = list.IndexOf(liveSearchListItem);
				this.mainForm_0.dataGridView_1.CellValueChanged -= this.mainForm_0.dataGridView_1_CellValueChanged;
				DataGridViewRow dataGridViewRow = this.mainForm_0.dataGridView_1.Rows[*(int*)ptr];
				dataGridViewRow.Cells[LiveSearchBuyForm.getString_0(107408705)].Value = this.liveSearchListItem_0.Description;
				dataGridViewRow.Cells[LiveSearchBuyForm.getString_0(107399237)].Value = this.liveSearchListItem_0.StockLimit;
				dataGridViewRow.Cells[LiveSearchBuyForm.getString_0(107399331)].Value = this.liveSearchListItem_0.MaxPriceAmount;
				dataGridViewRow.Cells[LiveSearchBuyForm.getString_0(107399274)].Value = this.liveSearchListItem_0.MaxPriceCurrency;
				DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[LiveSearchBuyForm.getString_0(107399208)];
				DataGridViewCell dataGridViewCell2 = dataGridViewRow.Cells[LiveSearchBuyForm.getString_0(107408356)];
				dataGridViewCell.Value = this.liveSearchListItem_0.PriceAmount;
				dataGridViewCell2.Value = this.liveSearchListItem_0.PriceCurrency;
				dataGridViewCell.smethod_26(this.liveSearchListItem_0.PriceAfterBuying && !this.liveSearchListItem_0.AutoPrice);
				dataGridViewCell2.smethod_26(this.liveSearchListItem_0.PriceAfterBuying && !this.liveSearchListItem_0.AutoPrice);
				this.mainForm_0.dataGridView_1.CellValueChanged += this.mainForm_0.dataGridView_1_CellValueChanged;
				list[*(int*)ptr] = this.liveSearchListItem_0;
				Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, true);
			}
		}

		private void checkBox_1_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_1.Checked;
			this.checkBox_0.Enabled = @checked;
			this.textBox_1.Enabled = @checked;
			this.comboBox_0.Enabled = @checked;
			this.checkBox_0_CheckedChanged(null, null);
		}

		private void checkBox_0_CheckedChanged(object sender, EventArgs e)
		{
			bool enabled = this.checkBox_0.Checked && this.checkBox_1.Checked;
			if (this.checkBox_1.Checked)
			{
				this.textBox_1.Enabled = !this.checkBox_0.Checked;
				this.comboBox_0.Enabled = !this.checkBox_0.Checked;
			}
			this.checkBox_3.Enabled = enabled;
			this.numericUpDown_2.Enabled = enabled;
			this.numericUpDown_1.Enabled = enabled;
			this.comboBox_3.Enabled = enabled;
			this.checkBox_3_CheckedChanged(null, null);
		}

		private void linkLabel_0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(Class103.TradeWebsiteUrl + LiveSearchBuyForm.getString_0(107374872) + this.liveSearchListItem_0.Id);
		}

		private unsafe void button_1_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = Class255.LiveSearchList.IndexOf(this.liveSearchListItem_0);
			((byte*)ptr)[4] = ((Class255.LiveSearchList.Count - 1 == *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				LiveSearchListItem liveSearchListItem = Class255.LiveSearchList[*(int*)ptr + 1];
				this.method_5();
				this.liveSearchListItem_0 = liveSearchListItem;
				this.method_0();
			}
		}

		private unsafe void button_2_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = Class255.LiveSearchList.IndexOf(this.liveSearchListItem_0);
			((byte*)ptr)[4] = ((*(int*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				LiveSearchListItem liveSearchListItem = Class255.LiveSearchList[*(int*)ptr - 1];
				this.method_5();
				this.liveSearchListItem_0 = liveSearchListItem;
				this.method_0();
			}
		}

		private void checkBox_3_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_4.Enabled = (this.checkBox_3.Checked && this.checkBox_3.Enabled);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_6()
		{
			this.label_0 = new Label();
			this.label_1 = new Label();
			this.groupBox_0 = new GroupBox();
			this.linkLabel_0 = new LinkLabel();
			this.numericUpDown_3 = new NumericUpDown();
			this.comboBox_1 = new ComboBox();
			this.label_3 = new Label();
			this.textBox_0 = new TextBox();
			this.groupBox_1 = new GroupBox();
			this.comboBox_3 = new ComboBox();
			this.label_9 = new Label();
			this.numericUpDown_4 = new NumericUpDown();
			this.label_8 = new Label();
			this.checkBox_3 = new CheckBox();
			this.numericUpDown_1 = new NumericUpDown();
			this.numericUpDown_2 = new NumericUpDown();
			this.label_6 = new Label();
			this.label_7 = new Label();
			this.comboBox_0 = new ComboBox();
			this.textBox_1 = new TextBox();
			this.label_2 = new Label();
			this.checkBox_0 = new CheckBox();
			this.checkBox_1 = new CheckBox();
			this.groupBox_2 = new GroupBox();
			this.comboBox_2 = new ComboBox();
			this.label_5 = new Label();
			this.checkBox_2 = new CheckBox();
			this.numericUpDown_0 = new NumericUpDown();
			this.label_4 = new Label();
			this.button_0 = new Button();
			this.button_1 = new Button();
			this.button_2 = new Button();
			this.groupBox_3 = new GroupBox();
			this.checkBox_4 = new CheckBox();
			this.checkBox_5 = new CheckBox();
			this.groupBox_0.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_3).BeginInit();
			this.groupBox_1.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_4).BeginInit();
			((ISupportInitialize)this.numericUpDown_1).BeginInit();
			((ISupportInitialize)this.numericUpDown_2).BeginInit();
			this.groupBox_2.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_0).BeginInit();
			this.groupBox_3.SuspendLayout();
			base.SuspendLayout();
			this.label_0.AutoSize = true;
			this.label_0.Font = new Font(LiveSearchBuyForm.getString_0(107400585), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_0.Location = new Point(3, 19);
			this.label_0.Name = LiveSearchBuyForm.getString_0(107388804);
			this.label_0.Size = new Size(57, 14);
			this.label_0.TabIndex = 0;
			this.label_0.Text = LiveSearchBuyForm.getString_0(107230835);
			this.label_1.AutoSize = true;
			this.label_1.Location = new Point(3, 43);
			this.label_1.Name = LiveSearchBuyForm.getString_0(107388862);
			this.label_1.Size = new Size(73, 14);
			this.label_1.TabIndex = 1;
			this.label_1.Text = LiveSearchBuyForm.getString_0(107408278);
			this.groupBox_0.Controls.Add(this.linkLabel_0);
			this.groupBox_0.Controls.Add(this.numericUpDown_3);
			this.groupBox_0.Controls.Add(this.comboBox_1);
			this.groupBox_0.Controls.Add(this.label_3);
			this.groupBox_0.Controls.Add(this.textBox_0);
			this.groupBox_0.Controls.Add(this.label_0);
			this.groupBox_0.Controls.Add(this.label_1);
			this.groupBox_0.Location = new Point(3, 3);
			this.groupBox_0.Name = LiveSearchBuyForm.getString_0(107389984);
			this.groupBox_0.Size = new Size(312, 93);
			this.groupBox_0.TabIndex = 2;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Text = LiveSearchBuyForm.getString_0(107231798);
			this.linkLabel_0.AutoSize = true;
			this.linkLabel_0.Location = new Point(56, 19);
			this.linkLabel_0.Name = LiveSearchBuyForm.getString_0(107230850);
			this.linkLabel_0.Size = new Size(52, 14);
			this.linkLabel_0.TabIndex = 9;
			this.linkLabel_0.TabStop = true;
			this.linkLabel_0.Text = LiveSearchBuyForm.getString_0(107230837);
			this.linkLabel_0.LinkClicked += this.linkLabel_0_LinkClicked;
			this.numericUpDown_3.DecimalPlaces = 1;
			NumericUpDown numericUpDown = this.numericUpDown_3;
			int[] array = new int[4];
			array[0] = 5;
			numericUpDown.Increment = new decimal(array);
			this.numericUpDown_3.Location = new Point(69, 66);
			NumericUpDown numericUpDown2 = this.numericUpDown_3;
			int[] array2 = new int[4];
			array2[0] = 100000;
			numericUpDown2.Maximum = new decimal(array2);
			this.numericUpDown_3.Name = LiveSearchBuyForm.getString_0(107231134);
			this.numericUpDown_3.Size = new Size(60, 20);
			this.numericUpDown_3.TabIndex = 8;
			this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_1.FormattingEnabled = true;
			this.comboBox_1.Location = new Point(135, 65);
			this.comboBox_1.Name = LiveSearchBuyForm.getString_0(107230792);
			this.comboBox_1.Size = new Size(150, 22);
			this.comboBox_1.TabIndex = 7;
			this.label_3.AutoSize = true;
			this.label_3.Location = new Point(3, 69);
			this.label_3.Name = LiveSearchBuyForm.getString_0(107388936);
			this.label_3.Size = new Size(63, 14);
			this.label_3.TabIndex = 5;
			this.label_3.Text = LiveSearchBuyForm.getString_0(107231085);
			this.textBox_0.Location = new Point(82, 40);
			this.textBox_0.Name = LiveSearchBuyForm.getString_0(107230759);
			this.textBox_0.Size = new Size(186, 20);
			this.textBox_0.TabIndex = 3;
			this.groupBox_1.Controls.Add(this.comboBox_3);
			this.groupBox_1.Controls.Add(this.label_9);
			this.groupBox_1.Controls.Add(this.numericUpDown_4);
			this.groupBox_1.Controls.Add(this.label_8);
			this.groupBox_1.Controls.Add(this.checkBox_3);
			this.groupBox_1.Controls.Add(this.numericUpDown_1);
			this.groupBox_1.Controls.Add(this.numericUpDown_2);
			this.groupBox_1.Controls.Add(this.label_6);
			this.groupBox_1.Controls.Add(this.label_7);
			this.groupBox_1.Controls.Add(this.comboBox_0);
			this.groupBox_1.Controls.Add(this.textBox_1);
			this.groupBox_1.Controls.Add(this.label_2);
			this.groupBox_1.Controls.Add(this.checkBox_0);
			this.groupBox_1.Controls.Add(this.checkBox_1);
			this.groupBox_1.Location = new Point(3, 102);
			this.groupBox_1.Name = LiveSearchBuyForm.getString_0(107391880);
			this.groupBox_1.Size = new Size(312, 214);
			this.groupBox_1.TabIndex = 3;
			this.groupBox_1.TabStop = false;
			this.groupBox_1.Text = LiveSearchBuyForm.getString_0(107355621);
			this.comboBox_3.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_3.FormattingEnabled = true;
			this.comboBox_3.Location = new Point(157, 110);
			this.comboBox_3.Name = LiveSearchBuyForm.getString_0(107231505);
			this.comboBox_3.Size = new Size(150, 22);
			this.comboBox_3.TabIndex = 22;
			this.label_9.AutoSize = true;
			this.label_9.Location = new Point(3, 114);
			this.label_9.Name = LiveSearchBuyForm.getString_0(107389565);
			this.label_9.Size = new Size(122, 14);
			this.label_9.TabIndex = 21;
			this.label_9.Text = LiveSearchBuyForm.getString_0(107231508);
			this.numericUpDown_4.Location = new Point(157, 189);
			NumericUpDown numericUpDown3 = this.numericUpDown_4;
			int[] array3 = new int[4];
			array3[0] = 60000;
			numericUpDown3.Maximum = new decimal(array3);
			NumericUpDown numericUpDown4 = this.numericUpDown_4;
			int[] array4 = new int[4];
			array4[0] = 1;
			numericUpDown4.Minimum = new decimal(array4);
			this.numericUpDown_4.Name = LiveSearchBuyForm.getString_0(107231100);
			this.numericUpDown_4.Size = new Size(56, 20);
			this.numericUpDown_4.TabIndex = 20;
			NumericUpDown numericUpDown5 = this.numericUpDown_4;
			int[] array5 = new int[4];
			array5[0] = 1;
			numericUpDown5.Value = new decimal(array5);
			this.label_8.AutoSize = true;
			this.label_8.Location = new Point(3, 191);
			this.label_8.Name = LiveSearchBuyForm.getString_0(107231075);
			this.label_8.Size = new Size(95, 14);
			this.label_8.TabIndex = 19;
			this.label_8.Text = LiveSearchBuyForm.getString_0(107231018);
			this.checkBox_3.AutoSize = true;
			this.checkBox_3.Location = new Point(5, 67);
			this.checkBox_3.Name = LiveSearchBuyForm.getString_0(107230226);
			this.checkBox_3.Size = new Size(115, 18);
			this.checkBox_3.TabIndex = 18;
			this.checkBox_3.Text = LiveSearchBuyForm.getString_0(107230237);
			this.checkBox_3.UseVisualStyleBackColor = true;
			this.checkBox_3.CheckedChanged += this.checkBox_3_CheckedChanged;
			this.numericUpDown_1.Location = new Point(157, 163);
			NumericUpDown numericUpDown6 = this.numericUpDown_1;
			int[] array6 = new int[4];
			array6[0] = 10;
			numericUpDown6.Maximum = new decimal(array6);
			NumericUpDown numericUpDown7 = this.numericUpDown_1;
			int[] array7 = new int[4];
			array7[0] = 1;
			numericUpDown7.Minimum = new decimal(array7);
			this.numericUpDown_1.Name = LiveSearchBuyForm.getString_0(107231479);
			this.numericUpDown_1.Size = new Size(56, 20);
			this.numericUpDown_1.TabIndex = 17;
			NumericUpDown numericUpDown8 = this.numericUpDown_1;
			int[] array8 = new int[4];
			array8[0] = 1;
			numericUpDown8.Value = new decimal(array8);
			this.numericUpDown_2.Location = new Point(157, 139);
			NumericUpDown numericUpDown9 = this.numericUpDown_2;
			int[] array9 = new int[4];
			array9[0] = 50;
			numericUpDown9.Maximum = new decimal(array9);
			this.numericUpDown_2.Name = LiveSearchBuyForm.getString_0(107231430);
			this.numericUpDown_2.Size = new Size(56, 20);
			this.numericUpDown_2.TabIndex = 16;
			this.label_6.AutoSize = true;
			this.label_6.Location = new Point(3, 165);
			this.label_6.Name = LiveSearchBuyForm.getString_0(107391299);
			this.label_6.Size = new Size(126, 14);
			this.label_6.TabIndex = 15;
			this.label_6.Text = LiveSearchBuyForm.getString_0(107231445);
			this.label_7.AutoSize = true;
			this.label_7.Location = new Point(3, 141);
			this.label_7.Name = LiveSearchBuyForm.getString_0(107391301);
			this.label_7.Size = new Size(126, 14);
			this.label_7.TabIndex = 14;
			this.label_7.Text = LiveSearchBuyForm.getString_0(107231412);
			this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Location = new Point(157, 85);
			this.comboBox_0.Name = LiveSearchBuyForm.getString_0(107231347);
			this.comboBox_0.Size = new Size(150, 22);
			this.comboBox_0.TabIndex = 4;
			this.textBox_1.Location = new Point(91, 85);
			this.textBox_1.MinimumSize = new Size(4, 22);
			this.textBox_1.Name = LiveSearchBuyForm.getString_0(107231310);
			this.textBox_1.Size = new Size(60, 20);
			this.textBox_1.TabIndex = 3;
			this.label_2.AutoSize = true;
			this.label_2.Location = new Point(3, 89);
			this.label_2.Name = LiveSearchBuyForm.getString_0(107389399);
			this.label_2.Size = new Size(85, 14);
			this.label_2.TabIndex = 2;
			this.label_2.Text = LiveSearchBuyForm.getString_0(107231321);
			this.checkBox_0.AutoSize = true;
			this.checkBox_0.Location = new Point(6, 43);
			this.checkBox_0.Name = LiveSearchBuyForm.getString_0(107356023);
			this.checkBox_0.Size = new Size(83, 18);
			this.checkBox_0.TabIndex = 1;
			this.checkBox_0.Text = LiveSearchBuyForm.getString_0(107231268);
			this.checkBox_0.UseVisualStyleBackColor = true;
			this.checkBox_0.CheckedChanged += this.checkBox_0_CheckedChanged;
			this.checkBox_1.AutoSize = true;
			this.checkBox_1.Location = new Point(6, 19);
			this.checkBox_1.Name = LiveSearchBuyForm.getString_0(107230739);
			this.checkBox_1.Size = new Size(123, 18);
			this.checkBox_1.TabIndex = 0;
			this.checkBox_1.Text = LiveSearchBuyForm.getString_0(107230746);
			this.checkBox_1.UseVisualStyleBackColor = true;
			this.checkBox_1.CheckedChanged += this.checkBox_1_CheckedChanged;
			this.groupBox_2.Controls.Add(this.comboBox_2);
			this.groupBox_2.Controls.Add(this.label_5);
			this.groupBox_2.Controls.Add(this.checkBox_2);
			this.groupBox_2.Controls.Add(this.numericUpDown_0);
			this.groupBox_2.Controls.Add(this.label_4);
			this.groupBox_2.Location = new Point(3, 393);
			this.groupBox_2.Name = LiveSearchBuyForm.getString_0(107416800);
			this.groupBox_2.Size = new Size(312, 89);
			this.groupBox_2.TabIndex = 4;
			this.groupBox_2.TabStop = false;
			this.groupBox_2.Text = LiveSearchBuyForm.getString_0(107367881);
			this.comboBox_2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_2.FormattingEnabled = true;
			this.comboBox_2.Location = new Point(82, 36);
			this.comboBox_2.Name = LiveSearchBuyForm.getString_0(107230721);
			this.comboBox_2.Size = new Size(150, 22);
			this.comboBox_2.TabIndex = 5;
			this.label_5.AutoSize = true;
			this.label_5.Location = new Point(3, 44);
			this.label_5.Name = LiveSearchBuyForm.getString_0(107391749);
			this.label_5.Size = new Size(76, 14);
			this.label_5.TabIndex = 20;
			this.label_5.Text = LiveSearchBuyForm.getString_0(107230668);
			this.checkBox_2.AutoSize = true;
			this.checkBox_2.Location = new Point(6, 69);
			this.checkBox_2.Name = LiveSearchBuyForm.getString_0(107230683);
			this.checkBox_2.Size = new Size(223, 18);
			this.checkBox_2.TabIndex = 19;
			this.checkBox_2.Text = LiveSearchBuyForm.getString_0(107230634);
			this.checkBox_2.UseVisualStyleBackColor = true;
			this.numericUpDown_0.Location = new Point(82, 15);
			NumericUpDown numericUpDown10 = this.numericUpDown_0;
			int[] array10 = new int[4];
			array10[0] = 100000;
			numericUpDown10.Maximum = new decimal(array10);
			this.numericUpDown_0.Name = LiveSearchBuyForm.getString_0(107230613);
			this.numericUpDown_0.Size = new Size(72, 20);
			this.numericUpDown_0.TabIndex = 18;
			this.label_4.AutoSize = true;
			this.label_4.Location = new Point(3, 16);
			this.label_4.Name = LiveSearchBuyForm.getString_0(107389486);
			this.label_4.Size = new Size(72, 14);
			this.label_4.TabIndex = 17;
			this.label_4.Text = LiveSearchBuyForm.getString_0(107230592);
			this.button_0.Location = new Point(219, 483);
			this.button_0.Name = LiveSearchBuyForm.getString_0(107230543);
			this.button_0.Size = new Size(96, 25);
			this.button_0.TabIndex = 9;
			this.button_0.Text = LiveSearchBuyForm.getString_0(107354517);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.button_1.Location = new Point(117, 483);
			this.button_1.Name = LiveSearchBuyForm.getString_0(107354496);
			this.button_1.Size = new Size(96, 25);
			this.button_1.TabIndex = 10;
			this.button_1.Text = LiveSearchBuyForm.getString_0(107354447);
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			this.button_2.Location = new Point(15, 483);
			this.button_2.Name = LiveSearchBuyForm.getString_0(107354458);
			this.button_2.Size = new Size(96, 25);
			this.button_2.TabIndex = 22;
			this.button_2.Text = LiveSearchBuyForm.getString_0(107354409);
			this.button_2.UseVisualStyleBackColor = true;
			this.button_2.Click += this.button_2_Click;
			this.groupBox_3.Controls.Add(this.checkBox_4);
			this.groupBox_3.Controls.Add(this.checkBox_5);
			this.groupBox_3.Location = new Point(3, 322);
			this.groupBox_3.Name = LiveSearchBuyForm.getString_0(107416410);
			this.groupBox_3.Size = new Size(312, 65);
			this.groupBox_3.TabIndex = 21;
			this.groupBox_3.TabStop = false;
			this.groupBox_3.Text = LiveSearchBuyForm.getString_0(107230558);
			this.checkBox_4.AutoSize = true;
			this.checkBox_4.Location = new Point(5, 43);
			this.checkBox_4.Name = LiveSearchBuyForm.getString_0(107230505);
			this.checkBox_4.Size = new Size(182, 18);
			this.checkBox_4.TabIndex = 20;
			this.checkBox_4.Text = LiveSearchBuyForm.getString_0(107230984);
			this.checkBox_4.UseVisualStyleBackColor = true;
			this.checkBox_5.AutoSize = true;
			this.checkBox_5.Location = new Point(5, 19);
			this.checkBox_5.Name = LiveSearchBuyForm.getString_0(107230971);
			this.checkBox_5.Size = new Size(174, 18);
			this.checkBox_5.TabIndex = 19;
			this.checkBox_5.Text = LiveSearchBuyForm.getString_0(107230938);
			this.checkBox_5.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(7f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(316, 509);
			base.Controls.Add(this.groupBox_3);
			base.Controls.Add(this.button_2);
			base.Controls.Add(this.button_1);
			base.Controls.Add(this.button_0);
			base.Controls.Add(this.groupBox_2);
			base.Controls.Add(this.groupBox_1);
			base.Controls.Add(this.groupBox_0);
			this.Font = new Font(LiveSearchBuyForm.getString_0(107400585), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = LiveSearchBuyForm.getString_0(107230163);
			this.Text = LiveSearchBuyForm.getString_0(107230852);
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_0.PerformLayout();
			((ISupportInitialize)this.numericUpDown_3).EndInit();
			this.groupBox_1.ResumeLayout(false);
			this.groupBox_1.PerformLayout();
			((ISupportInitialize)this.numericUpDown_4).EndInit();
			((ISupportInitialize)this.numericUpDown_1).EndInit();
			((ISupportInitialize)this.numericUpDown_2).EndInit();
			this.groupBox_2.ResumeLayout(false);
			this.groupBox_2.PerformLayout();
			((ISupportInitialize)this.numericUpDown_0).EndInit();
			this.groupBox_3.ResumeLayout(false);
			this.groupBox_3.PerformLayout();
			base.ResumeLayout(false);
		}

		[CompilerGenerated]
		private bool method_7(LiveSearchListItem liveSearchListItem_1)
		{
			return liveSearchListItem_1.Id == this.liveSearchListItem_0.Id;
		}

		static LiveSearchBuyForm()
		{
			Strings.CreateGetStringDelegate(typeof(LiveSearchBuyForm));
		}

		private MainForm mainForm_0;

		private LiveSearchListItem liveSearchListItem_0;

		private IContainer icontainer_0 = null;

		private Label label_0;

		private Label label_1;

		private GroupBox groupBox_0;

		private TextBox textBox_0;

		private GroupBox groupBox_1;

		private CheckBox checkBox_0;

		private CheckBox checkBox_1;

		private TextBox textBox_1;

		private Label label_2;

		private ComboBox comboBox_0;

		private ComboBox comboBox_1;

		private Label label_3;

		private GroupBox groupBox_2;

		private NumericUpDown numericUpDown_0;

		private Label label_4;

		private ComboBox comboBox_2;

		private Label label_5;

		private CheckBox checkBox_2;

		private NumericUpDown numericUpDown_1;

		private NumericUpDown numericUpDown_2;

		private Label label_6;

		private Label label_7;

		private NumericUpDown numericUpDown_3;

		private Button button_0;

		private LinkLabel linkLabel_0;

		private Button button_1;

		private Button button_2;

		private NumericUpDown numericUpDown_4;

		private Label label_8;

		private CheckBox checkBox_3;

		private ComboBox comboBox_3;

		private Label label_9;

		private GroupBox groupBox_3;

		private CheckBox checkBox_4;

		private CheckBox checkBox_5;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
