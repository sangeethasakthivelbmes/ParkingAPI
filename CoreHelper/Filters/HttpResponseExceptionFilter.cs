using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Parking.WebAPI.CoreHelper.Exceptions;
using Parking.WebAPI.CoreHelper.Models;

namespace Parking.WebAPI.CoreHelper.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;
        private readonly IWebHostEnvironment _env;

        public HttpResponseExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode
                };
            }
            else if (context.Exception is Exception ex)
            {
                Log.Error("{@Message} - {@StackTrace}", ex.Message, ex.StackTrace);
                var controllerName = context.RouteData.Values["controller"].ToString();
                var error = new CustomError
                {
                    ErrorCode = $"WIS_{controllerName}_000",
                    Message = context.Exception.Message,
                    LanguageCode = "en-GB"
                };
                context.Result = new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            context.ExceptionHandled = true;
        }
    }
}

