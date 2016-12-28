using Data.Helper;
using System.Linq;
using Model.Supplier;
using Data.Infrastructure;

namespace Data.Repositories.Supplier
{
    public class SupplierRepository : RepositoryBase<SupplierEntity>, ISupplierRepository
    {
        public SupplierRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override SupplierEntity GetById(string supplierId)
        {
            return Context.Supplier.Include("Location").Include("TenantInfo").FirstOrDefault(sup => sup.SupplierId == supplierId);
        }

        public override bool Delete(string supplierId)
        {
            var supplierEntity = Context.Supplier.Find(supplierId);
            if (supplierEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(supplierEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Supplier.Remove(supplierEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
