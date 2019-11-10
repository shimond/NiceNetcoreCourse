using System;

namespace Infra.Messaging.Model
{
    public class Command : Message
    {

        public Command() : this(Guid.NewGuid())
        {
        }

        public Command(Guid messageId) : base(messageId)
        {
        }

        public Command(string messageType) : base(Guid.NewGuid())
        {
        }
        public Command(Guid messageId, string messageType) : base(messageId, messageType)
        {
        }
    }
}
