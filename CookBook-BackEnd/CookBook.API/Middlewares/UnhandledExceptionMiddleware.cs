using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CookBook.Infrastructure.Exceptions;
using CookBook.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CookBook.API.Middlewares
{
   public class UnhandledExceptionMiddleware : IMiddleware
    {
        public UnhandledExceptionMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await BuildExceptionResponse(context, e);
            }
        }

        private async Task BuildExceptionResponse(HttpContext context, Exception exception)
        {
            Exception currentException = exception.InnerException is IAppException
               ? exception.InnerException
               : exception;

            if (currentException is IAppException e)
            {
                await BuildExceptionResponse(context, BuildErrorResponse(context.Request, currentException, e.ErrorCode), e.HttpStatusCode);
            }
            else
            {
                await BuildExceptionResponse(context, BuildErrorResponse(context.Request, currentException), HttpStatusCode.InternalServerError);
            }
        }

        private static async Task BuildExceptionResponse(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(message);
        }

        private static string BuildFullStackTrace(Exception e)
        {
            var builder = new StringBuilder();
            builder.AppendLine(e.Message);
            builder.AppendLine(e.StackTrace);

            Exception innEx = e.InnerException;
            while (innEx != null)
            {
                builder.AppendLine();
                builder.Append("--- Inner Exception: ");
                builder.AppendLine(innEx.Message);
                builder.AppendLine(innEx.StackTrace);
                innEx = innEx.InnerException;
            }

            return builder.ToString();
        }

        private string BuildErrorResponse(HttpRequest request, Exception e, string errorCode = ErrorCodes.UnknownError)
        {
             var response = new ErrorResponse<string>(errorCode, e.Message);

            return JsonConvert.SerializeObject(response);
        }
    }
}