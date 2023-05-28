using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	public sealed class BooleanCellEditor : ComboBox
	{
		public BooleanCellEditor()
		{
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			base.ValueMember = BooleanCellEditor.getString_0(107323100);
			base.DataSource = new ArrayList
			{
				new ComboBoxItem(false, BooleanCellEditor.getString_0(107314414)),
				new ComboBoxItem(true, BooleanCellEditor.getString_0(107314437))
			};
		}

		static BooleanCellEditor()
		{
			Strings.CreateGetStringDelegate(typeof(BooleanCellEditor));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
