using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TbcTestAppCore.Extension
{
    public static class Extensions
    {


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source, bool checkValues = false)
        {
            if (source == null)
                return true;

            bool result;

            if (checkValues)
            {
                result = source.All(p => p == null);
            }
            else
            {
                result = !source.Any();
            }

            return result;
        }


        public static bool IsNullOrEmpty<T>(this IList<T> source, bool checkValues = false)
        {
            if (source == null || source.Count == 0)
                return true;


            var result = false;

            if (checkValues)
            {
                result = source.All(p => p == null);
            }

            return result;

        }
    }
}
