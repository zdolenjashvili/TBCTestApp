using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppBLL.Models;
using TbcTestAppCore.Models;

namespace TbcTestAppBLL.BLL.Interface
{
    public interface ICommonBLL
    {
        Task<GenericResult<IEnumerable<PersonGenderModel>>> GetGender();
        Task<GenericResult<IEnumerable<CityModel>>> GetCities();
        Task<GenericResult<IEnumerable<PersonRelationTypeModel>>> GetPersonRelationTypes();
        Task<GenericResult<IEnumerable<PhoneTypeModel>>> GetPhoneTypes();
    }
}
