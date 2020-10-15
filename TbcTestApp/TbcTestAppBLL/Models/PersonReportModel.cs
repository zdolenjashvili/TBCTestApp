using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TbcTestAppBLL.Enums;
using TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes;

namespace TbcTestAppBLL.Models
{
    public class PersonReportModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public List<PersonRelationReportModel> personRelationReportModels{ get; set; }
}
}
