using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TbcTestAppBLL.Enums
{
    public enum PersonRelationTypeEnum
    {
        [Description("კოლეგა")]
        Colleague = 1,

        [Description("ნაცნობი")]
        Familiar = 2,

        [Description("ნათესავი")]
        Relative = 3,

        [Description("სხვა")]
        Other = 4,
    }
}
