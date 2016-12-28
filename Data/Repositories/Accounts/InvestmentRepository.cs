using Data.Helper;
using System.Linq;
using Model.Accounts;
using Data.Infrastructure;

namespace Data.Repositories.Accounts
{
    public class InvestmentRepository : RepositoryBase<InvestmentEntity>, IInvestmentRepository
    {
        public InvestmentRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override InvestmentEntity GetById(string investmentId)
        {
            return Context.Investment.Include("TenantInfo").FirstOrDefault(invst => invst.InvestmentId == investmentId);
        }

        public override bool Delete(string investmentId)
        {
            var investmentEntity = Context.Investment.Find(investmentId);
            if (investmentEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(investmentEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Investment.Remove(investmentEntity);
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
