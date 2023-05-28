using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class OperationTimeoutException : PusherException
	{
		public OperationTimeoutException(TimeSpan timeoutPeriod, string operation) : base(string.Format(OperationTimeoutException.getString_0(107311324), operation, timeoutPeriod.TotalSeconds), ErrorCodes.ClientTimeout)
		{
		}

		static OperationTimeoutException()
		{
			Strings.CreateGetStringDelegate(typeof(OperationTimeoutException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
