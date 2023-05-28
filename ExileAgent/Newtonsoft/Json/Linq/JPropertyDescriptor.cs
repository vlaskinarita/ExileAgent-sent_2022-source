using System;
using System.ComponentModel;

namespace Newtonsoft.Json.Linq
{
	public sealed class JPropertyDescriptor : PropertyDescriptor
	{
		public JPropertyDescriptor(string name) : base(name, null)
		{
		}

		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override object GetValue(object component)
		{
			JObject jobject = component as JObject;
			if (jobject == null)
			{
				return null;
			}
			return jobject[this.Name];
		}

		public override void ResetValue(object component)
		{
		}

		public override void SetValue(object component, object value)
		{
			JObject jobject;
			if ((jobject = (component as JObject)) != null)
			{
				JToken value2 = (value as JToken) ?? new JValue(value);
				jobject[this.Name] = value2;
			}
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
