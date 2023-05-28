using System;
using PoEv2.Models.Crafting;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.PublicModels
{
	public sealed class AffixItemViewModel
	{
		public Mod Affix { get; set; }

		public Tier Tier { get; set; }

		public AffixItemViewModel()
		{
		}

		public AffixItemViewModel(Mod affix, Tier tier)
		{
			this.Affix = affix;
			this.Tier = tier;
		}

		public static object ImageGetter(object obj)
		{
			AffixItemViewModel affixItemViewModel = (AffixItemViewModel)obj;
			return affixItemViewModel.Image;
		}

		static AffixItemViewModel()
		{
			Strings.CreateGetStringDelegate(typeof(AffixItemViewModel));
		}

		public object Image = AffixItemViewModel.getString_0(107398657);

		[NonSerialized]
		internal static GetString getString_0;
	}
}
