using Data.Infrastructure;
using Data.Helper;
using Model.Supplier;
using System.Linq;

namespace Data.Repositories.Supplier
{
    public class SupplierRepository : RepositoryBase<SupplierEntity>, ISupplierRepository
    {
        public SupplierRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override SupplierEntity GetById(string supplierId)
        {
            return DbContext.Supplier.Include("Location").FirstOrDefault(sup => sup.SupplierId == supplierId);
        }
    }
}
