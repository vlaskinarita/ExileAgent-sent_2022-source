using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	internal sealed partial class GlassPanelForm : Form
	{
		public GlassPanelForm()
		{
			base.Name = GlassPanelForm.getString_0(107314530);
			this.Text = GlassPanelForm.getString_0(107314530);
			base.ClientSize = new Size(0, 0);
			base.ControlBox = false;
			base.FormBorderStyle = FormBorderStyle.None;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.Manual;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.FormBorderStyle = FormBorderStyle.None;
			base.SetStyle(ControlStyles.Selectable, false);
			base.Opacity = 0.5;
			this.BackColor = Color.FromArgb(255, 254, 254, 254);
			base.TransparencyKey = this.BackColor;
			this.HideGlass();
			NativeMethods.ShowWithoutActivate(this);
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 32;
				createParams.ExStyle |= 128;
				return createParams;
			}
		}

		public void Bind(ObjectListView olv, IOverlay overlay)
		{
			if (this.objectListView != null)
			{
				this.Unbind();
			}
			this.objectListView = olv;
			this.Overlay = overlay;
			this.mdiClient = null;
			this.mdiOwner = null;
			if (this.objectListView == null)
			{
				return;
			}
			this.objectListView.Disposed += this.objectListView_Disposed;
			this.objectListView.LocationChanged += this.objectListView_LocationChanged;
			this.objectListView.SizeChanged += this.objectListView_SizeChanged;
			this.objectListView.VisibleChanged += this.objectListView_VisibleChanged;
			this.objectListView.ParentChanged += this.objectListView_ParentChanged;
			if (this.ancestors == null)
			{
				this.ancestors = new List<Control>();
			}
			for (Control parent = this.objectListView.Parent; parent != null; parent = parent.Parent)
			{
				this.ancestors.Add(parent);
			}
			foreach (Control control in this.ancestors)
			{
				control.ParentChanged += this.objectListView_ParentChanged;
				TabControl tabControl = control as TabControl;
				if (tabControl != null)
				{
					tabControl.Selected += this.tabControl_Selected;
				}
			}
			base.Owner = this.objectListView.FindForm();
			this.myOwner = base.Owner;
			if (base.Owner != null)
			{
				base.Owner.LocationChanged += this.Owner_LocationChanged;
				base.Owner.SizeChanged += this.Owner_SizeChanged;
				base.Owner.ResizeBegin += this.Owner_ResizeBegin;
				base.Owner.ResizeEnd += this.Owner_ResizeEnd;
				if (base.Owner.TopMost)
				{
					NativeMethods.MakeTopMost(this);
				}
				this.mdiOwner = base.Owner.MdiParent;
				if (this.mdiOwner != null)
				{
					this.mdiOwner.LocationChanged += this.Owner_LocationChanged;
					this.mdiOwner.SizeChanged += this.Owner_SizeChanged;
					this.mdiOwner.ResizeBegin += this.Owner_ResizeBegin;
					this.mdiOwner.ResizeEnd += this.Owner_ResizeEnd;
					foreach (object obj in this.mdiOwner.Controls)
					{
						Control control2 = (Control)obj;
						this.mdiClient = (control2 as MdiClient);
						if (this.mdiClient != null)
						{
							break;
						}
					}
					if (this.mdiClient != null)
					{
						this.mdiClient.ClientSizeChanged += this.myMdiClient_ClientSizeChanged;
					}
				}
			}
			this.UpdateTransparency();
		}

		private void myMdiClient_ClientSizeChanged(object sender, EventArgs e)
		{
			this.RecalculateBounds();
			base.Invalidate();
		}

		public void HideGlass()
		{
			if (!this.isGlassShown)
			{
				return;
			}
			this.isGlassShown = false;
			base.Bounds = new Rectangle(-10000, -10000, 1, 1);
		}

		public void ShowGlass()
		{
			if (!this.isGlassShown && !this.isDuringResizeSequence)
			{
				this.isGlassShown = true;
				this.RecalculateBounds();
				return;
			}
		}

		public void Unbind()
		{
			if (this.objectListView != null)
			{
				this.objectListView.Disposed -= this.objectListView_Disposed;
				this.objectListView.LocationChanged -= this.objectListView_LocationChanged;
				this.objectListView.SizeChanged -= this.objectListView_SizeChanged;
				this.objectListView.VisibleChanged -= this.objectListView_VisibleChanged;
				this.objectListView.ParentChanged -= this.objectListView_ParentChanged;
				this.objectListView = null;
			}
			if (this.ancestors != null)
			{
				foreach (Control control in this.ancestors)
				{
					control.ParentChanged -= this.objectListView_ParentChanged;
					TabControl tabControl = control as TabControl;
					if (tabControl != null)
					{
						tabControl.Selected -= this.tabControl_Selected;
					}
				}
				this.ancestors = null;
			}
			if (this.myOwner != null)
			{
				this.myOwner.LocationChanged -= this.Owner_LocationChanged;
				this.myOwner.SizeChanged -= this.Owner_SizeChanged;
				this.myOwner.ResizeBegin -= this.Owner_ResizeBegin;
				this.myOwner.ResizeEnd -= this.Owner_ResizeEnd;
				this.myOwner = null;
			}
			if (this.mdiOwner != null)
			{
				this.mdiOwner.LocationChanged -= this.Owner_LocationChanged;
				this.mdiOwner.SizeChanged -= this.Owner_SizeChanged;
				this.mdiOwner.ResizeBegin -= this.Owner_ResizeBegin;
				this.mdiOwner.ResizeEnd -= this.Owner_ResizeEnd;
				this.mdiOwner = null;
			}
			if (this.mdiClient != null)
			{
				this.mdiClient.ClientSizeChanged -= this.myMdiClient_ClientSizeChanged;
				this.mdiClient = null;
			}
		}

		private void objectListView_Disposed(object sender, EventArgs e)
		{
			this.Unbind();
		}

		private void Owner_ResizeBegin(object sender, EventArgs e)
		{
			this.isDuringResizeSequence = true;
			this.wasGlassShownBeforeResize = this.isGlassShown;
		}

		private void Owner_ResizeEnd(object sender, EventArgs e)
		{
			this.isDuringResizeSequence = false;
			if (this.wasGlassShownBeforeResize)
			{
				this.ShowGlass();
			}
		}

		private void Owner_LocationChanged(object sender, EventArgs e)
		{
			if (this.mdiOwner != null)
			{
				this.HideGlass();
				return;
			}
			this.RecalculateBounds();
		}

		private void Owner_SizeChanged(object sender, EventArgs e)
		{
			this.HideGlass();
		}

		private void objectListView_LocationChanged(object sender, EventArgs e)
		{
			if (this.isGlassShown)
			{
				this.RecalculateBounds();
			}
		}

		private void objectListView_SizeChanged(object sender, EventArgs e)
		{
		}

		private void tabControl_Selected(object sender, TabControlEventArgs e)
		{
			this.HideGlass();
		}

		private void objectListView_ParentChanged(object sender, EventArgs e)
		{
			ObjectListView olv = this.objectListView;
			IOverlay overlay = this.Overlay;
			this.Unbind();
			this.Bind(olv, overlay);
		}

		private void objectListView_VisibleChanged(object sender, EventArgs e)
		{
			if (this.objectListView.Visible)
			{
				this.ShowGlass();
				return;
			}
			this.HideGlass();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.objectListView != null && this.Overlay != null)
			{
				Graphics graphics = e.Graphics;
				graphics.TextRenderingHint = ObjectListView.TextRenderingHint;
				graphics.SmoothingMode = ObjectListView.SmoothingMode;
				if (this.mdiClient != null)
				{
					Rectangle r = this.mdiClient.RectangleToScreen(this.mdiClient.ClientRectangle);
					Rectangle rect = this.objectListView.RectangleToClient(r);
					graphics.SetClip(rect, CombineMode.Intersect);
				}
				this.Overlay.Draw(this.objectListView, graphics, this.objectListView.ClientRectangle);
				return;
			}
		}

		protected void RecalculateBounds()
		{
			if (!this.isGlassShown)
			{
				return;
			}
			Rectangle clientRectangle = this.objectListView.ClientRectangle;
			clientRectangle.X = 0;
			clientRectangle.Y = 0;
			base.Bounds = this.objectListView.RectangleToScreen(clientRectangle);
		}

		internal void UpdateTransparency()
		{
			ITransparentOverlay transparentOverlay = this.Overlay as ITransparentOverlay;
			if (transparentOverlay == null)
			{
				base.Opacity = (double)((float)this.objectListView.OverlayTransparency / 255f);
				return;
			}
			base.Opacity = (double)((float)transparentOverlay.Transparency / 255f);
		}

		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 132)
			{
				m.Result = (IntPtr)(-1);
			}
			base.WndProc(ref m);
		}

		static GlassPanelForm()
		{
			Strings.CreateGetStringDelegate(typeof(GlassPanelForm));
		}

		internal IOverlay Overlay;

		private ObjectListView objectListView;

		private bool isDuringResizeSequence;

		private bool isGlassShown;

		private bool wasGlassShownBeforeResize;

		private Form myOwner;

		private Form mdiOwner;

		private List<Control> ancestors;

		private MdiClient mdiClient;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
