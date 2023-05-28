using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Crafting
{
	public sealed class Materials
	{
		public int Alts
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384797)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384797)] = value;
			}
		}

		public int Chaos
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107393139)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107393139)] = value;
			}
		}

		public int Regals
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384141)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384141)] = value;
			}
		}

		public int Scours
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384204)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384204)] = value;
			}
		}

		public int Transmutes
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107407835)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107407835)] = value;
			}
		}

		public int Augs
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107407864)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107407864)] = value;
			}
		}

		public int Alchs
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384818)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384818)] = value;
			}
		}

		public int Chances
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384267)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384267)] = value;
			}
		}

		public int Jewellers
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384871)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384871)] = value;
			}
		}

		public int Fusings
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384214)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384214)] = value;
			}
		}

		public int Wisdoms
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107407305)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107407305)] = value;
			}
		}

		public int Chisels
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107385001)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107385001)] = value;
			}
		}

		public int Scraps
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107408004)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107408004)] = value;
			}
		}

		public int Whetstones
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107407979)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107407979)] = value;
			}
		}

		public int GCPs
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384896)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384896)] = value;
			}
		}

		public int Abrasives
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107403248)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107403248)] = value;
			}
		}

		public int Imbueds
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107402682)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107402682)] = value;
			}
		}

		public int Intrinsics
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107402693)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107402693)] = value;
			}
		}

		public int Fertiles
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107402668)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107402668)] = value;
			}
		}

		public int Prismatics
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107402586)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107402586)] = value;
			}
		}

		public int Turbulents
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107402593)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107402593)] = value;
			}
		}

		public int Vaals
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107384058)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107384058)] = value;
			}
		}

		public int Bindings
		{
			get
			{
				return this.dictionary_0[Materials.getString_0(107362792)];
			}
			set
			{
				this.dictionary_0[Materials.getString_0(107362792)] = value;
			}
		}

		public unsafe void method_0(string string_0)
		{
			void* ptr = stackalloc byte[27];
			((byte*)ptr)[4] = ((string_0 == Materials.getString_0(107384797)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 4) != 0)
			{
				*(int*)ptr = this.Alts;
				this.Alts = *(int*)ptr + 1;
			}
			((byte*)ptr)[5] = ((string_0 == Materials.getString_0(107393139)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 5) != 0)
			{
				*(int*)ptr = this.Chaos;
				this.Chaos = *(int*)ptr + 1;
			}
			((byte*)ptr)[6] = ((string_0 == Materials.getString_0(107384141)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 6) != 0)
			{
				*(int*)ptr = this.Regals;
				this.Regals = *(int*)ptr + 1;
			}
			((byte*)ptr)[7] = ((string_0 == Materials.getString_0(107384204)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 7) != 0)
			{
				*(int*)ptr = this.Scours;
				this.Scours = *(int*)ptr + 1;
			}
			((byte*)ptr)[8] = ((string_0 == Materials.getString_0(107407835)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 8) != 0)
			{
				*(int*)ptr = this.Transmutes;
				this.Transmutes = *(int*)ptr + 1;
			}
			((byte*)ptr)[9] = ((string_0 == Materials.getString_0(107407864)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 9) != 0)
			{
				*(int*)ptr = this.Augs;
				this.Augs = *(int*)ptr + 1;
			}
			((byte*)ptr)[10] = ((string_0 == Materials.getString_0(107384818)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 10) != 0)
			{
				*(int*)ptr = this.Alchs;
				this.Alchs = *(int*)ptr + 1;
			}
			((byte*)ptr)[11] = ((string_0 == Materials.getString_0(107384267)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 11) != 0)
			{
				*(int*)ptr = this.Chances;
				this.Chances = *(int*)ptr + 1;
			}
			((byte*)ptr)[12] = ((string_0 == Materials.getString_0(107384871)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 12) != 0)
			{
				*(int*)ptr = this.Jewellers;
				this.Jewellers = *(int*)ptr + 1;
			}
			((byte*)ptr)[13] = ((string_0 == Materials.getString_0(107384214)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 13) != 0)
			{
				*(int*)ptr = this.Fusings;
				this.Fusings = *(int*)ptr + 1;
			}
			((byte*)ptr)[14] = ((string_0 == Materials.getString_0(107407305)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 14) != 0)
			{
				*(int*)ptr = this.Wisdoms;
				this.Wisdoms = *(int*)ptr + 1;
			}
			((byte*)ptr)[15] = ((string_0 == Materials.getString_0(107385001)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 15) != 0)
			{
				*(int*)ptr = this.Chisels;
				this.Chisels = *(int*)ptr + 1;
			}
			((byte*)ptr)[16] = ((string_0 == Materials.getString_0(107408004)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 16) != 0)
			{
				*(int*)ptr = this.Scraps;
				this.Scraps = *(int*)ptr + 1;
			}
			((byte*)ptr)[17] = ((string_0 == Materials.getString_0(107407979)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 17) != 0)
			{
				*(int*)ptr = this.Whetstones;
				this.Whetstones = *(int*)ptr + 1;
			}
			((byte*)ptr)[18] = ((string_0 == Materials.getString_0(107384896)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 18) != 0)
			{
				*(int*)ptr = this.GCPs;
				this.GCPs = *(int*)ptr + 1;
			}
			((byte*)ptr)[19] = ((string_0 == Materials.getString_0(107403248)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 19) != 0)
			{
				*(int*)ptr = this.Abrasives;
				this.Abrasives = *(int*)ptr + 1;
			}
			((byte*)ptr)[20] = ((string_0 == Materials.getString_0(107402682)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 20) != 0)
			{
				*(int*)ptr = this.Imbueds;
				this.Imbueds = *(int*)ptr + 1;
			}
			((byte*)ptr)[21] = ((string_0 == Materials.getString_0(107402693)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 21) != 0)
			{
				*(int*)ptr = this.Intrinsics;
				this.Intrinsics = *(int*)ptr + 1;
			}
			((byte*)ptr)[22] = ((string_0 == Materials.getString_0(107402668)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 22) != 0)
			{
				*(int*)ptr = this.Fertiles;
				this.Fertiles = *(int*)ptr + 1;
			}
			((byte*)ptr)[23] = ((string_0 == Materials.getString_0(107402586)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 23) != 0)
			{
				*(int*)ptr = this.Prismatics;
				this.Prismatics = *(int*)ptr + 1;
			}
			((byte*)ptr)[24] = ((string_0 == Materials.getString_0(107402593)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 24) != 0)
			{
				*(int*)ptr = this.Turbulents;
				this.Turbulents = *(int*)ptr + 1;
			}
			((byte*)ptr)[25] = ((string_0 == Materials.getString_0(107384058)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 25) != 0)
			{
				*(int*)ptr = this.Vaals;
				this.Vaals = *(int*)ptr + 1;
			}
			((byte*)ptr)[26] = ((string_0 == Materials.getString_0(107362792)) ? 1 : 0);
			if (*(sbyte*)((byte*)ptr + 26) != 0)
			{
				*(int*)ptr = this.Bindings;
				this.Bindings = *(int*)ptr + 1;
			}
		}

		public Materials()
		{
			this.dictionary_0 = new Dictionary<string, int>
			{
				{
					Materials.getString_0(107384797),
					0
				},
				{
					Materials.getString_0(107393139),
					0
				},
				{
					Materials.getString_0(107384141),
					0
				},
				{
					Materials.getString_0(107384204),
					0
				},
				{
					Materials.getString_0(107407835),
					0
				},
				{
					Materials.getString_0(107407864),
					0
				},
				{
					Materials.getString_0(107384818),
					0
				},
				{
					Materials.getString_0(107384267),
					0
				},
				{
					Materials.getString_0(107384871),
					0
				},
				{
					Materials.getString_0(107384214),
					0
				},
				{
					Materials.getString_0(107407305),
					0
				},
				{
					Materials.getString_0(107385001),
					0
				},
				{
					Materials.getString_0(107408004),
					0
				},
				{
					Materials.getString_0(107407979),
					0
				},
				{
					Materials.getString_0(107384896),
					0
				},
				{
					Materials.getString_0(107403248),
					0
				},
				{
					Materials.getString_0(107402682),
					0
				},
				{
					Materials.getString_0(107402693),
					0
				},
				{
					Materials.getString_0(107402668),
					0
				},
				{
					Materials.getString_0(107402586),
					0
				},
				{
					Materials.getString_0(107402593),
					0
				},
				{
					Materials.getString_0(107384058),
					0
				},
				{
					Materials.getString_0(107362792),
					0
				}
			};
		}

		public string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(Materials.getString_0(107284998));
			foreach (KeyValuePair<string, int> keyValuePair in this.dictionary_0.Where(new Func<KeyValuePair<string, int>, bool>(Materials.<>c.<>9.method_0)).OrderBy(new Func<KeyValuePair<string, int>, string>(Materials.<>c.<>9.method_1)))
			{
				stringBuilder.AppendLine(string.Format(Materials.getString_0(107364042), keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString().Trim();
		}

		static Materials()
		{
			Strings.CreateGetStringDelegate(typeof(Materials));
		}

		private readonly Dictionary<string, int> dictionary_0;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
