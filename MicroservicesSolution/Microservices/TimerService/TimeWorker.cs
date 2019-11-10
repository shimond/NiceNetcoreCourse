using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infra.Messaging.Contract;
using Infra.Messaging.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TimerService
{
    public class TimeWorker : BackgroundService
    {
        private readonly ILogger<TimeWorker> _logger;
        private readonly IEventBus _eventBus;
        private DateTime _lastTime; 
        public TimeWorker(ILogger<TimeWorker> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
            _lastTime = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventBus.Publish(new DatePassedEvent());

            while (!stoppingToken.IsCancellationRequested)
            {
                if(DateTime.Now.Subtract(_lastTime).Days == 1)
                {
                    _lastTime = DateTime.Now;
                    await _eventBus.Publish(new DatePassedEvent());
                }
                await Task.Delay(600000, stoppingToken);
            }
        }
    }
}
