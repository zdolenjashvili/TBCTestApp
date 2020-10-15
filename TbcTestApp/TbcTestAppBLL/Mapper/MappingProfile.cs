using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Models.FilterModels;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Models;
using TbcTestAppDAL.Models.FilterModels;

namespace TbcTestAppBLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonGenderModel, PersonGender>();
            CreateMap<CityModel, Cities>();
            CreateMap<Cities, CityModel>();
            //.ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<PersonGender, PersonGenderModel>();
            CreateMap<PersonRelationTypes, PersonRelationTypeModel>();
            CreateMap<PhoneTypes, PhoneTypeModel>();
            CreateMap<Persons, PersonModel>();
            CreateMap<PersonPhoneNumbers, PersonPhoneNumberModel>();
            CreateMap<PersonPhoneNumberModel, PersonPhoneNumbers>();
            CreateMap<PhoneTypes, PhoneTypeModel>();
            CreateMap<PhoneTypeModel, PhoneTypes>();
            CreateMap<PersonReportModel, DALPersonReportModel>();
            CreateMap<DALPersonReportModel, PersonReportModel>();
            CreateMap<PersonRelationReportModel, DALPersonRelationReportModel>();
            CreateMap<DALPersonRelationReportModel, PersonRelationReportModel>();
            CreateMap<PersonFilterModel, DALPersonFilterModel>();

            //CreateMap<UserModel, ApplicationUserEntity>();
            //CreateMap<ApplicationUserEntity, UserModel>();
            //CreateMap<PersonModel, PersonEntity>().ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(m => m.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //    .ForMember(m => m.LastName, opt => opt.MapFrom(src => src.LastName))
            //    .ForMember(m => m.PersonalId, opt => opt.MapFrom(src => src.PersonalId))
            //    .ForMember(m => m.PersonalId, opt => opt.MapFrom(src => src.PersonalId));
        }
    }
}
