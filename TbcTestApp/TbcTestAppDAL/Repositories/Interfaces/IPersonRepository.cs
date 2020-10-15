
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TbcTestAppDAL.Models;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppCore.Models;
using TbcTestAppDAL.Models.FilterModels;

namespace TbcTestAppDAL.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Persons>
    {
        Task<Result> UpdatePersonAsync(Persons persons,Persons personsHistroy);
        Task<Persons> GetParentPersonAsync(int id);
        Task<Persons> GetRelationPersonAsync(int id);
        Task<Persons> GetPersonAsync(int id);
        Task<IList<DALPersonReportModel>> GetPersonRelationReportAsync();
        IQueryable<Persons> GetFilteredPerosns(DALPersonFilterModel personFilsterModel);
    }
}
