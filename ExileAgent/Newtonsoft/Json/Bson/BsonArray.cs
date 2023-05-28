using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	internal sealed class BsonArray : BsonToken, IEnumerable, IEnumerable<BsonToken>
	{
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
