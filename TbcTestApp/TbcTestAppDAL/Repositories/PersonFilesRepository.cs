using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TbcTestAppCore.Extension;
using TbcTestAppCore.Models;
using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppDAL.Repositories
{
    public class PersonFilesRepository : BaseRepository<PersonFiles>, IPersonFilesRepository
    {
        public PersonFilesRepository(ApplicationDBContext db) : base(db)
        {

        }

        public async Task<Result> UpdatePersonFilesAsync(PersonFiles personFiles)
        {

            var currentDate = DateTime.Now;
            var personFilesDb = db.PersonFiles.Where(w => w.PersonId == personFiles.PersonId && w.DateDeleted == null).ToList();

            if (!personFilesDb.IsNullOrEmpty())
                personFilesDb.ForEach(f => f.DateDeleted = currentDate);

            db.PersonFiles.Add(personFiles);

            var result = await SaveChangesAsync();

            return result;
        }


        public async Task<PersonFiles> GetPersonFilesAsync(int personId)
        {
            var personFilesDb = db.PersonFiles.Where(w => w.PersonId == personId && w.DateDeleted == null).FirstOrDefaultAsync();

            return personFilesDb.Result;
        }
    }
}
