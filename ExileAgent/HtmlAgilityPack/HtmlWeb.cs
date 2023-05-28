using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace HtmlAgilityPack
{
	public sealed class HtmlWeb
	{
		public bool AutoDetectEncoding
		{
			get
			{
				return this._autoDetectEncoding;
			}
			set
			{
				this._autoDetectEncoding = value;
			}
		}

		public Encoding OverrideEncoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
			}
		}

		public bool CacheOnly
		{
			get
			{
				return this._cacheOnly;
			}
			set
			{
				if (value && !this.UsingCache)
				{
					throw new HtmlWebException(HtmlWeb.getString_0(107240560));
				}
				this._cacheOnly = value;
			}
		}

		public bool UsingCacheIfExists
		{
			get
			{
				return this._usingCacheIfExists;
			}
			set
			{
				this._usingCacheIfExists = value;
			}
		}

		public string CachePath
		{
			get
			{
				return this._cachePath;
			}
			set
			{
				this._cachePath = value;
			}
		}

		public bool FromCache
		{
			get
			{
				return this._fromCache;
			}
		}

		public int RequestDuration
		{
			get
			{
				return this._requestDuration;
			}
		}

		public Uri ResponseUri
		{
			get
			{
				return this._responseUri;
			}
		}

		public HttpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		public int StreamBufferSize
		{
			get
			{
				return this._streamBufferSize;
			}
			set
			{
				if (this._streamBufferSize <= 0)
				{
					throw new ArgumentException(HtmlWeb.getString_0(107240459));
				}
				this._streamBufferSize = value;
			}
		}

		public bool UseCookies
		{
			get
			{
				return this._useCookies;
			}
			set
			{
				this._useCookies = value;
			}
		}

		public bool CaptureRedirect { get; set; }

		public string UserAgent
		{
			get
			{
				return this._userAgent;
			}
			set
			{
				this._userAgent = value;
			}
		}

		public bool UsingCache
		{
			get
			{
				return this._cachePath != null && this._usingCache;
			}
			set
			{
				if (value && this._cachePath == null)
				{
					throw new HtmlWebException(HtmlWeb.getString_0(107240414));
				}
				this._usingCache = value;
			}
		}

		public static string GetContentTypeForExtension(string extension, string def)
		{
			if (string.IsNullOrEmpty(extension))
			{
				return def;
			}
			string result = HtmlWeb.getString_0(107399944);
			if (!extension.StartsWith(HtmlWeb.getString_0(107371998)))
			{
				extension = HtmlWeb.getString_0(107371998) + extension;
			}
			if (!MimeTypeMap.Mappings.TryGetValue(extension, out result))
			{
				result = def;
			}
			return result;
		}

		public static string GetExtensionForContentType(string contentType, string def)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				return def;
			}
			if (contentType.StartsWith(HtmlWeb.getString_0(107371998)))
			{
				throw new ArgumentException(HtmlWeb.getString_0(107240361) + contentType);
			}
			string result = HtmlWeb.getString_0(107399944);
			if (!MimeTypeMap.Mappings.TryGetValue(contentType, out result))
			{
				result = def;
			}
			return result;
		}

		public object CreateInstance(string url, Type type)
		{
			return this.CreateInstance(url, null, null, type);
		}

		public void Get(string url, string path)
		{
			this.Get(url, path, HtmlWeb.getString_0(107452272));
		}

		public void Get(string url, string path, WebProxy proxy, NetworkCredential credentials)
		{
			this.Get(url, path, proxy, credentials, HtmlWeb.getString_0(107452272));
		}

		public void Get(string url, string path, string method)
		{
			Uri uri = new Uri(url);
			if (!(uri.Scheme == Uri.UriSchemeHttps) && !(uri.Scheme == Uri.UriSchemeHttp))
			{
				throw new HtmlWebException(HtmlWeb.getString_0(107240344) + uri.Scheme + HtmlWeb.getString_0(107239795));
			}
			this.Get(uri, method, path, null, null, null);
		}

		public void Get(string url, string path, WebProxy proxy, NetworkCredential credentials, string method)
		{
			Uri uri = new Uri(url);
			if (!(uri.Scheme == Uri.UriSchemeHttps) && !(uri.Scheme == Uri.UriSchemeHttp))
			{
				throw new HtmlWebException(HtmlWeb.getString_0(107240344) + uri.Scheme + HtmlWeb.getString_0(107239795));
			}
			this.Get(uri, method, path, null, proxy, credentials);
		}

		public string GetCachePath(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException(HtmlWeb.getString_0(107239790));
			}
			if (!this.UsingCache)
			{
				throw new HtmlWebException(HtmlWeb.getString_0(107240560));
			}
			string result;
			if (uri.AbsolutePath == HtmlWeb.getString_0(107375216))
			{
				result = Path.Combine(this._cachePath, HtmlWeb.getString_0(107239753));
			}
			else
			{
				string text = uri.AbsolutePath;
				string text2 = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
				for (int i = 0; i < text2.Length; i++)
				{
					text = text.Replace(text2[i].ToString(), HtmlWeb.getString_0(107399944));
				}
				if (uri.AbsolutePath[uri.AbsolutePath.Length - 1] == Path.AltDirectorySeparatorChar)
				{
					result = Path.Combine(this._cachePath, (uri.Host + text.TrimEnd(new char[]
					{
						Path.AltDirectorySeparatorChar
					})).Replace('/', '\\') + HtmlWeb.getString_0(107239753));
				}
				else
				{
					result = Path.Combine(this._cachePath, uri.Host + text.Replace('/', '\\'));
				}
			}
			return result;
		}

		public HtmlDocument Load(string url)
		{
			return this.Load(url, HtmlWeb.getString_0(107452272));
		}

		public HtmlDocument Load(Uri uri)
		{
			return this.Load(uri, HtmlWeb.getString_0(107452272));
		}

		public HtmlDocument Load(string url, string proxyHost, int proxyPort, string userId, string password)
		{
			WebProxy webProxy = new WebProxy(proxyHost, proxyPort);
			webProxy.BypassProxyOnLocal = true;
			NetworkCredential networkCredential = null;
			if (userId != null && password != null)
			{
				networkCredential = new NetworkCredential(userId, password);
				CredentialCache credentialCache = new CredentialCache();
				credentialCache.Add(webProxy.Address, HtmlWeb.getString_0(107239744), networkCredential);
				credentialCache.Add(webProxy.Address, HtmlWeb.getString_0(107239767), networkCredential);
			}
			return this.Load(url, HtmlWeb.getString_0(107452272), webProxy, networkCredential);
		}

		public HtmlDocument Load(Uri uri, string proxyHost, int proxyPort, string userId, string password)
		{
			WebProxy webProxy = new WebProxy(proxyHost, proxyPort);
			webProxy.BypassProxyOnLocal = true;
			NetworkCredential networkCredential = null;
			if (userId != null && password != null)
			{
				networkCredential = new NetworkCredential(userId, password);
				CredentialCache credentialCache = new CredentialCache();
				credentialCache.Add(webProxy.Address, HtmlWeb.getString_0(107239744), networkCredential);
				credentialCache.Add(webProxy.Address, HtmlWeb.getString_0(107239767), networkCredential);
			}
			return this.Load(uri, HtmlWeb.getString_0(107452272), webProxy, networkCredential);
		}

		public HtmlDocument Load(string url, string method)
		{
			Uri uri = new Uri(url);
			return this.Load(uri, method);
		}

		public HtmlDocument Load(Uri uri, string method)
		{
			if (this.UsingCache)
			{
				this._usingCacheAndLoad = true;
			}
			HtmlDocument htmlDocument;
			if (!(uri.Scheme == Uri.UriSchemeHttps) && !(uri.Scheme == Uri.UriSchemeHttp))
			{
				if (!(uri.Scheme == Uri.UriSchemeFile))
				{
					throw new HtmlWebException(HtmlWeb.getString_0(107240344) + uri.Scheme + HtmlWeb.getString_0(107239795));
				}
				htmlDocument = new HtmlDocument();
				htmlDocument.OptionAutoCloseOnEnd = false;
				htmlDocument.OptionAutoCloseOnEnd = true;
				if (this.OverrideEncoding != null)
				{
					htmlDocument.Load(uri.OriginalString, this.OverrideEncoding);
				}
				else
				{
					htmlDocument.DetectEncodingAndLoad(uri.OriginalString, this._autoDetectEncoding);
				}
			}
			else
			{
				htmlDocument = this.LoadUrl(uri, method, null, null);
			}
			if (this.PreHandleDocument != null)
			{
				this.PreHandleDocument(htmlDocument);
			}
			return htmlDocument;
		}

		public HtmlDocument Load(string url, string method, WebProxy proxy, NetworkCredential credentials)
		{
			Uri uri = new Uri(url);
			return this.Load(uri, method, proxy, credentials);
		}

		public HtmlDocument Load(Uri uri, string method, WebProxy proxy, NetworkCredential credentials)
		{
			if (this.UsingCache)
			{
				this._usingCacheAndLoad = true;
			}
			HtmlDocument htmlDocument;
			if (!(uri.Scheme == Uri.UriSchemeHttps) && !(uri.Scheme == Uri.UriSchemeHttp))
			{
				if (!(uri.Scheme == Uri.UriSchemeFile))
				{
					throw new HtmlWebException(HtmlWeb.getString_0(107240344) + uri.Scheme + HtmlWeb.getString_0(107239795));
				}
				htmlDocument = new HtmlDocument();
				htmlDocument.OptionAutoCloseOnEnd = false;
				htmlDocument.OptionAutoCloseOnEnd = true;
				htmlDocument.DetectEncodingAndLoad(uri.OriginalString, this._autoDetectEncoding);
			}
			else
			{
				htmlDocument = this.LoadUrl(uri, method, proxy, credentials);
			}
			if (this.PreHandleDocument != null)
			{
				this.PreHandleDocument(htmlDocument);
			}
			return htmlDocument;
		}

		public void LoadHtmlAsXml(string htmlUrl, XmlTextWriter writer)
		{
			this.Load(htmlUrl).Save(writer);
		}

		private static void FilePreparePath(string target)
		{
			if (File.Exists(target))
			{
				FileAttributes attributes = File.GetAttributes(target);
				File.SetAttributes(target, attributes & ~FileAttributes.ReadOnly);
				return;
			}
			string directoryName = Path.GetDirectoryName(target);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
		}

		private static DateTime RemoveMilliseconds(DateTime t)
		{
			return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second, 0);
		}

		private static DateTime RemoveMilliseconds(DateTimeOffset? offset)
		{
			DateTimeOffset dateTimeOffset = offset ?? DateTimeOffset.Now;
			return new DateTime(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, 0);
		}

		private static long SaveStream(Stream stream, string path, DateTime touchDate, int streamBufferSize)
		{
			HtmlWeb.FilePreparePath(path);
			long num = 0L;
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						byte[] array;
						do
						{
							array = binaryReader.ReadBytes(streamBufferSize);
							num += (long)array.Length;
							if (array.Length != 0)
							{
								binaryWriter.Write(array);
							}
						}
						while (array.Length != 0);
						binaryWriter.Flush();
					}
				}
			}
			File.SetLastWriteTime(path, touchDate);
			return num;
		}

		private HttpStatusCode Get(Uri uri, string method, string path, HtmlDocument doc, IWebProxy proxy, ICredentials creds)
		{
			string text = null;
			bool flag = false;
			HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
			httpWebRequest.Method = method;
			httpWebRequest.UserAgent = this.UserAgent;
			if (this.CaptureRedirect)
			{
				httpWebRequest.AllowAutoRedirect = false;
			}
			if (proxy != null)
			{
				if (creds != null)
				{
					proxy.Credentials = creds;
					httpWebRequest.Credentials = creds;
				}
				else
				{
					proxy.Credentials = CredentialCache.DefaultCredentials;
					httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
				}
				httpWebRequest.Proxy = proxy;
			}
			this._fromCache = false;
			this._requestDuration = 0;
			int tickCount = Environment.TickCount;
			if (this.UsingCache)
			{
				text = this.GetCachePath(httpWebRequest.RequestUri);
				if (File.Exists(text))
				{
					httpWebRequest.IfModifiedSince = File.GetLastAccessTime(text);
					flag = true;
				}
			}
			if (this._cacheOnly || this._usingCacheIfExists)
			{
				if (File.Exists(text))
				{
					if (path != null)
					{
						IOLibrary.CopyAlways(text, path);
						if (text != null)
						{
							File.SetLastWriteTime(path, File.GetLastWriteTime(text));
						}
					}
					this._fromCache = true;
					return HttpStatusCode.NotModified;
				}
				if (this._cacheOnly)
				{
					throw new HtmlWebException(HtmlWeb.getString_0(107239758) + text + HtmlWeb.getString_0(107455641));
				}
			}
			if (this._useCookies)
			{
				httpWebRequest.CookieContainer = new CookieContainer();
			}
			if (this.PreRequest != null && !this.PreRequest(httpWebRequest))
			{
				return HttpStatusCode.ResetContent;
			}
			HttpWebResponse httpWebResponse;
			HttpStatusCode result;
			try
			{
				httpWebResponse = (httpWebRequest.GetResponse() as HttpWebResponse);
				goto IL_1B0;
			}
			catch (WebException ex)
			{
				this._requestDuration = Environment.TickCount - tickCount;
				httpWebResponse = (HttpWebResponse)ex.Response;
				if (httpWebResponse != null)
				{
					goto IL_1B0;
				}
				if (!flag)
				{
					throw;
				}
				if (path != null)
				{
					IOLibrary.CopyAlways(text, path);
					File.SetLastWriteTime(path, File.GetLastWriteTime(text));
				}
				result = HttpStatusCode.NotModified;
			}
			catch (Exception)
			{
				this._requestDuration = Environment.TickCount - tickCount;
				throw;
			}
			return result;
			IL_1B0:
			if (this.PostResponse != null)
			{
				this.PostResponse(httpWebRequest, httpWebResponse);
			}
			this._requestDuration = Environment.TickCount - tickCount;
			this._responseUri = httpWebResponse.ResponseUri;
			HttpStatusCode statusCode = httpWebResponse.StatusCode;
			bool flag2 = this.IsHtmlContent(httpWebResponse.ContentType);
			bool flag3 = string.IsNullOrEmpty(httpWebResponse.ContentType);
			Encoding encoding = (!string.IsNullOrEmpty(flag2 ? httpWebResponse.CharacterSet : httpWebResponse.ContentEncoding)) ? Encoding.GetEncoding(flag2 ? httpWebResponse.CharacterSet : httpWebResponse.ContentEncoding) : null;
			if (this.OverrideEncoding != null)
			{
				encoding = this.OverrideEncoding;
			}
			if (this.CaptureRedirect && httpWebResponse.StatusCode == HttpStatusCode.Found)
			{
				string text2 = httpWebResponse.Headers[HtmlWeb.getString_0(107239677)];
				Uri uri2;
				if (!Uri.TryCreate(text2, UriKind.Absolute, out uri2))
				{
					uri2 = new Uri(uri, text2);
				}
				return this.Get(uri2, HtmlWeb.getString_0(107452272), path, doc, proxy, creds);
			}
			if (httpWebResponse.StatusCode != HttpStatusCode.NotModified)
			{
				Stream responseStream = httpWebResponse.GetResponseStream();
				if (responseStream != null)
				{
					if (this.UsingCache)
					{
						HtmlWeb.SaveStream(responseStream, text, HtmlWeb.RemoveMilliseconds(httpWebResponse.LastModified), this._streamBufferSize);
						this.SaveCacheHeaders(httpWebRequest.RequestUri, httpWebResponse);
						if (path != null)
						{
							IOLibrary.CopyAlways(text, path);
							File.SetLastWriteTime(path, File.GetLastWriteTime(text));
						}
						if (this._usingCacheAndLoad)
						{
							doc.Load(text);
						}
					}
					else
					{
						if (doc != null && flag2)
						{
							if (encoding == null)
							{
								doc.Load(responseStream, true);
							}
							else
							{
								doc.Load(responseStream, encoding);
							}
						}
						if (doc != null && flag3)
						{
							try
							{
								if (encoding == null)
								{
									doc.Load(responseStream, true);
								}
								else
								{
									doc.Load(responseStream, encoding);
								}
							}
							catch
							{
							}
						}
					}
					httpWebResponse.Close();
				}
				return statusCode;
			}
			if (this.UsingCache)
			{
				this._fromCache = true;
				if (path != null)
				{
					IOLibrary.CopyAlways(text, path);
					File.SetLastWriteTime(path, File.GetLastWriteTime(text));
				}
				return httpWebResponse.StatusCode;
			}
			throw new HtmlWebException(HtmlWeb.getString_0(107239696));
		}

		private string GetCacheHeader(Uri requestUri, string name, string def)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(this.GetCacheHeadersPath(requestUri));
			XmlNode xmlNode = xmlDocument.SelectSingleNode(HtmlWeb.getString_0(107239587) + name.ToUpperInvariant() + HtmlWeb.getString_0(107240026));
			if (xmlNode == null)
			{
				return def;
			}
			return xmlNode.Attributes[name].Value;
		}

		private string GetCacheHeadersPath(Uri uri)
		{
			return this.GetCachePath(uri) + HtmlWeb.getString_0(107240021);
		}

		private bool IsCacheHtmlContent(string path)
		{
			string contentTypeForExtension = HtmlWeb.GetContentTypeForExtension(Path.GetExtension(path), null);
			return this.IsHtmlContent(contentTypeForExtension);
		}

		private bool IsHtmlContent(string contentType)
		{
			return contentType.ToLowerInvariant().StartsWith(HtmlWeb.getString_0(107240012));
		}

		private bool IsGZipEncoding(string contentEncoding)
		{
			return contentEncoding.ToLowerInvariant().StartsWith(HtmlWeb.getString_0(107239967));
		}

		private HtmlDocument LoadUrl(Uri uri, string method, WebProxy proxy, NetworkCredential creds)
		{
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.OptionAutoCloseOnEnd = false;
			htmlDocument.OptionFixNestedTags = true;
			this._statusCode = this.Get(uri, method, null, htmlDocument, proxy, creds);
			if (this._statusCode == HttpStatusCode.NotModified)
			{
				htmlDocument.DetectEncodingAndLoad(this.GetCachePath(uri));
			}
			return htmlDocument;
		}

		private void SaveCacheHeaders(Uri requestUri, HttpWebResponse resp)
		{
			string cacheHeadersPath = this.GetCacheHeadersPath(requestUri);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(HtmlWeb.getString_0(107239990));
			XmlNode firstChild = xmlDocument.FirstChild;
			foreach (object obj in resp.Headers)
			{
				string text = (string)obj;
				XmlNode xmlNode = xmlDocument.CreateElement(HtmlWeb.getString_0(107398713));
				XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(HtmlWeb.getString_0(107387739));
				xmlAttribute.Value = text;
				xmlNode.Attributes.Append(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute(HtmlWeb.getString_0(107398667));
				xmlAttribute.Value = resp.Headers[text];
				xmlNode.Attributes.Append(xmlAttribute);
				firstChild.AppendChild(xmlNode);
			}
			xmlDocument.Save(cacheHeadersPath);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url)
		{
			return this.LoadFromWebAsync(new Uri(url), null, null);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), null, null, cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding)
		{
			return this.LoadFromWebAsync(new Uri(url), encoding, null, CancellationToken.None);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), encoding, null, cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password)
		{
			return this.LoadFromWebAsync(new Uri(url), encoding, new NetworkCredential(userName, password), CancellationToken.None);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), encoding, new NetworkCredential(userName, password), cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password, string domain)
		{
			return this.LoadFromWebAsync(new Uri(url), encoding, new NetworkCredential(userName, password, domain), CancellationToken.None);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, Encoding encoding, string userName, string password, string domain, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), encoding, new NetworkCredential(userName, password, domain), cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password, string domain)
		{
			return this.LoadFromWebAsync(new Uri(url), null, new NetworkCredential(userName, password, domain), CancellationToken.None);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password, string domain, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), null, new NetworkCredential(userName, password, domain), cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password)
		{
			return this.LoadFromWebAsync(new Uri(url), null, new NetworkCredential(userName, password), CancellationToken.None);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, string userName, string password, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), null, new NetworkCredential(userName, password), cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, NetworkCredential credentials)
		{
			return this.LoadFromWebAsync(new Uri(url), null, credentials, CancellationToken.None);
		}

		public Task<HtmlDocument> LoadFromWebAsync(string url, NetworkCredential credentials, CancellationToken cancellationToken)
		{
			return this.LoadFromWebAsync(new Uri(url), null, credentials, cancellationToken);
		}

		public Task<HtmlDocument> LoadFromWebAsync(Uri uri, Encoding encoding, NetworkCredential credentials)
		{
			return this.LoadFromWebAsync(uri, encoding, credentials, CancellationToken.None);
		}

		public async Task<HtmlDocument> LoadFromWebAsync(Uri uri, Encoding encoding, NetworkCredential credentials, CancellationToken cancellationToken)
		{
			HtmlDocument doc = new HtmlDocument();
			HttpClientHandler httpClientHandler = new HttpClientHandler();
			if (credentials == null)
			{
				httpClientHandler.UseDefaultCredentials = true;
			}
			else
			{
				httpClientHandler.Credentials = credentials;
			}
			if (this.CaptureRedirect)
			{
				httpClientHandler.AllowAutoRedirect = false;
			}
			HttpResponseMessage httpResponseMessage = await new HttpClient(httpClientHandler)
			{
				DefaultRequestHeaders = 
				{
					{
						HtmlWeb.<LoadFromWebAsync>d__103.getString_0(107320559),
						this.UserAgent
					}
				}
			}.GetAsync(uri, cancellationToken).ConfigureAwait(false);
			string text = string.Empty;
			ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
			if (encoding != null)
			{
				ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter configuredTaskAwaiter = httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter);
				}
				using (StreamReader streamReader = new StreamReader(configuredTaskAwaiter.GetResult(), encoding))
				{
					text = streamReader.ReadToEnd();
					goto IL_213;
				}
			}
			else
			{
				configuredTaskAwaiter3 = httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter();
				if (configuredTaskAwaiter3.IsCompleted)
				{
					goto IL_20A;
				}
				await configuredTaskAwaiter3;
			}
			ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
			configuredTaskAwaiter3 = configuredTaskAwaiter4;
			configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
			IL_20A:
			text = configuredTaskAwaiter3.GetResult();
			IL_213:
			if (this.PreHandleDocument != null)
			{
				this.PreHandleDocument(doc);
			}
			if (text != null)
			{
				doc.LoadHtml(text);
			}
			return doc;
		}

		public TimeSpan BrowserTimeout
		{
			get
			{
				return this._browserTimeout;
			}
			set
			{
				this._browserTimeout = value;
			}
		}

		public TimeSpan BrowserDelay
		{
			get
			{
				return this._browserDelay;
			}
			set
			{
				this._browserDelay = value;
			}
		}

		public HtmlDocument LoadFromBrowser(string url)
		{
			return this.LoadFromBrowser(url, (object browser) => true);
		}

		internal string WebBrowserOuterHtml(object webBrowser)
		{
			try
			{
				PropertyInfo property = webBrowser.GetType().GetProperty(HtmlWeb.getString_0(107239945));
				this._responseUri = (Uri)property.GetValue(webBrowser, null);
			}
			catch
			{
			}
			object value = webBrowser.GetType().GetProperty(HtmlWeb.getString_0(107239940)).GetValue(webBrowser, null);
			MethodBase method = value.GetType().GetMethod(HtmlWeb.getString_0(107239959), new Type[]
			{
				typeof(string)
			});
			object obj = value;
			object[] parameters = new string[]
			{
				HtmlWeb.getString_0(107239930)
			};
			object obj2 = method.Invoke(obj, parameters);
			object value2 = obj2.GetType().GetProperty(HtmlWeb.getString_0(107239921), new Type[]
			{
				typeof(int)
			}).GetValue(obj2, new object[]
			{
				0
			});
			return (string)value2.GetType().GetProperty(HtmlWeb.getString_0(107239880)).GetValue(value2, null);
		}

		public HtmlDocument LoadFromBrowser(string url, Func<string, bool> isBrowserScriptCompleted = null)
		{
			return this.LoadFromBrowser(url, (object browser) => isBrowserScriptCompleted == null || isBrowserScriptCompleted(this.WebBrowserOuterHtml(browser)));
		}

		public HtmlDocument LoadFromBrowser(string url, Func<object, bool> isBrowserScriptCompleted = null)
		{
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((Assembly x) => x.GetName().Name == HtmlWeb.<>c.getString_0(107239908));
			if (assembly == null)
			{
				try
				{
					Assembly assembly2 = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((Assembly x) => x.GetName().Name == HtmlWeb.<>c.getString_0(107239917));
					if (assembly2 != null)
					{
						Assembly.LoadFile(assembly2.CodeBase.Replace(HtmlWeb.getString_0(107239899), HtmlWeb.getString_0(107239890)).Replace(HtmlWeb.getString_0(107239861), HtmlWeb.getString_0(107399944)));
					}
				}
				catch (Exception)
				{
				}
				assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((Assembly x) => x.GetName().Name == HtmlWeb.<>c.getString_0(107239908));
				if (assembly == null)
				{
					throw new Exception(HtmlWeb.getString_0(107239816));
				}
			}
			Type type = assembly.GetType(HtmlWeb.getString_0(107239498));
			ConstructorInfo constructor = type.GetConstructor(new Type[0]);
			MethodInfo method = assembly.GetType(HtmlWeb.getString_0(107239453)).GetMethod(HtmlWeb.getString_0(107239440));
			Uri uri = new Uri(url);
			HtmlDocument htmlDocument = new HtmlDocument();
			string message = HtmlWeb.getString_0(107239395);
			using (IDisposable disposable = (IDisposable)constructor.Invoke(new object[0]))
			{
				type.GetProperty(HtmlWeb.getString_0(107238629)).SetValue(disposable, true, null);
				type.GetMethod(HtmlWeb.getString_0(107238596), new Type[]
				{
					typeof(Uri)
				}).Invoke(disposable, new object[]
				{
					uri
				});
				PropertyInfo property = type.GetProperty(HtmlWeb.getString_0(107238615));
				PropertyInfo property2 = type.GetProperty(HtmlWeb.getString_0(107238566));
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while ((int)property.GetValue(disposable, null) != 4 || (bool)property2.GetValue(disposable, null))
				{
					if (this.BrowserTimeout.TotalMilliseconds != 0.0 && (double)stopwatch.ElapsedMilliseconds > this.BrowserTimeout.TotalMilliseconds)
					{
						throw new Exception(message);
					}
					method.Invoke(null, new object[0]);
					Thread.Sleep(this._browserDelay);
				}
				if (isBrowserScriptCompleted != null)
				{
					while (!isBrowserScriptCompleted(disposable))
					{
						if (this.BrowserTimeout.TotalMilliseconds != 0.0 && (double)stopwatch.ElapsedMilliseconds > this.BrowserTimeout.TotalMilliseconds)
						{
							this.WebBrowserOuterHtml(disposable);
							throw new Exception(message);
						}
						method.Invoke(null, new object[0]);
						Thread.Sleep(this._browserDelay);
					}
				}
				string html = this.WebBrowserOuterHtml(disposable);
				htmlDocument.LoadHtml(html);
			}
			return htmlDocument;
		}

		public object CreateInstance(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, Type type)
		{
			return this.CreateInstance(htmlUrl, xsltUrl, xsltArgs, type, null);
		}

		public object CreateInstance(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, Type type, string xmlPath)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			if (xsltUrl == null)
			{
				this.LoadHtmlAsXml(htmlUrl, xmlTextWriter);
			}
			else if (xmlPath == null)
			{
				this.LoadHtmlAsXml(htmlUrl, xsltUrl, xsltArgs, xmlTextWriter);
			}
			else
			{
				this.LoadHtmlAsXml(htmlUrl, xsltUrl, xsltArgs, xmlTextWriter, xmlPath);
			}
			xmlTextWriter.Flush();
			XmlTextReader xmlReader = new XmlTextReader(new StringReader(stringWriter.ToString()));
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			object result;
			try
			{
				result = xmlSerializer.Deserialize(xmlReader);
			}
			catch (InvalidOperationException ex)
			{
				string str = (ex != null) ? ex.ToString() : null;
				string str2 = HtmlWeb.getString_0(107238557);
				StringWriter stringWriter2 = stringWriter;
				throw new Exception(str + str2 + ((stringWriter2 != null) ? stringWriter2.ToString() : null));
			}
			return result;
		}

		public void LoadHtmlAsXml(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, XmlTextWriter writer)
		{
			this.LoadHtmlAsXml(htmlUrl, xsltUrl, xsltArgs, writer, null);
		}

		public void LoadHtmlAsXml(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, XmlTextWriter writer, string xmlPath)
		{
			if (htmlUrl == null)
			{
				throw new ArgumentNullException(HtmlWeb.getString_0(107238572));
			}
			HtmlDocument htmlDocument = this.Load(htmlUrl);
			if (xmlPath != null)
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(xmlPath, htmlDocument.Encoding);
				htmlDocument.Save(xmlTextWriter);
				xmlTextWriter.Close();
			}
			if (xsltArgs == null)
			{
				xsltArgs = new XsltArgumentList();
			}
			xsltArgs.AddParam(HtmlWeb.getString_0(107398421), HtmlWeb.getString_0(107399944), htmlUrl);
			xsltArgs.AddParam(HtmlWeb.getString_0(107238527), HtmlWeb.getString_0(107399944), this.RequestDuration);
			xsltArgs.AddParam(HtmlWeb.getString_0(107239018), HtmlWeb.getString_0(107399944), this.FromCache);
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(xsltUrl);
			xslCompiledTransform.Transform(htmlDocument, xsltArgs, writer);
		}

		static HtmlWeb()
		{
			Strings.CreateGetStringDelegate(typeof(HtmlWeb));
		}

		private bool _autoDetectEncoding = true;

		private bool _cacheOnly;

		private string _cachePath;

		private bool _fromCache;

		private int _requestDuration;

		private Uri _responseUri;

		private HttpStatusCode _statusCode = HttpStatusCode.OK;

		private int _streamBufferSize = 1024;

		private bool _useCookies;

		private bool _usingCache;

		private bool _usingCacheAndLoad;

		private bool _usingCacheIfExists;

		private string _userAgent = HtmlWeb.getString_0(107239005);

		public HtmlWeb.PostResponseHandler PostResponse;

		public HtmlWeb.PreHandleDocumentHandler PreHandleDocument;

		public HtmlWeb.PreRequestHandler PreRequest;

		private Encoding _encoding;

		private TimeSpan _browserTimeout = TimeSpan.FromSeconds(30.0);

		private TimeSpan _browserDelay = TimeSpan.FromMilliseconds(100.0);

		[NonSerialized]
		internal static GetString getString_0;

		public delegate void PostResponseHandler(HttpWebRequest request, HttpWebResponse response);

		public delegate void PreHandleDocumentHandler(HtmlDocument document);

		public delegate bool PreRequestHandler(HttpWebRequest request);
	}
}
