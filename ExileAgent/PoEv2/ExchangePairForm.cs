using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ns27;
using ns29;
using ns6;
using PoEv2.Classes;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2
{
	public sealed partial class ExchangePairForm : Form
	{
		public event ExchangePairForm.GDelegate0 OnPairSavedEvent
		{
			[CompilerGenerated]
			add
			{
				ExchangePairForm.GDelegate0 gdelegate = this.gdelegate0_0;
				ExchangePairForm.GDelegate0 gdelegate2;
				do
				{
					gdelegate2 = gdelegate;
					ExchangePairForm.GDelegate0 value2 = (ExchangePairForm.GDelegate0)Delegate.Combine(gdelegate2, value);
					gdelegate = Interlocked.CompareExchange<ExchangePairForm.GDelegate0>(ref this.gdelegate0_0, value2, gdelegate2);
				}
				while (gdelegate != gdelegate2);
			}
			[CompilerGenerated]
			remove
			{
				ExchangePairForm.GDelegate0 gdelegate = this.gdelegate0_0;
				ExchangePairForm.GDelegate0 gdelegate2;
				do
				{
					gdelegate2 = gdelegate;
					ExchangePairForm.GDelegate0 value2 = (ExchangePairForm.GDelegate0)Delegate.Remove(gdelegate2, value);
					gdelegate = Interlocked.CompareExchange<ExchangePairForm.GDelegate0>(ref this.gdelegate0_0, value2, gdelegate2);
				}
				while (gdelegate != gdelegate2);
			}
		}

		public ExchangePairForm()
		{
			this.method_5();
			this.comboBox_0.Items.smethod_22(Class102.list_2);
			this.comboBox_1.Items.smethod_22(Class102.list_2);
			this.comboBox_0.SelectedIndex = 0;
			this.comboBox_1.SelectedIndex = 0;
		}

		private CheckBox method_0(string string_0, Bitmap bitmap_0, Point point_0, bool bool_2)
		{
			CheckBox checkBox = new CheckBox();
			checkBox.Appearance = Appearance.Button;
			checkBox.FlatAppearance.BorderColor = Color.FromArgb(61, 65, 69);
			checkBox.FlatAppearance.CheckedBackColor = Color.Silver;
			checkBox.FlatStyle = FlatStyle.Flat;
			checkBox.TabIndex = 1;
			checkBox.UseVisualStyleBackColor = true;
			checkBox.Location = point_0;
			checkBox.BackColor = Color.Black;
			checkBox.Tag = string_0;
			if (bool_2)
			{
				checkBox.Text = string_0;
				checkBox.Font = new Font(ExchangePairForm.getString_0(107397872), 10f);
				checkBox.Size = new Size(115, 50);
			}
			else
			{
				checkBox.Image = bitmap_0;
				checkBox.Size = new Size(40, 40);
			}
			checkBox.CheckedChanged += this.method_1;
			return checkBox;
		}

		private unsafe void method_1(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			*(byte*)ptr = (this.bool_0 ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				this.bool_0 = true;
				CheckBox checkBox = sender as CheckBox;
				((byte*)ptr)[1] = (this.panel_0.Controls.Contains(checkBox) ? 1 : 0);
				Panel panel;
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					panel = this.panel_0;
				}
				else
				{
					panel = this.panel_1;
				}
				foreach (object obj in panel.Controls)
				{
					((byte*)ptr)[2] = ((!(obj is CheckBox)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 2) == 0)
					{
						((byte*)ptr)[3] = ((obj == checkBox) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 3) == 0)
						{
							((CheckBox)obj).Checked = false;
						}
					}
				}
				this.bool_0 = false;
			}
		}

		private unsafe void comboBox_1_SelectedIndexChanged(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[19];
			((byte*)ptr)[13] = (this.bool_1 ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 13) != 0)
			{
				this.bool_1 = false;
			}
			else
			{
				GroupBox groupBox;
				Panel panel;
				ComboBox value;
				ComboBox comboBox;
				string text;
				if (sender == this.comboBox_0 || sender == this.comboBox_2)
				{
					groupBox = this.groupBox_0;
					panel = this.panel_0;
					value = this.comboBox_0;
					comboBox = this.comboBox_2;
					text = (string)this.comboBox_0.SelectedItem;
				}
				else
				{
					groupBox = this.groupBox_1;
					panel = this.panel_1;
					value = this.comboBox_1;
					comboBox = this.comboBox_3;
					text = (string)this.comboBox_1.SelectedItem;
				}
				((byte*)ptr)[14] = ((text == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 14) == 0)
				{
					((byte*)ptr)[15] = ((sender != comboBox) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						((byte*)ptr)[16] = (this.method_3(comboBox, text) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) != 0)
						{
							this.bool_1 = true;
							comboBox.SelectedIndex = 0;
						}
					}
					panel.Controls.Clear();
					groupBox.Controls.Clear();
					((byte*)ptr)[17] = ((groupBox == this.groupBox_1) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 17) != 0)
					{
						groupBox.Controls.Add(this.button_0);
					}
					groupBox.Controls.Add(value);
					groupBox.Controls.Add(comboBox);
					groupBox.Controls.Add(panel);
					*(int*)ptr = 0;
					*(int*)((byte*)ptr + 4) = 3;
					*(int*)((byte*)ptr + 8) = 2;
					((byte*)ptr)[12] = (this.method_2(text) ? 1 : 0);
					int num = (*(sbyte*)((byte*)ptr + 12) != 0) ? 6 : 16;
					int num2 = ((*(sbyte*)((byte*)ptr + 12) != 0) ? 115 : 40) + 5;
					int num3 = ((*(sbyte*)((byte*)ptr + 12) != 0) ? 50 : 40) + 5;
					foreach (string text2 in this.method_4(text))
					{
						if (*(sbyte*)((byte*)ptr + 12) == 0 || text2.StartsWith(comboBox.smethod_1()))
						{
							Bitmap bitmap_ = (*(sbyte*)((byte*)ptr + 12) != 0) ? null : Class308.dictionary_1[text2];
							CheckBox checkBox = this.method_0(text2, bitmap_, new Point(*(int*)((byte*)ptr + 4), *(int*)((byte*)ptr + 8)), *(sbyte*)((byte*)ptr + 12) != 0);
							new ToolTip
							{
								InitialDelay = 100
							}.SetToolTip(checkBox, text2);
							panel.Controls.Add(checkBox);
							*(int*)ptr = *(int*)ptr + 1;
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + num2;
							((byte*)ptr)[18] = ((*(int*)ptr == num) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 18) != 0)
							{
								*(int*)((byte*)ptr + 4) = 3;
								*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + num3;
								*(int*)ptr = 0;
							}
						}
					}
				}
			}
		}

		private unsafe void button_0_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[4];
			string text = null;
			string text2 = null;
			foreach (object obj in this.panel_0.Controls)
			{
				*(byte*)ptr = ((!(obj is CheckBox)) ? 1 : 0);
				if (*(sbyte*)ptr == 0)
				{
					CheckBox checkBox = obj as CheckBox;
					((byte*)ptr)[1] = (checkBox.Checked ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) != 0)
					{
						text = checkBox.Tag.ToString();
						break;
					}
				}
			}
			foreach (object obj2 in this.panel_1.Controls)
			{
				((byte*)ptr)[2] = ((!(obj2 is CheckBox)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 2) == 0)
				{
					CheckBox checkBox2 = obj2 as CheckBox;
					((byte*)ptr)[3] = (checkBox2.Checked ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 3) != 0)
					{
						text2 = checkBox2.Tag.ToString();
						break;
					}
				}
			}
			if (!text.smethod_25() && !text2.smethod_25())
			{
				this.gdelegate0_0(text, text2);
				base.Close();
			}
		}

		private bool method_2(string string_0)
		{
			return string_0 != null && (string_0 == ExchangePairForm.getString_0(107361390) || string_0 == ExchangePairForm.getString_0(107400750) || string_0 == ExchangePairForm.getString_0(107352202) || string_0 == ExchangePairForm.getString_0(107355731));
		}

		private unsafe bool method_3(ComboBox comboBox_4, string string_0)
		{
			void* ptr = stackalloc byte[9];
			comboBox_4.Items.Clear();
			((byte*)ptr)[4] = ((string_0 == ExchangePairForm.getString_0(107361390)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				List<string> list = new List<string>();
				string cardSubTypes = ExchangePairForm.CardSubTypes;
				*(int*)ptr = 0;
				while (*(int*)ptr < cardSubTypes.Length)
				{
					ExchangePairForm.Class104 @class = new ExchangePairForm.Class104();
					@class.char_0 = cardSubTypes[*(int*)ptr];
					((byte*)ptr)[5] = (this.method_4(ExchangePairForm.getString_0(107361390)).Any(new Func<string, bool>(@class.method_0)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 5) != 0)
					{
						list.Add(@class.char_0.ToString());
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				comboBox_4.Items.smethod_22(list);
			}
			else
			{
				((byte*)ptr)[6] = ((string_0 == ExchangePairForm.getString_0(107400750)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 6) == 0)
				{
					((byte*)ptr)[7] = ((string_0 == ExchangePairForm.getString_0(107352202)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 7) == 0)
					{
						comboBox_4.Visible = false;
						((byte*)ptr)[8] = 0;
						goto IL_176;
					}
					ComboBox.ObjectCollection items = comboBox_4.Items;
					object[] items2 = ExchangePairForm.ProphecySubTypes.Select(new Func<char, string>(ExchangePairForm.<>c.<>9.method_0)).OrderBy(new Func<string, string>(ExchangePairForm.<>c.<>9.method_1)).ToArray<string>();
					items.AddRange(items2);
				}
			}
			comboBox_4.Visible = true;
			((byte*)ptr)[8] = 1;
			IL_176:
			return *(sbyte*)((byte*)ptr + 8) != 0;
		}

		private static string CardSubTypes
		{
			get
			{
				return ExchangePairForm.getString_0(107352153);
			}
		}

		private static string ProphecySubTypes
		{
			get
			{
				return ExchangePairForm.getString_0(107352153);
			}
		}

		private List<string> method_4(string string_0)
		{
			if (string_0 != null)
			{
				uint num = Class396.smethod_0(string_0);
				if (num <= 2019713471U)
				{
					if (num <= 707214052U)
					{
						if (num <= 342827606U)
						{
							if (num != 130333969U)
							{
								if (num == 342827606U)
								{
									if (string_0 == ExchangePairForm.getString_0(107393946))
									{
										return Class102.string_0.ToList<string>();
									}
								}
							}
							else if (string_0 == ExchangePairForm.getString_0(107355326))
							{
								return Class102.string_2.ToList<string>();
							}
						}
						else if (num != 662684201U)
						{
							if (num == 707214052U)
							{
								if (string_0 == ExchangePairForm.getString_0(107361390))
								{
									return API.smethod_17(ExchangePairForm.getString_0(107361390));
								}
							}
						}
						else if (string_0 == ExchangePairForm.getString_0(107360942))
						{
							return API.smethod_17(ExchangePairForm.getString_0(107360942));
						}
					}
					else if (num <= 1319512326U)
					{
						if (num != 946391602U)
						{
							if (num == 1319512326U)
							{
								if (string_0 == ExchangePairForm.getString_0(107355613))
								{
									return Class102.string_5.ToList<string>();
								}
							}
						}
						else if (string_0 == ExchangePairForm.getString_0(107355631))
						{
							return API.smethod_17(ExchangePairForm.getString_0(107361413));
						}
					}
					else if (num != 1631987724U)
					{
						if (num != 1997917396U)
						{
							if (num == 2019713471U)
							{
								if (string_0 == ExchangePairForm.getString_0(107352202))
								{
									return API.smethod_17(ExchangePairForm.getString_0(107352116));
								}
							}
						}
						else if (string_0 == ExchangePairForm.getString_0(107361036))
						{
							return API.smethod_17(ExchangePairForm.getString_0(107361036));
						}
					}
					else if (string_0 == ExchangePairForm.getString_0(107355301))
					{
						return API.smethod_17(ExchangePairForm.getString_0(107361000));
					}
				}
				else if (num <= 2718788228U)
				{
					if (num <= 2493534857U)
					{
						if (num != 2176199594U)
						{
							if (num == 2493534857U)
							{
								if (string_0 == ExchangePairForm.getString_0(107355713))
								{
									return Class102.string_6.ToList<string>();
								}
							}
						}
						else if (string_0 == ExchangePairForm.getString_0(107361340))
						{
							return API.smethod_17(ExchangePairForm.getString_0(107361340));
						}
					}
					else if (num != 2681109405U)
					{
						if (num == 2718788228U)
						{
							if (string_0 == ExchangePairForm.getString_0(107355684))
							{
								return Class102.string_3.ToList<string>();
							}
						}
					}
					else if (string_0 == ExchangePairForm.getString_0(107355606))
					{
						return Class102.string_4.ToList<string>();
					}
				}
				else if (num <= 3758576395U)
				{
					if (num != 3645813303U)
					{
						if (num == 3758576395U)
						{
							if (string_0 == ExchangePairForm.getString_0(107355756))
							{
								return API.smethod_17(ExchangePairForm.getString_0(107352082));
							}
						}
					}
					else if (string_0 == ExchangePairForm.getString_0(107361404))
					{
						return API.smethod_17(ExchangePairForm.getString_0(107361404));
					}
				}
				else if (num != 3872910352U)
				{
					if (num != 4076838657U)
					{
						if (num == 4290180212U)
						{
							if (string_0 == ExchangePairForm.getString_0(107355731))
							{
								return API.smethod_17(ExchangePairForm.getString_0(107352057));
							}
						}
					}
					else if (string_0 == ExchangePairForm.getString_0(107355706))
					{
						return API.smethod_17(ExchangePairForm.getString_0(107352135));
					}
				}
				else if (string_0 == ExchangePairForm.getString_0(107355319))
				{
					return Class102.string_1.ToList<string>();
				}
			}
			return new List<string>();
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_5()
		{
			this.groupBox_0 = new GroupBox();
			this.comboBox_2 = new ComboBox();
			this.comboBox_0 = new ComboBox();
			this.groupBox_1 = new GroupBox();
			this.comboBox_3 = new ComboBox();
			this.button_0 = new Button();
			this.comboBox_1 = new ComboBox();
			this.panel_0 = new Panel();
			this.panel_1 = new Panel();
			this.groupBox_0.SuspendLayout();
			this.groupBox_1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox_0.BackColor = Color.Black;
			this.groupBox_0.Controls.Add(this.comboBox_0);
			this.groupBox_0.Controls.Add(this.comboBox_2);
			this.groupBox_0.Controls.Add(this.panel_0);
			this.groupBox_0.ForeColor = Color.WhiteSmoke;
			this.groupBox_0.Location = new Point(1, 0);
			this.groupBox_0.Name = ExchangePairForm.getString_0(107352068);
			this.groupBox_0.Padding = new Padding(0);
			this.groupBox_0.Size = new Size(905, 336);
			this.groupBox_0.TabIndex = 0;
			this.groupBox_0.TabStop = false;
			this.groupBox_0.Text = ExchangePairForm.getString_0(107407869);
			this.comboBox_2.BackColor = Color.FromArgb(61, 65, 69);
			this.comboBox_2.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_2.ForeColor = Color.LightGray;
			this.comboBox_2.FormattingEnabled = true;
			this.comboBox_2.Location = new Point(814, 41);
			this.comboBox_2.Name = ExchangePairForm.getString_0(107352023);
			this.comboBox_2.Size = new Size(88, 22);
			this.comboBox_2.TabIndex = 1;
			this.comboBox_2.Visible = false;
			this.comboBox_2.SelectedIndexChanged += this.comboBox_1_SelectedIndexChanged;
			this.comboBox_0.BackColor = Color.FromArgb(61, 65, 69);
			this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_0.ForeColor = Color.LightGray;
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Location = new Point(754, 13);
			this.comboBox_0.Name = ExchangePairForm.getString_0(107352030);
			this.comboBox_0.Size = new Size(148, 22);
			this.comboBox_0.TabIndex = 0;
			this.comboBox_0.SelectedIndexChanged += this.comboBox_1_SelectedIndexChanged;
			this.groupBox_1.BackColor = Color.Black;
			this.groupBox_1.Controls.Add(this.comboBox_1);
			this.groupBox_1.Controls.Add(this.comboBox_3);
			this.groupBox_1.Controls.Add(this.panel_1);
			this.groupBox_1.Controls.Add(this.button_0);
			this.groupBox_1.ForeColor = Color.WhiteSmoke;
			this.groupBox_1.Location = new Point(1, 342);
			this.groupBox_1.Name = ExchangePairForm.getString_0(107352009);
			this.groupBox_1.Size = new Size(905, 336);
			this.groupBox_1.TabIndex = 1;
			this.groupBox_1.TabStop = false;
			this.groupBox_1.Text = ExchangePairForm.getString_0(107407828);
			this.comboBox_3.BackColor = Color.FromArgb(61, 65, 69);
			this.comboBox_3.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_3.ForeColor = Color.LightGray;
			this.comboBox_3.FormattingEnabled = true;
			this.comboBox_3.Location = new Point(814, 41);
			this.comboBox_3.Name = ExchangePairForm.getString_0(107351996);
			this.comboBox_3.Size = new Size(88, 22);
			this.comboBox_3.TabIndex = 3;
			this.comboBox_3.Visible = false;
			this.comboBox_3.SelectedIndexChanged += this.comboBox_1_SelectedIndexChanged;
			this.button_0.ForeColor = Color.Black;
			this.button_0.Location = new Point(825, 307);
			this.button_0.Name = ExchangePairForm.getString_0(107351971);
			this.button_0.Size = new Size(75, 23);
			this.button_0.TabIndex = 2;
			this.button_0.Text = ExchangePairForm.getString_0(107407103);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.comboBox_1.BackColor = Color.FromArgb(61, 65, 69);
			this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_1.ForeColor = Color.LightGray;
			this.comboBox_1.FormattingEnabled = true;
			this.comboBox_1.Location = new Point(754, 13);
			this.comboBox_1.Name = ExchangePairForm.getString_0(107351410);
			this.comboBox_1.Size = new Size(148, 22);
			this.comboBox_1.TabIndex = 1;
			this.comboBox_1.SelectedIndexChanged += this.comboBox_1_SelectedIndexChanged;
			this.panel_0.AutoScroll = true;
			this.panel_0.Location = new Point(3, 13);
			this.panel_0.Name = ExchangePairForm.getString_0(107351421);
			this.panel_0.Size = new Size(745, 320);
			this.panel_0.TabIndex = 2;
			this.panel_1.AutoScroll = true;
			this.panel_1.Location = new Point(3, 13);
			this.panel_1.Name = ExchangePairForm.getString_0(107351376);
			this.panel_1.Size = new Size(745, 320);
			this.panel_1.TabIndex = 4;
			base.AutoScaleDimensions = new SizeF(7f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new Size(907, 679);
			base.Controls.Add(this.groupBox_1);
			base.Controls.Add(this.groupBox_0);
			this.Font = new Font(ExchangePairForm.getString_0(107397872), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			this.MaximumSize = new Size(934, 758);
			base.MinimizeBox = false;
			base.Name = ExchangePairForm.getString_0(107351395);
			this.Text = ExchangePairForm.getString_0(107351370);
			this.groupBox_0.ResumeLayout(false);
			this.groupBox_1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		static ExchangePairForm()
		{
			Strings.CreateGetStringDelegate(typeof(ExchangePairForm));
		}

		private const int int_0 = 3;

		private const int int_1 = 2;

		private const int int_2 = 40;

		private const int int_3 = 40;

		private const int int_4 = 115;

		private const int int_5 = 50;

		private bool bool_0 = false;

		private bool bool_1 = false;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ExchangePairForm.GDelegate0 gdelegate0_0;

		private IContainer icontainer_0 = null;

		private GroupBox groupBox_0;

		private GroupBox groupBox_1;

		private Button button_0;

		private ComboBox comboBox_0;

		private ComboBox comboBox_1;

		private ComboBox comboBox_2;

		private ComboBox comboBox_3;

		private Panel panel_0;

		private Panel panel_1;

		[NonSerialized]
		internal static GetString getString_0;

		public delegate void GDelegate0(string have, string want);

		[CompilerGenerated]
		private sealed class Class104
		{
			internal bool method_0(string string_0)
			{
				return string_0.StartsWith(this.char_0.ToString());
			}

			public char char_0;
		}
	}
}
