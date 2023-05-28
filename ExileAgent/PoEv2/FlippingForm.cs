using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using ns0;
using ns12;
using ns29;
using ns31;
using ns40;
using ns8;
using PoEv2.Classes;
using PoEv2.Features;
using PoEv2.Models.Flipping;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2
{
	public sealed partial class FlippingForm : Form
	{
		public FlippingForm(FlippingListItem item)
		{
			this.method_4();
			this.Text = string.Format(FlippingForm.getString_0(107353333), item.HaveName, item.WantName);
			int value = API.smethod_6(item.HaveName) * 60;
			int value2 = API.smethod_6(item.WantName) * 60;
			this.numericUpDown_3.Maximum = value;
			this.numericUpDown_1.Maximum = value;
			this.numericUpDown_18.Maximum = value;
			this.numericUpDown_13.Maximum = value;
			this.numericUpDown_2.Maximum = value2;
			this.numericUpDown_0.Maximum = value2;
			this.numericUpDown_17.Maximum = value2;
			this.numericUpDown_12.Maximum = value2;
			this.flippingListItem_0 = item;
		}

		public void method_0()
		{
			this.method_1();
			this.method_2();
			this.checkBox_0_CheckedChanged(null, null);
			this.checkBox_1_CheckedChanged(null, null);
			this.checkBox_2_CheckedChanged(null, null);
			int num = Class255.FlippingList.IndexOf(this.flippingListItem_0);
			this.button_4.Visible = (num > 0);
			this.button_3.Visible = (Class255.FlippingList.Count - 1 > num);
		}

		private unsafe void method_1()
		{
			void* ptr = stackalloc byte[2];
			this.label_17.Text = string.Empty;
			this.label_21.Text = string.Empty;
			this.label_20.Text = string.Empty;
			*(byte*)ptr = (Class308.dictionary_1.ContainsKey(this.flippingListItem_0.HaveName) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.pictureBox_1.Image = Class308.dictionary_1[this.flippingListItem_0.HaveName];
				this.pictureBox_2.Image = Class308.dictionary_1[this.flippingListItem_0.HaveName];
			}
			((byte*)ptr)[1] = (Class308.dictionary_1.ContainsKey(this.flippingListItem_0.WantName) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.pictureBox_0.Image = Class308.dictionary_1[this.flippingListItem_0.WantName];
				this.pictureBox_3.Image = Class308.dictionary_1[this.flippingListItem_0.WantName];
			}
			this.label_10.Text = this.flippingListItem_0.HaveName;
			this.label_9.Text = this.flippingListItem_0.HaveName;
			this.label_8.Text = this.flippingListItem_0.WantName;
			this.label_7.Text = this.flippingListItem_0.WantName;
			this.checkBox_1.Text = string.Format(FlippingForm.getString_0(107353348), this.flippingListItem_0.WantName);
		}

		private void method_2()
		{
			this.checkBox_0.Checked = this.flippingListItem_0.AutoPrice;
			this.checkBox_1.Checked = this.flippingListItem_0.OnlyPriceHave;
			this.checkBox_2.Checked = this.flippingListItem_0.AutoDetermineMargin;
			this.numericUpDown_6.smethod_2(this.flippingListItem_0.BuyListingsToSkip);
			this.numericUpDown_5.smethod_2(this.flippingListItem_0.BuyListingsToTake);
			this.numericUpDown_16.smethod_2(this.flippingListItem_0.SellListingsToSkip);
			this.numericUpDown_15.smethod_2(this.flippingListItem_0.SellListingsToTake);
			this.numericUpDown_4.smethod_2((decimal)this.flippingListItem_0.ResellMargin);
			this.numericUpDown_7.smethod_2(this.flippingListItem_0.MinHaveStock);
			this.numericUpDown_10.smethod_2(this.flippingListItem_0.MaxHaveStock);
			this.numericUpDown_9.smethod_2(this.flippingListItem_0.MinWantStock);
			this.numericUpDown_8.smethod_2(this.flippingListItem_0.MaxWantStock);
			this.numericUpDown_11.smethod_2(this.flippingListItem_0.HaveMinimumStock);
			this.numericUpDown_19.smethod_2(this.flippingListItem_0.WantMinimumStock);
			this.numericUpDown_18.smethod_2(this.flippingListItem_0.MinHavePerTrade);
			this.numericUpDown_17.smethod_2(this.flippingListItem_0.MinWantPerTrade);
			this.numericUpDown_13.smethod_2(this.flippingListItem_0.MaxHavePerTrade);
			this.numericUpDown_12.smethod_2(this.flippingListItem_0.MaxWantPerTrade);
			this.numericUpDown_14.smethod_2((decimal)this.flippingListItem_0.IgnoreBelowMargin);
			if (!this.flippingListItem_0.AutoPrice)
			{
				this.numericUpDown_1.smethod_2(this.flippingListItem_0.BuyHaveAmount);
				this.numericUpDown_0.smethod_2(this.flippingListItem_0.BuyWantAmount);
				this.numericUpDown_3.smethod_2(this.flippingListItem_0.SellHaveAmount);
				this.numericUpDown_2.smethod_2(this.flippingListItem_0.SellWantAmount);
			}
		}

		private void checkBox_0_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_3.Enabled = (!this.checkBox_0.Checked && !this.checkBox_1.Checked);
			this.numericUpDown_2.Enabled = (!this.checkBox_0.Checked && !this.checkBox_1.Checked);
			this.numericUpDown_1.Enabled = !this.checkBox_0.Checked;
			this.numericUpDown_0.Enabled = !this.checkBox_0.Checked;
			this.button_1.Enabled = this.checkBox_0.Checked;
			this.numericUpDown_4.Enabled = (this.checkBox_0.Checked && !this.checkBox_2.Checked);
			this.checkBox_2.Enabled = this.checkBox_0.Checked;
		}

		private void button_0_Click(object sender, EventArgs e)
		{
			this.method_3();
			base.Close();
		}

		private unsafe void method_3()
		{
			void* ptr = stackalloc byte[6];
			this.flippingListItem_0.AutoPrice = this.checkBox_0.Checked;
			this.flippingListItem_0.BuyListingsToSkip = (int)this.numericUpDown_6.Value;
			this.flippingListItem_0.BuyListingsToTake = (int)this.numericUpDown_5.Value;
			this.flippingListItem_0.SellListingsToSkip = (int)this.numericUpDown_16.Value;
			this.flippingListItem_0.SellListingsToTake = (int)this.numericUpDown_15.Value;
			this.flippingListItem_0.ResellMargin = (double)this.numericUpDown_4.Value;
			this.flippingListItem_0.MinHaveStock = (int)this.numericUpDown_7.Value;
			this.flippingListItem_0.MaxHaveStock = (int)this.numericUpDown_10.Value;
			this.flippingListItem_0.MinWantStock = (int)this.numericUpDown_9.Value;
			this.flippingListItem_0.MaxWantStock = (int)this.numericUpDown_8.Value;
			this.flippingListItem_0.HaveMinimumStock = (int)this.numericUpDown_11.Value;
			this.flippingListItem_0.WantMinimumStock = (int)this.numericUpDown_19.Value;
			this.flippingListItem_0.MinHavePerTrade = (int)this.numericUpDown_18.Value;
			this.flippingListItem_0.MinWantPerTrade = (int)this.numericUpDown_17.Value;
			this.flippingListItem_0.MaxHavePerTrade = (int)this.numericUpDown_13.Value;
			this.flippingListItem_0.MaxWantPerTrade = (int)this.numericUpDown_12.Value;
			this.flippingListItem_0.OnlyPriceHave = this.checkBox_1.Checked;
			this.flippingListItem_0.AutoDetermineMargin = this.checkBox_2.Checked;
			this.flippingListItem_0.IgnoreBelowMargin = (double)this.numericUpDown_14.Value;
			((byte*)ptr)[4] = ((!this.flippingListItem_0.AutoPrice) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				this.flippingListItem_0.BuyHaveAmount = (int)this.numericUpDown_1.Value;
				this.flippingListItem_0.BuyWantAmount = (int)this.numericUpDown_0.Value;
				this.flippingListItem_0.SellHaveAmount = (int)this.numericUpDown_3.Value;
				this.flippingListItem_0.SellWantAmount = (int)this.numericUpDown_2.Value;
			}
			Class255.class105_0.method_9(ConfigOptions.FlipFormLocation, base.Location, true);
			List<FlippingListItem> list = Class255.FlippingList.ToList<FlippingListItem>();
			FlippingListItem flippingListItem = list.FirstOrDefault(new Func<FlippingListItem, bool>(this.method_5));
			((byte*)ptr)[5] = ((flippingListItem == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) == 0)
			{
				*(int*)ptr = list.IndexOf(flippingListItem);
				list[*(int*)ptr] = this.flippingListItem_0;
				Class255.class105_0.method_9(ConfigOptions.FlippingList, list, true);
			}
		}

		private unsafe void button_1_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[6];
			this.method_3();
			List<FlippingListJsonItem> list = new List<FlippingListJsonItem>();
			*(byte*)ptr = (Class120.bool_2 ? 1 : 0);
			Class248 @class;
			if (*(sbyte*)ptr != 0)
			{
				list = Web.smethod_11(new List<FlippingListItem>
				{
					this.flippingListItem_0
				});
				if (list == null || !list.Any<FlippingListJsonItem>())
				{
					return;
				}
				@class = Flipping.smethod_11(this.flippingListItem_0, list.First(new Func<FlippingListJsonItem, bool>(FlippingForm.<>c.<>9.method_0)).Average);
			}
			else
			{
				@class = Flipping.smethod_0(this.flippingListItem_0);
				((byte*)ptr)[1] = ((@class == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return;
				}
				@class.method_0(Flipping.smethod_3(@class, this.flippingListItem_0));
				@class = Flipping.smethod_4(@class, this.flippingListItem_0);
				@class = Flipping.smethod_5(@class, this.flippingListItem_0);
			}
			this.numericUpDown_1.Value = @class.Numerator;
			this.numericUpDown_0.Value = @class.Denominator;
			((byte*)ptr)[2] = ((!this.checkBox_1.Checked) ? 1 : 0);
			Class248 class2;
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				((byte*)ptr)[3] = (this.checkBox_2.Checked ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					((byte*)ptr)[4] = (Class120.bool_2 ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						if (list == null || !list.Any<FlippingListJsonItem>())
						{
							return;
						}
						class2 = Flipping.smethod_12(this.flippingListItem_0, list.FirstOrDefault(new Func<FlippingListJsonItem, bool>(FlippingForm.<>c.<>9.method_1)).Average);
					}
					else
					{
						class2 = Flipping.smethod_1(this.flippingListItem_0);
					}
				}
				else
				{
					class2 = Flipping.smethod_2(@class, Math.Max(this.flippingListItem_0.MaxHavePerTrade, this.flippingListItem_0.MaxWantPerTrade), this.flippingListItem_0.ResellMargin);
				}
				class2.method_0(Flipping.smethod_3(class2, this.flippingListItem_0));
				class2 = Flipping.smethod_4(class2, this.flippingListItem_0);
				class2 = Flipping.smethod_5(class2, this.flippingListItem_0);
				decimal d = Class248.smethod_1(@class, class2);
				this.label_17.Text = string.Format(FlippingForm.getString_0(107353331), d.ToString(FlippingForm.getString_0(107353322)));
				((byte*)ptr)[5] = (this.checkBox_2.Checked ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					this.label_17.ForeColor = ((d >= this.numericUpDown_14.Value) ? Color.Black : Color.Red);
				}
				else
				{
					this.label_17.ForeColor = Color.Black;
				}
			}
			else
			{
				class2 = new Class248(0, 0);
			}
			this.numericUpDown_3.Value = class2.Numerator;
			this.numericUpDown_2.Value = class2.Denominator;
			this.label_21.Text = string.Format(FlippingForm.getString_0(107353281), Math.Round(@class.Value, 4));
			this.label_20.Text = (this.checkBox_1.Checked ? string.Empty : string.Format(FlippingForm.getString_0(107353281), Math.Round(class2.Value, 4)));
		}

		private void button_2_Click(object sender, EventArgs e)
		{
			string value = JsonConvert.SerializeObject(new Class272(new Class270(API.smethod_4(this.flippingListItem_0.HaveName), API.smethod_4(this.flippingListItem_0.WantName), this.flippingListItem_0.HaveMinimumStock)), Util.smethod_10());
			string fileName = string.Format(FlippingForm.getString_0(107353268), Class103.ExchangeUrl, WebUtility.UrlEncode(value));
			Process.Start(fileName);
		}

		private void checkBox_1_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_10.Enabled = !this.checkBox_1.Checked;
			this.numericUpDown_9.Enabled = !this.checkBox_1.Checked;
			this.checkBox_2.Enabled = !this.checkBox_1.Checked;
			this.numericUpDown_16.Enabled = !this.checkBox_1.Checked;
			this.numericUpDown_15.Enabled = !this.checkBox_1.Checked;
			this.numericUpDown_3.Enabled = (!this.checkBox_1.Checked && !this.checkBox_0.Checked);
			this.numericUpDown_2.Enabled = (!this.checkBox_1.Checked && !this.checkBox_0.Checked);
		}

		private void checkBox_2_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_4.Enabled = !this.checkBox_2.Checked;
			this.numericUpDown_14.Enabled = this.checkBox_2.Checked;
		}

		private void numericUpDown_3_ValueChanged(object sender, EventArgs e)
		{
			if (!this.checkBox_0.Checked)
			{
				Class248 @class = new Class248((int)this.numericUpDown_1.Value, (int)this.numericUpDown_0.Value);
				Class248 class2 = new Class248((int)this.numericUpDown_3.Value, (int)this.numericUpDown_2.Value);
				if (@class.Numerator == 0 || @class.Denominator == 0 || class2.Numerator == 0 || class2.Denominator == 0)
				{
					this.label_17.Text = string.Empty;
					this.label_21.Text = string.Empty;
					this.label_20.Text = string.Empty;
				}
				else
				{
					decimal d = Class248.smethod_1(@class, class2);
					this.label_17.Text = string.Format(FlippingForm.getString_0(107353331), d.ToString(FlippingForm.getString_0(107353322)));
					this.label_17.ForeColor = ((d >= 0m) ? Color.Black : Color.Red);
					this.label_21.Text = string.Format(FlippingForm.getString_0(107353281), Math.Round(@class.Value, 4));
					this.label_20.Text = (this.checkBox_1.Checked ? string.Empty : string.Format(FlippingForm.getString_0(107353281), Math.Round(class2.Value, 4)));
				}
			}
		}

		private unsafe void button_4_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = Class255.FlippingList.IndexOf(this.flippingListItem_0);
			((byte*)ptr)[4] = ((*(int*)ptr == 0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				FlippingListItem flippingListItem = Class255.FlippingList[*(int*)ptr - 1];
				this.method_3();
				this.flippingListItem_0 = flippingListItem;
				this.method_0();
			}
		}

		private unsafe void button_3_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			*(int*)ptr = Class255.FlippingList.IndexOf(this.flippingListItem_0);
			((byte*)ptr)[4] = ((Class255.FlippingList.Count - 1 == *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				FlippingListItem flippingListItem = Class255.FlippingList[*(int*)ptr + 1];
				this.method_3();
				this.flippingListItem_0 = flippingListItem;
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

		private void method_4()
		{
			this.checkBox_0 = new CheckBox();
			this.label_0 = new Label();
			this.groupBox_0 = new GroupBox();
			this.label_20 = new Label();
			this.label_21 = new Label();
			this.label_17 = new Label();
			this.button_2 = new Button();
			this.button_1 = new Button();
			this.pictureBox_0 = new PictureBox();
			this.pictureBox_1 = new PictureBox();
			this.label_14 = new Label();
			this.label_15 = new Label();
			this.numericUpDown_0 = new NumericUpDown();
			this.numericUpDown_1 = new NumericUpDown();
			this.numericUpDown_2 = new NumericUpDown();
			this.numericUpDown_3 = new NumericUpDown();
			this.label_1 = new Label();
			this.groupBox_1 = new GroupBox();
			this.pictureBox_2 = new PictureBox();
			this.pictureBox_3 = new PictureBox();
			this.numericUpDown_19 = new NumericUpDown();
			this.label_28 = new Label();
			this.numericUpDown_17 = new NumericUpDown();
			this.label_26 = new Label();
			this.numericUpDown_18 = new NumericUpDown();
			this.label_27 = new Label();
			this.numericUpDown_15 = new NumericUpDown();
			this.numericUpDown_16 = new NumericUpDown();
			this.label_24 = new Label();
			this.label_25 = new Label();
			this.label_22 = new Label();
			this.numericUpDown_14 = new NumericUpDown();
			this.label_23 = new Label();
			this.checkBox_2 = new CheckBox();
			this.checkBox_1 = new CheckBox();
			this.numericUpDown_12 = new NumericUpDown();
			this.label_18 = new Label();
			this.numericUpDown_13 = new NumericUpDown();
			this.label_19 = new Label();
			this.numericUpDown_11 = new NumericUpDown();
			this.label_16 = new Label();
			this.label_4 = new Label();
			this.numericUpDown_4 = new NumericUpDown();
			this.label_5 = new Label();
			this.numericUpDown_5 = new NumericUpDown();
			this.numericUpDown_6 = new NumericUpDown();
			this.label_2 = new Label();
			this.label_3 = new Label();
			this.groupBox_2 = new GroupBox();
			this.label_7 = new Label();
			this.label_8 = new Label();
			this.label_9 = new Label();
			this.label_10 = new Label();
			this.numericUpDown_8 = new NumericUpDown();
			this.numericUpDown_9 = new NumericUpDown();
			this.label_11 = new Label();
			this.label_12 = new Label();
			this.numericUpDown_10 = new NumericUpDown();
			this.label_13 = new Label();
			this.numericUpDown_7 = new NumericUpDown();
			this.label_6 = new Label();
			this.button_0 = new Button();
			this.button_3 = new Button();
			this.button_4 = new Button();
			this.groupBox_0.SuspendLayout();
			((ISupportInitialize)this.pictureBox_0).BeginInit();
			((ISupportInitialize)this.pictureBox_1).BeginInit();
			((ISupportInitialize)this.numericUpDown_0).BeginInit();
			((ISupportInitialize)this.numericUpDown_1).BeginInit();
			((ISupportInitialize)this.numericUpDown_2).BeginInit();
			((ISupportInitialize)this.numericUpDown_3).BeginInit();
			this.groupBox_1.SuspendLayout();
			((ISupportInitialize)this.pictureBox_2).BeginInit();
			((ISupportInitialize)this.pictureBox_3).BeginInit();
			((ISupportInitialize)this.numericUpDown_19).BeginInit();
			((ISupportInitialize)this.numericUpDown_17).BeginInit();
			((ISupportInitialize)this.numericUpDown_18).BeginInit();
			((ISupportInitialize)this.numericUpDown_15).BeginInit();
			((ISupportInitialize)this.numericUpDown_16).BeginInit();
			((ISupportInitialize)this.numericUpDown_14).BeginInit();
			((ISupportInitialize)this.numericUpDown_12).BeginInit();
			((ISupportInitialize)this.numericUpDown_13).BeginInit();
			((ISupportInitialize)this.numericUpDown_11).BeginInit();
			((ISupportInitialize)this.numericUpDown_4).BeginInit();
			((ISupportInitialize)this.numericUpDown_5).BeginInit();
			((ISupportInitialize)this.numericUpDown_6).BeginInit();
			this.groupBox_2.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_8).BeginInit();
			((ISupportInitialize)this.numericUpDown_9).BeginInit();
			((ISupportInitialize)this.numericUpDown_10).BeginInit();
			((ISupportInitialize)this.numericUpDown_7).BeginInit();
			base.SuspendLayout();
			this.checkBox_0.AutoSize = true;
			this.checkBox_0.Checked = true;
			this.checkBox_0.CheckState = CheckState.Checked;
			this.checkBox_0.Location = new Point(6, 19);
			this.checkBox_0.Name = FlippingForm.getString_0(107353287);
			this.checkBox_0.Size = new Size(159, 18);
			this.checkBox_0.TabIndex = 0;
			this.checkBox_0.Text = FlippingForm.getString_0(107353238);
			this.checkBox_0.UseVisualStyleBackColor = true;
			this.checkBox_0.CheckedChanged += this.checkBox_0_CheckedChanged;
			this.label_0.AutoSize = true;
			this.label_0.Location = new Point(6, 68);
			this.label_0.Name = FlippingForm.getString_0(107386126);
			this.label_0.Size = new Size(47, 14);
			this.label_0.TabIndex = 3;
			this.label_0.Text = FlippingForm.getString_0(107353205);
			this.groupBox_0.Controls.Add(this.label_20);
			this.groupBox_0.Controls.Add(this.label_21);
			this.groupBox_0.Controls.Add(this.label_17);
			this.groupBox_0.Controls.Add(this.button_2);
			this.groupBox_0.Controls.Add(this.button_1);
			this.groupBox_0.Controls.Add(this.pictureBox_0);
			this.groupBox_0.Controls.Add(this.pictureBox_1);
			this.groupBox_0.Controls.Add(this.label_14);
			this.groupBox_0.Controls.Add(this.label_15);
			this.groupBox_0.Controls.Add(this.numericUpDown_0);
			this.groupBox_0.Controls.Add(this.numericUpDown_1);
			this.groupBox_0.Controls.Add(this.numericUpDown_2);
			this.groupBox_0.Controls.Add(this.numericUpDown_3);
			this.groupBox_0.Controls.Add(this.label_1);
			this.groupBox_0.Controls.Add(this.label_0);
			this.groupBox_0.Location = new Point(3, 3);
			this.groupBox_0.Name = FlippingForm.getString_0(107387248);
			this.groupBox_0.Size = new Size(462, 120);
			this.groupBox_0.TabIndex = 4;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Text = FlippingForm.getString_0(107353224);
			this.label_20.AutoSize = true;
			this.label_20.Location = new Point(215, 95);
			this.label_20.Name = FlippingForm.getString_0(107353711);
			this.label_20.Size = new Size(57, 14);
			this.label_20.TabIndex = 34;
			this.label_20.Text = FlippingForm.getString_0(107353662);
			this.label_21.AutoSize = true;
			this.label_21.Location = new Point(215, 68);
			this.label_21.Name = FlippingForm.getString_0(107353677);
			this.label_21.Size = new Size(57, 14);
			this.label_21.TabIndex = 33;
			this.label_21.Text = FlippingForm.getString_0(107353628);
			this.label_17.AutoSize = true;
			this.label_17.Location = new Point(214, 39);
			this.label_17.Name = FlippingForm.getString_0(107353647);
			this.label_17.Size = new Size(45, 14);
			this.label_17.TabIndex = 32;
			this.label_17.Text = FlippingForm.getString_0(107353602);
			this.button_2.Location = new Point(331, 57);
			this.button_2.Name = FlippingForm.getString_0(107353593);
			this.button_2.Size = new Size(128, 25);
			this.button_2.TabIndex = 31;
			this.button_2.Text = FlippingForm.getString_0(107353608);
			this.button_2.UseVisualStyleBackColor = true;
			this.button_2.Click += this.button_2_Click;
			this.button_1.Location = new Point(331, 88);
			this.button_1.Name = FlippingForm.getString_0(107353583);
			this.button_1.Size = new Size(128, 25);
			this.button_1.TabIndex = 30;
			this.button_1.Text = FlippingForm.getString_0(107353530);
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			this.pictureBox_0.BackColor = Color.Transparent;
			this.pictureBox_0.Location = new Point(69, 20);
			this.pictureBox_0.Name = FlippingForm.getString_0(107353541);
			this.pictureBox_0.Size = new Size(37, 34);
			this.pictureBox_0.TabIndex = 29;
			this.pictureBox_0.TabStop = false;
			this.pictureBox_1.BackColor = Color.Transparent;
			this.pictureBox_1.Location = new Point(159, 20);
			this.pictureBox_1.Name = FlippingForm.getString_0(107353496);
			this.pictureBox_1.Size = new Size(37, 34);
			this.pictureBox_1.TabIndex = 28;
			this.pictureBox_1.TabStop = false;
			this.label_14.AutoSize = true;
			this.label_14.Location = new Point(122, 68);
			this.label_14.Name = FlippingForm.getString_0(107385535);
			this.label_14.Size = new Size(23, 14);
			this.label_14.TabIndex = 27;
			this.label_14.Text = FlippingForm.getString_0(107353515);
			this.label_15.AutoSize = true;
			this.label_15.Location = new Point(121, 95);
			this.label_15.Name = FlippingForm.getString_0(107385944);
			this.label_15.Size = new Size(23, 14);
			this.label_15.TabIndex = 26;
			this.label_15.Text = FlippingForm.getString_0(107353515);
			this.numericUpDown_0.Location = new Point(61, 66);
			this.numericUpDown_0.Name = FlippingForm.getString_0(107353510);
			this.numericUpDown_0.Size = new Size(54, 20);
			this.numericUpDown_0.TabIndex = 11;
			this.numericUpDown_0.ValueChanged += this.numericUpDown_3_ValueChanged;
			this.numericUpDown_1.Location = new Point(151, 66);
			this.numericUpDown_1.Name = FlippingForm.getString_0(107353461);
			this.numericUpDown_1.Size = new Size(54, 20);
			this.numericUpDown_1.TabIndex = 12;
			this.numericUpDown_1.ValueChanged += this.numericUpDown_3_ValueChanged;
			this.numericUpDown_2.Location = new Point(61, 92);
			this.numericUpDown_2.Name = FlippingForm.getString_0(107353476);
			this.numericUpDown_2.Size = new Size(54, 20);
			this.numericUpDown_2.TabIndex = 13;
			this.numericUpDown_2.ValueChanged += this.numericUpDown_3_ValueChanged;
			this.numericUpDown_3.Location = new Point(151, 92);
			this.numericUpDown_3.Name = FlippingForm.getString_0(107352947);
			this.numericUpDown_3.Size = new Size(54, 20);
			this.numericUpDown_3.TabIndex = 14;
			this.numericUpDown_3.ValueChanged += this.numericUpDown_3_ValueChanged;
			this.label_1.AutoSize = true;
			this.label_1.Location = new Point(6, 95);
			this.label_1.Name = FlippingForm.getString_0(107386663);
			this.label_1.Size = new Size(47, 14);
			this.label_1.TabIndex = 4;
			this.label_1.Text = FlippingForm.getString_0(107352898);
			this.groupBox_1.Controls.Add(this.pictureBox_2);
			this.groupBox_1.Controls.Add(this.pictureBox_3);
			this.groupBox_1.Controls.Add(this.numericUpDown_19);
			this.groupBox_1.Controls.Add(this.label_28);
			this.groupBox_1.Controls.Add(this.numericUpDown_17);
			this.groupBox_1.Controls.Add(this.label_26);
			this.groupBox_1.Controls.Add(this.numericUpDown_18);
			this.groupBox_1.Controls.Add(this.label_27);
			this.groupBox_1.Controls.Add(this.numericUpDown_15);
			this.groupBox_1.Controls.Add(this.numericUpDown_16);
			this.groupBox_1.Controls.Add(this.label_24);
			this.groupBox_1.Controls.Add(this.label_25);
			this.groupBox_1.Controls.Add(this.label_22);
			this.groupBox_1.Controls.Add(this.numericUpDown_14);
			this.groupBox_1.Controls.Add(this.label_23);
			this.groupBox_1.Controls.Add(this.checkBox_2);
			this.groupBox_1.Controls.Add(this.checkBox_1);
			this.groupBox_1.Controls.Add(this.numericUpDown_12);
			this.groupBox_1.Controls.Add(this.label_18);
			this.groupBox_1.Controls.Add(this.numericUpDown_13);
			this.groupBox_1.Controls.Add(this.label_19);
			this.groupBox_1.Controls.Add(this.numericUpDown_11);
			this.groupBox_1.Controls.Add(this.label_16);
			this.groupBox_1.Controls.Add(this.label_4);
			this.groupBox_1.Controls.Add(this.numericUpDown_4);
			this.groupBox_1.Controls.Add(this.label_5);
			this.groupBox_1.Controls.Add(this.numericUpDown_5);
			this.groupBox_1.Controls.Add(this.numericUpDown_6);
			this.groupBox_1.Controls.Add(this.label_2);
			this.groupBox_1.Controls.Add(this.label_3);
			this.groupBox_1.Controls.Add(this.checkBox_0);
			this.groupBox_1.Location = new Point(3, 129);
			this.groupBox_1.Name = FlippingForm.getString_0(107389144);
			this.groupBox_1.Size = new Size(463, 263);
			this.groupBox_1.TabIndex = 6;
			this.groupBox_1.TabStop = false;
			this.groupBox_1.Text = FlippingForm.getString_0(107352885);
			this.pictureBox_2.BackColor = Color.Transparent;
			this.pictureBox_2.Location = new Point(73, 142);
			this.pictureBox_2.Name = FlippingForm.getString_0(107352860);
			this.pictureBox_2.Size = new Size(37, 34);
			this.pictureBox_2.TabIndex = 35;
			this.pictureBox_2.TabStop = false;
			this.pictureBox_3.BackColor = Color.Transparent;
			this.pictureBox_3.Location = new Point(339, 142);
			this.pictureBox_3.Name = FlippingForm.getString_0(107352879);
			this.pictureBox_3.Size = new Size(37, 34);
			this.pictureBox_3.TabIndex = 37;
			this.pictureBox_3.TabStop = false;
			this.numericUpDown_19.Location = new Point(369, 182);
			NumericUpDown numericUpDown = this.numericUpDown_19;
			int[] array = new int[4];
			array[0] = 100000;
			numericUpDown.Maximum = new decimal(array);
			NumericUpDown numericUpDown2 = this.numericUpDown_19;
			int[] array2 = new int[4];
			array2[0] = 1;
			numericUpDown2.Minimum = new decimal(array2);
			this.numericUpDown_19.Name = FlippingForm.getString_0(107352834);
			this.numericUpDown_19.Size = new Size(56, 20);
			this.numericUpDown_19.TabIndex = 36;
			NumericUpDown numericUpDown3 = this.numericUpDown_19;
			int[] array3 = new int[4];
			array3[0] = 1;
			numericUpDown3.Value = new decimal(array3);
			this.label_28.AutoSize = true;
			this.label_28.Location = new Point(269, 184);
			this.label_28.Name = FlippingForm.getString_0(107352841);
			this.label_28.Size = new Size(96, 14);
			this.label_28.TabIndex = 35;
			this.label_28.Text = FlippingForm.getString_0(107352788);
			NumericUpDown numericUpDown4 = this.numericUpDown_17;
			int[] array4 = new int[4];
			array4[0] = 10;
			numericUpDown4.Increment = new decimal(array4);
			this.numericUpDown_17.Location = new Point(369, 210);
			NumericUpDown numericUpDown5 = this.numericUpDown_17;
			int[] array5 = new int[4];
			array5[0] = 20;
			numericUpDown5.Maximum = new decimal(array5);
			NumericUpDown numericUpDown6 = this.numericUpDown_17;
			int[] array6 = new int[4];
			array6[0] = 1;
			numericUpDown6.Minimum = new decimal(array6);
			this.numericUpDown_17.Name = FlippingForm.getString_0(107352767);
			this.numericUpDown_17.Size = new Size(56, 20);
			this.numericUpDown_17.TabIndex = 34;
			NumericUpDown numericUpDown7 = this.numericUpDown_17;
			int[] array7 = new int[4];
			array7[0] = 1;
			numericUpDown7.Value = new decimal(array7);
			this.label_26.AutoSize = true;
			this.label_26.Location = new Point(269, 212);
			this.label_26.Name = FlippingForm.getString_0(107352738);
			this.label_26.Size = new Size(96, 14);
			this.label_26.TabIndex = 33;
			this.label_26.Text = FlippingForm.getString_0(107352745);
			NumericUpDown numericUpDown8 = this.numericUpDown_18;
			int[] array8 = new int[4];
			array8[0] = 10;
			numericUpDown8.Increment = new decimal(array8);
			this.numericUpDown_18.Location = new Point(108, 210);
			NumericUpDown numericUpDown9 = this.numericUpDown_18;
			int[] array9 = new int[4];
			array9[0] = 20;
			numericUpDown9.Maximum = new decimal(array9);
			NumericUpDown numericUpDown10 = this.numericUpDown_18;
			int[] array10 = new int[4];
			array10[0] = 1;
			numericUpDown10.Minimum = new decimal(array10);
			this.numericUpDown_18.Name = FlippingForm.getString_0(107352720);
			this.numericUpDown_18.Size = new Size(56, 20);
			this.numericUpDown_18.TabIndex = 32;
			NumericUpDown numericUpDown11 = this.numericUpDown_18;
			int[] array11 = new int[4];
			array11[0] = 1;
			numericUpDown11.Value = new decimal(array11);
			this.label_27.AutoSize = true;
			this.label_27.Location = new Point(3, 212);
			this.label_27.Name = FlippingForm.getString_0(107353203);
			this.label_27.Size = new Size(96, 14);
			this.label_27.TabIndex = 31;
			this.label_27.Text = FlippingForm.getString_0(107352745);
			this.numericUpDown_15.Location = new Point(399, 90);
			NumericUpDown numericUpDown12 = this.numericUpDown_15;
			int[] array12 = new int[4];
			array12[0] = 10;
			numericUpDown12.Maximum = new decimal(array12);
			NumericUpDown numericUpDown13 = this.numericUpDown_15;
			int[] array13 = new int[4];
			array13[0] = 1;
			numericUpDown13.Minimum = new decimal(array13);
			this.numericUpDown_15.Name = FlippingForm.getString_0(107353146);
			this.numericUpDown_15.Size = new Size(56, 20);
			this.numericUpDown_15.TabIndex = 30;
			NumericUpDown numericUpDown14 = this.numericUpDown_15;
			int[] array14 = new int[4];
			array14[0] = 1;
			numericUpDown14.Value = new decimal(array14);
			this.numericUpDown_16.Location = new Point(399, 65);
			NumericUpDown numericUpDown15 = this.numericUpDown_16;
			int[] array15 = new int[4];
			array15[0] = 50;
			numericUpDown15.Maximum = new decimal(array15);
			this.numericUpDown_16.Name = FlippingForm.getString_0(107353121);
			this.numericUpDown_16.Size = new Size(56, 20);
			this.numericUpDown_16.TabIndex = 29;
			this.label_24.AutoSize = true;
			this.label_24.Location = new Point(269, 91);
			this.label_24.Name = FlippingForm.getString_0(107385422);
			this.label_24.Size = new Size(127, 14);
			this.label_24.TabIndex = 28;
			this.label_24.Text = FlippingForm.getString_0(107353128);
			this.label_25.AutoSize = true;
			this.label_25.Location = new Point(269, 67);
			this.label_25.Name = FlippingForm.getString_0(107385350);
			this.label_25.Size = new Size(127, 14);
			this.label_25.TabIndex = 27;
			this.label_25.Text = FlippingForm.getString_0(107353059);
			this.label_22.AutoSize = true;
			this.label_22.Location = new Point(232, 93);
			this.label_22.Name = FlippingForm.getString_0(107384980);
			this.label_22.Size = new Size(16, 14);
			this.label_22.TabIndex = 24;
			this.label_22.Text = FlippingForm.getString_0(107414875);
			this.numericUpDown_14.DecimalPlaces = 2;
			this.numericUpDown_14.Increment = new decimal(new int[]
			{
				5,
				0,
				0,
				65536
			});
			this.numericUpDown_14.Location = new Point(176, 90);
			this.numericUpDown_14.Name = FlippingForm.getString_0(107353022);
			this.numericUpDown_14.Size = new Size(56, 20);
			this.numericUpDown_14.TabIndex = 26;
			this.label_23.AutoSize = true;
			this.label_23.Location = new Point(3, 92);
			this.label_23.Name = FlippingForm.getString_0(107385441);
			this.label_23.Size = new Size(170, 14);
			this.label_23.TabIndex = 25;
			this.label_23.Text = FlippingForm.getString_0(107352993);
			this.checkBox_2.AutoSize = true;
			this.checkBox_2.Checked = true;
			this.checkBox_2.CheckState = CheckState.Checked;
			this.checkBox_2.Location = new Point(6, 43);
			this.checkBox_2.Name = FlippingForm.getString_0(107352948);
			this.checkBox_2.Size = new Size(238, 18);
			this.checkBox_2.TabIndex = 23;
			this.checkBox_2.Text = FlippingForm.getString_0(107352407);
			this.checkBox_2.UseVisualStyleBackColor = true;
			this.checkBox_2.CheckedChanged += this.checkBox_2_CheckedChanged;
			this.checkBox_1.AutoSize = true;
			this.checkBox_1.Location = new Point(6, 67);
			this.checkBox_1.Name = FlippingForm.getString_0(107352354);
			this.checkBox_1.Size = new Size(107, 18);
			this.checkBox_1.TabIndex = 22;
			this.checkBox_1.Text = FlippingForm.getString_0(107352365);
			this.checkBox_1.UseVisualStyleBackColor = true;
			this.checkBox_1.CheckedChanged += this.checkBox_1_CheckedChanged;
			NumericUpDown numericUpDown16 = this.numericUpDown_12;
			int[] array16 = new int[4];
			array16[0] = 10;
			numericUpDown16.Increment = new decimal(array16);
			this.numericUpDown_12.Location = new Point(369, 238);
			NumericUpDown numericUpDown17 = this.numericUpDown_12;
			int[] array17 = new int[4];
			array17[0] = 20;
			numericUpDown17.Maximum = new decimal(array17);
			NumericUpDown numericUpDown18 = this.numericUpDown_12;
			int[] array18 = new int[4];
			array18[0] = 1;
			numericUpDown18.Minimum = new decimal(array18);
			this.numericUpDown_12.Name = FlippingForm.getString_0(107352312);
			this.numericUpDown_12.Size = new Size(56, 20);
			this.numericUpDown_12.TabIndex = 21;
			NumericUpDown numericUpDown19 = this.numericUpDown_12;
			int[] array19 = new int[4];
			array19[0] = 1;
			numericUpDown19.Value = new decimal(array19);
			this.label_18.AutoSize = true;
			this.label_18.Location = new Point(269, 240);
			this.label_18.Name = FlippingForm.getString_0(107352283);
			this.label_18.Size = new Size(98, 14);
			this.label_18.TabIndex = 20;
			this.label_18.Text = FlippingForm.getString_0(107352258);
			NumericUpDown numericUpDown20 = this.numericUpDown_13;
			int[] array20 = new int[4];
			array20[0] = 10;
			numericUpDown20.Increment = new decimal(array20);
			this.numericUpDown_13.Location = new Point(108, 238);
			NumericUpDown numericUpDown21 = this.numericUpDown_13;
			int[] array21 = new int[4];
			array21[0] = 20;
			numericUpDown21.Maximum = new decimal(array21);
			NumericUpDown numericUpDown22 = this.numericUpDown_13;
			int[] array22 = new int[4];
			array22[0] = 1;
			numericUpDown22.Minimum = new decimal(array22);
			this.numericUpDown_13.Name = FlippingForm.getString_0(107352265);
			this.numericUpDown_13.Size = new Size(56, 20);
			this.numericUpDown_13.TabIndex = 19;
			NumericUpDown numericUpDown23 = this.numericUpDown_13;
			int[] array23 = new int[4];
			array23[0] = 1;
			numericUpDown23.Value = new decimal(array23);
			this.label_19.AutoSize = true;
			this.label_19.Location = new Point(3, 240);
			this.label_19.Name = FlippingForm.getString_0(107352236);
			this.label_19.Size = new Size(98, 14);
			this.label_19.TabIndex = 18;
			this.label_19.Text = FlippingForm.getString_0(107352258);
			this.numericUpDown_11.Location = new Point(108, 182);
			NumericUpDown numericUpDown24 = this.numericUpDown_11;
			int[] array24 = new int[4];
			array24[0] = 100000;
			numericUpDown24.Maximum = new decimal(array24);
			NumericUpDown numericUpDown25 = this.numericUpDown_11;
			int[] array25 = new int[4];
			array25[0] = 1;
			numericUpDown25.Minimum = new decimal(array25);
			this.numericUpDown_11.Name = FlippingForm.getString_0(107352211);
			this.numericUpDown_11.Size = new Size(56, 20);
			this.numericUpDown_11.TabIndex = 17;
			NumericUpDown numericUpDown26 = this.numericUpDown_11;
			int[] array26 = new int[4];
			array26[0] = 1;
			numericUpDown26.Value = new decimal(array26);
			this.label_16.AutoSize = true;
			this.label_16.Location = new Point(3, 184);
			this.label_16.Name = FlippingForm.getString_0(107352666);
			this.label_16.Size = new Size(96, 14);
			this.label_16.TabIndex = 16;
			this.label_16.Text = FlippingForm.getString_0(107352788);
			this.label_4.AutoSize = true;
			this.label_4.Location = new Point(232, 121);
			this.label_4.Name = FlippingForm.getString_0(107388565);
			this.label_4.Size = new Size(16, 14);
			this.label_4.TabIndex = 7;
			this.label_4.Text = FlippingForm.getString_0(107414875);
			this.numericUpDown_4.DecimalPlaces = 2;
			this.numericUpDown_4.Increment = new decimal(new int[]
			{
				5,
				0,
				0,
				65536
			});
			this.numericUpDown_4.Location = new Point(176, 118);
			this.numericUpDown_4.Name = FlippingForm.getString_0(107352677);
			this.numericUpDown_4.Size = new Size(56, 20);
			this.numericUpDown_4.TabIndex = 15;
			this.label_5.AutoSize = true;
			this.label_5.Location = new Point(3, 121);
			this.label_5.Name = FlippingForm.getString_0(107388563);
			this.label_5.Size = new Size(85, 14);
			this.label_5.TabIndex = 14;
			this.label_5.Text = FlippingForm.getString_0(107352632);
			this.numericUpDown_5.Location = new Point(399, 41);
			NumericUpDown numericUpDown27 = this.numericUpDown_5;
			int[] array27 = new int[4];
			array27[0] = 10;
			numericUpDown27.Maximum = new decimal(array27);
			NumericUpDown numericUpDown28 = this.numericUpDown_5;
			int[] array28 = new int[4];
			array28[0] = 1;
			numericUpDown28.Minimum = new decimal(array28);
			this.numericUpDown_5.Name = FlippingForm.getString_0(107352611);
			this.numericUpDown_5.Size = new Size(56, 20);
			this.numericUpDown_5.TabIndex = 13;
			NumericUpDown numericUpDown29 = this.numericUpDown_5;
			int[] array29 = new int[4];
			array29[0] = 1;
			numericUpDown29.Value = new decimal(array29);
			this.numericUpDown_6.Location = new Point(399, 17);
			NumericUpDown numericUpDown30 = this.numericUpDown_6;
			int[] array30 = new int[4];
			array30[0] = 50;
			numericUpDown30.Maximum = new decimal(array30);
			this.numericUpDown_6.Name = FlippingForm.getString_0(107352618);
			this.numericUpDown_6.Size = new Size(56, 20);
			this.numericUpDown_6.TabIndex = 12;
			this.label_2.AutoSize = true;
			this.label_2.Location = new Point(269, 43);
			this.label_2.Name = FlippingForm.getString_0(107389013);
			this.label_2.Size = new Size(127, 14);
			this.label_2.TabIndex = 8;
			this.label_2.Text = FlippingForm.getString_0(107352593);
			this.label_3.AutoSize = true;
			this.label_3.Location = new Point(269, 19);
			this.label_3.Name = FlippingForm.getString_0(107386750);
			this.label_3.Size = new Size(127, 14);
			this.label_3.TabIndex = 7;
			this.label_3.Text = FlippingForm.getString_0(107352560);
			this.groupBox_2.Controls.Add(this.label_7);
			this.groupBox_2.Controls.Add(this.label_8);
			this.groupBox_2.Controls.Add(this.label_9);
			this.groupBox_2.Controls.Add(this.label_10);
			this.groupBox_2.Controls.Add(this.numericUpDown_8);
			this.groupBox_2.Controls.Add(this.numericUpDown_9);
			this.groupBox_2.Controls.Add(this.label_11);
			this.groupBox_2.Controls.Add(this.label_12);
			this.groupBox_2.Controls.Add(this.numericUpDown_10);
			this.groupBox_2.Controls.Add(this.label_13);
			this.groupBox_2.Controls.Add(this.numericUpDown_7);
			this.groupBox_2.Controls.Add(this.label_6);
			this.groupBox_2.Location = new Point(3, 398);
			this.groupBox_2.Name = FlippingForm.getString_0(107414064);
			this.groupBox_2.Size = new Size(462, 124);
			this.groupBox_2.TabIndex = 7;
			this.groupBox_2.TabStop = false;
			this.groupBox_2.Text = FlippingForm.getString_0(107352527);
			this.label_7.AutoSize = true;
			this.label_7.Location = new Point(153, 105);
			this.label_7.Name = FlippingForm.getString_0(107352478);
			this.label_7.Size = new Size(38, 14);
			this.label_7.TabIndex = 25;
			this.label_7.Text = FlippingForm.getString_0(107352497);
			this.label_8.AutoSize = true;
			this.label_8.Location = new Point(153, 75);
			this.label_8.Name = FlippingForm.getString_0(107352488);
			this.label_8.Size = new Size(38, 14);
			this.label_8.TabIndex = 24;
			this.label_8.Text = FlippingForm.getString_0(107352497);
			this.label_9.AutoSize = true;
			this.label_9.Location = new Point(153, 46);
			this.label_9.Name = FlippingForm.getString_0(107352443);
			this.label_9.Size = new Size(38, 14);
			this.label_9.TabIndex = 23;
			this.label_9.Text = FlippingForm.getString_0(107352462);
			this.label_10.AutoSize = true;
			this.label_10.Location = new Point(153, 17);
			this.label_10.Name = FlippingForm.getString_0(107352453);
			this.label_10.Size = new Size(38, 14);
			this.label_10.TabIndex = 12;
			this.label_10.Text = FlippingForm.getString_0(107352462);
			this.numericUpDown_8.Location = new Point(76, 101);
			NumericUpDown numericUpDown31 = this.numericUpDown_8;
			int[] array31 = new int[4];
			array31[0] = 10000000;
			numericUpDown31.Maximum = new decimal(array31);
			this.numericUpDown_8.Name = FlippingForm.getString_0(107351896);
			this.numericUpDown_8.Size = new Size(72, 20);
			this.numericUpDown_8.TabIndex = 22;
			this.numericUpDown_9.Location = new Point(76, 72);
			NumericUpDown numericUpDown32 = this.numericUpDown_9;
			int[] array32 = new int[4];
			array32[0] = 100000;
			numericUpDown32.Maximum = new decimal(array32);
			this.numericUpDown_9.Name = FlippingForm.getString_0(107351871);
			this.numericUpDown_9.Size = new Size(72, 20);
			this.numericUpDown_9.TabIndex = 21;
			this.label_11.AutoSize = true;
			this.label_11.Location = new Point(3, 103);
			this.label_11.Name = FlippingForm.getString_0(107385470);
			this.label_11.Size = new Size(69, 14);
			this.label_11.TabIndex = 20;
			this.label_11.Text = FlippingForm.getString_0(107351878);
			this.label_12.AutoSize = true;
			this.label_12.Location = new Point(3, 74);
			this.label_12.Name = FlippingForm.getString_0(107386829);
			this.label_12.Size = new Size(67, 14);
			this.label_12.TabIndex = 19;
			this.label_12.Text = FlippingForm.getString_0(107351829);
			this.numericUpDown_10.Location = new Point(76, 43);
			NumericUpDown numericUpDown33 = this.numericUpDown_10;
			int[] array33 = new int[4];
			array33[0] = 10000000;
			numericUpDown33.Maximum = new decimal(array33);
			this.numericUpDown_10.Name = FlippingForm.getString_0(107351844);
			this.numericUpDown_10.Size = new Size(72, 20);
			this.numericUpDown_10.TabIndex = 18;
			this.label_13.AutoSize = true;
			this.label_13.Location = new Point(3, 45);
			this.label_13.Name = FlippingForm.getString_0(107386200);
			this.label_13.Size = new Size(69, 14);
			this.label_13.TabIndex = 17;
			this.label_13.Text = FlippingForm.getString_0(107351878);
			this.numericUpDown_7.Location = new Point(76, 14);
			NumericUpDown numericUpDown34 = this.numericUpDown_7;
			int[] array34 = new int[4];
			array34[0] = 100000;
			numericUpDown34.Maximum = new decimal(array34);
			this.numericUpDown_7.Name = FlippingForm.getString_0(107351819);
			this.numericUpDown_7.Size = new Size(72, 20);
			this.numericUpDown_7.TabIndex = 16;
			this.label_6.AutoSize = true;
			this.label_6.Location = new Point(3, 16);
			this.label_6.Name = FlippingForm.getString_0(107386068);
			this.label_6.Size = new Size(67, 14);
			this.label_6.TabIndex = 0;
			this.label_6.Text = FlippingForm.getString_0(107351829);
			this.button_0.Location = new Point(369, 522);
			this.button_0.Name = FlippingForm.getString_0(107351794);
			this.button_0.Size = new Size(96, 25);
			this.button_0.TabIndex = 11;
			this.button_0.Text = FlippingForm.getString_0(107351781);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.button_3.Location = new Point(267, 522);
			this.button_3.Name = FlippingForm.getString_0(107351760);
			this.button_3.Size = new Size(96, 25);
			this.button_3.TabIndex = 10;
			this.button_3.Text = FlippingForm.getString_0(107351711);
			this.button_3.UseVisualStyleBackColor = true;
			this.button_3.Click += this.button_3_Click;
			this.button_4.Location = new Point(166, 522);
			this.button_4.Name = FlippingForm.getString_0(107351722);
			this.button_4.Size = new Size(96, 25);
			this.button_4.TabIndex = 9;
			this.button_4.Text = FlippingForm.getString_0(107351673);
			this.button_4.UseVisualStyleBackColor = true;
			this.button_4.Click += this.button_4_Click;
			base.AutoScaleDimensions = new SizeF(7f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(467, 548);
			base.Controls.Add(this.button_4);
			base.Controls.Add(this.button_3);
			base.Controls.Add(this.button_0);
			base.Controls.Add(this.groupBox_2);
			base.Controls.Add(this.groupBox_1);
			base.Controls.Add(this.groupBox_0);
			this.Font = new Font(FlippingForm.getString_0(107397849), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = FlippingForm.getString_0(107351684);
			this.Text = FlippingForm.getString_0(107351684);
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_0.PerformLayout();
			((ISupportInitialize)this.pictureBox_0).EndInit();
			((ISupportInitialize)this.pictureBox_1).EndInit();
			((ISupportInitialize)this.numericUpDown_0).EndInit();
			((ISupportInitialize)this.numericUpDown_1).EndInit();
			((ISupportInitialize)this.numericUpDown_2).EndInit();
			((ISupportInitialize)this.numericUpDown_3).EndInit();
			this.groupBox_1.ResumeLayout(false);
			this.groupBox_1.PerformLayout();
			((ISupportInitialize)this.pictureBox_2).EndInit();
			((ISupportInitialize)this.pictureBox_3).EndInit();
			((ISupportInitialize)this.numericUpDown_19).EndInit();
			((ISupportInitialize)this.numericUpDown_17).EndInit();
			((ISupportInitialize)this.numericUpDown_18).EndInit();
			((ISupportInitialize)this.numericUpDown_15).EndInit();
			((ISupportInitialize)this.numericUpDown_16).EndInit();
			((ISupportInitialize)this.numericUpDown_14).EndInit();
			((ISupportInitialize)this.numericUpDown_12).EndInit();
			((ISupportInitialize)this.numericUpDown_13).EndInit();
			((ISupportInitialize)this.numericUpDown_11).EndInit();
			((ISupportInitialize)this.numericUpDown_4).EndInit();
			((ISupportInitialize)this.numericUpDown_5).EndInit();
			((ISupportInitialize)this.numericUpDown_6).EndInit();
			this.groupBox_2.ResumeLayout(false);
			this.groupBox_2.PerformLayout();
			((ISupportInitialize)this.numericUpDown_8).EndInit();
			((ISupportInitialize)this.numericUpDown_9).EndInit();
			((ISupportInitialize)this.numericUpDown_10).EndInit();
			((ISupportInitialize)this.numericUpDown_7).EndInit();
			base.ResumeLayout(false);
		}

		[CompilerGenerated]
		private bool method_5(FlippingListItem flippingListItem_1)
		{
			return flippingListItem_1.HaveName == this.flippingListItem_0.HaveName && flippingListItem_1.WantName == this.flippingListItem_0.WantName;
		}

		static FlippingForm()
		{
			Strings.CreateGetStringDelegate(typeof(FlippingForm));
		}

		private FlippingListItem flippingListItem_0;

		private IContainer icontainer_0 = null;

		private CheckBox checkBox_0;

		private Label label_0;

		private GroupBox groupBox_0;

		private NumericUpDown numericUpDown_0;

		private NumericUpDown numericUpDown_1;

		private NumericUpDown numericUpDown_2;

		private NumericUpDown numericUpDown_3;

		private Label label_1;

		private GroupBox groupBox_1;

		private Label label_2;

		private Label label_3;

		private Label label_4;

		private NumericUpDown numericUpDown_4;

		private Label label_5;

		private NumericUpDown numericUpDown_5;

		private NumericUpDown numericUpDown_6;

		private GroupBox groupBox_2;

		private NumericUpDown numericUpDown_7;

		private Label label_6;

		private Label label_7;

		private Label label_8;

		private Label label_9;

		private Label label_10;

		private NumericUpDown numericUpDown_8;

		private NumericUpDown numericUpDown_9;

		private Label label_11;

		private Label label_12;

		private NumericUpDown numericUpDown_10;

		private Label label_13;

		private Button button_0;

		private Label label_14;

		private Label label_15;

		private PictureBox pictureBox_0;

		private PictureBox pictureBox_1;

		private Button button_1;

		private NumericUpDown numericUpDown_11;

		private Label label_16;

		private Button button_2;

		private Label label_17;

		private NumericUpDown numericUpDown_12;

		private Label label_18;

		private NumericUpDown numericUpDown_13;

		private Label label_19;

		private Label label_20;

		private Label label_21;

		private CheckBox checkBox_1;

		private CheckBox checkBox_2;

		private Label label_22;

		private NumericUpDown numericUpDown_14;

		private Label label_23;

		private NumericUpDown numericUpDown_15;

		private NumericUpDown numericUpDown_16;

		private Label label_24;

		private Label label_25;

		private NumericUpDown numericUpDown_17;

		private Label label_26;

		private NumericUpDown numericUpDown_18;

		private Label label_27;

		private NumericUpDown numericUpDown_19;

		private Label label_28;

		private PictureBox pictureBox_2;

		private PictureBox pictureBox_3;

		private Button button_3;

		private Button button_4;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
