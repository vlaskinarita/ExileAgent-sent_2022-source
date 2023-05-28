using System;
using System.Collections;

namespace BrightIdeasSoftware
{
	public class OneOfFilter : IModelFilter
	{
		public OneOfFilter(AspectGetterDelegate valueGetter) : this(valueGetter, new ArrayList())
		{
		}

		public OneOfFilter(AspectGetterDelegate valueGetter, ICollection possibleValues)
		{
			this.ValueGetter = valueGetter;
			this.PossibleValues = new ArrayList(possibleValues);
		}

		public virtual AspectGetterDelegate ValueGetter
		{
			get
			{
				return this.valueGetter;
			}
			set
			{
				this.valueGetter = value;
			}
		}

		public virtual IList PossibleValues
		{
			get
			{
				return this.possibleValues;
			}
			set
			{
				this.possibleValues = value;
			}
		}

		public virtual bool Filter(object modelObject)
		{
			if (this.ValueGetter == null || this.PossibleValues == null || this.PossibleValues.Count == 0)
			{
				return false;
			}
			object obj = this.ValueGetter(modelObject);
			IEnumerable enumerable = obj as IEnumerable;
			if (!(obj is string) && enumerable != null)
			{
				foreach (object result in enumerable)
				{
					if (this.DoesValueMatch(result))
					{
						return true;
					}
				}
				return false;
			}
			return this.DoesValueMatch(obj);
		}

		protected virtual bool DoesValueMatch(object result)
		{
			return this.PossibleValues.Contains(result);
		}

		private AspectGetterDelegate valueGetter;

		private IList possibleValues;
	}
}
