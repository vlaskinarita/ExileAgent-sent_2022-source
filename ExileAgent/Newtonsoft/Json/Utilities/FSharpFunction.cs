using System;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class FSharpFunction
	{
		public FSharpFunction(object instance, MethodCall<object, object> invoker)
		{
			this._instance = instance;
			this._invoker = invoker;
		}

		public object Invoke(params object[] args)
		{
			return this._invoker(this._instance, args);
		}

		private readonly object _instance;

		private readonly MethodCall<object, object> _invoker;
	}
}
