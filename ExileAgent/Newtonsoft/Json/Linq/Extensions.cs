using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public static class Extensions
	{
		public static IJEnumerable<JToken> Ancestors<T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, Extensions.getString_0(107244546));
			return source.SelectMany((T j) => j.Ancestors()).AsJEnumerable();
		}

		public static IJEnumerable<JToken> AncestorsAndSelf<T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, Extensions.getString_0(107244546));
			return source.SelectMany((T j) => j.AncestorsAndSelf()).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Descendants<T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, Extensions.getString_0(107244546));
			return source.SelectMany((T j) => j.Descendants()).AsJEnumerable();
		}

		public static IJEnumerable<JToken> DescendantsAndSelf<T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, Extensions.getString_0(107244546));
			return source.SelectMany((T j) => j.DescendantsAndSelf()).AsJEnumerable();
		}

		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, Extensions.getString_0(107244546));
			return source.SelectMany((JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		public static U Value<U>(this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		public static U Value<T, U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, Extensions.getString_0(107454203));
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException(Extensions.getString_0(107295252));
			}
			return jtoken.Convert<JToken, U>();
		}

		internal static IEnumerable<U> Values<T, U>(this IEnumerable<T> source, object key) where T : JToken
		{
			Extensions.<Values>d__11<T, U> <Values>d__ = new Extensions.<Values>d__11<T, U>(-2);
			<Values>d__.<>3__source = source;
			<Values>d__.<>3__key = key;
			return <Values>d__;
		}

		public static IJEnumerable<JToken> Children<T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		public static IEnumerable<U> Children<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, Extensions.getString_0(107244546));
			return source.SelectMany((T c) => c.Children()).Convert<JToken, U>();
		}

		internal static IEnumerable<U> Convert<T, U>(this IEnumerable<T> source) where T : JToken
		{
			Extensions.<Convert>d__14<T, U> <Convert>d__ = new Extensions.<Convert>d__14<T, U>(-2);
			<Convert>d__.<>3__source = source;
			return <Convert>d__;
		}

		internal static U Convert<T, U>(this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U)
			{
				U result = (U)((object)token);
				if (typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
				{
					return result;
				}
			}
			JValue jvalue;
			if ((jvalue = (token as JValue)) == null)
			{
				throw new InvalidCastException(Extensions.getString_0(107295691).FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			object value;
			if ((value = jvalue.Value) is U)
			{
				return (U)((object)value);
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jvalue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)((object)System.Convert.ChangeType(jvalue.Value, type, CultureInfo.InvariantCulture));
		}

		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		public static IJEnumerable<T> AsJEnumerable<T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			IJEnumerable<T> result;
			if ((result = (source as IJEnumerable<T>)) != null)
			{
				return result;
			}
			return new JEnumerable<T>(source);
		}

		static Extensions()
		{
			Strings.CreateGetStringDelegate(typeof(Extensions));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
