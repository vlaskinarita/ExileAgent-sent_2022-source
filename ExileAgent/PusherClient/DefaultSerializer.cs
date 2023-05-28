using System;
using Newtonsoft.Json;

namespace PusherClient
{
	public sealed class DefaultSerializer : ISerializeObjectsToJson
	{
		public static ISerializeObjectsToJson Default { get; } = new DefaultSerializer();

		public string Serialize(object objectToSerialize)
		{
			return JsonConvert.SerializeObject(objectToSerialize, DefaultSerializer._settings);
		}

		private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
	}
}
