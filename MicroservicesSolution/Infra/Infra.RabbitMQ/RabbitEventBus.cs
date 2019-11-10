using Infra.Messaging.Contract;
using Infra.Messaging.Model;
using Infra.Messaging.Model.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.RabbitMQ
{
    public class RabbitEventBus : IEventBus
    {
        public EventBusConfiguration Config { get; }
        private IMessageSerializer messageSerializer;
        private Dictionary<string, List<Type>> handlers;
        private IServiceScopeFactory _serviceScopeFactory;

        public Task Publish(Event @event)
        {
            var factory = new ConnectionFactory()
            {
                HostName = Config.Host,
                UserName = Config.UserName,
                Password = Config.Password
            };

            using (var connection = factory.CreateConnection())
            {

                using (var model = connection.CreateModel())
                {
                    var data = this.messageSerializer.SerializeToBytes(@event);
                    model.BasicPublish(exchange: "",
                                         routingKey: @event.GetType().Name,
                                         body: data);
                }
            }
            return Task.CompletedTask;
        }

        public Task Subscribe<TEvent, THandler>()
            where TEvent : Event
            where THandler : IEventHandler
        {
            var eventName = typeof(TEvent).Name;
            if (!handlers.ContainsKey(eventName))
            {
                handlers[eventName] = new List<Type>();
            }
            handlers[eventName].Add(typeof(THandler));
            StartConsume<TEvent>();
            return Task.CompletedTask;
        }

        private void StartConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.Config.Host,
                UserName = Config.UserName,
                Password = Config.Password,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;
            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            string message = @event.RoutingKey;
            if (handlers.TryGetValue(message, out List<Type> listOfHandlers))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    foreach (var handlerType in listOfHandlers)
                    {
                        var handler = scope.ServiceProvider.GetService(handlerType) as IEventHandler;
                        var messageType = handlerType.GenericTypeArguments[0];
                        var messageBody = messageSerializer.Deserilize(@event.Body, messageType);
                        await handler.Handle(messageBody);
                    }
                }
            }
        }

        public RabbitEventBus(IOptions<EventBusConfiguration> options, IMessageSerializer messageSerializer,
            IServiceScopeFactory serviceScopeFactory
            )
        {
            this.messageSerializer = messageSerializer;
            Config = options.Value;
            handlers = new Dictionary<string, List<Type>>();
            _serviceScopeFactory = serviceScopeFactory;
        }
    }
}
