using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;

namespace SISBase.Infrastructure.Persistence.Sqlite
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
