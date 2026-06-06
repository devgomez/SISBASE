using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
