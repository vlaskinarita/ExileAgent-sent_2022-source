using System;

namespace Newtonsoft.Json.Bson
{
	internal class BsonValue : BsonToken
	{
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		public object Value
		{
			get
			{
				return this._value;
			}
		}

		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		private readonly object _value;

		private readonly BsonType _type;
	}
}
