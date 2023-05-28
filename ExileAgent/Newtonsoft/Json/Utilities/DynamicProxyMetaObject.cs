using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using ns20;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class DynamicProxyMetaObject<T> : DynamicMetaObject
	{
		internal DynamicProxyMetaObject(Expression expression, T value, DynamicProxy<T> proxy) : base(expression, BindingRestrictions.Empty, value)
		{
			this._proxy = proxy;
		}

		private bool IsOverridden(string method)
		{
			return ReflectionUtils.IsMethodOverridden(this._proxy.GetType(), typeof(DynamicProxy<T>), method);
		}

		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338304)))
			{
				return base.BindGetMember(binder);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338304), binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackGetMember(this, e), null);
		}

		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338287)))
			{
				return base.BindSetMember(binder, value);
			}
			return this.CallMethodReturnLast(Class401.smethod_0(107338287), binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[]
			{
				value
			}), (DynamicMetaObject e) => binder.FallbackSetMember(this, value, e));
		}

		public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338302)))
			{
				return base.BindDeleteMember(binder);
			}
			return this.CallMethodNoResult(Class401.smethod_0(107338302), binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackDeleteMember(this, e));
		}

		public override DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338249)))
			{
				return base.BindConvert(binder);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338249), binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackConvert(this, e), null);
		}

		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338264)))
			{
				return base.BindInvokeMember(binder, args);
			}
			DynamicProxyMetaObject<T>.Fallback fallback = (DynamicMetaObject e) => binder.FallbackInvokeMember(this, args, e);
			return this.BuildCallMethodWithResult(Class401.smethod_0(107338264), binder, DynamicProxyMetaObject<T>.GetArgArray(args), this.BuildCallMethodWithResult(Class401.smethod_0(107338304), new DynamicProxyMetaObject<T>.GetBinderAdapter(binder), DynamicProxyMetaObject<T>.NoArgs, fallback(null), (DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
		}

		public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338723)))
			{
				return base.BindCreateInstance(binder, args);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338723), binder, DynamicProxyMetaObject<T>.GetArgArray(args), (DynamicMetaObject e) => binder.FallbackCreateInstance(this, args, e), null);
		}

		public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338698)))
			{
				return base.BindInvoke(binder, args);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338698), binder, DynamicProxyMetaObject<T>.GetArgArray(args), (DynamicMetaObject e) => binder.FallbackInvoke(this, args, e), null);
		}

		public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338717)))
			{
				return base.BindBinaryOperation(binder, arg);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338717), binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[]
			{
				arg
			}), (DynamicMetaObject e) => binder.FallbackBinaryOperation(this, arg, e), null);
		}

		public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338660)))
			{
				return base.BindUnaryOperation(binder);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338660), binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackUnaryOperation(this, e), null);
		}

		public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338635)))
			{
				return base.BindGetIndex(binder, indexes);
			}
			return this.CallMethodWithResult(Class401.smethod_0(107338635), binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), (DynamicMetaObject e) => binder.FallbackGetIndex(this, indexes, e), null);
		}

		public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338650)))
			{
				return base.BindSetIndex(binder, indexes, value);
			}
			return this.CallMethodReturnLast(Class401.smethod_0(107338650), binder, DynamicProxyMetaObject<T>.GetArgArray(indexes, value), (DynamicMetaObject e) => binder.FallbackSetIndex(this, indexes, value, e));
		}

		public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden(Class401.smethod_0(107338601)))
			{
				return base.BindDeleteIndex(binder, indexes);
			}
			return this.CallMethodNoResult(Class401.smethod_0(107338601), binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), (DynamicMetaObject e) => binder.FallbackDeleteIndex(this, indexes, e));
		}

		private static Expression[] NoArgs
		{
			get
			{
				return CollectionUtils.ArrayEmpty<Expression>();
			}
		}

		private static IEnumerable<Expression> GetArgs(params DynamicMetaObject[] args)
		{
			return args.Select(delegate(DynamicMetaObject arg)
			{
				Expression expression = arg.Expression;
				if (!expression.Type.IsValueType())
				{
					return expression;
				}
				return Expression.Convert(expression, typeof(object));
			});
		}

		private static Expression[] GetArgArray(DynamicMetaObject[] args)
		{
			return new NewArrayExpression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args))
			};
		}

		private static Expression[] GetArgArray(DynamicMetaObject[] args, DynamicMetaObject value)
		{
			Expression expression = value.Expression;
			return new Expression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args)),
				expression.Type.IsValueType() ? Expression.Convert(expression, typeof(object)) : expression
			};
		}

		private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
		{
			Type type = binder.GetType();
			while (!type.IsVisible())
			{
				type = type.BaseType();
			}
			return Expression.Constant(binder, type);
		}

		private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicProxyMetaObject<T>.Fallback fallback, DynamicProxyMetaObject<T>.Fallback fallbackInvoke = null)
		{
			DynamicMetaObject fallbackResult = fallback(null);
			return this.BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
		}

		private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicMetaObject fallbackResult, DynamicProxyMetaObject<T>.Fallback fallbackInvoke)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list.Add(parameterExpression);
			DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
			if (binder.ReturnType != typeof(object))
			{
				dynamicMetaObject = new DynamicMetaObject(Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType), dynamicMetaObject.Restrictions);
			}
			if (fallbackInvoke != null)
			{
				dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
			}
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), dynamicMetaObject.Expression, fallbackResult.Expression, binder.ReturnType)
			}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
		}

		private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list[list.Count - 1] = Expression.Assign(parameterExpression, list[list.Count - 1]);
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), parameterExpression, dynamicMetaObject.Expression, typeof(object))
			}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			return new DynamicMetaObject(Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), Expression.Empty(), dynamicMetaObject.Expression, typeof(void)), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		private BindingRestrictions GetRestrictions()
		{
			if (base.Value == null && base.HasValue)
			{
				return BindingRestrictions.GetInstanceRestriction(base.Expression, null);
			}
			return BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this._proxy.GetDynamicMemberNames((T)((object)base.Value));
		}

		private readonly DynamicProxy<T> _proxy;

		private delegate DynamicMetaObject Fallback(DynamicMetaObject errorSuggestion);

		private sealed class GetBinderAdapter : GetMemberBinder
		{
			internal GetBinderAdapter(InvokeMemberBinder binder) : base(binder.Name, binder.IgnoreCase)
			{
			}

			public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
			{
				throw new NotSupportedException();
			}
		}
	}
}
