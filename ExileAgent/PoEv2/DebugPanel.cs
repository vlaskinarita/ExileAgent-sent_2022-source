using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ns0;
using ns1;
using ns12;
using ns14;
using ns25;
using ns29;
using ns33;
using ns34;
using ns35;
using ns36;
using ns37;
using ns6;
using ns8;
using ns9;
using PoEv2.Classes;
using PoEv2.Handlers.Events.Orders;
using PoEv2.Managers;
using PoEv2.Models;
using PoEv2.Models.Crafting;
using PoEv2.Models.Query;
using PoEv2.PublicModels;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2
{
	public sealed partial class DebugPanel : Form
	{
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr intptr_0, int int_2, int int_3, int int_4);

		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool OpenClipboard(IntPtr intptr_0);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseClipboard();

		[DllImport("User32.dll", SetLastError = true)]
		private static extern IntPtr GetClipboardData(uint uint_0);

		[DllImport("user32.dll")]
		private static extern IntPtr GetOpenClipboardWindow();

		[DllImport("user32.dll", SetLastError = true)]
		private static extern void keybd_event(byte byte_0, byte byte_1, int int_2, int int_3);

		[DllImport("user32.dll")]
		internal static extern uint SendInput(uint uint_0, [MarshalAs(UnmanagedType.LPArray)] [In] Class244.Struct5[] struct5_0, int int_2);

		public DebugPanel(MainForm form)
		{
			this.method_0();
			this.mainForm_0 = form;
		}

		private void button_4_Click(object sender, EventArgs e)
		{
			ProcessHelper.smethod_9();
			this.mainForm_0.thread_2 = new Thread(new ThreadStart(this.method_1));
			this.mainForm_0.thread_2.SetApartmentState(ApartmentState.STA);
			this.mainForm_0.thread_2.IsBackground = true;
			this.mainForm_0.thread_2.Start();
		}

		private void button_1_Click(object sender, EventArgs e)
		{
			this.mainForm_0.bool_2 = !this.mainForm_0.bool_2;
			bool bool_ = this.mainForm_0.bool_2;
			Class181.smethod_3(Enum11.const_0, bool_.ToString());
		}

		private void button_2_Click(object sender, EventArgs e)
		{
			Class181.smethod_3(Enum11.const_3, DebugPanel.getString_0(107396355));
			UI.smethod_63();
			Class181.smethod_3(Enum11.const_3, DebugPanel.getString_0(107396366));
		}

		private void textBox_2_Enter(object sender, EventArgs e)
		{
			if (this.textBox_2.Text == DebugPanel.getString_0(107396345))
			{
				this.textBox_2.Text = string.Empty;
			}
		}

		private unsafe void button_3_Click(object sender, EventArgs e)
		{
			void* ptr = stackalloc byte[2];
			*(byte*)ptr = ((this.comboBox_0.SelectedItem != null) ? 1 : 0);
			if (*(sbyte*)ptr != 0)
			{
				Process processById = Process.GetProcessById(int.Parse(this.comboBox_0.SelectedItem.ToString()));
				((byte*)ptr)[1] = ((processById == null) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) == 0)
				{
					UI.SetForegroundWindow(processById.MainWindowHandle);
					UI.smethod_1();
				}
			}
		}

		private void button_6_Click(object sender, EventArgs e)
		{
			if (this.comboBox_0.SelectedItem != null)
			{
				ProcessHelper.smethod_11(int.Parse(this.comboBox_0.SelectedItem.ToString()));
				Class181.smethod_3(Enum11.const_3, DebugPanel.getString_0(107396336));
			}
		}

		private void button_7_Click(object sender, EventArgs e)
		{
			this.comboBox_0.SelectedItem = string.Empty;
			this.comboBox_0.Items.Clear();
			ComboBox.ObjectCollection items = this.comboBox_0.Items;
			object[] items2 = ProcessHelper.smethod_10().Select(new Func<Process, string>(DebugPanel.<>c.<>9.method_0)).ToArray<string>();
			items.AddRange(items2);
			if (this.comboBox_0.Items.Count != 0)
			{
				this.comboBox_0.SelectedItem = this.comboBox_0.Items[0];
				this.button_6_Click(sender, e);
			}
		}

		private void DebugPanel_Shown(object sender, EventArgs e)
		{
			this.comboBox_0.Items.Clear();
			ComboBox.ObjectCollection items = this.comboBox_0.Items;
			object[] items2 = ProcessHelper.smethod_10().Select(new Func<Process, string>(DebugPanel.<>c.<>9.method_1)).ToArray<string>();
			items.AddRange(items2);
			if (this.comboBox_0.Items.Count != 0)
			{
				this.comboBox_0.SelectedItem = this.comboBox_0.Items[0];
				this.button_6_Click(sender, e);
			}
		}

		private void button_5_Click(object sender, EventArgs e)
		{
			ProcessHelper.smethod_15();
		}

		[DebuggerStepThrough]
		public void button_0_Click(object sender, EventArgs e)
		{
			DebugPanel.Class12 @class = new DebugPanel.Class12();
			@class.debugPanel_0 = this;
			@class.object_0 = sender;
			@class.eventArgs_0 = e;
			@class.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
			@class.int_0 = -1;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder_ = @class.asyncVoidMethodBuilder_0;
			asyncVoidMethodBuilder_.Start<DebugPanel.Class12>(ref @class);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void method_0()
		{
			this.panel_0 = new Panel();
			this.textBox_0 = new TextBox();
			this.textBox_1 = new TextBox();
			this.textBox_2 = new TextBox();
			this.button_0 = new Button();
			this.button_1 = new Button();
			this.button_2 = new Button();
			this.button_3 = new Button();
			this.button_4 = new Button();
			this.button_5 = new Button();
			this.textBox_3 = new TextBox();
			this.comboBox_0 = new ComboBox();
			this.button_6 = new Button();
			this.button_7 = new Button();
			this.textBox_4 = new TextBox();
			this.panel_0.SuspendLayout();
			base.SuspendLayout();
			this.panel_0.Controls.Add(this.textBox_0);
			this.panel_0.Controls.Add(this.textBox_1);
			this.panel_0.Controls.Add(this.textBox_2);
			this.panel_0.Controls.Add(this.button_0);
			this.panel_0.Controls.Add(this.button_1);
			this.panel_0.Controls.Add(this.button_2);
			this.panel_0.Controls.Add(this.button_3);
			this.panel_0.Controls.Add(this.button_4);
			this.panel_0.Controls.Add(this.button_5);
			this.panel_0.Controls.Add(this.textBox_3);
			this.panel_0.Controls.Add(this.comboBox_0);
			this.panel_0.Controls.Add(this.button_6);
			this.panel_0.Controls.Add(this.button_7);
			this.panel_0.Controls.Add(this.textBox_4);
			this.panel_0.Font = new Font(DebugPanel.getString_0(107396287), 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.panel_0.Location = new Point(5, 0);
			this.panel_0.Name = DebugPanel.getString_0(107396310);
			this.panel_0.Size = new Size(97, 396);
			this.panel_0.TabIndex = 28;
			this.textBox_0.Location = new Point(47, 247);
			this.textBox_0.Name = DebugPanel.getString_0(107396301);
			this.textBox_0.Size = new Size(38, 20);
			this.textBox_0.TabIndex = 31;
			this.textBox_0.Text = DebugPanel.getString_0(107396264);
			this.textBox_1.Location = new Point(3, 247);
			this.textBox_1.Name = DebugPanel.getString_0(107396259);
			this.textBox_1.Size = new Size(38, 20);
			this.textBox_1.TabIndex = 30;
			this.textBox_1.Text = DebugPanel.getString_0(107396264);
			this.textBox_2.Location = new Point(2, 221);
			this.textBox_2.Name = DebugPanel.getString_0(107396254);
			this.textBox_2.Size = new Size(93, 20);
			this.textBox_2.TabIndex = 29;
			this.textBox_2.Text = DebugPanel.getString_0(107396345);
			this.textBox_2.Enter += this.textBox_2_Enter;
			this.button_0.Location = new Point(15, 331);
			this.button_0.Name = DebugPanel.getString_0(107396277);
			this.button_0.Size = new Size(64, 25);
			this.button_0.TabIndex = 28;
			this.button_0.Text = DebugPanel.getString_0(107396232);
			this.button_0.UseVisualStyleBackColor = true;
			this.button_0.Click += this.button_0_Click;
			this.button_1.Location = new Point(7, 155);
			this.button_1.Name = DebugPanel.getString_0(107396223);
			this.button_1.Size = new Size(83, 25);
			this.button_1.TabIndex = 27;
			this.button_1.Text = DebugPanel.getString_0(107396242);
			this.button_1.UseVisualStyleBackColor = true;
			this.button_1.Click += this.button_1_Click;
			this.button_2.Location = new Point(8, 188);
			this.button_2.Name = DebugPanel.getString_0(107396201);
			this.button_2.Size = new Size(83, 25);
			this.button_2.TabIndex = 26;
			this.button_2.Text = DebugPanel.getString_0(107396188);
			this.button_2.UseVisualStyleBackColor = true;
			this.button_2.Click += this.button_2_Click;
			this.button_3.Location = new Point(42, 90);
			this.button_3.Name = DebugPanel.getString_0(107396203);
			this.button_3.Size = new Size(34, 25);
			this.button_3.TabIndex = 10;
			this.button_3.Text = DebugPanel.getString_0(107396666);
			this.button_3.UseVisualStyleBackColor = true;
			this.button_3.Click += this.button_3_Click;
			this.button_4.Location = new Point(3, 3);
			this.button_4.Name = DebugPanel.getString_0(107396693);
			this.button_4.Size = new Size(92, 41);
			this.button_4.TabIndex = 25;
			this.button_4.Text = DebugPanel.getString_0(107396648);
			this.button_4.UseVisualStyleBackColor = true;
			this.button_4.Click += this.button_4_Click;
			this.button_5.Location = new Point(15, 362);
			this.button_5.Name = DebugPanel.getString_0(107396659);
			this.button_5.Size = new Size(64, 25);
			this.button_5.TabIndex = 6;
			this.button_5.Text = DebugPanel.getString_0(107396650);
			this.button_5.UseVisualStyleBackColor = true;
			this.button_5.Click += this.button_5_Click;
			this.textBox_3.Location = new Point(16, 308);
			this.textBox_3.Name = DebugPanel.getString_0(107396609);
			this.textBox_3.Size = new Size(60, 20);
			this.textBox_3.TabIndex = 15;
			this.comboBox_0.FormattingEnabled = true;
			this.comboBox_0.Location = new Point(5, 51);
			this.comboBox_0.Name = DebugPanel.getString_0(107396632);
			this.comboBox_0.Size = new Size(69, 22);
			this.comboBox_0.TabIndex = 9;
			this.button_6.Location = new Point(6, 90);
			this.button_6.Name = DebugPanel.getString_0(107396583);
			this.button_6.Size = new Size(35, 25);
			this.button_6.TabIndex = 11;
			this.button_6.Text = DebugPanel.getString_0(107396590);
			this.button_6.UseVisualStyleBackColor = true;
			this.button_6.Click += this.button_6_Click;
			this.button_7.Location = new Point(6, 121);
			this.button_7.Name = DebugPanel.getString_0(107396553);
			this.button_7.Size = new Size(35, 25);
			this.button_7.TabIndex = 12;
			this.button_7.Text = DebugPanel.getString_0(107396564);
			this.button_7.UseVisualStyleBackColor = true;
			this.button_7.Click += this.button_7_Click;
			this.textBox_4.Location = new Point(0, 280);
			this.textBox_4.Name = DebugPanel.getString_0(107396559);
			this.textBox_4.Size = new Size(93, 20);
			this.textBox_4.TabIndex = 14;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(108, 404);
			base.ControlBox = false;
			base.Controls.Add(this.panel_0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = DebugPanel.getString_0(107396510);
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.Manual;
			base.Shown += this.DebugPanel_Shown;
			this.panel_0.ResumeLayout(false);
			this.panel_0.PerformLayout();
			base.ResumeLayout(false);
		}

		[CompilerGenerated]
		private void method_1()
		{
			this.mainForm_0.method_71(true);
		}

		static DebugPanel()
		{
			Strings.CreateGetStringDelegate(typeof(DebugPanel));
		}

		public const int int_0 = 161;

		public const int int_1 = 2;

		private MainForm mainForm_0;

		private IContainer icontainer_0 = null;

		private Panel panel_0;

		public TextBox textBox_0;

		public TextBox textBox_1;

		private TextBox textBox_2;

		private Button button_0;

		private Button button_1;

		private Button button_2;

		private Button button_3;

		private Button button_4;

		private Button button_5;

		public TextBox textBox_3;

		private ComboBox comboBox_0;

		private Button button_6;

		private Button button_7;

		private TextBox textBox_4;

		[NonSerialized]
		internal static GetString getString_0;

		private static class DebugHelpers
		{
			public static void smethod_0()
			{
				List<string> list = API.smethod_17(DebugPanel.DebugHelpers.getString_0(107359806));
				list = list.Select(new Func<string, string>(DebugPanel.DebugHelpers.<>c.<>9.method_0)).ToList<string>();
				File.WriteAllText(DebugPanel.DebugHelpers.getString_0(107225873), string.Join(Environment.NewLine, list));
				Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107225848));
			}

			public static Bitmap smethod_1(Bitmap bitmap_0)
			{
				int num = 10 * bitmap_0.Height / 52;
				Bitmap bitmap = new Bitmap(bitmap_0.Width - num * 2, bitmap_0.Height - num * 2);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(bitmap_0, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(num, num, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
				}
				return bitmap;
			}

			public unsafe static void smethod_2()
			{
				void* ptr = stackalloc byte[3];
				List<string> list = new List<string>();
				ResourceManager resourceManager = new ResourceManager(typeof(Class237));
				ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
				foreach (object obj in resourceSet)
				{
					Mods mods = JsonConvert.DeserializeObject<Mods>(((DictionaryEntry)obj).Value.ToString(), new JsonConverter[]
					{
						new Mods.GClass21()
					});
					foreach (string text in mods.AllMods.Where(new Func<Mod, bool>(DebugPanel.DebugHelpers.<>c.<>9.method_1)).Select(new Func<Mod, string>(DebugPanel.DebugHelpers.<>c.<>9.method_2)))
					{
						if (!text.smethod_25() && !(text == DebugPanel.DebugHelpers.getString_0(107363827)) && !(text == DebugPanel.DebugHelpers.getString_0(107363804)))
						{
							*(byte*)ptr = (text.StartsWith(DebugPanel.DebugHelpers.getString_0(107225839)) ? 1 : 0);
							if (*(sbyte*)ptr != 0)
							{
								string item = text.Replace(DebugPanel.DebugHelpers.getString_0(107225794), DebugPanel.DebugHelpers.getString_0(107395303));
								((byte*)ptr)[1] = ((!list.Contains(item)) ? 1 : 0);
								if (*(sbyte*)((byte*)ptr + 1) != 0)
								{
									list.Add(item);
								}
							}
							((byte*)ptr)[2] = ((!list.Contains(text)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								list.Add(text);
							}
						}
					}
				}
				list.Add(DebugPanel.DebugHelpers.getString_0(107225813));
				using (StreamWriter streamWriter = new StreamWriter(DebugPanel.DebugHelpers.getString_0(107225804)))
				{
					foreach (string value in list)
					{
						streamWriter.WriteLine(value);
					}
				}
				Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107225775));
			}

			public unsafe static void smethod_3()
			{
				void* ptr = stackalloc byte[17];
				WebClient webClient = new WebClient();
				string value = webClient.DownloadString(Class103.PoeNinjaCurrencyUrl);
				PoeNinja poeNinja = JsonConvert.DeserializeObject<PoeNinja>(value, Util.smethod_12());
				Dictionary<string, double> dictionary = new Dictionary<string, double>();
				PoeNinja.Line[] lines = poeNinja.Lines;
				*(int*)((byte*)ptr + 8) = 0;
				while (*(int*)((byte*)ptr + 8) < lines.Length)
				{
					PoeNinja.Line line = lines[*(int*)((byte*)ptr + 8)];
					((byte*)ptr)[12] = ((line.Pay == null) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 12) == 0 && (line.Pay.GetCurrencyId == 1L || line.Receive.GetCurrencyId == 1L))
					{
						((byte*)ptr)[13] = ((!Class102.string_0.Contains(line.CurrencyTypeName)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 13) == 0)
						{
							((byte*)ptr)[14] = ((line.CurrencyTypeName == DebugPanel.DebugHelpers.getString_0(107390007)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 14) == 0)
							{
								dictionary.Add(line.CurrencyTypeName, line.Receive.Value);
							}
						}
					}
					*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
				}
				*(double*)ptr = 0.0;
				foreach (JsonItem jsonItem in Stashes.Items[0])
				{
					((byte*)ptr)[15] = ((jsonItem.Name == DebugPanel.DebugHelpers.getString_0(107389950)) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 15) != 0)
					{
						*(double*)ptr = *(double*)ptr + (double)jsonItem.stack;
					}
					else
					{
						((byte*)ptr)[16] = ((!dictionary.ContainsKey(jsonItem.Name)) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 16) == 0)
						{
							*(double*)ptr = *(double*)ptr + dictionary[jsonItem.Name] * (double)jsonItem.stack;
						}
					}
				}
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107450462), new object[]
				{
					*(double*)ptr
				});
			}

			public unsafe static Bitmap smethod_4(string string_0, float float_0, int int_0)
			{
				void* ptr = stackalloc byte[12];
				PrivateFontCollection privateFontCollection = new PrivateFontCollection();
				*(int*)ptr = Class220.Fontin_Bold.Length;
				byte[] fontin_Bold = Class220.Fontin_Bold;
				IntPtr intPtr = Marshal.AllocCoTaskMem(*(int*)ptr);
				Marshal.Copy(fontin_Bold, 0, intPtr, *(int*)ptr);
				privateFontCollection.AddMemoryFont(intPtr, *(int*)ptr);
				Font font = new Font(privateFontCollection.Families[0], float_0);
				Bitmap bitmap = new Bitmap(300, 100);
				SolidBrush brush = new SolidBrush(Color.FromArgb(222, 173, 114));
				*(float*)((byte*)ptr + 4) = 0f;
				SizeF sizeF;
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(62, 30, 16)), new RectangleF(0f, 0f, (float)bitmap.Width, (float)bitmap.Height));
					GraphicsPath graphicsPath = new GraphicsPath();
					Pen pen = new Pen(Brushes.Black, 2f);
					pen.LineJoin = LineJoin.Round;
					*(int*)((byte*)ptr + 8) = 0;
					while (*(int*)((byte*)ptr + 8) < string_0.Length)
					{
						char c = string_0[*(int*)((byte*)ptr + 8)];
						PointF origin = new PointF(*(float*)((byte*)ptr + 4), 0f);
						graphicsPath.AddString(c.ToString(), font.FontFamily, (int)font.Style, float_0, origin, new StringFormat());
						graphics.DrawPath(pen, graphicsPath);
						graphics.FillPath(brush, graphicsPath);
						*(float*)((byte*)ptr + 4) = *(float*)((byte*)ptr + 4) + (graphics.MeasureString(c.ToString(), font).Width + (float)int_0);
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
					*(float*)((byte*)ptr + 4) = *(float*)((byte*)ptr + 4) + 4f;
					sizeF = graphics.MeasureString(string_0, font);
					sizeF.Height -= 3f;
					Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107393717), new object[]
					{
						*(float*)((byte*)ptr + 4),
						sizeF.Height
					});
				}
				Bitmap bitmap2 = new Bitmap((int)(*(float*)((byte*)ptr + 4)), (int)sizeF.Height);
				using (Graphics graphics2 = Graphics.FromImage(bitmap2))
				{
					graphics2.DrawImage(bitmap, 0, 0);
				}
				return bitmap2;
			}

			public static void smethod_5()
			{
				using (WebClient webClient = new WebClient())
				{
					string value = webClient.DownloadString(DebugPanel.DebugHelpers.getString_0(107225730));
					JObject jobject = JsonConvert.DeserializeObject<JObject>(value);
					using (StreamWriter streamWriter = new StreamWriter(DebugPanel.DebugHelpers.getString_0(107226173)))
					{
						foreach (JToken jtoken in ((IEnumerable<JToken>)jobject[DebugPanel.DebugHelpers.getString_0(107226152)]))
						{
							foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken[DebugPanel.DebugHelpers.getString_0(107226143)]))
							{
								if (jtoken2[DebugPanel.DebugHelpers.getString_0(107226162)] != null)
								{
									string text = jtoken2[DebugPanel.DebugHelpers.getString_0(107396346)].ToString();
									if (text.Contains(DebugPanel.DebugHelpers.getString_0(107437826)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107437796)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107437837)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107226121)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107226136)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107226127)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107226082)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107356962)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107437815)) || text == DebugPanel.DebugHelpers.getString_0(107356584) || text.Contains(DebugPanel.DebugHelpers.getString_0(107226097)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107355403)) || text.Contains(DebugPanel.DebugHelpers.getString_0(107226044)))
									{
										streamWriter.WriteLine(DebugPanel.DebugHelpers.getString_0(107226023), jtoken2[DebugPanel.DebugHelpers.getString_0(107396346)]);
									}
								}
							}
						}
					}
					Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107225848));
				}
			}

			public static void smethod_6()
			{
				UI.smethod_1();
				JsonItem jsonItem = StashManager.smethod_1(DebugPanel.DebugHelpers.getString_0(107354384), true).First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				JsonItem jsonItem2 = StashManager.smethod_1(DebugPanel.DebugHelpers.getString_0(107354321), true).First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				JsonItem jsonItem3 = StashManager.smethod_1(DebugPanel.DebugHelpers.getString_0(107353910), true).First<KeyValuePair<JsonTab, List<JsonItem>>>().Value.First<JsonItem>();
				UI.smethod_34(DebugPanel.DebugHelpers.getString_0(107380171), jsonItem.x, jsonItem.y, Enum2.const_2, false);
				Win32.smethod_2(true);
				UI.smethod_32(0, 0, Enum2.const_3, true);
				Win32.smethod_2(true);
				UI.smethod_34(DebugPanel.DebugHelpers.getString_0(107380171), jsonItem.x, jsonItem.y, Enum2.const_2, false);
				Win32.smethod_2(true);
				UI.smethod_34(DebugPanel.DebugHelpers.getString_0(107380171), jsonItem2.x, jsonItem2.y, Enum2.const_2, false);
				Win32.smethod_2(true);
				UI.smethod_32(1, 0, Enum2.const_3, true);
				Win32.smethod_2(true);
				UI.smethod_34(DebugPanel.DebugHelpers.getString_0(107380171), jsonItem2.x, jsonItem2.y, Enum2.const_2, false);
				Win32.smethod_2(true);
				UI.smethod_34(DebugPanel.DebugHelpers.getString_0(107380171), jsonItem3.x, jsonItem3.y, Enum2.const_2, false);
				Win32.smethod_2(true);
				UI.smethod_32(1, 1, Enum2.const_3, true);
				Win32.smethod_2(true);
				UI.smethod_34(DebugPanel.DebugHelpers.getString_0(107380171), jsonItem3.x, jsonItem3.y, Enum2.const_2, false);
				Win32.smethod_2(true);
			}

			public unsafe static void smethod_7()
			{
				void* ptr = stackalloc byte[20];
				string[] array = File.ReadAllText(DebugPanel.DebugHelpers.getString_0(107226014)).smethod_18(Environment.NewLine);
				List<int> list = new List<int>();
				string[] array2 = array;
				*(int*)ptr = 0;
				while (*(int*)ptr < array2.Length)
				{
					string text = array2[*(int*)ptr];
					*(int*)((byte*)ptr + 4) = text.IndexOf(DebugPanel.DebugHelpers.getString_0(107451000));
					*(int*)((byte*)ptr + 8) = text.IndexOf(DebugPanel.DebugHelpers.getString_0(107451000), *(int*)((byte*)ptr + 4) + 1);
					*(int*)((byte*)ptr + 12) = int.Parse(text.Substring(*(int*)((byte*)ptr + 4) + 1, *(int*)((byte*)ptr + 8) - *(int*)((byte*)ptr + 4) - 1));
					list.Add(*(int*)((byte*)ptr + 12));
					*(int*)ptr = *(int*)ptr + 1;
				}
				IEnumerable<int> collection = File.ReadAllText(DebugPanel.DebugHelpers.getString_0(107225981)).smethod_18(Environment.NewLine).Select(new Func<string, int>(DebugPanel.DebugHelpers.<>c.<>9.method_3));
				list.AddRange(collection);
				list = list.Distinct<int>().ToList<int>();
				string text2 = File.ReadAllText(DebugPanel.DebugHelpers.getString_0(107225948));
				string text3 = text2;
				foreach (int num in list)
				{
					*(int*)((byte*)ptr + 16) = num;
					text3 = text3 + Environment.NewLine + text2.Replace(DebugPanel.DebugHelpers.getString_0(107225403), ((int*)((byte*)ptr + 16))->ToString());
				}
				File.WriteAllText(DebugPanel.DebugHelpers.getString_0(107225426), text3);
			}

			public static void smethod_8()
			{
				string[] array = File.ReadAllText(Class238.POEDB_Json).smethod_18(Environment.NewLine);
				using (WebClient webClient = new WebClient())
				{
					foreach (string text in array)
					{
						if (!text.StartsWith(DebugPanel.DebugHelpers.getString_0(107225393)) && !text.smethod_25())
						{
							File.WriteAllText(DebugPanel.DebugHelpers.getString_0(107225388) + text.Replace(DebugPanel.DebugHelpers.getString_0(107225343), DebugPanel.DebugHelpers.getString_0(107395303)).Replace(DebugPanel.DebugHelpers.getString_0(107388487), DebugPanel.DebugHelpers.getString_0(107395303)) + DebugPanel.DebugHelpers.getString_0(107390862), webClient.DownloadString(text));
						}
					}
				}
			}

			public unsafe static void smethod_9(string string_0)
			{
				void* ptr = stackalloc byte[34];
				UI.smethod_1();
				((byte*)ptr)[24] = ((string_0 == DebugPanel.DebugHelpers.getString_0(107367492)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 24) != 0)
				{
					*(int*)ptr = 0;
					for (;;)
					{
						((byte*)ptr)[26] = ((*(int*)ptr < 11) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 26) == 0)
						{
							break;
						}
						*(int*)((byte*)ptr + 4) = 0;
						for (;;)
						{
							((byte*)ptr)[25] = ((*(int*)((byte*)ptr + 4) < 4) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 25) == 0)
							{
								break;
							}
							Rectangle rectangle_ = UI.smethod_93(*(int*)ptr, *(int*)((byte*)ptr + 4), false);
							Bitmap bitmap = Class197.smethod_1(rectangle_, DebugPanel.DebugHelpers.getString_0(107395303));
							bitmap.Save(string.Format(DebugPanel.DebugHelpers.getString_0(107225294), *(int*)ptr, *(int*)((byte*)ptr + 4)));
							*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
						}
						*(int*)ptr = *(int*)ptr + 1;
					}
				}
				((byte*)ptr)[27] = ((string_0 == DebugPanel.DebugHelpers.getString_0(107388855)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 27) != 0)
				{
					*(int*)((byte*)ptr + 8) = 0;
					for (;;)
					{
						((byte*)ptr)[29] = ((*(int*)((byte*)ptr + 8) < 11) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 29) == 0)
						{
							break;
						}
						*(int*)((byte*)ptr + 12) = 0;
						for (;;)
						{
							((byte*)ptr)[28] = ((*(int*)((byte*)ptr + 12) < 4) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 28) == 0)
							{
								break;
							}
							Rectangle rectangle_2 = UI.smethod_93(*(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12), false);
							Bitmap bitmap2 = Class197.smethod_1(rectangle_2, DebugPanel.DebugHelpers.getString_0(107395303));
							bitmap2.Save(string.Format(DebugPanel.DebugHelpers.getString_0(107225213), *(int*)((byte*)ptr + 8), *(int*)((byte*)ptr + 12)));
							*(int*)((byte*)ptr + 12) = *(int*)((byte*)ptr + 12) + 1;
						}
						*(int*)((byte*)ptr + 8) = *(int*)((byte*)ptr + 8) + 1;
					}
				}
				((byte*)ptr)[30] = ((string_0 == DebugPanel.DebugHelpers.getString_0(107225200)) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 30) != 0)
				{
					*(int*)((byte*)ptr + 16) = 0;
					for (;;)
					{
						((byte*)ptr)[33] = ((*(int*)((byte*)ptr + 16) < 11) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 33) == 0)
						{
							break;
						}
						*(int*)((byte*)ptr + 20) = 0;
						for (;;)
						{
							((byte*)ptr)[32] = ((*(int*)((byte*)ptr + 20) < 4) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 32) == 0)
							{
								break;
							}
							Bitmap bitmap_ = (Bitmap)Image.FromFile(string.Format(DebugPanel.DebugHelpers.getString_0(107225294), *(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 20)));
							Bitmap bitmap_2 = (Bitmap)Image.FromFile(string.Format(DebugPanel.DebugHelpers.getString_0(107225213), *(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 20)));
							List<Rectangle> source = UI.smethod_59(bitmap_, bitmap_2, DebugPanel.DebugHelpers.getString_0(107395303), 0.4, 0);
							((byte*)ptr)[31] = (source.Any<Rectangle>() ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 31) != 0)
							{
								UI.smethod_59(bitmap_, bitmap_2, string.Format(DebugPanel.DebugHelpers.getString_0(107225195), *(int*)((byte*)ptr + 16), *(int*)((byte*)ptr + 20)), 0.4, 0);
								Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107436780), new object[]
								{
									*(int*)((byte*)ptr + 16),
									*(int*)((byte*)ptr + 20)
								});
							}
							*(int*)((byte*)ptr + 20) = *(int*)((byte*)ptr + 20) + 1;
						}
						*(int*)((byte*)ptr + 16) = *(int*)((byte*)ptr + 16) + 1;
					}
				}
			}

			public static void smethod_10()
			{
				string str = DebugPanel.DebugHelpers.getString_0(107377689);
				Bitmap lillyTitle = Class235.LillyTitle;
				foreach (Class253 @class in Class120.list_1)
				{
					double num = (double)@class.Height / 1080.0;
					Bitmap bitmap = Util.smethod_1(lillyTitle, (int)Math.Round((double)lillyTitle.Width * num), (int)Math.Round((double)lillyTitle.Height * num));
					string text = string.Format(DebugPanel.DebugHelpers.getString_0(107225678), @class.Width, @class.Height);
					Directory.CreateDirectory(text);
					bitmap.Save(text + str + DebugPanel.DebugHelpers.getString_0(107225605));
				}
				Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107348535));
			}

			public unsafe static void smethod_11()
			{
				void* ptr = stackalloc byte[2];
				UI.smethod_1();
				*(byte*)ptr = ((UI.bitmap_0 == null) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					UI.bitmap_0 = (Bitmap)Image.FromFile(DebugPanel.DebugHelpers.getString_0(107225596));
					UI.smethod_65();
				}
				((byte*)ptr)[1] = ((!UI.smethod_71()) ? 1 : 0);
				if (*(sbyte*)((byte*)ptr + 1) != 0)
				{
					UI.smethod_65();
				}
				else
				{
					UI.smethod_66();
					List<JsonItem> list_ = InventoryManager.smethod_6(UI.list_0.smethod_17().Select(new Func<TradeWindowItem, Item>(DebugPanel.DebugHelpers.<>c.<>9.method_4)).ToList<Item>());
					Util.smethod_17(InventoryManager.smethod_7(list_), false);
				}
			}

			public unsafe static void smethod_12()
			{
				void* ptr = stackalloc byte[4];
				List<string> list = Class238.POEDB_Json.smethod_19(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
				list.RemoveAll(new Predicate<string>(DebugPanel.DebugHelpers.<>c.<>9.method_5));
				*(byte*)ptr = (Directory.Exists(DebugPanel.DebugHelpers.getString_0(107225388)) ? 1 : 0);
				if (*(sbyte*)ptr != 0)
				{
					Directory.Delete(DebugPanel.DebugHelpers.getString_0(107225388), true);
				}
				Directory.CreateDirectory(DebugPanel.DebugHelpers.getString_0(107225388));
				using (WebClient webClient = new WebClient())
				{
					foreach (string text in list)
					{
						string text2 = DebugPanel.DebugHelpers.smethod_13(text);
						string path = DebugPanel.DebugHelpers.getString_0(107225388) + text2 + DebugPanel.DebugHelpers.getString_0(107390862);
						((byte*)ptr)[1] = (File.Exists(path) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) == 0)
						{
							string text3 = webClient.DownloadString(text);
							((byte*)ptr)[2] = ((text2 == DebugPanel.DebugHelpers.getString_0(107442276)) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								Regex regex = new Regex(DebugPanel.DebugHelpers.getString_0(107225575));
								for (;;)
								{
									((byte*)ptr)[3] = (regex.IsMatch(text3) ? 1 : 0);
									if (*(sbyte*)((byte*)ptr + 3) == 0)
									{
										break;
									}
									Match match = regex.Match(text3);
									text3 = text3.Replace(DebugPanel.DebugHelpers.getString_0(107281222) + match.Groups[1].Value, DebugPanel.DebugHelpers.getString_0(107395303));
								}
								text3 = Regex.Replace(text3, DebugPanel.DebugHelpers.getString_0(107225510), DebugPanel.DebugHelpers.getString_0(107395303));
							}
							File.WriteAllText(path, text3);
							Class181.smethod_3(Enum11.const_0, text2);
						}
					}
				}
				Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107348535));
			}

			private unsafe static string smethod_13(string string_0)
			{
				void* ptr = stackalloc byte[9];
				string[] array = new string[]
				{
					DebugPanel.DebugHelpers.getString_0(107225343),
					DebugPanel.DebugHelpers.getString_0(107225489),
					DebugPanel.DebugHelpers.getString_0(107224920),
					DebugPanel.DebugHelpers.getString_0(107224863),
					DebugPanel.DebugHelpers.getString_0(107224886),
					DebugPanel.DebugHelpers.getString_0(107388487),
					DebugPanel.DebugHelpers.getString_0(107224881),
					DebugPanel.DebugHelpers.getString_0(107224828),
					DebugPanel.DebugHelpers.getString_0(107224803),
					DebugPanel.DebugHelpers.getString_0(107224826),
					DebugPanel.DebugHelpers.getString_0(107224777),
					DebugPanel.DebugHelpers.getString_0(107224792),
					DebugPanel.DebugHelpers.getString_0(107224739),
					DebugPanel.DebugHelpers.getString_0(107224750)
				};
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>
				{
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224729), DebugPanel.DebugHelpers.getString_0(107440616)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224676), DebugPanel.DebugHelpers.getString_0(107224691)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224686), DebugPanel.DebugHelpers.getString_0(107225149)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107225176), DebugPanel.DebugHelpers.getString_0(107225127)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107225122), DebugPanel.DebugHelpers.getString_0(107225133)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107225092), DebugPanel.DebugHelpers.getString_0(107225103)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107225062), DebugPanel.DebugHelpers.getString_0(107225073)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107225032), DebugPanel.DebugHelpers.getString_0(107225035)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224990), DebugPanel.DebugHelpers.getString_0(107225009)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107225004), DebugPanel.DebugHelpers.getString_0(107224959)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224986), DebugPanel.DebugHelpers.getString_0(107440277)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224937), DebugPanel.DebugHelpers.getString_0(107440277)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224948), DebugPanel.DebugHelpers.getString_0(107440277)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224387), DebugPanel.DebugHelpers.getString_0(107440277)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224406), DebugPanel.DebugHelpers.getString_0(107224353)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224376), DebugPanel.DebugHelpers.getString_0(107224323)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224334), DebugPanel.DebugHelpers.getString_0(107224313)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224260), DebugPanel.DebugHelpers.getString_0(107224231)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224202), DebugPanel.DebugHelpers.getString_0(107224209)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224156), DebugPanel.DebugHelpers.getString_0(107224635)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224618), DebugPanel.DebugHelpers.getString_0(107224577)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224548), DebugPanel.DebugHelpers.getString_0(107224555)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224530), DebugPanel.DebugHelpers.getString_0(107224481)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224496), DebugPanel.DebugHelpers.getString_0(107224447)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224462), DebugPanel.DebugHelpers.getString_0(107224433)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223892), DebugPanel.DebugHelpers.getString_0(107223847)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223858), DebugPanel.DebugHelpers.getString_0(107223817)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223828), DebugPanel.DebugHelpers.getString_0(107223779)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223802), DebugPanel.DebugHelpers.getString_0(107223757)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223684), DebugPanel.DebugHelpers.getString_0(107223671)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224142), DebugPanel.DebugHelpers.getString_0(107224061)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107224048), DebugPanel.DebugHelpers.getString_0(107223971)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223962), DebugPanel.DebugHelpers.getString_0(107223925)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223380), DebugPanel.DebugHelpers.getString_0(107223303)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223266), DebugPanel.DebugHelpers.getString_0(107223281)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223232), DebugPanel.DebugHelpers.getString_0(107223211)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223194), DebugPanel.DebugHelpers.getString_0(107223211)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223145), DebugPanel.DebugHelpers.getString_0(107223636)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223559), DebugPanel.DebugHelpers.getString_0(107223518)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223513), DebugPanel.DebugHelpers.getString_0(107223456)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223431), DebugPanel.DebugHelpers.getString_0(107223442)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223389), DebugPanel.DebugHelpers.getString_0(107223404)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222843), DebugPanel.DebugHelpers.getString_0(107222826)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222841), DebugPanel.DebugHelpers.getString_0(107222784)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222759), DebugPanel.DebugHelpers.getString_0(107222730)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222737), DebugPanel.DebugHelpers.getString_0(107222688)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222703), DebugPanel.DebugHelpers.getString_0(107222654)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222669), DebugPanel.DebugHelpers.getString_0(107222636)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223115), DebugPanel.DebugHelpers.getString_0(107223094)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107223041), DebugPanel.DebugHelpers.getString_0(107223024)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222951), DebugPanel.DebugHelpers.getString_0(107222922)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222929), DebugPanel.DebugHelpers.getString_0(107222900)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222331), DebugPanel.DebugHelpers.getString_0(107222330)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222269), DebugPanel.DebugHelpers.getString_0(107222284)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222239), DebugPanel.DebugHelpers.getString_0(107222254)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222205), DebugPanel.DebugHelpers.getString_0(107222188)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222111), DebugPanel.DebugHelpers.getString_0(107222606)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222533), DebugPanel.DebugHelpers.getString_0(107222516)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222439), DebugPanel.DebugHelpers.getString_0(107222450)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222401), DebugPanel.DebugHelpers.getString_0(107222416)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107222367), DebugPanel.DebugHelpers.getString_0(107221830)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107221841), DebugPanel.DebugHelpers.getString_0(107221788)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107221767), DebugPanel.DebugHelpers.getString_0(107221782)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107221733), DebugPanel.DebugHelpers.getString_0(107221740)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107221719), DebugPanel.DebugHelpers.getString_0(107221670)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107221685), DebugPanel.DebugHelpers.getString_0(107221636)),
					new KeyValuePair<string, string>(DebugPanel.DebugHelpers.getString_0(107221651), DebugPanel.DebugHelpers.getString_0(107221598))
				};
				string[] array2 = new string[]
				{
					DebugPanel.DebugHelpers.getString_0(107443487),
					DebugPanel.DebugHelpers.getString_0(107444217),
					DebugPanel.DebugHelpers.getString_0(107440986),
					DebugPanel.DebugHelpers.getString_0(107444220),
					DebugPanel.DebugHelpers.getString_0(107442302),
					DebugPanel.DebugHelpers.getString_0(107444109),
					DebugPanel.DebugHelpers.getString_0(107441515),
					DebugPanel.DebugHelpers.getString_0(107443401),
					DebugPanel.DebugHelpers.getString_0(107440588),
					DebugPanel.DebugHelpers.getString_0(107443653),
					DebugPanel.DebugHelpers.getString_0(107440254),
					DebugPanel.DebugHelpers.getString_0(107443427),
					DebugPanel.DebugHelpers.getString_0(107443442),
					DebugPanel.DebugHelpers.getString_0(107444198),
					DebugPanel.DebugHelpers.getString_0(107443545),
					DebugPanel.DebugHelpers.getString_0(107444068)
				};
				string text = string_0;
				string[] array3 = array;
				*(int*)ptr = 0;
				while (*(int*)ptr < array3.Length)
				{
					string oldValue = array3[*(int*)ptr];
					text = text.Replace(oldValue, string.Empty);
					*(int*)ptr = *(int*)ptr + 1;
				}
				foreach (KeyValuePair<string, string> keyValuePair in list)
				{
					text = text.Replace(keyValuePair.Key, keyValuePair.Value);
				}
				text = text.Replace(DebugPanel.DebugHelpers.getString_0(107221613), string.Empty);
				string[] array4 = array2;
				*(int*)((byte*)ptr + 4) = 0;
				while (*(int*)((byte*)ptr + 4) < array4.Length)
				{
					string text2 = array4[*(int*)((byte*)ptr + 4)];
					((byte*)ptr)[8] = (text.Contains(text2) ? 1 : 0);
					if (*(sbyte*)((byte*)ptr + 8) != 0)
					{
						return text2;
					}
					*(int*)((byte*)ptr + 4) = *(int*)((byte*)ptr + 4) + 1;
				}
				return text;
			}

			public static void smethod_14()
			{
				string text = Directory.GetFiles(Class120.PoEDirectory, DebugPanel.DebugHelpers.getString_0(107222088), SearchOption.AllDirectories).Select(new Func<string, string>(DebugPanel.DebugHelpers.<>c.<>9.method_6)).First(new Func<string, bool>(DebugPanel.DebugHelpers.<>c.<>9.method_7));
				string contents = string.Format(DebugPanel.DebugHelpers.getString_0(107222079), new object[]
				{
					Class255.class105_0.method_3(ConfigOptions.RunAsUser),
					Class120.PoEDirectory.Replace(DebugPanel.DebugHelpers.getString_0(107390332), DebugPanel.DebugHelpers.getString_0(107370575)),
					text,
					Path.GetPathRoot(new FileInfo(Class120.PoEDirectory).FullName).Trim(new char[]
					{
						'\\'
					})
				});
				File.WriteAllText(DebugPanel.DebugHelpers.getString_0(107221921), contents);
			}

			public static void smethod_15()
			{
				Dictionary<string, object> source = JsonConvert.DeserializeObject<Dictionary<string, object>>(Class238.FragTab);
				List<JsonItem> source2 = ((JArray)source.ElementAt(2).Value).ToObject<List<JsonItem>>();
				string text = DebugPanel.DebugHelpers.getString_0(107395303);
				foreach (JsonItem jsonItem in source2.OrderBy(new Func<JsonItem, int>(DebugPanel.DebugHelpers.<>c.<>9.method_8)))
				{
					text += string.Format(DebugPanel.DebugHelpers.getString_0(107221936), jsonItem.typeLine, jsonItem.x, Environment.NewLine);
				}
				File.WriteAllText(DebugPanel.DebugHelpers.getString_0(107221911), text);
				Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107348535));
			}

			public static void smethod_16()
			{
				Dictionary<string, object> source = JsonConvert.DeserializeObject<Dictionary<string, object>>(Class238.CurrencyTab);
				List<JsonItem> source2 = ((JArray)source.ElementAt(2).Value).ToObject<List<JsonItem>>();
				string text = DebugPanel.DebugHelpers.getString_0(107395303);
				foreach (JsonItem jsonItem in source2.OrderBy(new Func<JsonItem, int>(DebugPanel.DebugHelpers.<>c.<>9.method_9)))
				{
					text += string.Format(DebugPanel.DebugHelpers.getString_0(107221936), jsonItem.typeLine, jsonItem.x, Environment.NewLine);
				}
				File.WriteAllText(DebugPanel.DebugHelpers.getString_0(107221882), text);
				Class181.smethod_3(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107348535));
			}

			public unsafe static void smethod_17()
			{
				void* ptr = stackalloc byte[4];
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						DebugPanel.DebugHelpers.getString_0(107221337),
						DebugPanel.DebugHelpers.getString_0(107221332)
					},
					{
						DebugPanel.DebugHelpers.getString_0(107221358),
						DebugPanel.DebugHelpers.getString_0(107220809)
					},
					{
						DebugPanel.DebugHelpers.getString_0(107220835),
						DebugPanel.DebugHelpers.getString_0(107220830)
					},
					{
						DebugPanel.DebugHelpers.getString_0(107220436),
						DebugPanel.DebugHelpers.getString_0(107220391)
					},
					{
						DebugPanel.DebugHelpers.getString_0(107219993),
						DebugPanel.DebugHelpers.getString_0(107219936)
					},
					{
						DebugPanel.DebugHelpers.getString_0(107219534),
						DebugPanel.DebugHelpers.getString_0(107219489)
					},
					{
						DebugPanel.DebugHelpers.getString_0(107218739),
						DebugPanel.DebugHelpers.getString_0(107218690)
					}
				};
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					Class271 @class = Web.smethod_13(keyValuePair.Value);
					Thread.Sleep(2750);
					*(byte*)ptr = ((@class == null) ? 1 : 0);
					if (*(sbyte*)ptr != 0)
					{
						Class181.smethod_2(Enum11.const_2, DebugPanel.DebugHelpers.getString_0(107218932), new object[]
						{
							keyValuePair.Key
						});
					}
					else
					{
						((byte*)ptr)[1] = ((!@class.result.Any<string>()) ? 1 : 0);
						if (*(sbyte*)((byte*)ptr + 1) != 0)
						{
							Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218875), new object[]
							{
								keyValuePair.Key
							});
						}
						else
						{
							Stream stream = Web.smethod_9(@class.result.Take(10), DebugPanel.DebugHelpers.getString_0(107218854), false);
							Thread.Sleep(2000);
							((byte*)ptr)[2] = ((stream == null) ? 1 : 0);
							if (*(sbyte*)((byte*)ptr + 2) != 0)
							{
								Class181.smethod_2(Enum11.const_2, DebugPanel.DebugHelpers.getString_0(107218873), new object[]
								{
									keyValuePair.Key
								});
							}
							else
							{
								using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
								{
									Class276 class2 = JsonConvert.DeserializeObject<Class276>(streamReader.ReadToEnd());
									foreach (FetchTradeResult fetchTradeResult_ in class2.result)
									{
										BuyingTradeData buyingTradeData = Class151.smethod_0(fetchTradeResult_, keyValuePair.Key, TradeTypes.ItemBuying, 0);
										((byte*)ptr)[3] = ((buyingTradeData == null) ? 1 : 0);
										if (*(sbyte*)((byte*)ptr + 3) == 0)
										{
											Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218816), new object[]
											{
												keyValuePair.Key,
												buyingTradeData.Item.properties[2].values[0][0].ToString(),
												buyingTradeData.Item.properties[2].values[1][0].ToString(),
												buyingTradeData.PricePerItem,
												buyingTradeData.OurItemType,
												buyingTradeData.WhisperMessage
											});
										}
									}
								}
							}
						}
					}
				}
			}

			public static void smethod_18()
			{
				Order order_ = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107381745),
					my_item_amount = 4m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107381745),
					player_item_amount = 1m
				};
				SellOrderProcessor.GClass0 gclass = SellOrderProcessor.smethod_3(order_);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218803), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_2 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107381745),
					my_item_amount = 4m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107404757),
					player_item_amount = 1m
				};
				gclass = SellOrderProcessor.smethod_3(order_2);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218230), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_3 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107381745),
					my_item_amount = 4m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107359304),
					player_item_amount = 1m
				};
				gclass = SellOrderProcessor.smethod_3(order_3);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218141), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_4 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107218088),
					my_item_amount = 1m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107389950),
					player_item_amount = 5.5m
				};
				gclass = SellOrderProcessor.smethod_3(order_4);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218075), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_5 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107218088),
					my_item_amount = 1m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107389950),
					player_item_amount = 5.5m,
					left_pos = 2,
					top_pos = 1
				};
				gclass = SellOrderProcessor.smethod_3(order_5);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218506), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_6 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107381745),
					my_item_amount = 3m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107389950),
					player_item_amount = 210m
				};
				gclass = SellOrderProcessor.smethod_3(order_6);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218429), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_7 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107389950),
					my_item_amount = 1m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107381783),
					player_item_amount = 40m
				};
				gclass = SellOrderProcessor.smethod_3(order_7);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218368), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
				Order order_8 = new Order
				{
					my_item_name = DebugPanel.DebugHelpers.getString_0(107389950),
					my_item_amount = 40m,
					player_item_name = DebugPanel.DebugHelpers.getString_0(107381783),
					player_item_amount = 1m
				};
				gclass = SellOrderProcessor.smethod_3(order_8);
				Class181.smethod_2(Enum11.const_0, DebugPanel.DebugHelpers.getString_0(107218307), new object[]
				{
					gclass.bool_0,
					gclass.string_0
				});
			}

			static DebugHelpers()
			{
				Strings.CreateGetStringDelegate(typeof(DebugPanel.DebugHelpers));
			}

			[NonSerialized]
			internal static GetString getString_0;
		}
	}
}
