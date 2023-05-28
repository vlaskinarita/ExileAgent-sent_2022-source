﻿using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebSocketSharp.Net
{
	[Serializable]
	public sealed class CookieException : FormatException, ISerializable
	{
		internal CookieException(string message) : base(message)
		{
		}

		internal CookieException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CookieException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		public CookieException()
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
