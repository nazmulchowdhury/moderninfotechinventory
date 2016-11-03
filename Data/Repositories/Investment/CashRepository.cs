using Data.Infrastructure;
using Data.Helper;
using Model.Investment;

namespace Data.Repositories.Investment
{
    public class CashRepository : RepositoryBase<CashEntity>, ICashRepository
    {
        public CashRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
