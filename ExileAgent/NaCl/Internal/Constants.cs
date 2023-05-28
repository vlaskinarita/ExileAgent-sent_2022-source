using System;
using System.Runtime.CompilerServices;

namespace NaCl.Internal
{
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Constants
	{
		public static readonly byte[] Sigma = new byte[]
		{
			101,
			120,
			112,
			97,
			110,
			100,
			32,
			51,
			50,
			45,
			98,
			121,
			116,
			101,
			32,
			107
		};

		public static readonly byte[] N = new byte[32];

		public static readonly uint[] MinUsp = new uint[]
		{
			19U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			128U
		};
	}
}
