using Data.Helper;
using System.Linq;
using Model.Accounts;
using Data.Infrastructure;

namespace Data.Repositories.Accounts
{
    public class CashRepository : RepositoryBase<CashEntity>, ICashRepository
    {
        public CashRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override CashEntity GetById(string cashId)
        {
            return Context.Cash.Include("TenantInfo").FirstOrDefault(cash => cash.CashId == cashId);
        }

        public override bool Delete(string cashId)
        {
            var cashEntity = Context.Cash.Find(cashId);
            if (cashEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(cashEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Cash.Remove(cashEntity);
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
