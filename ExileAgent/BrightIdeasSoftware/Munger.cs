using System;
using System.Collections.Generic;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace BrightIdeasSoftware
{
	public sealed class Munger
	{
		public Munger()
		{
		}

		public Munger(string aspectName)
		{
			this.AspectName = aspectName;
		}

		public static bool PutProperty(object target, string propertyName, object value)
		{
			try
			{
				Munger munger = new Munger(propertyName);
				return munger.PutValue(target, value);
			}
			catch (MungerException)
			{
			}
			return false;
		}

		public static bool IgnoreMissingAspects
		{
			get
			{
				return Munger.ignoreMissingAspects;
			}
			set
			{
				Munger.ignoreMissingAspects = value;
			}
		}

		public string AspectName
		{
			get
			{
				return this.aspectName;
			}
			set
			{
				this.aspectName = value;
				this.aspectParts = null;
			}
		}

		public object GetValue(object target)
		{
			if (this.Parts.Count == 0)
			{
				return null;
			}
			object result;
			try
			{
				result = this.EvaluateParts(target, this.Parts);
			}
			catch (MungerException ex)
			{
				if (Munger.IgnoreMissingAspects)
				{
					result = null;
				}
				else
				{
					result = string.Format(Munger.getString_0(107314562), ex.Munger.AspectName, ex.Target.GetType());
				}
			}
			return result;
		}

		public object GetValueEx(object target)
		{
			if (this.Parts.Count == 0)
			{
				return null;
			}
			return this.EvaluateParts(target, this.Parts);
		}

		public bool PutValue(object target, object value)
		{
			if (this.Parts.Count == 0)
			{
				return false;
			}
			SimpleMunger simpleMunger = this.Parts[this.Parts.Count - 1];
			if (this.Parts.Count > 1)
			{
				List<SimpleMunger> list = new List<SimpleMunger>(this.Parts);
				list.RemoveAt(list.Count - 1);
				try
				{
					target = this.EvaluateParts(target, list);
				}
				catch (MungerException ex)
				{
					this.ReportPutValueException(ex);
					return false;
				}
			}
			if (target != null)
			{
				bool result;
				try
				{
					result = simpleMunger.PutValue(target, value);
				}
				catch (MungerException ex2)
				{
					this.ReportPutValueException(ex2);
					return false;
				}
				return result;
			}
			return false;
		}

		private IList<SimpleMunger> Parts
		{
			get
			{
				if (this.aspectParts == null)
				{
					this.aspectParts = this.BuildParts(this.AspectName);
				}
				return this.aspectParts;
			}
		}

		private IList<SimpleMunger> BuildParts(string aspect)
		{
			List<SimpleMunger> list = new List<SimpleMunger>();
			if (!string.IsNullOrEmpty(aspect))
			{
				foreach (string text in aspect.Split(new char[]
				{
					'.'
				}))
				{
					list.Add(new SimpleMunger(text.Trim()));
				}
			}
			return list;
		}

		private object EvaluateParts(object target, IList<SimpleMunger> parts)
		{
			foreach (SimpleMunger simpleMunger in parts)
			{
				if (target == null)
				{
					break;
				}
				target = simpleMunger.GetValue(target);
			}
			return target;
		}

		private void ReportPutValueException(MungerException ex)
		{
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Munger()
		{
			Strings.CreateGetStringDelegate(typeof(Munger));
			Munger.ignoreMissingAspects = true;
		}

		private static bool ignoreMissingAspects;

		private string aspectName;

		private IList<SimpleMunger> aspectParts;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
