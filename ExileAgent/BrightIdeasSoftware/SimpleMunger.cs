using System;
using System.Reflection;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class SimpleMunger
	{
		public SimpleMunger(string aspectName)
		{
			this.aspectName = aspectName;
		}

		public string AspectName
		{
			get
			{
				return this.aspectName;
			}
		}

		public object GetValue(object target)
		{
			if (target == null)
			{
				return null;
			}
			this.ResolveName(target, this.AspectName, 0);
			try
			{
				if (this.resolvedPropertyInfo != null)
				{
					return this.resolvedPropertyInfo.GetValue(target, null);
				}
				if (this.resolvedMethodInfo != null)
				{
					return this.resolvedMethodInfo.Invoke(target, null);
				}
				if (this.resolvedFieldInfo != null)
				{
					return this.resolvedFieldInfo.GetValue(target);
				}
				if (this.indexerPropertyInfo != null)
				{
					return this.indexerPropertyInfo.GetValue(target, new object[]
					{
						this.AspectName
					});
				}
			}
			catch (Exception ex)
			{
				throw new MungerException(this, target, ex);
			}
			throw new MungerException(this, target, new MissingMethodException());
		}

		public bool PutValue(object target, object value)
		{
			if (target == null)
			{
				return false;
			}
			this.ResolveName(target, this.AspectName, 1);
			try
			{
				if (this.resolvedPropertyInfo != null)
				{
					this.resolvedPropertyInfo.SetValue(target, value, null);
					return true;
				}
				if (this.resolvedMethodInfo != null)
				{
					this.resolvedMethodInfo.Invoke(target, new object[]
					{
						value
					});
					return true;
				}
				if (this.resolvedFieldInfo != null)
				{
					this.resolvedFieldInfo.SetValue(target, value);
					return true;
				}
				if (this.indexerPropertyInfo != null)
				{
					this.indexerPropertyInfo.SetValue(target, value, new object[]
					{
						this.AspectName
					});
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new MungerException(this, target, ex);
			}
			return false;
		}

		private void ResolveName(object target, string name, int numberMethodParameters)
		{
			if (this.cachedTargetType == target.GetType() && this.cachedName == name && this.cachedNumberParameters == numberMethodParameters)
			{
				return;
			}
			this.cachedTargetType = target.GetType();
			this.cachedName = name;
			this.cachedNumberParameters = numberMethodParameters;
			this.resolvedFieldInfo = null;
			this.resolvedPropertyInfo = null;
			this.resolvedMethodInfo = null;
			this.indexerPropertyInfo = null;
			foreach (PropertyInfo propertyInfo in target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.Name == name)
				{
					this.resolvedPropertyInfo = propertyInfo;
					return;
				}
				if (this.indexerPropertyInfo == null && propertyInfo.Name == SimpleMunger.getString_0(107243460))
				{
					ParameterInfo[] parameters = propertyInfo.GetGetMethod().GetParameters();
					if (parameters.Length > 0)
					{
						Type parameterType = parameters[0].ParameterType;
						if (parameterType == typeof(string) || parameterType == typeof(object))
						{
							this.indexerPropertyInfo = propertyInfo;
						}
					}
				}
			}
			foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (fieldInfo.Name == name)
				{
					this.resolvedFieldInfo = fieldInfo;
					return;
				}
			}
			foreach (MethodInfo methodInfo in target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
			{
				if (methodInfo.Name == name && methodInfo.GetParameters().Length == numberMethodParameters)
				{
					this.resolvedMethodInfo = methodInfo;
					break;
				}
			}
		}

		static SimpleMunger()
		{
			Strings.CreateGetStringDelegate(typeof(SimpleMunger));
		}

		private readonly string aspectName;

		private Type cachedTargetType;

		private string cachedName;

		private int cachedNumberParameters;

		private FieldInfo resolvedFieldInfo;

		private PropertyInfo resolvedPropertyInfo;

		private MethodInfo resolvedMethodInfo;

		private PropertyInfo indexerPropertyInfo;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
