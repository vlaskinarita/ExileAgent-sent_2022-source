using System;

namespace PusherClient
{
	internal interface IChannelDataDecrypter
	{
		string DecryptData(byte[] decryptionKey, EncryptedChannelData encryptedData);
	}
}
