using System;
using System.Collections;
using System.Collections.Generic;

namespace BrightIdeasSoftware
{
	public sealed class FlagBitSetFilter : OneOfFilter
	{
		public FlagBitSetFilter(AspectGetterDelegate valueGetter, ICollection possibleValues) : base(valueGetter, possibleValues)
		{
			this.ConvertPossibleValues();
		}

		public override IList PossibleValues
		{
			get
			{
				return base.PossibleValues;
			}
			set
			{
				base.PossibleValues = value;
				this.ConvertPossibleValues();
			}
		}

		private void ConvertPossibleValues()
		{
			this.possibleValuesAsUlongs = new List<ulong>();
			foreach (object value in this.PossibleValues)
			{
				this.possibleValuesAsUlongs.Add(Convert.ToUInt64(value));
			}
		}

		protected override bool DoesValueMatch(object result)
		{
			bool result2;
			try
			{
				ulong num = Convert.ToUInt64(result);
				foreach (ulong num2 in this.possibleValuesAsUlongs)
				{
					if ((num & num2) == num2)
					{
						return true;
					}
				}
				result2 = false;
			}
			catch (InvalidCastException)
			{
				result2 = false;
			}
			catch (FormatException)
			{
				result2 = false;
			}
			return result2;
		}

		private List<ulong> possibleValuesAsUlongs = new List<ulong>();
	}
}
