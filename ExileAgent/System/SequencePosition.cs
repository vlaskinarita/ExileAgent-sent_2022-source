using System;
using System.ComponentModel;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	[IsReadOnlyAttribute1]
	public struct SequencePosition : IEquatable<SequencePosition>
	{
		public SequencePosition(object @object, int integer)
		{
			this._object = @object;
			this._integer = integer;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetObject()
		{
			return this._object;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetInteger()
		{
			return this._integer;
		}

		public bool Equals(SequencePosition other)
		{
			return this._integer == other._integer && object.Equals(this._object, other._object);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is SequencePosition)
			{
				SequencePosition other = (SequencePosition)obj;
				return this.Equals(other);
			}
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			object @object = this._object;
			return HashHelpers.Combine((@object != null) ? @object.GetHashCode() : 0, this._integer);
		}

		private readonly object _object;

		private readonly int _integer;
	}
}
