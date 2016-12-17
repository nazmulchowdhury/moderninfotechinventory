using Data.Infrastructure;
using Data.Helper;
using Model.Accounts;
using System.Linq;

namespace Data.Repositories.Account
{
    public class InvestorRepository : RepositoryBase<InvestorEntity>, IInvestorRepository
    {
        public InvestorRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestorEntity GetById(string investorId)
        {
            return Context.Investor.Include("Location").FirstOrDefault(invst => invst.InvestorId == investorId);
        }
    }
}
