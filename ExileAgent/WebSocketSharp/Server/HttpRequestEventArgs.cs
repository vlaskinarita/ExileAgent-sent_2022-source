using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;

namespace WebSocketSharp.Server
{
	public sealed class HttpRequestEventArgs : EventArgs
	{
		internal HttpRequestEventArgs(HttpListenerContext context, string documentRootPath)
		{
			this._context = context;
			this._docRootPath = documentRootPath;
		}

		public HttpListenerRequest Request
		{
			get
			{
				return this._context.Request;
			}
		}

		public HttpListenerResponse Response
		{
			get
			{
				return this._context.Response;
			}
		}

		public IPrincipal User
		{
			get
			{
				return this._context.User;
			}
		}

		private string createFilePath(string childPath)
		{
			childPath = childPath.TrimStart(new char[]
			{
				'/',
				'\\'
			});
			return new StringBuilder(this._docRootPath, 32).AppendFormat(HttpRequestEventArgs.getString_0(107159188), childPath).ToString().Replace('\\', '/');
		}

		private static bool tryReadFile(string path, out byte[] contents)
		{
			contents = null;
			bool result;
			if (!File.Exists(path))
			{
				result = false;
			}
			else
			{
				try
				{
					contents = File.ReadAllBytes(path);
				}
				catch
				{
					return false;
				}
				result = true;
			}
			return result;
		}

		public byte[] ReadFile(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HttpRequestEventArgs.getString_0(107251121));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(HttpRequestEventArgs.getString_0(107140608), HttpRequestEventArgs.getString_0(107251121));
			}
			if (path.IndexOf(HttpRequestEventArgs.getString_0(107159179)) > -1)
			{
				throw new ArgumentException(HttpRequestEventArgs.getString_0(107159142), HttpRequestEventArgs.getString_0(107251121));
			}
			path = this.createFilePath(path);
			byte[] result;
			HttpRequestEventArgs.tryReadFile(path, out result);
			return result;
		}

		public bool TryReadFile(string path, out byte[] contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException(HttpRequestEventArgs.getString_0(107251121));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(HttpRequestEventArgs.getString_0(107140608), HttpRequestEventArgs.getString_0(107251121));
			}
			if (path.IndexOf(HttpRequestEventArgs.getString_0(107159179)) > -1)
			{
				throw new ArgumentException(HttpRequestEventArgs.getString_0(107159142), HttpRequestEventArgs.getString_0(107251121));
			}
			path = this.createFilePath(path);
			return HttpRequestEventArgs.tryReadFile(path, out contents);
		}

		static HttpRequestEventArgs()
		{
			Strings.CreateGetStringDelegate(typeof(HttpRequestEventArgs));
		}

		private HttpListenerContext _context;

		private string _docRootPath;

		[NonSerialized]
		internal static GetString getString_0;
	}
}
