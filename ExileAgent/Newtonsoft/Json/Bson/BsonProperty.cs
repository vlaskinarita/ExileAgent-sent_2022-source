using System;

namespace Newtonsoft.Json.Bson
{
	internal sealed class BsonProperty
	{
		public BsonString Name { get; set; }

		public BsonToken Value { get; set; }
	}
}
