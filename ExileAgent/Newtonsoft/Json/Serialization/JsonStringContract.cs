using System;

namespace Newtonsoft.Json.Serialization
{
	public sealed class JsonStringContract : JsonPrimitiveContract
	{
		public JsonStringContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
