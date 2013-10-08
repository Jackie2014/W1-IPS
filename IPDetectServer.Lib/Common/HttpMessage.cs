using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web;

namespace IPDetectServer.Lib
{
    public class HttpMessage
    {
        private HttpRequestMessage _message;

        public HttpMessage(HttpRequestMessage request)
        {
            _message = request;
        }

        public string GetHeaderValue(string headerKey)
        {
            string result = String.Empty;
            if (_message.Headers.Contains(headerKey))
            {
                result = _message.Headers.GetValues(headerKey).ToArray()[0];
            }

            return result;
        }

        public string GetQueryString(string key)
        {
            var queryStrings = _message.RequestUri.ParseQueryString();
            return queryStrings[key];
        }

        public string GetClientIP()
        {
            return ((HttpContextBase)_message.Properties["MS_HttpContext"]).Request.UserHostAddress;
        }

        public string GetClientLanguate()
        {
            return ((HttpContextBase)_message.Properties["MS_HttpContext"]).Request.UserLanguages.FirstOrDefault();
        }

        public string GetRequestUri()
        {
            return _message.RequestUri.ToString();
        }

        public string GetRequestUriTemplate()
        {
            return _message.GetRouteData().Route.RouteTemplate;
        }

        public string GetHttpMethod()
        {
            return _message.Method.ToString();
        }

        public string GetHttpHeader()
        {
            return _message.Headers.ToString().Replace("\r\n", "  ");
        }

        public string GetContentType()
        {
            return ((HttpContextBase)_message.Properties["MS_HttpContext"]).Request.ContentType;
            //return _message.Content.Headers.ContentType.ToString();
        }

        public int GetContentLength()
        {
            return ((HttpContextBase)_message.Properties["MS_HttpContext"]).Request.ContentLength;
            //return _message.Content.Headers.ContentLength.HasValue ? (int)_message.Content.Headers.ContentLength.Value : 0;
        }

        public string GetToken()
        {
            return GetHeaderValue(ResponseHeaderConstants.X_Standard_Header_Authorization);
        }
    }
}
