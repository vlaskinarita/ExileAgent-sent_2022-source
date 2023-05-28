using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;
using WebSocketSharp.Net;

namespace WebSocketSharp
{
	public static class Ext
	{
		private static byte[] compress(this byte[] data)
		{
			byte[] result;
			if ((long)data.Length == 0L)
			{
				result = data;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					result = memoryStream.compressToArray();
				}
			}
			return result;
		}

		private static MemoryStream compress(this Stream stream)
		{
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream result;
			if (stream.Length == 0L)
			{
				result = memoryStream;
			}
			else
			{
				stream.Position = 0L;
				using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
				{
					stream.CopyTo(deflateStream, 1024);
					deflateStream.Close();
					memoryStream.Write(Ext._last, 0, 1);
					memoryStream.Position = 0L;
					result = memoryStream;
				}
			}
			return result;
		}

		private static byte[] compressToArray(this Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = stream.compress())
			{
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		private static byte[] decompress(this byte[] data)
		{
			byte[] result;
			if ((long)data.Length == 0L)
			{
				result = data;
			}
			else
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					result = memoryStream.decompressToArray();
				}
			}
			return result;
		}

		private static MemoryStream decompress(this Stream stream)
		{
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream result;
			if (stream.Length == 0L)
			{
				result = memoryStream;
			}
			else
			{
				stream.Position = 0L;
				using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true))
				{
					deflateStream.CopyTo(memoryStream, 1024);
					memoryStream.Position = 0L;
					result = memoryStream;
				}
			}
			return result;
		}

		private static byte[] decompressToArray(this Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = stream.decompress())
			{
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		private static bool isHttpMethod(this string value)
		{
			return value == Ext.getString_0(107457415) || value == Ext.getString_0(107141905) || value == Ext.getString_0(107379689) || value == Ext.getString_0(107141896) || value == Ext.getString_0(107141891) || value == Ext.getString_0(107141850) || value == Ext.getString_0(107141869) || value == Ext.getString_0(107141824);
		}

		private static bool isHttpMethod10(this string value)
		{
			return value == Ext.getString_0(107457415) || value == Ext.getString_0(107141905) || value == Ext.getString_0(107379689);
		}

		internal static byte[] Append(this ushort code, string reason)
		{
			byte[] array = code.InternalToByteArray(ByteOrder.Big);
			byte[] result;
			if (reason == null || reason.Length == 0)
			{
				result = array;
			}
			else
			{
				List<byte> list = new List<byte>(array);
				list.AddRange(Encoding.UTF8.GetBytes(reason));
				result = list.ToArray();
			}
			return result;
		}

		internal static byte[] Compress(this byte[] data, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? data.compress() : data;
		}

		internal static Stream Compress(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.compress() : stream;
		}

		internal static byte[] CompressToArray(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.compressToArray() : stream.ToByteArray();
		}

		internal static bool Contains(this string value, params char[] anyOf)
		{
			return anyOf != null && anyOf.Length != 0 && value.IndexOfAny(anyOf) > -1;
		}

		internal static bool Contains(this NameValueCollection collection, string name)
		{
			return collection[name] != null;
		}

		internal static bool Contains(this NameValueCollection collection, string name, string value, StringComparison comparisonTypeForValue)
		{
			string text = collection[name];
			bool result;
			if (text == null)
			{
				result = false;
			}
			else
			{
				foreach (string text2 in text.Split(new char[]
				{
					','
				}))
				{
					if (text2.Trim().Equals(value, comparisonTypeForValue))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		internal static bool Contains<T>(this IEnumerable<T> source, Func<T, bool> condition)
		{
			foreach (T arg in source)
			{
				if (condition(arg))
				{
					return true;
				}
			}
			return false;
		}

		internal static bool ContainsTwice(this string[] values)
		{
			int len = values.Length;
			int end = len - 1;
			Func<int, bool> seek = null;
			seek = delegate(int idx)
			{
				bool result;
				if (idx == end)
				{
					result = false;
				}
				else
				{
					string b = values[idx];
					for (int i = idx + 1; i < len; i++)
					{
						if (values[i] == b)
						{
							return true;
						}
					}
					result = seek(++idx);
				}
				return result;
			};
			return seek(0);
		}

		internal static T[] Copy<T>(this T[] source, int length)
		{
			T[] array = new T[length];
			Array.Copy(source, 0, array, 0, length);
			return array;
		}

		internal static T[] Copy<T>(this T[] source, long length)
		{
			T[] array = new T[length];
			Array.Copy(source, 0L, array, 0L, length);
			return array;
		}

		internal static void CopyTo(this Stream source, Stream destination, int bufferLength)
		{
			byte[] buffer = new byte[bufferLength];
			for (;;)
			{
				int num = source.Read(buffer, 0, bufferLength);
				if (num <= 0)
				{
					break;
				}
				destination.Write(buffer, 0, num);
			}
		}

		internal static void CopyToAsync(this Stream source, Stream destination, int bufferLength, Action completed, Action<Exception> error)
		{
			byte[] buff = new byte[bufferLength];
			AsyncCallback callback = null;
			callback = delegate(IAsyncResult ar)
			{
				try
				{
					int num = source.EndRead(ar);
					if (num <= 0)
					{
						if (completed != null)
						{
							completed();
						}
					}
					else
					{
						destination.Write(buff, 0, num);
						source.BeginRead(buff, 0, bufferLength, callback, null);
					}
				}
				catch (Exception obj2)
				{
					if (error != null)
					{
						error(obj2);
					}
				}
			};
			try
			{
				source.BeginRead(buff, 0, bufferLength, callback, null);
			}
			catch (Exception obj)
			{
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static byte[] Decompress(this byte[] data, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? data.decompress() : data;
		}

		internal static Stream Decompress(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.decompress() : stream;
		}

		internal static byte[] DecompressToArray(this Stream stream, CompressionMethod method)
		{
			return (method == CompressionMethod.Deflate) ? stream.decompressToArray() : stream.ToByteArray();
		}

		internal static void Emit(this EventHandler eventHandler, object sender, EventArgs e)
		{
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		internal static void Emit<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e) where TEventArgs : EventArgs
		{
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		internal static bool EqualsWith(this int value, char c, Action<int> action)
		{
			action(value);
			return value == (int)c;
		}

		internal static string GetAbsolutePath(this Uri uri)
		{
			string result;
			if (uri.IsAbsoluteUri)
			{
				result = uri.AbsolutePath;
			}
			else
			{
				string originalString = uri.OriginalString;
				if (originalString[0] != '/')
				{
					result = null;
				}
				else
				{
					int num = originalString.IndexOfAny(new char[]
					{
						'?',
						'#'
					});
					result = ((num > 0) ? originalString.Substring(0, num) : originalString);
				}
			}
			return result;
		}

		internal static CookieCollection GetCookies(this NameValueCollection headers, bool response)
		{
			string text = headers[response ? Ext.getString_0(107141838) : Ext.getString_0(107141815)];
			return (text != null) ? CookieCollection.Parse(text, response) : new CookieCollection();
		}

		internal static string GetDnsSafeHost(this Uri uri, bool bracketIPv6)
		{
			if (bracketIPv6)
			{
				if (uri.HostNameType == UriHostNameType.IPv6)
				{
					return uri.Host;
				}
			}
			return uri.DnsSafeHost;
		}

		internal static string GetMessage(this CloseStatusCode code)
		{
			return (code == CloseStatusCode.ProtocolError) ? Ext.getString_0(107141377) : ((code == CloseStatusCode.UnsupportedData) ? Ext.getString_0(107141458) : ((code == CloseStatusCode.Abnormal) ? Ext.getString_0(107141463) : ((code == CloseStatusCode.InvalidData) ? Ext.getString_0(107142052) : ((code == CloseStatusCode.PolicyViolation) ? Ext.getString_0(107142097) : ((code == CloseStatusCode.TooBig) ? Ext.getString_0(107142114) : ((code == CloseStatusCode.MandatoryExtension) ? Ext.getString_0(107142219) : ((code == CloseStatusCode.ServerError) ? Ext.getString_0(107142240) : ((code == CloseStatusCode.TlsHandshakeFailure) ? Ext.getString_0(107141789) : string.Empty))))))));
		}

		internal static string GetName(this string nameAndValue, char separator)
		{
			int num = nameAndValue.IndexOf(separator);
			return (num > 0) ? nameAndValue.Substring(0, num).Trim() : null;
		}

		internal static string GetUTF8DecodedString(this byte[] bytes)
		{
			return Encoding.UTF8.GetString(bytes);
		}

		internal static byte[] GetUTF8EncodedBytes(this string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}

		internal static string GetValue(this string nameAndValue, char separator)
		{
			return nameAndValue.GetValue(separator, false);
		}

		internal static string GetValue(this string nameAndValue, char separator, bool unquote)
		{
			int num = nameAndValue.IndexOf(separator);
			string result;
			if (num < 0 || num == nameAndValue.Length - 1)
			{
				result = null;
			}
			else
			{
				string text = nameAndValue.Substring(num + 1).Trim();
				result = (unquote ? text.Unquote() : text);
			}
			return result;
		}

		internal static byte[] InternalToByteArray(this ushort value, ByteOrder order)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!order.IsHostOrder())
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		internal static byte[] InternalToByteArray(this ulong value, ByteOrder order)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!order.IsHostOrder())
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		internal static bool IsCompressionExtension(this string value, CompressionMethod method)
		{
			return value.StartsWith(method.ToExtensionString(new string[0]));
		}

		internal static bool IsControl(this byte opcode)
		{
			return opcode > 7 && opcode < 16;
		}

		internal static bool IsControl(this Opcode opcode)
		{
			return opcode >= Opcode.Close;
		}

		internal static bool IsData(this byte opcode)
		{
			return opcode == 1 || opcode == 2;
		}

		internal static bool IsData(this Opcode opcode)
		{
			return opcode == Opcode.Text || opcode == Opcode.Binary;
		}

		internal static bool IsHttpMethod(this string value, Version version)
		{
			return (version == HttpVersion.Version10) ? value.isHttpMethod10() : value.isHttpMethod();
		}

		internal static bool IsPortNumber(this int value)
		{
			return value > 0 && value < 65536;
		}

		internal static bool IsReserved(this ushort code)
		{
			return code == 1004 || code == 1005 || code == 1006 || code == 1015;
		}

		internal static bool IsReserved(this CloseStatusCode code)
		{
			return code == CloseStatusCode.Undefined || code == CloseStatusCode.NoStatus || code == CloseStatusCode.Abnormal || code == CloseStatusCode.TlsHandshakeFailure;
		}

		internal static bool IsSupported(this byte opcode)
		{
			return Enum.IsDefined(typeof(Opcode), opcode);
		}

		internal static bool IsText(this string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c < ' ')
				{
					if (Ext.getString_0(107141352).IndexOf(c) == -1)
					{
						return false;
					}
					if (c == '\n')
					{
						i++;
						if (i == length)
						{
							IL_80:
							return true;
						}
						c = value[i];
						if (Ext.getString_0(107141347).IndexOf(c) == -1)
						{
							return false;
						}
					}
				}
				else if (c == '\u007f')
				{
					return false;
				}
			}
			goto IL_80;
		}

		internal static bool IsToken(this string value)
		{
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool result;
				if (c >= ' ')
				{
					if (c <= '~')
					{
						if (Ext.getString_0(107141310).IndexOf(c) <= -1)
						{
							i++;
							continue;
						}
						result = false;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			return true;
		}

		internal static bool KeepsAlive(this NameValueCollection headers, Version version)
		{
			StringComparison comparisonTypeForValue = StringComparison.OrdinalIgnoreCase;
			return (version < HttpVersion.Version11) ? headers.Contains(Ext.getString_0(107141281), Ext.getString_0(107141287), comparisonTypeForValue) : (!headers.Contains(Ext.getString_0(107141281), Ext.getString_0(107141296), comparisonTypeForValue));
		}

		internal static string Quote(this string value)
		{
			return string.Format(Ext.getString_0(107141750), value.Replace(Ext.getString_0(107377100), Ext.getString_0(107346009)));
		}

		internal static byte[] ReadBytes(this Stream stream, int length)
		{
			byte[] array = new byte[length];
			int num = 0;
			int num2 = 0;
			while (length > 0)
			{
				int num3 = stream.Read(array, num, length);
				if (num3 <= 0)
				{
					if (num2 >= Ext._retry)
					{
						return array.SubArray(0, num);
					}
					num2++;
				}
				else
				{
					num2 = 0;
					num += num3;
					length -= num3;
				}
			}
			return array;
		}

		internal static byte[] ReadBytes(this Stream stream, long length, int bufferLength)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] buffer = new byte[bufferLength];
				int num = 0;
				while (length > 0L)
				{
					if (length < (long)bufferLength)
					{
						bufferLength = (int)length;
					}
					int num2 = stream.Read(buffer, 0, bufferLength);
					if (num2 <= 0)
					{
						if (num >= Ext._retry)
						{
							break;
						}
						num++;
					}
					else
					{
						num = 0;
						memoryStream.Write(buffer, 0, num2);
						length -= (long)num2;
					}
				}
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		internal static void ReadBytesAsync(this Stream stream, int length, Action<byte[]> completed, Action<Exception> error)
		{
			byte[] buff = new byte[length];
			int offset = 0;
			int retry = 0;
			AsyncCallback callback = null;
			callback = delegate(IAsyncResult ar)
			{
				try
				{
					int num = stream.EndRead(ar);
					if (num <= 0)
					{
						int retry;
						if (retry < Ext._retry)
						{
							retry = retry;
							retry++;
							stream.BeginRead(buff, offset, length, callback, null);
						}
						else if (completed != null)
						{
							completed(buff.SubArray(0, offset));
						}
					}
					else if (num == length)
					{
						if (completed != null)
						{
							completed(buff);
						}
					}
					else
					{
						int retry = 0;
						offset += num;
						length -= num;
						stream.BeginRead(buff, offset, length, callback, null);
					}
				}
				catch (Exception obj2)
				{
					if (error != null)
					{
						error(obj2);
					}
				}
			};
			try
			{
				stream.BeginRead(buff, offset, length, callback, null);
			}
			catch (Exception obj)
			{
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static void ReadBytesAsync(this Stream stream, long length, int bufferLength, Action<byte[]> completed, Action<Exception> error)
		{
			MemoryStream dest = new MemoryStream();
			byte[] buff = new byte[bufferLength];
			int retry = 0;
			Action<long> read = null;
			read = delegate(long len)
			{
				if (len < (long)bufferLength)
				{
					bufferLength = (int)len;
				}
				stream.BeginRead(buff, 0, bufferLength, delegate(IAsyncResult ar)
				{
					try
					{
						int num = stream.EndRead(ar);
						if (num <= 0)
						{
							int retry;
							if (retry < Ext._retry)
							{
								retry = retry;
								retry++;
								read(len);
							}
							else
							{
								if (completed != null)
								{
									dest.Close();
									completed(dest.ToArray());
								}
								dest.Dispose();
							}
						}
						else
						{
							dest.Write(buff, 0, num);
							if ((long)num == len)
							{
								if (completed != null)
								{
									dest.Close();
									completed(dest.ToArray());
								}
								dest.Dispose();
							}
							else
							{
								int retry = 0;
								read(len - (long)num);
							}
						}
					}
					catch (Exception obj2)
					{
						dest.Dispose();
						if (error != null)
						{
							error(obj2);
						}
					}
				}, null);
			};
			try
			{
				read(length);
			}
			catch (Exception obj)
			{
				dest.Dispose();
				if (error != null)
				{
					error(obj);
				}
			}
		}

		internal static T[] Reverse<T>(this T[] array)
		{
			int num = array.Length;
			T[] array2 = new T[num];
			int num2 = num - 1;
			for (int i = 0; i <= num2; i++)
			{
				array2[i] = array[num2 - i];
			}
			return array2;
		}

		internal static IEnumerable<string> SplitHeaderValue(this string value, params char[] separators)
		{
			int len = value.Length;
			int end = len - 1;
			StringBuilder buff = new StringBuilder(32);
			bool escaped = false;
			bool quoted = false;
			int num;
			for (int i = 0; i <= end; i = num + 1)
			{
				char c = value[i];
				buff.Append(c);
				if (c == '"')
				{
					if (escaped)
					{
						escaped = false;
					}
					else
					{
						quoted = !quoted;
					}
				}
				else if (c == '\\')
				{
					if (i == end)
					{
						break;
					}
					if (value[i + 1] == '"')
					{
						escaped = true;
					}
				}
				else if (Array.IndexOf<char>(separators, c) > -1 && !quoted)
				{
					buff.Length--;
					yield return buff.ToString();
					buff.Length = 0;
				}
				num = i;
			}
			yield return buff.ToString();
			yield break;
		}

		internal static byte[] ToByteArray(this Stream stream)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.Position = 0L;
				stream.CopyTo(memoryStream, 1024);
				memoryStream.Close();
				result = memoryStream.ToArray();
			}
			return result;
		}

		internal static CompressionMethod ToCompressionMethod(this string value)
		{
			Array values = Enum.GetValues(typeof(CompressionMethod));
			foreach (object obj in values)
			{
				CompressionMethod compressionMethod = (CompressionMethod)obj;
				if (compressionMethod.ToExtensionString(new string[0]) == value)
				{
					return compressionMethod;
				}
			}
			return CompressionMethod.None;
		}

		internal static string ToExtensionString(this CompressionMethod method, params string[] parameters)
		{
			string result;
			if (method == CompressionMethod.None)
			{
				result = string.Empty;
			}
			else
			{
				string text = string.Format(Ext.getString_0(107141773), method.ToString().ToLower());
				result = ((parameters == null || parameters.Length == 0) ? text : string.Format(Ext.getString_0(107141720), text, parameters.ToString(Ext.getString_0(107141739))));
			}
			return result;
		}

		internal static IPAddress ToIPAddress(this string value)
		{
			IPAddress result;
			IPAddress ipaddress;
			if (value == null || value.Length == 0)
			{
				result = null;
			}
			else if (IPAddress.TryParse(value, out ipaddress))
			{
				result = ipaddress;
			}
			else
			{
				try
				{
					IPAddress[] hostAddresses = Dns.GetHostAddresses(value);
					result = hostAddresses[0];
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		internal static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			return new List<TSource>(source);
		}

		internal static string ToString(this IPAddress address, bool bracketIPv6)
		{
			if (bracketIPv6)
			{
				if (address.AddressFamily == AddressFamily.InterNetworkV6)
				{
					return string.Format(Ext.getString_0(107446907), address.ToString());
				}
			}
			return address.ToString();
		}

		internal static ushort ToUInt16(this byte[] source, ByteOrder sourceOrder)
		{
			return BitConverter.ToUInt16(source.ToHostOrder(sourceOrder), 0);
		}

		internal static ulong ToUInt64(this byte[] source, ByteOrder sourceOrder)
		{
			return BitConverter.ToUInt64(source.ToHostOrder(sourceOrder), 0);
		}

		internal static IEnumerable<string> TrimEach(this IEnumerable<string> source)
		{
			Ext.<TrimEach>d__69 <TrimEach>d__ = new Ext.<TrimEach>d__69(-2);
			<TrimEach>d__.<>3__source = source;
			return <TrimEach>d__;
		}

		internal static string TrimSlashFromEnd(this string value)
		{
			string text = value.TrimEnd(new char[]
			{
				'/'
			});
			return (text.Length > 0) ? text : Ext.getString_0(107380359);
		}

		internal static string TrimSlashOrBackslashFromEnd(this string value)
		{
			string text = value.TrimEnd(new char[]
			{
				'/',
				'\\'
			});
			return (text.Length > 0) ? text : value[0].ToString();
		}

		internal static bool TryCreateVersion(this string versionString, out Version result)
		{
			result = null;
			try
			{
				result = new Version(versionString);
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static bool TryCreateWebSocketUri(this string uriString, out Uri result, out string message)
		{
			result = null;
			message = null;
			Uri uri = uriString.ToUri();
			bool result2;
			if (uri == null)
			{
				message = Ext.getString_0(107141734);
				result2 = false;
			}
			else if (!uri.IsAbsoluteUri)
			{
				message = Ext.getString_0(107141701);
				result2 = false;
			}
			else
			{
				string scheme = uri.Scheme;
				if (!(scheme == Ext.getString_0(107141680)) && !(scheme == Ext.getString_0(107363284)))
				{
					message = Ext.getString_0(107141675);
					result2 = false;
				}
				else
				{
					int port = uri.Port;
					if (port == 0)
					{
						message = Ext.getString_0(107141590);
						result2 = false;
					}
					else if (uri.Fragment.Length > 0)
					{
						message = Ext.getString_0(107141557);
						result2 = false;
					}
					else
					{
						result = ((port != -1) ? uri : new Uri(string.Format(Ext.getString_0(107141540), new object[]
						{
							scheme,
							uri.Host,
							(scheme == Ext.getString_0(107141680)) ? 80 : 443,
							uri.PathAndQuery
						})));
						result2 = true;
					}
				}
			}
			return result2;
		}

		internal static bool TryGetUTF8DecodedString(this byte[] bytes, out string s)
		{
			s = null;
			try
			{
				s = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static bool TryGetUTF8EncodedBytes(this string s, out byte[] bytes)
		{
			bytes = null;
			try
			{
				bytes = Encoding.UTF8.GetBytes(s);
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static bool TryOpenRead(this FileInfo fileInfo, out FileStream fileStream)
		{
			fileStream = null;
			try
			{
				fileStream = fileInfo.OpenRead();
			}
			catch
			{
				return false;
			}
			return true;
		}

		internal static string Unquote(this string value)
		{
			int num = value.IndexOf('"');
			string result;
			if (num == -1)
			{
				result = value;
			}
			else
			{
				int num2 = value.LastIndexOf('"');
				if (num2 == num)
				{
					result = value;
				}
				else
				{
					int num3 = num2 - num - 1;
					result = ((num3 > 0) ? value.Substring(num + 1, num3).Replace(Ext.getString_0(107346009), Ext.getString_0(107377100)) : string.Empty);
				}
			}
			return result;
		}

		internal static bool Upgrades(this NameValueCollection headers, string protocol)
		{
			StringComparison comparisonTypeForValue = StringComparison.OrdinalIgnoreCase;
			return headers.Contains(Ext.getString_0(107141003), protocol, StringComparison.OrdinalIgnoreCase) && headers.Contains(Ext.getString_0(107141281), Ext.getString_0(107141003), comparisonTypeForValue);
		}

		internal static string UrlDecode(this string value, Encoding encoding)
		{
			return HttpUtility.UrlDecode(value, encoding);
		}

		internal static string UrlEncode(this string value, Encoding encoding)
		{
			return HttpUtility.UrlEncode(value, encoding);
		}

		internal static void WriteBytes(this Stream stream, byte[] bytes, int bufferLength)
		{
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				memoryStream.CopyTo(stream, bufferLength);
			}
		}

		internal static void WriteBytesAsync(this Stream stream, byte[] bytes, int bufferLength, Action completed, Action<Exception> error)
		{
			MemoryStream src = new MemoryStream(bytes);
			src.CopyToAsync(stream, bufferLength, delegate
			{
				if (completed != null)
				{
					completed();
				}
				src.Dispose();
			}, delegate(Exception ex)
			{
				src.Dispose();
				if (error != null)
				{
					error(ex);
				}
			});
		}

		public static string GetDescription(this HttpStatusCode code)
		{
			return ((int)code).GetStatusDescription();
		}

		public static string GetStatusDescription(this int code)
		{
			if (code <= 207)
			{
				switch (code)
				{
				case 100:
					return Ext.getString_0(107140958);
				case 101:
					return Ext.getString_0(107140977);
				case 102:
					return Ext.getString_0(107140916);
				default:
					switch (code)
					{
					case 200:
						return Ext.getString_0(107140931);
					case 201:
						return Ext.getString_0(107140894);
					case 202:
						return Ext.getString_0(107140913);
					case 203:
						return Ext.getString_0(107140900);
					case 204:
						return Ext.getString_0(107140827);
					case 205:
						return Ext.getString_0(107140842);
					case 206:
						return Ext.getString_0(107140789);
					case 207:
						return Ext.getString_0(107140768);
					}
					break;
				}
			}
			else
			{
				switch (code)
				{
				case 300:
					return Ext.getString_0(107140783);
				case 301:
					return Ext.getString_0(107141238);
				case 302:
					return Ext.getString_0(107141213);
				case 303:
					return Ext.getString_0(107141204);
				case 304:
					return Ext.getString_0(107141223);
				case 305:
					return Ext.getString_0(107141174);
				case 306:
					break;
				case 307:
					return Ext.getString_0(107141193);
				default:
					switch (code)
					{
					case 400:
						return Ext.getString_0(107141168);
					case 401:
						return Ext.getString_0(107141119);
					case 402:
						return Ext.getString_0(107141134);
					case 403:
						return Ext.getString_0(107141077);
					case 404:
						return Ext.getString_0(107141096);
					case 405:
						return Ext.getString_0(107141051);
					case 406:
						return Ext.getString_0(107141026);
					case 407:
						return Ext.getString_0(107141037);
					case 408:
						return Ext.getString_0(107140484);
					case 409:
						return Ext.getString_0(107140463);
					case 410:
						return Ext.getString_0(107140418);
					case 411:
						return Ext.getString_0(107140409);
					case 412:
						return Ext.getString_0(107140420);
					case 413:
						return Ext.getString_0(107140391);
					case 414:
						return Ext.getString_0(107140358);
					case 415:
						return Ext.getString_0(107140329);
					case 416:
						return Ext.getString_0(107140296);
					case 417:
						return Ext.getString_0(107140731);
					case 418:
					case 419:
					case 420:
					case 421:
						break;
					case 422:
						return Ext.getString_0(107140706);
					case 423:
						return Ext.getString_0(107140709);
					case 424:
						return Ext.getString_0(107140668);
					default:
						switch (code)
						{
						case 500:
							return Ext.getString_0(107140675);
						case 501:
							return Ext.getString_0(107140646);
						case 502:
							return Ext.getString_0(107140625);
						case 503:
							return Ext.getString_0(107140576);
						case 504:
							return Ext.getString_0(107140579);
						case 505:
							return Ext.getString_0(107140558);
						case 507:
							return Ext.getString_0(107140521);
						}
						break;
					}
					break;
				}
			}
			return string.Empty;
		}

		public static bool IsCloseStatusCode(this ushort value)
		{
			return value > 999 && value < 5000;
		}

		public static bool IsEnclosedIn(this string value, char c)
		{
			bool result;
			if (value == null)
			{
				result = false;
			}
			else
			{
				int length = value.Length;
				result = (length >= 2 && value[0] == c && value[length - 1] == c);
			}
			return result;
		}

		public static bool IsHostOrder(this ByteOrder order)
		{
			return BitConverter.IsLittleEndian == (order == ByteOrder.Little);
		}

		public static bool IsLocal(this IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107251212));
			}
			bool result;
			if (address.Equals(IPAddress.Any))
			{
				result = true;
			}
			else if (address.Equals(IPAddress.Loopback))
			{
				result = true;
			}
			else
			{
				if (Socket.OSSupportsIPv6)
				{
					if (address.Equals(IPAddress.IPv6Any))
					{
						return true;
					}
					if (address.Equals(IPAddress.IPv6Loopback))
					{
						return true;
					}
				}
				string hostName = Dns.GetHostName();
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostName);
				foreach (IPAddress obj in hostAddresses)
				{
					if (address.Equals(obj))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return value == null || value.Length == 0;
		}

		public static bool IsPredefinedScheme(this string value)
		{
			bool result;
			if (value == null || value.Length < 2)
			{
				result = false;
			}
			else
			{
				char c = value[0];
				if (c == 'h')
				{
					result = (value == Ext.getString_0(107139980) || value == Ext.getString_0(107363293));
				}
				else if (c == 'w')
				{
					result = (value == Ext.getString_0(107141680) || value == Ext.getString_0(107363284));
				}
				else if (c == 'f')
				{
					result = (value == Ext.getString_0(107139971) || value == Ext.getString_0(107139930));
				}
				else if (c == 'g')
				{
					result = (value == Ext.getString_0(107139925));
				}
				else if (c == 'm')
				{
					result = (value == Ext.getString_0(107139948));
				}
				else if (c == 'n')
				{
					c = value[1];
					result = ((c == 'e') ? (value == Ext.getString_0(107139898) || value == Ext.getString_0(107139921) || value == Ext.getString_0(107139908)) : (value == Ext.getString_0(107139939)));
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static bool MaybeUri(this string value)
		{
			bool result;
			if (value == null)
			{
				result = false;
			}
			else if (value.Length == 0)
			{
				result = false;
			}
			else
			{
				int num = value.IndexOf(':');
				if (num == -1)
				{
					result = false;
				}
				else if (num >= 10)
				{
					result = false;
				}
				else
				{
					string value2 = value.Substring(0, num);
					result = value2.IsPredefinedScheme();
				}
			}
			return result;
		}

		public static T[] SubArray<T>(this T[] array, int startIndex, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107299357));
			}
			int num = array.Length;
			T[] result;
			if (num == 0)
			{
				if (startIndex != 0)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107139863));
				}
				if (length != 0)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107344086));
				}
				result = array;
			}
			else
			{
				if (startIndex < 0 || startIndex >= num)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107139863));
				}
				if (length < 0 || length > num - startIndex)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107344086));
				}
				if (length == 0)
				{
					result = new T[0];
				}
				else if (length == num)
				{
					result = array;
				}
				else
				{
					T[] array2 = new T[length];
					Array.Copy(array, startIndex, array2, 0, length);
					result = array2;
				}
			}
			return result;
		}

		public static T[] SubArray<T>(this T[] array, long startIndex, long length)
		{
			if (array == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107299357));
			}
			long num = (long)array.Length;
			T[] result;
			if (num == 0L)
			{
				if (startIndex != 0L)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107139863));
				}
				if (length != 0L)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107344086));
				}
				result = array;
			}
			else
			{
				if (startIndex < 0L || startIndex >= num)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107139863));
				}
				if (length < 0L || length > num - startIndex)
				{
					throw new ArgumentOutOfRangeException(Ext.getString_0(107344086));
				}
				if (length == 0L)
				{
					result = new T[0];
				}
				else if (length == num)
				{
					result = array;
				}
				else
				{
					T[] array2 = new T[length];
					Array.Copy(array, startIndex, array2, 0L, length);
					result = array2;
				}
			}
			return result;
		}

		public static void Times(this int n, Action action)
		{
			if (n > 0 && action != null)
			{
				for (int i = 0; i < n; i++)
				{
					action();
				}
			}
		}

		public static void Times(this long n, Action action)
		{
			if (n > 0L && action != null)
			{
				for (long num = 0L; num < n; num += 1L)
				{
					action();
				}
			}
		}

		public static void Times(this uint n, Action action)
		{
			if (n != 0U && action != null)
			{
				for (uint num = 0U; num < n; num += 1U)
				{
					action();
				}
			}
		}

		public static void Times(this ulong n, Action action)
		{
			if (n != 0UL && action != null)
			{
				for (ulong num = 0UL; num < n; num += 1UL)
				{
					action();
				}
			}
		}

		public static void Times(this int n, Action<int> action)
		{
			if (n > 0 && action != null)
			{
				for (int i = 0; i < n; i++)
				{
					action(i);
				}
			}
		}

		public static void Times(this long n, Action<long> action)
		{
			if (n > 0L && action != null)
			{
				for (long num = 0L; num < n; num += 1L)
				{
					action(num);
				}
			}
		}

		public static void Times(this uint n, Action<uint> action)
		{
			if (n != 0U && action != null)
			{
				for (uint num = 0U; num < n; num += 1U)
				{
					action(num);
				}
			}
		}

		public static void Times(this ulong n, Action<ulong> action)
		{
			if (n != 0UL && action != null)
			{
				for (ulong num = 0UL; num < n; num += 1UL)
				{
					action(num);
				}
			}
		}

		[Obsolete("This method will be removed.")]
		public static T To<T>(this byte[] source, ByteOrder sourceOrder) where T : struct
		{
			if (source == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107247767));
			}
			T result;
			if (source.Length == 0)
			{
				result = default(T);
			}
			else
			{
				Type typeFromHandle = typeof(T);
				byte[] value = source.ToHostOrder(sourceOrder);
				result = ((typeFromHandle == typeof(bool)) ? ((T)((object)BitConverter.ToBoolean(value, 0))) : ((typeFromHandle == typeof(char)) ? ((T)((object)BitConverter.ToChar(value, 0))) : ((typeFromHandle == typeof(double)) ? ((T)((object)BitConverter.ToDouble(value, 0))) : ((typeFromHandle == typeof(short)) ? ((T)((object)BitConverter.ToInt16(value, 0))) : ((typeFromHandle == typeof(int)) ? ((T)((object)BitConverter.ToInt32(value, 0))) : ((typeFromHandle == typeof(long)) ? ((T)((object)BitConverter.ToInt64(value, 0))) : ((typeFromHandle == typeof(float)) ? ((T)((object)BitConverter.ToSingle(value, 0))) : ((typeFromHandle == typeof(ushort)) ? ((T)((object)BitConverter.ToUInt16(value, 0))) : ((typeFromHandle == typeof(uint)) ? ((T)((object)BitConverter.ToUInt32(value, 0))) : ((typeFromHandle == typeof(ulong)) ? ((T)((object)BitConverter.ToUInt64(value, 0))) : default(T)))))))))));
			}
			return result;
		}

		[Obsolete("This method will be removed.")]
		public static byte[] ToByteArray<T>(this T value, ByteOrder order) where T : struct
		{
			Type typeFromHandle = typeof(T);
			byte[] array;
			if (typeFromHandle != typeof(bool))
			{
				if (typeFromHandle != typeof(byte))
				{
					array = ((typeFromHandle == typeof(char)) ? BitConverter.GetBytes((char)((object)value)) : ((typeFromHandle == typeof(double)) ? BitConverter.GetBytes((double)((object)value)) : ((typeFromHandle == typeof(short)) ? BitConverter.GetBytes((short)((object)value)) : ((typeFromHandle == typeof(int)) ? BitConverter.GetBytes((int)((object)value)) : ((typeFromHandle == typeof(long)) ? BitConverter.GetBytes((long)((object)value)) : ((typeFromHandle == typeof(float)) ? BitConverter.GetBytes((float)((object)value)) : ((typeFromHandle == typeof(ushort)) ? BitConverter.GetBytes((ushort)((object)value)) : ((typeFromHandle == typeof(uint)) ? BitConverter.GetBytes((uint)((object)value)) : ((typeFromHandle == typeof(ulong)) ? BitConverter.GetBytes((ulong)((object)value)) : WebSocket.EmptyBytes)))))))));
				}
				else
				{
					(array = new byte[1])[0] = (byte)((object)value);
				}
			}
			else
			{
				array = BitConverter.GetBytes((bool)((object)value));
			}
			byte[] array2 = array;
			if (array2.Length > 1 && !order.IsHostOrder())
			{
				Array.Reverse(array2);
			}
			return array2;
		}

		public static byte[] ToHostOrder(this byte[] source, ByteOrder sourceOrder)
		{
			if (source == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107247767));
			}
			byte[] result;
			if (source.Length < 2)
			{
				result = source;
			}
			else if (sourceOrder.IsHostOrder())
			{
				result = source;
			}
			else
			{
				result = source.Reverse<byte>();
			}
			return result;
		}

		public static string ToString<T>(this T[] array, string separator)
		{
			if (array == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107299357));
			}
			int num = array.Length;
			string result;
			if (num == 0)
			{
				result = string.Empty;
			}
			else
			{
				if (separator == null)
				{
					separator = string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder(64);
				int num2 = num - 1;
				for (int i = 0; i < num2; i++)
				{
					stringBuilder.AppendFormat(Ext.getString_0(107448321), array[i], separator);
				}
				stringBuilder.Append(array[num2].ToString());
				result = stringBuilder.ToString();
			}
			return result;
		}

		public static Uri ToUri(this string value)
		{
			Uri result;
			Uri.TryCreate(value, value.MaybeUri() ? UriKind.Absolute : UriKind.Relative, out result);
			return result;
		}

		[Obsolete("This method will be removed.")]
		public static void WriteContent(this HttpListenerResponse response, byte[] content)
		{
			if (response == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107139878));
			}
			if (content == null)
			{
				throw new ArgumentNullException(Ext.getString_0(107471565));
			}
			long num = (long)content.Length;
			if (num == 0L)
			{
				response.Close();
			}
			else
			{
				response.ContentLength64 = num;
				Stream outputStream = response.OutputStream;
				if (num <= 2147483647L)
				{
					outputStream.Write(content, 0, (int)num);
				}
				else
				{
					outputStream.WriteBytes(content, 1024);
				}
				outputStream.Close();
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static Ext()
		{
			Strings.CreateGetStringDelegate(typeof(Ext));
			Ext._last = new byte[1];
			Ext._retry = 5;
		}

		private static readonly byte[] _last;

		private static readonly int _retry;

		private const string _tspecials = "()<>@,;:\\\"/[]?={} \t";

		[NonSerialized]
		internal static GetString getString_0;
	}
}
