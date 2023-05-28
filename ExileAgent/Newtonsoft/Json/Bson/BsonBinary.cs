using System;

namespace Newtonsoft.Json.Bson
{
	internal sealed class BsonBinary : BsonValue
	{
		public BsonBinaryType BinaryType { get; set; }

		public BsonBinary(byte[] value, BsonBinaryType binaryType) : base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
