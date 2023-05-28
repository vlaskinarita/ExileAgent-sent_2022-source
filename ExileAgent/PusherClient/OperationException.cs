using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class OperationException : PusherException
	{
		public OperationException(ErrorCodes code, string operation, Exception innerException) : base(string.Concat(new string[]
		{
			OperationException.getString_0(107312787),
			operation,
			OperationException.getString_0(107311360),
			Environment.NewLine,
			innerException.Message
		}), code, innerException)
		{
		}

		static OperationException()
		{
			Strings.CreateGetStringDelegate(typeof(OperationException));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
