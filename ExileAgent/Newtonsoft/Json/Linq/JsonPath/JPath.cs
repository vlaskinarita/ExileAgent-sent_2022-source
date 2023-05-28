using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class JPath
	{
		public List<PathFilter> Filters { get; }

		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, JPath.getString_0(107291375));
			this._expression = expression;
			this.Filters = new List<PathFilter>();
			this.ParseMain();
		}

		private void ParseMain()
		{
			int currentIndex = this._currentIndex;
			this.EatWhitespace();
			if (this._expression.Length == this._currentIndex)
			{
				return;
			}
			if (this._expression[this._currentIndex] == '$')
			{
				if (this._expression.Length == 1)
				{
					return;
				}
				char c = this._expression[this._currentIndex + 1];
				if (c == '.' || c == '[')
				{
					this._currentIndex++;
					currentIndex = this._currentIndex;
				}
			}
			if (!this.ParsePath(this.Filters, currentIndex, false))
			{
				int currentIndex2 = this._currentIndex;
				this.EatWhitespace();
				if (this._currentIndex < this._expression.Length)
				{
					throw new JsonException(JPath.getString_0(107291390) + this._expression[currentIndex2].ToString());
				}
			}
		}

		private bool ParsePath(List<PathFilter> filters, int currentPartStartIndex, bool query)
		{
			bool scan = false;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			while (this._currentIndex < this._expression.Length && !flag3)
			{
				char c = this._expression[this._currentIndex];
				if (c <= ')')
				{
					if (c != ' ')
					{
						if (c == '(')
						{
							goto IL_C3;
						}
						if (c == ')')
						{
							goto IL_BC;
						}
					}
					else
					{
						if (this._currentIndex < this._expression.Length)
						{
							flag3 = true;
							continue;
						}
						continue;
					}
				}
				else
				{
					if (c == '.')
					{
						if (this._currentIndex > currentPartStartIndex)
						{
							string text = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex);
							if (text == JPath.getString_0(107446570))
							{
								text = null;
							}
							filters.Add(JPath.CreatePathFilter(text, scan));
							scan = false;
						}
						if (this._currentIndex + 1 < this._expression.Length && this._expression[this._currentIndex + 1] == '.')
						{
							scan = true;
							this._currentIndex++;
						}
						this._currentIndex++;
						currentPartStartIndex = this._currentIndex;
						flag = false;
						flag2 = true;
						continue;
					}
					if (c == '[')
					{
						goto IL_C3;
					}
					if (c == ']')
					{
						goto IL_BC;
					}
				}
				if (query && (c == '=' || c == '<' || c == '!' || c == '>' || c == '|' || c == '&'))
				{
					flag3 = true;
					continue;
				}
				if (!flag)
				{
					this._currentIndex++;
					continue;
				}
				throw new JsonException(JPath.getString_0(107291301) + c.ToString());
				IL_BC:
				flag3 = true;
				continue;
				IL_C3:
				if (this._currentIndex > currentPartStartIndex)
				{
					string text2 = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex);
					if (text2 == JPath.getString_0(107446570))
					{
						text2 = null;
					}
					filters.Add(JPath.CreatePathFilter(text2, scan));
					scan = false;
				}
				filters.Add(this.ParseIndexer(c, scan));
				this._currentIndex++;
				currentPartStartIndex = this._currentIndex;
				flag = true;
				flag2 = false;
			}
			bool flag4 = this._currentIndex == this._expression.Length;
			if (this._currentIndex > currentPartStartIndex)
			{
				string text3 = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex).TrimEnd(new char[0]);
				if (text3 == JPath.getString_0(107446570))
				{
					text3 = null;
				}
				filters.Add(JPath.CreatePathFilter(text3, scan));
			}
			else if (flag2 && (flag4 || query))
			{
				throw new JsonException(JPath.getString_0(107291244));
			}
			return flag4;
		}

		private static PathFilter CreatePathFilter(string member, bool scan)
		{
			if (!scan)
			{
				return new FieldFilter
				{
					Name = member
				};
			}
			return new ScanFilter
			{
				Name = member
			};
		}

		private PathFilter ParseIndexer(char indexerOpenChar, bool scan)
		{
			this._currentIndex++;
			char indexerCloseChar = (indexerOpenChar == '[') ? ']' : ')';
			this.EnsureLength(JPath.getString_0(107290715));
			this.EatWhitespace();
			if (this._expression[this._currentIndex] == '\'')
			{
				return this.ParseQuotedField(indexerCloseChar, scan);
			}
			if (this._expression[this._currentIndex] == '?')
			{
				return this.ParseQuery(indexerCloseChar, scan);
			}
			return this.ParseArrayIndexer(indexerCloseChar);
		}

		private PathFilter ParseArrayIndexer(char indexerCloseChar)
		{
			int currentIndex = this._currentIndex;
			int? num = null;
			List<int> list = null;
			int num2 = 0;
			int? start = null;
			int? end = null;
			int? step = null;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c == ' ')
				{
					num = new int?(this._currentIndex);
					this.EatWhitespace();
				}
				else if (c == indexerCloseChar)
				{
					int num3 = (num ?? this._currentIndex) - currentIndex;
					if (list != null)
					{
						if (num3 == 0)
						{
							throw new JsonException(JPath.getString_0(107290642));
						}
						int item = Convert.ToInt32(this._expression.Substring(currentIndex, num3), CultureInfo.InvariantCulture);
						list.Add(item);
						return new ArrayMultipleIndexFilter
						{
							Indexes = list
						};
					}
					else
					{
						if (num2 > 0)
						{
							if (num3 > 0)
							{
								int value = Convert.ToInt32(this._expression.Substring(currentIndex, num3), CultureInfo.InvariantCulture);
								if (num2 == 1)
								{
									end = new int?(value);
								}
								else
								{
									step = new int?(value);
								}
							}
							return new ArraySliceFilter
							{
								Start = start,
								End = end,
								Step = step
							};
						}
						if (num3 == 0)
						{
							throw new JsonException(JPath.getString_0(107290642));
						}
						int value2 = Convert.ToInt32(this._expression.Substring(currentIndex, num3), CultureInfo.InvariantCulture);
						return new ArrayIndexFilter
						{
							Index = new int?(value2)
						};
					}
				}
				else if (c == ',')
				{
					int num4 = (num ?? this._currentIndex) - currentIndex;
					if (num4 == 0)
					{
						throw new JsonException(JPath.getString_0(107290642));
					}
					if (list == null)
					{
						list = new List<int>();
					}
					string value3 = this._expression.Substring(currentIndex, num4);
					list.Add(Convert.ToInt32(value3, CultureInfo.InvariantCulture));
					this._currentIndex++;
					this.EatWhitespace();
					currentIndex = this._currentIndex;
					num = null;
				}
				else if (c == '*')
				{
					this._currentIndex++;
					this.EnsureLength(JPath.getString_0(107290715));
					this.EatWhitespace();
					if (this._expression[this._currentIndex] != indexerCloseChar)
					{
						throw new JsonException(JPath.getString_0(107290645) + c.ToString());
					}
					return new ArrayIndexFilter();
				}
				else if (c == ':')
				{
					int num5 = (num ?? this._currentIndex) - currentIndex;
					if (num5 > 0)
					{
						int value4 = Convert.ToInt32(this._expression.Substring(currentIndex, num5), CultureInfo.InvariantCulture);
						if (num2 == 0)
						{
							start = new int?(value4);
						}
						else if (num2 == 1)
						{
							end = new int?(value4);
						}
						else
						{
							step = new int?(value4);
						}
					}
					num2++;
					this._currentIndex++;
					this.EatWhitespace();
					currentIndex = this._currentIndex;
					num = null;
				}
				else
				{
					if (!char.IsDigit(c) && c != '-')
					{
						throw new JsonException(JPath.getString_0(107290645) + c.ToString());
					}
					if (num != null)
					{
						throw new JsonException(JPath.getString_0(107290645) + c.ToString());
					}
					this._currentIndex++;
				}
			}
			throw new JsonException(JPath.getString_0(107290715));
		}

		private void EatWhitespace()
		{
			while (this._currentIndex < this._expression.Length && this._expression[this._currentIndex] == ' ')
			{
				this._currentIndex++;
			}
		}

		private PathFilter ParseQuery(char indexerCloseChar, bool scan)
		{
			this._currentIndex++;
			this.EnsureLength(JPath.getString_0(107290715));
			if (this._expression[this._currentIndex] != '(')
			{
				throw new JsonException(JPath.getString_0(107290645) + this._expression[this._currentIndex].ToString());
			}
			this._currentIndex++;
			QueryExpression expression = this.ParseExpression();
			this._currentIndex++;
			this.EnsureLength(JPath.getString_0(107290715));
			this.EatWhitespace();
			if (this._expression[this._currentIndex] != indexerCloseChar)
			{
				throw new JsonException(JPath.getString_0(107290645) + this._expression[this._currentIndex].ToString());
			}
			if (!scan)
			{
				return new QueryFilter
				{
					Expression = expression
				};
			}
			return new QueryScanFilter
			{
				Expression = expression
			};
		}

		private bool TryParseExpression(out List<PathFilter> expressionPath)
		{
			if (this._expression[this._currentIndex] == '$')
			{
				expressionPath = new List<PathFilter>();
				expressionPath.Add(RootFilter.Instance);
			}
			else
			{
				if (this._expression[this._currentIndex] != '@')
				{
					expressionPath = null;
					return false;
				}
				expressionPath = new List<PathFilter>();
			}
			this._currentIndex++;
			if (this.ParsePath(expressionPath, this._currentIndex, true))
			{
				throw new JsonException(JPath.getString_0(107290544));
			}
			return true;
		}

		private JsonException CreateUnexpectedCharacterException()
		{
			return new JsonException(JPath.getString_0(107290507) + this._expression[this._currentIndex].ToString());
		}

		private object ParseSide()
		{
			this.EatWhitespace();
			List<PathFilter> result;
			if (this.TryParseExpression(out result))
			{
				this.EatWhitespace();
				this.EnsureLength(JPath.getString_0(107290544));
				return result;
			}
			object value;
			if (!this.TryParseValue(out value))
			{
				throw this.CreateUnexpectedCharacterException();
			}
			this.EatWhitespace();
			this.EnsureLength(JPath.getString_0(107290544));
			return new JValue(value);
		}

		private QueryExpression ParseExpression()
		{
			QueryExpression queryExpression = null;
			CompositeExpression compositeExpression = null;
			while (this._currentIndex < this._expression.Length)
			{
				object left = this.ParseSide();
				object right = null;
				if (this._expression[this._currentIndex] == ')' || this._expression[this._currentIndex] == '|')
				{
					goto IL_64;
				}
				if (this._expression[this._currentIndex] == '&')
				{
					goto IL_64;
				}
				QueryOperator @operator = this.ParseOperator();
				right = this.ParseSide();
				IL_67:
				BooleanQueryExpression booleanQueryExpression = new BooleanQueryExpression
				{
					Left = left,
					Operator = @operator,
					Right = right
				};
				if (this._expression[this._currentIndex] == ')')
				{
					if (compositeExpression != null)
					{
						compositeExpression.Expressions.Add(booleanQueryExpression);
						return queryExpression;
					}
					return booleanQueryExpression;
				}
				else
				{
					if (this._expression[this._currentIndex] == '&')
					{
						if (!this.Match(JPath.getString_0(107290954)))
						{
							throw this.CreateUnexpectedCharacterException();
						}
						if (compositeExpression == null || compositeExpression.Operator != QueryOperator.And)
						{
							CompositeExpression compositeExpression2 = new CompositeExpression
							{
								Operator = QueryOperator.And
							};
							if (compositeExpression != null)
							{
								compositeExpression.Expressions.Add(compositeExpression2);
							}
							compositeExpression = compositeExpression2;
							if (queryExpression == null)
							{
								queryExpression = compositeExpression;
							}
						}
						compositeExpression.Expressions.Add(booleanQueryExpression);
					}
					if (this._expression[this._currentIndex] != '|')
					{
						continue;
					}
					if (this.Match(JPath.getString_0(107290949)))
					{
						if (compositeExpression == null || compositeExpression.Operator != QueryOperator.Or)
						{
							CompositeExpression compositeExpression3 = new CompositeExpression
							{
								Operator = QueryOperator.Or
							};
							if (compositeExpression != null)
							{
								compositeExpression.Expressions.Add(compositeExpression3);
							}
							compositeExpression = compositeExpression3;
							if (queryExpression == null)
							{
								queryExpression = compositeExpression;
							}
						}
						compositeExpression.Expressions.Add(booleanQueryExpression);
						continue;
					}
					throw this.CreateUnexpectedCharacterException();
				}
				IL_64:
				@operator = QueryOperator.Exists;
				goto IL_67;
			}
			throw new JsonException(JPath.getString_0(107290544));
		}

		private bool TryParseValue(out object value)
		{
			char c = this._expression[this._currentIndex];
			if (c == '\'')
			{
				value = this.ReadQuotedString();
				return true;
			}
			if (!char.IsDigit(c))
			{
				if (c != '-')
				{
					if (c == 't')
					{
						if (this.Match(JPath.getString_0(107457279)))
						{
							value = true;
							return true;
						}
						goto IL_11D;
					}
					else if (c == 'f')
					{
						if (this.Match(JPath.getString_0(107353167)))
						{
							value = false;
							return true;
						}
						goto IL_11D;
					}
					else if (c == 'n')
					{
						if (this.Match(JPath.getString_0(107386849)))
						{
							value = null;
							return true;
						}
						goto IL_11D;
					}
					else
					{
						if (c == '/')
						{
							value = this.ReadRegexString();
							return true;
						}
						goto IL_11D;
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(c);
			this._currentIndex++;
			while (this._currentIndex < this._expression.Length)
			{
				c = this._expression[this._currentIndex];
				if (c != ' ')
				{
					if (c != ')')
					{
						stringBuilder.Append(c);
						this._currentIndex++;
						continue;
					}
				}
				string text = stringBuilder.ToString();
				if (text.IndexOfAny(JPath.FloatCharacters) != -1)
				{
					double num;
					bool result = double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num);
					value = num;
					return result;
				}
				long num2;
				bool result2 = long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2);
				value = num2;
				return result2;
			}
			IL_11D:
			value = null;
			return false;
		}

		private string ReadQuotedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._currentIndex++;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c == '\\' && this._currentIndex + 1 < this._expression.Length)
				{
					this._currentIndex++;
					c = this._expression[this._currentIndex];
					char value;
					if (c <= '\\')
					{
						if (c <= '\'')
						{
							if (c != '"' && c != '\'')
							{
								goto IL_132;
							}
						}
						else if (c != '/' && c != '\\')
						{
							goto IL_132;
						}
						value = c;
					}
					else if (c <= 'f')
					{
						if (c != 'b')
						{
							if (c != 'f')
							{
								goto IL_132;
							}
							value = '\f';
						}
						else
						{
							value = '\b';
						}
					}
					else if (c != 'n')
					{
						if (c != 'r')
						{
							if (c != 't')
							{
								goto IL_132;
							}
							value = '\t';
						}
						else
						{
							value = '\r';
						}
					}
					else
					{
						value = '\n';
					}
					stringBuilder.Append(value);
					this._currentIndex++;
					continue;
					IL_132:
					throw new JsonException(JPath.getString_0(107290976) + c.ToString());
				}
				if (c == '\'')
				{
					this._currentIndex++;
					return stringBuilder.ToString();
				}
				this._currentIndex++;
				stringBuilder.Append(c);
			}
			throw new JsonException(JPath.getString_0(107290939));
		}

		private string ReadRegexString()
		{
			int currentIndex = this._currentIndex;
			this._currentIndex++;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c == '\\' && this._currentIndex + 1 < this._expression.Length)
				{
					this._currentIndex += 2;
				}
				else
				{
					if (c == '/')
					{
						this._currentIndex++;
						while (this._currentIndex < this._expression.Length)
						{
							c = this._expression[this._currentIndex];
							if (!char.IsLetter(c))
							{
								break;
							}
							this._currentIndex++;
						}
						return this._expression.Substring(currentIndex, this._currentIndex - currentIndex);
					}
					this._currentIndex++;
				}
			}
			throw new JsonException(JPath.getString_0(107290862));
		}

		private bool Match(string s)
		{
			int num = this._currentIndex;
			foreach (char c in s)
			{
				if (num >= this._expression.Length || this._expression[num] != c)
				{
					return false;
				}
				num++;
			}
			this._currentIndex = num;
			return true;
		}

		private QueryOperator ParseOperator()
		{
			if (this._currentIndex + 1 >= this._expression.Length)
			{
				throw new JsonException(JPath.getString_0(107290544));
			}
			if (this.Match(JPath.getString_0(107290821)))
			{
				return QueryOperator.StrictEquals;
			}
			if (this.Match(JPath.getString_0(107290848)))
			{
				return QueryOperator.Equals;
			}
			if (this.Match(JPath.getString_0(107290843)))
			{
				return QueryOperator.RegexEquals;
			}
			if (this.Match(JPath.getString_0(107290838)))
			{
				return QueryOperator.StrictNotEquals;
			}
			if (this.Match(JPath.getString_0(107290801)) || this.Match(JPath.getString_0(107290796)))
			{
				return QueryOperator.NotEquals;
			}
			if (this.Match(JPath.getString_0(107290791)))
			{
				return QueryOperator.LessThanOrEquals;
			}
			if (this.Match(JPath.getString_0(107248096)))
			{
				return QueryOperator.LessThan;
			}
			if (this.Match(JPath.getString_0(107290818)))
			{
				return QueryOperator.GreaterThanOrEquals;
			}
			if (this.Match(JPath.getString_0(107412323)))
			{
				return QueryOperator.GreaterThan;
			}
			throw new JsonException(JPath.getString_0(107290813));
		}

		private PathFilter ParseQuotedField(char indexerCloseChar, bool scan)
		{
			List<string> list = null;
			while (this._currentIndex < this._expression.Length)
			{
				string text = this.ReadQuotedString();
				this.EatWhitespace();
				this.EnsureLength(JPath.getString_0(107290715));
				if (this._expression[this._currentIndex] == indexerCloseChar)
				{
					if (list == null)
					{
						return JPath.CreatePathFilter(text, scan);
					}
					list.Add(text);
					if (!scan)
					{
						return new FieldMultipleFilter
						{
							Names = list
						};
					}
					return new ScanMultipleFilter
					{
						Names = list
					};
				}
				else
				{
					if (this._expression[this._currentIndex] != ',')
					{
						throw new JsonException(JPath.getString_0(107290645) + this._expression[this._currentIndex].ToString());
					}
					this._currentIndex++;
					this.EatWhitespace();
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(text);
				}
			}
			throw new JsonException(JPath.getString_0(107290715));
		}

		private void EnsureLength(string message)
		{
			if (this._currentIndex >= this._expression.Length)
			{
				throw new JsonException(message);
			}
		}

		internal IEnumerable<JToken> Evaluate(JToken root, JToken t, bool errorWhenNoMatch)
		{
			return JPath.Evaluate(this.Filters, root, t, errorWhenNoMatch);
		}

		internal static IEnumerable<JToken> Evaluate(List<PathFilter> filters, JToken root, JToken t, bool errorWhenNoMatch)
		{
			IEnumerable<JToken> enumerable = new JToken[]
			{
				t
			};
			foreach (PathFilter pathFilter in filters)
			{
				enumerable = pathFilter.ExecuteFilter(root, enumerable, errorWhenNoMatch);
			}
			return enumerable;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JPath()
		{
			Strings.CreateGetStringDelegate(typeof(JPath));
			JPath.FloatCharacters = new char[]
			{
				'.',
				'E',
				'e'
			};
		}

		private static readonly char[] FloatCharacters;

		private readonly string _expression;

		private int _currentIndex;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
