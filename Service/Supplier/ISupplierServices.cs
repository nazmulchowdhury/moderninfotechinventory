using Model.Supplier;
using System.Collections.Generic;

namespace Service.Supplier
{
    public interface ISupplierServices
    {
        ICollection<SupplierEntity> GetAllSuppliers();
        SupplierEntity GetSupplier(string supplierId);
        SupplierEntity CreateSupplier(SupplierEntity supplierEntity);
        bool UpdateSupplier(string supplierId, SupplierEntity supplierEntity);
        bool DeleteSupplier(string supplierId);
    }
}
