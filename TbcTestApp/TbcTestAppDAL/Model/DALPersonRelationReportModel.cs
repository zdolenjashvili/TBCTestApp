using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TbcTestAppDAL.Models
{
    public class DALPersonRelationReportModel
    {
        public int? ParentId { get; set; }
        public int Count { get; set; }
        public int? RelationTypeId { get; set; }
        public string RelationName { get; set; }
    }
}
