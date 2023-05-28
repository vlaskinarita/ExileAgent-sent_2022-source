using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	internal sealed class UintUpDown : NumericUpDown
	{
		public UintUpDown()
		{
			base.DecimalPlaces = 0;
			base.Minimum = 0m;
			base.Maximum = 9999999m;
		}

		public new uint Value
		{
			get
			{
				return decimal.ToUInt32(base.Value);
			}
			set
			{
				base.Value = new decimal(value);
			}
		}
	}
}
