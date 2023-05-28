using System;

namespace Newtonsoft.Json.Schema
{
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		None = 0,
		String = 1,
		Float = 2,
		Integer = 4,
		Boolean = 8,
		Object = 16,
		Array = 32,
		Null = 64,
		Any = 127
	}
}
