using Model.Sale;
using System.Collections.Generic;

namespace Service.Sale
{
    public interface IBillEntryServices
    {
        ICollection<BillEntryEntity> GetAllBillEntries();
        BillEntryEntity GetBillEntry(string billEntryId);
        BillEntryEntity CreateBillEntry(BillEntryEntity billEntryEntity);
        bool UpdateBillEntry(string billEntryId, BillEntryEntity billEntryEntity);
        bool DeleteBillEntry(string billEntryId);
    }
}
