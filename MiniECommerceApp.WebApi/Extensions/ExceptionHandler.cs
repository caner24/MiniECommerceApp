﻿using Microsoft.AspNetCore.Diagnostics;
using MiniECommerceApp.Entity.Exceptions;

namespace MiniECommerceApp.WebApi.Extensions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not null)
            {
                var statusCodes = exception switch
                {
                    UserNotFoundExcepiton => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                var jsonMessage = new
                {
                    StatusCode = statusCodes,
                    Message = exception.Message,
                };
                await httpContext.Response.WriteAsJsonAsync(jsonMessage);
                return true;
            }
            return false;
        }
    }
}
