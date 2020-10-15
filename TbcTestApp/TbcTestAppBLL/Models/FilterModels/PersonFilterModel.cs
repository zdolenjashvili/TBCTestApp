using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TbcTestAppBLL.Enums;
using TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes;

namespace TbcTestAppBLL.Models.FilterModels
{
    public class PersonFilterModel
    {

        public int? Id { get; set; }
        public int? ParentId { get; set; }

        [RelationTypeValidation(typeof(PersonRelationTypeEnum))]
        public int? RelationTypeId { get; set; }
        
        [FirsAndLastNameValidation]
        public string FirstName { get; set; }

        [FirsAndLastNameValidation]
        public string LastName { get; set; }

        [GenderTypeValidation(typeof(GenderEnum))]
        public int? GenderId { get; set; }

        [PhoneAndPersonalNumberValidation]
        public string PersonalNumber { get; set; }

        [BirthDateValidation(18)]
        public DateTime? BirthDate { get; set; }

        public int? CityId { get; set; }

    }
}
