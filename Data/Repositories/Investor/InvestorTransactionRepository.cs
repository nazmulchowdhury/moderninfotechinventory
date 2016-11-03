using Data.Infrastructure;
using Data.Helper;
using Model.Investor;
using System.Linq;

namespace Data.Repositories.Investor
{
    public class InvestorTransactionRepository : RepositoryBase<InvestorTransactionEntity>, IInvestorTransactionRepository
    {
        public InvestorTransactionRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestorTransactionEntity GetById(string investorTransactionId)
        {
            return DbContext.InvestorTransaction.Include("Investor").FirstOrDefault(invstran => invstran.InvestorTransactionId == investorTransactionId);
        }
    }
}
