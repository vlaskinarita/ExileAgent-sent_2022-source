using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	internal sealed class BsonObject : BsonToken, IEnumerable, IEnumerable<BsonProperty>
	{
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
