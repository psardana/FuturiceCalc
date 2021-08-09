using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FuturiceCalc.Infrastructure.Models;
using FuturiceCalc.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace FuturiceCalc.Infrastructure.Middlewares
{
    /// <summary>
    /// handles exception generated in the app
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext, ILoggingFacade logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                httpContext.Response.StatusCode = (int)GetErrorCode(ex);
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse
                {
                    Error = true,
                    Message = ex.Message
                }));
            }
        }

        private static HttpStatusCode GetErrorCode(Exception e)
        {
            switch (e)
            {
                case ValidationException _:
                    return HttpStatusCode.BadRequest;
                case FormatException _:
                    return HttpStatusCode.BadRequest;
                case NotImplementedException _:
                    return HttpStatusCode.NotImplemented;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
