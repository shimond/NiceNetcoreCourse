using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace firstWebapp.code
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TImeInvokedMiddleware
    {
        private readonly RequestDelegate _next;

        public TImeInvokedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IDateData dateData)
        {
            httpContext.Response.Headers.Add("custom", "value");
            dateData.InvokedTime = DateTime.Now;
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TImeInvokedMiddlewareExtensions
    {
        public static IApplicationBuilder UseTImeInvokedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TImeInvokedMiddleware>();
        }
    }
}
