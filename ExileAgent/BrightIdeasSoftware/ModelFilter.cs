using System;

namespace BrightIdeasSoftware
{
	public sealed class ModelFilter : IModelFilter
	{
		public ModelFilter(Predicate<object> predicate)
		{
			this.Predicate = predicate;
		}

		protected Predicate<object> Predicate
		{
			get
			{
				return this.predicate;
			}
			set
			{
				this.predicate = value;
			}
		}

		public bool Filter(object modelObject)
		{
			return this.Predicate == null || this.Predicate(modelObject);
		}

		private Predicate<object> predicate;
	}
}
