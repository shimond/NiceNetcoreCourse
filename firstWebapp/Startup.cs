using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using firstWebapp.code;
using Microsoft.OpenApi.Models;

namespace firstWebapp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(p => p
            .AddConsole()
            .AddEventLog()
            .AddDebug());

            services.AddControllers();
            services.AddHealthChecks();
            services.AddScoped<IDateData, DateData>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IServiceSingleton, ServiceSingleton>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "My API", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseTImeInvokedMiddleware();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
            app.UseDeveloperExceptionPage();
            app.UseMyMiddleware();


            app.Run(async context =>
            {
                await context.Response.WriteAsync("RUN");
            });
        }
    }
}
