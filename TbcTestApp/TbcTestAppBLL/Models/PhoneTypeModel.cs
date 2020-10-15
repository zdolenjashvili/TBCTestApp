using System;
using System.Collections.Generic;

namespace TbcTestAppBLL.Models
{ 
    public class PhoneTypeModel
    {

        public int Id { get; set; }
        public string PhoneType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
