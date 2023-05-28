using System;

namespace Newtonsoft.Json.Bson
{
	internal sealed class BsonEmpty : BsonToken
	{
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		public override BsonType Type { get; }

		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
