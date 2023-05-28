using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		public NullableAttribute(byte byte_0)
		{
			this.NullableFlags = new byte[]
			{
				byte_0
			};
		}

		public NullableAttribute(byte[] byte_0)
		{
			this.NullableFlags = byte_0;
		}

		public readonly byte[] NullableFlags;
	}
}
