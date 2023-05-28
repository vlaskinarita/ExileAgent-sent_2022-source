using System;

namespace Newtonsoft.Json.Serialization
{
	public sealed class JsonISerializableContract : JsonContainerContract
	{
		public ObjectConstructor<object> ISerializableCreator { get; set; }

		public JsonISerializableContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
