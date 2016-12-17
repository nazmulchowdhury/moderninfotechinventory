using Data.Infrastructure;
using Data.Helper;
using Model.Vat;
using System.Linq;

namespace Data.Repositories.Vat
{
    public class VatRepository : RepositoryBase<VatEntity>, IVatRepository
    {
        public VatRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override VatEntity GetById(string vatId)
        {
            return Context.Vat.Include("Location").FirstOrDefault(vat => vat.VatId == vatId);
        }
    }
}
