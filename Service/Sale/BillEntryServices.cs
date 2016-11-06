using System.Collections.Generic;
using Data.Repositories.Sale;
using Data.Repositories.InvoiceInfo;
using Model.Sale;
using Model.InvoiceInfo;
using System;

namespace Service.Sale
{
    public class BillEntryServices : IBillEntryServices
    {
        private readonly IBillEntryRepository billEntryRepository;
        private readonly IInvoiceInfoRepository invoiceInfoRepository;

        public BillEntryServices(IBillEntryRepository billEntryRepository, IInvoiceInfoRepository invoiceInfoRepository)
        {
            this.billEntryRepository = billEntryRepository;
            this.invoiceInfoRepository = invoiceInfoRepository;
        }

        public IEnumerable<BillEntryEntity> GetAllBillEntries()
        {
            return billEntryRepository.GetAll();
        }

        public BillEntryEntity GetBillEntry(string billEntryId)
        {
            return billEntryRepository.GetById(billEntryId);
        }

        public BillEntryEntity CreateBillEntry(BillEntryEntity billEntryEntity)
        {
            var insertedEntity = billEntryRepository.Add(billEntryEntity);
            invoiceInfoRepository.Add(
                new InvoiceInfoEntity {
                    InvoiceInfoId = Guid.NewGuid().ToString(),
                    BillEntryId = insertedEntity.BillEntryId
                });
            return insertedEntity;
        }

        public bool UpdateBillEntry(string billEntryId, BillEntryEntity billEntryEntity)
        {
            var storedItem = billEntryRepository.GetById(billEntryId);

            if (storedItem != null)
            {
                storedItem.ProductQuantityId = billEntryEntity.ProductQuantityId;
                storedItem.CustomerId = billEntryEntity.CustomerId;

                billEntryRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBillEntry(string billEntryId)
        {
            var isDeletedInvoiceInfo = invoiceInfoRepository.DeleteByBillEntryId(billEntryId);
            if (isDeletedInvoiceInfo)
            {
                return billEntryRepository.Delete(billEntryId);
            }
            else
            {
                return false;
            }
        }
    }
}
