using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	public sealed class IntUpDown : NumericUpDown
	{
		public IntUpDown()
		{
			base.DecimalPlaces = 0;
			base.Minimum = -9999999m;
			base.Maximum = 9999999m;
		}

		public new int Value
		{
			get
			{
				return decimal.ToInt32(base.Value);
			}
			set
			{
				base.Value = new decimal(value);
			}
		}
	}
}
