using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	public sealed class ToolStripCheckedListBox : ToolStripControlHost
	{
		public ToolStripCheckedListBox() : base(new CheckedListBox())
		{
			this.CheckedListBoxControl.MaximumSize = new Size(400, 700);
			this.CheckedListBoxControl.ThreeDCheckBoxes = true;
			this.CheckedListBoxControl.CheckOnClick = true;
			this.CheckedListBoxControl.SelectionMode = SelectionMode.One;
		}

		public CheckedListBox CheckedListBoxControl
		{
			get
			{
				return base.Control as CheckedListBox;
			}
		}

		public CheckedListBox.ObjectCollection Items
		{
			get
			{
				return this.CheckedListBoxControl.Items;
			}
		}

		public bool CheckedOnClick
		{
			get
			{
				return this.CheckedListBoxControl.CheckOnClick;
			}
			set
			{
				this.CheckedListBoxControl.CheckOnClick = value;
			}
		}

		public CheckedListBox.CheckedItemCollection CheckedItems
		{
			get
			{
				return this.CheckedListBoxControl.CheckedItems;
			}
		}

		public void AddItem(object item, bool isChecked)
		{
			this.Items.Add(item);
			if (isChecked)
			{
				this.CheckedListBoxControl.SetItemChecked(this.Items.Count - 1, true);
			}
		}

		public void AddItem(object item, CheckState state)
		{
			this.Items.Add(item);
			this.CheckedListBoxControl.SetItemCheckState(this.Items.Count - 1, state);
		}

		public CheckState GetItemCheckState(int i)
		{
			return this.CheckedListBoxControl.GetItemCheckState(i);
		}

		public void SetItemState(int i, CheckState checkState)
		{
			if (i >= 0 && i < this.Items.Count)
			{
				this.CheckedListBoxControl.SetItemCheckState(i, checkState);
			}
		}

		public void CheckAll()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.CheckedListBoxControl.SetItemChecked(i, true);
			}
		}

		public void UncheckAll()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.CheckedListBoxControl.SetItemChecked(i, false);
			}
		}

		protected override void OnSubscribeControlEvents(Control c)
		{
			base.OnSubscribeControlEvents(c);
			CheckedListBox checkedListBox = (CheckedListBox)c;
			checkedListBox.ItemCheck += this.OnItemCheck;
		}

		protected override void OnUnsubscribeControlEvents(Control c)
		{
			base.OnUnsubscribeControlEvents(c);
			CheckedListBox checkedListBox = (CheckedListBox)c;
			checkedListBox.ItemCheck -= this.OnItemCheck;
		}

		public event ItemCheckEventHandler ItemCheck;

		private void OnItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (this.ItemCheck != null)
			{
				this.ItemCheck(this, e);
			}
		}
	}
}
