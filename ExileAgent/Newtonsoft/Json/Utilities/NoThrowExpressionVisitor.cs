using System;
using System.Linq.Expressions;

namespace Newtonsoft.Json.Utilities
{
	internal sealed class NoThrowExpressionVisitor : ExpressionVisitor
	{
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == ExpressionType.Throw)
			{
				return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(NoThrowExpressionVisitor.ErrorResult));
			}
			return base.VisitConditional(node);
		}

		internal static readonly object ErrorResult = new object();
	}
}
