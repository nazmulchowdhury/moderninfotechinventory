using Data.Infrastructure;
using Data.Helper;
using Model.Accounts;

namespace Data.Repositories.Vat
{
    public class ExpenseRepository : RepositoryBase<ExpenseEntity>, IExpenseRepository
    {
        public ExpenseRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
