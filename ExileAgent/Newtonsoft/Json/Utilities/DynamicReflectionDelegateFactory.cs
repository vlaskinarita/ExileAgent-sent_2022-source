using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class DynamicReflectionDelegateFactory : ReflectionDelegateFactory
	{
		internal static DynamicReflectionDelegateFactory Instance { get; }

		private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
		{
			if (owner.IsInterface())
			{
				return new DynamicMethod(name, returnType, parameterTypes, owner.Module, true);
			}
			return new DynamicMethod(name, returnType, parameterTypes, owner, true);
		}

		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(method.ToString(), typeof(object), new Type[]
			{
				typeof(object[])
			}, method.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateMethodCallIL(method, ilgenerator, 0);
			return (ObjectConstructor<object>)dynamicMethod.CreateDelegate(typeof(ObjectConstructor<object>));
		}

		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(method.ToString(), typeof(object), new Type[]
			{
				typeof(object),
				typeof(object[])
			}, method.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateMethodCallIL(method, ilgenerator, 1);
			return (MethodCall<T, object>)dynamicMethod.CreateDelegate(typeof(MethodCall<T, object>));
		}

		private void GenerateCreateMethodCallIL(MethodBase method, ILGenerator generator, int argsIndex)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Label label = generator.DefineLabel();
			generator.Emit(OpCodes.Ldarg, argsIndex);
			generator.Emit(OpCodes.Ldlen);
			generator.Emit(OpCodes.Ldc_I4, parameters.Length);
			generator.Emit(OpCodes.Beq, label);
			generator.Emit(OpCodes.Newobj, typeof(TargetParameterCountException).GetConstructor(ReflectionUtils.EmptyTypes));
			generator.Emit(OpCodes.Throw);
			generator.MarkLabel(label);
			if (!method.IsConstructor && !method.IsStatic)
			{
				generator.PushInstance(method.DeclaringType);
			}
			LocalBuilder local = generator.DeclareLocal(typeof(IConvertible));
			LocalBuilder local2 = generator.DeclareLocal(typeof(object));
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
					LocalBuilder local3 = generator.DeclareLocal(type);
					if (!parameterInfo.IsOut)
					{
						generator.PushArrayInstance(argsIndex, i);
						if (type.IsValueType())
						{
							Label label2 = generator.DefineLabel();
							Label label3 = generator.DefineLabel();
							generator.Emit(OpCodes.Brtrue_S, label2);
							generator.Emit(OpCodes.Ldloca_S, local3);
							generator.Emit(OpCodes.Initobj, type);
							generator.Emit(OpCodes.Br_S, label3);
							generator.MarkLabel(label2);
							generator.PushArrayInstance(argsIndex, i);
							generator.UnboxIfNeeded(type);
							generator.Emit(OpCodes.Stloc_S, local3);
							generator.MarkLabel(label3);
						}
						else
						{
							generator.UnboxIfNeeded(type);
							generator.Emit(OpCodes.Stloc_S, local3);
						}
					}
					generator.Emit(OpCodes.Ldloca_S, local3);
				}
				else if (type.IsValueType())
				{
					generator.PushArrayInstance(argsIndex, i);
					generator.Emit(OpCodes.Stloc_S, local2);
					Label label4 = generator.DefineLabel();
					Label label5 = generator.DefineLabel();
					generator.Emit(OpCodes.Ldloc_S, local2);
					generator.Emit(OpCodes.Brtrue_S, label4);
					LocalBuilder local4 = generator.DeclareLocal(type);
					generator.Emit(OpCodes.Ldloca_S, local4);
					generator.Emit(OpCodes.Initobj, type);
					generator.Emit(OpCodes.Ldloc_S, local4);
					generator.Emit(OpCodes.Br_S, label5);
					generator.MarkLabel(label4);
					if (type.IsPrimitive())
					{
						MethodInfo method2 = typeof(IConvertible).GetMethod(DynamicReflectionDelegateFactory.getString_1(107344646) + type.Name, new Type[]
						{
							typeof(IFormatProvider)
						});
						if (method2 != null)
						{
							Label label6 = generator.DefineLabel();
							generator.Emit(OpCodes.Ldloc_S, local2);
							generator.Emit(OpCodes.Isinst, type);
							generator.Emit(OpCodes.Brtrue_S, label6);
							generator.Emit(OpCodes.Ldloc_S, local2);
							generator.Emit(OpCodes.Isinst, typeof(IConvertible));
							generator.Emit(OpCodes.Stloc_S, local);
							generator.Emit(OpCodes.Ldloc_S, local);
							generator.Emit(OpCodes.Brfalse_S, label6);
							generator.Emit(OpCodes.Ldloc_S, local);
							generator.Emit(OpCodes.Ldnull);
							generator.Emit(OpCodes.Callvirt, method2);
							generator.Emit(OpCodes.Br_S, label5);
							generator.MarkLabel(label6);
						}
					}
					generator.Emit(OpCodes.Ldloc_S, local2);
					generator.UnboxIfNeeded(type);
					generator.MarkLabel(label5);
				}
				else
				{
					generator.PushArrayInstance(argsIndex, i);
					generator.UnboxIfNeeded(type);
				}
			}
			if (method.IsConstructor)
			{
				generator.Emit(OpCodes.Newobj, (ConstructorInfo)method);
			}
			else
			{
				generator.CallMethod((MethodInfo)method);
			}
			Type type2 = method.IsConstructor ? method.DeclaringType : ((MethodInfo)method).ReturnType;
			if (type2 != typeof(void))
			{
				generator.BoxIfNeeded(type2);
			}
			else
			{
				generator.Emit(OpCodes.Ldnull);
			}
			generator.Return();
		}

		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(DynamicReflectionDelegateFactory.getString_1(107344609) + type.FullName, typeof(T), ReflectionUtils.EmptyTypes, type);
			dynamicMethod.InitLocals = true;
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateDefaultConstructorIL(type, ilgenerator, typeof(T));
			return (Func<T>)dynamicMethod.CreateDelegate(typeof(Func<T>));
		}

		private void GenerateCreateDefaultConstructorIL(Type type, ILGenerator generator, Type delegateType)
		{
			if (type.IsValueType())
			{
				generator.DeclareLocal(type);
				generator.Emit(OpCodes.Ldloc_0);
				if (type != delegateType)
				{
					generator.Emit(OpCodes.Box, type);
				}
			}
			else
			{
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, ReflectionUtils.EmptyTypes, null);
				if (constructor == null)
				{
					throw new ArgumentException(DynamicReflectionDelegateFactory.getString_1(107344600).FormatWith(CultureInfo.InvariantCulture, type));
				}
				generator.Emit(OpCodes.Newobj, constructor);
			}
			generator.Return();
		}

		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(DynamicReflectionDelegateFactory.getString_1(107344583) + propertyInfo.Name, typeof(object), new Type[]
			{
				typeof(T)
			}, propertyInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateGetPropertyIL(propertyInfo, ilgenerator);
			return (Func<T, object>)dynamicMethod.CreateDelegate(typeof(Func<T, object>));
		}

		private void GenerateCreateGetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException(DynamicReflectionDelegateFactory.getString_1(107344578).FormatWith(CultureInfo.InvariantCulture, propertyInfo.Name));
			}
			if (!getMethod.IsStatic)
			{
				generator.PushInstance(propertyInfo.DeclaringType);
			}
			generator.CallMethod(getMethod);
			generator.BoxIfNeeded(propertyInfo.PropertyType);
			generator.Return();
		}

		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsLiteral)
			{
				object constantValue = fieldInfo.GetValue(null);
				return (T o) => constantValue;
			}
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(DynamicReflectionDelegateFactory.getString_1(107344583) + fieldInfo.Name, typeof(T), new Type[]
			{
				typeof(object)
			}, fieldInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			this.GenerateCreateGetFieldIL(fieldInfo, ilgenerator);
			return (Func<T, object>)dynamicMethod.CreateDelegate(typeof(Func<T, object>));
		}

		private void GenerateCreateGetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
		{
			if (!fieldInfo.IsStatic)
			{
				generator.PushInstance(fieldInfo.DeclaringType);
				generator.Emit(OpCodes.Ldfld, fieldInfo);
			}
			else
			{
				generator.Emit(OpCodes.Ldsfld, fieldInfo);
			}
			generator.BoxIfNeeded(fieldInfo.FieldType);
			generator.Return();
		}

		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(DynamicReflectionDelegateFactory.getString_1(107344013) + fieldInfo.Name, null, new Type[]
			{
				typeof(T),
				typeof(object)
			}, fieldInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			DynamicReflectionDelegateFactory.GenerateCreateSetFieldIL(fieldInfo, ilgenerator);
			return (Action<T, object>)dynamicMethod.CreateDelegate(typeof(Action<T, object>));
		}

		internal static void GenerateCreateSetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
		{
			if (!fieldInfo.IsStatic)
			{
				generator.PushInstance(fieldInfo.DeclaringType);
			}
			generator.Emit(OpCodes.Ldarg_1);
			generator.UnboxIfNeeded(fieldInfo.FieldType);
			if (!fieldInfo.IsStatic)
			{
				generator.Emit(OpCodes.Stfld, fieldInfo);
			}
			else
			{
				generator.Emit(OpCodes.Stsfld, fieldInfo);
			}
			generator.Return();
		}

		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			DynamicMethod dynamicMethod = DynamicReflectionDelegateFactory.CreateDynamicMethod(DynamicReflectionDelegateFactory.getString_1(107344013) + propertyInfo.Name, null, new Type[]
			{
				typeof(T),
				typeof(object)
			}, propertyInfo.DeclaringType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			DynamicReflectionDelegateFactory.GenerateCreateSetPropertyIL(propertyInfo, ilgenerator);
			return (Action<T, object>)dynamicMethod.CreateDelegate(typeof(Action<T, object>));
		}

		internal static void GenerateCreateSetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (!setMethod.IsStatic)
			{
				generator.PushInstance(propertyInfo.DeclaringType);
			}
			generator.Emit(OpCodes.Ldarg_1);
			generator.UnboxIfNeeded(propertyInfo.PropertyType);
			generator.CallMethod(setMethod);
			generator.Return();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static DynamicReflectionDelegateFactory()
		{
			Strings.CreateGetStringDelegate(typeof(DynamicReflectionDelegateFactory));
			DynamicReflectionDelegateFactory.Instance = new DynamicReflectionDelegateFactory();
		}

		[NonSerialized]
		internal static GetString getString_1;
	}
}
