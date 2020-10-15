using System;
using System.Collections.Generic;

namespace TbcTestAppBLL.Models
{
    public class FileModel
    {
        public string fileType { get; set; }
        public byte[] fileData { get; set; }
        public string fileName { get; set; }

    }
}