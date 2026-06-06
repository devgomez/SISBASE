using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
