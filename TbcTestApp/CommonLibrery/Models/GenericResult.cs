using System;
using System.Collections.Generic;
using System.Text;

namespace TbcTestAppCore.Models
{
    public class GenericResult<T> : Result
    {
        public GenericResult(Result result) :
            base(result.IsSuccess, result.Code, result.Message)
        {

        }

        public GenericResult(Result result, T value) :
            base(result.IsSuccess, result.Code, result.Message)
        {
            Result = value;
        }

        public GenericResult(bool isSuccess, int code, string message, T result) :
            base(isSuccess, code, message)
        {
            Result = result;
        }

        public T Result { get; private set; }

        static public GenericResult<T> SuccessInstance(T result)
        {
            return new GenericResult<T>(true, SUCCESS_CODE, SUCCESS_MESSAGE, result);
        }
    }
}
