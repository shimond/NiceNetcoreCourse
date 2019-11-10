using Infra.Messaging.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Messaging.Contract
{
    public interface IEventBus
    {
        Task Publish(Event message);
        Task Subscribe<TEvent, THandler>() where TEvent : Event
            where THandler : IEventHandler;

    }

}
