using Data.Infrastructure;
using Data.Helper;
using Model.Inventory;

namespace Data.Repositories.Inventory
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
