using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service
{
    /// <summary>
    /// Custom exception handling to organize error messages in a certain way
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            HttpStatusCode code;

            // By convention InvalidOperationException is thrown when service layer
            // invalidates the data which wasn't invalidated at upper layers
            if (e is InvalidOperationException)
                code = HttpStatusCode.BadRequest;
            // In any other unhandled situation throw 500 
            else
                code = HttpStatusCode.InternalServerError;

            // The convention is there is an "errors" string array in response data
            var result = new
            {
                errors = new[] { e.Message }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
