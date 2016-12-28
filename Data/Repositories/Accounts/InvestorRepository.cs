using Data.Helper;
using System.Linq;
using Model.Accounts;
using Data.Infrastructure;

namespace Data.Repositories.Accounts
{
    public class InvestorRepository : RepositoryBase<InvestorEntity>, IInvestorRepository
    {
        public InvestorRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override InvestorEntity GetById(string investorId)
        {
            return Context.Investor.Include("Location").Include("TenantInfo").FirstOrDefault(invst => invst.InvestorId == investorId);
        }

        public override bool Delete(string investorId)
        {
            var investorEntity = Context.Investor.Find(investorId);
            if (investorEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(investorEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Investor.Remove(investorEntity);
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
