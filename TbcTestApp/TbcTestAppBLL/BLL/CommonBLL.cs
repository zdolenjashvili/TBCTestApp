using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppBLL.BLL.Interface;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Validators.Interfaces;
using TbcTestAppCore.Extension;
using TbcTestAppCore.Models;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppBLL.BLL
{
    public class CommonBLL : ICommonBLL
    {
        private readonly IGenderRepository genderRepository;
        private readonly ICitiesRepository CitiesRepository;
        private readonly IPersonRelationTypeRepository personRelationTypeRepository;
        private readonly IPhoneTypeRepository phoneTypeRepository;
        private readonly IMapper mapper;
        public CommonBLL(IGenderRepository genderRepository,
                         IMapper mapper,
                         ICitiesRepository CitiesRepository,
                         IPersonRelationTypeRepository personRelationTypeRepository,
                         IPhoneTypeRepository phoneTypeRepository)
        {
            this.genderRepository = genderRepository;
            this.mapper = mapper;
            this.CitiesRepository = CitiesRepository;
            this.personRelationTypeRepository = personRelationTypeRepository;
            this.phoneTypeRepository = phoneTypeRepository;
        }

        /// <summary>
        /// პიროვნების სქესის ცნობარი
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<GenericResult<IEnumerable<PersonGenderModel>>> GetGender()
        {

            var personGenders = await genderRepository.GetAllAsync();

            if (personGenders.IsNullOrEmpty())
            {
                return new GenericResult<IEnumerable<PersonGenderModel>>(Result.SuccessInstance(), null);
            }

            var personGenderModel = mapper.Map<IEnumerable<PersonGender>, IEnumerable<PersonGenderModel>>(personGenders);

            return new GenericResult<IEnumerable<PersonGenderModel>>(Result.SuccessInstance(), personGenderModel);
        }



        /// <summary>
        /// ქალაქების ცნობარი
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<GenericResult<IEnumerable<CityModel>>> GetCities()
        {

            var cities = await CitiesRepository.GetAllAsync();

            if (cities.IsNullOrEmpty())
            {
                return new GenericResult<IEnumerable<CityModel>>(Result.SuccessInstance(), null);
            }

            var citiesModel = mapper.Map<IEnumerable<Cities>, IEnumerable<CityModel>>(cities);

            return new GenericResult<IEnumerable<CityModel>>(Result.SuccessInstance(), citiesModel);
        }


        /// <summary>
        /// ფიზიკური პირების კავშირის ცნობარი
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        public async Task<GenericResult<IEnumerable<PersonRelationTypeModel>>> GetPersonRelationTypes()
        {

            var personRelationTypes = await personRelationTypeRepository.GetAllAsync();

            if (personRelationTypes.IsNullOrEmpty())
            {
                return new GenericResult<IEnumerable<PersonRelationTypeModel>>(Result.SuccessInstance(), null);
            }

            var personRelationTypeModel = mapper.Map<IEnumerable<PersonRelationTypes>, IEnumerable<PersonRelationTypeModel>>(personRelationTypes);

            return new GenericResult<IEnumerable<PersonRelationTypeModel>>(Result.SuccessInstance(), personRelationTypeModel);
        }


        /// <summary>
        /// ტელეფონის ტიპის ცნობარი
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        public async Task<GenericResult<IEnumerable<PhoneTypeModel>>> GetPhoneTypes()
        {

            var phoneTypes = await phoneTypeRepository.GetAllAsync();

            if (phoneTypes.IsNullOrEmpty())
            {
                return new GenericResult<IEnumerable<PhoneTypeModel>>(Result.SuccessInstance(), null);
            }

            var phoneTypeModel = mapper.Map<IEnumerable<PhoneTypes>, IEnumerable<PhoneTypeModel>>(phoneTypes);

            return new GenericResult<IEnumerable<PhoneTypeModel>>(Result.SuccessInstance(), phoneTypeModel);
        }
    }
}
