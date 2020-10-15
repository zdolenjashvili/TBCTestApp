using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppCore.Models;

namespace TbcTestAppDAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        TEntity GetById(int id);

        Task<Result> InsertAsync(TEntity entity);

        Result Insert(TEntity entity);

        Task<Result> UpdateAsync(TEntity entity);

        Task<Result> RemoveAsync(int id);

        Result Remove(int id);

        Task<Result> SaveChangesAsync();

        Result SaveChanges();
    }
}
