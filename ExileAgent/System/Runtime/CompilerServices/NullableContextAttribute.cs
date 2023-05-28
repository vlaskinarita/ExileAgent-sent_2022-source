using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		public NullableContextAttribute(byte byte_0)
		{
			this.Flag = byte_0;
		}

		public readonly byte Flag;
	}
}
