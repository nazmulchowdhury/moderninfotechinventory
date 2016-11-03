using Data.Infrastructure;
using Data.Helper;
using Model.Investment;

namespace Data.Repositories.Investment
{
    public class VatRepository : RepositoryBase<VatEntity>, IVatRepository
    {
        public VatRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}
