using System;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	public sealed class PusherOptions
	{
		public PusherOptions()
		{
			this.Cluster = PusherOptions.getString_0(107309693);
		}

		public bool Encrypted { get; set; }

		public IAuthorizer Authorizer { get; set; } = null;

		public string Cluster
		{
			get
			{
				return this._cluster;
			}
			set
			{
				if (this._cluster != value)
				{
					this._cluster = value;
					if (this._cluster != null)
					{
						this._host = PusherOptions.getString_0(107309688) + this._cluster + PusherOptions.getString_0(107309683);
					}
					else
					{
						this._host = null;
					}
				}
			}
		}

		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				if (this._host != value)
				{
					this._host = value;
					this._cluster = null;
				}
			}
		}

		public TimeSpan ClientTimeout { get; set; } = TimeSpan.FromSeconds(30.0);

		public ITraceLogger TraceLogger { get; set; }

		internal TimeSpan InnerClientTimeout
		{
			get
			{
				return TimeSpan.FromTicks(this.ClientTimeout.Ticks - this.ClientTimeout.Ticks / 10L);
			}
		}

		static PusherOptions()
		{
			Strings.CreateGetStringDelegate(typeof(PusherOptions));
		}

		private string _cluster;

		private string _host;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
