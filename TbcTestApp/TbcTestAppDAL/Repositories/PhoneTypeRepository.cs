using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppDAL.Repositories
{
    public class PhoneTypeRepository : BaseRepository<PhoneTypes>, IPhoneTypeRepository
    {
        public PhoneTypeRepository(ApplicationDBContext db) : base(db)
        {

        }
    }

}
