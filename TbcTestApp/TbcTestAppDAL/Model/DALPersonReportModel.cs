using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TbcTestAppDAL.Models
{
    public class DALPersonReportModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public List<DALPersonRelationReportModel> personRelationReportModels{ get; set; }
}
}
