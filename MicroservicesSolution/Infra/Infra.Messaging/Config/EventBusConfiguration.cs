using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Messaging.Model.Configuration
{
    public class EventBusConfiguration
    {
        public string Host{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
    }
}
