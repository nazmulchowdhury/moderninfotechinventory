using Data.Infrastructure;
using Data.Helper;
using Model.Accounts;

namespace Data.Repositories.Vat
{
    public class CashRepository : RepositoryBase<CashEntity>, ICashRepository
    {
        public CashRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
