using System;
using System.Collections.Generic;
using System.Text;

namespace TbcTestAppBLL.Validation.ErrorMassages
{
    public static class ValidationMassages
    {
        #region ValidationAttributes
        public const string BirthAgeNotValid = "პიროვნების ასაკი არ უნდა იყოს {0} წელზე ნაკლები";

        public const string BirthDateNotValid = "დაბადების თარიღის ფორმატი არასწორია";

        public const string OnlyGeoOrOnlyEngSymbols = "უნდა შეიცავდეს მხოლოდ ქართულ ან მხოლოდ ინგლისურ სიმბოლოებს";

        public const string GenderIdIsNotValidNotValid = "მიუთითებული სქესის იდენტიფიკატორი არასწორია";

        public const string ReletionTypeIdNotValid = "მიუთითებული კავშირის ტიპის იდენტიფიკატორი არასწორია";

        public const string PhoneTypeIdNotValid = "ტელეფონის ტიპის იდენტიფიკატორი არასწორია";
        #endregion ValidationAttributes




        #region CommonValidations
        public const string ObjectIsEmpty = "გადმოცემული ობიექტი ცარიელია";

        public const string OnlyGeoSymbols = "უნდა შეიცავდეს მხოლოდ ქართულ სიმბოლოებს";

        public const string OnlyEngSymbols = "უნდა შეიცავდეს მხოლოდ ინგლისურ სიმბოლოებს";

        public const string OnlyNumericalSymbols = "უნდა შეიცავდეს მხოლოდ რიცხვით სიმბოლოებს";

        #endregion CommonValidations



        #region PersonBLL
        public const string FileNotFound = "ფაილი არ მოიძებნა";

        public const string PersonIdMustProvided = "პიროვნების ჩანაწერის იდენტიფიქატორი აუცილებელია";

        public const string UnHandledExeption = "მოხდა გაუთვალისწინებელი შეცდომა დეტალურად";

        public const string WrongFileFormat = "ფაილის ფორმატი არასწორია, გთხოვთ მიუთითოთ სწორი ფორმატი";

        public const string PersonNotFound = "პირვონება არ მოიძებნა";

        public const string FileMustProvided = "ფაილის მიმაგრება აუცილებელია";

        public const string CityNotFound = "ქალაქი მოცემული იდენტიფიქატორით არ მოიძებნა";

        public const string ReletionTypeMustProvided = "კავშირის ტიპის მითითება აუცილებელია";

        #endregion PersonBLL



    }
}
