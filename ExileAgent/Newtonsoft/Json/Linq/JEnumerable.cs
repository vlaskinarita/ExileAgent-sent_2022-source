using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;
using ns20;

namespace Newtonsoft.Json.Linq
{
	public readonly struct JEnumerable<T> : IEnumerable<!0>, IEnumerable, IJEnumerable<T>, IEquatable<JEnumerable<T>> where T : JToken
	{
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, Class401.smethod_0(107286997));
			this._enumerable = enumerable;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return (this._enumerable ?? JEnumerable<T>.Empty).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				if (this._enumerable == null)
				{
					return JEnumerable<JToken>.Empty;
				}
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		public bool Equals(JEnumerable<T> other)
		{
			return object.Equals(this._enumerable, other._enumerable);
		}

		public override bool Equals(object obj)
		{
			if (obj is JEnumerable<T>)
			{
				JEnumerable<T> other = (JEnumerable<T>)obj;
				return this.Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (this._enumerable == null)
			{
				return 0;
			}
			return this._enumerable.GetHashCode();
		}

		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		private readonly IEnumerable<T> _enumerable;
	}
}
