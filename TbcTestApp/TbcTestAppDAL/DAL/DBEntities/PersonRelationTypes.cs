using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcTestAppDAL.DAL.DBEntities
{
    public partial class PersonRelationTypes
    {
        public PersonRelationTypes()
        {
            Persons = new HashSet<Persons>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RelationTypeName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<Persons> Persons { get; set; }
    }
}
