using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	internal sealed class CompositeExpression : QueryExpression
	{
		public List<QueryExpression> Expressions { get; set; }

		public CompositeExpression()
		{
			this.Expressions = new List<QueryExpression>();
		}

		public override bool IsMatch(JToken root, JToken t)
		{
			QueryOperator @operator = base.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
