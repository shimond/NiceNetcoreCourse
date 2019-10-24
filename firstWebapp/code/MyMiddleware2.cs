using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace firstWebapp.code
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddleware2
    {
        private readonly RequestDelegate _next;

        public MyMiddleware2(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext httpContext,
            IServiceSingleton serviceSingleton,
            ILogger<MyMiddleware> logger)
        {
            //logger.LogWarning("Invoke from my middleware 1.");
            await _next(httpContext);
            serviceSingleton.Go(2);
            //logger.LogWarning("Invoke from my middleware 2.");
        }

        public MyMiddleware2()
        {
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddleware2Extensions
    {
        public static IApplicationBuilder UseMyMiddleware2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware2>();
        }
    }
}
