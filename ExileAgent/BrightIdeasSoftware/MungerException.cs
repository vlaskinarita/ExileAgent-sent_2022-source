using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class MungerException : ApplicationException
	{
		public MungerException(SimpleMunger munger, object target, Exception ex) : base(MungerException.getString_0(107313969), ex)
		{
			this.munger = munger;
			this.target = target;
		}

		public SimpleMunger Munger
		{
			get
			{
				return this.munger;
			}
		}

		public object Target
		{
			get
			{
				return this.target;
			}
		}

		static MungerException()
		{
			Strings.CreateGetStringDelegate(typeof(MungerException));
		}

		private readonly SimpleMunger munger;

		private readonly object target;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
