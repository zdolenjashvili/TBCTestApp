using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Validation.ErrorMassages;
using TbcTestAppBLL.Validators;

namespace TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class FirsAndLastNameValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var commonValidator = new CommonValidator();
                var geoSymbolValidateresult = commonValidator.ValidateStringOnGEOSymbols(value.ToString());
                var latinSymbolValidateresult = commonValidator.ValidateStringOnLatinSymbols(value.ToString());
                var member = validationContext.MemberName == "FirstName" ? "სახელი " : "გვარი ";


                return (geoSymbolValidateresult.IsSuccess || latinSymbolValidateresult.IsSuccess)
                    ? ValidationResult.Success
                    : new ValidationResult($"{member}{ValidationMassages.OnlyGeoOrOnlyEngSymbols}");
            }

            return ValidationResult.Success;
        }
    }

}


