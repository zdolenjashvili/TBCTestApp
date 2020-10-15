using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcTestAppDAL.DAL.DBEntities
{
    public partial class Cities
    {
        public Cities()
        {
            Persons = new HashSet<Persons>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CityName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<Persons> Persons { get; set; }
    }
}
