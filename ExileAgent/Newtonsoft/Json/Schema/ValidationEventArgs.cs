using System;
using Newtonsoft.Json.Utilities;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public sealed class ValidationEventArgs : EventArgs
	{
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, ValidationEventArgs.getString_0(107295249));
			this._ex = ex;
		}

		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		static ValidationEventArgs()
		{
			Strings.CreateGetStringDelegate(typeof(ValidationEventArgs));
		}

		private readonly JsonSchemaException _ex;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
