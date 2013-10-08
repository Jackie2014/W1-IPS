using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using IPDetectServer.Lib;
using IPDetectServer.Lib.WSModels;
using IPDetectServer.Lib.Exceptions;
using IPDetectServer.Repositories;
using log4net;
using System.Diagnostics;


namespace IPDetectServer.Lib.APIHandlers
{
    public class MessageContextHandler : DelegatingHandler
    {
        private static ILog Log = LogFactory.GetLogger("MessageContextHandler(API request and Response)");

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            HttpMessage message = new HttpMessage(request);
            string requestData = request.Content.ReadAsStringAsync().Result;
            StringBuilder requestContent = new StringBuilder();

            #region log

            try
            {
                if (Log.IsInfoEnabled)
                {

                    string api = message.GetRequestUriTemplate();

                    requestContent.Append("\r\n\tRequest ClientIP : ").Append(message.GetClientIP()).Append("\r\n");
                    requestContent.Append("\tRequest URL : ").Append(message.GetHttpMethod()).Append(" ").Append(message.GetRequestUri()).Append("\r\n");
                    requestContent.Append("\tRequest Hearder : ");
                    requestContent.Append(message.GetHttpHeader());

                    if (!String.IsNullOrEmpty(requestData))
                    {
                        requestContent.Append("\r\n\tRequest Body : ").Append(requestData);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
            }

            #endregion

            SetContext(request);

            // Token validation
            if (Context.RequestUriTemplate == "apis/v1/token")
            {
                return base.SendAsync(request, cancellationToken);
            }

            if (String.IsNullOrEmpty(Context.Token))
            {
                return HttpResponseHelper.SendAsync(UnAuthenticationResponse());
            }
            else
            {
                TokenRepository tr = new TokenRepository();
                var tokenModel = tr.GetTokenModel(Context.Token);
                if (tokenModel == null)
                {
                    return HttpResponseHelper.SendAsync(UnAuthenticationResponse());
                }
                else
                {
                    Context.LoginName = tokenModel.UserName;
                    var response = base.SendAsync(request, cancellationToken);

                    #region Response Log

                    if (Log.IsInfoEnabled)
                    {
                        try
                        {
                            Task<string> responseMessage = null;

                            if (response.Result != null && response.Result.Content != null)
                            {
                                responseMessage = response.Result.Content.ReadAsStringAsync();
                            }

                            watch.Stop();
                            int requestExcuteTime = Convert.ToInt32(watch.ElapsedMilliseconds);
                            watch = null;
                            requestContent.Append("\r\n\tResponse : ").Append(responseMessage.Result);
                            requestContent.Append("\r\n\tCost(ms) : ").Append(requestExcuteTime);
                            
                            Log.Info(requestContent.ToString());
                        }
                        catch (Exception exception)
                        {
                            Log.Error(exception.ToString());
                        }
                    }
                    #endregion

                    return response;
                }
            }
        }

        private void SetContext(HttpRequestMessage request)
        {
            // Get neccessary data from http request message
            HttpMessage message = new HttpMessage(request);
            Context.ClientIP = message.GetClientIP();
            //Context.IsAuthed = false;
            Context.Token = message.GetToken();
            Context.RequestUri = message.GetRequestUri();
            Context.RequestUriTemplate = message.GetRequestUriTemplate();
            Context.HttpMethod = message.GetHttpMethod();
            Context.HttpHeader = message.GetHttpHeader();
            Context.ContentType = message.GetContentType();
            Context.ContentLength = message.GetContentLength();
        }

        private WSErrorResponse UnAuthenticationResponse()
        {
            WSErrorResponse unauth = new WSErrorResponse
            {
                Code = Errors.invalid_token_401,
                Message = "未经授权的访问!",
                Status = 401
            };

            return unauth;
        }
    }
}
