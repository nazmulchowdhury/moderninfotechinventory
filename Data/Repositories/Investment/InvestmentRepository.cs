using Data.Infrastructure;
using Data.Helper;
using Model.Investment;
using System.Linq;

namespace Data.Repositories.Investment
{
    public class InvestmentRepository : RepositoryBase<InvestmentEntity>, IInvestmentRepository
    {
        public InvestmentRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestmentEntity GetById(string investmentId)
        {
            return DbContext.Investment.Include("User").FirstOrDefault(inv => inv.InvestmentId == investmentId);
        }
    }
}
