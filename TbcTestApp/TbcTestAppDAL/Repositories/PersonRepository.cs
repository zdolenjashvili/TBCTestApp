using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TbcTestAppDAL.Models;
using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;
using TbcTestAppCore.Models;
using TbcTestAppDAL.Models.FilterModels;

namespace TbcTestAppDAL.Repositories
{
    public class PersonRepository : BaseRepository<Persons>, IPersonRepository
    {
        public PersonRepository(ApplicationDBContext db) : base(db)
        {

        }

        public async Task<Result> UpdatePersonAsync(Persons persons, Persons personsHistroy)
        {
            var result = Result.SuccessInstance();

            if (db.Entry(persons).State == EntityState.Modified)
            {
                db.Persons.Add(personsHistroy);
            }

            result = await SaveChangesAsync();

            return result;
        }


        public async Task<Persons> GetParentPersonAsync(int id)
        {
            var query = db.Persons.Where(w => w.Id == id && w.DateDeleted == null && w.ParentId == null && w.HistoryRowId == null)
                .Include(i => i.PersonPhoneNumbers);

            var person = await query.FirstOrDefaultAsync();

            return person;
        }

        public async Task<Persons> GetRelationPersonAsync(int id)
        {
            var query = db.Persons.Where(w => w.Id == id && w.DateDeleted == null && w.HistoryRowId == null)
                .Include(i => i.PersonPhoneNumbers);

            var person = await query.FirstOrDefaultAsync();

            return person;
        }

        public async Task<Persons> GetPersonAsync(int id)
        {
            var query = db.Persons.Where(w => w.Id == id && w.DateDeleted == null && w.ParentId == null && w.HistoryRowId == null)
                .Include(i => i.PersonPhoneNumbers).ThenInclude(t => t.PhoneType)
                .Include(i => i.Gender)
                .Include(i => i.City)
                .Include(i => i.InverseParent).ThenInclude(t => t.RelationType)
                .Include(i => i.InverseParent).ThenInclude(t => t.Gender)
                .Include(i => i.InverseParent).ThenInclude(t => t.PersonPhoneNumbers).ThenInclude(t => t.PhoneType)
                .Include(i => i.InverseParent).ThenInclude(t => t.City)
                ;

            var person = await query.FirstOrDefaultAsync();

            return person;
        }



        /// <summary>
        /// რეპორტი თუ რამდენი დაკავშირებული პირი ჰყავს თითოეულ ფიზიკურ პირს, კავშირის ტიპის მიხედვით
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DALPersonReportModel>> GetPersonRelationReportAsync()
        {
            var queryGrouped = await db.Persons.Where(w => w.DateDeleted == null && w.ParentId != null && w.HistoryRowId == null)
                               .GroupBy(g => new { g.ParentId, g.RelationTypeId, g.RelationType.RelationTypeName })
                               .Select(s => new DALPersonRelationReportModel { ParentId = s.Key.ParentId, RelationTypeId = s.Key.RelationTypeId, RelationName = s.Key.RelationTypeName, Count = s.Count() })
                               .OrderBy(o => o.ParentId).ThenBy(t => t.RelationTypeId).ToListAsync();


            var result = await db.Persons.Where(w => w.DateDeleted == null && w.ParentId == null && w.HistoryRowId == null)
                .Select(s =>
                new DALPersonReportModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                }).ToListAsync();

            result.ForEach(f => f.personRelationReportModels = queryGrouped.Where(w => w.ParentId == f.Id).ToList());



            return result;
        }


        /// <summary>
        /// რეპორტი თუ რამდენი დაკავშირებული პირი ჰყავს თითოეულ ფიზიკურ პირს, კავშირის ტიპის მიხედვით
        /// </summary>
        /// <returns></returns>
        public IQueryable<Persons> GetFilteredPerosns(DALPersonFilterModel personFilsterModel)
        {
            var query = db.Persons.Where(w => w.DateDeleted == null && w.ParentId == null && w.HistoryRowId == null);

            if (personFilsterModel != null)
            {
                if (personFilsterModel.Id != null)
                {
                    query = query.Where(w => w.Id == personFilsterModel.Id);
                }

                if (!string.IsNullOrWhiteSpace(personFilsterModel.FirstName))
                {
                    query = query.Where(w => w.FirstName.Contains(personFilsterModel.FirstName));
                }

                if (!string.IsNullOrWhiteSpace(personFilsterModel.LastName))
                {
                    query = query.Where(w => w.LastName.Contains(personFilsterModel.LastName));
                }

                if (!string.IsNullOrWhiteSpace(personFilsterModel.PersonalNumber))
                {
                    query = query.Where(w => w.PersonalNumber.Contains(personFilsterModel.PersonalNumber));
                }

                if (personFilsterModel.GenderId != null)
                {
                    query = query.Where(w => w.GenderId == personFilsterModel.GenderId);
                }

                if (personFilsterModel.BirthDate != null)
                {
                    query = query.Where(w => w.BirthDate == personFilsterModel.BirthDate);
                }

                if (personFilsterModel.CityId != null)
                {
                    query = query.Where(w => w.CityId == personFilsterModel.CityId);
                }
            }


            var result = query.AsQueryable();

            return result;
        }
    }
}


