using System;
using System.Collections.Generic;
using System.Globalization;
using ns20;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class BidirectionalDictionary<TFirst, TSecond>
	{
		public BidirectionalDictionary() : this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer) : this(firstEqualityComparer, secondEqualityComparer, Class401.smethod_0(107340130), Class401.smethod_0(107340130))
		{
		}

		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		public void Set(TFirst first, TSecond second)
		{
			TSecond tsecond;
			if (this._firstToSecond.TryGetValue(first, out tsecond) && !tsecond.Equals(second))
			{
				throw new ArgumentException(this._duplicateFirstErrorMessage.FormatWith(CultureInfo.InvariantCulture, first));
			}
			TFirst tfirst;
			if (this._secondToFirst.TryGetValue(second, out tfirst) && !tfirst.Equals(first))
			{
				throw new ArgumentException(this._duplicateSecondErrorMessage.FormatWith(CultureInfo.InvariantCulture, second));
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		public bool TryGetByFirst(TFirst first, out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, out second);
		}

		public bool TryGetBySecond(TSecond second, out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, out first);
		}

		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		private readonly string _duplicateFirstErrorMessage;

		private readonly string _duplicateSecondErrorMessage;
	}
}
