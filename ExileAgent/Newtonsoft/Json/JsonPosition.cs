using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json
{
	internal struct JsonPosition
	{
		public JsonPosition(JsonContainerType type)
		{
			this.Type = type;
			this.HasIndex = JsonPosition.TypeHasIndex(type);
			this.Position = -1;
			this.PropertyName = null;
		}

		internal int CalculateLength()
		{
			JsonContainerType type = this.Type;
			if (type == JsonContainerType.Object)
			{
				return this.PropertyName.Length + 5;
			}
			if (type - JsonContainerType.Array > 1)
			{
				throw new ArgumentOutOfRangeException(JsonPosition.getString_0(107252613));
			}
			return MathUtils.IntLength((ulong)((long)this.Position)) + 2;
		}

		internal void WriteTo(StringBuilder sb, ref StringWriter writer, ref char[] buffer)
		{
			JsonContainerType type = this.Type;
			if (type != JsonContainerType.Object)
			{
				if (type - JsonContainerType.Array > 1)
				{
					return;
				}
				sb.Append('[');
				sb.Append(this.Position);
				sb.Append(']');
				return;
			}
			else
			{
				string propertyName = this.PropertyName;
				if (propertyName.IndexOfAny(JsonPosition.SpecialCharacters) != -1)
				{
					sb.Append(JsonPosition.getString_0(107351449));
					if (writer == null)
					{
						writer = new StringWriter(sb);
					}
					JavaScriptUtils.WriteEscapedJavaScriptString(writer, propertyName, '\'', false, JavaScriptUtils.SingleQuoteCharEscapeFlags, StringEscapeHandling.Default, null, ref buffer);
					sb.Append(JsonPosition.getString_0(107240343));
					return;
				}
				if (sb.Length > 0)
				{
					sb.Append('.');
				}
				sb.Append(propertyName);
				return;
			}
		}

		internal static bool TypeHasIndex(JsonContainerType type)
		{
			return type == JsonContainerType.Array || type == JsonContainerType.Constructor;
		}

		internal static string BuildPath(List<JsonPosition> positions, JsonPosition? currentPosition)
		{
			int num = 0;
			if (positions != null)
			{
				for (int i = 0; i < positions.Count; i++)
				{
					num += positions[i].CalculateLength();
				}
			}
			if (currentPosition != null)
			{
				num += currentPosition.GetValueOrDefault().CalculateLength();
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			StringWriter stringWriter = null;
			char[] array = null;
			if (positions != null)
			{
				foreach (JsonPosition jsonPosition in positions)
				{
					jsonPosition.WriteTo(stringBuilder, ref stringWriter, ref array);
				}
			}
			if (currentPosition != null)
			{
				currentPosition.GetValueOrDefault().WriteTo(stringBuilder, ref stringWriter, ref array);
			}
			return stringBuilder.ToString();
		}

		internal static string FormatMessage(IJsonLineInfo lineInfo, string path, string message)
		{
			if (!message.EndsWith(Environment.NewLine, StringComparison.Ordinal))
			{
				message = message.Trim();
				if (!message.EndsWith('.'))
				{
					message += JsonPosition.getString_0(107372315);
				}
				message += JsonPosition.getString_0(107400298);
			}
			message += JsonPosition.getString_0(107351476).FormatWith(CultureInfo.InvariantCulture, path);
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				message += JsonPosition.getString_0(107351427).FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);
			}
			message += JsonPosition.getString_0(107372315);
			return message;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static JsonPosition()
		{
			Strings.CreateGetStringDelegate(typeof(JsonPosition));
			JsonPosition.SpecialCharacters = new char[]
			{
				'.',
				' ',
				'\'',
				'/',
				'"',
				'[',
				']',
				'(',
				')',
				'\t',
				'\n',
				'\r',
				'\f',
				'\b',
				'\\',
				'\u0085',
				'\u2028',
				'\u2029'
			};
		}

		private static readonly char[] SpecialCharacters;

		internal JsonContainerType Type;

		internal int Position;

		internal string PropertyName;

		internal bool HasIndex;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
