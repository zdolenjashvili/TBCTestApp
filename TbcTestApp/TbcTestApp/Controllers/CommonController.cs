using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TbcTestAppBLL.BLL.Interface;
using TbcTestAppBLL.Models;
using TbcTestAppCore.Models;

namespace TbcTestAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonBLL commonBLL;

        public CommonController(ICommonBLL commonBLL)
        {
            this.commonBLL = commonBLL;
        }



        /// <summary>
        /// პიროვნების სქესის ცნობარი
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet("GetPersonGender")]
        public async Task<GenericResult<IEnumerable<PersonGenderModel>>> GetPersonGender()
        {
            return await commonBLL.GetGender();
        }


        /// <summary>
        /// ქალაქების ცნობარი
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet("GetCities")]
        public async Task<GenericResult<IEnumerable<CityModel>>> GetCities()
        {
            return await commonBLL.GetCities();
        }


        /// <summary>
        /// ფიზიკური პირების კავშირის ცნობარი
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet("GetPersonRelation")]
        public async Task<GenericResult<IEnumerable<PersonRelationTypeModel>>> GetPersonRelation()
        {
            return await commonBLL.GetPersonRelationTypes();
        }


        /// <summary>
        /// ტელეფონის ტიპის ცნობარი
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet("GetPhoneTypes")]
        public async Task<GenericResult<IEnumerable<PhoneTypeModel>>> GetPhoneTypes()
        {
            return await commonBLL.GetPhoneTypes();
        }
    }
}
