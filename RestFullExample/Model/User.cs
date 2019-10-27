using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Department { get; set; }
        public string Role { get; set; }
        public DateTime LastConnectionTime { get; set; }
    }
}
