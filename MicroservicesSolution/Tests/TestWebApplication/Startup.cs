using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infra.Messaging.Contract;
using Infra.RabbitMQ;
using Infra.Messaging.Model.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestWebApplication.Configuration;
using Infra.Messaging.Services;
using Infra.Messaging.Model;
using TestWebApplication.Services;

namespace TestWebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSeq(Configuration.GetSection("Seq")));
            services.Configure<EventBusConfiguration>(Configuration.GetSection("EventBus"));
            services.AddTransient<IMessageSerializer, JsonMessageSerializer>();
            services.AddTransient<IEventHandler<DatePassedEvent>, MyHandlerService>();
            services.AddSingleton<IEventBus, RabbitEventBus>();
            services.Configure<MicroservicesConfig>(Configuration.GetSection("urls"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            eventBus.Subscribe<DatePassedEvent, IEventHandler<DatePassedEvent>>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
