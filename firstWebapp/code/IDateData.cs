using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstWebapp.code
{
    public interface  IDateData
    {
        public DateTime InvokedTime { get; set; }
    }
    public class DateData : IDateData
    {
        public DateTime InvokedTime { get ; set ; }
    }


    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime InvokedDate { get; internal set; }
    }
}
