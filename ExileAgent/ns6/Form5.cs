using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ns13;
using ns27;
using ns29;
using ns32;
using ns36;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns6
{
	internal sealed partial class Form5 : Form
	{
		public event Form5.Delegate0 OnItemChosenEvent
		{
			[CompilerGenerated]
			add
			{
				Form5.Delegate0 @delegate = this.delegate0_0;
				Form5.Delegate0 delegate2;
				do
				{
					delegate2 = @delegate;
					Form5.Delegate0 value2 = (Form5.Delegate0)Delegate.Combine(delegate2, value);
					@delegate = Interlocked.CompareExchange<Form5.Delegate0>(ref this.delegate0_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
			[CompilerGenerated]
			remove
			{
				Form5.Delegate0 @delegate = this.delegate0_0;
				Form5.Delegate0 delegate2;
				do
				{
					delegate2 = @delegate;
					Form5.Delegate0 value2 = (Form5.Delegate0)Delegate.Remove(delegate2, value);
					@delegate = Interlocked.CompareExchange<Form5.Delegate0>(ref this.delegate0_0, value2, delegate2);
				}
				while (@delegate != delegate2);
			}
		}

		public Form5(int int_1)
		{
			this.method_4();
			base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			this.int_0 = int_1;
			this.method_0();
		}

		private unsafe void method_0()
		{
			void* ptr = stackalloc byte[18];
			((byte*)ptr)[12] = ((Stashes.Tabs == null) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) == 0)
			{
				JsonTab jsonTab = Stashes.smethod_11(this.int_0);
				if (jsonTab != null && jsonTab.IsSupported && Stashes.Items.ContainsKey(this.int_0))
				{
					this.Text = string.Format(Form5.getString_0(107371451), jsonTab.n);
					List<JsonItem> list = Stashes.Items[jsonTab.i];
					Enum10 enum10_ = UI.smethod_64(jsonTab.type);
					*(int*)ptr = (int)Class149.smethod_0(enum10_);
					Bitmap bitmap = this.method_3(jsonTab.type);
					this.panel_0.BackgroundImage = bitmap;
					foreach (JsonItem jsonItem in list)
					{
						((byte*)ptr)[13] = (Class308.dictionary_1.ContainsKey(jsonItem.CleanedTypeLine) ? 1 : 0);
						Bitmap bitmap2;
						if (*(sbyte*)((byte*)ptr + 13) != 0)
						{
							bitmap2 = new Bitmap(Class308.dictionary_1[jsonItem.CleanedTypeLine], new Size(*(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h));
						}
						else
						{
							((byte*)ptr)[14] = ((jsonItem.icon == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) != 0)
							{
								using (Bitmap bitmap3 = new Bitmap(Class238.x))
								{
									bitmap2 = new Bitmap(bitmap3, new Size(*(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h));
									goto IL_245;
								}
							}
							((byte*)ptr)[15] = (Form5.dictionary_0.ContainsKey(jsonItem.icon) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 15) != 0)
							{
								bitmap2 = Form5.dictionary_0[jsonItem.icon];
							}
							else
							{
								try
								{
									Stream responseStream = WebRequest.Create(jsonItem.icon).GetResponse().GetResponseStream();
									using (Bitmap bitmap4 = new Bitmap(responseStream))
									{
										bitmap2 = new Bitmap(bitmap4, *(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h);
									}
								}
								catch
								{
									using (Bitmap bitmap5 = new Bitmap(Class238.x))
									{
										bitmap2 = new Bitmap(bitmap5, new Size(*(int*)ptr * jsonItem.w, *(int*)ptr * jsonItem.h));
									}
								}
								Form5.dictionary_0.Add(jsonItem.icon, bitmap2);
							}
						}
						IL_245:
						jsonItem.ItemImage = bitmap2;
						using (Graphics graphics = Graphics.FromImage(bitmap))
						{
							if (jsonItem.BaseItemStackSize > 1 || jsonItem.stack > 1)
							{
								using (Graphics graphics2 = Graphics.FromImage(bitmap2))
								{
									using (Font font = new Font(Form5.getString_0(107398101), 10f, FontStyle.Bold))
									{
										Graphics graphics3 = graphics2;
										*(int*)((byte*)ptr + 8) = jsonItem.stack;
										SizeF sizeF = graphics3.MeasureString(((int*)((byte*)ptr + 8))->ToString(), font);
										Rectangle rect = new Rectangle(0, 0, (int)sizeF.Width, (int)sizeF.Height);
										Pen pen = new Pen(Color.White, 1f);
										pen.Alignment = PenAlignment.Inset;
										graphics2.FillRectangle(Brushes.Black, rect);
										graphics2.DrawRectangle(pen, rect);
										Graphics graphics4 = graphics2;
										*(int*)((byte*)ptr + 8) = jsonItem.stack;
										graphics4.DrawString(((int*)((byte*)ptr + 8))->ToString(), font, Brushes.White, 0f, 0f);
									}
								}
							}
							*(int*)((byte*)ptr + 4) = (int)Math.Round(Class149.smethod_0(enum10_) / 2.0);
							((byte*)ptr)[16] = (jsonTab.IsSpecialTab ? 1 : 0);
							Position position;
							if (*(sbyte*)((byte*)ptr + 16) != 0)
							{
								position = Class149.smethod_1(UI.smethod_62(jsonTab.type), jsonItem.x);
								position.Left -= (int)Math.Round(Class149.StashOffset.X - Class149.WindowOffset.X);
								position.Top -= (int)Math.Round(Class149.StashOffset.Y - Class149.WindowOffset.Y);
							}
							else
							{
								position = Class149.smethod_2(enum10_, jsonItem.x, jsonItem.y);
								position.Left += -(int)Math.Round(Class149.StashOffset.X + Class149.WindowOffset.X);
								position.Top += -(int)Math.Round(Class149.StashOffset.Y + Class149.WindowOffset.Y);
							}
							position.Left -= *(int*)((byte*)ptr + 4);
							position.Top -= *(int*)((byte*)ptr + 4);
							graphics.DrawImage(bitmap2, position.X, position.Y);
							((byte*)ptr)[17] = (jsonTab.IsSpecialTab ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 17) != 0)
							{
								this.method_1(jsonItem, bitmap2, new Point(position.X - 2, position.Y - 2), *(int*)ptr + 2);
							}
							else
							{
								this.method_1(jsonItem, bitmap2, new Point(position.X, position.Y), *(int*)ptr);
							}
						}
					}
				}
			}
		}

		private void method_1(JsonItem jsonItem_1, Bitmap bitmap_0, Point point_0, int int_1)
		{
			CheckBox checkBox = new CheckBox();
			checkBox.Appearance = Appearance.Button;
			checkBox.FlatAppearance.BorderColor = Color.FromArgb(61, 65, 69);
			checkBox.FlatAppearance.CheckedBackColor = Color.Silver;
			checkBox.FlatStyle = FlatStyle.Flat;
			checkBox.Size = new Size(int_1 * jsonItem_1.w, int_1 * jsonItem_1.h);
			checkBox.TabIndex = 1;
			checkBox.UseVisualStyleBackColor = true;
			checkBox.Image = bitmap_0;
			checkBox.Location = point_0;
			checkBox.BackColor = Color.Black;
			checkBox.Tag = jsonItem_1;
			checkBox.CheckedChanged += this.method_2;
			checkBox.AutoCheck = Form5.smethod_0(jsonItem_1);
			ToolTip toolTip = new ToolTip
			{
				InitialDelay = 100,
				AutoPopDelay = 60000
			};
			toolTip.SetToolTip(checkBox, jsonItem_1.method_3());
			this.panel_0.Controls.Add(checkBox);
		}

		private unsafe void method_2(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[3];
			*(byte*)ptr = (this.bool_0 ? 1 : 0);
			if (*(sbyte*)ptr == 0)
			{
				this.bool_0 = true;
				CheckBox checkBox = sender as CheckBox;
				foreach (object obj in this.panel_0.Controls)
				{
					((byte*)ptr)[1] = ((!(obj is CheckBox)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 1) == 0)
					{
						((byte*)ptr)[2] = ((obj == checkBox) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 2) == 0)
						{
							((CheckBox)obj).Checked = false;
						}
					}
				}
				this.bool_0 = false;
				this.jsonItem_0 = (JsonItem)checkBox.Tag;
			}
		}

		private void button_0_Click(object sender, EventArgs e)
		{
			this.delegate0_0(this.jsonItem_0);
			base.Close();
		}

		private Bitmap method_3(string string_0)
		{
			if (string_0 != null)
			{
				uint num = Class396.smethod_0(string_0);
				if (num <= 611440075U)
				{
					if (num <= 443440635U)
					{
						if (num != 54800890U)
						{
							if (num == 443440635U)
							{
								if (string_0 == Form5.getString_0(107381967))
								{
									return Class238.QuadStash;
								}
							}
						}
						else if (string_0 == Form5.getString_0(107381954))
						{
							return Class238.FragmentStash;
						}
					}
					else if (num != 599248347U)
					{
						if (num == 611440075U)
						{
							if (!(string_0 == Form5.getString_0(107380044)))
							{
							}
						}
					}
					else if (!(string_0 == Form5.getString_0(107379991)))
					{
					}
				}
				else if (num <= 2977208620U)
				{
					if (num != 809384579U)
					{
						if (num == 2977208620U)
						{
							if (string_0 == Form5.getString_0(107380029))
							{
								return Class238.DelveStash;
							}
						}
					}
					else if (string_0 == Form5.getString_0(107394029))
					{
						return Class238.CurrencyStash;
					}
				}
				else if (num != 3067167979U)
				{
					if (num != 3568218232U)
					{
						if (num == 4294430810U)
						{
							if (string_0 == Form5.getString_0(107380078))
							{
								return Class238.EssenceStash;
							}
						}
					}
					else if (!(string_0 == Form5.getString_0(107380002)))
					{
					}
				}
				else if (string_0 == Form5.getString_0(107381984))
				{
					return Class238.PremiumStash;
				}
			}
			return Class238.PremiumStash;
		}

		public static bool smethod_0(JsonItem jsonItem_1)
		{
			bool result;
			if (jsonItem_1.corrupted || !jsonItem_1.identified || jsonItem_1.IsGem || jsonItem_1.HasSocketedItemsInside)
			{
				result = false;
			}
			else
			{
				string type = API.smethod_7(jsonItem_1.typeLine).Type;
				string text = type;
				string text2 = text;
				result = (text2 != null && (text2 == Form5.getString_0(107400979) || text2 == Form5.getString_0(107371432) || text2 == Form5.getString_0(107371383)));
			}
			return result;
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
			this.button_0 = new Button();
			this.panel_0 = new Panel();
			this.panel_0.SuspendLayout();
			base.SuspendLayout();
			this.button_0.Anchor = AnchorStyles.None;
			this.button_0.Font = new Font(Form5.getString_0(107398101), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button_0.Location = new Point(530, 593);
			this.button_0.Name = Form5.getString_0(107371402);
			this.button_0.Size = new Size(98, 34);
			this.button_0.TabIndex = 0;
			this.button_0.Text = Form5.getString_0(107371861);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.panel_0.BackgroundImageLayout = ImageLayout.None;
			this.panel_0.Controls.Add(this.button_0);
			this.panel_0.Location = new Point(3, 3);
			this.panel_0.Name = Form5.getString_0(107371876);
			this.panel_0.Size = new Size(631, 630);
			this.panel_0.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new Size(637, 635);
			base.Controls.Add(this.panel_0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = Form5.getString_0(107371827);
			this.panel_0.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Form5()
		{
			Strings.CreateGetStringDelegate(typeof(Form5));
			Form5.dictionary_0 = new Dictionary<string, Bitmap>();
		}

		private static readonly Dictionary<string, Bitmap> dictionary_0;

		private int int_0;

		private bool bool_0 = false;

		private JsonItem jsonItem_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Form5.Delegate0 delegate0_0;

		private IContainer icontainer_0 = null;

		private Button button_0;

		private Panel panel_0;

		[NonSerialized]
		internal static GetString getString_0;

		public delegate void Delegate0(JsonItem item);
	}
}
