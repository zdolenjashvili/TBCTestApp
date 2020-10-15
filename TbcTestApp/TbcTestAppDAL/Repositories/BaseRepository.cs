using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TbcTestAppCore.Models;
using TbcTestAppDAL.DAL;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppDAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDBContext db;
        public BaseRepository(
            ApplicationDBContext db )
        {
            this.db = db;
        }

        public virtual async Task<Result> RemoveAsync(int id)
        {
            var entity = await db.Set<TEntity>().FindAsync(id);

            db.Set<TEntity>().Remove(entity);

            var result = await SaveChangesAsync();

            return result;
        }

        public virtual Result Remove(int id)
        {
            var entity = db.Set<TEntity>().Find(id);

            db.Set<TEntity>().Remove(entity);

            return SaveChanges();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await db.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity GetById(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public virtual Result Insert(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);

            return SaveChanges();
        }

        public virtual async Task<Result> InsertAsync(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);

            var result = await SaveChangesAsync();

            return result;
        }

        public virtual async Task<Result> UpdateAsync(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;

            var result = await SaveChangesAsync();

            return result;
        }


        public async Task<Result> SaveChangesAsync()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //if (ex.Number == 2627)
                //{

                //}
                return new Result(false, 2, ex.InnerException.ToString());
            }

            return Result.SuccessInstance();
        }

        public Result SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                //if (ex.Number == 2627)
                //{

                //}
                return new Result(false, 1, ex.InnerException.ToString());
            }

            return Result.SuccessInstance();
        }

    }
}
