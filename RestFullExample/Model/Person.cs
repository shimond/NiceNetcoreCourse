using RestFullExample.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Model
{
    [FullObjectValidationError]
    public class Person
    {
        public int ID { get; set; }
        [Required]
        [MyCustomValidation()]
        public string Name { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
 
}
