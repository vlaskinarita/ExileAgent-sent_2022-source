using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	public sealed class JRaw : JValue
	{
		public static async Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw result;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
				{
					await jsonWriter.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(false);
					result = new JRaw(sw.ToString());
				}
			}
			return result;
		}

		public JRaw(JRaw other) : base(other)
		{
		}

		public JRaw(object rawJson) : base(rawJson, JTokenType.Raw)
		{
		}

		public static JRaw Create(JsonReader reader)
		{
			JRaw result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					result = new JRaw(stringWriter.ToString());
				}
			}
			return result;
		}

		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
