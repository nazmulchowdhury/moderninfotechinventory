using Data.Helper;
using System.Linq;
using Model.Accounts;
using Data.Infrastructure;

namespace Data.Repositories.Accounts
{
    public class InvestorTransactionRepository : RepositoryBase<InvestorTransactionEntity>, IInvestorTransactionRepository
    {
        public InvestorTransactionRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestorTransactionEntity GetById(string investorTransactionId)
        {
            return Context.InvestorTransaction.Include("Investor").Include("TenantInfo").FirstOrDefault(invstran => invstran.InvestorTransactionId == investorTransactionId);
        }

        public override bool Delete(string investorTransactionId)
        {
            var investorTransactionEntity = Context.InvestorTransaction.Find(investorTransactionId);
            if (investorTransactionEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(investorTransactionEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.InvestorTransaction.Remove(investorTransactionEntity);
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
