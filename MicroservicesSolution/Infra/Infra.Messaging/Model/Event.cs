using System;

namespace Infra.Messaging.Model
{
    public class Event: Message
    {

        public Event() : this(Guid.NewGuid())
        {
        }

        public Event(Guid messageId) : base(messageId)
        {
        }

        public Event(string messageType) : base(Guid.NewGuid())
        {
        }

        public Event(Guid messageId, string messageType) : base(messageId, messageType)
        {
        }
    }
    public class ShimonEvent : Event
    {

    }
}
