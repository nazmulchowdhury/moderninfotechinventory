using Data.Helper;
using System.Linq;
using Model.Accounts;
using Data.Infrastructure;

namespace Data.Repositories.Accounts
{
    public class ExpenseRepository : RepositoryBase<ExpenseEntity>, IExpenseRepository
    {
        public ExpenseRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override ExpenseEntity GetById(string expenseId)
        {
            return Context.Expense.Include("TenantInfo").FirstOrDefault(exp => exp.ExpenseId == expenseId);
        }

        public override bool Delete(string expenseId)
        {
            var expenseEntity = Context.Expense.Find(expenseId);
            if (expenseEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(expenseEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Expense.Remove(expenseEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
