using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Formatting;
using System.Net;
using System.Threading.Tasks;
using IPDetectServer.Lib.WSModels;

namespace IPDetectServer.Lib
{
    public class HttpResponseHelper
    {
        public static Task<HttpResponseMessage> SendAsync(WSErrorResponse response)
        {
            return Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                var responseMessage = new HttpResponseMessage(ConvertToStatusCode(response.Status));

                responseMessage.Content = new ObjectContent(typeof(WSErrorResponse), response, new JsonMediaTypeFormatter());

                responseMessage.Headers.Add(ResponseHeaderConstants.X_Error_Message, RemoveInvalidNewLine(response.Message));
                return responseMessage;
            });
        }

        public static HttpResponseMessage BuildErrorResponse(WSErrorResponse response)
        {
            var responseMessage = new HttpResponseMessage(ConvertToStatusCode(response.Status));

            responseMessage.Content = new ObjectContent(typeof(WSErrorResponse), response, new JsonMediaTypeFormatter());

            responseMessage.Headers.Add(ResponseHeaderConstants.X_Error_Message, RemoveInvalidNewLine(response.Message));
            return responseMessage;
        }

        private static HttpStatusCode ConvertToStatusCode(int status)
        {
            HttpStatusCode result;
            bool isSuccess = Enum.TryParse<HttpStatusCode>(status.ToString(),out result);

            if (!isSuccess)
            {
                result = HttpStatusCode.OK;
            }

            return result;
        }

        public static string RemoveInvalidNewLine(string headerValue)
        {
            if (String.IsNullOrEmpty(headerValue))
            {
                return headerValue;
            }
            else
            {
                return headerValue.Replace("\r\n", "");
            }
        }
    }
}
