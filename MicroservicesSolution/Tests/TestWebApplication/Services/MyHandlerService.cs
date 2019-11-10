using Infra.Messaging.Contract;
using Infra.Messaging.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApplication.Services
{
    public class MyHandlerService : IEventHandler<DatePassedEvent>
    {
        private ILogger<MyHandlerService> _logger;

        public Task Handle(DatePassedEvent @event)
        {
            _logger.LogInformation("Date Passed from eventbus!");
            return Task.CompletedTask;
        }

        public Task Handle(object @event)
        {
            return this.Handle(@event as DatePassedEvent);
        }

        public MyHandlerService(ILogger<MyHandlerService> logger)
        {
            _logger = logger;
        }


        

    }
}
