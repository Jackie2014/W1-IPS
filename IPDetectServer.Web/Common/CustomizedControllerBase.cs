using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using IPDetectServer.Lib;
using log4net;
using System.Configuration;

namespace IPDetectServer.Web
{
    /// <summary>
    /// Customized controller base
    /// </summary>
    public abstract class CustomizedControllerBase : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizedControllerBase"/> class.
        /// </summary>
        public CustomizedControllerBase()
        {
        }

        /// <summary>
        /// Login Redirect Url 
        /// </summary>
        const string LOGIN_REDIRECT_URL_KEY = "LoginRedirectUrl";

        /// <summary>
        /// Get an instance of ILog.
        /// </summary>
        protected ILog Log
        {
            get
            {
               return LogFactory.GetLogger(this.GetType());
            }
        }

        /// <summary>
        /// Log exception.
        /// </summary>
        /// <param name="ex">The excepion.</param>
        /// <returns>return the log id.</returns>
        protected void LogException(Exception ex)
        {
            Log.Error(ex);
        }

        /// <summary>
        /// Executes the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext"/> parameter is null.</exception>
        protected override void Execute(RequestContext requestContext)
        {
            base.Execute(requestContext);
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            //var requestParam = "Input Parameters:(" + requestContext.HttpContext.Request.Form.ToString() + ")";
            Context.ClientIP = GetClientIP(requestContext);
            return base.BeginExecute(requestContext, callback, state);
        }

        /// <summary>
        /// Called after the action result that is returned by an action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action result</param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
       
        /// <summary>
        /// Called before the action result that is returned by an action method is executed.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action result</param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// Called after the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isAuthenticationIgnored = this.IsAuthenticationIgnored(filterContext);

            if (!isAuthenticationIgnored)
            {
                var isAuthenticated = SessionManager.IsLogin;

                if (!isAuthenticated)
                {
                    var redirectUrl = this.GetLoginRedirectUrl();

                    if (String.IsNullOrWhiteSpace(redirectUrl))
                    {
                        throw new Exception("No configured Login Redirection Url.");
                    }

                    filterContext.Result = new EmptyResult();

                    filterContext.HttpContext.Response.Redirect(redirectUrl + "?returnUrl="
                                        + HttpUtility.UrlEncode(filterContext.HttpContext.Request.RawUrl));
                }
            }

            base.OnActionExecuting(filterContext);
        }


        /// <summary>
        /// Determines whether current request is authentication ignored.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <returns>
        /// <c>true</c> if current request is authentication ignored; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAuthenticationIgnored(ActionExecutingContext filterContext)
        {
            var controllerAttributes = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(true);
            var actionAttributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            var hasIgnoreControllerAuthentication = controllerAttributes.Where(a => a is IgnoreAuthenticationAttribute).Count() > 0;
            var hasIgnoreActionAuthentication = actionAttributes.Where(a => a is IgnoreAuthenticationAttribute).Count() > 0;
            var result = hasIgnoreControllerAuthentication || hasIgnoreActionAuthentication;
            return result;
        }

        /// <summary>
        /// Gets the login redirect URL.
        /// </summary>
        /// <returns>the login redirect URL.</returns>
        private string GetLoginRedirectUrl()
        {
            string key_name = LOGIN_REDIRECT_URL_KEY;
            string result = ConfigurationManager.AppSettings[key_name];

            if (String.IsNullOrWhiteSpace(result) && FormsAuthentication.IsEnabled)
            {
                result = FormsAuthentication.LoginUrl;
            }

            return result;
        }

        private string GetClientIP(RequestContext request)
        {
            String clientIP = "";
            try
            {
                if (request.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    clientIP = request.HttpContext.Request.UserHostAddress;
                }
                else
                {
                    clientIP = request.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
            }
            catch
            { }

            return clientIP;
        }
    }
}
