using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TbcTestAppBLL.Enums;
using TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes;

namespace TbcTestAppBLL.Models
{
    public class PersonPhoneNumberModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "ტელეფონის ნომრის მითითება აუცილებელია")]
        [StringLength(50, ErrorMessage = "ტელეფონის ნომერი უნდა შეიცავდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოს", MinimumLength = 4)]
        //[PhoneAndPersonalNumberValidation]
        public string PhoneNumber { get; set; }

        [PhoneTypeValidation(typeof(PhoneTypeEnum),ErrorMessage = "მიუთითეთ ტელეფონის ტიპი")]
        public int PhoneTypeId { get; set; }
        public PhoneTypeModel PhoneType { get; set; }

    }
}
