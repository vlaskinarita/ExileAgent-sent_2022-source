using System;

namespace PusherClient
{
	public interface ISerializeObjectsToJson
	{
		string Serialize(object objectToSerialize);
	}
}
