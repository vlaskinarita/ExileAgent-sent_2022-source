using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class BooleanQueryExpression : QueryExpression
	{
		public object Left { get; set; }

		public object Right { get; set; }

		private IEnumerable<JToken> GetResult(JToken root, JToken t, object o)
		{
			JToken jtoken;
			if ((jtoken = (o as JToken)) != null)
			{
				return new JToken[]
				{
					jtoken
				};
			}
			List<PathFilter> filters;
			if ((filters = (o as List<PathFilter>)) != null)
			{
				return JPath.Evaluate(filters, root, t, false);
			}
			return CollectionUtils.ArrayEmpty<JToken>();
		}

		public override bool IsMatch(JToken root, JToken t)
		{
			if (base.Operator == QueryOperator.Exists)
			{
				return this.GetResult(root, t, this.Left).Any<JToken>();
			}
			using (IEnumerator<JToken> enumerator = this.GetResult(root, t, this.Left).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IEnumerable<JToken> result = this.GetResult(root, t, this.Right);
					ICollection<JToken> collection = (result as ICollection<JToken>) ?? result.ToList<JToken>();
					do
					{
						JToken leftResult = enumerator.Current;
						foreach (JToken rightResult in collection)
						{
							if (this.MatchTokens(leftResult, rightResult))
							{
								return true;
							}
						}
					}
					while (enumerator.MoveNext());
				}
			}
			return false;
		}

		private bool MatchTokens(JToken leftResult, JToken rightResult)
		{
			JValue jvalue;
			JValue jvalue2;
			if ((jvalue = (leftResult as JValue)) != null && (jvalue2 = (rightResult as JValue)) != null)
			{
				switch (base.Operator)
				{
				case QueryOperator.Equals:
					if (BooleanQueryExpression.EqualsWithStringCoercion(jvalue, jvalue2))
					{
						return true;
					}
					break;
				case QueryOperator.NotEquals:
					if (!BooleanQueryExpression.EqualsWithStringCoercion(jvalue, jvalue2))
					{
						return true;
					}
					break;
				case QueryOperator.Exists:
					return true;
				case QueryOperator.LessThan:
					if (jvalue.CompareTo(jvalue2) < 0)
					{
						return true;
					}
					break;
				case QueryOperator.LessThanOrEquals:
					if (jvalue.CompareTo(jvalue2) <= 0)
					{
						return true;
					}
					break;
				case QueryOperator.GreaterThan:
					if (jvalue.CompareTo(jvalue2) > 0)
					{
						return true;
					}
					break;
				case QueryOperator.GreaterThanOrEquals:
					if (jvalue.CompareTo(jvalue2) >= 0)
					{
						return true;
					}
					break;
				case QueryOperator.RegexEquals:
					if (BooleanQueryExpression.RegexEquals(jvalue, jvalue2))
					{
						return true;
					}
					break;
				case QueryOperator.StrictEquals:
					if (BooleanQueryExpression.EqualsWithStrictMatch(jvalue, jvalue2))
					{
						return true;
					}
					break;
				case QueryOperator.StrictNotEquals:
					if (!BooleanQueryExpression.EqualsWithStrictMatch(jvalue, jvalue2))
					{
						return true;
					}
					break;
				}
			}
			else
			{
				QueryOperator @operator = base.Operator;
				if (@operator - QueryOperator.NotEquals <= 1)
				{
					return true;
				}
			}
			return false;
		}

		private static bool RegexEquals(JValue input, JValue pattern)
		{
			if (input.Type == JTokenType.String)
			{
				if (pattern.Type == JTokenType.String)
				{
					string text = (string)pattern.Value;
					int num = text.LastIndexOf('/');
					string pattern2 = text.Substring(1, num - 1);
					string optionsText = text.Substring(num + 1);
					return Regex.IsMatch((string)input.Value, pattern2, MiscellaneousUtils.GetRegexOptions(optionsText));
				}
			}
			return false;
		}

		internal static bool EqualsWithStringCoercion(JValue value, JValue queryValue)
		{
			if (value.Equals(queryValue))
			{
				return true;
			}
			if ((value.Type == JTokenType.Integer && queryValue.Type == JTokenType.Float) || (value.Type == JTokenType.Float && queryValue.Type == JTokenType.Integer))
			{
				return JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			if (queryValue.Type != JTokenType.String)
			{
				return false;
			}
			string b = (string)queryValue.Value;
			string a;
			switch (value.Type)
			{
			case JTokenType.Date:
				using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
				{
					object value2;
					if ((value2 = value.Value) is DateTimeOffset)
					{
						DateTimeOffset value3 = (DateTimeOffset)value2;
						DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value3, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					else
					{
						DateTimeUtils.WriteDateTimeString(stringWriter, (DateTime)value.Value, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					a = stringWriter.ToString();
					goto IL_11C;
				}
				break;
			case JTokenType.Bytes:
				a = Convert.ToBase64String((byte[])value.Value);
				goto IL_11C;
			case JTokenType.Guid:
			case JTokenType.TimeSpan:
				a = value.Value.ToString();
				goto IL_11C;
			case JTokenType.Uri:
				a = ((Uri)value.Value).OriginalString;
				goto IL_11C;
			}
			return false;
			IL_11C:
			return string.Equals(a, b, StringComparison.Ordinal);
		}

		internal static bool EqualsWithStrictMatch(JValue value, JValue queryValue)
		{
			if ((value.Type == JTokenType.Integer && queryValue.Type == JTokenType.Float) || (value.Type == JTokenType.Float && queryValue.Type == JTokenType.Integer))
			{
				return JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			return value.Type == queryValue.Type && value.Equals(queryValue);
		}
	}
}
