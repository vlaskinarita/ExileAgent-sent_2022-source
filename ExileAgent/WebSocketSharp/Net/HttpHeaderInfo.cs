using System;

namespace WebSocketSharp.Net
{
	internal sealed class HttpHeaderInfo
	{
		internal HttpHeaderInfo(string headerName, HttpHeaderType headerType)
		{
			this._headerName = headerName;
			this._headerType = headerType;
		}

		internal bool IsMultiValueInRequest
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.MultiValueInRequest;
				return httpHeaderType == HttpHeaderType.MultiValueInRequest;
			}
		}

		internal bool IsMultiValueInResponse
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.MultiValueInResponse;
				return httpHeaderType == HttpHeaderType.MultiValueInResponse;
			}
		}

		public string HeaderName
		{
			get
			{
				return this._headerName;
			}
		}

		public HttpHeaderType HeaderType
		{
			get
			{
				return this._headerType;
			}
		}

		public bool IsRequest
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.Request;
				return httpHeaderType == HttpHeaderType.Request;
			}
		}

		public bool IsResponse
		{
			get
			{
				HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.Response;
				return httpHeaderType == HttpHeaderType.Response;
			}
		}

		public bool IsMultiValue(bool response)
		{
			HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.MultiValue;
			bool result;
			if (httpHeaderType != HttpHeaderType.MultiValue)
			{
				result = (response ? this.IsMultiValueInResponse : this.IsMultiValueInRequest);
			}
			else
			{
				result = (response ? this.IsResponse : this.IsRequest);
			}
			return result;
		}

		public bool IsRestricted(bool response)
		{
			HttpHeaderType httpHeaderType = this._headerType & HttpHeaderType.Restricted;
			return httpHeaderType == HttpHeaderType.Restricted && (response ? this.IsResponse : this.IsRequest);
		}

		private string _headerName;

		private HttpHeaderType _headerType;
	}
}
