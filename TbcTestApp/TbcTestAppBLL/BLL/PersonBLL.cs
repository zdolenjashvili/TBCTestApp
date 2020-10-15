using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppBLL.BLL.Interface;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Models.FilterModels;
using TbcTestAppCore.Extension;
using TbcTestAppCore.Models;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Models;
using TbcTestAppDAL.Models.FilterModels;
using TbcTestAppDAL.Repositories.Interfaces;
using TbcTestAppBLL.Tools;
using Microsoft.AspNetCore.StaticFiles;
using TbcTestAppBLL.Validation.ErrorMassages;

namespace TbcTestAppBLL.BLL
{
    public class PersonBLL : IPersonBLL
    {
        private readonly IPersonRepository personRepository;
        private readonly IPersonFilesRepository personFilesRepository;
        private readonly ICitiesRepository citiesRepository;
        private readonly IMapper mapper;

        private const string fileFormat = ".JPG, .JPEG, .PNG, .GIF";

        private const string directoryAddress = "\\PersonFiles\\";
        public PersonBLL(IPersonRepository personRepository, IPersonFilesRepository personFilesRepository, ICitiesRepository citiesRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.personFilesRepository = personFilesRepository;
            this.citiesRepository = citiesRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// პიროვნების დამტება
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        public async Task<Result> AddPerson(PersonModel personModel)
        {
            var personPhoneNumber = new List<PersonPhoneNumbers>();

            if (!personModel.PersonPhoneNumbers.IsNullOrEmpty())
            {
                personModel.PersonPhoneNumbers.ForEach(f => personPhoneNumber.Add(
                    new PersonPhoneNumbers { PhoneNumber = f.PhoneNumber, PhoneTypeId = (int)f.PhoneTypeId, DateCreated = DateTime.Now }));
            }

            if (personModel.CityId != null)
            {
                var city = await citiesRepository.GetByIdAsync(personModel.CityId.Value);

                if (city == null)
                {
                    return new Result(false, 11, ValidationMassages.CityNotFound);
                }
            }


            var personEntity = new Persons
            {
                FirstName = personModel.FirstName,
                LastName = personModel.LastName,
                PersonalNumber = personModel.PersonalNumber,
                BirthDate = personModel.BirthDate,
                CityId = personModel.CityId,
                GenderId = personModel.GenderId,
                PersonPhoneNumbers = personPhoneNumber,
                DateCreated = DateTime.Now
            };



            var result = await personRepository.InsertAsync(personEntity);

            return result;
        }


        /// <summary>
        /// პიროვნების განახლება : სახელი, გვარი, სქესი, პირადი ნომერი, დაბადების თარიღი, ქალაქი, ტელეფონის ნომრები
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        public async Task<Result> UpdatePerson(PersonUpdateModel personModel)
        {
            var currentDate = DateTime.Now;

            if (personModel.Id == 0)
            {
                return new Result(false, 12, ValidationMassages.PersonIdMustProvided);
            }

            // არსებობს თუ არა ასეთ იდენტიფიქატორით ჩანაწერი
            var personDB = await personRepository.GetParentPersonAsync(personModel.Id);

            if (personDB == null)
            {
                return new Result(false, 13, ValidationMassages.PersonNotFound);
            }


            #region ქალაქზე ჩანაწერი არსებობს თუ არა
            if (personModel.CityId != null)
            {
                var city = await citiesRepository.GetByIdAsync(personModel.CityId.Value);

                if (city == null)
                {
                    return new Result(false, 11, "ქალაქიზე ჩანაწერი მოცემული იდენტიფიქატორით არ მოიძებნა");
                }
            }

            #endregion ქალაქზე ჩანაწერი არსებობს თუ არა 

            // ისტორიის შექმნა
            Persons personsHistroy = new Persons
            {
                DateCreated = DateTime.Now,
                FirstName = personDB.FirstName,
                LastName = personDB.LastName,
                PersonalNumber = personDB.PersonalNumber,
                BirthDate = personDB.BirthDate,
                CityId = personDB.CityId,
                GenderId = personDB.GenderId,
                PersonPhoneNumbers = personDB.PersonPhoneNumbers,
                RelationTypeId = personDB.RelationTypeId,
                HistoryRowId = personDB.Id
            };


            // პიროვნების ცხრილში კორექტირება
            if (personModel.FirstName != null && personDB.FirstName != personModel.FirstName)
                personDB.FirstName = personModel.FirstName;

            if (personModel.LastName != null && personDB.LastName != personModel.LastName)
                personDB.LastName = personModel.LastName;

            if (personModel.PersonalNumber != null && personDB.PersonalNumber != personModel.PersonalNumber)
                personDB.PersonalNumber = personModel.PersonalNumber;

            if (personModel.BirthDate != null && personDB.BirthDate != personModel.BirthDate)
                personDB.BirthDate = personModel.BirthDate.GetValueOrDefault();

            if (personModel.CityId != null && personDB.CityId != personModel.CityId)
                personDB.CityId = personModel.CityId;

            if (personModel.GenderId != null && personDB.GenderId != personModel.GenderId)
                personDB.GenderId = personModel.GenderId;


            var personPhoneNumber = new List<PersonPhoneNumbers>();

            if (!personModel.PersonPhoneNumbers.IsNullOrEmpty())
            {
                foreach (var phoneModel in personModel.PersonPhoneNumbers)
                {
                    if (!personDB.PersonPhoneNumbers.Any(a => a.PhoneNumber == phoneModel.PhoneNumber && a.PhoneTypeId == phoneModel.PhoneTypeId && a.DateDeleted == null))
                    {
                        personPhoneNumber.Add(
                            new PersonPhoneNumbers
                            {
                                PhoneNumber = phoneModel.PhoneNumber,
                                PhoneTypeId = (int)phoneModel.PhoneTypeId,
                                DateCreated = currentDate
                            }
                        );
                    }
                }

                if (!personPhoneNumber.IsNullOrEmpty())
                {
                    foreach (var phone in personDB.PersonPhoneNumbers)
                    {
                        if (phone.DateDeleted == null)
                            phone.DateDeleted = currentDate;
                    }

                    foreach (var newPhone in personPhoneNumber)
                    {
                        personDB.PersonPhoneNumbers.Add(newPhone);
                    }
                }
            }


            var result = await personRepository.UpdatePersonAsync(personDB, personsHistroy);

            return result;
        }




        /// <summary>
        /// პიროვნების წაშლა , მონაცემები რეალურად არ იშლება, ეთითება მხოლოდ გაუქმების თარიღი
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<Result> DeletePerson(int personId)
        {
            var currentDate = DateTime.Now;

            if (personId == 0)
            {
                return new Result(false, 12, ValidationMassages.PersonIdMustProvided);
            }

            // არსებობს თუ არა ასეთ იდენტიფიქატორით ჩანაწერი
            var personDB = await personRepository.GetParentPersonAsync(personId);

            if (personDB == null)
            {
                return new Result(false, 13, ValidationMassages.PersonNotFound);
            }

            personDB.DateDeleted = currentDate;

            var result = await personRepository.UpdateAsync(personDB);

            return result;
        }



        /// <summary>
        /// პიროვნების დამტება
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        public async Task<Result> AddRelationPerson(PersonModel personModel)
        {
            if (personModel.ParentId == null)
            {
                return new Result(false, 15, ValidationMassages.PhoneTypeIdNotValid);
            }

            if (personModel.RelationTypeId == null)
            {
                return new Result(false, 15, ValidationMassages.ReletionTypeMustProvided);
            }

            // არსებობს თუ არა ასეთ იდენტიფიქატორით ჩანაწერი
            var personDB = await personRepository.GetParentPersonAsync(personModel.ParentId.GetValueOrDefault());

            if (personDB == null)
            {
                return new Result(false, 16, ValidationMassages.PersonNotFound);
            }


            var personPhoneNumber = new List<PersonPhoneNumbers>();

            if (!personModel.PersonPhoneNumbers.IsNullOrEmpty())
            {
                personModel.PersonPhoneNumbers.ForEach(f => personPhoneNumber.Add(
                    new PersonPhoneNumbers { PhoneNumber = f.PhoneNumber, PhoneTypeId = (int)f.PhoneTypeId, DateCreated = DateTime.Now }));
            }

            if (personModel.CityId != null)
            {
                var city = await citiesRepository.GetByIdAsync(personModel.CityId.Value);

                if (city == null)
                {
                    return new Result(false, 18, ValidationMassages.CityNotFound);
                }
            }


            var personEntity = new Persons
            {
                ParentId = personModel.ParentId,
                RelationTypeId = personModel.RelationTypeId,
                FirstName = personModel.FirstName,
                LastName = personModel.LastName,
                PersonalNumber = personModel.PersonalNumber,
                BirthDate = personModel.BirthDate,
                CityId = personModel.CityId,
                GenderId = personModel.GenderId,
                PersonPhoneNumbers = personPhoneNumber,
                DateCreated = DateTime.Now
            };



            var result = await personRepository.InsertAsync(personEntity);

            return result;
        }


        /// <summary>
        /// დაკავშირებული პიროვნების წაშლა , მონაცემები რეალურად არ იშლება, ეთითება მხოლოდ გაუქმების თარიღი
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<Result> DeleteRelationPerson(int personId)
        {
            var currentDate = DateTime.Now;

            if (personId == 0)
            {
                return new Result(false, 12, ValidationMassages.PersonIdMustProvided);
            }

            // არსებობს თუ არა ასეთ იდენტიფიქატორით ჩანაწერი
            var personDB = await personRepository.GetRelationPersonAsync(personId);

            if (personDB == null)
            {
                return new Result(false, 13, ValidationMassages.PersonNotFound);
            }

            personDB.DateDeleted = currentDate;

            var result = await personRepository.UpdateAsync(personDB);

            return result;
        }



        /// <summary>
        /// ფიზიკური პირის სრული ინფორმაციის წამოღება
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<GenericResult<PersonModel>> GetPerson(int personId)
        {

            if (personId == 0)
            {
                return new GenericResult<PersonModel>(false, 16, ValidationMassages.PersonIdMustProvided, null);
            }

            var personDB = await personRepository.GetPersonAsync(personId);

            var InverseParent = mapper.Map<List<Persons>, List<PersonModel>>(personDB.InverseParent.Where(x => x.DateDeleted == null && x.ParentId != null).ToList());

            var gender = mapper.Map<PersonGender, PersonGenderModel>(personDB.Gender);

            var city = mapper.Map<Cities, CityModel>(personDB.City);

            var personPhoneNumbers = mapper.Map<List<PersonPhoneNumbers>, List<PersonPhoneNumberModel>>(personDB.PersonPhoneNumbers.Where(x => x.DateDeleted == null).DefaultIfEmpty().ToList());


            var personModel = new PersonModel
            {
                Id = personDB.Id,
                FirstName = personDB.FirstName,
                LastName = personDB.LastName,
                BirthDate = personDB.BirthDate,
                PersonalNumber = personDB.PersonalNumber,
                CityId = personDB.CityId,
                GenderId = personDB.GenderId,
                InverseParent = InverseParent,
                Gender = gender,
                City = city,
                PersonPhoneNumbers = personPhoneNumbers
            };

            //var personModel = mapper.Map<Persons, PersonModel>(personDB);

            return new GenericResult<PersonModel>(Result.SuccessInstance(), personModel);
        }


        /// <summary>
        /// რეპორტი თუ რამდენი დაკავშირებული პირი ჰყავს თითოეულ ფიზიკურ პირს, კავშირის ტიპის მიხედვით
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public async Task<GenericResult<List<PersonReportModel>>> GetPersonRelationReport()
        {
            var report = await personRepository.GetPersonRelationReportAsync();

            var personRelationReport = mapper.Map<List<DALPersonReportModel>, List<PersonReportModel>>(report.ToList());

            return new GenericResult<List<PersonReportModel>>(Result.SuccessInstance(), personRelationReport);
        }


        /// <summary>
        /// პიროვნების ფილტრი
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public GenericResult<Page<PersonModel>> GetFilteredPerosns(int pageIndex, int pageSize, PersonFilterModel personFilsterModel)
        {

            var dALPersonFilsterModel = mapper.Map<PersonFilterModel, DALPersonFilterModel>(personFilsterModel);

            var result = personRepository.GetFilteredPerosns(dALPersonFilsterModel);

            var page = new Pager<Persons, PersonModel>(result, pageIndex, pageSize, mapper);

            return new GenericResult<Page<PersonModel>>(Result.SuccessInstance(), page.GetPage());
        }



        /// <summary>
        /// პიროვნების ფაილის ატვირთვა რედაქტირება
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="filesData"></param>
        /// <returns></returns>
        public async Task<Result> UploadUpdatePersonFile(int? personId, IFormFileCollection pFiles)
        {
            var currentDate = DateTime.Now;

            if (personId == 0 || personId == null)
            {
                return new Result(false, 12, ValidationMassages.PersonIdMustProvided);
            }

            if (pFiles.IsNullOrEmpty())
            {
                return new Result(false, 13, ValidationMassages.FileMustProvided);
            }

            var filesData = pFiles.First();

            // არსებობს თუ არა ასეთი იდენტიფიქატორით ჩანაწერი
            var personDB = await personRepository.GetParentPersonAsync(personId.GetValueOrDefault());

            if (personDB == null)
            {
                return new Result(false, 13, ValidationMassages.PersonNotFound);
            }

            //ფაილის ფორმატზე გადამოწმება
            var fileType = Path.GetExtension(filesData.FileName).ToLower();

            if (!fileFormat.Contains(fileType, StringComparison.OrdinalIgnoreCase))
            {
                return new Result(false, -1, ValidationMassages.WrongFileFormat);
            }

            //var megabyte = new decimal(1024 * 1024);
            //// ფაილის ზომის წამოღება 
            //var uploadFileSize = Math.Round(filesData.Length / megabyte, 2, MidpointRounding.AwayFromZero);

            //if ((uploadFileSize) > 5)
            //{
            //    return new Result(false, -1, "ფაილების მაქსიმალური ზომა არ უნდა აღემატებოდეს 5MB -ს");
            //}



            //ფაილებს ვინახავთ პიროვნების იდენტიფიქატორის სახელის მქონე საქაღალდეში
            var subDirectory = personId.GetValueOrDefault().ToString();
            var target = Path.Combine(directoryAddress, subDirectory);

            // თუ ესეთი საქაღალდე არ არსებობს
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            //ფაილის შენახვა მოცემულ საქაღალდეში
            var filePath = Path.Combine(target, Guid.NewGuid().ToString() + fileType);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await filesData.CopyToAsync(stream);
            }


            var personFiles = new PersonFiles
            {
                FilePath = filePath,
                PersonId = personId.GetValueOrDefault(),
                DateCreated = currentDate,
            };

            var result = await personFilesRepository.UpdatePersonFilesAsync(personFiles);

            return result;
        }



