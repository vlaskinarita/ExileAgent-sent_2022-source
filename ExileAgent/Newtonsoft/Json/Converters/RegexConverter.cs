using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Converters
{
	public sealed class RegexConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Regex regex = (Regex)value;
			BsonWriter writer2;
			if ((writer2 = (writer as BsonWriter)) != null)
			{
				this.WriteBson(writer2, regex);
				return;
			}
			this.WriteJson(writer, regex, serializer);
		}

		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		private void WriteBson(BsonWriter writer, Regex regex)
		{
			string text = null;
			if (this.HasFlag(regex.Options, RegexOptions.IgnoreCase))
			{
				text += RegexConverter.getString_0(107401116);
			}
			if (this.HasFlag(regex.Options, RegexOptions.Multiline))
			{
				text += RegexConverter.getString_0(107401074);
			}
			if (this.HasFlag(regex.Options, RegexOptions.Singleline))
			{
				text += RegexConverter.getString_0(107401069);
			}
			text += RegexConverter.getString_0(107321677);
			if (this.HasFlag(regex.Options, RegexOptions.ExplicitCapture))
			{
				text += RegexConverter.getString_0(107402392);
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		private void WriteJson(JsonWriter writer, Regex regex, JsonSerializer serializer)
		{
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(RegexConverter.getString_0(107321704)) : RegexConverter.getString_0(107321704));
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(RegexConverter.getString_0(107321691)) : RegexConverter.getString_0(107321691));
			serializer.Serialize(writer, regex.Options);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadRegexObject(reader, serializer);
			}
			if (tokenType == JsonToken.String)
			{
				return this.ReadRegexString(reader);
			}
			if (tokenType != JsonToken.Null)
			{
				throw JsonSerializationException.Create(reader, RegexConverter.getString_0(107321646));
			}
			return null;
		}

		private object ReadRegexString(JsonReader reader)
		{
			string text = (string)reader.Value;
			if (text.Length > 0 && text[0] == '/')
			{
				int num = text.LastIndexOf('/');
				if (num > 0)
				{
					string pattern = text.Substring(1, num - 1);
					RegexOptions regexOptions = MiscellaneousUtils.GetRegexOptions(text.Substring(num + 1));
					return new Regex(pattern, regexOptions);
				}
			}
			throw JsonSerializationException.Create(reader, RegexConverter.getString_0(107321629));
		}

		private Regex ReadRegexObject(JsonReader reader, JsonSerializer serializer)
		{
			string text = null;
			RegexOptions? regexOptions = null;
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							if (text == null)
							{
								throw JsonSerializationException.Create(reader, RegexConverter.getString_0(107320979));
							}
							return new Regex(text, regexOptions ?? RegexOptions.None);
						}
					}
				}
				else
				{
					string a = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, RegexConverter.getString_0(107321572));
					}
					if (string.Equals(a, RegexConverter.getString_0(107321704), StringComparison.OrdinalIgnoreCase))
					{
						text = (string)reader.Value;
					}
					else if (string.Equals(a, RegexConverter.getString_0(107321691), StringComparison.OrdinalIgnoreCase))
					{
						regexOptions = new RegexOptions?(serializer.Deserialize<RegexOptions>(reader));
					}
					else
					{
						reader.Skip();
					}
				}
			}
			throw JsonSerializationException.Create(reader, RegexConverter.getString_0(107321572));
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.Name == RegexConverter.getString_0(107320918) && this.IsRegex(objectType);
		}

		private bool IsRegex(Type objectType)
		{
			return objectType == typeof(Regex);
		}

		static RegexConverter()
		{
			Strings.CreateGetStringDelegate(typeof(RegexConverter));
		}

		private const string PatternName = "Pattern";

		private const string OptionsName = "Options";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
