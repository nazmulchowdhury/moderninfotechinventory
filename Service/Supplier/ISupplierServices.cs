using System.Collections.Generic;
using Model.Supplier;

namespace Service.Supplier
{
    public interface ISupplierServices
    {
        IEnumerable<SupplierEntity> GetAllSuppliers();
        SupplierEntity GetSupplier(string supplierId);
        SupplierEntity CreateSupplier(SupplierEntity supplierEntity);
        bool UpdateSupplier(string supplierId, SupplierEntity supplierEntity);
        bool DeleteSupplier(string supplierId);
    }
}
