using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ns36;
using PoEv2.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PoEv2.Models.Crafting
{
	public sealed class Mod
	{
		[JsonProperty]
		private long ModGenerationTypeId { get; set; }

		[JsonProperty]
		private string Code { get; set; }

		[JsonProperty("str")]
		public string InGameText { get; set; }

		[JsonProperty]
		public string Level { get; set; }

		public string Name { get; set; }

		public string ToString()
		{
			string text = ModUtilities.smethod_0(this.InGameText);
			string text2 = ModUtilities.smethod_1(text).Replace(Mod.getString_0(107284995), Mod.getString_0(107370623)).Replace(Mod.getString_0(107284442), Mod.getString_0(107370623));
			return this.IsMap ? ModUtilities.smethod_7(text2) : text2;
		}

		public Enum18 Affix
		{
			get
			{
				return (this.ModGenerationTypeId == 1L) ? Enum18.const_0 : Enum18.const_1;
			}
		}

		public string[] RegexSplit
		{
			get
			{
				return this.Regex.smethod_18(Mod.getString_0(107370623));
			}
		}

		public string ModGroup
		{
			get
			{
				return this.ToString();
			}
		}

		public bool MultiLine
		{
			get
			{
				return this.ToString().Contains(Mod.getString_0(107370623));
			}
		}

		private bool IsMap
		{
			get
			{
				return this.Code != null && this.Code.StartsWith(Mod.getString_0(107445496));
			}
		}

		public unsafe string Regex
		{
			get
			{
				void* ptr = stackalloc byte[5];
				string text = this.ToString().Replace(Mod.getString_0(107391707), Mod.getString_0(107398523));
				string text2 = Mod.getString_0(107284433);
				string[] array = text.smethod_18(Mod.getString_0(107370623));
				string[] array2 = array;
				*(int*)ptr = 0;
				while (*(int*)ptr < array2.Length)
				{
					string source = array2[*(int*)ptr];
					if (source.Count(new Func<char, bool>(Mod.<>c.<>9.method_0)) > 1)
					{
						text2 = Mod.getString_0(107284400) + text2;
					}
					*(int*)ptr = *(int*)ptr + 1;
				}
				((byte*)ptr)[4] = ((text[0] == '#') ? 1 : 0);
				string result;
				if (*(sbyte*)((byte*)ptr + 4) != 0)
				{
					result = Mod.getString_0(107284427) + text.Replace(Mod.getString_0(107399716), text2);
				}
				else
				{
					result = text.Replace(Mod.getString_0(107399716), text2);
				}
				return result;
			}
		}

		static Mod()
		{
			Strings.CreateGetStringDelegate(typeof(Mod));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long long_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_2;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_3;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
