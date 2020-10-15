using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Validators;

namespace TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PhoneAndPersonalNumberValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                var commonValidator = new CommonValidator();
                var result = commonValidator.ValidateStringOnNumbers(value.ToString());
                var member = validationContext.MemberName == "PersonalNumber" ? "პირადი ნომერი " : "ტელეფონის ნომერი ";

                return result.IsSuccess
                    ? ValidationResult.Success
                    : new ValidationResult($"{member}{result.Message}");
            }

            return ValidationResult.Success;
        }
    }

}


