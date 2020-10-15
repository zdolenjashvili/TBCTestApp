using System;
using System.Collections.Generic;

namespace TbcTestAppBLL.Models
{
    public class PersonRelationTypeModel
    {

        public int Id { get; set; }
        public string RelationTypeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
