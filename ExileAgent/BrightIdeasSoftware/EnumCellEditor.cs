using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	[ToolboxItem(false)]
	public sealed class EnumCellEditor : ComboBox
	{
		public EnumCellEditor(Type type)
		{
			base.DropDownStyle = ComboBoxStyle.DropDownList;
			base.ValueMember = EnumCellEditor.getString_0(107323099);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in Enum.GetValues(type))
			{
				arrayList.Add(new ComboBoxItem(obj, Enum.GetName(type, obj)));
			}
			base.DataSource = arrayList;
		}

		static EnumCellEditor()
		{
			Strings.CreateGetStringDelegate(typeof(EnumCellEditor));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
