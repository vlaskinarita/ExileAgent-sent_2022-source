using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	public sealed class FloatCellEditor : NumericUpDown
	{
		public FloatCellEditor()
		{
			base.DecimalPlaces = 2;
			base.Minimum = -9999999m;
			base.Maximum = 9999999m;
		}

		public new double Value
		{
			get
			{
				return Convert.ToDouble(base.Value);
			}
			set
			{
				base.Value = Convert.ToDecimal(value);
			}
		}
	}
}
