using Data.Helper;
using System.Linq;
using Model.Supplier;
using Data.Infrastructure;

namespace Data.Repositories.Supplier
{
    public class SupplierPaymentRepository : RepositoryBase<SupplierPaymentEntity>, ISupplierPaymentRepository
    {
        public SupplierPaymentRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override SupplierPaymentEntity GetById(string supplierPaymentId)
        {
            return Context.SupplierPayment.Include("Supplier").Include("TenantInfo").FirstOrDefault(suppay => suppay.SupplierPaymentId == supplierPaymentId);
        }

        public override bool Delete(string supplierPaymentId)
        {
            var supplierPaymentEntity = Context.SupplierPayment.Find(supplierPaymentId);
            if (supplierPaymentEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(supplierPaymentEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.SupplierPayment.Remove(supplierPaymentEntity);
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
