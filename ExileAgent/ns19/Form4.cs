using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ns0;
using ns13;
using ns29;
using ns32;
using ns36;
using PoEv2;
using PoEv2.Classes;
using PoEv2.Models;
using PoEv2.PublicModels;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns19
{
	internal sealed partial class Form4 : Form
	{
		public Form4(int int_1, bool bool_0)
		{
			this.method_3();
			this.int_0 = int_1;
			base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
			if (bool_0)
			{
				Bitmap bitmap = this.method_0();
				if (bitmap != null)
				{
					this.pictureBox_0.Image = bitmap;
				}
			}
		}

		public unsafe Bitmap method_0()
		{
			void* ptr = stackalloc byte[17];
			((byte*)ptr)[12] = ((Stashes.Tabs == null) ? 1 : 0);
			Bitmap result;
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				result = null;
			}
			else
			{
				JsonTab jsonTab = Stashes.smethod_11(this.int_0);
				if (jsonTab == null || !jsonTab.IsSupported || !Stashes.Items.ContainsKey(this.int_0))
				{
					result = null;
				}
				else
				{
					this.Text = string.Format(Form4.getString_0(107371442), jsonTab.n);
					List<JsonItem> list = Stashes.Items[jsonTab.i];
					Enum10 enum10_ = UI.smethod_64(jsonTab.type);
					*(int*)ptr = (int)Class149.smethod_0(enum10_);
					Bitmap bitmap = null;
					string type = jsonTab.type;
					string text = type;
					if (text != null)
					{
						if (!(text == Form4.getString_0(107381958)))
						{
							if (!(text == Form4.getString_0(107394020)))
							{
								if (text == Form4.getString_0(107381975) || text == Form4.getString_0(107381960))
								{
									bitmap = Class238.PremiumStash;
								}
							}
							else
							{
								bitmap = Class238.CurrencyStash;
							}
						}
						else
						{
							bitmap = Class238.QuadStash;
						}
					}
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
									goto IL_2C0;
								}
							}
							((byte*)ptr)[15] = (Form4.dictionary_0.ContainsKey(jsonItem.icon) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 15) != 0)
							{
								bitmap2 = Form4.dictionary_0[jsonItem.icon];
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
								Form4.dictionary_0.Add(jsonItem.icon, bitmap2);
							}
						}
						IL_2C0:
						jsonItem.ItemImage = bitmap2;
						using (Graphics graphics = Graphics.FromImage(bitmap))
						{
							if (jsonItem.BaseItemStackSize > 1 || jsonItem.stack > 1)
							{
								using (Graphics graphics2 = Graphics.FromImage(bitmap2))
								{
									using (Font font = new Font(Form4.getString_0(107398092), 10f, FontStyle.Bold))
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
								position.Top += -(int)Math.Round(Class251.StashOffset.Y + Class149.WindowOffset.Y);
							}
							position.Left -= *(int*)((byte*)ptr + 4);
							position.Top -= *(int*)((byte*)ptr + 4);
							graphics.DrawImage(bitmap2, position.X, position.Y);
						}
					}
					result = bitmap;
				}
			}
			return result;
		}

		public void method_1()
		{
			base.Invoke(new Action(this.method_4));
		}

		public void method_2()
		{
			base.Move += this.Form4_Move;
			base.Resize += this.Form4_Resize;
		}

		private void Form4_Move(object sender, EventArgs e)
		{
			Class255.class105_0.method_9(ConfigOptions.CurrencyTabLocation, base.Location, false);
		}

		private void Form4_Resize(object sender, EventArgs e)
		{
			Class255.class105_0.method_9(ConfigOptions.CurrencyTabSize, base.ClientSize, false);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_3()
		{
			this.pictureBox_0 = new PictureBox();
			((ISupportInitialize)this.pictureBox_0).BeginInit();
			base.SuspendLayout();
			this.pictureBox_0.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.pictureBox_0.Location = new Point(3, 3);
			this.pictureBox_0.Name = Form4.getString_0(107371461);
			this.pictureBox_0.Size = new Size(631, 630);
			this.pictureBox_0.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox_0.TabIndex = 0;
			this.pictureBox_0.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Black;
			base.ClientSize = new Size(637, 635);
			base.Controls.Add(this.pictureBox_0);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = Form4.getString_0(107371412);
			((ISupportInitialize)this.pictureBox_0).EndInit();
			base.ResumeLayout(false);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Form4()
		{
			Strings.CreateGetStringDelegate(typeof(Form4));
			Form4.dictionary_0 = new Dictionary<string, Bitmap>();
		}

		[CompilerGenerated]
		private void method_4()
		{
			Bitmap image = this.method_0();
			this.pictureBox_0.Image = null;
			this.pictureBox_0.Image = image;
		}

		private static readonly Dictionary<string, Bitmap> dictionary_0;

		private int int_0;

		private IContainer icontainer_0 = null;

		private PictureBox pictureBox_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
