using System;

namespace Newtonsoft.Json.Converters
{
	public abstract class DateTimeConverterBase : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?);
		}
	}
}
