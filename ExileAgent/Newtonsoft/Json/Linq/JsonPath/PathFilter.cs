using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal abstract class PathFilter
	{
		public abstract IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch);

		protected static JToken GetTokenIndex(JToken t, bool errorWhenNoMatch, int index)
		{
			JArray jarray;
			JConstructor jconstructor;
			if ((jarray = (t as JArray)) != null)
			{
				if (jarray.Count > index)
				{
					return jarray[index];
				}
				if (errorWhenNoMatch)
				{
					throw new JsonException(PathFilter.getString_0(107290708).FormatWith(CultureInfo.InvariantCulture, index));
				}
				return null;
			}
			else if ((jconstructor = (t as JConstructor)) != null)
			{
				if (jconstructor.Count > index)
				{
					return jconstructor[index];
				}
				if (errorWhenNoMatch)
				{
					throw new JsonException(PathFilter.getString_0(107290143).FormatWith(CultureInfo.InvariantCulture, index));
				}
				return null;
			}
			else
			{
				if (errorWhenNoMatch)
				{
					throw new JsonException(PathFilter.getString_0(107290082).FormatWith(CultureInfo.InvariantCulture, index, t.GetType().Name));
				}
				return null;
			}
		}

		protected static JToken GetNextScanValue(JToken originalParent, JToken container, JToken value)
		{
			if (container == null || !container.HasValues)
			{
				while (value != null && value != originalParent)
				{
					if (value != value.Parent.Last)
					{
						break;
					}
					value = value.Parent;
				}
				if (value != null)
				{
					if (value != originalParent)
					{
						return value.Next;
					}
				}
				return null;
			}
			value = container.First;
			return value;
		}

		static PathFilter()
		{
			Strings.CreateGetStringDelegate(typeof(PathFilter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
