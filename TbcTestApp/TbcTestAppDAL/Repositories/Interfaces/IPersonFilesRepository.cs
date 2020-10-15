
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TbcTestAppCore.Models;
using TbcTestAppDAL.DAL.DBEntities;

namespace TbcTestAppDAL.Repositories.Interfaces
{
    public interface IPersonFilesRepository : IBaseRepository<PersonFiles>
    {
        Task<Result> UpdatePersonFilesAsync(PersonFiles PersonFiles);
        Task<PersonFiles> GetPersonFilesAsync(int personId);
    }
}
