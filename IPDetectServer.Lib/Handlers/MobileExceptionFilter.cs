using IPDetectServer.Lib.Exceptions;
using IPDetectServer.Lib.WSModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace IPDetectServer.Lib.APIHandlers
{
    public class MobileExceptionFilter : ExceptionFilterAttribute  
    {
        public override void OnException(HttpActionExecutedContext context) 
        {
            WSErrorResponse error = new WSErrorResponse
            {
                Message = context.Exception == null? null : context.Exception.Message,
                More = context.Exception == null? null : context.Exception.ToString()
            };

            if (context.Exception is BadRequestException) 
            {
                error.Status = (int)HttpStatusCode.BadRequest;

                if (String.IsNullOrWhiteSpace(error.Code))
                {
                    error.Code = Errors.bad_request_400;
                }
            }
            else if (context.Exception is DuplicateObjectException)
            {
                error.Status = (int)HttpStatusCode.BadRequest;
                if (String.IsNullOrWhiteSpace(error.Code))
                {
                    error.Code = Errors.record_has_existed_400;
                }
            }
            else if (context.Exception is UnauthorizedException)
            {
                error.Status = (int)HttpStatusCode.Unauthorized;
                if (String.IsNullOrWhiteSpace(error.Code))
                {
                    error.Code = Errors.unauthorized_401;
                }
            }
            else if (context.Exception is ForbiddenException)
            {
                error.Status = (int)HttpStatusCode.Forbidden;
                if (String.IsNullOrWhiteSpace(error.Code))
                {
                    error.Code = Errors.forbidden_403;
                }
            }
            else if (context.Exception is NotFoundException)
            {
                error.Status = (int)HttpStatusCode.NotFound;
                {
                    error.Code = Errors.not_found_404;
                }
            }
            else if (context.Exception is MobileException)
            {
                error.Status = (int)HttpStatusCode.InternalServerError;
                
                /*
                 * This is a temporary solution.
                 * There is a kind of error, says EMSE Script error, which does not impact the process.
                 * The JSON response should indicate it via the code field.
                //*/
                var exception = context.Exception as MobileException;
                if (exception != null)
                {
                    error.Code = exception.ErrorCode;
                }

                if (String.IsNullOrWhiteSpace(error.Code))
                {
                    error.Code = Errors.internal_server_error_500;
                }
            }
            else
            {
                error.Status = (int)HttpStatusCode.InternalServerError;
                error.Code = Errors.internal_server_error_500;
            }

            context.Response = HttpResponseHelper.BuildErrorResponse(error);
        } 
    } 
}
