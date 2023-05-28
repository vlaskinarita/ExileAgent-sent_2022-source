using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using BrightIdeasSoftware;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ns0;
using ns1;
using ns10;
using ns12;
using ns14;
using ns15;
using ns16;
using ns17;
using ns18;
using ns19;
using ns2;
using ns20;
using ns21;
using ns22;
using ns23;
using ns25;
using ns27;
using ns29;
using ns32;
using ns33;
using ns34;
using ns35;
using ns36;
using ns39;
using ns42;
using ns6;
using ns7;
using ns8;
using ns9;
using PoEv2.BuyingForms;
using PoEv2.Classes;
using PoEv2.Features;
using PoEv2.Features.Crafting;
using PoEv2.Features.Expedition;
using PoEv2.Handlers;
using PoEv2.Handlers.Events.Messages;
using PoEv2.Handlers.Events.Orders;
using PoEv2.Handlers.Events.Trades;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using PoEv2.Models.Items;
using PoEv2.Models.Query;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2
{
	public sealed partial class MainForm : Form
	{
		private unsafe void dataGridView_2_MouseClick(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[3];
			MainForm.Class13 @class = new MainForm.Class13();
			@class.mainForm_0 = this;
			*(byte*)ptr = ((e.Button != MouseButtons.Right) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				@class.dataGridView_0 = (sender as DataGridView);
				@class.int_0 = @class.dataGridView_0.HitTest(e.X, e.Y).RowIndex;
				((byte*)ptr)[1] = ((@class.int_0 == -1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					@class.string_0 = @class.dataGridView_0[MainForm.getString_0(107396816), @class.int_0].Value.ToString();
					@class.string_1 = @class.dataGridView_0[MainForm.getString_0(107396827), @class.int_0].Value.ToString();
					@class.bulkBuyingListItem_0 = Class255.BulkBuyingList.FirstOrDefault(new Func<BulkBuyingListItem, bool>(@class.method_0));
					((byte*)ptr)[2] = ((@class.bulkBuyingListItem_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						ContextMenu contextMenu = new ContextMenu();
						contextMenu.MenuItems.Add(MainForm.getString_0(107396774), new EventHandler(@class.method_1));
						contextMenu.MenuItems.Add(MainForm.getString_0(107396785), new EventHandler(@class.method_2));
						contextMenu.MenuItems.Add(MainForm.getString_0(107396740), new EventHandler(@class.method_3));
						contextMenu.Show(@class.dataGridView_0, new Point(e.X, e.Y));
					}
				}
			}
		}

		private void button_31_Click(object sender, EventArgs e)
		{
			ExchangePairForm exchangePairForm = new ExchangePairForm();
			exchangePairForm.OnPairSavedEvent += this.method_150;
			exchangePairForm.Show();
		}

		public void method_0(string string_11, string string_12)
		{
			MainForm.Class14 @class = new MainForm.Class14();
			@class.string_0 = string_11;
			@class.string_1 = string_12;
			if (!Class255.BulkBuyingList.Any(new Func<BulkBuyingListItem, bool>(@class.method_0)))
			{
				BulkBuyingListItem bulkBuyingListItem = new BulkBuyingListItem(@class.string_0, @class.string_1);
				Class255.BulkBuyingList.Add(bulkBuyingListItem);
				this.dataGridView_2.Rows.Add(bulkBuyingListItem.ToDataGrid());
				Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, Class255.BulkBuyingList, true);
			}
		}

		private unsafe void dataGridView_2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((e.RowIndex == -1) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = ((e.ColumnIndex == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.dataGridView_2.EndEdit();
				}
			}
		}

		private unsafe void dataGridView_2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			void* ptr = stackalloc byte[22];
			e.Control.KeyPress -= this.method_25;
			e.Control.KeyPress -= this.method_26;
			e.Control.KeyPress -= this.method_27;
			*(int*)ptr = this.dataGridView_2.Columns[MainForm.getString_0(107396763)].Index;
			*(int*)((byte*)ptr + 4) = this.dataGridView_2.Columns[MainForm.getString_0(107396706)].Index;
			*(int*)((byte*)ptr + 8) = this.dataGridView_2.Columns[MainForm.getString_0(107396677)].Index;
			*(int*)((byte*)ptr + 12) = this.dataGridView_2.CurrentCell.ColumnIndex;
			((byte*)ptr)[16] = ((*(int*)((byte*)ptr + 12) == *(int*)((byte*)ptr + 4)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				TextBox textBox = e.Control as TextBox;
				((byte*)ptr)[17] = ((textBox != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 17) != 0)
				{
					textBox.KeyPress += this.method_25;
				}
			}
			else
			{
				((byte*)ptr)[18] = ((*(int*)((byte*)ptr + 12) == *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 18) != 0)
				{
					TextBox textBox2 = e.Control as TextBox;
					((byte*)ptr)[19] = ((textBox2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 19) != 0)
					{
						textBox2.KeyPress += this.method_27;
					}
				}
				else
				{
					((byte*)ptr)[20] = ((*(int*)((byte*)ptr + 12) == *(int*)((byte*)ptr + 8)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 20) != 0)
					{
						TextBox textBox3 = e.Control as TextBox;
						((byte*)ptr)[21] = ((textBox3 != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 21) != 0)
						{
							textBox3.KeyPress += this.method_26;
						}
					}
				}
			}
		}

		public void dataGridView_2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1)
			{
				this.method_103(true);
			}
		}

		private void button_94_Click(object sender, EventArgs e)
		{
			this.method_145((Button)sender, new Action(this.method_1), new Action(this.method_2));
		}

		private void method_1()
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Class255.class105_0.method_3(ConfigOptions.LastImportDirectory);
				openFileDialog.Filter = MainForm.getString_0(107396648);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					List<BulkBuyingListItem> list = JsonConvert.DeserializeObject<List<BulkBuyingListItem>>(File.ReadAllText(openFileDialog.FileName));
					Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, list, true);
					this.dataGridView_2.Rows.Clear();
					foreach (BulkBuyingListItem bulkBuyingListItem in Class255.BulkBuyingList)
					{
						this.dataGridView_2.Rows.Add(bulkBuyingListItem.ToDataGrid());
					}
				}
			}
		}

		private void method_2()
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = Class255.class105_0.method_3(ConfigOptions.LastImportDirectory);
				saveFileDialog.Filter = MainForm.getString_0(107396648);
				saveFileDialog.FileName = MainForm.getString_0(107397151);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string contents = JsonConvert.SerializeObject(Class255.BulkBuyingList, Formatting.Indented, Util.smethod_30());
					File.WriteAllText(saveFileDialog.FileName, contents);
				}
			}
		}

		private unsafe void dataGridView_2_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[11];
			MainForm.Class15 @class = new MainForm.Class15();
			DataGridView dataGridView = sender as DataGridView;
			*(int*)ptr = dataGridView.HitTest(e.X, e.Y).RowIndex;
			*(int*)((byte*)ptr + 4) = dataGridView.HitTest(e.X, e.Y).ColumnIndex;
			((byte*)ptr)[8] = ((*(int*)ptr == -1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				((byte*)ptr)[9] = ((*(int*)((byte*)ptr + 4) <= 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) == 0)
				{
					@class.string_0 = dataGridView[MainForm.getString_0(107396816), *(int*)ptr].Value.ToString();
					@class.string_1 = dataGridView[MainForm.getString_0(107396827), *(int*)ptr].Value.ToString();
					BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList.FirstOrDefault(new Func<BulkBuyingListItem, bool>(@class.method_0));
					((byte*)ptr)[10] = ((bulkBuyingListItem == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) == 0)
					{
						this.method_3(bulkBuyingListItem);
					}
				}
			}
		}

		private void method_3(BulkBuyingListItem bulkBuyingListItem_0)
		{
			BulkBuyForm bulkBuyForm = new BulkBuyForm(this, bulkBuyingListItem_0);
			bulkBuyForm.Show();
			bulkBuyForm.method_0();
		}

		private void textBox_17_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			string text = textBox.Text;
			char c = (text.Length > 0) ? text[0] : e.KeyChar;
			char c2 = c;
			char c3 = c2;
			switch (c3)
			{
			case '#':
				this.method_4(MainForm.getString_0(107397102));
				break;
			case '$':
				this.method_4(MainForm.getString_0(107397071));
				break;
			case '%':
				this.method_4(MainForm.getString_0(107397093));
				break;
			case '&':
				this.method_4(MainForm.getString_0(107397062));
				break;
			default:
				if (c3 != '@')
				{
					this.method_4(MainForm.getString_0(107397085));
				}
				else
				{
					this.method_4(MainForm.getString_0(107397116));
				}
				break;
			}
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				if (!text.smethod_25() && (this.genum0_0 == MainForm.GEnum0.const_0 || MainForm.IsPaused))
				{
					string text2;
					if (this.comboBox_57.smethod_1() == MainForm.getString_0(107397116) && this.comboBox_56.SelectedIndex > 0)
					{
						text2 = string.Format(MainForm.getString_0(107397076), this.comboBox_56.smethod_1(), text);
					}
					else
					{
						text2 = text;
					}
					textBox.Text = string.Empty;
					this.list_0.Add(text2);
					this.int_0 = this.list_0.Count - 1;
					MainForm.smethod_0(text2);
				}
			}
		}

		private static void smethod_0(string string_11)
		{
			if (ProcessHelper.smethod_9())
			{
				Win32.smethod_16(string_11, true, true, true, true);
			}
		}

		private unsafe void textBox_17_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((e.KeyCode == Keys.Down) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.method_5(1);
			}
			((byte*)ptr)[1] = ((e.KeyCode == Keys.Up) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.method_5(-1);
			}
		}

		private void textBox_17_Enter(object sender, EventArgs e)
		{
			this.textBox_17.Text = string.Empty;
		}

		private void textBox_17_Leave(object sender, EventArgs e)
		{
			if (this.textBox_17.Text.smethod_25())
			{
				this.textBox_17.Text = MainForm.getString_0(107397031);
			}
		}

		private void method_4(string string_11)
		{
			if (this.comboBox_56.SelectedIndex > 0)
			{
				this.comboBox_57.SelectedItem = MainForm.getString_0(107397116);
			}
			else
			{
				this.comboBox_57.SelectedItem = string_11;
			}
		}

		private unsafe void method_5(int int_7)
		{
			void* ptr = stackalloc byte[7];
			((byte*)ptr)[4] = ((!this.list_0.Any<string>()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				*(int*)ptr = this.int_0 + int_7;
				((byte*)ptr)[5] = ((*(int*)ptr < 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					*(int*)ptr = 0;
				}
				((byte*)ptr)[6] = ((*(int*)ptr >= this.list_0.Count) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					*(int*)ptr = this.list_0.Count - 1;
				}
				this.textBox_17.Text = this.list_0[*(int*)ptr];
				this.textBox_17.SelectionStart = this.textBox_17.Text.Length;
				this.textBox_17.SelectionLength = 0;
				this.int_0 = *(int*)ptr;
			}
		}

		private void method_6()
		{
			base.Invoke(new Action(this.method_151));
		}

		private void comboBox_56_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBox_56.SelectedIndex > 0)
			{
				this.method_4(MainForm.getString_0(107397116));
			}
			else
			{
				this.method_4(MainForm.getString_0(107397085));
			}
		}

		private unsafe void comboBox_57_SelectedIndexChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_57.smethod_1() != MainForm.getString_0(107397116)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = ((this.comboBox_56.Items.Count > 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.comboBox_56.SelectedIndex = 0;
				}
			}
		}

		public void method_7(string string_11)
		{
			MainForm.Class16 @class = new MainForm.Class16();
			@class.mainForm_0 = this;
			@class.string_0 = string_11;
			if (!this.list_1.Contains(@class.string_0))
			{
				this.list_1.Insert(1, @class.string_0);
				this.list_1 = this.list_1.Take(10).ToList<string>();
				this.method_6();
			}
			base.Invoke(new Action(@class.method_0));
		}

		private void button_95_Click(object sender, EventArgs e)
		{
			if (this.comboBox_56.SelectedIndex > 0 && (this.genum0_0 == MainForm.GEnum0.const_0 || MainForm.IsPaused))
			{
				MainForm.smethod_0(MainForm.getString_0(107397014) + this.comboBox_56.smethod_1());
			}
		}

		private void button_34_Click(object sender, EventArgs e)
		{
			JsonTab jsonTab = (JsonTab)this.comboBox_34.SelectedItem;
			if (jsonTab != null)
			{
				Form5 form = new Form5(jsonTab.i);
				form.OnItemChosenEvent += this.method_8;
				form.Show();
			}
		}

		public unsafe void method_8(JsonItem jsonItem_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((jsonItem_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				this.class299_0.InitialItem = jsonItem_0;
				this.panel_0.BackColor = Class120.color_0;
				this.panel_0.BackgroundImage = this.class299_0.InitialItem.ItemImage;
				((byte*)ptr)[1] = ((this.class299_0.ToolTip == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.class299_0.ToolTip = new ToolTip
					{
						InitialDelay = 100,
						AutoPopDelay = 60000
					};
				}
				this.class299_0.ToolTip.SetToolTip(this.panel_0, jsonItem_0.method_3());
				string text = ItemData.smethod_0(jsonItem_0);
				((byte*)ptr)[2] = ((text == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[3] = (jsonItem_0.IsMap ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						MessageBox.Show(MainForm.getString_0(107396969));
						this.tabControl_11.SelectedTab = this.tabPage_20;
					}
					else
					{
						MessageBox.Show(string.Format(MainForm.getString_0(107396912), API.smethod_15(jsonItem_0)));
					}
				}
				else
				{
					this.class299_0.Mods = JsonConvert.DeserializeObject<Mods>(text, new JsonConverter[]
					{
						new Mods.GClass21()
					});
					this.class299_0.Mods.method_0(jsonItem_0);
					this.method_9();
					this.method_10();
					this.method_12(true);
					this.button_51.Enabled = (jsonItem_0.SocketCount > 0);
					foreach (Button button in this.method_15().Select(new Func<CraftingBoxData, Button>(MainForm.<>c.<>9.method_0)).Concat(this.method_15().Select(new Func<CraftingBoxData, Button>(MainForm.<>c.<>9.method_1))))
					{
						button.Enabled = true;
					}
				}
			}
		}

		private void method_9()
		{
			foreach (CraftingBoxData craftingBoxData in this.method_15())
			{
				craftingBoxData.Box.ClearObjects();
				craftingBoxData.CountBox.SelectedIndex = 0;
				craftingBoxData.AffixBox.Items.Clear();
				craftingBoxData.AffixBox.Items.Add(MainForm.getString_0(107396375));
				craftingBoxData.AffixBox.SelectedIndex = 0;
			}
		}

		private void method_10()
		{
			foreach (ComboBox comboBox in this.method_15().Where(new Func<CraftingBoxData, bool>(MainForm.<>c.<>9.method_2)).Select(new Func<CraftingBoxData, ComboBox>(MainForm.<>c.<>9.method_3)))
			{
				comboBox.Items.smethod_22(this.class299_0.Mods.ModList.Where(new Func<Mod, bool>(MainForm.<>c.<>9.method_4)).GroupBy(new Func<Mod, string>(MainForm.<>c.<>9.method_5)).Select(new Func<IGrouping<string, Mod>, Mod>(MainForm.<>c.<>9.method_6)));
			}
			foreach (ComboBox comboBox2 in this.method_15().Where(new Func<CraftingBoxData, bool>(MainForm.<>c.<>9.method_7)).Select(new Func<CraftingBoxData, ComboBox>(MainForm.<>c.<>9.method_8)))
			{
				comboBox2.Items.smethod_22(this.class299_0.Mods.ModList.Where(new Func<Mod, bool>(MainForm.<>c.<>9.method_9)).GroupBy(new Func<Mod, string>(MainForm.<>c.<>9.method_10)).Select(new Func<IGrouping<string, Mod>, Mod>(MainForm.<>c.<>9.method_11)));
			}
		}

		private unsafe void comboBox_45_SelectedIndexChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[3];
			MainForm.Class17 @class = new MainForm.Class17();
			@class.object_0 = sender;
			*(byte*)ptr = ((this.class299_0.Mods == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				Mod mod = ((ComboBox)@class.object_0).SelectedItem as Mod;
				CraftingBoxData craftingBoxData = this.method_15().First(new Func<CraftingBoxData, bool>(@class.method_0));
				craftingBoxData.TierBox.Items.Clear();
				((byte*)ptr)[1] = ((mod == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					craftingBoxData.TierBox.Items.smethod_22(ModUtilities.smethod_3(this.class299_0.Mods, mod));
					((byte*)ptr)[2] = ((craftingBoxData.TierBox.Items.Count > 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						craftingBoxData.TierBox.SelectedIndex = 0;
					}
				}
			}
		}

		private void method_11()
		{
			base.Invoke(new Action(this.method_152));
		}

		public void method_12(bool bool_23)
		{
			MainForm.Class18 @class = new MainForm.Class18();
			@class.mainForm_0 = this;
			@class.bool_0 = bool_23;
			base.Invoke(new Action(@class.method_0));
		}

		private void method_13()
		{
			using (List<CraftingBoxData>.Enumerator enumerator = this.method_15().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MainForm.Class19 @class = new MainForm.Class19();
					@class.mainForm_0 = this;
					@class.craftingBoxData_0 = enumerator.Current;
					@class.craftingBoxData_0.Box.SmallImageList = new ImageList();
					@class.craftingBoxData_0.Box.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
					@class.craftingBoxData_0.Box.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(AffixItemViewModel.ImageGetter);
					@class.craftingBoxData_0.Box.MouseDown += @class.method_0;
					@class.craftingBoxData_0.AddButton.Click += @class.method_1;
				}
			}
		}

		private unsafe void method_14(FastObjectListView fastObjectListView_25, Mod mod_0, Tier tier_0)
		{
			void* ptr = stackalloc byte[2];
			MainForm.Class20 @class = new MainForm.Class20();
			@class.mod_0 = mod_0;
			*(byte*)ptr = ((@class.mod_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (fastObjectListView_25.Objects.Cast<AffixItemViewModel>().Any(new Func<AffixItemViewModel, bool>(@class.method_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					fastObjectListView_25.AddObject(new AffixItemViewModel(@class.mod_0, tier_0));
				}
			}
		}

		private List<CraftingBoxData> method_15()
		{
			return new List<CraftingBoxData>
			{
				new CraftingBoxData
				{
					Type = Enum18.const_0,
					Box = this.fastObjectListView_7,
					AffixBox = this.comboBox_39,
					TierBox = this.comboBox_40,
					CountBox = this.comboBox_38,
					AddButton = this.button_37,
					LoadButton = this.button_40,
					SaveButton = this.button_39
				},
				new CraftingBoxData
				{
					Type = Enum18.const_1,
					Box = this.fastObjectListView_6,
					AffixBox = this.comboBox_36,
					TierBox = this.comboBox_37,
					CountBox = this.comboBox_35,
					AddButton = this.button_36,
					LoadButton = this.button_40,
					SaveButton = this.button_39
				},
				new CraftingBoxData
				{
					Type = Enum18.const_0,
					Box = this.fastObjectListView_9,
					AffixBox = this.comboBox_45,
					TierBox = this.comboBox_46,
					CountBox = this.comboBox_44,
					AddButton = this.button_45,
					LoadButton = this.button_42,
					SaveButton = this.button_41
				},
				new CraftingBoxData
				{
					Type = Enum18.const_1,
					Box = this.fastObjectListView_8,
					AffixBox = this.comboBox_42,
					TierBox = this.comboBox_43,
					CountBox = this.comboBox_41,
					AddButton = this.button_44,
					LoadButton = this.button_42,
					SaveButton = this.button_41
				}
			};
		}

		private void checkBox_35_CheckedChanged(object sender, EventArgs e)
		{
			this.checkBox_34.Enabled = this.checkBox_35.Checked;
		}

		private string ItemBaseTypeString
		{
			get
			{
				return API.smethod_15(this.class299_0.InitialItem);
			}
		}

		private string InfluenceString
		{
			get
			{
				string result;
				if (this.class299_0.InitialItem.influences == null)
				{
					result = MainForm.getString_0(107396329);
				}
				else
				{
					result = string.Join(MainForm.getString_0(107396352), this.class299_0.InitialItem.influences.Results.Where(new Func<KeyValuePair<string, bool>, bool>(MainForm.<>c.<>9.method_16)).Select(new Func<KeyValuePair<string, bool>, string>(MainForm.<>c.<>9.method_17)));
				}
				return result;
			}
		}

		private void button_42_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Class395.SettingsPath;
				openFileDialog.Filter = MainForm.getString_0(107396347);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					this.method_16((Button)sender, openFileDialog.FileName);
				}
			}
		}

		private void button_41_Click(object sender, EventArgs e)
		{
			string arg = API.smethod_15(this.class299_0.InitialItem).Replace(MainForm.getString_0(107396306), MainForm.getString_0(107396269)).Replace(MainForm.getString_0(107396268), MainForm.getString_0(107396269));
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Class395.SettingsPath;
			saveFileDialog.Filter = MainForm.getString_0(107396347);
			saveFileDialog.FileName = string.Format(MainForm.getString_0(107396263), arg, ((Button)sender).Tag);
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				this.method_17((Button)sender, saveFileDialog.FileName);
			}
		}

		private unsafe void method_16(Button button_101, string string_11)
		{
			void* ptr = stackalloc byte[22];
			MainForm.Class21 @class = new MainForm.Class21();
			@class.button_0 = button_101;
			Class301 class2 = JsonConvert.DeserializeObject<Class301>(File.ReadAllText(string_11));
			CraftingBoxData craftingBoxData = this.method_15().First(new Func<CraftingBoxData, bool>(@class.method_0));
			CraftingBoxData craftingBoxData2 = this.method_15().First(new Func<CraftingBoxData, bool>(@class.method_1));
			if (this.class299_0 == null || this.class299_0.InitialItem == null)
			{
				MessageBox.Show(MainForm.getString_0(107396282));
			}
			else
			{
				((byte*)ptr)[16] = ((this.ItemBaseTypeString != class2.ItemBaseType) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 16) != 0)
				{
					MessageBox.Show(MainForm.getString_0(107396169));
				}
				else
				{
					((byte*)ptr)[17] = ((this.InfluenceString != class2.Influences) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) != 0)
					{
						MessageBox.Show(MainForm.getString_0(107396612));
					}
					else
					{
						craftingBoxData.Box.ClearObjects();
						craftingBoxData2.Box.ClearObjects();
						*(int*)ptr = 0;
						for (;;)
						{
							((byte*)ptr)[19] = ((*(int*)ptr < class2.PrefixSettings.Affixes.Count) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) == 0)
							{
								break;
							}
							MainForm.Class22 class3 = new MainForm.Class22();
							*(int*)((byte*)ptr + 4) = class2.PrefixSettings.Affixes[*(int*)ptr];
							class3.int_0 = class2.PrefixSettings.Tiers[*(int*)ptr];
							Mod mod_ = craftingBoxData.AffixBox.Items.OfType<Mod>().ElementAt(*(int*)((byte*)ptr + 4) - 1);
							Tier tier_ = null;
							((byte*)ptr)[18] = ((class3.int_0 >= 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 18) != 0)
							{
								tier_ = ModUtilities.smethod_3(this.class299_0.Mods, mod_).First(new Func<Tier, bool>(class3.method_0));
							}
							this.method_14(craftingBoxData.Box, mod_, tier_);
							*(int*)ptr = *(int*)ptr + 1;
						}
						*(int*)((byte*)ptr + 8) = 0;
						for (;;)
						{
							((byte*)ptr)[21] = ((*(int*)((byte*)ptr + 8) < class2.SuffixSettings.Affixes.Count) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 21) == 0)
							{
								break;
							}
							MainForm.Class23 class4 = new MainForm.Class23();
							*(int*)((byte*)ptr + 12) = class2.SuffixSettings.Affixes[*(int*)((byte*)ptr + 8)];
							class4.int_0 = class2.SuffixSettings.Tiers[*(int*)((byte*)ptr + 8)];
							Mod mod_2 = craftingBoxData2.AffixBox.Items.OfType<Mod>().ElementAt(*(int*)((byte*)ptr + 12) - 1);
							Tier tier_2 = null;
							((byte*)ptr)[20] = ((class4.int_0 >= 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 20) != 0)
							{
								tier_2 = ModUtilities.smethod_3(this.class299_0.Mods, mod_2).First(new Func<Tier, bool>(class4.method_0));
							}
							this.method_14(craftingBoxData2.Box, mod_2, tier_2);
							*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
						}
						craftingBoxData.CountBox.SelectedIndex = class2.PrefixSettings.Count;
						craftingBoxData2.CountBox.SelectedIndex = class2.SuffixSettings.Count;
					}
				}
			}
		}

		private void method_17(Button button_101, string string_11)
		{
			MainForm.Class24 @class = new MainForm.Class24();
			@class.button_0 = button_101;
			CraftingBoxData craftingBoxData_ = this.method_15().First(new Func<CraftingBoxData, bool>(@class.method_0));
			CraftingBoxData craftingBoxData_2 = this.method_15().First(new Func<CraftingBoxData, bool>(@class.method_1));
			Class301 value = new Class301(craftingBoxData_, craftingBoxData_2)
			{
				ItemBaseType = this.ItemBaseTypeString,
				Influences = this.InfluenceString
			};
			string contents = JsonConvert.SerializeObject(value, Formatting.Indented);
			File.WriteAllText(string_11, contents);
		}

		private void radioButton_4_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			string text = radioButton.Text;
			if (radioButton.Checked)
			{
				Class255.class105_0.method_9(ConfigOptions.RareCraftType, text, true);
			}
		}

		private void button_43_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.method_12(false);
				this.method_130(new ThreadStart(this.method_18));
			}
		}

		private void method_18()
		{
			base.Invoke(new Action(this.method_153));
		}

		private void button_46_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.method_12(false);
				this.method_130(new ThreadStart(this.method_19));
			}
		}

		private void method_19()
		{
			base.Invoke(new Action(this.method_154));
		}

		private void button_51_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.method_12(false);
				this.method_130(new ThreadStart(this.method_20));
			}
		}

		private void method_20()
		{
			base.Invoke(new Action(this.method_155));
		}

		private void radioButton_6_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			string text = radioButton.Text;
			if (radioButton.Checked)
			{
				Class255.class105_0.method_9(ConfigOptions.MapCraftChoice, text, true);
			}
		}

		private void dataGridView_3_MouseClick(object sender, MouseEventArgs e)
		{
			MainForm.Class25 @class = new MainForm.Class25();
			@class.mainForm_0 = this;
			if (e.Button == MouseButtons.Right)
			{
				@class.dataGridView_0 = (sender as DataGridView);
				@class.int_0 = @class.dataGridView_0.HitTest(e.X, e.Y).RowIndex;
				if (@class.int_0 != -1 && Class255.ItemBuyingList.Count > @class.int_0)
				{
					@class.itemBuyingListItem_0 = Class255.ItemBuyingList[@class.int_0];
					@class.string_0 = @class.itemBuyingListItem_0.Id;
					ContextMenu contextMenu = new ContextMenu();
					contextMenu.MenuItems.Add(MainForm.getString_0(107396774), new EventHandler(@class.method_0));
					contextMenu.MenuItems.Add(MainForm.getString_0(107396527), new EventHandler(@class.method_1));
					contextMenu.MenuItems.Add(MainForm.getString_0(107396785), new EventHandler(@class.method_2));
					contextMenu.MenuItems.Add(MainForm.getString_0(107396740), new EventHandler(@class.method_3));
					contextMenu.Show(@class.dataGridView_0, new Point(e.X, e.Y));
				}
			}
		}

		private void dataGridView_3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && e.RowIndex != -1)
			{
				this.dataGridView_3.EndEdit();
			}
		}

		private unsafe void dataGridView_3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			void* ptr = stackalloc byte[18];
			e.Control.KeyPress -= this.method_25;
			e.Control.KeyPress -= this.method_26;
			e.Control.KeyPress -= this.method_27;
			*(int*)ptr = this.dataGridView_3.Columns[MainForm.getString_0(107396514)].Index;
			*(int*)((byte*)ptr + 4) = this.dataGridView_3.Columns[MainForm.getString_0(107396485)].Index;
			*(int*)((byte*)ptr + 8) = this.dataGridView_3.Columns[MainForm.getString_0(107396460)].Index;
			((byte*)ptr)[12] = ((this.dataGridView_3.CurrentCell.ColumnIndex == *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				TextBox textBox = e.Control as TextBox;
				((byte*)ptr)[13] = ((textBox != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					textBox.KeyPress += this.method_25;
				}
			}
			else
			{
				((byte*)ptr)[14] = ((this.dataGridView_3.CurrentCell.ColumnIndex == *(int*)((byte*)ptr + 4)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					TextBox textBox2 = e.Control as TextBox;
					((byte*)ptr)[15] = ((textBox2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						textBox2.KeyPress += this.method_27;
					}
				}
				else
				{
					((byte*)ptr)[16] = ((this.dataGridView_3.CurrentCell.ColumnIndex == *(int*)((byte*)ptr + 8)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						TextBox textBox3 = e.Control as TextBox;
						((byte*)ptr)[17] = ((textBox3 != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							textBox3.KeyPress += this.method_26;
						}
					}
				}
			}
		}

		public void dataGridView_3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1)
			{
				this.method_104(true);
			}
		}

		private unsafe void button_32_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[3];
			MainForm.Class26 @class = new MainForm.Class26();
			*(byte*)ptr = (string.IsNullOrEmpty(this.textBox_13.Text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string text = this.method_107(this.textBox_13.Text);
				((byte*)ptr)[1] = ((!this.method_108(text)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					MessageBox.Show(this, string.Format(MainForm.getString_0(107396431), Class103.TradeWebsiteUrl));
				}
				else
				{
					Match match = MainForm.regex_5.Match(text);
					@class.string_0 = match.Groups[1].Value;
					((byte*)ptr)[2] = (Class255.ItemBuyingList.Any(new Func<ItemBuyingListItem, bool>(@class.method_0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						MessageBox.Show(this, MainForm.getString_0(107396410));
					}
					else
					{
						ItemBuyingListItem itemBuyingListItem = new ItemBuyingListItem(@class.string_0, this.textBox_12.Text);
						this.dataGridView_3.Rows.Add(itemBuyingListItem.ToDataGrid());
						Class255.ItemBuyingList.Add(itemBuyingListItem);
						this.textBox_13.Text = string.Empty;
						this.textBox_12.Text = string.Empty;
						this.method_104(true);
					}
				}
			}
		}

		private void textBox_12_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				this.button_32_Click(null, null);
			}
		}

		private void button_93_Click(object sender, EventArgs e)
		{
			this.method_145((Button)sender, new Action(this.method_21), new Action(this.method_22));
		}

		private void method_21()
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Class255.class105_0.method_3(ConfigOptions.LastImportDirectory);
				openFileDialog.Filter = MainForm.getString_0(107395813);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					List<ItemBuyingListItem> list = JsonConvert.DeserializeObject<List<ItemBuyingListItem>>(File.ReadAllText(openFileDialog.FileName));
					Class255.class105_0.method_9(ConfigOptions.ItemBuyingList, list, true);
					this.dataGridView_3.Rows.Clear();
					foreach (ItemBuyingListItem itemBuyingListItem in Class255.ItemBuyingList)
					{
						this.dataGridView_3.Rows.Add(itemBuyingListItem.ToDataGrid());
					}
				}
			}
		}

		private void method_22()
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = Class255.class105_0.method_3(ConfigOptions.LastImportDirectory);
				saveFileDialog.Filter = MainForm.getString_0(107395813);
				saveFileDialog.FileName = MainForm.getString_0(107395804);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string contents = JsonConvert.SerializeObject(Class255.ItemBuyingList, Formatting.Indented, Util.smethod_30());
					File.WriteAllText(saveFileDialog.FileName, contents);
				}
			}
		}

		private unsafe void dataGridView_3_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[13];
			DataGridView dataGridView = sender as DataGridView;
			*(int*)ptr = dataGridView.HitTest(e.X, e.Y).RowIndex;
			*(int*)((byte*)ptr + 4) = dataGridView.HitTest(e.X, e.Y).ColumnIndex;
			*(int*)((byte*)ptr + 8) = this.dataGridView_3.Columns[MainForm.getString_0(107395755)].Index;
			if (*(int*)ptr != -1 && Class255.ItemBuyingList.Count > *(int*)ptr)
			{
				((byte*)ptr)[12] = ((*(int*)((byte*)ptr + 4) <= 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) == 0)
				{
					ItemBuyingListItem itemBuyingListItem_ = Class255.ItemBuyingList[*(int*)ptr];
					this.method_23(itemBuyingListItem_);
				}
			}
		}

		private void method_23(ItemBuyingListItem itemBuyingListItem_0)
		{
			ItemBuyForm itemBuyForm = new ItemBuyForm(this, itemBuyingListItem_0);
			itemBuyForm.Show();
			itemBuyForm.method_0();
		}

		private unsafe void method_24(ItemBuyingListItem itemBuyingListItem_0)
		{
			void* ptr = stackalloc byte[6];
			string text = Interaction.InputBox(MainForm.getString_0(107395718), itemBuyingListItem_0.Description, MainForm.getString_0(107396269), -1, -1);
			((byte*)ptr)[4] = (string.IsNullOrEmpty(text) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				string text2 = this.method_107(text);
				((byte*)ptr)[5] = ((!this.method_108(text2)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					MessageBox.Show(this, string.Format(MainForm.getString_0(107396431), Class103.TradeWebsiteUrl));
				}
				else
				{
					Match match = MainForm.regex_5.Match(text2);
					string value = match.Groups[1].Value;
					List<ItemBuyingListItem> list = Class255.ItemBuyingList.ToList<ItemBuyingListItem>();
					*(int*)ptr = Class255.ItemBuyingList.IndexOf(itemBuyingListItem_0);
					itemBuyingListItem_0.Id = value;
					list[*(int*)ptr] = itemBuyingListItem_0;
					Class255.class105_0.method_9(ConfigOptions.ItemBuyingList, list, true);
				}
			}
		}

		private void dataGridView_2_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
		}

		private void method_25(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		private void method_26(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
			{
				DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = sender as DataGridViewTextBoxEditingControl;
				if (e.KeyChar == '.' || e.KeyChar == '/' || e.KeyChar == ',')
				{
					if (dataGridViewTextBoxEditingControl.Text.Contains(e.KeyChar))
					{
						e.Handled = true;
					}
				}
				else
				{
					e.Handled = true;
				}
			}
		}

		private void method_27(object sender, KeyPressEventArgs e)
		{
			if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
			{
				DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = sender as DataGridViewTextBoxEditingControl;
				if (e.KeyChar == '.' || e.KeyChar == ',')
				{
					if (dataGridViewTextBoxEditingControl.Text.Contains(e.KeyChar))
					{
						e.Handled = true;
					}
				}
				else
				{
					e.Handled = true;
				}
			}
		}

		private void dataGridView_2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.Button == MouseButtons.Left)
			{
				this.int_6 = e.RowIndex;
			}
		}

		private unsafe void dataGridView_2_MouseUp(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[4] = ((e.Button != MouseButtons.Left) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				DataGridView dataGridView = (DataGridView)sender;
				DataGridView.HitTestInfo hitTestInfo = dataGridView.HitTest(e.X, e.Y);
				((byte*)ptr)[5] = ((hitTestInfo.Type > DataGridViewHitTestType.None) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					*(int*)ptr = hitTestInfo.RowIndex;
					if (this.int_6 >= 0 && *(int*)ptr >= 0)
					{
						((byte*)ptr)[6] = ((*(int*)ptr != this.int_6) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							DataGridViewRow dataGridViewRow = dataGridView.Rows[this.int_6];
							dataGridView.Rows.Remove(dataGridViewRow);
							dataGridView.Rows.Insert(*(int*)ptr, dataGridViewRow);
							dataGridView.ClearSelection();
							dataGridViewRow.Selected = true;
							((byte*)ptr)[7] = ((dataGridView == this.dataGridView_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								FlippingListItem item = Class255.FlippingList.ElementAt(this.int_6);
								Class255.FlippingList.Remove(item);
								Class255.FlippingList.Insert(*(int*)ptr, item);
								Class255.class105_0.method_9(ConfigOptions.FlippingList, Class255.FlippingList, true);
							}
							((byte*)ptr)[8] = ((dataGridView == this.dataGridView_1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 8) != 0)
							{
								LiveSearchListItem item2 = Class255.LiveSearchList.ElementAt(this.int_6);
								List<LiveSearchListItem> list = Class255.LiveSearchList.ToList<LiveSearchListItem>();
								list.Remove(item2);
								list.Insert(*(int*)ptr, item2);
								Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, true);
							}
							((byte*)ptr)[9] = ((dataGridView == this.dataGridView_3) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								ItemBuyingListItem item3 = Class255.ItemBuyingList.ElementAt(this.int_6);
								List<ItemBuyingListItem> list2 = Class255.ItemBuyingList.ToList<ItemBuyingListItem>();
								list2.Remove(item3);
								list2.Insert(*(int*)ptr, item3);
								Class255.class105_0.method_9(ConfigOptions.ItemBuyingList, list2, true);
							}
							((byte*)ptr)[10] = ((dataGridView == this.dataGridView_2) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 10) != 0)
							{
								BulkBuyingListItem item4 = Class255.BulkBuyingList.ElementAt(this.int_6);
								List<BulkBuyingListItem> list3 = Class255.BulkBuyingList.ToList<BulkBuyingListItem>();
								list3.Remove(item4);
								list3.Insert(*(int*)ptr, item4);
								Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, list3, true);
							}
						}
					}
				}
				else if (this.int_6 >= 0 && dataGridView.Rows.Count < this.int_6)
				{
					dataGridView.Rows[this.int_6].Selected = true;
				}
				((byte*)ptr)[11] = ((this.label_0 != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 11) != 0)
				{
					this.label_0.Dispose();
					this.label_0 = null;
				}
			}
		}

		private unsafe void dataGridView_0_MouseClick(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((e.Button == MouseButtons.Right) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				MainForm.Class27 @class = new MainForm.Class27();
				@class.mainForm_0 = this;
				@class.dataGridView_0 = (sender as DataGridView);
				@class.int_0 = @class.dataGridView_0.HitTest(e.X, e.Y).RowIndex;
				((byte*)ptr)[1] = ((@class.int_0 == -1) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					@class.string_0 = @class.dataGridView_0[MainForm.getString_0(107395697), @class.int_0].Value.ToString();
					@class.string_1 = @class.dataGridView_0[MainForm.getString_0(107395680), @class.int_0].Value.ToString();
					@class.flippingListItem_0 = Class255.FlippingList.FirstOrDefault(new Func<FlippingListItem, bool>(@class.method_0));
					((byte*)ptr)[2] = ((@class.flippingListItem_0 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						ContextMenu contextMenu = new ContextMenu();
						contextMenu.MenuItems.Add(MainForm.getString_0(107396774), new EventHandler(@class.method_1));
						contextMenu.MenuItems.Add(MainForm.getString_0(107396740), new EventHandler(@class.method_2));
						contextMenu.Show(@class.dataGridView_0, new Point(e.X, e.Y));
					}
				}
			}
		}

		private unsafe void dataGridView_0_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[6];
			MainForm.Class28 @class = new MainForm.Class28();
			DataGridView dataGridView = sender as DataGridView;
			*(int*)ptr = dataGridView.HitTest(e.X, e.Y).RowIndex;
			((byte*)ptr)[4] = ((*(int*)ptr == -1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				@class.string_0 = dataGridView[MainForm.getString_0(107395697), *(int*)ptr].Value.ToString();
				@class.string_1 = dataGridView[MainForm.getString_0(107395680), *(int*)ptr].Value.ToString();
				FlippingListItem flippingListItem = Class255.FlippingList.FirstOrDefault(new Func<FlippingListItem, bool>(@class.method_0));
				((byte*)ptr)[5] = ((flippingListItem == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					this.method_28(flippingListItem);
				}
			}
		}

		private void button_23_Click(object sender, EventArgs e)
		{
			ExchangePairForm exchangePairForm = new ExchangePairForm();
			exchangePairForm.OnPairSavedEvent += this.method_156;
			exchangePairForm.Show();
		}

		private void method_28(FlippingListItem flippingListItem_0)
		{
			if (Class306.smethod_0(0))
			{
				FlippingForm flippingForm = new FlippingForm(flippingListItem_0);
				flippingForm.StartPosition = FormStartPosition.Manual;
				flippingForm.Location = Class255.class105_0.method_2<Point>(ConfigOptions.FlipFormLocation);
				flippingForm.Show();
				flippingForm.method_0();
			}
		}

		private unsafe void dataGridView_0_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			void* ptr = stackalloc byte[2];
			MainForm.Class29 @class = new MainForm.Class29();
			DataGridView dataGridView = sender as DataGridView;
			if (e.RowIndex != -1 && e.ColumnIndex == 0)
			{
				*(byte*)ptr = (((bool)((DataGridViewCheckBoxCell)dataGridView[0, e.RowIndex]).Value) ? 1 : 0);
				@class.string_0 = dataGridView[MainForm.getString_0(107395697), e.RowIndex].Value.ToString();
				@class.string_1 = dataGridView[MainForm.getString_0(107395680), e.RowIndex].Value.ToString();
				FlippingListItem flippingListItem = Class255.FlippingList.FirstOrDefault(new Func<FlippingListItem, bool>(@class.method_0));
				((byte*)ptr)[1] = ((flippingListItem == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					flippingListItem.Enabled = (*(sbyte*)ptr != 0);
					Class255.class105_0.method_9(ConfigOptions.FlippingList, Class255.FlippingList, true);
				}
			}
		}

		public unsafe void method_29(string string_11, string string_12)
		{
			void* ptr = stackalloc byte[2];
			MainForm.Class30 @class = new MainForm.Class30();
			@class.string_0 = string_11;
			@class.string_1 = string_12;
			*(byte*)ptr = (Class255.FlippingList.Any(new Func<FlippingListItem, bool>(@class.method_0)) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.FlippingList.Any(new Func<FlippingListItem, bool>(@class.method_1)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					FlippingListItem flippingListItem = new FlippingListItem(@class.string_0, @class.string_1);
					flippingListItem.MaxHavePerTrade = API.smethod_6(@class.string_0) * 60;
					flippingListItem.MaxWantPerTrade = API.smethod_6(@class.string_1) * 60;
					Class255.FlippingList.Add(flippingListItem);
					this.dataGridView_0.Rows.Add(flippingListItem.ToDataGrid());
					Class255.class105_0.method_9(ConfigOptions.FlippingList, Class255.FlippingList, true);
				}
			}
		}

		private void button_91_Click(object sender, EventArgs e)
		{
			this.method_145((Button)sender, new Action(this.method_30), new Action(this.method_31));
		}

		private void method_30()
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Class255.class105_0.method_3(ConfigOptions.LastImportDirectory);
				openFileDialog.Filter = MainForm.getString_0(107395631);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					List<FlippingListItem> list = JsonConvert.DeserializeObject<List<FlippingListItem>>(File.ReadAllText(openFileDialog.FileName));
					Class255.class105_0.method_9(ConfigOptions.FlippingList, list, true);
					this.dataGridView_0.Rows.Clear();
					foreach (FlippingListItem flippingListItem in Class255.FlippingList)
					{
						this.dataGridView_0.Rows.Add(flippingListItem.ToDataGrid());
					}
				}
			}
		}

		private void method_31()
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = Class255.class105_0.method_3(ConfigOptions.LastImportDirectory);
				saveFileDialog.Filter = MainForm.getString_0(107395631);
				saveFileDialog.FileName = MainForm.getString_0(107396102);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string contents = JsonConvert.SerializeObject(Class255.FlippingList, Formatting.Indented, Util.smethod_30());
					File.WriteAllText(saveFileDialog.FileName, contents);
				}
			}
		}

		private unsafe void button_19_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = (string.IsNullOrEmpty(this.textBox_10.Text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string text = this.method_107(this.textBox_10.Text);
				((byte*)ptr)[1] = ((!this.method_108(text)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					MessageBox.Show(this, string.Format(MainForm.getString_0(107396121), Class103.TradeWebsiteUrl, Class255.class105_0.method_3(ConfigOptions.League)));
				}
				else
				{
					Match match = MainForm.regex_5.Match(text);
					string value = match.Groups[1].Value;
					((byte*)ptr)[2] = (this.dictionary_2.ContainsKey(value) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						MessageBox.Show(this, MainForm.getString_0(107396410));
					}
					else
					{
						Class260 @class = this.method_96(value);
						LiveSearchListItem liveSearchListItem = new LiveSearchListItem(value, this.textBox_9.Text, true);
						this.method_35(liveSearchListItem);
						Class255.LiveSearchList.Add(liveSearchListItem);
						this.dictionary_2.Add(value, @class);
						((byte*)ptr)[3] = ((this.genum0_0 == MainForm.GEnum0.const_2) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							this.method_147(liveSearchListItem, @class);
						}
						this.textBox_10.Text = string.Empty;
						this.textBox_9.Text = string.Empty;
						this.method_102(true);
						this.method_101();
					}
				}
			}
		}

		private void dataGridView_1_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				MainForm.Class31 @class = new MainForm.Class31();
				@class.mainForm_0 = this;
				@class.int_0 = this.dataGridView_1.HitTest(e.X, e.Y).RowIndex;
				if (@class.int_0 != -1 && Class255.LiveSearchList.Count > @class.int_0)
				{
					@class.liveSearchListItem_0 = Class255.LiveSearchList[@class.int_0];
					@class.string_0 = @class.liveSearchListItem_0.Id;
					ContextMenu contextMenu = new ContextMenu();
					contextMenu.MenuItems.Add(MainForm.getString_0(107396774), new EventHandler(@class.method_0));
					contextMenu.MenuItems.Add(MainForm.getString_0(107396527), new EventHandler(@class.method_1));
					contextMenu.MenuItems.Add(MainForm.getString_0(107396785), new EventHandler(@class.method_2));
					contextMenu.MenuItems.Add(MainForm.getString_0(107396740), new EventHandler(@class.method_3));
					contextMenu.Show(this.dataGridView_1, new Point(e.X, e.Y));
				}
			}
		}

		public unsafe void dataGridView_1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			void* ptr = stackalloc byte[14];
			if (e.RowIndex != -1 && Class255.LiveSearchList.Count > e.RowIndex)
			{
				LiveSearchListItem liveSearchListItem = Class255.LiveSearchList[e.RowIndex];
				string id = liveSearchListItem.Id;
				*(int*)ptr = this.dataGridView_1.Columns[MainForm.getString_0(107396000)].Index;
				*(int*)((byte*)ptr + 4) = this.dataGridView_1.Columns[MainForm.getString_0(107395943)].Index;
				((byte*)ptr)[8] = (((bool)((DataGridViewCheckBoxCell)this.dataGridView_1[0, e.RowIndex]).Value) ? 1 : 0);
				((byte*)ptr)[9] = ((e.ColumnIndex == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					Class260 @class = this.dictionary_2[id];
					liveSearchListItem.Enabled = (*(sbyte*)((byte*)ptr + 8) != 0);
					((byte*)ptr)[10] = ((this.genum0_0 == MainForm.GEnum0.const_2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						((byte*)ptr)[11] = (byte)(*(sbyte*)((byte*)ptr + 8));
						if (*(sbyte*)((byte*)ptr + 11) != 0)
						{
							this.method_147(liveSearchListItem, @class);
						}
						else
						{
							@class.method_2(false);
						}
					}
				}
				else if (e.ColumnIndex == *(int*)ptr || e.ColumnIndex == *(int*)((byte*)ptr + 4))
				{
					Class249 class2 = new Class249(Util.smethod_6(this.dataGridView_1[*(int*)ptr, e.RowIndex].Value.smethod_10()), this.dataGridView_1[*(int*)((byte*)ptr + 4), e.RowIndex].Value.smethod_10());
					((byte*)ptr)[12] = ((!this.dictionary_0[id].Equals(class2)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) != 0)
					{
						this.dictionary_0[id] = class2;
						((byte*)ptr)[13] = (byte)(((this.genum0_0 == MainForm.GEnum0.const_2) ? 1 : 0) & *(sbyte*)((byte*)ptr + 8));
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							this.method_37(id, class2);
						}
					}
				}
				this.method_102(true);
			}
		}

		private void dataGridView_1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && e.RowIndex != -1)
			{
				this.dataGridView_1.EndEdit();
			}
		}

		private unsafe void dataGridView_1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			void* ptr = stackalloc byte[18];
			e.Control.KeyPress -= this.method_25;
			e.Control.KeyPress -= this.method_26;
			*(int*)ptr = this.dataGridView_1.Columns[MainForm.getString_0(107395906)].Index;
			*(int*)((byte*)ptr + 4) = this.dataGridView_1.Columns[MainForm.getString_0(107396000)].Index;
			*(int*)((byte*)ptr + 8) = this.dataGridView_1.Columns[MainForm.getString_0(107395877)].Index;
			((byte*)ptr)[12] = ((this.dataGridView_1.CurrentCell.ColumnIndex == *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				TextBox textBox = e.Control as TextBox;
				((byte*)ptr)[13] = ((textBox != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 13) != 0)
				{
					textBox.KeyPress += this.method_25;
				}
			}
			else
			{
				((byte*)ptr)[14] = ((this.dataGridView_1.CurrentCell.ColumnIndex == *(int*)((byte*)ptr + 4)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) != 0)
				{
					TextBox textBox2 = e.Control as TextBox;
					((byte*)ptr)[15] = ((textBox2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						textBox2.KeyPress += this.method_27;
					}
				}
				else
				{
					((byte*)ptr)[16] = ((this.dataGridView_1.CurrentCell.ColumnIndex == *(int*)((byte*)ptr + 8)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 16) != 0)
					{
						TextBox textBox3 = e.Control as TextBox;
						((byte*)ptr)[17] = ((textBox3 != null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 17) != 0)
						{
							textBox3.KeyPress += this.method_26;
						}
					}
				}
			}
		}

		private void textBox_9_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				this.button_19_Click(null, null);
			}
		}

		private void button_92_Click(object sender, EventArgs e)
		{
			this.method_145((Button)sender, new Action(this.method_32), new Action(this.method_33));
		}

		private void method_32()
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Class395.SettingsPath;
				openFileDialog.Filter = MainForm.getString_0(107395336);
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					List<LiveSearchListItem> list = JsonConvert.DeserializeObject<List<LiveSearchListItem>>(File.ReadAllText(openFileDialog.FileName));
					Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, true);
					this.method_36();
					foreach (LiveSearchListItem liveSearchListItem_ in Class255.LiveSearchList)
					{
						this.method_35(liveSearchListItem_);
					}
				}
			}
		}

		private void method_33()
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = Class395.SettingsPath;
				saveFileDialog.Filter = MainForm.getString_0(107395336);
				saveFileDialog.FileName = MainForm.getString_0(107395323);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string contents = JsonConvert.SerializeObject(Class255.LiveSearchList, Formatting.Indented, Util.smethod_30());
					File.WriteAllText(saveFileDialog.FileName, contents);
				}
			}
		}

		private unsafe void dataGridView_1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[12];
			DataGridView dataGridView = sender as DataGridView;
			*(int*)ptr = dataGridView.HitTest(e.X, e.Y).RowIndex;
			*(int*)((byte*)ptr + 4) = dataGridView.HitTest(e.X, e.Y).ColumnIndex;
			*(int*)((byte*)ptr + 8) = this.dataGridView_1.Columns[MainForm.getString_0(107395943)].Index;
			if (*(int*)ptr != -1 && Class255.LiveSearchList.Count > *(int*)ptr && *(int*)((byte*)ptr + 4) > 1 && *(int*)((byte*)ptr + 4) != *(int*)((byte*)ptr + 8))
			{
				LiveSearchListItem liveSearchListItem_ = Class255.LiveSearchList[*(int*)ptr];
				this.method_34(liveSearchListItem_);
			}
		}

		private void method_34(LiveSearchListItem liveSearchListItem_0)
		{
			LiveSearchBuyForm liveSearchBuyForm = new LiveSearchBuyForm(this, liveSearchListItem_0);
			liveSearchBuyForm.Show();
			liveSearchBuyForm.method_0();
		}

		private void method_35(LiveSearchListItem liveSearchListItem_0)
		{
			this.dictionary_0.Add(liveSearchListItem_0.Id, liveSearchListItem_0.MaxPrice);
			this.dataGridView_1.Rows.Add(liveSearchListItem_0.ToDataGrid());
		}

		private void method_36()
		{
			this.dictionary_0.Clear();
			this.dataGridView_1.Rows.Clear();
		}

		private void method_37(string string_11, Class249 class249_0)
		{
			MainForm.Class32 @class = new MainForm.Class32();
			@class.string_0 = string_11;
			@class.mainForm_0 = this;
			@class.class249_0 = class249_0;
			Task.Run(new Action(@class.method_0));
		}

		private unsafe void method_38(LiveSearchListItem liveSearchListItem_0)
		{
			void* ptr = stackalloc byte[7];
			string text = Interaction.InputBox(MainForm.getString_0(107395274), liveSearchListItem_0.Description, MainForm.getString_0(107396269), -1, -1);
			((byte*)ptr)[4] = (string.IsNullOrEmpty(text) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				string text2 = this.method_107(text);
				((byte*)ptr)[5] = ((!this.method_108(text2)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					MessageBox.Show(this, string.Format(MainForm.getString_0(107396431), Class103.TradeWebsiteUrl));
				}
				else
				{
					Match match = MainForm.regex_5.Match(text2);
					string value = match.Groups[1].Value;
					List<LiveSearchListItem> list = Class255.LiveSearchList.ToList<LiveSearchListItem>();
					*(int*)ptr = Class255.LiveSearchList.IndexOf(liveSearchListItem_0);
					if (!this.dictionary_2.ContainsKey(value) && *(int*)ptr != -1)
					{
						((byte*)ptr)[6] = (this.dictionary_2.ContainsKey(liveSearchListItem_0.Id) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							this.dictionary_2[liveSearchListItem_0.Id].method_2(false);
							this.dictionary_2.Remove(liveSearchListItem_0.Id);
						}
						liveSearchListItem_0.Id = value;
						Class260 @class = this.method_96(value);
						if (this.genum0_0 == MainForm.GEnum0.const_2 && liveSearchListItem_0.Enabled)
						{
							this.method_147(liveSearchListItem_0, @class);
						}
						this.dictionary_2.Add(value, @class);
						list[*(int*)ptr] = liveSearchListItem_0;
						Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, true);
					}
				}
			}
		}

		public static bool IsPaused { get; set; }

		public string CharacterName { get; set; }

		public Dictionary<string, DateTime> LastPlayerWhispers { get; set; } = new Dictionary<string, DateTime>();

		private DateTime NextFlipUpdateTime
		{
			get
			{
				return Class255.class105_0.method_2<DateTime>(ConfigOptions.NextFlipUpdateTime);
			}
		}

		public void method_39()
		{
			MainForm.string_10 = this.method_44(MainForm.regex_0);
		}

		public bool method_40()
		{
			this.method_39();
			return MainForm.string_10.ToLower().Contains(MainForm.getString_0(107395253));
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string string_11, string string_12);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr GetWindowRect(IntPtr intptr_0, out Struct2 struct2_0);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool MoveWindow(IntPtr intptr_0, int int_7, int int_8, int int_9, int int_10, bool bool_23);

		public unsafe bool method_41()
		{
			void* ptr = stackalloc byte[3];
			try
			{
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107395208));
				string a = Web.smethod_0(Class103.string_1);
				string b = Util.smethod_24(MainForm.getString_0(107395175));
				*(byte*)ptr = ((a == b) ? 1 : 0);
				string contents;
				if (*(sbyte*)ptr != 0)
				{
					contents = File.ReadAllText(MainForm.getString_0(107395175));
				}
				else
				{
					contents = Web.smethod_0(Class103.string_3);
					File.WriteAllText(MainForm.getString_0(107395175), contents);
				}
				((byte*)ptr)[1] = (API.smethod_0(contents) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107395117));
					((byte*)ptr)[2] = 0;
					goto IL_102;
				}
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107395190));
				base.Invoke(new Action(this.method_157));
			}
			catch (Exception ex)
			{
				Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107395596), new object[]
				{
					ex
				});
				((byte*)ptr)[2] = 0;
				goto IL_102;
			}
			((byte*)ptr)[2] = 1;
			IL_102:
			return *(sbyte*)((byte*)ptr + 2) != 0;
		}

		public unsafe void method_42()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((!Class255.class105_0.method_4(ConfigOptions.LimitedUser)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107395607));
				this.bool_11 = true;
			}
			else
			{
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107395522));
				((byte*)ptr)[1] = ((!ProcessHelper.smethod_1()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107395465));
				}
				else
				{
					((byte*)ptr)[2] = ((Class163.smethod_0() == string.Empty) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107395432));
					}
					else
					{
						this.bool_11 = true;
						Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107395415));
					}
				}
			}
		}

		public unsafe void method_43()
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = (this.bool_21 ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string value = Web.smethod_2(string.Format(MainForm.getString_0(107394846), Class103.string_0, Class120.string_3), Encoding.UTF8, new Dictionary<string, string>());
				JObject jobject = JsonConvert.DeserializeObject<JObject>(value);
				if (jobject == null || jobject.ContainsKey(MainForm.getString_0(107394813)))
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107394768));
				}
				else
				{
					((byte*)ptr)[1] = (((int)jobject[MainForm.getString_0(107394723)] == 0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						string text = jobject[MainForm.getString_0(107394746)].ToString();
						Regex regex = new Regex(MainForm.getString_0(107394741));
						Match match = regex.Match(text);
						Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107394716));
						MainForm.smethod_1();
						((byte*)ptr)[2] = (File.Exists(Class120.string_4) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							string arguments = string.Format(MainForm.getString_0(107394683), text, string.Format(Class103.string_4, match.Groups[1].Value));
							Process.Start(Class120.string_4, arguments);
							Process.GetCurrentProcess().Kill();
						}
						else
						{
							Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107394638));
						}
					}
				}
			}
		}

		private static void smethod_1()
		{
			string text = Class120.string_4 + MainForm.getString_0(107394605);
			try
			{
				using (WebClient webClient = new WebClient())
				{
					webClient.DownloadFile(Class103.string_5, text);
				}
			}
			catch
			{
				MessageBox.Show(string.Format(MainForm.getString_0(107394596), Class103.string_5));
				return;
			}
			File.Delete(Class120.string_4);
			File.Move(text, Class120.string_4);
		}

		public string method_44(Regex regex_11)
		{
			string result = MainForm.getString_0(107396269);
			using (FileStream fileStream = new FileStream(Class120.PoELogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					IEnumerable<string> enumerable = streamReader.ReadToEnd().Replace(MainForm.getString_0(107395063), string.Empty).Split(new char[]
					{
						'\r'
					}).Reverse<string>();
					foreach (string input in enumerable)
					{
						Match match = regex_11.Match(input);
						if (match.Success)
						{
							return match.Groups[1].Value;
						}
					}
				}
			}
			return result;
		}

		public unsafe void method_45()
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[8] = ((!File.Exists(Class120.PoELogFile)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				using (FileStream fileStream = new FileStream(Class120.PoELogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (StreamReader streamReader = new StreamReader(fileStream))
					{
						*(long*)ptr = streamReader.BaseStream.Length;
						for (;;)
						{
							((byte*)ptr)[11] = 1;
							((byte*)ptr)[9] = ((streamReader.BaseStream.Length > *(long*)ptr) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 9) != 0)
							{
								List<string> list = new List<string>();
								streamReader.BaseStream.Seek(*(long*)ptr, SeekOrigin.Begin);
								string item;
								while ((item = streamReader.ReadLine()) != null)
								{
									list.Add(item);
								}
								foreach (string string_ in list)
								{
									this.method_46(string_);
								}
								*(long*)ptr = streamReader.BaseStream.Position;
							}
							else
							{
								((byte*)ptr)[10] = ((streamReader.BaseStream.Length < *(long*)ptr) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									*(long*)ptr = 0L;
								}
							}
							Thread.Sleep(10);
						}
					}
				}
			}
		}

		[DebuggerStepThrough]
		public void method_46(string string_11)
		{
			MainForm.Class38 @class = new MainForm.Class38();
			@class.mainForm_0 = this;
			@class.string_0 = string_11;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class38>(ref @class);
		}

		public MainForm(string Name)
		{
			this.bool_21 = (Debugger.IsAttached || Name == MainForm.getString_0(107395033));
			Class181.smethod_0(this);
			this.method_149();
			this.Text = Name;
			if (this.bool_21)
			{
				this.debugPanel_0 = new DebugPanel(this);
				this.button_67.Visible = true;
			}
			else
			{
				this.tabControl_11.TabPages.Remove(this.tabPage_38);
				this.tabControl_11.TabPages.Remove(this.tabPage_39);
				this.tabControl_7.TabPages.Remove(this.tabPage_49);
			}
			this.tabControl_4.TabPages.Remove(this.tabPage_46);
			this.button_0.BringToFront();
			this.toolStripLabel_2.Text = string.Empty;
			this.toolStripLabel_0.Text = MainForm.getString_0(107394992) + MainForm.smethod_3();
			Stashes.smethod_0(this);
			UI.smethod_0(this);
			this.stopwatch_0 = new Stopwatch();
			this.method_49();
			this.method_50();
			Win32.smethod_0(this.trackBar_0.Maximum);
			Web.mainForm_0 = this;
			ComboBox.ObjectCollection items = this.comboBox_29.Items;
			object[] names = Class102.string_7;
			items.AddRange(names);
			ComboBox.ObjectCollection items2 = this.comboBox_28.Items;
			names = Class102.string_7;
			items2.AddRange(names);
			ComboBox.ObjectCollection items3 = this.comboBox_67.Items;
			names = Class102.string_7;
			items3.AddRange(names);
			this.comboBox_52.Items.smethod_22(Class102.PricingTypes.Keys.OrderBy(new Func<string, string>(MainForm.<>c.<>9.method_23)));
			ComboBox.ObjectCollection items4 = this.comboBox_70.Items;
			names = Enum.GetNames(typeof(InstanceZones));
			items4.AddRange(names);
			ComboBox.ObjectCollection items5 = this.comboBox_72.Items;
			names = Class102.string_10;
			items5.AddRange(names);
			this.comboBox_74.Items.smethod_22(ItemData.smethod_3().OrderBy(new Func<string, string>(MainForm.<>c.<>9.method_24)));
			this.comboBox_29.SelectedIndex = 0;
			this.comboBox_28.SelectedIndex = 0;
			this.comboBox_68.SelectedIndex = 0;
			this.comboBox_52.SelectedIndex = 0;
			this.comboBox_72.SelectedIndex = 0;
			this.Refresh();
		}

		private void method_47()
		{
			KeyToStringHelper.smethod_0();
			this.class242_0 = new Class242(ConfigOptions.KillHotkey, this.textBox_2, new Action<object, KeyEventArgs, Class242, Action>(this.method_83), new Action(this.method_62));
			this.class242_1 = new Class242(ConfigOptions.StopAfterTradesHotkey, this.textBox_15, new Action<object, KeyEventArgs, Class242, Action>(this.method_83), new Action(this.method_119));
			this.class242_2 = new Class242(ConfigOptions.PauseHotkey, this.textBox_16, new Action<object, KeyEventArgs, Class242, Action>(this.method_83), new Action(this.method_128));
		}

		private unsafe void method_48()
		{
			void* ptr = stackalloc byte[6];
			ComboBox.ObjectCollection items = this.comboBox_19.Items;
			object[] items2 = Class255.class105_0.method_8<string>(ConfigOptions.StashProfiles).ToArray();
			items.AddRange(items2);
			*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.StashProfileSelected);
			((byte*)ptr)[4] = ((*(int*)ptr == -1) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				*(int*)ptr = 0;
			}
			((byte*)ptr)[5] = ((this.comboBox_19.Items.Count - 1 >= *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				this.comboBox_19.SelectedIndex = *(int*)ptr;
			}
			else
			{
				this.comboBox_19.SelectedIndex = 0;
			}
			this.comboBox_19.SelectedIndexChanged += this.comboBox_19_SelectedIndexChanged;
			this.method_91(this.comboBox_19.SelectedIndex != 0);
		}

		private unsafe void method_49()
		{
			void* ptr = stackalloc byte[5];
			Directory.CreateDirectory(MainForm.getString_0(107394987));
			Directory.CreateDirectory(MainForm.getString_0(107394978));
			Directory.CreateDirectory(MainForm.getString_0(107395001));
			((byte*)ptr)[4] = (Directory.Exists(MainForm.getString_0(107394956)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				try
				{
					string[] files = Directory.GetFiles(MainForm.getString_0(107394956));
					*(int*)ptr = 0;
					while (*(int*)ptr < files.Length)
					{
						string text = files[*(int*)ptr];
						File.Move(text, text.Replace(MainForm.getString_0(107394975), MainForm.getString_0(107394962)));
						*(int*)ptr = *(int*)ptr + 1;
					}
					Directory.Delete(MainForm.getString_0(107394956));
				}
				catch
				{
				}
			}
		}

		private void method_50()
		{
			CleanInventory.smethod_0(this);
			CleanDumpTab.smethod_0(this);
			Class361.smethod_0(this);
			Class170.smethod_0(this);
			Class175.smethod_0(this);
			TradeProcessor.smethod_0(this);
			ProcessHelper.smethod_0(this);
			BuyMessageProcessor.smethod_0(this);
			Mule.smethod_0(this);
			Class164.smethod_0(this);
		}

		private void method_51()
		{
			Class120.dictionary_1 = new Dictionary<string, Class241>
			{
				{
					MainForm.getString_0(107394917),
					new Class241(Class120.PoEDirectory, MainForm.getString_0(107394944), string.Empty, string.Empty, MainForm.getString_0(107394883), string.Empty)
				},
				{
					MainForm.getString_0(107394221),
					new Class241(Class120.string_0, string.Empty, MainForm.getString_0(107394212), MainForm.getString_0(107394187), MainForm.getString_0(107394883), MainForm.getString_0(107394178))
				},
				{
					MainForm.getString_0(107394565),
					new Class241(Class120.PoEDirectory, MainForm.getString_0(107394588), string.Empty, MainForm.getString_0(107394555), MainForm.getString_0(107394883), MainForm.getString_0(107394498))
				}
			};
		}

		private void method_52()
		{
			string[] source = Class238.Dictionary.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string item in source.Where(new Func<string, bool>(MainForm.<>c.<>9.method_25)))
			{
				Class120.EnglishDictionary.Add(item);
			}
		}

		private unsafe void method_53()
		{
			void* ptr = stackalloc byte[2];
			try
			{
				*(byte*)ptr = (File.Exists(Class120.PoELogFile) ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107394348));
					return;
				}
				File.WriteAllText(Class120.PoELogFile, string.Empty);
			}
			catch
			{
			}
			this.method_99();
			this.form1_0 = new Form1();
			((byte*)ptr)[1] = (ProcessHelper.smethod_9() ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0 && (Class255.class105_0.method_4(ConfigOptions.ShowInGameChat) && ProcessHelper.smethod_7(ProcessHelper.int_0)))
			{
				this.form1_0.method_1();
			}
		}

		public unsafe void method_54()
		{
			void* ptr = stackalloc byte[3];
			this.checkBox_54.Checked = Class255.class105_0.method_4(ConfigOptions.MessagesAfterTradeEnabled);
			this.fastObjectListView_20.Enabled = Class255.class105_0.method_4(ConfigOptions.MessagesAfterTradeEnabled);
			this.fastObjectListView_20.SmallImageList = new ImageList();
			this.fastObjectListView_20.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_26.ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_20.MouseDown += this.fastObjectListView_20_MouseDown;
			this.button_88.Click += this.button_88_Click;
			foreach (string text in Class255.class105_0.method_8<string>(ConfigOptions.MessagesAfterTradeList))
			{
				this.fastObjectListView_20.AddObject(new ListItemViewModel(text));
			}
			this.checkBox_54.CheckedChanged += this.checkBox_54_CheckedChanged;
			this.checkBox_55.Checked = Class255.class105_0.method_4(ConfigOptions.IgnoreListEnabled);
			this.fastObjectListView_21.Enabled = Class255.class105_0.method_4(ConfigOptions.IgnoreListEnabled);
			this.fastObjectListView_21.SmallImageList = new ImageList();
			this.fastObjectListView_21.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_27.ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_21.MouseDown += this.fastObjectListView_21_MouseDown;
			this.button_89.Click += this.button_89_Click;
			foreach (string text2 in Class255.class105_0.method_8<string>(ConfigOptions.IgnoreList))
			{
				this.fastObjectListView_21.AddObject(new ListItemViewModel(text2));
				this.list_15.Add(new Player(text2)
				{
					PermanentBan = true
				});
			}
			this.checkBox_55.CheckedChanged += this.checkBox_55_CheckedChanged;
			this.fastObjectListView_0.SmallImageList = new ImageList();
			this.fastObjectListView_0.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_0.ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_0.MouseDown += this.fastObjectListView_0_MouseDown;
			this.button_4.Click += this.button_4_Click;
			this.fastObjectListView_11.SmallImageList = new ImageList();
			this.fastObjectListView_11.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_16.ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_11.MouseDown += this.fastObjectListView_11_MouseDown;
			this.button_52.Click += this.button_52_Click;
			this.fastObjectListView_1.SmallImageList = new ImageList();
			this.fastObjectListView_1.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_1.ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_1.MouseDown += this.fastObjectListView_1_MouseDown;
			this.button_12.Click += this.button_12_Click;
			this.method_101();
			foreach (LiveSearchListItem liveSearchListItem in Class255.LiveSearchList)
			{
				*(byte*)ptr = (this.dictionary_2.ContainsKey(liveSearchListItem.Id) ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					this.method_35(liveSearchListItem);
					this.dictionary_2.Add(liveSearchListItem.Id, this.method_96(liveSearchListItem.Id));
				}
			}
			this.fastObjectListView_18.SmallImageList = new ImageList();
			this.fastObjectListView_18.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_24.ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_18.MouseDown += this.fastObjectListView_18_MouseDown;
			this.button_86.Click += this.button_86_Click;
			this.fastObjectListView_2.SmallImageList = new ImageList();
			this.fastObjectListView_2.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_2.ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_2.MouseDown += this.fastObjectListView_2_MouseDown;
			this.button_16.Click += this.button_16_Click;
			foreach (FlippingListItem flippingListItem in Class255.FlippingList)
			{
				this.dataGridView_0.Rows.Add(flippingListItem.ToDataGrid());
			}
			this.fastObjectListView_3.Enabled = Class255.class105_0.method_4(ConfigOptions.EnableGoAFK);
			this.fastObjectListView_3.SmallImageList = new ImageList();
			this.fastObjectListView_3.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_3.ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_3.MouseDown += this.fastObjectListView_3_MouseDown;
			this.button_21.Click += this.button_21_Click;
			foreach (string text3 in Class255.class105_0.method_8<string>(ConfigOptions.AFKMessagesList))
			{
				this.fastObjectListView_3.AddObject(new ListItemViewModel(text3));
			}
			this.checkBox_1.CheckedChanged += this.checkBox_1_CheckedChanged;
			this.fastObjectListView_4.SmallImageList = new ImageList();
			this.fastObjectListView_4.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_4.ImageGetter = new ImageGetterDelegate(DecimalCurrencyListItem.ImageGetter);
			this.fastObjectListView_4.MouseDown += this.fastObjectListView_4_MouseDown;
			this.button_27.Click += this.button_27_Click;
			this.fastObjectListView_4.AddObjects(Class255.DecimalCurrencyList);
			this.fastObjectListView_5.SmallImageList = new ImageList();
			this.fastObjectListView_5.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.olvcolumn_6.ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_5.MouseDown += this.fastObjectListView_5_MouseDown;
			this.button_30.Click += this.button_30_Click;
			foreach (BulkBuyingListItem bulkBuyingListItem in Class255.BulkBuyingList)
			{
				this.dataGridView_2.Rows.Add(bulkBuyingListItem.ToDataGrid());
			}
			foreach (ItemBuyingListItem itemBuyingListItem in Class255.ItemBuyingList)
			{
				this.dataGridView_3.Rows.Add(itemBuyingListItem.ToDataGrid());
			}
			this.fastObjectListView_10.SmallImageList = new ImageList();
			this.fastObjectListView_10.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_10.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_10.MouseDown += this.fastObjectListView_10_MouseDown;
			this.button_50.Click += this.button_50_Click;
			this.fastObjectListView_17.SmallImageList = new ImageList();
			this.fastObjectListView_17.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_17.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_17.MouseDown += this.fastObjectListView_17_MouseDown;
			this.button_84.Click += this.button_84_Click;
			foreach (string text4 in Class255.class105_0.method_8<string>(ConfigOptions.AcceptedCurrencyList))
			{
				this.fastObjectListView_17.AddObject(new ListItemViewModel(text4));
			}
			this.fastObjectListView_12.SmallImageList = new ImageList();
			this.fastObjectListView_12.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_12.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_12.MouseDown += this.fastObjectListView_12_MouseDown;
			this.button_55.Click += this.button_55_Click;
			foreach (string text5 in Class255.class105_0.method_8<string>(ConfigOptions.BulkTypeList))
			{
				this.fastObjectListView_12.AddObject(new ListItemViewModel(text5));
			}
			Mods mods = JsonConvert.DeserializeObject<Mods>(Class237.Map);
			IEnumerable<IGrouping<string, Mod>> source = mods.Normal.GroupBy(new Func<Mod, string>(MainForm.<>c.<>9.method_45));
			this.fastObjectListView_14.SmallImageList = new ImageList();
			this.fastObjectListView_14.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_14.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(AffixItemViewModel.ImageGetter);
			this.fastObjectListView_14.MouseDown += this.fastObjectListView_14_MouseDown;
			this.button_58.Click += this.button_58_Click;
			this.fastObjectListView_13.SmallImageList = new ImageList();
			this.fastObjectListView_13.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_13.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(AffixItemViewModel.ImageGetter);
			this.fastObjectListView_13.MouseDown += this.fastObjectListView_13_MouseDown;
			this.button_57.Click += this.button_57_Click;
			this.comboBox_54.Items.smethod_22(source.Select(new Func<IGrouping<string, Mod>, Mod>(MainForm.<>c.<>9.method_50)));
			this.comboBox_53.Items.smethod_22(source.Select(new Func<IGrouping<string, Mod>, Mod>(MainForm.<>c.<>9.method_51)));
			using (List<string>.Enumerator enumerator10 = Class255.MapPreventedModList.GetEnumerator())
			{
				while (enumerator10.MoveNext())
				{
					MainForm.Class40 @class = new MainForm.Class40();
					@class.string_0 = enumerator10.Current;
					Mod mod = mods.Normal.FirstOrDefault(new Func<Mod, bool>(@class.method_0));
					((byte*)ptr)[1] = ((mod == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						this.fastObjectListView_14.AddObject(new AffixItemViewModel(mod, null));
					}
				}
			}
			using (List<string>.Enumerator enumerator11 = Class255.MapForcedModList.GetEnumerator())
			{
				while (enumerator11.MoveNext())
				{
					MainForm.Class41 class2 = new MainForm.Class41();
					class2.string_0 = enumerator11.Current;
					Mod mod2 = mods.Normal.FirstOrDefault(new Func<Mod, bool>(class2.method_0));
					((byte*)ptr)[2] = ((mod2 == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						this.fastObjectListView_13.AddObject(new AffixItemViewModel(mod2, null));
					}
				}
			}
			this.fastObjectListView_14.Refresh();
			this.fastObjectListView_13.Refresh();
			this.fastObjectListView_15.SmallImageList = new ImageList();
			this.fastObjectListView_15.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_15.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_15.MouseDown += this.fastObjectListView_15_MouseDown;
			this.button_68.Click += this.button_68_Click;
			this.checkBox_53.Checked = Class255.class105_0.method_4(ConfigOptions.SoldMessageEnabled);
			this.fastObjectListView_19.Enabled = Class255.class105_0.method_4(ConfigOptions.SoldMessageEnabled);
			this.fastObjectListView_19.SmallImageList = new ImageList();
			this.fastObjectListView_19.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_19.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_19.MouseDown += this.fastObjectListView_19_MouseDown;
			this.button_87.Click += this.button_87_Click;
			foreach (string text6 in Class255.class105_0.method_8<string>(ConfigOptions.SoldMessageList))
			{
				this.fastObjectListView_19.AddObject(new ListItemViewModel(text6));
			}
			this.checkBox_53.CheckedChanged += this.checkBox_53_CheckedChanged;
			this.fastObjectListView_16.SmallImageList = new ImageList();
			this.fastObjectListView_16.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_16.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_16.MouseDown += this.fastObjectListView_16_MouseDown;
			this.button_74.Click += this.button_74_Click;
			this.fastObjectListView_22.SmallImageList = new ImageList();
			this.fastObjectListView_22.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_22.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_22.MouseDown += this.fastObjectListView_22_MouseDown;
			this.button_96.Click += this.button_96_Click;
			foreach (string text7 in Class255.class105_0.method_8<string>(ConfigOptions.CheapChaosList))
			{
				this.fastObjectListView_22.AddObject(new ListItemViewModel(text7));
			}
			this.fastObjectListView_24.SmallImageList = new ImageList();
			this.fastObjectListView_24.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_24.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(ListItemViewModel.ImageGetter);
			this.fastObjectListView_24.MouseDown += this.fastObjectListView_24_MouseDown;
			this.button_100.Click += this.button_100_Click;
			foreach (string text8 in Class255.class105_0.method_8<string>(ConfigOptions.GwennenItemList))
			{
				this.fastObjectListView_24.AddObject(new ListItemViewModel(text8));
			}
			this.fastObjectListView_23.SmallImageList = new ImageList();
			this.fastObjectListView_23.SmallImageList.Images.Add(MainForm.getString_0(107396334), Class238.x);
			this.fastObjectListView_23.AllColumns.First<OLVColumn>().ImageGetter = new ImageGetterDelegate(JsonTab.smethod_0);
			this.fastObjectListView_23.MouseDown += this.fastObjectListView_23_MouseDown;
			this.button_97.Click += this.button_97_Click;
			this.method_13();
		}

		private unsafe void MainForm_Load(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[8];
			this.form2_0 = new Form2();
			ComboBox.ObjectCollection items = this.comboBox_11.Items;
			object[] items2 = Class120.list_1.ToArray();
			items.AddRange(items2);
			ComboBox.ObjectCollection items3 = this.comboBox_10.Items;
			items2 = Class120.list_2.ToArray();
			items3.AddRange(items2);
			Class255.smethod_0(this);
			Class120.smethod_0();
			this.method_85(null);
			this.method_54();
			this.method_47();
			this.method_48();
			this.method_53();
			this.method_51();
			this.method_43();
			this.method_55();
			Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107393722), new object[]
			{
				Class120.string_3
			});
			if (!string.IsNullOrEmpty(Class120.PoEDirectory) && UI.intptr_0 == IntPtr.Zero)
			{
				try
				{
					File.WriteAllText(Class120.PoELogFile, MainForm.getString_0(107396269));
				}
				catch (Exception)
				{
				}
			}
			*(byte*)ptr = ((!Class255.class105_0.method_3(ConfigOptions.AuthKey).smethod_25()) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Class123.smethod_2(this);
			}
			else
			{
				this.bool_1 = true;
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107393673));
			}
			Task.Run(new Action(this.method_159));
			foreach (object obj in this.groupBox_20.Controls)
			{
				((byte*)ptr)[1] = ((obj is RadioButton) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((byte*)ptr)[2] = ((((RadioButton)obj).Text == Class255.class105_0.method_3(ConfigOptions.SelectedPreset)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						((RadioButton)obj).Checked = true;
					}
				}
			}
			foreach (object obj2 in this.groupBox_38.Controls)
			{
				((byte*)ptr)[3] = ((obj2 is RadioButton) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					((byte*)ptr)[4] = ((((RadioButton)obj2).Text == Class255.class105_0.method_3(ConfigOptions.RareCraftType)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						((RadioButton)obj2).Checked = true;
					}
				}
			}
			foreach (object obj3 in this.groupBox_16.Controls)
			{
				((byte*)ptr)[5] = ((obj3 is RadioButton) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					((byte*)ptr)[6] = ((((RadioButton)obj3).Text == Class255.class105_0.method_3(ConfigOptions.MapCraftChoice)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						((RadioButton)obj3).Checked = true;
					}
				}
			}
			new ToolTip().SetToolTip(this.pictureBox_2, MainForm.getString_0(107393588));
			Util.smethod_25();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.Refresh();
			((byte*)ptr)[7] = (this.bool_21 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 7) != 0)
			{
				this.button_67_Click(null, null);
			}
			if (Class120.UsingWindows7 && Class255.class105_0.method_4(ConfigOptions.DisableAero))
			{
				Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107393942));
				UI.smethod_104();
			}
		}

		private void method_55()
		{
			this.method_68();
			this.method_75(Class255.class105_0.method_4(ConfigOptions.UseDumpTabs));
			this.checkBox_4_CheckedChanged(null, null);
			this.checkBox_52_CheckedChanged(null, null);
			this.method_113(null, null);
			this.checkBox_29_CheckedChanged(null, null);
			this.method_9();
			this.checkBox_35_CheckedChanged(null, null);
			this.checkBox_58_CheckedChanged(null, null);
			this.checkBox_46_CheckedChanged(null, null);
			this.checkBox_45_CheckedChanged(null, null);
			this.checkBox_47_CheckedChanged(null, null);
			this.checkBox_38_CheckedChanged(null, null);
			this.checkBox_63_CheckedChanged(null, null);
			this.checkBox_72_CheckedChanged(null, null);
			this.checkBox_68_CheckedChanged(null, null);
			this.checkBox_75_CheckedChanged(null, null);
			this.checkBox_79_CheckedChanged(null, null);
			this.comboBox_57.SelectedIndex = 0;
			this.comboBox_60.Items.smethod_22(Class344.smethod_0());
			this.comboBox_60.SelectedIndex = 0;
		}

		public void method_56(Player player_0, bool bool_23 = false)
		{
			MainForm.Class42 @class = new MainForm.Class42();
			@class.player_0 = player_0;
			@class.bool_0 = bool_23;
			@class.mainForm_0 = this;
			if (@class.player_0 != null)
			{
				base.Invoke(new Action(@class.method_0));
			}
		}

		public void method_57(Player player_0)
		{
			MainForm.Class43 @class = new MainForm.Class43();
			@class.mainForm_0 = this;
			@class.player_0 = player_0;
			base.Invoke(new Action(@class.method_0));
		}

		[DebuggerStepThrough]
		public Task method_58(bool bool_23 = false, MainForm.GEnum1 genum1_0 = MainForm.GEnum1.const_0, bool bool_24 = false)
		{
			MainForm.Class46 @class = new MainForm.Class46();
			@class.mainForm_0 = this;
			@class.bool_0 = bool_23;
			@class.genum1_0 = genum1_0;
			@class.bool_1 = bool_24;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<MainForm.Class46>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		[DebuggerStepThrough]
		public Task method_59(MainForm.GEnum1 genum1_0)
		{
			MainForm.Class49 @class = new MainForm.Class49();
			@class.mainForm_0 = this;
			@class.genum1_0 = genum1_0;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<MainForm.Class49>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		public unsafe void method_60()
		{
			void* ptr = stackalloc byte[14];
			Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393865));
			UI.smethod_1();
			UI.smethod_17();
			UI.smethod_15();
			UI.smethod_32(0, -2, Enum2.const_3, true);
			Thread.Sleep(1000);
			for (;;)
			{
				Position position;
				((byte*)ptr)[10] = ((!UI.smethod_3(out position, Images.InventoryPatch, MainForm.getString_0(107393225))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) == 0)
				{
					break;
				}
				((byte*)ptr)[8] = (UI.smethod_3(out position, Images.KeepDestroyWindow, MainForm.getString_0(107393308)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 8) != 0)
				{
					Win32.smethod_14(MainForm.getString_0(107393263), false);
					Thread.Sleep(200);
				}
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393254));
				UI.smethod_32(0, -2, Enum2.const_3, true);
				*(int*)ptr = UI.smethod_83(12);
				List<JsonItem> list = new List<JsonItem>();
				*(int*)((byte*)ptr + 4) = 0;
				for (;;)
				{
					((byte*)ptr)[9] = ((*(int*)((byte*)ptr + 4) < *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						break;
					}
					JsonItem item = new JsonItem
					{
						w = 1,
						h = 1
					};
					list.Add(item);
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				list = InventoryManager.smethod_5(list);
				Position position2 = InventoryManager.smethod_8(list, new JsonItem
				{
					w = 1,
					h = 1
				}).First<Position>();
				UI.smethod_32(position2.Left, position2.Top, Enum2.const_3, true);
				Thread.Sleep(100);
				Win32.smethod_2(true);
			}
			UI.smethod_31(false, 1, 12, 5);
			((byte*)ptr)[11] = ((!this.list_14.Any<Item>()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 11) != 0)
			{
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393248));
			}
			else
			{
				UI.bitmap_0.smethod_12();
				UI.bitmap_0 = UI.smethod_67();
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393207));
				UI.smethod_44(null);
				((byte*)ptr)[12] = (this.method_40() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 12) != 0)
				{
					((byte*)ptr)[13] = (UI.smethod_72() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 13) != 0)
					{
						UI.smethod_18();
					}
					UI.smethod_31(false, 60, 12, 5);
					CleanInventory.smethod_1(null, this.list_14.smethod_11());
					UI.smethod_51();
				}
			}
		}

		public void method_61()
		{
			this.list_4.Clear();
			this.list_7.Clear();
			this.list_9.Clear();
			this.list_8.Clear();
			this.list_12.Clear();
			this.list_13.Clear();
			this.list_3.Clear();
			this.list_2.Clear();
			this.list_11.Clear();
			this.list_10.Clear();
			this.dictionary_5.Clear();
			this.bool_6 = false;
			base.Invoke(new Action(this.method_160));
			this.list_18.Clear();
			this.list_19.Clear();
			this.int_4 = 0;
			foreach (KeyValuePair<string, Class260> keyValuePair in this.dictionary_2)
			{
				keyValuePair.Value.method_2(false);
			}
		}

		[DebuggerStepThrough]
		public void method_62()
		{
			MainForm.Class50 @class = new MainForm.Class50();
			@class.mainForm_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class50>(ref @class);
		}

		public void method_63()
		{
			this.bool_17 = true;
		}

		public void method_64(bool bool_23 = true)
		{
			MainForm.Class51 @class = new MainForm.Class51();
			@class.mainForm_0 = this;
			@class.bool_0 = bool_23;
			try
			{
				base.Invoke(new Action(@class.method_0));
			}
			catch
			{
			}
		}

		public void method_65()
		{
			base.Invoke(new Action(this.method_163));
		}

		public void method_66()
		{
			base.Invoke(new Action(this.method_164));
		}

		public void method_67(bool bool_23, Order order_1)
		{
			MainForm.Class52 @class = new MainForm.Class52();
			@class.mainForm_0 = this;
			@class.order_0 = order_1;
			@class.bool_0 = bool_23;
			base.Invoke(new Action(@class.method_0));
		}

		public void method_68()
		{
			base.Invoke(new Action(this.method_165));
		}

		public unsafe void method_69()
		{
			void* ptr = stackalloc byte[34];
			this.method_68();
			((byte*)ptr)[32] = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 32) == 0)
			{
				JsonTab[] array = Stashes.Tabs.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_69)).ToArray<JsonTab>();
				this.comboBox_5.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_70)));
				ComboBox.ObjectCollection items = this.comboBox_9.Items;
				object[] items2 = array;
				items.AddRange(items2);
				ComboBox.ObjectCollection items3 = this.comboBox_8.Items;
				items2 = array;
				items3.AddRange(items2);
				ComboBox.ObjectCollection items4 = this.comboBox_7.Items;
				items2 = array;
				items4.AddRange(items2);
				ComboBox.ObjectCollection items5 = this.comboBox_6.Items;
				items2 = array;
				items5.AddRange(items2);
				ComboBox.ObjectCollection items6 = this.comboBox_4.Items;
				items2 = array;
				items6.AddRange(items2);
				ComboBox.ObjectCollection items7 = this.comboBox_3.Items;
				items2 = array;
				items7.AddRange(items2);
				this.comboBox_51.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_71)));
				this.comboBox_2.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_72)));
				this.comboBox_18.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_73)));
				this.comboBox_27.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_74)));
				this.comboBox_69.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_75)));
				this.comboBox_30.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_76)));
				ComboBox.ObjectCollection items8 = this.comboBox_47.Items;
				items2 = array;
				items8.AddRange(items2);
				this.comboBox_26.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_77)));
				this.comboBox_23.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_78)));
				this.comboBox_22.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_79)));
				this.comboBox_21.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_80)));
				this.comboBox_16.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_81)));
				this.comboBox_15.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_82)));
				this.comboBox_64.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_83)));
				ComboBox.ObjectCollection items9 = this.comboBox_14.Items;
				items2 = array;
				items9.AddRange(items2);
				this.comboBox_34.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_84)));
				this.comboBox_48.Items.smethod_22(Stashes.smethod_9(MainForm.getString_0(107393182)));
				this.comboBox_71.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_85)));
				this.comboBox_17.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_86)));
				this.comboBox_73.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_87)));
				this.comboBox_58.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_88)));
				this.comboBox_61.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_89)));
				this.comboBox_63.Items.smethod_22(array.Where(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_90)));
				((byte*)ptr)[33] = (array.Any<JsonTab>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 33) != 0)
				{
					this.comboBox_51.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_91));
					this.comboBox_2.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_92));
					this.comboBox_18.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_93));
					this.comboBox_73.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_94));
					this.comboBox_30.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_95));
					this.comboBox_34.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_96));
					this.comboBox_47.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_97));
					this.comboBox_58.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_98));
					this.comboBox_61.SelectedItem = array.FirstOrDefault(new Func<JsonTab, bool>(MainForm.<>c.<>9.method_99));
				}
				List<int> list = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_100)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.DumpTabList)).ToList<int>();
				foreach (int num in list)
				{
					*(int*)ptr = num;
					this.fastObjectListView_0.AddObject(Stashes.smethod_11(*(int*)ptr));
				}
				List<int> list2 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_101)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.ExcludedTabList)).ToList<int>();
				foreach (int num2 in list2)
				{
					*(int*)((byte*)ptr + 4) = num2;
					this.fastObjectListView_18.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 4)));
				}
				List<int> list3 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_102)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.VendorRecipeStashList)).ToList<int>();
				foreach (int num3 in list3)
				{
					*(int*)((byte*)ptr + 8) = num3;
					this.fastObjectListView_5.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 8)));
				}
				List<int> list4 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_103)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.MuleStashList)).ToList<int>();
				foreach (int num4 in list4)
				{
					*(int*)((byte*)ptr + 12) = num4;
					this.fastObjectListView_10.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 12)));
				}
				List<int> list5 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_104)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.PriceStashList)).ToList<int>();
				foreach (int num5 in list5)
				{
					*(int*)((byte*)ptr + 16) = num5;
					this.fastObjectListView_11.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 16)));
				}
				List<int> list6 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_105)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.GwennenStashList)).ToList<int>();
				foreach (int num6 in list6)
				{
					*(int*)((byte*)ptr + 20) = num6;
					this.fastObjectListView_23.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 20)));
				}
				List<int> list7 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_106)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.BeastStashList)).ToList<int>();
				foreach (int num7 in list7)
				{
					*(int*)((byte*)ptr + 24) = num7;
					this.fastObjectListView_15.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 24)));
				}
				List<int> list8 = array.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_107)).Intersect(Class255.class105_0.method_8<int>(ConfigOptions.StackedDeckList)).ToList<int>();
				foreach (int num8 in list8)
				{
					*(int*)((byte*)ptr + 28) = num8;
					this.fastObjectListView_16.AddObject(Stashes.smethod_11(*(int*)((byte*)ptr + 28)));
				}
				this.bool_13 = true;
				base.Invoke(new Action(this.method_166));
				this.bool_13 = false;
				this.method_84();
				this.method_75(Class255.class105_0.method_4(ConfigOptions.UseDumpTabs));
			}
		}

		public void method_70()
		{
			if (Stashes.Tabs != null && Stashes.Tabs.Any<JsonTab>())
			{
				base.Invoke(new Action(this.method_167));
				Class255.class105_0.method_9(ConfigOptions.CurrencyDumpTab, this.comboBox_5.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.MapsDumpTab, this.comboBox_9.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.FragmentsDumpTab, this.comboBox_8.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.DelveDumpTab, this.comboBox_7.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.EssenceDumpTab, this.comboBox_6.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.OtherDumpTab, this.comboBox_4.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.CardDumpTab, this.comboBox_3.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.FlippingTab, this.comboBox_27.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.HarvestDumpTab, this.comboBox_26.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.DeliriumDumpTab, this.comboBox_23.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.MetamorphDumpTab, this.comboBox_22.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.BlightDumpTab, this.comboBox_21.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.UltimatumDumpTab, this.comboBox_64.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.IncubatorDumpTab, this.comboBox_16.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.VialDumpTab, this.comboBox_15.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.StackedDeckDumpTab, this.comboBox_14.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.CraftCurrencyTab, this.comboBox_48.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.CraftMoreItemsTab, this.comboBox_71.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.MapCraftStashTab, this.comboBox_17.smethod_1(), false);
				Class255.class105_0.method_9(ConfigOptions.VaalCraftStash, this.comboBox_63.smethod_1(), false);
				Class255.class105_0.method_1();
				this.method_84();
			}
		}

		[DebuggerStepThrough]
		private void button_75_Click(object sender, EventArgs e)
		{
			MainForm.Class54 @class = new MainForm.Class54();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class54>(ref @class);
		}

		[DebuggerStepThrough]
		public void method_71(bool bool_23)
		{
			MainForm.Class56 @class = new MainForm.Class56();
			@class.mainForm_0 = this;
			@class.bool_0 = bool_23;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class56>(ref @class);
		}

		[DebuggerStepThrough]
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.Class57 @class = new MainForm.Class57();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.formClosingEventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class57>(ref @class);
		}

		private void button_0_Click(object sender, EventArgs e)
		{
			this.richTextBox_0.Clear();
			this.richTextBox_2.Clear();
		}

		private void button_1_Click(object sender, EventArgs e)
		{
			if (this.genum0_0 == MainForm.GEnum0.const_0)
			{
				this.method_58(false, MainForm.GEnum1.const_0, false);
			}
			else
			{
				Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107393129));
				this.bool_3 = true;
				this.bool_4 = true;
				this.method_64(true);
			}
		}

		private unsafe void comboBox_17_SelectedIndexChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (this.bool_13 ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = ((!string.IsNullOrEmpty(((ComboBox)sender).smethod_1())) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.method_70();
				}
			}
		}

		private unsafe void MainForm_Leave(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			object obj = this.object_1;
			*(byte*)ptr = 0;
			try
			{
				Monitor.Enter(obj, ref *(bool*)ptr);
				if (API.DataLoaded && this.bool_11)
				{
					this.method_86();
					this.method_102(false);
					this.method_103(false);
					this.method_104(false);
					Class255.class105_0.method_1();
					Class120.smethod_0();
					((byte*)ptr)[1] = ((this.form1_0 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						this.form1_0.method_3();
					}
					if (this.bool_1 && !Class255.class105_0.method_3(ConfigOptions.AuthKey).smethod_25())
					{
						this.bool_1 = false;
						Class123.smethod_2(this);
					}
					this.Refresh();
				}
			}
			finally
			{
				if (*(sbyte*)ptr != 0)
				{
					Monitor.Exit(obj);
				}
			}
		}

		private unsafe void method_72()
		{
			void* ptr = stackalloc byte[55];
			try
			{
				MainForm.Class58 @class;
				JsonTab jsonTab;
				for (;;)
				{
					((byte*)ptr)[54] = 1;
					Thread.Sleep(100);
					if (this.genum0_0 == MainForm.GEnum0.const_2 && !this.bool_17 && !MainForm.IsPaused)
					{
						((byte*)ptr)[20] = (this.bool_22 ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 20) == 0)
						{
							if (!this.bool_19)
							{
								goto IL_73;
							}
							if (this.list_7.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_108)))
							{
								goto IL_73;
							}
							bool flag = !this.list_8.Any<Order>();
							IL_74:
							if (flag)
							{
								goto IL_105F;
							}
							if (Class255.class105_0.method_4(ConfigOptions.FlippingEnabled) && !Stashes.bool_0)
							{
								continue;
							}
							((byte*)ptr)[21] = (this.bool_0 ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 21) != 0)
							{
								Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107393100));
								UI.smethod_55();
								continue;
							}
							((byte*)ptr)[22] = (Class255.class105_0.method_4(ConfigOptions.HideoutLogoutEnabled) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 22) != 0)
							{
								@class = new MainForm.Class58();
								*(int*)ptr = (int)Class255.class105_0.method_6(ConfigOptions.HideoutLogoutTime);
								@class.double_0 = (double)Class255.class105_0.method_6(ConfigOptions.HideoutLogoutPlayerTime);
								((byte*)ptr)[23] = (this.list_12.Any(new Func<Player, bool>(@class.method_0)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 23) != 0)
								{
									goto IL_106B;
								}
							}
							((byte*)ptr)[24] = ((DateTime.Now > this.dateTime_4) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 24) != 0 && (this.LiveSearchFeatureEnabled || this.ItemBuyFeatureEnabled || this.BulkBuyFeatureEnabled))
							{
								this.method_98();
							}
							if (DateTime.Now > this.dateTime_5 && Class255.DecimalCurrencyList.Any<DecimalCurrencyListItem>() && !this.bool_15)
							{
								this.method_106();
							}
							if (!this.PastFlipUpdatingTime || !Stashes.bool_0)
							{
								goto IL_1F1;
							}
							if (this.list_7.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_109)))
							{
								goto IL_1F1;
							}
							bool flag2 = !this.list_8.Any<Order>();
							IL_1F2:
							if (flag2)
							{
								MainForm.Class59 class2 = new MainForm.Class59();
								class2.mainForm_0 = this;
								class2.jsonTab_0 = null;
								this.comboBox_27.Invoke(new Action(class2.method_0));
								((byte*)ptr)[25] = ((class2.jsonTab_0 != null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 25) != 0)
								{
									((byte*)ptr)[26] = (this.bool_18 ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 26) != 0)
									{
										if (Class255.FlippingList.Where(new Func<FlippingListItem, bool>(MainForm.<>c.<>9.method_110)).All(new Func<FlippingListItem, bool>(MainForm.<>c.<>9.method_111)))
										{
											this.method_110();
											continue;
										}
									}
									Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393397));
									((byte*)ptr)[27] = ((!this.bool_18) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 27) != 0)
									{
										jsonTab = Stashes.smethod_14(MainForm.getString_0(107393328));
										if (jsonTab != null && !Web.smethod_15(jsonTab))
										{
											goto IL_1100;
										}
										this.bool_18 = true;
									}
									Flipping.smethod_9(class2.jsonTab_0);
									if ((Class120.bool_2 ? Flipping.smethod_7(this, class2.jsonTab_0) : Flipping.smethod_6(this, class2.jsonTab_0)) && Class255.class105_0.method_4(ConfigOptions.ResetInstanceAfterUpdate))
									{
										((byte*)ptr)[28] = ((!UI.smethod_51()) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 28) != 0)
										{
											break;
										}
										((byte*)ptr)[29] = ((!UI.smethod_44(null)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 29) != 0)
										{
											break;
										}
										((byte*)ptr)[30] = ((!UI.smethod_13(1)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 30) != 0)
										{
											break;
										}
									}
								}
								else
								{
									Class181.smethod_3(Enum11.const_2, string.Format(MainForm.getString_0(107392608), Class255.class105_0.method_3(ConfigOptions.FlippingTab)));
								}
								this.method_110();
							}
							((byte*)ptr)[31] = (Class255.class105_0.method_4(ConfigOptions.PreventAFK) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 31) != 0)
							{
								DateTime value = Win32.dateTime_0;
								TimeSpan timeSpan = DateTime.Now.Subtract(value);
								if (!this.bool_8 && timeSpan.Minutes >= 12)
								{
									UI.smethod_1();
									this.method_105();
								}
							}
							((byte*)ptr)[32] = (Class255.class105_0.method_4(ConfigOptions.EnableGoAFK) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 32) != 0)
							{
								((byte*)ptr)[33] = ((!this.bool_9) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 33) != 0)
								{
									*(int*)((byte*)ptr + 4) = this.random_0.Next((int)this.numericUpDown_4.Value, (int)this.numericUpDown_5.Value);
									*(int*)((byte*)ptr + 8) = this.random_0.Next((int)this.numericUpDown_7.Value, (int)this.numericUpDown_6.Value);
									this.dateTime_0 = DateTime.Now + new TimeSpan(0, *(int*)((byte*)ptr + 4), 0);
									this.dateTime_1 = DateTime.Now + new TimeSpan(0, *(int*)((byte*)ptr + 4) + *(int*)((byte*)ptr + 8), 0);
									this.bool_9 = true;
									Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393051) + this.dateTime_0.ToShortTimeString() + MainForm.getString_0(107392994) + this.dateTime_1.ToShortTimeString());
								}
								if (!this.bool_9 || !this.PastAFKTime)
								{
									goto IL_56A;
								}
								if (this.list_7.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_112)))
								{
									goto IL_56A;
								}
								bool flag3 = !this.list_8.Any<Order>();
								IL_56B:
								if (flag3)
								{
									Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393017) + this.dateTime_1.ToShortTimeString());
									this.bool_8 = true;
									List<string> list = Class255.class105_0.method_8<string>(ConfigOptions.AFKMessagesList);
									((byte*)ptr)[34] = (list.Any<string>() ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 34) != 0)
									{
										string arg = list[this.random_0.Next(list.Count)];
										Win32.smethod_16(string.Format(MainForm.getString_0(107392992), arg), true, true, false, false);
									}
									else
									{
										Win32.smethod_16(MainForm.getString_0(107392979), true, true, false, false);
									}
									do
									{
										((byte*)ptr)[36] = (this.PastAFKTime ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 36) == 0)
										{
											break;
										}
										Thread.Sleep(1000);
										this.method_111();
										this.method_112();
										((byte*)ptr)[35] = ((DateTime.Now >= this.dateTime_1) ? 1 : 0);
									}
									while (*(sbyte*)((byte*)ptr + 35) == 0);
								}
								if (DateTime.Now >= this.dateTime_1 && !this.bool_22)
								{
									Enum11 enum11_ = Enum11.const_0;
									string str = MainForm.getString_0(107392938);
									TimeSpan timeSpan2 = this.dateTime_1 - this.dateTime_0;
									*(int*)((byte*)ptr + 12) = timeSpan2.Minutes;
									Class181.smethod_3(enum11_, str + ((int*)((byte*)ptr + 12))->ToString() + MainForm.getString_0(107392909));
									this.bool_8 = false;
									this.bool_9 = false;
									this.method_105();
									goto IL_6DC;
								}
								goto IL_6DC;
								IL_56A:
								flag3 = false;
								goto IL_56B;
							}
							IL_6DC:
							using (List<Player>.Enumerator enumerator = this.list_13.ToList<Player>().GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									MainForm.Class60 class3 = new MainForm.Class60();
									class3.player_0 = enumerator.Current;
									if (DateTime.Now > class3.player_0.PartyTimeout && !this.list_12.Any(new Func<Player, bool>(class3.method_0)) && !this.list_4.Contains(class3.player_0) && this.list_7.Any(new Func<Order, bool>(class3.method_1)))
									{
										this.list_7.RemoveAll(new Predicate<Order>(class3.method_2));
										this.method_65();
										this.list_4.Add(class3.player_0);
										Class181.smethod_3(Enum11.const_3, string.Format(MainForm.getString_0(107392928), class3.player_0.name));
									}
								}
							}
							((byte*)ptr)[37] = ((this.list_4.Count != 0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 37) != 0)
							{
								using (List<Player>.Enumerator enumerator2 = this.list_4.ToList<Player>().GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										MainForm.Class61 class4 = new MainForm.Class61();
										class4.player_0 = enumerator2.Current;
										((byte*)ptr)[38] = 0;
										List<Order> list2 = this.list_8.ToList<Order>().Where(new Func<Order, bool>(class4.method_0)).ToList<Order>();
										foreach (Order item in list2)
										{
											this.list_7.Remove(item);
											List<Order> list3 = this.list_8;
											Predicate<Order> match;
											if ((match = class4.predicate_0) == null)
											{
												match = (class4.predicate_0 = new Predicate<Order>(class4.method_1));
											}
											list3.RemoveAll(match);
											this.list_12.Remove(class4.player_0);
											((byte*)ptr)[38] = 1;
											this.method_65();
											Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107392851), new object[]
											{
												class4.player_0.name
											});
										}
										((byte*)ptr)[39] = (this.list_13.Any(new Func<Player, bool>(class4.method_2)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 39) != 0)
										{
											UI.smethod_1();
											UI.smethod_40(class4.player_0);
										}
										this.list_4.Remove(class4.player_0);
										((byte*)ptr)[40] = ((*(sbyte*)((byte*)ptr + 38) == 0) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 40) != 0)
										{
											Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107392226), new object[]
											{
												class4.player_0.name
											});
										}
									}
								}
							}
							if (this.list_8.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_113)))
							{
								Order order = this.list_8.First(new Func<Order, bool>(MainForm.<>c.<>9.method_114));
								this.method_85(order);
								TradeProcessor.smethod_1(order, 1);
								Enum11 enum11_2 = Enum11.const_3;
								string text = MainForm.getString_0(107392137);
								object[] array = new object[3];
								array[0] = this.list_8.Count;
								array[1] = string.Join(MainForm.getString_0(107396352), this.list_8.Select(new Func<Order, string>(MainForm.<>c.<>9.method_115)));
								array[2] = this.list_7.Count;
								Class181.smethod_2(enum11_2, text, array);
								continue;
							}
							this.method_111();
							this.method_112();
							if (!this.PastFlipUpdatingTime && !this.PastAFKTime)
							{
								if (Class255.LiveSearchEnabled && Class255.LiveSearchList.Any<LiveSearchListItem>())
								{
									this.method_93();
								}
								((byte*)ptr)[41] = (Class255.class105_0.method_4(ConfigOptions.SkipPerandusCoinListings) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 41) != 0)
								{
									this.list_10.RemoveAll(new Predicate<BuyingTradeData>(MainForm.<>c.<>9.method_116));
								}
								((byte*)ptr)[42] = (this.list_10.Any<BuyingTradeData>() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 42) != 0)
								{
									MainForm.Class62 class5 = new MainForm.Class62();
									class5.buyingTradeData_0 = this.method_126();
									((byte*)ptr)[43] = ((class5.buyingTradeData_0 == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 43) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107392120));
										this.list_10.Clear();
										continue;
									}
									Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107392039), new object[]
									{
										class5.buyingTradeData_0
									});
									switch (class5.buyingTradeData_0.TradeType)
									{
									case TradeTypes.LiveSearch:
									{
										class5.buyingTradeData_0 = this.list_10.ToList<BuyingTradeData>().Where(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_117)).OrderBy(new Func<BuyingTradeData, int>(MainForm.<>c.<>9.method_118)).ThenByDescending(new Func<BuyingTradeData, DateTime>(MainForm.<>c.<>9.method_119)).First<BuyingTradeData>();
										LiveSearchListItem liveSearchListItem = Class255.LiveSearchList.FirstOrDefault(new Func<LiveSearchListItem, bool>(class5.method_0));
										((byte*)ptr)[44] = ((liveSearchListItem != null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 44) != 0)
										{
											((byte*)ptr)[45] = (liveSearchListItem.Enabled ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 45) != 0)
											{
												Class240 class240_ = new Class240(liveSearchListItem);
												BuyMessageProcessor.smethod_1(class5.buyingTradeData_0, class240_);
											}
											else
											{
												this.list_10.Remove(class5.buyingTradeData_0);
											}
										}
										break;
									}
									case TradeTypes.ItemBuying:
									{
										class5.buyingTradeData_0 = this.list_10.ToList<BuyingTradeData>().Where(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_120)).OrderBy(new Func<BuyingTradeData, int>(MainForm.<>c.<>9.method_121)).ThenByDescending(new Func<BuyingTradeData, int>(MainForm.<>c.<>9.method_122)).First<BuyingTradeData>();
										ItemBuyingListItem itemBuyingListItem = Class255.ItemBuyingList.FirstOrDefault(new Func<ItemBuyingListItem, bool>(class5.method_1));
										((byte*)ptr)[46] = ((itemBuyingListItem != null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 46) != 0)
										{
											((byte*)ptr)[47] = (itemBuyingListItem.Enabled ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 47) != 0)
											{
												Class240 class240_2 = new Class240(itemBuyingListItem);
												BuyMessageProcessor.smethod_1(class5.buyingTradeData_0, class240_2);
											}
											else
											{
												this.list_10.Remove(class5.buyingTradeData_0);
											}
										}
										break;
									}
									case TradeTypes.BulkBuying:
									{
										class5.buyingTradeData_0 = this.list_10.ToList<BuyingTradeData>().Where(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_123)).OrderBy(new Func<BuyingTradeData, int>(MainForm.<>c.<>9.method_124)).ThenBy(new Func<BuyingTradeData, decimal>(MainForm.<>c.<>9.method_125)).First<BuyingTradeData>();
										BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList.FirstOrDefault(new Func<BulkBuyingListItem, bool>(class5.method_2));
										((byte*)ptr)[48] = ((bulkBuyingListItem != null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 48) != 0)
										{
											((byte*)ptr)[49] = (bulkBuyingListItem.Enabled ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 49) != 0)
											{
												Class240 class240_3 = new Class240(bulkBuyingListItem);
												BuyMessageProcessor.smethod_1(class5.buyingTradeData_0, class240_3);
											}
											else
											{
												this.list_10.Remove(class5.buyingTradeData_0);
											}
										}
										break;
									}
									}
									*(int*)((byte*)ptr + 16) = this.list_10.RemoveAll(new Predicate<BuyingTradeData>(class5.method_3));
									Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107392522), new object[]
									{
										*(int*)((byte*)ptr + 16),
										this.list_10.Count,
										class5.buyingTradeData_0
									});
									this.method_124(TradeTypes.LiveSearch);
									this.method_124(TradeTypes.ItemBuying);
									this.method_124(TradeTypes.BulkBuying);
								}
							}
							if (!this.PastFlipUpdatingTime && !this.PastAFKTime)
							{
								((byte*)ptr)[50] = ((!this.bool_6) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 50) != 0)
								{
									Order order2 = this.method_140();
									((byte*)ptr)[51] = ((order2 != null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 51) != 0)
									{
										bool flag4;
										if (order2.OrderType == Order.Type.Buy)
										{
											flag4 = this.list_7.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_126));
										}
										else
										{
											flag4 = false;
										}
										if (!flag4)
										{
											Class170.smethod_1(order2);
											continue;
										}
										continue;
									}
								}
								else
								{
									Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107392453));
								}
							}
							((byte*)ptr)[52] = (this.PastItemBuyingTime ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 52) != 0)
							{
								Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107392440));
								this.method_94();
							}
							((byte*)ptr)[53] = (this.PastBulkBuyingTime ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 53) != 0)
							{
								Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107392363));
								this.method_95();
								continue;
							}
							continue;
							IL_1F1:
							flag2 = false;
							goto IL_1F2;
							IL_73:
							flag = false;
							goto IL_74;
						}
						this.method_112();
					}
				}
				return;
				IL_105F:
				this.method_64(true);
				return;
				IL_106B:
				Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107393563), new object[]
				{
					@class.double_0,
					*(int*)ptr
				});
				this.timer_2.Enabled = false;
				UI.smethod_85();
				Thread.Sleep(1000);
				this.gameProcessState_0 = GameProcessState.Login;
				this.expectedState_0 = ExpectedState.Running;
				UI.smethod_1();
				Thread.Sleep(*(int*)ptr * 60000);
				this.bool_10 = true;
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107393410));
				this.method_63();
				return;
				IL_1100:
				Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107393315), new object[]
				{
					jsonTab.n
				});
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107392693));
				this.method_64(true);
			}
			catch (ThreadAbortException)
			{
			}
			catch (KeyNotFoundException)
			{
				Class181.smethod_3(Enum11.const_2, string.Format(MainForm.getString_0(107392350), Array.Empty<object>()));
				this.method_64(true);
			}
			catch (Exception ex)
			{
				if (ex is Win32Exception && ex.HResult == -2147467259)
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107391725));
					this.method_64(true);
				}
				else
				{
					Class181.smethod_3(Enum11.const_2, string.Format(MainForm.getString_0(107391603), ex));
					this.method_63();
				}
			}
		}

		private void timer_0_Tick(object sender, EventArgs e)
		{
			this.toolStripLabel_1.Text = string.Format(MainForm.getString_0(107391574), this.stopwatch_0.Elapsed.Days * 24 + this.stopwatch_0.Elapsed.Hours, this.stopwatch_0.Elapsed.Minutes, this.stopwatch_0.Elapsed.Seconds);
			this.list_15.Where(new Func<Player, bool>(MainForm.<>c.<>9.method_127)).ToList<Player>().ForEach(new Action<Player>(this.method_168));
		}

		private void timer_1_Tick(object sender, EventArgs e)
		{
			Task.Run(new Action(this.method_169));
		}

		[DebuggerStepThrough]
		private void button_54_Click(object sender, EventArgs e)
		{
			MainForm.Class63 @class = new MainForm.Class63();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class63>(ref @class);
		}

		[DebuggerStepThrough]
		private void method_73()
		{
			MainForm.Class64 @class = new MainForm.Class64();
			@class.mainForm_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class64>(ref @class);
		}

		public void method_74()
		{
			base.Invoke(new Action(this.method_172));
		}

		private void button_3_Click(object sender, EventArgs e)
		{
			new Process
			{
				StartInfo = new ProcessStartInfo(Class120.string_1),
				StartInfo = 
				{
					Verb = string.Empty
				}
			}.Start();
		}

		private void tabControl_2_TabIndexChanged(object sender, EventArgs e)
		{
			this.MainForm_Leave(sender, e);
		}

		private void checkBox_6_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox checkBox = (CheckBox)sender;
			this.method_75(checkBox.Checked);
		}

		private void method_75(bool bool_23)
		{
			this.comboBox_5.Enabled = bool_23;
			this.comboBox_9.Enabled = bool_23;
			this.comboBox_6.Enabled = bool_23;
			this.comboBox_3.Enabled = bool_23;
			this.comboBox_4.Enabled = bool_23;
			this.comboBox_7.Enabled = bool_23;
			this.comboBox_8.Enabled = bool_23;
			this.button_4.Enabled = bool_23;
			this.comboBox_2.Enabled = bool_23;
			this.fastObjectListView_0.Enabled = bool_23;
			this.comboBox_23.Enabled = bool_23;
			this.comboBox_22.Enabled = bool_23;
			this.comboBox_21.Enabled = bool_23;
			this.comboBox_16.Enabled = bool_23;
			this.comboBox_15.Enabled = bool_23;
			this.comboBox_14.Enabled = bool_23;
			this.comboBox_26.Enabled = bool_23;
		}

		[DebuggerStepThrough]
		private void button_11_Click(object sender, EventArgs e)
		{
			MainForm.Class65 @class = new MainForm.Class65();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class65>(ref @class);
		}

		[DebuggerStepThrough]
		private void method_76()
		{
			MainForm.Class69 @class = new MainForm.Class69();
			@class.mainForm_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class69>(ref @class);
		}

		public unsafe void method_77(string string_11)
		{
			void* ptr = stackalloc byte[2];
			MainForm.Class70 @class = new MainForm.Class70();
			@class.string_0 = string_11;
			*(byte*)ptr = ((this.genum0_0 != MainForm.GEnum0.const_2) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				@class.liveSearchListItem_0 = Class255.LiveSearchList.FirstOrDefault(new Func<LiveSearchListItem, bool>(@class.method_0));
				((byte*)ptr)[1] = ((@class.liveSearchListItem_0 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					Task.Run(new Action(@class.method_1));
				}
			}
		}

		public unsafe void method_78(Class260 class260_0, string string_11)
		{
			void* ptr = stackalloc byte[2];
			MainForm.Class71 @class = new MainForm.Class71();
			@class.string_0 = string_11;
			@class.class260_0 = class260_0;
			*(byte*)ptr = ((this.genum0_0 != MainForm.GEnum0.const_2) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				@class.liveSearchListItem_0 = Class255.LiveSearchList.FirstOrDefault(new Func<LiveSearchListItem, bool>(@class.method_0));
				((byte*)ptr)[1] = ((@class.liveSearchListItem_0 == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107391545), new object[]
					{
						@class.string_0,
						@class.liveSearchListItem_0.Description,
						@class.class260_0.CloseError
					});
					if (@class.liveSearchListItem_0.Enabled && (@class.liveSearchListItem_0.MaxPrice == null || @class.liveSearchListItem_0.MaxPrice.Amount > 0m))
					{
						MainForm.Class72 class2 = new MainForm.Class72();
						class2.class71_0 = @class;
						class2.int_0 = new Random().Next(30, 90);
						Task.Run(new Action(class2.method_0));
					}
					else
					{
						Class181.smethod_3(Enum11.const_0, string.Format(MainForm.getString_0(107392020), @class.liveSearchListItem_0.Description));
					}
				}
			}
		}

		public void method_79(string string_11, string string_12)
		{
			if (this.genum0_0 == MainForm.GEnum0.const_2)
			{
				Class181.smethod_3(Enum11.const_2, string.Format(MainForm.getString_0(107391947), string_11, string_12));
			}
		}

		public void method_80(string string_11, List<string> list_20)
		{
			if (this.genum0_0 != MainForm.GEnum0.const_0 && !this.bool_12 && !this.bool_8 && !(UI.intptr_0 == IntPtr.Zero))
			{
				Class181.smethod_3(Enum11.const_3, string.Format(MainForm.getString_0(107391934), string_11, string.Join(MainForm.getString_0(107391893), list_20)));
				foreach (string id in list_20)
				{
					this.list_11.Add(new TradeFetchId(string_11, id));
				}
			}
		}

		private unsafe void comboBox_1_DrawItem(object sender, DrawItemEventArgs e)
		{
			void* ptr = stackalloc byte[3];
			ComboBox comboBox = sender as ComboBox;
			*(byte*)ptr = ((comboBox != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				e.DrawBackground();
				((byte*)ptr)[1] = ((e.Index >= 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					StringFormat stringFormat = new StringFormat();
					stringFormat.LineAlignment = StringAlignment.Center;
					stringFormat.Alignment = StringAlignment.Center;
					Brush brush = new SolidBrush(comboBox.ForeColor);
					((byte*)ptr)[2] = (((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						brush = SystemBrushes.HighlightText;
					}
					e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), comboBox.Font, brush, e.Bounds, stringFormat);
				}
			}
		}

		protected CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 33554432;
				return createParams;
			}
		}

		private void method_81(Button button_101, Class242 class242_3)
		{
			if (!class242_3.SettingHotkeyActive)
			{
				class242_3.method_3();
				class242_3.method_2();
				class242_3.method_5();
				button_101.Text = MainForm.getString_0(107391856);
				class242_3.method_3();
			}
			else
			{
				button_101.Text = MainForm.getString_0(107391871);
				class242_3.ResetKeysPressed();
				class242_3.method_6(class242_3.KeysPressed);
			}
			class242_3.method_1();
		}

		private unsafe void button_6_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((sender == this.button_6) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.method_81((Button)sender, this.class242_0);
			}
			((byte*)ptr)[1] = ((sender == this.button_64) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.method_81((Button)sender, this.class242_1);
			}
			((byte*)ptr)[2] = ((sender == this.button_66) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				this.method_81((Button)sender, this.class242_2);
			}
		}

		private unsafe void method_82(Class242 class242_3, PreviewKeyDownEventArgs previewKeyDownEventArgs_0)
		{
			void* ptr = stackalloc byte[5];
			*(byte*)ptr = ((previewKeyDownEventArgs_0.KeyCode == Keys.Escape) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				class242_3.method_3();
			}
			else
			{
				Key key_ = KeyInterop.KeyFromVirtualKey((int)previewKeyDownEventArgs_0.KeyCode);
				((byte*)ptr)[1] = ((!class242_3.SettingHotkeyActive) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					return;
				}
				((byte*)ptr)[2] = ((!KeyToStringHelper.smethod_1(key_)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					return;
				}
				string item = KeyToStringHelper.smethod_2(key_);
				((byte*)ptr)[3] = (class242_3.SetHotkeys.Contains(item) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					return;
				}
				((byte*)ptr)[4] = ((!KeyToStringHelper.smethod_5(key_)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					class242_3.method_3();
				}
				class242_3.method_4(item);
			}
			class242_3.method_2();
		}

		private unsafe void textBox_2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((sender == this.textBox_2) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.method_82(this.class242_0, e);
			}
			((byte*)ptr)[1] = ((sender == this.textBox_15) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.method_82(this.class242_1, e);
			}
			((byte*)ptr)[2] = ((sender == this.textBox_16) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				this.method_82(this.class242_2, e);
			}
		}

		private unsafe void method_83(object object_3, KeyEventArgs keyEventArgs_0, Class242 class242_3, Action action_0)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = (class242_3.SettingHotkeyActive ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				Key key_ = KeyInterop.KeyFromVirtualKey(keyEventArgs_0.KeyValue);
				((byte*)ptr)[1] = ((!KeyToStringHelper.smethod_1(key_)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					string item = KeyToStringHelper.smethod_2(key_);
					((byte*)ptr)[2] = ((!class242_3.KeysPressed.Contains(item)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						class242_3.ResetKeysPressed();
					}
					else
					{
						class242_3.KeysPressed.Remove(item);
						((byte*)ptr)[3] = ((!class242_3.KeysPressed.Any<string>()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							action_0();
							class242_3.ResetKeysPressed();
						}
					}
				}
			}
		}

		private unsafe void method_84()
		{
			void* ptr = stackalloc byte[2];
			JsonTab jsonTab = (JsonTab)this.comboBox_27.SelectedItem;
			foreach (JsonTab jsonTab2 in Stashes.Tabs)
			{
				jsonTab2.IsExcluded = false;
			}
			foreach (JsonTab jsonTab3 in Stashes.Tabs)
			{
				foreach (object obj in this.fastObjectListView_18.Objects)
				{
					JsonTab jsonTab4 = (JsonTab)obj;
					*(byte*)ptr = ((jsonTab3 == jsonTab4) ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						jsonTab3.IsExcluded = true;
					}
				}
			}
			foreach (JsonTab jsonTab5 in Stashes.Tabs)
			{
				((byte*)ptr)[1] = ((jsonTab5 == jsonTab) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					jsonTab.IsExcluded = true;
				}
			}
		}

		private void button_2_Click(object sender, EventArgs e)
		{
			Task.Run(new Action(MainForm.<>c.<>9.method_128));
		}

		public void method_85(Order order_1)
		{
			MainForm.Class73 @class = new MainForm.Class73();
			@class.order_0 = order_1;
			@class.mainForm_0 = this;
			base.Invoke(new Action(@class.method_0));
		}

		[DebuggerStepThrough]
		private void method_86()
		{
			MainForm.Class75 @class = new MainForm.Class75();
			@class.mainForm_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class75>(ref @class);
		}

		private void comboBox_0_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.bool_14)
			{
				Class255.class105_0.method_9(ConfigOptions.CharacterSelected, this.comboBox_0.SelectedIndex, true);
			}
		}

		private void button_59_Click(object sender, EventArgs e)
		{
			string text = Class120.string_5 + MainForm.getString_0(107391822) + DateTime.Now.ToString(MainForm.getString_0(107391813)) + MainForm.getString_0(107391828);
			if (File.Exists(text))
			{
				Process.Start(text);
			}
		}

		private void checkBox_4_CheckedChanged(object sender, EventArgs e)
		{
			this.textBox_0.Enabled = this.checkBox_4.Checked;
			this.textBox_1.Enabled = this.checkBox_4.Checked;
			this.checkBox_17.Enabled = this.checkBox_4.Checked;
			this.numericUpDown_3.Enabled = this.checkBox_17.Checked;
			this.numericUpDown_2.Enabled = this.checkBox_17.Checked;
		}

		private unsafe void radioButton_2_CheckedChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			RadioButton radioButton = sender as RadioButton;
			string text = ((RadioButton)sender).Text;
			if (!radioButton.Checked && text == MainForm.getString_0(107391787))
			{
				this.method_90();
			}
			else
			{
				Class255.class105_0.method_9(ConfigOptions.SelectedPreset, text, true);
				*(byte*)ptr = ((text == MainForm.getString_0(107391798)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					this.method_87(MainForm.getString_0(107391233));
					this.method_89(false);
				}
				((byte*)ptr)[1] = ((text == MainForm.getString_0(107391256)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.method_87(MainForm.getString_0(107391207));
					this.method_89(false);
				}
				((byte*)ptr)[2] = ((text == MainForm.getString_0(107391787)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					this.method_89(true);
					((byte*)ptr)[3] = (Class255.class105_0.method_2<Dictionary<NumericUpDown, decimal>>(ConfigOptions.CustomSettings).Any<KeyValuePair<NumericUpDown, decimal>>() ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						this.method_88();
					}
				}
			}
		}

		private unsafe void method_87(string string_11)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((string_11 == MainForm.getString_0(107391233)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.numericUpDown_14.Value = 500m;
				this.numericUpDown_10.Value = 600m;
				this.numericUpDown_13.Value = 200m;
				this.numericUpDown_12.Value = 200m;
				this.numericUpDown_11.Value = 750m;
				this.numericUpDown_22.Value = 60m;
				this.numericUpDown_20.Value = 40m;
				this.numericUpDown_21.Value = 20m;
				this.numericUpDown_19.Value = 3m;
				this.numericUpDown_18.Value = 200m;
				this.numericUpDown_17.Value = 200m;
				this.numericUpDown_16.Value = 200m;
				this.numericUpDown_15.Value = 200m;
				this.numericUpDown_24.Value = 30m;
				this.numericUpDown_27.Value = 50m;
				this.numericUpDown_28.Value = 20m;
				this.numericUpDown_23.Value = 200m;
				this.numericUpDown_25.Value = 800m;
			}
			((byte*)ptr)[1] = ((string_11 == MainForm.getString_0(107391207)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.numericUpDown_14.Value = 100m;
				this.numericUpDown_10.Value = 200m;
				this.numericUpDown_13.Value = 100m;
				this.numericUpDown_12.Value = 100m;
				this.numericUpDown_11.Value = 200m;
				this.numericUpDown_22.Value = 60m;
				this.numericUpDown_20.Value = 40m;
				this.numericUpDown_21.Value = 20m;
				this.numericUpDown_19.Value = 3m;
				this.numericUpDown_18.Value = 200m;
				this.numericUpDown_17.Value = 200m;
				this.numericUpDown_16.Value = 200m;
				this.numericUpDown_15.Value = 200m;
				this.numericUpDown_24.Value = 30m;
				this.numericUpDown_27.Value = 50m;
				this.numericUpDown_28.Value = 20m;
				this.numericUpDown_23.Value = 200m;
				this.numericUpDown_25.Value = 400m;
			}
			Class255.class105_0.method_1();
		}

		private void method_88()
		{
			foreach (KeyValuePair<NumericUpDown, decimal> keyValuePair in Class255.class105_0.method_2<Dictionary<NumericUpDown, decimal>>(ConfigOptions.CustomSettings))
			{
				keyValuePair.Key.Value = keyValuePair.Value;
			}
		}

		private unsafe void method_89(bool bool_23)
		{
			void* ptr = stackalloc byte[4];
			foreach (object obj in this.groupBox_10.Controls)
			{
				*(byte*)ptr = ((obj is NumericUpDown) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					((NumericUpDown)obj).Enabled = bool_23;
				}
			}
			foreach (object obj2 in this.groupBox_11.Controls)
			{
				((byte*)ptr)[1] = ((obj2 is NumericUpDown) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					((NumericUpDown)obj2).Enabled = bool_23;
				}
			}
			foreach (object obj3 in this.groupBox_12.Controls)
			{
				((byte*)ptr)[2] = ((obj3 is NumericUpDown) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((NumericUpDown)obj3).Enabled = bool_23;
				}
			}
			foreach (object obj4 in this.groupBox_13.Controls)
			{
				((byte*)ptr)[3] = ((obj4 is NumericUpDown) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 3) != 0)
				{
					((NumericUpDown)obj4).Enabled = bool_23;
				}
			}
		}

		private void method_90()
		{
			Dictionary<NumericUpDown, decimal> dictionary = new Dictionary<NumericUpDown, decimal>();
			dictionary.Add(this.numericUpDown_14, Class255.class105_0.method_6(ConfigOptions.ChangeStashTab));
			dictionary.Add(this.numericUpDown_10, Class255.class105_0.method_6(ConfigOptions.LoadStashTab));
			dictionary.Add(this.numericUpDown_13, Class255.class105_0.method_6(ConfigOptions.ClickItemFromStash));
			dictionary.Add(this.numericUpDown_12, Class255.class105_0.method_6(ConfigOptions.ClickItemFromInventory));
			dictionary.Add(this.numericUpDown_11, Class255.class105_0.method_6(ConfigOptions.ClipboardTiming));
			dictionary.Add(this.numericUpDown_22, Class255.class105_0.method_6(ConfigOptions.TimeBeforeSaleExpires));
			dictionary.Add(this.numericUpDown_20, Class255.class105_0.method_6(ConfigOptions.MaxTradeTime));
			dictionary.Add(this.numericUpDown_21, Class255.class105_0.method_6(ConfigOptions.MaxTimeAcceptTrade));
			dictionary.Add(this.numericUpDown_19, Class255.class105_0.method_6(ConfigOptions.IntervalBetweenTrades));
			dictionary.Add(this.numericUpDown_18, Class255.class105_0.method_6(ConfigOptions.PartyInvite));
			dictionary.Add(this.numericUpDown_17, Class255.class105_0.method_6(ConfigOptions.PartyKick));
			dictionary.Add(this.numericUpDown_16, Class255.class105_0.method_6(ConfigOptions.AcceptDeclineRequest));
			dictionary.Add(this.numericUpDown_15, Class255.class105_0.method_6(ConfigOptions.Whisper));
			dictionary.Add(this.numericUpDown_24, Class255.class105_0.method_6(ConfigOptions.TimeBeforeBuyInviteExpires));
			dictionary.Add(this.numericUpDown_23, Class255.class105_0.method_6(ConfigOptions.BuyAcceptDeclineRequest));
			dictionary.Add(this.numericUpDown_25, Class255.class105_0.method_6(ConfigOptions.SetNote));
			Class255.class105_0.method_9(ConfigOptions.CustomSettings, dictionary, true);
		}

		private void button_61_Click(object sender, EventArgs e)
		{
			if (File.Exists(MainForm.getString_0(107391230)))
			{
				Process.Start(MainForm.getString_0(107391230));
			}
			else
			{
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107391177));
			}
		}

		private void button_60_Click(object sender, EventArgs e)
		{
			if (File.Exists(MainForm.getString_0(107391120)))
			{
				Process.Start(MainForm.getString_0(107391120));
			}
			else
			{
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107391131));
			}
		}

		private unsafe void button_15_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[6];
			string text = Interaction.InputBox(MainForm.getString_0(107391070), MainForm.getString_0(107391025), MainForm.getString_0(107396269), -1, -1);
			List<string> list = new List<string>(Class255.class105_0.method_8<string>(ConfigOptions.StashProfiles));
			((byte*)ptr)[4] = (list.Contains(text) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				list.Add(text);
				this.comboBox_19.Items.Add(text);
				this.comboBox_19.SelectedItem = text;
				*(int*)ptr = this.comboBox_19.SelectedIndex;
				Dictionary<int, List<int>> stashProfileTabs = Class255.StashProfileTabs;
				((byte*)ptr)[5] = ((!stashProfileTabs.ContainsKey(*(int*)ptr)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) != 0)
				{
					stashProfileTabs.Add(*(int*)ptr, new List<int>());
				}
				Class255.class105_0.method_9(ConfigOptions.StashProfiles, list, false);
				Class255.class105_0.method_9(ConfigOptions.StashProfileSelected, *(int*)ptr, false);
				Class255.class105_0.method_9(ConfigOptions.StashProfileTabs, stashProfileTabs, false);
				Class255.class105_0.method_1();
			}
		}

		private void button_13_Click(object sender, EventArgs e)
		{
			string value = Interaction.InputBox(MainForm.getString_0(107391516), MainForm.getString_0(107391435), MainForm.getString_0(107396269), -1, -1);
			List<string> list = new List<string>(Class255.class105_0.method_8<string>(ConfigOptions.StashProfiles));
			list[Class255.class105_0.method_5(ConfigOptions.StashProfileSelected)] = value;
			this.comboBox_19.Items.Clear();
			ComboBox.ObjectCollection items = this.comboBox_19.Items;
			object[] items2 = list.ToArray();
			items.AddRange(items2);
			Class255.class105_0.method_9(ConfigOptions.StashProfiles, list, true);
		}

		private unsafe void button_14_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[5];
			List<string> list = new List<string>(Class255.class105_0.method_8<string>(ConfigOptions.StashProfiles));
			*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.StashProfileSelected);
			((byte*)ptr)[4] = ((list.Count - 1 >= *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				list.RemoveAt(*(int*)ptr);
			}
			this.comboBox_19.Items.RemoveAt(*(int*)ptr);
			Class255.class105_0.method_9(ConfigOptions.StashProfiles, list, true);
		}

		private unsafe void comboBox_19_SelectedIndexChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[10];
			*(int*)ptr = this.comboBox_19.SelectedIndex;
			this.method_91(*(int*)ptr != 0);
			Class255.class105_0.method_9(ConfigOptions.StashProfileSelected, *(int*)ptr, true);
			this.fastObjectListView_2.ClearObjects();
			Dictionary<int, List<int>> stashProfileTabs = Class255.StashProfileTabs;
			((byte*)ptr)[8] = ((!stashProfileTabs.ContainsKey(*(int*)ptr)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				foreach (int num in stashProfileTabs[*(int*)ptr])
				{
					*(int*)((byte*)ptr + 4) = num;
					JsonTab jsonTab = Stashes.smethod_11(*(int*)((byte*)ptr + 4));
					((byte*)ptr)[9] = ((jsonTab == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) == 0)
					{
						this.fastObjectListView_2.AddObject(jsonTab);
					}
				}
			}
		}

		private unsafe void button_17_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[9];
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = MainForm.getString_0(107391446);
			folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			DialogResult dialogResult = folderBrowserDialog.ShowDialog();
			((byte*)ptr)[4] = 0;
			((byte*)ptr)[5] = ((dialogResult == DialogResult.OK) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath);
				string[] array = files;
				*(int*)ptr = 0;
				while (*(int*)ptr < array.Length)
				{
					string text = array[*(int*)ptr];
					if (!text.Contains(MainForm.getString_0(107391333)) || !text.Contains(MainForm.getString_0(107391312)))
					{
						*(int*)ptr = *(int*)ptr + 1;
					}
					else
					{
						((byte*)ptr)[6] = (text.Contains(MainForm.getString_0(107394221)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							this.comboBox_66.SelectedItem = MainForm.getString_0(107394221);
						}
						else
						{
							((byte*)ptr)[7] = (text.Contains(MainForm.getString_0(107391303)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 7) != 0)
							{
								this.comboBox_66.SelectedItem = MainForm.getString_0(107394565);
							}
							else
							{
								this.comboBox_66.SelectedItem = MainForm.getString_0(107394917);
							}
						}
						FolderBrowserDialog folderBrowserDialog2 = folderBrowserDialog;
						folderBrowserDialog2.SelectedPath += MainForm.getString_0(107391298);
						((byte*)ptr)[4] = 1;
						this.textBox_8.Text = folderBrowserDialog.SelectedPath;
						Class255.class105_0.method_9(ConfigOptions.GamePath, folderBrowserDialog.SelectedPath, false);
						Class255.class105_0.method_9(ConfigOptions.GameClient, this.comboBox_66.smethod_1(), true);
						this.method_100();
						this.method_53();
						this.method_51();
						IL_1B1:
						((byte*)ptr)[8] = ((*(sbyte*)((byte*)ptr + 4) == 0) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							MessageBox.Show(MainForm.getString_0(107391325), MainForm.getString_0(107390639), MessageBoxButtons.OK, MessageBoxIcon.Hand);
							goto IL_1E9;
						}
						goto IL_1E9;
					}
				}
				goto IL_1B1;
			}
			IL_1E9:
			folderBrowserDialog.Dispose();
		}

		public bool PastFlipUpdatingTime
		{
			get
			{
				bool flag;
				if (Class255.class105_0.method_4(ConfigOptions.FlippingEnabled))
				{
					flag = !Class255.FlippingList.Any(new Func<FlippingListItem, bool>(MainForm.<>c.<>9.method_130));
				}
				else
				{
					flag = true;
				}
				return !flag && (this.NextFlipUpdateTime.Subtract(DateTime.Now).TotalMinutes < 0.0 || this.NextFlipUpdateTime == default(DateTime));
			}
		}

		private void method_91(bool bool_23)
		{
			this.button_14.Enabled = bool_23;
			this.button_13.Enabled = bool_23;
			this.button_16.Enabled = bool_23;
		}

		[DebuggerStepThrough]
		public void button_18_Click(object sender, EventArgs e)
		{
			MainForm.Class76 @class = new MainForm.Class76();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class76>(ref @class);
		}

		public static Bitmap smethod_2(int int_7, int int_8, int int_9, int int_10, string string_11 = "")
		{
			Rectangle rectangle = new Rectangle(int_7, int_8, int_9, int_10);
			Bitmap result;
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				result = null;
			}
			else
			{
				Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format32bppRgb);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
				}
				result = bitmap;
			}
			return result;
		}

		private void method_92(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.button_52.PerformClick();
			}
		}

		private void comboBox_18_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.button_12.PerformClick();
			}
		}

		[DebuggerStepThrough]
		private void timer_2_Tick(object sender, EventArgs e)
		{
			MainForm.Class77 @class = new MainForm.Class77();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class77>(ref @class);
		}

		private void comboBox_1_SelectedValueChanged(object sender, EventArgs e)
		{
			Class255.class105_0.method_9(ConfigOptions.League, this.comboBox_1.smethod_1(), true);
			this.method_86();
		}

		private unsafe void method_93()
		{
			void* ptr = stackalloc byte[11];
			List<TradeFetchId> list = new List<TradeFetchId>();
			((byte*)ptr)[8] = ((!this.list_11.Any<TradeFetchId>()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				list = this.list_11.smethod_0(10).ToList<TradeFetchId>();
				this.list_11.RemoveRange(this.list_11.Count - list.Count, list.Count);
				Stream stream = Web.smethod_9(list.Select(new Func<TradeFetchId, string>(MainForm.<>c.<>9.method_132)), list.First<TradeFetchId>().Query, false);
				((byte*)ptr)[9] = ((stream == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) == 0)
				{
					using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
					{
						*(int*)ptr = 0;
						Class276 @class = JsonConvert.DeserializeObject<Class276>(streamReader.ReadToEnd());
						using (List<FetchTradeResult>.Enumerator enumerator = @class.result.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MainForm.Class78 class2 = new MainForm.Class78();
								class2.fetchTradeResult_0 = enumerator.Current;
								MainForm.Class79 class3 = new MainForm.Class79();
								class3.string_0 = list[*(int*)ptr].Query;
								*(int*)((byte*)ptr + 4) = 0;
								LiveSearchListItem liveSearchListItem = Class255.LiveSearchList.FirstOrDefault(new Func<LiveSearchListItem, bool>(class3.method_0));
								((byte*)ptr)[10] = ((liveSearchListItem != null) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 10) != 0)
								{
									*(int*)((byte*)ptr + 4) = int.Parse(liveSearchListItem.Priority);
								}
								else
								{
									*(int*)((byte*)ptr + 4) = 1;
								}
								BuyingTradeData buyingTradeData = Class151.smethod_0(class2.fetchTradeResult_0, list[*(int*)ptr].Query, TradeTypes.LiveSearch, *(int*)((byte*)ptr + 4));
								if (buyingTradeData == null || buyingTradeData.CharacterName == this.CharacterName)
								{
									*(int*)ptr = *(int*)ptr + 1;
								}
								else
								{
									this.list_10.RemoveAll(new Predicate<BuyingTradeData>(class2.method_0));
									this.list_10.Add(buyingTradeData);
									*(int*)ptr = *(int*)ptr + 1;
									Class307.smethod_1(ConfigOptions.OnBuyLivesearchHit, MainForm.getString_0(107390646), MainForm.getString_0(107390621), new object[]
									{
										class2.fetchTradeResult_0.item.Name
									});
								}
							}
						}
					}
					this.method_124(TradeTypes.LiveSearch);
				}
			}
		}

		private void method_94()
		{
			Task.Run(new Action(this.method_181));
		}

		private void method_95()
		{
			Task.Run(new Action(this.method_182));
		}

		private Class260 method_96(string string_11)
		{
			Class260 @class = new Class260(string_11);
			@class.OnConnectedEvent += this.method_77;
			@class.OnClosedEvent += this.method_78;
			@class.OnErrorEvent += this.method_79;
			@class.OnTradeFoundEvent += this.method_80;
			return @class;
		}

		private void checkBox_0_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_0.Enabled = this.checkBox_0.Checked;
			this.numericUpDown_1.Enabled = this.checkBox_0.Checked;
		}

		private void checkBox_17_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_3.Enabled = this.checkBox_17.Checked;
			this.numericUpDown_2.Enabled = this.checkBox_17.Checked;
		}

		private DateTime method_97()
		{
			int minutes = new Random().Next((int)Class255.class105_0.method_6(ConfigOptions.ClearIgnoreListMin), (int)Class255.class105_0.method_6(ConfigOptions.ClearIgnoreListMax));
			return DateTime.Now + new TimeSpan(0, minutes, 0);
		}

		private void method_98()
		{
			Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107390580));
			this.list_16.Clear();
			this.dateTime_4 = this.method_97();
		}

		private void method_99()
		{
			this.thread_0 = new Thread(new ThreadStart(this.method_45));
			this.thread_0.SetApartmentState(ApartmentState.STA);
			this.thread_0.IsBackground = true;
			this.thread_0.Start();
		}

		[DebuggerStepThrough]
		private void method_100()
		{
			MainForm.Class81 @class = new MainForm.Class81();
			@class.mainForm_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class81>(ref @class);
		}

		private static string smethod_3()
		{
			return Class120.string_3;
		}

		private unsafe void method_101()
		{
			void* ptr = stackalloc byte[22];
			((DataGridViewComboBoxColumn)this.dataGridView_1.Columns[MainForm.getString_0(107390551)]).Items.Clear();
			*(int*)((byte*)ptr + 4) = 0;
			for (;;)
			{
				((byte*)ptr)[20] = ((*(int*)((byte*)ptr + 4) < Class255.LiveSearchList.Count) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 20) == 0)
				{
					break;
				}
				DataGridViewComboBoxCell.ObjectCollection items = ((DataGridViewComboBoxColumn)this.dataGridView_1.Columns[MainForm.getString_0(107390551)]).Items;
				*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 4) + 1;
				items.Add(((int*)((byte*)ptr + 8))->ToString());
				*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
			}
			*(int*)ptr = Class255.LiveSearchList.Count;
			foreach (object obj in ((IEnumerable)this.dataGridView_1.Rows))
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
				*(int*)((byte*)ptr + 12) = int.Parse(dataGridViewRow.Cells[MainForm.getString_0(107390551)].Value.smethod_10());
				((byte*)ptr)[21] = ((*(int*)((byte*)ptr + 12) > *(int*)ptr) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 21) != 0)
				{
					*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 12) - *(int*)ptr;
					*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) - *(int*)((byte*)ptr + 16);
					dataGridViewRow.Cells[MainForm.getString_0(107390551)].Value = ((int*)((byte*)ptr + 12))->ToString();
					this.method_102(false);
				}
			}
			Class255.class105_0.method_1();
		}

		public void method_102(bool bool_23 = true)
		{
			List<LiveSearchListItem> list = new List<LiveSearchListItem>();
			foreach (object obj in ((IEnumerable)this.dataGridView_1.Rows))
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				list.Add(LiveSearchListItem.SaveFromDataGrid(row));
			}
			Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, bool_23);
		}

		public void method_103(bool bool_23 = true)
		{
			List<BulkBuyingListItem> list = new List<BulkBuyingListItem>();
			foreach (object obj in ((IEnumerable)this.dataGridView_2.Rows))
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				list.Add(BulkBuyingListItem.FromDataGrid(row));
			}
			Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, list, bool_23);
		}

		public void method_104(bool bool_23 = true)
		{
			List<ItemBuyingListItem> list = new List<ItemBuyingListItem>();
			foreach (object obj in ((IEnumerable)this.dataGridView_3.Rows))
			{
				DataGridViewRow row = (DataGridViewRow)obj;
				list.Add(ItemBuyingListItem.FromDataGrid(row));
			}
			Class255.class105_0.method_9(ConfigOptions.ItemBuyingList, list, bool_23);
		}

		private void linkLabel_0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(MainForm.getString_0(107390526));
		}

		private unsafe void button_22_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[37];
			using (WebClient webClient = new WebClient())
			{
				try
				{
					string value = webClient.DownloadString(Class103.PoeNinjaCurrencyUrl);
					PoeNinja poeNinja = JsonConvert.DeserializeObject<PoeNinja>(value, Util.smethod_12());
					List<FlippingListItem> list = new List<FlippingListItem>();
					PoeNinja.Line[] lines = poeNinja.Lines;
					*(int*)((byte*)ptr + 24) = 0;
					while (*(int*)((byte*)ptr + 24) < lines.Length)
					{
						PoeNinja.Line line = lines[*(int*)((byte*)ptr + 24)];
						if (line.Pay != null && line.Receive != null && (line.Pay.GetCurrencyId == 1L || line.Receive.GetCurrencyId == 1L))
						{
							((byte*)ptr)[29] = ((!Class102.string_7.Contains(line.CurrencyTypeName)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 29) == 0)
							{
								((byte*)ptr)[30] = ((line.CurrencyTypeName == MainForm.getString_0(107390973)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 30) == 0)
								{
									*(double*)ptr = line.Receive.Value;
									*(double*)((byte*)ptr + 8) = line.Pay.Value;
									((byte*)ptr)[28] = 0;
									((byte*)ptr)[31] = ((*(double*)((byte*)ptr + 8) < 1.0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 31) != 0)
									{
										((byte*)ptr)[28] = 1;
										*(double*)((byte*)ptr + 8) = 1.0 / *(double*)((byte*)ptr + 8);
									}
									((byte*)ptr)[32] = ((*(double*)ptr < 1.0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 32) != 0)
									{
										*(double*)ptr = 1.0 / *(double*)ptr;
									}
									((byte*)ptr)[33] = (byte)(*(sbyte*)((byte*)ptr + 28));
									if (*(sbyte*)((byte*)ptr + 33) != 0)
									{
										((byte*)ptr)[34] = ((*(double*)((byte*)ptr + 8) > *(double*)ptr) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 34) != 0)
										{
											goto IL_22C;
										}
									}
									else
									{
										((byte*)ptr)[35] = ((*(double*)((byte*)ptr + 8) < *(double*)ptr) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 35) != 0)
										{
											goto IL_22C;
										}
									}
									double num = (*(sbyte*)((byte*)ptr + 28) != 0) ? (*(double*)((byte*)ptr + 8) / *(double*)ptr) : (*(double*)ptr / *(double*)((byte*)ptr + 8));
									*(double*)((byte*)ptr + 16) = Math.Round(1.0 - num, 4) * 100.0;
									((byte*)ptr)[36] = ((*(double*)((byte*)ptr + 16) == 0.0) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 36) == 0)
									{
										list.Add(new FlippingListItem(MainForm.getString_0(107390916), line.CurrencyTypeName)
										{
											ResellMargin = *(double*)((byte*)ptr + 16)
										});
									}
								}
							}
						}
						IL_22C:
						*(int*)((byte*)ptr + 24) = *(int*)((byte*)ptr + 24) + 1;
					}
					Form3 form = new Form3(list);
					form.Show();
				}
				catch (Exception arg)
				{
					MessageBox.Show(string.Format(MainForm.getString_0(107390935), arg));
				}
			}
		}

		private unsafe void method_105()
		{
			void* ptr = stackalloc byte[11];
			Random random = new Random();
			*(int*)ptr = random.Next(1, 4);
			Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107390854) + ((int*)ptr)->ToString());
			((byte*)ptr)[8] = ((*(int*)ptr == 3) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				Win32.smethod_14(this.string_1, false);
				Thread.Sleep(new Random().Next(5000, 15000));
				Win32.smethod_14(this.string_1, false);
				UI.smethod_13(1);
			}
			else
			{
				((byte*)ptr)[9] = ((*(int*)ptr == 2) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					Win32.smethod_14(this.string_2, false);
					Thread.Sleep(new Random().Next(5000, 15000));
					Win32.smethod_14(this.string_2, false);
					UI.smethod_13(1);
				}
				else
				{
					((byte*)ptr)[10] = (UI.smethod_13(1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 10) != 0)
					{
						UI.smethod_80();
						*(int*)((byte*)ptr + 4) = new Random().Next(1, 20);
						Thread.Sleep(*(int*)((byte*)ptr + 4) * 1000);
						UI.smethod_13(1);
					}
				}
			}
		}

		private void timer_3_Tick(object sender, EventArgs e)
		{
			if (Class255.class105_0.method_4(ConfigOptions.FlippingEnabled))
			{
				base.Invoke(new Action(this.method_184));
			}
		}

		public bool PastAFKTime
		{
			get
			{
				return Class255.class105_0.method_4(ConfigOptions.EnableGoAFK) && DateTime.Now >= this.dateTime_0;
			}
		}

		private void button_25_Click(object sender, EventArgs e)
		{
			string text = string.Format(MainForm.getString_0(107390825), Util.smethod_16());
			try
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(Class120.string_1, text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format(MainForm.getString_0(107390836), ex.Message));
			}
		}

		private void button_26_Click(object sender, EventArgs e)
		{
			Process.Start(Environment.CurrentDirectory);
		}

		private unsafe void method_106()
		{
			void* ptr = stackalloc byte[6];
			object obj = this.object_2;
			((byte*)ptr)[4] = 0;
			try
			{
				Monitor.Enter(obj, ref *(bool*)((byte*)ptr + 4));
				((byte*)ptr)[5] = (this.bool_15 ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					this.bool_15 = true;
					*(int*)ptr = new Random().Next(15, 40);
					this.dateTime_5 = DateTime.Now.AddMinutes((double)(*(int*)ptr));
					Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107390763), new object[]
					{
						this.dateTime_5
					});
					Task.Run(new Action(this.method_185));
				}
			}
			finally
			{
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					Monitor.Exit(obj);
				}
			}
		}

		private void checkBox_52_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_62.Enabled = this.checkBox_52.Checked;
			this.fastObjectListView_17.Enabled = this.checkBox_52.Checked;
		}

		private string method_107(string string_11)
		{
			return string_11.Replace(MainForm.getString_0(107390210), MainForm.getString_0(107396269)).Replace(MainForm.getString_0(107390233), MainForm.getString_0(107396269)).Replace(MainForm.getString_0(107390192), MainForm.getString_0(107396306));
		}

		private unsafe bool method_108(string string_11)
		{
			void* ptr = stackalloc byte[3];
			Match match = MainForm.regex_5.Match(string_11);
			*(byte*)ptr = ((!match.Success) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				((byte*)ptr)[1] = 0;
			}
			else
			{
				((byte*)ptr)[2] = ((!string_11.Contains(string.Format(MainForm.getString_0(107390187), Class255.class105_0.method_3(ConfigOptions.League)))) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					((byte*)ptr)[1] = 0;
				}
				else if (!string_11.Contains(MainForm.getString_0(107390174)) && !string_11.Contains(MainForm.getString_0(107390125)))
				{
					((byte*)ptr)[1] = 0;
				}
				else
				{
					((byte*)ptr)[1] = 1;
				}
			}
			return *(sbyte*)((byte*)ptr + 1) != 0;
		}

		private unsafe void checkBox_22_CheckedChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.form1_0 == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (this.checkBox_22.Checked ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					if (ProcessHelper.smethod_9() && ProcessHelper.smethod_7(ProcessHelper.int_0))
					{
						this.form1_0.method_1();
					}
				}
				else
				{
					this.form1_0.method_2();
				}
			}
		}

		private void tabControl_6_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}

		private void button_29_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.button_29.Enabled = false;
				this.method_130(new ThreadStart(this.method_186));
			}
		}

		public void method_109()
		{
			base.Invoke(new Action(this.method_187));
			this.method_131();
		}

		private void comboBox_31_SelectedIndexChanged(object sender, EventArgs e)
		{
			string a = this.comboBox_31.smethod_1();
			this.comboBox_33.Enabled = (a != MainForm.getString_0(107390144) && a != MainForm.getString_0(107390139));
			bool visible = a == MainForm.getString_0(107390094);
			this.numericUpDown_59.Visible = visible;
			this.comboBox_67.Visible = visible;
		}

		private unsafe void method_110()
		{
			void* ptr = stackalloc byte[9];
			*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.UpdateMinFlippingPrices);
			*(int*)((byte*)ptr + 4) = Class255.class105_0.method_5(ConfigOptions.UpdateMaxFlippingPrices);
			((byte*)ptr)[8] = ((*(int*)((byte*)ptr + 4) <= *(int*)ptr) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				*(int*)((byte*)ptr + 4) = *(int*)ptr + 5;
			}
			this.int_3 = this.random_0.Next(*(int*)ptr, *(int*)((byte*)ptr + 4));
			Class255.class105_0.method_9(ConfigOptions.NextFlipUpdateTime, DateTime.Now.AddMinutes((double)this.int_3), true);
			this.timer_3_Tick(null, null);
		}

		private void method_111()
		{
			if (Class255.class105_0.method_4(ConfigOptions.EnableLogout) && DateTime.Now >= this.dateTime_2 && this.timer_2.Enabled)
			{
				try
				{
					if (Class255.class105_0.method_4(ConfigOptions.EnableLogin))
					{
						Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107390101), new object[]
						{
							this.dateTime_3
						});
					}
					else
					{
						Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107390036));
					}
					this.timer_2.Enabled = false;
					this.bool_22 = true;
					UI.smethod_85();
					Thread.Sleep(10000);
					this.gameProcessState_0 = GameProcessState.Login;
					this.expectedState_0 = ExpectedState.Running;
					UI.smethod_1();
				}
				catch
				{
				}
			}
		}

		private void method_112()
		{
			if (Class255.class105_0.method_4(ConfigOptions.EnableLogin) && DateTime.Now >= this.dateTime_3 && !this.bool_10)
			{
				this.bool_10 = true;
				Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107390487), new object[]
				{
					this.dateTime_2
				});
				this.method_63();
			}
		}

		private void method_113(object sender, EventArgs e)
		{
			this.numericUpDown_43.Enabled = this.checkBox_38.Checked;
			this.comboBox_49.Enabled = this.checkBox_38.Checked;
		}

		private void checkBox_29_CheckedChanged(object sender, EventArgs e)
		{
			this.checkBox_28.Enabled = this.checkBox_29.Checked;
		}

		private bool PastItemBuyingTime
		{
			get
			{
				if (this.dateTime_6 < DateTime.Now && this.ItemBuyFeatureEnabled)
				{
					if (Class255.ItemBuyingList.Any(new Func<ItemBuyingListItem, bool>(MainForm.<>c.<>9.method_138)))
					{
						return !this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_139));
					}
				}
				return false;
			}
		}

		private bool PastBulkBuyingTime
		{
			get
			{
				if (this.dateTime_7 < DateTime.Now && this.BulkBuyFeatureEnabled)
				{
					if (Class255.BulkBuyingList.Any(new Func<BulkBuyingListItem, bool>(MainForm.<>c.<>9.method_140)))
					{
						return !this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_141));
					}
				}
				return false;
			}
		}

		private unsafe void method_114()
		{
			void* ptr = stackalloc byte[12];
			bool flag;
			if (this.ItemBuyFeatureEnabled)
			{
				if (!this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_142)))
				{
					flag = this.list_9.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_143));
					goto IL_67;
				}
			}
			flag = true;
			IL_67:
			if (!flag)
			{
				foreach (ItemBuyingListItem itemBuyingListItem in Class255.ItemBuyingList)
				{
					itemBuyingListItem.FetchList = null;
				}
				this.int_4 = 0;
				*(int*)ptr = (int)Class255.class105_0.method_6(ConfigOptions.ItemBuyingMinTime);
				*(int*)((byte*)ptr + 4) = (int)Class255.class105_0.method_6(ConfigOptions.ItemBuyingMaxTime);
				*(int*)((byte*)ptr + 8) = this.random_0.Next(*(int*)ptr, *(int*)((byte*)ptr + 4));
				this.dateTime_6 = DateTime.Now.AddMinutes((double)(*(int*)((byte*)ptr + 8)));
				Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107390422), new object[]
				{
					this.dateTime_6
				});
			}
		}

		private unsafe void method_115()
		{
			void* ptr = stackalloc byte[12];
			bool flag;
			if (this.BulkBuyFeatureEnabled)
			{
				if (!this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_144)))
				{
					flag = this.list_9.Any(new Func<Order, bool>(MainForm.<>c.<>9.method_145));
					goto IL_67;
				}
			}
			flag = true;
			IL_67:
			if (!flag)
			{
				foreach (BulkBuyingListItem bulkBuyingListItem in Class255.BulkBuyingList)
				{
					bulkBuyingListItem.FetchList = null;
				}
				this.int_5 = 0;
				*(int*)ptr = (int)Class255.class105_0.method_6(ConfigOptions.BulkBuyingMinTime);
				*(int*)((byte*)ptr + 4) = (int)Class255.class105_0.method_6(ConfigOptions.BulkBuyingMaxTime);
				*(int*)((byte*)ptr + 8) = this.random_0.Next(*(int*)ptr, *(int*)((byte*)ptr + 4));
				this.dateTime_7 = DateTime.Now.AddMinutes((double)(*(int*)((byte*)ptr + 8)));
				Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107390345), new object[]
				{
					this.dateTime_7
				});
			}
		}

		[DebuggerStepThrough]
		private void button_33_Click(object sender, EventArgs e)
		{
			MainForm.Class83 @class = new MainForm.Class83();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class83>(ref @class);
		}

		private unsafe void comboBox_11_SelectedIndexChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_10.smethod_1() != MainForm.getString_0(107390332)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.pictureBox_2.Visible = false;
			}
			else
			{
				((byte*)ptr)[1] = ((((Class253)this.comboBox_11.SelectedItem).Height >= 1050) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.pictureBox_2.Visible = true;
				}
				else
				{
					this.pictureBox_2.Visible = false;
				}
			}
		}

		public void method_116()
		{
			base.Invoke(new Action(this.method_188));
		}

		public void method_117()
		{
			base.Invoke(new Action(this.method_189));
		}

		private void button_38_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.method_12(false);
				this.method_130(new ThreadStart(this.method_11));
			}
		}

		private void button_47_Click(object sender, EventArgs e)
		{
			Blackout blackout = new Blackout(this);
			this.method_116();
			if (!blackout.method_16())
			{
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107390279));
			}
			else
			{
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107390242));
			}
		}

		private unsafe void button_49_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = ((this.button_49.Text == MainForm.getString_0(107389713)) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.method_118();
			}
			else
			{
				((byte*)ptr)[1] = ((this.genum0_0 > MainForm.GEnum0.const_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					Class181.smethod_5(Enum11.const_2, MainForm.getString_0(107389672));
				}
				else
				{
					((byte*)ptr)[2] = ((!Class255.MuleStashIds.Any<int>()) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_5(Enum11.const_2, MainForm.getString_0(107389607));
					}
					else
					{
						this.button_49.Text = MainForm.getString_0(107389713);
						this.button_49.Enabled = false;
						ProcessHelper.smethod_9();
						this.method_116();
						this.method_58(false, MainForm.GEnum1.const_1, false);
					}
				}
			}
		}

		public void method_118()
		{
			base.Invoke(new Action(this.method_190));
		}

		private void checkBox_58_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_66.Enabled = this.checkBox_58.Checked;
			this.numericUpDown_65.Enabled = this.checkBox_58.Checked;
		}

		public void method_119()
		{
			base.Invoke(new Action(this.method_191));
		}

		private void button_56_Click(object sender, EventArgs e)
		{
			this.method_119();
		}

		private void button_9_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.method_12(false);
				this.method_130(new ThreadStart(this.method_120));
			}
		}

		private void method_120()
		{
			base.Invoke(new Action(this.method_192));
		}

		public void method_121()
		{
			base.Invoke(new Action(this.method_193));
		}

		public void method_122(int int_7)
		{
			MainForm.Class84 @class = new MainForm.Class84();
			@class.mainForm_0 = this;
			@class.int_0 = int_7;
			this.method_121();
			base.Invoke(new Action(@class.method_0));
		}

		public void method_123(int int_7 = 1)
		{
			MainForm.Class85 @class = new MainForm.Class85();
			@class.mainForm_0 = this;
			@class.int_0 = int_7;
			base.Invoke(new Action(@class.method_0));
		}

		public void method_124(TradeTypes tradeTypes_0)
		{
			MainForm.Class86 @class = new MainForm.Class86();
			@class.tradeTypes_0 = tradeTypes_0;
			@class.mainForm_0 = this;
			base.Invoke(new Action(@class.method_0));
		}

		private void button_62_Click(object sender, EventArgs e)
		{
			this.method_125(-1);
		}

		private void button_63_Click(object sender, EventArgs e)
		{
			this.method_125(1);
		}

		private void method_125(int int_7)
		{
			object selectedItem = this.listBox_0.SelectedItem;
			if (selectedItem != null && this.listBox_0.SelectedIndex >= 0)
			{
				int num = this.listBox_0.SelectedIndex + int_7;
				if (num >= 0 && num < this.listBox_0.Items.Count)
				{
					this.listBox_0.Items.Remove(selectedItem);
					this.listBox_0.Items.Insert(num, selectedItem);
					this.listBox_0.SetSelected(num, true);
					Class255.class105_0.method_1();
				}
			}
		}

		private unsafe BuyingTradeData method_126()
		{
			void* ptr = stackalloc byte[6];
			Class255.class105_0.method_8<string>(ConfigOptions.BuyingPriority);
			*(int*)ptr = 0;
			BuyingTradeData buyingTradeData;
			for (;;)
			{
				((byte*)ptr)[5] = ((*(int*)ptr < Class255.BuyingPriorityList.Count) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					break;
				}
				MainForm.Class87 @class = new MainForm.Class87();
				@class.tradeTypes_0 = this.method_127(*(int*)ptr);
				buyingTradeData = this.list_10.FirstOrDefault(new Func<BuyingTradeData, bool>(@class.method_0));
				((byte*)ptr)[4] = ((buyingTradeData != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					goto IL_73;
				}
				*(int*)ptr = *(int*)ptr + 1;
			}
			return null;
			IL_73:
			return buyingTradeData;
		}

		private TradeTypes method_127(int int_7)
		{
			List<string> list = Class255.class105_0.method_8<string>(ConfigOptions.BuyingPriority);
			TradeTypes tradeTypes;
			TradeTypes result;
			if (Enum.TryParse<TradeTypes>(list[int_7].Replace(MainForm.getString_0(107396306), MainForm.getString_0(107396269)), out tradeTypes))
			{
				result = tradeTypes;
			}
			else
			{
				result = TradeTypes.LiveSearch;
			}
			return result;
		}

		private void checkBox_46_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_46.Checked;
			this.textBox_10.Enabled = @checked;
			this.textBox_9.Enabled = @checked;
			this.button_19.Enabled = @checked;
			this.dataGridView_1.Enabled = @checked;
		}

		private void checkBox_45_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_45.Checked;
			this.numericUpDown_34.Enabled = @checked;
			this.numericUpDown_33.Enabled = @checked;
			this.textBox_13.Enabled = @checked;
			this.textBox_12.Enabled = @checked;
			this.button_32.Enabled = @checked;
			this.dataGridView_3.Enabled = @checked;
		}

		private void checkBox_47_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_47.Checked;
			this.numericUpDown_32.Enabled = @checked;
			this.numericUpDown_31.Enabled = @checked;
			this.button_31.Enabled = @checked;
			this.dataGridView_2.Enabled = @checked;
		}

		private bool LiveSearchFeatureEnabled
		{
			get
			{
				bool result;
				if (Class255.LiveSearchEnabled)
				{
					result = Class255.LiveSearchList.Any(new Func<LiveSearchListItem, bool>(MainForm.<>c.<>9.method_146));
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		private bool ItemBuyFeatureEnabled
		{
			get
			{
				bool result;
				if (Class255.ItemBuyEnabled)
				{
					result = Class255.ItemBuyingList.Any(new Func<ItemBuyingListItem, bool>(MainForm.<>c.<>9.method_147));
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		private bool BulkBuyFeatureEnabled
		{
			get
			{
				bool result;
				if (Class255.BulkBuyEnabled)
				{
					result = Class255.BulkBuyingList.Any(new Func<BulkBuyingListItem, bool>(MainForm.<>c.<>9.method_148));
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		public void method_128()
		{
			if (this.genum0_0 == MainForm.GEnum0.const_2 || this.enum8_0 <= Enum8.const_0)
			{
				base.Invoke(new Action(this.method_194));
			}
		}

		private void method_129()
		{
			base.Invoke(new Action(this.method_195));
		}

		private void method_130(ThreadStart threadStart_0)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				ProcessHelper.smethod_9();
				this.enum8_0 = Enum8.const_0;
				this.method_129();
				this.thread_5 = new Thread(threadStart_0);
				this.thread_5.IsBackground = true;
				this.thread_5.SetApartmentState(ApartmentState.STA);
				this.thread_5.Start();
			}
		}

		private unsafe void method_131()
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.enum8_0 == Enum8.const_1) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				this.enum8_0 = Enum8.const_1;
				base.Invoke(new Action(this.method_196));
				((byte*)ptr)[1] = ((this.thread_5 != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.thread_5.Abort();
					this.thread_5 = null;
				}
			}
		}

		private void button_67_Click(object sender, EventArgs e)
		{
			if (this.bool_20)
			{
				this.debugPanel_0.Hide();
			}
			else
			{
				this.debugPanel_0.Show();
			}
			this.bool_20 = !this.bool_20;
		}

		private unsafe void MainForm_Move(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[9];
			((byte*)ptr)[8] = ((this.debugPanel_0 == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) == 0)
			{
				*(int*)ptr = base.Location.X - this.debugPanel_0.Width + 8;
				Point location = base.Location;
				*(int*)((byte*)ptr + 4) = location.Y + base.Height - this.debugPanel_0.Height + -7;
				this.debugPanel_0.Location = new Point(*(int*)ptr, *(int*)((byte*)ptr + 4));
			}
		}

		private void button_65_Click(object sender, EventArgs e)
		{
			this.method_128();
		}

		[DebuggerStepThrough]
		private void button_70_Click(object sender, EventArgs e)
		{
			MainForm.Class88 @class = new MainForm.Class88();
			@class.mainForm_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class88>(ref @class);
		}

		public int method_132()
		{
			MainForm.Class89 @class = new MainForm.Class89();
			@class.mainForm_0 = this;
			@class.int_0 = 0;
			base.Invoke(new Action(@class.method_0));
			return @class.int_0;
		}

		public int method_133()
		{
			MainForm.Class90 @class = new MainForm.Class90();
			@class.mainForm_0 = this;
			@class.int_0 = 0;
			base.Invoke(new Action(@class.method_0));
			return @class.int_0;
		}

		public int method_134()
		{
			MainForm.Class91 @class = new MainForm.Class91();
			@class.mainForm_0 = this;
			@class.int_0 = 0;
			base.Invoke(new Action(@class.method_0));
			return @class.int_0;
		}

		private void button_72_Click(object sender, EventArgs e)
		{
			Struct2 @struct;
			UI.GetWindowRect(UI.intptr_0, out @struct);
			this.method_116();
			if (!UI.smethod_26())
			{
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107389618));
			}
			else
			{
				Class255.class105_0.method_9(ConfigOptions.GameLocation, new Point(@struct.int_0, @struct.int_1), true);
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107389537));
			}
		}

		public unsafe int method_135()
		{
			void* ptr = stackalloc byte[5];
			((byte*)ptr)[4] = ((this.genum0_0 == MainForm.GEnum0.const_2) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				*(int*)ptr = (int)DateTime.Now.Subtract(this.dateTime_8).TotalSeconds;
			}
			else
			{
				*(int*)ptr = 0;
			}
			return *(int*)ptr;
		}

		private void button_73_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.button_73.Enabled = false;
				this.method_130(new ThreadStart(this.method_198));
			}
		}

		private void button_77_Click(object sender, EventArgs e)
		{
			MainForm.Class92 @class = new MainForm.Class92();
			@class.mainForm_0 = this;
			if (this.enum8_0 != Enum8.const_0)
			{
				this.button_77.Enabled = false;
				@class.jsonTab_0 = (JsonTab)this.comboBox_63.SelectedItem;
				this.method_130(new ThreadStart(@class.method_0));
			}
		}

		public void method_136(string string_11)
		{
			MainForm.Class93 @class = new MainForm.Class93();
			@class.mainForm_0 = this;
			@class.string_0 = string_11;
			base.Invoke(new Action(@class.method_0));
		}

		private void button_78_Click(object sender, EventArgs e)
		{
			Point location = Class255.class105_0.method_2<Point>(ConfigOptions.PopoutChatLocation);
			try
			{
				this.form2_0.Show();
				this.form2_0.Location = location;
			}
			catch (ObjectDisposedException)
			{
				this.form2_0 = new Form2();
				this.form2_0.Show();
				this.form2_0.Location = location;
			}
			this.form2_0.method_0();
		}

		private void method_137(string string_11)
		{
			Class181.smethod_10(this.richTextBox_1, string_11);
			Class181.smethod_10(this.form1_0.ChatBox, string_11);
			this.form1_0.Invoke(new Action(this.method_199));
			try
			{
				Class181.smethod_10(this.form2_0.richTextBox_0, string_11);
			}
			catch
			{
			}
		}

		private void button_79_Click(object sender, EventArgs e)
		{
			Class255.class105_0.method_9(ConfigOptions.NextFlipUpdateTime, default(DateTime), true);
			this.bool_18 = false;
		}

		private void timer_4_Tick(object sender, EventArgs e)
		{
			try
			{
				string text = Web.smethod_0(Class103.string_7);
				if (!text.Contains(MainForm.getString_0(107394813)))
				{
					this.list_17 = JsonConvert.DeserializeObject<List<string>>(text);
				}
			}
			catch
			{
			}
		}

		private bool method_138()
		{
			return Directory.Exists(Class255.class105_0.method_3(ConfigOptions.GamePath)) && Directory.Exists(Class255.class105_0.method_3(ConfigOptions.GamePath) + MainForm.getString_0(107394987)) && File.Exists(Class120.PoELogFile);
		}

		private unsafe void button_80_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[10];
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			List<DivinationCardListItem> list = new List<DivinationCardListItem>();
			PoeNinja poeNinja;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string value = webClient.DownloadString(Class103.PoeNinjaDivCardUrl);
					poeNinja = JsonConvert.DeserializeObject<PoeNinja>(value, Util.smethod_12());
				}
			}
			catch
			{
				MessageBox.Show(MainForm.getString_0(107389504));
				return;
			}
			foreach (List<JsonItem> source in Stashes.Items.Values)
			{
				foreach (JsonItem jsonItem in source.Where(new Func<JsonItem, bool>(MainForm.<>c.<>9.method_149)))
				{
					((byte*)ptr)[8] = ((!dictionary.ContainsKey(jsonItem.typeLine)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						dictionary.Add(jsonItem.typeLine, 0);
					}
					Dictionary<string, int> dictionary2 = dictionary;
					string typeLine = jsonItem.typeLine;
					dictionary2[typeLine] += jsonItem.stack;
				}
			}
			using (Dictionary<string, int>.Enumerator enumerator3 = dictionary.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					MainForm.Class94 @class = new MainForm.Class94();
					@class.keyValuePair_0 = enumerator3.Current;
					*(double*)ptr = 0.0;
					PoeNinja.Line line = poeNinja.Lines.FirstOrDefault(new Func<PoeNinja.Line, bool>(@class.method_0));
					((byte*)ptr)[9] = ((line != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 9) != 0)
					{
						*(double*)ptr = line.ChaosValue;
					}
					list.Add(new DivinationCardListItem(@class.keyValuePair_0.Key, @class.keyValuePair_0.Value, *(double*)ptr));
				}
			}
			Form0 form = new Form0(list.OrderByDescending(new Func<DivinationCardListItem, double>(MainForm.<>c.<>9.method_150)));
			form.Show();
		}

		private void checkBox_38_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_43.Enabled = this.checkBox_38.Checked;
		}

		private void button_82_Click(object sender, EventArgs e)
		{
			this.method_139(-1);
		}

		private void button_83_Click(object sender, EventArgs e)
		{
			this.method_139(1);
		}

		private void method_139(int int_7)
		{
			object selectedItem = this.listBox_1.SelectedItem;
			if (selectedItem != null && this.listBox_1.SelectedIndex >= 0)
			{
				int num = this.listBox_1.SelectedIndex + int_7;
				if (num >= 0 && num < this.listBox_1.Items.Count)
				{
					this.listBox_1.Items.Remove(selectedItem);
					this.listBox_1.Items.Insert(num, selectedItem);
					this.listBox_1.SetSelected(num, true);
					Class255.class105_0.method_1();
				}
			}
		}

		public Order method_140()
		{
			Dictionary<string, Func<Order>> dictionary = new Dictionary<string, Func<Order>>
			{
				{
					MainForm.getString_0(107389975),
					new Func<Order>(this.method_141)
				},
				{
					MainForm.getString_0(107396102),
					new Func<Order>(this.method_142)
				},
				{
					MainForm.getString_0(107389930),
					new Func<Order>(this.method_143)
				}
			};
			foreach (string key in Class255.TradingPriorityList)
			{
				Order order = dictionary[key]();
				if (order != null)
				{
					return order;
				}
			}
			return null;
		}

		private Order method_141()
		{
			Order result;
			if (!this.list_7.Any<Order>())
			{
				result = null;
			}
			else
			{
				result = this.list_7.LastOrDefault(new Func<Order, bool>(MainForm.<>c.<>9.method_151));
			}
			return result;
		}

		private Order method_142()
		{
			Order result;
			if (!this.list_7.Any<Order>())
			{
				result = null;
			}
			else
			{
				result = this.list_7.LastOrDefault(new Func<Order, bool>(MainForm.<>c.<>9.method_152));
			}
			return result;
		}

		private unsafe Order method_143()
		{
			void* ptr = stackalloc byte[8];
			((byte*)ptr)[4] = ((!this.list_9.Any<Order>()) ? 1 : 0);
			Order result;
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				result = null;
			}
			else
			{
				TradeTypes tradeTypes = this.method_127(0);
				this.method_127(1);
				this.method_127(2);
				*(int*)ptr = 0;
				for (;;)
				{
					((byte*)ptr)[7] = ((*(int*)ptr < Class255.BuyingPriorityList.Count) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) == 0)
					{
						break;
					}
					MainForm.Class95 @class = new MainForm.Class95();
					@class.tradeTypes_0 = this.method_127(*(int*)ptr);
					((byte*)ptr)[5] = (this.list_9.Any(new Func<Order, bool>(@class.method_0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						goto IL_9B;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				return null;
				IL_9B:
				((byte*)ptr)[6] = ((tradeTypes == TradeTypes.LiveSearch) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					MainForm.Class95 @class;
					result = this.list_9.Where(new Func<Order, bool>(@class.method_1)).OrderByDescending(new Func<Order, DateTime>(MainForm.<>c.<>9.method_153)).First<Order>();
				}
				else
				{
					MainForm.Class95 @class;
					result = this.list_9.Where(new Func<Order, bool>(@class.method_2)).OrderBy(new Func<Order, DateTime>(MainForm.<>c.<>9.method_154)).First<Order>();
				}
			}
			return result;
		}

		private void button_90_Click(object sender, EventArgs e)
		{
			if (Stashes.Tabs == null)
			{
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107389921));
			}
			else
			{
				JsonTab jsonTab = Stashes.smethod_14(MainForm.getString_0(107393328));
				if (jsonTab == null || !Stashes.Items.ContainsKey(jsonTab.i))
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107389860));
				}
				else
				{
					Size clientSize = Class255.class105_0.method_2<Size>(ConfigOptions.CurrencyTabSize);
					Point location = Class255.class105_0.method_2<Point>(ConfigOptions.CurrencyTabLocation);
					try
					{
						this.form4_0.ClientSize = clientSize;
						this.form4_0.Show();
						this.form4_0.Location = location;
					}
					catch
					{
						this.form4_0 = new Form4(jsonTab.i, true);
						this.form4_0.ClientSize = clientSize;
						this.form4_0.Show();
						this.form4_0.Location = location;
					}
					this.form4_0.method_2();
				}
			}
		}

		public void method_144()
		{
			Task.Run(new Action(this.method_200));
		}

		private void checkBox_61_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBox_61.Checked)
			{
				this.textBox_22.Enabled = false;
			}
			else
			{
				this.textBox_22.Enabled = true;
			}
		}

		private void method_145(Button button_101, Action action_0, Action action_1)
		{
			MainForm.Class96 @class = new MainForm.Class96();
			@class.action_0 = action_0;
			@class.action_1 = action_1;
			@class.contextMenuStrip_0 = new ContextMenuStrip();
			@class.contextMenuStrip_0.Items.Add(MainForm.getString_0(107389831));
			@class.contextMenuStrip_0.Items.Add(MainForm.getString_0(107389842));
			@class.contextMenuStrip_0.ItemClicked += @class.method_0;
			@class.contextMenuStrip_0.Show(button_101, new Point(0, button_101.Height));
		}

		private void checkBox_63_CheckedChanged(object sender, EventArgs e)
		{
			this.comboBox_71.Enabled = this.checkBox_63.Checked;
		}

		private void textBox_3_TextChanged(object sender, EventArgs e)
		{
			if (this.textBox_3.Text.Contains(MainForm.getString_0(107396306)))
			{
				this.textBox_3.Text = this.textBox_3.Text.Replace(MainForm.getString_0(107396306), MainForm.getString_0(107396269));
				this.textBox_3.SelectionStart = this.textBox_3.Text.Length;
				this.textBox_3.SelectionLength = 0;
			}
		}

		private unsafe void checkBox_72_CheckedChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (this.checkBox_72.Checked ? 1 : 0);
			this.checkBox_73.Enabled = (*(sbyte*)ptr != 0);
			this.checkBox_71.Enabled = (*(sbyte*)ptr != 0);
			this.checkBox_70.Enabled = (*(sbyte*)ptr != 0);
			this.checkBox_69.Enabled = (*(sbyte*)ptr != 0);
			this.checkBox_68.Enabled = (*(sbyte*)ptr != 0);
			this.numericUpDown_72.Enabled = (*(sbyte*)ptr != 0);
			this.checkBox_67.Enabled = (*(sbyte*)ptr != 0);
			this.comboBox_72.Enabled = (*(sbyte*)ptr != 0);
			this.button_96.Enabled = (*(sbyte*)ptr != 0);
			this.fastObjectListView_22.Enabled = (*(sbyte*)ptr != 0);
			((byte*)ptr)[1] = (byte)(*(sbyte*)ptr);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				this.checkBox_68_CheckedChanged(null, null);
				this.checkBox_67_CheckedChanged(null, null);
			}
		}

		private void checkBox_68_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_72.Enabled = this.checkBox_68.Checked;
		}

		private void checkBox_67_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox_67.Checked;
			this.comboBox_72.Enabled = @checked;
			this.button_96.Enabled = @checked;
			this.fastObjectListView_22.Enabled = @checked;
		}

		private unsafe void timer_5_Tick(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			object obj = this.object_0;
			*(byte*)ptr = 0;
			try
			{
				Monitor.Enter(obj, ref *(bool*)ptr);
				((byte*)ptr)[1] = ((!this.bool_17) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					MainForm.IsPaused = true;
					this.bool_17 = false;
					((byte*)ptr)[2] = ((this.genum0_0 == MainForm.GEnum0.const_0) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107389821));
						Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107389816));
						this.method_64(true);
					}
					else
					{
						Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107389763));
						this.method_64(true);
						if (UI.smethod_26() && UI.smethod_29() == GameProcessState.Online)
						{
							Win32.smethod_16(MainForm.getString_0(107389742), true, true, false, false);
							Thread.Sleep(10000);
							for (;;)
							{
								((byte*)ptr)[3] = (UI.smethod_28() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 3) == 0)
								{
									break;
								}
								Thread.Sleep(100);
							}
						}
						this.method_58(true, MainForm.GEnum1.const_0, false);
					}
				}
			}
			finally
			{
				if (*(sbyte*)ptr != 0)
				{
					Monitor.Exit(obj);
				}
			}
		}

		private void method_146()
		{
			Task.Run(new Action(this.method_201));
		}

		private void method_147(LiveSearchListItem liveSearchListItem_0, Class260 class260_0)
		{
			if (liveSearchListItem_0.Enabled && this.genum0_0 == MainForm.GEnum0.const_2)
			{
				if (liveSearchListItem_0.MaxPrice.Amount == 0m)
				{
					Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107389733), new object[]
					{
						liveSearchListItem_0.Id,
						liveSearchListItem_0.Description
					});
				}
				else
				{
					class260_0.method_0(Class145.smethod_2(liveSearchListItem_0.Id, liveSearchListItem_0.MaxPrice));
					Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107389204), new object[]
					{
						liveSearchListItem_0.Id,
						liveSearchListItem_0.MaxPrice
					});
					class260_0.method_1();
				}
			}
		}

		private void timer_6_Tick(object sender, EventArgs e)
		{
			if (!Class123.IsConnected)
			{
				Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107389127));
				Class123.smethod_12(false, true);
				Class123.smethod_2(this);
			}
		}

		private void timer_7_Tick(object sender, EventArgs e)
		{
			using (WebClient webClient = new WebClient())
			{
				try
				{
					string value = webClient.DownloadString(Class103.PoeNinjaCurrencyUrl);
					PoeNinja poeNinja = JsonConvert.DeserializeObject<PoeNinja>(value, Util.smethod_12());
					List<PoeNinja.Line> list = poeNinja.Lines.ToList<PoeNinja.Line>();
					list.RemoveAll(new Predicate<PoeNinja.Line>(MainForm.<>c.<>9.method_155));
					foreach (PoeNinja.Line line in list)
					{
						if (!this.dictionary_5.ContainsKey(line.CurrencyTypeName))
						{
							this.dictionary_5.Add(line.CurrencyTypeName, line.Receive.Value);
						}
					}
				}
				catch (Exception ex)
				{
					Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107389062), new object[]
					{
						ex
					});
				}
			}
		}

		private void checkBox_75_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_74.Enabled = this.checkBox_75.Checked;
		}

		private void checkBox_79_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDown_75.Enabled = this.checkBox_79.Checked;
		}

		private void comboBox_74_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.button_100.PerformClick();
			}
		}

		private void button_99_Click(object sender, EventArgs e)
		{
			if (this.enum8_0 != Enum8.const_0)
			{
				this.button_99.Enabled = false;
				this.method_130(new ThreadStart(this.method_202));
			}
		}

		public void method_148()
		{
			base.Invoke(new Action(this.method_203));
			this.method_131();
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_149()
		{
			this.icontainer_0 = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.tabControl_4 = new TabControl();
			this.tabPage_0 = new TabPage();
			this.button_95 = new Button();
			this.button_90 = new Button();
			this.button_78 = new Button();
			this.textBox_17 = new TextBox();
			this.comboBox_56 = new ComboBox();
			this.comboBox_57 = new ComboBox();
			this.button_67 = new Button();
			this.button_65 = new Button();
			this.button_59 = new Button();
			this.button_60 = new Button();
			this.button_61 = new Button();
			this.button_56 = new Button();
			this.tabControl_6 = new TabControl();
			this.tabPage_30 = new TabPage();
			this.richTextBox_0 = new RichTextBox();
			this.tabPage_31 = new TabPage();
			this.richTextBox_1 = new RichTextBox();
			this.button_2 = new Button();
			this.button_1 = new Button();
			this.groupBox_0 = new GroupBox();
			this.label_132 = new Label();
			this.label_133 = new Label();
			this.label_134 = new Label();
			this.label_135 = new Label();
			this.label_136 = new Label();
			this.label_137 = new Label();
			this.label_98 = new Label();
			this.label_99 = new Label();
			this.groupBox_2 = new GroupBox();
			this.label_7 = new Label();
			this.label_8 = new Label();
			this.label_9 = new Label();
			this.groupBox_1 = new GroupBox();
			this.label_1 = new Label();
			this.label_2 = new Label();
			this.label_3 = new Label();
			this.label_4 = new Label();
			this.label_5 = new Label();
			this.label_6 = new Label();
			this.label_147 = new Label();
			this.label_148 = new Label();
			this.label_149 = new Label();
			this.button_0 = new Button();
			this.tabPage_1 = new TabPage();
			this.tabControl_0 = new TabControl();
			this.tabPage_2 = new TabPage();
			this.groupBox_53 = new GroupBox();
			this.label_140 = new Label();
			this.trackBar_2 = new TrackBar();
			this.checkBox_22 = new CheckBox();
			this.groupBox_32 = new GroupBox();
			this.button_47 = new Button();
			this.checkBox_30 = new CheckBox();
			this.button_33 = new Button();
			this.groupBox_3 = new GroupBox();
			this.checkBox_62 = new CheckBox();
			this.label_177 = new Label();
			this.comboBox_70 = new ComboBox();
			this.label_163 = new Label();
			this.comboBox_66 = new ComboBox();
			this.label_159 = new Label();
			this.textBox_18 = new TextBox();
			this.comboBox_65 = new ComboBox();
			this.label_160 = new Label();
			this.button_72 = new Button();
			this.pictureBox_2 = new PictureBox();
			this.textBox_8 = new TextBox();
			this.label_83 = new Label();
			this.button_17 = new Button();
			this.checkBox_15 = new CheckBox();
			this.checkBox_18 = new CheckBox();
			this.comboBox_10 = new ComboBox();
			this.label_10 = new Label();
			this.comboBox_11 = new ComboBox();
			this.label_11 = new Label();
			this.tabPage_3 = new TabPage();
			this.tabControl_5 = new TabControl();
			this.tabPage_27 = new TabPage();
			this.groupBox_7 = new GroupBox();
			this.checkBox_80 = new CheckBox();
			this.checkBox_49 = new CheckBox();
			this.groupBox_52 = new GroupBox();
			this.button_66 = new Button();
			this.textBox_16 = new TextBox();
			this.groupBox_51 = new GroupBox();
			this.button_64 = new Button();
			this.textBox_15 = new TextBox();
			this.pictureBox_1 = new PictureBox();
			this.button_26 = new Button();
			this.button_25 = new Button();
			this.button_3 = new Button();
			this.groupBox_14 = new GroupBox();
			this.button_6 = new Button();
			this.textBox_2 = new TextBox();
			this.label_17 = new Label();
			this.textBox_0 = new TextBox();
			this.label_18 = new Label();
			this.textBox_1 = new TextBox();
			this.comboBox_0 = new ComboBox();
			this.label_19 = new Label();
			this.checkBox_2 = new CheckBox();
			this.label_20 = new Label();
			this.textBox_3 = new TextBox();
			this.numericUpDown_8 = new NumericUpDown();
			this.numericUpDown_9 = new NumericUpDown();
			this.checkBox_3 = new CheckBox();
			this.label_21 = new Label();
			this.comboBox_1 = new ComboBox();
			this.label_22 = new Label();
			this.label_23 = new Label();
			this.checkBox_4 = new CheckBox();
			this.checkBox_5 = new CheckBox();
			this.label_24 = new Label();
			this.label_25 = new Label();
			this.textBox_4 = new TextBox();
			this.textBox_5 = new TextBox();
			this.tabPage_28 = new TabPage();
			this.groupBox_4 = new GroupBox();
			this.groupBox_5 = new GroupBox();
			this.checkBox_17 = new CheckBox();
			this.checkBox_0 = new CheckBox();
			this.numericUpDown_0 = new NumericUpDown();
			this.numericUpDown_1 = new NumericUpDown();
			this.numericUpDown_2 = new NumericUpDown();
			this.numericUpDown_3 = new NumericUpDown();
			this.groupBox_6 = new GroupBox();
			this.label_92 = new Label();
			this.textBox_11 = new TextBox();
			this.button_21 = new Button();
			this.fastObjectListView_3 = new FastObjectListView();
			this.olvcolumn_3 = new OLVColumn();
			this.checkBox_1 = new CheckBox();
			this.label_12 = new Label();
			this.numericUpDown_4 = new NumericUpDown();
			this.label_13 = new Label();
			this.numericUpDown_5 = new NumericUpDown();
			this.label_14 = new Label();
			this.label_15 = new Label();
			this.numericUpDown_6 = new NumericUpDown();
			this.label_16 = new Label();
			this.numericUpDown_7 = new NumericUpDown();
			this.tabPage_4 = new TabPage();
			this.tabControl_3 = new TabControl();
			this.tabPage_16 = new TabPage();
			this.groupBox_9 = new GroupBox();
			this.button_5 = new Button();
			this.checkBox_6 = new CheckBox();
			this.comboBox_3 = new ComboBox();
			this.label_26 = new Label();
			this.comboBox_4 = new ComboBox();
			this.checkBox_7 = new CheckBox();
			this.comboBox_5 = new ComboBox();
			this.comboBox_6 = new ComboBox();
			this.label_27 = new Label();
			this.comboBox_7 = new ComboBox();
			this.label_28 = new Label();
			this.comboBox_8 = new ComboBox();
			this.label_29 = new Label();
			this.comboBox_9 = new ComboBox();
			this.label_30 = new Label();
			this.label_31 = new Label();
			this.label_32 = new Label();
			this.groupBox_8 = new GroupBox();
			this.comboBox_2 = new ComboBox();
			this.button_4 = new Button();
			this.fastObjectListView_0 = new FastObjectListView();
			this.olvcolumn_0 = new OLVColumn();
			this.tabPage_26 = new TabPage();
			this.groupBox_23 = new GroupBox();
			this.label_155 = new Label();
			this.comboBox_64 = new ComboBox();
			this.button_20 = new Button();
			this.label_86 = new Label();
			this.label_87 = new Label();
			this.comboBox_21 = new ComboBox();
			this.comboBox_22 = new ComboBox();
			this.comboBox_23 = new ComboBox();
			this.label_88 = new Label();
			this.label_89 = new Label();
			this.comboBox_24 = new ComboBox();
			this.comboBox_25 = new ComboBox();
			this.label_90 = new Label();
			this.comboBox_26 = new ComboBox();
			this.label_91 = new Label();
			this.tabPage_18 = new TabPage();
			this.groupBox_15 = new GroupBox();
			this.button_7 = new Button();
			this.label_69 = new Label();
			this.label_70 = new Label();
			this.label_71 = new Label();
			this.comboBox_14 = new ComboBox();
			this.comboBox_15 = new ComboBox();
			this.comboBox_16 = new ComboBox();
			this.label_67 = new Label();
			this.comboBox_12 = new ComboBox();
			this.comboBox_13 = new ComboBox();
			this.label_68 = new Label();
			this.tabPage_24 = new TabPage();
			this.groupBox_18 = new GroupBox();
			this.button_13 = new Button();
			this.button_14 = new Button();
			this.comboBox_19 = new ComboBox();
			this.button_15 = new Button();
			this.groupBox_19 = new GroupBox();
			this.button_18 = new Button();
			this.comboBox_20 = new ComboBox();
			this.button_16 = new Button();
			this.fastObjectListView_2 = new FastObjectListView();
			this.olvcolumn_2 = new OLVColumn();
			this.tabPage_50 = new TabPage();
			this.tabControl_9 = new TabControl();
			this.tabPage_51 = new TabPage();
			this.groupBox_68 = new GroupBox();
			this.checkBox_78 = new CheckBox();
			this.checkBox_75 = new CheckBox();
			this.numericUpDown_74 = new NumericUpDown();
			this.label_185 = new Label();
			this.checkBox_64 = new CheckBox();
			this.groupBox_61 = new GroupBox();
			this.listBox_1 = new ListBox();
			this.button_82 = new Button();
			this.button_83 = new Button();
			this.groupBox_62 = new GroupBox();
			this.comboBox_68 = new ComboBox();
			this.button_84 = new Button();
			this.fastObjectListView_17 = new FastObjectListView();
			this.olvcolumn_23 = new OLVColumn();
			this.checkBox_52 = new CheckBox();
			this.numericUpDown_62 = new NumericUpDown();
			this.label_167 = new Label();
			this.label_168 = new Label();
			this.tabPage_54 = new TabPage();
			this.groupBox_69 = new GroupBox();
			this.checkBox_73 = new CheckBox();
			this.comboBox_72 = new ComboBox();
			this.button_96 = new Button();
			this.fastObjectListView_22 = new FastObjectListView();
			this.olvcolumn_28 = new OLVColumn();
			this.checkBox_67 = new CheckBox();
			this.checkBox_69 = new CheckBox();
			this.checkBox_70 = new CheckBox();
			this.checkBox_71 = new CheckBox();
			this.checkBox_68 = new CheckBox();
			this.numericUpDown_72 = new NumericUpDown();
			this.label_182 = new Label();
			this.checkBox_72 = new CheckBox();
			this.tabPage_52 = new TabPage();
			this.groupBox_63 = new GroupBox();
			this.button_85 = new Button();
			this.comboBox_69 = new ComboBox();
			this.button_86 = new Button();
			this.fastObjectListView_18 = new FastObjectListView();
			this.olvcolumn_24 = new OLVColumn();
			this.tabPage_53 = new TabPage();
			this.groupBox_64 = new GroupBox();
			this.checkBox_53 = new CheckBox();
			this.textBox_19 = new TextBox();
			this.button_87 = new Button();
			this.fastObjectListView_19 = new FastObjectListView();
			this.olvcolumn_25 = new OLVColumn();
			this.groupBox_65 = new GroupBox();
			this.checkBox_54 = new CheckBox();
			this.textBox_20 = new TextBox();
			this.button_88 = new Button();
			this.fastObjectListView_20 = new FastObjectListView();
			this.olvcolumn_26 = new OLVColumn();
			this.groupBox_66 = new GroupBox();
			this.checkBox_55 = new CheckBox();
			this.textBox_21 = new TextBox();
			this.button_89 = new Button();
			this.fastObjectListView_21 = new FastObjectListView();
			this.olvcolumn_27 = new OLVColumn();
			this.checkBox_56 = new CheckBox();
			this.numericUpDown_63 = new NumericUpDown();
			this.label_169 = new Label();
			this.checkBox_57 = new CheckBox();
			this.numericUpDown_64 = new NumericUpDown();
			this.label_170 = new Label();
			this.tabPage_5 = new TabPage();
			this.groupBox_67 = new GroupBox();
			this.checkBox_58 = new CheckBox();
			this.label_171 = new Label();
			this.numericUpDown_65 = new NumericUpDown();
			this.label_172 = new Label();
			this.numericUpDown_66 = new NumericUpDown();
			this.label_173 = new Label();
			this.numericUpDown_67 = new NumericUpDown();
			this.checkBox_59 = new CheckBox();
			this.label_174 = new Label();
			this.numericUpDown_68 = new NumericUpDown();
			this.label_175 = new Label();
			this.numericUpDown_69 = new NumericUpDown();
			this.checkBox_60 = new CheckBox();
			this.label_176 = new Label();
			this.numericUpDown_70 = new NumericUpDown();
			this.tabPage_29 = new TabPage();
			this.groupBox_50 = new GroupBox();
			this.listBox_0 = new ListBox();
			this.button_62 = new Button();
			this.button_63 = new Button();
			this.groupBox_33 = new GroupBox();
			this.checkBox_76 = new CheckBox();
			this.label_164 = new Label();
			this.numericUpDown_60 = new NumericUpDown();
			this.numericUpDown_61 = new NumericUpDown();
			this.label_165 = new Label();
			this.label_166 = new Label();
			this.label_158 = new Label();
			this.numericUpDown_57 = new NumericUpDown();
			this.checkBox_31 = new CheckBox();
			this.groupBox_26 = new GroupBox();
			this.button_27 = new Button();
			this.fastObjectListView_4 = new FastObjectListView();
			this.olvcolumn_4 = new OLVColumn();
			this.olvcolumn_5 = new OLVColumn();
			this.comboBox_28 = new ComboBox();
			this.comboBox_29 = new ComboBox();
			this.label_96 = new Label();
			this.label_97 = new Label();
			this.tabPage_7 = new TabPage();
			this.tabControl_1 = new TabControl();
			this.tabPage_8 = new TabPage();
			this.checkBox_79 = new CheckBox();
			this.numericUpDown_75 = new NumericUpDown();
			this.checkBox_74 = new CheckBox();
			this.checkBox_66 = new CheckBox();
			this.checkBox_32 = new CheckBox();
			this.checkBox_26 = new CheckBox();
			this.checkBox_23 = new CheckBox();
			this.checkBox_24 = new CheckBox();
			this.checkBox_25 = new CheckBox();
			this.checkBox_20 = new CheckBox();
			this.checkBox_8 = new CheckBox();
			this.checkBox_9 = new CheckBox();
			this.checkBox_10 = new CheckBox();
			this.checkBox_11 = new CheckBox();
			this.checkBox_12 = new CheckBox();
			this.tabPage_10 = new TabPage();
			this.checkBox_61 = new CheckBox();
			this.label_178 = new Label();
			this.textBox_22 = new TextBox();
			this.label_179 = new Label();
			this.textBox_23 = new TextBox();
			this.checkBox_14 = new CheckBox();
			this.label_34 = new Label();
			this.textBox_7 = new TextBox();
			this.tabPage_9 = new TabPage();
			this.checkBox_13 = new CheckBox();
			this.label_33 = new Label();
			this.textBox_6 = new TextBox();
			this.tabPage_11 = new TabPage();
			this.tabControl_2 = new TabControl();
			this.tabPage_17 = new TabPage();
			this.groupBox_20 = new GroupBox();
			this.radioButton_0 = new RadioButton();
			this.radioButton_1 = new RadioButton();
			this.radioButton_2 = new RadioButton();
			this.tabPage_12 = new TabPage();
			this.groupBox_10 = new GroupBox();
			this.label_183 = new Label();
			this.numericUpDown_73 = new NumericUpDown();
			this.label_184 = new Label();
			this.label_180 = new Label();
			this.numericUpDown_71 = new NumericUpDown();
			this.label_181 = new Label();
			this.label_156 = new Label();
			this.numericUpDown_56 = new NumericUpDown();
			this.label_157 = new Label();
			this.label_138 = new Label();
			this.numericUpDown_52 = new NumericUpDown();
			this.label_139 = new Label();
			this.label_100 = new Label();
			this.trackBar_0 = new TrackBar();
			this.label_35 = new Label();
			this.numericUpDown_10 = new NumericUpDown();
			this.label_36 = new Label();
			this.label_37 = new Label();
			this.numericUpDown_11 = new NumericUpDown();
			this.label_38 = new Label();
			this.label_39 = new Label();
			this.numericUpDown_12 = new NumericUpDown();
			this.label_40 = new Label();
			this.numericUpDown_13 = new NumericUpDown();
			this.label_41 = new Label();
			this.numericUpDown_14 = new NumericUpDown();
			this.label_42 = new Label();
			this.label_43 = new Label();
			this.label_44 = new Label();
			this.tabPage_13 = new TabPage();
			this.groupBox_11 = new GroupBox();
			this.label_45 = new Label();
			this.numericUpDown_15 = new NumericUpDown();
			this.label_46 = new Label();
			this.label_47 = new Label();
			this.label_48 = new Label();
			this.label_49 = new Label();
			this.label_50 = new Label();
			this.label_51 = new Label();
			this.numericUpDown_16 = new NumericUpDown();
			this.label_52 = new Label();
			this.numericUpDown_17 = new NumericUpDown();
			this.label_53 = new Label();
			this.numericUpDown_18 = new NumericUpDown();
			this.label_54 = new Label();
			this.numericUpDown_19 = new NumericUpDown();
			this.label_55 = new Label();
			this.numericUpDown_20 = new NumericUpDown();
			this.label_56 = new Label();
			this.numericUpDown_21 = new NumericUpDown();
			this.label_57 = new Label();
			this.label_58 = new Label();
			this.label_59 = new Label();
			this.label_60 = new Label();
			this.numericUpDown_22 = new NumericUpDown();
			this.tabPage_14 = new TabPage();
			this.groupBox_12 = new GroupBox();
			this.label_161 = new Label();
			this.label_162 = new Label();
			this.numericUpDown_58 = new NumericUpDown();
			this.label_79 = new Label();
			this.label_80 = new Label();
			this.label_81 = new Label();
			this.numericUpDown_27 = new NumericUpDown();
			this.label_82 = new Label();
			this.numericUpDown_28 = new NumericUpDown();
			this.label_61 = new Label();
			this.numericUpDown_23 = new NumericUpDown();
			this.label_62 = new Label();
			this.label_63 = new Label();
			this.label_64 = new Label();
			this.numericUpDown_24 = new NumericUpDown();
			this.tabPage_15 = new TabPage();
			this.groupBox_13 = new GroupBox();
			this.label_65 = new Label();
			this.label_66 = new Label();
			this.numericUpDown_25 = new NumericUpDown();
			this.tabPage_6 = new TabPage();
			this.groupBox_24 = new GroupBox();
			this.dataGridView_0 = new DataGridView();
			this.dataGridViewCheckBoxColumn_0 = new DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn_0 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_1 = new DataGridViewTextBoxColumn();
			this.button_22 = new Button();
			this.linkLabel_0 = new LinkLabel();
			this.pictureBox_0 = new PictureBox();
			this.button_23 = new Button();
			this.groupBox_25 = new GroupBox();
			this.button_91 = new Button();
			this.button_79 = new Button();
			this.numericUpDown_35 = new NumericUpDown();
			this.label_113 = new Label();
			this.checkBox_28 = new CheckBox();
			this.checkBox_29 = new CheckBox();
			this.checkBox_27 = new CheckBox();
			this.label_104 = new Label();
			this.numericUpDown_30 = new NumericUpDown();
			this.checkBox_21 = new CheckBox();
			this.button_24 = new Button();
			this.numericUpDown_29 = new NumericUpDown();
			this.label_93 = new Label();
			this.label_94 = new Label();
			this.checkBox_19 = new CheckBox();
			this.label_95 = new Label();
			this.comboBox_27 = new ComboBox();
			this.tabPage_21 = new TabPage();
			this.tabControl_8 = new TabControl();
			this.tabPage_25 = new TabPage();
			this.button_92 = new Button();
			this.checkBox_46 = new CheckBox();
			this.groupBox_21 = new GroupBox();
			this.dataGridView_1 = new DataGridView();
			this.dataGridViewCheckBoxColumn_1 = new DataGridViewCheckBoxColumn();
			this.dataGridViewComboBoxColumn_0 = new DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn_2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_4 = new DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn_1 = new DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn_5 = new DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn_2 = new DataGridViewComboBoxColumn();
			this.groupBox_22 = new GroupBox();
			this.textBox_9 = new TextBox();
			this.textBox_10 = new TextBox();
			this.label_84 = new Label();
			this.label_85 = new Label();
			this.button_19 = new Button();
			this.tabPage_34 = new TabPage();
			this.button_93 = new Button();
			this.checkBox_45 = new CheckBox();
			this.label_110 = new Label();
			this.numericUpDown_33 = new NumericUpDown();
			this.numericUpDown_34 = new NumericUpDown();
			this.label_111 = new Label();
			this.label_112 = new Label();
			this.groupBox_30 = new GroupBox();
			this.dataGridView_3 = new DataGridView();
			this.dataGridViewCheckBoxColumn_2 = new DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn_6 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_7 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_8 = new DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn_3 = new DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn_9 = new DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn_4 = new DataGridViewComboBoxColumn();
			this.groupBox_31 = new GroupBox();
			this.textBox_12 = new TextBox();
			this.textBox_13 = new TextBox();
			this.label_108 = new Label();
			this.label_109 = new Label();
			this.button_32 = new Button();
			this.tabPage_33 = new TabPage();
			this.button_94 = new Button();
			this.checkBox_47 = new CheckBox();
			this.label_105 = new Label();
			this.numericUpDown_31 = new NumericUpDown();
			this.numericUpDown_32 = new NumericUpDown();
			this.label_106 = new Label();
			this.label_107 = new Label();
			this.button_31 = new Button();
			this.groupBox_29 = new GroupBox();
			this.dataGridView_2 = new DataGridView();
			this.dataGridViewCheckBoxColumn_3 = new DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn_10 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_11 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_12 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_13 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_14 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn_15 = new DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn_5 = new DataGridViewComboBoxColumn();
			this.tabPage_22 = new TabPage();
			this.tabControl_7 = new TabControl();
			this.tabPage_43 = new TabPage();
			this.groupBox_46 = new GroupBox();
			this.checkBox_77 = new CheckBox();
			this.checkBox_51 = new CheckBox();
			this.label_122 = new Label();
			this.label_123 = new Label();
			this.numericUpDown_43 = new NumericUpDown();
			this.label_124 = new Label();
			this.checkBox_38 = new CheckBox();
			this.comboBox_49 = new ComboBox();
			this.checkBox_39 = new CheckBox();
			this.checkBox_40 = new CheckBox();
			this.label_125 = new Label();
			this.comboBox_50 = new ComboBox();
			this.label_126 = new Label();
			this.label_127 = new Label();
			this.numericUpDown_44 = new NumericUpDown();
			this.numericUpDown_45 = new NumericUpDown();
			this.checkBox_41 = new CheckBox();
			this.checkBox_42 = new CheckBox();
			this.comboBox_51 = new ComboBox();
			this.button_52 = new Button();
			this.fastObjectListView_11 = new FastObjectListView();
			this.olvcolumn_16 = new OLVColumn();
			this.button_53 = new Button();
			this.label_128 = new Label();
			this.textBox_14 = new TextBox();
			this.checkBox_43 = new CheckBox();
			this.button_54 = new Button();
			this.tabPage_44 = new TabPage();
			this.groupBox_47 = new GroupBox();
			this.comboBox_52 = new ComboBox();
			this.button_55 = new Button();
			this.fastObjectListView_12 = new FastObjectListView();
			this.olvcolumn_17 = new OLVColumn();
			this.tabPage_49 = new TabPage();
			this.groupBox_60 = new GroupBox();
			this.button_80 = new Button();
			this.button_81 = new Button();
			this.tabPage_19 = new TabPage();
			this.tabControl_11 = new TabControl();
			this.tabPage_35 = new TabPage();
			this.groupBox_45 = new GroupBox();
			this.checkBox_65 = new CheckBox();
			this.comboBox_71 = new ComboBox();
			this.checkBox_63 = new CheckBox();
			this.comboBox_48 = new ComboBox();
			this.checkBox_37 = new CheckBox();
			this.label_121 = new Label();
			this.trackBar_1 = new TrackBar();
			this.groupBox_34 = new GroupBox();
			this.panel_0 = new Panel();
			this.button_34 = new Button();
			this.button_35 = new Button();
			this.comboBox_34 = new ComboBox();
			this.tabPage_36 = new TabPage();
			this.groupBox_35 = new GroupBox();
			this.button_39 = new Button();
			this.button_40 = new Button();
			this.checkBox_33 = new CheckBox();
			this.checkBox_34 = new CheckBox();
			this.groupBox_36 = new GroupBox();
			this.comboBox_35 = new ComboBox();
			this.button_36 = new Button();
			this.fastObjectListView_6 = new FastObjectListView();
			this.olvcolumn_7 = new OLVColumn();
			this.olvcolumn_8 = new OLVColumn();
			this.comboBox_36 = new ComboBox();
			this.comboBox_37 = new ComboBox();
			this.groupBox_37 = new GroupBox();
			this.comboBox_38 = new ComboBox();
			this.button_37 = new Button();
			this.fastObjectListView_7 = new FastObjectListView();
			this.olvcolumn_9 = new OLVColumn();
			this.olvcolumn_10 = new OLVColumn();
			this.comboBox_39 = new ComboBox();
			this.comboBox_40 = new ComboBox();
			this.checkBox_35 = new CheckBox();
			this.numericUpDown_36 = new NumericUpDown();
			this.label_114 = new Label();
			this.button_38 = new Button();
			this.tabPage_37 = new TabPage();
			this.groupBox_38 = new GroupBox();
			this.groupBox_39 = new GroupBox();
			this.comboBox_41 = new ComboBox();
			this.button_44 = new Button();
			this.fastObjectListView_8 = new FastObjectListView();
			this.olvcolumn_11 = new OLVColumn();
			this.olvcolumn_12 = new OLVColumn();
			this.comboBox_42 = new ComboBox();
			this.comboBox_43 = new ComboBox();
			this.groupBox_40 = new GroupBox();
			this.comboBox_44 = new ComboBox();
			this.button_45 = new Button();
			this.fastObjectListView_9 = new FastObjectListView();
			this.olvcolumn_13 = new OLVColumn();
			this.olvcolumn_14 = new OLVColumn();
			this.comboBox_45 = new ComboBox();
			this.comboBox_46 = new ComboBox();
			this.radioButton_3 = new RadioButton();
			this.radioButton_4 = new RadioButton();
			this.button_41 = new Button();
			this.button_42 = new Button();
			this.checkBox_36 = new CheckBox();
			this.numericUpDown_37 = new NumericUpDown();
			this.label_115 = new Label();
			this.button_43 = new Button();
			this.tabPage_48 = new TabPage();
			this.groupBox_59 = new GroupBox();
			this.comboBox_62 = new ComboBox();
			this.label_153 = new Label();
			this.button_76 = new Button();
			this.button_77 = new Button();
			this.label_154 = new Label();
			this.comboBox_63 = new ComboBox();
			this.tabPage_40 = new TabPage();
			this.groupBox_41 = new GroupBox();
			this.numericUpDown_38 = new NumericUpDown();
			this.label_116 = new Label();
			this.button_46 = new Button();
			this.tabPage_41 = new TabPage();
			this.groupBox_44 = new GroupBox();
			this.numericUpDown_39 = new NumericUpDown();
			this.label_117 = new Label();
			this.numericUpDown_40 = new NumericUpDown();
			this.label_118 = new Label();
			this.numericUpDown_41 = new NumericUpDown();
			this.label_119 = new Label();
			this.numericUpDown_42 = new NumericUpDown();
			this.label_120 = new Label();
			this.button_51 = new Button();
			this.tabPage_20 = new TabPage();
			this.groupBox_48 = new GroupBox();
			this.comboBox_55 = new ComboBox();
			this.button_57 = new Button();
			this.fastObjectListView_13 = new FastObjectListView();
			this.olvcolumn_18 = new OLVColumn();
			this.comboBox_53 = new ComboBox();
			this.groupBox_49 = new GroupBox();
			this.button_58 = new Button();
			this.fastObjectListView_14 = new FastObjectListView();
			this.olvcolumn_19 = new OLVColumn();
			this.comboBox_54 = new ComboBox();
			this.groupBox_16 = new GroupBox();
			this.radioButton_7 = new RadioButton();
			this.checkBox_48 = new CheckBox();
			this.numericUpDown_49 = new NumericUpDown();
			this.label_129 = new Label();
			this.numericUpDown_50 = new NumericUpDown();
			this.label_130 = new Label();
			this.numericUpDown_51 = new NumericUpDown();
			this.label_131 = new Label();
			this.checkBox_44 = new CheckBox();
			this.radioButton_5 = new RadioButton();
			this.radioButton_6 = new RadioButton();
			this.numericUpDown_46 = new NumericUpDown();
			this.label_75 = new Label();
			this.button_8 = new Button();
			this.button_9 = new Button();
			this.label_72 = new Label();
			this.comboBox_17 = new ComboBox();
			this.numericUpDown_48 = new NumericUpDown();
			this.label_73 = new Label();
			this.numericUpDown_47 = new NumericUpDown();
			this.label_74 = new Label();
			this.tabPage_38 = new TabPage();
			this.tabPage_39 = new TabPage();
			this.tabPage_55 = new TabPage();
			this.tabControl_10 = new TabControl();
			this.tabPage_56 = new TabPage();
			this.groupBox_70 = new GroupBox();
			this.comboBox_75 = new ComboBox();
			this.label_190 = new Label();
			this.button_97 = new Button();
			this.fastObjectListView_23 = new FastObjectListView();
			this.olvcolumn_29 = new OLVColumn();
			this.label_186 = new Label();
			this.numericUpDown_76 = new NumericUpDown();
			this.label_187 = new Label();
			this.comboBox_73 = new ComboBox();
			this.label_188 = new Label();
			this.button_98 = new Button();
			this.button_99 = new Button();
			this.groupBox_71 = new GroupBox();
			this.comboBox_74 = new ComboBox();
			this.button_100 = new Button();
			this.fastObjectListView_24 = new FastObjectListView();
			this.olvcolumn_30 = new OLVColumn();
			this.label_189 = new Label();
			this.tabPage_45 = new TabPage();
			this.groupBox_56 = new GroupBox();
			this.label_143 = new Label();
			this.numericUpDown_54 = new NumericUpDown();
			this.label_144 = new Label();
			this.label_145 = new Label();
			this.numericUpDown_55 = new NumericUpDown();
			this.label_146 = new Label();
			this.button_70 = new Button();
			this.groupBox_55 = new GroupBox();
			this.label_142 = new Label();
			this.numericUpDown_53 = new NumericUpDown();
			this.checkBox_50 = new CheckBox();
			this.groupBox_54 = new GroupBox();
			this.fastObjectListView_15 = new FastObjectListView();
			this.olvcolumn_21 = new OLVColumn();
			this.button_68 = new Button();
			this.comboBox_58 = new ComboBox();
			this.label_141 = new Label();
			this.button_69 = new Button();
			this.tabPage_32 = new TabPage();
			this.groupBox_27 = new GroupBox();
			this.button_28 = new Button();
			this.button_29 = new Button();
			this.comboBox_30 = new ComboBox();
			this.button_30 = new Button();
			this.fastObjectListView_5 = new FastObjectListView();
			this.olvcolumn_6 = new OLVColumn();
			this.groupBox_28 = new GroupBox();
			this.numericUpDown_59 = new NumericUpDown();
			this.comboBox_67 = new ComboBox();
			this.comboBox_32 = new ComboBox();
			this.label_102 = new Label();
			this.comboBox_33 = new ComboBox();
			this.label_103 = new Label();
			this.comboBox_31 = new ComboBox();
			this.label_101 = new Label();
			this.tabPage_42 = new TabPage();
			this.groupBox_43 = new GroupBox();
			this.richTextBox_2 = new RichTextBox();
			this.groupBox_42 = new GroupBox();
			this.button_48 = new Button();
			this.button_49 = new Button();
			this.comboBox_47 = new ComboBox();
			this.button_50 = new Button();
			this.fastObjectListView_10 = new FastObjectListView();
			this.olvcolumn_15 = new OLVColumn();
			this.tabPage_23 = new TabPage();
			this.groupBox_17 = new GroupBox();
			this.label_76 = new Label();
			this.numericUpDown_26 = new NumericUpDown();
			this.label_77 = new Label();
			this.button_10 = new Button();
			this.button_11 = new Button();
			this.comboBox_18 = new ComboBox();
			this.button_12 = new Button();
			this.fastObjectListView_1 = new FastObjectListView();
			this.olvcolumn_1 = new OLVColumn();
			this.tabPage_46 = new TabPage();
			this.groupBox_57 = new GroupBox();
			this.button_71 = new Button();
			this.comboBox_59 = new ComboBox();
			this.comboBox_60 = new ComboBox();
			this.label_150 = new Label();
			this.label_151 = new Label();
			this.tabPage_47 = new TabPage();
			this.groupBox_58 = new GroupBox();
			this.button_73 = new Button();
			this.fastObjectListView_16 = new FastObjectListView();
			this.olvcolumn_22 = new OLVColumn();
			this.button_74 = new Button();
			this.comboBox_61 = new ComboBox();
			this.label_152 = new Label();
			this.button_75 = new Button();
			this.olvcolumn_20 = new OLVColumn();
			this.timer_0 = new Timer(this.icontainer_0);
			this.timer_1 = new Timer(this.icontainer_0);
			this.toolStrip_0 = new ToolStrip();
			this.toolStripProgressBar_0 = new ToolStripProgressBar();
			this.toolStripLabel_0 = new ToolStripLabel();
			this.toolStripSeparator_0 = new ToolStripSeparator();
			this.toolStripLabel_1 = new ToolStripLabel();
			this.toolStripSeparator_1 = new ToolStripSeparator();
			this.toolStripLabel_2 = new ToolStripLabel();
			this.toolTip_0 = new ToolTip(this.icontainer_0);
			this.checkBox_16 = new CheckBox();
			this.timer_2 = new Timer(this.icontainer_0);
			this.label_78 = new Label();
			this.timer_3 = new Timer(this.icontainer_0);
			this.timer_4 = new Timer(this.icontainer_0);
			this.timer_5 = new Timer(this.icontainer_0);
			this.timer_6 = new Timer(this.icontainer_0);
			this.timer_7 = new Timer(this.icontainer_0);
			this.comboBox_77 = new ComboBox();
			this.comboBox_76 = new ComboBox();
			this.tabControl_4.SuspendLayout();
			this.tabPage_0.SuspendLayout();
			this.tabControl_6.SuspendLayout();
			this.tabPage_30.SuspendLayout();
			this.tabPage_31.SuspendLayout();
			this.groupBox_0.SuspendLayout();
			this.groupBox_2.SuspendLayout();
			this.groupBox_1.SuspendLayout();
			this.tabPage_1.SuspendLayout();
			this.tabControl_0.SuspendLayout();
			this.tabPage_2.SuspendLayout();
			this.groupBox_53.SuspendLayout();
			((ISupportInitialize)this.trackBar_2).BeginInit();
			this.groupBox_32.SuspendLayout();
			this.groupBox_3.SuspendLayout();
			((ISupportInitialize)this.pictureBox_2).BeginInit();
			this.tabPage_3.SuspendLayout();
			this.tabControl_5.SuspendLayout();
			this.tabPage_27.SuspendLayout();
			this.groupBox_7.SuspendLayout();
			this.groupBox_52.SuspendLayout();
			this.groupBox_51.SuspendLayout();
			((ISupportInitialize)this.pictureBox_1).BeginInit();
			this.groupBox_14.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_8).BeginInit();
			((ISupportInitialize)this.numericUpDown_9).BeginInit();
			this.tabPage_28.SuspendLayout();
			this.groupBox_4.SuspendLayout();
			this.groupBox_5.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_0).BeginInit();
			((ISupportInitialize)this.numericUpDown_1).BeginInit();
			((ISupportInitialize)this.numericUpDown_2).BeginInit();
			((ISupportInitialize)this.numericUpDown_3).BeginInit();
			this.groupBox_6.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_3).BeginInit();
			((ISupportInitialize)this.numericUpDown_4).BeginInit();
			((ISupportInitialize)this.numericUpDown_5).BeginInit();
			((ISupportInitialize)this.numericUpDown_6).BeginInit();
			((ISupportInitialize)this.numericUpDown_7).BeginInit();
			this.tabPage_4.SuspendLayout();
			this.tabControl_3.SuspendLayout();
			this.tabPage_16.SuspendLayout();
			this.groupBox_9.SuspendLayout();
			this.groupBox_8.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_0).BeginInit();
			this.tabPage_26.SuspendLayout();
			this.groupBox_23.SuspendLayout();
			this.tabPage_18.SuspendLayout();
			this.groupBox_15.SuspendLayout();
			this.tabPage_24.SuspendLayout();
			this.groupBox_18.SuspendLayout();
			this.groupBox_19.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_2).BeginInit();
			this.tabPage_50.SuspendLayout();
			this.tabControl_9.SuspendLayout();
			this.tabPage_51.SuspendLayout();
			this.groupBox_68.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_74).BeginInit();
			this.groupBox_61.SuspendLayout();
			this.groupBox_62.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_17).BeginInit();
			((ISupportInitialize)this.numericUpDown_62).BeginInit();
			this.tabPage_54.SuspendLayout();
			this.groupBox_69.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_22).BeginInit();
			((ISupportInitialize)this.numericUpDown_72).BeginInit();
			this.tabPage_52.SuspendLayout();
			this.groupBox_63.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_18).BeginInit();
			this.tabPage_53.SuspendLayout();
			this.groupBox_64.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_19).BeginInit();
			this.groupBox_65.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_20).BeginInit();
			this.groupBox_66.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_21).BeginInit();
			((ISupportInitialize)this.numericUpDown_63).BeginInit();
			((ISupportInitialize)this.numericUpDown_64).BeginInit();
			this.tabPage_5.SuspendLayout();
			this.groupBox_67.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_65).BeginInit();
			((ISupportInitialize)this.numericUpDown_66).BeginInit();
			((ISupportInitialize)this.numericUpDown_67).BeginInit();
			((ISupportInitialize)this.numericUpDown_68).BeginInit();
			((ISupportInitialize)this.numericUpDown_69).BeginInit();
			((ISupportInitialize)this.numericUpDown_70).BeginInit();
			this.tabPage_29.SuspendLayout();
			this.groupBox_50.SuspendLayout();
			this.groupBox_33.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_60).BeginInit();
			((ISupportInitialize)this.numericUpDown_61).BeginInit();
			((ISupportInitialize)this.numericUpDown_57).BeginInit();
			this.groupBox_26.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_4).BeginInit();
			this.tabPage_7.SuspendLayout();
			this.tabControl_1.SuspendLayout();
			this.tabPage_8.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_75).BeginInit();
			this.tabPage_10.SuspendLayout();
			this.tabPage_9.SuspendLayout();
			this.tabPage_11.SuspendLayout();
			this.tabControl_2.SuspendLayout();
			this.tabPage_17.SuspendLayout();
			this.groupBox_20.SuspendLayout();
			this.tabPage_12.SuspendLayout();
			this.groupBox_10.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_73).BeginInit();
			((ISupportInitialize)this.numericUpDown_71).BeginInit();
			((ISupportInitialize)this.numericUpDown_56).BeginInit();
			((ISupportInitialize)this.numericUpDown_52).BeginInit();
			((ISupportInitialize)this.trackBar_0).BeginInit();
			((ISupportInitialize)this.numericUpDown_10).BeginInit();
			((ISupportInitialize)this.numericUpDown_11).BeginInit();
			((ISupportInitialize)this.numericUpDown_12).BeginInit();
			((ISupportInitialize)this.numericUpDown_13).BeginInit();
			((ISupportInitialize)this.numericUpDown_14).BeginInit();
			this.tabPage_13.SuspendLayout();
			this.groupBox_11.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_15).BeginInit();
			((ISupportInitialize)this.numericUpDown_16).BeginInit();
			((ISupportInitialize)this.numericUpDown_17).BeginInit();
			((ISupportInitialize)this.numericUpDown_18).BeginInit();
			((ISupportInitialize)this.numericUpDown_19).BeginInit();
			((ISupportInitialize)this.numericUpDown_20).BeginInit();
			((ISupportInitialize)this.numericUpDown_21).BeginInit();
			((ISupportInitialize)this.numericUpDown_22).BeginInit();
			this.tabPage_14.SuspendLayout();
			this.groupBox_12.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_58).BeginInit();
			((ISupportInitialize)this.numericUpDown_27).BeginInit();
			((ISupportInitialize)this.numericUpDown_28).BeginInit();
			((ISupportInitialize)this.numericUpDown_23).BeginInit();
			((ISupportInitialize)this.numericUpDown_24).BeginInit();
			this.tabPage_15.SuspendLayout();
			this.groupBox_13.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_25).BeginInit();
			this.tabPage_6.SuspendLayout();
			this.groupBox_24.SuspendLayout();
			((ISupportInitialize)this.dataGridView_0).BeginInit();
			((ISupportInitialize)this.pictureBox_0).BeginInit();
			this.groupBox_25.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_35).BeginInit();
			((ISupportInitialize)this.numericUpDown_30).BeginInit();
			((ISupportInitialize)this.numericUpDown_29).BeginInit();
			this.tabPage_21.SuspendLayout();
			this.tabControl_8.SuspendLayout();
			this.tabPage_25.SuspendLayout();
			this.groupBox_21.SuspendLayout();
			((ISupportInitialize)this.dataGridView_1).BeginInit();
			this.groupBox_22.SuspendLayout();
			this.tabPage_34.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_33).BeginInit();
			((ISupportInitialize)this.numericUpDown_34).BeginInit();
			this.groupBox_30.SuspendLayout();
			((ISupportInitialize)this.dataGridView_3).BeginInit();
			this.groupBox_31.SuspendLayout();
			this.tabPage_33.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_31).BeginInit();
			((ISupportInitialize)this.numericUpDown_32).BeginInit();
			this.groupBox_29.SuspendLayout();
			((ISupportInitialize)this.dataGridView_2).BeginInit();
			this.tabPage_22.SuspendLayout();
			this.tabControl_7.SuspendLayout();
			this.tabPage_43.SuspendLayout();
			this.groupBox_46.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_43).BeginInit();
			((ISupportInitialize)this.numericUpDown_44).BeginInit();
			((ISupportInitialize)this.numericUpDown_45).BeginInit();
			((ISupportInitialize)this.fastObjectListView_11).BeginInit();
			this.tabPage_44.SuspendLayout();
			this.groupBox_47.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_12).BeginInit();
			this.tabPage_49.SuspendLayout();
			this.groupBox_60.SuspendLayout();
			this.tabPage_19.SuspendLayout();
			this.tabControl_11.SuspendLayout();
			this.tabPage_35.SuspendLayout();
			this.groupBox_45.SuspendLayout();
			((ISupportInitialize)this.trackBar_1).BeginInit();
			this.groupBox_34.SuspendLayout();
			this.tabPage_36.SuspendLayout();
			this.groupBox_35.SuspendLayout();
			this.groupBox_36.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_6).BeginInit();
			this.groupBox_37.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_7).BeginInit();
			((ISupportInitialize)this.numericUpDown_36).BeginInit();
			this.tabPage_37.SuspendLayout();
			this.groupBox_38.SuspendLayout();
			this.groupBox_39.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_8).BeginInit();
			this.groupBox_40.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_9).BeginInit();
			((ISupportInitialize)this.numericUpDown_37).BeginInit();
			this.tabPage_48.SuspendLayout();
			this.groupBox_59.SuspendLayout();
			this.tabPage_40.SuspendLayout();
			this.groupBox_41.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_38).BeginInit();
			this.tabPage_41.SuspendLayout();
			this.groupBox_44.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_39).BeginInit();
			((ISupportInitialize)this.numericUpDown_40).BeginInit();
			((ISupportInitialize)this.numericUpDown_41).BeginInit();
			((ISupportInitialize)this.numericUpDown_42).BeginInit();
			this.tabPage_20.SuspendLayout();
			this.groupBox_48.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_13).BeginInit();
			this.groupBox_49.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_14).BeginInit();
			this.groupBox_16.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_49).BeginInit();
			((ISupportInitialize)this.numericUpDown_50).BeginInit();
			((ISupportInitialize)this.numericUpDown_51).BeginInit();
			((ISupportInitialize)this.numericUpDown_46).BeginInit();
			((ISupportInitialize)this.numericUpDown_48).BeginInit();
			((ISupportInitialize)this.numericUpDown_47).BeginInit();
			this.tabPage_55.SuspendLayout();
			this.tabControl_10.SuspendLayout();
			this.tabPage_56.SuspendLayout();
			this.groupBox_70.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_23).BeginInit();
			((ISupportInitialize)this.numericUpDown_76).BeginInit();
			this.groupBox_71.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_24).BeginInit();
			this.tabPage_45.SuspendLayout();
			this.groupBox_56.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_54).BeginInit();
			((ISupportInitialize)this.numericUpDown_55).BeginInit();
			this.groupBox_55.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_53).BeginInit();
			this.groupBox_54.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_15).BeginInit();
			this.tabPage_32.SuspendLayout();
			this.groupBox_27.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_5).BeginInit();
			this.groupBox_28.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_59).BeginInit();
			this.tabPage_42.SuspendLayout();
			this.groupBox_43.SuspendLayout();
			this.groupBox_42.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_10).BeginInit();
			this.tabPage_23.SuspendLayout();
			this.groupBox_17.SuspendLayout();
			((ISupportInitialize)this.numericUpDown_26).BeginInit();
			((ISupportInitialize)this.fastObjectListView_1).BeginInit();
			this.tabPage_46.SuspendLayout();
			this.groupBox_57.SuspendLayout();
			this.tabPage_47.SuspendLayout();
			this.groupBox_58.SuspendLayout();
			((ISupportInitialize)this.fastObjectListView_16).BeginInit();
			this.toolStrip_0.SuspendLayout();
			base.SuspendLayout();
			this.tabControl_4.Controls.Add(this.tabPage_0);
			this.tabControl_4.Controls.Add(this.tabPage_1);
			this.tabControl_4.Controls.Add(this.tabPage_6);
			this.tabControl_4.Controls.Add(this.tabPage_21);
			this.tabControl_4.Controls.Add(this.tabPage_22);
			this.tabControl_4.Controls.Add(this.tabPage_19);
			this.tabControl_4.Controls.Add(this.tabPage_55);
			this.tabControl_4.Controls.Add(this.tabPage_45);
			this.tabControl_4.Controls.Add(this.tabPage_32);
			this.tabControl_4.Controls.Add(this.tabPage_42);
			this.tabControl_4.Controls.Add(this.tabPage_23);
			this.tabControl_4.Controls.Add(this.tabPage_46);
			this.tabControl_4.Controls.Add(this.tabPage_47);
			this.tabControl_4.ItemSize = new Size(87, 19);
			this.tabControl_4.Location = new Point(0, 0);
			this.tabControl_4.Margin = new Padding(5, 3, 5, 3);
			this.tabControl_4.Multiline = true;
			this.tabControl_4.Name = MainForm.getString_0(107389005);
			this.tabControl_4.SelectedIndex = 0;
			this.tabControl_4.Size = new Size(528, 589);
			this.tabControl_4.SizeMode = TabSizeMode.FillToRight;
			this.tabControl_4.TabIndex = 0;
			this.tabControl_4.SelectedIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_4.TabIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_4.Leave += this.MainForm_Leave;
			this.tabPage_0.Controls.Add(this.button_95);
			this.tabPage_0.Controls.Add(this.button_90);
			this.tabPage_0.Controls.Add(this.button_78);
			this.tabPage_0.Controls.Add(this.textBox_17);
			this.tabPage_0.Controls.Add(this.comboBox_56);
			this.tabPage_0.Controls.Add(this.comboBox_57);
			this.tabPage_0.Controls.Add(this.button_67);
			this.tabPage_0.Controls.Add(this.button_65);
			this.tabPage_0.Controls.Add(this.button_59);
			this.tabPage_0.Controls.Add(this.button_60);
			this.tabPage_0.Controls.Add(this.button_61);
			this.tabPage_0.Controls.Add(this.button_56);
			this.tabPage_0.Controls.Add(this.tabControl_6);
			this.tabPage_0.Controls.Add(this.button_2);
			this.tabPage_0.Controls.Add(this.button_1);
			this.tabPage_0.Controls.Add(this.groupBox_0);
			this.tabPage_0.Controls.Add(this.button_0);
			this.tabPage_0.Location = new Point(4, 42);
			this.tabPage_0.Margin = new Padding(5, 3, 5, 3);
			this.tabPage_0.Name = MainForm.getString_0(107389016);
			this.tabPage_0.Padding = new Padding(5, 3, 5, 3);
			this.tabPage_0.Size = new Size(520, 543);
			this.tabPage_0.TabIndex = 0;
			this.tabPage_0.Text = MainForm.getString_0(107388971);
			this.tabPage_0.UseVisualStyleBackColor = true;
			this.button_95.Location = new Point(167, 516);
			this.button_95.Name = MainForm.getString_0(107388962);
			this.button_95.Size = new Size(24, 24);
			this.button_95.TabIndex = 40;
			this.button_95.Text = MainForm.getString_0(107389453);
			this.button_95.UseVisualStyleBackColor = true;
			this.button_95.Click += this.button_95_Click;
			this.button_90.Location = new Point(179, 152);
			this.button_90.Name = MainForm.getString_0(107389448);
			this.button_90.Size = new Size(66, 23);
			this.button_90.TabIndex = 39;
			this.button_90.Text = MainForm.getString_0(107393328);
			this.button_90.UseVisualStyleBackColor = true;
			this.button_90.Click += this.button_90_Click;
			this.button_78.Location = new Point(3, 89);
			this.button_78.Name = MainForm.getString_0(107389467);
			this.button_78.Size = new Size(87, 24);
			this.button_78.TabIndex = 38;
			this.button_78.Text = MainForm.getString_0(107389422);
			this.button_78.UseVisualStyleBackColor = true;
			this.button_78.Click += this.button_78_Click;
			this.textBox_17.Location = new Point(193, 518);
			this.textBox_17.Name = MainForm.getString_0(107389437);
			this.textBox_17.Size = new Size(324, 20);
			this.textBox_17.TabIndex = 37;
			this.textBox_17.Text = MainForm.getString_0(107397031);
			this.textBox_17.Enter += this.textBox_17_Enter;
			this.textBox_17.KeyPress += this.textBox_17_KeyPress;
			this.textBox_17.Leave += this.textBox_17_Leave;
			this.textBox_17.PreviewKeyDown += this.textBox_17_PreviewKeyDown;
			this.comboBox_56.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_56.FormattingEnabled = true;
			this.comboBox_56.Location = new Point(81, 517);
			this.comboBox_56.Name = MainForm.getString_0(107389392);
			this.comboBox_56.Size = new Size(84, 22);
			this.comboBox_56.TabIndex = 36;
			this.comboBox_56.SelectedIndexChanged += this.comboBox_56_SelectedIndexChanged;
			this.comboBox_57.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_57.FormattingEnabled = true;
			this.comboBox_57.Items.AddRange(new object[]
			{
				MainForm.getString_0(107397085),
				MainForm.getString_0(107397102),
				MainForm.getString_0(107397093),
				MainForm.getString_0(107397116),
				MainForm.getString_0(107397071),
				MainForm.getString_0(107397062)
			});
			this.comboBox_57.Location = new Point(4, 517);
			this.comboBox_57.Name = MainForm.getString_0(107389399);
			this.comboBox_57.Size = new Size(71, 22);
			this.comboBox_57.TabIndex = 35;
			this.comboBox_57.SelectedIndexChanged += this.comboBox_57_SelectedIndexChanged;
			this.button_67.Location = new Point(111, 152);
			this.button_67.Name = MainForm.getString_0(107389346);
			this.button_67.Size = new Size(66, 23);
			this.button_67.TabIndex = 34;
			this.button_67.Text = MainForm.getString_0(107397209);
			this.button_67.UseVisualStyleBackColor = true;
			this.button_67.Visible = false;
			this.button_67.Click += this.button_67_Click;
			this.button_65.Enabled = false;
			this.button_65.Location = new Point(3, 34);
			this.button_65.Name = MainForm.getString_0(107389365);
			this.button_65.Size = new Size(87, 24);
			this.button_65.TabIndex = 33;
			this.button_65.Text = MainForm.getString_0(107389320);
			this.button_65.UseVisualStyleBackColor = true;
			this.button_65.Click += this.button_65_Click;
			this.button_59.Location = new Point(383, 152);
			this.button_59.Name = MainForm.getString_0(107389343);
			this.button_59.Size = new Size(66, 23);
			this.button_59.TabIndex = 32;
			this.button_59.Text = MainForm.getString_0(107389294);
			this.button_59.UseVisualStyleBackColor = true;
			this.button_59.Click += this.button_59_Click;
			this.button_60.Location = new Point(315, 152);
			this.button_60.Name = MainForm.getString_0(107389289);
			this.button_60.Size = new Size(66, 23);
			this.button_60.TabIndex = 31;
			this.button_60.Text = MainForm.getString_0(107389300);
			this.button_60.UseVisualStyleBackColor = true;
			this.button_60.Click += this.button_60_Click;
			this.button_61.Location = new Point(247, 152);
			this.button_61.Name = MainForm.getString_0(107389255);
			this.button_61.Size = new Size(66, 23);
			this.button_61.TabIndex = 30;
			this.button_61.Text = MainForm.getString_0(107389270);
			this.button_61.UseVisualStyleBackColor = true;
			this.button_61.Click += this.button_61_Click;
			this.button_56.Enabled = false;
			this.button_56.Location = new Point(3, 116);
			this.button_56.Name = MainForm.getString_0(107389229);
			this.button_56.Size = new Size(87, 37);
			this.button_56.TabIndex = 29;
			this.button_56.Text = MainForm.getString_0(107389236);
			this.button_56.UseVisualStyleBackColor = true;
			this.button_56.Click += this.button_56_Click;
			this.tabControl_6.Controls.Add(this.tabPage_30);
			this.tabControl_6.Controls.Add(this.tabPage_31);
			this.tabControl_6.Location = new Point(4, 152);
			this.tabControl_6.Name = MainForm.getString_0(107388699);
			this.tabControl_6.SelectedIndex = 0;
			this.tabControl_6.Size = new Size(515, 362);
			this.tabControl_6.TabIndex = 28;
			this.tabControl_6.SelectedIndexChanged += this.tabControl_6_SelectedIndexChanged;
			this.tabPage_30.Controls.Add(this.richTextBox_0);
			this.tabPage_30.Location = new Point(4, 23);
			this.tabPage_30.Name = MainForm.getString_0(107388690);
			this.tabPage_30.Padding = new Padding(3);
			this.tabPage_30.Size = new Size(507, 335);
			this.tabPage_30.TabIndex = 0;
			this.tabPage_30.Text = MainForm.getString_0(107389294);
			this.tabPage_30.UseVisualStyleBackColor = true;
			this.richTextBox_0.Anchor = AnchorStyles.None;
			this.richTextBox_0.BackColor = Color.WhiteSmoke;
			this.richTextBox_0.BorderStyle = BorderStyle.FixedSingle;
			this.richTextBox_0.Font = new Font(MainForm.getString_0(107397254), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox_0.ForeColor = SystemColors.ActiveCaptionText;
			this.richTextBox_0.HideSelection = false;
			this.richTextBox_0.Location = new Point(0, 0);
			this.richTextBox_0.Name = MainForm.getString_0(107388645);
			this.richTextBox_0.ReadOnly = true;
			this.richTextBox_0.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.richTextBox_0.Size = new Size(507, 335);
			this.richTextBox_0.TabIndex = 6;
			this.richTextBox_0.Text = MainForm.getString_0(107396269);
			this.tabPage_31.Controls.Add(this.richTextBox_1);
			this.tabPage_31.Location = new Point(4, 23);
			this.tabPage_31.Name = MainForm.getString_0(107388668);
			this.tabPage_31.Padding = new Padding(3);
			this.tabPage_31.Size = new Size(507, 335);
			this.tabPage_31.TabIndex = 1;
			this.tabPage_31.Text = MainForm.getString_0(107388623);
			this.tabPage_31.UseVisualStyleBackColor = true;
			this.richTextBox_1.Anchor = AnchorStyles.None;
			this.richTextBox_1.BackColor = Color.Black;
			this.richTextBox_1.BorderStyle = BorderStyle.FixedSingle;
			this.richTextBox_1.Font = new Font(MainForm.getString_0(107397254), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox_1.ForeColor = SystemColors.ActiveCaptionText;
			this.richTextBox_1.HideSelection = false;
			this.richTextBox_1.Location = new Point(0, 0);
			this.richTextBox_1.Name = MainForm.getString_0(107388614);
			this.richTextBox_1.ReadOnly = true;
			this.richTextBox_1.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.richTextBox_1.Size = new Size(507, 335);
			this.richTextBox_1.TabIndex = 7;
			this.richTextBox_1.Text = MainForm.getString_0(107396269);
			this.button_2.Location = new Point(3, 62);
			this.button_2.Name = MainForm.getString_0(107388637);
			this.button_2.Size = new Size(87, 24);
			this.button_2.TabIndex = 3;
			this.button_2.Text = MainForm.getString_0(107388584);
			this.button_2.UseVisualStyleBackColor = true;
			this.button_2.Click += this.button_2_Click;
			this.button_1.Location = new Point(3, 6);
			this.button_1.Name = MainForm.getString_0(107388603);
			this.button_1.Size = new Size(87, 26);
			this.button_1.TabIndex = 1;
			this.button_1.Text = MainForm.getString_0(107388558);
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			this.groupBox_0.Controls.Add(this.label_132);
			this.groupBox_0.Controls.Add(this.label_133);
			this.groupBox_0.Controls.Add(this.label_134);
			this.groupBox_0.Controls.Add(this.label_135);
			this.groupBox_0.Controls.Add(this.label_136);
			this.groupBox_0.Controls.Add(this.label_137);
			this.groupBox_0.Controls.Add(this.label_98);
			this.groupBox_0.Controls.Add(this.label_99);
			this.groupBox_0.Controls.Add(this.groupBox_2);
			this.groupBox_0.Controls.Add(this.groupBox_1);
			this.groupBox_0.Controls.Add(this.label_4);
			this.groupBox_0.Controls.Add(this.label_5);
			this.groupBox_0.Controls.Add(this.label_6);
			this.groupBox_0.Controls.Add(this.label_147);
			this.groupBox_0.Controls.Add(this.label_148);
			this.groupBox_0.Controls.Add(this.label_149);
			this.groupBox_0.Location = new Point(96, 6);
			this.groupBox_0.Name = MainForm.getString_0(107388549);
			this.groupBox_0.Size = new Size(421, 147);
			this.groupBox_0.TabIndex = 0;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Text = MainForm.getString_0(107388568);
			this.label_132.AutoSize = true;
			this.label_132.Location = new Point(355, 84);
			this.label_132.Name = MainForm.getString_0(107388527);
			this.label_132.Size = new Size(13, 14);
			this.label_132.TabIndex = 21;
			this.label_132.Text = MainForm.getString_0(107397231);
			this.label_133.AutoSize = true;
			this.label_133.Location = new Point(214, 84);
			this.label_133.Name = MainForm.getString_0(107388534);
			this.label_133.Size = new Size(139, 14);
			this.label_133.TabIndex = 20;
			this.label_133.Text = MainForm.getString_0(107388489);
			this.label_134.AutoSize = true;
			this.label_134.Location = new Point(355, 112);
			this.label_134.Name = MainForm.getString_0(107388456);
			this.label_134.Size = new Size(13, 14);
			this.label_134.TabIndex = 19;
			this.label_134.Text = MainForm.getString_0(107397231);
			this.label_135.AutoSize = true;
			this.label_135.Location = new Point(355, 98);
			this.label_135.Name = MainForm.getString_0(107388467);
			this.label_135.Size = new Size(13, 14);
			this.label_135.TabIndex = 18;
			this.label_135.Text = MainForm.getString_0(107397231);
			this.label_136.AutoSize = true;
			this.label_136.Location = new Point(214, 98);
			this.label_136.Name = MainForm.getString_0(107388958);
			this.label_136.Size = new Size(116, 14);
			this.label_136.TabIndex = 17;
			this.label_136.Text = MainForm.getString_0(107388945);
			this.label_137.AutoSize = true;
			this.label_137.Location = new Point(214, 112);
			this.label_137.Name = MainForm.getString_0(107388916);
			this.label_137.Size = new Size(115, 14);
			this.label_137.TabIndex = 16;
			this.label_137.Text = MainForm.getString_0(107388871);
			this.label_98.AutoSize = true;
			this.label_98.Location = new Point(147, 84);
			this.label_98.Name = MainForm.getString_0(107388842);
			this.label_98.Size = new Size(13, 14);
			this.label_98.TabIndex = 15;
			this.label_98.Text = MainForm.getString_0(107397231);
			this.label_99.AutoSize = true;
			this.label_99.Location = new Point(6, 84);
			this.label_99.Name = MainForm.getString_0(107388853);
			this.label_99.Size = new Size(96, 14);
			this.label_99.TabIndex = 14;
			this.label_99.Text = MainForm.getString_0(107388808);
			this.groupBox_2.Controls.Add(this.label_7);
			this.groupBox_2.Controls.Add(this.label_8);
			this.groupBox_2.Controls.Add(this.label_9);
			this.groupBox_2.Location = new Point(217, 12);
			this.groupBox_2.Name = MainForm.getString_0(107388819);
			this.groupBox_2.Size = new Size(200, 63);
			this.groupBox_2.TabIndex = 11;
			this.groupBox_2.TabStop = false;
			this.groupBox_2.Text = MainForm.getString_0(107388798);
			this.label_7.AutoSize = true;
			this.label_7.Location = new Point(6, 46);
			this.label_7.Name = MainForm.getString_0(107388745);
			this.label_7.Size = new Size(0, 14);
			this.label_7.TabIndex = 10;
			this.label_8.AutoSize = true;
			this.label_8.Location = new Point(6, 30);
			this.label_8.Name = MainForm.getString_0(107388716);
			this.label_8.Size = new Size(0, 14);
			this.label_8.TabIndex = 9;
			this.label_9.AutoSize = true;
			this.label_9.Location = new Point(6, 14);
			this.label_9.Name = MainForm.getString_0(107388723);
			this.label_9.Size = new Size(35, 14);
			this.label_9.TabIndex = 4;
			this.label_9.Text = MainForm.getString_0(107396375);
			this.groupBox_1.Controls.Add(this.label_1);
			this.groupBox_1.Controls.Add(this.label_2);
			this.groupBox_1.Controls.Add(this.label_3);
			this.groupBox_1.Location = new Point(6, 12);
			this.groupBox_1.Name = MainForm.getString_0(107388182);
			this.groupBox_1.Size = new Size(200, 63);
			this.groupBox_1.TabIndex = 8;
			this.groupBox_1.TabStop = false;
			this.groupBox_1.Text = MainForm.getString_0(107388133);
			this.label_1.AutoSize = true;
			this.label_1.Location = new Point(6, 46);
			this.label_1.Name = MainForm.getString_0(107388112);
			this.label_1.Size = new Size(0, 14);
			this.label_1.TabIndex = 10;
			this.label_2.AutoSize = true;
			this.label_2.Location = new Point(6, 30);
			this.label_2.Name = MainForm.getString_0(107388119);
			this.label_2.Size = new Size(0, 14);
			this.label_2.TabIndex = 9;
			this.label_3.AutoSize = true;
			this.label_3.Location = new Point(6, 14);
			this.label_3.Name = MainForm.getString_0(107388066);
			this.label_3.Size = new Size(35, 14);
			this.label_3.TabIndex = 4;
			this.label_3.Text = MainForm.getString_0(107396375);
			this.label_4.AutoSize = true;
			this.label_4.Location = new Point(147, 126);
			this.label_4.Name = MainForm.getString_0(107388041);
			this.label_4.Size = new Size(13, 14);
			this.label_4.TabIndex = 7;
			this.label_4.Text = MainForm.getString_0(107397231);
			this.label_5.AutoSize = true;
			this.label_5.Location = new Point(147, 112);
			this.label_5.Name = MainForm.getString_0(107388052);
			this.label_5.Size = new Size(13, 14);
			this.label_5.TabIndex = 6;
			this.label_5.Text = MainForm.getString_0(107397231);
			this.label_6.AutoSize = true;
			this.label_6.Location = new Point(147, 98);
			this.label_6.Name = MainForm.getString_0(107388027);
			this.label_6.Size = new Size(13, 14);
			this.label_6.TabIndex = 5;
			this.label_6.Text = MainForm.getString_0(107397231);
			this.label_147.AutoSize = true;
			this.label_147.Location = new Point(6, 98);
			this.label_147.Name = MainForm.getString_0(107387970);
			this.label_147.Size = new Size(109, 14);
			this.label_147.TabIndex = 3;
			this.label_147.Text = MainForm.getString_0(107387993);
			this.label_148.AutoSize = true;
			this.label_148.Location = new Point(6, 126);
			this.label_148.Name = MainForm.getString_0(107387968);
			this.label_148.Size = new Size(81, 14);
			this.label_148.TabIndex = 2;
			this.label_148.Text = MainForm.getString_0(107387959);
			this.label_149.AutoSize = true;
			this.label_149.Location = new Point(6, 112);
			this.label_149.Name = MainForm.getString_0(107388418);
			this.label_149.Size = new Size(110, 14);
			this.label_149.TabIndex = 1;
			this.label_149.Text = MainForm.getString_0(107388441);
			this.button_0.Location = new Point(451, 152);
			this.button_0.Name = MainForm.getString_0(107388416);
			this.button_0.Size = new Size(66, 23);
			this.button_0.TabIndex = 5;
			this.button_0.Text = MainForm.getString_0(107388403);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.tabPage_1.Controls.Add(this.tabControl_0);
			this.tabPage_1.Location = new Point(4, 42);
			this.tabPage_1.Name = MainForm.getString_0(107388362);
			this.tabPage_1.Size = new Size(520, 543);
			this.tabPage_1.TabIndex = 3;
			this.tabPage_1.Text = MainForm.getString_0(107388377);
			this.tabPage_1.UseVisualStyleBackColor = true;
			this.tabControl_0.Controls.Add(this.tabPage_2);
			this.tabControl_0.Controls.Add(this.tabPage_3);
			this.tabControl_0.Controls.Add(this.tabPage_4);
			this.tabControl_0.Controls.Add(this.tabPage_50);
			this.tabControl_0.Controls.Add(this.tabPage_5);
			this.tabControl_0.Controls.Add(this.tabPage_29);
			this.tabControl_0.Controls.Add(this.tabPage_7);
			this.tabControl_0.Controls.Add(this.tabPage_11);
			this.tabControl_0.Location = new Point(1, 0);
			this.tabControl_0.Name = MainForm.getString_0(107388332);
			this.tabControl_0.SelectedIndex = 0;
			this.tabControl_0.Size = new Size(517, 542);
			this.tabControl_0.TabIndex = 1;
			this.tabControl_0.SelectedIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_0.TabIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_0.Leave += this.MainForm_Leave;
			this.tabPage_2.Controls.Add(this.groupBox_53);
			this.tabPage_2.Controls.Add(this.groupBox_32);
			this.tabPage_2.Controls.Add(this.groupBox_3);
			this.tabPage_2.Location = new Point(4, 23);
			this.tabPage_2.Name = MainForm.getString_0(107388339);
			this.tabPage_2.Size = new Size(509, 515);
			this.tabPage_2.TabIndex = 13;
			this.tabPage_2.Text = MainForm.getString_0(107388294);
			this.tabPage_2.UseVisualStyleBackColor = true;
			this.groupBox_53.Controls.Add(this.label_140);
			this.groupBox_53.Controls.Add(this.trackBar_2);
			this.groupBox_53.Controls.Add(this.checkBox_22);
			this.groupBox_53.Location = new Point(3, 312);
			this.groupBox_53.Name = MainForm.getString_0(107388317);
			this.groupBox_53.Size = new Size(503, 109);
			this.groupBox_53.TabIndex = 27;
			this.groupBox_53.TabStop = false;
			this.groupBox_53.Text = MainForm.getString_0(107388268);
			this.label_140.AutoSize = true;
			this.label_140.Location = new Point(6, 40);
			this.label_140.Name = MainForm.getString_0(107388283);
			this.label_140.Size = new Size(142, 14);
			this.label_140.TabIndex = 56;
			this.label_140.Text = MainForm.getString_0(107388238);
			this.trackBar_2.LargeChange = 1;
			this.trackBar_2.Location = new Point(9, 57);
			this.trackBar_2.Minimum = 1;
			this.trackBar_2.Name = MainForm.getString_0(107388205);
			this.trackBar_2.Size = new Size(230, 45);
			this.trackBar_2.TabIndex = 55;
			this.trackBar_2.Value = 10;
			this.trackBar_2.ValueChanged += this.MainForm_Leave;
			this.trackBar_2.Leave += this.MainForm_Leave;
			this.checkBox_22.AutoSize = true;
			this.checkBox_22.Location = new Point(6, 19);
			this.checkBox_22.Name = MainForm.getString_0(107388216);
			this.checkBox_22.Size = new Size(139, 18);
			this.checkBox_22.TabIndex = 24;
			this.checkBox_22.Text = MainForm.getString_0(107387679);
			this.checkBox_22.UseVisualStyleBackColor = true;
			this.checkBox_22.CheckedChanged += this.checkBox_22_CheckedChanged;
			this.checkBox_22.Leave += this.MainForm_Leave;
			this.groupBox_32.Controls.Add(this.button_47);
			this.groupBox_32.Controls.Add(this.checkBox_30);
			this.groupBox_32.Controls.Add(this.button_33);
			this.groupBox_32.Location = new Point(3, 427);
			this.groupBox_32.Name = MainForm.getString_0(107387618);
			this.groupBox_32.Size = new Size(503, 85);
			this.groupBox_32.TabIndex = 26;
			this.groupBox_32.TabStop = false;
			this.groupBox_32.Text = MainForm.getString_0(107387633);
			this.button_47.Location = new Point(406, 14);
			this.button_47.Name = MainForm.getString_0(107387588);
			this.button_47.Size = new Size(91, 26);
			this.button_47.TabIndex = 25;
			this.button_47.Text = MainForm.getString_0(107387607);
			this.button_47.UseVisualStyleBackColor = true;
			this.button_47.Click += this.button_47_Click;
			this.checkBox_30.AutoSize = true;
			this.checkBox_30.Location = new Point(9, 19);
			this.checkBox_30.Name = MainForm.getString_0(107387566);
			this.checkBox_30.Size = new Size(204, 18);
			this.checkBox_30.TabIndex = 24;
			this.checkBox_30.Text = MainForm.getString_0(107387573);
			this.checkBox_30.UseVisualStyleBackColor = true;
			this.button_33.Location = new Point(406, 53);
			this.button_33.Name = MainForm.getString_0(107387500);
			this.button_33.Size = new Size(91, 26);
			this.button_33.TabIndex = 21;
			this.button_33.Text = MainForm.getString_0(107387519);
			this.button_33.UseVisualStyleBackColor = true;
			this.button_33.Click += this.button_33_Click;
			this.groupBox_3.Controls.Add(this.checkBox_62);
			this.groupBox_3.Controls.Add(this.label_177);
			this.groupBox_3.Controls.Add(this.comboBox_70);
			this.groupBox_3.Controls.Add(this.label_163);
			this.groupBox_3.Controls.Add(this.comboBox_66);
			this.groupBox_3.Controls.Add(this.label_159);
			this.groupBox_3.Controls.Add(this.textBox_18);
			this.groupBox_3.Controls.Add(this.comboBox_65);
			this.groupBox_3.Controls.Add(this.label_160);
			this.groupBox_3.Controls.Add(this.button_72);
			this.groupBox_3.Controls.Add(this.pictureBox_2);
			this.groupBox_3.Controls.Add(this.textBox_8);
			this.groupBox_3.Controls.Add(this.label_83);
			this.groupBox_3.Controls.Add(this.button_17);
			this.groupBox_3.Controls.Add(this.checkBox_15);
			this.groupBox_3.Controls.Add(this.checkBox_18);
			this.groupBox_3.Controls.Add(this.comboBox_10);
			this.groupBox_3.Controls.Add(this.label_10);
			this.groupBox_3.Controls.Add(this.comboBox_11);
			this.groupBox_3.Controls.Add(this.label_11);
			this.groupBox_3.Location = new Point(3, 3);
			this.groupBox_3.Name = MainForm.getString_0(107387466);
			this.groupBox_3.Size = new Size(503, 303);
			this.groupBox_3.TabIndex = 0;
			this.groupBox_3.TabStop = false;
			this.groupBox_3.Text = MainForm.getString_0(107388294);
			this.checkBox_62.AutoSize = true;
			this.checkBox_62.Location = new Point(9, 189);
			this.checkBox_62.Name = MainForm.getString_0(107387481);
			this.checkBox_62.Size = new Size(218, 18);
			this.checkBox_62.TabIndex = 65;
			this.checkBox_62.Text = MainForm.getString_0(107387428);
			this.checkBox_62.UseVisualStyleBackColor = true;
			this.checkBox_62.Leave += this.MainForm_Leave;
			this.label_177.AutoSize = true;
			this.label_177.Location = new Point(6, 234);
			this.label_177.Name = MainForm.getString_0(107387927);
			this.label_177.Size = new Size(133, 14);
			this.label_177.TabIndex = 64;
			this.label_177.Text = MainForm.getString_0(107387882);
			this.comboBox_70.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_70.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_70.FormattingEnabled = true;
			this.comboBox_70.Location = new Point(139, 231);
			this.comboBox_70.Name = MainForm.getString_0(107387849);
			this.comboBox_70.Size = new Size(122, 21);
			this.comboBox_70.TabIndex = 63;
			this.comboBox_70.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_70.Leave += this.MainForm_Leave;
			this.label_163.AutoSize = true;
			this.label_163.Location = new Point(6, 47);
			this.label_163.Name = MainForm.getString_0(107387864);
			this.label_163.Size = new Size(77, 14);
			this.label_163.TabIndex = 62;
			this.label_163.Text = MainForm.getString_0(107387819);
			this.comboBox_66.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_66.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_66.FormattingEnabled = true;
			this.comboBox_66.Items.AddRange(new object[]
			{
				MainForm.getString_0(107394917),
				MainForm.getString_0(107394221),
				MainForm.getString_0(107394565)
			});
			this.comboBox_66.Location = new Point(89, 44);
			this.comboBox_66.Name = MainForm.getString_0(107387834);
			this.comboBox_66.Size = new Size(160, 21);
			this.comboBox_66.TabIndex = 61;
			this.comboBox_66.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_66.Leave += this.MainForm_Leave;
			this.label_159.AutoSize = true;
			this.label_159.Location = new Point(6, 280);
			this.label_159.Name = MainForm.getString_0(107387785);
			this.label_159.Size = new Size(97, 14);
			this.label_159.TabIndex = 60;
			this.label_159.Text = MainForm.getString_0(107387804);
			this.textBox_18.Location = new Point(109, 277);
			this.textBox_18.Name = MainForm.getString_0(107387751);
			this.textBox_18.Size = new Size(213, 20);
			this.textBox_18.TabIndex = 59;
			this.textBox_18.Leave += this.MainForm_Leave;
			this.comboBox_65.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_65.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_65.FormattingEnabled = true;
			this.comboBox_65.Items.AddRange(new object[]
			{
				MainForm.getString_0(107387766),
				MainForm.getString_0(107387725),
				MainForm.getString_0(107387744)
			});
			this.comboBox_65.Location = new Point(118, 131);
			this.comboBox_65.Name = MainForm.getString_0(107387695);
			this.comboBox_65.Size = new Size(176, 21);
			this.comboBox_65.TabIndex = 58;
			this.comboBox_65.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_65.Leave += this.MainForm_Leave;
			this.label_160.AutoSize = true;
			this.label_160.Location = new Point(6, 134);
			this.label_160.Name = MainForm.getString_0(107387702);
			this.label_160.Size = new Size(108, 14);
			this.label_160.TabIndex = 57;
			this.label_160.Text = MainForm.getString_0(107387145);
			this.button_72.Location = new Point(380, 107);
			this.button_72.Name = MainForm.getString_0(107397190);
			this.button_72.Size = new Size(117, 45);
			this.button_72.TabIndex = 56;
			this.button_72.Text = MainForm.getString_0(107387120);
			this.button_72.UseVisualStyleBackColor = true;
			this.button_72.Click += this.button_72_Click;
			this.pictureBox_2.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107387083));
			this.pictureBox_2.InitialImage = null;
			this.pictureBox_2.Location = new Point(300, 100);
			this.pictureBox_2.Name = MainForm.getString_0(107387046);
			this.pictureBox_2.Size = new Size(25, 25);
			this.pictureBox_2.TabIndex = 55;
			this.pictureBox_2.TabStop = false;
			this.pictureBox_2.Visible = false;
			this.textBox_8.Location = new Point(89, 20);
			this.textBox_8.Name = MainForm.getString_0(107387017);
			this.textBox_8.ReadOnly = true;
			this.textBox_8.Size = new Size(408, 20);
			this.textBox_8.TabIndex = 23;
			this.label_83.AutoSize = true;
			this.label_83.Location = new Point(6, 23);
			this.label_83.Name = MainForm.getString_0(107387032);
			this.label_83.Size = new Size(69, 14);
			this.label_83.TabIndex = 22;
			this.label_83.Text = MainForm.getString_0(107386987);
			this.button_17.Location = new Point(380, 44);
			this.button_17.Name = MainForm.getString_0(107387002);
			this.button_17.Size = new Size(117, 23);
			this.button_17.TabIndex = 21;
			this.button_17.Text = MainForm.getString_0(107386945);
			this.button_17.UseVisualStyleBackColor = true;
			this.button_17.Click += this.button_17_Click;
			this.checkBox_15.AutoSize = true;
			this.checkBox_15.Location = new Point(9, 213);
			this.checkBox_15.Name = MainForm.getString_0(107386920);
			this.checkBox_15.Size = new Size(260, 18);
			this.checkBox_15.TabIndex = 20;
			this.checkBox_15.Text = MainForm.getString_0(107387407);
			this.checkBox_15.UseVisualStyleBackColor = true;
			this.checkBox_15.Leave += this.MainForm_Leave;
			this.checkBox_18.AutoSize = true;
			this.checkBox_18.Location = new Point(9, 165);
			this.checkBox_18.Name = MainForm.getString_0(107387382);
			this.checkBox_18.Size = new Size(163, 18);
			this.checkBox_18.TabIndex = 17;
			this.checkBox_18.Text = MainForm.getString_0(107387329);
			this.checkBox_18.UseVisualStyleBackColor = true;
			this.checkBox_18.Leave += this.MainForm_Leave;
			this.comboBox_10.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_10.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_10.FormattingEnabled = true;
			this.comboBox_10.Location = new Point(118, 104);
			this.comboBox_10.Name = MainForm.getString_0(107387328);
			this.comboBox_10.Size = new Size(176, 21);
			this.comboBox_10.TabIndex = 4;
			this.comboBox_10.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_10.SelectedIndexChanged += this.comboBox_11_SelectedIndexChanged;
			this.comboBox_10.Leave += this.MainForm_Leave;
			this.label_10.AutoSize = true;
			this.label_10.Location = new Point(6, 107);
			this.label_10.Name = MainForm.getString_0(107387279);
			this.label_10.Size = new Size(88, 14);
			this.label_10.TabIndex = 3;
			this.label_10.Text = MainForm.getString_0(107387266);
			this.comboBox_11.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_11.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_11.FormattingEnabled = true;
			this.comboBox_11.Location = new Point(118, 77);
			this.comboBox_11.Name = MainForm.getString_0(107387281);
			this.comboBox_11.Size = new Size(176, 21);
			this.comboBox_11.TabIndex = 2;
			this.comboBox_11.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_11.SelectedIndexChanged += this.comboBox_11_SelectedIndexChanged;
			this.comboBox_11.Leave += this.MainForm_Leave;
			this.label_11.AutoSize = true;
			this.label_11.Location = new Point(6, 80);
			this.label_11.Name = MainForm.getString_0(107387260);
			this.label_11.Size = new Size(69, 14);
			this.label_11.TabIndex = 0;
			this.label_11.Text = MainForm.getString_0(107387215);
			this.tabPage_3.Controls.Add(this.tabControl_5);
			this.tabPage_3.Location = new Point(4, 23);
			this.tabPage_3.Name = MainForm.getString_0(107387230);
			this.tabPage_3.Size = new Size(509, 515);
			this.tabPage_3.TabIndex = 5;
			this.tabPage_3.Text = MainForm.getString_0(107387217);
			this.tabPage_3.UseVisualStyleBackColor = true;
			this.tabControl_5.Controls.Add(this.tabPage_27);
			this.tabControl_5.Controls.Add(this.tabPage_28);
			this.tabControl_5.Location = new Point(3, 3);
			this.tabControl_5.Name = MainForm.getString_0(107387172);
			this.tabControl_5.SelectedIndex = 0;
			this.tabControl_5.Size = new Size(503, 509);
			this.tabControl_5.TabIndex = 3;
			this.tabPage_27.Controls.Add(this.groupBox_7);
			this.tabPage_27.Location = new Point(4, 23);
			this.tabPage_27.Name = MainForm.getString_0(107387187);
			this.tabPage_27.Padding = new Padding(3);
			this.tabPage_27.Size = new Size(495, 482);
			this.tabPage_27.TabIndex = 0;
			this.tabPage_27.Text = MainForm.getString_0(107386630);
			this.tabPage_27.UseVisualStyleBackColor = true;
			this.groupBox_7.Controls.Add(this.checkBox_80);
			this.groupBox_7.Controls.Add(this.checkBox_49);
			this.groupBox_7.Controls.Add(this.groupBox_52);
			this.groupBox_7.Controls.Add(this.groupBox_51);
			this.groupBox_7.Controls.Add(this.pictureBox_1);
			this.groupBox_7.Controls.Add(this.button_26);
			this.groupBox_7.Controls.Add(this.button_25);
			this.groupBox_7.Controls.Add(this.button_3);
			this.groupBox_7.Controls.Add(this.groupBox_14);
			this.groupBox_7.Controls.Add(this.label_17);
			this.groupBox_7.Controls.Add(this.textBox_0);
			this.groupBox_7.Controls.Add(this.label_18);
			this.groupBox_7.Controls.Add(this.textBox_1);
			this.groupBox_7.Controls.Add(this.comboBox_0);
			this.groupBox_7.Controls.Add(this.label_19);
			this.groupBox_7.Controls.Add(this.checkBox_2);
			this.groupBox_7.Controls.Add(this.label_20);
			this.groupBox_7.Controls.Add(this.textBox_3);
			this.groupBox_7.Controls.Add(this.numericUpDown_8);
			this.groupBox_7.Controls.Add(this.numericUpDown_9);
			this.groupBox_7.Controls.Add(this.checkBox_3);
			this.groupBox_7.Controls.Add(this.label_21);
			this.groupBox_7.Controls.Add(this.comboBox_1);
			this.groupBox_7.Controls.Add(this.label_22);
			this.groupBox_7.Controls.Add(this.label_23);
			this.groupBox_7.Controls.Add(this.checkBox_4);
			this.groupBox_7.Controls.Add(this.checkBox_5);
			this.groupBox_7.Controls.Add(this.label_24);
			this.groupBox_7.Controls.Add(this.label_25);
			this.groupBox_7.Controls.Add(this.textBox_4);
			this.groupBox_7.Controls.Add(this.textBox_5);
			this.groupBox_7.Location = new Point(6, 6);
			this.groupBox_7.Name = MainForm.getString_0(107386653);
			this.groupBox_7.Size = new Size(483, 470);
			this.groupBox_7.TabIndex = 1;
			this.groupBox_7.TabStop = false;
			this.groupBox_7.Text = MainForm.getString_0(107387217);
			this.checkBox_80.AutoSize = true;
			this.checkBox_80.Location = new Point(6, 301);
			this.checkBox_80.Name = MainForm.getString_0(107386608);
			this.checkBox_80.Size = new Size(172, 18);
			this.checkBox_80.TabIndex = 58;
			this.checkBox_80.Text = MainForm.getString_0(107386611);
			this.checkBox_80.UseVisualStyleBackColor = true;
			this.checkBox_80.Leave += this.MainForm_Leave;
			this.checkBox_49.AutoSize = true;
			this.checkBox_49.Location = new Point(6, 277);
			this.checkBox_49.Name = MainForm.getString_0(107386542);
			this.checkBox_49.Size = new Size(168, 18);
			this.checkBox_49.TabIndex = 57;
			this.checkBox_49.Text = MainForm.getString_0(107386553);
			this.checkBox_49.UseVisualStyleBackColor = true;
			this.checkBox_49.Leave += this.MainForm_Leave;
			this.groupBox_52.Controls.Add(this.button_66);
			this.groupBox_52.Controls.Add(this.textBox_16);
			this.groupBox_52.Location = new Point(237, 341);
			this.groupBox_52.Name = MainForm.getString_0(107386520);
			this.groupBox_52.Size = new Size(219, 46);
			this.groupBox_52.TabIndex = 56;
			this.groupBox_52.TabStop = false;
			this.groupBox_52.Text = MainForm.getString_0(107386471);
			this.button_66.Location = new Point(6, 15);
			this.button_66.Name = MainForm.getString_0(107386486);
			this.button_66.Size = new Size(83, 25);
			this.button_66.TabIndex = 18;
			this.button_66.Text = MainForm.getString_0(107391871);
			this.button_66.UseVisualStyleBackColor = true;
			this.button_66.Click += this.button_6_Click;
			this.textBox_16.Location = new Point(95, 17);
			this.textBox_16.Name = MainForm.getString_0(107386461);
			this.textBox_16.ReadOnly = true;
			this.textBox_16.Size = new Size(120, 20);
			this.textBox_16.TabIndex = 19;
			this.textBox_16.PreviewKeyDown += this.textBox_2_PreviewKeyDown;
			this.groupBox_51.Controls.Add(this.button_64);
			this.groupBox_51.Controls.Add(this.textBox_15);
			this.groupBox_51.Location = new Point(237, 393);
			this.groupBox_51.Name = MainForm.getString_0(107386408);
			this.groupBox_51.Size = new Size(219, 46);
			this.groupBox_51.TabIndex = 55;
			this.groupBox_51.TabStop = false;
			this.groupBox_51.Text = MainForm.getString_0(107386423);
			this.button_64.Location = new Point(6, 15);
			this.button_64.Name = MainForm.getString_0(107386902);
			this.button_64.Size = new Size(83, 25);
			this.button_64.TabIndex = 18;
			this.button_64.Text = MainForm.getString_0(107391871);
			this.button_64.UseVisualStyleBackColor = true;
			this.button_64.Click += this.button_6_Click;
			this.textBox_15.Location = new Point(95, 17);
			this.textBox_15.Name = MainForm.getString_0(107386865);
			this.textBox_15.ReadOnly = true;
			this.textBox_15.Size = new Size(120, 20);
			this.textBox_15.TabIndex = 19;
			this.textBox_15.PreviewKeyDown += this.textBox_2_PreviewKeyDown;
			this.pictureBox_1.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107386800));
			this.pictureBox_1.InitialImage = null;
			this.pictureBox_1.Location = new Point(117, 16);
			this.pictureBox_1.Name = MainForm.getString_0(107386803);
			this.pictureBox_1.Size = new Size(25, 25);
			this.pictureBox_1.TabIndex = 54;
			this.pictureBox_1.TabStop = false;
			this.pictureBox_1.Visible = false;
			this.button_26.Location = new Point(6, 325);
			this.button_26.Name = MainForm.getString_0(107386782);
			this.button_26.Size = new Size(120, 25);
			this.button_26.TabIndex = 52;
			this.button_26.Text = MainForm.getString_0(107386769);
			this.button_26.UseVisualStyleBackColor = true;
			this.button_26.Click += this.button_26_Click;
			this.button_25.Location = new Point(6, 356);
			this.button_25.Name = MainForm.getString_0(107386752);
			this.button_25.Size = new Size(120, 25);
			this.button_25.TabIndex = 51;
			this.button_25.Text = MainForm.getString_0(107386739);
			this.button_25.UseVisualStyleBackColor = true;
			this.button_25.Click += this.button_25_Click;
			this.button_3.Location = new Point(6, 387);
			this.button_3.Name = MainForm.getString_0(107386718);
			this.button_3.Size = new Size(120, 25);
			this.button_3.TabIndex = 17;
			this.button_3.Text = MainForm.getString_0(107386665);
			this.button_3.UseVisualStyleBackColor = true;
			this.button_3.Click += this.button_3_Click;
			this.groupBox_14.Controls.Add(this.button_6);
			this.groupBox_14.Controls.Add(this.textBox_2);
			this.groupBox_14.Location = new Point(237, 289);
			this.groupBox_14.Name = MainForm.getString_0(107386676);
			this.groupBox_14.Size = new Size(219, 46);
			this.groupBox_14.TabIndex = 50;
			this.groupBox_14.TabStop = false;
			this.groupBox_14.Text = MainForm.getString_0(107386115);
			this.button_6.Location = new Point(6, 15);
			this.button_6.Name = MainForm.getString_0(107386090);
			this.button_6.Size = new Size(83, 25);
			this.button_6.TabIndex = 18;
			this.button_6.Text = MainForm.getString_0(107391871);
			this.button_6.UseVisualStyleBackColor = true;
			this.button_6.Click += this.button_6_Click;
			this.textBox_2.Location = new Point(95, 17);
			this.textBox_2.Name = MainForm.getString_0(107386061);
			this.textBox_2.ReadOnly = true;
			this.textBox_2.Size = new Size(120, 20);
			this.textBox_2.TabIndex = 19;
			this.textBox_2.PreviewKeyDown += this.textBox_2_PreviewKeyDown;
			this.label_17.AutoSize = true;
			this.label_17.Location = new Point(3, 156);
			this.label_17.Name = MainForm.getString_0(107386068);
			this.label_17.Size = new Size(42, 14);
			this.label_17.TabIndex = 49;
			this.label_17.Text = MainForm.getString_0(107386027);
			this.textBox_0.Location = new Point(237, 156);
			this.textBox_0.Name = MainForm.getString_0(107386046);
			this.textBox_0.Size = new Size(240, 20);
			this.textBox_0.TabIndex = 7;
			this.textBox_0.TextAlign = HorizontalAlignment.Center;
			this.label_18.AutoSize = true;
			this.label_18.Location = new Point(3, 182);
			this.label_18.Name = MainForm.getString_0(107386033);
			this.label_18.Size = new Size(66, 14);
			this.label_18.TabIndex = 47;
			this.label_18.Text = MainForm.getString_0(107385988);
			this.textBox_1.Location = new Point(237, 182);
			this.textBox_1.Name = MainForm.getString_0(107386007);
			this.textBox_1.PasswordChar = '*';
			this.textBox_1.Size = new Size(240, 20);
			this.textBox_1.TabIndex = 8;
			this.textBox_1.TextAlign = HorizontalAlignment.Center;
			this.comboBox_0.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Location = new Point(237, 129);
			this.comboBox_0.Name = MainForm.getString_0(107385958);
			this.comboBox_0.RightToLeft = RightToLeft.No;
			this.comboBox_0.Size = new Size(240, 21);
			this.comboBox_0.TabIndex = 6;
			this.comboBox_0.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_0.SelectedIndexChanged += this.comboBox_0_SelectedIndexChanged;
			this.comboBox_0.Leave += this.MainForm_Leave;
			this.label_19.AutoSize = true;
			this.label_19.Location = new Point(3, 129);
			this.label_19.Name = MainForm.getString_0(107385929);
			this.label_19.Size = new Size(64, 14);
			this.label_19.TabIndex = 44;
			this.label_19.Text = MainForm.getString_0(107385948);
			this.checkBox_2.AutoSize = true;
			this.checkBox_2.Location = new Point(6, 253);
			this.checkBox_2.Name = MainForm.getString_0(107385899);
			this.checkBox_2.Size = new Size(128, 18);
			this.checkBox_2.TabIndex = 13;
			this.checkBox_2.Text = MainForm.getString_0(107385914);
			this.checkBox_2.UseVisualStyleBackColor = true;
			this.checkBox_2.Leave += this.MainForm_Leave;
			this.label_20.AutoSize = true;
			this.label_20.Location = new Point(3, 21);
			this.label_20.Name = MainForm.getString_0(107386369);
			this.label_20.Size = new Size(113, 14);
			this.label_20.TabIndex = 34;
			this.label_20.Text = MainForm.getString_0(107386388);
			this.textBox_3.Location = new Point(237, 21);
			this.textBox_3.Name = MainForm.getString_0(107386359);
			this.textBox_3.Size = new Size(240, 20);
			this.textBox_3.TabIndex = 2;
			this.textBox_3.TextAlign = HorizontalAlignment.Center;
			this.textBox_3.TextChanged += this.textBox_3_TextChanged;
			this.textBox_3.Leave += this.MainForm_Leave;
			NumericUpDown numericUpDown = this.numericUpDown_8;
			int[] array = new int[4];
			array[0] = 5;
			numericUpDown.Increment = new decimal(array);
			this.numericUpDown_8.Location = new Point(363, 257);
			NumericUpDown numericUpDown2 = this.numericUpDown_8;
			int[] array2 = new int[4];
			array2[0] = 300;
			numericUpDown2.Maximum = new decimal(array2);
			NumericUpDown numericUpDown3 = this.numericUpDown_8;
			int[] array3 = new int[4];
			array3[0] = 5;
			numericUpDown3.Minimum = new decimal(array3);
			this.numericUpDown_8.Name = MainForm.getString_0(107386330);
			this.numericUpDown_8.Size = new Size(47, 20);
			this.numericUpDown_8.TabIndex = 16;
			this.numericUpDown_8.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown4 = this.numericUpDown_8;
			int[] array4 = new int[4];
			array4[0] = 5;
			numericUpDown4.Value = new decimal(array4);
			this.numericUpDown_8.Leave += this.MainForm_Leave;
			this.numericUpDown_9.Location = new Point(363, 230);
			NumericUpDown numericUpDown5 = this.numericUpDown_9;
			int[] array5 = new int[4];
			array5[0] = 5;
			numericUpDown5.Maximum = new decimal(array5);
			this.numericUpDown_9.Name = MainForm.getString_0(107386301);
			this.numericUpDown_9.Size = new Size(47, 20);
			this.numericUpDown_9.TabIndex = 14;
			this.numericUpDown_9.TextAlign = HorizontalAlignment.Center;
			this.numericUpDown_9.Leave += this.MainForm_Leave;
			this.checkBox_3.AutoSize = true;
			this.checkBox_3.Location = new Point(6, 229);
			this.checkBox_3.Name = MainForm.getString_0(107386268);
			this.checkBox_3.Size = new Size(93, 18);
			this.checkBox_3.TabIndex = 11;
			this.checkBox_3.Text = MainForm.getString_0(107386219);
			this.checkBox_3.UseVisualStyleBackColor = true;
			this.checkBox_3.Leave += this.MainForm_Leave;
			this.label_21.AutoSize = true;
			this.label_21.Location = new Point(234, 260);
			this.label_21.Name = MainForm.getString_0(107386234);
			this.label_21.Size = new Size(103, 14);
			this.label_21.TabIndex = 15;
			this.label_21.Text = MainForm.getString_0(107386225);
			this.comboBox_1.DrawMode = DrawMode.OwnerDrawFixed;
			this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_1.FormattingEnabled = true;
			this.comboBox_1.Location = new Point(237, 102);
			this.comboBox_1.Name = MainForm.getString_0(107386200);
			this.comboBox_1.Size = new Size(240, 21);
			this.comboBox_1.TabIndex = 5;
			this.comboBox_1.DrawItem += this.comboBox_1_DrawItem;
			this.comboBox_1.SelectedValueChanged += this.comboBox_1_SelectedValueChanged;
			this.comboBox_1.Leave += this.MainForm_Leave;
			this.label_22.AutoSize = true;
			this.label_22.Location = new Point(234, 236);
			this.label_22.Name = MainForm.getString_0(107386155);
			this.label_22.Size = new Size(123, 14);
			this.label_22.TabIndex = 13;
			this.label_22.Text = MainForm.getString_0(107386146);
			this.label_23.AutoSize = true;
			this.label_23.Location = new Point(3, 102);
			this.label_23.Name = MainForm.getString_0(107385605);
			this.label_23.Size = new Size(51, 14);
			this.label_23.TabIndex = 7;
			this.label_23.Text = MainForm.getString_0(107385628);
			this.checkBox_4.AutoSize = true;
			this.checkBox_4.Location = new Point(6, 205);
			this.checkBox_4.Name = MainForm.getString_0(107385583);
			this.checkBox_4.Size = new Size(86, 18);
			this.checkBox_4.TabIndex = 9;
			this.checkBox_4.Text = MainForm.getString_0(107385598);
			this.checkBox_4.UseVisualStyleBackColor = true;
			this.checkBox_4.CheckedChanged += this.checkBox_4_CheckedChanged;
			this.checkBox_4.Leave += this.MainForm_Leave;
			this.checkBox_5.AutoSize = true;
			this.checkBox_5.Location = new Point(237, 208);
			this.checkBox_5.Name = MainForm.getString_0(107385549);
			this.checkBox_5.Size = new Size(165, 18);
			this.checkBox_5.TabIndex = 12;
			this.checkBox_5.Text = MainForm.getString_0(107385564);
			this.checkBox_5.UseVisualStyleBackColor = true;
			this.checkBox_5.Leave += this.MainForm_Leave;
			this.label_24.AutoSize = true;
			this.label_24.Location = new Point(3, 75);
			this.label_24.Name = MainForm.getString_0(107385531);
			this.label_24.Size = new Size(68, 14);
			this.label_24.TabIndex = 3;
			this.label_24.Text = MainForm.getString_0(107385522);
			this.label_25.AutoSize = true;
			this.label_25.Location = new Point(3, 48);
			this.label_25.Name = MainForm.getString_0(107385473);
			this.label_25.Size = new Size(89, 14);
			this.label_25.TabIndex = 2;
			this.label_25.Text = MainForm.getString_0(107385496);
			this.textBox_4.Location = new Point(237, 75);
			this.textBox_4.Name = MainForm.getString_0(107385443);
			this.textBox_4.Size = new Size(240, 20);
			this.textBox_4.TabIndex = 4;
			this.textBox_4.TextAlign = HorizontalAlignment.Center;
			this.textBox_4.Leave += this.MainForm_Leave;
			this.textBox_5.Location = new Point(237, 48);
			this.textBox_5.Name = MainForm.getString_0(107385458);
			this.textBox_5.Size = new Size(240, 20);
			this.textBox_5.TabIndex = 3;
			this.textBox_5.TextAlign = HorizontalAlignment.Center;
			this.textBox_5.Leave += this.MainForm_Leave;
			this.tabPage_28.Controls.Add(this.groupBox_4);
			this.tabPage_28.Location = new Point(4, 23);
			this.tabPage_28.Name = MainForm.getString_0(107385437);
			this.tabPage_28.Padding = new Padding(3);
			this.tabPage_28.Size = new Size(495, 482);
			this.tabPage_28.TabIndex = 1;
			this.tabPage_28.Text = MainForm.getString_0(107385392);
			this.tabPage_28.UseVisualStyleBackColor = true;
			this.groupBox_4.Controls.Add(this.groupBox_5);
			this.groupBox_4.Controls.Add(this.groupBox_6);
			this.groupBox_4.Location = new Point(6, 6);
			this.groupBox_4.Name = MainForm.getString_0(107385383);
			this.groupBox_4.Size = new Size(483, 458);
			this.groupBox_4.TabIndex = 2;
			this.groupBox_4.TabStop = false;
			this.groupBox_4.Text = MainForm.getString_0(107385402);
			this.groupBox_5.Controls.Add(this.checkBox_17);
			this.groupBox_5.Controls.Add(this.checkBox_0);
			this.groupBox_5.Controls.Add(this.numericUpDown_0);
			this.groupBox_5.Controls.Add(this.numericUpDown_1);
			this.groupBox_5.Controls.Add(this.numericUpDown_2);
			this.groupBox_5.Controls.Add(this.numericUpDown_3);
			this.groupBox_5.Location = new Point(6, 251);
			this.groupBox_5.Name = MainForm.getString_0(107385393);
			this.groupBox_5.Size = new Size(471, 70);
			this.groupBox_5.TabIndex = 37;
			this.groupBox_5.TabStop = false;
			this.groupBox_5.Text = MainForm.getString_0(107385860);
			this.checkBox_17.AutoSize = true;
			this.checkBox_17.Location = new Point(6, 45);
			this.checkBox_17.Name = MainForm.getString_0(107385883);
			this.checkBox_17.Size = new Size(70, 18);
			this.checkBox_17.TabIndex = 30;
			this.checkBox_17.Text = MainForm.getString_0(107385838);
			this.checkBox_17.UseVisualStyleBackColor = true;
			this.checkBox_17.CheckedChanged += this.checkBox_17_CheckedChanged;
			this.checkBox_0.AutoSize = true;
			this.checkBox_0.Location = new Point(6, 19);
			this.checkBox_0.Name = MainForm.getString_0(107385825);
			this.checkBox_0.Size = new Size(78, 18);
			this.checkBox_0.TabIndex = 25;
			this.checkBox_0.Text = MainForm.getString_0(107385808);
			this.checkBox_0.UseVisualStyleBackColor = true;
			this.checkBox_0.CheckedChanged += this.checkBox_0_CheckedChanged;
			this.checkBox_0.Leave += this.MainForm_Leave;
			this.numericUpDown_0.Enabled = false;
			this.numericUpDown_0.Location = new Point(125, 17);
			NumericUpDown numericUpDown6 = this.numericUpDown_0;
			int[] array6 = new int[4];
			array6[0] = 23;
			numericUpDown6.Maximum = new decimal(array6);
			this.numericUpDown_0.Name = MainForm.getString_0(107385795);
			this.numericUpDown_0.Size = new Size(47, 20);
			this.numericUpDown_0.TabIndex = 26;
			this.numericUpDown_0.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown7 = this.numericUpDown_0;
			int[] array7 = new int[4];
			array7[0] = 5;
			numericUpDown7.Value = new decimal(array7);
			this.numericUpDown_0.Leave += this.MainForm_Leave;
			this.numericUpDown_1.Enabled = false;
			this.numericUpDown_1.Location = new Point(178, 17);
			NumericUpDown numericUpDown8 = this.numericUpDown_1;
			int[] array8 = new int[4];
			array8[0] = 59;
			numericUpDown8.Maximum = new decimal(array8);
			this.numericUpDown_1.Name = MainForm.getString_0(107385770);
			this.numericUpDown_1.Size = new Size(47, 20);
			this.numericUpDown_1.TabIndex = 27;
			this.numericUpDown_1.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown9 = this.numericUpDown_1;
			int[] array9 = new int[4];
			array9[0] = 20;
			numericUpDown9.Value = new decimal(array9);
			this.numericUpDown_1.Leave += this.MainForm_Leave;
			this.numericUpDown_2.Enabled = false;
			this.numericUpDown_2.Location = new Point(178, 43);
			NumericUpDown numericUpDown10 = this.numericUpDown_2;
			int[] array10 = new int[4];
			array10[0] = 59;
			numericUpDown10.Maximum = new decimal(array10);
			this.numericUpDown_2.Name = MainForm.getString_0(107385741);
			this.numericUpDown_2.Size = new Size(47, 20);
			this.numericUpDown_2.TabIndex = 29;
			this.numericUpDown_2.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown11 = this.numericUpDown_2;
			int[] array11 = new int[4];
			array11[0] = 20;
			numericUpDown11.Value = new decimal(array11);
			this.numericUpDown_2.Leave += this.MainForm_Leave;
			this.numericUpDown_3.Enabled = false;
			this.numericUpDown_3.Location = new Point(125, 43);
			NumericUpDown numericUpDown12 = this.numericUpDown_3;
			int[] array12 = new int[4];
			array12[0] = 23;
			numericUpDown12.Maximum = new decimal(array12);
			this.numericUpDown_3.Name = MainForm.getString_0(107385712);
			this.numericUpDown_3.Size = new Size(47, 20);
			this.numericUpDown_3.TabIndex = 28;
			this.numericUpDown_3.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown13 = this.numericUpDown_3;
			int[] array13 = new int[4];
			array13[0] = 5;
			numericUpDown13.Value = new decimal(array13);
			this.numericUpDown_3.Leave += this.MainForm_Leave;
			this.groupBox_6.Controls.Add(this.label_92);
			this.groupBox_6.Controls.Add(this.textBox_11);
			this.groupBox_6.Controls.Add(this.button_21);
			this.groupBox_6.Controls.Add(this.fastObjectListView_3);
			this.groupBox_6.Controls.Add(this.checkBox_1);
			this.groupBox_6.Controls.Add(this.label_12);
			this.groupBox_6.Controls.Add(this.numericUpDown_4);
			this.groupBox_6.Controls.Add(this.label_13);
			this.groupBox_6.Controls.Add(this.numericUpDown_5);
			this.groupBox_6.Controls.Add(this.label_14);
			this.groupBox_6.Controls.Add(this.label_15);
			this.groupBox_6.Controls.Add(this.numericUpDown_6);
			this.groupBox_6.Controls.Add(this.label_16);
			this.groupBox_6.Controls.Add(this.numericUpDown_7);
			this.groupBox_6.Location = new Point(6, 11);
			this.groupBox_6.Name = MainForm.getString_0(107385719);
			this.groupBox_6.Size = new Size(471, 234);
			this.groupBox_6.TabIndex = 36;
			this.groupBox_6.TabStop = false;
			this.groupBox_6.Text = MainForm.getString_0(107385674);
			this.label_92.AutoSize = true;
			this.label_92.Location = new Point(3, 117);
			this.label_92.Name = MainForm.getString_0(107385669);
			this.label_92.Size = new Size(82, 14);
			this.label_92.TabIndex = 39;
			this.label_92.Text = MainForm.getString_0(107385688);
			this.textBox_11.Location = new Point(6, 134);
			this.textBox_11.Name = MainForm.getString_0(107385639);
			this.textBox_11.Size = new Size(148, 20);
			this.textBox_11.TabIndex = 36;
			this.button_21.Location = new Point(6, 160);
			this.button_21.Name = MainForm.getString_0(107385654);
			this.button_21.Size = new Size(69, 20);
			this.button_21.TabIndex = 37;
			this.button_21.Text = MainForm.getString_0(107385117);
			this.button_21.UseVisualStyleBackColor = true;
			this.fastObjectListView_3.AllColumns.Add(this.olvcolumn_3);
			this.fastObjectListView_3.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_3
			});
			this.fastObjectListView_3.FullRowSelect = true;
			this.fastObjectListView_3.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_3.HideSelection = false;
			this.fastObjectListView_3.Location = new Point(213, 117);
			this.fastObjectListView_3.MultiSelect = false;
			this.fastObjectListView_3.Name = MainForm.getString_0(107385112);
			this.fastObjectListView_3.OwnerDraw = true;
			this.fastObjectListView_3.ShowGroups = false;
			this.fastObjectListView_3.Size = new Size(252, 110);
			this.fastObjectListView_3.TabIndex = 38;
			this.fastObjectListView_3.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_3.View = View.Details;
			this.fastObjectListView_3.VirtualMode = true;
			this.olvcolumn_3.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_3.CellPadding = null;
			this.olvcolumn_3.FillsFreeSpace = true;
			this.olvcolumn_3.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_3.Text = MainForm.getString_0(107385082);
			this.olvcolumn_3.Width = 250;
			this.checkBox_1.AutoSize = true;
			this.checkBox_1.Location = new Point(6, 19);
			this.checkBox_1.Name = MainForm.getString_0(107385033);
			this.checkBox_1.Size = new Size(113, 18);
			this.checkBox_1.TabIndex = 20;
			this.checkBox_1.Text = MainForm.getString_0(107385052);
			this.checkBox_1.UseVisualStyleBackColor = true;
			this.checkBox_1.Leave += this.MainForm_Leave;
			this.label_12.AutoSize = true;
			this.label_12.Location = new Point(13, 45);
			this.label_12.Name = MainForm.getString_0(107384999);
			this.label_12.Size = new Size(106, 14);
			this.label_12.TabIndex = 35;
			this.label_12.Text = MainForm.getString_0(107385018);
			NumericUpDown numericUpDown14 = this.numericUpDown_4;
			int[] array14 = new int[4];
			array14[0] = 5;
			numericUpDown14.Increment = new decimal(array14);
			this.numericUpDown_4.Location = new Point(125, 17);
			NumericUpDown numericUpDown15 = this.numericUpDown_4;
			int[] array15 = new int[4];
			array15[0] = 300;
			numericUpDown15.Maximum = new decimal(array15);
			NumericUpDown numericUpDown16 = this.numericUpDown_4;
			int[] array16 = new int[4];
			array16[0] = 5;
			numericUpDown16.Minimum = new decimal(array16);
			this.numericUpDown_4.Name = MainForm.getString_0(107384961);
			this.numericUpDown_4.Size = new Size(47, 20);
			this.numericUpDown_4.TabIndex = 21;
			this.numericUpDown_4.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown17 = this.numericUpDown_4;
			int[] array17 = new int[4];
			array17[0] = 5;
			numericUpDown17.Value = new decimal(array17);
			this.numericUpDown_4.Leave += this.MainForm_Leave;
			this.label_13.AutoSize = true;
			this.label_13.Location = new Point(178, 45);
			this.label_13.Name = MainForm.getString_0(107384940);
			this.label_13.Size = new Size(27, 14);
			this.label_13.TabIndex = 34;
			this.label_13.Text = MainForm.getString_0(107384959);
			NumericUpDown numericUpDown18 = this.numericUpDown_5;
			int[] array18 = new int[4];
			array18[0] = 10;
			numericUpDown18.Increment = new decimal(array18);
			this.numericUpDown_5.Location = new Point(211, 17);
			NumericUpDown numericUpDown19 = this.numericUpDown_5;
			int[] array19 = new int[4];
			array19[0] = 600;
			numericUpDown19.Maximum = new decimal(array19);
			NumericUpDown numericUpDown20 = this.numericUpDown_5;
			int[] array20 = new int[4];
			array20[0] = 10;
			numericUpDown20.Minimum = new decimal(array20);
			this.numericUpDown_5.Name = MainForm.getString_0(107384954);
			this.numericUpDown_5.Size = new Size(47, 20);
			this.numericUpDown_5.TabIndex = 22;
			this.numericUpDown_5.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown21 = this.numericUpDown_5;
			int[] array21 = new int[4];
			array21[0] = 10;
			numericUpDown21.Value = new decimal(array21);
			this.numericUpDown_5.Leave += this.MainForm_Leave;
			this.label_14.AutoSize = true;
			this.label_14.Location = new Point(264, 45);
			this.label_14.Name = MainForm.getString_0(107384901);
			this.label_14.Size = new Size(53, 14);
			this.label_14.TabIndex = 33;
			this.label_14.Text = MainForm.getString_0(107384920);
			this.label_15.AutoSize = true;
			this.label_15.Location = new Point(264, 20);
			this.label_15.Name = MainForm.getString_0(107384875);
			this.label_15.Size = new Size(53, 14);
			this.label_15.TabIndex = 29;
			this.label_15.Text = MainForm.getString_0(107384920);
			NumericUpDown numericUpDown22 = this.numericUpDown_6;
			int[] array22 = new int[4];
			array22[0] = 10;
			numericUpDown22.Increment = new decimal(array22);
			this.numericUpDown_6.Location = new Point(211, 43);
			NumericUpDown numericUpDown23 = this.numericUpDown_6;
			int[] array23 = new int[4];
			array23[0] = 600;
			numericUpDown23.Maximum = new decimal(array23);
			NumericUpDown numericUpDown24 = this.numericUpDown_6;
			int[] array24 = new int[4];
			array24[0] = 10;
			numericUpDown24.Minimum = new decimal(array24);
			this.numericUpDown_6.Name = MainForm.getString_0(107384894);
			this.numericUpDown_6.Size = new Size(47, 20);
			this.numericUpDown_6.TabIndex = 24;
			this.numericUpDown_6.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown25 = this.numericUpDown_6;
			int[] array25 = new int[4];
			array25[0] = 10;
			numericUpDown25.Value = new decimal(array25);
			this.numericUpDown_6.Leave += this.MainForm_Leave;
			this.label_16.AutoSize = true;
			this.label_16.Location = new Point(178, 20);
			this.label_16.Name = MainForm.getString_0(107385349);
			this.label_16.Size = new Size(27, 14);
			this.label_16.TabIndex = 30;
			this.label_16.Text = MainForm.getString_0(107384959);
			NumericUpDown numericUpDown26 = this.numericUpDown_7;
			int[] array26 = new int[4];
			array26[0] = 5;
			numericUpDown26.Increment = new decimal(array26);
			this.numericUpDown_7.Location = new Point(125, 43);
			NumericUpDown numericUpDown27 = this.numericUpDown_7;
			int[] array27 = new int[4];
			array27[0] = 300;
			numericUpDown27.Maximum = new decimal(array27);
			NumericUpDown numericUpDown28 = this.numericUpDown_7;
			int[] array28 = new int[4];
			array28[0] = 5;
			numericUpDown28.Minimum = new decimal(array28);
			this.numericUpDown_7.Name = MainForm.getString_0(107385368);
			this.numericUpDown_7.Size = new Size(47, 20);
			this.numericUpDown_7.TabIndex = 23;
			this.numericUpDown_7.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown29 = this.numericUpDown_7;
			int[] array29 = new int[4];
			array29[0] = 5;
			numericUpDown29.Value = new decimal(array29);
			this.numericUpDown_7.Leave += this.MainForm_Leave;
			this.tabPage_4.Controls.Add(this.tabControl_3);
			this.tabPage_4.Location = new Point(4, 23);
			this.tabPage_4.Name = MainForm.getString_0(107385343);
			this.tabPage_4.Size = new Size(509, 515);
			this.tabPage_4.TabIndex = 6;
			this.tabPage_4.Text = MainForm.getString_0(107385330);
			this.tabPage_4.UseVisualStyleBackColor = true;
			this.tabControl_3.Controls.Add(this.tabPage_16);
			this.tabControl_3.Controls.Add(this.tabPage_26);
			this.tabControl_3.Controls.Add(this.tabPage_18);
			this.tabControl_3.Controls.Add(this.tabPage_24);
			this.tabControl_3.Location = new Point(3, 3);
			this.tabControl_3.Name = MainForm.getString_0(107385289);
			this.tabControl_3.SelectedIndex = 0;
			this.tabControl_3.Size = new Size(503, 500);
			this.tabControl_3.TabIndex = 26;
			this.tabControl_3.SelectedIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_3.Leave += this.MainForm_Leave;
			this.tabPage_16.Controls.Add(this.groupBox_9);
			this.tabPage_16.Controls.Add(this.groupBox_8);
			this.tabPage_16.Location = new Point(4, 23);
			this.tabPage_16.Name = MainForm.getString_0(107385304);
			this.tabPage_16.Padding = new Padding(3);
			this.tabPage_16.Size = new Size(495, 473);
			this.tabPage_16.TabIndex = 0;
			this.tabPage_16.Text = MainForm.getString_0(107385259);
			this.tabPage_16.UseVisualStyleBackColor = true;
			this.groupBox_9.Controls.Add(this.button_5);
			this.groupBox_9.Controls.Add(this.checkBox_6);
			this.groupBox_9.Controls.Add(this.comboBox_3);
			this.groupBox_9.Controls.Add(this.label_26);
			this.groupBox_9.Controls.Add(this.comboBox_4);
			this.groupBox_9.Controls.Add(this.checkBox_7);
			this.groupBox_9.Controls.Add(this.comboBox_5);
			this.groupBox_9.Controls.Add(this.comboBox_6);
			this.groupBox_9.Controls.Add(this.label_27);
			this.groupBox_9.Controls.Add(this.comboBox_7);
			this.groupBox_9.Controls.Add(this.label_28);
			this.groupBox_9.Controls.Add(this.comboBox_8);
			this.groupBox_9.Controls.Add(this.label_29);
			this.groupBox_9.Controls.Add(this.comboBox_9);
			this.groupBox_9.Controls.Add(this.label_30);
			this.groupBox_9.Controls.Add(this.label_31);
			this.groupBox_9.Controls.Add(this.label_32);
			this.groupBox_9.Location = new Point(6, 6);
			this.groupBox_9.Name = MainForm.getString_0(107385278);
			this.groupBox_9.Size = new Size(483, 336);
			this.groupBox_9.TabIndex = 24;
			this.groupBox_9.TabStop = false;
			this.groupBox_9.Text = MainForm.getString_0(107385265);
			this.button_5.Location = new Point(6, 305);
			this.button_5.Name = MainForm.getString_0(107385240);
			this.button_5.Size = new Size(123, 25);
			this.button_5.TabIndex = 25;
			this.button_5.Text = MainForm.getString_0(107385211);
			this.button_5.UseVisualStyleBackColor = true;
			this.button_5.Click += this.button_75_Click;
			this.checkBox_6.AutoSize = true;
			this.checkBox_6.Location = new Point(6, 239);
			this.checkBox_6.Name = MainForm.getString_0(107385158);
			this.checkBox_6.Size = new Size(180, 18);
			this.checkBox_6.TabIndex = 9;
			this.checkBox_6.Text = MainForm.getString_0(107385133);
			this.checkBox_6.UseVisualStyleBackColor = true;
			this.checkBox_6.CheckedChanged += this.checkBox_6_CheckedChanged;
			this.checkBox_6.Leave += this.MainForm_Leave;
			this.comboBox_3.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_3.FormattingEnabled = true;
			this.comboBox_3.Location = new Point(139, 103);
			this.comboBox_3.Name = MainForm.getString_0(107384588);
			this.comboBox_3.Size = new Size(338, 22);
			this.comboBox_3.TabIndex = 5;
			this.comboBox_3.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_3.Leave += this.MainForm_Leave;
			this.label_26.AutoSize = true;
			this.label_26.Location = new Point(3, 106);
			this.label_26.Name = MainForm.getString_0(107384603);
			this.label_26.Size = new Size(120, 14);
			this.label_26.TabIndex = 24;
			this.label_26.Text = MainForm.getString_0(107384558);
			this.comboBox_4.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_4.FormattingEnabled = true;
			this.comboBox_4.Location = new Point(139, 187);
			this.comboBox_4.Name = MainForm.getString_0(107384561);
			this.comboBox_4.Size = new Size(338, 22);
			this.comboBox_4.TabIndex = 8;
			this.comboBox_4.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_4.Leave += this.MainForm_Leave;
			this.checkBox_7.AutoSize = true;
			this.checkBox_7.Location = new Point(6, 215);
			this.checkBox_7.Name = MainForm.getString_0(107384544);
			this.checkBox_7.Size = new Size(176, 18);
			this.checkBox_7.TabIndex = 10;
			this.checkBox_7.Text = MainForm.getString_0(107384495);
			this.checkBox_7.UseVisualStyleBackColor = true;
			this.checkBox_7.Leave += this.MainForm_Leave;
			this.comboBox_5.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_5.FormattingEnabled = true;
			this.comboBox_5.Location = new Point(139, 19);
			this.comboBox_5.Name = MainForm.getString_0(107384462);
			this.comboBox_5.Size = new Size(338, 22);
			this.comboBox_5.TabIndex = 2;
			this.comboBox_5.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_5.Leave += this.MainForm_Leave;
			this.comboBox_6.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_6.FormattingEnabled = true;
			this.comboBox_6.Location = new Point(139, 131);
			this.comboBox_6.Name = MainForm.getString_0(107384477);
			this.comboBox_6.Size = new Size(338, 22);
			this.comboBox_6.TabIndex = 6;
			this.comboBox_6.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_6.Leave += this.MainForm_Leave;
			this.label_27.AutoSize = true;
			this.label_27.Location = new Point(3, 190);
			this.label_27.Name = MainForm.getString_0(107384432);
			this.label_27.Size = new Size(99, 14);
			this.label_27.TabIndex = 12;
			this.label_27.Text = MainForm.getString_0(107384419);
			this.comboBox_7.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_7.FormattingEnabled = true;
			this.comboBox_7.Location = new Point(139, 159);
			this.comboBox_7.Name = MainForm.getString_0(107384398);
			this.comboBox_7.Size = new Size(338, 22);
			this.comboBox_7.TabIndex = 7;
			this.comboBox_7.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_7.Leave += this.MainForm_Leave;
			this.label_28.AutoSize = true;
			this.label_28.Location = new Point(3, 50);
			this.label_28.Name = MainForm.getString_0(107384385);
			this.label_28.Size = new Size(97, 14);
			this.label_28.TabIndex = 10;
			this.label_28.Text = MainForm.getString_0(107384404);
			this.comboBox_8.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_8.FormattingEnabled = true;
			this.comboBox_8.Location = new Point(139, 75);
			this.comboBox_8.Name = MainForm.getString_0(107384383);
			this.comboBox_8.Size = new Size(338, 22);
			this.comboBox_8.TabIndex = 4;
			this.comboBox_8.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_8.Leave += this.MainForm_Leave;
			this.label_29.AutoSize = true;
			this.label_29.Location = new Point(3, 78);
			this.label_29.Name = MainForm.getString_0(107384846);
			this.label_29.Size = new Size(127, 14);
			this.label_29.TabIndex = 8;
			this.label_29.Text = MainForm.getString_0(107384833);
			this.comboBox_9.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_9.FormattingEnabled = true;
			this.comboBox_9.Location = new Point(139, 47);
			this.comboBox_9.Name = MainForm.getString_0(107384804);
			this.comboBox_9.Size = new Size(338, 22);
			this.comboBox_9.TabIndex = 3;
			this.comboBox_9.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_9.Leave += this.MainForm_Leave;
			this.label_30.AutoSize = true;
			this.label_30.Location = new Point(3, 162);
			this.label_30.Name = MainForm.getString_0(107384827);
			this.label_30.Size = new Size(97, 14);
			this.label_30.TabIndex = 6;
			this.label_30.Text = MainForm.getString_0(107384782);
			this.label_31.AutoSize = true;
			this.label_31.Location = new Point(3, 22);
			this.label_31.Name = MainForm.getString_0(107384793);
			this.label_31.Size = new Size(118, 14);
			this.label_31.TabIndex = 2;
			this.label_31.Text = MainForm.getString_0(107384748);
			this.label_32.AutoSize = true;
			this.label_32.Location = new Point(3, 134);
			this.label_32.Name = MainForm.getString_0(107384755);
			this.label_32.Size = new Size(121, 14);
			this.label_32.TabIndex = 4;
			this.label_32.Text = MainForm.getString_0(107384710);
			this.groupBox_8.Controls.Add(this.comboBox_2);
			this.groupBox_8.Controls.Add(this.button_4);
			this.groupBox_8.Controls.Add(this.fastObjectListView_0);
			this.groupBox_8.Location = new Point(6, 348);
			this.groupBox_8.Name = MainForm.getString_0(107384685);
			this.groupBox_8.Size = new Size(483, 121);
			this.groupBox_8.TabIndex = 25;
			this.groupBox_8.TabStop = false;
			this.groupBox_8.Text = MainForm.getString_0(107384704);
			this.groupBox_8.Leave += this.MainForm_Leave;
			this.comboBox_2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_2.FormattingEnabled = true;
			this.comboBox_2.Location = new Point(6, 17);
			this.comboBox_2.Name = MainForm.getString_0(107384635);
			this.comboBox_2.Size = new Size(218, 22);
			this.comboBox_2.TabIndex = 11;
			this.button_4.Location = new Point(6, 45);
			this.button_4.Name = MainForm.getString_0(107384070);
			this.button_4.Size = new Size(69, 20);
			this.button_4.TabIndex = 12;
			this.button_4.Text = MainForm.getString_0(107385117);
			this.button_4.UseVisualStyleBackColor = true;
			this.fastObjectListView_0.AllColumns.Add(this.olvcolumn_0);
			this.fastObjectListView_0.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_0
			});
			this.fastObjectListView_0.FullRowSelect = true;
			this.fastObjectListView_0.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_0.HideSelection = false;
			this.fastObjectListView_0.Location = new Point(243, 17);
			this.fastObjectListView_0.MultiSelect = false;
			this.fastObjectListView_0.Name = MainForm.getString_0(107384081);
			this.fastObjectListView_0.OwnerDraw = true;
			this.fastObjectListView_0.ShowGroups = false;
			this.fastObjectListView_0.Size = new Size(234, 96);
			this.fastObjectListView_0.TabIndex = 13;
			this.fastObjectListView_0.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_0.View = View.Details;
			this.fastObjectListView_0.VirtualMode = true;
			this.olvcolumn_0.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_0.CellPadding = null;
			this.olvcolumn_0.FillsFreeSpace = true;
			this.olvcolumn_0.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_0.Text = MainForm.getString_0(107384059);
			this.olvcolumn_0.Width = 250;
			this.tabPage_26.Controls.Add(this.groupBox_23);
			this.tabPage_26.Location = new Point(4, 23);
			this.tabPage_26.Name = MainForm.getString_0(107384010);
			this.tabPage_26.Size = new Size(495, 473);
			this.tabPage_26.TabIndex = 4;
			this.tabPage_26.Text = MainForm.getString_0(107384029);
			this.tabPage_26.UseVisualStyleBackColor = true;
			this.groupBox_23.Controls.Add(this.label_155);
			this.groupBox_23.Controls.Add(this.comboBox_64);
			this.groupBox_23.Controls.Add(this.button_20);
			this.groupBox_23.Controls.Add(this.label_86);
			this.groupBox_23.Controls.Add(this.label_87);
			this.groupBox_23.Controls.Add(this.comboBox_21);
			this.groupBox_23.Controls.Add(this.comboBox_22);
			this.groupBox_23.Controls.Add(this.comboBox_23);
			this.groupBox_23.Controls.Add(this.label_88);
			this.groupBox_23.Controls.Add(this.label_89);
			this.groupBox_23.Controls.Add(this.comboBox_24);
			this.groupBox_23.Controls.Add(this.comboBox_25);
			this.groupBox_23.Controls.Add(this.label_90);
			this.groupBox_23.Controls.Add(this.comboBox_26);
			this.groupBox_23.Controls.Add(this.label_91);
			this.groupBox_23.Location = new Point(6, 6);
			this.groupBox_23.Name = MainForm.getString_0(107384000);
			this.groupBox_23.Size = new Size(486, 464);
			this.groupBox_23.TabIndex = 28;
			this.groupBox_23.TabStop = false;
			this.groupBox_23.Text = MainForm.getString_0(107383951);
			this.label_155.AutoSize = true;
			this.label_155.Location = new Point(3, 133);
			this.label_155.Name = MainForm.getString_0(107383918);
			this.label_155.Size = new Size(123, 14);
			this.label_155.TabIndex = 60;
			this.label_155.Text = MainForm.getString_0(107383905);
			this.comboBox_64.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_64.FormattingEnabled = true;
			this.comboBox_64.Location = new Point(150, 130);
			this.comboBox_64.Name = MainForm.getString_0(107383876);
			this.comboBox_64.Size = new Size(330, 22);
			this.comboBox_64.TabIndex = 59;
			this.comboBox_64.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_64.Leave += this.MainForm_Leave;
			this.button_20.Location = new Point(6, 433);
			this.button_20.Name = MainForm.getString_0(107383891);
			this.button_20.Size = new Size(123, 25);
			this.button_20.TabIndex = 56;
			this.button_20.Text = MainForm.getString_0(107385211);
			this.button_20.UseVisualStyleBackColor = true;
			this.label_86.AutoSize = true;
			this.label_86.Location = new Point(3, 106);
			this.label_86.Name = MainForm.getString_0(107383846);
			this.label_86.Size = new Size(98, 14);
			this.label_86.TabIndex = 52;
			this.label_86.Text = MainForm.getString_0(107383865);
			this.label_87.AutoSize = true;
			this.label_87.Location = new Point(3, 78);
			this.label_87.Name = MainForm.getString_0(107384352);
			this.label_87.Size = new Size(131, 14);
			this.label_87.TabIndex = 51;
			this.label_87.Text = MainForm.getString_0(107384339);
			this.comboBox_21.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_21.FormattingEnabled = true;
			this.comboBox_21.Location = new Point(150, 103);
			this.comboBox_21.Name = MainForm.getString_0(107384310);
			this.comboBox_21.Size = new Size(330, 22);
			this.comboBox_21.TabIndex = 47;
			this.comboBox_21.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_21.Leave += this.MainForm_Leave;
			this.comboBox_22.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_22.FormattingEnabled = true;
			this.comboBox_22.Location = new Point(150, 75);
			this.comboBox_22.Name = MainForm.getString_0(107384265);
			this.comboBox_22.Size = new Size(330, 22);
			this.comboBox_22.TabIndex = 46;
			this.comboBox_22.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_22.Leave += this.MainForm_Leave;
			this.comboBox_23.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_23.FormattingEnabled = true;
			this.comboBox_23.Location = new Point(150, 47);
			this.comboBox_23.Name = MainForm.getString_0(107384280);
			this.comboBox_23.Size = new Size(330, 22);
			this.comboBox_23.TabIndex = 45;
			this.comboBox_23.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_23.Leave += this.MainForm_Leave;
			this.label_88.AutoSize = true;
			this.label_88.Location = new Point(3, 50);
			this.label_88.Name = MainForm.getString_0(107384231);
			this.label_88.Size = new Size(113, 14);
			this.label_88.TabIndex = 44;
			this.label_88.Text = MainForm.getString_0(107384250);
			this.label_89.AutoSize = true;
			this.label_89.Location = new Point(-82, -28);
			this.label_89.Name = MainForm.getString_0(107384193);
			this.label_89.Size = new Size(112, 14);
			this.label_89.TabIndex = 43;
			this.label_89.Text = MainForm.getString_0(107384212);
			this.comboBox_24.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_24.FormattingEnabled = true;
			this.comboBox_24.Location = new Point(54, -31);
			this.comboBox_24.Name = MainForm.getString_0(107384187);
			this.comboBox_24.Size = new Size(229, 22);
			this.comboBox_24.TabIndex = 42;
			this.comboBox_25.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_25.FormattingEnabled = true;
			this.comboBox_25.Location = new Point(54, -59);
			this.comboBox_25.Name = MainForm.getString_0(107384138);
			this.comboBox_25.Size = new Size(229, 22);
			this.comboBox_25.TabIndex = 28;
			this.label_90.AutoSize = true;
			this.label_90.Location = new Point(3, 22);
			this.label_90.Name = MainForm.getString_0(107384153);
			this.label_90.Size = new Size(109, 14);
			this.label_90.TabIndex = 39;
			this.label_90.Text = MainForm.getString_0(107384108);
			this.comboBox_26.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_26.FormattingEnabled = true;
			this.comboBox_26.Location = new Point(150, 19);
			this.comboBox_26.Name = MainForm.getString_0(107384115);
			this.comboBox_26.Size = new Size(330, 22);
			this.comboBox_26.TabIndex = 30;
			this.comboBox_26.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_26.Leave += this.MainForm_Leave;
			this.label_91.AutoSize = true;
			this.label_91.Location = new Point(-82, -56);
			this.label_91.Name = MainForm.getString_0(107383558);
			this.label_91.Size = new Size(118, 14);
			this.label_91.TabIndex = 29;
			this.label_91.Text = MainForm.getString_0(107384748);
			this.tabPage_18.Controls.Add(this.groupBox_15);
			this.tabPage_18.Location = new Point(4, 23);
			this.tabPage_18.Name = MainForm.getString_0(107383577);
			this.tabPage_18.Size = new Size(495, 473);
			this.tabPage_18.TabIndex = 2;
			this.tabPage_18.Text = MainForm.getString_0(107383532);
			this.tabPage_18.UseVisualStyleBackColor = true;
			this.groupBox_15.Controls.Add(this.button_7);
			this.groupBox_15.Controls.Add(this.label_69);
			this.groupBox_15.Controls.Add(this.label_70);
			this.groupBox_15.Controls.Add(this.label_71);
			this.groupBox_15.Controls.Add(this.comboBox_14);
			this.groupBox_15.Controls.Add(this.comboBox_15);
			this.groupBox_15.Controls.Add(this.comboBox_16);
			this.groupBox_15.Controls.Add(this.label_67);
			this.groupBox_15.Controls.Add(this.comboBox_12);
			this.groupBox_15.Controls.Add(this.comboBox_13);
			this.groupBox_15.Controls.Add(this.label_68);
			this.groupBox_15.Location = new Point(6, 6);
			this.groupBox_15.Name = MainForm.getString_0(107383539);
			this.groupBox_15.Size = new Size(486, 137);
			this.groupBox_15.TabIndex = 27;
			this.groupBox_15.TabStop = false;
			this.groupBox_15.Text = MainForm.getString_0(107383490);
			this.button_7.Location = new Point(6, 105);
			this.button_7.Name = MainForm.getString_0(107383485);
			this.button_7.Size = new Size(123, 25);
			this.button_7.TabIndex = 56;
			this.button_7.Text = MainForm.getString_0(107385211);
			this.button_7.UseVisualStyleBackColor = true;
			this.button_7.Click += this.button_75_Click;
			this.label_69.AutoSize = true;
			this.label_69.Location = new Point(3, 78);
			this.label_69.Name = MainForm.getString_0(107383444);
			this.label_69.Size = new Size(141, 14);
			this.label_69.TabIndex = 55;
			this.label_69.Text = MainForm.getString_0(107383399);
			this.label_70.AutoSize = true;
			this.label_70.Location = new Point(3, 50);
			this.label_70.Name = MainForm.getString_0(107383366);
			this.label_70.Size = new Size(94, 14);
			this.label_70.TabIndex = 53;
			this.label_70.Text = MainForm.getString_0(107383385);
			this.label_71.AutoSize = true;
			this.label_71.Location = new Point(3, 22);
			this.label_71.Name = MainForm.getString_0(107383332);
			this.label_71.Size = new Size(126, 14);
			this.label_71.TabIndex = 52;
			this.label_71.Text = MainForm.getString_0(107383351);
			this.comboBox_14.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_14.FormattingEnabled = true;
			this.comboBox_14.Location = new Point(150, 75);
			this.comboBox_14.Name = MainForm.getString_0(107383834);
			this.comboBox_14.Size = new Size(330, 22);
			this.comboBox_14.TabIndex = 50;
			this.comboBox_14.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_14.Leave += this.MainForm_Leave;
			this.comboBox_15.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_15.FormattingEnabled = true;
			this.comboBox_15.Location = new Point(150, 47);
			this.comboBox_15.Name = MainForm.getString_0(107383781);
			this.comboBox_15.Size = new Size(330, 22);
			this.comboBox_15.TabIndex = 48;
			this.comboBox_15.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_15.Leave += this.MainForm_Leave;
			this.comboBox_16.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_16.FormattingEnabled = true;
			this.comboBox_16.Location = new Point(150, 19);
			this.comboBox_16.Name = MainForm.getString_0(107383800);
			this.comboBox_16.Size = new Size(330, 22);
			this.comboBox_16.TabIndex = 47;
			this.comboBox_16.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_16.Leave += this.MainForm_Leave;
			this.label_67.AutoSize = true;
			this.label_67.Location = new Point(-82, -28);
			this.label_67.Name = MainForm.getString_0(107383751);
			this.label_67.Size = new Size(112, 14);
			this.label_67.TabIndex = 43;
			this.label_67.Text = MainForm.getString_0(107384212);
			this.comboBox_12.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_12.FormattingEnabled = true;
			this.comboBox_12.Location = new Point(54, -31);
			this.comboBox_12.Name = MainForm.getString_0(107383770);
			this.comboBox_12.Size = new Size(229, 22);
			this.comboBox_12.TabIndex = 42;
			this.comboBox_13.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_13.FormattingEnabled = true;
			this.comboBox_13.Location = new Point(54, -59);
			this.comboBox_13.Name = MainForm.getString_0(107383725);
			this.comboBox_13.Size = new Size(229, 22);
			this.comboBox_13.TabIndex = 28;
			this.label_68.AutoSize = true;
			this.label_68.Location = new Point(-82, -56);
			this.label_68.Name = MainForm.getString_0(107383744);
			this.label_68.Size = new Size(118, 14);
			this.label_68.TabIndex = 29;
			this.label_68.Text = MainForm.getString_0(107384748);
			this.tabPage_24.Controls.Add(this.groupBox_18);
			this.tabPage_24.Controls.Add(this.groupBox_19);
			this.tabPage_24.Location = new Point(4, 23);
			this.tabPage_24.Name = MainForm.getString_0(107383731);
			this.tabPage_24.Padding = new Padding(3);
			this.tabPage_24.Size = new Size(495, 473);
			this.tabPage_24.TabIndex = 3;
			this.tabPage_24.Text = MainForm.getString_0(107383686);
			this.tabPage_24.UseVisualStyleBackColor = true;
			this.groupBox_18.Controls.Add(this.button_13);
			this.groupBox_18.Controls.Add(this.button_14);
			this.groupBox_18.Controls.Add(this.comboBox_19);
			this.groupBox_18.Controls.Add(this.button_15);
			this.groupBox_18.Location = new Point(6, 6);
			this.groupBox_18.Name = MainForm.getString_0(107383705);
			this.groupBox_18.Size = new Size(483, 108);
			this.groupBox_18.TabIndex = 27;
			this.groupBox_18.TabStop = false;
			this.groupBox_18.Text = MainForm.getString_0(107383656);
			this.button_13.Location = new Point(81, 45);
			this.button_13.Name = MainForm.getString_0(107383647);
			this.button_13.Size = new Size(69, 20);
			this.button_13.TabIndex = 14;
			this.button_13.Text = MainForm.getString_0(107383590);
			this.button_13.UseVisualStyleBackColor = true;
			this.button_13.Click += this.button_13_Click;
			this.button_14.Location = new Point(156, 45);
			this.button_14.Name = MainForm.getString_0(107383613);
			this.button_14.Size = new Size(69, 20);
			this.button_14.TabIndex = 13;
			this.button_14.Text = MainForm.getString_0(107383044);
			this.button_14.UseVisualStyleBackColor = true;
			this.button_14.Click += this.button_14_Click;
			this.comboBox_19.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_19.FormattingEnabled = true;
			this.comboBox_19.Location = new Point(6, 17);
			this.comboBox_19.Name = MainForm.getString_0(107383067);
			this.comboBox_19.Size = new Size(219, 22);
			this.comboBox_19.TabIndex = 11;
			this.button_15.Location = new Point(6, 45);
			this.button_15.Name = MainForm.getString_0(107383014);
			this.button_15.Size = new Size(69, 20);
			this.button_15.TabIndex = 12;
			this.button_15.Text = MainForm.getString_0(107385117);
			this.button_15.UseVisualStyleBackColor = true;
			this.button_15.Click += this.button_15_Click;
			this.groupBox_19.Controls.Add(this.button_18);
			this.groupBox_19.Controls.Add(this.comboBox_20);
			this.groupBox_19.Controls.Add(this.button_16);
			this.groupBox_19.Controls.Add(this.fastObjectListView_2);
			this.groupBox_19.Location = new Point(6, 120);
			this.groupBox_19.Name = MainForm.getString_0(107383025);
			this.groupBox_19.Size = new Size(483, 228);
			this.groupBox_19.TabIndex = 26;
			this.groupBox_19.TabStop = false;
			this.groupBox_19.Text = MainForm.getString_0(107383008);
			this.button_18.Location = new Point(6, 198);
			this.button_18.Name = MainForm.getString_0(107382943);
			this.button_18.Size = new Size(144, 25);
			this.button_18.TabIndex = 16;
			this.button_18.Text = MainForm.getString_0(107382930);
			this.button_18.UseVisualStyleBackColor = true;
			this.button_18.Click += this.button_18_Click;
			this.comboBox_20.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_20.FormattingEnabled = true;
			this.comboBox_20.Location = new Point(6, 19);
			this.comboBox_20.Name = MainForm.getString_0(107382901);
			this.comboBox_20.Size = new Size(189, 22);
			this.comboBox_20.TabIndex = 15;
			this.button_16.Location = new Point(6, 45);
			this.button_16.Name = MainForm.getString_0(107382876);
			this.button_16.Size = new Size(69, 20);
			this.button_16.TabIndex = 12;
			this.button_16.Text = MainForm.getString_0(107385117);
			this.button_16.UseVisualStyleBackColor = true;
			this.fastObjectListView_2.AllColumns.Add(this.olvcolumn_2);
			this.fastObjectListView_2.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_2
			});
			this.fastObjectListView_2.FullRowSelect = true;
			this.fastObjectListView_2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_2.HideSelection = false;
			this.fastObjectListView_2.Location = new Point(226, 17);
			this.fastObjectListView_2.MultiSelect = false;
			this.fastObjectListView_2.Name = MainForm.getString_0(107382823);
			this.fastObjectListView_2.OwnerDraw = true;
			this.fastObjectListView_2.ShowGroups = false;
			this.fastObjectListView_2.Size = new Size(251, 205);
			this.fastObjectListView_2.TabIndex = 13;
			this.fastObjectListView_2.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_2.View = View.Details;
			this.fastObjectListView_2.VirtualMode = true;
			this.olvcolumn_2.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_2.CellPadding = null;
			this.olvcolumn_2.FillsFreeSpace = true;
			this.olvcolumn_2.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_2.Text = MainForm.getString_0(107384059);
			this.olvcolumn_2.Width = 250;
			this.tabPage_50.Controls.Add(this.tabControl_9);
			this.tabPage_50.Location = new Point(4, 23);
			this.tabPage_50.Name = MainForm.getString_0(107382838);
			this.tabPage_50.Size = new Size(509, 515);
			this.tabPage_50.TabIndex = 15;
			this.tabPage_50.Text = MainForm.getString_0(107383305);
			this.tabPage_50.UseVisualStyleBackColor = true;
			this.tabControl_9.Controls.Add(this.tabPage_51);
			this.tabControl_9.Controls.Add(this.tabPage_54);
			this.tabControl_9.Controls.Add(this.tabPage_52);
			this.tabControl_9.Controls.Add(this.tabPage_53);
			this.tabControl_9.Location = new Point(3, 3);
			this.tabControl_9.Name = MainForm.getString_0(107383324);
			this.tabControl_9.SelectedIndex = 0;
			this.tabControl_9.Size = new Size(503, 509);
			this.tabControl_9.TabIndex = 0;
			this.tabPage_51.Controls.Add(this.groupBox_68);
			this.tabPage_51.Controls.Add(this.groupBox_61);
			this.tabPage_51.Controls.Add(this.groupBox_62);
			this.tabPage_51.Location = new Point(4, 23);
			this.tabPage_51.Name = MainForm.getString_0(107383275);
			this.tabPage_51.Padding = new Padding(3);
			this.tabPage_51.Size = new Size(495, 482);
			this.tabPage_51.TabIndex = 0;
			this.tabPage_51.Text = MainForm.getString_0(107397071);
			this.tabPage_51.UseVisualStyleBackColor = true;
			this.groupBox_68.Controls.Add(this.checkBox_78);
			this.groupBox_68.Controls.Add(this.checkBox_75);
			this.groupBox_68.Controls.Add(this.numericUpDown_74);
			this.groupBox_68.Controls.Add(this.label_185);
			this.groupBox_68.Controls.Add(this.checkBox_64);
			this.groupBox_68.Location = new Point(3, 6);
			this.groupBox_68.Name = MainForm.getString_0(107383294);
			this.groupBox_68.Size = new Size(484, 86);
			this.groupBox_68.TabIndex = 35;
			this.groupBox_68.TabStop = false;
			this.groupBox_68.Text = MainForm.getString_0(107388377);
			this.checkBox_78.AutoSize = true;
			this.checkBox_78.Location = new Point(6, 16);
			this.checkBox_78.Name = MainForm.getString_0(107383245);
			this.checkBox_78.Size = new Size(193, 18);
			this.checkBox_78.TabIndex = 34;
			this.checkBox_78.Text = MainForm.getString_0(107383252);
			this.checkBox_78.UseVisualStyleBackColor = true;
			this.checkBox_78.Leave += this.MainForm_Leave;
			this.checkBox_75.AutoSize = true;
			this.checkBox_75.Location = new Point(6, 64);
			this.checkBox_75.Name = MainForm.getString_0(107383179);
			this.checkBox_75.Size = new Size(191, 18);
			this.checkBox_75.TabIndex = 31;
			this.checkBox_75.Text = MainForm.getString_0(107383150);
			this.checkBox_75.UseVisualStyleBackColor = true;
			this.checkBox_75.CheckedChanged += this.checkBox_75_CheckedChanged;
			this.checkBox_75.Leave += this.MainForm_Leave;
			this.numericUpDown_74.Enabled = false;
			NumericUpDown numericUpDown30 = this.numericUpDown_74;
			int[] array30 = new int[4];
			array30[0] = 10;
			numericUpDown30.Increment = new decimal(array30);
			this.numericUpDown_74.Location = new Point(197, 62);
			NumericUpDown numericUpDown31 = this.numericUpDown_74;
			int[] array31 = new int[4];
			array31[0] = 300;
			numericUpDown31.Maximum = new decimal(array31);
			NumericUpDown numericUpDown32 = this.numericUpDown_74;
			int[] array32 = new int[4];
			array32[0] = 5;
			numericUpDown32.Minimum = new decimal(array32);
			this.numericUpDown_74.Name = MainForm.getString_0(107383109);
			this.numericUpDown_74.Size = new Size(47, 20);
			this.numericUpDown_74.TabIndex = 32;
			this.numericUpDown_74.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown33 = this.numericUpDown_74;
			int[] array33 = new int[4];
			array33[0] = 5;
			numericUpDown33.Value = new decimal(array33);
			this.numericUpDown_74.Leave += this.MainForm_Leave;
			this.label_185.AutoSize = true;
			this.label_185.Location = new Point(245, 64);
			this.label_185.Name = MainForm.getString_0(107383084);
			this.label_185.Size = new Size(65, 14);
			this.label_185.TabIndex = 33;
			this.label_185.Text = MainForm.getString_0(107390916);
			this.checkBox_64.AutoSize = true;
			this.checkBox_64.Location = new Point(6, 40);
			this.checkBox_64.Name = MainForm.getString_0(107383103);
			this.checkBox_64.Size = new Size(232, 18);
			this.checkBox_64.TabIndex = 0;
			this.checkBox_64.Text = MainForm.getString_0(107382530);
			this.checkBox_64.UseVisualStyleBackColor = true;
			this.checkBox_64.Leave += this.MainForm_Leave;
			this.groupBox_61.Controls.Add(this.listBox_1);
			this.groupBox_61.Controls.Add(this.button_82);
			this.groupBox_61.Controls.Add(this.button_83);
			this.groupBox_61.Location = new Point(3, 97);
			this.groupBox_61.Name = MainForm.getString_0(107382513);
			this.groupBox_61.Size = new Size(483, 85);
			this.groupBox_61.TabIndex = 34;
			this.groupBox_61.TabStop = false;
			this.groupBox_61.Text = MainForm.getString_0(107382496);
			this.listBox_1.FormattingEnabled = true;
			this.listBox_1.ItemHeight = 14;
			this.listBox_1.Location = new Point(38, 19);
			this.listBox_1.Name = MainForm.getString_0(107382459);
			this.listBox_1.Size = new Size(155, 60);
			this.listBox_1.TabIndex = 2;
			this.button_82.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107382402));
			this.button_82.Location = new Point(6, 19);
			this.button_82.Name = MainForm.getString_0(107382381);
			this.button_82.Size = new Size(26, 26);
			this.button_82.TabIndex = 1;
			this.button_82.UseVisualStyleBackColor = true;
			this.button_82.Click += this.button_82_Click;
			this.button_83.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107382400));
			this.button_83.Location = new Point(6, 51);
			this.button_83.Name = MainForm.getString_0(107382347);
			this.button_83.Size = new Size(26, 26);
			this.button_83.TabIndex = 0;
			this.button_83.UseVisualStyleBackColor = true;
			this.button_83.Click += this.button_83_Click;
			this.groupBox_62.Controls.Add(this.comboBox_68);
			this.groupBox_62.Controls.Add(this.button_84);
			this.groupBox_62.Controls.Add(this.fastObjectListView_17);
			this.groupBox_62.Controls.Add(this.checkBox_52);
			this.groupBox_62.Controls.Add(this.numericUpDown_62);
			this.groupBox_62.Controls.Add(this.label_167);
			this.groupBox_62.Controls.Add(this.label_168);
			this.groupBox_62.Location = new Point(3, 188);
			this.groupBox_62.Name = MainForm.getString_0(107382366);
			this.groupBox_62.Size = new Size(483, 210);
			this.groupBox_62.TabIndex = 33;
			this.groupBox_62.TabStop = false;
			this.groupBox_62.Text = MainForm.getString_0(107382317);
			this.comboBox_68.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_68.FormattingEnabled = true;
			this.comboBox_68.Items.AddRange(new object[]
			{
				MainForm.getString_0(107390916),
				MainForm.getString_0(107382788),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382662),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107390973),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107382581),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107381928),
				MainForm.getString_0(107381939),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107381905),
				MainForm.getString_0(107381888),
				MainForm.getString_0(107381835)
			});
			this.comboBox_68.Location = new Point(6, 63);
			this.comboBox_68.Name = MainForm.getString_0(107381854);
			this.comboBox_68.Size = new Size(193, 22);
			this.comboBox_68.TabIndex = 37;
			this.button_84.Location = new Point(6, 92);
			this.button_84.Name = MainForm.getString_0(107381797);
			this.button_84.Size = new Size(69, 20);
			this.button_84.TabIndex = 38;
			this.button_84.Text = MainForm.getString_0(107385117);
			this.button_84.UseVisualStyleBackColor = true;
			this.fastObjectListView_17.AllColumns.Add(this.olvcolumn_23);
			this.fastObjectListView_17.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_23
			});
			this.fastObjectListView_17.FullRowSelect = true;
			this.fastObjectListView_17.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_17.HideSelection = false;
			this.fastObjectListView_17.Location = new Point(257, 63);
			this.fastObjectListView_17.MultiSelect = false;
			this.fastObjectListView_17.Name = MainForm.getString_0(107382276);
			this.fastObjectListView_17.OwnerDraw = true;
			this.fastObjectListView_17.ShowGroups = false;
			this.fastObjectListView_17.Size = new Size(222, 142);
			this.fastObjectListView_17.TabIndex = 39;
			this.fastObjectListView_17.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_17.View = View.Details;
			this.fastObjectListView_17.VirtualMode = true;
			this.olvcolumn_23.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_23.CellPadding = null;
			this.olvcolumn_23.FillsFreeSpace = true;
			this.olvcolumn_23.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_23.Text = MainForm.getString_0(107382247);
			this.olvcolumn_23.Width = 250;
			this.checkBox_52.AutoSize = true;
			this.checkBox_52.Location = new Point(6, 19);
			this.checkBox_52.Name = MainForm.getString_0(107382222);
			this.checkBox_52.Size = new Size(199, 18);
			this.checkBox_52.TabIndex = 3;
			this.checkBox_52.Text = MainForm.getString_0(107382225);
			this.checkBox_52.UseVisualStyleBackColor = true;
			this.checkBox_52.CheckedChanged += this.checkBox_52_CheckedChanged;
			this.checkBox_52.Leave += this.MainForm_Leave;
			this.numericUpDown_62.Location = new Point(283, 38);
			this.numericUpDown_62.Name = MainForm.getString_0(107382152);
			this.numericUpDown_62.Size = new Size(47, 20);
			this.numericUpDown_62.TabIndex = 4;
			this.numericUpDown_62.TextAlign = HorizontalAlignment.Center;
			this.numericUpDown_62.Leave += this.MainForm_Leave;
			this.label_167.AutoSize = true;
			this.label_167.Location = new Point(3, 40);
			this.label_167.Name = MainForm.getString_0(107382115);
			this.label_167.Size = new Size(274, 14);
			this.label_167.TabIndex = 35;
			this.label_167.Text = MainForm.getString_0(107382134);
			this.label_168.AutoSize = true;
			this.label_168.Location = new Point(332, 41);
			this.label_168.Name = MainForm.getString_0(107382069);
			this.label_168.Size = new Size(16, 14);
			this.label_168.TabIndex = 36;
			this.label_168.Text = MainForm.getString_0(107414280);
			this.tabPage_54.Controls.Add(this.groupBox_69);
			this.tabPage_54.Location = new Point(4, 23);
			this.tabPage_54.Name = MainForm.getString_0(107414275);
			this.tabPage_54.Size = new Size(495, 482);
			this.tabPage_54.TabIndex = 5;
			this.tabPage_54.Text = MainForm.getString_0(107414294);
			this.tabPage_54.UseVisualStyleBackColor = true;
			this.groupBox_69.Controls.Add(this.checkBox_73);
			this.groupBox_69.Controls.Add(this.comboBox_72);
			this.groupBox_69.Controls.Add(this.button_96);
			this.groupBox_69.Controls.Add(this.fastObjectListView_22);
			this.groupBox_69.Controls.Add(this.checkBox_67);
			this.groupBox_69.Controls.Add(this.checkBox_69);
			this.groupBox_69.Controls.Add(this.checkBox_70);
			this.groupBox_69.Controls.Add(this.checkBox_71);
			this.groupBox_69.Controls.Add(this.checkBox_68);
			this.groupBox_69.Controls.Add(this.numericUpDown_72);
			this.groupBox_69.Controls.Add(this.label_182);
			this.groupBox_69.Controls.Add(this.checkBox_72);
			this.groupBox_69.Location = new Point(3, 3);
			this.groupBox_69.Name = MainForm.getString_0(107414269);
			this.groupBox_69.Size = new Size(489, 476);
			this.groupBox_69.TabIndex = 0;
			this.groupBox_69.TabStop = false;
			this.groupBox_69.Text = MainForm.getString_0(107388377);
			this.checkBox_73.AutoSize = true;
			this.checkBox_73.Location = new Point(6, 43);
			this.checkBox_73.Name = MainForm.getString_0(107414220);
			this.checkBox_73.Size = new Size(329, 18);
			this.checkBox_73.TabIndex = 86;
			this.checkBox_73.Text = MainForm.getString_0(107414191);
			this.checkBox_73.UseVisualStyleBackColor = true;
			this.comboBox_72.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_72.FormattingEnabled = true;
			this.comboBox_72.Location = new Point(6, 209);
			this.comboBox_72.Name = MainForm.getString_0(107414114);
			this.comboBox_72.Size = new Size(207, 22);
			this.comboBox_72.TabIndex = 83;
			this.button_96.Location = new Point(6, 237);
			this.button_96.Name = MainForm.getString_0(107414089);
			this.button_96.Size = new Size(69, 20);
			this.button_96.TabIndex = 84;
			this.button_96.Text = MainForm.getString_0(107385117);
			this.button_96.UseVisualStyleBackColor = true;
			this.fastObjectListView_22.AllColumns.Add(this.olvcolumn_28);
			this.fastObjectListView_22.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_28
			});
			this.fastObjectListView_22.FullRowSelect = true;
			this.fastObjectListView_22.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_22.HideSelection = false;
			this.fastObjectListView_22.Location = new Point(237, 209);
			this.fastObjectListView_22.MultiSelect = false;
			this.fastObjectListView_22.Name = MainForm.getString_0(107414064);
			this.fastObjectListView_22.OwnerDraw = true;
			this.fastObjectListView_22.ShowGroups = false;
			this.fastObjectListView_22.Size = new Size(246, 186);
			this.fastObjectListView_22.TabIndex = 85;
			this.fastObjectListView_22.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_22.View = View.Details;
			this.fastObjectListView_22.VirtualMode = true;
			this.olvcolumn_28.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_28.CellPadding = null;
			this.olvcolumn_28.FillsFreeSpace = true;
			this.olvcolumn_28.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_28.Text = MainForm.getString_0(107414071);
			this.olvcolumn_28.Width = 260;
			this.checkBox_67.AutoSize = true;
			this.checkBox_67.Location = new Point(6, 185);
			this.checkBox_67.Name = MainForm.getString_0(107414538);
			this.checkBox_67.Size = new Size(365, 18);
			this.checkBox_67.TabIndex = 37;
			this.checkBox_67.Text = MainForm.getString_0(107414509);
			this.checkBox_67.UseVisualStyleBackColor = true;
			this.checkBox_67.CheckedChanged += this.checkBox_67_CheckedChanged;
			this.checkBox_69.AutoSize = true;
			this.checkBox_69.Location = new Point(6, 115);
			this.checkBox_69.Name = MainForm.getString_0(107414456);
			this.checkBox_69.Size = new Size(356, 18);
			this.checkBox_69.TabIndex = 36;
			this.checkBox_69.Text = MainForm.getString_0(107414419);
			this.checkBox_69.UseVisualStyleBackColor = true;
			this.checkBox_70.AutoSize = true;
			this.checkBox_70.Location = new Point(6, 91);
			this.checkBox_70.Name = MainForm.getString_0(107414306);
			this.checkBox_70.Size = new Size(293, 18);
			this.checkBox_70.TabIndex = 35;
			this.checkBox_70.Text = MainForm.getString_0(107413765);
			this.checkBox_70.UseVisualStyleBackColor = true;
			this.checkBox_71.AutoSize = true;
			this.checkBox_71.Location = new Point(6, 67);
			this.checkBox_71.Name = MainForm.getString_0(107413728);
			this.checkBox_71.Size = new Size(300, 18);
			this.checkBox_71.TabIndex = 34;
			this.checkBox_71.Text = MainForm.getString_0(107413667);
			this.checkBox_71.UseVisualStyleBackColor = true;
			this.checkBox_68.AutoSize = true;
			this.checkBox_68.Location = new Point(6, 139);
			this.checkBox_68.Name = MainForm.getString_0(107413630);
			this.checkBox_68.Size = new Size(268, 18);
			this.checkBox_68.TabIndex = 31;
			this.checkBox_68.Text = MainForm.getString_0(107413593);
			this.checkBox_68.UseVisualStyleBackColor = true;
			this.checkBox_68.CheckedChanged += this.checkBox_68_CheckedChanged;
			NumericUpDown numericUpDown34 = this.numericUpDown_72;
			int[] array34 = new int[4];
			array34[0] = 10;
			numericUpDown34.Increment = new decimal(array34);
			this.numericUpDown_72.Location = new Point(275, 137);
			NumericUpDown numericUpDown35 = this.numericUpDown_72;
			int[] array35 = new int[4];
			array35[0] = 300;
			numericUpDown35.Maximum = new decimal(array35);
			NumericUpDown numericUpDown36 = this.numericUpDown_72;
			int[] array36 = new int[4];
			array36[0] = 5;
			numericUpDown36.Minimum = new decimal(array36);
			this.numericUpDown_72.Name = MainForm.getString_0(107414040);
			this.numericUpDown_72.Size = new Size(47, 20);
			this.numericUpDown_72.TabIndex = 32;
			this.numericUpDown_72.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown37 = this.numericUpDown_72;
			int[] array37 = new int[4];
			array37[0] = 5;
			numericUpDown37.Value = new decimal(array37);
			this.label_182.AutoSize = true;
			this.label_182.Location = new Point(328, 139);
			this.label_182.Name = MainForm.getString_0(107414011);
			this.label_182.Size = new Size(72, 14);
			this.label_182.TabIndex = 33;
			this.label_182.Text = MainForm.getString_0(107413966);
			this.checkBox_72.AutoSize = true;
			this.checkBox_72.Location = new Point(6, 19);
			this.checkBox_72.Name = MainForm.getString_0(107413981);
			this.checkBox_72.Size = new Size(156, 18);
			this.checkBox_72.TabIndex = 0;
			this.checkBox_72.Text = MainForm.getString_0(107413952);
			this.checkBox_72.UseVisualStyleBackColor = true;
			this.checkBox_72.CheckedChanged += this.checkBox_72_CheckedChanged;
			this.tabPage_52.Controls.Add(this.groupBox_63);
			this.tabPage_52.Location = new Point(4, 23);
			this.tabPage_52.Name = MainForm.getString_0(107413919);
			this.tabPage_52.Size = new Size(495, 482);
			this.tabPage_52.TabIndex = 1;
			this.tabPage_52.Text = MainForm.getString_0(107413906);
			this.tabPage_52.UseVisualStyleBackColor = true;
			this.groupBox_63.Controls.Add(this.button_85);
			this.groupBox_63.Controls.Add(this.comboBox_69);
			this.groupBox_63.Controls.Add(this.button_86);
			this.groupBox_63.Controls.Add(this.fastObjectListView_18);
			this.groupBox_63.Location = new Point(6, 6);
			this.groupBox_63.Name = MainForm.getString_0(107413885);
			this.groupBox_63.Size = new Size(483, 165);
			this.groupBox_63.TabIndex = 28;
			this.groupBox_63.TabStop = false;
			this.groupBox_63.Text = MainForm.getString_0(107413906);
			this.button_85.Location = new Point(6, 134);
			this.button_85.Name = MainForm.getString_0(107413836);
			this.button_85.Size = new Size(123, 25);
			this.button_85.TabIndex = 36;
			this.button_85.Text = MainForm.getString_0(107385211);
			this.button_85.UseVisualStyleBackColor = true;
			this.comboBox_69.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_69.FormattingEnabled = true;
			this.comboBox_69.Location = new Point(6, 17);
			this.comboBox_69.Name = MainForm.getString_0(107413803);
			this.comboBox_69.Size = new Size(193, 22);
			this.comboBox_69.TabIndex = 2;
			this.button_86.Location = new Point(6, 45);
			this.button_86.Name = MainForm.getString_0(107413810);
			this.button_86.Size = new Size(69, 20);
			this.button_86.TabIndex = 3;
			this.button_86.Text = MainForm.getString_0(107385117);
			this.button_86.UseVisualStyleBackColor = true;
			this.fastObjectListView_18.AllColumns.Add(this.olvcolumn_24);
			this.fastObjectListView_18.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_24
			});
			this.fastObjectListView_18.FullRowSelect = true;
			this.fastObjectListView_18.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_18.HideSelection = false;
			this.fastObjectListView_18.Location = new Point(205, 17);
			this.fastObjectListView_18.MultiSelect = false;
			this.fastObjectListView_18.Name = MainForm.getString_0(107413273);
			this.fastObjectListView_18.OwnerDraw = true;
			this.fastObjectListView_18.ShowGroups = false;
			this.fastObjectListView_18.Size = new Size(272, 142);
			this.fastObjectListView_18.TabIndex = 4;
			this.fastObjectListView_18.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_18.View = View.Details;
			this.fastObjectListView_18.VirtualMode = true;
			this.olvcolumn_24.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_24.CellPadding = null;
			this.olvcolumn_24.FillsFreeSpace = true;
			this.olvcolumn_24.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_24.Text = MainForm.getString_0(107384059);
			this.olvcolumn_24.Width = 250;
			this.tabPage_53.Controls.Add(this.groupBox_64);
			this.tabPage_53.Controls.Add(this.groupBox_65);
			this.tabPage_53.Controls.Add(this.groupBox_66);
			this.tabPage_53.Location = new Point(4, 23);
			this.tabPage_53.Name = MainForm.getString_0(107413220);
			this.tabPage_53.Size = new Size(495, 482);
			this.tabPage_53.TabIndex = 4;
			this.tabPage_53.Text = MainForm.getString_0(107413239);
			this.tabPage_53.UseVisualStyleBackColor = true;
			this.groupBox_64.Controls.Add(this.checkBox_53);
			this.groupBox_64.Controls.Add(this.textBox_19);
			this.groupBox_64.Controls.Add(this.button_87);
			this.groupBox_64.Controls.Add(this.fastObjectListView_19);
			this.groupBox_64.Location = new Point(6, 337);
			this.groupBox_64.Name = MainForm.getString_0(107413194);
			this.groupBox_64.Size = new Size(483, 134);
			this.groupBox_64.TabIndex = 33;
			this.groupBox_64.TabStop = false;
			this.groupBox_64.Text = MainForm.getString_0(107413209);
			this.checkBox_53.AutoSize = true;
			this.checkBox_53.Location = new Point(6, 19);
			this.checkBox_53.Name = MainForm.getString_0(107413156);
			this.checkBox_53.Size = new Size(69, 18);
			this.checkBox_53.TabIndex = 7;
			this.checkBox_53.Text = MainForm.getString_0(107413127);
			this.checkBox_53.UseVisualStyleBackColor = true;
			this.checkBox_53.Leave += this.MainForm_Leave;
			this.textBox_19.Location = new Point(5, 43);
			this.textBox_19.Name = MainForm.getString_0(107413146);
			this.textBox_19.Size = new Size(157, 20);
			this.textBox_19.TabIndex = 8;
			this.button_87.Location = new Point(5, 69);
			this.button_87.Name = MainForm.getString_0(107413093);
			this.button_87.Size = new Size(69, 20);
			this.button_87.TabIndex = 9;
			this.button_87.Text = MainForm.getString_0(107385117);
			this.button_87.UseVisualStyleBackColor = true;
			this.fastObjectListView_19.AllColumns.Add(this.olvcolumn_25);
			this.fastObjectListView_19.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_25
			});
			this.fastObjectListView_19.FullRowSelect = true;
			this.fastObjectListView_19.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_19.HideSelection = false;
			this.fastObjectListView_19.Location = new Point(245, 19);
			this.fastObjectListView_19.MultiSelect = false;
			this.fastObjectListView_19.Name = MainForm.getString_0(107413068);
			this.fastObjectListView_19.OwnerDraw = true;
			this.fastObjectListView_19.ShowGroups = false;
			this.fastObjectListView_19.Size = new Size(232, 110);
			this.fastObjectListView_19.TabIndex = 10;
			this.fastObjectListView_19.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_19.View = View.Details;
			this.fastObjectListView_19.VirtualMode = true;
			this.olvcolumn_25.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_25.CellPadding = null;
			this.olvcolumn_25.FillsFreeSpace = true;
			this.olvcolumn_25.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_25.Text = MainForm.getString_0(107413239);
			this.olvcolumn_25.Width = 250;
			this.groupBox_65.Controls.Add(this.checkBox_54);
			this.groupBox_65.Controls.Add(this.textBox_20);
			this.groupBox_65.Controls.Add(this.button_88);
			this.groupBox_65.Controls.Add(this.fastObjectListView_20);
			this.groupBox_65.Location = new Point(6, 6);
			this.groupBox_65.Name = MainForm.getString_0(107413079);
			this.groupBox_65.Size = new Size(483, 134);
			this.groupBox_65.TabIndex = 32;
			this.groupBox_65.TabStop = false;
			this.groupBox_65.Text = MainForm.getString_0(107413034);
			this.checkBox_54.AutoSize = true;
			this.checkBox_54.Location = new Point(6, 19);
			this.checkBox_54.Name = MainForm.getString_0(107413517);
			this.checkBox_54.Size = new Size(69, 18);
			this.checkBox_54.TabIndex = 7;
			this.checkBox_54.Text = MainForm.getString_0(107413127);
			this.checkBox_54.UseVisualStyleBackColor = true;
			this.checkBox_54.Leave += this.MainForm_Leave;
			this.textBox_20.Location = new Point(5, 43);
			this.textBox_20.Name = MainForm.getString_0(107413524);
			this.textBox_20.Size = new Size(157, 20);
			this.textBox_20.TabIndex = 8;
			this.button_88.Location = new Point(5, 69);
			this.button_88.Name = MainForm.getString_0(107413475);
			this.button_88.Size = new Size(69, 20);
			this.button_88.TabIndex = 9;
			this.button_88.Text = MainForm.getString_0(107385117);
			this.button_88.UseVisualStyleBackColor = true;
			this.fastObjectListView_20.AllColumns.Add(this.olvcolumn_26);
			this.fastObjectListView_20.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_26
			});
			this.fastObjectListView_20.FullRowSelect = true;
			this.fastObjectListView_20.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_20.HideSelection = false;
			this.fastObjectListView_20.Location = new Point(245, 19);
			this.fastObjectListView_20.MultiSelect = false;
			this.fastObjectListView_20.Name = MainForm.getString_0(107413454);
			this.fastObjectListView_20.OwnerDraw = true;
			this.fastObjectListView_20.ShowGroups = false;
			this.fastObjectListView_20.Size = new Size(232, 110);
			this.fastObjectListView_20.TabIndex = 10;
			this.fastObjectListView_20.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_20.View = View.Details;
			this.fastObjectListView_20.VirtualMode = true;
			this.olvcolumn_26.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_26.CellPadding = null;
			this.olvcolumn_26.FillsFreeSpace = true;
			this.olvcolumn_26.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_26.Text = MainForm.getString_0(107413239);
			this.olvcolumn_26.Width = 250;
			this.groupBox_66.Controls.Add(this.checkBox_55);
			this.groupBox_66.Controls.Add(this.textBox_21);
			this.groupBox_66.Controls.Add(this.button_89);
			this.groupBox_66.Controls.Add(this.fastObjectListView_21);
			this.groupBox_66.Controls.Add(this.checkBox_56);
			this.groupBox_66.Controls.Add(this.numericUpDown_63);
			this.groupBox_66.Controls.Add(this.label_169);
			this.groupBox_66.Controls.Add(this.checkBox_57);
			this.groupBox_66.Controls.Add(this.numericUpDown_64);
			this.groupBox_66.Controls.Add(this.label_170);
			this.groupBox_66.Location = new Point(6, 145);
			this.groupBox_66.Name = MainForm.getString_0(107413469);
			this.groupBox_66.Size = new Size(483, 186);
			this.groupBox_66.TabIndex = 32;
			this.groupBox_66.TabStop = false;
			this.groupBox_66.Text = MainForm.getString_0(107413424);
			this.checkBox_55.AutoSize = true;
			this.checkBox_55.Location = new Point(6, 19);
			this.checkBox_55.Name = MainForm.getString_0(107413439);
			this.checkBox_55.Size = new Size(69, 18);
			this.checkBox_55.TabIndex = 11;
			this.checkBox_55.Text = MainForm.getString_0(107413127);
			this.checkBox_55.UseVisualStyleBackColor = true;
			this.checkBox_55.Leave += this.MainForm_Leave;
			this.textBox_21.Location = new Point(5, 43);
			this.textBox_21.Name = MainForm.getString_0(107413386);
			this.textBox_21.Size = new Size(157, 20);
			this.textBox_21.TabIndex = 12;
			this.button_89.Location = new Point(5, 69);
			this.button_89.Name = MainForm.getString_0(107413401);
			this.button_89.Size = new Size(69, 20);
			this.button_89.TabIndex = 13;
			this.button_89.Text = MainForm.getString_0(107385117);
			this.button_89.UseVisualStyleBackColor = true;
			this.fastObjectListView_21.AllColumns.Add(this.olvcolumn_27);
			this.fastObjectListView_21.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_27
			});
			this.fastObjectListView_21.FullRowSelect = true;
			this.fastObjectListView_21.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_21.HideSelection = false;
			this.fastObjectListView_21.Location = new Point(245, 19);
			this.fastObjectListView_21.MultiSelect = false;
			this.fastObjectListView_21.Name = MainForm.getString_0(107413352);
			this.fastObjectListView_21.OwnerDraw = true;
			this.fastObjectListView_21.ShowGroups = false;
			this.fastObjectListView_21.Size = new Size(232, 110);
			this.fastObjectListView_21.TabIndex = 14;
			this.fastObjectListView_21.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_21.View = View.Details;
			this.fastObjectListView_21.VirtualMode = true;
			this.olvcolumn_27.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_27.CellPadding = null;
			this.olvcolumn_27.FillsFreeSpace = true;
			this.olvcolumn_27.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_27.Text = MainForm.getString_0(107413371);
			this.olvcolumn_27.Width = 250;
			this.checkBox_56.AutoSize = true;
			this.checkBox_56.Location = new Point(5, 160);
			this.checkBox_56.Name = MainForm.getString_0(107413318);
			this.checkBox_56.Size = new Size(158, 18);
			this.checkBox_56.TabIndex = 17;
			this.checkBox_56.Text = MainForm.getString_0(107413333);
			this.checkBox_56.UseVisualStyleBackColor = true;
			this.checkBox_56.Leave += this.MainForm_Leave;
			NumericUpDown numericUpDown38 = this.numericUpDown_63;
			int[] array38 = new int[4];
			array38[0] = 5;
			numericUpDown38.Increment = new decimal(array38);
			this.numericUpDown_63.Location = new Point(169, 159);
			NumericUpDown numericUpDown39 = this.numericUpDown_63;
			int[] array39 = new int[4];
			array39[0] = 300;
			numericUpDown39.Maximum = new decimal(array39);
			NumericUpDown numericUpDown40 = this.numericUpDown_63;
			int[] array40 = new int[4];
			array40[0] = 1;
			numericUpDown40.Minimum = new decimal(array40);
			this.numericUpDown_63.Name = MainForm.getString_0(107413300);
			this.numericUpDown_63.Size = new Size(47, 20);
			this.numericUpDown_63.TabIndex = 18;
			this.numericUpDown_63.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown41 = this.numericUpDown_63;
			int[] array41 = new int[4];
			array41[0] = 1;
			numericUpDown41.Value = new decimal(array41);
			this.numericUpDown_63.Leave += this.MainForm_Leave;
			this.label_169.AutoSize = true;
			this.label_169.Location = new Point(219, 161);
			this.label_169.Name = MainForm.getString_0(107412763);
			this.label_169.Size = new Size(53, 14);
			this.label_169.TabIndex = 35;
			this.label_169.Text = MainForm.getString_0(107384920);
			this.checkBox_57.AutoSize = true;
			this.checkBox_57.Location = new Point(6, 136);
			this.checkBox_57.Name = MainForm.getString_0(107412718);
			this.checkBox_57.Size = new Size(158, 18);
			this.checkBox_57.TabIndex = 15;
			this.checkBox_57.Text = MainForm.getString_0(107412729);
			this.checkBox_57.UseVisualStyleBackColor = true;
			this.checkBox_57.Leave += this.MainForm_Leave;
			NumericUpDown numericUpDown42 = this.numericUpDown_64;
			int[] array42 = new int[4];
			array42[0] = 5;
			numericUpDown42.Increment = new decimal(array42);
			this.numericUpDown_64.Location = new Point(169, 135);
			NumericUpDown numericUpDown43 = this.numericUpDown_64;
			int[] array43 = new int[4];
			array43[0] = 300;
			numericUpDown43.Maximum = new decimal(array43);
			NumericUpDown numericUpDown44 = this.numericUpDown_64;
			int[] array44 = new int[4];
			array44[0] = 1;
			numericUpDown44.Minimum = new decimal(array44);
			this.numericUpDown_64.Name = MainForm.getString_0(107412692);
			this.numericUpDown_64.Size = new Size(47, 20);
			this.numericUpDown_64.TabIndex = 16;
			this.numericUpDown_64.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown45 = this.numericUpDown_64;
			int[] array45 = new int[4];
			array45[0] = 1;
			numericUpDown45.Value = new decimal(array45);
			this.numericUpDown_64.Leave += this.MainForm_Leave;
			this.label_170.AutoSize = true;
			this.label_170.Location = new Point(219, 137);
			this.label_170.Name = MainForm.getString_0(107412663);
			this.label_170.Size = new Size(53, 14);
			this.label_170.TabIndex = 32;
			this.label_170.Text = MainForm.getString_0(107384920);
			this.tabPage_5.Controls.Add(this.groupBox_67);
			this.tabPage_5.Location = new Point(4, 23);
			this.tabPage_5.Name = MainForm.getString_0(107412618);
			this.tabPage_5.Padding = new Padding(3);
			this.tabPage_5.Size = new Size(509, 515);
			this.tabPage_5.TabIndex = 7;
			this.tabPage_5.Text = MainForm.getString_0(107389975);
			this.tabPage_5.UseVisualStyleBackColor = true;
			this.groupBox_67.Controls.Add(this.checkBox_58);
			this.groupBox_67.Controls.Add(this.label_171);
			this.groupBox_67.Controls.Add(this.numericUpDown_65);
			this.groupBox_67.Controls.Add(this.label_172);
			this.groupBox_67.Controls.Add(this.numericUpDown_66);
			this.groupBox_67.Controls.Add(this.label_173);
			this.groupBox_67.Controls.Add(this.numericUpDown_67);
			this.groupBox_67.Controls.Add(this.checkBox_59);
			this.groupBox_67.Controls.Add(this.label_174);
			this.groupBox_67.Controls.Add(this.numericUpDown_68);
			this.groupBox_67.Controls.Add(this.label_175);
			this.groupBox_67.Controls.Add(this.numericUpDown_69);
			this.groupBox_67.Controls.Add(this.checkBox_60);
			this.groupBox_67.Controls.Add(this.label_176);
			this.groupBox_67.Controls.Add(this.numericUpDown_70);
			this.groupBox_67.Location = new Point(3, 3);
			this.groupBox_67.Name = MainForm.getString_0(107412637);
			this.groupBox_67.Size = new Size(500, 156);
			this.groupBox_67.TabIndex = 32;
			this.groupBox_67.TabStop = false;
			this.groupBox_67.Text = MainForm.getString_0(107412588);
			this.checkBox_58.AutoSize = true;
			this.checkBox_58.Location = new Point(9, 133);
			this.checkBox_58.Name = MainForm.getString_0(107412595);
			this.checkBox_58.Size = new Size(87, 18);
			this.checkBox_58.TabIndex = 49;
			this.checkBox_58.Text = MainForm.getString_0(107412574);
			this.checkBox_58.UseVisualStyleBackColor = true;
			this.checkBox_58.CheckedChanged += this.checkBox_58_CheckedChanged;
			this.checkBox_58.Leave += this.MainForm_Leave;
			this.label_171.AutoSize = true;
			this.label_171.Location = new Point(410, 134);
			this.label_171.Name = MainForm.getString_0(107412525);
			this.label_171.Size = new Size(56, 14);
			this.label_171.TabIndex = 48;
			this.label_171.Text = MainForm.getString_0(107412544);
			this.numericUpDown_65.Location = new Point(362, 131);
			NumericUpDown numericUpDown46 = this.numericUpDown_65;
			int[] array46 = new int[4];
			array46[0] = 999;
			numericUpDown46.Maximum = new decimal(array46);
			NumericUpDown numericUpDown47 = this.numericUpDown_65;
			int[] array47 = new int[4];
			array47[0] = 1;
			numericUpDown47.Minimum = new decimal(array47);
			this.numericUpDown_65.Name = MainForm.getString_0(107412531);
			this.numericUpDown_65.Size = new Size(47, 20);
			this.numericUpDown_65.TabIndex = 47;
			this.numericUpDown_65.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown48 = this.numericUpDown_65;
			int[] array48 = new int[4];
			array48[0] = 1;
			numericUpDown48.Value = new decimal(array48);
			this.numericUpDown_65.Leave += this.MainForm_Leave;
			this.label_172.AutoSize = true;
			this.label_172.Location = new Point(144, 134);
			this.label_172.Name = MainForm.getString_0(107413014);
			this.label_172.Size = new Size(222, 14);
			this.label_172.TabIndex = 46;
			this.label_172.Text = MainForm.getString_0(107412969);
			this.numericUpDown_66.Location = new Point(96, 131);
			NumericUpDown numericUpDown49 = this.numericUpDown_66;
			int[] array49 = new int[4];
			array49[0] = 999;
			numericUpDown49.Maximum = new decimal(array49);
			NumericUpDown numericUpDown50 = this.numericUpDown_66;
			int[] array50 = new int[4];
			array50[0] = 1;
			numericUpDown50.Minimum = new decimal(array50);
			this.numericUpDown_66.Name = MainForm.getString_0(107412912);
			this.numericUpDown_66.Size = new Size(47, 20);
			this.numericUpDown_66.TabIndex = 45;
			this.numericUpDown_66.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown51 = this.numericUpDown_66;
			int[] array51 = new int[4];
			array51[0] = 1;
			numericUpDown51.Value = new decimal(array51);
			this.numericUpDown_66.Leave += this.MainForm_Leave;
			this.label_173.AutoSize = true;
			this.label_173.Location = new Point(332, 110);
			this.label_173.Name = MainForm.getString_0(107412915);
			this.label_173.Size = new Size(56, 14);
			this.label_173.TabIndex = 43;
			this.label_173.Text = MainForm.getString_0(107412544);
			this.numericUpDown_67.Location = new Point(284, 108);
			NumericUpDown numericUpDown52 = this.numericUpDown_67;
			int[] array52 = new int[4];
			array52[0] = 999;
			numericUpDown52.Maximum = new decimal(array52);
			NumericUpDown numericUpDown53 = this.numericUpDown_67;
			int[] array53 = new int[4];
			array53[0] = 1;
			numericUpDown53.Minimum = new decimal(array53);
			this.numericUpDown_67.Name = MainForm.getString_0(107412870);
			this.numericUpDown_67.Size = new Size(47, 20);
			this.numericUpDown_67.TabIndex = 42;
			this.numericUpDown_67.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown54 = this.numericUpDown_67;
			int[] array54 = new int[4];
			array54[0] = 1;
			numericUpDown54.Value = new decimal(array54);
			this.numericUpDown_67.Leave += this.MainForm_Leave;
			this.checkBox_59.AutoSize = true;
			this.checkBox_59.Location = new Point(9, 109);
			this.checkBox_59.Name = MainForm.getString_0(107412837);
			this.checkBox_59.Size = new Size(277, 18);
			this.checkBox_59.TabIndex = 41;
			this.checkBox_59.Text = MainForm.getString_0(107412808);
			this.checkBox_59.UseVisualStyleBackColor = true;
			this.checkBox_59.Leave += this.MainForm_Leave;
			this.label_174.AutoSize = true;
			this.label_174.Location = new Point(6, 64);
			this.label_174.Name = MainForm.getString_0(107412235);
			this.label_174.Size = new Size(222, 14);
			this.label_174.TabIndex = 40;
			this.label_174.Text = MainForm.getString_0(107412254);
			this.numericUpDown_68.Location = new Point(233, 62);
			NumericUpDown numericUpDown55 = this.numericUpDown_68;
			int[] array55 = new int[4];
			array55[0] = 20;
			numericUpDown55.Maximum = new decimal(array55);
			this.numericUpDown_68.Name = MainForm.getString_0(107412169);
			this.numericUpDown_68.Size = new Size(47, 20);
			this.numericUpDown_68.TabIndex = 39;
			this.numericUpDown_68.TextAlign = HorizontalAlignment.Center;
			this.numericUpDown_68.Leave += this.MainForm_Leave;
			this.label_175.AutoSize = true;
			this.label_175.Location = new Point(6, 40);
			this.label_175.Name = MainForm.getString_0(107412160);
			this.label_175.Size = new Size(212, 14);
			this.label_175.TabIndex = 38;
			this.label_175.Text = MainForm.getString_0(107412147);
			this.numericUpDown_69.Location = new Point(233, 38);
			NumericUpDown numericUpDown56 = this.numericUpDown_69;
			int[] array56 = new int[4];
			array56[0] = 20;
			numericUpDown56.Maximum = new decimal(array56);
			NumericUpDown numericUpDown57 = this.numericUpDown_69;
			int[] array57 = new int[4];
			array57[0] = 1;
			numericUpDown57.Minimum = new decimal(array57);
			this.numericUpDown_69.Name = MainForm.getString_0(107412070);
			this.numericUpDown_69.Size = new Size(47, 20);
			this.numericUpDown_69.TabIndex = 37;
			this.numericUpDown_69.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown58 = this.numericUpDown_69;
			int[] array58 = new int[4];
			array58[0] = 1;
			numericUpDown58.Value = new decimal(array58);
			this.numericUpDown_69.Leave += this.MainForm_Leave;
			this.checkBox_60.AutoSize = true;
			this.checkBox_60.Location = new Point(9, 85);
			this.checkBox_60.Name = MainForm.getString_0(107412037);
			this.checkBox_60.Size = new Size(260, 18);
			this.checkBox_60.TabIndex = 5;
			this.checkBox_60.Text = MainForm.getString_0(107412008);
			this.checkBox_60.UseVisualStyleBackColor = true;
			this.checkBox_60.Leave += this.MainForm_Leave;
			this.label_176.AutoSize = true;
			this.label_176.Location = new Point(6, 16);
			this.label_176.Name = MainForm.getString_0(107412463);
			this.label_176.Size = new Size(210, 14);
			this.label_176.TabIndex = 30;
			this.label_176.Text = MainForm.getString_0(107412450);
			this.numericUpDown_70.Location = new Point(233, 14);
			NumericUpDown numericUpDown59 = this.numericUpDown_70;
			int[] array59 = new int[4];
			array59[0] = 10;
			numericUpDown59.Maximum = new decimal(array59);
			this.numericUpDown_70.Name = MainForm.getString_0(107412437);
			this.numericUpDown_70.Size = new Size(47, 20);
			this.numericUpDown_70.TabIndex = 2;
			this.numericUpDown_70.TextAlign = HorizontalAlignment.Center;
			this.numericUpDown_70.Leave += this.MainForm_Leave;
			this.tabPage_29.Controls.Add(this.groupBox_50);
			this.tabPage_29.Controls.Add(this.groupBox_33);
			this.tabPage_29.Controls.Add(this.groupBox_26);
			this.tabPage_29.Location = new Point(4, 23);
			this.tabPage_29.Name = MainForm.getString_0(107412408);
			this.tabPage_29.Size = new Size(509, 515);
			this.tabPage_29.TabIndex = 14;
			this.tabPage_29.Text = MainForm.getString_0(107389930);
			this.tabPage_29.UseVisualStyleBackColor = true;
			this.groupBox_50.Controls.Add(this.listBox_0);
			this.groupBox_50.Controls.Add(this.button_62);
			this.groupBox_50.Controls.Add(this.button_63);
			this.groupBox_50.Location = new Point(2, 331);
			this.groupBox_50.Name = MainForm.getString_0(107412363);
			this.groupBox_50.Size = new Size(503, 85);
			this.groupBox_50.TabIndex = 2;
			this.groupBox_50.TabStop = false;
			this.groupBox_50.Text = MainForm.getString_0(107412378);
			this.listBox_0.FormattingEnabled = true;
			this.listBox_0.ItemHeight = 14;
			this.listBox_0.Location = new Point(38, 19);
			this.listBox_0.Name = MainForm.getString_0(107412345);
			this.listBox_0.Size = new Size(155, 60);
			this.listBox_0.TabIndex = 2;
			this.button_62.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107412320));
			this.button_62.Location = new Point(6, 19);
			this.button_62.Name = MainForm.getString_0(107412267);
			this.button_62.Size = new Size(26, 26);
			this.button_62.TabIndex = 1;
			this.button_62.UseVisualStyleBackColor = true;
			this.button_62.Click += this.button_62_Click;
			this.button_63.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107412286));
			this.button_63.Location = new Point(6, 51);
			this.button_63.Name = MainForm.getString_0(107411721);
			this.button_63.Size = new Size(26, 26);
			this.button_63.TabIndex = 0;
			this.button_63.UseVisualStyleBackColor = true;
			this.button_63.Click += this.button_63_Click;
			this.groupBox_33.Controls.Add(this.checkBox_76);
			this.groupBox_33.Controls.Add(this.label_164);
			this.groupBox_33.Controls.Add(this.numericUpDown_60);
			this.groupBox_33.Controls.Add(this.numericUpDown_61);
			this.groupBox_33.Controls.Add(this.label_165);
			this.groupBox_33.Controls.Add(this.label_166);
			this.groupBox_33.Controls.Add(this.label_158);
			this.groupBox_33.Controls.Add(this.numericUpDown_57);
			this.groupBox_33.Controls.Add(this.checkBox_31);
			this.groupBox_33.Location = new Point(3, 3);
			this.groupBox_33.Name = MainForm.getString_0(107411740);
			this.groupBox_33.Size = new Size(503, 111);
			this.groupBox_33.TabIndex = 1;
			this.groupBox_33.TabStop = false;
			this.groupBox_33.Text = MainForm.getString_0(107411691);
			this.checkBox_76.AutoSize = true;
			this.checkBox_76.Location = new Point(6, 43);
			this.checkBox_76.Name = MainForm.getString_0(107411702);
			this.checkBox_76.Size = new Size(293, 18);
			this.checkBox_76.TabIndex = 49;
			this.checkBox_76.Text = MainForm.getString_0(107411665);
			this.checkBox_76.UseVisualStyleBackColor = true;
			this.checkBox_76.Leave += this.MainForm_Leave;
			this.label_164.AutoSize = true;
			this.label_164.Location = new Point(199, 88);
			this.label_164.Name = MainForm.getString_0(107411568);
			this.label_164.Size = new Size(18, 14);
			this.label_164.TabIndex = 48;
			this.label_164.Text = MainForm.getString_0(107411555);
			this.numericUpDown_60.Location = new Point(219, 86);
			NumericUpDown numericUpDown60 = this.numericUpDown_60;
			int[] array60 = new int[4];
			array60[0] = 90;
			numericUpDown60.Maximum = new decimal(array60);
			NumericUpDown numericUpDown61 = this.numericUpDown_60;
			int[] array61 = new int[4];
			array61[0] = 60;
			numericUpDown61.Minimum = new decimal(array61);
			this.numericUpDown_60.Name = MainForm.getString_0(107411582);
			this.numericUpDown_60.Size = new Size(47, 20);
			this.numericUpDown_60.TabIndex = 47;
			this.numericUpDown_60.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown62 = this.numericUpDown_60;
			int[] array62 = new int[4];
			array62[0] = 60;
			numericUpDown62.Value = new decimal(array62);
			this.numericUpDown_61.Location = new Point(149, 86);
			NumericUpDown numericUpDown63 = this.numericUpDown_61;
			int[] array63 = new int[4];
			array63[0] = 60;
			numericUpDown63.Maximum = new decimal(array63);
			NumericUpDown numericUpDown64 = this.numericUpDown_61;
			int[] array64 = new int[4];
			array64[0] = 30;
			numericUpDown64.Minimum = new decimal(array64);
			this.numericUpDown_61.Name = MainForm.getString_0(107411525);
			this.numericUpDown_61.Size = new Size(47, 20);
			this.numericUpDown_61.TabIndex = 45;
			this.numericUpDown_61.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown65 = this.numericUpDown_61;
			int[] array65 = new int[4];
			array65[0] = 30;
			numericUpDown65.Value = new decimal(array65);
			this.label_165.AutoSize = true;
			this.label_165.Location = new Point(269, 88);
			this.label_165.Name = MainForm.getString_0(107411500);
			this.label_165.Size = new Size(53, 14);
			this.label_165.TabIndex = 46;
			this.label_165.Text = MainForm.getString_0(107384920);
			this.label_166.AutoSize = true;
			this.label_166.Location = new Point(3, 88);
			this.label_166.Name = MainForm.getString_0(107411519);
			this.label_166.Size = new Size(135, 14);
			this.label_166.TabIndex = 44;
			this.label_166.Text = MainForm.getString_0(107411506);
			this.label_158.AutoSize = true;
			this.label_158.Location = new Point(3, 64);
			this.label_158.Name = MainForm.getString_0(107411949);
			this.label_158.Size = new Size(210, 14);
			this.label_158.TabIndex = 32;
			this.label_158.Text = MainForm.getString_0(107412450);
			this.numericUpDown_57.Location = new Point(219, 62);
			NumericUpDown numericUpDown66 = this.numericUpDown_57;
			int[] array66 = new int[4];
			array66[0] = 10;
			numericUpDown66.Maximum = new decimal(array66);
			this.numericUpDown_57.Name = MainForm.getString_0(107411968);
			this.numericUpDown_57.Size = new Size(47, 20);
			this.numericUpDown_57.TabIndex = 31;
			this.numericUpDown_57.TextAlign = HorizontalAlignment.Center;
			this.checkBox_31.AutoSize = true;
			this.checkBox_31.Location = new Point(6, 19);
			this.checkBox_31.Name = MainForm.getString_0(107411931);
			this.checkBox_31.Size = new Size(264, 18);
			this.checkBox_31.TabIndex = 0;
			this.checkBox_31.Text = MainForm.getString_0(107411902);
			this.checkBox_31.UseVisualStyleBackColor = true;
			this.checkBox_31.Leave += this.MainForm_Leave;
			this.groupBox_26.Controls.Add(this.button_27);
			this.groupBox_26.Controls.Add(this.fastObjectListView_4);
			this.groupBox_26.Controls.Add(this.comboBox_28);
			this.groupBox_26.Controls.Add(this.comboBox_29);
			this.groupBox_26.Controls.Add(this.label_96);
			this.groupBox_26.Controls.Add(this.label_97);
			this.groupBox_26.Location = new Point(2, 120);
			this.groupBox_26.Name = MainForm.getString_0(107411813);
			this.groupBox_26.Size = new Size(503, 205);
			this.groupBox_26.TabIndex = 0;
			this.groupBox_26.TabStop = false;
			this.groupBox_26.Text = MainForm.getString_0(107411828);
			this.button_27.Location = new Point(6, 85);
			this.button_27.Name = MainForm.getString_0(107411803);
			this.button_27.Size = new Size(69, 20);
			this.button_27.TabIndex = 34;
			this.button_27.Text = MainForm.getString_0(107385117);
			this.button_27.UseVisualStyleBackColor = true;
			this.fastObjectListView_4.AllColumns.Add(this.olvcolumn_4);
			this.fastObjectListView_4.AllColumns.Add(this.olvcolumn_5);
			this.fastObjectListView_4.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_4,
				this.olvcolumn_5
			});
			this.fastObjectListView_4.FullRowSelect = true;
			this.fastObjectListView_4.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_4.HideSelection = false;
			this.fastObjectListView_4.Location = new Point(6, 111);
			this.fastObjectListView_4.MultiSelect = false;
			this.fastObjectListView_4.Name = MainForm.getString_0(107411774);
			this.fastObjectListView_4.OwnerDraw = true;
			this.fastObjectListView_4.ShowGroups = false;
			this.fastObjectListView_4.Size = new Size(491, 89);
			this.fastObjectListView_4.TabIndex = 33;
			this.fastObjectListView_4.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_4.View = View.Details;
			this.fastObjectListView_4.VirtualMode = true;
			this.olvcolumn_4.AspectName = MainForm.getString_0(107393328);
			this.olvcolumn_4.CellPadding = null;
			this.olvcolumn_4.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_4.Text = MainForm.getString_0(107393328);
			this.olvcolumn_4.Width = 196;
			this.olvcolumn_5.AspectName = MainForm.getString_0(107411229);
			this.olvcolumn_5.CellPadding = null;
			this.olvcolumn_5.FillsFreeSpace = true;
			this.olvcolumn_5.Text = MainForm.getString_0(107411180);
			this.olvcolumn_5.Width = 174;
			this.comboBox_28.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_28.FormattingEnabled = true;
			this.comboBox_28.Location = new Point(145, 55);
			this.comboBox_28.Name = MainForm.getString_0(107411195);
			this.comboBox_28.Size = new Size(195, 22);
			this.comboBox_28.TabIndex = 32;
			this.comboBox_28.Leave += this.MainForm_Leave;
			this.comboBox_29.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_29.FormattingEnabled = true;
			this.comboBox_29.Location = new Point(145, 19);
			this.comboBox_29.Name = MainForm.getString_0(107411138);
			this.comboBox_29.Size = new Size(195, 22);
			this.comboBox_29.TabIndex = 31;
			this.comboBox_29.Leave += this.MainForm_Leave;
			this.label_96.AutoSize = true;
			this.label_96.Location = new Point(3, 58);
			this.label_96.Name = MainForm.getString_0(107411117);
			this.label_96.Size = new Size(136, 14);
			this.label_96.TabIndex = 1;
			this.label_96.Text = MainForm.getString_0(107411136);
			this.label_97.AutoSize = true;
			this.label_97.Location = new Point(3, 22);
			this.label_97.Name = MainForm.getString_0(107411103);
			this.label_97.Size = new Size(61, 14);
			this.label_97.TabIndex = 0;
			this.label_97.Text = MainForm.getString_0(107411090);
			this.tabPage_7.Controls.Add(this.tabControl_1);
			this.tabPage_7.Location = new Point(4, 23);
			this.tabPage_7.Name = MainForm.getString_0(107411045);
			this.tabPage_7.Size = new Size(509, 515);
			this.tabPage_7.TabIndex = 11;
			this.tabPage_7.Text = MainForm.getString_0(107411064);
			this.tabPage_7.UseVisualStyleBackColor = true;
			this.tabControl_1.Controls.Add(this.tabPage_8);
			this.tabControl_1.Controls.Add(this.tabPage_10);
			this.tabControl_1.Controls.Add(this.tabPage_9);
			this.tabControl_1.Location = new Point(3, 3);
			this.tabControl_1.Name = MainForm.getString_0(107411011);
			this.tabControl_1.SelectedIndex = 0;
			this.tabControl_1.Size = new Size(503, 509);
			this.tabControl_1.TabIndex = 2;
			this.tabControl_1.SelectedIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_1.Leave += this.MainForm_Leave;
			this.tabPage_8.Controls.Add(this.checkBox_79);
			this.tabPage_8.Controls.Add(this.numericUpDown_75);
			this.tabPage_8.Controls.Add(this.checkBox_74);
			this.tabPage_8.Controls.Add(this.checkBox_66);
			this.tabPage_8.Controls.Add(this.checkBox_32);
			this.tabPage_8.Controls.Add(this.checkBox_26);
			this.tabPage_8.Controls.Add(this.checkBox_23);
			this.tabPage_8.Controls.Add(this.checkBox_24);
			this.tabPage_8.Controls.Add(this.checkBox_25);
			this.tabPage_8.Controls.Add(this.checkBox_20);
			this.tabPage_8.Controls.Add(this.checkBox_8);
			this.tabPage_8.Controls.Add(this.checkBox_9);
			this.tabPage_8.Controls.Add(this.checkBox_10);
			this.tabPage_8.Controls.Add(this.checkBox_11);
			this.tabPage_8.Controls.Add(this.checkBox_12);
			this.tabPage_8.Location = new Point(4, 23);
			this.tabPage_8.Name = MainForm.getString_0(107410986);
			this.tabPage_8.Padding = new Padding(3);
			this.tabPage_8.Size = new Size(495, 482);
			this.tabPage_8.TabIndex = 0;
			this.tabPage_8.Text = MainForm.getString_0(107411005);
			this.tabPage_8.UseVisualStyleBackColor = true;
			this.checkBox_79.AutoSize = true;
			this.checkBox_79.Location = new Point(6, 126);
			this.checkBox_79.Name = MainForm.getString_0(107410996);
			this.checkBox_79.Size = new Size(163, 18);
			this.checkBox_79.TabIndex = 27;
			this.checkBox_79.Text = MainForm.getString_0(107411475);
			this.checkBox_79.UseVisualStyleBackColor = true;
			this.checkBox_79.CheckedChanged += this.checkBox_79_CheckedChanged;
			this.checkBox_79.Leave += this.MainForm_Leave;
			this.numericUpDown_75.Enabled = false;
			NumericUpDown numericUpDown67 = this.numericUpDown_75;
			int[] array67 = new int[4];
			array67[0] = 5;
			numericUpDown67.Increment = new decimal(array67);
			this.numericUpDown_75.Location = new Point(169, 125);
			NumericUpDown numericUpDown68 = this.numericUpDown_75;
			int[] array68 = new int[4];
			array68[0] = 5000;
			numericUpDown68.Maximum = new decimal(array68);
			NumericUpDown numericUpDown69 = this.numericUpDown_75;
			int[] array69 = new int[4];
			array69[0] = 1;
			numericUpDown69.Minimum = new decimal(array69);
			this.numericUpDown_75.Name = MainForm.getString_0(107411406);
			this.numericUpDown_75.Size = new Size(47, 20);
			this.numericUpDown_75.TabIndex = 28;
			this.numericUpDown_75.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown70 = this.numericUpDown_75;
			int[] array70 = new int[4];
			array70[0] = 5;
			numericUpDown70.Value = new decimal(array70);
			this.numericUpDown_75.Leave += this.MainForm_Leave;
			this.checkBox_74.AutoSize = true;
			this.checkBox_74.Location = new Point(6, 6);
			this.checkBox_74.Name = MainForm.getString_0(107411373);
			this.checkBox_74.Size = new Size(143, 18);
			this.checkBox_74.TabIndex = 24;
			this.checkBox_74.Text = MainForm.getString_0(107411340);
			this.checkBox_74.UseVisualStyleBackColor = true;
			this.checkBox_66.AutoSize = true;
			this.checkBox_66.Location = new Point(6, 371);
			this.checkBox_66.Name = MainForm.getString_0(107411311);
			this.checkBox_66.Size = new Size(182, 18);
			this.checkBox_66.TabIndex = 23;
			this.checkBox_66.Text = MainForm.getString_0(107411314);
			this.checkBox_66.UseVisualStyleBackColor = true;
			this.checkBox_32.AutoSize = true;
			this.checkBox_32.Location = new Point(6, 333);
			this.checkBox_32.Name = MainForm.getString_0(107411245);
			this.checkBox_32.Size = new Size(186, 18);
			this.checkBox_32.TabIndex = 22;
			this.checkBox_32.Text = MainForm.getString_0(107410700);
			this.checkBox_32.UseVisualStyleBackColor = true;
			this.checkBox_26.Location = new Point(6, 247);
			this.checkBox_26.Name = MainForm.getString_0(107410659);
			this.checkBox_26.Size = new Size(181, 18);
			this.checkBox_26.TabIndex = 21;
			this.checkBox_26.Text = MainForm.getString_0(107410634);
			this.checkBox_26.UseVisualStyleBackColor = true;
			this.checkBox_23.AutoSize = true;
			this.checkBox_23.Location = new Point(6, 295);
			this.checkBox_23.Name = MainForm.getString_0(107410597);
			this.checkBox_23.Size = new Size(152, 18);
			this.checkBox_23.TabIndex = 20;
			this.checkBox_23.Text = MainForm.getString_0(107410592);
			this.checkBox_23.UseVisualStyleBackColor = true;
			this.checkBox_24.AutoSize = true;
			this.checkBox_24.Location = new Point(6, 271);
			this.checkBox_24.Name = MainForm.getString_0(107410531);
			this.checkBox_24.Size = new Size(157, 18);
			this.checkBox_24.TabIndex = 18;
			this.checkBox_24.Text = MainForm.getString_0(107410522);
			this.checkBox_24.UseVisualStyleBackColor = true;
			this.checkBox_25.AutoSize = true;
			this.checkBox_25.Location = new Point(6, 223);
			this.checkBox_25.Name = MainForm.getString_0(107410489);
			this.checkBox_25.Size = new Size(152, 18);
			this.checkBox_25.TabIndex = 17;
			this.checkBox_25.Text = MainForm.getString_0(107410968);
			this.checkBox_25.UseVisualStyleBackColor = true;
			this.checkBox_20.AutoSize = true;
			this.checkBox_20.Location = new Point(6, 30);
			this.checkBox_20.Name = MainForm.getString_0(107410935);
			this.checkBox_20.Size = new Size(111, 18);
			this.checkBox_20.TabIndex = 8;
			this.checkBox_20.Text = MainForm.getString_0(107410902);
			this.checkBox_20.UseVisualStyleBackColor = true;
			this.checkBox_8.AutoSize = true;
			this.checkBox_8.Location = new Point(6, 54);
			this.checkBox_8.Name = MainForm.getString_0(107410849);
			this.checkBox_8.Size = new Size(105, 18);
			this.checkBox_8.TabIndex = 4;
			this.checkBox_8.Text = MainForm.getString_0(107410848);
			this.checkBox_8.UseVisualStyleBackColor = true;
			this.checkBox_8.Leave += this.MainForm_Leave;
			this.checkBox_9.AutoSize = true;
			this.checkBox_9.Location = new Point(6, 185);
			this.checkBox_9.Name = MainForm.getString_0(107410795);
			this.checkBox_9.Size = new Size(172, 18);
			this.checkBox_9.TabIndex = 3;
			this.checkBox_9.Text = MainForm.getString_0(107410754);
			this.checkBox_9.UseVisualStyleBackColor = true;
			this.checkBox_9.Leave += this.MainForm_Leave;
			this.checkBox_10.AutoSize = true;
			this.checkBox_10.Location = new Point(6, 161);
			this.checkBox_10.Name = MainForm.getString_0(107410749);
			this.checkBox_10.Size = new Size(152, 18);
			this.checkBox_10.TabIndex = 7;
			this.checkBox_10.Text = MainForm.getString_0(107410200);
			this.checkBox_10.UseVisualStyleBackColor = true;
			this.checkBox_10.Leave += this.MainForm_Leave;
			this.checkBox_11.AutoSize = true;
			this.checkBox_11.Location = new Point(6, 102);
			this.checkBox_11.Name = MainForm.getString_0(107410167);
			this.checkBox_11.Size = new Size(92, 18);
			this.checkBox_11.TabIndex = 6;
			this.checkBox_11.Text = MainForm.getString_0(107410130);
			this.checkBox_11.UseVisualStyleBackColor = true;
			this.checkBox_11.Leave += this.MainForm_Leave;
			this.checkBox_12.AutoSize = true;
			this.checkBox_12.Location = new Point(6, 78);
			this.checkBox_12.Name = MainForm.getString_0(107410081);
			this.checkBox_12.Size = new Size(121, 18);
			this.checkBox_12.TabIndex = 5;
			this.checkBox_12.Text = MainForm.getString_0(107410076);
			this.checkBox_12.UseVisualStyleBackColor = true;
			this.checkBox_12.Leave += this.MainForm_Leave;
			this.tabPage_10.Controls.Add(this.checkBox_61);
			this.tabPage_10.Controls.Add(this.label_178);
			this.tabPage_10.Controls.Add(this.textBox_22);
			this.tabPage_10.Controls.Add(this.label_179);
			this.tabPage_10.Controls.Add(this.textBox_23);
			this.tabPage_10.Controls.Add(this.checkBox_14);
			this.tabPage_10.Controls.Add(this.label_34);
			this.tabPage_10.Controls.Add(this.textBox_7);
			this.tabPage_10.Location = new Point(4, 23);
			this.tabPage_10.Name = MainForm.getString_0(107410019);
			this.tabPage_10.Size = new Size(495, 482);
			this.tabPage_10.TabIndex = 2;
			this.tabPage_10.Text = MainForm.getString_0(107410038);
			this.tabPage_10.UseVisualStyleBackColor = true;
			this.checkBox_61.AutoSize = true;
			this.checkBox_61.Location = new Point(358, 78);
			this.checkBox_61.Name = MainForm.getString_0(107409993);
			this.checkBox_61.Size = new Size(136, 18);
			this.checkBox_61.TabIndex = 9;
			this.checkBox_61.Text = MainForm.getString_0(107409968);
			this.checkBox_61.UseVisualStyleBackColor = true;
			this.checkBox_61.CheckedChanged += this.checkBox_61_CheckedChanged;
			this.checkBox_61.Leave += this.MainForm_Leave;
			this.label_178.AutoSize = true;
			this.label_178.Location = new Point(3, 79);
			this.label_178.Name = MainForm.getString_0(107409975);
			this.label_178.Size = new Size(67, 14);
			this.label_178.TabIndex = 7;
			this.label_178.Text = MainForm.getString_0(107410442);
			this.textBox_22.Location = new Point(89, 76);
			this.textBox_22.Name = MainForm.getString_0(107410461);
			this.textBox_22.Size = new Size(263, 20);
			this.textBox_22.TabIndex = 8;
			this.textBox_22.Leave += this.MainForm_Leave;
			this.label_179.AutoSize = true;
			this.label_179.Location = new Point(3, 53);
			this.label_179.Name = MainForm.getString_0(107410404);
			this.label_179.Size = new Size(63, 14);
			this.label_179.TabIndex = 5;
			this.label_179.Text = MainForm.getString_0(107410423);
			this.textBox_23.Location = new Point(89, 50);
			this.textBox_23.Name = MainForm.getString_0(107410374);
			this.textBox_23.Size = new Size(263, 20);
			this.textBox_23.TabIndex = 6;
			this.textBox_23.Leave += this.MainForm_Leave;
			this.checkBox_14.AutoSize = true;
			this.checkBox_14.Location = new Point(6, 6);
			this.checkBox_14.Name = MainForm.getString_0(107410385);
			this.checkBox_14.Size = new Size(69, 18);
			this.checkBox_14.TabIndex = 3;
			this.checkBox_14.Text = MainForm.getString_0(107413127);
			this.checkBox_14.UseVisualStyleBackColor = true;
			this.checkBox_14.Leave += this.MainForm_Leave;
			this.label_34.AutoSize = true;
			this.label_34.Location = new Point(3, 27);
			this.label_34.Name = MainForm.getString_0(107410360);
			this.label_34.Size = new Size(80, 14);
			this.label_34.TabIndex = 1;
			this.label_34.Text = MainForm.getString_0(107410315);
			this.textBox_7.Location = new Point(89, 24);
			this.textBox_7.Name = MainForm.getString_0(107410330);
			this.textBox_7.Size = new Size(263, 20);
			this.textBox_7.TabIndex = 4;
			this.textBox_7.Leave += this.MainForm_Leave;
			this.tabPage_9.Controls.Add(this.checkBox_13);
			this.tabPage_9.Controls.Add(this.label_33);
			this.tabPage_9.Controls.Add(this.textBox_6);
			this.tabPage_9.Location = new Point(4, 23);
			this.tabPage_9.Name = MainForm.getString_0(107410281);
			this.tabPage_9.Padding = new Padding(3);
			this.tabPage_9.Size = new Size(495, 482);
			this.tabPage_9.TabIndex = 1;
			this.tabPage_9.Text = MainForm.getString_0(107410300);
			this.tabPage_9.UseVisualStyleBackColor = true;
			this.checkBox_13.AutoSize = true;
			this.checkBox_13.Location = new Point(6, 6);
			this.checkBox_13.Name = MainForm.getString_0(107410251);
			this.checkBox_13.Size = new Size(69, 18);
			this.checkBox_13.TabIndex = 3;
			this.checkBox_13.Text = MainForm.getString_0(107413127);
			this.checkBox_13.UseVisualStyleBackColor = true;
			this.checkBox_13.Leave += this.MainForm_Leave;
			this.label_33.AutoSize = true;
			this.label_33.Location = new Point(3, 40);
			this.label_33.Name = MainForm.getString_0(107410222);
			this.label_33.Size = new Size(44, 14);
			this.label_33.TabIndex = 1;
			this.label_33.Text = MainForm.getString_0(107410209);
			this.textBox_6.Location = new Point(53, 37);
			this.textBox_6.Name = MainForm.getString_0(107410232);
			this.textBox_6.Size = new Size(263, 20);
			this.textBox_6.TabIndex = 4;
			this.textBox_6.Leave += this.MainForm_Leave;
			this.tabPage_11.Controls.Add(this.tabControl_2);
			this.tabPage_11.Location = new Point(4, 23);
			this.tabPage_11.Name = MainForm.getString_0(107409695);
			this.tabPage_11.Padding = new Padding(3);
			this.tabPage_11.Size = new Size(509, 515);
			this.tabPage_11.TabIndex = 8;
			this.tabPage_11.Text = MainForm.getString_0(107409682);
			this.tabPage_11.UseVisualStyleBackColor = true;
			this.tabControl_2.Controls.Add(this.tabPage_17);
			this.tabControl_2.Controls.Add(this.tabPage_12);
			this.tabControl_2.Controls.Add(this.tabPage_13);
			this.tabControl_2.Controls.Add(this.tabPage_14);
			this.tabControl_2.Controls.Add(this.tabPage_15);
			this.tabControl_2.Location = new Point(3, 3);
			this.tabControl_2.Name = MainForm.getString_0(107409637);
			this.tabControl_2.SelectedIndex = 0;
			this.tabControl_2.Size = new Size(500, 509);
			this.tabControl_2.TabIndex = 2;
			this.tabControl_2.SelectedIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_2.TabIndexChanged += this.tabControl_2_TabIndexChanged;
			this.tabControl_2.Leave += this.MainForm_Leave;
			this.tabPage_17.Controls.Add(this.groupBox_20);
			this.tabPage_17.Location = new Point(4, 23);
			this.tabPage_17.Name = MainForm.getString_0(107409652);
			this.tabPage_17.Size = new Size(492, 482);
			this.tabPage_17.TabIndex = 5;
			this.tabPage_17.Text = MainForm.getString_0(107409603);
			this.tabPage_17.UseVisualStyleBackColor = true;
			this.groupBox_20.Controls.Add(this.radioButton_0);
			this.groupBox_20.Controls.Add(this.radioButton_1);
			this.groupBox_20.Controls.Add(this.radioButton_2);
			this.groupBox_20.Location = new Point(6, 6);
			this.groupBox_20.Name = MainForm.getString_0(107409622);
			this.groupBox_20.Size = new Size(483, 473);
			this.groupBox_20.TabIndex = 0;
			this.groupBox_20.TabStop = false;
			this.groupBox_20.Text = MainForm.getString_0(107409573);
			this.radioButton_0.AutoSize = true;
			this.radioButton_0.Location = new Point(6, 67);
			this.radioButton_0.Name = MainForm.getString_0(107409552);
			this.radioButton_0.Size = new Size(117, 18);
			this.radioButton_0.TabIndex = 6;
			this.radioButton_0.TabStop = true;
			this.radioButton_0.Text = MainForm.getString_0(107391787);
			this.radioButton_0.UseVisualStyleBackColor = true;
			this.radioButton_0.CheckedChanged += this.radioButton_2_CheckedChanged;
			this.radioButton_1.AutoSize = true;
			this.radioButton_1.Location = new Point(6, 43);
			this.radioButton_1.Name = MainForm.getString_0(107409563);
			this.radioButton_1.Size = new Size(96, 18);
			this.radioButton_1.TabIndex = 5;
			this.radioButton_1.TabStop = true;
			this.radioButton_1.Text = MainForm.getString_0(107391256);
			this.radioButton_1.UseVisualStyleBackColor = true;
			this.radioButton_1.CheckedChanged += this.radioButton_2_CheckedChanged;
			this.radioButton_2.AutoSize = true;
			this.radioButton_2.Location = new Point(6, 19);
			this.radioButton_2.Name = MainForm.getString_0(107409510);
			this.radioButton_2.Size = new Size(112, 18);
			this.radioButton_2.TabIndex = 2;
			this.radioButton_2.TabStop = true;
			this.radioButton_2.Text = MainForm.getString_0(107391798);
			this.radioButton_2.UseVisualStyleBackColor = true;
			this.radioButton_2.CheckedChanged += this.radioButton_2_CheckedChanged;
			this.tabPage_12.Controls.Add(this.groupBox_10);
			this.tabPage_12.Location = new Point(4, 23);
			this.tabPage_12.Name = MainForm.getString_0(107409521);
			this.tabPage_12.Padding = new Padding(3);
			this.tabPage_12.Size = new Size(492, 482);
			this.tabPage_12.TabIndex = 0;
			this.tabPage_12.Text = MainForm.getString_0(107409476);
			this.tabPage_12.UseVisualStyleBackColor = true;
			this.groupBox_10.Controls.Add(this.label_183);
			this.groupBox_10.Controls.Add(this.numericUpDown_73);
			this.groupBox_10.Controls.Add(this.label_184);
			this.groupBox_10.Controls.Add(this.label_180);
			this.groupBox_10.Controls.Add(this.numericUpDown_71);
			this.groupBox_10.Controls.Add(this.label_181);
			this.groupBox_10.Controls.Add(this.label_156);
			this.groupBox_10.Controls.Add(this.numericUpDown_56);
			this.groupBox_10.Controls.Add(this.label_157);
			this.groupBox_10.Controls.Add(this.label_138);
			this.groupBox_10.Controls.Add(this.numericUpDown_52);
			this.groupBox_10.Controls.Add(this.label_139);
			this.groupBox_10.Controls.Add(this.label_100);
			this.groupBox_10.Controls.Add(this.trackBar_0);
			this.groupBox_10.Controls.Add(this.label_35);
			this.groupBox_10.Controls.Add(this.numericUpDown_10);
			this.groupBox_10.Controls.Add(this.label_36);
			this.groupBox_10.Controls.Add(this.label_37);
			this.groupBox_10.Controls.Add(this.numericUpDown_11);
			this.groupBox_10.Controls.Add(this.label_38);
			this.groupBox_10.Controls.Add(this.label_39);
			this.groupBox_10.Controls.Add(this.numericUpDown_12);
			this.groupBox_10.Controls.Add(this.label_40);
			this.groupBox_10.Controls.Add(this.numericUpDown_13);
			this.groupBox_10.Controls.Add(this.label_41);
			this.groupBox_10.Controls.Add(this.numericUpDown_14);
			this.groupBox_10.Controls.Add(this.label_42);
			this.groupBox_10.Controls.Add(this.label_43);
			this.groupBox_10.Controls.Add(this.label_44);
			this.groupBox_10.Location = new Point(6, 6);
			this.groupBox_10.Name = MainForm.getString_0(107409495);
			this.groupBox_10.Size = new Size(480, 473);
			this.groupBox_10.TabIndex = 2;
			this.groupBox_10.TabStop = false;
			this.groupBox_10.Text = MainForm.getString_0(107409476);
			this.label_183.AutoSize = true;
			this.label_183.Location = new Point(222, 146);
			this.label_183.Name = MainForm.getString_0(107409466);
			this.label_183.Size = new Size(86, 14);
			this.label_183.TabIndex = 66;
			this.label_183.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown71 = this.numericUpDown_73;
			int[] array71 = new int[4];
			array71[0] = 5;
			numericUpDown71.Increment = new decimal(array71);
			this.numericUpDown_73.Location = new Point(164, 144);
			NumericUpDown numericUpDown72 = this.numericUpDown_73;
			int[] array72 = new int[4];
			array72[0] = 1000;
			numericUpDown72.Maximum = new decimal(array72);
			this.numericUpDown_73.Name = MainForm.getString_0(107409944);
			this.numericUpDown_73.Size = new Size(52, 20);
			this.numericUpDown_73.TabIndex = 64;
			this.numericUpDown_73.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown73 = this.numericUpDown_73;
			int[] array73 = new int[4];
			array73[0] = 200;
			numericUpDown73.Value = new decimal(array73);
			this.label_184.AutoSize = true;
			this.label_184.Location = new Point(6, 146);
			this.label_184.Name = MainForm.getString_0(107409915);
			this.label_184.Size = new Size(109, 14);
			this.label_184.TabIndex = 65;
			this.label_184.Text = MainForm.getString_0(107409870);
			this.label_180.AutoSize = true;
			this.label_180.Location = new Point(222, 224);
			this.label_180.Name = MainForm.getString_0(107409873);
			this.label_180.Size = new Size(61, 14);
			this.label_180.TabIndex = 63;
			this.label_180.Text = MainForm.getString_0(107409828);
			this.numericUpDown_71.Location = new Point(164, 222);
			NumericUpDown numericUpDown74 = this.numericUpDown_71;
			int[] array74 = new int[4];
			array74[0] = 10;
			numericUpDown74.Maximum = new decimal(array74);
			NumericUpDown numericUpDown75 = this.numericUpDown_71;
			int[] array75 = new int[4];
			array75[0] = 2;
			numericUpDown75.Minimum = new decimal(array75);
			this.numericUpDown_71.Name = MainForm.getString_0(107409847);
			this.numericUpDown_71.Size = new Size(52, 20);
			this.numericUpDown_71.TabIndex = 61;
			this.numericUpDown_71.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown76 = this.numericUpDown_71;
			int[] array76 = new int[4];
			array76[0] = 10;
			numericUpDown76.Value = new decimal(array76);
			this.numericUpDown_71.Leave += this.MainForm_Leave;
			this.label_181.AutoSize = true;
			this.label_181.Location = new Point(6, 224);
			this.label_181.Name = MainForm.getString_0(107409822);
			this.label_181.Size = new Size(144, 14);
			this.label_181.TabIndex = 62;
			this.label_181.Text = MainForm.getString_0(107409809);
			this.label_156.AutoSize = true;
			this.label_156.Location = new Point(222, 198);
			this.label_156.Name = MainForm.getString_0(107409744);
			this.label_156.Size = new Size(63, 14);
			this.label_156.TabIndex = 60;
			this.label_156.Text = MainForm.getString_0(107409731);
			this.numericUpDown_56.Location = new Point(164, 196);
			NumericUpDown numericUpDown77 = this.numericUpDown_56;
			int[] array77 = new int[4];
			array77[0] = 30;
			numericUpDown77.Maximum = new decimal(array77);
			NumericUpDown numericUpDown78 = this.numericUpDown_56;
			int[] array78 = new int[4];
			array78[0] = 1;
			numericUpDown78.Minimum = new decimal(array78);
			this.numericUpDown_56.Name = MainForm.getString_0(107409750);
			this.numericUpDown_56.Size = new Size(52, 20);
			this.numericUpDown_56.TabIndex = 58;
			this.numericUpDown_56.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown79 = this.numericUpDown_56;
			int[] array79 = new int[4];
			array79[0] = 30;
			numericUpDown79.Value = new decimal(array79);
			this.numericUpDown_56.Leave += this.MainForm_Leave;
			this.label_157.AutoSize = true;
			this.label_157.Location = new Point(6, 198);
			this.label_157.Name = MainForm.getString_0(107409725);
			this.label_157.Size = new Size(85, 14);
			this.label_157.TabIndex = 59;
			this.label_157.Text = MainForm.getString_0(107409168);
			this.label_138.AutoSize = true;
			this.label_138.Location = new Point(222, 120);
			this.label_138.Name = MainForm.getString_0(107409183);
			this.label_138.Size = new Size(86, 14);
			this.label_138.TabIndex = 57;
			this.label_138.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown80 = this.numericUpDown_52;
			int[] array80 = new int[4];
			array80[0] = 5;
			numericUpDown80.Increment = new decimal(array80);
			this.numericUpDown_52.Location = new Point(164, 118);
			NumericUpDown numericUpDown81 = this.numericUpDown_52;
			int[] array81 = new int[4];
			array81[0] = 1000;
			numericUpDown81.Maximum = new decimal(array81);
			NumericUpDown numericUpDown82 = this.numericUpDown_52;
			int[] array82 = new int[4];
			array82[0] = 5;
			numericUpDown82.Minimum = new decimal(array82);
			this.numericUpDown_52.Name = MainForm.getString_0(107409170);
			this.numericUpDown_52.Size = new Size(52, 20);
			this.numericUpDown_52.TabIndex = 55;
			this.numericUpDown_52.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown83 = this.numericUpDown_52;
			int[] array83 = new int[4];
			array83[0] = 200;
			numericUpDown83.Value = new decimal(array83);
			this.label_139.AutoSize = true;
			this.label_139.Location = new Point(6, 120);
			this.label_139.Name = MainForm.getString_0(107409101);
			this.label_139.Size = new Size(111, 14);
			this.label_139.TabIndex = 56;
			this.label_139.Text = MainForm.getString_0(107409120);
			this.label_100.AutoSize = true;
			this.label_100.Location = new Point(6, 255);
			this.label_100.Name = MainForm.getString_0(107409059);
			this.label_100.Size = new Size(149, 14);
			this.label_100.TabIndex = 54;
			this.label_100.Text = MainForm.getString_0(107409078);
			this.trackBar_0.LargeChange = 2;
			this.trackBar_0.Location = new Point(9, 272);
			this.trackBar_0.Name = MainForm.getString_0(107409049);
			this.trackBar_0.Size = new Size(230, 45);
			this.trackBar_0.TabIndex = 53;
			this.trackBar_0.Leave += this.MainForm_Leave;
			this.label_35.AutoSize = true;
			this.label_35.Location = new Point(222, 42);
			this.label_35.Name = MainForm.getString_0(107409000);
			this.label_35.Size = new Size(86, 14);
			this.label_35.TabIndex = 52;
			this.label_35.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown84 = this.numericUpDown_10;
			int[] array84 = new int[4];
			array84[0] = 5;
			numericUpDown84.Increment = new decimal(array84);
			this.numericUpDown_10.Location = new Point(164, 40);
			NumericUpDown numericUpDown85 = this.numericUpDown_10;
			int[] array85 = new int[4];
			array85[0] = 1000;
			numericUpDown85.Maximum = new decimal(array85);
			NumericUpDown numericUpDown86 = this.numericUpDown_10;
			int[] array86 = new int[4];
			array86[0] = 5;
			numericUpDown86.Minimum = new decimal(array86);
			this.numericUpDown_10.Name = MainForm.getString_0(107409019);
			this.numericUpDown_10.Size = new Size(52, 20);
			this.numericUpDown_10.TabIndex = 10;
			this.numericUpDown_10.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown87 = this.numericUpDown_10;
			int[] array87 = new int[4];
			array87[0] = 200;
			numericUpDown87.Value = new decimal(array87);
			this.label_36.AutoSize = true;
			this.label_36.Location = new Point(6, 42);
			this.label_36.Name = MainForm.getString_0(107408990);
			this.label_36.Size = new Size(88, 14);
			this.label_36.TabIndex = 50;
			this.label_36.Text = MainForm.getString_0(107408933);
			this.label_37.AutoSize = true;
			this.label_37.Location = new Point(222, 172);
			this.label_37.Name = MainForm.getString_0(107409424);
			this.label_37.Size = new Size(86, 14);
			this.label_37.TabIndex = 49;
			this.label_37.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown88 = this.numericUpDown_11;
			int[] array88 = new int[4];
			array88[0] = 5;
			numericUpDown88.Increment = new decimal(array88);
			this.numericUpDown_11.Location = new Point(164, 170);
			NumericUpDown numericUpDown89 = this.numericUpDown_11;
			int[] array89 = new int[4];
			array89[0] = 1000;
			numericUpDown89.Maximum = new decimal(array89);
			NumericUpDown numericUpDown90 = this.numericUpDown_11;
			int[] array90 = new int[4];
			array90[0] = 5;
			numericUpDown90.Minimum = new decimal(array90);
			this.numericUpDown_11.Name = MainForm.getString_0(107409411);
			this.numericUpDown_11.Size = new Size(52, 20);
			this.numericUpDown_11.TabIndex = 13;
			this.numericUpDown_11.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown91 = this.numericUpDown_11;
			int[] array91 = new int[4];
			array91[0] = 200;
			numericUpDown91.Value = new decimal(array91);
			this.numericUpDown_11.Leave += this.MainForm_Leave;
			this.label_38.AutoSize = true;
			this.label_38.Location = new Point(6, 172);
			this.label_38.Name = MainForm.getString_0(107409382);
			this.label_38.Size = new Size(146, 14);
			this.label_38.TabIndex = 47;
			this.label_38.Text = MainForm.getString_0(107409353);
			this.label_39.AutoSize = true;
			this.label_39.Location = new Point(222, 94);
			this.label_39.Name = MainForm.getString_0(107409320);
			this.label_39.Size = new Size(86, 14);
			this.label_39.TabIndex = 43;
			this.label_39.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown92 = this.numericUpDown_12;
			int[] array92 = new int[4];
			array92[0] = 5;
			numericUpDown92.Increment = new decimal(array92);
			this.numericUpDown_12.Location = new Point(164, 92);
			NumericUpDown numericUpDown93 = this.numericUpDown_12;
			int[] array93 = new int[4];
			array93[0] = 1000;
			numericUpDown93.Maximum = new decimal(array93);
			NumericUpDown numericUpDown94 = this.numericUpDown_12;
			int[] array94 = new int[4];
			array94[0] = 5;
			numericUpDown94.Minimum = new decimal(array94);
			this.numericUpDown_12.Name = MainForm.getString_0(107409339);
			this.numericUpDown_12.Size = new Size(52, 20);
			this.numericUpDown_12.TabIndex = 12;
			this.numericUpDown_12.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown95 = this.numericUpDown_12;
			int[] array95 = new int[4];
			array95[0] = 200;
			numericUpDown95.Value = new decimal(array95);
			this.numericUpDown_12.Leave += this.MainForm_Leave;
			this.label_40.AutoSize = true;
			this.label_40.Location = new Point(222, 68);
			this.label_40.Name = MainForm.getString_0(107409262);
			this.label_40.Size = new Size(86, 14);
			this.label_40.TabIndex = 41;
			this.label_40.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown96 = this.numericUpDown_13;
			int[] array96 = new int[4];
			array96[0] = 5;
			numericUpDown96.Increment = new decimal(array96);
			this.numericUpDown_13.Location = new Point(164, 66);
			NumericUpDown numericUpDown97 = this.numericUpDown_13;
			int[] array97 = new int[4];
			array97[0] = 1000;
			numericUpDown97.Maximum = new decimal(array97);
			NumericUpDown numericUpDown98 = this.numericUpDown_13;
			int[] array98 = new int[4];
			array98[0] = 5;
			numericUpDown98.Minimum = new decimal(array98);
			this.numericUpDown_13.Name = MainForm.getString_0(107409249);
			this.numericUpDown_13.Size = new Size(52, 20);
			this.numericUpDown_13.TabIndex = 11;
			this.numericUpDown_13.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown99 = this.numericUpDown_13;
			int[] array99 = new int[4];
			array99[0] = 200;
			numericUpDown99.Value = new decimal(array99);
			this.numericUpDown_13.Leave += this.MainForm_Leave;
			this.label_41.AutoSize = true;
			this.label_41.Location = new Point(222, 16);
			this.label_41.Name = MainForm.getString_0(107409244);
			this.label_41.Size = new Size(86, 14);
			this.label_41.TabIndex = 39;
			this.label_41.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown100 = this.numericUpDown_14;
			int[] array100 = new int[4];
			array100[0] = 5;
			numericUpDown100.Increment = new decimal(array100);
			this.numericUpDown_14.Location = new Point(164, 14);
			NumericUpDown numericUpDown101 = this.numericUpDown_14;
			int[] array101 = new int[4];
			array101[0] = 1000;
			numericUpDown101.Maximum = new decimal(array101);
			NumericUpDown numericUpDown102 = this.numericUpDown_14;
			int[] array102 = new int[4];
			array102[0] = 5;
			numericUpDown102.Minimum = new decimal(array102);
			this.numericUpDown_14.Name = MainForm.getString_0(107409199);
			this.numericUpDown_14.Size = new Size(52, 20);
			this.numericUpDown_14.TabIndex = 9;
			this.numericUpDown_14.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown103 = this.numericUpDown_14;
			int[] array103 = new int[4];
			array103[0] = 200;
			numericUpDown103.Value = new decimal(array103);
			this.numericUpDown_14.Leave += this.MainForm_Leave;
			this.label_42.AutoSize = true;
			this.label_42.Location = new Point(6, 16);
			this.label_42.Name = MainForm.getString_0(107408650);
			this.label_42.Size = new Size(106, 14);
			this.label_42.TabIndex = 35;
			this.label_42.Text = MainForm.getString_0(107408657);
			this.label_43.AutoSize = true;
			this.label_43.Location = new Point(6, 94);
			this.label_43.Name = MainForm.getString_0(107408632);
			this.label_43.Size = new Size(150, 14);
			this.label_43.TabIndex = 34;
			this.label_43.Text = MainForm.getString_0(107408595);
			this.label_44.AutoSize = true;
			this.label_44.Location = new Point(6, 68);
			this.label_44.Name = MainForm.getString_0(107408526);
			this.label_44.Size = new Size(129, 14);
			this.label_44.TabIndex = 33;
			this.label_44.Text = MainForm.getString_0(107408493);
			this.tabPage_13.Controls.Add(this.groupBox_11);
			this.tabPage_13.Location = new Point(4, 23);
			this.tabPage_13.Name = MainForm.getString_0(107408460);
			this.tabPage_13.Padding = new Padding(3);
			this.tabPage_13.Size = new Size(492, 482);
			this.tabPage_13.TabIndex = 1;
			this.tabPage_13.Text = MainForm.getString_0(107389975);
			this.tabPage_13.UseVisualStyleBackColor = true;
			this.groupBox_11.Controls.Add(this.label_45);
			this.groupBox_11.Controls.Add(this.numericUpDown_15);
			this.groupBox_11.Controls.Add(this.label_46);
			this.groupBox_11.Controls.Add(this.label_47);
			this.groupBox_11.Controls.Add(this.label_48);
			this.groupBox_11.Controls.Add(this.label_49);
			this.groupBox_11.Controls.Add(this.label_50);
			this.groupBox_11.Controls.Add(this.label_51);
			this.groupBox_11.Controls.Add(this.numericUpDown_16);
			this.groupBox_11.Controls.Add(this.label_52);
			this.groupBox_11.Controls.Add(this.numericUpDown_17);
			this.groupBox_11.Controls.Add(this.label_53);
			this.groupBox_11.Controls.Add(this.numericUpDown_18);
			this.groupBox_11.Controls.Add(this.label_54);
			this.groupBox_11.Controls.Add(this.numericUpDown_19);
			this.groupBox_11.Controls.Add(this.label_55);
			this.groupBox_11.Controls.Add(this.numericUpDown_20);
			this.groupBox_11.Controls.Add(this.label_56);
			this.groupBox_11.Controls.Add(this.numericUpDown_21);
			this.groupBox_11.Controls.Add(this.label_57);
			this.groupBox_11.Controls.Add(this.label_58);
			this.groupBox_11.Controls.Add(this.label_59);
			this.groupBox_11.Controls.Add(this.label_60);
			this.groupBox_11.Controls.Add(this.numericUpDown_22);
			this.groupBox_11.Location = new Point(6, 6);
			this.groupBox_11.Name = MainForm.getString_0(107408479);
			this.groupBox_11.Size = new Size(480, 473);
			this.groupBox_11.TabIndex = 3;
			this.groupBox_11.TabStop = false;
			this.groupBox_11.Text = MainForm.getString_0(107389975);
			this.label_45.AutoSize = true;
			this.label_45.Location = new Point(220, 199);
			this.label_45.Name = MainForm.getString_0(107408418);
			this.label_45.Size = new Size(86, 14);
			this.label_45.TabIndex = 52;
			this.label_45.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown104 = this.numericUpDown_15;
			int[] array104 = new int[4];
			array104[0] = 5;
			numericUpDown104.Increment = new decimal(array104);
			this.numericUpDown_15.Location = new Point(162, 197);
			NumericUpDown numericUpDown105 = this.numericUpDown_15;
			int[] array105 = new int[4];
			array105[0] = 1000;
			numericUpDown105.Maximum = new decimal(array105);
			NumericUpDown numericUpDown106 = this.numericUpDown_15;
			int[] array106 = new int[4];
			array106[0] = 5;
			numericUpDown106.Minimum = new decimal(array106);
			this.numericUpDown_15.Name = MainForm.getString_0(107408437);
			this.numericUpDown_15.Size = new Size(52, 20);
			this.numericUpDown_15.TabIndex = 10;
			this.numericUpDown_15.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown107 = this.numericUpDown_15;
			int[] array107 = new int[4];
			array107[0] = 200;
			numericUpDown107.Value = new decimal(array107);
			this.numericUpDown_15.Leave += this.MainForm_Leave;
			this.label_46.AutoSize = true;
			this.label_46.Location = new Point(6, 199);
			this.label_46.Name = MainForm.getString_0(107408916);
			this.label_46.Size = new Size(56, 14);
			this.label_46.TabIndex = 50;
			this.label_46.Text = MainForm.getString_0(107408867);
			this.label_47.AutoSize = true;
			this.label_47.Location = new Point(6, 95);
			this.label_47.Name = MainForm.getString_0(107408886);
			this.label_47.Size = new Size(139, 14);
			this.label_47.TabIndex = 49;
			this.label_47.Text = MainForm.getString_0(107408849);
			this.label_48.AutoSize = true;
			this.label_48.Location = new Point(6, 43);
			this.label_48.Name = MainForm.getString_0(107408784);
			this.label_48.Size = new Size(155, 14);
			this.label_48.TabIndex = 48;
			this.label_48.Text = MainForm.getString_0(107408747);
			this.label_49.AutoSize = true;
			this.label_49.Location = new Point(6, 69);
			this.label_49.Name = MainForm.getString_0(107408710);
			this.label_49.Size = new Size(154, 14);
			this.label_49.TabIndex = 47;
			this.label_49.Text = MainForm.getString_0(107408673);
			this.label_50.AutoSize = true;
			this.label_50.Location = new Point(6, 17);
			this.label_50.Name = MainForm.getString_0(107408156);
			this.label_50.Size = new Size(149, 14);
			this.label_50.TabIndex = 46;
			this.label_50.Text = MainForm.getString_0(107408119);
			this.label_51.AutoSize = true;
			this.label_51.Location = new Point(220, 173);
			this.label_51.Name = MainForm.getString_0(107408082);
			this.label_51.Size = new Size(86, 14);
			this.label_51.TabIndex = 33;
			this.label_51.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown108 = this.numericUpDown_16;
			int[] array108 = new int[4];
			array108[0] = 5;
			numericUpDown108.Increment = new decimal(array108);
			this.numericUpDown_16.Location = new Point(162, 171);
			NumericUpDown numericUpDown109 = this.numericUpDown_16;
			int[] array109 = new int[4];
			array109[0] = 1000;
			numericUpDown109.Maximum = new decimal(array109);
			NumericUpDown numericUpDown110 = this.numericUpDown_16;
			int[] array110 = new int[4];
			array110[0] = 5;
			numericUpDown110.Minimum = new decimal(array110);
			this.numericUpDown_16.Name = MainForm.getString_0(107408037);
			this.numericUpDown_16.Size = new Size(52, 20);
			this.numericUpDown_16.TabIndex = 9;
			this.numericUpDown_16.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown111 = this.numericUpDown_16;
			int[] array111 = new int[4];
			array111[0] = 200;
			numericUpDown111.Value = new decimal(array111);
			this.numericUpDown_16.Leave += this.MainForm_Leave;
			this.label_52.AutoSize = true;
			this.label_52.Location = new Point(220, 147);
			this.label_52.Name = MainForm.getString_0(107408028);
			this.label_52.Size = new Size(86, 14);
			this.label_52.TabIndex = 27;
			this.label_52.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown112 = this.numericUpDown_17;
			int[] array112 = new int[4];
			array112[0] = 5;
			numericUpDown112.Increment = new decimal(array112);
			this.numericUpDown_17.Location = new Point(162, 145);
			NumericUpDown numericUpDown113 = this.numericUpDown_17;
			int[] array113 = new int[4];
			array113[0] = 1000;
			numericUpDown113.Maximum = new decimal(array113);
			NumericUpDown numericUpDown114 = this.numericUpDown_17;
			int[] array114 = new int[4];
			array114[0] = 5;
			numericUpDown114.Minimum = new decimal(array114);
			this.numericUpDown_17.Name = MainForm.getString_0(107407983);
			this.numericUpDown_17.Size = new Size(52, 20);
			this.numericUpDown_17.TabIndex = 8;
			this.numericUpDown_17.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown115 = this.numericUpDown_17;
			int[] array115 = new int[4];
			array115[0] = 200;
			numericUpDown115.Value = new decimal(array115);
			this.numericUpDown_17.Leave += this.MainForm_Leave;
			this.label_53.AutoSize = true;
			this.label_53.Location = new Point(220, 121);
			this.label_53.Name = MainForm.getString_0(107407946);
			this.label_53.Size = new Size(86, 14);
			this.label_53.TabIndex = 25;
			this.label_53.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown116 = this.numericUpDown_18;
			int[] array116 = new int[4];
			array116[0] = 50;
			numericUpDown116.Increment = new decimal(array116);
			this.numericUpDown_18.Location = new Point(162, 119);
			NumericUpDown numericUpDown117 = this.numericUpDown_18;
			int[] array117 = new int[4];
			array117[0] = 10000;
			numericUpDown117.Maximum = new decimal(array117);
			this.numericUpDown_18.Name = MainForm.getString_0(107407965);
			this.numericUpDown_18.Size = new Size(52, 20);
			this.numericUpDown_18.TabIndex = 7;
			this.numericUpDown_18.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown118 = this.numericUpDown_18;
			int[] array118 = new int[4];
			array118[0] = 200;
			numericUpDown118.Value = new decimal(array118);
			this.numericUpDown_18.Leave += this.MainForm_Leave;
			this.label_54.AutoSize = true;
			this.label_54.Location = new Point(220, 95);
			this.label_54.Name = MainForm.getString_0(107407924);
			this.label_54.Size = new Size(63, 14);
			this.label_54.TabIndex = 19;
			this.label_54.Text = MainForm.getString_0(107409731);
			this.numericUpDown_19.Location = new Point(162, 93);
			NumericUpDown numericUpDown119 = this.numericUpDown_19;
			int[] array119 = new int[4];
			array119[0] = 10;
			numericUpDown119.Maximum = new decimal(array119);
			NumericUpDown numericUpDown120 = this.numericUpDown_19;
			int[] array120 = new int[4];
			array120[0] = 1;
			numericUpDown120.Minimum = new decimal(array120);
			this.numericUpDown_19.Name = MainForm.getString_0(107408391);
			this.numericUpDown_19.Size = new Size(52, 20);
			this.numericUpDown_19.TabIndex = 6;
			this.numericUpDown_19.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown121 = this.numericUpDown_19;
			int[] array121 = new int[4];
			array121[0] = 1;
			numericUpDown121.Value = new decimal(array121);
			this.numericUpDown_19.Leave += this.MainForm_Leave;
			this.label_55.AutoSize = true;
			this.label_55.Location = new Point(220, 43);
			this.label_55.Name = MainForm.getString_0(107408382);
			this.label_55.Size = new Size(63, 14);
			this.label_55.TabIndex = 17;
			this.label_55.Text = MainForm.getString_0(107409731);
			NumericUpDown numericUpDown122 = this.numericUpDown_20;
			int[] array122 = new int[4];
			array122[0] = 5;
			numericUpDown122.Increment = new decimal(array122);
			this.numericUpDown_20.Location = new Point(162, 41);
			NumericUpDown numericUpDown123 = this.numericUpDown_20;
			int[] array123 = new int[4];
			array123[0] = 120;
			numericUpDown123.Maximum = new decimal(array123);
			NumericUpDown numericUpDown124 = this.numericUpDown_20;
			int[] array124 = new int[4];
			array124[0] = 10;
			numericUpDown124.Minimum = new decimal(array124);
			this.numericUpDown_20.Name = MainForm.getString_0(107408369);
			this.numericUpDown_20.Size = new Size(52, 20);
			this.numericUpDown_20.TabIndex = 4;
			this.numericUpDown_20.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown125 = this.numericUpDown_20;
			int[] array125 = new int[4];
			array125[0] = 10;
			numericUpDown125.Value = new decimal(array125);
			this.numericUpDown_20.Leave += this.MainForm_Leave;
			this.label_56.AutoSize = true;
			this.label_56.Location = new Point(220, 69);
			this.label_56.Name = MainForm.getString_0(107408320);
			this.label_56.Size = new Size(63, 14);
			this.label_56.TabIndex = 15;
			this.label_56.Text = MainForm.getString_0(107409731);
			NumericUpDown numericUpDown126 = this.numericUpDown_21;
			int[] array126 = new int[4];
			array126[0] = 5;
			numericUpDown126.Increment = new decimal(array126);
			this.numericUpDown_21.Location = new Point(162, 67);
			NumericUpDown numericUpDown127 = this.numericUpDown_21;
			int[] array127 = new int[4];
			array127[0] = 60;
			numericUpDown127.Maximum = new decimal(array127);
			NumericUpDown numericUpDown128 = this.numericUpDown_21;
			int[] array128 = new int[4];
			array128[0] = 10;
			numericUpDown128.Minimum = new decimal(array128);
			this.numericUpDown_21.Name = MainForm.getString_0(107408307);
			this.numericUpDown_21.Size = new Size(52, 20);
			this.numericUpDown_21.TabIndex = 5;
			this.numericUpDown_21.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown129 = this.numericUpDown_21;
			int[] array129 = new int[4];
			array129[0] = 10;
			numericUpDown129.Value = new decimal(array129);
			this.numericUpDown_21.Leave += this.MainForm_Leave;
			this.label_57.AutoSize = true;
			this.label_57.Location = new Point(6, 173);
			this.label_57.Name = MainForm.getString_0(107408230);
			this.label_57.Size = new Size(138, 14);
			this.label_57.TabIndex = 12;
			this.label_57.Text = MainForm.getString_0(107408193);
			this.label_58.AutoSize = true;
			this.label_58.Location = new Point(6, 147);
			this.label_58.Name = MainForm.getString_0(107408192);
			this.label_58.Size = new Size(64, 14);
			this.label_58.TabIndex = 7;
			this.label_58.Text = MainForm.getString_0(107407627);
			this.label_59.AutoSize = true;
			this.label_59.Location = new Point(6, 121);
			this.label_59.Name = MainForm.getString_0(107407642);
			this.label_59.Size = new Size(142, 14);
			this.label_59.TabIndex = 4;
			this.label_59.Text = MainForm.getString_0(107407585);
			this.label_60.AutoSize = true;
			this.label_60.Location = new Point(220, 17);
			this.label_60.Name = MainForm.getString_0(107407580);
			this.label_60.Size = new Size(63, 14);
			this.label_60.TabIndex = 2;
			this.label_60.Text = MainForm.getString_0(107409731);
			NumericUpDown numericUpDown130 = this.numericUpDown_22;
			int[] array130 = new int[4];
			array130[0] = 5;
			numericUpDown130.Increment = new decimal(array130);
			this.numericUpDown_22.Location = new Point(162, 15);
			NumericUpDown numericUpDown131 = this.numericUpDown_22;
			int[] array131 = new int[4];
			array131[0] = 120;
			numericUpDown131.Maximum = new decimal(array131);
			NumericUpDown numericUpDown132 = this.numericUpDown_22;
			int[] array132 = new int[4];
			array132[0] = 1;
			numericUpDown132.Minimum = new decimal(array132);
			this.numericUpDown_22.Name = MainForm.getString_0(107407535);
			this.numericUpDown_22.Size = new Size(52, 20);
			this.numericUpDown_22.TabIndex = 3;
			this.numericUpDown_22.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown133 = this.numericUpDown_22;
			int[] array133 = new int[4];
			array133[0] = 1;
			numericUpDown133.Value = new decimal(array133);
			this.numericUpDown_22.Leave += this.MainForm_Leave;
			this.tabPage_14.Controls.Add(this.groupBox_12);
			this.tabPage_14.Location = new Point(4, 23);
			this.tabPage_14.Name = MainForm.getString_0(107407502);
			this.tabPage_14.Size = new Size(492, 482);
			this.tabPage_14.TabIndex = 2;
			this.tabPage_14.Text = MainForm.getString_0(107389930);
			this.tabPage_14.UseVisualStyleBackColor = true;
			this.groupBox_12.Controls.Add(this.label_161);
			this.groupBox_12.Controls.Add(this.label_162);
			this.groupBox_12.Controls.Add(this.numericUpDown_58);
			this.groupBox_12.Controls.Add(this.label_79);
			this.groupBox_12.Controls.Add(this.label_80);
			this.groupBox_12.Controls.Add(this.label_81);
			this.groupBox_12.Controls.Add(this.numericUpDown_27);
			this.groupBox_12.Controls.Add(this.label_82);
			this.groupBox_12.Controls.Add(this.numericUpDown_28);
			this.groupBox_12.Controls.Add(this.label_61);
			this.groupBox_12.Controls.Add(this.numericUpDown_23);
			this.groupBox_12.Controls.Add(this.label_62);
			this.groupBox_12.Controls.Add(this.label_63);
			this.groupBox_12.Controls.Add(this.label_64);
			this.groupBox_12.Controls.Add(this.numericUpDown_24);
			this.groupBox_12.Location = new Point(6, 6);
			this.groupBox_12.Name = MainForm.getString_0(107407489);
			this.groupBox_12.Size = new Size(483, 473);
			this.groupBox_12.TabIndex = 60;
			this.groupBox_12.TabStop = false;
			this.groupBox_12.Text = MainForm.getString_0(107389930);
			this.label_161.AutoSize = true;
			this.label_161.Location = new Point(7, 95);
			this.label_161.Name = MainForm.getString_0(107407464);
			this.label_161.Size = new Size(201, 14);
			this.label_161.TabIndex = 61;
			this.label_161.Text = MainForm.getString_0(107407483);
			this.label_162.AutoSize = true;
			this.label_162.Location = new Point(269, 95);
			this.label_162.Name = MainForm.getString_0(107407402);
			this.label_162.Size = new Size(63, 14);
			this.label_162.TabIndex = 60;
			this.label_162.Text = MainForm.getString_0(107409731);
			this.numericUpDown_58.Location = new Point(211, 93);
			NumericUpDown numericUpDown134 = this.numericUpDown_58;
			int[] array134 = new int[4];
			array134[0] = 60;
			numericUpDown134.Maximum = new decimal(array134);
			this.numericUpDown_58.Name = MainForm.getString_0(107407421);
			this.numericUpDown_58.Size = new Size(52, 20);
			this.numericUpDown_58.TabIndex = 59;
			this.numericUpDown_58.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown135 = this.numericUpDown_58;
			int[] array135 = new int[4];
			array135[0] = 10;
			numericUpDown135.Value = new decimal(array135);
			this.numericUpDown_58.Leave += this.MainForm_Leave;
			this.label_79.AutoSize = true;
			this.label_79.Location = new Point(7, 43);
			this.label_79.Name = MainForm.getString_0(107407900);
			this.label_79.Size = new Size(155, 14);
			this.label_79.TabIndex = 58;
			this.label_79.Text = MainForm.getString_0(107408747);
			this.label_80.AutoSize = true;
			this.label_80.Location = new Point(7, 69);
			this.label_80.Name = MainForm.getString_0(107407859);
			this.label_80.Size = new Size(154, 14);
			this.label_80.TabIndex = 57;
			this.label_80.Text = MainForm.getString_0(107408673);
			this.label_81.AutoSize = true;
			this.label_81.Location = new Point(269, 43);
			this.label_81.Name = MainForm.getString_0(107407786);
			this.label_81.Size = new Size(63, 14);
			this.label_81.TabIndex = 56;
			this.label_81.Text = MainForm.getString_0(107409731);
			NumericUpDown numericUpDown136 = this.numericUpDown_27;
			int[] array136 = new int[4];
			array136[0] = 5;
			numericUpDown136.Increment = new decimal(array136);
			this.numericUpDown_27.Location = new Point(211, 41);
			NumericUpDown numericUpDown137 = this.numericUpDown_27;
			int[] array137 = new int[4];
			array137[0] = 120;
			numericUpDown137.Maximum = new decimal(array137);
			NumericUpDown numericUpDown138 = this.numericUpDown_27;
			int[] array138 = new int[4];
			array138[0] = 10;
			numericUpDown138.Minimum = new decimal(array138);
			this.numericUpDown_27.Name = MainForm.getString_0(107407805);
			this.numericUpDown_27.Size = new Size(52, 20);
			this.numericUpDown_27.TabIndex = 53;
			this.numericUpDown_27.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown139 = this.numericUpDown_27;
			int[] array139 = new int[4];
			array139[0] = 10;
			numericUpDown139.Value = new decimal(array139);
			this.label_82.AutoSize = true;
			this.label_82.Location = new Point(269, 69);
			this.label_82.Name = MainForm.getString_0(107407720);
			this.label_82.Size = new Size(63, 14);
			this.label_82.TabIndex = 55;
			this.label_82.Text = MainForm.getString_0(107409731);
			NumericUpDown numericUpDown140 = this.numericUpDown_28;
			int[] array140 = new int[4];
			array140[0] = 5;
			numericUpDown140.Increment = new decimal(array140);
			this.numericUpDown_28.Location = new Point(211, 67);
			NumericUpDown numericUpDown141 = this.numericUpDown_28;
			int[] array141 = new int[4];
			array141[0] = 60;
			numericUpDown141.Maximum = new decimal(array141);
			NumericUpDown numericUpDown142 = this.numericUpDown_28;
			int[] array142 = new int[4];
			array142[0] = 10;
			numericUpDown142.Minimum = new decimal(array142);
			this.numericUpDown_28.Name = MainForm.getString_0(107407739);
			this.numericUpDown_28.Size = new Size(52, 20);
			this.numericUpDown_28.TabIndex = 54;
			this.numericUpDown_28.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown143 = this.numericUpDown_28;
			int[] array143 = new int[4];
			array143[0] = 10;
			numericUpDown143.Value = new decimal(array143);
			this.label_61.AutoSize = true;
			this.label_61.Location = new Point(269, 121);
			this.label_61.Name = MainForm.getString_0(107407658);
			this.label_61.Size = new Size(86, 14);
			this.label_61.TabIndex = 52;
			this.label_61.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown144 = this.numericUpDown_23;
			int[] array144 = new int[4];
			array144[0] = 5;
			numericUpDown144.Increment = new decimal(array144);
			this.numericUpDown_23.Location = new Point(211, 119);
			NumericUpDown numericUpDown145 = this.numericUpDown_23;
			int[] array145 = new int[4];
			array145[0] = 1000;
			numericUpDown145.Maximum = new decimal(array145);
			NumericUpDown numericUpDown146 = this.numericUpDown_23;
			int[] array146 = new int[4];
			array146[0] = 5;
			numericUpDown146.Minimum = new decimal(array146);
			this.numericUpDown_23.Name = MainForm.getString_0(107407677);
			this.numericUpDown_23.Size = new Size(52, 20);
			this.numericUpDown_23.TabIndex = 4;
			this.numericUpDown_23.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown147 = this.numericUpDown_23;
			int[] array147 = new int[4];
			array147[0] = 200;
			numericUpDown147.Value = new decimal(array147);
			this.numericUpDown_23.Leave += this.MainForm_Leave;
			this.label_62.AutoSize = true;
			this.label_62.Location = new Point(6, 121);
			this.label_62.Name = MainForm.getString_0(107407124);
			this.label_62.Size = new Size(138, 14);
			this.label_62.TabIndex = 50;
			this.label_62.Text = MainForm.getString_0(107408193);
			this.label_63.AutoSize = true;
			this.label_63.Location = new Point(6, 16);
			this.label_63.Name = MainForm.getString_0(107407051);
			this.label_63.Size = new Size(156, 14);
			this.label_63.TabIndex = 49;
			this.label_63.Text = MainForm.getString_0(107407022);
			this.label_64.AutoSize = true;
			this.label_64.Location = new Point(269, 16);
			this.label_64.Name = MainForm.getString_0(107406985);
			this.label_64.Size = new Size(63, 14);
			this.label_64.TabIndex = 48;
			this.label_64.Text = MainForm.getString_0(107409731);
			NumericUpDown numericUpDown148 = this.numericUpDown_24;
			int[] array148 = new int[4];
			array148[0] = 5;
			numericUpDown148.Increment = new decimal(array148);
			this.numericUpDown_24.Location = new Point(211, 14);
			NumericUpDown numericUpDown149 = this.numericUpDown_24;
			int[] array149 = new int[4];
			array149[0] = 120;
			numericUpDown149.Maximum = new decimal(array149);
			NumericUpDown numericUpDown150 = this.numericUpDown_24;
			int[] array150 = new int[4];
			array150[0] = 1;
			numericUpDown150.Minimum = new decimal(array150);
			this.numericUpDown_24.Name = MainForm.getString_0(107407004);
			this.numericUpDown_24.Size = new Size(52, 20);
			this.numericUpDown_24.TabIndex = 3;
			this.numericUpDown_24.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown151 = this.numericUpDown_24;
			int[] array151 = new int[4];
			array151[0] = 1;
			numericUpDown151.Value = new decimal(array151);
			this.numericUpDown_24.Leave += this.MainForm_Leave;
			this.tabPage_15.Controls.Add(this.groupBox_13);
			this.tabPage_15.Location = new Point(4, 23);
			this.tabPage_15.Name = MainForm.getString_0(107406971);
			this.tabPage_15.Size = new Size(492, 482);
			this.tabPage_15.TabIndex = 3;
			this.tabPage_15.Text = MainForm.getString_0(107406926);
			this.tabPage_15.UseVisualStyleBackColor = true;
			this.groupBox_13.Controls.Add(this.label_65);
			this.groupBox_13.Controls.Add(this.label_66);
			this.groupBox_13.Controls.Add(this.numericUpDown_25);
			this.groupBox_13.Location = new Point(6, 6);
			this.groupBox_13.Name = MainForm.getString_0(107406913);
			this.groupBox_13.Size = new Size(483, 473);
			this.groupBox_13.TabIndex = 51;
			this.groupBox_13.TabStop = false;
			this.groupBox_13.Text = MainForm.getString_0(107406926);
			this.label_65.AutoSize = true;
			this.label_65.Location = new Point(6, 16);
			this.label_65.Name = MainForm.getString_0(107406884);
			this.label_65.Size = new Size(101, 14);
			this.label_65.TabIndex = 47;
			this.label_65.Text = MainForm.getString_0(107406899);
			this.label_66.AutoSize = true;
			this.label_66.Location = new Point(183, 16);
			this.label_66.Name = MainForm.getString_0(107407386);
			this.label_66.Size = new Size(86, 14);
			this.label_66.TabIndex = 50;
			this.label_66.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown152 = this.numericUpDown_25;
			int[] array152 = new int[4];
			array152[0] = 5;
			numericUpDown152.Increment = new decimal(array152);
			this.numericUpDown_25.Location = new Point(125, 14);
			NumericUpDown numericUpDown153 = this.numericUpDown_25;
			int[] array153 = new int[4];
			array153[0] = 1000;
			numericUpDown153.Maximum = new decimal(array153);
			NumericUpDown numericUpDown154 = this.numericUpDown_25;
			int[] array154 = new int[4];
			array154[0] = 5;
			numericUpDown154.Minimum = new decimal(array154);
			this.numericUpDown_25.Name = MainForm.getString_0(107407341);
			this.numericUpDown_25.Size = new Size(52, 20);
			this.numericUpDown_25.TabIndex = 3;
			this.numericUpDown_25.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown155 = this.numericUpDown_25;
			int[] array155 = new int[4];
			array155[0] = 200;
			numericUpDown155.Value = new decimal(array155);
			this.numericUpDown_25.Leave += this.MainForm_Leave;
			this.tabPage_6.Controls.Add(this.groupBox_24);
			this.tabPage_6.Controls.Add(this.groupBox_25);
			this.tabPage_6.Location = new Point(4, 42);
			this.tabPage_6.Name = MainForm.getString_0(107407312);
			this.tabPage_6.Padding = new Padding(3);
			this.tabPage_6.Size = new Size(520, 543);
			this.tabPage_6.TabIndex = 1;
			this.tabPage_6.Text = MainForm.getString_0(107396102);
			this.tabPage_6.UseVisualStyleBackColor = true;
			this.groupBox_24.Controls.Add(this.dataGridView_0);
			this.groupBox_24.Controls.Add(this.button_22);
			this.groupBox_24.Controls.Add(this.linkLabel_0);
			this.groupBox_24.Controls.Add(this.pictureBox_0);
			this.groupBox_24.Controls.Add(this.button_23);
			this.groupBox_24.Location = new Point(6, 191);
			this.groupBox_24.Name = MainForm.getString_0(107407327);
			this.groupBox_24.Size = new Size(508, 329);
			this.groupBox_24.TabIndex = 3;
			this.groupBox_24.TabStop = false;
			this.groupBox_24.Text = MainForm.getString_0(107407278);
			this.dataGridView_0.AllowUserToAddRows = false;
			this.dataGridView_0.AllowUserToDeleteRows = false;
			this.dataGridView_0.AllowUserToResizeRows = false;
			this.dataGridView_0.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_0.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewCheckBoxColumn_0,
				this.dataGridViewTextBoxColumn_0,
				this.dataGridViewTextBoxColumn_1
			});
			this.dataGridView_0.Location = new Point(6, 50);
			this.dataGridView_0.MultiSelect = false;
			this.dataGridView_0.Name = MainForm.getString_0(107407289);
			this.dataGridView_0.RowHeadersVisible = false;
			this.dataGridView_0.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_0.ShowEditingIcon = false;
			this.dataGridView_0.Size = new Size(498, 273);
			this.dataGridView_0.TabIndex = 18;
			this.dataGridView_0.CellMouseDown += this.dataGridView_2_CellMouseDown;
			this.dataGridView_0.CellValueChanged += this.dataGridView_0_CellValueChanged;
			this.dataGridView_0.Leave += this.MainForm_Leave;
			this.dataGridView_0.MouseClick += this.dataGridView_0_MouseClick;
			this.dataGridView_0.MouseDoubleClick += this.dataGridView_0_MouseDoubleClick;
			this.dataGridView_0.MouseUp += this.dataGridView_2_MouseUp;
			this.dataGridViewCheckBoxColumn_0.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewCheckBoxColumn_0.HeaderText = MainForm.getString_0(107396269);
			this.dataGridViewCheckBoxColumn_0.Name = MainForm.getString_0(107407240);
			this.dataGridViewCheckBoxColumn_0.Width = 20;
			this.dataGridViewTextBoxColumn_0.HeaderText = MainForm.getString_0(107407251);
			this.dataGridViewTextBoxColumn_0.Name = MainForm.getString_0(107395697);
			this.dataGridViewTextBoxColumn_0.ReadOnly = true;
			this.dataGridViewTextBoxColumn_0.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_0.Width = 150;
			this.dataGridViewTextBoxColumn_1.HeaderText = MainForm.getString_0(107407210);
			this.dataGridViewTextBoxColumn_1.Name = MainForm.getString_0(107395680);
			this.dataGridViewTextBoxColumn_1.ReadOnly = true;
			this.dataGridViewTextBoxColumn_1.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_1.Width = 150;
			this.button_22.Location = new Point(129, 19);
			this.button_22.Name = MainForm.getString_0(107407201);
			this.button_22.Size = new Size(129, 22);
			this.button_22.TabIndex = 17;
			this.button_22.Text = MainForm.getString_0(107407220);
			this.button_22.UseVisualStyleBackColor = true;
			this.button_22.Click += this.button_22_Click;
			this.linkLabel_0.AutoSize = true;
			this.linkLabel_0.Location = new Point(261, 22);
			this.linkLabel_0.Name = MainForm.getString_0(107407195);
			this.linkLabel_0.Size = new Size(103, 14);
			this.linkLabel_0.TabIndex = 16;
			this.linkLabel_0.TabStop = true;
			this.linkLabel_0.Text = MainForm.getString_0(107407146);
			this.linkLabel_0.LinkClicked += this.linkLabel_0_LinkClicked;
			this.pictureBox_0.Image = (Image)componentResourceManager.GetObject(MainForm.getString_0(107407153));
			this.pictureBox_0.InitialImage = null;
			this.pictureBox_0.Location = new Point(366, 17);
			this.pictureBox_0.Name = MainForm.getString_0(107406616);
			this.pictureBox_0.Size = new Size(25, 25);
			this.pictureBox_0.TabIndex = 15;
			this.pictureBox_0.TabStop = false;
			this.toolTip_0.SetToolTip(this.pictureBox_0, MainForm.getString_0(107406567));
			this.button_23.Location = new Point(6, 19);
			this.button_23.Name = MainForm.getString_0(107406470);
			this.button_23.Size = new Size(117, 22);
			this.button_23.TabIndex = 13;
			this.button_23.Text = MainForm.getString_0(107406485);
			this.button_23.UseVisualStyleBackColor = true;
			this.button_23.Click += this.button_23_Click;
			this.groupBox_25.Controls.Add(this.button_91);
			this.groupBox_25.Controls.Add(this.button_79);
			this.groupBox_25.Controls.Add(this.numericUpDown_35);
			this.groupBox_25.Controls.Add(this.label_113);
			this.groupBox_25.Controls.Add(this.checkBox_28);
			this.groupBox_25.Controls.Add(this.checkBox_29);
			this.groupBox_25.Controls.Add(this.checkBox_27);
			this.groupBox_25.Controls.Add(this.label_104);
			this.groupBox_25.Controls.Add(this.numericUpDown_30);
			this.groupBox_25.Controls.Add(this.checkBox_21);
			this.groupBox_25.Controls.Add(this.button_24);
			this.groupBox_25.Controls.Add(this.numericUpDown_29);
			this.groupBox_25.Controls.Add(this.label_93);
			this.groupBox_25.Controls.Add(this.label_94);
			this.groupBox_25.Controls.Add(this.checkBox_19);
			this.groupBox_25.Controls.Add(this.label_95);
			this.groupBox_25.Controls.Add(this.comboBox_27);
			this.groupBox_25.Location = new Point(6, 6);
			this.groupBox_25.Name = MainForm.getString_0(107406440);
			this.groupBox_25.Size = new Size(508, 179);
			this.groupBox_25.TabIndex = 2;
			this.groupBox_25.TabStop = false;
			this.groupBox_25.Text = MainForm.getString_0(107406455);
			this.button_91.Location = new Point(478, 14);
			this.button_91.Name = MainForm.getString_0(107406430);
			this.button_91.Size = new Size(24, 23);
			this.button_91.TabIndex = 45;
			this.button_91.Text = MainForm.getString_0(107406369);
			this.button_91.UseVisualStyleBackColor = true;
			this.button_91.Click += this.button_91_Click;
			this.button_79.Location = new Point(415, 129);
			this.button_79.Name = MainForm.getString_0(107406396);
			this.button_79.Size = new Size(77, 45);
			this.button_79.TabIndex = 44;
			this.button_79.Text = MainForm.getString_0(107406863);
			this.button_79.UseVisualStyleBackColor = true;
			this.button_79.Click += this.button_79_Click;
			this.numericUpDown_35.Location = new Point(296, 100);
			NumericUpDown numericUpDown156 = this.numericUpDown_35;
			int[] array156 = new int[4];
			array156[0] = 9999;
			numericUpDown156.Maximum = new decimal(array156);
			NumericUpDown numericUpDown157 = this.numericUpDown_35;
			int[] array157 = new int[4];
			array157[0] = 1;
			numericUpDown157.Minimum = new decimal(array157);
			this.numericUpDown_35.Name = MainForm.getString_0(107406870);
			this.numericUpDown_35.Size = new Size(47, 20);
			this.numericUpDown_35.TabIndex = 43;
			this.numericUpDown_35.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown158 = this.numericUpDown_35;
			int[] array158 = new int[4];
			array158[0] = 15;
			numericUpDown158.Value = new decimal(array158);
			this.numericUpDown_35.Leave += this.MainForm_Leave;
			this.label_113.AutoSize = true;
			this.label_113.Location = new Point(346, 102);
			this.label_113.Name = MainForm.getString_0(107406841);
			this.label_113.Size = new Size(53, 14);
			this.label_113.TabIndex = 42;
			this.label_113.Text = MainForm.getString_0(107406796);
			this.checkBox_28.AutoSize = true;
			this.checkBox_28.Location = new Point(6, 101);
			this.checkBox_28.Name = MainForm.getString_0(107406815);
			this.checkBox_28.Size = new Size(292, 18);
			this.checkBox_28.TabIndex = 41;
			this.checkBox_28.Text = MainForm.getString_0(107406754);
			this.checkBox_28.UseVisualStyleBackColor = true;
			this.checkBox_28.Leave += this.MainForm_Leave;
			this.checkBox_29.AutoSize = true;
			this.checkBox_29.Location = new Point(6, 81);
			this.checkBox_29.Name = MainForm.getString_0(107406689);
			this.checkBox_29.Size = new Size(337, 18);
			this.checkBox_29.TabIndex = 40;
			this.checkBox_29.Text = MainForm.getString_0(107406688);
			this.checkBox_29.UseVisualStyleBackColor = true;
			this.checkBox_29.CheckedChanged += this.checkBox_29_CheckedChanged;
			this.checkBox_29.Leave += this.MainForm_Leave;
			this.checkBox_27.AutoSize = true;
			this.checkBox_27.Location = new Point(6, 61);
			this.checkBox_27.Name = MainForm.getString_0(107406099);
			this.checkBox_27.Size = new Size(257, 18);
			this.checkBox_27.TabIndex = 39;
			this.checkBox_27.Text = MainForm.getString_0(107406050);
			this.checkBox_27.UseVisualStyleBackColor = true;
			this.checkBox_27.Leave += this.MainForm_Leave;
			this.label_104.AutoSize = true;
			this.label_104.Location = new Point(179, 129);
			this.label_104.Name = MainForm.getString_0(107405993);
			this.label_104.Size = new Size(18, 14);
			this.label_104.TabIndex = 38;
			this.label_104.Text = MainForm.getString_0(107411555);
			this.numericUpDown_30.Location = new Point(199, 127);
			NumericUpDown numericUpDown159 = this.numericUpDown_30;
			int[] array159 = new int[4];
			array159[0] = 10000;
			numericUpDown159.Maximum = new decimal(array159);
			NumericUpDown numericUpDown160 = this.numericUpDown_30;
			int[] array160 = new int[4];
			array160[0] = 15;
			numericUpDown160.Minimum = new decimal(array160);
			this.numericUpDown_30.Name = MainForm.getString_0(107406012);
			this.numericUpDown_30.Size = new Size(47, 20);
			this.numericUpDown_30.TabIndex = 37;
			this.numericUpDown_30.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown161 = this.numericUpDown_30;
			int[] array161 = new int[4];
			array161[0] = 15;
			numericUpDown161.Value = new decimal(array161);
			this.numericUpDown_30.Leave += this.MainForm_Leave;
			this.checkBox_21.AutoSize = true;
			this.checkBox_21.Location = new Point(6, 41);
			this.checkBox_21.Name = MainForm.getString_0(107405971);
			this.checkBox_21.Size = new Size(233, 18);
			this.checkBox_21.TabIndex = 36;
			this.checkBox_21.Text = MainForm.getString_0(107405898);
			this.checkBox_21.UseVisualStyleBackColor = true;
			this.checkBox_21.Leave += this.MainForm_Leave;
			this.button_24.Location = new Point(332, 129);
			this.button_24.Name = MainForm.getString_0(107405877);
			this.button_24.Size = new Size(77, 45);
			this.button_24.TabIndex = 35;
			this.button_24.Text = MainForm.getString_0(107385211);
			this.button_24.UseVisualStyleBackColor = true;
			this.button_24.Click += this.button_75_Click;
			this.numericUpDown_29.Location = new Point(129, 127);
			NumericUpDown numericUpDown162 = this.numericUpDown_29;
			int[] array162 = new int[4];
			array162[0] = 10000;
			numericUpDown162.Maximum = new decimal(array162);
			NumericUpDown numericUpDown163 = this.numericUpDown_29;
			int[] array163 = new int[4];
			array163[0] = 10;
			numericUpDown163.Minimum = new decimal(array163);
			this.numericUpDown_29.Name = MainForm.getString_0(107406344);
			this.numericUpDown_29.Size = new Size(47, 20);
			this.numericUpDown_29.TabIndex = 4;
			this.numericUpDown_29.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown164 = this.numericUpDown_29;
			int[] array164 = new int[4];
			array164[0] = 10;
			numericUpDown164.Value = new decimal(array164);
			this.numericUpDown_29.Leave += this.MainForm_Leave;
			this.label_93.AutoSize = true;
			this.label_93.Location = new Point(249, 129);
			this.label_93.Name = MainForm.getString_0(107406335);
			this.label_93.Size = new Size(53, 14);
			this.label_93.TabIndex = 34;
			this.label_93.Text = MainForm.getString_0(107384920);
			this.label_94.AutoSize = true;
			this.label_94.Location = new Point(3, 129);
			this.label_94.Name = MainForm.getString_0(107406322);
			this.label_94.Size = new Size(120, 14);
			this.label_94.TabIndex = 3;
			this.label_94.Text = MainForm.getString_0(107406277);
			this.checkBox_19.AutoSize = true;
			this.checkBox_19.Location = new Point(6, 19);
			this.checkBox_19.Name = MainForm.getString_0(107406248);
			this.checkBox_19.Size = new Size(162, 18);
			this.checkBox_19.TabIndex = 2;
			this.checkBox_19.Text = MainForm.getString_0(107406223);
			this.checkBox_19.UseVisualStyleBackColor = true;
			this.checkBox_19.Leave += this.MainForm_Leave;
			this.label_95.AutoSize = true;
			this.label_95.Location = new Point(2, 154);
			this.label_95.Name = MainForm.getString_0(107406190);
			this.label_95.Size = new Size(118, 14);
			this.label_95.TabIndex = 1;
			this.label_95.Text = MainForm.getString_0(107406177);
			this.comboBox_27.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_27.FormattingEnabled = true;
			this.comboBox_27.Location = new Point(128, 151);
			this.comboBox_27.Name = MainForm.getString_0(107406148);
			this.comboBox_27.Size = new Size(198, 22);
			this.comboBox_27.TabIndex = 3;
			this.comboBox_27.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_27.Leave += this.MainForm_Leave;
			this.tabPage_21.Controls.Add(this.tabControl_8);
			this.tabPage_21.Location = new Point(4, 42);
			this.tabPage_21.Name = MainForm.getString_0(107406127);
			this.tabPage_21.Padding = new Padding(3);
			this.tabPage_21.Size = new Size(520, 543);
			this.tabPage_21.TabIndex = 7;
			this.tabPage_21.Text = MainForm.getString_0(107389930);
			this.tabPage_21.UseVisualStyleBackColor = true;
			this.tabControl_8.Controls.Add(this.tabPage_25);
			this.tabControl_8.Controls.Add(this.tabPage_34);
			this.tabControl_8.Controls.Add(this.tabPage_33);
			this.tabControl_8.Location = new Point(0, 0);
			this.tabControl_8.Name = MainForm.getString_0(107406114);
			this.tabControl_8.SelectedIndex = 0;
			this.tabControl_8.Size = new Size(517, 539);
			this.tabControl_8.TabIndex = 3;
			this.tabPage_25.Controls.Add(this.button_92);
			this.tabPage_25.Controls.Add(this.checkBox_46);
			this.tabPage_25.Controls.Add(this.groupBox_21);
			this.tabPage_25.Controls.Add(this.groupBox_22);
			this.tabPage_25.Location = new Point(4, 23);
			this.tabPage_25.Name = MainForm.getString_0(107405581);
			this.tabPage_25.Padding = new Padding(3);
			this.tabPage_25.Size = new Size(509, 512);
			this.tabPage_25.TabIndex = 3;
			this.tabPage_25.Text = MainForm.getString_0(107405592);
			this.tabPage_25.UseVisualStyleBackColor = true;
			this.tabPage_25.Leave += this.MainForm_Leave;
			this.button_92.Location = new Point(479, 6);
			this.button_92.Name = MainForm.getString_0(107405543);
			this.button_92.Size = new Size(24, 23);
			this.button_92.TabIndex = 46;
			this.button_92.Text = MainForm.getString_0(107406369);
			this.button_92.UseVisualStyleBackColor = true;
			this.button_92.Click += this.button_92_Click;
			this.checkBox_46.AutoSize = true;
			this.checkBox_46.Location = new Point(6, 6);
			this.checkBox_46.Name = MainForm.getString_0(107405514);
			this.checkBox_46.Size = new Size(129, 18);
			this.checkBox_46.TabIndex = 28;
			this.checkBox_46.Text = MainForm.getString_0(107405521);
			this.checkBox_46.UseVisualStyleBackColor = true;
			this.checkBox_46.CheckedChanged += this.checkBox_46_CheckedChanged;
			this.groupBox_21.Controls.Add(this.dataGridView_1);
			this.groupBox_21.Location = new Point(6, 98);
			this.groupBox_21.Name = MainForm.getString_0(107405496);
			this.groupBox_21.Size = new Size(497, 413);
			this.groupBox_21.TabIndex = 27;
			this.groupBox_21.TabStop = false;
			this.groupBox_21.Text = MainForm.getString_0(107405447);
			this.dataGridView_1.AllowUserToAddRows = false;
			this.dataGridView_1.AllowUserToDeleteRows = false;
			this.dataGridView_1.AllowUserToResizeRows = false;
			this.dataGridView_1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewCheckBoxColumn_1,
				this.dataGridViewComboBoxColumn_0,
				this.dataGridViewTextBoxColumn_2,
				this.dataGridViewTextBoxColumn_3,
				this.dataGridViewTextBoxColumn_4,
				this.dataGridViewComboBoxColumn_1,
				this.dataGridViewTextBoxColumn_5,
				this.dataGridViewComboBoxColumn_2
			});
			this.dataGridView_1.Location = new Point(9, 19);
			this.dataGridView_1.MultiSelect = false;
			this.dataGridView_1.Name = MainForm.getString_0(107405418);
			this.dataGridView_1.RowHeadersVisible = false;
			this.dataGridView_1.ShowEditingIcon = false;
			this.dataGridView_1.Size = new Size(485, 388);
			this.dataGridView_1.TabIndex = 0;
			this.dataGridView_1.CellContentClick += this.dataGridView_1_CellContentClick;
			this.dataGridView_1.CellMouseDown += this.dataGridView_2_CellMouseDown;
			this.dataGridView_1.CellValueChanged += this.dataGridView_1_CellValueChanged;
			this.dataGridView_1.DataError += this.dataGridView_2_DataError;
			this.dataGridView_1.EditingControlShowing += this.dataGridView_1_EditingControlShowing;
			this.dataGridView_1.Leave += this.MainForm_Leave;
			this.dataGridView_1.MouseClick += this.dataGridView_1_MouseClick;
			this.dataGridView_1.MouseDoubleClick += this.dataGridView_1_MouseDoubleClick;
			this.dataGridView_1.MouseUp += this.dataGridView_2_MouseUp;
			this.dataGridViewCheckBoxColumn_1.HeaderText = MainForm.getString_0(107396269);
			this.dataGridViewCheckBoxColumn_1.Name = MainForm.getString_0(107405429);
			this.dataGridViewCheckBoxColumn_1.Width = 20;
			this.dataGridViewComboBoxColumn_0.HeaderText = MainForm.getString_0(107405404);
			this.dataGridViewComboBoxColumn_0.Name = MainForm.getString_0(107390551);
			this.dataGridViewComboBoxColumn_0.Width = 55;
			this.dataGridViewTextBoxColumn_2.HeaderText = MainForm.getString_0(107405359);
			this.dataGridViewTextBoxColumn_2.Name = MainForm.getString_0(107405374);
			this.dataGridViewTextBoxColumn_2.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_3.HeaderText = MainForm.getString_0(107405825);
			this.dataGridViewTextBoxColumn_3.Name = MainForm.getString_0(107395906);
			this.dataGridViewTextBoxColumn_3.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_3.Width = 50;
			this.dataGridViewTextBoxColumn_4.HeaderText = MainForm.getString_0(107405808);
			this.dataGridViewTextBoxColumn_4.Name = MainForm.getString_0(107396000);
			this.dataGridViewTextBoxColumn_4.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_4.Width = 50;
			this.dataGridViewComboBoxColumn_1.HeaderText = MainForm.getString_0(107393328);
			this.dataGridViewComboBoxColumn_1.Items.AddRange(new object[]
			{
				MainForm.getString_0(107405795),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107390916),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107381835)
			});
			this.dataGridViewComboBoxColumn_1.Name = MainForm.getString_0(107395943);
			this.dataGridViewComboBoxColumn_1.Width = 145;
			this.dataGridViewTextBoxColumn_5.HeaderText = MainForm.getString_0(107405766);
			this.dataGridViewTextBoxColumn_5.Name = MainForm.getString_0(107395877);
			this.dataGridViewTextBoxColumn_5.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_5.Width = 50;
			this.dataGridViewComboBoxColumn_2.HeaderText = MainForm.getString_0(107393328);
			this.dataGridViewComboBoxColumn_2.Items.AddRange(new object[]
			{
				MainForm.getString_0(107390916),
				MainForm.getString_0(107405781),
				MainForm.getString_0(107382788),
				MainForm.getString_0(107405756),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107405723),
				MainForm.getString_0(107382662),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107405670),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107390973),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107405641),
				MainForm.getString_0(107382581),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107405612),
				MainForm.getString_0(107405071),
				MainForm.getString_0(107381928),
				MainForm.getString_0(107381939),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107405082),
				MainForm.getString_0(107381905),
				MainForm.getString_0(107381888),
				MainForm.getString_0(107381835)
			});
			this.dataGridViewComboBoxColumn_2.Name = MainForm.getString_0(107405025);
			this.dataGridViewComboBoxColumn_2.Width = 145;
			this.groupBox_22.Controls.Add(this.textBox_9);
			this.groupBox_22.Controls.Add(this.textBox_10);
			this.groupBox_22.Controls.Add(this.label_84);
			this.groupBox_22.Controls.Add(this.label_85);
			this.groupBox_22.Controls.Add(this.button_19);
			this.groupBox_22.Location = new Point(6, 30);
			this.groupBox_22.Name = MainForm.getString_0(107405024);
			this.groupBox_22.Size = new Size(497, 66);
			this.groupBox_22.TabIndex = 26;
			this.groupBox_22.TabStop = false;
			this.groupBox_22.Text = MainForm.getString_0(107404975);
			this.textBox_9.Location = new Point(97, 42);
			this.textBox_9.Name = MainForm.getString_0(107404962);
			this.textBox_9.Size = new Size(309, 20);
			this.textBox_9.TabIndex = 4;
			this.textBox_9.KeyPress += this.textBox_9_KeyPress;
			this.textBox_10.Location = new Point(97, 14);
			this.textBox_10.Name = MainForm.getString_0(107404977);
			this.textBox_10.Size = new Size(309, 20);
			this.textBox_10.TabIndex = 3;
			this.label_84.AutoSize = true;
			this.label_84.Font = new Font(MainForm.getString_0(107397254), 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_84.Location = new Point(6, 42);
			this.label_84.Name = MainForm.getString_0(107404960);
			this.label_84.Size = new Size(83, 16);
			this.label_84.TabIndex = 11;
			this.label_84.Text = MainForm.getString_0(107404947);
			this.label_85.AutoSize = true;
			this.label_85.Font = new Font(MainForm.getString_0(107397254), 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_85.Location = new Point(6, 18);
			this.label_85.Name = MainForm.getString_0(107404898);
			this.label_85.Size = new Size(79, 16);
			this.label_85.TabIndex = 9;
			this.label_85.Text = MainForm.getString_0(107404917);
			this.button_19.Location = new Point(422, 17);
			this.button_19.Name = MainForm.getString_0(107404868);
			this.button_19.Size = new Size(69, 41);
			this.button_19.TabIndex = 5;
			this.button_19.Text = MainForm.getString_0(107385117);
			this.button_19.UseVisualStyleBackColor = true;
			this.button_19.Click += this.button_19_Click;
			this.tabPage_34.Controls.Add(this.button_93);
			this.tabPage_34.Controls.Add(this.checkBox_45);
			this.tabPage_34.Controls.Add(this.label_110);
			this.tabPage_34.Controls.Add(this.numericUpDown_33);
			this.tabPage_34.Controls.Add(this.numericUpDown_34);
			this.tabPage_34.Controls.Add(this.label_111);
			this.tabPage_34.Controls.Add(this.label_112);
			this.tabPage_34.Controls.Add(this.groupBox_30);
			this.tabPage_34.Controls.Add(this.groupBox_31);
			this.tabPage_34.Location = new Point(4, 23);
			this.tabPage_34.Name = MainForm.getString_0(107404883);
			this.tabPage_34.Size = new Size(509, 512);
			this.tabPage_34.TabIndex = 5;
			this.tabPage_34.Text = MainForm.getString_0(107404862);
			this.tabPage_34.UseVisualStyleBackColor = true;
			this.button_93.Location = new Point(482, 6);
			this.button_93.Name = MainForm.getString_0(107405325);
			this.button_93.Size = new Size(24, 23);
			this.button_93.TabIndex = 50;
			this.button_93.Text = MainForm.getString_0(107406369);
			this.button_93.UseVisualStyleBackColor = true;
			this.button_93.Click += this.button_93_Click;
			this.checkBox_45.AutoSize = true;
			this.checkBox_45.Location = new Point(6, 6);
			this.checkBox_45.Name = MainForm.getString_0(107405296);
			this.checkBox_45.Size = new Size(130, 18);
			this.checkBox_45.TabIndex = 49;
			this.checkBox_45.Text = MainForm.getString_0(107405303);
			this.checkBox_45.UseVisualStyleBackColor = true;
			this.checkBox_45.CheckedChanged += this.checkBox_45_CheckedChanged;
			this.checkBox_45.Leave += this.MainForm_Leave;
			this.label_110.AutoSize = true;
			this.label_110.Location = new Point(180, 27);
			this.label_110.Name = MainForm.getString_0(107405278);
			this.label_110.Size = new Size(18, 14);
			this.label_110.TabIndex = 48;
			this.label_110.Text = MainForm.getString_0(107411555);
			this.numericUpDown_33.Location = new Point(200, 25);
			NumericUpDown numericUpDown165 = this.numericUpDown_33;
			int[] array165 = new int[4];
			array165[0] = 600;
			numericUpDown165.Maximum = new decimal(array165);
			NumericUpDown numericUpDown166 = this.numericUpDown_33;
			int[] array166 = new int[4];
			array166[0] = 5;
			numericUpDown166.Minimum = new decimal(array166);
			this.numericUpDown_33.Name = MainForm.getString_0(107405265);
			this.numericUpDown_33.Size = new Size(47, 20);
			this.numericUpDown_33.TabIndex = 47;
			this.numericUpDown_33.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown167 = this.numericUpDown_33;
			int[] array167 = new int[4];
			array167[0] = 20;
			numericUpDown167.Value = new decimal(array167);
			this.numericUpDown_33.Leave += this.MainForm_Leave;
			this.numericUpDown_34.Location = new Point(130, 25);
			NumericUpDown numericUpDown168 = this.numericUpDown_34;
			int[] array168 = new int[4];
			array168[0] = 300;
			numericUpDown168.Maximum = new decimal(array168);
			NumericUpDown numericUpDown169 = this.numericUpDown_34;
			int[] array169 = new int[4];
			array169[0] = 1;
			numericUpDown169.Minimum = new decimal(array169);
			this.numericUpDown_34.Name = MainForm.getString_0(107405200);
			this.numericUpDown_34.Size = new Size(47, 20);
			this.numericUpDown_34.TabIndex = 45;
			this.numericUpDown_34.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown170 = this.numericUpDown_34;
			int[] array170 = new int[4];
			array170[0] = 15;
			numericUpDown170.Value = new decimal(array170);
			this.numericUpDown_34.Leave += this.MainForm_Leave;
			this.label_111.AutoSize = true;
			this.label_111.Location = new Point(250, 27);
			this.label_111.Name = MainForm.getString_0(107405167);
			this.label_111.Size = new Size(53, 14);
			this.label_111.TabIndex = 46;
			this.label_111.Text = MainForm.getString_0(107384920);
			this.label_112.AutoSize = true;
			this.label_112.Location = new Point(4, 27);
			this.label_112.Name = MainForm.getString_0(107405154);
			this.label_112.Size = new Size(123, 14);
			this.label_112.TabIndex = 44;
			this.label_112.Text = MainForm.getString_0(107405173);
			this.groupBox_30.Controls.Add(this.dataGridView_3);
			this.groupBox_30.Location = new Point(6, 123);
			this.groupBox_30.Name = MainForm.getString_0(107405144);
			this.groupBox_30.Size = new Size(500, 384);
			this.groupBox_30.TabIndex = 29;
			this.groupBox_30.TabStop = false;
			this.groupBox_30.Text = MainForm.getString_0(107405095);
			this.dataGridView_3.AllowUserToAddRows = false;
			this.dataGridView_3.AllowUserToDeleteRows = false;
			this.dataGridView_3.AllowUserToResizeRows = false;
			this.dataGridView_3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_3.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewCheckBoxColumn_2,
				this.dataGridViewTextBoxColumn_6,
				this.dataGridViewTextBoxColumn_7,
				this.dataGridViewTextBoxColumn_8,
				this.dataGridViewComboBoxColumn_3,
				this.dataGridViewTextBoxColumn_9,
				this.dataGridViewComboBoxColumn_4
			});
			this.dataGridView_3.Location = new Point(6, 19);
			this.dataGridView_3.MultiSelect = false;
			this.dataGridView_3.Name = MainForm.getString_0(107404554);
			this.dataGridView_3.RowHeadersVisible = false;
			this.dataGridView_3.ShowEditingIcon = false;
			this.dataGridView_3.Size = new Size(488, 359);
			this.dataGridView_3.TabIndex = 0;
			this.dataGridView_3.CellContentClick += this.dataGridView_3_CellContentClick;
			this.dataGridView_3.CellMouseDown += this.dataGridView_2_CellMouseDown;
			this.dataGridView_3.CellValueChanged += this.dataGridView_3_CellValueChanged;
			this.dataGridView_3.DataError += this.dataGridView_2_DataError;
			this.dataGridView_3.EditingControlShowing += this.dataGridView_3_EditingControlShowing;
			this.dataGridView_3.Leave += this.MainForm_Leave;
			this.dataGridView_3.MouseClick += this.dataGridView_3_MouseClick;
			this.dataGridView_3.MouseDoubleClick += this.dataGridView_3_MouseDoubleClick;
			this.dataGridView_3.MouseUp += this.dataGridView_2_MouseUp;
			this.dataGridViewCheckBoxColumn_2.HeaderText = MainForm.getString_0(107396269);
			this.dataGridViewCheckBoxColumn_2.Name = MainForm.getString_0(107404565);
			this.dataGridViewCheckBoxColumn_2.Width = 20;
			this.dataGridViewTextBoxColumn_6.HeaderText = MainForm.getString_0(107405359);
			this.dataGridViewTextBoxColumn_6.Name = MainForm.getString_0(107404540);
			this.dataGridViewTextBoxColumn_6.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_7.HeaderText = MainForm.getString_0(107405825);
			this.dataGridViewTextBoxColumn_7.Name = MainForm.getString_0(107396514);
			this.dataGridViewTextBoxColumn_7.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_7.Width = 50;
			this.dataGridViewTextBoxColumn_8.HeaderText = MainForm.getString_0(107405808);
			this.dataGridViewTextBoxColumn_8.Name = MainForm.getString_0(107396485);
			this.dataGridViewTextBoxColumn_8.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_8.Width = 50;
			this.dataGridViewComboBoxColumn_3.HeaderText = MainForm.getString_0(107393328);
			this.dataGridViewComboBoxColumn_3.Items.AddRange(new object[]
			{
				MainForm.getString_0(107405795),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107390916),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107381835)
			});
			this.dataGridViewComboBoxColumn_3.Name = MainForm.getString_0(107395755);
			this.dataGridViewComboBoxColumn_3.Width = 145;
			this.dataGridViewTextBoxColumn_9.HeaderText = MainForm.getString_0(107405766);
			this.dataGridViewTextBoxColumn_9.Name = MainForm.getString_0(107396460);
			this.dataGridViewTextBoxColumn_9.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_9.Width = 50;
			this.dataGridViewComboBoxColumn_4.HeaderText = MainForm.getString_0(107393328);
			this.dataGridViewComboBoxColumn_4.Items.AddRange(new object[]
			{
				MainForm.getString_0(107390916),
				MainForm.getString_0(107405781),
				MainForm.getString_0(107382788),
				MainForm.getString_0(107405756),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107405723),
				MainForm.getString_0(107382662),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107405670),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107390973),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107405641),
				MainForm.getString_0(107382581),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107405612),
				MainForm.getString_0(107405071),
				MainForm.getString_0(107381928),
				MainForm.getString_0(107381939),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107405082),
				MainForm.getString_0(107381905),
				MainForm.getString_0(107381888),
				MainForm.getString_0(107381835)
			});
			this.dataGridViewComboBoxColumn_4.Name = MainForm.getString_0(107404511);
			this.dataGridViewComboBoxColumn_4.Width = 145;
			this.groupBox_31.Controls.Add(this.textBox_12);
			this.groupBox_31.Controls.Add(this.textBox_13);
			this.groupBox_31.Controls.Add(this.label_108);
			this.groupBox_31.Controls.Add(this.label_109);
			this.groupBox_31.Controls.Add(this.button_32);
			this.groupBox_31.Location = new Point(9, 51);
			this.groupBox_31.Name = MainForm.getString_0(107404478);
			this.groupBox_31.Size = new Size(497, 66);
			this.groupBox_31.TabIndex = 28;
			this.groupBox_31.TabStop = false;
			this.groupBox_31.Text = MainForm.getString_0(107404975);
			this.textBox_12.Location = new Point(97, 42);
			this.textBox_12.Name = MainForm.getString_0(107404429);
			this.textBox_12.Size = new Size(309, 20);
			this.textBox_12.TabIndex = 4;
			this.textBox_12.KeyPress += this.textBox_12_KeyPress;
			this.textBox_13.Location = new Point(97, 14);
			this.textBox_13.Name = MainForm.getString_0(107404440);
			this.textBox_13.Size = new Size(309, 20);
			this.textBox_13.TabIndex = 3;
			this.label_108.AutoSize = true;
			this.label_108.Font = new Font(MainForm.getString_0(107397254), 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_108.Location = new Point(6, 46);
			this.label_108.Name = MainForm.getString_0(107404391);
			this.label_108.Size = new Size(83, 16);
			this.label_108.TabIndex = 11;
			this.label_108.Text = MainForm.getString_0(107404947);
			this.label_109.AutoSize = true;
			this.label_109.Font = new Font(MainForm.getString_0(107397254), 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_109.Location = new Point(6, 18);
			this.label_109.Name = MainForm.getString_0(107404410);
			this.label_109.Size = new Size(79, 16);
			this.label_109.TabIndex = 9;
			this.label_109.Text = MainForm.getString_0(107404917);
			this.button_32.Location = new Point(422, 19);
			this.button_32.Name = MainForm.getString_0(107404365);
			this.button_32.Size = new Size(69, 41);
			this.button_32.TabIndex = 5;
			this.button_32.Text = MainForm.getString_0(107385117);
			this.button_32.UseVisualStyleBackColor = true;
			this.button_32.Click += this.button_32_Click;
			this.tabPage_33.Controls.Add(this.button_94);
			this.tabPage_33.Controls.Add(this.checkBox_47);
			this.tabPage_33.Controls.Add(this.label_105);
			this.tabPage_33.Controls.Add(this.numericUpDown_31);
			this.tabPage_33.Controls.Add(this.numericUpDown_32);
			this.tabPage_33.Controls.Add(this.label_106);
			this.tabPage_33.Controls.Add(this.label_107);
			this.tabPage_33.Controls.Add(this.button_31);
			this.tabPage_33.Controls.Add(this.groupBox_29);
			this.tabPage_33.Location = new Point(4, 23);
			this.tabPage_33.Name = MainForm.getString_0(107404376);
			this.tabPage_33.Size = new Size(509, 512);
			this.tabPage_33.TabIndex = 4;
			this.tabPage_33.Text = MainForm.getString_0(107404323);
			this.tabPage_33.UseVisualStyleBackColor = true;
			this.button_94.Location = new Point(477, 6);
			this.button_94.Name = MainForm.getString_0(107404338);
			this.button_94.Size = new Size(24, 23);
			this.button_94.TabIndex = 51;
			this.button_94.Text = MainForm.getString_0(107406369);
			this.button_94.UseVisualStyleBackColor = true;
			this.button_94.Click += this.button_94_Click;
			this.checkBox_47.AutoSize = true;
			this.checkBox_47.Location = new Point(6, 6);
			this.checkBox_47.Name = MainForm.getString_0(107404821);
			this.checkBox_47.Size = new Size(129, 18);
			this.checkBox_47.TabIndex = 50;
			this.checkBox_47.Text = MainForm.getString_0(107404796);
			this.checkBox_47.UseVisualStyleBackColor = true;
			this.checkBox_47.CheckedChanged += this.checkBox_47_CheckedChanged;
			this.checkBox_47.Leave += this.MainForm_Leave;
			this.label_105.AutoSize = true;
			this.label_105.Location = new Point(180, 27);
			this.label_105.Name = MainForm.getString_0(107404739);
			this.label_105.Size = new Size(18, 14);
			this.label_105.TabIndex = 43;
			this.label_105.Text = MainForm.getString_0(107411555);
			this.numericUpDown_31.Location = new Point(200, 25);
			NumericUpDown numericUpDown171 = this.numericUpDown_31;
			int[] array171 = new int[4];
			array171[0] = 600;
			numericUpDown171.Maximum = new decimal(array171);
			NumericUpDown numericUpDown172 = this.numericUpDown_31;
			int[] array172 = new int[4];
			array172[0] = 5;
			numericUpDown172.Minimum = new decimal(array172);
			this.numericUpDown_31.Name = MainForm.getString_0(107404758);
			this.numericUpDown_31.Size = new Size(47, 20);
			this.numericUpDown_31.TabIndex = 42;
			this.numericUpDown_31.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown173 = this.numericUpDown_31;
			int[] array173 = new int[4];
			array173[0] = 20;
			numericUpDown173.Value = new decimal(array173);
			this.numericUpDown_31.Leave += this.MainForm_Leave;
			this.numericUpDown_32.Location = new Point(130, 25);
			NumericUpDown numericUpDown174 = this.numericUpDown_32;
			int[] array174 = new int[4];
			array174[0] = 300;
			numericUpDown174.Maximum = new decimal(array174);
			NumericUpDown numericUpDown175 = this.numericUpDown_32;
			int[] array175 = new int[4];
			array175[0] = 1;
			numericUpDown175.Minimum = new decimal(array175);
			this.numericUpDown_32.Name = MainForm.getString_0(107404725);
			this.numericUpDown_32.Size = new Size(47, 20);
			this.numericUpDown_32.TabIndex = 40;
			this.numericUpDown_32.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown176 = this.numericUpDown_32;
			int[] array176 = new int[4];
			array176[0] = 15;
			numericUpDown176.Value = new decimal(array176);
			this.numericUpDown_32.Leave += this.MainForm_Leave;
			this.label_106.AutoSize = true;
			this.label_106.Location = new Point(250, 27);
			this.label_106.Name = MainForm.getString_0(107404692);
			this.label_106.Size = new Size(53, 14);
			this.label_106.TabIndex = 41;
			this.label_106.Text = MainForm.getString_0(107384920);
			this.label_107.AutoSize = true;
			this.label_107.Location = new Point(4, 27);
			this.label_107.Name = MainForm.getString_0(107404647);
			this.label_107.Size = new Size(123, 14);
			this.label_107.TabIndex = 39;
			this.label_107.Text = MainForm.getString_0(107405173);
			this.button_31.Location = new Point(7, 50);
			this.button_31.Name = MainForm.getString_0(107404666);
			this.button_31.Size = new Size(120, 22);
			this.button_31.TabIndex = 14;
			this.button_31.Text = MainForm.getString_0(107406485);
			this.button_31.UseVisualStyleBackColor = true;
			this.button_31.Click += this.button_31_Click;
			this.groupBox_29.Controls.Add(this.dataGridView_2);
			this.groupBox_29.Location = new Point(1, 78);
			this.groupBox_29.Name = MainForm.getString_0(107404609);
			this.groupBox_29.Size = new Size(500, 429);
			this.groupBox_29.TabIndex = 0;
			this.groupBox_29.TabStop = false;
			this.groupBox_29.Text = MainForm.getString_0(107404592);
			this.dataGridView_2.AllowUserToAddRows = false;
			this.dataGridView_2.AllowUserToDeleteRows = false;
			this.dataGridView_2.AllowUserToResizeRows = false;
			this.dataGridView_2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_2.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewCheckBoxColumn_3,
				this.dataGridViewTextBoxColumn_10,
				this.dataGridViewTextBoxColumn_11,
				this.dataGridViewTextBoxColumn_12,
				this.dataGridViewTextBoxColumn_13,
				this.dataGridViewTextBoxColumn_14,
				this.dataGridViewTextBoxColumn_15,
				this.dataGridViewComboBoxColumn_5
			});
			this.dataGridView_2.Location = new Point(6, 19);
			this.dataGridView_2.MultiSelect = false;
			this.dataGridView_2.Name = MainForm.getString_0(107404595);
			this.dataGridView_2.RowHeadersVisible = false;
			this.dataGridView_2.ShowEditingIcon = false;
			this.dataGridView_2.Size = new Size(488, 404);
			this.dataGridView_2.TabIndex = 1;
			this.dataGridView_2.CellContentClick += this.dataGridView_2_CellContentClick;
			this.dataGridView_2.CellMouseDown += this.dataGridView_2_CellMouseDown;
			this.dataGridView_2.CellValueChanged += this.dataGridView_2_CellValueChanged;
			this.dataGridView_2.DataError += this.dataGridView_2_DataError;
			this.dataGridView_2.EditingControlShowing += this.dataGridView_2_EditingControlShowing;
			this.dataGridView_2.Leave += this.MainForm_Leave;
			this.dataGridView_2.MouseClick += this.dataGridView_2_MouseClick;
			this.dataGridView_2.MouseDoubleClick += this.dataGridView_2_MouseDoubleClick;
			this.dataGridView_2.MouseUp += this.dataGridView_2_MouseUp;
			this.dataGridViewCheckBoxColumn_3.HeaderText = MainForm.getString_0(107396269);
			this.dataGridViewCheckBoxColumn_3.Name = MainForm.getString_0(107404062);
			this.dataGridViewCheckBoxColumn_3.Width = 20;
			this.dataGridViewTextBoxColumn_10.HeaderText = MainForm.getString_0(107407251);
			this.dataGridViewTextBoxColumn_10.Name = MainForm.getString_0(107396816);
			this.dataGridViewTextBoxColumn_10.ReadOnly = true;
			this.dataGridViewTextBoxColumn_10.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_11.HeaderText = MainForm.getString_0(107407210);
			this.dataGridViewTextBoxColumn_11.Name = MainForm.getString_0(107396827);
			this.dataGridViewTextBoxColumn_11.ReadOnly = true;
			this.dataGridViewTextBoxColumn_11.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_12.HeaderText = MainForm.getString_0(107405825);
			this.dataGridViewTextBoxColumn_12.Name = MainForm.getString_0(107396706);
			this.dataGridViewTextBoxColumn_12.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_12.Width = 50;
			this.dataGridViewTextBoxColumn_13.HeaderText = MainForm.getString_0(107405808);
			this.dataGridViewTextBoxColumn_13.Name = MainForm.getString_0(107396763);
			this.dataGridViewTextBoxColumn_13.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_13.Width = 50;
			this.dataGridViewTextBoxColumn_14.HeaderText = MainForm.getString_0(107393328);
			this.dataGridViewTextBoxColumn_14.Name = MainForm.getString_0(107404005);
			this.dataGridViewTextBoxColumn_14.ReadOnly = true;
			this.dataGridViewTextBoxColumn_14.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_14.Width = 145;
			this.dataGridViewTextBoxColumn_15.HeaderText = MainForm.getString_0(107405766);
			this.dataGridViewTextBoxColumn_15.Name = MainForm.getString_0(107396677);
			this.dataGridViewTextBoxColumn_15.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewTextBoxColumn_15.Width = 50;
			this.dataGridViewComboBoxColumn_5.HeaderText = MainForm.getString_0(107393328);
			this.dataGridViewComboBoxColumn_5.Items.AddRange(new object[]
			{
				MainForm.getString_0(107390916),
				MainForm.getString_0(107405781),
				MainForm.getString_0(107382788),
				MainForm.getString_0(107405756),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107405723),
				MainForm.getString_0(107382662),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107405670),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107390973),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107405641),
				MainForm.getString_0(107382581),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107405612),
				MainForm.getString_0(107405071),
				MainForm.getString_0(107381928),
				MainForm.getString_0(107381939),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107405082),
				MainForm.getString_0(107381905),
				MainForm.getString_0(107381888),
				MainForm.getString_0(107381835)
			});
			this.dataGridViewComboBoxColumn_5.Name = MainForm.getString_0(107404000);
			this.dataGridViewComboBoxColumn_5.Width = 145;
			this.tabPage_22.Controls.Add(this.tabControl_7);
			this.tabPage_22.Location = new Point(4, 42);
			this.tabPage_22.Name = MainForm.getString_0(107403967);
			this.tabPage_22.Padding = new Padding(3);
			this.tabPage_22.Size = new Size(520, 543);
			this.tabPage_22.TabIndex = 8;
			this.tabPage_22.Text = MainForm.getString_0(107403954);
			this.tabPage_22.UseVisualStyleBackColor = true;
			this.tabControl_7.Controls.Add(this.tabPage_43);
			this.tabControl_7.Controls.Add(this.tabPage_44);
			this.tabControl_7.Controls.Add(this.tabPage_49);
			this.tabControl_7.Location = new Point(0, 0);
			this.tabControl_7.Name = MainForm.getString_0(107403913);
			this.tabControl_7.SelectedIndex = 0;
			this.tabControl_7.Size = new Size(517, 545);
			this.tabControl_7.TabIndex = 0;
			this.tabPage_43.Controls.Add(this.groupBox_46);
			this.tabPage_43.Location = new Point(4, 23);
			this.tabPage_43.Name = MainForm.getString_0(107403928);
			this.tabPage_43.Padding = new Padding(3);
			this.tabPage_43.Size = new Size(509, 518);
			this.tabPage_43.TabIndex = 0;
			this.tabPage_43.Text = MainForm.getString_0(107403883);
			this.tabPage_43.UseVisualStyleBackColor = true;
			this.groupBox_46.Controls.Add(this.checkBox_77);
			this.groupBox_46.Controls.Add(this.checkBox_51);
			this.groupBox_46.Controls.Add(this.label_122);
			this.groupBox_46.Controls.Add(this.label_123);
			this.groupBox_46.Controls.Add(this.numericUpDown_43);
			this.groupBox_46.Controls.Add(this.label_124);
			this.groupBox_46.Controls.Add(this.checkBox_38);
			this.groupBox_46.Controls.Add(this.comboBox_49);
			this.groupBox_46.Controls.Add(this.checkBox_39);
			this.groupBox_46.Controls.Add(this.checkBox_40);
			this.groupBox_46.Controls.Add(this.label_125);
			this.groupBox_46.Controls.Add(this.comboBox_50);
			this.groupBox_46.Controls.Add(this.label_126);
			this.groupBox_46.Controls.Add(this.label_127);
			this.groupBox_46.Controls.Add(this.numericUpDown_44);
			this.groupBox_46.Controls.Add(this.numericUpDown_45);
			this.groupBox_46.Controls.Add(this.checkBox_41);
			this.groupBox_46.Controls.Add(this.checkBox_42);
			this.groupBox_46.Controls.Add(this.comboBox_51);
			this.groupBox_46.Controls.Add(this.button_52);
			this.groupBox_46.Controls.Add(this.fastObjectListView_11);
			this.groupBox_46.Controls.Add(this.button_53);
			this.groupBox_46.Controls.Add(this.label_128);
			this.groupBox_46.Controls.Add(this.textBox_14);
			this.groupBox_46.Controls.Add(this.checkBox_43);
			this.groupBox_46.Controls.Add(this.button_54);
			this.groupBox_46.Location = new Point(6, 6);
			this.groupBox_46.Name = MainForm.getString_0(107403894);
			this.groupBox_46.Size = new Size(500, 484);
			this.groupBox_46.TabIndex = 0;
			this.groupBox_46.TabStop = false;
			this.groupBox_46.Text = MainForm.getString_0(107388377);
			this.checkBox_77.AutoSize = true;
			this.checkBox_77.Location = new Point(6, 132);
			this.checkBox_77.Name = MainForm.getString_0(107403845);
			this.checkBox_77.Size = new Size(237, 18);
			this.checkBox_77.TabIndex = 102;
			this.checkBox_77.Text = MainForm.getString_0(107403820);
			this.checkBox_77.UseVisualStyleBackColor = true;
			this.checkBox_77.Leave += this.MainForm_Leave;
			this.checkBox_51.AutoSize = true;
			this.checkBox_51.Location = new Point(6, 310);
			this.checkBox_51.Name = MainForm.getString_0(107404311);
			this.checkBox_51.Size = new Size(160, 18);
			this.checkBox_51.TabIndex = 101;
			this.checkBox_51.Text = MainForm.getString_0(107404262);
			this.checkBox_51.UseVisualStyleBackColor = true;
			this.checkBox_51.Leave += this.MainForm_Leave;
			this.label_122.AutoSize = true;
			this.label_122.Location = new Point(170, 233);
			this.label_122.Name = MainForm.getString_0(107404229);
			this.label_122.Size = new Size(16, 14);
			this.label_122.TabIndex = 100;
			this.label_122.Text = MainForm.getString_0(107414280);
			this.label_123.AutoSize = true;
			this.label_123.Location = new Point(3, 233);
			this.label_123.Name = MainForm.getString_0(107404248);
			this.label_123.Size = new Size(103, 14);
			this.label_123.TabIndex = 99;
			this.label_123.Text = MainForm.getString_0(107404203);
			this.numericUpDown_43.Location = new Point(107, 231);
			NumericUpDown numericUpDown177 = this.numericUpDown_43;
			int[] array177 = new int[4];
			array177[0] = 99;
			numericUpDown177.Maximum = new decimal(array177);
			NumericUpDown numericUpDown178 = this.numericUpDown_43;
			int[] array178 = new int[4];
			array178[0] = 1;
			numericUpDown178.Minimum = new decimal(array178);
			this.numericUpDown_43.Name = MainForm.getString_0(107404210);
			this.numericUpDown_43.Size = new Size(61, 20);
			this.numericUpDown_43.TabIndex = 98;
			this.numericUpDown_43.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown179 = this.numericUpDown_43;
			int[] array179 = new int[4];
			array179[0] = 10;
			numericUpDown179.Value = new decimal(array179);
			this.numericUpDown_43.Leave += this.MainForm_Leave;
			this.label_124.AutoSize = true;
			this.label_124.Location = new Point(3, 254);
			this.label_124.Name = MainForm.getString_0(107404181);
			this.label_124.Size = new Size(158, 14);
			this.label_124.TabIndex = 97;
			this.label_124.Text = MainForm.getString_0(107404136);
			this.checkBox_38.AutoSize = true;
			this.checkBox_38.Location = new Point(6, 212);
			this.checkBox_38.Name = MainForm.getString_0(107404099);
			this.checkBox_38.Size = new Size(132, 18);
			this.checkBox_38.TabIndex = 96;
			this.checkBox_38.Text = MainForm.getString_0(107404074);
			this.checkBox_38.UseVisualStyleBackColor = true;
			this.checkBox_38.CheckedChanged += this.checkBox_38_CheckedChanged;
			this.checkBox_38.Leave += this.MainForm_Leave;
			this.comboBox_49.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_49.FormattingEnabled = true;
			this.comboBox_49.Items.AddRange(new object[]
			{
				MainForm.getString_0(107390916),
				MainForm.getString_0(107382788),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107390973),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107382581),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107381928),
				MainForm.getString_0(107381939),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107381905),
				MainForm.getString_0(107381888),
				MainForm.getString_0(107381835)
			});
			this.comboBox_49.Location = new Point(224, 251);
			this.comboBox_49.Name = MainForm.getString_0(107403533);
			this.comboBox_49.Size = new Size(150, 22);
			this.comboBox_49.TabIndex = 95;
			this.comboBox_49.Leave += this.MainForm_Leave;
			this.checkBox_39.AutoSize = true;
			this.checkBox_39.Location = new Point(6, 406);
			this.checkBox_39.Name = MainForm.getString_0(107403500);
			this.checkBox_39.Size = new Size(216, 18);
			this.checkBox_39.TabIndex = 94;
			this.checkBox_39.Text = MainForm.getString_0(107403511);
			this.checkBox_39.UseVisualStyleBackColor = true;
			this.checkBox_39.Leave += this.MainForm_Leave;
			this.checkBox_40.AutoSize = true;
			this.checkBox_40.Location = new Point(6, 382);
			this.checkBox_40.Name = MainForm.getString_0(107403434);
			this.checkBox_40.Size = new Size(186, 18);
			this.checkBox_40.TabIndex = 93;
			this.checkBox_40.Text = MainForm.getString_0(107403445);
			this.checkBox_40.UseVisualStyleBackColor = true;
			this.checkBox_40.Leave += this.MainForm_Leave;
			this.label_125.AutoSize = true;
			this.label_125.Location = new Point(3, 293);
			this.label_125.Name = MainForm.getString_0(107403372);
			this.label_125.Size = new Size(165, 14);
			this.label_125.TabIndex = 92;
			this.label_125.Text = MainForm.getString_0(107403391);
			this.comboBox_50.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_50.FormattingEnabled = true;
			this.comboBox_50.Items.AddRange(new object[]
			{
				MainForm.getString_0(107405795),
				MainForm.getString_0(107390916),
				MainForm.getString_0(107382788),
				MainForm.getString_0(107382763),
				MainForm.getString_0(107382778),
				MainForm.getString_0(107382749),
				MainForm.getString_0(107382696),
				MainForm.getString_0(107382711),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107382648),
				MainForm.getString_0(107390973),
				MainForm.getString_0(107382595),
				MainForm.getString_0(107382574),
				MainForm.getString_0(107382581),
				MainForm.getString_0(107382044),
				MainForm.getString_0(107381991),
				MainForm.getString_0(107382002),
				MainForm.getString_0(107381981),
				MainForm.getString_0(107381928),
				MainForm.getString_0(107381939),
				MainForm.getString_0(107381918),
				MainForm.getString_0(107381905),
				MainForm.getString_0(107381888),
				MainForm.getString_0(107381835)
			});
			this.comboBox_50.Location = new Point(224, 290);
			this.comboBox_50.Name = MainForm.getString_0(107403350);
			this.comboBox_50.Size = new Size(150, 22);
			this.comboBox_50.TabIndex = 91;
			this.label_126.AutoSize = true;
			this.label_126.Location = new Point(3, 181);
			this.label_126.Name = MainForm.getString_0(107403321);
			this.label_126.Size = new Size(98, 14);
			this.label_126.TabIndex = 90;
			this.label_126.Text = MainForm.getString_0(107403792);
			this.label_127.AutoSize = true;
			this.label_127.Location = new Point(3, 153);
			this.label_127.Name = MainForm.getString_0(107403799);
			this.label_127.Size = new Size(96, 14);
			this.label_127.TabIndex = 89;
			this.label_127.Text = MainForm.getString_0(107403758);
			this.numericUpDown_44.Location = new Point(161, 179);
			NumericUpDown numericUpDown180 = this.numericUpDown_44;
			int[] array180 = new int[4];
			array180[0] = 10;
			numericUpDown180.Maximum = new decimal(array180);
			NumericUpDown numericUpDown181 = this.numericUpDown_44;
			int[] array181 = new int[4];
			array181[0] = 1;
			numericUpDown181.Minimum = new decimal(array181);
			this.numericUpDown_44.Name = MainForm.getString_0(107403765);
			this.numericUpDown_44.Size = new Size(61, 20);
			this.numericUpDown_44.TabIndex = 88;
			this.numericUpDown_44.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown182 = this.numericUpDown_44;
			int[] array182 = new int[4];
			array182[0] = 10;
			numericUpDown182.Value = new decimal(array182);
			this.numericUpDown_44.Leave += this.MainForm_Leave;
			this.numericUpDown_45.Location = new Point(161, 151);
			NumericUpDown numericUpDown183 = this.numericUpDown_45;
			int[] array183 = new int[4];
			array183[0] = 10;
			numericUpDown183.Maximum = new decimal(array183);
			NumericUpDown numericUpDown184 = this.numericUpDown_45;
			int[] array184 = new int[4];
			array184[0] = 1;
			numericUpDown184.Minimum = new decimal(array184);
			this.numericUpDown_45.Name = MainForm.getString_0(107403736);
			this.numericUpDown_45.Size = new Size(61, 20);
			this.numericUpDown_45.TabIndex = 87;
			this.numericUpDown_45.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown185 = this.numericUpDown_45;
			int[] array185 = new int[4];
			array185[0] = 3;
			numericUpDown185.Value = new decimal(array185);
			this.numericUpDown_45.Leave += this.MainForm_Leave;
			this.checkBox_41.AutoSize = true;
			this.checkBox_41.Location = new Point(6, 358);
			this.checkBox_41.Name = MainForm.getString_0(107403707);
			this.checkBox_41.Size = new Size(180, 18);
			this.checkBox_41.TabIndex = 81;
			this.checkBox_41.Text = MainForm.getString_0(107403654);
			this.checkBox_41.UseVisualStyleBackColor = true;
			this.checkBox_41.Leave += this.MainForm_Leave;
			this.checkBox_42.AutoSize = true;
			this.checkBox_42.Location = new Point(6, 334);
			this.checkBox_42.Name = MainForm.getString_0(107403617);
			this.checkBox_42.Size = new Size(205, 18);
			this.checkBox_42.TabIndex = 80;
			this.checkBox_42.Text = MainForm.getString_0(107403588);
			this.checkBox_42.UseVisualStyleBackColor = true;
			this.checkBox_42.Leave += this.MainForm_Leave;
			this.comboBox_51.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_51.FormattingEnabled = true;
			this.comboBox_51.Location = new Point(6, 19);
			this.comboBox_51.Name = MainForm.getString_0(107403571);
			this.comboBox_51.Size = new Size(207, 22);
			this.comboBox_51.TabIndex = 77;
			this.button_52.Location = new Point(6, 47);
			this.button_52.Name = MainForm.getString_0(107403038);
			this.button_52.Size = new Size(69, 20);
			this.button_52.TabIndex = 78;
			this.button_52.Text = MainForm.getString_0(107385117);
			this.button_52.UseVisualStyleBackColor = true;
			this.fastObjectListView_11.AllColumns.Add(this.olvcolumn_16);
			this.fastObjectListView_11.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_16
			});
			this.fastObjectListView_11.FullRowSelect = true;
			this.fastObjectListView_11.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_11.HideSelection = false;
			this.fastObjectListView_11.Location = new Point(248, 19);
			this.fastObjectListView_11.MultiSelect = false;
			this.fastObjectListView_11.Name = MainForm.getString_0(107402985);
			this.fastObjectListView_11.OwnerDraw = true;
			this.fastObjectListView_11.ShowGroups = false;
			this.fastObjectListView_11.Size = new Size(246, 198);
			this.fastObjectListView_11.TabIndex = 79;
			this.fastObjectListView_11.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_11.View = View.Details;
			this.fastObjectListView_11.VirtualMode = true;
			this.olvcolumn_16.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_16.CellPadding = null;
			this.olvcolumn_16.FillsFreeSpace = true;
			this.olvcolumn_16.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_16.Text = MainForm.getString_0(107384059);
			this.olvcolumn_16.Width = 250;
			this.button_53.Location = new Point(374, 454);
			this.button_53.Name = MainForm.getString_0(107403000);
			this.button_53.Size = new Size(123, 25);
			this.button_53.TabIndex = 85;
			this.button_53.Text = MainForm.getString_0(107385211);
			this.button_53.UseVisualStyleBackColor = true;
			this.button_53.Click += this.button_75_Click;
			this.label_128.AutoSize = true;
			this.label_128.Location = new Point(3, 457);
			this.label_128.Name = MainForm.getString_0(107402971);
			this.label_128.Size = new Size(97, 14);
			this.label_128.TabIndex = 86;
			this.label_128.Text = MainForm.getString_0(107402926);
			this.textBox_14.Location = new Point(100, 454);
			this.textBox_14.Name = MainForm.getString_0(107402937);
			this.textBox_14.Size = new Size(111, 20);
			this.textBox_14.TabIndex = 84;
			this.textBox_14.Leave += this.MainForm_Leave;
			this.checkBox_43.AutoSize = true;
			this.checkBox_43.Location = new Point(6, 430);
			this.checkBox_43.Name = MainForm.getString_0(107402884);
			this.checkBox_43.Size = new Size(217, 18);
			this.checkBox_43.TabIndex = 82;
			this.checkBox_43.Text = MainForm.getString_0(107402863);
			this.checkBox_43.UseVisualStyleBackColor = true;
			this.checkBox_43.Leave += this.MainForm_Leave;
			this.button_54.Location = new Point(374, 423);
			this.button_54.Name = MainForm.getString_0(107402846);
			this.button_54.Size = new Size(123, 25);
			this.button_54.TabIndex = 83;
			this.button_54.Text = MainForm.getString_0(107402797);
			this.button_54.UseVisualStyleBackColor = true;
			this.button_54.Click += this.button_54_Click;
			this.tabPage_44.Controls.Add(this.groupBox_47);
			this.tabPage_44.Location = new Point(4, 23);
			this.tabPage_44.Name = MainForm.getString_0(107402816);
			this.tabPage_44.Padding = new Padding(3);
			this.tabPage_44.Size = new Size(509, 518);
			this.tabPage_44.TabIndex = 1;
			this.tabPage_44.Text = MainForm.getString_0(107402803);
			this.tabPage_44.UseVisualStyleBackColor = true;
			this.groupBox_47.Controls.Add(this.comboBox_52);
			this.groupBox_47.Controls.Add(this.button_55);
			this.groupBox_47.Controls.Add(this.fastObjectListView_12);
			this.groupBox_47.Location = new Point(6, 6);
			this.groupBox_47.Name = MainForm.getString_0(107403294);
			this.groupBox_47.Size = new Size(500, 262);
			this.groupBox_47.TabIndex = 0;
			this.groupBox_47.TabStop = false;
			this.groupBox_47.Text = MainForm.getString_0(107402803);
			this.comboBox_52.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_52.FormattingEnabled = true;
			this.comboBox_52.Location = new Point(6, 19);
			this.comboBox_52.Name = MainForm.getString_0(107403245);
			this.comboBox_52.Size = new Size(207, 22);
			this.comboBox_52.TabIndex = 80;
			this.button_55.Location = new Point(6, 47);
			this.button_55.Name = MainForm.getString_0(107403256);
			this.button_55.Size = new Size(69, 20);
			this.button_55.TabIndex = 81;
			this.button_55.Text = MainForm.getString_0(107385117);
			this.button_55.UseVisualStyleBackColor = true;
			this.fastObjectListView_12.AllColumns.Add(this.olvcolumn_17);
			this.fastObjectListView_12.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_17
			});
			this.fastObjectListView_12.FullRowSelect = true;
			this.fastObjectListView_12.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_12.HideSelection = false;
			this.fastObjectListView_12.Location = new Point(251, 19);
			this.fastObjectListView_12.MultiSelect = false;
			this.fastObjectListView_12.Name = MainForm.getString_0(107403203);
			this.fastObjectListView_12.OwnerDraw = true;
			this.fastObjectListView_12.ShowGroups = false;
			this.fastObjectListView_12.Size = new Size(246, 237);
			this.fastObjectListView_12.TabIndex = 82;
			this.fastObjectListView_12.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_12.View = View.Details;
			this.fastObjectListView_12.VirtualMode = true;
			this.olvcolumn_17.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_17.CellPadding = null;
			this.olvcolumn_17.FillsFreeSpace = true;
			this.olvcolumn_17.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_17.Text = MainForm.getString_0(107414071);
			this.olvcolumn_17.Width = 260;
			this.tabPage_49.Controls.Add(this.groupBox_60);
			this.tabPage_49.Location = new Point(4, 23);
			this.tabPage_49.Name = MainForm.getString_0(107403178);
			this.tabPage_49.Size = new Size(509, 518);
			this.tabPage_49.TabIndex = 2;
			this.tabPage_49.Text = MainForm.getString_0(107396881);
			this.tabPage_49.UseVisualStyleBackColor = true;
			this.groupBox_60.Controls.Add(this.button_80);
			this.groupBox_60.Controls.Add(this.button_81);
			this.groupBox_60.Location = new Point(3, 3);
			this.groupBox_60.Name = MainForm.getString_0(107403197);
			this.groupBox_60.Size = new Size(503, 48);
			this.groupBox_60.TabIndex = 0;
			this.groupBox_60.TabStop = false;
			this.groupBox_60.Text = MainForm.getString_0(107403148);
			this.button_80.Location = new Point(374, 17);
			this.button_80.Name = MainForm.getString_0(107403155);
			this.button_80.Size = new Size(123, 25);
			this.button_80.TabIndex = 87;
			this.button_80.Text = MainForm.getString_0(107403110);
			this.button_80.UseVisualStyleBackColor = true;
			this.button_80.Click += this.button_80_Click;
			this.button_81.Location = new Point(6, 19);
			this.button_81.Name = MainForm.getString_0(107403125);
			this.button_81.Size = new Size(123, 25);
			this.button_81.TabIndex = 86;
			this.button_81.Text = MainForm.getString_0(107385211);
			this.button_81.UseVisualStyleBackColor = true;
			this.button_81.Click += this.button_75_Click;
			this.tabPage_19.Controls.Add(this.tabControl_11);
			this.tabPage_19.Location = new Point(4, 42);
			this.tabPage_19.Name = MainForm.getString_0(107403080);
			this.tabPage_19.Size = new Size(520, 543);
			this.tabPage_19.TabIndex = 6;
			this.tabPage_19.Text = MainForm.getString_0(107403095);
			this.tabPage_19.UseVisualStyleBackColor = true;
			this.tabControl_11.Controls.Add(this.tabPage_35);
			this.tabControl_11.Controls.Add(this.tabPage_36);
			this.tabControl_11.Controls.Add(this.tabPage_37);
			this.tabControl_11.Controls.Add(this.tabPage_48);
			this.tabControl_11.Controls.Add(this.tabPage_40);
			this.tabControl_11.Controls.Add(this.tabPage_41);
			this.tabControl_11.Controls.Add(this.tabPage_20);
			this.tabControl_11.Controls.Add(this.tabPage_38);
			this.tabControl_11.Controls.Add(this.tabPage_39);
			this.tabControl_11.Location = new Point(0, 0);
			this.tabControl_11.Name = MainForm.getString_0(107403050);
			this.tabControl_11.SelectedIndex = 0;
			this.tabControl_11.Size = new Size(517, 542);
			this.tabControl_11.TabIndex = 0;
			this.tabControl_11.SelectedIndexChanged += this.MainForm_Leave;
			this.tabPage_35.Controls.Add(this.groupBox_45);
			this.tabPage_35.Controls.Add(this.groupBox_34);
			this.tabPage_35.Location = new Point(4, 23);
			this.tabPage_35.Name = MainForm.getString_0(107403061);
			this.tabPage_35.Size = new Size(509, 515);
			this.tabPage_35.TabIndex = 4;
			this.tabPage_35.Text = MainForm.getString_0(107388377);
			this.tabPage_35.UseVisualStyleBackColor = true;
			this.groupBox_45.Controls.Add(this.checkBox_65);
			this.groupBox_45.Controls.Add(this.comboBox_71);
			this.groupBox_45.Controls.Add(this.checkBox_63);
			this.groupBox_45.Controls.Add(this.comboBox_48);
			this.groupBox_45.Controls.Add(this.checkBox_37);
			this.groupBox_45.Controls.Add(this.label_121);
			this.groupBox_45.Controls.Add(this.trackBar_1);
			this.groupBox_45.Location = new Point(4, 307);
			this.groupBox_45.Name = MainForm.getString_0(107402524);
			this.groupBox_45.Size = new Size(502, 205);
			this.groupBox_45.TabIndex = 1;
			this.groupBox_45.TabStop = false;
			this.groupBox_45.Text = MainForm.getString_0(107388377);
			this.checkBox_65.AutoSize = true;
			this.checkBox_65.Location = new Point(5, 69);
			this.checkBox_65.Name = MainForm.getString_0(107402475);
			this.checkBox_65.Size = new Size(209, 18);
			this.checkBox_65.TabIndex = 81;
			this.checkBox_65.Text = MainForm.getString_0(107402446);
			this.checkBox_65.UseVisualStyleBackColor = true;
			this.checkBox_65.Leave += this.MainForm_Leave;
			this.comboBox_71.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_71.FormattingEnabled = true;
			this.comboBox_71.Location = new Point(240, 43);
			this.comboBox_71.Name = MainForm.getString_0(107402429);
			this.comboBox_71.Size = new Size(165, 22);
			this.comboBox_71.TabIndex = 60;
			this.comboBox_71.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_71.Leave += this.MainForm_Leave;
			this.checkBox_63.AutoSize = true;
			this.checkBox_63.Location = new Point(6, 45);
			this.checkBox_63.Name = MainForm.getString_0(107402372);
			this.checkBox_63.Size = new Size(236, 18);
			this.checkBox_63.TabIndex = 59;
			this.checkBox_63.Text = MainForm.getString_0(107402387);
			this.checkBox_63.UseVisualStyleBackColor = true;
			this.checkBox_63.CheckedChanged += this.checkBox_63_CheckedChanged;
			this.checkBox_63.Leave += this.MainForm_Leave;
			this.comboBox_48.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_48.FormattingEnabled = true;
			this.comboBox_48.Location = new Point(240, 17);
			this.comboBox_48.Name = MainForm.getString_0(107402334);
			this.comboBox_48.Size = new Size(165, 22);
			this.comboBox_48.TabIndex = 58;
			this.comboBox_48.Visible = false;
			this.comboBox_48.SelectedValueChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_48.Leave += this.MainForm_Leave;
			this.checkBox_37.AutoSize = true;
			this.checkBox_37.Location = new Point(6, 19);
			this.checkBox_37.Name = MainForm.getString_0(107402301);
			this.checkBox_37.Size = new Size(214, 18);
			this.checkBox_37.TabIndex = 57;
			this.checkBox_37.Text = MainForm.getString_0(107402756);
			this.checkBox_37.UseVisualStyleBackColor = true;
			this.checkBox_37.Leave += this.MainForm_Leave;
			this.label_121.AutoSize = true;
			this.label_121.Location = new Point(3, 90);
			this.label_121.Name = MainForm.getString_0(107402739);
			this.label_121.Size = new Size(92, 14);
			this.label_121.TabIndex = 56;
			this.label_121.Text = MainForm.getString_0(107402694);
			this.trackBar_1.LargeChange = 2;
			this.trackBar_1.Location = new Point(6, 107);
			this.trackBar_1.Name = MainForm.getString_0(107402705);
			this.trackBar_1.Size = new Size(203, 45);
			this.trackBar_1.TabIndex = 55;
			this.trackBar_1.Leave += this.MainForm_Leave;
			this.groupBox_34.Controls.Add(this.panel_0);
			this.groupBox_34.Controls.Add(this.button_34);
			this.groupBox_34.Controls.Add(this.button_35);
			this.groupBox_34.Controls.Add(this.comboBox_34);
			this.groupBox_34.Location = new Point(3, 3);
			this.groupBox_34.Name = MainForm.getString_0(107402688);
			this.groupBox_34.Size = new Size(503, 298);
			this.groupBox_34.TabIndex = 0;
			this.groupBox_34.TabStop = false;
			this.groupBox_34.Text = MainForm.getString_0(107402639);
			this.panel_0.BackgroundImage = (Image)componentResourceManager.GetObject(MainForm.getString_0(107402646));
			this.panel_0.BackgroundImageLayout = ImageLayout.Center;
			this.panel_0.Location = new Point(200, 83);
			this.panel_0.Name = MainForm.getString_0(107402573);
			this.panel_0.Size = new Size(103, 209);
			this.panel_0.TabIndex = 29;
			this.button_34.Location = new Point(388, 47);
			this.button_34.Name = MainForm.getString_0(107402584);
			this.button_34.Size = new Size(109, 25);
			this.button_34.TabIndex = 27;
			this.button_34.Text = MainForm.getString_0(107402555);
			this.button_34.UseVisualStyleBackColor = true;
			this.button_34.Click += this.button_34_Click;
			this.button_35.Location = new Point(6, 47);
			this.button_35.Name = MainForm.getString_0(107401994);
			this.button_35.Size = new Size(123, 25);
			this.button_35.TabIndex = 26;
			this.button_35.Text = MainForm.getString_0(107385211);
			this.button_35.UseVisualStyleBackColor = true;
			this.button_35.Click += this.button_75_Click;
			this.comboBox_34.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_34.FormattingEnabled = true;
			this.comboBox_34.Location = new Point(6, 19);
			this.comboBox_34.Name = MainForm.getString_0(107401957);
			this.comboBox_34.Size = new Size(207, 22);
			this.comboBox_34.TabIndex = 3;
			this.tabPage_36.Controls.Add(this.groupBox_35);
			this.tabPage_36.Location = new Point(4, 23);
			this.tabPage_36.Name = MainForm.getString_0(107401928);
			this.tabPage_36.Size = new Size(509, 515);
			this.tabPage_36.TabIndex = 5;
			this.tabPage_36.Text = MainForm.getString_0(107401939);
			this.tabPage_36.UseVisualStyleBackColor = true;
			this.groupBox_35.Controls.Add(this.comboBox_77);
			this.groupBox_35.Controls.Add(this.button_39);
			this.groupBox_35.Controls.Add(this.button_40);
			this.groupBox_35.Controls.Add(this.checkBox_33);
			this.groupBox_35.Controls.Add(this.checkBox_34);
			this.groupBox_35.Controls.Add(this.groupBox_36);
			this.groupBox_35.Controls.Add(this.groupBox_37);
			this.groupBox_35.Controls.Add(this.checkBox_35);
			this.groupBox_35.Controls.Add(this.numericUpDown_36);
			this.groupBox_35.Controls.Add(this.label_114);
			this.groupBox_35.Controls.Add(this.button_38);
			this.groupBox_35.Location = new Point(3, 3);
			this.groupBox_35.Name = MainForm.getString_0(107401890);
			this.groupBox_35.Size = new Size(503, 509);
			this.groupBox_35.TabIndex = 2;
			this.groupBox_35.TabStop = false;
			this.groupBox_35.Text = MainForm.getString_0(107388377);
			this.button_39.Enabled = false;
			this.button_39.Location = new Point(390, 43);
			this.button_39.Name = MainForm.getString_0(107401905);
			this.button_39.Size = new Size(107, 25);
			this.button_39.TabIndex = 46;
			this.button_39.Tag = MainForm.getString_0(107401939);
			this.button_39.Text = MainForm.getString_0(107401880);
			this.button_39.UseVisualStyleBackColor = true;
			this.button_39.Click += this.button_41_Click;
			this.button_40.Enabled = false;
			this.button_40.Location = new Point(390, 12);
			this.button_40.Name = MainForm.getString_0(107401827);
			this.button_40.Size = new Size(107, 25);
			this.button_40.TabIndex = 45;
			this.button_40.Text = MainForm.getString_0(107401802);
			this.button_40.UseVisualStyleBackColor = true;
			this.button_40.Click += this.button_42_Click;
			this.checkBox_33.AutoSize = true;
			this.checkBox_33.Location = new Point(6, 43);
			this.checkBox_33.Name = MainForm.getString_0(107401813);
			this.checkBox_33.Size = new Size(196, 18);
			this.checkBox_33.TabIndex = 44;
			this.checkBox_33.Text = MainForm.getString_0(107401764);
			this.checkBox_33.UseVisualStyleBackColor = true;
			this.checkBox_33.Leave += this.MainForm_Leave;
			this.checkBox_34.AutoSize = true;
			this.checkBox_34.Location = new Point(6, 67);
			this.checkBox_34.Name = MainForm.getString_0(107402263);
			this.checkBox_34.Size = new Size(198, 18);
			this.checkBox_34.TabIndex = 43;
			this.checkBox_34.Text = MainForm.getString_0(107402210);
			this.checkBox_34.UseVisualStyleBackColor = true;
			this.checkBox_34.Leave += this.MainForm_Leave;
			this.groupBox_36.Controls.Add(this.comboBox_35);
			this.groupBox_36.Controls.Add(this.button_36);
			this.groupBox_36.Controls.Add(this.fastObjectListView_6);
			this.groupBox_36.Controls.Add(this.comboBox_36);
			this.groupBox_36.Controls.Add(this.comboBox_37);
			this.groupBox_36.Location = new Point(6, 323);
			this.groupBox_36.Name = MainForm.getString_0(107402197);
			this.groupBox_36.Size = new Size(491, 180);
			this.groupBox_36.TabIndex = 42;
			this.groupBox_36.TabStop = false;
			this.groupBox_36.Text = MainForm.getString_0(107402148);
			this.comboBox_35.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_35.FormattingEnabled = true;
			this.comboBox_35.Items.AddRange(new object[]
			{
				MainForm.getString_0(107402171),
				MainForm.getString_0(107402162)
			});
			this.comboBox_35.Location = new Point(409, 46);
			this.comboBox_35.Name = MainForm.getString_0(107402121);
			this.comboBox_35.Size = new Size(76, 22);
			this.comboBox_35.TabIndex = 44;
			this.button_36.Location = new Point(461, 18);
			this.button_36.Name = MainForm.getString_0(107402096);
			this.button_36.Size = new Size(24, 24);
			this.button_36.TabIndex = 43;
			this.button_36.Text = MainForm.getString_0(107389453);
			this.button_36.UseVisualStyleBackColor = true;
			this.fastObjectListView_6.AllColumns.Add(this.olvcolumn_7);
			this.fastObjectListView_6.AllColumns.Add(this.olvcolumn_8);
			this.fastObjectListView_6.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_7,
				this.olvcolumn_8
			});
			this.fastObjectListView_6.FullRowSelect = true;
			this.fastObjectListView_6.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_6.HideSelection = false;
			this.fastObjectListView_6.Location = new Point(6, 75);
			this.fastObjectListView_6.MultiSelect = false;
			this.fastObjectListView_6.Name = MainForm.getString_0(107402107);
			this.fastObjectListView_6.OwnerDraw = true;
			this.fastObjectListView_6.ShowGroups = false;
			this.fastObjectListView_6.ShowSortIndicators = false;
			this.fastObjectListView_6.Size = new Size(479, 100);
			this.fastObjectListView_6.TabIndex = 42;
			this.fastObjectListView_6.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_6.View = View.Details;
			this.fastObjectListView_6.VirtualMode = true;
			this.olvcolumn_7.AspectName = MainForm.getString_0(107402058);
			this.olvcolumn_7.CellPadding = null;
			this.olvcolumn_7.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_7.Text = MainForm.getString_0(107402148);
			this.olvcolumn_7.Width = 349;
			this.olvcolumn_8.AspectName = MainForm.getString_0(107402049);
			this.olvcolumn_8.CellPadding = null;
			this.olvcolumn_8.Text = MainForm.getString_0(107402049);
			this.olvcolumn_8.Width = 125;
			this.comboBox_36.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_36.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_36.FormattingEnabled = true;
			this.comboBox_36.Location = new Point(6, 19);
			this.comboBox_36.Name = MainForm.getString_0(107402072);
			this.comboBox_36.Size = new Size(452, 22);
			this.comboBox_36.TabIndex = 39;
			this.comboBox_36.SelectedIndexChanged += this.comboBox_45_SelectedIndexChanged;
			this.comboBox_37.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_37.FormattingEnabled = true;
			this.comboBox_37.Location = new Point(6, 47);
			this.comboBox_37.Name = MainForm.getString_0(107402019);
			this.comboBox_37.Size = new Size(159, 22);
			this.comboBox_37.TabIndex = 40;
			this.groupBox_37.Controls.Add(this.comboBox_38);
			this.groupBox_37.Controls.Add(this.button_37);
			this.groupBox_37.Controls.Add(this.fastObjectListView_7);
			this.groupBox_37.Controls.Add(this.comboBox_39);
			this.groupBox_37.Controls.Add(this.comboBox_40);
			this.groupBox_37.Location = new Point(6, 120);
			this.groupBox_37.Name = MainForm.getString_0(107401482);
			this.groupBox_37.Size = new Size(491, 180);
			this.groupBox_37.TabIndex = 41;
			this.groupBox_37.TabStop = false;
			this.groupBox_37.Text = MainForm.getString_0(107401497);
			this.comboBox_38.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_38.FormattingEnabled = true;
			this.comboBox_38.Items.AddRange(new object[]
			{
				MainForm.getString_0(107402171),
				MainForm.getString_0(107402162)
			});
			this.comboBox_38.Location = new Point(409, 47);
			this.comboBox_38.Name = MainForm.getString_0(107401456);
			this.comboBox_38.Size = new Size(76, 22);
			this.comboBox_38.TabIndex = 43;
			this.button_37.Location = new Point(461, 18);
			this.button_37.Name = MainForm.getString_0(107401463);
			this.button_37.Size = new Size(24, 24);
			this.button_37.TabIndex = 42;
			this.button_37.Text = MainForm.getString_0(107389453);
			this.button_37.UseVisualStyleBackColor = true;
			this.fastObjectListView_7.AllColumns.Add(this.olvcolumn_9);
			this.fastObjectListView_7.AllColumns.Add(this.olvcolumn_10);
			this.fastObjectListView_7.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_9,
				this.olvcolumn_10
			});
			this.fastObjectListView_7.FullRowSelect = true;
			this.fastObjectListView_7.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_7.HideSelection = false;
			this.fastObjectListView_7.Location = new Point(6, 75);
			this.fastObjectListView_7.MultiSelect = false;
			this.fastObjectListView_7.Name = MainForm.getString_0(107401410);
			this.fastObjectListView_7.OwnerDraw = true;
			this.fastObjectListView_7.ShowGroups = false;
			this.fastObjectListView_7.ShowSortIndicators = false;
			this.fastObjectListView_7.Size = new Size(479, 100);
			this.fastObjectListView_7.TabIndex = 41;
			this.fastObjectListView_7.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_7.View = View.Details;
			this.fastObjectListView_7.VirtualMode = true;
			this.olvcolumn_9.AspectName = MainForm.getString_0(107402058);
			this.olvcolumn_9.CellPadding = null;
			this.olvcolumn_9.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_9.Text = MainForm.getString_0(107401497);
			this.olvcolumn_9.Width = 349;
			this.olvcolumn_10.AspectName = MainForm.getString_0(107402049);
			this.olvcolumn_10.CellPadding = null;
			this.olvcolumn_10.Text = MainForm.getString_0(107402049);
			this.olvcolumn_10.Width = 125;
			this.comboBox_39.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_39.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_39.FormattingEnabled = true;
			this.comboBox_39.Location = new Point(6, 19);
			this.comboBox_39.Name = MainForm.getString_0(107401425);
			this.comboBox_39.Size = new Size(452, 22);
			this.comboBox_39.TabIndex = 39;
			this.comboBox_39.SelectedIndexChanged += this.comboBox_45_SelectedIndexChanged;
			this.comboBox_40.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_40.FormattingEnabled = true;
			this.comboBox_40.Location = new Point(6, 47);
			this.comboBox_40.Name = MainForm.getString_0(107401404);
			this.comboBox_40.Size = new Size(159, 22);
			this.comboBox_40.TabIndex = 40;
			this.checkBox_35.AutoSize = true;
			this.checkBox_35.Location = new Point(6, 19);
			this.checkBox_35.Name = MainForm.getString_0(107401347);
			this.checkBox_35.Size = new Size(188, 18);
			this.checkBox_35.TabIndex = 38;
			this.checkBox_35.Text = MainForm.getString_0(107401318);
			this.checkBox_35.UseVisualStyleBackColor = true;
			this.checkBox_35.CheckedChanged += this.checkBox_35_CheckedChanged;
			this.checkBox_35.Leave += this.MainForm_Leave;
			this.numericUpDown_36.Location = new Point(359, 92);
			NumericUpDown numericUpDown186 = this.numericUpDown_36;
			int[] array186 = new int[4];
			array186[0] = 20000;
			numericUpDown186.Maximum = new decimal(array186);
			this.numericUpDown_36.Name = MainForm.getString_0(107401309);
			this.numericUpDown_36.Size = new Size(53, 20);
			this.numericUpDown_36.TabIndex = 37;
			this.numericUpDown_36.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown187 = this.numericUpDown_36;
			int[] array187 = new int[4];
			array187[0] = 1;
			numericUpDown187.Value = new decimal(array187);
			this.numericUpDown_36.Leave += this.MainForm_Leave;
			this.label_114.AutoSize = true;
			this.label_114.Location = new Point(261, 95);
			this.label_114.Name = MainForm.getString_0(107401260);
			this.label_114.Size = new Size(95, 14);
			this.label_114.TabIndex = 36;
			this.label_114.Text = MainForm.getString_0(107401279);
			this.button_38.Enabled = false;
			this.button_38.Location = new Point(418, 89);
			this.button_38.Name = MainForm.getString_0(107401734);
			this.button_38.Size = new Size(79, 25);
			this.button_38.TabIndex = 0;
			this.button_38.Text = MainForm.getString_0(107401705);
			this.button_38.UseVisualStyleBackColor = true;
			this.button_38.Click += this.button_38_Click;
			this.tabPage_37.Controls.Add(this.groupBox_38);
			this.tabPage_37.Location = new Point(4, 23);
			this.tabPage_37.Name = MainForm.getString_0(107401728);
			this.tabPage_37.Size = new Size(509, 515);
			this.tabPage_37.TabIndex = 6;
			this.tabPage_37.Text = MainForm.getString_0(107401671);
			this.tabPage_37.UseVisualStyleBackColor = true;
			this.groupBox_38.Controls.Add(this.comboBox_76);
			this.groupBox_38.Controls.Add(this.groupBox_39);
			this.groupBox_38.Controls.Add(this.groupBox_40);
			this.groupBox_38.Controls.Add(this.radioButton_3);
			this.groupBox_38.Controls.Add(this.radioButton_4);
			this.groupBox_38.Controls.Add(this.button_41);
			this.groupBox_38.Controls.Add(this.button_42);
			this.groupBox_38.Controls.Add(this.checkBox_36);
			this.groupBox_38.Controls.Add(this.numericUpDown_37);
			this.groupBox_38.Controls.Add(this.label_115);
			this.groupBox_38.Controls.Add(this.button_43);
			this.groupBox_38.Location = new Point(4, 3);
			this.groupBox_38.Name = MainForm.getString_0(107401694);
			this.groupBox_38.Size = new Size(502, 509);
			this.groupBox_38.TabIndex = 3;
			this.groupBox_38.TabStop = false;
			this.groupBox_38.Text = MainForm.getString_0(107388377);
			this.groupBox_39.Controls.Add(this.comboBox_41);
			this.groupBox_39.Controls.Add(this.button_44);
			this.groupBox_39.Controls.Add(this.fastObjectListView_8);
			this.groupBox_39.Controls.Add(this.comboBox_42);
			this.groupBox_39.Controls.Add(this.comboBox_43);
			this.groupBox_39.Location = new Point(6, 323);
			this.groupBox_39.Name = MainForm.getString_0(107401685);
			this.groupBox_39.Size = new Size(491, 180);
			this.groupBox_39.TabIndex = 51;
			this.groupBox_39.TabStop = false;
			this.groupBox_39.Text = MainForm.getString_0(107402148);
			this.comboBox_41.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_41.FormattingEnabled = true;
			this.comboBox_41.Items.AddRange(new object[]
			{
				MainForm.getString_0(107402171),
				MainForm.getString_0(107402162),
				MainForm.getString_0(107401636)
			});
			this.comboBox_41.Location = new Point(409, 47);
			this.comboBox_41.Name = MainForm.getString_0(107401659);
			this.comboBox_41.Size = new Size(76, 22);
			this.comboBox_41.TabIndex = 44;
			this.button_44.Location = new Point(461, 18);
			this.button_44.Name = MainForm.getString_0(107401602);
			this.button_44.Size = new Size(24, 24);
			this.button_44.TabIndex = 43;
			this.button_44.Text = MainForm.getString_0(107389453);
			this.button_44.UseVisualStyleBackColor = true;
			this.fastObjectListView_8.AllColumns.Add(this.olvcolumn_11);
			this.fastObjectListView_8.AllColumns.Add(this.olvcolumn_12);
			this.fastObjectListView_8.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_11,
				this.olvcolumn_12
			});
			this.fastObjectListView_8.FullRowSelect = true;
			this.fastObjectListView_8.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_8.HideSelection = false;
			this.fastObjectListView_8.Location = new Point(6, 75);
			this.fastObjectListView_8.MultiSelect = false;
			this.fastObjectListView_8.Name = MainForm.getString_0(107401577);
			this.fastObjectListView_8.OwnerDraw = true;
			this.fastObjectListView_8.ShowGroups = false;
			this.fastObjectListView_8.ShowSortIndicators = false;
			this.fastObjectListView_8.Size = new Size(479, 100);
			this.fastObjectListView_8.TabIndex = 42;
			this.fastObjectListView_8.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_8.View = View.Details;
			this.fastObjectListView_8.VirtualMode = true;
			this.olvcolumn_11.AspectName = MainForm.getString_0(107402058);
			this.olvcolumn_11.CellPadding = null;
			this.olvcolumn_11.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_11.Text = MainForm.getString_0(107402148);
			this.olvcolumn_11.Width = 349;
			this.olvcolumn_12.AspectName = MainForm.getString_0(107402049);
			this.olvcolumn_12.CellPadding = null;
			this.olvcolumn_12.Text = MainForm.getString_0(107402049);
			this.olvcolumn_12.Width = 125;
			this.comboBox_42.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_42.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_42.FormattingEnabled = true;
			this.comboBox_42.Location = new Point(6, 19);
			this.comboBox_42.Name = MainForm.getString_0(107401588);
			this.comboBox_42.Size = new Size(449, 22);
			this.comboBox_42.TabIndex = 39;
			this.comboBox_42.SelectedIndexChanged += this.comboBox_45_SelectedIndexChanged;
			this.comboBox_43.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_43.FormattingEnabled = true;
			this.comboBox_43.Location = new Point(6, 47);
			this.comboBox_43.Name = MainForm.getString_0(107401563);
			this.comboBox_43.Size = new Size(159, 22);
			this.comboBox_43.TabIndex = 40;
			this.groupBox_40.Controls.Add(this.comboBox_44);
			this.groupBox_40.Controls.Add(this.button_45);
			this.groupBox_40.Controls.Add(this.fastObjectListView_9);
			this.groupBox_40.Controls.Add(this.comboBox_45);
			this.groupBox_40.Controls.Add(this.comboBox_46);
			this.groupBox_40.Location = new Point(6, 120);
			this.groupBox_40.Name = MainForm.getString_0(107401506);
			this.groupBox_40.Size = new Size(491, 180);
			this.groupBox_40.TabIndex = 50;
			this.groupBox_40.TabStop = false;
			this.groupBox_40.Text = MainForm.getString_0(107401497);
			this.comboBox_44.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_44.FormattingEnabled = true;
			this.comboBox_44.Items.AddRange(new object[]
			{
				MainForm.getString_0(107402171),
				MainForm.getString_0(107402162),
				MainForm.getString_0(107401636)
			});
			this.comboBox_44.Location = new Point(409, 47);
			this.comboBox_44.Name = MainForm.getString_0(107401521);
			this.comboBox_44.Size = new Size(76, 22);
			this.comboBox_44.TabIndex = 43;
			this.button_45.Location = new Point(461, 18);
			this.button_45.Name = MainForm.getString_0(107400984);
			this.button_45.Size = new Size(24, 24);
			this.button_45.TabIndex = 42;
			this.button_45.Text = MainForm.getString_0(107389453);
			this.button_45.UseVisualStyleBackColor = true;
			this.fastObjectListView_9.AllColumns.Add(this.olvcolumn_13);
			this.fastObjectListView_9.AllColumns.Add(this.olvcolumn_14);
			this.fastObjectListView_9.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_13,
				this.olvcolumn_14
			});
			this.fastObjectListView_9.FullRowSelect = true;
			this.fastObjectListView_9.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_9.HideSelection = false;
			this.fastObjectListView_9.Location = new Point(6, 75);
			this.fastObjectListView_9.MultiSelect = false;
			this.fastObjectListView_9.Name = MainForm.getString_0(107400959);
			this.fastObjectListView_9.OwnerDraw = true;
			this.fastObjectListView_9.ShowGroups = false;
			this.fastObjectListView_9.ShowSortIndicators = false;
			this.fastObjectListView_9.Size = new Size(479, 100);
			this.fastObjectListView_9.TabIndex = 41;
			this.fastObjectListView_9.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_9.View = View.Details;
			this.fastObjectListView_9.VirtualMode = true;
			this.olvcolumn_13.AspectName = MainForm.getString_0(107402058);
			this.olvcolumn_13.CellPadding = null;
			this.olvcolumn_13.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_13.Text = MainForm.getString_0(107401497);
			this.olvcolumn_13.Width = 349;
			this.olvcolumn_14.AspectName = MainForm.getString_0(107402049);
			this.olvcolumn_14.CellPadding = null;
			this.olvcolumn_14.Text = MainForm.getString_0(107402049);
			this.olvcolumn_14.Width = 125;
			this.comboBox_45.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_45.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_45.FormattingEnabled = true;
			this.comboBox_45.Location = new Point(6, 19);
			this.comboBox_45.Name = MainForm.getString_0(107400906);
			this.comboBox_45.Size = new Size(449, 22);
			this.comboBox_45.TabIndex = 39;
			this.comboBox_45.SelectedIndexChanged += this.comboBox_45_SelectedIndexChanged;
			this.comboBox_46.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_46.FormattingEnabled = true;
			this.comboBox_46.Location = new Point(6, 47);
			this.comboBox_46.Name = MainForm.getString_0(107400913);
			this.comboBox_46.Size = new Size(159, 22);
			this.comboBox_46.TabIndex = 40;
			this.radioButton_3.AutoSize = true;
			this.radioButton_3.Location = new Point(6, 67);
			this.radioButton_3.Name = MainForm.getString_0(107400888);
			this.radioButton_3.Size = new Size(108, 18);
			this.radioButton_3.TabIndex = 49;
			this.radioButton_3.TabStop = true;
			this.radioButton_3.Text = MainForm.getString_0(107400863);
			this.radioButton_3.UseVisualStyleBackColor = true;
			this.radioButton_3.CheckedChanged += this.radioButton_4_CheckedChanged;
			this.radioButton_3.Leave += this.MainForm_Leave;
			this.radioButton_4.AutoSize = true;
			this.radioButton_4.Location = new Point(6, 43);
			this.radioButton_4.Name = MainForm.getString_0(107400810);
			this.radioButton_4.Size = new Size(107, 18);
			this.radioButton_4.TabIndex = 48;
			this.radioButton_4.TabStop = true;
			this.radioButton_4.Text = MainForm.getString_0(107400821);
			this.radioButton_4.UseVisualStyleBackColor = true;
			this.radioButton_4.CheckedChanged += this.radioButton_4_CheckedChanged;
			this.radioButton_4.Leave += this.MainForm_Leave;
			this.button_41.Enabled = false;
			this.button_41.Location = new Point(390, 40);
			this.button_41.Name = MainForm.getString_0(107400800);
			this.button_41.Size = new Size(107, 25);
			this.button_41.TabIndex = 46;
			this.button_41.Tag = MainForm.getString_0(107401671);
			this.button_41.Text = MainForm.getString_0(107401880);
			this.button_41.UseVisualStyleBackColor = true;
			this.button_41.Click += this.button_41_Click;
			this.button_42.Enabled = false;
			this.button_42.Location = new Point(390, 12);
			this.button_42.Name = MainForm.getString_0(107400739);
			this.button_42.Size = new Size(107, 25);
			this.button_42.TabIndex = 45;
			this.button_42.Text = MainForm.getString_0(107401802);
			this.button_42.UseVisualStyleBackColor = true;
			this.button_42.Click += this.button_42_Click;
			this.checkBox_36.AutoSize = true;
			this.checkBox_36.Location = new Point(6, 19);
			this.checkBox_36.Name = MainForm.getString_0(107401222);
			this.checkBox_36.Size = new Size(142, 18);
			this.checkBox_36.TabIndex = 38;
			this.checkBox_36.Text = MainForm.getString_0(107401197);
			this.checkBox_36.UseVisualStyleBackColor = true;
			this.checkBox_36.Leave += this.MainForm_Leave;
			this.numericUpDown_37.Location = new Point(112, 91);
			NumericUpDown numericUpDown188 = this.numericUpDown_37;
			int[] array188 = new int[4];
			array188[0] = 20000;
			numericUpDown188.Maximum = new decimal(array188);
			this.numericUpDown_37.Name = MainForm.getString_0(107401164);
			this.numericUpDown_37.Size = new Size(53, 20);
			this.numericUpDown_37.TabIndex = 37;
			this.numericUpDown_37.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown189 = this.numericUpDown_37;
			int[] array189 = new int[4];
			array189[0] = 1;
			numericUpDown189.Value = new decimal(array189);
			this.numericUpDown_37.Leave += this.MainForm_Leave;
			this.label_115.AutoSize = true;
			this.label_115.Location = new Point(3, 94);
			this.label_115.Name = MainForm.getString_0(107401171);
			this.label_115.Size = new Size(103, 14);
			this.label_115.TabIndex = 36;
			this.label_115.Text = MainForm.getString_0(107401126);
			this.button_43.Enabled = false;
			this.button_43.Location = new Point(418, 89);
			this.button_43.Name = MainForm.getString_0(107401101);
			this.button_43.Size = new Size(79, 25);
			this.button_43.TabIndex = 0;
			this.button_43.Text = MainForm.getString_0(107401705);
			this.button_43.UseVisualStyleBackColor = true;
			this.button_43.Click += this.button_43_Click;
			this.tabPage_48.Controls.Add(this.groupBox_59);
			this.tabPage_48.Location = new Point(4, 23);
			this.tabPage_48.Name = MainForm.getString_0(107401072);
			this.tabPage_48.Size = new Size(509, 515);
			this.tabPage_48.TabIndex = 11;
			this.tabPage_48.Text = MainForm.getString_0(107401083);
			this.tabPage_48.UseVisualStyleBackColor = true;
			this.groupBox_59.Controls.Add(this.comboBox_62);
			this.groupBox_59.Controls.Add(this.label_153);
			this.groupBox_59.Controls.Add(this.button_76);
			this.groupBox_59.Controls.Add(this.button_77);
			this.groupBox_59.Controls.Add(this.label_154);
			this.groupBox_59.Controls.Add(this.comboBox_63);
			this.groupBox_59.Location = new Point(3, 3);
			this.groupBox_59.Name = MainForm.getString_0(107401074);
			this.groupBox_59.Size = new Size(503, 71);
			this.groupBox_59.TabIndex = 0;
			this.groupBox_59.TabStop = false;
			this.groupBox_59.Text = MainForm.getString_0(107388377);
			this.comboBox_62.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_62.FormattingEnabled = true;
			this.comboBox_62.Items.AddRange(new object[]
			{
				MainForm.getString_0(107396375),
				MainForm.getString_0(107405781),
				MainForm.getString_0(107405756),
				MainForm.getString_0(107382673),
				MainForm.getString_0(107401025),
				MainForm.getString_0(107401000),
				MainForm.getString_0(107400459),
				MainForm.getString_0(107400470),
				MainForm.getString_0(107400445),
				MainForm.getString_0(107400388),
				MainForm.getString_0(107400363),
				MainForm.getString_0(107400370),
				MainForm.getString_0(107400345)
			});
			this.comboBox_62.Location = new Point(6, 42);
			this.comboBox_62.Name = MainForm.getString_0(107400320);
			this.comboBox_62.Size = new Size(164, 22);
			this.comboBox_62.TabIndex = 25;
			this.comboBox_62.Leave += this.MainForm_Leave;
			this.label_153.AutoSize = true;
			this.label_153.Location = new Point(6, 22);
			this.label_153.Name = MainForm.getString_0(107400259);
			this.label_153.Size = new Size(134, 14);
			this.label_153.TabIndex = 26;
			this.label_153.Text = MainForm.getString_0(107400278);
			this.button_76.Location = new Point(300, 42);
			this.button_76.Name = MainForm.getString_0(107400245);
			this.button_76.Size = new Size(123, 25);
			this.button_76.TabIndex = 23;
			this.button_76.Text = MainForm.getString_0(107385211);
			this.button_76.UseVisualStyleBackColor = true;
			this.button_76.Click += this.button_75_Click;
			this.button_77.Location = new Point(428, 42);
			this.button_77.Name = MainForm.getString_0(107400712);
			this.button_77.Size = new Size(69, 25);
			this.button_77.TabIndex = 24;
			this.button_77.Text = MainForm.getString_0(107401705);
			this.button_77.UseVisualStyleBackColor = true;
			this.button_77.Click += this.button_77_Click;
			this.label_154.AutoSize = true;
			this.label_154.Location = new Point(270, 17);
			this.label_154.Name = MainForm.getString_0(107400687);
			this.label_154.Size = new Size(63, 14);
			this.label_154.TabIndex = 22;
			this.label_154.Text = MainForm.getString_0(107400674);
			this.comboBox_63.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_63.FormattingEnabled = true;
			this.comboBox_63.Location = new Point(337, 14);
			this.comboBox_63.Name = MainForm.getString_0(107400689);
			this.comboBox_63.Size = new Size(160, 22);
			this.comboBox_63.TabIndex = 21;
			this.comboBox_63.SelectedIndexChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_63.Leave += this.MainForm_Leave;
			this.tabPage_40.Controls.Add(this.groupBox_41);
			this.tabPage_40.Location = new Point(4, 23);
			this.tabPage_40.Name = MainForm.getString_0(107400664);
			this.tabPage_40.Size = new Size(509, 515);
			this.tabPage_40.TabIndex = 9;
			this.tabPage_40.Text = MainForm.getString_0(107400639);
			this.tabPage_40.UseVisualStyleBackColor = true;
			this.groupBox_41.Controls.Add(this.numericUpDown_38);
			this.groupBox_41.Controls.Add(this.label_116);
			this.groupBox_41.Controls.Add(this.button_46);
			this.groupBox_41.Location = new Point(3, 3);
			this.groupBox_41.Name = MainForm.getString_0(107400630);
			this.groupBox_41.Size = new Size(503, 80);
			this.groupBox_41.TabIndex = 0;
			this.groupBox_41.TabStop = false;
			this.groupBox_41.Text = MainForm.getString_0(107388377);
			this.numericUpDown_38.Location = new Point(444, 20);
			NumericUpDown numericUpDown190 = this.numericUpDown_38;
			int[] array190 = new int[4];
			array190[0] = 20000;
			numericUpDown190.Maximum = new decimal(array190);
			this.numericUpDown_38.Name = MainForm.getString_0(107400581);
			this.numericUpDown_38.Size = new Size(53, 20);
			this.numericUpDown_38.TabIndex = 50;
			this.numericUpDown_38.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown191 = this.numericUpDown_38;
			int[] array191 = new int[4];
			array191[0] = 1;
			numericUpDown191.Value = new decimal(array191);
			this.label_116.AutoSize = true;
			this.label_116.Location = new Point(362, 23);
			this.label_116.Name = MainForm.getString_0(107400560);
			this.label_116.Size = new Size(82, 14);
			this.label_116.TabIndex = 49;
			this.label_116.Text = MainForm.getString_0(107400547);
			this.button_46.Enabled = false;
			this.button_46.Location = new Point(418, 46);
			this.button_46.Name = MainForm.getString_0(107400526);
			this.button_46.Size = new Size(79, 25);
			this.button_46.TabIndex = 48;
			this.button_46.Text = MainForm.getString_0(107401705);
			this.button_46.UseVisualStyleBackColor = true;
			this.button_46.Click += this.button_46_Click;
			this.tabPage_41.Controls.Add(this.groupBox_44);
			this.tabPage_41.Location = new Point(4, 23);
			this.tabPage_41.Name = MainForm.getString_0(107400493);
			this.tabPage_41.Size = new Size(509, 515);
			this.tabPage_41.TabIndex = 10;
			this.tabPage_41.Text = MainForm.getString_0(107400504);
			this.tabPage_41.UseVisualStyleBackColor = true;
			this.groupBox_44.Controls.Add(this.numericUpDown_39);
			this.groupBox_44.Controls.Add(this.label_117);
			this.groupBox_44.Controls.Add(this.numericUpDown_40);
			this.groupBox_44.Controls.Add(this.label_118);
			this.groupBox_44.Controls.Add(this.numericUpDown_41);
			this.groupBox_44.Controls.Add(this.label_119);
			this.groupBox_44.Controls.Add(this.numericUpDown_42);
			this.groupBox_44.Controls.Add(this.label_120);
			this.groupBox_44.Controls.Add(this.button_51);
			this.groupBox_44.Location = new Point(3, 3);
			this.groupBox_44.Name = MainForm.getString_0(107399947);
			this.groupBox_44.Size = new Size(503, 102);
			this.groupBox_44.TabIndex = 1;
			this.groupBox_44.TabStop = false;
			this.groupBox_44.Text = MainForm.getString_0(107388377);
			this.numericUpDown_39.Location = new Point(122, 39);
			NumericUpDown numericUpDown192 = this.numericUpDown_39;
			int[] array192 = new int[4];
			array192[0] = 6;
			numericUpDown192.Maximum = new decimal(array192);
			NumericUpDown numericUpDown193 = this.numericUpDown_39;
			int[] array193 = new int[4];
			array193[0] = 1;
			numericUpDown193.Minimum = new decimal(array193);
			this.numericUpDown_39.Name = MainForm.getString_0(107399962);
			this.numericUpDown_39.Size = new Size(53, 20);
			this.numericUpDown_39.TabIndex = 57;
			this.numericUpDown_39.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown194 = this.numericUpDown_39;
			int[] array194 = new int[4];
			array194[0] = 1;
			numericUpDown194.Value = new decimal(array194);
			this.label_117.AutoSize = true;
			this.label_117.Location = new Point(6, 42);
			this.label_117.Name = MainForm.getString_0(107399905);
			this.label_117.Size = new Size(96, 14);
			this.label_117.TabIndex = 56;
			this.label_117.Text = MainForm.getString_0(107399924);
			this.numericUpDown_40.Location = new Point(122, 13);
			NumericUpDown numericUpDown195 = this.numericUpDown_40;
			int[] array195 = new int[4];
			array195[0] = 6;
			numericUpDown195.Maximum = new decimal(array195);
			NumericUpDown numericUpDown196 = this.numericUpDown_40;
			int[] array196 = new int[4];
			array196[0] = 1;
			numericUpDown196.Minimum = new decimal(array196);
			this.numericUpDown_40.Name = MainForm.getString_0(107399903);
			this.numericUpDown_40.Size = new Size(53, 20);
			this.numericUpDown_40.TabIndex = 55;
			this.numericUpDown_40.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown197 = this.numericUpDown_40;
			int[] array197 = new int[4];
			array197[0] = 1;
			numericUpDown197.Value = new decimal(array197);
			this.label_118.AutoSize = true;
			this.label_118.Location = new Point(6, 16);
			this.label_118.Name = MainForm.getString_0(107399842);
			this.label_118.Size = new Size(110, 14);
			this.label_118.TabIndex = 54;
			this.label_118.Text = MainForm.getString_0(107399861);
			this.numericUpDown_41.Location = new Point(444, 39);
			NumericUpDown numericUpDown198 = this.numericUpDown_41;
			int[] array198 = new int[4];
			array198[0] = 20000;
			numericUpDown198.Maximum = new decimal(array198);
			this.numericUpDown_41.Name = MainForm.getString_0(107399836);
			this.numericUpDown_41.Size = new Size(53, 20);
			this.numericUpDown_41.TabIndex = 53;
			this.numericUpDown_41.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown199 = this.numericUpDown_41;
			int[] array199 = new int[4];
			array199[0] = 1;
			numericUpDown199.Value = new decimal(array199);
			this.label_119.AutoSize = true;
			this.label_119.Location = new Point(357, 41);
			this.label_119.Name = MainForm.getString_0(107399807);
			this.label_119.Size = new Size(85, 14);
			this.label_119.TabIndex = 52;
			this.label_119.Text = MainForm.getString_0(107399794);
			this.numericUpDown_42.Location = new Point(444, 13);
			NumericUpDown numericUpDown200 = this.numericUpDown_42;
			int[] array200 = new int[4];
			array200[0] = 20000;
			numericUpDown200.Maximum = new decimal(array200);
			this.numericUpDown_42.Name = MainForm.getString_0(107399773);
			this.numericUpDown_42.Size = new Size(53, 20);
			this.numericUpDown_42.TabIndex = 50;
			this.numericUpDown_42.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown201 = this.numericUpDown_42;
			int[] array201 = new int[4];
			array201[0] = 1;
			numericUpDown201.Value = new decimal(array201);
			this.label_120.AutoSize = true;
			this.label_120.Location = new Point(346, 16);
			this.label_120.Name = MainForm.getString_0(107399740);
			this.label_120.Size = new Size(96, 14);
			this.label_120.TabIndex = 49;
			this.label_120.Text = MainForm.getString_0(107400207);
			this.button_51.Enabled = false;
			this.button_51.Location = new Point(418, 73);
			this.button_51.Name = MainForm.getString_0(107400214);
			this.button_51.Size = new Size(79, 25);
			this.button_51.TabIndex = 48;
			this.button_51.Text = MainForm.getString_0(107401705);
			this.button_51.UseVisualStyleBackColor = true;
			this.button_51.Click += this.button_51_Click;
			this.tabPage_20.Controls.Add(this.groupBox_48);
			this.tabPage_20.Controls.Add(this.groupBox_49);
			this.tabPage_20.Controls.Add(this.groupBox_16);
			this.tabPage_20.Location = new Point(4, 23);
			this.tabPage_20.Name = MainForm.getString_0(107400185);
			this.tabPage_20.Padding = new Padding(3);
			this.tabPage_20.Size = new Size(509, 515);
			this.tabPage_20.TabIndex = 2;
			this.tabPage_20.Text = MainForm.getString_0(107400132);
			this.tabPage_20.UseVisualStyleBackColor = true;
			this.groupBox_48.Controls.Add(this.comboBox_55);
			this.groupBox_48.Controls.Add(this.button_57);
			this.groupBox_48.Controls.Add(this.fastObjectListView_13);
			this.groupBox_48.Controls.Add(this.comboBox_53);
			this.groupBox_48.Location = new Point(6, 338);
			this.groupBox_48.Name = MainForm.getString_0(107400155);
			this.groupBox_48.Size = new Size(497, 168);
			this.groupBox_48.TabIndex = 46;
			this.groupBox_48.TabStop = false;
			this.groupBox_48.Text = MainForm.getString_0(107400106);
			this.comboBox_55.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_55.FormattingEnabled = true;
			this.comboBox_55.Items.AddRange(new object[]
			{
				MainForm.getString_0(107402171),
				MainForm.getString_0(107402162),
				MainForm.getString_0(107401636),
				MainForm.getString_0(107400121),
				MainForm.getString_0(107400080)
			});
			this.comboBox_55.Location = new Point(428, 19);
			this.comboBox_55.Name = MainForm.getString_0(107400071);
			this.comboBox_55.Size = new Size(63, 22);
			this.comboBox_55.TabIndex = 45;
			this.comboBox_55.Leave += this.MainForm_Leave;
			this.button_57.Location = new Point(401, 18);
			this.button_57.Name = MainForm.getString_0(107400046);
			this.button_57.Size = new Size(24, 24);
			this.button_57.TabIndex = 43;
			this.button_57.Text = MainForm.getString_0(107389453);
			this.button_57.UseVisualStyleBackColor = true;
			this.fastObjectListView_13.AllColumns.Add(this.olvcolumn_18);
			this.fastObjectListView_13.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_18
			});
			this.fastObjectListView_13.FullRowSelect = true;
			this.fastObjectListView_13.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_13.HideSelection = false;
			this.fastObjectListView_13.Location = new Point(6, 47);
			this.fastObjectListView_13.MultiSelect = false;
			this.fastObjectListView_13.Name = MainForm.getString_0(107400061);
			this.fastObjectListView_13.OwnerDraw = true;
			this.fastObjectListView_13.ShowGroups = false;
			this.fastObjectListView_13.ShowSortIndicators = false;
			this.fastObjectListView_13.Size = new Size(485, 115);
			this.fastObjectListView_13.TabIndex = 42;
			this.fastObjectListView_13.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_13.View = View.Details;
			this.fastObjectListView_13.VirtualMode = true;
			this.olvcolumn_18.AspectName = MainForm.getString_0(107402058);
			this.olvcolumn_18.CellPadding = null;
			this.olvcolumn_18.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_18.Text = MainForm.getString_0(107400004);
			this.olvcolumn_18.Width = 480;
			this.comboBox_53.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_53.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_53.FormattingEnabled = true;
			this.comboBox_53.Location = new Point(6, 19);
			this.comboBox_53.Name = MainForm.getString_0(107400031);
			this.comboBox_53.Size = new Size(392, 22);
			this.comboBox_53.TabIndex = 39;
			this.groupBox_49.Controls.Add(this.button_58);
			this.groupBox_49.Controls.Add(this.fastObjectListView_14);
			this.groupBox_49.Controls.Add(this.comboBox_54);
			this.groupBox_49.Location = new Point(6, 164);
			this.groupBox_49.Name = MainForm.getString_0(107399974);
			this.groupBox_49.Size = new Size(497, 168);
			this.groupBox_49.TabIndex = 45;
			this.groupBox_49.TabStop = false;
			this.groupBox_49.Text = MainForm.getString_0(107399989);
			this.button_58.Location = new Point(469, 18);
			this.button_58.Name = MainForm.getString_0(107399456);
			this.button_58.Size = new Size(24, 24);
			this.button_58.TabIndex = 42;
			this.button_58.Text = MainForm.getString_0(107389453);
			this.button_58.UseVisualStyleBackColor = true;
			this.fastObjectListView_14.AllColumns.Add(this.olvcolumn_19);
			this.fastObjectListView_14.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_19
			});
			this.fastObjectListView_14.FullRowSelect = true;
			this.fastObjectListView_14.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_14.HideSelection = false;
			this.fastObjectListView_14.Location = new Point(6, 47);
			this.fastObjectListView_14.MultiSelect = false;
			this.fastObjectListView_14.Name = MainForm.getString_0(107399403);
			this.fastObjectListView_14.OwnerDraw = true;
			this.fastObjectListView_14.ShowGroups = false;
			this.fastObjectListView_14.ShowSortIndicators = false;
			this.fastObjectListView_14.Size = new Size(485, 104);
			this.fastObjectListView_14.TabIndex = 41;
			this.fastObjectListView_14.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_14.View = View.Details;
			this.fastObjectListView_14.VirtualMode = true;
			this.olvcolumn_19.AspectName = MainForm.getString_0(107402058);
			this.olvcolumn_19.CellPadding = null;
			this.olvcolumn_19.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_19.Text = MainForm.getString_0(107400004);
			this.olvcolumn_19.Width = 480;
			this.comboBox_54.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_54.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_54.FormattingEnabled = true;
			this.comboBox_54.Location = new Point(6, 19);
			this.comboBox_54.Name = MainForm.getString_0(107399410);
			this.comboBox_54.Size = new Size(459, 22);
			this.comboBox_54.TabIndex = 39;
			this.groupBox_16.Controls.Add(this.radioButton_7);
			this.groupBox_16.Controls.Add(this.checkBox_48);
			this.groupBox_16.Controls.Add(this.numericUpDown_49);
			this.groupBox_16.Controls.Add(this.label_129);
			this.groupBox_16.Controls.Add(this.numericUpDown_50);
			this.groupBox_16.Controls.Add(this.label_130);
			this.groupBox_16.Controls.Add(this.numericUpDown_51);
			this.groupBox_16.Controls.Add(this.label_131);
			this.groupBox_16.Controls.Add(this.checkBox_44);
			this.groupBox_16.Controls.Add(this.radioButton_5);
			this.groupBox_16.Controls.Add(this.radioButton_6);
			this.groupBox_16.Controls.Add(this.numericUpDown_46);
			this.groupBox_16.Controls.Add(this.label_75);
			this.groupBox_16.Controls.Add(this.button_8);
			this.groupBox_16.Controls.Add(this.button_9);
			this.groupBox_16.Controls.Add(this.label_72);
			this.groupBox_16.Controls.Add(this.comboBox_17);
			this.groupBox_16.Controls.Add(this.numericUpDown_48);
			this.groupBox_16.Controls.Add(this.label_73);
			this.groupBox_16.Controls.Add(this.numericUpDown_47);
			this.groupBox_16.Controls.Add(this.label_74);
			this.groupBox_16.Location = new Point(6, 6);
			this.groupBox_16.Name = MainForm.getString_0(107399381);
			this.groupBox_16.Size = new Size(497, 152);
			this.groupBox_16.TabIndex = 0;
			this.groupBox_16.TabStop = false;
			this.groupBox_16.Text = MainForm.getString_0(107399352);
			this.radioButton_7.AutoSize = true;
			this.radioButton_7.Location = new Point(192, 62);
			this.radioButton_7.Name = MainForm.getString_0(107399323);
			this.radioButton_7.Size = new Size(125, 18);
			this.radioButton_7.TabIndex = 37;
			this.radioButton_7.TabStop = true;
			this.radioButton_7.Text = MainForm.getString_0(107399282);
			this.radioButton_7.UseVisualStyleBackColor = true;
			this.radioButton_7.CheckedChanged += this.radioButton_6_CheckedChanged;
			this.radioButton_7.Leave += this.MainForm_Leave;
			this.checkBox_48.AutoSize = true;
			this.checkBox_48.Location = new Point(9, 96);
			this.checkBox_48.Name = MainForm.getString_0(107399257);
			this.checkBox_48.Size = new Size(222, 18);
			this.checkBox_48.TabIndex = 36;
			this.checkBox_48.Text = MainForm.getString_0(107399232);
			this.checkBox_48.UseVisualStyleBackColor = true;
			this.checkBox_48.Leave += this.MainForm_Leave;
			NumericUpDown numericUpDown202 = this.numericUpDown_49;
			int[] array202 = new int[4];
			array202[0] = 50;
			numericUpDown202.Increment = new decimal(array202);
			this.numericUpDown_49.Location = new Point(436, 12);
			NumericUpDown numericUpDown203 = this.numericUpDown_49;
			int[] array203 = new int[4];
			array203[0] = 10000;
			numericUpDown203.Maximum = new decimal(array203);
			this.numericUpDown_49.Name = MainForm.getString_0(107399699);
			this.numericUpDown_49.Size = new Size(53, 20);
			this.numericUpDown_49.TabIndex = 35;
			this.numericUpDown_49.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown204 = this.numericUpDown_49;
			int[] array204 = new int[4];
			array204[0] = 1;
			numericUpDown204.Value = new decimal(array204);
			this.numericUpDown_49.Leave += this.MainForm_Leave;
			this.label_129.AutoSize = true;
			this.label_129.Location = new Point(326, 14);
			this.label_129.Name = MainForm.getString_0(107399674);
			this.label_129.Size = new Size(73, 14);
			this.label_129.TabIndex = 34;
			this.label_129.Text = MainForm.getString_0(107399629);
			NumericUpDown numericUpDown205 = this.numericUpDown_50;
			int[] array205 = new int[4];
			array205[0] = 50;
			numericUpDown205.Increment = new decimal(array205);
			this.numericUpDown_50.Location = new Point(436, 64);
			NumericUpDown numericUpDown206 = this.numericUpDown_50;
			int[] array206 = new int[4];
			array206[0] = 10000;
			numericUpDown206.Maximum = new decimal(array206);
			this.numericUpDown_50.Name = MainForm.getString_0(107399644);
			this.numericUpDown_50.Size = new Size(53, 20);
			this.numericUpDown_50.TabIndex = 33;
			this.numericUpDown_50.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown207 = this.numericUpDown_50;
			int[] array207 = new int[4];
			array207[0] = 1;
			numericUpDown207.Value = new decimal(array207);
			this.numericUpDown_50.Leave += this.MainForm_Leave;
			this.label_130.AutoSize = true;
			this.label_130.Location = new Point(326, 66);
			this.label_130.Name = MainForm.getString_0(107399587);
			this.label_130.Size = new Size(99, 14);
			this.label_130.TabIndex = 32;
			this.label_130.Text = MainForm.getString_0(107399606);
			NumericUpDown numericUpDown208 = this.numericUpDown_51;
			int[] array208 = new int[4];
			array208[0] = 50;
			numericUpDown208.Increment = new decimal(array208);
			this.numericUpDown_51.Location = new Point(436, 38);
			NumericUpDown numericUpDown209 = this.numericUpDown_51;
			int[] array209 = new int[4];
			array209[0] = 10000;
			numericUpDown209.Maximum = new decimal(array209);
			this.numericUpDown_51.Name = MainForm.getString_0(107399581);
			this.numericUpDown_51.Size = new Size(53, 20);
			this.numericUpDown_51.TabIndex = 31;
			this.numericUpDown_51.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown210 = this.numericUpDown_51;
			int[] array210 = new int[4];
			array210[0] = 1;
			numericUpDown210.Value = new decimal(array210);
			this.numericUpDown_51.Leave += this.MainForm_Leave;
			this.label_131.AutoSize = true;
			this.label_131.Location = new Point(326, 40);
			this.label_131.Name = MainForm.getString_0(107399528);
			this.label_131.Size = new Size(109, 14);
			this.label_131.TabIndex = 30;
			this.label_131.Text = MainForm.getString_0(107399547);
			this.checkBox_44.AutoSize = true;
			this.checkBox_44.Location = new Point(9, 120);
			this.checkBox_44.Name = MainForm.getString_0(107399518);
			this.checkBox_44.Size = new Size(107, 18);
			this.checkBox_44.TabIndex = 28;
			this.checkBox_44.Text = MainForm.getString_0(107399469);
			this.checkBox_44.UseVisualStyleBackColor = true;
			this.checkBox_44.Leave += this.MainForm_Leave;
			this.radioButton_5.AutoSize = true;
			this.radioButton_5.Location = new Point(192, 14);
			this.radioButton_5.Name = MainForm.getString_0(107399480);
			this.radioButton_5.Size = new Size(107, 18);
			this.radioButton_5.TabIndex = 27;
			this.radioButton_5.TabStop = true;
			this.radioButton_5.Text = MainForm.getString_0(107400821);
			this.radioButton_5.UseVisualStyleBackColor = true;
			this.radioButton_5.CheckedChanged += this.radioButton_6_CheckedChanged;
			this.radioButton_5.Leave += this.MainForm_Leave;
			this.radioButton_6.AutoSize = true;
			this.radioButton_6.Location = new Point(192, 38);
			this.radioButton_6.Name = MainForm.getString_0(107398939);
			this.radioButton_6.Size = new Size(108, 18);
			this.radioButton_6.TabIndex = 26;
			this.radioButton_6.TabStop = true;
			this.radioButton_6.Text = MainForm.getString_0(107400863);
			this.radioButton_6.UseVisualStyleBackColor = true;
			this.radioButton_6.CheckedChanged += this.radioButton_6_CheckedChanged;
			this.radioButton_6.Leave += this.MainForm_Leave;
			this.numericUpDown_46.Location = new Point(122, 14);
			NumericUpDown numericUpDown211 = this.numericUpDown_46;
			int[] array211 = new int[4];
			array211[0] = 20;
			numericUpDown211.Maximum = new decimal(array211);
			this.numericUpDown_46.Name = MainForm.getString_0(107398902);
			this.numericUpDown_46.Size = new Size(53, 20);
			this.numericUpDown_46.TabIndex = 25;
			this.numericUpDown_46.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown212 = this.numericUpDown_46;
			int[] array212 = new int[4];
			array212[0] = 1;
			numericUpDown212.Value = new decimal(array212);
			this.numericUpDown_46.Leave += this.MainForm_Leave;
			this.label_75.AutoSize = true;
			this.label_75.Location = new Point(6, 16);
			this.label_75.Name = MainForm.getString_0(107398877);
			this.label_75.Size = new Size(102, 14);
			this.label_75.TabIndex = 24;
			this.label_75.Text = MainForm.getString_0(107398832);
			this.button_8.Location = new Point(292, 120);
			this.button_8.Name = MainForm.getString_0(107398839);
			this.button_8.Size = new Size(123, 25);
			this.button_8.TabIndex = 19;
			this.button_8.Text = MainForm.getString_0(107385211);
			this.button_8.UseVisualStyleBackColor = true;
			this.button_8.Click += this.button_75_Click;
			this.button_9.Location = new Point(420, 120);
			this.button_9.Name = MainForm.getString_0(107398794);
			this.button_9.Size = new Size(69, 25);
			this.button_9.TabIndex = 20;
			this.button_9.Text = MainForm.getString_0(107401705);
			this.button_9.UseVisualStyleBackColor = true;
			this.button_9.Click += this.button_9_Click;
			this.label_72.AutoSize = true;
			this.label_72.Location = new Point(310, 94);
			this.label_72.Name = MainForm.getString_0(107398813);
			this.label_72.Size = new Size(63, 14);
			this.label_72.TabIndex = 12;
			this.label_72.Text = MainForm.getString_0(107400674);
			this.comboBox_17.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_17.FormattingEnabled = true;
			this.comboBox_17.Location = new Point(379, 92);
			this.comboBox_17.Name = MainForm.getString_0(107398768);
			this.comboBox_17.Size = new Size(110, 22);
			this.comboBox_17.TabIndex = 11;
			this.comboBox_17.SelectedIndexChanged += this.comboBox_17_SelectedIndexChanged;
			this.comboBox_17.Leave += this.MainForm_Leave;
			this.numericUpDown_48.Location = new Point(122, 66);
			NumericUpDown numericUpDown213 = this.numericUpDown_48;
			int[] array213 = new int[4];
			array213[0] = 50;
			numericUpDown213.Maximum = new decimal(array213);
			this.numericUpDown_48.Name = MainForm.getString_0(107398775);
			this.numericUpDown_48.Size = new Size(53, 20);
			this.numericUpDown_48.TabIndex = 6;
			this.numericUpDown_48.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown214 = this.numericUpDown_48;
			int[] array214 = new int[4];
			array214[0] = 1;
			numericUpDown214.Value = new decimal(array214);
			this.numericUpDown_48.Leave += this.MainForm_Leave;
			this.label_73.AutoSize = true;
			this.label_73.Location = new Point(6, 68);
			this.label_73.Name = MainForm.getString_0(107398750);
			this.label_73.Size = new Size(114, 14);
			this.label_73.TabIndex = 5;
			this.label_73.Text = MainForm.getString_0(107398737);
			this.numericUpDown_47.Location = new Point(122, 40);
			NumericUpDown numericUpDown215 = this.numericUpDown_47;
			int[] array215 = new int[4];
			array215[0] = 200;
			numericUpDown215.Maximum = new decimal(array215);
			this.numericUpDown_47.Name = MainForm.getString_0(107398712);
			this.numericUpDown_47.Size = new Size(53, 20);
			this.numericUpDown_47.TabIndex = 4;
			this.numericUpDown_47.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown216 = this.numericUpDown_47;
			int[] array216 = new int[4];
			array216[0] = 1;
			numericUpDown216.Value = new decimal(array216);
			this.numericUpDown_47.Leave += this.MainForm_Leave;
			this.label_74.AutoSize = true;
			this.label_74.Location = new Point(6, 42);
			this.label_74.Name = MainForm.getString_0(107399199);
			this.label_74.Size = new Size(110, 14);
			this.label_74.TabIndex = 3;
			this.label_74.Text = MainForm.getString_0(107399186);
			this.tabPage_38.Location = new Point(4, 23);
			this.tabPage_38.Name = MainForm.getString_0(107399161);
			this.tabPage_38.Size = new Size(509, 515);
			this.tabPage_38.TabIndex = 7;
			this.tabPage_38.Text = MainForm.getString_0(107399136);
			this.tabPage_38.UseVisualStyleBackColor = true;
			this.tabPage_39.Location = new Point(4, 23);
			this.tabPage_39.Name = MainForm.getString_0(107399123);
			this.tabPage_39.Size = new Size(509, 515);
			this.tabPage_39.TabIndex = 8;
			this.tabPage_39.Text = MainForm.getString_0(107399098);
			this.tabPage_39.UseVisualStyleBackColor = true;
			this.tabPage_55.Controls.Add(this.tabControl_10);
			this.tabPage_55.Location = new Point(4, 42);
			this.tabPage_55.Name = MainForm.getString_0(107399089);
			this.tabPage_55.Size = new Size(520, 543);
			this.tabPage_55.TabIndex = 16;
			this.tabPage_55.Text = MainForm.getString_0(107399068);
			this.tabPage_55.UseVisualStyleBackColor = true;
			this.tabControl_10.Controls.Add(this.tabPage_56);
			this.tabControl_10.Location = new Point(3, 3);
			this.tabControl_10.Name = MainForm.getString_0(107399019);
			this.tabControl_10.SelectedIndex = 0;
			this.tabControl_10.Size = new Size(514, 534);
			this.tabControl_10.TabIndex = 0;
			this.tabPage_56.Controls.Add(this.groupBox_70);
			this.tabPage_56.Controls.Add(this.groupBox_71);
			this.tabPage_56.Location = new Point(4, 23);
			this.tabPage_56.Name = MainForm.getString_0(107399034);
			this.tabPage_56.Padding = new Padding(3);
			this.tabPage_56.Size = new Size(506, 507);
			this.tabPage_56.TabIndex = 0;
			this.tabPage_56.Text = MainForm.getString_0(107398989);
			this.tabPage_56.UseVisualStyleBackColor = true;
			this.groupBox_70.Controls.Add(this.comboBox_75);
			this.groupBox_70.Controls.Add(this.label_190);
			this.groupBox_70.Controls.Add(this.button_97);
			this.groupBox_70.Controls.Add(this.fastObjectListView_23);
			this.groupBox_70.Controls.Add(this.label_186);
			this.groupBox_70.Controls.Add(this.numericUpDown_76);
			this.groupBox_70.Controls.Add(this.label_187);
			this.groupBox_70.Controls.Add(this.comboBox_73);
			this.groupBox_70.Controls.Add(this.label_188);
			this.groupBox_70.Controls.Add(this.button_98);
			this.groupBox_70.Controls.Add(this.button_99);
			this.groupBox_70.Location = new Point(3, 216);
			this.groupBox_70.Name = MainForm.getString_0(107399008);
			this.groupBox_70.Size = new Size(506, 207);
			this.groupBox_70.TabIndex = 61;
			this.groupBox_70.TabStop = false;
			this.groupBox_70.Text = MainForm.getString_0(107398959);
			this.comboBox_75.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_75.FormattingEnabled = true;
			this.comboBox_75.Items.AddRange(new object[]
			{
				MainForm.getString_0(107385330),
				MainForm.getString_0(107398970),
				MainForm.getString_0(107393308)
			});
			this.comboBox_75.Location = new Point(6, 150);
			this.comboBox_75.Name = MainForm.getString_0(107398961);
			this.comboBox_75.Size = new Size(209, 22);
			this.comboBox_75.TabIndex = 60;
			this.label_190.AutoSize = true;
			this.label_190.Location = new Point(3, 133);
			this.label_190.Name = MainForm.getString_0(107398384);
			this.label_190.Size = new Size(137, 14);
			this.label_190.TabIndex = 61;
			this.label_190.Text = MainForm.getString_0(107398371);
			this.button_97.Location = new Point(6, 64);
			this.button_97.Name = MainForm.getString_0(107398338);
			this.button_97.Size = new Size(69, 20);
			this.button_97.TabIndex = 18;
			this.button_97.Text = MainForm.getString_0(107385117);
			this.button_97.UseVisualStyleBackColor = true;
			this.fastObjectListView_23.AllColumns.Add(this.olvcolumn_29);
			this.fastObjectListView_23.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_29
			});
			this.fastObjectListView_23.FullRowSelect = true;
			this.fastObjectListView_23.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_23.HideSelection = false;
			this.fastObjectListView_23.Location = new Point(276, 13);
			this.fastObjectListView_23.MultiSelect = false;
			this.fastObjectListView_23.Name = MainForm.getString_0(107398313);
			this.fastObjectListView_23.OwnerDraw = true;
			this.fastObjectListView_23.ShowGroups = false;
			this.fastObjectListView_23.Size = new Size(224, 188);
			this.fastObjectListView_23.TabIndex = 59;
			this.fastObjectListView_23.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_23.View = View.Details;
			this.fastObjectListView_23.VirtualMode = true;
			this.olvcolumn_29.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_29.CellPadding = null;
			this.olvcolumn_29.FillsFreeSpace = true;
			this.olvcolumn_29.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_29.Text = MainForm.getString_0(107384059);
			this.olvcolumn_29.Width = 250;
			this.label_186.AutoSize = true;
			this.label_186.Location = new Point(171, 103);
			this.label_186.Name = MainForm.getString_0(107398324);
			this.label_186.Size = new Size(33, 14);
			this.label_186.TabIndex = 56;
			this.label_186.Text = MainForm.getString_0(107398279);
			NumericUpDown numericUpDown217 = this.numericUpDown_76;
			int[] array217 = new int[4];
			array217[0] = 5;
			numericUpDown217.Increment = new decimal(array217);
			this.numericUpDown_76.Location = new Point(113, 101);
			NumericUpDown numericUpDown218 = this.numericUpDown_76;
			int[] array218 = new int[4];
			array218[0] = 1000;
			numericUpDown218.Maximum = new decimal(array218);
			NumericUpDown numericUpDown219 = this.numericUpDown_76;
			int[] array219 = new int[4];
			array219[0] = 50;
			numericUpDown219.Minimum = new decimal(array219);
			this.numericUpDown_76.Name = MainForm.getString_0(107398302);
			this.numericUpDown_76.Size = new Size(52, 20);
			this.numericUpDown_76.TabIndex = 55;
			this.numericUpDown_76.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown220 = this.numericUpDown_76;
			int[] array220 = new int[4];
			array220[0] = 100;
			numericUpDown220.Value = new decimal(array220);
			this.label_187.AutoSize = true;
			this.label_187.BackColor = Color.Transparent;
			this.label_187.Location = new Point(3, 103);
			this.label_187.Name = MainForm.getString_0(107398249);
			this.label_187.Size = new Size(107, 14);
			this.label_187.TabIndex = 54;
			this.label_187.Text = MainForm.getString_0(107398268);
			this.comboBox_73.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_73.FormattingEnabled = true;
			this.comboBox_73.Location = new Point(6, 36);
			this.comboBox_73.Name = MainForm.getString_0(107398211);
			this.comboBox_73.Size = new Size(209, 22);
			this.comboBox_73.TabIndex = 14;
			this.label_188.AutoSize = true;
			this.label_188.Location = new Point(3, 19);
			this.label_188.Name = MainForm.getString_0(107398190);
			this.label_188.Size = new Size(155, 14);
			this.label_188.TabIndex = 15;
			this.label_188.Text = MainForm.getString_0(107398177);
			this.button_98.Location = new Point(6, 178);
			this.button_98.Name = MainForm.getString_0(107398684);
			this.button_98.Size = new Size(123, 23);
			this.button_98.TabIndex = 16;
			this.button_98.Text = MainForm.getString_0(107385211);
			this.button_98.UseVisualStyleBackColor = true;
			this.button_98.Click += this.button_75_Click;
			this.button_99.Location = new Point(135, 178);
			this.button_99.Name = MainForm.getString_0(107398639);
			this.button_99.Size = new Size(77, 23);
			this.button_99.TabIndex = 10;
			this.button_99.Text = MainForm.getString_0(107401705);
			this.button_99.UseVisualStyleBackColor = true;
			this.button_99.Click += this.button_99_Click;
			this.groupBox_71.Controls.Add(this.comboBox_74);
			this.groupBox_71.Controls.Add(this.button_100);
			this.groupBox_71.Controls.Add(this.fastObjectListView_24);
			this.groupBox_71.Controls.Add(this.label_189);
			this.groupBox_71.Location = new Point(3, 3);
			this.groupBox_71.Name = MainForm.getString_0(107398650);
			this.groupBox_71.Size = new Size(503, 207);
			this.groupBox_71.TabIndex = 60;
			this.groupBox_71.TabStop = false;
			this.groupBox_71.Text = MainForm.getString_0(107398601);
			this.comboBox_74.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.comboBox_74.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.comboBox_74.FormattingEnabled = true;
			this.comboBox_74.Location = new Point(6, 36);
			this.comboBox_74.Name = MainForm.getString_0(107398612);
			this.comboBox_74.Size = new Size(209, 22);
			this.comboBox_74.TabIndex = 60;
			this.comboBox_74.KeyDown += this.comboBox_74_KeyDown;
			this.button_100.Location = new Point(6, 64);
			this.button_100.Name = MainForm.getString_0(107398587);
			this.button_100.Size = new Size(69, 20);
			this.button_100.TabIndex = 18;
			this.button_100.Text = MainForm.getString_0(107385117);
			this.button_100.UseVisualStyleBackColor = true;
			this.fastObjectListView_24.AllColumns.Add(this.olvcolumn_30);
			this.fastObjectListView_24.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_30
			});
			this.fastObjectListView_24.FullRowSelect = true;
			this.fastObjectListView_24.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_24.HideSelection = false;
			this.fastObjectListView_24.Location = new Point(276, 13);
			this.fastObjectListView_24.MultiSelect = false;
			this.fastObjectListView_24.Name = MainForm.getString_0(107398530);
			this.fastObjectListView_24.OwnerDraw = true;
			this.fastObjectListView_24.ShowGroups = false;
			this.fastObjectListView_24.Size = new Size(224, 188);
			this.fastObjectListView_24.TabIndex = 59;
			this.fastObjectListView_24.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_24.View = View.Details;
			this.fastObjectListView_24.VirtualMode = true;
			this.olvcolumn_30.AspectName = MainForm.getString_0(107385059);
			this.olvcolumn_30.CellPadding = null;
			this.olvcolumn_30.FillsFreeSpace = true;
			this.olvcolumn_30.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_30.Text = MainForm.getString_0(107398509);
			this.olvcolumn_30.Width = 250;
			this.label_189.AutoSize = true;
			this.label_189.Location = new Point(6, 19);
			this.label_189.Name = MainForm.getString_0(107398524);
			this.label_189.Size = new Size(141, 14);
			this.label_189.TabIndex = 15;
			this.label_189.Text = MainForm.getString_0(107398479);
			this.tabPage_45.Controls.Add(this.groupBox_56);
			this.tabPage_45.Controls.Add(this.groupBox_55);
			this.tabPage_45.Controls.Add(this.groupBox_54);
			this.tabPage_45.Location = new Point(4, 42);
			this.tabPage_45.Name = MainForm.getString_0(107398446);
			this.tabPage_45.Size = new Size(520, 543);
			this.tabPage_45.TabIndex = 13;
			this.tabPage_45.Text = MainForm.getString_0(107398433);
			this.tabPage_45.UseVisualStyleBackColor = true;
			this.groupBox_56.Controls.Add(this.label_143);
			this.groupBox_56.Controls.Add(this.numericUpDown_54);
			this.groupBox_56.Controls.Add(this.label_144);
			this.groupBox_56.Controls.Add(this.label_145);
			this.groupBox_56.Controls.Add(this.numericUpDown_55);
			this.groupBox_56.Controls.Add(this.label_146);
			this.groupBox_56.Controls.Add(this.button_70);
			this.groupBox_56.Location = new Point(3, 204);
			this.groupBox_56.Name = MainForm.getString_0(107398456);
			this.groupBox_56.Size = new Size(500, 62);
			this.groupBox_56.TabIndex = 31;
			this.groupBox_56.TabStop = false;
			this.groupBox_56.Text = MainForm.getString_0(107365127);
			this.label_143.AutoSize = true;
			this.label_143.Location = new Point(221, 42);
			this.label_143.Name = MainForm.getString_0(107365098);
			this.label_143.Size = new Size(86, 14);
			this.label_143.TabIndex = 66;
			this.label_143.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown221 = this.numericUpDown_54;
			int[] array221 = new int[4];
			array221[0] = 50;
			numericUpDown221.Increment = new decimal(array221);
			this.numericUpDown_54.Location = new Point(163, 40);
			NumericUpDown numericUpDown222 = this.numericUpDown_54;
			int[] array222 = new int[4];
			array222[0] = 1000;
			numericUpDown222.Maximum = new decimal(array222);
			NumericUpDown numericUpDown223 = this.numericUpDown_54;
			int[] array223 = new int[4];
			array223[0] = 50;
			numericUpDown223.Minimum = new decimal(array223);
			this.numericUpDown_54.Name = MainForm.getString_0(107365117);
			this.numericUpDown_54.Size = new Size(52, 20);
			this.numericUpDown_54.TabIndex = 64;
			this.numericUpDown_54.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown224 = this.numericUpDown_54;
			int[] array224 = new int[4];
			array224[0] = 200;
			numericUpDown224.Value = new decimal(array224);
			this.numericUpDown_54.Leave += this.MainForm_Leave;
			this.label_144.AutoSize = true;
			this.label_144.Location = new Point(5, 42);
			this.label_144.Name = MainForm.getString_0(107365084);
			this.label_144.Size = new Size(150, 14);
			this.label_144.TabIndex = 65;
			this.label_144.Text = MainForm.getString_0(107365039);
			this.label_145.AutoSize = true;
			this.label_145.Location = new Point(221, 16);
			this.label_145.Name = MainForm.getString_0(107365002);
			this.label_145.Size = new Size(86, 14);
			this.label_145.TabIndex = 61;
			this.label_145.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown225 = this.numericUpDown_55;
			int[] array225 = new int[4];
			array225[0] = 50;
			numericUpDown225.Increment = new decimal(array225);
			this.numericUpDown_55.Location = new Point(163, 14);
			NumericUpDown numericUpDown226 = this.numericUpDown_55;
			int[] array226 = new int[4];
			array226[0] = 1000;
			numericUpDown226.Maximum = new decimal(array226);
			NumericUpDown numericUpDown227 = this.numericUpDown_55;
			int[] array227 = new int[4];
			array227[0] = 50;
			numericUpDown227.Minimum = new decimal(array227);
			this.numericUpDown_55.Name = MainForm.getString_0(107365021);
			this.numericUpDown_55.Size = new Size(52, 20);
			this.numericUpDown_55.TabIndex = 58;
			this.numericUpDown_55.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown228 = this.numericUpDown_55;
			int[] array228 = new int[4];
			array228[0] = 200;
			numericUpDown228.Value = new decimal(array228);
			this.numericUpDown_55.Leave += this.MainForm_Leave;
			this.label_146.AutoSize = true;
			this.label_146.Location = new Point(5, 16);
			this.label_146.Name = MainForm.getString_0(107364964);
			this.label_146.Size = new Size(87, 14);
			this.label_146.TabIndex = 60;
			this.label_146.Text = MainForm.getString_0(107364983);
			this.button_70.Location = new Point(417, 33);
			this.button_70.Name = MainForm.getString_0(107364930);
			this.button_70.Size = new Size(77, 23);
			this.button_70.TabIndex = 10;
			this.button_70.Text = MainForm.getString_0(107401705);
			this.button_70.UseVisualStyleBackColor = true;
			this.button_70.Click += this.button_70_Click;
			this.groupBox_55.Controls.Add(this.label_142);
			this.groupBox_55.Controls.Add(this.numericUpDown_53);
			this.groupBox_55.Controls.Add(this.checkBox_50);
			this.groupBox_55.Location = new Point(3, 3);
			this.groupBox_55.Name = MainForm.getString_0(107364909);
			this.groupBox_55.Size = new Size(506, 46);
			this.groupBox_55.TabIndex = 30;
			this.groupBox_55.TabStop = false;
			this.groupBox_55.Text = MainForm.getString_0(107388377);
			this.label_142.AutoSize = true;
			this.label_142.Location = new Point(309, 19);
			this.label_142.Name = MainForm.getString_0(107364924);
			this.label_142.Size = new Size(109, 14);
			this.label_142.TabIndex = 13;
			this.label_142.Text = MainForm.getString_0(107365391);
			NumericUpDown numericUpDown229 = this.numericUpDown_53;
			int[] array229 = new int[4];
			array229[0] = 10;
			numericUpDown229.Increment = new decimal(array229);
			this.numericUpDown_53.Location = new Point(424, 16);
			NumericUpDown numericUpDown230 = this.numericUpDown_53;
			int[] array230 = new int[4];
			array230[0] = 5000;
			numericUpDown230.Maximum = new decimal(array230);
			this.numericUpDown_53.Name = MainForm.getString_0(107365394);
			this.numericUpDown_53.Size = new Size(76, 20);
			this.numericUpDown_53.TabIndex = 11;
			this.numericUpDown_53.TextAlign = HorizontalAlignment.Center;
			this.numericUpDown_53.Leave += this.MainForm_Leave;
			this.checkBox_50.AutoSize = true;
			this.checkBox_50.Location = new Point(9, 19);
			this.checkBox_50.Name = MainForm.getString_0(107365369);
			this.checkBox_50.Size = new Size(150, 18);
			this.checkBox_50.TabIndex = 58;
			this.checkBox_50.Text = MainForm.getString_0(107365332);
			this.checkBox_50.UseVisualStyleBackColor = true;
			this.checkBox_50.Leave += this.MainForm_Leave;
			this.groupBox_54.Controls.Add(this.fastObjectListView_15);
			this.groupBox_54.Controls.Add(this.button_68);
			this.groupBox_54.Controls.Add(this.comboBox_58);
			this.groupBox_54.Controls.Add(this.label_141);
			this.groupBox_54.Controls.Add(this.button_69);
			this.groupBox_54.Location = new Point(3, 55);
			this.groupBox_54.Name = MainForm.getString_0(107365299);
			this.groupBox_54.Size = new Size(506, 143);
			this.groupBox_54.TabIndex = 29;
			this.groupBox_54.TabStop = false;
			this.groupBox_54.Text = MainForm.getString_0(107398959);
			this.fastObjectListView_15.AllColumns.Add(this.olvcolumn_21);
			this.fastObjectListView_15.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_21
			});
			this.fastObjectListView_15.FullRowSelect = true;
			this.fastObjectListView_15.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_15.HideSelection = false;
			this.fastObjectListView_15.Location = new Point(251, 19);
			this.fastObjectListView_15.MultiSelect = false;
			this.fastObjectListView_15.Name = MainForm.getString_0(107365250);
			this.fastObjectListView_15.OwnerDraw = true;
			this.fastObjectListView_15.ShowGroups = false;
			this.fastObjectListView_15.Size = new Size(249, 117);
			this.fastObjectListView_15.TabIndex = 27;
			this.fastObjectListView_15.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_15.View = View.Details;
			this.fastObjectListView_15.VirtualMode = true;
			this.olvcolumn_21.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_21.CellPadding = null;
			this.olvcolumn_21.FillsFreeSpace = true;
			this.olvcolumn_21.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_21.Text = MainForm.getString_0(107384059);
			this.olvcolumn_21.Width = 245;
			this.button_68.Location = new Point(176, 38);
			this.button_68.Name = MainForm.getString_0(107365229);
			this.button_68.Size = new Size(69, 20);
			this.button_68.TabIndex = 18;
			this.button_68.Text = MainForm.getString_0(107385117);
			this.button_68.UseVisualStyleBackColor = true;
			this.comboBox_58.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_58.FormattingEnabled = true;
			this.comboBox_58.Location = new Point(6, 36);
			this.comboBox_58.Name = MainForm.getString_0(107365236);
			this.comboBox_58.Size = new Size(164, 22);
			this.comboBox_58.TabIndex = 14;
			this.label_141.AutoSize = true;
			this.label_141.Location = new Point(6, 19);
			this.label_141.Name = MainForm.getString_0(107365211);
			this.label_141.Size = new Size(161, 14);
			this.label_141.TabIndex = 15;
			this.label_141.Text = MainForm.getString_0(107365166);
			this.button_69.Location = new Point(5, 113);
			this.button_69.Name = MainForm.getString_0(107364613);
			this.button_69.Size = new Size(123, 23);
			this.button_69.TabIndex = 16;
			this.button_69.Text = MainForm.getString_0(107385211);
			this.button_69.UseVisualStyleBackColor = true;
			this.button_69.Click += this.button_75_Click;
			this.tabPage_32.Controls.Add(this.groupBox_27);
			this.tabPage_32.Controls.Add(this.groupBox_28);
			this.tabPage_32.Location = new Point(4, 42);
			this.tabPage_32.Name = MainForm.getString_0(107364580);
			this.tabPage_32.Size = new Size(520, 543);
			this.tabPage_32.TabIndex = 11;
			this.tabPage_32.Text = MainForm.getString_0(107398970);
			this.tabPage_32.UseVisualStyleBackColor = true;
			this.groupBox_27.Controls.Add(this.button_28);
			this.groupBox_27.Controls.Add(this.button_29);
			this.groupBox_27.Controls.Add(this.comboBox_30);
			this.groupBox_27.Controls.Add(this.button_30);
			this.groupBox_27.Controls.Add(this.fastObjectListView_5);
			this.groupBox_27.Location = new Point(6, 234);
			this.groupBox_27.Name = MainForm.getString_0(107364599);
			this.groupBox_27.Size = new Size(511, 211);
			this.groupBox_27.TabIndex = 1;
			this.groupBox_27.TabStop = false;
			this.groupBox_27.Text = MainForm.getString_0(107364550);
			this.button_28.Location = new Point(6, 182);
			this.button_28.Name = MainForm.getString_0(107364561);
			this.button_28.Size = new Size(123, 25);
			this.button_28.TabIndex = 22;
			this.button_28.Text = MainForm.getString_0(107385211);
			this.button_28.UseVisualStyleBackColor = true;
			this.button_28.Click += this.button_75_Click;
			this.button_29.Location = new Point(143, 182);
			this.button_29.Name = MainForm.getString_0(107364496);
			this.button_29.Size = new Size(69, 25);
			this.button_29.TabIndex = 23;
			this.button_29.Text = MainForm.getString_0(107401705);
			this.button_29.UseVisualStyleBackColor = true;
			this.button_29.Click += this.button_29_Click;
			this.comboBox_30.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_30.FormattingEnabled = true;
			this.comboBox_30.Location = new Point(6, 19);
			this.comboBox_30.Name = MainForm.getString_0(107364499);
			this.comboBox_30.Size = new Size(207, 22);
			this.comboBox_30.TabIndex = 19;
			this.button_30.Location = new Point(6, 47);
			this.button_30.Name = MainForm.getString_0(107364474);
			this.button_30.Size = new Size(69, 20);
			this.button_30.TabIndex = 20;
			this.button_30.Text = MainForm.getString_0(107385117);
			this.button_30.UseVisualStyleBackColor = true;
			this.fastObjectListView_5.AllColumns.Add(this.olvcolumn_6);
			this.fastObjectListView_5.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_6
			});
			this.fastObjectListView_5.FullRowSelect = true;
			this.fastObjectListView_5.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_5.HideSelection = false;
			this.fastObjectListView_5.Location = new Point(263, 17);
			this.fastObjectListView_5.MultiSelect = false;
			this.fastObjectListView_5.Name = MainForm.getString_0(107364441);
			this.fastObjectListView_5.OwnerDraw = true;
			this.fastObjectListView_5.ShowGroups = false;
			this.fastObjectListView_5.Size = new Size(241, 188);
			this.fastObjectListView_5.TabIndex = 21;
			this.fastObjectListView_5.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_5.View = View.Details;
			this.fastObjectListView_5.VirtualMode = true;
			this.olvcolumn_6.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_6.CellPadding = null;
			this.olvcolumn_6.FillsFreeSpace = true;
			this.olvcolumn_6.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_6.Text = MainForm.getString_0(107384059);
			this.olvcolumn_6.Width = 250;
			this.groupBox_28.Controls.Add(this.numericUpDown_59);
			this.groupBox_28.Controls.Add(this.comboBox_67);
			this.groupBox_28.Controls.Add(this.comboBox_32);
			this.groupBox_28.Controls.Add(this.label_102);
			this.groupBox_28.Controls.Add(this.comboBox_33);
			this.groupBox_28.Controls.Add(this.label_103);
			this.groupBox_28.Controls.Add(this.comboBox_31);
			this.groupBox_28.Controls.Add(this.label_101);
			this.groupBox_28.Location = new Point(6, 6);
			this.groupBox_28.Name = MainForm.getString_0(107364408);
			this.groupBox_28.Size = new Size(511, 222);
			this.groupBox_28.TabIndex = 0;
			this.groupBox_28.TabStop = false;
			this.groupBox_28.Text = MainForm.getString_0(107364871);
			NumericUpDown numericUpDown231 = this.numericUpDown_59;
			int[] array231 = new int[4];
			array231[0] = 5;
			numericUpDown231.Increment = new decimal(array231);
			this.numericUpDown_59.Location = new Point(240, 17);
			NumericUpDown numericUpDown232 = this.numericUpDown_59;
			int[] array232 = new int[4];
			array232[0] = 1000;
			numericUpDown232.Maximum = new decimal(array232);
			NumericUpDown numericUpDown233 = this.numericUpDown_59;
			int[] array233 = new int[4];
			array233[0] = 1;
			numericUpDown233.Minimum = new decimal(array233);
			this.numericUpDown_59.Name = MainForm.getString_0(107364838);
			this.numericUpDown_59.Size = new Size(52, 20);
			this.numericUpDown_59.TabIndex = 34;
			this.numericUpDown_59.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown234 = this.numericUpDown_59;
			int[] array234 = new int[4];
			array234[0] = 100;
			numericUpDown234.Value = new decimal(array234);
			this.numericUpDown_59.Leave += this.MainForm_Leave;
			this.comboBox_67.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_67.FormattingEnabled = true;
			this.comboBox_67.Location = new Point(298, 16);
			this.comboBox_67.Name = MainForm.getString_0(107364849);
			this.comboBox_67.Size = new Size(195, 22);
			this.comboBox_67.TabIndex = 33;
			this.comboBox_67.Leave += this.MainForm_Leave;
			this.comboBox_32.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_32.FormattingEnabled = true;
			this.comboBox_32.Items.AddRange(new object[]
			{
				MainForm.getString_0(107364820),
				MainForm.getString_0(107364779),
				MainForm.getString_0(107364770),
				MainForm.getString_0(107364793),
				MainForm.getString_0(107364788),
				MainForm.getString_0(107364747),
				MainForm.getString_0(107364738)
			});
			this.comboBox_32.Location = new Point(107, 73);
			this.comboBox_32.Name = MainForm.getString_0(107364761);
			this.comboBox_32.Size = new Size(127, 22);
			this.comboBox_32.TabIndex = 7;
			this.comboBox_32.Leave += this.MainForm_Leave;
			this.label_102.AutoSize = true;
			this.label_102.Location = new Point(6, 76);
			this.label_102.Name = MainForm.getString_0(107364716);
			this.label_102.Size = new Size(50, 14);
			this.label_102.TabIndex = 8;
			this.label_102.Text = MainForm.getString_0(107364735);
			this.comboBox_33.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_33.FormattingEnabled = true;
			this.comboBox_33.Items.AddRange(new object[]
			{
				MainForm.getString_0(107364722),
				MainForm.getString_0(107364697),
				MainForm.getString_0(107364644)
			});
			this.comboBox_33.Location = new Point(107, 45);
			this.comboBox_33.Name = MainForm.getString_0(107364667);
			this.comboBox_33.Size = new Size(127, 22);
			this.comboBox_33.TabIndex = 5;
			this.comboBox_33.Leave += this.MainForm_Leave;
			this.label_103.AutoSize = true;
			this.label_103.Location = new Point(6, 48);
			this.label_103.Name = MainForm.getString_0(107364098);
			this.label_103.Size = new Size(91, 14);
			this.label_103.TabIndex = 6;
			this.label_103.Text = MainForm.getString_0(107364117);
			this.comboBox_31.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_31.FormattingEnabled = true;
			this.comboBox_31.Items.AddRange(new object[]
			{
				MainForm.getString_0(107364092),
				MainForm.getString_0(107364083),
				MainForm.getString_0(107390144),
				MainForm.getString_0(107382662),
				MainForm.getString_0(107390139),
				MainForm.getString_0(107390094)
			});
			this.comboBox_31.Location = new Point(107, 17);
			this.comboBox_31.Name = MainForm.getString_0(107364042);
			this.comboBox_31.Size = new Size(127, 22);
			this.comboBox_31.TabIndex = 3;
			this.comboBox_31.SelectedIndexChanged += this.comboBox_31_SelectedIndexChanged;
			this.comboBox_31.Leave += this.MainForm_Leave;
			this.label_101.AutoSize = true;
			this.label_101.Location = new Point(6, 20);
			this.label_101.Name = MainForm.getString_0(107364057);
			this.label_101.Size = new Size(76, 14);
			this.label_101.TabIndex = 4;
			this.label_101.Text = MainForm.getString_0(107364012);
			this.tabPage_42.Controls.Add(this.groupBox_43);
			this.tabPage_42.Controls.Add(this.groupBox_42);
			this.tabPage_42.Location = new Point(4, 42);
			this.tabPage_42.Name = MainForm.getString_0(107364027);
			this.tabPage_42.Size = new Size(520, 543);
			this.tabPage_42.TabIndex = 12;
			this.tabPage_42.Text = MainForm.getString_0(107363982);
			this.tabPage_42.UseVisualStyleBackColor = true;
			this.groupBox_43.Controls.Add(this.richTextBox_2);
			this.groupBox_43.Location = new Point(3, 121);
			this.groupBox_43.Name = MainForm.getString_0(107363973);
			this.groupBox_43.Size = new Size(514, 421);
			this.groupBox_43.TabIndex = 1;
			this.groupBox_43.TabStop = false;
			this.groupBox_43.Text = MainForm.getString_0(107389294);
			this.richTextBox_2.Anchor = AnchorStyles.None;
			this.richTextBox_2.BackColor = Color.WhiteSmoke;
			this.richTextBox_2.BorderStyle = BorderStyle.FixedSingle;
			this.richTextBox_2.Font = new Font(MainForm.getString_0(107397254), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox_2.ForeColor = SystemColors.ActiveCaptionText;
			this.richTextBox_2.HideSelection = false;
			this.richTextBox_2.Location = new Point(6, 16);
			this.richTextBox_2.MaximumSize = new Size(999, 999);
			this.richTextBox_2.Name = MainForm.getString_0(107363988);
			this.richTextBox_2.ReadOnly = true;
			this.richTextBox_2.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.richTextBox_2.Size = new Size(501, 399);
			this.richTextBox_2.TabIndex = 7;
			this.richTextBox_2.Text = MainForm.getString_0(107396269);
			this.groupBox_42.Controls.Add(this.button_48);
			this.groupBox_42.Controls.Add(this.button_49);
			this.groupBox_42.Controls.Add(this.comboBox_47);
			this.groupBox_42.Controls.Add(this.button_50);
			this.groupBox_42.Controls.Add(this.fastObjectListView_10);
			this.groupBox_42.Location = new Point(3, 3);
			this.groupBox_42.Name = MainForm.getString_0(107363939);
			this.groupBox_42.Size = new Size(514, 112);
			this.groupBox_42.TabIndex = 0;
			this.groupBox_42.TabStop = false;
			this.groupBox_42.Text = MainForm.getString_0(107388377);
			this.button_48.Location = new Point(5, 82);
			this.button_48.Name = MainForm.getString_0(107363954);
			this.button_48.Size = new Size(123, 25);
			this.button_48.TabIndex = 27;
			this.button_48.Text = MainForm.getString_0(107385211);
			this.button_48.UseVisualStyleBackColor = true;
			this.button_48.Click += this.button_75_Click;
			this.button_49.Location = new Point(142, 82);
			this.button_49.Name = MainForm.getString_0(107363925);
			this.button_49.Size = new Size(82, 25);
			this.button_49.TabIndex = 28;
			this.button_49.Text = MainForm.getString_0(107388558);
			this.button_49.UseVisualStyleBackColor = true;
			this.button_49.Click += this.button_49_Click;
			this.comboBox_47.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_47.FormattingEnabled = true;
			this.comboBox_47.Location = new Point(5, 19);
			this.comboBox_47.Name = MainForm.getString_0(107363876);
			this.comboBox_47.Size = new Size(219, 22);
			this.comboBox_47.TabIndex = 24;
			this.button_50.Location = new Point(5, 47);
			this.button_50.Name = MainForm.getString_0(107364367);
			this.button_50.Size = new Size(69, 20);
			this.button_50.TabIndex = 25;
			this.button_50.Text = MainForm.getString_0(107385117);
			this.button_50.UseVisualStyleBackColor = true;
			this.fastObjectListView_10.AllColumns.Add(this.olvcolumn_15);
			this.fastObjectListView_10.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_15
			});
			this.fastObjectListView_10.FullRowSelect = true;
			this.fastObjectListView_10.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_10.HideSelection = false;
			this.fastObjectListView_10.Location = new Point(238, 10);
			this.fastObjectListView_10.MultiSelect = false;
			this.fastObjectListView_10.Name = MainForm.getString_0(107364378);
			this.fastObjectListView_10.OwnerDraw = true;
			this.fastObjectListView_10.ShowGroups = false;
			this.fastObjectListView_10.Size = new Size(273, 97);
			this.fastObjectListView_10.TabIndex = 26;
			this.fastObjectListView_10.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_10.View = View.Details;
			this.fastObjectListView_10.VirtualMode = true;
			this.olvcolumn_15.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_15.CellPadding = null;
			this.olvcolumn_15.FillsFreeSpace = true;
			this.olvcolumn_15.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_15.Text = MainForm.getString_0(107384059);
			this.olvcolumn_15.Width = 300;
			this.tabPage_23.Controls.Add(this.groupBox_17);
			this.tabPage_23.Location = new Point(4, 42);
			this.tabPage_23.Name = MainForm.getString_0(107364325);
			this.tabPage_23.Size = new Size(520, 543);
			this.tabPage_23.TabIndex = 9;
			this.tabPage_23.Text = MainForm.getString_0(107364340);
			this.tabPage_23.UseVisualStyleBackColor = true;
			this.groupBox_17.Controls.Add(this.label_76);
			this.groupBox_17.Controls.Add(this.numericUpDown_26);
			this.groupBox_17.Controls.Add(this.label_77);
			this.groupBox_17.Controls.Add(this.button_10);
			this.groupBox_17.Controls.Add(this.button_11);
			this.groupBox_17.Controls.Add(this.comboBox_18);
			this.groupBox_17.Controls.Add(this.button_12);
			this.groupBox_17.Controls.Add(this.fastObjectListView_1);
			this.groupBox_17.Location = new Point(3, 3);
			this.groupBox_17.Name = MainForm.getString_0(107364295);
			this.groupBox_17.Size = new Size(514, 211);
			this.groupBox_17.TabIndex = 28;
			this.groupBox_17.TabStop = false;
			this.groupBox_17.Text = MainForm.getString_0(107364310);
			this.label_76.AutoSize = true;
			this.label_76.Location = new Point(139, 134);
			this.label_76.Name = MainForm.getString_0(107364285);
			this.label_76.Size = new Size(86, 14);
			this.label_76.TabIndex = 53;
			this.label_76.Text = MainForm.getString_0(107409933);
			NumericUpDown numericUpDown235 = this.numericUpDown_26;
			int[] array235 = new int[4];
			array235[0] = 5;
			numericUpDown235.Increment = new decimal(array235);
			this.numericUpDown_26.Location = new Point(83, 131);
			NumericUpDown numericUpDown236 = this.numericUpDown_26;
			int[] array236 = new int[4];
			array236[0] = 1000;
			numericUpDown236.Maximum = new decimal(array236);
			NumericUpDown numericUpDown237 = this.numericUpDown_26;
			int[] array237 = new int[4];
			array237[0] = 50;
			numericUpDown237.Minimum = new decimal(array237);
			this.numericUpDown_26.Name = MainForm.getString_0(107364240);
			this.numericUpDown_26.Size = new Size(52, 20);
			this.numericUpDown_26.TabIndex = 20;
			this.numericUpDown_26.TextAlign = HorizontalAlignment.Center;
			NumericUpDown numericUpDown238 = this.numericUpDown_26;
			int[] array238 = new int[4];
			array238[0] = 100;
			numericUpDown238.Value = new decimal(array238);
			this.label_77.AutoSize = true;
			this.label_77.BackColor = Color.Transparent;
			this.label_77.Location = new Point(6, 134);
			this.label_77.Name = MainForm.getString_0(107364243);
			this.label_77.Size = new Size(75, 14);
			this.label_77.TabIndex = 19;
			this.label_77.Text = MainForm.getString_0(107364214);
			this.button_10.Location = new Point(6, 180);
			this.button_10.Name = MainForm.getString_0(107364165);
			this.button_10.Size = new Size(123, 25);
			this.button_10.TabIndex = 17;
			this.button_10.Text = MainForm.getString_0(107385211);
			this.button_10.UseVisualStyleBackColor = true;
			this.button_10.Click += this.button_75_Click;
			this.button_11.Location = new Point(143, 180);
			this.button_11.Name = MainForm.getString_0(107364160);
			this.button_11.Size = new Size(69, 25);
			this.button_11.TabIndex = 18;
			this.button_11.Text = MainForm.getString_0(107401705);
			this.button_11.UseVisualStyleBackColor = true;
			this.button_11.Click += this.button_11_Click;
			this.comboBox_18.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_18.FormattingEnabled = true;
			this.comboBox_18.Location = new Point(6, 17);
			this.comboBox_18.Name = MainForm.getString_0(107363595);
			this.comboBox_18.Size = new Size(207, 22);
			this.comboBox_18.TabIndex = 14;
			this.comboBox_18.KeyDown += this.comboBox_18_KeyDown;
			this.button_12.Location = new Point(6, 45);
			this.button_12.Name = MainForm.getString_0(107363566);
			this.button_12.Size = new Size(69, 20);
			this.button_12.TabIndex = 15;
			this.button_12.Text = MainForm.getString_0(107385117);
			this.button_12.UseVisualStyleBackColor = true;
			this.fastObjectListView_1.AllColumns.Add(this.olvcolumn_1);
			this.fastObjectListView_1.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_1
			});
			this.fastObjectListView_1.FullRowSelect = true;
			this.fastObjectListView_1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_1.HideSelection = false;
			this.fastObjectListView_1.Location = new Point(269, 17);
			this.fastObjectListView_1.MultiSelect = false;
			this.fastObjectListView_1.Name = MainForm.getString_0(107363569);
			this.fastObjectListView_1.OwnerDraw = true;
			this.fastObjectListView_1.ShowGroups = false;
			this.fastObjectListView_1.Size = new Size(238, 188);
			this.fastObjectListView_1.TabIndex = 16;
			this.fastObjectListView_1.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_1.View = View.Details;
			this.fastObjectListView_1.VirtualMode = true;
			this.olvcolumn_1.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_1.CellPadding = null;
			this.olvcolumn_1.FillsFreeSpace = true;
			this.olvcolumn_1.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_1.Text = MainForm.getString_0(107384059);
			this.olvcolumn_1.Width = 250;
			this.tabPage_46.Controls.Add(this.groupBox_57);
			this.tabPage_46.Location = new Point(4, 42);
			this.tabPage_46.Name = MainForm.getString_0(107363544);
			this.tabPage_46.Size = new Size(520, 543);
			this.tabPage_46.TabIndex = 14;
			this.tabPage_46.Text = MainForm.getString_0(107363495);
			this.tabPage_46.UseVisualStyleBackColor = true;
			this.groupBox_57.Controls.Add(this.button_71);
			this.groupBox_57.Controls.Add(this.comboBox_59);
			this.groupBox_57.Controls.Add(this.comboBox_60);
			this.groupBox_57.Controls.Add(this.label_150);
			this.groupBox_57.Controls.Add(this.label_151);
			this.groupBox_57.Location = new Point(3, 3);
			this.groupBox_57.Name = MainForm.getString_0(107363510);
			this.groupBox_57.Size = new Size(514, 99);
			this.groupBox_57.TabIndex = 0;
			this.groupBox_57.TabStop = false;
			this.groupBox_57.Text = MainForm.getString_0(107363495);
			this.button_71.Location = new Point(433, 70);
			this.button_71.Name = MainForm.getString_0(107397168);
			this.button_71.Size = new Size(75, 23);
			this.button_71.TabIndex = 17;
			this.button_71.Text = MainForm.getString_0(107401705);
			this.button_71.UseVisualStyleBackColor = true;
			this.comboBox_59.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_59.FormattingEnabled = true;
			this.comboBox_59.Location = new Point(104, 44);
			this.comboBox_59.Name = MainForm.getString_0(107363461);
			this.comboBox_59.Size = new Size(403, 22);
			this.comboBox_59.TabIndex = 16;
			this.comboBox_60.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_60.FormattingEnabled = true;
			this.comboBox_60.Location = new Point(104, 16);
			this.comboBox_60.Name = MainForm.getString_0(107363480);
			this.comboBox_60.Size = new Size(403, 22);
			this.comboBox_60.TabIndex = 15;
			this.label_150.AutoSize = true;
			this.label_150.Location = new Point(5, 47);
			this.label_150.Name = MainForm.getString_0(107363455);
			this.label_150.Size = new Size(93, 14);
			this.label_150.TabIndex = 1;
			this.label_150.Text = MainForm.getString_0(107363442);
			this.label_151.AutoSize = true;
			this.label_151.Location = new Point(6, 19);
			this.label_151.Name = MainForm.getString_0(107363421);
			this.label_151.Size = new Size(78, 14);
			this.label_151.TabIndex = 0;
			this.label_151.Text = MainForm.getString_0(107363376);
			this.tabPage_47.Controls.Add(this.groupBox_58);
			this.tabPage_47.Location = new Point(4, 42);
			this.tabPage_47.Name = MainForm.getString_0(107363391);
			this.tabPage_47.Size = new Size(520, 543);
			this.tabPage_47.TabIndex = 15;
			this.tabPage_47.Text = MainForm.getString_0(107363850);
			this.tabPage_47.UseVisualStyleBackColor = true;
			this.groupBox_58.Controls.Add(this.button_73);
			this.groupBox_58.Controls.Add(this.fastObjectListView_16);
			this.groupBox_58.Controls.Add(this.button_74);
			this.groupBox_58.Controls.Add(this.comboBox_61);
			this.groupBox_58.Controls.Add(this.label_152);
			this.groupBox_58.Controls.Add(this.button_75);
			this.groupBox_58.Location = new Point(4, 3);
			this.groupBox_58.Name = MainForm.getString_0(107363865);
			this.groupBox_58.Size = new Size(513, 143);
			this.groupBox_58.TabIndex = 30;
			this.groupBox_58.TabStop = false;
			this.groupBox_58.Text = MainForm.getString_0(107398959);
			this.button_73.Location = new Point(176, 113);
			this.button_73.Name = MainForm.getString_0(107363816);
			this.button_73.Size = new Size(69, 23);
			this.button_73.TabIndex = 28;
			this.button_73.Text = MainForm.getString_0(107401705);
			this.button_73.UseVisualStyleBackColor = true;
			this.button_73.Click += this.button_73_Click;
			this.fastObjectListView_16.AllColumns.Add(this.olvcolumn_22);
			this.fastObjectListView_16.Columns.AddRange(new ColumnHeader[]
			{
				this.olvcolumn_22
			});
			this.fastObjectListView_16.FullRowSelect = true;
			this.fastObjectListView_16.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.fastObjectListView_16.HideSelection = false;
			this.fastObjectListView_16.Location = new Point(251, 19);
			this.fastObjectListView_16.MultiSelect = false;
			this.fastObjectListView_16.Name = MainForm.getString_0(107363787);
			this.fastObjectListView_16.OwnerDraw = true;
			this.fastObjectListView_16.ShowGroups = false;
			this.fastObjectListView_16.Size = new Size(256, 117);
			this.fastObjectListView_16.TabIndex = 27;
			this.fastObjectListView_16.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView_16.View = View.Details;
			this.fastObjectListView_16.VirtualMode = true;
			this.olvcolumn_22.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_22.CellPadding = null;
			this.olvcolumn_22.FillsFreeSpace = true;
			this.olvcolumn_22.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_22.Text = MainForm.getString_0(107384059);
			this.olvcolumn_22.Width = 253;
			this.button_74.Location = new Point(176, 38);
			this.button_74.Name = MainForm.getString_0(107363758);
			this.button_74.Size = new Size(69, 20);
			this.button_74.TabIndex = 18;
			this.button_74.Text = MainForm.getString_0(107385117);
			this.button_74.UseVisualStyleBackColor = true;
			this.comboBox_61.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_61.FormattingEnabled = true;
			this.comboBox_61.Location = new Point(6, 36);
			this.comboBox_61.Name = MainForm.getString_0(107363761);
			this.comboBox_61.Size = new Size(164, 22);
			this.comboBox_61.TabIndex = 14;
			this.label_152.AutoSize = true;
			this.label_152.Location = new Point(6, 19);
			this.label_152.Name = MainForm.getString_0(107363696);
			this.label_152.Size = new Size(154, 14);
			this.label_152.TabIndex = 15;
			this.label_152.Text = MainForm.getString_0(107363683);
			this.button_75.Location = new Point(5, 113);
			this.button_75.Name = MainForm.getString_0(107363678);
			this.button_75.Size = new Size(123, 23);
			this.button_75.TabIndex = 16;
			this.button_75.Text = MainForm.getString_0(107385211);
			this.button_75.UseVisualStyleBackColor = true;
			this.button_75.Click += this.button_75_Click;
			this.olvcolumn_20.AspectName = MainForm.getString_0(107384064);
			this.olvcolumn_20.CellPadding = null;
			this.olvcolumn_20.DisplayIndex = 0;
			this.olvcolumn_20.FillsFreeSpace = true;
			this.olvcolumn_20.HeaderTextAlign = HorizontalAlignment.Center;
			this.olvcolumn_20.Text = MainForm.getString_0(107384059);
			this.olvcolumn_20.Width = 250;
			this.timer_0.Interval = 750;
			this.timer_0.Tick += this.timer_0_Tick;
			this.timer_1.Interval = 60000;
			this.timer_1.Tick += this.timer_1_Tick;
			this.toolStrip_0.Dock = DockStyle.Bottom;
			this.toolStrip_0.Font = new Font(MainForm.getString_0(107363665), 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.toolStrip_0.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStrip_0.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripProgressBar_0,
				this.toolStripLabel_0,
				this.toolStripSeparator_0,
				this.toolStripLabel_1,
				this.toolStripSeparator_1,
				this.toolStripLabel_2
			});
			this.toolStrip_0.Location = new Point(0, 582);
			this.toolStrip_0.Name = MainForm.getString_0(107363620);
			this.toolStrip_0.Size = new Size(526, 25);
			this.toolStrip_0.TabIndex = 5;
			this.toolStrip_0.Text = MainForm.getString_0(107363620);
			this.toolStripProgressBar_0.ForeColor = Color.Green;
			this.toolStripProgressBar_0.Name = MainForm.getString_0(107363639);
			this.toolStripProgressBar_0.Size = new Size(140, 22);
			this.toolStripProgressBar_0.Style = ProgressBarStyle.Continuous;
			this.toolStripLabel_0.Alignment = ToolStripItemAlignment.Right;
			this.toolStripLabel_0.Name = MainForm.getString_0(107363078);
			this.toolStripLabel_0.Size = new Size(49, 22);
			this.toolStripLabel_0.Text = MainForm.getString_0(107363097);
			this.toolStripLabel_0.TextAlign = ContentAlignment.MiddleRight;
			this.toolStripSeparator_0.Name = MainForm.getString_0(107363052);
			this.toolStripSeparator_0.Size = new Size(6, 25);
			this.toolStripLabel_1.Name = MainForm.getString_0(107363023);
			this.toolStripLabel_1.Size = new Size(55, 22);
			this.toolStripLabel_1.Text = MainForm.getString_0(107363034);
			this.toolStripSeparator_1.Name = MainForm.getString_0(107362989);
			this.toolStripSeparator_1.Size = new Size(6, 25);
			this.toolStripLabel_2.Name = MainForm.getString_0(107362960);
			this.toolStripLabel_2.Size = new Size(37, 22);
			this.toolStripLabel_2.Text = MainForm.getString_0(107362951);
			this.toolTip_0.AutoPopDelay = 10000;
			this.toolTip_0.InitialDelay = 50;
			this.toolTip_0.IsBalloon = true;
			this.toolTip_0.OwnerDraw = true;
			this.toolTip_0.ReshowDelay = 50;
			this.toolTip_0.ToolTipIcon = ToolTipIcon.Info;
			this.toolTip_0.ToolTipTitle = MainForm.getString_0(107362974);
			this.checkBox_16.AutoSize = true;
			this.checkBox_16.Location = new Point(9, 75);
			this.checkBox_16.Name = MainForm.getString_0(107362965);
			this.checkBox_16.Size = new Size(197, 18);
			this.checkBox_16.TabIndex = 57;
			this.checkBox_16.Text = MainForm.getString_0(107362936);
			this.checkBox_16.UseVisualStyleBackColor = true;
			this.timer_2.Interval = 1000;
			this.timer_2.Tick += this.timer_2_Tick;
			this.label_78.AutoSize = true;
			this.label_78.Location = new Point(3, 228);
			this.label_78.Name = MainForm.getString_0(107362859);
			this.label_78.Size = new Size(96, 14);
			this.label_78.TabIndex = 41;
			this.label_78.Text = MainForm.getString_0(107403758);
			this.timer_3.Interval = 10000;
			this.timer_3.Tick += this.timer_3_Tick;
			this.timer_4.Interval = 30000;
			this.timer_4.Tick += this.timer_4_Tick;
			this.timer_5.Enabled = true;
			this.timer_5.Tick += this.timer_5_Tick;
			this.timer_6.Enabled = true;
			this.timer_6.Interval = 120000;
			this.timer_6.Tick += this.timer_6_Tick;
			this.timer_7.Interval = 60000;
			this.timer_7.Tick += this.timer_7_Tick;
			this.comboBox_77.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_77.FormattingEnabled = true;
			this.comboBox_77.Items.AddRange(new object[]
			{
				MainForm.getString_0(107362878),
				MainForm.getString_0(107362873)
			});
			this.comboBox_77.Location = new Point(206, 303);
			this.comboBox_77.Name = MainForm.getString_0(107362868);
			this.comboBox_77.Size = new Size(82, 22);
			this.comboBox_77.TabIndex = 47;
			this.comboBox_77.Leave += this.MainForm_Leave;
			this.comboBox_76.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_76.FormattingEnabled = true;
			this.comboBox_76.Items.AddRange(new object[]
			{
				MainForm.getString_0(107362878),
				MainForm.getString_0(107362873)
			});
			this.comboBox_76.Location = new Point(206, 303);
			this.comboBox_76.Name = MainForm.getString_0(107363331);
			this.comboBox_76.Size = new Size(82, 22);
			this.comboBox_76.TabIndex = 52;
			this.comboBox_76.Leave += this.MainForm_Leave;
			base.AutoScaleDimensions = new SizeF(7f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(526, 607);
			base.Controls.Add(this.toolStrip_0);
			base.Controls.Add(this.tabControl_4);
			this.Font = new Font(MainForm.getString_0(107397254), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.Name = MainForm.getString_0(107363346);
			base.StartPosition = FormStartPosition.Manual;
			this.Text = MainForm.getString_0(107363301);
			base.Deactivate += this.MainForm_Leave;
			base.FormClosing += this.MainForm_FormClosing;
			base.Load += this.MainForm_Load;
			base.Leave += this.MainForm_Leave;
			base.Move += this.MainForm_Move;
			this.tabControl_4.ResumeLayout(false);
			this.tabPage_0.ResumeLayout(false);
			this.tabPage_0.PerformLayout();
			this.tabControl_6.ResumeLayout(false);
			this.tabPage_30.ResumeLayout(false);
			this.tabPage_31.ResumeLayout(false);
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_0.PerformLayout();
			this.groupBox_2.ResumeLayout(false);
			this.groupBox_2.PerformLayout();
			this.groupBox_1.ResumeLayout(false);
			this.groupBox_1.PerformLayout();
			this.tabPage_1.ResumeLayout(false);
			this.tabControl_0.ResumeLayout(false);
			this.tabPage_2.ResumeLayout(false);
			this.groupBox_53.ResumeLayout(false);
			this.groupBox_53.PerformLayout();
			((ISupportInitialize)this.trackBar_2).EndInit();
			this.groupBox_32.ResumeLayout(false);
			this.groupBox_32.PerformLayout();
			this.groupBox_3.ResumeLayout(false);
			this.groupBox_3.PerformLayout();
			((ISupportInitialize)this.pictureBox_2).EndInit();
			this.tabPage_3.ResumeLayout(false);
			this.tabControl_5.ResumeLayout(false);
			this.tabPage_27.ResumeLayout(false);
			this.groupBox_7.ResumeLayout(false);
			this.groupBox_7.PerformLayout();
			this.groupBox_52.ResumeLayout(false);
			this.groupBox_52.PerformLayout();
			this.groupBox_51.ResumeLayout(false);
			this.groupBox_51.PerformLayout();
			((ISupportInitialize)this.pictureBox_1).EndInit();
			this.groupBox_14.ResumeLayout(false);
			this.groupBox_14.PerformLayout();
			((ISupportInitialize)this.numericUpDown_8).EndInit();
			((ISupportInitialize)this.numericUpDown_9).EndInit();
			this.tabPage_28.ResumeLayout(false);
			this.groupBox_4.ResumeLayout(false);
			this.groupBox_5.ResumeLayout(false);
			this.groupBox_5.PerformLayout();
			((ISupportInitialize)this.numericUpDown_0).EndInit();
			((ISupportInitialize)this.numericUpDown_1).EndInit();
			((ISupportInitialize)this.numericUpDown_2).EndInit();
			((ISupportInitialize)this.numericUpDown_3).EndInit();
			this.groupBox_6.ResumeLayout(false);
			this.groupBox_6.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_3).EndInit();
			((ISupportInitialize)this.numericUpDown_4).EndInit();
			((ISupportInitialize)this.numericUpDown_5).EndInit();
			((ISupportInitialize)this.numericUpDown_6).EndInit();
			((ISupportInitialize)this.numericUpDown_7).EndInit();
			this.tabPage_4.ResumeLayout(false);
			this.tabControl_3.ResumeLayout(false);
			this.tabPage_16.ResumeLayout(false);
			this.groupBox_9.ResumeLayout(false);
			this.groupBox_9.PerformLayout();
			this.groupBox_8.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_0).EndInit();
			this.tabPage_26.ResumeLayout(false);
			this.groupBox_23.ResumeLayout(false);
			this.groupBox_23.PerformLayout();
			this.tabPage_18.ResumeLayout(false);
			this.groupBox_15.ResumeLayout(false);
			this.groupBox_15.PerformLayout();
			this.tabPage_24.ResumeLayout(false);
			this.groupBox_18.ResumeLayout(false);
			this.groupBox_19.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_2).EndInit();
			this.tabPage_50.ResumeLayout(false);
			this.tabControl_9.ResumeLayout(false);
			this.tabPage_51.ResumeLayout(false);
			this.groupBox_68.ResumeLayout(false);
			this.groupBox_68.PerformLayout();
			((ISupportInitialize)this.numericUpDown_74).EndInit();
			this.groupBox_61.ResumeLayout(false);
			this.groupBox_62.ResumeLayout(false);
			this.groupBox_62.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_17).EndInit();
			((ISupportInitialize)this.numericUpDown_62).EndInit();
			this.tabPage_54.ResumeLayout(false);
			this.groupBox_69.ResumeLayout(false);
			this.groupBox_69.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_22).EndInit();
			((ISupportInitialize)this.numericUpDown_72).EndInit();
			this.tabPage_52.ResumeLayout(false);
			this.groupBox_63.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_18).EndInit();
			this.tabPage_53.ResumeLayout(false);
			this.groupBox_64.ResumeLayout(false);
			this.groupBox_64.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_19).EndInit();
			this.groupBox_65.ResumeLayout(false);
			this.groupBox_65.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_20).EndInit();
			this.groupBox_66.ResumeLayout(false);
			this.groupBox_66.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_21).EndInit();
			((ISupportInitialize)this.numericUpDown_63).EndInit();
			((ISupportInitialize)this.numericUpDown_64).EndInit();
			this.tabPage_5.ResumeLayout(false);
			this.groupBox_67.ResumeLayout(false);
			this.groupBox_67.PerformLayout();
			((ISupportInitialize)this.numericUpDown_65).EndInit();
			((ISupportInitialize)this.numericUpDown_66).EndInit();
			((ISupportInitialize)this.numericUpDown_67).EndInit();
			((ISupportInitialize)this.numericUpDown_68).EndInit();
			((ISupportInitialize)this.numericUpDown_69).EndInit();
			((ISupportInitialize)this.numericUpDown_70).EndInit();
			this.tabPage_29.ResumeLayout(false);
			this.groupBox_50.ResumeLayout(false);
			this.groupBox_33.ResumeLayout(false);
			this.groupBox_33.PerformLayout();
			((ISupportInitialize)this.numericUpDown_60).EndInit();
			((ISupportInitialize)this.numericUpDown_61).EndInit();
			((ISupportInitialize)this.numericUpDown_57).EndInit();
			this.groupBox_26.ResumeLayout(false);
			this.groupBox_26.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_4).EndInit();
			this.tabPage_7.ResumeLayout(false);
			this.tabControl_1.ResumeLayout(false);
			this.tabPage_8.ResumeLayout(false);
			this.tabPage_8.PerformLayout();
			((ISupportInitialize)this.numericUpDown_75).EndInit();
			this.tabPage_10.ResumeLayout(false);
			this.tabPage_10.PerformLayout();
			this.tabPage_9.ResumeLayout(false);
			this.tabPage_9.PerformLayout();
			this.tabPage_11.ResumeLayout(false);
			this.tabControl_2.ResumeLayout(false);
			this.tabPage_17.ResumeLayout(false);
			this.groupBox_20.ResumeLayout(false);
			this.groupBox_20.PerformLayout();
			this.tabPage_12.ResumeLayout(false);
			this.groupBox_10.ResumeLayout(false);
			this.groupBox_10.PerformLayout();
			((ISupportInitialize)this.numericUpDown_73).EndInit();
			((ISupportInitialize)this.numericUpDown_71).EndInit();
			((ISupportInitialize)this.numericUpDown_56).EndInit();
			((ISupportInitialize)this.numericUpDown_52).EndInit();
			((ISupportInitialize)this.trackBar_0).EndInit();
			((ISupportInitialize)this.numericUpDown_10).EndInit();
			((ISupportInitialize)this.numericUpDown_11).EndInit();
			((ISupportInitialize)this.numericUpDown_12).EndInit();
			((ISupportInitialize)this.numericUpDown_13).EndInit();
			((ISupportInitialize)this.numericUpDown_14).EndInit();
			this.tabPage_13.ResumeLayout(false);
			this.groupBox_11.ResumeLayout(false);
			this.groupBox_11.PerformLayout();
			((ISupportInitialize)this.numericUpDown_15).EndInit();
			((ISupportInitialize)this.numericUpDown_16).EndInit();
			((ISupportInitialize)this.numericUpDown_17).EndInit();
			((ISupportInitialize)this.numericUpDown_18).EndInit();
			((ISupportInitialize)this.numericUpDown_19).EndInit();
			((ISupportInitialize)this.numericUpDown_20).EndInit();
			((ISupportInitialize)this.numericUpDown_21).EndInit();
			((ISupportInitialize)this.numericUpDown_22).EndInit();
			this.tabPage_14.ResumeLayout(false);
			this.groupBox_12.ResumeLayout(false);
			this.groupBox_12.PerformLayout();
			((ISupportInitialize)this.numericUpDown_58).EndInit();
			((ISupportInitialize)this.numericUpDown_27).EndInit();
			((ISupportInitialize)this.numericUpDown_28).EndInit();
			((ISupportInitialize)this.numericUpDown_23).EndInit();
			((ISupportInitialize)this.numericUpDown_24).EndInit();
			this.tabPage_15.ResumeLayout(false);
			this.groupBox_13.ResumeLayout(false);
			this.groupBox_13.PerformLayout();
			((ISupportInitialize)this.numericUpDown_25).EndInit();
			this.tabPage_6.ResumeLayout(false);
			this.groupBox_24.ResumeLayout(false);
			this.groupBox_24.PerformLayout();
			((ISupportInitialize)this.dataGridView_0).EndInit();
			((ISupportInitialize)this.pictureBox_0).EndInit();
			this.groupBox_25.ResumeLayout(false);
			this.groupBox_25.PerformLayout();
			((ISupportInitialize)this.numericUpDown_35).EndInit();
			((ISupportInitialize)this.numericUpDown_30).EndInit();
			((ISupportInitialize)this.numericUpDown_29).EndInit();
			this.tabPage_21.ResumeLayout(false);
			this.tabControl_8.ResumeLayout(false);
			this.tabPage_25.ResumeLayout(false);
			this.tabPage_25.PerformLayout();
			this.groupBox_21.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_1).EndInit();
			this.groupBox_22.ResumeLayout(false);
			this.groupBox_22.PerformLayout();
			this.tabPage_34.ResumeLayout(false);
			this.tabPage_34.PerformLayout();
			((ISupportInitialize)this.numericUpDown_33).EndInit();
			((ISupportInitialize)this.numericUpDown_34).EndInit();
			this.groupBox_30.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_3).EndInit();
			this.groupBox_31.ResumeLayout(false);
			this.groupBox_31.PerformLayout();
			this.tabPage_33.ResumeLayout(false);
			this.tabPage_33.PerformLayout();
			((ISupportInitialize)this.numericUpDown_31).EndInit();
			((ISupportInitialize)this.numericUpDown_32).EndInit();
			this.groupBox_29.ResumeLayout(false);
			((ISupportInitialize)this.dataGridView_2).EndInit();
			this.tabPage_22.ResumeLayout(false);
			this.tabControl_7.ResumeLayout(false);
			this.tabPage_43.ResumeLayout(false);
			this.groupBox_46.ResumeLayout(false);
			this.groupBox_46.PerformLayout();
			((ISupportInitialize)this.numericUpDown_43).EndInit();
			((ISupportInitialize)this.numericUpDown_44).EndInit();
			((ISupportInitialize)this.numericUpDown_45).EndInit();
			((ISupportInitialize)this.fastObjectListView_11).EndInit();
			this.tabPage_44.ResumeLayout(false);
			this.groupBox_47.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_12).EndInit();
			this.tabPage_49.ResumeLayout(false);
			this.groupBox_60.ResumeLayout(false);
			this.tabPage_19.ResumeLayout(false);
			this.tabControl_11.ResumeLayout(false);
			this.tabPage_35.ResumeLayout(false);
			this.groupBox_45.ResumeLayout(false);
			this.groupBox_45.PerformLayout();
			((ISupportInitialize)this.trackBar_1).EndInit();
			this.groupBox_34.ResumeLayout(false);
			this.tabPage_36.ResumeLayout(false);
			this.groupBox_35.ResumeLayout(false);
			this.groupBox_35.PerformLayout();
			this.groupBox_36.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_6).EndInit();
			this.groupBox_37.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_7).EndInit();
			((ISupportInitialize)this.numericUpDown_36).EndInit();
			this.tabPage_37.ResumeLayout(false);
			this.groupBox_38.ResumeLayout(false);
			this.groupBox_38.PerformLayout();
			this.groupBox_39.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_8).EndInit();
			this.groupBox_40.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_9).EndInit();
			((ISupportInitialize)this.numericUpDown_37).EndInit();
			this.tabPage_48.ResumeLayout(false);
			this.groupBox_59.ResumeLayout(false);
			this.groupBox_59.PerformLayout();
			this.tabPage_40.ResumeLayout(false);
			this.groupBox_41.ResumeLayout(false);
			this.groupBox_41.PerformLayout();
			((ISupportInitialize)this.numericUpDown_38).EndInit();
			this.tabPage_41.ResumeLayout(false);
			this.groupBox_44.ResumeLayout(false);
			this.groupBox_44.PerformLayout();
			((ISupportInitialize)this.numericUpDown_39).EndInit();
			((ISupportInitialize)this.numericUpDown_40).EndInit();
			((ISupportInitialize)this.numericUpDown_41).EndInit();
			((ISupportInitialize)this.numericUpDown_42).EndInit();
			this.tabPage_20.ResumeLayout(false);
			this.groupBox_48.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_13).EndInit();
			this.groupBox_49.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_14).EndInit();
			this.groupBox_16.ResumeLayout(false);
			this.groupBox_16.PerformLayout();
			((ISupportInitialize)this.numericUpDown_49).EndInit();
			((ISupportInitialize)this.numericUpDown_50).EndInit();
			((ISupportInitialize)this.numericUpDown_51).EndInit();
			((ISupportInitialize)this.numericUpDown_46).EndInit();
			((ISupportInitialize)this.numericUpDown_48).EndInit();
			((ISupportInitialize)this.numericUpDown_47).EndInit();
			this.tabPage_55.ResumeLayout(false);
			this.tabControl_10.ResumeLayout(false);
			this.tabPage_56.ResumeLayout(false);
			this.groupBox_70.ResumeLayout(false);
			this.groupBox_70.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_23).EndInit();
			((ISupportInitialize)this.numericUpDown_76).EndInit();
			this.groupBox_71.ResumeLayout(false);
			this.groupBox_71.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_24).EndInit();
			this.tabPage_45.ResumeLayout(false);
			this.groupBox_56.ResumeLayout(false);
			this.groupBox_56.PerformLayout();
			((ISupportInitialize)this.numericUpDown_54).EndInit();
			((ISupportInitialize)this.numericUpDown_55).EndInit();
			this.groupBox_55.ResumeLayout(false);
			this.groupBox_55.PerformLayout();
			((ISupportInitialize)this.numericUpDown_53).EndInit();
			this.groupBox_54.ResumeLayout(false);
			this.groupBox_54.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_15).EndInit();
			this.tabPage_32.ResumeLayout(false);
			this.groupBox_27.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_5).EndInit();
			this.groupBox_28.ResumeLayout(false);
			this.groupBox_28.PerformLayout();
			((ISupportInitialize)this.numericUpDown_59).EndInit();
			this.tabPage_42.ResumeLayout(false);
			this.groupBox_43.ResumeLayout(false);
			this.groupBox_42.ResumeLayout(false);
			((ISupportInitialize)this.fastObjectListView_10).EndInit();
			this.tabPage_23.ResumeLayout(false);
			this.groupBox_17.ResumeLayout(false);
			this.groupBox_17.PerformLayout();
			((ISupportInitialize)this.numericUpDown_26).EndInit();
			((ISupportInitialize)this.fastObjectListView_1).EndInit();
			this.tabPage_46.ResumeLayout(false);
			this.groupBox_57.ResumeLayout(false);
			this.groupBox_57.PerformLayout();
			this.tabPage_47.ResumeLayout(false);
			this.groupBox_58.ResumeLayout(false);
			this.groupBox_58.PerformLayout();
			((ISupportInitialize)this.fastObjectListView_16).EndInit();
			this.toolStrip_0.ResumeLayout(false);
			this.toolStrip_0.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static MainForm()
		{
			Strings.CreateGetStringDelegate(typeof(MainForm));
			MainForm.string_10 = MainForm.getString_0(107396269);
			MainForm.regex_0 = new Regex(MainForm.getString_0(107363280));
			MainForm.regex_1 = new Regex(MainForm.getString_0(107363215));
			MainForm.regex_2 = new Regex(MainForm.getString_0(107363194));
			MainForm.regex_3 = new Regex(MainForm.getString_0(107363153));
			MainForm.regex_4 = new Regex(MainForm.getString_0(107362572));
			MainForm.regex_5 = new Regex(MainForm.getString_0(107362559));
			MainForm.regex_6 = new Regex(MainForm.getString_0(107362546));
			MainForm.regex_7 = new Regex(MainForm.getString_0(107362517));
			MainForm.regex_8 = new Regex(MainForm.getString_0(107362517));
			MainForm.regex_9 = new Regex(MainForm.getString_0(107362448));
			MainForm.regex_10 = new Regex(MainForm.getString_0(107362455));
		}

		[CompilerGenerated]
		private void method_150(string string_11, string string_12)
		{
			this.method_0(string_11, string_12);
		}

		[CompilerGenerated]
		private void method_151()
		{
			this.comboBox_56.Items.Clear();
			this.comboBox_56.Items.smethod_22(this.list_1);
		}

		[CompilerGenerated]
		private void method_152()
		{
			this.class299_0.RequiredPrefixes = Math.Min(this.fastObjectListView_7.Objects.Cast<AffixItemViewModel>().Count<AffixItemViewModel>(), this.comboBox_38.SelectedIndex + 1);
			this.class299_0.RequiredSuffixes = Math.Min(this.fastObjectListView_6.Objects.Cast<AffixItemViewModel>().Count<AffixItemViewModel>(), this.comboBox_35.SelectedIndex + 1);
			this.class299_0.Prefixes = this.fastObjectListView_7.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Mod>(MainForm.<>c.<>9.method_12)).ToList<Mod>();
			this.class299_0.PrefixTiers = this.fastObjectListView_7.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Tier>(MainForm.<>c.<>9.method_13)).ToList<Tier>();
			this.class299_0.Suffixes = this.fastObjectListView_6.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Mod>(MainForm.<>c.<>9.method_14)).ToList<Mod>();
			this.class299_0.SuffixTiers = this.fastObjectListView_6.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Tier>(MainForm.<>c.<>9.method_15)).ToList<Tier>();
			new Class384(this, this.class299_0).method_0();
		}

		[CompilerGenerated]
		private void method_153()
		{
			this.class299_0.RequiredPrefixes = Math.Min(this.fastObjectListView_9.Objects.Cast<AffixItemViewModel>().Count<AffixItemViewModel>(), this.comboBox_44.SelectedIndex + 1);
			this.class299_0.RequiredSuffixes = Math.Min(this.fastObjectListView_8.Objects.Cast<AffixItemViewModel>().Count<AffixItemViewModel>(), this.comboBox_41.SelectedIndex + 1);
			this.class299_0.Prefixes = this.fastObjectListView_9.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Mod>(MainForm.<>c.<>9.method_18)).ToList<Mod>();
			this.class299_0.PrefixTiers = this.fastObjectListView_9.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Tier>(MainForm.<>c.<>9.method_19)).ToList<Tier>();
			this.class299_0.Suffixes = this.fastObjectListView_8.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Mod>(MainForm.<>c.<>9.method_20)).ToList<Mod>();
			this.class299_0.SuffixTiers = this.fastObjectListView_8.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, Tier>(MainForm.<>c.<>9.method_21)).ToList<Tier>();
			new RareCrafter(this, this.class299_0).method_0();
		}

		[CompilerGenerated]
		private void method_154()
		{
			new Class383(this, this.class299_0).method_0();
		}

		[CompilerGenerated]
		private void method_155()
		{
			new SocketCrafter(this, this.class299_0).method_0();
		}

		[CompilerGenerated]
		private void method_156(string string_11, string string_12)
		{
			this.method_29(string_11, string_12);
		}

		[CompilerGenerated]
		private unsafe void method_157()
		{
			void* ptr = stackalloc byte[2];
			this.comboBox_1.SelectedValueChanged -= this.comboBox_1_SelectedValueChanged;
			ComboBox.ObjectCollection items = this.comboBox_1.Items;
			object[] items2 = API.smethod_1().ToArray();
			items.AddRange(items2);
			*(byte*)ptr = ((!string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.League))) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.comboBox_1.SelectedItem = Class255.class105_0.method_3(ConfigOptions.League);
			}
			else
			{
				this.comboBox_1.SelectedIndex = 0;
			}
			Class255.class105_0.method_9(ConfigOptions.League, this.comboBox_1.smethod_1(), true);
			this.comboBox_1.SelectedValueChanged += this.comboBox_1_SelectedValueChanged;
			this.method_86();
			((byte*)ptr)[1] = (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.UserAgent)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Class255.class105_0.method_9(ConfigOptions.UserAgent, API.smethod_11(), true);
			}
		}

		[DebuggerStepThrough]
		[CompilerGenerated]
		private Task method_158()
		{
			MainForm.Class98 @class = new MainForm.Class98();
			@class.mainForm_0 = this;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<MainForm.Class98>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		[CompilerGenerated]
		private void fastObjectListView_20_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_20.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_20.SelectedObject;
				this.fastObjectListView_20.RemoveObject(this.fastObjectListView_20.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.MessagesAfterTradeList, this.fastObjectListView_20.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_26)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_88_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (string.IsNullOrEmpty(this.textBox_20.Text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.class105_0.method_8<string>(ConfigOptions.MessagesAfterTradeList).Contains(this.textBox_20.Text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_20.AddObject(new ListItemViewModel(this.textBox_20.Text));
					this.textBox_20.Text = MainForm.getString_0(107396269);
					Class255.class105_0.method_9(ConfigOptions.MessagesAfterTradeList, this.fastObjectListView_20.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_27)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void checkBox_54_CheckedChanged(object sender, EventArgs e)
		{
			this.fastObjectListView_20.Enabled = this.checkBox_54.Checked;
		}

		[CompilerGenerated]
		private void fastObjectListView_21_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_21.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_21.SelectedObject;
				this.method_57(new Player(listItemViewModel.Text));
			}
		}

		[CompilerGenerated]
		private void button_89_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.textBox_21.Text))
			{
				this.method_56(new Player(this.textBox_21.Text), true);
				this.textBox_21.Text = MainForm.getString_0(107396269);
			}
		}

		[CompilerGenerated]
		private void checkBox_55_CheckedChanged(object sender, EventArgs e)
		{
			this.fastObjectListView_21.Enabled = this.checkBox_55.Checked;
		}

		[CompilerGenerated]
		private void fastObjectListView_0_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_0.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_0.SelectedObject;
				this.fastObjectListView_0.RemoveObject(this.fastObjectListView_0.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.DumpTabList, this.fastObjectListView_0.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_28)).ToList<int>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_4_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_2.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_2.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.DumpTabList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_0.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.DumpTabList, this.fastObjectListView_0.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_29)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_11_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_11.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_11.SelectedObject;
				this.fastObjectListView_11.RemoveObject(this.fastObjectListView_11.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.PriceStashList, this.fastObjectListView_11.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_30)).ToList<int>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_52_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_51.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_51.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.PriceStashList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_11.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.PriceStashList, this.fastObjectListView_11.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_31)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_1.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_1.SelectedObject;
				this.fastObjectListView_1.RemoveObject(this.fastObjectListView_1.SelectedObject);
			}
		}

		[CompilerGenerated]
		private unsafe void button_12_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_18.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_18.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						foreach (object obj in this.fastObjectListView_1.Objects)
						{
							JsonTab jsonTab2 = (JsonTab)obj;
							((byte*)ptr)[3] = ((jsonTab.i == jsonTab2.i) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								return;
							}
						}
						this.fastObjectListView_1.AddObject(jsonTab);
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_18_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_18.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_18.SelectedObject;
				jsonTab.IsExcluded = false;
				this.fastObjectListView_18.RemoveObject(this.fastObjectListView_18.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.ExcludedTabList, this.fastObjectListView_18.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_32)).ToList<int>(), true);
				this.method_84();
			}
		}

		[CompilerGenerated]
		private unsafe void button_86_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_69.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_69.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.ExcludedTabList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							jsonTab.IsExcluded = true;
							this.fastObjectListView_18.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.ExcludedTabList, this.fastObjectListView_18.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_33)).ToList<int>(), true);
							this.method_84();
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private unsafe void fastObjectListView_2_MouseDown(object sender, MouseEventArgs e)
		{
			void* ptr = stackalloc byte[7];
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_2.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_2.SelectedObject;
				*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.StashProfileSelected);
				((byte*)ptr)[4] = ((*(int*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) == 0)
				{
					this.fastObjectListView_2.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_34)).ToList<int>();
					Dictionary<int, List<int>> stashProfileTabs = Class255.StashProfileTabs;
					((byte*)ptr)[5] = ((!stashProfileTabs.ContainsKey(*(int*)ptr)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) == 0)
					{
						((byte*)ptr)[6] = ((!stashProfileTabs[*(int*)ptr].Contains(jsonTab.i)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) == 0)
						{
							stashProfileTabs[*(int*)ptr].Remove(jsonTab.i);
							this.fastObjectListView_2.RemoveObject(this.fastObjectListView_2.SelectedObject);
							Class255.class105_0.method_9(ConfigOptions.StashProfileTabs, stashProfileTabs, true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private unsafe void button_16_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[9];
			((byte*)ptr)[4] = ((this.comboBox_20.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_20.SelectedItem;
				((byte*)ptr)[5] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 5) == 0)
				{
					((byte*)ptr)[6] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) == 0)
					{
						*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.StashProfileSelected);
						Dictionary<int, List<int>> stashProfileTabs = Class255.StashProfileTabs;
						((byte*)ptr)[7] = ((!stashProfileTabs.ContainsKey(*(int*)ptr)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							stashProfileTabs.Add(*(int*)ptr, new List<int>());
						}
						((byte*)ptr)[8] = (stashProfileTabs[*(int*)ptr].Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) == 0)
						{
							stashProfileTabs[*(int*)ptr].Add(jsonTab.i);
							this.fastObjectListView_2.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.StashProfileTabs, stashProfileTabs, true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_3_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_3.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_3.SelectedObject;
				this.fastObjectListView_3.RemoveObject(this.fastObjectListView_3.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.AFKMessagesList, this.fastObjectListView_3.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_35)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_21_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (string.IsNullOrEmpty(this.textBox_11.Text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.class105_0.method_8<string>(ConfigOptions.AFKMessagesList).Contains(this.textBox_11.Text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_3.AddObject(new ListItemViewModel(this.textBox_11.Text));
					this.textBox_11.Text = MainForm.getString_0(107396269);
					Class255.class105_0.method_9(ConfigOptions.AFKMessagesList, this.fastObjectListView_3.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_36)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void checkBox_1_CheckedChanged(object sender, EventArgs e)
		{
			this.fastObjectListView_3.Enabled = this.checkBox_1.Checked;
		}

		[CompilerGenerated]
		private void fastObjectListView_4_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_4.SelectedObjects.Count > 0)
			{
				DecimalCurrencyListItem decimalCurrencyListItem = (DecimalCurrencyListItem)this.fastObjectListView_4.SelectedObject;
				this.fastObjectListView_4.RemoveObject(this.fastObjectListView_4.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.DecimalCurrencyList, this.fastObjectListView_4.Objects.Cast<DecimalCurrencyListItem>().ToList<DecimalCurrencyListItem>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_27_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			MainForm.Class39 @class = new MainForm.Class39();
			@class.string_0 = this.comboBox_29.smethod_1();
			string text = this.comboBox_28.smethod_1();
			*(byte*)ptr = ((@class.string_0 == text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.DecimalCurrencyList.Any(new Func<DecimalCurrencyListItem, bool>(@class.method_0)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_4.AddObject(new DecimalCurrencyListItem(@class.string_0, text));
					Class255.class105_0.method_9(ConfigOptions.DecimalCurrencyList, this.fastObjectListView_4.Objects.Cast<DecimalCurrencyListItem>().ToList<DecimalCurrencyListItem>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_5_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_5.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_5.SelectedObject;
				this.fastObjectListView_5.RemoveObject(this.fastObjectListView_5.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.VendorRecipeStashList, this.fastObjectListView_5.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_37)).ToList<int>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_30_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_30.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_30.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.VendorRecipeStashList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_5.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.VendorRecipeStashList, this.fastObjectListView_5.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_38)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_10_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_10.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_10.SelectedObject;
				this.fastObjectListView_10.RemoveObject(this.fastObjectListView_10.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.MuleStashList, this.fastObjectListView_10.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_39)).ToList<int>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_50_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_47.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_47.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.MuleStashList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_10.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.MuleStashList, this.fastObjectListView_10.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_40)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_17_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_17.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_17.SelectedObject;
				this.fastObjectListView_17.RemoveObject(this.fastObjectListView_17.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.AcceptedCurrencyList, this.fastObjectListView_17.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_41)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_84_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_68.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string text = this.comboBox_68.smethod_1();
				((byte*)ptr)[1] = (Class255.class105_0.method_8<string>(ConfigOptions.AcceptedCurrencyList).Contains(text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_17.AddObject(new ListItemViewModel(text));
					Class255.class105_0.method_9(ConfigOptions.AcceptedCurrencyList, this.fastObjectListView_17.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_42)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_12_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_12.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_12.SelectedObject;
				this.fastObjectListView_12.RemoveObject(this.fastObjectListView_12.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.BulkTypeList, this.fastObjectListView_12.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_43)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_55_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_52.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string text = this.comboBox_52.smethod_1();
				((byte*)ptr)[1] = (Class255.class105_0.method_8<string>(ConfigOptions.BulkTypeList).Contains(text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_12.AddObject(new ListItemViewModel(text));
					Class255.class105_0.method_9(ConfigOptions.BulkTypeList, this.fastObjectListView_12.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_44)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_14_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_14.SelectedObjects.Count > 0 && this.fastObjectListView_14.SelectedObject != null)
			{
				this.fastObjectListView_14.RemoveObject(this.fastObjectListView_14.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.MapPreventedMods, this.fastObjectListView_14.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, string>(MainForm.<>c.<>9.method_46)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_58_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_54.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				Mod mod = (Mod)this.comboBox_54.SelectedItem;
				((byte*)ptr)[1] = (Class255.MapPreventedModList.Contains(mod.ToString()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_14.AddObject(new AffixItemViewModel(mod, null));
					Class255.class105_0.method_9(ConfigOptions.MapPreventedMods, this.fastObjectListView_14.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, string>(MainForm.<>c.<>9.method_47)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_13_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_13.SelectedObjects.Count > 0 && this.fastObjectListView_13.SelectedObject != null)
			{
				this.fastObjectListView_13.RemoveObject(this.fastObjectListView_13.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.MapForcedMods, this.fastObjectListView_13.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, string>(MainForm.<>c.<>9.method_48)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_57_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_53.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				Mod mod = (Mod)this.comboBox_53.SelectedItem;
				((byte*)ptr)[1] = (Class255.MapForcedModList.Contains(mod.ToString()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_13.AddObject(new AffixItemViewModel(mod, null));
					Class255.class105_0.method_9(ConfigOptions.MapForcedMods, this.fastObjectListView_13.Objects.Cast<AffixItemViewModel>().Select(new Func<AffixItemViewModel, string>(MainForm.<>c.<>9.method_49)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_15_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_15.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_15.SelectedObject;
				this.fastObjectListView_15.RemoveObject(this.fastObjectListView_15.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.BeastStashList, this.fastObjectListView_15.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_52)).ToList<int>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_68_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_58.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_58.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.BeastStashList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_15.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.BeastStashList, this.fastObjectListView_15.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_53)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_19_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_19.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_19.SelectedObject;
				this.fastObjectListView_19.RemoveObject(this.fastObjectListView_19.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.SoldMessageList, this.fastObjectListView_19.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_54)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_87_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = (string.IsNullOrEmpty(this.textBox_19.Text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = (Class255.class105_0.method_8<string>(ConfigOptions.SoldMessageList).Contains(this.textBox_19.Text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_19.AddObject(new ListItemViewModel(this.textBox_19.Text));
					this.textBox_19.Text = MainForm.getString_0(107396269);
					Class255.class105_0.method_9(ConfigOptions.SoldMessageList, this.fastObjectListView_19.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_55)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void checkBox_53_CheckedChanged(object sender, EventArgs e)
		{
			this.fastObjectListView_19.Enabled = this.checkBox_53.Checked;
		}

		[CompilerGenerated]
		private void fastObjectListView_16_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_16.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_16.SelectedObject;
				this.fastObjectListView_16.RemoveObject(this.fastObjectListView_16.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.StackedDeckList, this.fastObjectListView_16.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_56)).ToList<int>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_74_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_61.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_61.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.StackedDeckList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_16.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.StackedDeckList, this.fastObjectListView_16.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_57)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_22_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_22.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_22.SelectedObject;
				this.fastObjectListView_22.RemoveObject(this.fastObjectListView_22.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.CheapChaosList, this.fastObjectListView_22.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_58)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_96_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_72.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				string text = this.comboBox_72.smethod_1();
				((byte*)ptr)[1] = (Class255.class105_0.method_8<string>(ConfigOptions.CheapChaosList).Contains(text) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_22.AddObject(new ListItemViewModel(text));
					Class255.class105_0.method_9(ConfigOptions.CheapChaosList, this.fastObjectListView_22.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_59)).ToList<string>(), true);
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_24_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_24.SelectedObjects.Count > 0)
			{
				ListItemViewModel listItemViewModel = (ListItemViewModel)this.fastObjectListView_24.SelectedObject;
				this.fastObjectListView_24.RemoveObject(this.fastObjectListView_24.SelectedObject);
				Class255.class105_0.method_9(ConfigOptions.GwennenItemList, this.fastObjectListView_24.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_60)).ToList<string>(), true);
			}
		}

		[CompilerGenerated]
		private unsafe void button_100_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			string text = this.comboBox_74.smethod_1();
			*(byte*)ptr = (Class255.class105_0.method_8<string>(ConfigOptions.GwennenItemList).Contains(text) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				((byte*)ptr)[1] = ((!ItemData.smethod_3().Contains(text)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					this.fastObjectListView_24.AddObject(new ListItemViewModel(text));
					Class255.class105_0.method_9(ConfigOptions.GwennenItemList, this.fastObjectListView_24.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_61)).ToList<string>(), true);
					this.Refresh();
				}
			}
		}

		[CompilerGenerated]
		private void fastObjectListView_23_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && e.X <= 18 && this.fastObjectListView_23.SelectedObjects.Count > 0)
			{
				JsonTab jsonTab = (JsonTab)this.fastObjectListView_23.SelectedObject;
				this.fastObjectListView_23.RemoveObject(this.fastObjectListView_23.SelectedObject);
			}
		}

		[CompilerGenerated]
		private unsafe void button_97_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = ((this.comboBox_73.SelectedItem == null) ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				JsonTab jsonTab = (JsonTab)this.comboBox_73.SelectedItem;
				((byte*)ptr)[1] = ((jsonTab == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					((byte*)ptr)[2] = ((Stashes.smethod_11(jsonTab.i) == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = (Class255.class105_0.method_8<int>(ConfigOptions.GwennenStashList).Contains(jsonTab.i) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							this.fastObjectListView_23.AddObject(jsonTab);
							Class255.class105_0.method_9(ConfigOptions.GwennenStashList, this.fastObjectListView_23.Objects.Cast<JsonTab>().Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_62)).ToList<int>(), true);
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private unsafe void method_159()
		{
			void* ptr = stackalloc byte[5];
			((byte*)ptr)[1] = ((!this.method_41()) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107362410));
			}
			else
			{
				this.method_42();
				this.method_52();
				((byte*)ptr)[2] = ((UI.smethod_81() != 1f) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) != 0)
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107362349));
				}
				*(byte*)ptr = 0;
				foreach (object obj in InputLanguage.InstalledInputLanguages)
				{
					InputLanguage inputLanguage = (InputLanguage)obj;
					((byte*)ptr)[3] = (inputLanguage.Culture.Name.StartsWith(MainForm.getString_0(107362639)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						*(byte*)ptr = 1;
						break;
					}
				}
				((byte*)ptr)[4] = ((*(sbyte*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107362634));
				}
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107362008));
			}
		}

		[CompilerGenerated]
		private void method_160()
		{
			this.label_98.Text = MainForm.getString_0(107397231);
			this.label_132.Text = MainForm.getString_0(107397231);
			this.label_134.Text = MainForm.getString_0(107397231);
			this.label_135.Text = MainForm.getString_0(107397231);
			this.list_0 = new List<string>
			{
				string.Empty
			};
			this.method_6();
		}

		[CompilerGenerated]
		private unsafe void method_161()
		{
			void* ptr = stackalloc byte[4];
			Class181.smethod_3(Enum11.const_2, MainForm.getString_0(107361891));
			Class181.smethod_5(Enum11.const_2, MainForm.getString_0(107361891));
			Win32.smethod_18();
			Win32.smethod_20();
			this.bool_12 = true;
			this.bool_4 = true;
			this.bool_17 = false;
			*(byte*)ptr = ((this.thread_1 != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				this.thread_1.Abort();
				this.thread_1 = null;
			}
			((byte*)ptr)[1] = ((this.thread_2 != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 1) != 0)
			{
				Stashes.LoadState = Enum9.const_0;
				this.thread_2.Abort();
				this.thread_2 = null;
			}
			((byte*)ptr)[2] = ((this.thread_5 != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 2) != 0)
			{
				this.thread_5.Abort();
				this.thread_5 = null;
			}
			((byte*)ptr)[3] = ((this.thread_4 != null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 3) != 0)
			{
				this.thread_4.Abort();
				this.thread_4 = null;
			}
			base.Invoke(new Action(this.method_162));
			this.method_64(true);
		}

		[CompilerGenerated]
		private void method_162()
		{
			this.toolStripProgressBar_0.Value = 0;
		}

		[CompilerGenerated]
		private void method_163()
		{
			this.label_98.Text = (this.list_7.Count(new Func<Order, bool>(MainForm.<>c.<>9.method_68)) + ((BuyOrderProcessor.order_0 != null) ? 1 : 0)).ToString();
		}

		[CompilerGenerated]
		private void method_164()
		{
			int num;
			if (int.TryParse(this.label_6.Text, out num))
			{
				Control control = this.label_6;
				int num2;
				num = (num2 = num + 1);
				control.Text = num2.ToString();
			}
		}

		[CompilerGenerated]
		private void method_165()
		{
			this.comboBox_5.Items.Clear();
			this.comboBox_6.Items.Clear();
			this.comboBox_7.Items.Clear();
			this.comboBox_8.Items.Clear();
			this.comboBox_9.Items.Clear();
			this.comboBox_4.Items.Clear();
			this.comboBox_2.Items.Clear();
			this.comboBox_3.Items.Clear();
			this.fastObjectListView_0.ClearObjects();
			this.comboBox_51.Items.Clear();
			this.fastObjectListView_11.ClearObjects();
			this.fastObjectListView_1.ClearObjects();
			this.comboBox_18.Items.Clear();
			this.comboBox_27.Items.Clear();
			this.fastObjectListView_18.ClearObjects();
			this.comboBox_69.Items.Clear();
			this.comboBox_23.Items.Clear();
			this.comboBox_22.Items.Clear();
			this.comboBox_21.Items.Clear();
			this.comboBox_16.Items.Clear();
			this.comboBox_15.Items.Clear();
			this.comboBox_14.Items.Clear();
			this.comboBox_73.Items.Clear();
			this.fastObjectListView_23.ClearObjects();
			this.comboBox_30.Items.Clear();
			this.fastObjectListView_5.ClearObjects();
			this.comboBox_34.Items.Clear();
			this.comboBox_47.Items.Clear();
			this.fastObjectListView_10.ClearObjects();
			this.comboBox_48.Items.Clear();
			this.comboBox_71.Items.Clear();
			this.comboBox_17.Items.Clear();
			this.comboBox_58.Items.Clear();
			this.fastObjectListView_15.ClearObjects();
			this.comboBox_61.Items.Clear();
			this.fastObjectListView_16.ClearObjects();
			this.comboBox_63.Items.Clear();
			this.comboBox_64.Items.Clear();
		}

		[CompilerGenerated]
		private void method_166()
		{
			this.comboBox_5.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.CurrencyDumpTab), false);
			this.comboBox_9.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.MapsDumpTab), false);
			this.comboBox_8.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.FragmentsDumpTab), false);
			this.comboBox_7.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.DelveDumpTab), false);
			this.comboBox_6.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.EssenceDumpTab), false);
			this.comboBox_4.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.OtherDumpTab), false);
			this.comboBox_3.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.CardDumpTab), false);
			this.comboBox_26.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.HarvestDumpTab), false);
			this.comboBox_23.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.DeliriumDumpTab), false);
			this.comboBox_22.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.MetamorphDumpTab), false);
			this.comboBox_21.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.BlightDumpTab), false);
			this.comboBox_64.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.UltimatumDumpTab), false);
			this.comboBox_16.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.IncubatorDumpTab), false);
			this.comboBox_15.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.VialDumpTab), false);
			this.comboBox_14.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.StackedDeckDumpTab), false);
			if (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.CraftCurrencyTab)) && Stashes.smethod_9(MainForm.getString_0(107393182)).Any<JsonTab>())
			{
				Class255.class105_0.method_9(ConfigOptions.CraftCurrencyTab, Stashes.smethod_9(MainForm.getString_0(107393182)).First<JsonTab>().n, true);
			}
			this.comboBox_48.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.CraftCurrencyTab), false);
			this.comboBox_71.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.CraftMoreItemsTab), false);
			this.comboBox_17.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.MapCraftStashTab), false);
			this.comboBox_63.SelectedItem = Stashes.smethod_7(Class255.class105_0.method_3(ConfigOptions.VaalCraftStash), false);
			this.comboBox_27.SelectedItem = Stashes.smethod_13();
		}

		[CompilerGenerated]
		private void method_167()
		{
			this.class256_0.jsonTab_0 = (JsonTab)this.comboBox_5.SelectedItem;
			this.class256_0.jsonTab_1 = (JsonTab)this.comboBox_9.SelectedItem;
			this.class256_0.jsonTab_2 = (JsonTab)this.comboBox_8.SelectedItem;
			this.class256_0.jsonTab_3 = (JsonTab)this.comboBox_7.SelectedItem;
			this.class256_0.jsonTab_4 = (JsonTab)this.comboBox_6.SelectedItem;
			this.class256_0.jsonTab_5 = (JsonTab)this.comboBox_4.SelectedItem;
			this.class256_0.jsonTab_6 = (JsonTab)this.comboBox_3.SelectedItem;
			this.class256_0.class257_0.jsonTab_6 = (JsonTab)this.comboBox_26.SelectedItem;
			this.class256_0.class257_0.jsonTab_0 = (JsonTab)this.comboBox_23.SelectedItem;
			this.class256_0.class257_0.jsonTab_1 = (JsonTab)this.comboBox_22.SelectedItem;
			this.class256_0.class257_0.jsonTab_2 = (JsonTab)this.comboBox_21.SelectedItem;
			this.class256_0.class257_0.jsonTab_7 = (JsonTab)this.comboBox_64.SelectedItem;
			this.class256_0.class257_0.jsonTab_3 = (JsonTab)this.comboBox_16.SelectedItem;
			this.class256_0.class257_0.jsonTab_4 = (JsonTab)this.comboBox_15.SelectedItem;
			this.class256_0.class257_0.jsonTab_5 = (JsonTab)this.comboBox_14.SelectedItem;
		}

		[CompilerGenerated]
		private void method_168(Player player_0)
		{
			this.method_57(player_0);
		}

		[CompilerGenerated]
		private unsafe void method_169()
		{
			void* ptr = stackalloc byte[10];
			((byte*)ptr)[8] = ((!string.IsNullOrEmpty(this.string_5)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				PingReply pingReply = new Ping().Send(IPAddress.Parse(this.string_5));
				((byte*)ptr)[9] = ((pingReply.Status == IPStatus.Success) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					Enum11 enum11_ = Enum11.const_3;
					string str = MainForm.getString_0(107361878);
					*(long*)ptr = pingReply.RoundtripTime;
					Class181.smethod_3(enum11_, str + ((long*)ptr)->ToString());
					this.long_0 = pingReply.RoundtripTime * 2L;
				}
				else
				{
					Class181.smethod_3(Enum11.const_3, MainForm.getString_0(107361853) + pingReply.Status.ToString());
				}
			}
		}

		[CompilerGenerated]
		private void method_170()
		{
			this.tabControl_4.SelectedTab = this.tabControl_4.TabPages[0];
			this.Refresh();
		}

		[CompilerGenerated]
		private void method_171()
		{
			this.fastObjectListView_11.ClearObjects();
		}

		[CompilerGenerated]
		private void method_172()
		{
			this.enum8_0 = Enum8.const_1;
			this.button_54.Enabled = true;
		}

		[CompilerGenerated]
		private void method_173()
		{
			this.button_11.Enabled = true;
		}

		[CompilerGenerated]
		private void method_174()
		{
			this.tabControl_4.SelectedTab = this.tabControl_4.TabPages[0];
			this.Refresh();
		}

		[CompilerGenerated]
		private void method_175()
		{
			this.button_11.Enabled = true;
		}

		[CompilerGenerated]
		private void method_176()
		{
			try
			{
				base.Invoke(new Action(this.method_177));
			}
			catch
			{
			}
		}

		[DebuggerStepThrough]
		[CompilerGenerated]
		private void method_177()
		{
			MainForm.Class99 @class = new MainForm.Class99();
			@class.mainForm_0 = this;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<MainForm.Class99>(ref @class);
		}

		[CompilerGenerated]
		private bool method_178(Character character_0)
		{
			return character_0.league == this.string_7;
		}

		[CompilerGenerated]
		private unsafe void method_179()
		{
			void* ptr = stackalloc byte[12];
			((byte*)ptr)[8] = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				this.tabControl_4.SelectedTab = this.tabControl_4.TabPages[0];
				this.Refresh();
			}
			else
			{
				this.comboBox_20.Items.Clear();
				ComboBox.ObjectCollection items = this.comboBox_20.Items;
				object[] items2 = Stashes.Tabs.ToArray();
				items.AddRange(items2);
				this.comboBox_20.SelectedIndex = 0;
				this.fastObjectListView_2.ClearObjects();
				Dictionary<int, List<int>> stashProfileTabs = Class255.StashProfileTabs;
				*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.StashProfileSelected);
				((byte*)ptr)[9] = ((!stashProfileTabs.ContainsKey(*(int*)ptr)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 9) != 0)
				{
					stashProfileTabs.Add(*(int*)ptr, new List<int>());
				}
				((byte*)ptr)[10] = ((*(int*)ptr == 0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 10) != 0)
				{
					stashProfileTabs[*(int*)ptr].Clear();
					stashProfileTabs[*(int*)ptr].AddRange(Stashes.Tabs.Select(new Func<JsonTab, int>(MainForm.<>c.<>9.method_131)));
				}
				foreach (int num in stashProfileTabs[*(int*)ptr])
				{
					*(int*)((byte*)ptr + 4) = num;
					JsonTab jsonTab = Stashes.smethod_11(*(int*)((byte*)ptr + 4));
					((byte*)ptr)[11] = ((jsonTab != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 11) != 0)
					{
						this.fastObjectListView_2.AddObject(jsonTab);
					}
				}
				this.fastObjectListView_2.Refresh();
				this.Refresh();
				Class255.class105_0.method_9(ConfigOptions.StashProfileTabs, stashProfileTabs, true);
			}
		}

		[DebuggerStepThrough]
		[CompilerGenerated]
		private Task method_180()
		{
			MainForm.Class100 @class = new MainForm.Class100();
			@class.mainForm_0 = this;
			@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
			asyncTaskMethodBuilder_.Start<MainForm.Class100>(ref @class);
			return @class.asyncTaskMethodBuilder_0.Task;
		}

		[CompilerGenerated]
		private unsafe void method_181()
		{
			void* ptr = stackalloc byte[13];
			if (!this.bool_16 && this.genum0_0 == MainForm.GEnum0.const_2)
			{
				this.bool_16 = true;
				bool flag;
				if (this.int_4 == 0)
				{
					flag = this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_133));
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					this.bool_16 = false;
				}
				else
				{
					((byte*)ptr)[4] = ((Class255.ItemBuyingList.Count <= this.int_4) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						if (!this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_134)))
						{
							this.method_114();
						}
						this.bool_16 = false;
					}
					else
					{
						ItemBuyingListItem itemBuyingListItem = Class255.ItemBuyingList[this.int_4];
						((byte*)ptr)[5] = ((itemBuyingListItem == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							this.method_114();
							this.bool_16 = false;
						}
						else
						{
							this.int_4++;
							((byte*)ptr)[6] = ((!itemBuyingListItem.Enabled) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 6) != 0)
							{
								this.bool_16 = false;
								this.method_94();
							}
							else
							{
								((byte*)ptr)[7] = ((itemBuyingListItem.MaxPriceAmount <= 0m) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 7) != 0)
								{
									Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107362320), new object[]
									{
										itemBuyingListItem.Description
									});
									this.bool_16 = false;
									this.method_94();
								}
								else
								{
									Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107362255), new object[]
									{
										itemBuyingListItem.Id
									});
									itemBuyingListItem.Query = Class145.smethod_0(string.Format(MainForm.getString_0(107362222), Class103.TradeWebsiteUrl, itemBuyingListItem.Id), itemBuyingListItem.MaxPrice);
									((byte*)ptr)[8] = ((itemBuyingListItem.Query == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 8) != 0)
									{
										Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107362209), new object[]
										{
											itemBuyingListItem.Id,
											itemBuyingListItem.Description
										});
										this.bool_16 = false;
									}
									else
									{
										Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107362148), new object[]
										{
											itemBuyingListItem.Id,
											itemBuyingListItem.Description,
											itemBuyingListItem.Query
										});
										((byte*)ptr)[9] = ((itemBuyingListItem.FetchList == null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 9) != 0)
										{
											Class271 @class = Web.smethod_13(itemBuyingListItem.Query);
											Thread.Sleep(5500);
											((byte*)ptr)[10] = ((@class == null) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 10) != 0)
											{
												Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107362127), new object[]
												{
													itemBuyingListItem.Id,
													itemBuyingListItem.Description
												});
												Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107362106), new object[]
												{
													itemBuyingListItem.Query
												});
												this.bool_16 = false;
												return;
											}
											((byte*)ptr)[11] = ((!@class.result.Any<string>()) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 11) != 0)
											{
												Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107361545), new object[]
												{
													itemBuyingListItem.Id,
													itemBuyingListItem.Description
												});
												this.bool_16 = false;
												return;
											}
											itemBuyingListItem.FetchList = new List<string>(@class.result);
										}
										*(int*)ptr = 0;
										for (;;)
										{
											IEnumerable<string> enumerable = itemBuyingListItem.FetchList.Take(10);
											Stream stream = Web.smethod_9(enumerable, itemBuyingListItem.Id, false);
											Thread.Sleep(4000);
											((byte*)ptr)[12] = ((stream == null) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 12) != 0)
											{
												break;
											}
											using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
											{
												Class276 class2 = JsonConvert.DeserializeObject<Class276>(streamReader.ReadToEnd());
												foreach (FetchTradeResult fetchTradeResult_ in class2.result)
												{
													BuyingTradeData buyingTradeData = Class151.smethod_0(fetchTradeResult_, itemBuyingListItem.Id, TradeTypes.ItemBuying, Class255.ItemBuyingList.IndexOf(itemBuyingListItem));
													if (buyingTradeData != null && !(buyingTradeData.CharacterName == this.CharacterName))
													{
														*(int*)ptr = *(int*)ptr + 1;
														Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361471), new object[]
														{
															buyingTradeData
														});
														this.list_10.Add(buyingTradeData);
													}
												}
												itemBuyingListItem.FetchList.RemoveRange(0, enumerable.Count<string>());
												this.method_124(TradeTypes.ItemBuying);
											}
											if (!itemBuyingListItem.FetchList.Any<string>() || this.genum0_0 != MainForm.GEnum0.const_2)
											{
												goto IL_4BF;
											}
										}
										Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107361524), new object[]
										{
											itemBuyingListItem.Id
										});
										this.bool_16 = false;
										return;
										IL_4BF:
										Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107361410), new object[]
										{
											*(int*)ptr,
											itemBuyingListItem.Id,
											itemBuyingListItem.Description
										});
										this.bool_16 = false;
									}
								}
							}
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private unsafe void method_182()
		{
			void* ptr = stackalloc byte[14];
			if (!this.bool_16 && this.genum0_0 == MainForm.GEnum0.const_2)
			{
				this.bool_16 = true;
				bool flag;
				if (this.int_5 == 0)
				{
					flag = this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_135));
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					this.bool_16 = false;
				}
				else
				{
					((byte*)ptr)[4] = ((Class255.BulkBuyingList.Count <= this.int_5) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						if (!this.list_10.Any(new Func<BuyingTradeData, bool>(MainForm.<>c.<>9.method_136)))
						{
							this.method_115();
						}
						this.bool_16 = false;
					}
					else
					{
						BulkBuyingListItem bulkBuyingListItem = Class255.BulkBuyingList[this.int_5];
						((byte*)ptr)[5] = ((bulkBuyingListItem == null) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 5) != 0)
						{
							this.method_115();
							this.bool_16 = false;
						}
						else
						{
							this.int_5++;
							((byte*)ptr)[6] = ((!bulkBuyingListItem.Enabled) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 6) != 0)
							{
								this.bool_16 = false;
								this.method_95();
							}
							else
							{
								((byte*)ptr)[7] = ((bulkBuyingListItem.MaxPriceAmount <= 0m) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 7) != 0)
								{
									Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107362320), new object[]
									{
										bulkBuyingListItem
									});
									this.bool_16 = false;
									this.method_95();
								}
								else
								{
									((byte*)ptr)[8] = ((bulkBuyingListItem.FetchList == null) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 8) != 0)
									{
										Class266 @class = Web.smethod_14(bulkBuyingListItem.Query);
										Thread.Sleep(7300);
										((byte*)ptr)[9] = ((@class == null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 9) != 0)
										{
											Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107361397), new object[]
											{
												bulkBuyingListItem
											});
											Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107362106), new object[]
											{
												bulkBuyingListItem.Query
											});
											this.bool_16 = false;
											return;
										}
										((byte*)ptr)[10] = ((!@class.result.Any<FetchTradeResult>()) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 10) != 0)
										{
											Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107361320), new object[]
											{
												bulkBuyingListItem
											});
											this.bool_16 = false;
											return;
										}
										bulkBuyingListItem.QueryId = @class.id;
										bulkBuyingListItem.FetchList = new List<FetchTradeResult>(@class.result);
									}
									*(int*)ptr = 0;
									IEnumerable<FetchTradeResult> enumerable;
									do
									{
										enumerable = bulkBuyingListItem.FetchList.Take(10);
										IEnumerable<string> ienumerable_ = enumerable.Select(new Func<FetchTradeResult, string>(MainForm.<>c.<>9.method_137));
										Stream stream = Web.smethod_9(ienumerable_, bulkBuyingListItem.QueryId, false);
										Thread.Sleep(4000);
										((byte*)ptr)[11] = ((stream == null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 11) != 0)
										{
											goto IL_47C;
										}
										using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
										{
											Class276 class2 = JsonConvert.DeserializeObject<Class276>(streamReader.ReadToEnd());
											using (IEnumerator<FetchTradeResult> enumerator = enumerable.GetEnumerator())
											{
												while (enumerator.MoveNext())
												{
													MainForm.Class80 class3 = new MainForm.Class80();
													class3.fetchTradeResult_0 = enumerator.Current;
													BuyingTradeData buyingTradeData = Class151.smethod_0(class3.fetchTradeResult_0, bulkBuyingListItem.QueryId, TradeTypes.BulkBuying, Class255.BulkBuyingList.IndexOf(bulkBuyingListItem));
													if (buyingTradeData != null && !(buyingTradeData.CharacterName == this.CharacterName))
													{
														FetchTradeResult fetchTradeResult = class2.result.FirstOrDefault(new Func<FetchTradeResult, bool>(class3.method_0));
														((byte*)ptr)[12] = ((fetchTradeResult == null) ? 1 : 0);
														if (*(sbyte*)((byte*)ptr + 12) == 0)
														{
															buyingTradeData.Item = fetchTradeResult.item;
															((byte*)ptr)[13] = ((buyingTradeData.PricePerItem > bulkBuyingListItem.MaxPriceAmount) ? 1 : 0);
															if (*(sbyte*)((byte*)ptr + 13) != 0)
															{
																Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361774), new object[]
																{
																	buyingTradeData.PricePerItem,
																	bulkBuyingListItem.MaxPriceAmount
																});
																bulkBuyingListItem.FetchList.Clear();
																break;
															}
															*(int*)ptr = *(int*)ptr + 1;
															Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361745), new object[]
															{
																buyingTradeData
															});
															this.list_10.Add(buyingTradeData);
														}
													}
												}
											}
											bulkBuyingListItem.FetchList.RemoveRange(0, enumerable.Count<FetchTradeResult>());
											this.method_124(TradeTypes.BulkBuying);
										}
									}
									while (bulkBuyingListItem.FetchList.Any<FetchTradeResult>() && this.genum0_0 == MainForm.GEnum0.const_2);
									Class181.smethod_2(Enum11.const_0, MainForm.getString_0(107361716), new object[]
									{
										*(int*)ptr,
										bulkBuyingListItem
									});
									this.bool_16 = false;
									return;
									IL_47C:
									Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107361524), new object[]
									{
										bulkBuyingListItem
									});
									Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361819), new object[]
									{
										bulkBuyingListItem.Query,
										string.Join<FetchTradeResult>(MainForm.getString_0(107396306), enumerable)
									});
									this.bool_16 = false;
								}
							}
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void method_183()
		{
			if (this.thread_0 != null)
			{
				this.thread_0.Abort();
				this.thread_0 = null;
			}
		}

		[CompilerGenerated]
		private unsafe void method_184()
		{
			void* ptr = stackalloc byte[9];
			*(double*)ptr = Math.Round(this.NextFlipUpdateTime.Subtract(DateTime.Now).TotalMinutes);
			((byte*)ptr)[8] = ((*(double*)ptr < 0.0) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				this.toolStripLabel_2.Text = MainForm.getString_0(107361647);
			}
			else
			{
				this.toolStripLabel_2.Text = string.Format(MainForm.getString_0(107361606), *(double*)ptr, (*(double*)ptr == 1.0) ? MainForm.getString_0(107361597) : MainForm.getString_0(107384920));
			}
		}

		[CompilerGenerated]
		private unsafe void method_185()
		{
			void* ptr = stackalloc byte[2];
			foreach (DecimalCurrencyListItem decimalCurrencyListItem in Class255.DecimalCurrencyList)
			{
				Dictionary<string, decimal> dictionary = Pricing.smethod_6(API.smethod_4(decimalCurrencyListItem.DecimalType), API.smethod_4(decimalCurrencyListItem.Currency), 1, 4, 2);
				*(byte*)ptr = (dictionary.ContainsKey(MainForm.getString_0(107361588)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					decimal value = dictionary.First<KeyValuePair<string, decimal>>().Value;
					((byte*)ptr)[1] = ((value < 1m) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						decimalCurrencyListItem.Value = value;
						Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361039), new object[]
						{
							decimalCurrencyListItem,
							value
						});
					}
					else
					{
						Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107361006), new object[]
						{
							decimalCurrencyListItem,
							decimalCurrencyListItem.DecimalType,
							decimalCurrencyListItem.Currency,
							value
						});
						decimalCurrencyListItem.Value = 0m;
					}
				}
				else
				{
					Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107360909), new object[]
					{
						decimalCurrencyListItem
					});
					decimalCurrencyListItem.Value = 0m;
				}
			}
			this.bool_15 = false;
		}

		[CompilerGenerated]
		private void method_186()
		{
			VendorRecipe.smethod_0(this);
		}

		[CompilerGenerated]
		private void method_187()
		{
			this.button_29.Enabled = true;
		}

		[CompilerGenerated]
		private void method_188()
		{
			this.tabControl_4.SelectedIndex = 0;
			this.Refresh();
		}

		[CompilerGenerated]
		private void method_189()
		{
			this.tabControl_4.SelectedIndex = this.tabControl_4.TabPages.IndexOf(this.tabPage_42);
			this.Refresh();
		}

		[CompilerGenerated]
		private void method_190()
		{
			this.button_49.Enabled = true;
			this.button_49.Text = MainForm.getString_0(107388558);
			this.method_64(true);
			Class181.smethod_5(Enum11.const_2, MainForm.getString_0(107360868));
		}

		[CompilerGenerated]
		private void method_191()
		{
			if (this.genum0_0 == MainForm.GEnum0.const_2)
			{
				this.bool_19 = true;
				this.button_56.Enabled = false;
				Class181.smethod_3(Enum11.const_0, MainForm.getString_0(107360839));
			}
		}

		[CompilerGenerated]
		private void method_192()
		{
			JsonTab tab = (JsonTab)this.comboBox_17.SelectedItem;
			new MapCrafter(this, tab).method_0();
		}

		[CompilerGenerated]
		private void method_193()
		{
			this.toolStripProgressBar_0.Value = 0;
		}

		[CompilerGenerated]
		private void method_194()
		{
			MainForm.IsPaused = !MainForm.IsPaused;
			this.button_65.Text = (MainForm.IsPaused ? MainForm.getString_0(107361286) : MainForm.getString_0(107389320));
			this.textBox_17.Enabled = MainForm.IsPaused;
			string text = MainForm.IsPaused ? MainForm.getString_0(107361272) : MainForm.getString_0(107361305);
			Class181.smethod_3(Enum11.const_0, text);
			Class123.smethod_15();
			if (!MainForm.IsPaused)
			{
				this.method_116();
				UI.smethod_1();
				Thread.Sleep(300);
				Win32.smethod_11();
				Thread.Sleep(200);
			}
		}

		[CompilerGenerated]
		private void method_195()
		{
			this.button_65.Enabled = true;
			this.button_65.Text = MainForm.getString_0(107389320);
		}

		[CompilerGenerated]
		private void method_196()
		{
			this.button_65.Enabled = false;
		}

		[CompilerGenerated]
		private void method_197()
		{
			BeastCapture.smethod_0(this);
		}

		[CompilerGenerated]
		private void method_198()
		{
			StackedDeckOpener.smethod_0(this);
		}

		[CompilerGenerated]
		private void method_199()
		{
			this.form1_0.ChatBox.SelectionStart = this.form1_0.ChatBox.Text.Length;
			this.form1_0.ChatBox.ScrollToCaret();
		}

		[CompilerGenerated]
		private void method_200()
		{
			if (this.form4_0 != null && !this.form4_0.IsDisposed)
			{
				this.form4_0.method_1();
			}
		}

		[CompilerGenerated]
		private unsafe void method_201()
		{
			void* ptr = stackalloc byte[2];
			if (this.genum0_0 == MainForm.GEnum0.const_2 && Class255.LiveSearchEnabled)
			{
				using (Dictionary<string, Class260>.Enumerator enumerator = this.dictionary_2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MainForm.Class97 @class = new MainForm.Class97();
						@class.keyValuePair_0 = enumerator.Current;
						try
						{
							Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361131), new object[]
							{
								@class.keyValuePair_0.Key
							});
							LiveSearchListItem liveSearchListItem = Class255.LiveSearchList.FirstOrDefault(new Func<LiveSearchListItem, bool>(@class.method_0));
							*(byte*)ptr = ((liveSearchListItem == null) ? 1 : 0);
							if (*(sbyte*)ptr != 0)
							{
								Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361138), new object[]
								{
									@class.keyValuePair_0.Key
								});
							}
							else
							{
								((byte*)ptr)[1] = (liveSearchListItem.Enabled ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 1) != 0)
								{
									this.method_147(liveSearchListItem, @class.keyValuePair_0.Value);
								}
								else
								{
									Class181.smethod_2(Enum11.const_3, MainForm.getString_0(107361109), new object[]
									{
										@class.keyValuePair_0.Key
									});
								}
							}
						}
						catch (Exception ex)
						{
							Class181.smethod_2(Enum11.const_2, MainForm.getString_0(107361080), new object[]
							{
								@class.keyValuePair_0.Key,
								ex
							});
						}
					}
				}
			}
		}

		[CompilerGenerated]
		private void method_202()
		{
			Gwennen.smethod_0(this);
		}

		[CompilerGenerated]
		private void method_203()
		{
			this.button_99.Enabled = true;
		}

		private List<string> list_0 = new List<string>();

		private List<string> list_1 = new List<string>
		{
			string.Empty
		};

		private int int_0 = 0;

		private const char char_0 = '\b';

		private Dictionary<string, Class249> dictionary_0 = new Dictionary<string, Class249>();

		private Thread thread_0;

		public Thread thread_1;

		public Thread thread_2;

		public Thread thread_3;

		private Thread thread_4;

		public Thread thread_5;

		public GameProcessState gameProcessState_0;

		public ExpectedState expectedState_0;

		public Dictionary<string, Dictionary<string, object>> dictionary_1 = new Dictionary<string, Dictionary<string, object>>();

		public List<Order> list_2 = new List<Order>();

		public List<Order> list_3 = new List<Order>();

		public List<Player> list_4 = new List<Player>();

		public Class256 class256_0 = new Class256();

		public List<string> list_5 = new List<string>();

		public Stopwatch stopwatch_0;

		public int int_1 = 0;

		public bool bool_0 = false;

		private bool bool_1 = false;

		public int int_2 = 0;

		public string string_0 = MainForm.getString_0(107395058);

		public string string_1 = MainForm.getString_0(107395021);

		public string string_2 = MainForm.getString_0(107395016);

		public string string_3 = MainForm.getString_0(107395011);

		public string string_4 = MainForm.getString_0(107395038);

		public long long_0 = 0L;

		public string string_5 = MainForm.getString_0(107396269);

		public bool bool_2 = false;

		public bool bool_3 = true;

		private bool bool_4 = false;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool bool_5;

		public List<string> list_6 = new List<string>();

		public List<Order> list_7 = new List<Order>();

		public List<Order> list_8 = new List<Order>();

		public List<Order> list_9 = new List<Order>();

		public List<BuyingTradeData> list_10 = new List<BuyingTradeData>();

		public List<TradeFetchId> list_11 = new List<TradeFetchId>();

		public Dictionary<string, Class260> dictionary_2 = new Dictionary<string, Class260>();

		public List<Player> list_12 = new List<Player>();

		public List<Player> list_13 = new List<Player>();

		public List<Item> list_14 = new List<Item>();

		private Random random_0 = new Random();

		public bool bool_6 = false;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_6;

		public Enum13 enum13_0 = Enum13.const_0;

		public bool bool_7 = false;

		private Order order_0;

		private object object_0 = new object();

		private object object_1 = new object();

		public List<Player> list_15 = new List<Player>();

		private bool bool_8 = false;

		private bool bool_9 = false;

		private DateTime dateTime_0;

		private DateTime dateTime_1;

		private bool bool_10 = false;

		private DateTime dateTime_2;

		private DateTime dateTime_3;

		private Random random_1 = new Random();

		public bool bool_11 = false;

		private Class242 class242_0;

		private Class242 class242_1;

		private Class242 class242_2;

		private string string_7 = MainForm.getString_0(107396269);

		private string string_8 = MainForm.getString_0(107396269);

		private string string_9 = MainForm.getString_0(107396269);

		public MainForm.GEnum0 genum0_0 = MainForm.GEnum0.const_0;

		public Enum8 enum8_0 = Enum8.const_1;

		public bool bool_12 = false;

		private bool bool_13 = false;

		private bool bool_14 = false;

		private DateTime dateTime_4 = default(DateTime);

		private DateTime dateTime_5 = DateTime.Now;

		public List<string> list_16 = new List<string>();

		public Dictionary<string, List<JsonItem>> dictionary_3 = new Dictionary<string, List<JsonItem>>();

		private bool bool_15 = false;

		private object object_2 = new object();

		private bool bool_16 = false;

		private int int_3 = 0;

		private DateTime dateTime_6 = default(DateTime);

		private DateTime dateTime_7 = default(DateTime);

		private int int_4 = 0;

		private int int_5 = 0;

		private int int_6 = -1;

		private Label label_0;

		private bool bool_17;

		private bool bool_18 = false;

		public bool bool_19 = false;

		private Class299 class299_0 = new Class299();

		public Form1 form1_0;

		private DebugPanel debugPanel_0;

		private Form2 form2_0;

		private Form4 form4_0;

		private bool bool_20 = false;

		private bool bool_21 = false;

		private bool bool_22 = false;

		public List<string> list_17 = new List<string>();

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Dictionary<string, DateTime> dictionary_4;

		public List<string> list_18 = new List<string>();

		public List<string> list_19 = new List<string>();

		public Dictionary<string, double> dictionary_5 = new Dictionary<string, double>();

		public DateTime dateTime_8;

		public static string string_10;

		private static Regex regex_0;

		private static Regex regex_1;

		private static Regex regex_2;

		private static Regex regex_3;

		private static Regex regex_4;

		private static Regex regex_5;

		private static Regex regex_6;

		private static Regex regex_7;

		private static Regex regex_8;

		private static Regex regex_9;

		private static Regex regex_10;

		private IContainer icontainer_0 = null;

		private TabPage tabPage_0;

		public Button button_0;

		public RichTextBox richTextBox_0;

		private TabPage tabPage_1;

		private Timer timer_0;

		public Button button_1;

		private Timer timer_1;

		private ToolStrip toolStrip_0;

		private ToolStripLabel toolStripLabel_0;

		private ToolStripSeparator toolStripSeparator_0;

		private ToolStripLabel toolStripLabel_1;

		private ToolStripSeparator toolStripSeparator_1;

		private ToolTip toolTip_0;

		public Button button_2;

		private GroupBox groupBox_0;

		private GroupBox groupBox_1;

		private Label label_1;

		private Label label_2;

		private Label label_3;

		private Label label_4;

		private Label label_5;

		private Label label_6;

		private GroupBox groupBox_2;

		private Label label_7;

		private Label label_8;

		private Label label_9;

		private TabControl tabControl_0;

		private TabPage tabPage_2;

		private GroupBox groupBox_3;

		private Label label_10;

		private Label label_11;

		private TabPage tabPage_3;

		private GroupBox groupBox_4;

		private GroupBox groupBox_5;

		public CheckBox checkBox_0;

		public NumericUpDown numericUpDown_0;

		public NumericUpDown numericUpDown_1;

		public NumericUpDown numericUpDown_2;

		public NumericUpDown numericUpDown_3;

		private GroupBox groupBox_6;

		public CheckBox checkBox_1;

		private Label label_12;

		public NumericUpDown numericUpDown_4;

		private Label label_13;

		public NumericUpDown numericUpDown_5;

		private Label label_14;

		private Label label_15;

		public NumericUpDown numericUpDown_6;

		private Label label_16;

		public NumericUpDown numericUpDown_7;

		private GroupBox groupBox_7;

		private Label label_17;

		public TextBox textBox_0;

		private Label label_18;

		public TextBox textBox_1;

		public ComboBox comboBox_0;

		private Label label_19;

		public TextBox textBox_2;

		public Button button_3;

		public CheckBox checkBox_2;

		private Label label_20;

		public TextBox textBox_3;

		public NumericUpDown numericUpDown_8;

		public NumericUpDown numericUpDown_9;

		public CheckBox checkBox_3;

		private Label label_21;

		public ComboBox comboBox_1;

		private Label label_22;

		private Label label_23;

		public CheckBox checkBox_4;

		public CheckBox checkBox_5;

		private Label label_24;

		private Label label_25;

		public TextBox textBox_4;

		public TextBox textBox_5;

		private TabPage tabPage_4;

		private GroupBox groupBox_8;

		public ComboBox comboBox_2;

		public Button button_4;

		public FastObjectListView fastObjectListView_0;

		public OLVColumn olvcolumn_0;

		private GroupBox groupBox_9;

		public CheckBox checkBox_6;

		public ComboBox comboBox_3;

		private Label label_26;

		public ComboBox comboBox_4;

		public CheckBox checkBox_7;

		public ComboBox comboBox_5;

		public ComboBox comboBox_6;

		private Label label_27;

		public ComboBox comboBox_7;

		private Label label_28;

		public ComboBox comboBox_8;

		private Label label_29;

		public ComboBox comboBox_9;

		private Label label_30;

		private Label label_31;

		private Label label_32;

		private TabPage tabPage_5;

		private TabPage tabPage_6;

		private TabPage tabPage_7;

		private TabControl tabControl_1;

		private TabPage tabPage_8;

		public CheckBox checkBox_8;

		public CheckBox checkBox_9;

		public CheckBox checkBox_10;

		public CheckBox checkBox_11;

		public CheckBox checkBox_12;

		private TabPage tabPage_9;

		public CheckBox checkBox_13;

		private Label label_33;

		public TextBox textBox_6;

		private TabPage tabPage_10;

		public CheckBox checkBox_14;

		private Label label_34;

		public TextBox textBox_7;

		private TabPage tabPage_11;

		private TabControl tabControl_2;

		private TabPage tabPage_12;

		private GroupBox groupBox_10;

		private Label label_35;

		public NumericUpDown numericUpDown_10;

		public Label label_36;

		private Label label_37;

		public NumericUpDown numericUpDown_11;

		public Label label_38;

		private Label label_39;

		public NumericUpDown numericUpDown_12;

		private Label label_40;

		public NumericUpDown numericUpDown_13;

		private Label label_41;

		public NumericUpDown numericUpDown_14;

		public Label label_42;

		public Label label_43;

		public Label label_44;

		private TabPage tabPage_13;

		private GroupBox groupBox_11;

		private Label label_45;

		public NumericUpDown numericUpDown_15;

		public Label label_46;

		public Label label_47;

		public Label label_48;

		public Label label_49;

		public Label label_50;

		public Label label_51;

		public NumericUpDown numericUpDown_16;

		private Label label_52;

		public NumericUpDown numericUpDown_17;

		private Label label_53;

		public NumericUpDown numericUpDown_18;

		private Label label_54;

		public NumericUpDown numericUpDown_19;

		private Label label_55;

		public NumericUpDown numericUpDown_20;

		private Label label_56;

		public NumericUpDown numericUpDown_21;

		public Label label_57;

		public Label label_58;

		public Label label_59;

		private Label label_60;

		public NumericUpDown numericUpDown_22;

		private TabPage tabPage_14;

		private GroupBox groupBox_12;

		private Label label_61;

		public NumericUpDown numericUpDown_23;

		public Label label_62;

		public Label label_63;

		private Label label_64;

		public NumericUpDown numericUpDown_24;

		private TabPage tabPage_15;

		private GroupBox groupBox_13;

		public Label label_65;

		private Label label_66;

		public NumericUpDown numericUpDown_25;

		public ComboBox comboBox_10;

		public ComboBox comboBox_11;

		public ToolStripProgressBar toolStripProgressBar_0;

		private TabPage tabPage_16;

		public Button button_5;

		private GroupBox groupBox_14;

		private TabPage tabPage_17;

		public Button button_6;

		private TabPage tabPage_18;

		private GroupBox groupBox_15;

		private Label label_67;

		public ComboBox comboBox_12;

		public ComboBox comboBox_13;

		private Label label_68;

		private Label label_69;

		private Label label_70;

		private Label label_71;

		public ComboBox comboBox_14;

		public ComboBox comboBox_15;

		public ComboBox comboBox_16;

		public Button button_7;

		public TabControl tabControl_3;

		public CheckBox checkBox_15;

		private TabPage tabPage_19;

		private TabPage tabPage_20;

		private GroupBox groupBox_16;

		public Button button_8;

		public Button button_9;

		private Label label_72;

		public ComboBox comboBox_17;

		private Label label_73;

		private Label label_74;

		private TabPage tabPage_21;

		private Label label_75;

		private TabPage tabPage_22;

		private TabPage tabPage_23;

		private GroupBox groupBox_17;

		public Button button_10;

		public Button button_11;

		public ComboBox comboBox_18;

		public Button button_12;

		public FastObjectListView fastObjectListView_1;

		public OLVColumn olvcolumn_1;

		public ToolStripLabel toolStripLabel_2;

		public NumericUpDown numericUpDown_26;

		private TabPage tabPage_24;

		private GroupBox groupBox_18;

		public Button button_13;

		public Button button_14;

		public ComboBox comboBox_19;

		public Button button_15;

		private GroupBox groupBox_19;

		public Button button_16;

		public FastObjectListView fastObjectListView_2;

		public OLVColumn olvcolumn_2;

		private Label label_76;

		public Label label_77;

		public ComboBox comboBox_20;

		public GroupBox groupBox_20;

		public RadioButton radioButton_0;

		public RadioButton radioButton_1;

		public RadioButton radioButton_2;

		private Button button_17;

		public Button button_18;

		public TabControl tabControl_4;

		public CheckBox checkBox_16;

		private Timer timer_2;

		private Label label_78;

		public Label label_79;

		public Label label_80;

		private Label label_81;

		public NumericUpDown numericUpDown_27;

		private Label label_82;

		public NumericUpDown numericUpDown_28;

		public CheckBox checkBox_17;

		private Label label_83;

		public TextBox textBox_8;

		private TabPage tabPage_25;

		private GroupBox groupBox_21;

		private GroupBox groupBox_22;

		private TextBox textBox_9;

		private TextBox textBox_10;

		private Label label_84;

		private Label label_85;

		private Button button_19;

		private TabPage tabPage_26;

		private GroupBox groupBox_23;

		public Button button_20;

		private Label label_86;

		private Label label_87;

		public ComboBox comboBox_21;

		public ComboBox comboBox_22;

		public ComboBox comboBox_23;

		private Label label_88;

		private Label label_89;

		public ComboBox comboBox_24;

		public ComboBox comboBox_25;

		private Label label_90;

		public ComboBox comboBox_26;

		private Label label_91;

		public CheckBox checkBox_18;

		private Timer timer_3;

		private TabControl tabControl_5;

		private TabPage tabPage_27;

		private TabPage tabPage_28;

		private Label label_92;

		public TextBox textBox_11;

		public Button button_21;

		public FastObjectListView fastObjectListView_3;

		public OLVColumn olvcolumn_3;

		private GroupBox groupBox_24;

		private DataGridView dataGridView_0;

		public Button button_22;

		private LinkLabel linkLabel_0;

		private PictureBox pictureBox_0;

		public Button button_23;

		private GroupBox groupBox_25;

		public Button button_24;

		public NumericUpDown numericUpDown_29;

		private Label label_93;

		private Label label_94;

		public CheckBox checkBox_19;

		private Label label_95;

		public ComboBox comboBox_27;

		public Button button_25;

		public Button button_26;

		private TabPage tabPage_29;

		private GroupBox groupBox_26;

		public FastObjectListView fastObjectListView_4;

		public OLVColumn olvcolumn_4;

		private OLVColumn olvcolumn_5;

		public ComboBox comboBox_28;

		public ComboBox comboBox_29;

		private Label label_96;

		private Label label_97;

		public Button button_27;

		private PictureBox pictureBox_1;

		private Label label_98;

		private Label label_99;

		public CheckBox checkBox_20;

		public CheckBox checkBox_21;

		public Label label_100;

		public TrackBar trackBar_0;

		private TabControl tabControl_6;

		private TabPage tabPage_30;

		private TabPage tabPage_31;

		public RichTextBox richTextBox_1;

		public CheckBox checkBox_22;

		private TabPage tabPage_32;

		private GroupBox groupBox_27;

		public Button button_28;

		public Button button_29;

		public ComboBox comboBox_30;

		public Button button_30;

		public FastObjectListView fastObjectListView_5;

		public OLVColumn olvcolumn_6;

		private GroupBox groupBox_28;

		public ComboBox comboBox_31;

		private Label label_101;

		public ComboBox comboBox_32;

		private Label label_102;

		public ComboBox comboBox_33;

		private Label label_103;

		public CheckBox checkBox_23;

		public CheckBox checkBox_24;

		public CheckBox checkBox_25;

		public CheckBox checkBox_26;

		private Label label_104;

		public NumericUpDown numericUpDown_30;

		private TabPage tabPage_33;

		public Button button_31;

		private GroupBox groupBox_29;

		public CheckBox checkBox_27;

		private Label label_105;

		public NumericUpDown numericUpDown_31;

		public NumericUpDown numericUpDown_32;

		private Label label_106;

		private Label label_107;

		private TabPage tabPage_34;

		private GroupBox groupBox_30;

		private GroupBox groupBox_31;

		private TextBox textBox_12;

		private TextBox textBox_13;

		private Label label_108;

		private Label label_109;

		private Button button_32;

		private Label label_110;

		public NumericUpDown numericUpDown_33;

		public NumericUpDown numericUpDown_34;

		private Label label_111;

		private Label label_112;

		public NumericUpDown numericUpDown_35;

		private Label label_113;

		public CheckBox checkBox_28;

		public CheckBox checkBox_29;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn_0;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_0;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_1;

		private GroupBox groupBox_32;

		public CheckBox checkBox_30;

		private Button button_33;

		private GroupBox groupBox_33;

		public CheckBox checkBox_31;

		private PictureBox pictureBox_2;

		private TabPage tabPage_35;

		private GroupBox groupBox_34;

		private Button button_34;

		public Button button_35;

		public ComboBox comboBox_34;

		private Panel panel_0;

		public CheckBox checkBox_32;

		private TabPage tabPage_36;

		private GroupBox groupBox_35;

		public CheckBox checkBox_33;

		public CheckBox checkBox_34;

		private GroupBox groupBox_36;

		private ComboBox comboBox_35;

		private Button button_36;

		public FastObjectListView fastObjectListView_6;

		public OLVColumn olvcolumn_7;

		private OLVColumn olvcolumn_8;

		private ComboBox comboBox_36;

		private ComboBox comboBox_37;

		private GroupBox groupBox_37;

		private ComboBox comboBox_38;

		private Button button_37;

		public FastObjectListView fastObjectListView_7;

		public OLVColumn olvcolumn_9;

		private OLVColumn olvcolumn_10;

		private ComboBox comboBox_39;

		private ComboBox comboBox_40;

		public CheckBox checkBox_35;

		public NumericUpDown numericUpDown_36;

		private Label label_114;

		private Button button_38;

		private TabPage tabPage_37;

		private TabPage tabPage_38;

		private TabPage tabPage_39;

		private TabPage tabPage_40;

		private Button button_39;

		private Button button_40;

		private GroupBox groupBox_38;

		private Button button_41;

		private Button button_42;

		public CheckBox checkBox_36;

		public NumericUpDown numericUpDown_37;

		private Label label_115;

		private Button button_43;

		private TabPage tabPage_41;

		private GroupBox groupBox_39;

		private ComboBox comboBox_41;

		private Button button_44;

		public FastObjectListView fastObjectListView_8;

		public OLVColumn olvcolumn_11;

		private OLVColumn olvcolumn_12;

		private ComboBox comboBox_42;

		private ComboBox comboBox_43;

		private GroupBox groupBox_40;

		private ComboBox comboBox_44;

		private Button button_45;

		public FastObjectListView fastObjectListView_9;

		public OLVColumn olvcolumn_13;

		private OLVColumn olvcolumn_14;

		private ComboBox comboBox_45;

		private ComboBox comboBox_46;

		public RadioButton radioButton_3;

		public RadioButton radioButton_4;

		private GroupBox groupBox_41;

		public NumericUpDown numericUpDown_38;

		private Label label_116;

		private Button button_46;

		private Button button_47;

		private TabPage tabPage_42;

		private GroupBox groupBox_42;

		public Button button_48;

		public Button button_49;

		public ComboBox comboBox_47;

		public Button button_50;

		public FastObjectListView fastObjectListView_10;

		public OLVColumn olvcolumn_15;

		private GroupBox groupBox_43;

		public RichTextBox richTextBox_2;

		private GroupBox groupBox_44;

		public NumericUpDown numericUpDown_39;

		private Label label_117;

		public NumericUpDown numericUpDown_40;

		private Label label_118;

		public NumericUpDown numericUpDown_41;

		private Label label_119;

		public NumericUpDown numericUpDown_42;

		private Label label_120;

		private Button button_51;

		private GroupBox groupBox_45;

		public Label label_121;

		public TrackBar trackBar_1;

		public CheckBox checkBox_37;

		public ComboBox comboBox_48;

		private TabControl tabControl_7;

		private TabPage tabPage_43;

		private GroupBox groupBox_46;

		private Label label_122;

		public Label label_123;

		public NumericUpDown numericUpDown_43;

		public Label label_124;

		public CheckBox checkBox_38;

		public ComboBox comboBox_49;

		public CheckBox checkBox_39;

		public CheckBox checkBox_40;

		public Label label_125;

		public ComboBox comboBox_50;

		public Label label_126;

		public Label label_127;

		public NumericUpDown numericUpDown_44;

		public NumericUpDown numericUpDown_45;

		public CheckBox checkBox_41;

		public CheckBox checkBox_42;

		public ComboBox comboBox_51;

		public Button button_52;

		public FastObjectListView fastObjectListView_11;

		public OLVColumn olvcolumn_16;

		public Button button_53;

		private Label label_128;

		public TextBox textBox_14;

		public CheckBox checkBox_43;

		public Button button_54;

		private TabPage tabPage_44;

		private GroupBox groupBox_47;

		public ComboBox comboBox_52;

		public Button button_55;

		public FastObjectListView fastObjectListView_12;

		public OLVColumn olvcolumn_17;

		public Button button_56;

		private GroupBox groupBox_48;

		private Button button_57;

		public FastObjectListView fastObjectListView_13;

		public OLVColumn olvcolumn_18;

		private ComboBox comboBox_53;

		private GroupBox groupBox_49;

		private Button button_58;

		public FastObjectListView fastObjectListView_14;

		public OLVColumn olvcolumn_19;

		private ComboBox comboBox_54;

		public NumericUpDown numericUpDown_46;

		public NumericUpDown numericUpDown_47;

		public NumericUpDown numericUpDown_48;

		public CheckBox checkBox_44;

		public RadioButton radioButton_5;

		public RadioButton radioButton_6;

		public ComboBox comboBox_55;

		public NumericUpDown numericUpDown_49;

		private Label label_129;

		public NumericUpDown numericUpDown_50;

		private Label label_130;

		public NumericUpDown numericUpDown_51;

		private Label label_131;

		private Label label_132;

		private Label label_133;

		private Label label_134;

		private Label label_135;

		private Label label_136;

		private Label label_137;

		public Button button_59;

		public Button button_60;

		public Button button_61;

		private GroupBox groupBox_50;

		private Button button_62;

		private Button button_63;

		public ListBox listBox_0;

		private GroupBox groupBox_51;

		public Button button_64;

		public TextBox textBox_15;

		public CheckBox checkBox_45;

		public TabControl tabControl_8;

		public CheckBox checkBox_46;

		public CheckBox checkBox_47;

		public Button button_65;

		private GroupBox groupBox_52;

		public Button button_66;

		public TextBox textBox_16;

		private Label label_138;

		public NumericUpDown numericUpDown_52;

		public Label label_139;

		public Button button_67;

		private TextBox textBox_17;

		private ComboBox comboBox_56;

		private ComboBox comboBox_57;

		public CheckBox checkBox_48;

		public CheckBox checkBox_49;

		private GroupBox groupBox_53;

		public Label label_140;

		public TrackBar trackBar_2;

		private TabPage tabPage_45;

		private GroupBox groupBox_54;

		public Button button_68;

		public OLVColumn olvcolumn_20;

		public CheckBox checkBox_50;

		public ComboBox comboBox_58;

		private Label label_141;

		public Button button_69;

		public Button button_70;

		private GroupBox groupBox_55;

		private Label label_142;

		public NumericUpDown numericUpDown_53;

		private GroupBox groupBox_56;

		private Label label_143;

		public NumericUpDown numericUpDown_54;

		public Label label_144;

		private Label label_145;

		public NumericUpDown numericUpDown_55;

		public Label label_146;

		public FastObjectListView fastObjectListView_15;

		public OLVColumn olvcolumn_21;

		private Label label_147;

		private Label label_148;

		private Label label_149;

		private TabPage tabPage_46;

		private GroupBox groupBox_57;

		private Button button_71;

		public ComboBox comboBox_59;

		public ComboBox comboBox_60;

		private Label label_150;

		private Label label_151;

		private Button button_72;

		private TabPage tabPage_47;

		private GroupBox groupBox_58;

		public Button button_73;

		public FastObjectListView fastObjectListView_16;

		public OLVColumn olvcolumn_22;

		public Button button_74;

		public ComboBox comboBox_61;

		private Label label_152;

		public Button button_75;

		private TabPage tabPage_48;

		private GroupBox groupBox_59;

		public ComboBox comboBox_62;

		private Label label_153;

		public Button button_76;

		public Button button_77;

		private Label label_154;

		public ComboBox comboBox_63;

		private Label label_155;

		public ComboBox comboBox_64;

		public Button button_78;

		public Button button_79;

		private Label label_156;

		public NumericUpDown numericUpDown_56;

		public Label label_157;

		private Timer timer_4;

		private Label label_158;

		public NumericUpDown numericUpDown_57;

		private Label label_159;

		public ComboBox comboBox_65;

		private Label label_160;

		public TextBox textBox_18;

		public CheckBox checkBox_51;

		private TabPage tabPage_49;

		private GroupBox groupBox_60;

		public Button button_80;

		public Button button_81;

		public Label label_161;

		private Label label_162;

		public NumericUpDown numericUpDown_58;

		private Label label_163;

		public ComboBox comboBox_66;

		public NumericUpDown numericUpDown_59;

		public ComboBox comboBox_67;

		private Label label_164;

		public NumericUpDown numericUpDown_60;

		public NumericUpDown numericUpDown_61;

		private Label label_165;

		private Label label_166;

		private TabPage tabPage_50;

		private TabControl tabControl_9;

		private TabPage tabPage_51;

		private GroupBox groupBox_61;

		public ListBox listBox_1;

		private Button button_82;

		private Button button_83;

		private GroupBox groupBox_62;

		public ComboBox comboBox_68;

		public Button button_84;

		public FastObjectListView fastObjectListView_17;

		public OLVColumn olvcolumn_23;

		public CheckBox checkBox_52;

		public NumericUpDown numericUpDown_62;

		private Label label_167;

		private Label label_168;

		private TabPage tabPage_52;

		private GroupBox groupBox_63;

		public Button button_85;

		public ComboBox comboBox_69;

		public Button button_86;

		public FastObjectListView fastObjectListView_18;

		public OLVColumn olvcolumn_24;

		private TabPage tabPage_53;

		private GroupBox groupBox_64;

		public CheckBox checkBox_53;

		public TextBox textBox_19;

		public Button button_87;

		public FastObjectListView fastObjectListView_19;

		public OLVColumn olvcolumn_25;

		private GroupBox groupBox_65;

		public CheckBox checkBox_54;

		public TextBox textBox_20;

		public Button button_88;

		public FastObjectListView fastObjectListView_20;

		public OLVColumn olvcolumn_26;

		private GroupBox groupBox_66;

		public CheckBox checkBox_55;

		public TextBox textBox_21;

		public Button button_89;

		public FastObjectListView fastObjectListView_21;

		public OLVColumn olvcolumn_27;

		public CheckBox checkBox_56;

		public NumericUpDown numericUpDown_63;

		private Label label_169;

		public CheckBox checkBox_57;

		public NumericUpDown numericUpDown_64;

		private Label label_170;

		private GroupBox groupBox_67;

		public CheckBox checkBox_58;

		private Label label_171;

		public NumericUpDown numericUpDown_65;

		private Label label_172;

		public NumericUpDown numericUpDown_66;

		private Label label_173;

		public NumericUpDown numericUpDown_67;

		public CheckBox checkBox_59;

		private Label label_174;

		public NumericUpDown numericUpDown_68;

		private Label label_175;

		public NumericUpDown numericUpDown_69;

		public CheckBox checkBox_60;

		private Label label_176;

		public NumericUpDown numericUpDown_70;

		public ComboBox comboBox_70;

		private Label label_177;

		public Button button_90;

		private Label label_178;

		public TextBox textBox_22;

		private Label label_179;

		public TextBox textBox_23;

		public CheckBox checkBox_61;

		public CheckBox checkBox_62;

		private Button button_91;

		private Button button_92;

		private Button button_93;

		private Button button_94;

		public ComboBox comboBox_71;

		public CheckBox checkBox_63;

		private GroupBox groupBox_68;

		public CheckBox checkBox_64;

		private Label label_180;

		public NumericUpDown numericUpDown_71;

		public Label label_181;

		public CheckBox checkBox_65;

		public CheckBox checkBox_66;

		private Button button_95;

		private TabPage tabPage_54;

		private GroupBox groupBox_69;

		public ComboBox comboBox_72;

		public Button button_96;

		public FastObjectListView fastObjectListView_22;

		public OLVColumn olvcolumn_28;

		public CheckBox checkBox_67;

		public CheckBox checkBox_68;

		public NumericUpDown numericUpDown_72;

		public CheckBox checkBox_69;

		public CheckBox checkBox_70;

		public CheckBox checkBox_71;

		public Label label_182;

		public CheckBox checkBox_72;

		public CheckBox checkBox_73;

		public CheckBox checkBox_74;

		private Timer timer_5;

		private Label label_183;

		public NumericUpDown numericUpDown_73;

		public Label label_184;

		public DataGridView dataGridView_1;

		public DataGridView dataGridView_2;

		public DataGridView dataGridView_3;

		private Timer timer_6;

		private Timer timer_7;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn_1;

		private DataGridViewComboBoxColumn dataGridViewComboBoxColumn_0;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_4;

		private DataGridViewComboBoxColumn dataGridViewComboBoxColumn_1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_5;

		private DataGridViewComboBoxColumn dataGridViewComboBoxColumn_2;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn_2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_8;

		private DataGridViewComboBoxColumn dataGridViewComboBoxColumn_3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_9;

		private DataGridViewComboBoxColumn dataGridViewComboBoxColumn_4;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn_3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_14;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn_15;

		private DataGridViewComboBoxColumn dataGridViewComboBoxColumn_5;

		public CheckBox checkBox_75;

		public NumericUpDown numericUpDown_74;

		private Label label_185;

		public CheckBox checkBox_76;

		public CheckBox checkBox_77;

		public CheckBox checkBox_78;

		public CheckBox checkBox_79;

		public NumericUpDown numericUpDown_75;

		public CheckBox checkBox_80;

		private TabPage tabPage_55;

		private TabControl tabControl_10;

		private TabPage tabPage_56;

		private GroupBox groupBox_70;

		public Button button_97;

		public FastObjectListView fastObjectListView_23;

		public OLVColumn olvcolumn_29;

		private Label label_186;

		public NumericUpDown numericUpDown_76;

		public Label label_187;

		public ComboBox comboBox_73;

		private Label label_188;

		public Button button_98;

		public Button button_99;

		private GroupBox groupBox_71;

		public Button button_100;

		public FastObjectListView fastObjectListView_24;

		public OLVColumn olvcolumn_30;

		private Label label_189;

		public ComboBox comboBox_74;

		public ComboBox comboBox_75;

		private Label label_190;

		public RadioButton radioButton_7;

		public ComboBox comboBox_76;

		public TabControl tabControl_11;

		public ComboBox comboBox_77;

		[NonSerialized]
		internal static GetString getString_0;

		public enum GEnum0
		{
			const_0,
			const_1,
			const_2
		}

		public enum GEnum1
		{
			const_0,
			const_1
		}

		[CompilerGenerated]
		private sealed class Class13
		{
			internal bool method_0(BulkBuyingListItem bulkBuyingListItem_1)
			{
				return bulkBuyingListItem_1.HaveName == this.string_0 && bulkBuyingListItem_1.WantName == this.string_1;
			}

			internal void method_1(object sender, EventArgs e)
			{
				this.mainForm_0.method_3(this.bulkBuyingListItem_0);
			}

			internal void method_2(object sender, EventArgs e)
			{
				string fileName = string.Format(MainForm.Class13.getString_0(107352687), Class103.ExchangeUrl, WebUtility.UrlEncode(this.bulkBuyingListItem_0.QueryURL));
				Process.Start(fileName);
			}

			internal void method_3(object sender, EventArgs e)
			{
				Class255.BulkBuyingList.Remove(this.bulkBuyingListItem_0);
				this.dataGridView_0.Rows.RemoveAt(this.int_0);
				Class255.class105_0.method_9(ConfigOptions.BulkBuyingList, Class255.BulkBuyingList, true);
			}

			static Class13()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class13));
			}

			public string string_0;

			public string string_1;

			public MainForm mainForm_0;

			public BulkBuyingListItem bulkBuyingListItem_0;

			public DataGridView dataGridView_0;

			public int int_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class14
		{
			internal bool method_0(BulkBuyingListItem bulkBuyingListItem_0)
			{
				return bulkBuyingListItem_0.HaveName == this.string_0 && bulkBuyingListItem_0.WantName == this.string_1;
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class15
		{
			internal bool method_0(BulkBuyingListItem bulkBuyingListItem_0)
			{
				return bulkBuyingListItem_0.HaveName == this.string_0 && bulkBuyingListItem_0.WantName == this.string_1;
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class16
		{
			internal void method_0()
			{
				if (this.mainForm_0.textBox_17.Text.smethod_25() || this.mainForm_0.textBox_17.Text == MainForm.Class16.getString_0(107397052))
				{
					this.mainForm_0.comboBox_56.SelectedItem = this.string_0;
				}
			}

			static Class16()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class16));
			}

			public MainForm mainForm_0;

			public string string_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class17
		{
			internal bool method_0(CraftingBoxData craftingBoxData_0)
			{
				return craftingBoxData_0.AffixBox == this.object_0;
			}

			public object object_0;
		}

		[CompilerGenerated]
		private sealed class Class18
		{
			internal void method_0()
			{
				this.mainForm_0.button_38.Enabled = this.bool_0;
				this.mainForm_0.button_43.Enabled = this.bool_0;
				this.mainForm_0.button_46.Enabled = this.bool_0;
			}

			public MainForm mainForm_0;

			public bool bool_0;
		}

		[CompilerGenerated]
		private sealed class Class19
		{
			internal void method_0(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left && e.X <= 18 && this.craftingBoxData_0.Box.SelectedObjects.Count > 0 && this.craftingBoxData_0.Box.SelectedObject != null)
				{
					this.craftingBoxData_0.Box.RemoveObject(this.craftingBoxData_0.Box.SelectedObject);
				}
			}

			internal void method_1(object sender, EventArgs e)
			{
				Mod mod_ = this.craftingBoxData_0.AffixBox.SelectedItem as Mod;
				Tier tier_ = this.craftingBoxData_0.TierBox.SelectedItem as Tier;
				this.mainForm_0.method_14(this.craftingBoxData_0.Box, mod_, tier_);
			}

			public CraftingBoxData craftingBoxData_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class20
		{
			internal bool method_0(AffixItemViewModel affixItemViewModel_0)
			{
				return affixItemViewModel_0.Affix.ToString() == this.mod_0.ToString();
			}

			public Mod mod_0;
		}

		[CompilerGenerated]
		private sealed class Class21
		{
			internal bool method_0(CraftingBoxData craftingBoxData_0)
			{
				return craftingBoxData_0.Type == Enum18.const_0 && craftingBoxData_0.LoadButton == this.button_0;
			}

			internal bool method_1(CraftingBoxData craftingBoxData_0)
			{
				return craftingBoxData_0.Type == Enum18.const_1 && craftingBoxData_0.LoadButton == this.button_0;
			}

			public Button button_0;
		}

		[CompilerGenerated]
		private sealed class Class22
		{
			internal bool method_0(Tier tier_0)
			{
				return tier_0.Rank == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class23
		{
			internal bool method_0(Tier tier_0)
			{
				return tier_0.Rank == this.int_0;
			}

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class24
		{
			internal bool method_0(CraftingBoxData craftingBoxData_0)
			{
				return craftingBoxData_0.Type == Enum18.const_0 && craftingBoxData_0.SaveButton == this.button_0;
			}

			internal bool method_1(CraftingBoxData craftingBoxData_0)
			{
				return craftingBoxData_0.Type == Enum18.const_1 && craftingBoxData_0.SaveButton == this.button_0;
			}

			public Button button_0;
		}

		[CompilerGenerated]
		private sealed class Class25
		{
			internal void method_0(object sender, EventArgs e)
			{
				this.mainForm_0.method_23(this.itemBuyingListItem_0);
			}

			internal void method_1(object sender, EventArgs e)
			{
				this.mainForm_0.method_24(this.itemBuyingListItem_0);
			}

			internal void method_2(object sender, EventArgs e)
			{
				Process.Start(string.Format(MainForm.Class25.getString_0(107362417), Class103.TradeWebsiteUrl, this.string_0));
			}

			internal void method_3(object sender, EventArgs e)
			{
				List<ItemBuyingListItem> list = Class255.ItemBuyingList.ToList<ItemBuyingListItem>();
				list.RemoveAt(this.int_0);
				this.dataGridView_0.Rows.RemoveAt(this.int_0);
				Class255.class105_0.method_9(ConfigOptions.ItemBuyingList, list, true);
			}

			static Class25()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class25));
			}

			public MainForm mainForm_0;

			public ItemBuyingListItem itemBuyingListItem_0;

			public string string_0;

			public int int_0;

			public DataGridView dataGridView_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class26
		{
			internal bool method_0(ItemBuyingListItem itemBuyingListItem_0)
			{
				return itemBuyingListItem_0.Id == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class27
		{
			internal bool method_0(FlippingListItem flippingListItem_1)
			{
				return flippingListItem_1.HaveName == this.string_0 && flippingListItem_1.WantName == this.string_1;
			}

			internal void method_1(object sender, EventArgs e)
			{
				this.mainForm_0.method_28(this.flippingListItem_0);
			}

			internal void method_2(object sender, EventArgs e)
			{
				Class255.FlippingList.Remove(this.flippingListItem_0);
				this.dataGridView_0.Rows.RemoveAt(this.int_0);
				Class255.class105_0.method_9(ConfigOptions.FlippingList, Class255.FlippingList, true);
			}

			public string string_0;

			public string string_1;

			public FlippingListItem flippingListItem_0;

			public DataGridView dataGridView_0;

			public int int_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class28
		{
			internal bool method_0(FlippingListItem flippingListItem_0)
			{
				return flippingListItem_0.HaveName == this.string_0 && flippingListItem_0.WantName == this.string_1;
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class29
		{
			internal bool method_0(FlippingListItem flippingListItem_0)
			{
				return flippingListItem_0.HaveName == this.string_0 && flippingListItem_0.WantName == this.string_1;
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class30
		{
			internal bool method_0(FlippingListItem flippingListItem_0)
			{
				return flippingListItem_0.HaveName == this.string_0 && flippingListItem_0.WantName == this.string_1;
			}

			internal bool method_1(FlippingListItem flippingListItem_0)
			{
				return flippingListItem_0.HaveName == this.string_1 && flippingListItem_0.WantName == this.string_0;
			}

			public string string_0;

			public string string_1;
		}

		[CompilerGenerated]
		private sealed class Class31
		{
			internal void method_0(object sender, EventArgs e)
			{
				this.mainForm_0.method_34(this.liveSearchListItem_0);
			}

			internal void method_1(object sender, EventArgs e)
			{
				this.mainForm_0.method_38(this.liveSearchListItem_0);
			}

			internal void method_2(object sender, EventArgs e)
			{
				Process.Start(string.Format(MainForm.Class31.getString_0(107362435), Class103.TradeWebsiteUrl, this.string_0));
			}

			internal void method_3(object sender, EventArgs e)
			{
				this.mainForm_0.dictionary_0.Remove(this.string_0);
				this.mainForm_0.dataGridView_1.Rows.RemoveAt(this.int_0);
				if (this.mainForm_0.dictionary_2.ContainsKey(this.string_0))
				{
					this.mainForm_0.dictionary_2[this.string_0].method_2(false);
					this.mainForm_0.dictionary_2.Remove(this.string_0);
				}
				List<LiveSearchListItem> list = Class255.LiveSearchList.ToList<LiveSearchListItem>();
				list.RemoveAt(this.int_0);
				Class255.class105_0.method_9(ConfigOptions.LiveSearchList, list, true);
				this.mainForm_0.method_101();
			}

			static Class31()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class31));
			}

			public LiveSearchListItem liveSearchListItem_0;

			public string string_0;

			public int int_0;

			public MainForm mainForm_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class32
		{
			internal void method_0()
			{
				IEnumerable<LiveSearchListItem> liveSearchList = Class255.LiveSearchList;
				Func<LiveSearchListItem, bool> predicate;
				if ((predicate = this.func_0) == null)
				{
					predicate = (this.func_0 = new Func<LiveSearchListItem, bool>(this.method_1));
				}
				LiveSearchListItem liveSearchListItem = liveSearchList.FirstOrDefault(predicate);
				if (liveSearchListItem != null)
				{
					Class260 @class = this.mainForm_0.dictionary_2[this.string_0];
					liveSearchListItem.Enabled = false;
					@class.method_2(false);
					@class.method_0(Class145.smethod_2(this.string_0, this.class249_0));
					liveSearchListItem.Enabled = true;
					@class.method_1();
				}
			}

			internal bool method_1(LiveSearchListItem liveSearchListItem_0)
			{
				return liveSearchListItem_0.Id == this.string_0;
			}

			public string string_0;

			public MainForm mainForm_0;

			public Class249 class249_0;

			public Func<LiveSearchListItem, bool> func_0;
		}

		[CompilerGenerated]
		private sealed class Class33
		{
			internal bool method_0(Order order_0)
			{
				return order_0.player.name == this.string_0 && order_0.bool_2;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class34
		{
			internal bool method_0(Player player_0)
			{
				return player_0.name == this.string_0;
			}

			internal bool method_1(Order order_0)
			{
				return order_0.player.name == this.string_0 && order_0.tradeStates_0 == Order.TradeStates.NotStarted;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class35
		{
			internal bool method_0(Order order_0)
			{
				return order_0.player.name == this.string_0;
			}

			internal bool method_1(Order order_0)
			{
				return order_0.player.name == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class36
		{
			internal void method_0()
			{
				SellProcessor.smethod_12(this.order_0, this.int_0, TradeProcessor.order_0 == this.order_0);
			}

			public Order order_0;

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class37
		{
			internal void method_0()
			{
				SellProcessor.smethod_12(this.order_0, this.int_0, TradeProcessor.order_0 == this.order_0);
			}

			public Order order_0;

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class39
		{
			internal bool method_0(DecimalCurrencyListItem decimalCurrencyListItem_0)
			{
				return decimalCurrencyListItem_0.Currency == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class40
		{
			internal bool method_0(Mod mod_0)
			{
				return mod_0.ToString() == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class41
		{
			internal bool method_0(Mod mod_0)
			{
				return mod_0.ToString() == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class42
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[3];
				*(byte*)ptr = (Class255.class105_0.method_8<string>(ConfigOptions.IgnoreList).Contains(this.player_0.name.ToLower()) ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					((byte*)ptr)[1] = (this.bool_0 ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						this.mainForm_0.fastObjectListView_21.AddObject(new ListItemViewModel(this.player_0.name));
						Class255.class105_0.method_9(ConfigOptions.IgnoreList, this.mainForm_0.fastObjectListView_21.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_63)).ToList<string>(), true);
					}
					foreach (object obj in this.mainForm_0.fastObjectListView_21.Objects)
					{
						ListItemViewModel listItemViewModel = (ListItemViewModel)obj;
						((byte*)ptr)[2] = ((this.player_0.name.ToLower() == listItemViewModel.Text.ToLower()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							return;
						}
					}
					this.player_0.IgnoredUntil = DateTime.Now + new TimeSpan(0, (int)this.mainForm_0.numericUpDown_63.Value, 0);
					this.player_0.PermanentBan = this.bool_0;
					this.mainForm_0.list_15.Add(this.player_0);
				}
			}

			public Player player_0;

			public bool bool_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class43
		{
			internal void method_0()
			{
				foreach (ListItemViewModel listItemViewModel in this.mainForm_0.fastObjectListView_21.Objects.Cast<ListItemViewModel>().ToList<ListItemViewModel>())
				{
					if (listItemViewModel.Text.ToLower() == this.player_0.name.ToLower())
					{
						this.mainForm_0.fastObjectListView_21.RemoveObject(listItemViewModel);
					}
				}
				this.mainForm_0.list_15.Remove(this.player_0);
				Class255.class105_0.method_9(ConfigOptions.IgnoreList, this.mainForm_0.fastObjectListView_21.Objects.Cast<ListItemViewModel>().Select(new Func<ListItemViewModel, string>(MainForm.<>c.<>9.method_64)).ToList<string>(), true);
			}

			public MainForm mainForm_0;

			public Player player_0;
		}

		[CompilerGenerated]
		private sealed class Class44
		{
			[DebuggerStepThrough]
			internal Task method_0()
			{
				MainForm.Class44.Class45 @class = new MainForm.Class44.Class45();
				@class.class44_0 = this;
				@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
				asyncTaskMethodBuilder_.Start<MainForm.Class44.Class45>(ref @class);
				return @class.asyncTaskMethodBuilder_0.Task;
			}

			internal void method_1()
			{
				this.mainForm_0.button_1.Enabled = false;
				this.mainForm_0.genum0_0 = MainForm.GEnum0.const_1;
				this.mainForm_0.button_1.Text = MainForm.Class44.getString_0(107389992);
				Class123.smethod_15();
			}

			static Class44()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class44));
			}

			public MainForm mainForm_0;

			public bool bool_0;

			public bool bool_1;

			public MainForm.GEnum1 genum1_0;

			public Action action_0;

			[NonSerialized]
			internal static GetString getString_0;

			private sealed class Class45 : IAsyncStateMachine
			{
				unsafe void IAsyncStateMachine.MoveNext()
				{
					void* ptr = stackalloc byte[21];
					int num = this.int_0;
					try
					{
						if (num <= 1)
						{
						}
						try
						{
							TaskAwaiter awaiter;
							if (num != 0)
							{
								TaskAwaiter awaiter2;
								if (num != 1)
								{
									this.class44_0.mainForm_0.thread_3 = Thread.CurrentThread;
									this.class44_0.mainForm_0.bool_12 = false;
									this.class44_0.mainForm_0.bool_4 = false;
									this.class44_0.mainForm_0.bool_3 = true;
									if (this.class44_0.mainForm_0.genum0_0 != MainForm.GEnum0.const_0 && !this.class44_0.bool_0)
									{
										goto IL_7B5;
									}
									*(byte*)ptr = ((this.class44_0.mainForm_0.enum8_0 == Enum8.const_0) ? 1 : 0);
									if (*(sbyte*)ptr != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244487));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									Control mainForm_ = this.class44_0.mainForm_0;
									Action method;
									if ((method = this.class44_0.action_0) == null)
									{
										method = (this.class44_0.action_0 = new Action(this.class44_0.method_1));
									}
									mainForm_.Invoke(method);
									((byte*)ptr)[1] = ((!API.DataLoaded) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 1) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244442));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[2] = ((!this.class44_0.mainForm_0.bool_11) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 2) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244357));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[3] = (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.POESESSID)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 3) != 0)
									{
										Class181.smethod_3(Enum11.const_0, MainForm.Class44.Class45.getString_0(107244808));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[4] = (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.AccountName)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 4) != 0)
									{
										Class181.smethod_3(Enum11.const_0, MainForm.Class44.Class45.getString_0(107244767));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[5] = (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.League)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 5) != 0)
									{
										Class181.smethod_3(Enum11.const_0, MainForm.Class44.Class45.getString_0(107244754));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[6] = (string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.AuthKey)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 6) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244685));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[7] = ((!this.class44_0.mainForm_0.method_138()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 7) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244664));
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									((byte*)ptr)[8] = ((!Class306.smethod_0(0)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 8) != 0)
									{
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									this.int_1 = Class255.LiveSearchList.Count(new Func<LiveSearchListItem, bool>(MainForm.<>c.<>9.method_65));
									if (this.int_1 > 20 && Class255.LiveSearchEnabled)
									{
										Class181.smethod_2(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244623), new object[]
										{
											20,
											this.int_1
										});
										this.class44_0.mainForm_0.method_64(true);
										goto IL_7B5;
									}
									ProcessHelper.smethod_9();
									Stashes.int_0 = 0;
									UI.bitmap_1 = null;
									UI.bitmap_2 = null;
									((byte*)ptr)[9] = ((UI.intptr_0 != IntPtr.Zero) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 9) != 0)
									{
										((byte*)ptr)[10] = ((!ProcessHelper.smethod_13(false)) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 10) != 0)
										{
											Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107243998));
											this.class44_0.mainForm_0.method_64(true);
											goto IL_7B5;
										}
									}
									else
									{
										ProcessHelper.smethod_14();
										ProcessHelper.smethod_4();
									}
									this.class44_0.mainForm_0.expectedState_0 = ExpectedState.Running;
									UI.smethod_1();
									UI.smethod_92();
									((byte*)ptr)[11] = (Class255.class105_0.method_4(ConfigOptions.ShowInGameChat) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 11) != 0)
									{
										this.class44_0.mainForm_0.form1_0.method_1();
									}
									((byte*)ptr)[12] = ((!UI.smethod_16()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 12) != 0)
									{
										((byte*)ptr)[13] = (Class255.class105_0.method_4(ConfigOptions.AutoLogin) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 13) != 0)
										{
											Position position;
											if (!UI.smethod_2(out position, Class238.Launch, MainForm.Class44.Class45.getString_0(107349692), 0.8) && !UI.smethod_3(out position, Images.Exit, MainForm.Class44.Class45.getString_0(107243901)) && !UI.smethod_3(out position, Images.CharacterSelect, MainForm.Class44.Class45.getString_0(107446251)))
											{
												Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107243892));
												this.class44_0.mainForm_0.method_64(true);
												goto IL_7B5;
											}
											((byte*)ptr)[14] = ((!UI.smethod_30()) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 14) != 0)
											{
												this.class44_0.mainForm_0.method_64(true);
												goto IL_7B5;
											}
										}
										else
										{
											Position position;
											((byte*)ptr)[15] = (UI.smethod_3(out position, Images.GGGLogin, MainForm.Class44.Class45.getString_0(107381618)) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 15) != 0)
											{
												Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107243855));
												this.class44_0.mainForm_0.method_64(true);
												goto IL_7B5;
											}
											((byte*)ptr)[16] = ((!UI.smethod_30()) ? 1 : 0);
											if (*(sbyte*)((byte*)ptr + 16) != 0)
											{
												this.class44_0.mainForm_0.method_64(true);
												goto IL_7B5;
											}
										}
									}
									((byte*)ptr)[17] = (this.class44_0.mainForm_0.method_40() ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 17) != 0)
									{
										((byte*)ptr)[18] = ((!UI.smethod_51()) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 18) != 0)
										{
											goto IL_7B5;
										}
										awaiter = this.class44_0.mainForm_0.method_58(this.class44_0.bool_1, this.class44_0.genum1_0, true).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.int_0 = 0;
											this.taskAwaiter_0 = awaiter;
											MainForm.Class44.Class45 @class = this;
											this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, MainForm.Class44.Class45>(ref awaiter, ref @class);
											return;
										}
										goto IL_74A;
									}
									else
									{
										Win32.smethod_18();
										Win32.smethod_20();
										((byte*)ptr)[19] = (this.class44_0.bool_1 ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 19) != 0)
										{
											this.class44_0.mainForm_0.method_60();
											for (;;)
											{
												((byte*)ptr)[20] = (UI.smethod_28() ? 1 : 0);
												if (*(sbyte*)((byte*)ptr + 20) == 0)
												{
													break;
												}
												Thread.Sleep(100);
											}
										}
										awaiter2 = this.class44_0.mainForm_0.method_59(this.class44_0.genum1_0).GetAwaiter();
										if (!awaiter2.IsCompleted)
										{
											this.int_0 = 1;
											this.taskAwaiter_0 = awaiter2;
											MainForm.Class44.Class45 @class = this;
											this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, MainForm.Class44.Class45>(ref awaiter2, ref @class);
											return;
										}
									}
								}
								else
								{
									awaiter2 = this.taskAwaiter_0;
									this.taskAwaiter_0 = default(TaskAwaiter);
									this.int_0 = -1;
								}
								awaiter2.GetResult();
								goto IL_79A;
							}
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							this.int_0 = -1;
							IL_74A:
							awaiter.GetResult();
						}
						catch (ThreadAbortException)
						{
						}
						catch (TypeInitializationException)
						{
							Class181.smethod_3(Enum11.const_2, MainForm.Class44.Class45.getString_0(107244238));
						}
						catch (Exception exception)
						{
							this.exception_0 = exception;
							Class181.smethod_3(Enum11.const_2, string.Format(MainForm.Class44.Class45.getString_0(107250611), this.exception_0));
						}
						IL_79A:;
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncTaskMethodBuilder_0.SetException(exception);
						return;
					}
					IL_7B5:
					this.int_0 = -2;
					this.asyncTaskMethodBuilder_0.SetResult();
				}

				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				static Class45()
				{
					Strings.CreateGetStringDelegate(typeof(MainForm.Class44.Class45));
				}

				public int int_0;

				public AsyncTaskMethodBuilder asyncTaskMethodBuilder_0;

				public MainForm.Class44 class44_0;

				private int int_1;

				private Exception exception_0;

				private TaskAwaiter taskAwaiter_0;

				[NonSerialized]
				internal static GetString getString_0;
			}
		}

		[CompilerGenerated]
		private sealed class Class47
		{
			[DebuggerStepThrough]
			internal Task method_0()
			{
				MainForm.Class47.Class48 @class = new MainForm.Class47.Class48();
				@class.class47_0 = this;
				@class.asyncTaskMethodBuilder_0 = AsyncTaskMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder_ = @class.asyncTaskMethodBuilder_0;
				asyncTaskMethodBuilder_.Start<MainForm.Class47.Class48>(ref @class);
				return @class.asyncTaskMethodBuilder_0.Task;
			}

			internal void method_1()
			{
				this.mainForm_0.button_1.Enabled = true;
				this.mainForm_0.button_49.Enabled = true;
				this.mainForm_0.timer_0.Enabled = true;
				this.mainForm_0.timer_1.Enabled = true;
				this.mainForm_0.timer_2.Enabled = true;
				this.mainForm_0.timer_3.Enabled = true;
				this.mainForm_0.timer_4.Enabled = true;
				this.mainForm_0.button_56.Enabled = true;
				this.mainForm_0.textBox_17.Enabled = false;
				this.mainForm_0.timer_7.Enabled = true;
				this.mainForm_0.timer_7_Tick(null, null);
				this.mainForm_0.method_129();
				this.mainForm_0.stopwatch_0.Start();
			}

			public MainForm mainForm_0;

			public MainForm.GEnum1 genum1_0;

			public Action action_0;

			private sealed class Class48 : IAsyncStateMachine
			{
				unsafe void IAsyncStateMachine.MoveNext()
				{
					void* ptr = stackalloc byte[20];
					int num = this.int_0;
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							this.class47_0.mainForm_0.thread_3 = Thread.CurrentThread;
							Class181.smethod_3(Enum11.const_0, MainForm.Class47.Class48.getString_0(107243560));
							UI.smethod_1();
							Class181.smethod_2(Enum11.const_3, MainForm.Class47.Class48.getString_0(107243507), new object[]
							{
								Class255.class105_0.method_3(ConfigOptions.AuthKey)
							});
							Class181.smethod_2(Enum11.const_3, MainForm.Class47.Class48.getString_0(107243486), new object[]
							{
								Class255.Resolution.Width,
								Class255.Resolution.Height,
								Class255.class105_0.method_5(ConfigOptions.WindowMode)
							});
							Class181.smethod_2(Enum11.const_3, MainForm.Class47.Class48.getString_0(107243449), new object[]
							{
								Class255.class105_0.method_3(ConfigOptions.League)
							});
							UI.smethod_17();
							UI.smethod_31(false, 1, 12, 5);
							*(byte*)ptr = (this.class47_0.mainForm_0.list_14.Any<Item>() ? 1 : 0);
							if (*(sbyte*)ptr != 0)
							{
								((byte*)ptr)[1] = ((!Class255.class105_0.method_4(ConfigOptions.AllowCleanInventory)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 1) != 0)
								{
									Class181.smethod_3(Enum11.const_2, MainForm.Class47.Class48.getString_0(107243420));
									this.class47_0.mainForm_0.method_64(true);
									goto IL_9DB;
								}
							}
							UI.bitmap_0.smethod_12();
							UI.bitmap_0 = UI.smethod_67();
							Class181.smethod_3(Enum11.const_0, MainForm.Class47.Class48.getString_0(107393509));
							UI.smethod_44(null);
							((byte*)ptr)[2] = (this.class47_0.mainForm_0.method_40() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) == 0)
							{
								goto IL_9C0;
							}
							Stashes.LoadState = Enum9.const_0;
							Stashes.Tabs = null;
							Stashes.bool_0 = false;
							Stashes.Items.Clear();
							Stashes.Layout.Clear();
							this.class47_0.mainForm_0.method_68();
							this.class47_0.mainForm_0.method_61();
							((byte*)ptr)[3] = ((this.class47_0.mainForm_0.thread_1 != null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 3) != 0)
							{
								this.class47_0.mainForm_0.thread_1.Abort();
								this.class47_0.mainForm_0.thread_1 = null;
							}
							((byte*)ptr)[4] = (this.class47_0.mainForm_0.list_14.Any<Item>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 4) == 0)
							{
								goto IL_32A;
							}
							Class181.smethod_3(Enum11.const_0, MainForm.Class47.Class48.getString_0(107243810));
							awaiter = Stashes.smethod_2(0).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.int_0 = 0;
								this.taskAwaiter_0 = awaiter;
								MainForm.Class47.Class48 @class = this;
								this.asyncTaskMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, MainForm.Class47.Class48>(ref awaiter, ref @class);
								return;
							}
						}
						else
						{
							awaiter = this.taskAwaiter_0;
							this.taskAwaiter_0 = default(TaskAwaiter);
							this.int_0 = -1;
						}
						awaiter.GetResult();
						for (;;)
						{
							((byte*)ptr)[5] = ((!Stashes.bool_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 5) == 0)
							{
								break;
							}
							Thread.Sleep(1000);
						}
						((byte*)ptr)[6] = (UI.smethod_72() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 6) != 0)
						{
							UI.smethod_18();
						}
						UI.smethod_31(false, 60, 12, 5);
						CleanInventory.smethod_1(null, this.class47_0.mainForm_0.list_14.smethod_11());
						UI.smethod_51();
						UI.smethod_44(null);
						IL_32A:
						this.class47_0.mainForm_0.thread_2 = new Thread(new ThreadStart(MainForm.<>c.<>9.method_66));
						this.class47_0.mainForm_0.thread_2.SetApartmentState(ApartmentState.STA);
						this.class47_0.mainForm_0.thread_2.IsBackground = true;
						this.class47_0.mainForm_0.thread_2.Start();
						((byte*)ptr)[7] = (string.IsNullOrEmpty(this.class47_0.mainForm_0.CharacterName) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 7) != 0)
						{
							this.class47_0.mainForm_0.CharacterName = Characters.smethod_1(Class255.Cookies);
						}
						((byte*)ptr)[8] = (UI.smethod_72() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 8) != 0)
						{
							UI.smethod_18();
						}
						((byte*)ptr)[9] = (UI.smethod_69() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							Win32.smethod_14(MainForm.Class47.Class48.getString_0(107393565), false);
						}
						this.class47_0.mainForm_0.string_5 = this.class47_0.mainForm_0.method_44(MainForm.regex_1);
						this.class47_0.mainForm_0.int_2 = 0;
						Win32.dateTime_0 = DateTime.Now;
						this.class47_0.mainForm_0.dateTime_8 = DateTime.Now;
						this.class47_0.mainForm_0.genum0_0 = MainForm.GEnum0.const_2;
						Control mainForm_ = this.class47_0.mainForm_0;
						Action method;
						if ((method = this.class47_0.action_0) == null)
						{
							method = (this.class47_0.action_0 = new Action(this.class47_0.method_1));
						}
						mainForm_.Invoke(method);
						((byte*)ptr)[10] = (UI.smethod_79() ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 10) != 0)
						{
							UI.smethod_41();
						}
						((byte*)ptr)[11] = ((!UI.smethod_13(1)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 11) == 0)
						{
							((byte*)ptr)[12] = (UI.smethod_72() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 12) != 0)
							{
								UI.smethod_18();
							}
							((byte*)ptr)[13] = ((this.class47_0.genum1_0 == MainForm.GEnum1.const_0) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 13) != 0)
							{
								this.class47_0.mainForm_0.method_146();
								this.class47_0.mainForm_0.expectedState_0 = ExpectedState.Online;
								this.class47_0.mainForm_0.dateTime_4 = this.class47_0.mainForm_0.method_97();
								this.class47_0.mainForm_0.thread_1 = new Thread(new ThreadStart(this.class47_0.mainForm_0.method_72));
								this.class47_0.mainForm_0.thread_1.SetApartmentState(ApartmentState.STA);
								this.class47_0.mainForm_0.thread_1.IsBackground = true;
								this.class47_0.mainForm_0.thread_1.Start();
								((byte*)ptr)[14] = (Class255.class105_0.method_4(ConfigOptions.EnableLogout) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 14) != 0)
								{
									this.class47_0.mainForm_0.dateTime_2 = DateTime.Parse(this.class47_0.mainForm_0.numericUpDown_0.Value.ToString() + MainForm.Class47.Class48.getString_0(107396570) + this.class47_0.mainForm_0.numericUpDown_1.Value.ToString());
									this.class47_0.mainForm_0.dateTime_2 = this.class47_0.mainForm_0.dateTime_2 + new TimeSpan(0, 0, this.class47_0.mainForm_0.random_1.Next(1, 30), this.class47_0.mainForm_0.random_1.Next(1, 30));
									((byte*)ptr)[15] = ((DateTime.Now > this.class47_0.mainForm_0.dateTime_2) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 15) != 0)
									{
										this.class47_0.mainForm_0.dateTime_2 = this.class47_0.mainForm_0.dateTime_2.AddDays(1.0);
									}
									Class181.smethod_2(Enum11.const_0, MainForm.Class47.Class48.getString_0(107243705), new object[]
									{
										this.class47_0.mainForm_0.dateTime_2
									});
								}
								((byte*)ptr)[16] = (Class255.class105_0.method_4(ConfigOptions.EnableLogin) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 16) != 0)
								{
									this.class47_0.mainForm_0.dateTime_3 = DateTime.Parse(this.class47_0.mainForm_0.numericUpDown_3.Value.ToString() + MainForm.Class47.Class48.getString_0(107396570) + this.class47_0.mainForm_0.numericUpDown_2.Value.ToString());
									this.class47_0.mainForm_0.dateTime_3 = this.class47_0.mainForm_0.dateTime_3 + new TimeSpan(0, 0, this.class47_0.mainForm_0.random_1.Next(1, 30), this.class47_0.mainForm_0.random_1.Next(1, 30));
									((byte*)ptr)[17] = ((DateTime.Now > this.class47_0.mainForm_0.dateTime_3) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 17) != 0)
									{
										this.class47_0.mainForm_0.dateTime_3 = this.class47_0.mainForm_0.dateTime_3.AddDays(1.0);
									}
									Class181.smethod_2(Enum11.const_0, MainForm.Class47.Class48.getString_0(107243672), new object[]
									{
										this.class47_0.mainForm_0.dateTime_3
									});
								}
								this.class47_0.mainForm_0.dateTime_6 = default(DateTime);
								this.class47_0.mainForm_0.dateTime_7 = default(DateTime);
							}
							else
							{
								((byte*)ptr)[18] = ((this.class47_0.genum1_0 == MainForm.GEnum1.const_1) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 18) != 0)
								{
									this.class47_0.mainForm_0.thread_1 = new Thread(new ThreadStart(Mule.smethod_1));
									this.class47_0.mainForm_0.thread_1.SetApartmentState(ApartmentState.STA);
									this.class47_0.mainForm_0.thread_1.IsBackground = true;
									this.class47_0.mainForm_0.thread_1.Start();
								}
							}
							((byte*)ptr)[19] = (File.Exists(Class120.string_2) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 19) != 0)
							{
								File.Delete(Class120.string_2);
							}
							File.Copy(Class120.string_1, Class120.string_2);
							Task.Run(new Action(MainForm.<>c.<>9.method_67));
							this.class47_0.mainForm_0.thread_3 = null;
							Class123.smethod_15();
							Class181.smethod_3(Enum11.const_0, MainForm.Class47.Class48.getString_0(107243639));
						}
						IL_9C0:;
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncTaskMethodBuilder_0.SetException(exception);
						return;
					}
					IL_9DB:
					this.int_0 = -2;
					this.asyncTaskMethodBuilder_0.SetResult();
				}

				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				static Class48()
				{
					Strings.CreateGetStringDelegate(typeof(MainForm.Class47.Class48));
				}

				public int int_0;

				public AsyncTaskMethodBuilder asyncTaskMethodBuilder_0;

				public MainForm.Class47 class47_0;

				private TaskAwaiter taskAwaiter_0;

				[NonSerialized]
				internal static GetString getString_0;
			}
		}

		[CompilerGenerated]
		private sealed class Class51
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[10];
				Class123.smethod_20();
				this.mainForm_0.button_11.Enabled = true;
				this.mainForm_0.button_54.Enabled = true;
				this.mainForm_0.button_99.Enabled = true;
				this.mainForm_0.button_29.Enabled = true;
				this.mainForm_0.button_56.Enabled = false;
				this.mainForm_0.button_70.Enabled = true;
				this.mainForm_0.button_73.Enabled = true;
				this.mainForm_0.button_77.Enabled = true;
				this.mainForm_0.textBox_17.Enabled = true;
				this.mainForm_0.method_129();
				Stashes.int_0 = 0;
				UI.bitmap_1 = null;
				UI.bitmap_2 = null;
				UI.position_0 = null;
				this.mainForm_0.int_5 = 0;
				this.mainForm_0.int_4 = 0;
				this.mainForm_0.bool_19 = false;
				MainForm.IsPaused = false;
				Win32.smethod_18();
				Win32.smethod_20();
				this.mainForm_0.method_12(true);
				*(byte*)ptr = (this.bool_0 ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					this.mainForm_0.method_131();
					((byte*)ptr)[1] = ((this.mainForm_0.thread_3 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						this.mainForm_0.thread_3.Abort();
						this.mainForm_0.thread_3 = null;
					}
					((byte*)ptr)[2] = ((this.mainForm_0.thread_1 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) != 0)
					{
						this.mainForm_0.thread_1.Abort();
						this.mainForm_0.thread_1 = null;
					}
					((byte*)ptr)[3] = ((this.mainForm_0.thread_2 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						Stashes.LoadState = Enum9.const_0;
						this.mainForm_0.thread_2.Abort();
						this.mainForm_0.thread_2 = null;
					}
					((byte*)ptr)[4] = ((this.mainForm_0.thread_5 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 4) != 0)
					{
						this.mainForm_0.thread_5.Abort();
						this.mainForm_0.thread_5 = null;
					}
					((byte*)ptr)[5] = ((this.mainForm_0.thread_4 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						this.mainForm_0.thread_4.Abort();
						this.mainForm_0.thread_5 = null;
					}
				}
				((byte*)ptr)[6] = ((UI.bitmap_0 != null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) != 0)
				{
					UI.bitmap_0.Dispose();
				}
				this.mainForm_0.timer_1.Enabled = false;
				this.mainForm_0.timer_2.Enabled = false;
				this.mainForm_0.timer_3.Enabled = false;
				this.mainForm_0.timer_4.Enabled = false;
				this.mainForm_0.timer_7.Enabled = false;
				this.mainForm_0.stopwatch_0.Stop();
				this.mainForm_0.bool_10 = false;
				this.mainForm_0.bool_17 = false;
				this.mainForm_0.bool_22 = false;
				this.mainForm_0.button_1.Text = MainForm.Class51.getString_0(107388874);
				this.mainForm_0.button_1.Enabled = true;
				this.mainForm_0.button_49.Text = MainForm.Class51.getString_0(107388874);
				this.mainForm_0.button_49.Enabled = true;
				this.mainForm_0.bool_12 = true;
				UI.dateTime_0 = default(DateTime);
				this.mainForm_0.toolStripLabel_2.Text = string.Empty;
				this.mainForm_0.bool_18 = false;
				this.mainForm_0.bool_16 = false;
				this.mainForm_0.method_85(null);
				this.mainForm_0.method_61();
				((byte*)ptr)[7] = ((this.mainForm_0.genum0_0 == MainForm.GEnum0.const_0) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 7) == 0)
				{
					this.mainForm_0.genum0_0 = MainForm.GEnum0.const_0;
					Class123.smethod_15();
					Class181.smethod_3(Enum11.const_0, MainForm.Class51.getString_0(107390132));
					((byte*)ptr)[8] = ((!this.mainForm_0.bool_3) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						Class181.smethod_3(Enum11.const_0, MainForm.Class51.getString_0(107250616));
						this.mainForm_0.method_63();
					}
					else
					{
						((byte*)ptr)[9] = ((!this.mainForm_0.bool_4) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 9) != 0)
						{
							Class307.smethod_0(ConfigOptions.OnBotStopped, MainForm.Class51.getString_0(107251011), MainForm.Class51.getString_0(107251026));
						}
					}
				}
			}

			static Class51()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class51));
			}

			public MainForm mainForm_0;

			public bool bool_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class52
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[3];
				this.mainForm_0.method_85(null);
				this.order_0.dateTime_1 = DateTime.Now;
				this.order_0.TradeCompleted = (this.bool_0 ? MainForm.Class52.getString_0(107250952) : MainForm.Class52.getString_0(107250993));
				Class181.smethod_8(this.order_0);
				*(byte*)ptr = (this.bool_0 ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					int num;
					((byte*)ptr)[1] = ((!int.TryParse(this.mainForm_0.label_5.Text, out num)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						Control label_ = this.mainForm_0.label_5;
						int num2;
						num = (num2 = num + 1);
						label_.Text = num2.ToString();
					}
				}
				else
				{
					int num3;
					((byte*)ptr)[2] = ((!int.TryParse(this.mainForm_0.label_4.Text, out num3)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						Control label_2 = this.mainForm_0.label_4;
						int num2;
						num3 = (num2 = num3 + 1);
						label_2.Text = num2.ToString();
						if (Class255.class105_0.method_4(ConfigOptions.OnMaxFailedNotification) && Class255.class105_0.method_6(ConfigOptions.MaxFailedNotificationThreshold) <= num3)
						{
							Class307.smethod_0(ConfigOptions.OnMaxFailedNotification, MainForm.Class52.getString_0(107250971), string.Format(MainForm.Class52.getString_0(107250938), num3));
						}
					}
				}
			}

			static Class52()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class52));
			}

			public MainForm mainForm_0;

			public Order order_0;

			public bool bool_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class53
		{
			internal void method_0()
			{
				this.mainForm_0.method_71(this.bool_0);
			}

			public MainForm mainForm_0;

			public bool bool_0;
		}

		[CompilerGenerated]
		private sealed class Class55
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[8];
				this.mainForm_0.bool_12 = false;
				this.mainForm_0.thread_2 = Thread.CurrentThread;
				*(byte*)ptr = ((Stashes.LoadState == Enum9.const_1) ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					((byte*)ptr)[1] = ((!Class306.smethod_0(0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						((byte*)ptr)[2] = (ProcessHelper.UsingVulkan ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) != 0)
						{
							Class181.smethod_3(Enum11.const_2, MainForm.Class55.getString_0(107250875));
						}
						else if (!string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.AccountName)) && !string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.League)) && !string.IsNullOrEmpty(Class255.class105_0.method_3(ConfigOptions.POESESSID)))
						{
							if (!this.bool_0 && Class255.class105_0.method_4(ConfigOptions.ForceNewInstance))
							{
								((byte*)ptr)[3] = ((!UI.smethod_26()) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 3) != 0)
								{
									Control control = this.mainForm_0;
									Action method;
									if ((method = this.action_0) == null)
									{
										method = (this.action_0 = new Action(this.method_1));
									}
									control.Invoke(method);
									Class181.smethod_3(Enum11.const_2, MainForm.Class55.getString_0(107250854));
									return;
								}
								UI.smethod_1();
								((byte*)ptr)[4] = (this.mainForm_0.method_40() ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 4) != 0)
								{
									((byte*)ptr)[5] = ((!UI.smethod_51()) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 5) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class55.getString_0(107250225));
										return;
									}
									((byte*)ptr)[6] = ((!UI.smethod_44(null)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 6) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class55.getString_0(107250140));
										return;
									}
								}
								else
								{
									((byte*)ptr)[7] = ((!UI.smethod_44(null)) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 7) != 0)
									{
										Class181.smethod_3(Enum11.const_2, MainForm.Class55.getString_0(107250140));
										return;
									}
								}
							}
							Control control2 = this.mainForm_0;
							Action method2;
							if ((method2 = this.action_1) == null)
							{
								method2 = (this.action_1 = new Action(this.method_2));
							}
							control2.Invoke(method2);
							Stashes.smethod_2(0);
						}
					}
				}
			}

			internal void method_1()
			{
				this.mainForm_0.tabControl_4.SelectedIndex = 0;
			}

			internal void method_2()
			{
				this.mainForm_0.method_68();
				Stashes.Tabs = null;
				Stashes.bool_0 = false;
				Stashes.Items.Clear();
				Stashes.Layout.Clear();
			}

			static Class55()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class55));
			}

			public MainForm mainForm_0;

			public bool bool_0;

			public Action action_0;

			public Action action_1;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class58
		{
			internal bool method_0(Player player_0)
			{
				return DateTime.Now.Subtract(player_0.AddedTime).TotalMinutes >= this.double_0;
			}

			public double double_0;
		}

		[CompilerGenerated]
		private sealed class Class59
		{
			internal void method_0()
			{
				this.jsonTab_0 = (JsonTab)this.mainForm_0.comboBox_27.SelectedItem;
			}

			public JsonTab jsonTab_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class60
		{
			internal bool method_0(Player player_1)
			{
				return player_1.name == this.player_0.name;
			}

			internal bool method_1(Order order_0)
			{
				return order_0.player.name == this.player_0.name;
			}

			internal bool method_2(Order order_0)
			{
				return order_0.player.name == this.player_0.name;
			}

			public Player player_0;
		}

		[CompilerGenerated]
		private sealed class Class61
		{
			internal bool method_0(Order order_0)
			{
				return order_0.player.name == this.player_0.name && order_0.tradeStates_0 == Order.TradeStates.NotStarted;
			}

			internal bool method_1(Order order_0)
			{
				return order_0.player.name == this.player_0.name;
			}

			internal bool method_2(Player player_1)
			{
				return player_1.name == this.player_0.name;
			}

			public Player player_0;

			public Predicate<Order> predicate_0;
		}

		[CompilerGenerated]
		private sealed class Class62
		{
			internal bool method_0(LiveSearchListItem liveSearchListItem_0)
			{
				return liveSearchListItem_0.Id == this.buyingTradeData_0.Query;
			}

			internal bool method_1(ItemBuyingListItem itemBuyingListItem_0)
			{
				return itemBuyingListItem_0.Id == this.buyingTradeData_0.Query;
			}

			internal bool method_2(BulkBuyingListItem bulkBuyingListItem_0)
			{
				return bulkBuyingListItem_0.QueryId == this.buyingTradeData_0.Query;
			}

			internal bool method_3(BuyingTradeData buyingTradeData_1)
			{
				return BuyMessageProcessor.smethod_5(this.buyingTradeData_0).Contains(buyingTradeData_1);
			}

			public BuyingTradeData buyingTradeData_0;
		}

		[CompilerGenerated]
		private sealed class Class66
		{
			[DebuggerStepThrough]
			internal void method_0()
			{
				MainForm.Class66.Class67 @class = new MainForm.Class66.Class67();
				@class.class66_0 = this;
				@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
				@class.int_0 = -1;
				AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
				asyncVoidMethodBuilder_.Start<MainForm.Class66.Class67>(ref @class);
			}

			public bool bool_0;

			public MainForm mainForm_0;

			private sealed class Class67 : IAsyncStateMachine
			{
				unsafe void IAsyncStateMachine.MoveNext()
				{
					void* ptr = stackalloc byte[4];
					int num = this.int_0;
					try
					{
						if (num != 0)
						{
							this.ienumerator_0 = this.class66_0.mainForm_0.fastObjectListView_1.Objects.GetEnumerator();
						}
						try
						{
							ConfiguredTaskAwaitable<Tuple<bool, string>>.ConfiguredTaskAwaiter awaiter;
							if (num == 0)
							{
								awaiter = this.configuredTaskAwaiter_0;
								this.configuredTaskAwaiter_0 = default(ConfiguredTaskAwaitable<Tuple<bool, string>>.ConfiguredTaskAwaiter);
								int num2 = -1;
								num = -1;
								this.int_0 = num2;
								goto IL_157;
							}
							IL_106:
							while (this.ienumerator_0.MoveNext())
							{
								this.class68_0 = new MainForm.Class68();
								this.class68_0.jsonTab_0 = (JsonTab)this.ienumerator_0.Current;
								*(byte*)ptr = ((Stashes.smethod_11(this.class68_0.jsonTab_0.i) == null) ? 1 : 0);
								if (*(sbyte*)ptr != 0)
								{
									Class181.smethod_3(Enum11.const_2, string.Format(MainForm.Class66.Class67.getString_0(107243694), this.class68_0.jsonTab_0.n));
								}
								else
								{
									awaiter = Task.Run<Tuple<bool, string>>(new Func<Task<Tuple<bool, string>>>(this.class68_0.method_0)).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										int num3 = 0;
										num = 0;
										this.int_0 = num3;
										this.configuredTaskAwaiter_0 = awaiter;
										MainForm.Class66.Class67 @class = this;
										this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<bool, string>>.ConfiguredTaskAwaiter, MainForm.Class66.Class67>(ref awaiter, ref @class);
										return;
									}
									goto IL_157;
								}
							}
							goto IL_19F;
							IL_157:
							this.tuple_1 = awaiter.GetResult();
							this.tuple_0 = this.tuple_1;
							this.tuple_1 = null;
							((byte*)ptr)[1] = ((UI.bitmap_0 != null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 1) != 0)
							{
								UI.bitmap_0.Dispose();
							}
							this.class66_0.bool_0 = this.tuple_0.Item1;
							((byte*)ptr)[2] = ((!this.tuple_0.Item1) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) == 0)
							{
								this.tuple_0 = null;
								this.class68_0 = null;
								goto IL_106;
							}
							Class181.smethod_3(Enum11.const_2, this.tuple_0.Item2);
							IL_19F:;
						}
						finally
						{
							if (num < 0)
							{
								IDisposable disposable = this.ienumerator_0 as IDisposable;
								if (disposable != null)
								{
									disposable.Dispose();
								}
							}
						}
						this.ienumerator_0 = null;
						((byte*)ptr)[3] = (this.class66_0.bool_0 ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) != 0)
						{
							this.class66_0.mainForm_0.fastObjectListView_1.ClearObjects();
							Class181.smethod_3(Enum11.const_0, MainForm.Class66.Class67.getString_0(107243117));
						}
						this.class66_0.mainForm_0.thread_5 = null;
						this.class66_0.mainForm_0.Invoke(new Action(this.class66_0.mainForm_0.method_175));
						this.class66_0.mainForm_0.enum8_0 = Enum8.const_1;
					}
					catch (Exception exception)
					{
						this.int_0 = -2;
						this.asyncVoidMethodBuilder_0.SetException(exception);
						return;
					}
					this.int_0 = -2;
					this.asyncVoidMethodBuilder_0.SetResult();
				}

				[DebuggerHidden]
				void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
				{
				}

				static Class67()
				{
					Strings.CreateGetStringDelegate(typeof(MainForm.Class66.Class67));
				}

				public int int_0;

				public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

				public MainForm.Class66 class66_0;

				private IEnumerator ienumerator_0;

				private MainForm.Class68 class68_0;

				private Tuple<bool, string> tuple_0;

				private Tuple<bool, string> tuple_1;

				private ConfiguredTaskAwaitable<Tuple<bool, string>>.ConfiguredTaskAwaiter configuredTaskAwaiter_0;

				[NonSerialized]
				internal static GetString getString_0;
			}
		}

		[CompilerGenerated]
		private sealed class Class68
		{
			internal Task<Tuple<bool, string>> method_0()
			{
				return CleanDumpTab.smethod_1(this.jsonTab_0.i);
			}

			public JsonTab jsonTab_0;
		}

		[CompilerGenerated]
		private sealed class Class70
		{
			internal bool method_0(LiveSearchListItem liveSearchListItem_1)
			{
				return liveSearchListItem_1.Id == this.string_0;
			}

			internal void method_1()
			{
				Class181.smethod_2(Enum11.const_3, MainForm.Class70.getString_0(107249830), new object[]
				{
					this.string_0,
					this.liveSearchListItem_0.MaxPrice
				});
				Class181.smethod_3(Enum11.const_0, string.Format(MainForm.Class70.getString_0(107249793), this.liveSearchListItem_0.Description, this.liveSearchListItem_0.MaxPrice));
				this.liveSearchListItem_0.ReconnectCount = 0;
			}

			static Class70()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class70));
			}

			public string string_0;

			public LiveSearchListItem liveSearchListItem_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class71
		{
			internal bool method_0(LiveSearchListItem liveSearchListItem_1)
			{
				return liveSearchListItem_1.Id == this.string_0;
			}

			public string string_0;

			public LiveSearchListItem liveSearchListItem_0;

			public Class260 class260_0;
		}

		[CompilerGenerated]
		private sealed class Class72
		{
			internal void method_0()
			{
				LiveSearchListItem liveSearchListItem_ = this.class71_0.liveSearchListItem_0;
				int reconnectCount = liveSearchListItem_.ReconnectCount;
				liveSearchListItem_.ReconnectCount = reconnectCount + 1;
				Class181.smethod_3(Enum11.const_0, string.Format(MainForm.Class72.getString_0(107249738), this.class71_0.liveSearchListItem_0.Description, this.class71_0.liveSearchListItem_0.ReconnectCount, this.int_0));
				Thread.Sleep(this.int_0 * 1000);
				this.class71_0.class260_0.method_1();
			}

			static Class72()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class72));
			}

			public int int_0;

			public MainForm.Class71 class71_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class73
		{
			internal unsafe void method_0()
			{
				void* ptr = stackalloc byte[2];
				*(byte*)ptr = ((this.order_0 == null) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					this.mainForm_0.label_9.Text = this.mainForm_0.label_3.Text;
					this.mainForm_0.label_8.Text = this.mainForm_0.label_2.Text;
					this.mainForm_0.label_7.Text = this.mainForm_0.label_1.Text;
					this.mainForm_0.label_3.Text = MainForm.Class73.getString_0(107396776);
					this.mainForm_0.label_2.Text = string.Empty;
					this.mainForm_0.label_1.Text = string.Empty;
					string text = MainForm.Class73.getString_0(107389199);
					((byte*)ptr)[1] = ((this.mainForm_0.order_0 != null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						text = string.Format(MainForm.Class73.getString_0(107249644), (this.mainForm_0.order_0.tradeStates_0 == Order.TradeStates.Complete) ? MainForm.Class73.getString_0(107249610) : MainForm.Class73.getString_0(107249615));
					}
					this.mainForm_0.groupBox_2.Text = text;
				}
				else
				{
					this.mainForm_0.label_3.Text = this.order_0.player.name;
					this.mainForm_0.label_2.Text = string.Format(MainForm.Class73.getString_0(107395084), this.order_0.my_item_amount, this.order_0.my_item_name);
					this.mainForm_0.label_1.Text = string.Format(MainForm.Class73.getString_0(107395084), this.order_0.player_item_amount, this.order_0.player_item_name);
					this.mainForm_0.order_0 = this.order_0;
				}
				this.mainForm_0.Refresh();
			}

			static Class73()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class73));
			}

			public Order order_0;

			public MainForm mainForm_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class74
		{
			internal List<Character> method_0()
			{
				return this.list_0 = Characters.smethod_0(this.dictionary_0).Where(new Func<Character, bool>(this.mainForm_0.method_178)).OrderBy(new Func<Character, string>(MainForm.<>c.<>9.method_129)).ToList<Character>();
			}

			internal unsafe void method_1()
			{
				void* ptr = stackalloc byte[7];
				this.mainForm_0.bool_14 = true;
				ComboBox.ObjectCollection items = this.mainForm_0.comboBox_0.Items;
				object[] items2 = this.list_0.ToArray();
				items.AddRange(items2);
				this.mainForm_0.bool_14 = false;
				((byte*)ptr)[4] = (this.list_0.Any<Character>() ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					*(int*)ptr = Class255.class105_0.method_5(ConfigOptions.CharacterSelected);
					((byte*)ptr)[5] = ((this.mainForm_0.comboBox_0.Items.Count - 1 < *(int*)ptr) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						this.mainForm_0.comboBox_0.SelectedIndex = -1;
					}
					else
					{
						this.mainForm_0.comboBox_0.SelectedIndex = *(int*)ptr;
					}
					((byte*)ptr)[6] = ((this.mainForm_0.comboBox_0.SelectedIndex == -1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 6) != 0)
					{
						this.mainForm_0.comboBox_0.SelectedIndex = 0;
					}
				}
				else
				{
					this.mainForm_0.comboBox_0.SelectedIndex = -1;
				}
				Class255.class105_0.method_9(ConfigOptions.CharacterSelected, this.mainForm_0.comboBox_0.SelectedIndex, true);
			}

			public List<Character> list_0;

			public Dictionary<string, string> dictionary_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class78
		{
			internal bool method_0(BuyingTradeData buyingTradeData_0)
			{
				return buyingTradeData_0.Id == this.fetchTradeResult_0.id;
			}

			public FetchTradeResult fetchTradeResult_0;
		}

		[CompilerGenerated]
		private sealed class Class79
		{
			internal bool method_0(LiveSearchListItem liveSearchListItem_0)
			{
				return liveSearchListItem_0.Id == this.string_0;
			}

			public string string_0;
		}

		[CompilerGenerated]
		private sealed class Class80
		{
			internal bool method_0(FetchTradeResult fetchTradeResult_1)
			{
				return fetchTradeResult_1 != null && fetchTradeResult_1.id == this.fetchTradeResult_0.id;
			}

			public FetchTradeResult fetchTradeResult_0;
		}

		[CompilerGenerated]
		private sealed class Class82
		{
			internal void method_0()
			{
				Class181.smethod_3(Enum11.const_0, MainForm.Class82.getString_0(107250048));
				if (this.blackout_0.method_2())
				{
					Class181.smethod_3(Enum11.const_0, MainForm.Class82.getString_0(107250055));
					this.blackout_0.method_3();
					Class181.smethod_3(Enum11.const_0, MainForm.Class82.getString_0(107249982));
					Control control = this.mainForm_0;
					Action method;
					if ((method = this.action_1) == null)
					{
						method = (this.action_1 = new Action(this.method_2));
					}
					control.Invoke(method);
					this.blackout_0.method_10();
					this.blackout_0 = null;
					Control control2 = this.mainForm_0;
					Action method2;
					if ((method2 = this.action_2) == null)
					{
						method2 = (this.action_2 = new Action(this.method_3));
					}
					control2.Invoke(method2);
					GC.Collect();
					GC.WaitForPendingFinalizers();
				}
				else
				{
					this.blackout_0.method_10();
					Control control3 = this.mainForm_0;
					Action method3;
					if ((method3 = this.action_0) == null)
					{
						method3 = (this.action_0 = new Action(this.method_1));
					}
					control3.Invoke(method3);
				}
			}

			internal void method_1()
			{
				this.mainForm_0.button_1.Enabled = true;
			}

			internal void method_2()
			{
				this.mainForm_0.toolStripProgressBar_0.Value = 0;
			}

			internal void method_3()
			{
				this.mainForm_0.button_1.Enabled = true;
			}

			internal void method_4()
			{
				this.mainForm_0.button_1.Enabled = true;
			}

			static Class82()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class82));
			}

			public Blackout blackout_0;

			public MainForm mainForm_0;

			public Action action_0;

			public Action action_1;

			public Action action_2;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class84
		{
			internal void method_0()
			{
				this.mainForm_0.toolStripProgressBar_0.Maximum = this.int_0;
			}

			public MainForm mainForm_0;

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class85
		{
			internal void method_0()
			{
				if (this.mainForm_0.toolStripProgressBar_0.Value + this.int_0 <= this.mainForm_0.toolStripProgressBar_0.Maximum)
				{
					this.mainForm_0.toolStripProgressBar_0.Value += this.int_0;
				}
			}

			public MainForm mainForm_0;

			public int int_0;
		}

		[CompilerGenerated]
		private sealed class Class86
		{
			internal void method_0()
			{
				Label label = null;
				switch (this.tradeTypes_0)
				{
				case TradeTypes.LiveSearch:
					label = this.mainForm_0.label_132;
					break;
				case TradeTypes.ItemBuying:
					label = this.mainForm_0.label_135;
					break;
				case TradeTypes.BulkBuying:
					label = this.mainForm_0.label_134;
					break;
				}
				Control control = label;
				IEnumerable<BuyingTradeData> source = this.mainForm_0.list_10.ToList<BuyingTradeData>();
				Func<BuyingTradeData, bool> predicate;
				if ((predicate = this.func_0) == null)
				{
					predicate = (this.func_0 = new Func<BuyingTradeData, bool>(this.method_1));
				}
				int num = source.Count(predicate);
				IEnumerable<Order> source2 = this.mainForm_0.list_9.ToList<Order>();
				Func<Order, bool> predicate2;
				if ((predicate2 = this.func_1) == null)
				{
					predicate2 = (this.func_1 = new Func<Order, bool>(this.method_2));
				}
				control.Text = (num + source2.Count(predicate2)).ToString();
			}

			internal bool method_1(BuyingTradeData buyingTradeData_0)
			{
				return buyingTradeData_0 != null && buyingTradeData_0.TradeType == this.tradeTypes_0;
			}

			internal bool method_2(Order order_0)
			{
				return order_0 != null && order_0.BuyType == this.tradeTypes_0;
			}

			public TradeTypes tradeTypes_0;

			public MainForm mainForm_0;

			public Func<BuyingTradeData, bool> func_0;

			public Func<Order, bool> func_1;
		}

		[CompilerGenerated]
		private sealed class Class87
		{
			internal bool method_0(BuyingTradeData buyingTradeData_0)
			{
				return buyingTradeData_0.TradeType == this.tradeTypes_0;
			}

			public TradeTypes tradeTypes_0;
		}

		[CompilerGenerated]
		private sealed class Class89
		{
			internal void method_0()
			{
				this.int_0 = int.Parse(this.mainForm_0.label_6.Text);
			}

			public int int_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class90
		{
			internal void method_0()
			{
				this.int_0 = int.Parse(this.mainForm_0.label_5.Text);
			}

			public int int_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class91
		{
			internal void method_0()
			{
				this.int_0 = int.Parse(this.mainForm_0.label_4.Text);
			}

			public int int_0;

			public MainForm mainForm_0;
		}

		[CompilerGenerated]
		private sealed class Class92
		{
			internal void method_0()
			{
				new VaalCrafter(this.mainForm_0, this.jsonTab_0).method_0();
			}

			public MainForm mainForm_0;

			public JsonTab jsonTab_0;
		}

		[CompilerGenerated]
		private sealed class Class93
		{
			internal void method_0()
			{
				new ToolTip().SetToolTip(this.mainForm_0.pictureBox_1, MainForm.Class93.getString_0(107249186) + this.string_0 + MainForm.Class93.getString_0(107249201));
				this.mainForm_0.pictureBox_1.Visible = true;
			}

			static Class93()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class93));
			}

			public MainForm mainForm_0;

			public string string_0;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class94
		{
			internal bool method_0(PoeNinja.Line line_0)
			{
				return line_0.Name == this.keyValuePair_0.Key;
			}

			public KeyValuePair<string, int> keyValuePair_0;
		}

		[CompilerGenerated]
		private sealed class Class95
		{
			internal bool method_0(Order order_0)
			{
				return order_0.BuyType == this.tradeTypes_0;
			}

			internal bool method_1(Order order_0)
			{
				return order_0.BuyType == this.tradeTypes_0;
			}

			internal bool method_2(Order order_0)
			{
				return order_0.BuyType == this.tradeTypes_0;
			}

			public TradeTypes tradeTypes_0;
		}

		[CompilerGenerated]
		private sealed class Class96
		{
			internal unsafe void method_0(object sender, ToolStripItemClickedEventArgs e)
			{
				void* ptr = stackalloc byte[2];
				this.contextMenuStrip_0.Hide();
				string text = e.ClickedItem.Text;
				*(byte*)ptr = ((text == MainForm.Class96.getString_0(107390304)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					this.action_0();
				}
				((byte*)ptr)[1] = ((text == MainForm.Class96.getString_0(107390315)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					this.action_1();
				}
			}

			static Class96()
			{
				Strings.CreateGetStringDelegate(typeof(MainForm.Class96));
			}

			public ContextMenuStrip contextMenuStrip_0;

			public Action action_0;

			public Action action_1;

			[NonSerialized]
			internal static GetString getString_0;
		}

		[CompilerGenerated]
		private sealed class Class97
		{
			internal bool method_0(LiveSearchListItem liveSearchListItem_0)
			{
				return liveSearchListItem_0.Id == this.keyValuePair_0.Key;
			}

			public KeyValuePair<string, Class260> keyValuePair_0;
		}
	}
}
