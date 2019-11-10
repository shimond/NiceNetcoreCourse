using Infra.Messaging.Contract;
using Infra.Messaging.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Messaging.Services
{
    public class JsonMessageSerializer : IMessageSerializer
    {
        public T Deserilize<T>(byte[] data) where T : Message
        {
            return null;
        }

        public object Deserilize(byte[] data, Type t)
        {
            string value = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject(value, t);
        }

        public byte[] SerializeToBytes(Message message)
        {
            var data = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(data);
        }


    }
}
