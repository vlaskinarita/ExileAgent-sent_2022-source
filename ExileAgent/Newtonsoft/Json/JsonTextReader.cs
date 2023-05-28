using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	public sealed class JsonTextReader : JsonReader, IJsonLineInfo
	{
		public override Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsync(cancellationToken);
			}
			return this.DoReadAsync(cancellationToken);
		}

		internal Task<bool> DoReadAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			Task<bool> task;
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_7B;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_64;
				case JsonReader.State.PostValue:
					task = this.ParsePostValueAsync(false, cancellationToken);
					if (!task.IsCompletedSucessfully())
					{
						goto IL_72;
					}
					if (!task.Result)
					{
						continue;
					}
					goto IL_6C;
				case JsonReader.State.Finished:
					goto IL_AE;
				}
				break;
			}
			goto IL_83;
			IL_64:
			return this.ParseObjectAsync(cancellationToken);
			IL_6C:
			return AsyncUtils.True;
			IL_72:
			return this.DoReadAsync(task, cancellationToken);
			IL_7B:
			return this.ParseValueAsync(cancellationToken);
			IL_83:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349309).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			IL_AE:
			return this.ReadFromFinishedAsync(cancellationToken);
		}

		private async Task<bool> DoReadAsync(Task<bool> task, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (configuredTaskAwaiter.GetResult())
			{
				result = true;
			}
			else
			{
				configuredTaskAwaiter = this.DoReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				result = configuredTaskAwaiter.GetResult();
			}
			return result;
		}

		private async Task<bool> ParsePostValueAsync(bool ignoreComments, CancellationToken cancellationToken)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_F5;
							case '\r':
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									await configuredTaskAwaiter;
									configuredTaskAwaiter = configuredTaskAwaiter2;
									configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter.GetResult();
								continue;
							}
							default:
								goto IL_F5;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								await configuredTaskAwaiter3;
								ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
								configuredTaskAwaiter3 = configuredTaskAwaiter4;
								configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter3.GetResult() == 0)
							{
								goto Block_11;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_F5;
						}
						goto IL_25D;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_2A5;
					}
					if (c == '/')
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ParseCommentAsync(!ignoreComments, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						if (!ignoreComments)
						{
							break;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_31A;
					}
					if (c == '}')
					{
						goto IL_300;
					}
				}
				IL_F5:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_2C0;
				}
				this._charPos++;
			}
			return true;
			Block_11:
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_25D:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_2A5:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_2C0:
			if (base.SupportMultipleContent && this.Depth == 0)
			{
				base.SetStateBasedOnCurrent();
				return false;
			}
			throw JsonReaderException.Create(this, JsonTextReader.<ParsePostValueAsync>d__4.getString_0(107348826).FormatWith(CultureInfo.InvariantCulture, c));
			IL_300:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_31A:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
		}

		private async Task<bool> ReadFromFinishedAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (configuredTaskAwaiter.GetResult())
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				if (this._isEndOfFile)
				{
					base.SetToken(JsonToken.None);
					result = false;
				}
				else
				{
					if (this._chars[this._charPos] != '/')
					{
						throw JsonReaderException.Create(this, JsonTextReader.<ReadFromFinishedAsync>d__5.getString_0(107349297).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
					}
					configuredTaskAwaiter3 = this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						await configuredTaskAwaiter3;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter3.GetResult();
					result = true;
				}
			}
			else
			{
				base.SetToken(JsonToken.None);
				result = false;
			}
			return result;
		}

		private Task<int> ReadDataAsync(bool append, CancellationToken cancellationToken)
		{
			return this.ReadDataAsync(append, 0, cancellationToken);
		}

		private async Task<int> ReadDataAsync(bool append, int charsRequired, CancellationToken cancellationToken)
		{
			int result;
			if (this._isEndOfFile)
			{
				result = 0;
			}
			else
			{
				this.PrepareBufferForReadData(append, charsRequired);
				int num = await this._reader.ReadAsync(this._chars, this._charsUsed, this._chars.Length - this._charsUsed - 1, cancellationToken).ConfigureAwait(false);
				this._charsUsed += num;
				if (num == 0)
				{
					this._isEndOfFile = true;
				}
				this._chars[this._charsUsed] = '\0';
				result = num;
			}
			return result;
		}

		private async Task<bool> ParseValueAsync(CancellationToken cancellationToken)
		{
			char c;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'N')
				{
					if (c <= ' ')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_29D;
							case '\r':
								configuredTaskAwaiter = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									await configuredTaskAwaiter;
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
									configuredTaskAwaiter = configuredTaskAwaiter2;
									configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter.GetResult();
								continue;
							default:
								if (c != ' ')
								{
									goto IL_29D;
								}
								break;
							}
							this._charPos++;
							continue;
						}
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter3.GetResult() == 0)
						{
							break;
						}
						continue;
					}
					else if (c <= '/')
					{
						if (c == '"')
						{
							goto IL_5F1;
						}
						switch (c)
						{
						case '\'':
							goto IL_5F1;
						case ')':
							goto IL_44E;
						case ',':
							goto IL_46B;
						case '-':
							goto IL_47A;
						case '/':
							goto IL_596;
						}
					}
					else
					{
						if (c == 'I')
						{
							goto IL_6A5;
						}
						if (c == 'N')
						{
							goto IL_64B;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == '[')
					{
						goto IL_774;
					}
					if (c == ']')
					{
						goto IL_757;
					}
					if (c == 'f')
					{
						goto IL_6FF;
					}
				}
				else if (c <= 't')
				{
					if (c == 'n')
					{
						goto IL_7E8;
					}
					if (c == 't')
					{
						goto IL_790;
					}
				}
				else
				{
					if (c == 'u')
					{
						goto IL_9B9;
					}
					if (c == '{')
					{
						goto IL_99D;
					}
				}
				IL_29D:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_931;
				}
				this._charPos++;
			}
			return false;
			IL_44E:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_46B:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_47A:
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter7;
			if (configuredTaskAwaiter5.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				configuredTaskAwaiter7 = this.ParseNumberNegativeInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter7.IsCompleted)
				{
					await configuredTaskAwaiter7;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter8;
					configuredTaskAwaiter7 = configuredTaskAwaiter8;
					configuredTaskAwaiter8 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter7.GetResult();
			}
			else
			{
				configuredTaskAwaiter = this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
			return true;
			IL_596:
			configuredTaskAwaiter = this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
			IL_5F1:
			configuredTaskAwaiter = this.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
			IL_64B:
			configuredTaskAwaiter7 = this.ParseNumberNaNAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter7.IsCompleted)
			{
				await configuredTaskAwaiter7;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter8;
				configuredTaskAwaiter7 = configuredTaskAwaiter8;
				configuredTaskAwaiter8 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter7.GetResult();
			return true;
			IL_6A5:
			configuredTaskAwaiter7 = this.ParseNumberPositiveInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter7.IsCompleted)
			{
				await configuredTaskAwaiter7;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter8;
				configuredTaskAwaiter7 = configuredTaskAwaiter8;
				configuredTaskAwaiter8 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter7.GetResult();
			return true;
			IL_6FF:
			configuredTaskAwaiter = this.ParseFalseAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
			IL_757:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_774:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_790:
			configuredTaskAwaiter = this.ParseTrueAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
			IL_7E8:
			configuredTaskAwaiter5 = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter5.GetResult())
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 != 'e')
				{
					if (c2 != 'u')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					configuredTaskAwaiter = this.ParseNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				else
				{
					configuredTaskAwaiter = this.ParseConstructorAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter.GetResult();
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_931:
			if (!char.IsNumber(c) && c != '-')
			{
				if (c != '.')
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			configuredTaskAwaiter = this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
			IL_99D:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_9B9:
			configuredTaskAwaiter = this.ParseUndefinedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
		}

		private async Task ReadStringIntoBufferAsync(char quote, CancellationToken cancellationToken)
		{
			int charPos = this._charPos;
			int initialPosition = this._charPos;
			int lastWritePosition = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char[] chars = this._chars;
				int num = charPos;
				charPos = num + 1;
				char c = chars[num];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = charPos - 1;
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									await configuredTaskAwaiter;
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
									configuredTaskAwaiter = configuredTaskAwaiter2;
									configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter.GetResult();
								charPos = this._charPos;
							}
						}
						else
						{
							this._charPos = charPos - 1;
							this.ProcessLineFeed();
							charPos = this._charPos;
						}
					}
					else if (this._charsUsed == charPos - 1)
					{
						num = charPos;
						charPos = num - 1;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter3.GetResult() == 0)
						{
							goto IL_5C4;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = charPos;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = this.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
						if (!configuredTaskAwaiter5.IsCompleted)
						{
							await configuredTaskAwaiter5;
							configuredTaskAwaiter5 = configuredTaskAwaiter6;
							configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (!configuredTaskAwaiter5.GetResult())
						{
							goto IL_61F;
						}
						int escapeStartPos = charPos - 1;
						c2 = this._chars[charPos];
						num = charPos;
						charPos = num + 1;
						char writeChar;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									break;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									break;
								}
								writeChar = '\\';
								goto IL_434;
							}
							writeChar = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									break;
								}
								writeChar = '\f';
							}
							else
							{
								writeChar = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									writeChar = '\r';
									goto IL_434;
								case 't':
									writeChar = '\t';
									goto IL_434;
								case 'u':
								{
									this._charPos = charPos;
									ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter configuredTaskAwaiter7 = this.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
									ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter configuredTaskAwaiter8;
									if (!configuredTaskAwaiter7.IsCompleted)
									{
										await configuredTaskAwaiter7;
										configuredTaskAwaiter7 = configuredTaskAwaiter8;
										configuredTaskAwaiter8 = default(ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter);
									}
									c = configuredTaskAwaiter7.GetResult();
									writeChar = c;
									if (StringUtils.IsLowSurrogate(writeChar))
									{
										writeChar = '�';
									}
									else if (StringUtils.IsHighSurrogate(writeChar))
									{
										bool anotherHighSurrogate;
										do
										{
											anotherHighSurrogate = false;
											configuredTaskAwaiter5 = this.EnsureCharsAsync(2, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
											if (!configuredTaskAwaiter5.IsCompleted)
											{
												await configuredTaskAwaiter5;
												configuredTaskAwaiter5 = configuredTaskAwaiter6;
												configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
											}
											if (configuredTaskAwaiter5.GetResult() && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char highSurrogate = writeChar;
												this._charPos += 2;
												configuredTaskAwaiter7 = this.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
												if (!configuredTaskAwaiter7.IsCompleted)
												{
													await configuredTaskAwaiter7;
													configuredTaskAwaiter7 = configuredTaskAwaiter8;
													configuredTaskAwaiter8 = default(ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter);
												}
												c = configuredTaskAwaiter7.GetResult();
												writeChar = c;
												if (!StringUtils.IsLowSurrogate(writeChar))
												{
													if (StringUtils.IsHighSurrogate(writeChar))
													{
														highSurrogate = '�';
														anotherHighSurrogate = true;
													}
													else
													{
														highSurrogate = '�';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(highSurrogate, lastWritePosition, escapeStartPos);
												lastWritePosition = this._charPos;
											}
											else
											{
												writeChar = '�';
											}
										}
										while (anotherHighSurrogate);
									}
									charPos = this._charPos;
									goto IL_434;
								}
								}
								break;
							}
							writeChar = '\n';
						}
						IL_434:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(writeChar, lastWritePosition, escapeStartPos);
						lastWritePosition = charPos;
					}
				}
				else if (this._chars[charPos - 1] == quote)
				{
					goto Block_24;
				}
			}
			goto IL_64A;
			Block_24:
			this.FinishReadStringIntoBuffer(charPos - 1, initialPosition, lastWritePosition);
			return;
			IL_5C4:
			this._charPos = charPos;
			throw JsonReaderException.Create(this, JsonTextReader.<ReadStringIntoBufferAsync>d__9.getString_0(107349174).FormatWith(CultureInfo.InvariantCulture, quote));
			IL_61F:
			throw JsonReaderException.Create(this, JsonTextReader.<ReadStringIntoBufferAsync>d__9.getString_0(107349174).FormatWith(CultureInfo.InvariantCulture, quote));
			IL_64A:
			this._charPos = charPos;
			throw JsonReaderException.Create(this, JsonTextReader.<ReadStringIntoBufferAsync>d__9.getString_0(107349625).FormatWith(CultureInfo.InvariantCulture, JsonTextReader.<ReadStringIntoBufferAsync>d__9.getString_0(107395524) + c2.ToString()));
		}

		private Task ProcessCarriageReturnAsync(bool append, CancellationToken cancellationToken)
		{
			this._charPos++;
			Task<bool> task = this.EnsureCharsAsync(1, append, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				this.SetNewLine(task.Result);
				return AsyncUtils.CompletedTask;
			}
			return this.ProcessCarriageReturnAsync(task);
		}

		private async Task ProcessCarriageReturnAsync(Task<bool> task)
		{
			bool newLine = await task.ConfigureAwait(false);
			this.SetNewLine(newLine);
		}

		private async Task<char> ParseUnicodeAsync(CancellationToken cancellationToken)
		{
			bool enoughChars = await this.EnsureCharsAsync(4, true, cancellationToken).ConfigureAwait(false);
			return this.ConvertUnicode(enoughChars);
		}

		private Task<bool> EnsureCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			if (this._charPos + relativePosition < this._charsUsed)
			{
				return AsyncUtils.True;
			}
			if (this._isEndOfFile)
			{
				return AsyncUtils.False;
			}
			return this.ReadCharsAsync(relativePosition, append, cancellationToken);
		}

		private async Task<bool> ReadCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			int charsRequired = this._charPos + relativePosition - this._charsUsed + 1;
			for (;;)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(append, charsRequired, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				int result = configuredTaskAwaiter.GetResult();
				if (result == 0)
				{
					break;
				}
				charsRequired -= result;
				if (charsRequired <= 0)
				{
					goto IL_A6;
				}
			}
			return false;
			IL_A6:
			return true;
		}

		private async Task<bool> ParseObjectAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							goto IL_C2;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\r':
							configuredTaskAwaiter = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							}
							configuredTaskAwaiter.GetResult();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter3.GetResult() == 0)
						{
							break;
						}
						continue;
					}
				}
				else
				{
					if (c == ' ')
					{
						goto IL_C2;
					}
					if (c == '/')
					{
						goto IL_254;
					}
					if (c == '}')
					{
						goto IL_237;
					}
				}
				if (char.IsWhiteSpace(c))
				{
					this._charPos++;
					continue;
				}
				goto IL_1EA;
				IL_C2:
				this._charPos++;
			}
			return false;
			IL_1EA:
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = this.ParsePropertyAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			return configuredTaskAwaiter5.GetResult();
			IL_237:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_254:
			configuredTaskAwaiter = this.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			return true;
		}

		private async Task ParseCommentAsync(bool setToken, CancellationToken cancellationToken)
		{
			this._charPos++;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(1, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, JsonTextReader.<ParseCommentAsync>d__16.getString_0(107347881));
			}
			bool singlelineComment;
			if (this._chars[this._charPos] == '*')
			{
				singlelineComment = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, JsonTextReader.<ParseCommentAsync>d__16.getString_0(107347828).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				singlelineComment = true;
			}
			this._charPos++;
			int initialPosition = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (!singlelineComment)
							{
								this.ProcessLineFeed();
								continue;
							}
							goto IL_2F5;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							await configuredTaskAwaiter3;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter3.GetResult() == 0)
						{
							goto Block_13;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (singlelineComment)
						{
							continue;
						}
						configuredTaskAwaiter = this.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos] == '/')
						{
							break;
						}
						continue;
					}
				}
				else
				{
					if (!singlelineComment)
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter5 = this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter5.IsCompleted)
						{
							await configuredTaskAwaiter5;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter6;
							configuredTaskAwaiter5 = configuredTaskAwaiter6;
							configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter5.GetResult();
						continue;
					}
					goto IL_3BE;
				}
				this._charPos++;
			}
			this.EndComment(setToken, initialPosition, this._charPos - 1);
			this._charPos++;
			return;
			Block_13:
			if (!singlelineComment)
			{
				throw JsonReaderException.Create(this, JsonTextReader.<ParseCommentAsync>d__16.getString_0(107347881));
			}
			this.EndComment(setToken, initialPosition, this._charPos);
			return;
			IL_2F5:
			this.EndComment(setToken, initialPosition, this._charPos);
			return;
			IL_3BE:
			this.EndComment(setToken, initialPosition, this._charPos);
		}

		private async Task EatWhitespaceAsync(CancellationToken cancellationToken)
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c != '\0')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != ' ' && !char.IsWhiteSpace(c))
							{
								break;
							}
							this._charPos++;
						}
						else
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							}
							configuredTaskAwaiter.GetResult();
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						await configuredTaskAwaiter3;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter3.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					this._charPos++;
				}
			}
		}

		private async Task ParseStringAsync(char quote, ReadType readType, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this._charPos++;
			this.ShiftBufferIfNeeded();
			await this.ReadStringIntoBufferAsync(quote, cancellationToken).ConfigureAwait(false);
			this.ParseReadString(quote, readType);
		}

		private async Task<bool> MatchValueAsync(string value, CancellationToken cancellationToken)
		{
			bool enoughChars = await this.EnsureCharsAsync(value.Length - 1, true, cancellationToken).ConfigureAwait(false);
			return this.MatchValue(enoughChars, value);
		}

		private async Task<bool> MatchValueWithTrailingSeparatorAsync(string value, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			bool result;
			if (!configuredTaskAwaiter.GetResult())
			{
				result = false;
			}
			else
			{
				configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					result = true;
				}
				else
				{
					result = (this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
				}
			}
			return result;
		}

		private async Task MatchAndSetAsync(string value, JsonToken newToken, object tokenValue, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, JsonTextReader.<MatchAndSetAsync>d__21.getString_0(107316258) + newToken.ToString().ToLowerInvariant() + JsonTextReader.<MatchAndSetAsync>d__21.getString_0(107316205));
			}
			base.SetToken(newToken, tokenValue);
		}

		private Task ParseTrueAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.True, JsonToken.Boolean, true, cancellationToken);
		}

		private Task ParseFalseAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.False, JsonToken.Boolean, false, cancellationToken);
		}

		private Task ParseNullAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.Null, JsonToken.Null, null, cancellationToken);
		}

		private async Task ParseConstructorAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(JsonTextReader.<ParseConstructorAsync>d__25.getString_0(107442221), cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonReaderException.Create(this, JsonTextReader.<ParseConstructorAsync>d__25.getString_0(107348383));
			}
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			int initialPosition = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_1F9;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter5.IsCompleted)
					{
						await configuredTaskAwaiter5;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
						configuredTaskAwaiter5 = configuredTaskAwaiter6;
						configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter5.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_218;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, JsonTextReader.<ParseConstructorAsync>d__25.getString_0(107349025));
			IL_1F9:
			int endPosition = this._charPos;
			this._charPos++;
			goto IL_319;
			IL_218:
			if (c == '\r')
			{
				endPosition = this._charPos;
				configuredTaskAwaiter3 = this.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
			}
			else if (c == '\n')
			{
				endPosition = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				endPosition = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, JsonTextReader.<ParseConstructorAsync>d__25.getString_0(107349000).FormatWith(CultureInfo.InvariantCulture, c));
				}
				endPosition = this._charPos;
			}
			IL_319:
			this._stringReference = new StringReference(this._chars, initialPosition, endPosition - initialPosition);
			string constructorName = this._stringReference.ToString();
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, JsonTextReader.<ParseConstructorAsync>d__25.getString_0(107349000).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, constructorName);
			constructorName = null;
		}

		private async Task<object> ParseNumberNaNAsync(ReadType readType, CancellationToken cancellationToken)
		{
			bool matched = await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.NaN, cancellationToken).ConfigureAwait(false);
			return this.ParseNumberNaN(readType, matched);
		}

		private async Task<object> ParseNumberPositiveInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			bool matched = await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.PositiveInfinity, cancellationToken).ConfigureAwait(false);
			return this.ParseNumberPositiveInfinity(readType, matched);
		}

		private async Task<object> ParseNumberNegativeInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			bool matched = await this.MatchValueWithTrailingSeparatorAsync(JsonConvert.NegativeInfinity, cancellationToken).ConfigureAwait(false);
			return this.ParseNumberNegativeInfinity(readType, matched);
		}

		private async Task ParseNumberAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.ShiftBufferIfNeeded();
			char firstChar = this._chars[this._charPos];
			int initialPosition = this._charPos;
			await this.ReadNumberIntoBufferAsync(cancellationToken).ConfigureAwait(false);
			this.ParseReadNumber(readType, firstChar, initialPosition);
		}

		private Task ParseUndefinedAsync(CancellationToken cancellationToken)
		{
			return this.MatchAndSetAsync(JsonConvert.Undefined, JsonToken.Undefined, null, cancellationToken);
		}

		private async Task<bool> ParsePropertyAsync(CancellationToken cancellationToken)
		{
			char c = this._chars[this._charPos];
			char quoteChar;
			if (c != '"')
			{
				if (c != '\'')
				{
					if (this.ValidIdentifierChar(c))
					{
						quoteChar = '\0';
						this.ShiftBufferIfNeeded();
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ParseUnquotedPropertyAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						goto IL_18A;
					}
					throw JsonReaderException.Create(this, JsonTextReader.<ParsePropertyAsync>d__31.getString_0(107348858).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
			}
			this._charPos++;
			quoteChar = c;
			this.ShiftBufferIfNeeded();
			await this.ReadStringIntoBufferAsync(quoteChar, cancellationToken).ConfigureAwait(false);
			IL_18A:
			string propertyName;
			if (this.PropertyNameTable != null)
			{
				propertyName = (this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length) ?? this._stringReference.ToString());
			}
			else
			{
				propertyName = this._stringReference.ToString();
			}
			await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, JsonTextReader.<ParsePropertyAsync>d__31.getString_0(107348797).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, propertyName);
			this._quoteChar = quoteChar;
			this.ClearRecentString();
			return true;
		}

		private async Task ReadNumberIntoBufferAsync(CancellationToken cancellationToken)
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[charPos];
				if (c == '\0')
				{
					this._charPos = charPos;
					if (this._charsUsed != charPos)
					{
						break;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, charPos))
					{
						break;
					}
					charPos++;
				}
			}
		}

		private async Task ParseUnquotedPropertyAsync(CancellationToken cancellationToken)
		{
			int initialPosition = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_D3;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() == 0)
					{
						break;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, initialPosition))
				{
					return;
				}
			}
			throw JsonReaderException.Create(this, JsonTextReader.<ParseUnquotedPropertyAsync>d__33.getString_0(107349253));
			IL_D3:
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
		}

		private async Task<bool> ReadNullCharAsync(CancellationToken cancellationToken)
		{
			if (this._charsUsed == this._charPos)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult() == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		private async Task HandleNullAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ParseNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		private async Task ReadFinishedAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult())
			{
				await this.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false);
				if (this._isEndOfFile)
				{
					base.SetToken(JsonToken.None);
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, JsonTextReader.<ReadFinishedAsync>d__36.getString_0(107349478).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3 = this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
			}
			base.SetToken(JsonToken.None);
		}

		private async Task<object> ReadStringValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_DF;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				configuredTaskAwaiter3 = this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				return null;
			default:
				goto IL_DF;
			}
			char c;
			string expected;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								goto IL_301;
							case '\v':
							case '\f':
								goto IL_482;
							case '\r':
								configuredTaskAwaiter3 = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter3.IsCompleted)
								{
									await configuredTaskAwaiter3;
									configuredTaskAwaiter3 = configuredTaskAwaiter4;
									configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter3.GetResult();
								goto IL_301;
							default:
								goto IL_482;
							}
						}
						else
						{
							configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult())
							{
								break;
							}
							goto IL_301;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_482;
						case '"':
						case '\'':
							goto IL_5A1;
						case ',':
							this.ProcessValueComma();
							goto IL_301;
						case '-':
							goto IL_60C;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_70F;
						case '/':
							configuredTaskAwaiter3 = this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								await configuredTaskAwaiter3;
								configuredTaskAwaiter3 = configuredTaskAwaiter4;
								configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							}
							configuredTaskAwaiter3.GetResult();
							goto IL_301;
						default:
							if (c != 'I')
							{
								goto IL_482;
							}
							goto IL_544;
						}
					}
					this._charPos++;
				}
				else if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_7D0;
					}
					if (c != ']')
					{
						goto IL_482;
					}
					goto IL_78D;
				}
				else
				{
					if (c == 'f')
					{
						goto IL_890;
					}
					if (c == 'n')
					{
						goto IL_836;
					}
					if (c != 't')
					{
						goto IL_482;
					}
					goto IL_890;
				}
				IL_301:
				expected = null;
				continue;
				IL_482:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_16;
				}
				goto IL_301;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			Block_16:
			throw this.CreateUnexpectedCharacterException(c);
			IL_544:
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = this.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return configuredTaskAwaiter5.GetResult();
			IL_5A1:
			configuredTaskAwaiter3 = this.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return this.FinishReadQuotedStringValue(readType);
			IL_60C:
			configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			configuredTaskAwaiter3 = this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return this.Value;
			IL_70F:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			configuredTaskAwaiter3 = this.ParseNumberAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return this.Value;
			IL_78D:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_7D0:
			configuredTaskAwaiter5 = this.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return configuredTaskAwaiter5.GetResult();
			IL_836:
			configuredTaskAwaiter3 = this.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return null;
			IL_890:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			expected = ((c == 't') ? JsonConvert.True : JsonConvert.False);
			configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(expected, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, expected);
			return expected;
			IL_DF:
			throw JsonReaderException.Create(this, JsonTextReader.<ReadStringValueAsync>d__37.getString_0(107349521).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		private async Task<object> ReadNumberValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_DF;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				configuredTaskAwaiter3 = this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				return null;
			default:
				goto IL_DF;
			}
			char c;
			do
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_332;
						case '\r':
							configuredTaskAwaiter3 = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								await configuredTaskAwaiter3;
								configuredTaskAwaiter3 = configuredTaskAwaiter4;
								configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							}
							configuredTaskAwaiter3.GetResult();
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_332;
							case '"':
							case '\'':
								goto IL_4C6;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_531;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_67F;
							case '/':
								configuredTaskAwaiter3 = this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter3.IsCompleted)
								{
									await configuredTaskAwaiter3;
									configuredTaskAwaiter3 = configuredTaskAwaiter4;
									configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter3.GetResult();
								continue;
							default:
								goto IL_332;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult())
					{
						goto Block_13;
					}
					continue;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_79B;
					}
					if (c == 'N')
					{
						goto IL_73E;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_859;
					}
					if (c == 'n')
					{
						goto IL_801;
					}
				}
				IL_332:
				this._charPos++;
			}
			while (char.IsWhiteSpace(c));
			throw this.CreateUnexpectedCharacterException(c);
			Block_13:
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_4C6:
			configuredTaskAwaiter3 = this.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return this.FinishReadQuotedNumber(readType);
			IL_531:
			configuredTaskAwaiter = this.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter5;
			if (configuredTaskAwaiter.GetResult() && this._chars[this._charPos + 1] == 'I')
			{
				configuredTaskAwaiter5 = this.ParseNumberNegativeInfinityAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter5.IsCompleted)
				{
					await configuredTaskAwaiter5;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
					configuredTaskAwaiter5 = configuredTaskAwaiter6;
					configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
				}
				return configuredTaskAwaiter5.GetResult();
			}
			configuredTaskAwaiter3 = this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return this.Value;
			IL_67F:
			configuredTaskAwaiter3 = this.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return this.Value;
			IL_73E:
			configuredTaskAwaiter5 = this.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return configuredTaskAwaiter5.GetResult();
			IL_79B:
			configuredTaskAwaiter5 = this.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return configuredTaskAwaiter5.GetResult();
			IL_801:
			configuredTaskAwaiter3 = this.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return null;
			IL_859:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_DF:
			throw JsonReaderException.Create(this, JsonTextReader.<ReadNumberValueAsync>d__38.getString_0(107349530).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		public override Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsBooleanAsync(cancellationToken);
			}
			return this.DoReadAsBooleanAsync(cancellationToken);
		}

		internal async Task<bool?> DoReadAsBooleanAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_CF;
			case JsonReader.State.PostValue:
				configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				configuredTaskAwaiter3 = this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				return null;
			default:
				goto IL_CF;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							goto IL_26D;
						case '\v':
						case '\f':
							goto IL_3DF;
						case '\r':
							configuredTaskAwaiter3 = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								await configuredTaskAwaiter3;
								configuredTaskAwaiter3 = configuredTaskAwaiter4;
								configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							}
							configuredTaskAwaiter3.GetResult();
							goto IL_26D;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_3DF;
							case '"':
							case '\'':
								goto IL_44A;
							case ',':
								this.ProcessValueComma();
								goto IL_26D;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_4DF;
							case '/':
								configuredTaskAwaiter3 = this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter3.IsCompleted)
								{
									await configuredTaskAwaiter3;
									configuredTaskAwaiter3 = configuredTaskAwaiter4;
									configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter3.GetResult();
								goto IL_26D;
							default:
								goto IL_3DF;
							}
							break;
						}
						this._charPos++;
					}
					else
					{
						configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						}
						if (configuredTaskAwaiter.GetResult())
						{
							break;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_5F3;
					}
					if (c != 'f')
					{
						goto IL_3DF;
					}
					goto IL_645;
				}
				else
				{
					if (c == 'n')
					{
						goto IL_6F0;
					}
					if (c != 't')
					{
						goto IL_3DF;
					}
					goto IL_645;
				}
				IL_26D:
				BigInteger i = default(BigInteger);
				continue;
				IL_3DF:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_13;
				}
				goto IL_26D;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			Block_13:
			throw this.CreateUnexpectedCharacterException(c);
			IL_44A:
			configuredTaskAwaiter3 = this.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_4DF:
			configuredTaskAwaiter3 = this.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			object value = this.Value;
			bool flag;
			if (value is BigInteger)
			{
				BigInteger i = (BigInteger)value;
				flag = (i != 0L);
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_5F3:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_645:
			bool isTrue = c == 't';
			configuredTaskAwaiter = this.MatchValueWithTrailingSeparatorAsync(isTrue ? JsonConvert.True : JsonConvert.False, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, isTrue);
			return new bool?(isTrue);
			IL_6F0:
			configuredTaskAwaiter3 = this.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return null;
			IL_CF:
			throw JsonReaderException.Create(this, JsonTextReader.<DoReadAsBooleanAsync>d__40.getString_0(107349539).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		public override Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsBytesAsync(cancellationToken);
			}
			return this.DoReadAsBytesAsync(cancellationToken);
		}

		internal async Task<byte[]> DoReadAsBytesAsync(CancellationToken cancellationToken)
		{
			this.EnsureBuffer();
			bool isWrapped = false;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_DA;
			case JsonReader.State.PostValue:
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (configuredTaskAwaiter.GetResult())
				{
					return null;
				}
				break;
			}
			case JsonReader.State.Finished:
				configuredTaskAwaiter3 = this.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				return null;
			default:
				goto IL_DA;
			}
			char c;
			byte[] data;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								goto IL_296;
							case '\v':
							case '\f':
								goto IL_3B4;
							case '\r':
								configuredTaskAwaiter3 = this.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter3.IsCompleted)
								{
									await configuredTaskAwaiter3;
									configuredTaskAwaiter3 = configuredTaskAwaiter4;
									configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								}
								configuredTaskAwaiter3.GetResult();
								goto IL_296;
							default:
								goto IL_3B4;
							}
						}
						else
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								await configuredTaskAwaiter;
								configuredTaskAwaiter = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							}
							if (configuredTaskAwaiter.GetResult())
							{
								break;
							}
							goto IL_296;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_3B4;
						}
						goto IL_4C5;
					}
					this._charPos++;
				}
				else if (c <= '[')
				{
					if (c != ',')
					{
						if (c != '/')
						{
							if (c != '[')
							{
								goto IL_3B4;
							}
							goto IL_5D6;
						}
						else
						{
							configuredTaskAwaiter3 = this.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								await configuredTaskAwaiter3;
								configuredTaskAwaiter3 = configuredTaskAwaiter4;
								configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							}
							configuredTaskAwaiter3.GetResult();
						}
					}
					else
					{
						this.ProcessValueComma();
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_6EB;
					}
					if (c == 'n')
					{
						goto IL_693;
					}
					if (c != '{')
					{
						goto IL_3B4;
					}
					this._charPos++;
					base.SetToken(JsonToken.StartObject);
					configuredTaskAwaiter3 = this.ReadIntoWrappedTypeObjectAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						await configuredTaskAwaiter3;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					}
					configuredTaskAwaiter3.GetResult();
					isWrapped = true;
				}
				IL_296:
				data = null;
				continue;
				IL_3B4:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_18;
				}
				goto IL_296;
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			Block_18:
			throw this.CreateUnexpectedCharacterException(c);
			IL_4C5:
			configuredTaskAwaiter3 = this.ParseStringAsync(c, ReadType.ReadAsBytes, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			data = (byte[])this.Value;
			if (isWrapped)
			{
				configuredTaskAwaiter3 = base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter3.IsCompleted)
				{
					await configuredTaskAwaiter3;
					configuredTaskAwaiter3 = configuredTaskAwaiter4;
					configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter3.GetResult();
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, JsonTextReader.<DoReadAsBytesAsync>d__42.getString_0(107350977).FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, data, false);
			}
			return data;
			IL_5D6:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter configuredTaskAwaiter5 = base.ReadArrayIntoByteArrayAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter5.IsCompleted)
			{
				await configuredTaskAwaiter5;
				ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter configuredTaskAwaiter6;
				configuredTaskAwaiter5 = configuredTaskAwaiter6;
				configuredTaskAwaiter6 = default(ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter);
			}
			return configuredTaskAwaiter5.GetResult();
			IL_693:
			configuredTaskAwaiter3 = this.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter3.IsCompleted)
			{
				await configuredTaskAwaiter3;
				configuredTaskAwaiter3 = configuredTaskAwaiter4;
				configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter3.GetResult();
			return null;
			IL_6EB:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_DA:
			throw JsonReaderException.Create(this, JsonTextReader.<DoReadAsBytesAsync>d__42.getString_0(107349549).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		private async Task ReadIntoWrappedTypeObjectAsync(CancellationToken cancellationToken)
		{
			await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
			if (this.Value != null && this.Value.ToString() == JsonTextReader.<ReadIntoWrappedTypeObjectAsync>d__43.getString_0(107350701))
			{
				await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
				if (this.Value != null && this.Value.ToString().StartsWith(JsonTextReader.<ReadIntoWrappedTypeObjectAsync>d__43.getString_0(107350692), StringComparison.Ordinal))
				{
					await base.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false);
					if (this.Value.ToString() == JsonTextReader.<ReadIntoWrappedTypeObjectAsync>d__43.getString_0(107350671))
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, JsonTextReader.<ReadIntoWrappedTypeObjectAsync>d__43.getString_0(107350983).FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		public override Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDateTimeAsync(cancellationToken);
			}
			return this.DoReadAsDateTimeAsync(cancellationToken);
		}

		internal async Task<DateTime?> DoReadAsDateTimeAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsDateTime, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (DateTime?)configuredTaskAwaiter.GetResult();
		}

		public override Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDateTimeOffsetAsync(cancellationToken);
			}
			return this.DoReadAsDateTimeOffsetAsync(cancellationToken);
		}

		internal async Task<DateTimeOffset?> DoReadAsDateTimeOffsetAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsDateTimeOffset, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (DateTimeOffset?)configuredTaskAwaiter.GetResult();
		}

		public override Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDecimalAsync(cancellationToken);
			}
			return this.DoReadAsDecimalAsync(cancellationToken);
		}

		internal async Task<decimal?> DoReadAsDecimalAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsDecimal, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (decimal?)configuredTaskAwaiter.GetResult();
		}

		public override Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsDoubleAsync(cancellationToken);
			}
			return this.DoReadAsDoubleAsync(cancellationToken);
		}

		internal async Task<double?> DoReadAsDoubleAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsDouble, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (double?)configuredTaskAwaiter.GetResult();
		}

		public override Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsInt32Async(cancellationToken);
			}
			return this.DoReadAsInt32Async(cancellationToken);
		}

		internal async Task<int?> DoReadAsInt32Async(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadNumberValueAsync(ReadType.ReadAsInt32, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (int?)configuredTaskAwaiter.GetResult();
		}

		public override Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.ReadAsStringAsync(cancellationToken);
			}
			return this.DoReadAsStringAsync(cancellationToken);
		}

		internal async Task<string> DoReadAsStringAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadStringValueAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
			}
			return (string)configuredTaskAwaiter.GetResult();
		}

		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(JsonTextReader.getString_1(107246229));
			}
			this._reader = reader;
			this._lineNumber = 1;
			this._safeAsync = (base.GetType() == typeof(JsonTextReader));
		}

		public JsonNameTable PropertyNameTable { get; set; }

		public IArrayPool<char> ArrayPool
		{
			get
			{
				return this._arrayPool;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(JsonTextReader.getString_1(107452779));
				}
				this._arrayPool = value;
			}
		}

		private void EnsureBufferNotEmpty()
		{
			if (this._stringBuffer.IsEmpty)
			{
				this._stringBuffer = new StringBuffer(this._arrayPool, 1024);
			}
		}

		private void SetNewLine(bool hasNextChar)
		{
			if (hasNextChar && this._chars[this._charPos] == '\n')
			{
				this._charPos++;
			}
			this.OnNewLine(this._charPos);
		}

		private void OnNewLine(int pos)
		{
			this._lineNumber++;
			this._lineStartPos = pos;
		}

		private void ParseString(char quote, ReadType readType)
		{
			this._charPos++;
			this.ShiftBufferIfNeeded();
			this.ReadStringIntoBuffer(quote);
			this.ParseReadString(quote, readType);
		}

		private void ParseReadString(char quote, ReadType readType)
		{
			base.SetPostValueState(true);
			switch (readType)
			{
			case ReadType.ReadAsInt32:
			case ReadType.ReadAsDecimal:
			case ReadType.ReadAsBoolean:
				return;
			case ReadType.ReadAsBytes:
			{
				byte[] value;
				Guid guid;
				if (this._stringReference.Length == 0)
				{
					value = CollectionUtils.ArrayEmpty<byte>();
				}
				else if (this._stringReference.Length == 36 && ConvertUtils.TryConvertGuid(this._stringReference.ToString(), out guid))
				{
					value = guid.ToByteArray();
				}
				else
				{
					value = Convert.FromBase64CharArray(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				}
				base.SetToken(JsonToken.Bytes, value, false);
				return;
			}
			case ReadType.ReadAsString:
			{
				string value2 = this._stringReference.ToString();
				base.SetToken(JsonToken.String, value2, false);
				this._quoteChar = quote;
				return;
			}
			}
			if (this._dateParseHandling != DateParseHandling.None)
			{
				DateParseHandling dateParseHandling;
				if (readType == ReadType.ReadAsDateTime)
				{
					dateParseHandling = DateParseHandling.DateTime;
				}
				else if (readType == ReadType.ReadAsDateTimeOffset)
				{
					dateParseHandling = DateParseHandling.DateTimeOffset;
				}
				else
				{
					dateParseHandling = this._dateParseHandling;
				}
				DateTimeOffset dateTimeOffset;
				if (dateParseHandling == DateParseHandling.DateTime)
				{
					DateTime dateTime;
					if (DateTimeUtils.TryParseDateTime(this._stringReference, base.DateTimeZoneHandling, base.DateFormatString, base.Culture, out dateTime))
					{
						base.SetToken(JsonToken.Date, dateTime, false);
						return;
					}
				}
				else if (DateTimeUtils.TryParseDateTimeOffset(this._stringReference, base.DateFormatString, base.Culture, out dateTimeOffset))
				{
					base.SetToken(JsonToken.Date, dateTimeOffset, false);
					return;
				}
			}
			base.SetToken(JsonToken.String, this._stringReference.ToString(), false);
			this._quoteChar = quote;
		}

		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		private void ShiftBufferIfNeeded()
		{
			int num = this._chars.Length;
			if ((double)(num - this._charPos) <= (double)num * 0.1 || num >= 1073741823)
			{
				int num2 = this._charsUsed - this._charPos;
				if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
				this._chars[this._charsUsed] = '\0';
			}
		}

		private int ReadData(bool append)
		{
			return this.ReadData(append, 0);
		}

		private void PrepareBufferForReadData(bool append, int charsRequired)
		{
			if (this._charsUsed + charsRequired >= this._chars.Length - 1)
			{
				if (append)
				{
					int num = this._chars.Length * 2;
					int minSize = Math.Max((num < 0) ? int.MaxValue : num, this._charsUsed + charsRequired + 1);
					char[] array = BufferUtils.RentBuffer(this._arrayPool, minSize);
					JsonTextReader.BlockCopyChars(this._chars, 0, array, 0, this._chars.Length);
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array;
					return;
				}
				int num2 = this._charsUsed - this._charPos;
				if (num2 + charsRequired + 1 >= this._chars.Length)
				{
					char[] array2 = BufferUtils.RentBuffer(this._arrayPool, num2 + charsRequired + 1);
					if (num2 > 0)
					{
						JsonTextReader.BlockCopyChars(this._chars, this._charPos, array2, 0, num2);
					}
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array2;
				}
				else if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
			}
		}

		private int ReadData(bool append, int charsRequired)
		{
			if (this._isEndOfFile)
			{
				return 0;
			}
			this.PrepareBufferForReadData(append, charsRequired);
			int count = this._chars.Length - this._charsUsed - 1;
			int num = this._reader.Read(this._chars, this._charsUsed, count);
			this._charsUsed += num;
			if (num == 0)
			{
				this._isEndOfFile = true;
			}
			this._chars[this._charsUsed] = '\0';
			return num;
		}

		private bool EnsureChars(int relativePosition, bool append)
		{
			return this._charPos + relativePosition < this._charsUsed || this.ReadChars(relativePosition, append);
		}

		private bool ReadChars(int relativePosition, bool append)
		{
			if (this._isEndOfFile)
			{
				return false;
			}
			int num = this._charPos + relativePosition - this._charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = this.ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			return num2 >= num;
		}

		public override bool Read()
		{
			this.EnsureBuffer();
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_5D;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_54;
				case JsonReader.State.PostValue:
					if (!this.ParsePostValue(false))
					{
						continue;
					}
					return true;
				case JsonReader.State.Finished:
					goto IL_8F;
				}
				break;
			}
			goto IL_64;
			IL_54:
			return this.ParseObject();
			IL_5D:
			return this.ParseValue();
			IL_64:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349309).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			IL_8F:
			if (!this.EnsureChars(0, false))
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			this.EatWhitespace();
			if (this._isEndOfFile)
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			if (this._chars[this._charPos] == '/')
			{
				this.ParseComment(true);
				return true;
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349276).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
		}

		public override int? ReadAsInt32()
		{
			return (int?)this.ReadNumberValue(ReadType.ReadAsInt32);
		}

		public override DateTime? ReadAsDateTime()
		{
			return (DateTime?)this.ReadStringValue(ReadType.ReadAsDateTime);
		}

		public override string ReadAsString()
		{
			return (string)this.ReadStringValue(ReadType.ReadAsString);
		}

		public override byte[] ReadAsBytes()
		{
			this.EnsureBuffer();
			bool flag = false;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_22C;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_22C;
			}
			char c;
			do
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_A1;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_A1;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								goto Block_13;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_A1;
						}
						goto IL_165;
					}
					this._charPos++;
					continue;
				}
				if (c <= '[')
				{
					if (c == ',')
					{
						this.ProcessValueComma();
						continue;
					}
					if (c == '/')
					{
						this.ParseComment(false);
						continue;
					}
					if (c == '[')
					{
						goto IL_1C3;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1EF;
					}
					if (c == 'n')
					{
						goto IL_1E7;
					}
					if (c == '{')
					{
						this._charPos++;
						base.SetToken(JsonToken.StartObject);
						base.ReadIntoWrappedTypeObject();
						flag = true;
						continue;
					}
				}
				IL_A1:
				this._charPos++;
			}
			while (char.IsWhiteSpace(c));
			throw this.CreateUnexpectedCharacterException(c);
			Block_13:
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_165:
			this.ParseString(c, ReadType.ReadAsBytes);
			byte[] array = (byte[])this.Value;
			if (flag)
			{
				base.ReaderReadAndAssert();
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107350737).FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, array, false);
			}
			return array;
			IL_1C3:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return base.ReadArrayIntoByteArray();
			IL_1E7:
			this.HandleNull();
			return null;
			IL_1EF:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_22C:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349309).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		private object ReadStringValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_2D1;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_2D1;
			}
			char c;
			do
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_89;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_89;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								goto Block_12;
							}
							continue;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_89;
						case '"':
						case '\'':
							goto IL_1BD;
						case ',':
							this.ProcessValueComma();
							continue;
						case '-':
							goto IL_1CD;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_200;
						case '/':
							this.ParseComment(false);
							continue;
						default:
							if (c != 'I')
							{
								goto IL_89;
							}
							goto IL_1B5;
						}
					}
					this._charPos++;
					continue;
				}
				if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_265;
					}
					if (c == ']')
					{
						goto IL_228;
					}
				}
				else
				{
					if (c == 'f')
					{
						goto IL_27D;
					}
					if (c == 'n')
					{
						goto IL_275;
					}
					if (c == 't')
					{
						goto IL_27D;
					}
				}
				IL_89:
				this._charPos++;
			}
			while (char.IsWhiteSpace(c));
			throw this.CreateUnexpectedCharacterException(c);
			Block_12:
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_1B5:
			return this.ParseNumberPositiveInfinity(readType);
			IL_1BD:
			this.ParseString(c, readType);
			return this.FinishReadQuotedStringValue(readType);
			IL_1CD:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_200:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			this.ParseNumber(ReadType.ReadAsString);
			return this.Value;
			IL_228:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_265:
			return this.ParseNumberNaN(readType);
			IL_275:
			this.HandleNull();
			return null;
			IL_27D:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			string text = (c == 't') ? JsonConvert.True : JsonConvert.False;
			if (!this.MatchValueWithTrailingSeparator(text))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, text);
			return text;
			IL_2D1:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349309).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		private object FinishReadQuotedStringValue(ReadType readType)
		{
			switch (readType)
			{
			case ReadType.ReadAsBytes:
			case ReadType.ReadAsString:
				return this.Value;
			case ReadType.ReadAsDateTime:
			{
				object value;
				if ((value = this.Value) is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					return dateTime;
				}
				return base.ReadDateTimeString((string)this.Value);
			}
			case ReadType.ReadAsDateTimeOffset:
			{
				object value;
				if ((value = this.Value) is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					return dateTimeOffset;
				}
				return base.ReadDateTimeOffsetString((string)this.Value);
			}
			}
			throw new ArgumentOutOfRangeException(JsonTextReader.getString_1(107349215));
		}

		private JsonReaderException CreateUnexpectedCharacterException(char c)
		{
			return JsonReaderException.Create(this, JsonTextReader.getString_1(107349170).FormatWith(CultureInfo.InvariantCulture, c));
		}

		public override bool? ReadAsBoolean()
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_2CF;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_2CF;
			}
			char c;
			do
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_89;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_89;
							case '"':
							case '\'':
								goto IL_198;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_1B8;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_89;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (!this.ReadNullChar())
					{
						continue;
					}
					goto IL_212;
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_225;
					}
					if (c == 'f')
					{
						goto IL_272;
					}
				}
				else
				{
					if (c == 'n')
					{
						goto IL_2BF;
					}
					if (c == 't')
					{
						goto IL_272;
					}
				}
				IL_89:
				this._charPos++;
			}
			while (char.IsWhiteSpace(c));
			throw this.CreateUnexpectedCharacterException(c);
			IL_198:
			this.ParseString(c, ReadType.Read);
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_1B8:
			this.ParseNumber(ReadType.Read);
			object value;
			bool flag;
			if ((value = this.Value) is BigInteger)
			{
				BigInteger left = (BigInteger)value;
				flag = (left != 0L);
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_212:
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_225:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_272:
			bool flag2;
			string value2 = (flag2 = (c == 't')) ? JsonConvert.True : JsonConvert.False;
			if (!this.MatchValueWithTrailingSeparator(value2))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, flag2);
			return new bool?(flag2);
			IL_2BF:
			this.HandleNull();
			return null;
			IL_2CF:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349309).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		private void ProcessValueComma()
		{
			this._charPos++;
			if (this._currentState != JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.Undefined);
				JsonReaderException ex = this.CreateUnexpectedCharacterException(',');
				this._charPos--;
				throw ex;
			}
			base.SetStateBasedOnCurrent();
		}

		private object ReadNumberValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_246;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_246;
			}
			char c;
			do
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_81;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_81;
							case '"':
							case '\'':
								goto IL_18D;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_19D;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_1D0;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_81;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (!this.ReadNullChar())
					{
						continue;
					}
					goto IL_1DE;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_1F1;
					}
					if (c == 'N')
					{
						goto IL_1E9;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_209;
					}
					if (c == 'n')
					{
						goto IL_201;
					}
				}
				IL_81:
				this._charPos++;
			}
			while (char.IsWhiteSpace(c));
			throw this.CreateUnexpectedCharacterException(c);
			IL_18D:
			this.ParseString(c, readType);
			return this.FinishReadQuotedNumber(readType);
			IL_19D:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_1D0:
			this.ParseNumber(readType);
			return this.Value;
			IL_1DE:
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_1E9:
			return this.ParseNumberNaN(readType);
			IL_1F1:
			return this.ParseNumberPositiveInfinity(readType);
			IL_201:
			this.HandleNull();
			return null;
			IL_209:
			this._charPos++;
			if (this._currentState != JsonReader.State.Array && this._currentState != JsonReader.State.ArrayStart)
			{
				if (this._currentState != JsonReader.State.PostValue)
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			base.SetToken(JsonToken.EndArray);
			return null;
			IL_246:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349309).FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		private object FinishReadQuotedNumber(ReadType readType)
		{
			if (readType == ReadType.ReadAsInt32)
			{
				return base.ReadInt32String(this._stringReference.ToString());
			}
			if (readType == ReadType.ReadAsDecimal)
			{
				return base.ReadDecimalString(this._stringReference.ToString());
			}
			if (readType != ReadType.ReadAsDouble)
			{
				throw new ArgumentOutOfRangeException(JsonTextReader.getString_1(107349215));
			}
			return base.ReadDoubleString(this._stringReference.ToString());
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return (DateTimeOffset?)this.ReadStringValue(ReadType.ReadAsDateTimeOffset);
		}

		public override decimal? ReadAsDecimal()
		{
			return (decimal?)this.ReadNumberValue(ReadType.ReadAsDecimal);
		}

		public override double? ReadAsDouble()
		{
			return (double?)this.ReadNumberValue(ReadType.ReadAsDouble);
		}

		private void HandleNull()
		{
			if (!this.EnsureChars(1, true))
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				this.ParseNull();
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		private void ReadFinished()
		{
			if (this.EnsureChars(0, false))
			{
				this.EatWhitespace();
				if (this._isEndOfFile)
				{
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349276).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				this.ParseComment(false);
			}
			base.SetToken(JsonToken.None);
		}

		private bool ReadNullChar()
		{
			if (this._charsUsed == this._charPos)
			{
				if (this.ReadData(false) == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		private void EnsureBuffer()
		{
			if (this._chars == null)
			{
				this._chars = BufferUtils.RentBuffer(this._arrayPool, 1024);
				this._chars[0] = '\0';
			}
		}

		private void ReadStringIntoBuffer(char quote)
		{
			int num = this._charPos;
			int charPos = this._charPos;
			int lastWritePosition = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char c = this._chars[num++];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = num - 1;
								this.ProcessCarriageReturn(true);
								num = this._charPos;
							}
						}
						else
						{
							this._charPos = num - 1;
							this.ProcessLineFeed();
							num = this._charPos;
						}
					}
					else if (this._charsUsed == num - 1)
					{
						num--;
						if (this.ReadData(true) == 0)
						{
							goto IL_289;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = num;
						if (!this.EnsureChars(0, true))
						{
							goto IL_2B6;
						}
						int writeToPosition = num - 1;
						c2 = this._chars[num];
						num++;
						char c3;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									break;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									break;
								}
								c3 = '\\';
								goto IL_1F3;
							}
							c3 = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									break;
								}
								c3 = '\f';
							}
							else
							{
								c3 = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									c3 = '\r';
									goto IL_1F3;
								case 't':
									c3 = '\t';
									goto IL_1F3;
								case 'u':
									this._charPos = num;
									c3 = this.ParseUnicode();
									if (StringUtils.IsLowSurrogate(c3))
									{
										c3 = '�';
									}
									else if (StringUtils.IsHighSurrogate(c3))
									{
										bool flag;
										do
										{
											flag = false;
											if (this.EnsureChars(2, true) && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char writeChar = c3;
												this._charPos += 2;
												c3 = this.ParseUnicode();
												if (!StringUtils.IsLowSurrogate(c3))
												{
													if (StringUtils.IsHighSurrogate(c3))
													{
														writeChar = '�';
														flag = true;
													}
													else
													{
														writeChar = '�';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(writeChar, lastWritePosition, writeToPosition);
												lastWritePosition = this._charPos;
											}
											else
											{
												c3 = '�';
											}
										}
										while (flag);
									}
									num = this._charPos;
									goto IL_1F3;
								}
								break;
							}
							c3 = '\n';
						}
						IL_1F3:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(c3, lastWritePosition, writeToPosition);
						lastWritePosition = num;
					}
				}
				else if (this._chars[num - 1] == quote)
				{
					goto Block_24;
				}
			}
			goto IL_2DC;
			Block_24:
			this.FinishReadStringIntoBuffer(num - 1, charPos, lastWritePosition);
			return;
			IL_289:
			this._charPos = num;
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349121).FormatWith(CultureInfo.InvariantCulture, quote));
			IL_2B6:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349121).FormatWith(CultureInfo.InvariantCulture, quote));
			IL_2DC:
			this._charPos = num;
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349572).FormatWith(CultureInfo.InvariantCulture, JsonTextReader.getString_1(107395471) + c2.ToString()));
		}

		private void FinishReadStringIntoBuffer(int charPos, int initialPosition, int lastWritePosition)
		{
			if (initialPosition == lastWritePosition)
			{
				this._stringReference = new StringReference(this._chars, initialPosition, charPos - initialPosition);
			}
			else
			{
				this.EnsureBufferNotEmpty();
				if (charPos > lastWritePosition)
				{
					this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, charPos - lastWritePosition);
				}
				this._stringReference = new StringReference(this._stringBuffer.InternalBuffer, 0, this._stringBuffer.Position);
			}
			this._charPos = charPos + 1;
		}

		private void WriteCharToBuffer(char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			this._stringBuffer.Append(this._arrayPool, writeChar);
		}

		private char ConvertUnicode(bool enoughChars)
		{
			if (!enoughChars)
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349478));
			}
			int value;
			if (!ConvertUtils.TryHexTextToInt(this._chars, this._charPos, this._charPos + 4, out value))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349499).FormatWith(CultureInfo.InvariantCulture, new string(this._chars, this._charPos, 4)));
			}
			char result = Convert.ToChar(value);
			this._charPos += 4;
			return result;
		}

		private char ParseUnicode()
		{
			return this.ConvertUnicode(this.EnsureChars(4, true));
		}

		private void ReadNumberIntoBuffer()
		{
			int num = this._charPos;
			for (;;)
			{
				char c = this._chars[num];
				if (c == '\0')
				{
					this._charPos = num;
					if (this._charsUsed != num)
					{
						return;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, num))
					{
						return;
					}
					num++;
				}
			}
		}

		private bool ReadNumberCharIntoBuffer(char currentChar, int charPos)
		{
			if (currentChar <= 'X')
			{
				switch (currentChar)
				{
				case '+':
				case '-':
				case '.':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
					return false;
				case ',':
				case '/':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					break;
				default:
					if (currentChar == 'X')
					{
						return false;
					}
					break;
				}
			}
			else
			{
				switch (currentChar)
				{
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
					return false;
				default:
					if (currentChar == 'x')
					{
						return false;
					}
					break;
				}
			}
			this._charPos = charPos;
			if (!char.IsWhiteSpace(currentChar) && currentChar != ',' && currentChar != '}' && currentChar != ']' && currentChar != ')')
			{
				if (currentChar != '/')
				{
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349373).FormatWith(CultureInfo.InvariantCulture, currentChar));
				}
			}
			return true;
		}

		private void ClearRecentString()
		{
			this._stringBuffer.Position = 0;
			this._stringReference = default(StringReference);
		}

		private bool ParsePostValue(bool ignoreComments)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_49;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_49;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							if (this.ReadData(false) == 0)
							{
								goto Block_11;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_49;
						}
						goto IL_FD;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_117;
					}
					if (c == '/')
					{
						this.ParseComment(!ignoreComments);
						if (!ignoreComments)
						{
							break;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_183;
					}
					if (c == '}')
					{
						goto IL_16B;
					}
				}
				IL_49:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_12D;
				}
				this._charPos++;
			}
			return true;
			Block_11:
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_FD:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_117:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_12D:
			if (base.SupportMultipleContent && this.Depth == 0)
			{
				base.SetStateBasedOnCurrent();
				return false;
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348812).FormatWith(CultureInfo.InvariantCulture, c));
			IL_16B:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_183:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
		}

		private bool ParseObject()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c > '\r')
				{
					if (c == ' ')
					{
						goto IL_43;
					}
					if (c == '/')
					{
						goto IL_D9;
					}
					if (c == '}')
					{
						goto IL_C1;
					}
				}
				else if (c != '\0')
				{
					switch (c)
					{
					case '\t':
						goto IL_43;
					case '\n':
						this.ProcessLineFeed();
						continue;
					case '\r':
						this.ProcessCarriageReturn(false);
						continue;
					}
				}
				else
				{
					if (this._charsUsed != this._charPos)
					{
						this._charPos++;
						continue;
					}
					if (this.ReadData(false) == 0)
					{
						break;
					}
					continue;
				}
				if (char.IsWhiteSpace(c))
				{
					this._charPos++;
					continue;
				}
				goto IL_BA;
				IL_43:
				this._charPos++;
			}
			return false;
			IL_BA:
			return this.ParseProperty();
			IL_C1:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_D9:
			this.ParseComment(true);
			return true;
		}

		private bool ParseProperty()
		{
			char c = this._chars[this._charPos];
			char c2;
			if (c != '"')
			{
				if (c != '\'')
				{
					if (this.ValidIdentifierChar(c))
					{
						c2 = '\0';
						this.ShiftBufferIfNeeded();
						this.ParseUnquotedProperty();
						goto IL_82;
					}
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348687).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
			}
			this._charPos++;
			c2 = c;
			this.ShiftBufferIfNeeded();
			this.ReadStringIntoBuffer(c2);
			IL_82:
			string text;
			if (this.PropertyNameTable != null)
			{
				text = this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				if (text == null)
				{
					text = this._stringReference.ToString();
				}
			}
			else
			{
				text = this._stringReference.ToString();
			}
			this.EatWhitespace();
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348626).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, text);
			this._quoteChar = c2;
			this.ClearRecentString();
			return true;
		}

		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		private void ParseUnquotedProperty()
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_55;
					}
					if (this.ReadData(true) == 0)
					{
						goto IL_3F;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, charPos))
				{
					break;
				}
			}
			return;
			IL_3F:
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107349069));
			IL_55:
			this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
		}

		private bool ReadUnquotedPropertyReportIfDone(char currentChar, int initialPosition)
		{
			if (this.ValidIdentifierChar(currentChar))
			{
				this._charPos++;
				return false;
			}
			if (!char.IsWhiteSpace(currentChar))
			{
				if (currentChar != ':')
				{
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348996).FormatWith(CultureInfo.InvariantCulture, currentChar));
				}
			}
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
			return true;
		}

		private bool ParseValue()
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c > 'N')
				{
					if (c <= 'f')
					{
						if (c == '[')
						{
							goto IL_1E5;
						}
						if (c == ']')
						{
							goto IL_1CD;
						}
						if (c == 'f')
						{
							goto IL_1C5;
						}
					}
					else if (c <= 't')
					{
						if (c == 'n')
						{
							goto IL_204;
						}
						if (c == 't')
						{
							goto IL_1FC;
						}
					}
					else
					{
						if (c == 'u')
						{
							goto IL_29D;
						}
						if (c == '{')
						{
							goto IL_286;
						}
					}
				}
				else if (c <= ' ')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_59;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							if (c != ' ')
							{
								goto IL_59;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this._charsUsed != this._charPos)
					{
						this._charPos++;
						continue;
					}
					if (this.ReadData(false) == 0)
					{
						break;
					}
					continue;
				}
				else if (c <= '/')
				{
					if (c == '"')
					{
						goto IL_1A7;
					}
					switch (c)
					{
					case '\'':
						goto IL_1A7;
					case ')':
						goto IL_14C;
					case ',':
						goto IL_164;
					case '-':
						goto IL_16E;
					case '/':
						goto IL_19E;
					}
				}
				else
				{
					if (c == 'I')
					{
						goto IL_1BB;
					}
					if (c == 'N')
					{
						goto IL_1B1;
					}
				}
				IL_59:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_261;
				}
				this._charPos++;
			}
			return false;
			IL_14C:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_164:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_16E:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				this.ParseNumberNegativeInfinity(ReadType.Read);
			}
			else
			{
				this.ParseNumber(ReadType.Read);
			}
			return true;
			IL_19E:
			this.ParseComment(true);
			return true;
			IL_1A7:
			this.ParseString(c, ReadType.Read);
			return true;
			IL_1B1:
			this.ParseNumberNaN(ReadType.Read);
			return true;
			IL_1BB:
			this.ParseNumberPositiveInfinity(ReadType.Read);
			return true;
			IL_1C5:
			this.ParseFalse();
			return true;
			IL_1CD:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_1E5:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_1FC:
			this.ParseTrue();
			return true;
			IL_204:
			if (this.EnsureChars(1, true))
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c2 != 'e')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					this.ParseConstructor();
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_261:
			if (!char.IsNumber(c) && c != '-')
			{
				if (c != '.')
				{
					throw this.CreateUnexpectedCharacterException(c);
				}
			}
			this.ParseNumber(ReadType.Read);
			return true;
			IL_286:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_29D:
			this.ParseUndefined();
			return true;
		}

		private void ProcessLineFeed()
		{
			this._charPos++;
			this.OnNewLine(this._charPos);
		}

		private void ProcessCarriageReturn(bool append)
		{
			this._charPos++;
			this.SetNewLine(this.EnsureChars(1, append));
		}

		private void EatWhitespace()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed == this._charPos)
					{
						if (this.ReadData(false) == 0)
						{
							break;
						}
					}
					else
					{
						this._charPos++;
					}
				}
				else if (c != '\n')
				{
					if (c != '\r')
					{
						if (c != ' ' && !char.IsWhiteSpace(c))
						{
							return;
						}
						this._charPos++;
					}
					else
					{
						this.ProcessCarriageReturn(false);
					}
				}
				else
				{
					this.ProcessLineFeed();
				}
			}
		}

		private void ParseConstructor()
		{
			if (!this.MatchValueWithTrailingSeparator(JsonTextReader.getString_1(107442087)))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348249));
			}
			this.EatWhitespace();
			int charPos = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_81;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_98;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348891));
			IL_81:
			int charPos2 = this._charPos;
			this._charPos++;
			goto IL_EC;
			IL_98:
			if (c == '\r')
			{
				charPos2 = this._charPos;
				this.ProcessCarriageReturn(true);
			}
			else if (c == '\n')
			{
				charPos2 = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				charPos2 = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348866).FormatWith(CultureInfo.InvariantCulture, c));
				}
				charPos2 = this._charPos;
			}
			IL_EC:
			this._stringReference = new StringReference(this._chars, charPos, charPos2 - charPos);
			string value = this._stringReference.ToString();
			this.EatWhitespace();
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348866).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, value);
		}

		private void ParseNumber(ReadType readType)
		{
			this.ShiftBufferIfNeeded();
			char firstChar = this._chars[this._charPos];
			int charPos = this._charPos;
			this.ReadNumberIntoBuffer();
			this.ParseReadNumber(readType, firstChar, charPos);
		}

		private void ParseReadNumber(ReadType readType, char firstChar, int initialPosition)
		{
			base.SetPostValueState(true);
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
			bool flag = char.IsDigit(firstChar) && this._stringReference.Length == 1;
			bool flag2 = firstChar == '0' && this._stringReference.Length > 1 && this._stringReference.Chars[this._stringReference.StartIndex + 1] != '.' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'e' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'E';
			object value;
			JsonToken newToken;
			switch (readType)
			{
			case ReadType.Read:
			case ReadType.ReadAsInt64:
			{
				if (flag)
				{
					value = (long)((ulong)firstChar - 48UL);
					newToken = JsonToken.Integer;
					goto IL_6DB;
				}
				if (flag2)
				{
					string text = this._stringReference.ToString();
					try
					{
						value = (text.StartsWith(JsonTextReader.getString_1(107348228), StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
					}
					catch (Exception ex)
					{
						throw this.ThrowReaderError(JsonTextReader.getString_1(107348223).FormatWith(CultureInfo.InvariantCulture, text), ex);
					}
					newToken = JsonToken.Integer;
					goto IL_6DB;
				}
				long num;
				ParseResult parseResult = ConvertUtils.Int64TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num);
				if (parseResult == ParseResult.Success)
				{
					value = num;
					newToken = JsonToken.Integer;
					goto IL_6DB;
				}
				if (parseResult != ParseResult.Overflow)
				{
					if (this._floatParseHandling == FloatParseHandling.Decimal)
					{
						decimal num2;
						parseResult = ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num2);
						if (parseResult != ParseResult.Success)
						{
							throw this.ThrowReaderError(JsonTextReader.getString_1(107348548).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						value = num2;
					}
					else
					{
						double num3;
						if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
						{
							throw this.ThrowReaderError(JsonTextReader.getString_1(107348223).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						value = num3;
					}
					newToken = JsonToken.Float;
					goto IL_6DB;
				}
				string text2 = this._stringReference.ToString();
				if (text2.Length > 380)
				{
					throw this.ThrowReaderError(JsonTextReader.getString_1(107348402).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				value = JsonTextReader.BigIntegerParse(text2, CultureInfo.InvariantCulture);
				newToken = JsonToken.Integer;
				goto IL_6DB;
			}
			case ReadType.ReadAsInt32:
				if (flag)
				{
					value = (int)(firstChar - '0');
				}
				else
				{
					if (flag2)
					{
						string text3 = this._stringReference.ToString();
						try
						{
							value = (text3.StartsWith(JsonTextReader.getString_1(107348228), StringComparison.OrdinalIgnoreCase) ? Convert.ToInt32(text3, 16) : Convert.ToInt32(text3, 8));
							goto IL_194;
						}
						catch (Exception ex2)
						{
							throw this.ThrowReaderError(JsonTextReader.getString_1(107348166).FormatWith(CultureInfo.InvariantCulture, text3), ex2);
						}
					}
					int num4;
					ParseResult parseResult2 = ConvertUtils.Int32TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num4);
					if (parseResult2 == ParseResult.Success)
					{
						value = num4;
					}
					else
					{
						if (parseResult2 == ParseResult.Overflow)
						{
							throw this.ThrowReaderError(JsonTextReader.getString_1(107348109).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						throw this.ThrowReaderError(JsonTextReader.getString_1(107348166).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
				}
				IL_194:
				newToken = JsonToken.Integer;
				goto IL_6DB;
			case ReadType.ReadAsString:
			{
				string text4 = this._stringReference.ToString();
				if (flag2)
				{
					try
					{
						if (text4.StartsWith(JsonTextReader.getString_1(107348228), StringComparison.OrdinalIgnoreCase))
						{
							Convert.ToInt64(text4, 16);
						}
						else
						{
							Convert.ToInt64(text4, 8);
						}
						goto IL_4C8;
					}
					catch (Exception ex3)
					{
						throw this.ThrowReaderError(JsonTextReader.getString_1(107348223).FormatWith(CultureInfo.InvariantCulture, text4), ex3);
					}
				}
				double num5;
				if (!double.TryParse(text4, NumberStyles.Float, CultureInfo.InvariantCulture, out num5))
				{
					throw this.ThrowReaderError(JsonTextReader.getString_1(107348223).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				IL_4C8:
				newToken = JsonToken.String;
				value = text4;
				goto IL_6DB;
			}
			case ReadType.ReadAsDecimal:
				if (flag)
				{
					value = firstChar - 48m;
				}
				else
				{
					if (flag2)
					{
						string text5 = this._stringReference.ToString();
						try
						{
							value = Convert.ToDecimal(text5.StartsWith(JsonTextReader.getString_1(107348228), StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text5, 16) : Convert.ToInt64(text5, 8));
							goto IL_59B;
						}
						catch (Exception ex4)
						{
							throw this.ThrowReaderError(JsonTextReader.getString_1(107348548).FormatWith(CultureInfo.InvariantCulture, text5), ex4);
						}
					}
					decimal num6;
					if (ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num6) != ParseResult.Success)
					{
						throw this.ThrowReaderError(JsonTextReader.getString_1(107348548).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					value = num6;
				}
				IL_59B:
				newToken = JsonToken.Float;
				goto IL_6DB;
			case ReadType.ReadAsDouble:
				if (flag)
				{
					value = (double)firstChar - 48.0;
				}
				else
				{
					if (flag2)
					{
						string text6 = this._stringReference.ToString();
						try
						{
							value = Convert.ToDouble(text6.StartsWith(JsonTextReader.getString_1(107348228), StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text6, 16) : Convert.ToInt64(text6, 8));
							goto IL_6A5;
						}
						catch (Exception ex5)
						{
							throw this.ThrowReaderError(JsonTextReader.getString_1(107348491).FormatWith(CultureInfo.InvariantCulture, text6), ex5);
						}
					}
					double num7;
					if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num7))
					{
						throw this.ThrowReaderError(JsonTextReader.getString_1(107348491).FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					value = num7;
				}
				IL_6A5:
				newToken = JsonToken.Float;
				goto IL_6DB;
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348349));
			IL_6DB:
			this.ClearRecentString();
			base.SetToken(newToken, value, false);
		}

		private JsonReaderException ThrowReaderError(string message, Exception ex = null)
		{
			base.SetToken(JsonToken.Undefined, null, false);
			return JsonReaderException.Create(this, message, ex);
		}

		private static object BigIntegerParse(string number, CultureInfo culture)
		{
			return BigInteger.Parse(number, culture);
		}

		private void ParseComment(bool setToken)
		{
			this._charPos++;
			if (!this.EnsureChars(1, false))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347792));
			}
			bool flag;
			if (this._chars[this._charPos] == '*')
			{
				flag = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347739).FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				flag = true;
			}
			this._charPos++;
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (!flag)
							{
								this.ProcessLineFeed();
								continue;
							}
							goto IL_121;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(true) == 0)
						{
							goto Block_13;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (!flag && this.EnsureChars(0, true) && this._chars[this._charPos] == '/')
						{
							break;
						}
						continue;
					}
				}
				else
				{
					if (!flag)
					{
						this.ProcessCarriageReturn(true);
						continue;
					}
					goto IL_1A9;
				}
				this._charPos++;
			}
			this.EndComment(setToken, charPos, this._charPos - 1);
			this._charPos++;
			return;
			Block_13:
			if (!flag)
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347792));
			}
			this.EndComment(setToken, charPos, this._charPos);
			return;
			IL_121:
			this.EndComment(setToken, charPos, this._charPos);
			return;
			IL_1A9:
			this.EndComment(setToken, charPos, this._charPos);
		}

		private void EndComment(bool setToken, int initialPosition, int endPosition)
		{
			if (setToken)
			{
				base.SetToken(JsonToken.Comment, new string(this._chars, initialPosition, endPosition - initialPosition));
			}
		}

		private bool MatchValue(string value)
		{
			return this.MatchValue(this.EnsureChars(value.Length - 1, true), value);
		}

		private bool MatchValue(bool enoughChars, string value)
		{
			if (!enoughChars)
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (this._chars[this._charPos + i] != value[i])
				{
					this._charPos += i;
					return false;
				}
			}
			this._charPos += value.Length;
			return true;
		}

		private bool MatchValueWithTrailingSeparator(string value)
		{
			return this.MatchValue(value) && (!this.EnsureChars(0, false) || this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
		}

		private bool IsSeparator(char c)
		{
			if (c <= ')')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_8C;
				default:
					if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_8C;
						}
						if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
						{
							return true;
						}
						return false;
					}
					break;
				}
				return true;
			}
			if (c <= '/')
			{
				if (c != ',')
				{
					if (c != '/')
					{
						goto IL_8C;
					}
					if (!this.EnsureChars(1, false))
					{
						return false;
					}
					char c2 = this._chars[this._charPos + 1];
					return c2 == '*' || c2 == '/';
				}
			}
			else if (c != ']')
			{
				if (c != '}')
				{
					goto IL_8C;
				}
			}
			return true;
			IL_8C:
			if (char.IsWhiteSpace(c))
			{
				return true;
			}
			return false;
		}

		private void ParseTrue()
		{
			if (!this.MatchValueWithTrailingSeparator(JsonConvert.True))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347710));
			}
			base.SetToken(JsonToken.Boolean, true);
		}

		private void ParseNull()
		{
			if (!this.MatchValueWithTrailingSeparator(JsonConvert.Null))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347637));
			}
			base.SetToken(JsonToken.Null);
		}

		private void ParseUndefined()
		{
			if (!this.MatchValueWithTrailingSeparator(JsonConvert.Undefined))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347600));
			}
			base.SetToken(JsonToken.Undefined);
		}

		private void ParseFalse()
		{
			if (!this.MatchValueWithTrailingSeparator(JsonConvert.False))
			{
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347710));
			}
			base.SetToken(JsonToken.Boolean, false);
		}

		private object ParseNumberNegativeInfinity(ReadType readType)
		{
			return this.ParseNumberNegativeInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NegativeInfinity));
		}

		private object ParseNumberNegativeInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NegativeInfinity);
						return JsonConvert.NegativeInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_2B;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NegativeInfinity);
					return double.NegativeInfinity;
				}
				IL_2B:
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347591));
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107348062));
		}

		private object ParseNumberPositiveInfinity(ReadType readType)
		{
			return this.ParseNumberPositiveInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.PositiveInfinity));
		}

		private object ParseNumberPositiveInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.PositiveInfinity);
						return JsonConvert.PositiveInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_2B;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.PositiveInfinity);
					return double.PositiveInfinity;
				}
				IL_2B:
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347989));
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347952));
		}

		private object ParseNumberNaN(ReadType readType)
		{
			return this.ParseNumberNaN(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NaN));
		}

		private object ParseNumberNaN(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NaN);
						return JsonConvert.NaN;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_2B;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NaN);
					return double.NaN;
				}
				IL_2B:
				throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347943));
			}
			throw JsonReaderException.Create(this, JsonTextReader.getString_1(107347910));
		}

		public override void Close()
		{
			base.Close();
			if (this._chars != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
				this._chars = null;
			}
			if (base.CloseInput)
			{
				TextReader reader = this._reader;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this._stringBuffer.Clear(this._arrayPool);
		}

		public bool HasLineInfo()
		{
			return true;
		}

		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start && this.LinePosition == 0 && this.TokenType != JsonToken.Comment)
				{
					return 0;
				}
				return this._lineNumber;
			}
		}

		public int LinePosition
		{
			get
			{
				return this._charPos - this._lineStartPos;
			}
		}

		static JsonTextReader()
		{
			Strings.CreateGetStringDelegate(typeof(JsonTextReader));
		}

		private readonly bool _safeAsync;

		private const char UnicodeReplacementChar = '�';

		private const int MaximumJavascriptIntegerCharacterLength = 380;

		private const int LargeBufferLength = 1073741823;

		private readonly TextReader _reader;

		private char[] _chars;

		private int _charsUsed;

		private int _charPos;

		private int _lineStartPos;

		private int _lineNumber;

		private bool _isEndOfFile;

		private StringBuffer _stringBuffer;

		private StringReference _stringReference;

		private IArrayPool<char> _arrayPool;

		[NonSerialized]
		internal static GetString getString_1;
	}
}
