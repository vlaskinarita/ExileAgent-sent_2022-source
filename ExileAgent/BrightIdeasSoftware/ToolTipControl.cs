using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class ToolTipControl : NativeWindow
	{
		internal int WindowStyle
		{
			get
			{
				return NativeMethods.GetWindowLong(base.Handle, -16);
			}
			set
			{
				NativeMethods.SetWindowLong(base.Handle, -16, value);
			}
		}

		public bool IsBalloon
		{
			get
			{
				return (this.WindowStyle & 64) == 64;
			}
			set
			{
				if (this.IsBalloon == value)
				{
					return;
				}
				int num = this.WindowStyle;
				if (value)
				{
					num |= 320;
					if (!ObjectListView.IsVistaOrLater)
					{
						num &= -8388609;
					}
				}
				else
				{
					num &= -321;
					if (!ObjectListView.IsVistaOrLater)
					{
						if (this.hasBorder)
						{
							num |= 8388608;
						}
						else
						{
							num &= -8388609;
						}
					}
				}
				this.WindowStyle = num;
			}
		}

		public bool HasBorder
		{
			get
			{
				return this.hasBorder;
			}
			set
			{
				if (this.hasBorder == value)
				{
					return;
				}
				if (value)
				{
					this.WindowStyle |= 8388608;
					return;
				}
				this.WindowStyle &= -8388609;
			}
		}

		public Color BackColor
		{
			get
			{
				int win32Color = (int)NativeMethods.SendMessage(base.Handle, 1046, 0, 0);
				return ColorTranslator.FromWin32(win32Color);
			}
			set
			{
				if (!ObjectListView.IsVistaOrLater)
				{
					int wParam = ColorTranslator.ToWin32(value);
					NativeMethods.SendMessage(base.Handle, 1043, wParam, 0);
				}
			}
		}

		public Color ForeColor
		{
			get
			{
				int win32Color = (int)NativeMethods.SendMessage(base.Handle, 1047, 0, 0);
				return ColorTranslator.FromWin32(win32Color);
			}
			set
			{
				if (!ObjectListView.IsVistaOrLater)
				{
					int wParam = ColorTranslator.ToWin32(value);
					NativeMethods.SendMessage(base.Handle, 1044, wParam, 0);
				}
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.title = string.Empty;
				}
				else if (value.Length >= 100)
				{
					this.title = value.Substring(0, 99);
				}
				else
				{
					this.title = value;
				}
				NativeMethods.SendMessage_7(base.Handle, 1057, (int)this.standardIcon, this.title);
			}
		}

		public ToolTipControl.StandardIcons StandardIcon
		{
			get
			{
				return this.standardIcon;
			}
			set
			{
				this.standardIcon = value;
				NativeMethods.SendMessage_7(base.Handle, 1057, (int)this.standardIcon, this.title);
			}
		}

		public Font Font
		{
			get
			{
				IntPtr intPtr = NativeMethods.SendMessage(base.Handle, 49, 0, 0);
				if (intPtr == IntPtr.Zero)
				{
					return Control.DefaultFont;
				}
				return Font.FromHfont(intPtr);
			}
			set
			{
				Font font = value ?? Control.DefaultFont;
				if (font == this.font)
				{
					return;
				}
				this.font = font;
				IntPtr wParam = this.font.ToHfont();
				NativeMethods.SendMessage(base.Handle, 48, wParam, 0);
			}
		}

		public int AutoPopDelay
		{
			get
			{
				return this.GetDelayTime(2);
			}
			set
			{
				this.SetDelayTime(2, value);
			}
		}

		public int InitialDelay
		{
			get
			{
				return this.GetDelayTime(3);
			}
			set
			{
				this.SetDelayTime(3, value);
			}
		}

		public int ReshowDelay
		{
			get
			{
				return this.GetDelayTime(1);
			}
			set
			{
				this.SetDelayTime(1, value);
			}
		}

		private int GetDelayTime(int which)
		{
			return (int)NativeMethods.SendMessage(base.Handle, 1045, which, 0);
		}

		private void SetDelayTime(int which, int value)
		{
			NativeMethods.SendMessage(base.Handle, 1027, which, value);
		}

		public void Create(IntPtr parentHandle)
		{
			if (base.Handle != IntPtr.Zero)
			{
				return;
			}
			this.CreateHandle(new CreateParams
			{
				ClassName = ToolTipControl.getString_0(107313498),
				Style = 2,
				ExStyle = 8,
				Parent = parentHandle
			});
			this.SetMaxWidth();
		}

		public void PushSettings()
		{
			if (this.settings != null)
			{
				return;
			}
			this.settings = new Hashtable();
			this.settings[ToolTipControl.getString_0(107313505)] = this.IsBalloon;
			this.settings[ToolTipControl.getString_0(107313460)] = this.HasBorder;
			this.settings[ToolTipControl.getString_0(107313479)] = this.BackColor;
			this.settings[ToolTipControl.getString_0(107313434)] = this.ForeColor;
			this.settings[ToolTipControl.getString_0(107313421)] = this.Title;
			this.settings[ToolTipControl.getString_0(107313444)] = this.StandardIcon;
			this.settings[ToolTipControl.getString_0(107312883)] = this.AutoPopDelay;
			this.settings[ToolTipControl.getString_0(107312898)] = this.InitialDelay;
			this.settings[ToolTipControl.getString_0(107312849)] = this.ReshowDelay;
			this.settings[ToolTipControl.getString_0(107312864)] = this.Font;
		}

		public void PopSettings()
		{
			if (this.settings == null)
			{
				return;
			}
			this.IsBalloon = (bool)this.settings[ToolTipControl.getString_0(107313505)];
			this.HasBorder = (bool)this.settings[ToolTipControl.getString_0(107313460)];
			this.BackColor = (Color)this.settings[ToolTipControl.getString_0(107313479)];
			this.ForeColor = (Color)this.settings[ToolTipControl.getString_0(107313434)];
			this.Title = (string)this.settings[ToolTipControl.getString_0(107313421)];
			this.StandardIcon = (ToolTipControl.StandardIcons)this.settings[ToolTipControl.getString_0(107313444)];
			this.AutoPopDelay = (int)this.settings[ToolTipControl.getString_0(107312883)];
			this.InitialDelay = (int)this.settings[ToolTipControl.getString_0(107312898)];
			this.ReshowDelay = (int)this.settings[ToolTipControl.getString_0(107312849)];
			this.Font = (Font)this.settings[ToolTipControl.getString_0(107312864)];
			this.settings = null;
		}

		public void AddTool(IWin32Window window)
		{
			NativeMethods.TOOLINFO lParam = this.MakeToolInfoStruct(window);
			NativeMethods.SendMessage_5(base.Handle, 1074, 0, lParam);
		}

		public void PopToolTip(IWin32Window window)
		{
			NativeMethods.SendMessage(base.Handle, 1052, 0, 0);
		}

		public void RemoveToolTip(IWin32Window window)
		{
			NativeMethods.TOOLINFO lParam = this.MakeToolInfoStruct(window);
			NativeMethods.SendMessage_5(base.Handle, 1075, 0, lParam);
		}

		public void SetMaxWidth()
		{
			this.SetMaxWidth(SystemInformation.MaxWindowTrackSize.Width);
		}

		public void SetMaxWidth(int maxWidth)
		{
			NativeMethods.SendMessage(base.Handle, 1048, 0, maxWidth);
		}

		private NativeMethods.TOOLINFO MakeToolInfoStruct(IWin32Window window)
		{
			return new NativeMethods.TOOLINFO
			{
				hwnd = window.Handle,
				uFlags = 17,
				uId = window.Handle,
				lpszText = (IntPtr)(-1)
			};
		}

		protected bool HandleNotify(ref Message msg)
		{
			return false;
		}

		public bool HandleGetDispInfo(ref Message msg)
		{
			this.SetMaxWidth();
			ToolTipShowingEventArgs toolTipShowingEventArgs = new ToolTipShowingEventArgs();
			toolTipShowingEventArgs.ToolTipControl = this;
			this.OnShowing(toolTipShowingEventArgs);
			if (string.IsNullOrEmpty(toolTipShowingEventArgs.Text))
			{
				return false;
			}
			this.ApplyEventFormatting(toolTipShowingEventArgs);
			NativeMethods.NMTTDISPINFO structure = (NativeMethods.NMTTDISPINFO)msg.GetLParam(typeof(NativeMethods.NMTTDISPINFO));
			structure.lpszText = toolTipShowingEventArgs.Text;
			structure.hinst = IntPtr.Zero;
			if (toolTipShowingEventArgs.RightToLeft == RightToLeft.Yes)
			{
				structure.uFlags |= 4;
			}
			Marshal.StructureToPtr<NativeMethods.NMTTDISPINFO>(structure, msg.LParam, false);
			return true;
		}

		private void ApplyEventFormatting(ToolTipShowingEventArgs args)
		{
			if (args.IsBalloon == null && args.BackColor == null && args.ForeColor == null && args.Title == null && args.StandardIcon == null && args.AutoPopDelay == null && args.Font == null)
			{
				return;
			}
			this.PushSettings();
			if (args.IsBalloon != null)
			{
				this.IsBalloon = args.IsBalloon.Value;
			}
			if (args.BackColor != null)
			{
				this.BackColor = args.BackColor.Value;
			}
			if (args.ForeColor != null)
			{
				this.ForeColor = args.ForeColor.Value;
			}
			if (args.StandardIcon != null)
			{
				this.StandardIcon = args.StandardIcon.Value;
			}
			if (args.AutoPopDelay != null)
			{
				this.AutoPopDelay = args.AutoPopDelay.Value;
			}
			if (args.Font != null)
			{
				this.Font = args.Font;
			}
			if (args.Title != null)
			{
				this.Title = args.Title;
			}
		}

		public bool HandleLinkClick(ref Message msg)
		{
			return false;
		}

		public bool HandlePop(ref Message msg)
		{
			this.PopSettings();
			return true;
		}

		public bool HandleShow(ref Message msg)
		{
			return false;
		}

		protected bool HandleReflectNotify(ref Message msg)
		{
			int code = ((NativeMethods.NMHEADER)msg.GetLParam(typeof(NativeMethods.NMHEADER))).nhdr.code;
			if (code != -530)
			{
				switch (code)
				{
				case -523:
					if (this.HandleLinkClick(ref msg))
					{
						return true;
					}
					break;
				case -522:
					if (this.HandlePop(ref msg))
					{
						return true;
					}
					break;
				case -521:
					if (this.HandleShow(ref msg))
					{
						return true;
					}
					break;
				}
			}
			else if (this.HandleGetDispInfo(ref msg))
			{
				return true;
			}
			return false;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 != 78)
			{
				if (msg2 == 8270)
				{
					if (!this.HandleReflectNotify(ref msg))
					{
						return;
					}
				}
			}
			else if (!this.HandleNotify(ref msg))
			{
				return;
			}
			base.WndProc(ref msg);
		}

		public event EventHandler<ToolTipShowingEventArgs> Showing;

		public event EventHandler<EventArgs> Pop;

		protected void OnShowing(ToolTipShowingEventArgs e)
		{
			if (this.Showing != null)
			{
				this.Showing(this, e);
			}
		}

		protected void OnPop(EventArgs e)
		{
			if (this.Pop != null)
			{
				this.Pop(this, e);
			}
		}

		static ToolTipControl()
		{
			Strings.CreateGetStringDelegate(typeof(ToolTipControl));
		}

		private const int GWL_STYLE = -16;

		private const int WM_GETFONT = 49;

		private const int WM_SETFONT = 48;

		private const int WS_BORDER = 8388608;

		private const int WS_EX_TOPMOST = 8;

		private const int TTM_ADDTOOL = 1074;

		private const int TTM_ADJUSTRECT = 1055;

		private const int TTM_DELTOOL = 1075;

		private const int TTM_GETBUBBLESIZE = 1054;

		private const int TTM_GETCURRENTTOOL = 1083;

		private const int TTM_GETTIPBKCOLOR = 1046;

		private const int TTM_GETTIPTEXTCOLOR = 1047;

		private const int TTM_GETDELAYTIME = 1045;

		private const int TTM_NEWTOOLRECT = 1076;

		private const int TTM_POP = 1052;

		private const int TTM_SETDELAYTIME = 1027;

		private const int TTM_SETMAXTIPWIDTH = 1048;

		private const int TTM_SETTIPBKCOLOR = 1043;

		private const int TTM_SETTIPTEXTCOLOR = 1044;

		private const int TTM_SETTITLE = 1057;

		private const int TTM_SETTOOLINFO = 1078;

		private const int TTF_IDISHWND = 1;

		private const int TTF_CENTERTIP = 2;

		private const int TTF_RTLREADING = 4;

		private const int TTF_SUBCLASS = 16;

		private const int TTF_PARSELINKS = 4096;

		private const int TTS_NOPREFIX = 2;

		private const int TTS_BALLOON = 64;

		private const int TTS_USEVISUALSTYLE = 256;

		private const int TTN_FIRST = -520;

		public const int TTN_SHOW = -521;

		public const int TTN_POP = -522;

		public const int TTN_LINKCLICK = -523;

		public const int TTN_GETDISPINFO = -530;

		private const int TTDT_AUTOMATIC = 0;

		private const int TTDT_RESHOW = 1;

		private const int TTDT_AUTOPOP = 2;

		private const int TTDT_INITIAL = 3;

		private bool hasBorder = true;

		private string title;

		private ToolTipControl.StandardIcons standardIcon;

		private Font font;

		private Hashtable settings;

		[NonSerialized]
		internal static GetString getString_0;

		public enum StandardIcons
		{
			None,
			Info,
			Warning,
			Error,
			InfoLarge,
			WarningLarge,
			ErrorLarge
		}
	}
}
