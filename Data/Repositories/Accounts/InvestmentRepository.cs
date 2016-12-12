using Data.Infrastructure;
using Data.Helper;
using Model.Accounts;
using System.Linq;

namespace Data.Repositories.Vat
{
    public class InvestmentRepository : RepositoryBase<InvestmentEntity>, IInvestmentRepository
    {
        public InvestmentRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestmentEntity GetById(string investmentId)
        {
            return DbContext.Investment.Find(investmentId);
        }
    }
}
