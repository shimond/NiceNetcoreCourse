using Infra.Messaging.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Messaging.Contract
{
    public interface IMessageSerializer
    {
        byte[] SerializeToBytes(Message message);
        T Deserilize<T>(byte[] data) where T : Message;
        object Deserilize(byte[] data, Type t);

    }
}
