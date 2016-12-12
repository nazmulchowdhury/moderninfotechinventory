using System.Collections.Generic;
using Data.Repositories.Supplier;
using Model.Supplier;

namespace Service.Supplier
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierServices(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public ICollection<SupplierEntity> GetAllSuppliers()
        {
            return supplierRepository.GetAll();
        }

        public SupplierEntity GetSupplier(string supplierId)
        {
            return supplierRepository.GetById(supplierId);
        }

        public SupplierEntity CreateSupplier(SupplierEntity supplierEntity)
        {
            return supplierRepository.Add(supplierEntity);
        }

        public bool UpdateSupplier(string supplierId, SupplierEntity supplierEntity)
        {
            var storedItem = supplierRepository.GetById(supplierId);

            if (storedItem != null)
            {
                storedItem.SupplierName = supplierEntity.SupplierName;
                storedItem.LocationId = supplierEntity.LocationId;
                storedItem.PhoneNumber = supplierEntity.PhoneNumber;

                supplierRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSupplier(string supplierId)
        {
            return supplierRepository.Delete(supplierId);
        }
    }
}
