using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using ns12;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace ns0
{
	internal sealed class Class305
	{
		public string FilePath { get; private set; }

		public string BundleName { get; private set; }

		public Class305(string string_2)
		{
			this.FilePath = string_2;
			this.BundleName = string_2.Replace(Class305.getString_0(107393561), Class305.getString_0(107373804));
		}

		public string SteamFilePath
		{
			get
			{
				return string.Format(Class305.getString_0(107284398), Class120.PoEDirectory, this.FilePath);
			}
		}

		public string BinName
		{
			get
			{
				string[] array = this.FilePath.Split(new char[]
				{
					'\\'
				});
				return (array.Count<string>() == 1) ? this.FilePath : array[1];
			}
		}

		static Class305()
		{
			Strings.CreateGetStringDelegate(typeof(Class305));
		}

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_0;

		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string string_1;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
