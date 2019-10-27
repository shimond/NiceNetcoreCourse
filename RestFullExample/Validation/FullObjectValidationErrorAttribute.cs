using RestFullExample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Validation
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]

    public class FullObjectValidationErrorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            
            var p = value as Person;
            //if(p.Birthdate.Value.Year == 2019 && p.Email.EndsWith("gmail.com"))
            //{
            //    return true;
            //}
            return true;
        }
    }
}
