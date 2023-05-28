using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public sealed class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		public event ValidationEventHandler ValidationEventHandler;

		public override object Value
		{
			get
			{
				return this._reader.Value;
			}
		}

		public override int Depth
		{
			get
			{
				return this._reader.Depth;
			}
		}

		public override string Path
		{
			get
			{
				return this._reader.Path;
			}
		}

		public override char QuoteChar
		{
			get
			{
				return this._reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		public override JsonToken TokenType
		{
			get
			{
				return this._reader.TokenType;
			}
		}

		public override Type ValueType
		{
			get
			{
				return this._reader.ValueType;
			}
		}

		private void Push(JsonValidatingReader.SchemaScope scope)
		{
			this._stack.Push(scope);
			this._currentScope = scope;
		}

		private JsonValidatingReader.SchemaScope Pop()
		{
			JsonValidatingReader.SchemaScope result = this._stack.Pop();
			this._currentScope = ((this._stack.Count != 0) ? this._stack.Peek() : null);
			return result;
		}

		private IList<JsonSchemaModel> CurrentSchemas
		{
			get
			{
				return this._currentScope.Schemas;
			}
		}

		private IList<JsonSchemaModel> CurrentMemberSchemas
		{
			get
			{
				if (this._currentScope == null)
				{
					return new List<JsonSchemaModel>(new JsonSchemaModel[]
					{
						this._model
					});
				}
				if (this._currentScope.Schemas == null || this._currentScope.Schemas.Count == 0)
				{
					return JsonValidatingReader.EmptySchemaList;
				}
				switch (this._currentScope.TokenType)
				{
				case JTokenType.None:
					return this._currentScope.Schemas;
				case JTokenType.Object:
				{
					if (this._currentScope.CurrentPropertyName == null)
					{
						throw new JsonReaderException(JsonValidatingReader.getString_1(107347687));
					}
					IList<JsonSchemaModel> list = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
					{
						JsonSchemaModel item;
						if (jsonSchemaModel.Properties != null && jsonSchemaModel.Properties.TryGetValue(this._currentScope.CurrentPropertyName, out item))
						{
							list.Add(item);
						}
						if (jsonSchemaModel.PatternProperties != null)
						{
							foreach (KeyValuePair<string, JsonSchemaModel> keyValuePair in jsonSchemaModel.PatternProperties)
							{
								if (Regex.IsMatch(this._currentScope.CurrentPropertyName, keyValuePair.Key))
								{
									list.Add(keyValuePair.Value);
								}
							}
						}
						if (list.Count == 0 && jsonSchemaModel.AllowAdditionalProperties && jsonSchemaModel.AdditionalProperties != null)
						{
							list.Add(jsonSchemaModel.AdditionalProperties);
						}
					}
					return list;
				}
				case JTokenType.Array:
				{
					IList<JsonSchemaModel> list2 = new List<JsonSchemaModel>();
					foreach (JsonSchemaModel jsonSchemaModel2 in this.CurrentSchemas)
					{
						if (!jsonSchemaModel2.PositionalItemsValidation)
						{
							if (jsonSchemaModel2.Items != null && jsonSchemaModel2.Items.Count > 0)
							{
								list2.Add(jsonSchemaModel2.Items[0]);
							}
						}
						else
						{
							if (jsonSchemaModel2.Items != null && jsonSchemaModel2.Items.Count > 0 && jsonSchemaModel2.Items.Count > this._currentScope.ArrayItemCount - 1)
							{
								list2.Add(jsonSchemaModel2.Items[this._currentScope.ArrayItemCount - 1]);
							}
							if (jsonSchemaModel2.AllowAdditionalItems && jsonSchemaModel2.AdditionalItems != null)
							{
								list2.Add(jsonSchemaModel2.AdditionalItems);
							}
						}
					}
					return list2;
				}
				case JTokenType.Constructor:
					return JsonValidatingReader.EmptySchemaList;
				default:
					throw new ArgumentOutOfRangeException(JsonValidatingReader.getString_1(107347622), JsonValidatingReader.getString_1(107348089).FormatWith(CultureInfo.InvariantCulture, this._currentScope.TokenType));
				}
			}
		}

		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string message2 = ((IJsonLineInfo)this).HasLineInfo() ? (message + JsonValidatingReader.getString_1(107348052).FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message;
			this.OnValidationEvent(new JsonSchemaException(message2, null, this.Path, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler == null)
			{
				throw exception;
			}
			validationEventHandler(this, new ValidationEventArgs(exception));
		}

		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, JsonValidatingReader.getString_1(107246778));
			this._reader = reader;
			this._stack = new Stack<JsonValidatingReader.SchemaScope>();
		}

		public JsonSchema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				if (this.TokenType != JsonToken.None)
				{
					throw new InvalidOperationException(JsonValidatingReader.getString_1(107348019));
				}
				this._schema = value;
				this._model = null;
			}
		}

		public JsonReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				JsonReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Close();
			}
		}

		private void ValidateNotDisallowed(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			JsonSchemaType? currentNodeSchemaType = this.GetCurrentNodeSchemaType();
			if (currentNodeSchemaType != null && JsonSchemaGenerator.HasFlag(new JsonSchemaType?(schema.Disallow), currentNodeSchemaType.GetValueOrDefault()))
			{
				this.RaiseError(JsonValidatingReader.getString_1(107347958).FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
			}
		}

		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (this._reader.TokenType)
			{
			case JsonToken.StartObject:
				return new JsonSchemaType?(JsonSchemaType.Object);
			case JsonToken.StartArray:
				return new JsonSchemaType?(JsonSchemaType.Array);
			case JsonToken.Integer:
				return new JsonSchemaType?(JsonSchemaType.Integer);
			case JsonToken.Float:
				return new JsonSchemaType?(JsonSchemaType.Float);
			case JsonToken.String:
				return new JsonSchemaType?(JsonSchemaType.String);
			case JsonToken.Boolean:
				return new JsonSchemaType?(JsonSchemaType.Boolean);
			case JsonToken.Null:
				return new JsonSchemaType?(JsonSchemaType.Null);
			}
			return null;
		}

		public override int? ReadAsInt32()
		{
			int? result = this._reader.ReadAsInt32();
			this.ValidateCurrentToken();
			return result;
		}

		public override byte[] ReadAsBytes()
		{
			byte[] result = this._reader.ReadAsBytes();
			this.ValidateCurrentToken();
			return result;
		}

		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._reader.ReadAsDecimal();
			this.ValidateCurrentToken();
			return result;
		}

		public override double? ReadAsDouble()
		{
			double? result = this._reader.ReadAsDouble();
			this.ValidateCurrentToken();
			return result;
		}

		public override bool? ReadAsBoolean()
		{
			bool? result = this._reader.ReadAsBoolean();
			this.ValidateCurrentToken();
			return result;
		}

		public override string ReadAsString()
		{
			string result = this._reader.ReadAsString();
			this.ValidateCurrentToken();
			return result;
		}

		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._reader.ReadAsDateTime();
			this.ValidateCurrentToken();
			return result;
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._reader.ReadAsDateTimeOffset();
			this.ValidateCurrentToken();
			return result;
		}

		public override bool Read()
		{
			if (!this._reader.Read())
			{
				return false;
			}
			if (this._reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			this.ValidateCurrentToken();
			return true;
		}

		private void ValidateCurrentToken()
		{
			if (this._model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				this._model = jsonSchemaModelBuilder.Build(this._schema);
				if (!JsonTokenUtils.IsStartToken(this._reader.TokenType))
				{
					this.Push(new JsonValidatingReader.SchemaScope(JTokenType.None, this.CurrentMemberSchemas));
				}
			}
			switch (this._reader.TokenType)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateObject)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Object, schemas));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartArray:
			{
				this.ProcessValue();
				IList<JsonSchemaModel> schemas2 = this.CurrentMemberSchemas.Where(new Func<JsonSchemaModel, bool>(this.ValidateArray)).ToList<JsonSchemaModel>();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Array, schemas2));
				this.WriteToken(this.CurrentSchemas);
				return;
			}
			case JsonToken.StartConstructor:
				this.ProcessValue();
				this.Push(new JsonValidatingReader.SchemaScope(JTokenType.Constructor, null));
				this.WriteToken(this.CurrentSchemas);
				return;
			case JsonToken.PropertyName:
				this.WriteToken(this.CurrentSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel schema = enumerator.Current;
						this.ValidatePropertyName(schema);
					}
					return;
				}
				break;
			case JsonToken.Raw:
				this.ProcessValue();
				return;
			case JsonToken.Integer:
				this.ProcessValue();
				this.WriteToken(this.CurrentMemberSchemas);
				using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JsonSchemaModel schema2 = enumerator.Current;
						this.ValidateInteger(schema2);
					}
					return;
				}
				goto IL_1DE;
			case JsonToken.Float:
				goto IL_1DE;
			case JsonToken.String:
				goto IL_22B;
			case JsonToken.Boolean:
				goto IL_278;
			case JsonToken.Null:
				goto IL_2C2;
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.WriteToken(this.CurrentMemberSchemas);
				return;
			case JsonToken.EndObject:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel schema3 in this.CurrentSchemas)
				{
					this.ValidateEndObject(schema3);
				}
				this.Pop();
				return;
			case JsonToken.EndArray:
				this.WriteToken(this.CurrentSchemas);
				foreach (JsonSchemaModel schema4 in this.CurrentSchemas)
				{
					this.ValidateEndArray(schema4);
				}
				this.Pop();
				return;
			case JsonToken.EndConstructor:
				this.WriteToken(this.CurrentSchemas);
				this.Pop();
				return;
			}
			throw new ArgumentOutOfRangeException();
			IL_1DE:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema5 = enumerator.Current;
					this.ValidateFloat(schema5);
				}
				return;
			}
			IL_22B:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema6 = enumerator.Current;
					this.ValidateString(schema6);
				}
				return;
			}
			IL_278:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			using (IEnumerator<JsonSchemaModel> enumerator = this.CurrentMemberSchemas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonSchemaModel schema7 = enumerator.Current;
					this.ValidateBoolean(schema7);
				}
				return;
			}
			IL_2C2:
			this.ProcessValue();
			this.WriteToken(this.CurrentMemberSchemas);
			foreach (JsonSchemaModel schema8 in this.CurrentMemberSchemas)
			{
				this.ValidateNull(schema8);
			}
		}

		private void WriteToken(IList<JsonSchemaModel> schemas)
		{
			foreach (JsonValidatingReader.SchemaScope schemaScope in this._stack)
			{
				bool flag;
				if (!(flag = (schemaScope.TokenType == JTokenType.Array && schemaScope.IsUniqueArray && schemaScope.ArrayItemCount > 0)))
				{
					if (!schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						continue;
					}
				}
				if (schemaScope.CurrentItemWriter == null)
				{
					if (JsonTokenUtils.IsEndToken(this._reader.TokenType))
					{
						continue;
					}
					schemaScope.CurrentItemWriter = new JTokenWriter();
				}
				schemaScope.CurrentItemWriter.WriteToken(this._reader, false);
				if (schemaScope.CurrentItemWriter.Top == 0 && this._reader.TokenType != JsonToken.PropertyName)
				{
					JToken token = schemaScope.CurrentItemWriter.Token;
					schemaScope.CurrentItemWriter = null;
					if (flag)
					{
						if (schemaScope.UniqueArrayItems.Contains(token, JToken.EqualityComparer))
						{
							this.RaiseError(JsonValidatingReader.getString_1(107347925).FormatWith(CultureInfo.InvariantCulture, schemaScope.ArrayItemCount - 1), schemaScope.Schemas.First((JsonSchemaModel s) => s.UniqueItems));
						}
						schemaScope.UniqueArrayItems.Add(token);
					}
					else if (schemas.Any((JsonSchemaModel s) => s.Enum != null))
					{
						foreach (JsonSchemaModel jsonSchemaModel in schemas)
						{
							if (jsonSchemaModel.Enum != null && !jsonSchemaModel.Enum.ContainsValue(token, JToken.EqualityComparer))
							{
								StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
								token.WriteTo(new JsonTextWriter(stringWriter), new JsonConverter[0]);
								this.RaiseError(JsonValidatingReader.getString_1(107347908).FormatWith(CultureInfo.InvariantCulture, stringWriter.ToString()), jsonSchemaModel);
							}
						}
					}
				}
			}
		}

		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = this._currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				if (requiredProperties.Values.Any((bool v) => !v))
				{
					IEnumerable<string> values = from kv in requiredProperties
					where !kv.Value
					select kv.Key;
					this.RaiseError(JsonValidatingReader.getString_1(107347319).FormatWith(CultureInfo.InvariantCulture, string.Join(JsonValidatingReader.getString_1(107401074), values)), schema);
				}
			}
		}

		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			int arrayItemCount = this._currentScope.ArrayItemCount;
			if (schema.MaximumItems != null)
			{
				int num = arrayItemCount;
				int? num2 = schema.MaximumItems;
				if (num > num2.GetValueOrDefault() & num2 != null)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107347282).FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
				}
			}
			if (schema.MinimumItems != null)
			{
				int num3 = arrayItemCount;
				int? num2 = schema.MinimumItems;
				if (num3 < num2.GetValueOrDefault() & num2 != null)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107347213).FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
				}
			}
		}

		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Null))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Boolean))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
		}

		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			string text = this._reader.Value.ToString();
			if (schema.MaximumLength != null)
			{
				int length = text.Length;
				int? num = schema.MaximumLength;
				if (length > num.GetValueOrDefault() & num != null)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107347104).FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
				}
			}
			if (schema.MinimumLength != null)
			{
				int length2 = text.Length;
				int? num = schema.MinimumLength;
				if (length2 < num.GetValueOrDefault() & num != null)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107347587).FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
				}
			}
			if (schema.Patterns != null)
			{
				foreach (string text2 in schema.Patterns)
				{
					if (!Regex.IsMatch(text, text2))
					{
						this.RaiseError(JsonValidatingReader.getString_1(107347490).FormatWith(CultureInfo.InvariantCulture, text, text2), schema);
					}
				}
			}
		}

		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Integer))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			object value = this._reader.Value;
			if (schema.Maximum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Maximum) > 0)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107347425).FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum && JValue.Compare(JTokenType.Integer, value, schema.Maximum) == 0)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107347400).FormatWith(CultureInfo.InvariantCulture, value, schema.Maximum), schema);
				}
			}
			if (schema.Minimum != null)
			{
				if (JValue.Compare(JTokenType.Integer, value, schema.Minimum) < 0)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107346791).FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum && JValue.Compare(JTokenType.Integer, value, schema.Minimum) == 0)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107346726).FormatWith(CultureInfo.InvariantCulture, value, schema.Minimum), schema);
				}
			}
			if (schema.DivisibleBy != null)
			{
				object obj;
				bool flag;
				if ((obj = value) is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)obj;
					if (!Math.Abs(schema.DivisibleBy.Value - Math.Truncate(schema.DivisibleBy.Value)).Equals(0.0))
					{
						flag = (bigInteger != 0L);
					}
					else
					{
						flag = (bigInteger % new BigInteger(schema.DivisibleBy.Value) != 0L);
					}
				}
				else
				{
					flag = !JsonValidatingReader.IsZero((double)Convert.ToInt64(value, CultureInfo.InvariantCulture) % schema.DivisibleBy.GetValueOrDefault());
				}
				if (flag)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107346629).FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(value), schema.DivisibleBy), schema);
				}
			}
		}

		private void ProcessValue()
		{
			if (this._currentScope != null && this._currentScope.TokenType == JTokenType.Array)
			{
				JsonValidatingReader.SchemaScope currentScope = this._currentScope;
				int arrayItemCount = currentScope.ArrayItemCount;
				currentScope.ArrayItemCount = arrayItemCount + 1;
				foreach (JsonSchemaModel jsonSchemaModel in this.CurrentSchemas)
				{
					if (jsonSchemaModel != null && jsonSchemaModel.PositionalItemsValidation && !jsonSchemaModel.AllowAdditionalItems && (jsonSchemaModel.Items == null || this._currentScope.ArrayItemCount - 1 >= jsonSchemaModel.Items.Count))
					{
						this.RaiseError(JsonValidatingReader.getString_1(107347080).FormatWith(CultureInfo.InvariantCulture, this._currentScope.ArrayItemCount), jsonSchemaModel);
					}
				}
			}
		}

		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			if (!this.TestType(schema, JsonSchemaType.Float))
			{
				return;
			}
			this.ValidateNotDisallowed(schema);
			double num = Convert.ToDouble(this._reader.Value, CultureInfo.InvariantCulture);
			if (schema.Maximum != null)
			{
				double num2 = num;
				double? num3 = schema.Maximum;
				if (num2 > num3.GetValueOrDefault() & num3 != null)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107346943).FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.ExclusiveMaximum)
				{
					double num4 = num;
					num3 = schema.Maximum;
					if (num4 == num3.GetValueOrDefault() & num3 != null)
					{
						this.RaiseError(JsonValidatingReader.getString_1(107346922).FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
					}
				}
			}
			if (schema.Minimum != null)
			{
				double num5 = num;
				double? num3 = schema.Minimum;
				if (num5 < num3.GetValueOrDefault() & num3 != null)
				{
					this.RaiseError(JsonValidatingReader.getString_1(107346317).FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.ExclusiveMinimum)
				{
					double num6 = num;
					num3 = schema.Minimum;
					if (num6 == num3.GetValueOrDefault() & num3 != null)
					{
						this.RaiseError(JsonValidatingReader.getString_1(107346256).FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
					}
				}
			}
			if (schema.DivisibleBy != null && !JsonValidatingReader.IsZero(JsonValidatingReader.FloatingPointRemainder(num, schema.DivisibleBy.GetValueOrDefault())))
			{
				this.RaiseError(JsonValidatingReader.getString_1(107346131).FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.DivisibleBy), schema);
			}
		}

		private static double FloatingPointRemainder(double dividend, double divisor)
		{
			return dividend - Math.Floor(dividend / divisor) * divisor;
		}

		private static bool IsZero(double value)
		{
			return Math.Abs(value) < 4.440892098500626E-15;
		}

		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(this._reader.Value, CultureInfo.InvariantCulture);
			if (this._currentScope.RequiredProperties.ContainsKey(text))
			{
				this._currentScope.RequiredProperties[text] = true;
			}
			if (!schema.AllowAdditionalProperties && !this.IsPropertyDefinied(schema, text))
			{
				this.RaiseError(JsonValidatingReader.getString_1(107346074).FormatWith(CultureInfo.InvariantCulture, text), schema);
			}
			this._currentScope.CurrentPropertyName = text;
		}

		private bool IsPropertyDefinied(JsonSchemaModel schema, string propertyName)
		{
			if (schema.Properties != null && schema.Properties.ContainsKey(propertyName))
			{
				return true;
			}
			if (schema.PatternProperties != null)
			{
				using (IEnumerator<string> enumerator = schema.PatternProperties.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string pattern = enumerator.Current;
						if (Regex.IsMatch(propertyName, pattern))
						{
							return true;
						}
					}
					return false;
				}
				bool result;
				return result;
			}
			return false;
		}

		private bool ValidateArray(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Array);
		}

		private bool ValidateObject(JsonSchemaModel schema)
		{
			return schema == null || this.TestType(schema, JsonSchemaType.Object);
		}

		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(new JsonSchemaType?(currentSchema.Type), currentType))
			{
				this.RaiseError(JsonValidatingReader.getString_1(107346465).FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo;
			return (jsonLineInfo = (this._reader as IJsonLineInfo)) != null && jsonLineInfo.HasLineInfo();
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo;
				if ((jsonLineInfo = (this._reader as IJsonLineInfo)) == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo;
				if ((jsonLineInfo = (this._reader as IJsonLineInfo)) == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JsonValidatingReader()
		{
			Strings.CreateGetStringDelegate(typeof(JsonValidatingReader));
			JsonValidatingReader.EmptySchemaList = new List<JsonSchemaModel>();
		}

		private readonly JsonReader _reader;

		private readonly Stack<JsonValidatingReader.SchemaScope> _stack;

		private JsonSchema _schema;

		private JsonSchemaModel _model;

		private JsonValidatingReader.SchemaScope _currentScope;

		private static readonly IList<JsonSchemaModel> EmptySchemaList;

		[NonSerialized]
		internal static GetString getString_1;

		private sealed class SchemaScope
		{
			public string CurrentPropertyName { get; set; }

			public int ArrayItemCount { get; set; }

			public bool IsUniqueArray { get; }

			public IList<JToken> UniqueArrayItems { get; }

			public JTokenWriter CurrentItemWriter { get; set; }

			public IList<JsonSchemaModel> Schemas
			{
				get
				{
					return this._schemas;
				}
			}

			public Dictionary<string, bool> RequiredProperties
			{
				get
				{
					return this._requiredProperties;
				}
			}

			public JTokenType TokenType
			{
				get
				{
					return this._tokenType;
				}
			}

			public SchemaScope(JTokenType tokenType, IList<JsonSchemaModel> schemas)
			{
				this._tokenType = tokenType;
				this._schemas = schemas;
				this._requiredProperties = schemas.SelectMany(new Func<JsonSchemaModel, IEnumerable<string>>(this.GetRequiredProperties)).Distinct<string>().ToDictionary((string p) => p, (string p) => false);
				if (tokenType == JTokenType.Array)
				{
					if (schemas.Any((JsonSchemaModel s) => s.UniqueItems))
					{
						this.IsUniqueArray = 1;
						this.UniqueArrayItems = new List<JToken>();
					}
				}
			}

			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				if (((schema != null) ? schema.Properties : null) == null)
				{
					return Enumerable.Empty<string>();
				}
				return from p in schema.Properties
				where p.Value.Required
				select p.Key;
			}

			private readonly JTokenType _tokenType;

			private readonly IList<JsonSchemaModel> _schemas;

			private readonly Dictionary<string, bool> _requiredProperties;
		}
	}
}
