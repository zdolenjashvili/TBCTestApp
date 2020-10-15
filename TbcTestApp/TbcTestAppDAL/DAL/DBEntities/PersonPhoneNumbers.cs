using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcTestAppDAL.DAL.DBEntities
{
    public partial class PersonPhoneNumbers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int PersonId { get; set; }
        public int PhoneTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Persons Person { get; set; }
        public virtual PhoneTypes PhoneType { get; set; }
    }
}
