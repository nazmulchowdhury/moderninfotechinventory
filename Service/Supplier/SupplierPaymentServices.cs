using System.Collections.Generic;
using Data.Repositories.Supplier;
using Model.Supplier;

namespace Service.Supplier
{
    public class SupplierPaymentServices : ISupplierPaymentServices
    {
        private readonly SupplierPaymentRepository supplierPaymentRepository;

        public SupplierPaymentServices(SupplierPaymentRepository supplierPaymentRepository)
        {
            this.supplierPaymentRepository = supplierPaymentRepository;
        }

        public IEnumerable<SupplierPaymentEntity> GetAllSupplierPayments()
        {
            return supplierPaymentRepository.GetAll();
        }

        public SupplierPaymentEntity GetSupplierPayment(string supplierPaymentId)
        {
            return supplierPaymentRepository.GetById(supplierPaymentId);
        }

        public SupplierPaymentEntity CreateSupplierPayment(SupplierPaymentEntity supplierPaymentEntity)
        {
            return supplierPaymentRepository.Add(supplierPaymentEntity);
        }

        public bool UpdateSupplierPayment(string supplierPaymentId, SupplierPaymentEntity suppliePaymentEntity)
        {
            var storedItem = supplierPaymentRepository.GetById(supplierPaymentId);

            if (storedItem != null)
            {
                storedItem.PaymentDate = suppliePaymentEntity.PaymentDate;
                storedItem.PaidAmount = suppliePaymentEntity.PaidAmount;
                storedItem.Description = suppliePaymentEntity.Description;

                supplierPaymentRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSupplierPayment(string supplierPaymentId)
        {
            return supplierPaymentRepository.Delete(supplierPaymentId);
        }
    }
}
