using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TbcTestAppBLL.Enums;
using TbcTestAppBLL.Validation.ValidationAttributes.PersonModelValidationAttributes;

namespace TbcTestAppBLL.Models
{
    public class PersonRelationReportModel
    {
        public int? ParentId { get; set; }
        public int Count { get; set; }
        public int? RelationTypeId { get; set; }
        public string RelationName { get; set; }
    }
}
