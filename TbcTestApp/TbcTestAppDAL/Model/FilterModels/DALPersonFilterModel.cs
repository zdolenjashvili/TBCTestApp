using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TbcTestAppDAL.Models.FilterModels
{
    public class DALPersonFilterModel
    {

        public int? Id { get; set; }
        public int? ParentId { get; set; }
        public int? RelationTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }

    }
}
