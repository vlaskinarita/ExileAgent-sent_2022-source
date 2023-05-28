using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class ListItemViewModel
	{
		public string Text { get; set; }

		public ListItemViewModel()
		{
		}

		public ListItemViewModel(string text)
		{
			this.Text = text;
		}

		public static object ImageGetter(object obj)
		{
			ListItemViewModel listItemViewModel = (ListItemViewModel)obj;
			return listItemViewModel.Image;
		}

		static ListItemViewModel()
		{
			Strings.CreateGetStringDelegate(typeof(ListItemViewModel));
		}

		public object Image = ListItemViewModel.getString_0(107398969);

		[NonSerialized]
		internal static GetString getString_0;
	}
}
