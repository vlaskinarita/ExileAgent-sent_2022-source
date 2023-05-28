using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[Browsable(false)]
	public sealed class OLVListSubItem : ListViewItem.ListViewSubItem
	{
		public OLVListSubItem()
		{
		}

		public OLVListSubItem(object modelValue, string text, object image)
		{
			this.ModelValue = modelValue;
			base.Text = text;
			this.ImageSelector = image;
		}

		public Rectangle? CellPadding
		{
			get
			{
				return this.cellPadding;
			}
			set
			{
				this.cellPadding = value;
			}
		}

		public StringAlignment? CellVerticalAlignment
		{
			get
			{
				return this.cellVerticalAlignment;
			}
			set
			{
				this.cellVerticalAlignment = value;
			}
		}

		public object ModelValue
		{
			get
			{
				return this.modelValue;
			}
			private set
			{
				this.modelValue = value;
			}
		}

		public bool HasDecoration
		{
			get
			{
				return this.decorations != null && this.decorations.Count > 0;
			}
		}

		public IDecoration Decoration
		{
			get
			{
				if (!this.HasDecoration)
				{
					return null;
				}
				return this.Decorations[0];
			}
			set
			{
				this.Decorations.Clear();
				if (value != null)
				{
					this.Decorations.Add(value);
				}
			}
		}

		public IList<IDecoration> Decorations
		{
			get
			{
				if (this.decorations == null)
				{
					this.decorations = new List<IDecoration>();
				}
				return this.decorations;
			}
		}

		public object ImageSelector
		{
			get
			{
				return this.imageSelector;
			}
			set
			{
				this.imageSelector = value;
			}
		}

		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		private Rectangle? cellPadding;

		private StringAlignment? cellVerticalAlignment;

		private object modelValue;

		private IList<IDecoration> decorations;

		private object imageSelector;

		private string url;

		internal ImageRenderer.AnimationState AnimationState;
	}
}
