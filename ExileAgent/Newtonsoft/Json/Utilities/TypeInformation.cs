using System;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class TypeInformation
	{
		public Type Type { get; set; }

		public PrimitiveTypeCode TypeCode { get; set; }
	}
}
