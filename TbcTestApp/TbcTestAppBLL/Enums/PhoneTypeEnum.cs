using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TbcTestAppBLL.Enums
{
    public enum PhoneTypeEnum
    {
        [Description("მობილური")]
        Mobile = 1,

        [Description("ოფისის")]
        Office = 2,

        [Description("სახლის")]
        Home = 3
    }
}