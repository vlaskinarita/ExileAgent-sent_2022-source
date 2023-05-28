using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class ExpressionReflectionDelegateFactory : ReflectionDelegateFactory
	{
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return ExpressionReflectionDelegateFactory._instance;
			}
		}

		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, ExpressionReflectionDelegateFactory.getString_1(107344155));
			Type typeFromHandle = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), ExpressionReflectionDelegateFactory.getString_1(107344146));
			Expression body = this.BuildMethodCall(method, typeFromHandle, null, parameterExpression);
			return (ObjectConstructor<object>)Expression.Lambda(typeof(ObjectConstructor<object>), body, new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, ExpressionReflectionDelegateFactory.getString_1(107344155));
			Type typeFromHandle = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, ExpressionReflectionDelegateFactory.getString_1(107350170));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), ExpressionReflectionDelegateFactory.getString_1(107344146));
			Expression body = this.BuildMethodCall(method, typeFromHandle, parameterExpression, parameterExpression2);
			return (MethodCall<T, object>)Expression.Lambda(typeof(MethodCall<T, object>), body, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		private Expression BuildMethodCall(MethodBase method, Type type, ParameterExpression targetParameterExpression, ParameterExpression argsParameterExpression)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Expression[] array;
			IList<ExpressionReflectionDelegateFactory.ByRefParameter> list;
			if (parameters.Length == 0)
			{
				array = CollectionUtils.ArrayEmpty<Expression>();
				list = CollectionUtils.ArrayEmpty<ExpressionReflectionDelegateFactory.ByRefParameter>();
			}
			else
			{
				array = new Expression[parameters.Length];
				list = new List<ExpressionReflectionDelegateFactory.ByRefParameter>();
				for (int i = 0; i < parameters.Length; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					Type type2 = parameterInfo.ParameterType;
					bool flag = false;
					if (type2.IsByRef)
					{
						type2 = type2.GetElementType();
						flag = true;
					}
					Expression index = Expression.Constant(i);
					Expression expression = Expression.ArrayIndex(argsParameterExpression, index);
					Expression expression2 = this.EnsureCastExpression(expression, type2, !flag);
					if (flag)
					{
						ParameterExpression parameterExpression = Expression.Variable(type2);
						list.Add(new ExpressionReflectionDelegateFactory.ByRefParameter
						{
							Value = expression2,
							Variable = parameterExpression,
							IsOut = parameterInfo.IsOut
						});
						expression2 = parameterExpression;
					}
					array[i] = expression2;
				}
			}
			Expression expression3;
			if (method.IsConstructor)
			{
				expression3 = Expression.New((ConstructorInfo)method, array);
			}
			else if (method.IsStatic)
			{
				expression3 = Expression.Call((MethodInfo)method, array);
			}
			else
			{
				expression3 = Expression.Call(this.EnsureCastExpression(targetParameterExpression, method.DeclaringType, false), (MethodInfo)method, array);
			}
			MethodInfo methodInfo;
			if ((methodInfo = (method as MethodInfo)) != null)
			{
				if (methodInfo.ReturnType != typeof(void))
				{
					expression3 = this.EnsureCastExpression(expression3, type, false);
				}
				else
				{
					expression3 = Expression.Block(expression3, Expression.Constant(null));
				}
			}
			else
			{
				expression3 = this.EnsureCastExpression(expression3, type, false);
			}
			if (list.Count > 0)
			{
				IList<ParameterExpression> list2 = new List<ParameterExpression>();
				IList<Expression> list3 = new List<Expression>();
				foreach (ExpressionReflectionDelegateFactory.ByRefParameter byRefParameter in list)
				{
					if (!byRefParameter.IsOut)
					{
						list3.Add(Expression.Assign(byRefParameter.Variable, byRefParameter.Value));
					}
					list2.Add(byRefParameter.Variable);
				}
				list3.Add(expression3);
				expression3 = Expression.Block(list2, list3);
			}
			return expression3;
		}

		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, ExpressionReflectionDelegateFactory.getString_1(107373940));
			if (type.IsAbstract())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			Func<T> result;
			try
			{
				Type typeFromHandle = typeof(T);
				Expression expression = Expression.New(type);
				expression = this.EnsureCastExpression(expression, typeFromHandle, false);
				result = (Func<T>)Expression.Lambda(typeof(Func<T>), expression, new ParameterExpression[0]).Compile();
			}
			catch
			{
				result = (() => (T)((object)Activator.CreateInstance(type)));
			}
			return result;
		}

		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, ExpressionReflectionDelegateFactory.getString_1(107344169));
			Type typeFromHandle = typeof(T);
			Type typeFromHandle2 = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, ExpressionReflectionDelegateFactory.getString_1(107344120));
			Expression expression;
			if (propertyInfo.GetGetMethod(true).IsStatic)
			{
				expression = Expression.MakeMemberAccess(null, propertyInfo);
			}
			else
			{
				expression = Expression.MakeMemberAccess(this.EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType, false), propertyInfo);
			}
			expression = this.EnsureCastExpression(expression, typeFromHandle2, false);
			return (Func<T, object>)Expression.Lambda(typeof(Func<T, object>), expression, new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, ExpressionReflectionDelegateFactory.getString_1(107344139));
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), ExpressionReflectionDelegateFactory.getString_1(107243957));
			Expression expression;
			if (fieldInfo.IsStatic)
			{
				expression = Expression.Field(null, fieldInfo);
			}
			else
			{
				expression = Expression.Field(this.EnsureCastExpression(parameterExpression, fieldInfo.DeclaringType, false), fieldInfo);
			}
			expression = this.EnsureCastExpression(expression, typeof(object), false);
			return Expression.Lambda<Func<T, object>>(expression, new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
		}

		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, ExpressionReflectionDelegateFactory.getString_1(107344139));
			if (!fieldInfo.DeclaringType.IsValueType() && !fieldInfo.IsInitOnly)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(T), ExpressionReflectionDelegateFactory.getString_1(107243957));
				ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), ExpressionReflectionDelegateFactory.getString_1(107453614));
				Expression expression;
				if (fieldInfo.IsStatic)
				{
					expression = Expression.Field(null, fieldInfo);
				}
				else
				{
					expression = Expression.Field(this.EnsureCastExpression(parameterExpression, fieldInfo.DeclaringType, false), fieldInfo);
				}
				Expression right = this.EnsureCastExpression(parameterExpression2, expression.Type, false);
				BinaryExpression body = Expression.Assign(expression, right);
				return (Action<T, object>)Expression.Lambda(typeof(Action<T, object>), body, new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression2
				}).Compile();
			}
			return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(fieldInfo);
		}

		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, ExpressionReflectionDelegateFactory.getString_1(107344169));
			if (propertyInfo.DeclaringType.IsValueType())
			{
				return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(propertyInfo);
			}
			Type typeFromHandle = typeof(T);
			Type typeFromHandle2 = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, ExpressionReflectionDelegateFactory.getString_1(107344120));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeFromHandle2, ExpressionReflectionDelegateFactory.getString_1(107453614));
			Expression expression = this.EnsureCastExpression(parameterExpression2, propertyInfo.PropertyType, false);
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			Expression body;
			if (setMethod.IsStatic)
			{
				body = Expression.Call(setMethod, expression);
			}
			else
			{
				body = Expression.Call(this.EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType, false), setMethod, new Expression[]
				{
					expression
				});
			}
			return (Action<T, object>)Expression.Lambda(typeof(Action<T, object>), body, new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
		}

		private Expression EnsureCastExpression(Expression expression, Type targetType, bool allowWidening = false)
		{
			Type type = expression.Type;
			if (type == targetType || (!type.IsValueType() && targetType.IsAssignableFrom(type)))
			{
				return expression;
			}
			if (targetType.IsValueType())
			{
				Expression expression2 = Expression.Unbox(expression, targetType);
				if (allowWidening && targetType.IsPrimitive())
				{
					MethodInfo method = typeof(Convert).GetMethod(ExpressionReflectionDelegateFactory.getString_1(107344677) + targetType.Name, new Type[]
					{
						typeof(object)
					});
					if (method != null)
					{
						expression2 = Expression.Condition(Expression.TypeIs(expression, targetType), expression2, Expression.Call(method, expression));
					}
				}
				return Expression.Condition(Expression.Equal(expression, Expression.Constant(null, typeof(object))), Expression.Default(targetType), expression2);
			}
			return Expression.Convert(expression, targetType);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ExpressionReflectionDelegateFactory()
		{
			Strings.CreateGetStringDelegate(typeof(ExpressionReflectionDelegateFactory));
			ExpressionReflectionDelegateFactory._instance = new ExpressionReflectionDelegateFactory();
		}

		private static readonly ExpressionReflectionDelegateFactory _instance;

		[NonSerialized]
		internal static GetString getString_1;

		private sealed class ByRefParameter
		{
			public Expression Value;

			public ParameterExpression Variable;

			public bool IsOut;
		}
	}
}
