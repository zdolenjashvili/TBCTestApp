using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcTestAppDAL.DAL.DBEntities
{
    public partial class PhoneTypes
    {
        public PhoneTypes()
        {
            PersonPhoneNumbers = new HashSet<PersonPhoneNumbers>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string PhoneType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<PersonPhoneNumbers> PersonPhoneNumbers { get; set; }
    }
}
