using Data.Infrastructure;
using Data.Helper;
using Model.Supplier;
using System.Linq;

namespace Data.Repositories.Supplier
{
    public class SupplierPaymentRepository : RepositoryBase<SupplierPaymentEntity>, ISupplierPaymentRepository
    {
        public SupplierPaymentRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override SupplierPaymentEntity GetById(string supplierPaymentId)
        {
            return DbContext.SupplierPayment.Include("Supplier").FirstOrDefault(suppay => suppay.SupplierPaymentId == supplierPaymentId);
        }
    }
}
