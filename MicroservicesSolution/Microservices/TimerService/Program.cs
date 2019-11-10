using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infra.Messaging.Contract;
using Infra.RabbitMQ;
using Infra.Messaging.Model.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infra.Messaging.Services;

namespace TimerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<EventBusConfiguration>(hostContext.Configuration.GetSection("EventBus"));
                    services.AddTransient<IMessageSerializer, JsonMessageSerializer>();
                    services.AddSingleton<IEventBus, RabbitEventBus>();
                    services.AddHostedService<TimeWorker>();
                });
    }
}
