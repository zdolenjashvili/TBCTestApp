using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppDAL.Repositories
{
    public class GenderRepository : BaseRepository<PersonGender>, IGenderRepository
    {
        public GenderRepository(ApplicationDBContext db) : base(db)
        {

        }
    }
}
