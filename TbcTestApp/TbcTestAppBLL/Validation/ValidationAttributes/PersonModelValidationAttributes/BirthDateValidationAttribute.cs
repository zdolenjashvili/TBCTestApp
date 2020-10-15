using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;
using System.Text;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Validation.ErrorMassages;
using TbcTestAppBLL.Validators;

namespace TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class BirthDateValidationAttribute : ValidationAttribute
    {
        private int age { get; set; }

        public BirthDateValidationAttribute(int age)
        {
            this.age = age;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

                if (!DateTime.TryParse(value.ToString(), out var bithDate))
                {
                    return new ValidationResult(ValidationMassages.BirthDateNotValid);
                }

                var currentDate = DateTime.Now.Date;
                if (currentDate < bithDate.AddYears(age))
                {
                    return new ValidationResult(string.Format(ValidationMassages.BirthAgeNotValid, age.ToString()));
                }
            }

            return ValidationResult.Success;
        }
    }
  
}


