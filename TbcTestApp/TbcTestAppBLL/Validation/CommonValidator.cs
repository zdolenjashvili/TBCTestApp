using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using TbcTestAppBLL.Validation.ErrorMassages;
using TbcTestAppBLL.Validators.Interfaces;
using TbcTestAppCore.Models;

namespace TbcTestAppBLL.Validators
{
    public class CommonValidator : ICommonValidator
    {
        private const string GeoSymbols = "აბგდევზთიკლმნოპჟრსტუფქღყშჩცძწჭხჯჰ";
        private const string Numbers = "0123456789";
        private const string LatinSymbolsLower = "abcdefghijklmnopqrstuvwxyz";
        //private const string LatinSymbolsUpper = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        //private const string LatinSymbols = "abcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// ობიექტის მონაცემების შემცველობაზე შემოწმება
        /// </summary>
        /// <param name="objectParameter"></param>
        /// <returns></returns>
        public Result ValidateObjectIsEmpty(object objectParameter)
        {

            if (objectParameter == null)
            {
                return new Result(false, 1, ValidationMassages.ObjectIsEmpty);
            }

            return Result.SuccessInstance();

        }


        /// <summary>
        /// ტექსტური ველის ვალიდაცია ქართულ სიმბოლოებზე
        /// </summary>
        /// <param name="stringParameter"></param>
        /// <returns></returns>
        public Result ValidateStringOnGEOSymbols(string stringParameter)
        {
            var result = ValidateObjectIsEmpty(stringParameter);

            if (!result.IsSuccess)
            {
                return result;
            }

            if (!stringParameter.All(x => GeoSymbols.Contains(x)))
            {
                return new Result(false, 3, ValidationMassages.OnlyGeoSymbols);
            }

            return Result.SuccessInstance();
        }


        /// <summary>
        /// ტექსტური ველის ვალიდაცია ლათინურ სიმბოლოებზე
        /// </summary>
        /// <param name="stringParameter"></param>
        /// <returns></returns>
        public Result ValidateStringOnLatinSymbols(string stringParameter)
        {
            var result = ValidateObjectIsEmpty(stringParameter);

            if (!result.IsSuccess)
            {
                return result;
            }

            if (!stringParameter.All(x => LatinSymbolsLower.Contains(x,StringComparison.OrdinalIgnoreCase)))
            {
                return new Result(false, 4, ValidationMassages.OnlyEngSymbols);
            }

            return Result.SuccessInstance();

        }


        /// <summary>
        /// ტექსტური ველის ვალიდაცია რიცხვით სიმბოლოებზე
        /// </summary>
        /// <param name="stringParameter"></param>
        /// <returns></returns>
        public Result ValidateStringOnNumbers(string stringParameter)
        {
            var result = ValidateObjectIsEmpty(stringParameter);

            if (!result.IsSuccess)
            {
                return result;
            }

            if (!stringParameter.All(x => Numbers.Contains(x)))
            {
                return new Result(false, 4, ValidationMassages.OnlyNumericalSymbols);
            }

            return Result.SuccessInstance();
        }

    }
}
