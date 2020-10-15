using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppDAL.Repositories
{
    public class CitiesRepository : BaseRepository<Cities>, ICitiesRepository
    {
        public CitiesRepository(ApplicationDBContext db) : base(db)
        {

        }
    }

}
