using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal static class JavaScriptUtils
	{
		static JavaScriptUtils()
		{
			Strings.CreateGetStringDelegate(typeof(JavaScriptUtils));
			JavaScriptUtils.SingleQuoteCharEscapeFlags = new bool[128];
			JavaScriptUtils.DoubleQuoteCharEscapeFlags = new bool[128];
			JavaScriptUtils.HtmlCharEscapeFlags = new bool[128];
			IList<char> list = new List<char>
			{
				'\n',
				'\r',
				'\t',
				'\\',
				'\f',
				'\b'
			};
			for (int i = 0; i < 32; i++)
			{
				list.Add((char)i);
			}
			foreach (char c in list.Union(new char[]
			{
				'\''
			}))
			{
				JavaScriptUtils.SingleQuoteCharEscapeFlags[(int)c] = true;
			}
			foreach (char c2 in list.Union(new char[]
			{
				'"'
			}))
			{
				JavaScriptUtils.DoubleQuoteCharEscapeFlags[(int)c2] = true;
			}
			foreach (char c3 in list.Union(new char[]
			{
				'"',
				'\'',
				'<',
				'>',
				'&'
			}))
			{
				JavaScriptUtils.HtmlCharEscapeFlags[(int)c3] = true;
			}
		}

		public static bool[] GetCharEscapeFlags(StringEscapeHandling stringEscapeHandling, char quoteChar)
		{
			if (stringEscapeHandling == StringEscapeHandling.EscapeHtml)
			{
				return JavaScriptUtils.HtmlCharEscapeFlags;
			}
			if (quoteChar == '"')
			{
				return JavaScriptUtils.DoubleQuoteCharEscapeFlags;
			}
			return JavaScriptUtils.SingleQuoteCharEscapeFlags;
		}

		public static bool ShouldEscapeJavaScriptString(string s, bool[] charEscapeFlags)
		{
			if (s == null)
			{
				return false;
			}
			foreach (char c in s)
			{
				if ((int)c >= charEscapeFlags.Length || charEscapeFlags[(int)c])
				{
					return true;
				}
			}
			return false;
		}

		public static void WriteEscapedJavaScriptString(TextWriter writer, string s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, IArrayPool<char> bufferPool, ref char[] writeBuffer)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (!string.IsNullOrEmpty(s))
			{
				int num = JavaScriptUtils.FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
				if (num == -1)
				{
					writer.Write(s);
				}
				else
				{
					if (num != 0)
					{
						if (writeBuffer == null || writeBuffer.Length < num)
						{
							writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, num, writeBuffer);
						}
						s.CopyTo(0, writeBuffer, 0, num);
						writer.Write(writeBuffer, 0, num);
					}
					int num2;
					for (int i = num; i < s.Length; i++)
					{
						char c = s[i];
						if ((int)c >= charEscapeFlags.Length || charEscapeFlags[(int)c])
						{
							string text;
							if (c <= '\\')
							{
								switch (c)
								{
								case '\b':
									text = JavaScriptUtils.getString_0(107341829);
									break;
								case '\t':
									text = JavaScriptUtils.getString_0(107341817);
									break;
								case '\n':
									text = JavaScriptUtils.getString_0(107341844);
									break;
								case '\v':
									goto IL_15B;
								case '\f':
									text = JavaScriptUtils.getString_0(107341834);
									break;
								case '\r':
									text = JavaScriptUtils.getString_0(107341839);
									break;
								default:
									if (c != '\\')
									{
										goto IL_15B;
									}
									text = JavaScriptUtils.getString_0(107342304);
									break;
								}
							}
							else if (c != '\u0085')
							{
								if (c != '\u2028')
								{
									if (c != '\u2029')
									{
										goto IL_15B;
									}
									text = JavaScriptUtils.getString_0(107342313);
								}
								else
								{
									text = JavaScriptUtils.getString_0(107342322);
								}
							}
							else
							{
								text = JavaScriptUtils.getString_0(107342299);
							}
							IL_21C:
							if (text == null)
							{
								goto IL_2C7;
							}
							bool flag = string.Equals(text, JavaScriptUtils.getString_0(107342262));
							if (i > num)
							{
								num2 = i - num + (flag ? 6 : 0);
								int num3 = flag ? 6 : 0;
								if (writeBuffer == null || writeBuffer.Length < num2)
								{
									char[] array = BufferUtils.RentBuffer(bufferPool, num2);
									if (flag)
									{
										Array.Copy(writeBuffer, array, 6);
									}
									BufferUtils.ReturnBuffer(bufferPool, writeBuffer);
									writeBuffer = array;
								}
								s.CopyTo(num, writeBuffer, num3, num2 - num3);
								writer.Write(writeBuffer, num3, num2 - num3);
							}
							num = i + 1;
							if (!flag)
							{
								writer.Write(text);
								goto IL_2C7;
							}
							writer.Write(writeBuffer, 0, 6);
							goto IL_2C7;
							IL_15B:
							if ((int)c >= charEscapeFlags.Length)
							{
								if (stringEscapeHandling != StringEscapeHandling.EscapeNonAscii)
								{
									text = null;
									goto IL_21C;
								}
							}
							if (c == '\'' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
							{
								text = JavaScriptUtils.getString_0(107342272);
								goto IL_21C;
							}
							if (c == '"' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
							{
								text = JavaScriptUtils.getString_0(107342267);
								goto IL_21C;
							}
							if (writeBuffer == null || writeBuffer.Length < 6)
							{
								writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, 6, writeBuffer);
							}
							StringUtils.ToCharAsUnicode(c, writeBuffer);
							text = JavaScriptUtils.getString_0(107342262);
							goto IL_21C;
						}
						IL_2C7:;
					}
					num2 = s.Length - num;
					if (num2 > 0)
					{
						if (writeBuffer == null || writeBuffer.Length < num2)
						{
							writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, num2, writeBuffer);
						}
						s.CopyTo(num, writeBuffer, 0, num2);
						writer.Write(writeBuffer, 0, num2);
					}
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters, StringEscapeHandling stringEscapeHandling)
		{
			bool[] charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(stringEscapeHandling, delimiter);
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter((value != null) ? value.Length : 16))
			{
				char[] array = null;
				JavaScriptUtils.WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters, charEscapeFlags, stringEscapeHandling, null, ref array);
				result = stringWriter.ToString();
			}
			return result;
		}

		private static int FirstCharToEscape(string s, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling)
		{
			for (int num = 0; num != s.Length; num++)
			{
				char c = s[num];
				if ((int)c < charEscapeFlags.Length)
				{
					if (charEscapeFlags[(int)c])
					{
						return num;
					}
				}
				else
				{
					if (stringEscapeHandling == StringEscapeHandling.EscapeNonAscii)
					{
						return num;
					}
					if (c != '\u0085' && c != '\u2028')
					{
						if (c != '\u2029')
						{
							goto IL_39;
						}
					}
					return num;
				}
				IL_39:;
			}
			return -1;
		}

		public static Task WriteEscapedJavaScriptStringAsync(TextWriter writer, string s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (appendDelimiters)
			{
				return JavaScriptUtils.WriteEscapedJavaScriptStringWithDelimitersAsync(writer, s, delimiter, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			if (string.IsNullOrEmpty(s))
			{
				return cancellationToken.CancelIfRequestedAsync() ?? AsyncUtils.CompletedTask;
			}
			return JavaScriptUtils.WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
		}

		private static Task WriteEscapedJavaScriptStringWithDelimitersAsync(TextWriter writer, string s, char delimiter, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			Task task = writer.WriteAsync(delimiter, cancellationToken);
			if (!task.IsCompletedSucessfully())
			{
				return JavaScriptUtils.WriteEscapedJavaScriptStringWithDelimitersAsync(task, writer, s, delimiter, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			if (!string.IsNullOrEmpty(s))
			{
				task = JavaScriptUtils.WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
				if (task.IsCompletedSucessfully())
				{
					return writer.WriteAsync(delimiter, cancellationToken);
				}
			}
			return JavaScriptUtils.WriteCharAsync(task, writer, delimiter, cancellationToken);
		}

		private static async Task WriteEscapedJavaScriptStringWithDelimitersAsync(Task task, TextWriter writer, string s, char delimiter, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			if (!string.IsNullOrEmpty(s))
			{
				await JavaScriptUtils.WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken).ConfigureAwait(false);
			}
			await writer.WriteAsync(delimiter).ConfigureAwait(false);
		}

		public static async Task WriteCharAsync(Task task, TextWriter writer, char c, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = task.ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
			configuredTaskAwaiter = writer.WriteAsync(c, cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
			}
			configuredTaskAwaiter.GetResult();
		}

		private static Task WriteEscapedJavaScriptStringWithoutDelimitersAsync(TextWriter writer, string s, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			int num = JavaScriptUtils.FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
			if (num != -1)
			{
				return JavaScriptUtils.WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, num, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			return writer.WriteAsync(s, cancellationToken);
		}

		private static async Task WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync(TextWriter writer, string s, int lastWritePosition, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			if (writeBuffer == null || writeBuffer.Length < lastWritePosition)
			{
				writeBuffer = client.EnsureWriteBuffer(lastWritePosition, 6);
			}
			if (lastWritePosition != 0)
			{
				s.CopyTo(0, writeBuffer, 0, lastWritePosition);
				await writer.WriteAsync(writeBuffer, 0, lastWritePosition, cancellationToken).ConfigureAwait(false);
			}
			bool isEscapedUnicodeText = false;
			string escapedValue = null;
			int num;
			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			for (int i = lastWritePosition; i < s.Length; i++)
			{
				char c = s[i];
				if ((int)c >= charEscapeFlags.Length || charEscapeFlags[(int)c])
				{
					if (c <= '\\')
					{
						switch (c)
						{
						case '\b':
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107341863);
							goto IL_386;
						case '\t':
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107341851);
							goto IL_386;
						case '\n':
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107341878);
							goto IL_386;
						case '\v':
							break;
						case '\f':
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107341868);
							goto IL_386;
						case '\r':
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107341873);
							goto IL_386;
						default:
							if (c == '\\')
							{
								escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107342338);
								goto IL_386;
							}
							break;
						}
					}
					else
					{
						if (c == '\u0085')
						{
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107342333);
							goto IL_386;
						}
						if (c == '\u2028')
						{
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107342356);
							goto IL_386;
						}
						if (c == '\u2029')
						{
							escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107342347);
							goto IL_386;
						}
					}
					if ((int)c >= charEscapeFlags.Length && stringEscapeHandling != StringEscapeHandling.EscapeNonAscii)
					{
						goto IL_489;
					}
					if (c == '\'' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
					{
						escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107342306);
					}
					else if (c == '"' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
					{
						escapedValue = JavaScriptUtils.<WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync>d__16.getString_0(107342301);
					}
					else
					{
						if (writeBuffer.Length < 6)
						{
							writeBuffer = client.EnsureWriteBuffer(6, 0);
						}
						StringUtils.ToCharAsUnicode(c, writeBuffer);
						isEscapedUnicodeText = true;
					}
					IL_386:
					if (i > lastWritePosition)
					{
						num = i - lastWritePosition + (isEscapedUnicodeText ? 6 : 0);
						int num2 = isEscapedUnicodeText ? 6 : 0;
						if (writeBuffer.Length < num)
						{
							writeBuffer = client.EnsureWriteBuffer(num, 6);
						}
						s.CopyTo(lastWritePosition, writeBuffer, num2, num - num2);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = writer.WriteAsync(writeBuffer, num2, num - num2, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
					}
					lastWritePosition = i + 1;
					if (!isEscapedUnicodeText)
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = writer.WriteAsync(escapedValue, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
					}
					else
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = writer.WriteAsync(writeBuffer, 0, 6, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						}
						configuredTaskAwaiter.GetResult();
						isEscapedUnicodeText = false;
					}
				}
				IL_489:;
			}
			num = s.Length - lastWritePosition;
			if (num != 0)
			{
				if (writeBuffer.Length < num)
				{
					writeBuffer = client.EnsureWriteBuffer(num, 0);
				}
				s.CopyTo(lastWritePosition, writeBuffer, 0, num);
				ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter = writer.WriteAsync(writeBuffer, 0, num, cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
				}
				configuredTaskAwaiter.GetResult();
			}
		}

		public static bool TryGetDateFromConstructorJson(JsonReader reader, out DateTime dateTime, out string errorMessage)
		{
			dateTime = default(DateTime);
			errorMessage = null;
			long? num;
			if (!JavaScriptUtils.TryGetDateConstructorValue(reader, out num, out errorMessage) || num == null)
			{
				errorMessage = (errorMessage ?? JavaScriptUtils.getString_0(107342289));
				return false;
			}
			long? num2;
			if (!JavaScriptUtils.TryGetDateConstructorValue(reader, out num2, out errorMessage))
			{
				return false;
			}
			if (num2 != null)
			{
				List<long> list = new List<long>
				{
					num.Value,
					num2.Value
				};
				long? num3;
				while (JavaScriptUtils.TryGetDateConstructorValue(reader, out num3, out errorMessage))
				{
					if (num3 != null)
					{
						list.Add(num3.Value);
					}
					else
					{
						if (list.Count > 7)
						{
							errorMessage = JavaScriptUtils.getString_0(107342208);
							return false;
						}
						while (list.Count < 7)
						{
							list.Add(0L);
						}
						dateTime = new DateTime((int)list[0], (int)list[1] + 1, (list[2] == 0L) ? 1 : ((int)list[2]), (int)list[3], (int)list[4], (int)list[5], (int)list[6]);
						return true;
					}
				}
				return false;
			}
			dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(num.Value);
			return true;
		}

		private static bool TryGetDateConstructorValue(JsonReader reader, out long? integer, out string errorMessage)
		{
			integer = null;
			errorMessage = null;
			if (!reader.Read())
			{
				errorMessage = JavaScriptUtils.getString_0(107342155);
				return false;
			}
			if (reader.TokenType == JsonToken.EndConstructor)
			{
				return true;
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				errorMessage = JavaScriptUtils.getString_0(107342094) + reader.TokenType;
				return false;
			}
			integer = new long?((long)reader.Value);
			return true;
		}

		internal static readonly bool[] SingleQuoteCharEscapeFlags;

		internal static readonly bool[] DoubleQuoteCharEscapeFlags;

		internal static readonly bool[] HtmlCharEscapeFlags;

		private const int UnicodeTextLength = 6;

		private const string EscapedUnicodeText = "!";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
