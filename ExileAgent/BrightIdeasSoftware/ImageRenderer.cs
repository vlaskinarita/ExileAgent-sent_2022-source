using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public class ImageRenderer : BaseRenderer
	{
		public ImageRenderer()
		{
			this.stopwatch = new Stopwatch();
		}

		public ImageRenderer(bool startAnimations) : this()
		{
			this.Paused = !startAnimations;
		}

		protected override void Dispose(bool disposing)
		{
			this.Paused = true;
			base.Dispose(disposing);
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Paused
		{
			get
			{
				return this.isPaused;
			}
			set
			{
				if (this.isPaused == value)
				{
					return;
				}
				this.isPaused = value;
				if (this.isPaused)
				{
					this.StopTickler();
					this.stopwatch.Stop();
					return;
				}
				this.Tickler.Change(1, -1);
				this.stopwatch.Start();
			}
		}

		private void StopTickler()
		{
			this.Tickler.Change(-1, -1);
			this.Tickler.Dispose();
			this.tickler = null;
		}

		protected Timer Tickler
		{
			get
			{
				if (this.tickler == null)
				{
					this.tickler = new Timer(new TimerCallback(this.OnTimer), null, -1, -1);
				}
				return this.tickler;
			}
		}

		public void Pause()
		{
			this.Paused = true;
		}

		public void Unpause()
		{
			this.Paused = false;
		}

		public override void Render(Graphics g, Rectangle r)
		{
			this.DrawBackground(g, r);
			if (base.Aspect != null)
			{
				if (base.Aspect != DBNull.Value)
				{
					r = this.ApplyCellPadding(r);
					if (base.Aspect is byte[])
					{
						this.DrawAlignedImage(g, r, this.GetImageFromAspect());
						return;
					}
					ICollection collection = base.Aspect as ICollection;
					if (collection == null)
					{
						this.DrawAlignedImage(g, r, this.GetImageFromAspect());
						return;
					}
					this.DrawImages(g, r, collection);
					return;
				}
			}
		}

		protected Image GetImageFromAspect()
		{
			if (base.OLVSubItem == null || !(base.OLVSubItem.ImageSelector is Image))
			{
				Image image = null;
				if (base.Aspect is byte[])
				{
					using (MemoryStream memoryStream = new MemoryStream((byte[])base.Aspect))
					{
						try
						{
							image = Image.FromStream(memoryStream);
						}
						catch (ArgumentException)
						{
						}
						goto IL_DA;
					}
				}
				if (base.Aspect is int)
				{
					image = this.GetImage(base.Aspect);
				}
				else
				{
					string text = base.Aspect as string;
					if (!string.IsNullOrEmpty(text))
					{
						try
						{
							image = Image.FromFile(text);
						}
						catch (FileNotFoundException)
						{
							image = this.GetImage(base.Aspect);
						}
						catch (OutOfMemoryException)
						{
							image = this.GetImage(base.Aspect);
						}
					}
				}
				IL_DA:
				if (base.OLVSubItem != null && ImageRenderer.AnimationState.IsAnimation(image))
				{
					base.OLVSubItem.AnimationState = new ImageRenderer.AnimationState(image);
				}
				if (base.OLVSubItem != null)
				{
					base.OLVSubItem.ImageSelector = image;
				}
				return image;
			}
			if (base.OLVSubItem.AnimationState == null)
			{
				return (Image)base.OLVSubItem.ImageSelector;
			}
			return base.OLVSubItem.AnimationState.image;
		}

		public void OnTimer(object state)
		{
			if (base.ListView == null || this.Paused)
			{
				return;
			}
			if (base.ListView.InvokeRequired)
			{
				base.ListView.Invoke(new MethodInvoker(delegate()
				{
					this.OnTimer(state);
				}));
				return;
			}
			this.OnTimerInThread();
		}

		protected void OnTimerInThread()
		{
			if (base.ListView == null || this.Paused || base.ListView.IsDisposed)
			{
				return;
			}
			if (base.ListView.View == View.Details && base.Column != null && base.Column.Index >= 0)
			{
				long elapsedMilliseconds = this.stopwatch.ElapsedMilliseconds;
				int index = base.Column.Index;
				long num = elapsedMilliseconds + 1000L;
				Rectangle rectangle = default(Rectangle);
				for (int i = 0; i < base.ListView.GetItemCount(); i++)
				{
					OLVListItem item = base.ListView.GetItem(i);
					OLVListSubItem subItem = item.GetSubItem(index);
					ImageRenderer.AnimationState animationState = subItem.AnimationState;
					if (animationState != null && animationState.IsValid)
					{
						if (elapsedMilliseconds >= animationState.currentFrameExpiresAt)
						{
							animationState.AdvanceFrame(elapsedMilliseconds);
							if (rectangle.IsEmpty)
							{
								rectangle = subItem.Bounds;
							}
							else
							{
								rectangle = Rectangle.Union(rectangle, subItem.Bounds);
							}
						}
						num = Math.Min(num, animationState.currentFrameExpiresAt);
					}
				}
				if (!rectangle.IsEmpty)
				{
					base.ListView.Invalidate(rectangle);
				}
				this.Tickler.Change(num - elapsedMilliseconds, -1L);
				return;
			}
			this.Tickler.Change(1000, -1);
		}

		private bool isPaused = true;

		private Timer tickler;

		private Stopwatch stopwatch;

		internal sealed class AnimationState
		{
			public static bool IsAnimation(Image image)
			{
				return image != null && new List<Guid>(image.FrameDimensionsList).Contains(FrameDimension.Time.Guid);
			}

			public AnimationState()
			{
				this.imageDuration = new List<int>();
			}

			public AnimationState(Image image) : this()
			{
				if (!ImageRenderer.AnimationState.IsAnimation(image))
				{
					return;
				}
				this.image = image;
				this.frameCount = this.image.GetFrameCount(FrameDimension.Time);
				foreach (PropertyItem propertyItem in this.image.PropertyItems)
				{
					if (propertyItem.Id == 20736)
					{
						for (int j = 0; j < propertyItem.Len; j += 4)
						{
							int num = ((int)propertyItem.Value[j + 3] << 24) + ((int)propertyItem.Value[j + 2] << 16) + ((int)propertyItem.Value[j + 1] << 8) + (int)propertyItem.Value[j];
							this.imageDuration.Add(num * 10);
						}
						return;
					}
				}
			}

			public bool IsValid
			{
				get
				{
					return this.image != null && this.frameCount > 0;
				}
			}

			public void AdvanceFrame(long millisecondsNow)
			{
				this.currentFrame = (this.currentFrame + 1) % this.frameCount;
				this.currentFrameExpiresAt = millisecondsNow + (long)this.imageDuration[this.currentFrame];
				this.image.SelectActiveFrame(FrameDimension.Time, this.currentFrame);
			}

			private const int PropertyTagTypeShort = 3;

			private const int PropertyTagTypeLong = 4;

			private const int PropertyTagFrameDelay = 20736;

			private const int PropertyTagLoopCount = 20737;

			internal int currentFrame;

			internal long currentFrameExpiresAt;

			internal Image image;

			internal List<int> imageDuration;

			internal int frameCount;
		}
	}
}
