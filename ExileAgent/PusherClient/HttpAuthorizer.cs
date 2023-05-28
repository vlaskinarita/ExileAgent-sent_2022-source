using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PusherClient
{
	public sealed class HttpAuthorizer : IAuthorizer, IAuthorizerAsync
	{
		public HttpAuthorizer(string authEndpoint)
		{
			this._authEndpoint = new Uri(authEndpoint);
		}

		public AuthenticationHeaderValue AuthenticationHeader { get; set; }

		public TimeSpan? Timeout { get; set; }

		public string Authorize(string channelName, string socketId)
		{
			string result;
			try
			{
				result = this.AuthorizeAsync(channelName, socketId).Result;
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		[DebuggerStepThrough]
		public Task<string> AuthorizeAsync(string channelName, string socketId)
		{
			HttpAuthorizer.<AuthorizeAsync>d__11 <AuthorizeAsync>d__ = new HttpAuthorizer.<AuthorizeAsync>d__11();
			<AuthorizeAsync>d__.<>4__this = this;
			<AuthorizeAsync>d__.channelName = channelName;
			<AuthorizeAsync>d__.socketId = socketId;
			<AuthorizeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<AuthorizeAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<string> <>t__builder = <AuthorizeAsync>d__.<>t__builder;
			<>t__builder.Start<HttpAuthorizer.<AuthorizeAsync>d__11>(ref <AuthorizeAsync>d__);
			return <AuthorizeAsync>d__.<>t__builder.Task;
		}

		public void PreAuthorize(HttpClient httpClient)
		{
			if (this.Timeout != null)
			{
				httpClient.Timeout = this.Timeout.Value;
			}
			if (this.AuthenticationHeader != null)
			{
				httpClient.DefaultRequestHeaders.Authorization = this.AuthenticationHeader;
			}
		}

		private readonly Uri _authEndpoint;
	}
}