        /// <summary>
        /// პიროვნების ფაილის ატვირთვა რედაქტირება
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="filesData"></param>
        /// <returns></returns>
        public async Task<GenericResult<FileModel>> DownloadPersonFile(int? personId)
        {
            if (personId == 0 || personId == null)
            {
                return new GenericResult<FileModel>(false, 12, ValidationMassages.PersonIdMustProvided, null);
            }

            try
            {
                var result = await personFilesRepository.GetPersonFilesAsync(personId.GetValueOrDefault());

                if (result == null || string.IsNullOrWhiteSpace(result.FilePath) || !File.Exists(result.FilePath))
                {
                    return new GenericResult<FileModel>(false, 15, ValidationMassages.FileNotFound, null);
                }

                var path = result.FilePath;

                var file = await File.ReadAllBytesAsync(path);

                var fileName = Path.GetFileName(path);

                var fileModel = new FileModel {fileData = file ,fileName = fileName ,fileType = GetContetnType(fileName)};

                return new GenericResult<FileModel>(Result.SuccessInstance(), fileModel);
            }
            catch (Exception e)
            {
                return new GenericResult<FileModel>(false, 15, $"{ValidationMassages.UnHandledExeption} : {e}", null);
            }
        }

        public string GetContetnType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

    }
}
