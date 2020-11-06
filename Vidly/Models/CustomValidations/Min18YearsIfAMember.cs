using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Models.CustomValidations
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var _customer = (Customer)validationContext.ObjectInstance;

            if (_customer.MembershipTypeId == MembershipType.Unknown)
            {
                return ValidationResult.Success;
            }

            if (_customer.MembershipTypeId == MembershipType.PayAsYouGo && 
                _customer.BirthDate == null)
            {
                return new ValidationResult("Please enter Birth Date.");
            }

            var _age = DateTime.Today.Year - _customer.BirthDate.Value.Year;
            return (_age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");

        }
    }
}
