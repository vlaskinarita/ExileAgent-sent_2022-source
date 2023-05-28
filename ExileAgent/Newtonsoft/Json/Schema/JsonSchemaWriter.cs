using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal sealed class JsonSchemaWriter
	{
		public JsonSchemaWriter(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, JsonSchemaWriter.getString_0(107248108));
			this._writer = writer;
			this._resolver = resolver;
		}

		private void ReferenceOrWriteSchema(JsonSchema schema)
		{
			if (schema.Id != null && this._resolver.GetSchema(schema.Id) != null)
			{
				this._writer.WriteStartObject();
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107303852));
				this._writer.WriteValue(schema.Id);
				this._writer.WriteEndObject();
				return;
			}
			this.WriteSchema(schema);
		}

		public void WriteSchema(JsonSchema schema)
		{
			ValidationUtils.ArgumentNotNull(schema, JsonSchemaWriter.getString_0(107297204));
			if (!this._resolver.LoadedSchemas.Contains(schema))
			{
				this._resolver.LoadedSchemas.Add(schema);
			}
			this._writer.WriteStartObject();
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107455952), schema.Id);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107247297), schema.Title);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107297060), schema.Description);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296407), schema.Required);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296745), schema.ReadOnly);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296722), schema.Hidden);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107295222), schema.Transient);
			if (schema.Type != null)
			{
				this.WriteType(JsonSchemaWriter.getString_0(107374513), this._writer, schema.Type.GetValueOrDefault());
			}
			if (!schema.AllowAdditionalProperties)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107297026));
				this._writer.WriteValue(schema.AllowAdditionalProperties);
			}
			else if (schema.AdditionalProperties != null)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107297026));
				this.ReferenceOrWriteSchema(schema.AdditionalProperties);
			}
			if (!schema.AllowAdditionalItems)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107296485));
				this._writer.WriteValue(schema.AllowAdditionalItems);
			}
			else if (schema.AdditionalItems != null)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107296485));
				this.ReferenceOrWriteSchema(schema.AdditionalItems);
			}
			this.WriteSchemaDictionaryIfNotNull(this._writer, JsonSchemaWriter.getString_0(107297011), schema.Properties);
			this.WriteSchemaDictionaryIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296432), schema.PatternProperties);
			this.WriteItems(schema);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296381), schema.Minimum);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296368), schema.Maximum);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296387), schema.ExclusiveMinimum);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296362), schema.ExclusiveMaximum);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296324), schema.MinimumLength);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296305), schema.MaximumLength);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296298), schema.MinimumItems);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296279), schema.MaximumItems);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296253), schema.DivisibleBy);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107340888), schema.Format);
			this.WritePropertyIfNotNull(this._writer, JsonSchemaWriter.getString_0(107296700), schema.Pattern);
			if (schema.Enum != null)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107296687));
				this._writer.WriteStartArray();
				foreach (JToken jtoken in schema.Enum)
				{
					jtoken.WriteTo(this._writer, new JsonConverter[0]);
				}
				this._writer.WriteEndArray();
			}
			if (schema.Default != null)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107296255));
				schema.Default.WriteTo(this._writer, new JsonConverter[0]);
			}
			if (schema.Disallow != null)
			{
				this.WriteType(JsonSchemaWriter.getString_0(107296268), this._writer, schema.Disallow.GetValueOrDefault());
			}
			if (schema.Extends != null && schema.Extends.Count > 0)
			{
				this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107296710));
				if (schema.Extends.Count == 1)
				{
					this.ReferenceOrWriteSchema(schema.Extends[0]);
				}
				else
				{
					this._writer.WriteStartArray();
					foreach (JsonSchema schema2 in schema.Extends)
					{
						this.ReferenceOrWriteSchema(schema2);
					}
					this._writer.WriteEndArray();
				}
			}
			this._writer.WriteEndObject();
		}

		private void WriteSchemaDictionaryIfNotNull(JsonWriter writer, string propertyName, IDictionary<string, JsonSchema> properties)
		{
			if (properties != null)
			{
				writer.WritePropertyName(propertyName);
				writer.WriteStartObject();
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in properties)
				{
					writer.WritePropertyName(keyValuePair.Key);
					this.ReferenceOrWriteSchema(keyValuePair.Value);
				}
				writer.WriteEndObject();
			}
		}

		private void WriteItems(JsonSchema schema)
		{
			if (schema.Items == null && !schema.PositionalItemsValidation)
			{
				return;
			}
			this._writer.WritePropertyName(JsonSchemaWriter.getString_0(107455961));
			if (schema.PositionalItemsValidation)
			{
				this._writer.WriteStartArray();
				if (schema.Items != null)
				{
					foreach (JsonSchema schema2 in schema.Items)
					{
						this.ReferenceOrWriteSchema(schema2);
					}
				}
				this._writer.WriteEndArray();
				return;
			}
			if (schema.Items != null && schema.Items.Count > 0)
			{
				this.ReferenceOrWriteSchema(schema.Items[0]);
				return;
			}
			this._writer.WriteStartObject();
			this._writer.WriteEndObject();
		}

		private void WriteType(string propertyName, JsonWriter writer, JsonSchemaType type)
		{
			if (Enum.IsDefined(typeof(JsonSchemaType), type))
			{
				writer.WritePropertyName(propertyName);
				writer.WriteValue(JsonSchemaBuilder.MapType(type));
				return;
			}
			IEnumerator<JsonSchemaType> enumerator = (from v in EnumUtils.GetFlagsValues<JsonSchemaType>(type)
			where v > JsonSchemaType.None
			select v).GetEnumerator();
			if (enumerator.MoveNext())
			{
				writer.WritePropertyName(propertyName);
				JsonSchemaType type2 = enumerator.Current;
				if (enumerator.MoveNext())
				{
					writer.WriteStartArray();
					writer.WriteValue(JsonSchemaBuilder.MapType(type2));
					do
					{
						writer.WriteValue(JsonSchemaBuilder.MapType(enumerator.Current));
					}
					while (enumerator.MoveNext());
					writer.WriteEndArray();
					return;
				}
				writer.WriteValue(JsonSchemaBuilder.MapType(type2));
			}
		}

		private void WritePropertyIfNotNull(JsonWriter writer, string propertyName, object value)
		{
			if (value != null)
			{
				writer.WritePropertyName(propertyName);
				writer.WriteValue(value);
			}
		}

		static JsonSchemaWriter()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSchemaWriter));
		}

		private readonly JsonWriter _writer;

		private readonly JsonSchemaResolver _resolver;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
