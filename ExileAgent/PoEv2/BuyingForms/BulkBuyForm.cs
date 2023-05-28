using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ns0;
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
	public sealed partial class BulkBuyForm : Form
	{
		public BulkBuyForm(MainForm form, BulkBuyingListItem buyData)
		{
			this.mainForm_0 = form;
			this.bulkBuyingListItem_0 = buyData;
			this.method_6();
			this.textBox_0.KeyPress += Util.smethod_32;
		}

		public void method_0()
		{
			this.Text = this.bulkBuyingListItem_0.HaveName + BulkBuyForm.getString_0(107458432) + this.bulkBuyingListItem_0.WantName;
			this.method_1();
			this.method_4();
			this.method_2();
			this.method_3();
			this.numericUpDown_3.Focus();
			this.numericUpDown_3.Select(10, 0);
		}

		private void method_1()
		{
			int num = Class255.BulkBuyingList.IndexOf(this.bulkBuyingListItem_0);
			this.button_2.Visible = (num > 0);
			this.button_1.Visible = (Class255.BulkBuyingList.Count - 1 > num);
		}

		private unsafe void method_2()
		{
			void* ptr = stackalloc byte[6];
			*(byte*)ptr = (this.bulkBuyingListItem_0.PriceCurrency.smethod_25() ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.bulkBuyingListItem_0.PriceCurrency = BulkBuyForm.getString_0(107394163);
			}
			((byte*)ptr)[1] = (this.bulkBuyingListItem_0.AutoPriceCurrency.smethod_25() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.bulkBuyingListItem_0.AutoPriceCurrency = BulkBuyForm.getString_0(107394163);
			}
			((byte*)ptr)[2] = (this.bulkBuyingListItem_0.Stash.smethod_25() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				this.bulkBuyingListItem_0.Stash = BulkBuyForm.getString_0(107354157);
			}
			((byte*)ptr)[3] = ((this.bulkBuyingListItem_0.MaxListingSize == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				this.bulkBuyingListItem_0.MaxListingSize = 100;
			}
			((byte*)ptr)[4] = ((this.bulkBuyingListItem_0.PriceSkip == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				this.bulkBuyingListItem_0.PriceSkip = 4;
			}
			((byte*)ptr)[5] = ((this.bulkBuyingListItem_0.PriceTake == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				this.bulkBuyingListItem_0.PriceTake = 2;
			}
			this.linkLabel_1.Text = BulkBuyForm.getString_0(107231700) + this.bulkBuyingListItem_0.HaveName;
			this.linkLabel_0.Text = BulkBuyForm.getString_0(107231723) + this.bulkBuyingListItem_0.WantName;
			this.label_6.Text = this.bulkBuyingListItem_0.HaveName;
			ComboBox.ObjectCollection items = this.comboBox_0.Items;
			object[] string_ = Class102.string_7;
			items.AddRange(string_);
			ComboBox.ObjectCollection items2 = this.comboBox_2.Items;
			string_ = Class102.string_7;
			items2.AddRange(string_);
			this.numericUpDown_4.smethod_2(this.bulkBuyingListItem_0.MinStock);
			this.numericUpDown_3.smethod_2(this.bulkBuyingListItem_0.MaxPriceAmount);
			this.checkBox_1.Checked = this.bulkBuyingListItem_0.PriceAfterBuying;
			this.checkBox_0.Checked = this.bulkBuyingListItem_0.AutoPrice;
			this.textBox_0.Text = this.bulkBuyingListItem_0.PriceAmount;
			this.comboBox_0.SelectedItem = this.bulkBuyingListItem_0.PriceCurrency;
			this.numericUpDown_2.smethod_2(this.bulkBuyingListItem_0.PriceSkip);
			this.numericUpDown_1.smethod_2(this.bulkBuyingListItem_0.PriceTake);
			this.numericUpDown_5.smethod_2(this.bulkBuyingListItem_0.MaxListingSize);
			this.comboBox_2.SelectedItem = this.bulkBuyingListItem_0.AutoPriceCurrency;
			this.checkBox_4.Checked = this.bulkBuyingListItem_0.DisableAfterPurchase;
			this.checkBox_3.Checked = this.bulkBuyingListItem_0.DisableAfterStashFull;
			this.numericUpDown_0.smethod_2(this.bulkBuyingListItem_0.StockLimit);
			this.comboBox_1.SelectedItem = this.bulkBuyingListItem_0.Stash;
			this.checkBox_2.Checked = this.bulkBuyingListItem_0.TurnInCard;
		}

		private void method_3()
		{
			this.checkBox_1_CheckedChanged(null, null);
		}

		private void method_4()
		{
			this.comboBox_1.Items.Clear();
			this.comboBox_1.Items.Add(BulkBuyForm.getString_0(107354157));
			if (Stashes.Tabs != null && Stashes.Tabs.Any<JsonTab>())
			{
				this.comboBox_1.Items.smethod_22(Stashes.Tabs.Where(new Func<JsonTab, bool>(BulkBuyForm.<>c.<>9.method_0)).Select(new Func<JsonTab, string>(BulkBuyForm.<>c.<>9.method_1)));
			}
		}

		private void button_0_Click(object sender, EventArgs e)
		{
			this.method_5();
			base.Close();
		}

		private void method_5()
		{
			this.bulkBuyingListItem_0.MinStock = (int)this.numericUpDown_4.Value;
			this.bulkBuyingListItem_0.MaxPriceAmount = this.numericUpDown_3.Value;
			this.bulkBuyingListItem_0.PriceAfterBuying = this.checkBox_1.Checked;
			this.bulkBuyingListItem_0.AutoPrice = this.checkBox_0.Checked;
			this.bulkBuyingListItem_0.PriceAmount = this.textBox_0.Text;
			this.bulkBuyingListItem_0.PriceCurrency = this.comboBox_0.smethod_1();
			this.bulkBuyingListItem_0.PriceSkip = (int)this.numericUpDown_2.Value;
			this.bulkBuyingListItem_0.PriceTake = (int)this.numericUpDown_1.Value;
			this.bulkBuyingListItem_0.MaxListingSize = (int)this.numericUpDown_5.Value;
			this.bulkBuyingListItem_0.AutoPriceCurrency = this.comboBox_2.smethod_1();
			this.bulkBuyingListItem_0.DisableAfterPurchase = this.checkBox_4.Checked;
			this.bulkBuyingListItem_0.DisableAfterStashFull = this.checkBox_3.Checked;
			this.bulkBuyingListItem_0.StockLimit = (int)this.numericUpDown_0.Value;
			this.bulkBuyingListItem_0.Stash = this.comboBox_1.smethod_1();
			this.bulkBuyingListItem_0.TurnInCard = this.checkBox_2.Checked;
			List<BulkBuyingListItem> list = Class255.BulkBuyingList.ToList<BulkBuyingListItem>();
			BulkBuyingListItem bulkBuyingListItem = list.FirstOrDefault(new Func<BulkBuyingListItem, bool>(this.method_7));
			if (bulkBuyingListItem != null)
			{
				this.mainForm_0.dataGridView_2.CellValueChanged -= this.mainForm_0.dataGridView_2_CellValueChanged;
				foreach (object obj in ((IEnumerable)this.mainForm_0.dataGridView_2.Rows))
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
					string a = dataGridViewRow.Cells[BulkBuyForm.getString_0(107400063)].Value.smethod_10();
					string a2 = dataGridViewRow.Cells[BulkBuyForm.getString_0(107400074)].Value.smethod_10();
					if (a == this.bulkBuyingListItem_0.HaveName && a2 == this.bulkBuyingListItem_0.WantName)
					{
						dataGridViewRow.Cells[BulkBuyForm.getString_0(107399953)].Value = this.bulkBuyingListItem_0.StockLimit;
						dataGridViewRow.Cells[BulkBuyForm.getString_0(107400010)].Value = this.bulkBuyingListItem_0.MaxPriceAmount;
						DataGridViewCell dataGridViewCell = dataGridViewRow.Cells[BulkBuyForm.getString_0(107399924)];
						DataGridViewCell dataGridViewCell2 = dataGridViewRow.Cells[BulkBuyForm.getString_0(107407247)];
						dataGridViewCell.Value = this.bulkBuyingListItem_0.PriceAmount;
						dataGridViewCell2.Value = this.bulkBuyingListItem_0.PriceCurrency;
						dataGridViewCell.smethod_26(this.bulkBuyingListItem_0.PriceAfterBuying && !this.bulkBuyingListItem_0.AutoPrice);
						dataGridViewCell2.smethod_26(this.bulkBuyingListItem_0.PriceAfterBuying && !this.bulkBuyingListItem_0.AutoPrice);
						break;
					}
				}
				this.mainForm_0.dataGridView_2.CellValueChanged += this.mainForm_0.dataGridView_2_CellValueChanged;
				list[list.IndexOf(bulkBuyingListItem)] = this.bulkBuyingListItem_0;
				Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, list, true);
			}
		}

		private void checkBox_1_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_1.Checked;
			this.checkBox_0.Enabled = @checked;
			this.textBox_0.Enabled = @checked;
			this.comboBox_0.Enabled = @checked;
			this.checkBox_0_CheckedChanged(null, null);
		}

		private void checkBox_0_CheckedChanged(object sender, EventArgs e)
		{
			bool enabled = this.checkBox_0.Checked && this.checkBox_1.Checked;
			if (this.checkBox_1.Checked)
			{
				this.textBox_0.Enabled = !this.checkBox_0.Checked;
				this.comboBox_0.Enabled = !this.checkBox_0.Checked;
			}
			this.numericUpDown_2.Enabled = enabled;
			this.numericUpDown_1.Enabled = enabled;
			this.numericUpDown_5.Enabled = enabled;
		}

		private void linkLabel_1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string fileName = string.Format(BulkBuyForm.getString_0(107355920), Class103.ExchangeUrl, WebUtility.UrlEncode(this.bulkBuyingListItem_0.QueryURL));
			Process.Start(fileName);
		}

		private unsafe void button_1_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = Class255.BulkBuyingList.IndexOf(this.bulkBuyingListItem_0);
			((byte*)ptr)[4] = ((Class255.BulkBuyingList.Count - 1 == *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList[*(int*)ptr + 1];
				this.method_5();
				this.bulkBuyingListItem_0 = bulkBuyingListItem;
				this.method_0();
			}
		}

		private unsafe void button_2_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = Class255.BulkBuyingList.IndexOf(this.bulkBuyingListItem_0);
			((byte*)ptr)[4] = ((*(int*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList[*(int*)ptr - 1];
				this.method_5();
				this.bulkBuyingListItem_0 = bulkBuyingListItem;
				this.method_0();
			}
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
			this.groupBox_0 = new GroupBox();
			this.numericUpDown_4 = new NumericUpDown();
			this.label_7 = new Label();
			this.linkLabel_0 = new LinkLabel();
			this.linkLabel_1 = new LinkLabel();
			this.label_6 = new Label();
			this.numericUpDown_3 = new NumericUpDown();
			this.label_1 = new Label();
			this.groupBox_1 = new GroupBox();
			this.numericUpDown_5 = new NumericUpDown();
			this.label_9 = new Label();
			this.comboBox_2 = new ComboBox();
			this.label_8 = new Label();
			this.numericUpDown_1 = new NumericUpDown();
			this.numericUpDown_2 = new NumericUpDown();
			this.label_4 = new Label();
			this.label_5 = new Label();
			this.comboBox_0 = new ComboBox();
			this.textBox_0 = new TextBox();
			this.label_0 = new Label();
			this.checkBox_0 = new CheckBox();
			this.checkBox_1 = new CheckBox();
			this.groupBox_2 = new GroupBox();
			this.comboBox_1 = new ComboBox();
			this.label_3 = new Label();
			this.checkBox_2 = new CheckBox();
			this.numericUpDown_0 = new NumericUpDown();
			this.label_2 = new Label();
			this.button_0 = new Button();
			this.button_1 = new Button();
			this.button_2 = new Button();
			this.groupBox_3 = new GroupBox();
			this.checkBox_3 = new CheckBox();
			this.checkBox_4 = new CheckBox();
			this.groupBox_0.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_4).BeginInit();
			((ISupportInitialize)this.numericUpDown_3).BeginInit();
			this.groupBox_1.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_5).BeginInit();
			((ISupportInitialize)this.numericUpDown_1).BeginInit();
			((ISupportInitialize)this.numericUpDown_2).BeginInit();
			this.groupBox_2.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_0).BeginInit();
			this.groupBox_3.SuspendLayout();
			base.SuspendLayout();
			this.groupBox_0.Controls.Add(this.numericUpDown_4);
			this.groupBox_0.Controls.Add(this.label_7);
			this.groupBox_0.Controls.Add(this.linkLabel_0);
			this.groupBox_0.Controls.Add(this.linkLabel_1);
			this.groupBox_0.Controls.Add(this.label_6);
			this.groupBox_0.Controls.Add(this.numericUpDown_3);
			this.groupBox_0.Controls.Add(this.label_1);
			this.groupBox_0.Location = new Point(3, 3);
			this.groupBox_0.Name = BulkBuyForm.getString_0(107389900);
			this.groupBox_0.Size = new Size(312, 112);
			this.groupBox_0.TabIndex = 2;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Text = BulkBuyForm.getString_0(107231714);
			NumericUpDown numericUpDown = this.numericUpDown_4;
			int[] array = new int[4];
			array[0] = 5;
			numericUpDown.Increment = new decimal(array);
			this.numericUpDown_4.Location = new Point(72, 60);
			NumericUpDown numericUpDown2 = this.numericUpDown_4;
			int[] array2 = new int[4];
			array2[0] = 100000;
			numericUpDown2.Maximum = new decimal(array2);
			NumericUpDown numericUpDown3 = this.numericUpDown_4;
			int[] array3 = new int[4];
			array3[0] = 1;
			numericUpDown3.Minimum = new decimal(array3);
			this.numericUpDown_4.Name = BulkBuyForm.getString_0(107231177);
			this.numericUpDown_4.Size = new Size(82, 20);
			this.numericUpDown_4.TabIndex = 14;
			NumericUpDown numericUpDown4 = this.numericUpDown_4;
			int[] array4 = new int[4];
			array4[0] = 1;
			numericUpDown4.Value = new decimal(array4);
			this.label_7.AutoSize = true;
			this.label_7.Location = new Point(6, 63);
			this.label_7.Name = BulkBuyForm.getString_0(107388720);
			this.label_7.Size = new Size(64, 14);
			this.label_7.TabIndex = 13;
			this.label_7.Text = BulkBuyForm.getString_0(107231128);
			this.linkLabel_0.AutoSize = true;
			this.linkLabel_0.Location = new Point(6, 39);
			this.linkLabel_0.Name = BulkBuyForm.getString_0(107231143);
			this.linkLabel_0.Size = new Size(65, 14);
			this.linkLabel_0.TabIndex = 12;
			this.linkLabel_0.TabStop = true;
			this.linkLabel_0.Text = BulkBuyForm.getString_0(107231094);
			this.linkLabel_0.LinkClicked += this.linkLabel_1_LinkClicked;
			this.linkLabel_1.AutoSize = true;
			this.linkLabel_1.Location = new Point(6, 16);
			this.linkLabel_1.Name = BulkBuyForm.getString_0(107231109);
			this.linkLabel_1.Size = new Size(64, 14);
			this.linkLabel_1.TabIndex = 11;
			this.linkLabel_1.TabStop = true;
			this.linkLabel_1.Text = BulkBuyForm.getString_0(107231060);
			this.linkLabel_1.LinkClicked += this.linkLabel_1_LinkClicked;
			this.label_6.AutoSize = true;
			this.label_6.Location = new Point(157, 91);
			this.label_6.Name = BulkBuyForm.getString_0(107231075);
			this.label_6.Size = new Size(64, 14);
			this.label_6.TabIndex = 10;
			this.label_6.Text = BulkBuyForm.getString_0(107373996);
			this.numericUpDown_3.DecimalPlaces = 6;
			NumericUpDown numericUpDown5 = this.numericUpDown_3;
			int[] array5 = new int[4];
			array5[0] = 5;
			numericUpDown5.Increment = new decimal(array5);
			this.numericUpDown_3.Location = new Point(72, 88);
			NumericUpDown numericUpDown6 = this.numericUpDown_3;
			int[] array6 = new int[4];
			array6[0] = 100000;
			numericUpDown6.Maximum = new decimal(array6);
			this.numericUpDown_3.Name = BulkBuyForm.getString_0(107231050);
			this.numericUpDown_3.Size = new Size(82, 20);
			this.numericUpDown_3.TabIndex = 8;
			this.label_1.AutoSize = true;
			this.label_1.Location = new Point(6, 91);
			this.label_1.Name = BulkBuyForm.getString_0(107388852);
			this.label_1.Size = new Size(63, 14);
			this.label_1.TabIndex = 5;
			this.label_1.Text = BulkBuyForm.getString_0(107231001);
			this.groupBox_1.Controls.Add(this.numericUpDown_5);
			this.groupBox_1.Controls.Add(this.label_9);
			this.groupBox_1.Controls.Add(this.comboBox_2);
			this.groupBox_1.Controls.Add(this.label_8);
			this.groupBox_1.Controls.Add(this.numericUpDown_1);
			this.groupBox_1.Controls.Add(this.numericUpDown_2);
			this.groupBox_1.Controls.Add(this.label_4);
			this.groupBox_1.Controls.Add(this.label_5);
			this.groupBox_1.Controls.Add(this.comboBox_0);
			this.groupBox_1.Controls.Add(this.textBox_0);
			this.groupBox_1.Controls.Add(this.label_0);
			this.groupBox_1.Controls.Add(this.checkBox_0);
			this.groupBox_1.Controls.Add(this.checkBox_1);
			this.groupBox_1.Location = new Point(3, 121);
			this.groupBox_1.Name = BulkBuyForm.getString_0(107391796);
			this.groupBox_1.Size = new Size(312, 195);
			this.groupBox_1.TabIndex = 3;
			this.groupBox_1.TabStop = false;
			this.groupBox_1.Text = BulkBuyForm.getString_0(107355537);
			this.numericUpDown_5.Location = new Point(157, 165);
			NumericUpDown numericUpDown7 = this.numericUpDown_5;
			int[] array7 = new int[4];
			array7[0] = 60000;
			numericUpDown7.Maximum = new decimal(array7);
			NumericUpDown numericUpDown8 = this.numericUpDown_5;
			int[] array8 = new int[4];
			array8[0] = 1;
			numericUpDown8.Minimum = new decimal(array8);
			this.numericUpDown_5.Name = BulkBuyForm.getString_0(107231016);
			this.numericUpDown_5.Size = new Size(56, 20);
			this.numericUpDown_5.TabIndex = 26;
			NumericUpDown numericUpDown9 = this.numericUpDown_5;
			int[] array9 = new int[4];
			array9[0] = 1;
			numericUpDown9.Value = new decimal(array9);
			this.label_9.AutoSize = true;
			this.label_9.Location = new Point(3, 167);
			this.label_9.Name = BulkBuyForm.getString_0(107230991);
			this.label_9.Size = new Size(95, 14);
			this.label_9.TabIndex = 25;
			this.label_9.Text = BulkBuyForm.getString_0(107230934);
			this.comboBox_2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_2.FormattingEnabled = true;
			this.comboBox_2.Location = new Point(157, 88);
			this.comboBox_2.Name = BulkBuyForm.getString_0(107231421);
			this.comboBox_2.Size = new Size(150, 22);
			this.comboBox_2.TabIndex = 24;
			this.label_8.AutoSize = true;
			this.label_8.Location = new Point(3, 92);
			this.label_8.Name = BulkBuyForm.getString_0(107389481);
			this.label_8.Size = new Size(122, 14);
			this.label_8.TabIndex = 23;
			this.label_8.Text = BulkBuyForm.getString_0(107231424);
			this.numericUpDown_1.Location = new Point(157, 140);
			NumericUpDown numericUpDown10 = this.numericUpDown_1;
			int[] array10 = new int[4];
			array10[0] = 10;
			numericUpDown10.Maximum = new decimal(array10);
			NumericUpDown numericUpDown11 = this.numericUpDown_1;
			int[] array11 = new int[4];
			array11[0] = 1;
			numericUpDown11.Minimum = new decimal(array11);
			this.numericUpDown_1.Name = BulkBuyForm.getString_0(107231395);
			this.numericUpDown_1.Size = new Size(56, 20);
			this.numericUpDown_1.TabIndex = 17;
			NumericUpDown numericUpDown12 = this.numericUpDown_1;
			int[] array12 = new int[4];
			array12[0] = 1;
			numericUpDown12.Value = new decimal(array12);
			this.numericUpDown_2.Location = new Point(157, 116);
			NumericUpDown numericUpDown13 = this.numericUpDown_2;
			int[] array13 = new int[4];
			array13[0] = 50;
			numericUpDown13.Maximum = new decimal(array13);
			this.numericUpDown_2.Name = BulkBuyForm.getString_0(107231346);
			this.numericUpDown_2.Size = new Size(56, 20);
			this.numericUpDown_2.TabIndex = 16;
			this.label_4.AutoSize = true;
			this.label_4.Location = new Point(3, 142);
			this.label_4.Name = BulkBuyForm.getString_0(107391215);
			this.label_4.Size = new Size(126, 14);
			this.label_4.TabIndex = 15;
			this.label_4.Text = BulkBuyForm.getString_0(107231361);
			this.label_5.AutoSize = true;
			this.label_5.Location = new Point(3, 118);
			this.label_5.Name = BulkBuyForm.getString_0(107391217);
			this.label_5.Size = new Size(126, 14);
			this.label_5.TabIndex = 14;
			this.label_5.Text = BulkBuyForm.getString_0(107231328);
			this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Location = new Point(157, 60);
			this.comboBox_0.Name = BulkBuyForm.getString_0(107231263);
			this.comboBox_0.Size = new Size(150, 22);
			this.comboBox_0.TabIndex = 4;
			this.textBox_0.Location = new Point(91, 60);
			this.textBox_0.MinimumSize = new Size(4, 22);
			this.textBox_0.Name = BulkBuyForm.getString_0(107231226);
			this.textBox_0.Size = new Size(60, 20);
			this.textBox_0.TabIndex = 3;
			this.label_0.AutoSize = true;
			this.label_0.Location = new Point(3, 64);
			this.label_0.Name = BulkBuyForm.getString_0(107389315);
			this.label_0.Size = new Size(85, 14);
			this.label_0.TabIndex = 2;
			this.label_0.Text = BulkBuyForm.getString_0(107231237);
			this.checkBox_0.AutoSize = true;
			this.checkBox_0.Location = new Point(6, 43);
			this.checkBox_0.Name = BulkBuyForm.getString_0(107355939);
			this.checkBox_0.Size = new Size(83, 18);
			this.checkBox_0.TabIndex = 1;
			this.checkBox_0.Text = BulkBuyForm.getString_0(107231184);
			this.checkBox_0.UseVisualStyleBackColor = true;
			this.checkBox_0.CheckedChanged += this.checkBox_0_CheckedChanged;
			this.checkBox_1.AutoSize = true;
			this.checkBox_1.Location = new Point(6, 19);
			this.checkBox_1.Name = BulkBuyForm.getString_0(107230655);
			this.checkBox_1.Size = new Size(123, 18);
			this.checkBox_1.TabIndex = 0;
			this.checkBox_1.Text = BulkBuyForm.getString_0(107230662);
			this.checkBox_1.UseVisualStyleBackColor = true;
			this.checkBox_1.CheckedChanged += this.checkBox_1_CheckedChanged;
			this.groupBox_2.Controls.Add(this.comboBox_1);
			this.groupBox_2.Controls.Add(this.label_3);
			this.groupBox_2.Controls.Add(this.checkBox_2);
			this.groupBox_2.Controls.Add(this.numericUpDown_0);
			this.groupBox_2.Controls.Add(this.label_2);
			this.groupBox_2.Location = new Point(3, 393);
			this.groupBox_2.Name = BulkBuyForm.getString_0(107416716);
			this.groupBox_2.Size = new Size(312, 89);
			this.groupBox_2.TabIndex = 4;
			this.groupBox_2.TabStop = false;
			this.groupBox_2.Text = BulkBuyForm.getString_0(107367797);
			this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_1.FormattingEnabled = true;
			this.comboBox_1.Location = new Point(82, 36);
			this.comboBox_1.Name = BulkBuyForm.getString_0(107230637);
			this.comboBox_1.Size = new Size(150, 22);
			this.comboBox_1.TabIndex = 5;
			this.label_3.AutoSize = true;
			this.label_3.Location = new Point(3, 44);
			this.label_3.Name = BulkBuyForm.getString_0(107391665);
			this.label_3.Size = new Size(76, 14);
			this.label_3.TabIndex = 20;
			this.label_3.Text = BulkBuyForm.getString_0(107230584);
			this.checkBox_2.AutoSize = true;
			this.checkBox_2.Location = new Point(6, 69);
			this.checkBox_2.Name = BulkBuyForm.getString_0(107230599);
			this.checkBox_2.Size = new Size(223, 18);
			this.checkBox_2.TabIndex = 19;
			this.checkBox_2.Text = BulkBuyForm.getString_0(107230550);
			this.checkBox_2.UseVisualStyleBackColor = true;
			this.numericUpDown_0.Location = new Point(82, 15);
			NumericUpDown numericUpDown14 = this.numericUpDown_0;
			int[] array14 = new int[4];
			array14[0] = 100000;
			numericUpDown14.Maximum = new decimal(array14);
			this.numericUpDown_0.Name = BulkBuyForm.getString_0(107230529);
			this.numericUpDown_0.Size = new Size(72, 20);
			this.numericUpDown_0.TabIndex = 18;
			this.label_2.AutoSize = true;
			this.label_2.Location = new Point(3, 16);
			this.label_2.Name = BulkBuyForm.getString_0(107389402);
			this.label_2.Size = new Size(72, 14);
			this.label_2.TabIndex = 17;
			this.label_2.Text = BulkBuyForm.getString_0(107230508);
			this.button_0.Location = new Point(219, 483);
			this.button_0.Name = BulkBuyForm.getString_0(107230459);
			this.button_0.Size = new Size(96, 25);
			this.button_0.TabIndex = 9;
			this.button_0.Text = BulkBuyForm.getString_0(107354433);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.button_1.Location = new Point(117, 483);
			this.button_1.Name = BulkBuyForm.getString_0(107354412);
			this.button_1.Size = new Size(96, 25);
			this.button_1.TabIndex = 10;
			this.button_1.Text = BulkBuyForm.getString_0(107354363);
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			this.button_2.Location = new Point(15, 483);
			this.button_2.Name = BulkBuyForm.getString_0(107354374);
			this.button_2.Size = new Size(96, 25);
			this.button_2.TabIndex = 22;
			this.button_2.Text = BulkBuyForm.getString_0(107354325);
			this.button_2.UseVisualStyleBackColor = true;
			this.button_2.Click += this.button_2_Click;
			this.groupBox_3.Controls.Add(this.checkBox_3);
			this.groupBox_3.Controls.Add(this.checkBox_4);
			this.groupBox_3.Location = new Point(3, 322);
			this.groupBox_3.Name = BulkBuyForm.getString_0(107416326);
			this.groupBox_3.Size = new Size(312, 65);
			this.groupBox_3.TabIndex = 23;
			this.groupBox_3.TabStop = false;
			this.groupBox_3.Text = BulkBuyForm.getString_0(107230474);
			this.checkBox_3.AutoSize = true;
			this.checkBox_3.Location = new Point(5, 43);
			this.checkBox_3.Name = BulkBuyForm.getString_0(107230421);
			this.checkBox_3.Size = new Size(182, 18);
			this.checkBox_3.TabIndex = 20;
			this.checkBox_3.Text = BulkBuyForm.getString_0(107230900);
			this.checkBox_3.UseVisualStyleBackColor = true;
			this.checkBox_4.AutoSize = true;
			this.checkBox_4.Location = new Point(5, 19);
			this.checkBox_4.Name = BulkBuyForm.getString_0(107230887);
			this.checkBox_4.Size = new Size(174, 18);
			this.checkBox_4.TabIndex = 19;
			this.checkBox_4.Text = BulkBuyForm.getString_0(107230854);
			this.checkBox_4.UseVisualStyleBackColor = true;
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
			this.Font = new Font(BulkBuyForm.getString_0(107400501), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = BulkBuyForm.getString_0(107230817);
			this.Text = BulkBuyForm.getString_0(107230768);
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_0.PerformLayout();
			((ISupportInitialize)this.numericUpDown_4).EndInit();
			((ISupportInitialize)this.numericUpDown_3).EndInit();
			this.groupBox_1.ResumeLayout(false);
			this.groupBox_1.PerformLayout();
			((ISupportInitialize)this.numericUpDown_5).EndInit();
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
		private bool method_7(BulkBuyingListItem bulkBuyingListItem_1)
		{
			return bulkBuyingListItem_1.HaveName == this.bulkBuyingListItem_0.HaveName && bulkBuyingListItem_1.WantName == this.bulkBuyingListItem_0.WantName;
		}

		static BulkBuyForm()
		{
			Strings.CreateGetStringDelegate(typeof(BulkBuyForm));
		}

		private MainForm mainForm_0;

		private BulkBuyingListItem bulkBuyingListItem_0;

		private IContainer icontainer_0 = null;

		private GroupBox groupBox_0;

		private GroupBox groupBox_1;

		private CheckBox checkBox_0;

		private CheckBox checkBox_1;

		private TextBox textBox_0;

		private Label label_0;

		private ComboBox comboBox_0;

		private Label label_1;

		private GroupBox groupBox_2;

		private NumericUpDown numericUpDown_0;

		private Label label_2;

		private ComboBox comboBox_1;

		private Label label_3;

		private CheckBox checkBox_2;

		private NumericUpDown numericUpDown_1;

		private NumericUpDown numericUpDown_2;

		private Label label_4;

		private Label label_5;

		private NumericUpDown numericUpDown_3;

		private Button button_0;

		private Button button_1;

		private Button button_2;

		private Label label_6;

		private LinkLabel linkLabel_0;

		private LinkLabel linkLabel_1;

		private NumericUpDown numericUpDown_4;

		private Label label_7;

		private ComboBox comboBox_2;

		private Label label_8;

		private NumericUpDown numericUpDown_5;

		private Label label_9;

		private GroupBox groupBox_3;

		private CheckBox checkBox_3;

		private CheckBox checkBox_4;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
