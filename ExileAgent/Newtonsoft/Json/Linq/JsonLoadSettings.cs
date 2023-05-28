using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace Newtonsoft.Json.Linq
{
	public sealed class JsonLoadSettings
	{
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException(JsonLoadSettings.getString_0(107454439));
				}
				this._commentHandling = value;
			}
		}

		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException(JsonLoadSettings.getString_0(107454439));
				}
				this._lineInfoHandling = value;
			}
		}

		public DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return this._duplicatePropertyNameHandling;
			}
			set
			{
				if (value < DuplicatePropertyNameHandling.Ignore || value > DuplicatePropertyNameHandling.Error)
				{
					throw new ArgumentOutOfRangeException(JsonLoadSettings.getString_0(107454439));
				}
				this._duplicatePropertyNameHandling = value;
			}
		}

		static JsonLoadSettings()
		{
			Strings.CreateGetStringDelegate(typeof(JsonLoadSettings));
		}

		private CommentHandling _commentHandling;

		private LineInfoHandling _lineInfoHandling;

		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
