﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public sealed class JsonSchema
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public bool? Required { get; set; }

		public bool? ReadOnly { get; set; }

		public bool? Hidden { get; set; }

		public bool? Transient { get; set; }

		public string Description { get; set; }

		public JsonSchemaType? Type { get; set; }

		public string Pattern { get; set; }

		public int? MinimumLength { get; set; }

		public int? MaximumLength { get; set; }

		public double? DivisibleBy { get; set; }

		public double? Minimum { get; set; }

		public double? Maximum { get; set; }

		public bool? ExclusiveMinimum { get; set; }

		public bool? ExclusiveMaximum { get; set; }

		public int? MinimumItems { get; set; }

		public int? MaximumItems { get; set; }

		public IList<JsonSchema> Items { get; set; }

		public bool PositionalItemsValidation { get; set; }

		public JsonSchema AdditionalItems { get; set; }

		public bool AllowAdditionalItems { get; set; }

		public bool UniqueItems { get; set; }

		public IDictionary<string, JsonSchema> Properties { get; set; }

		public JsonSchema AdditionalProperties { get; set; }

		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		public bool AllowAdditionalProperties { get; set; }

		public string Requires { get; set; }

		public IList<JToken> Enum { get; set; }

		public JsonSchemaType? Disallow { get; set; }

		public JToken Default { get; set; }

		public IList<JsonSchema> Extends { get; set; }

		public string Format { get; set; }

		internal string Location { get; set; }

		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		internal string DeferredReference { get; set; }

		internal bool ReferencesResolved { get; set; }

		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
		}

		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, JsonSchema.getString_0(107247531));
			ValidationUtils.ArgumentNotNull(resolver, JsonSchema.getString_0(107297116));
			return new JsonSchemaBuilder(resolver).Read(reader);
		}

		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, JsonSchema.getString_0(107297071));
			JsonSchema result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				result = JsonSchema.Read(jsonReader, resolver);
			}
			return result;
		}

		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, JsonSchema.getString_0(107248002));
			ValidationUtils.ArgumentNotNull(resolver, JsonSchema.getString_0(107297116));
			new JsonSchemaWriter(writer, resolver).WriteSchema(this);
		}

		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		static JsonSchema()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSchema));
		}

		private readonly string _internalId = Guid.NewGuid().ToString(JsonSchema.getString_0(107297121));

		[NonSerialized]
		internal static GetString getString_0;
	}
}
