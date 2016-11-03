using Data.Infrastructure;
using Data.Helper;
using Model.Investment;

namespace Data.Repositories.Investment
{
    public class ExpenseRepository : RepositoryBase<ExpenseEntity>, IExpenseRepository
    {
        public ExpenseRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
