using TbcTestAppDAL.DAL;
using TbcTestAppDAL.DAL.DBEntities;
using TbcTestAppDAL.Repositories.Interfaces;

namespace TbcTestAppDAL.Repositories
{
    public class PersonRelationTypeRepository : BaseRepository<PersonRelationTypes>, IPersonRelationTypeRepository
    {
        public PersonRelationTypeRepository(ApplicationDBContext db) : base(db)
        {

        }
    }

}
