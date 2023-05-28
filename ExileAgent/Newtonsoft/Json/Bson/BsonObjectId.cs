using System;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Bson
{
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public sealed class BsonObjectId
	{
		public byte[] Value { get; }

		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, BsonObjectId.getString_0(107454713));
			if (value.Length != 12)
			{
				throw new ArgumentException(BsonObjectId.getString_0(107318409), BsonObjectId.getString_0(107454713));
			}
			this.Value = value;
		}

		static BsonObjectId()
		{
			Strings.CreateGetStringDelegate(typeof(BsonObjectId));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
