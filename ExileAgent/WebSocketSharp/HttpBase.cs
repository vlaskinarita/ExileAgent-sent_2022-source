using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	internal abstract class HttpBase
	{
		protected HttpBase(Version version, NameValueCollection headers)
		{
			this._version = version;
			this._headers = headers;
		}

		public string EntityBody
		{
			get
			{
				string result;
				if (this.EntityBodyData == null || (long)this.EntityBodyData.Length == 0L)
				{
					result = string.Empty;
				}
				else
				{
					Encoding encoding = null;
					string text = this._headers[HttpBase.getString_0(107472583)];
					if (text != null && text.Length > 0)
					{
						encoding = HttpUtility.GetEncoding(text);
					}
					result = (encoding ?? Encoding.UTF8).GetString(this.EntityBodyData);
				}
				return result;
			}
		}

		public NameValueCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		public Version ProtocolVersion
		{
			get
			{
				return this._version;
			}
		}

		private static byte[] readEntityBody(Stream stream, string length)
		{
			long num;
			if (!long.TryParse(length, out num))
			{
				throw new ArgumentException(HttpBase.getString_0(107133816), HttpBase.getString_0(107344366));
			}
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(HttpBase.getString_0(107344366), HttpBase.getString_0(107133823));
			}
			return (num > 1024L) ? stream.ReadBytes(num, 1024) : ((num > 0L) ? stream.ReadBytes((int)num) : null);
		}

		private static string[] readHeaders(Stream stream, int maxLength)
		{
			List<byte> buff = new List<byte>();
			int cnt = 0;
			Action<int> action = delegate(int i)
			{
				if (i == -1)
				{
					throw new EndOfStreamException(HttpBase.<>c__DisplayClass13_0.getString_0(107158365));
				}
				buff.Add((byte)i);
				int cnt = cnt;
				cnt++;
			};
			bool flag = false;
			while (cnt < maxLength)
			{
				if (stream.ReadByte().EqualsWith('\r', action) && stream.ReadByte().EqualsWith('\n', action) && stream.ReadByte().EqualsWith('\r', action) && stream.ReadByte().EqualsWith('\n', action))
				{
					flag = true;
					IL_7B:
					if (!flag)
					{
						throw new WebSocketException(HttpBase.getString_0(107133802));
					}
					return Encoding.UTF8.GetString(buff.ToArray()).Replace(HttpBase.getString_0(107133725), HttpBase.getString_0(107405404)).Replace(HttpBase.getString_0(107141632), HttpBase.getString_0(107405404)).Split(new string[]
					{
						HttpBase.getString_0(107248510)
					}, StringSplitOptions.RemoveEmptyEntries);
				}
			}
			goto IL_7B;
		}

		protected static T Read<T>(Stream stream, Func<string[], T> parser, int millisecondsTimeout) where T : HttpBase
		{
			bool timeout = false;
			Timer timer = new Timer(delegate(object state)
			{
				timeout = true;
				stream.Close();
			}, null, millisecondsTimeout, -1);
			T t = default(T);
			Exception ex = null;
			try
			{
				t = parser(HttpBase.readHeaders(stream, 8192));
				string text = t.Headers[HttpBase.getString_0(107133688)];
				if (text != null && text.Length > 0)
				{
					t.EntityBodyData = HttpBase.readEntityBody(stream, text);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				timer.Change(-1, -1);
				timer.Dispose();
			}
			string text2 = timeout ? HttpBase.getString_0(107133098) : ((ex != null) ? HttpBase.getString_0(107133699) : null);
			if (text2 != null)
			{
				throw new WebSocketException(text2, ex);
			}
			return t;
		}

		public byte[] ToByteArray()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		static HttpBase()
		{
			Strings.CreateGetStringDelegate(typeof(HttpBase));
		}

		private NameValueCollection _headers;

		private const int _headersMaxLength = 8192;

		private Version _version;

		internal byte[] EntityBodyData;

		protected const string CrLf = "\r\n";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
