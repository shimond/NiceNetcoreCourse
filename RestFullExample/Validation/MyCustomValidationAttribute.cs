using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullExample.Validation
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class MyCustomValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string name = value.ToString();
            if (name.StartsWith("aa"))
            {
                return true;
            }
            //this.ErrorMessage = "";
            return false;
        }
    }
}
