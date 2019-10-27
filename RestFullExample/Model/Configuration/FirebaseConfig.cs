using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Model.Configuration
{

    public class FirebaseConfig
    {
        public string apiKey { get; set; }
        public string authDomain { get; set; }
        public string databaseURL { get; set; }
        public int projectId { get; set; }
        public string storageBucket { get; set; }
        public string messagingSenderId { get; set; }
        public int appId { get; set; }
        public string measurementId { get; set; }
    }

}
