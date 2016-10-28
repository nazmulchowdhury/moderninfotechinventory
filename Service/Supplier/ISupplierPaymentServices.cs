using System.Collections.Generic;
using Model.Supplier;

namespace Service.Supplier
{
    public interface ISupplierPaymentServices
    {
        IEnumerable<SupplierPaymentEntity> GetAllSupplierPayments();
        SupplierPaymentEntity GetSupplierPayment(string supplierPaymentId);
        SupplierPaymentEntity CreateSupplierPayment(SupplierPaymentEntity supplierPaymentEntity);
        bool UpdateSupplierPayment(string supplierPaymentId, SupplierPaymentEntity suppliePaymentEntity);
        bool DeleteSupplierPayment(string supplierPaymentId);
    }
}
