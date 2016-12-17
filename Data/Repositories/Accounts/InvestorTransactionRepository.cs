using Data.Infrastructure;
using Data.Helper;
using Model.Accounts;
using System.Linq;

namespace Data.Repositories.Account
{
    public class InvestorTransactionRepository : RepositoryBase<InvestorTransactionEntity>, IInvestorTransactionRepository
    {
        public InvestorTransactionRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override InvestorTransactionEntity GetById(string investorTransactionId)
        {
            return Context.InvestorTransaction.Include("Investor").FirstOrDefault(invstran => invstran.InvestorTransactionId == investorTransactionId);
        }
    }
}
