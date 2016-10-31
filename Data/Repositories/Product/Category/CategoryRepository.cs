﻿using Data.Infrastructure;
using Data.Helper;
using Model.Product;

namespace Data.Repositories.Product.Category
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}