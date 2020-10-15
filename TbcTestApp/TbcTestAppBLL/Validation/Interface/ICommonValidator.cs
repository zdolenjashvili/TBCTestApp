using System;
using System.Collections.Generic;
using System.Text;
using TbcTestAppCore.Models;

namespace TbcTestAppBLL.Validators.Interfaces
{
    public interface ICommonValidator
    {
        Result ValidateObjectIsEmpty(object objectParameter);
        Result ValidateStringOnGEOSymbols(string stringParameter);
        Result ValidateStringOnLatinSymbols(string stringParameter);
        Result ValidateStringOnNumbers(string stringParameter);
    }
}
