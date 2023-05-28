using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal sealed class JsonSchemaModelBuilder
	{
		public JsonSchemaModel Build(JsonSchema schema)
		{
			this._nodes = new JsonSchemaNodeCollection();
			this._node = this.AddSchema(null, schema);
			this._nodeModels = new Dictionary<JsonSchemaNode, JsonSchemaModel>();
			return this.BuildNodeModel(this._node);
		}

		public JsonSchemaNode AddSchema(JsonSchemaNode existingNode, JsonSchema schema)
		{
			string id;
			if (existingNode != null)
			{
				if (existingNode.Schemas.Contains(schema))
				{
					return existingNode;
				}
				id = JsonSchemaNode.GetId(existingNode.Schemas.Union(new JsonSchema[]
				{
					schema
				}));
			}
			else
			{
				id = JsonSchemaNode.GetId(new JsonSchema[]
				{
					schema
				});
			}
			if (this._nodes.Contains(id))
			{
				return this._nodes[id];
			}
			JsonSchemaNode jsonSchemaNode = (existingNode != null) ? existingNode.Combine(schema) : new JsonSchemaNode(schema);
			this._nodes.Add(jsonSchemaNode);
			this.AddProperties(schema.Properties, jsonSchemaNode.Properties);
			this.AddProperties(schema.PatternProperties, jsonSchemaNode.PatternProperties);
			if (schema.Items != null)
			{
				for (int i = 0; i < schema.Items.Count; i++)
				{
					this.AddItem(jsonSchemaNode, i, schema.Items[i]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				this.AddAdditionalItems(jsonSchemaNode, schema.AdditionalItems);
			}
			if (schema.AdditionalProperties != null)
			{
				this.AddAdditionalProperties(jsonSchemaNode, schema.AdditionalProperties);
			}
			if (schema.Extends != null)
			{
				foreach (JsonSchema schema2 in schema.Extends)
				{
					jsonSchemaNode = this.AddSchema(jsonSchemaNode, schema2);
				}
			}
			return jsonSchemaNode;
		}

		public void AddProperties(IDictionary<string, JsonSchema> source, IDictionary<string, JsonSchemaNode> target)
		{
			if (source != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in source)
				{
					this.AddProperty(target, keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		public void AddProperty(IDictionary<string, JsonSchemaNode> target, string propertyName, JsonSchema schema)
		{
			JsonSchemaNode existingNode;
			target.TryGetValue(propertyName, out existingNode);
			target[propertyName] = this.AddSchema(existingNode, schema);
		}

		public void AddItem(JsonSchemaNode parentNode, int index, JsonSchema schema)
		{
			JsonSchemaNode existingNode = (parentNode.Items.Count > index) ? parentNode.Items[index] : null;
			JsonSchemaNode jsonSchemaNode = this.AddSchema(existingNode, schema);
			if (parentNode.Items.Count <= index)
			{
				parentNode.Items.Add(jsonSchemaNode);
				return;
			}
			parentNode.Items[index] = jsonSchemaNode;
		}

		public void AddAdditionalProperties(JsonSchemaNode parentNode, JsonSchema schema)
		{
			parentNode.AdditionalProperties = this.AddSchema(parentNode.AdditionalProperties, schema);
		}

		public void AddAdditionalItems(JsonSchemaNode parentNode, JsonSchema schema)
		{
			parentNode.AdditionalItems = this.AddSchema(parentNode.AdditionalItems, schema);
		}

		private JsonSchemaModel BuildNodeModel(JsonSchemaNode node)
		{
			JsonSchemaModel jsonSchemaModel;
			if (this._nodeModels.TryGetValue(node, out jsonSchemaModel))
			{
				return jsonSchemaModel;
			}
			jsonSchemaModel = JsonSchemaModel.Create(node.Schemas);
			this._nodeModels[node] = jsonSchemaModel;
			foreach (KeyValuePair<string, JsonSchemaNode> keyValuePair in node.Properties)
			{
				if (jsonSchemaModel.Properties == null)
				{
					jsonSchemaModel.Properties = new Dictionary<string, JsonSchemaModel>();
				}
				jsonSchemaModel.Properties[keyValuePair.Key] = this.BuildNodeModel(keyValuePair.Value);
			}
			foreach (KeyValuePair<string, JsonSchemaNode> keyValuePair2 in node.PatternProperties)
			{
				if (jsonSchemaModel.PatternProperties == null)
				{
					jsonSchemaModel.PatternProperties = new Dictionary<string, JsonSchemaModel>();
				}
				jsonSchemaModel.PatternProperties[keyValuePair2.Key] = this.BuildNodeModel(keyValuePair2.Value);
			}
			foreach (JsonSchemaNode node2 in node.Items)
			{
				if (jsonSchemaModel.Items == null)
				{
					jsonSchemaModel.Items = new List<JsonSchemaModel>();
				}
				jsonSchemaModel.Items.Add(this.BuildNodeModel(node2));
			}
			if (node.AdditionalProperties != null)
			{
				jsonSchemaModel.AdditionalProperties = this.BuildNodeModel(node.AdditionalProperties);
			}
			if (node.AdditionalItems != null)
			{
				jsonSchemaModel.AdditionalItems = this.BuildNodeModel(node.AdditionalItems);
			}
			return jsonSchemaModel;
		}

		private JsonSchemaNodeCollection _nodes = new JsonSchemaNodeCollection();

		private Dictionary<JsonSchemaNode, JsonSchemaModel> _nodeModels = new Dictionary<JsonSchemaNode, JsonSchemaModel>();

		private JsonSchemaNode _node;
	}
}
