using Data.Infrastructure;
using Data.Helper;
using Model.Investor;
using System.Linq;

namespace Data.Repositories.Investor
{
    public class InvestorRepository : RepositoryBase<InvestorEntity>, IInvestorRepository
    {
        public InvestorRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestorEntity GetById(string investorId)
        {
            return DbContext.Investor.Include("Location").FirstOrDefault(invst => invst.InvestorId == investorId);
        }
    }
}
