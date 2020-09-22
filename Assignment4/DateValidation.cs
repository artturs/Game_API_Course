using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Assignment4
{
    public class DateValidation : ValidationAttribute
    {
        public string GetErrorMessage() =>
        $"Incorrect date. Date should be current.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var givenDate = (DateTime)value;

            if (givenDate < DateTime.Today)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}
