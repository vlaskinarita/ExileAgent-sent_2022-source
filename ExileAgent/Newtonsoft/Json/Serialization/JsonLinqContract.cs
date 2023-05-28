using System;

namespace Newtonsoft.Json.Serialization
{
	public sealed class JsonLinqContract : JsonContract
	{
		public JsonLinqContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
