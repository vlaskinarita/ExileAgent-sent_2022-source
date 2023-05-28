using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal sealed class JsonSchemaNode
	{
		public string Id { get; }

		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		public Dictionary<string, JsonSchemaNode> Properties { get; }

		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		public List<JsonSchemaNode> Items { get; }

		public JsonSchemaNode AdditionalProperties { get; set; }

		public JsonSchemaNode AdditionalItems { get; set; }

		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[]
			{
				schema
			});
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[]
			{
				schema
			}).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join(JsonSchemaNode.getString_0(107373751), (from s in schemata
			select s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal));
		}

		static JsonSchemaNode()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSchemaNode));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
