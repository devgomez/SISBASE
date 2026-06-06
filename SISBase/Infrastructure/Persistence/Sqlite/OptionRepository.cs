using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class OptionRepository : GenericRepository<Option>, IOptionRepository
    {
        public OptionRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
