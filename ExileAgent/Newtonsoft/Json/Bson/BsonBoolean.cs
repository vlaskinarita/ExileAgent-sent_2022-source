using System;

namespace Newtonsoft.Json.Bson
{
	internal sealed class BsonBoolean : BsonValue
	{
		private BsonBoolean(bool value) : base(value, BsonType.Boolean)
		{
		}

		public static readonly BsonBoolean False = new BsonBoolean(false);

		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
