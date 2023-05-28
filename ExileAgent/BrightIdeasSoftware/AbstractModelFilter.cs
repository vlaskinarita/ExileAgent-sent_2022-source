using System;

namespace BrightIdeasSoftware
{
	public class AbstractModelFilter : IModelFilter
	{
		public virtual bool Filter(object modelObject)
		{
			return true;
		}
	}
}
