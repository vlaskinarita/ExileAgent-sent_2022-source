using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal sealed class JsonSchemaBuilder
	{
		public JsonSchemaBuilder(JsonSchemaResolver resolver)
		{
			this._stack = new List<JsonSchema>();
			this._documentSchemas = new Dictionary<string, JsonSchema>();
			this._resolver = resolver;
		}

		private void Push(JsonSchema value)
		{
			this._currentSchema = value;
			this._stack.Add(value);
			this._resolver.LoadedSchemas.Add(value);
			this._documentSchemas.Add(value.Location, value);
		}

		private JsonSchema Pop()
		{
			JsonSchema currentSchema = this._currentSchema;
			this._stack.RemoveAt(this._stack.Count - 1);
			this._currentSchema = this._stack.LastOrDefault<JsonSchema>();
			return currentSchema;
		}

		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		internal JsonSchema Read(JsonReader reader)
		{
			JToken jtoken = JToken.ReadFrom(reader);
			this._rootSchema = (jtoken as JObject);
			JsonSchema jsonSchema = this.BuildSchema(jtoken);
			this.ResolveReferences(jsonSchema);
			return jsonSchema;
		}

		private string UnescapeReference(string reference)
		{
			return Uri.UnescapeDataString(reference).Replace(JsonSchemaBuilder.getString_0(107297068), JsonSchemaBuilder.getString_0(107377022)).Replace(JsonSchemaBuilder.getString_0(107297095), JsonSchemaBuilder.getString_0(107386196));
		}

		private JsonSchema ResolveReferences(JsonSchema schema)
		{
			if (schema.DeferredReference != null)
			{
				string text = schema.DeferredReference;
				bool flag;
				if (flag = text.StartsWith(JsonSchemaBuilder.getString_0(107402943), StringComparison.Ordinal))
				{
					text = this.UnescapeReference(text);
				}
				JsonSchema jsonSchema = this._resolver.GetSchema(text);
				if (jsonSchema == null)
				{
					if (flag)
					{
						string[] array = schema.DeferredReference.TrimStart(new char[]
						{
							'#'
						}).Split(new char[]
						{
							'/'
						}, StringSplitOptions.RemoveEmptyEntries);
						JToken jtoken = this._rootSchema;
						foreach (string reference in array)
						{
							string text2 = this.UnescapeReference(reference);
							if (jtoken.Type == JTokenType.Object)
							{
								jtoken = jtoken[text2];
							}
							else if (jtoken.Type == JTokenType.Array || jtoken.Type == JTokenType.Constructor)
							{
								int num;
								if (int.TryParse(text2, out num) && num >= 0 && num < jtoken.Count<JToken>())
								{
									jtoken = jtoken[num];
								}
								else
								{
									jtoken = null;
								}
							}
							if (jtoken == null)
							{
								break;
							}
						}
						if (jtoken != null)
						{
							jsonSchema = this.BuildSchema(jtoken);
						}
					}
					if (jsonSchema == null)
					{
						throw new JsonException(JsonSchemaBuilder.getString_0(107297090).FormatWith(CultureInfo.InvariantCulture, schema.DeferredReference));
					}
				}
				schema = jsonSchema;
			}
			if (schema.ReferencesResolved)
			{
				return schema;
			}
			schema.ReferencesResolved = true;
			if (schema.Extends != null)
			{
				for (int j = 0; j < schema.Extends.Count; j++)
				{
					schema.Extends[j] = this.ResolveReferences(schema.Extends[j]);
				}
			}
			if (schema.Items != null)
			{
				for (int k = 0; k < schema.Items.Count; k++)
				{
					schema.Items[k] = this.ResolveReferences(schema.Items[k]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				schema.AdditionalItems = this.ResolveReferences(schema.AdditionalItems);
			}
			if (schema.PatternProperties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in schema.PatternProperties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.PatternProperties[keyValuePair.Key] = this.ResolveReferences(keyValuePair.Value);
				}
			}
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> keyValuePair2 in schema.Properties.ToList<KeyValuePair<string, JsonSchema>>())
				{
					schema.Properties[keyValuePair2.Key] = this.ResolveReferences(keyValuePair2.Value);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				schema.AdditionalProperties = this.ResolveReferences(schema.AdditionalProperties);
			}
			return schema;
		}

		private JsonSchema BuildSchema(JToken token)
		{
			JObject jobject;
			if ((jobject = (token as JObject)) == null)
			{
				throw JsonException.Create(token, token.Path, JsonSchemaBuilder.getString_0(107297033).FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			JToken value;
			if (jobject.TryGetValue(JsonSchemaBuilder.getString_0(107303752), out value))
			{
				return new JsonSchema
				{
					DeferredReference = (string)value
				};
			}
			string text = token.Path.Replace(JsonSchemaBuilder.getString_0(107373804), JsonSchemaBuilder.getString_0(107377022)).Replace(JsonSchemaBuilder.getString_0(107373741), JsonSchemaBuilder.getString_0(107377022)).Replace(JsonSchemaBuilder.getString_0(107373768), string.Empty);
			if (!string.IsNullOrEmpty(text))
			{
				text = JsonSchemaBuilder.getString_0(107377022) + text;
			}
			text = JsonSchemaBuilder.getString_0(107402943) + text;
			JsonSchema result;
			if (this._documentSchemas.TryGetValue(text, out result))
			{
				return result;
			}
			this.Push(new JsonSchema
			{
				Location = text
			});
			this.ProcessSchemaProperties(jobject);
			return this.Pop();
		}

		private void ProcessSchemaProperties(JObject schemaObject)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in schemaObject)
			{
				string key = keyValuePair.Key;
				uint num = <PrivateImplementationDetails>3.ComputeStringHash(key);
				if (num <= 2223801888U)
				{
					if (num <= 981021583U)
					{
						if (num <= 353304077U)
						{
							if (num != 299789532U)
							{
								if (num != 334560121U)
								{
									if (num == 353304077U)
									{
										if (key == JsonSchemaBuilder.getString_0(107296153))
										{
											this.CurrentSchema.DivisibleBy = new double?((double)keyValuePair.Value);
										}
									}
								}
								else if (key == JsonSchemaBuilder.getString_0(107296198))
								{
									this.CurrentSchema.MinimumItems = new int?((int)keyValuePair.Value);
								}
							}
							else if (key == JsonSchemaBuilder.getString_0(107296911))
							{
								this.CurrentSchema.Properties = this.ProcessProperties(keyValuePair.Value);
							}
						}
						else if (num <= 879704937U)
						{
							if (num != 479998177U)
							{
								if (num == 879704937U)
								{
									if (key == JsonSchemaBuilder.getString_0(107296960))
									{
										this.CurrentSchema.Description = (string)keyValuePair.Value;
									}
								}
							}
							else if (key == JsonSchemaBuilder.getString_0(107296926))
							{
								this.ProcessAdditionalProperties(keyValuePair.Value);
							}
						}
						else if (num != 926444256U)
						{
							if (num == 981021583U)
							{
								if (key == JsonSchemaBuilder.getString_0(107455861))
								{
									this.ProcessItems(keyValuePair.Value);
								}
							}
						}
						else if (key == JsonSchemaBuilder.getString_0(107455852))
						{
							this.CurrentSchema.Id = (string)keyValuePair.Value;
						}
					}
					else if (num <= 1693958795U)
					{
						if (num != 1361572173U)
						{
							if (num != 1542649473U)
							{
								if (num == 1693958795U)
								{
									if (key == JsonSchemaBuilder.getString_0(107296262))
									{
										this.CurrentSchema.ExclusiveMaximum = new bool?((bool)keyValuePair.Value);
									}
								}
							}
							else if (key == JsonSchemaBuilder.getString_0(107296268))
							{
								this.CurrentSchema.Maximum = new double?((double)keyValuePair.Value);
							}
						}
						else if (key == JsonSchemaBuilder.getString_0(107374413))
						{
							this.CurrentSchema.Type = this.ProcessType(keyValuePair.Value);
						}
					}
					else if (num <= 2051482624U)
					{
						if (num != 1913005517U)
						{
							if (num == 2051482624U)
							{
								if (key == JsonSchemaBuilder.getString_0(107296385))
								{
									this.ProcessAdditionalItems(keyValuePair.Value);
								}
							}
						}
						else if (key == JsonSchemaBuilder.getString_0(107296287))
						{
							this.CurrentSchema.ExclusiveMinimum = new bool?((bool)keyValuePair.Value);
						}
					}
					else if (num != 2171383808U)
					{
						if (num == 2223801888U)
						{
							if (key == JsonSchemaBuilder.getString_0(107296307))
							{
								this.CurrentSchema.Required = new bool?((bool)keyValuePair.Value);
							}
						}
					}
					else if (key == JsonSchemaBuilder.getString_0(107296587))
					{
						this.ProcessEnum(keyValuePair.Value);
					}
				}
				else if (num <= 2692244416U)
				{
					if (num <= 2474713847U)
					{
						if (num != 2268922153U)
						{
							if (num != 2470140894U)
							{
								if (num == 2474713847U)
								{
									if (key == JsonSchemaBuilder.getString_0(107296281))
									{
										this.CurrentSchema.Minimum = new double?((double)keyValuePair.Value);
									}
								}
							}
							else if (key == JsonSchemaBuilder.getString_0(107296155))
							{
								this.CurrentSchema.Default = keyValuePair.Value.DeepClone();
							}
						}
						else if (key == JsonSchemaBuilder.getString_0(107296600))
						{
							this.CurrentSchema.Pattern = (string)keyValuePair.Value;
						}
					}
					else if (num <= 2609687125U)
					{
						if (num != 2556802313U)
						{
							if (num == 2609687125U)
							{
								if (key == JsonSchemaBuilder.getString_0(107296326))
								{
									this.CurrentSchema.Requires = (string)keyValuePair.Value;
								}
							}
						}
						else if (key == JsonSchemaBuilder.getString_0(107247197))
						{
							this.CurrentSchema.Title = (string)keyValuePair.Value;
						}
					}
					else if (num != 2642794062U)
					{
						if (num == 2692244416U)
						{
							if (key == JsonSchemaBuilder.getString_0(107296168))
							{
								this.CurrentSchema.Disallow = this.ProcessType(keyValuePair.Value);
							}
						}
					}
					else if (key == JsonSchemaBuilder.getString_0(107296610))
					{
						this.ProcessExtends(keyValuePair.Value);
					}
				}
				else if (num <= 3522602594U)
				{
					if (num <= 3114108242U)
					{
						if (num != 2957261815U)
						{
							if (num == 3114108242U)
							{
								if (key == JsonSchemaBuilder.getString_0(107340788))
								{
									this.CurrentSchema.Format = (string)keyValuePair.Value;
								}
							}
						}
						else if (key == JsonSchemaBuilder.getString_0(107296224))
						{
							this.CurrentSchema.MinimumLength = new int?((int)keyValuePair.Value);
						}
					}
					else if (num != 3456888823U)
					{
						if (num == 3522602594U)
						{
							if (key == JsonSchemaBuilder.getString_0(107296565))
							{
								this.CurrentSchema.UniqueItems = (bool)keyValuePair.Value;
							}
						}
					}
					else if (key == JsonSchemaBuilder.getString_0(107296645))
					{
						this.CurrentSchema.ReadOnly = new bool?((bool)keyValuePair.Value);
					}
				}
				else if (num <= 3947606640U)
				{
					if (num != 3526559937U)
					{
						if (num == 3947606640U)
						{
							if (key == JsonSchemaBuilder.getString_0(107296332))
							{
								this.CurrentSchema.PatternProperties = this.ProcessProperties(keyValuePair.Value);
							}
						}
					}
					else if (key == JsonSchemaBuilder.getString_0(107296205))
					{
						this.CurrentSchema.MaximumLength = new int?((int)keyValuePair.Value);
					}
				}
				else if (num != 4128829753U)
				{
					if (num == 4244322099U)
					{
						if (key == JsonSchemaBuilder.getString_0(107296179))
						{
							this.CurrentSchema.MaximumItems = new int?((int)keyValuePair.Value);
						}
					}
				}
				else if (key == JsonSchemaBuilder.getString_0(107296622))
				{
					this.CurrentSchema.Hidden = new bool?((bool)keyValuePair.Value);
				}
			}
		}

		private void ProcessExtends(JToken token)
		{
			IList<JsonSchema> list = new List<JsonSchema>();
			if (token.Type == JTokenType.Array)
			{
				using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)token).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken token2 = enumerator.Current;
						list.Add(this.BuildSchema(token2));
					}
					goto IL_53;
				}
			}
			JsonSchema jsonSchema = this.BuildSchema(token);
			if (jsonSchema != null)
			{
				list.Add(jsonSchema);
			}
			IL_53:
			if (list.Count > 0)
			{
				this.CurrentSchema.Extends = list;
			}
		}

		private void ProcessEnum(JToken token)
		{
			if (token.Type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, JsonSchemaBuilder.getString_0(107296580).FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.Enum = new List<JToken>();
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				this.CurrentSchema.Enum.Add(jtoken.DeepClone());
			}
		}

		private void ProcessAdditionalProperties(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalProperties = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalProperties = this.BuildSchema(token);
		}

		private void ProcessAdditionalItems(JToken token)
		{
			if (token.Type == JTokenType.Boolean)
			{
				this.CurrentSchema.AllowAdditionalItems = (bool)token;
				return;
			}
			this.CurrentSchema.AdditionalItems = this.BuildSchema(token);
		}

		private IDictionary<string, JsonSchema> ProcessProperties(JToken token)
		{
			IDictionary<string, JsonSchema> dictionary = new Dictionary<string, JsonSchema>();
			if (token.Type != JTokenType.Object)
			{
				throw JsonException.Create(token, token.Path, JsonSchemaBuilder.getString_0(107296471).FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			foreach (JToken jtoken in ((IEnumerable<JToken>)token))
			{
				JProperty jproperty = (JProperty)jtoken;
				if (dictionary.ContainsKey(jproperty.Name))
				{
					throw new JsonException(JsonSchemaBuilder.getString_0(107296418).FormatWith(CultureInfo.InvariantCulture, jproperty.Name));
				}
				dictionary.Add(jproperty.Name, this.BuildSchema(jproperty.Value));
			}
			return dictionary;
		}

		private void ProcessItems(JToken token)
		{
			this.CurrentSchema.Items = new List<JsonSchema>();
			JTokenType type = token.Type;
			if (type == JTokenType.Object)
			{
				this.CurrentSchema.Items.Add(this.BuildSchema(token));
				this.CurrentSchema.PositionalItemsValidation = false;
				return;
			}
			if (type != JTokenType.Array)
			{
				throw JsonException.Create(token, token.Path, JsonSchemaBuilder.getString_0(107295841).FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			this.CurrentSchema.PositionalItemsValidation = true;
			foreach (JToken token2 in ((IEnumerable<JToken>)token))
			{
				this.CurrentSchema.Items.Add(this.BuildSchema(token2));
			}
		}

		private JsonSchemaType? ProcessType(JToken token)
		{
			JTokenType type = token.Type;
			if (type == JTokenType.Array)
			{
				JsonSchemaType? jsonSchemaType = new JsonSchemaType?(JsonSchemaType.None);
				foreach (JToken jtoken in ((IEnumerable<JToken>)token))
				{
					if (jtoken.Type != JTokenType.String)
					{
						throw JsonException.Create(jtoken, jtoken.Path, JsonSchemaBuilder.getString_0(107295776).FormatWith(CultureInfo.InvariantCulture, token.Type));
					}
					jsonSchemaType |= JsonSchemaBuilder.MapType((string)jtoken);
				}
				return jsonSchemaType;
			}
			if (type != JTokenType.String)
			{
				throw JsonException.Create(token, token.Path, JsonSchemaBuilder.getString_0(107295711).FormatWith(CultureInfo.InvariantCulture, token.Type));
			}
			return new JsonSchemaType?(JsonSchemaBuilder.MapType((string)token));
		}

		internal static JsonSchemaType MapType(string type)
		{
			JsonSchemaType result;
			if (!JsonSchemaConstants.JsonSchemaTypeMapping.TryGetValue(type, out result))
			{
				throw new JsonException(JsonSchemaBuilder.getString_0(107296114).FormatWith(CultureInfo.InvariantCulture, type));
			}
			return result;
		}

		internal static string MapType(JsonSchemaType type)
		{
			return JsonSchemaConstants.JsonSchemaTypeMapping.Single((KeyValuePair<string, JsonSchemaType> kv) => kv.Value == type).Key;
		}

		static JsonSchemaBuilder()
		{
			Strings.CreateGetStringDelegate(typeof(JsonSchemaBuilder));
		}

		private readonly IList<JsonSchema> _stack;

		private readonly JsonSchemaResolver _resolver;

		private readonly IDictionary<string, JsonSchema> _documentSchemas;

		private JsonSchema _currentSchema;

		private JObject _rootSchema;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
