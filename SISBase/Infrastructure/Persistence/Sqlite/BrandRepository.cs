using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
