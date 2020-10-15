using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TbcTestAppBLL.Enums;
using TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes;

namespace TbcTestAppBLL.Models
{
    public class PersonUpdateModel
    {

        public int Id { get; set; }
       
        [StringLength(50,ErrorMessage = "სახელი უნდა შეიცავდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოს",MinimumLength = 2)]
        [FirsAndLastNameValidation]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "გვარი უნდა შეიცავდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოს", MinimumLength = 2)]
        [FirsAndLastNameValidation]
        public string LastName { get; set; }

        [GenderTypeValidation(typeof(GenderEnum))]
        public int? GenderId { get; set; }

        public PersonGenderModel Gender { get; set; }

        [StringLength(11, ErrorMessage = "პირადი ნომერი უნდა შეიცავდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოს", MinimumLength = 11)]
        [PhoneAndPersonalNumberValidation]
        public string PersonalNumber { get; set; }

        [BirthDateValidation(18)]
        public DateTime? BirthDate { get; set; }

        public int? CityId { get; set; }
        public CityModel City { get; set; }
        
        public List<PersonPhoneNumberModel> PersonPhoneNumbers { get; set; }

    }
}
