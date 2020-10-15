using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TbcTestAppDAL.DAL.DBEntities
{
    public partial class Persons
    {
        public Persons()
        {
            InverseHistoryRow = new HashSet<Persons>();
            InverseParent = new HashSet<Persons>();
            PersonPhoneNumbers = new HashSet<PersonPhoneNumbers>();
            PersonFiles = new HashSet<PersonFiles>();
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? RelationTypeId { get; set; }

        [Required,MinLength(2),MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
        public int? GenderId { get; set; }

        [StringLength(11)]
        public String PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int? CityId { get; set; }
        public int? HistoryRowId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Cities City { get; set; }
        public virtual PersonGender Gender { get; set; }
        public virtual Persons HistoryRow { get; set; }
        public virtual Persons Parent { get; set; }
        public virtual PersonRelationTypes RelationType { get; set; }
        public virtual ICollection<Persons> InverseHistoryRow { get; set; }
        public virtual ICollection<Persons> InverseParent { get; set; }
        public virtual ICollection<PersonPhoneNumbers> PersonPhoneNumbers { get; set; }
        public virtual ICollection<PersonFiles> PersonFiles { get; set; }
    }
}
