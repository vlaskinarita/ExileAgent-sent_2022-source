﻿using System;
using Newtonsoft.Json;

namespace PusherClient
{
	internal sealed class EncryptedChannelData
	{
		public static EncryptedChannelData CreateFromJson(string jsonData)
		{
			return JsonConvert.DeserializeObject<EncryptedChannelData>(jsonData);
		}

		public string nonce { get; set; }

		public string ciphertext { get; set; }
	}
}
