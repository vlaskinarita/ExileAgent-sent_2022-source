using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PoEv2.Models;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns8
{
	internal static class Class392
	{
		public static Position smethod_0(string string_0)
		{
			Position result;
			if (!Class392.dictionary_0.ContainsKey(string_0))
			{
				result = new Position();
			}
			else
			{
				result = new Position(Class392.dictionary_0[string_0], 0);
			}
			return result;
		}

		public static bool smethod_1(int int_0)
		{
			Class392.Class393 @class = new Class392.Class393();
			@class.int_0 = int_0;
			return !Class392.dictionary_0.Any(new Func<KeyValuePair<string, int>, bool>(@class.method_0));
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Class392()
		{
			Strings.CreateGetStringDelegate(typeof(Class392));
			Class392.dictionary_0 = new Dictionary<string, int>
			{
				{
					Class392.getString_0(107408956),
					0
				},
				{
					Class392.getString_0(107408282),
					1
				},
				{
					Class392.getString_0(107394116),
					2
				},
				{
					Class392.getString_0(107408981),
					3
				},
				{
					Class392.getString_0(107394173),
					4
				},
				{
					Class392.getString_0(107385795),
					5
				},
				{
					Class392.getString_0(107385244),
					6
				},
				{
					Class392.getString_0(107385202),
					7
				},
				{
					Class392.getString_0(107408812),
					8
				},
				{
					Class392.getString_0(107385774),
					9
				},
				{
					Class392.getString_0(107385181),
					10
				},
				{
					Class392.getString_0(107385911),
					11
				},
				{
					Class392.getString_0(107385118),
					12
				},
				{
					Class392.getString_0(107408841),
					13
				},
				{
					Class392.getString_0(107408271),
					14
				},
				{
					Class392.getString_0(107408870),
					15
				},
				{
					Class392.getString_0(107385873),
					16
				},
				{
					Class392.getString_0(107385949),
					17
				},
				{
					Class392.getString_0(107385191),
					18
				},
				{
					Class392.getString_0(107385848),
					19
				},
				{
					Class392.getString_0(107385896),
					20
				},
				{
					Class392.getString_0(107385963),
					21
				},
				{
					Class392.getString_0(107385978),
					22
				},
				{
					Class392.getString_0(107231798),
					23
				},
				{
					Class392.getString_0(107231745),
					24
				},
				{
					Class392.getString_0(107231716),
					25
				},
				{
					Class392.getString_0(107231691),
					26
				},
				{
					Class392.getString_0(107385035),
					27
				},
				{
					Class392.getString_0(107385988),
					38
				},
				{
					Class392.getString_0(107362904),
					49
				},
				{
					Class392.getString_0(107362522),
					50
				},
				{
					Class392.getString_0(107385862),
					51
				},
				{
					Class392.getString_0(107362855),
					52
				},
				{
					Class392.getString_0(107362511),
					53
				},
				{
					Class392.getString_0(107385781),
					54
				},
				{
					Class392.getString_0(107363219),
					55
				},
				{
					Class392.getString_0(107362484),
					56
				},
				{
					Class392.getString_0(107363198),
					57
				},
				{
					Class392.getString_0(107363204),
					58
				},
				{
					Class392.getString_0(107363769),
					59
				},
				{
					Class392.getString_0(107408923),
					60
				},
				{
					Class392.getString_0(107363036),
					61
				},
				{
					Class392.getString_0(107362893),
					62
				},
				{
					Class392.getString_0(107362473),
					63
				},
				{
					Class392.getString_0(107362975),
					64
				},
				{
					Class392.getString_0(107362918),
					65
				},
				{
					Class392.getString_0(107363160),
					66
				},
				{
					Class392.getString_0(107363127),
					67
				},
				{
					Class392.getString_0(107363094),
					68
				},
				{
					Class392.getString_0(107363065),
					69
				},
				{
					Class392.getString_0(107363285),
					70
				},
				{
					Class392.getString_0(107362983),
					71
				},
				{
					Class392.getString_0(107363303),
					72
				},
				{
					Class392.getString_0(107362570),
					73
				},
				{
					Class392.getString_0(107362581),
					74
				},
				{
					Class392.getString_0(107362595),
					75
				},
				{
					Class392.getString_0(107362560),
					76
				},
				{
					Class392.getString_0(107276098),
					77
				},
				{
					Class392.getString_0(107276164),
					78
				},
				{
					Class392.getString_0(107276766),
					79
				},
				{
					Class392.getString_0(107276808),
					80
				},
				{
					Class392.getString_0(107276069),
					81
				},
				{
					Class392.getString_0(107276131),
					82
				},
				{
					Class392.getString_0(107276705),
					83
				},
				{
					Class392.getString_0(107276771),
					84
				},
				{
					Class392.getString_0(107276051),
					85
				},
				{
					Class392.getString_0(107276936),
					86
				},
				{
					Class392.getString_0(107276911),
					87
				},
				{
					Class392.getString_0(107276914),
					88
				}
			};
		}

		private static Dictionary<string, int> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;

		[CompilerGenerated]
		private sealed class Class393
		{
			internal bool method_0(KeyValuePair<string, int> keyValuePair_0)
			{
				return keyValuePair_0.Value == this.int_0;
			}

			public int int_0;
		}
	}
}
