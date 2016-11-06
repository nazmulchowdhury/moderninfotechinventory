using System.Collections.Generic;
using Model.Sale;

namespace Service.Sale
{
    public interface IBillEntryServices
    {
        IEnumerable<BillEntryEntity> GetAllBillEntries();
        BillEntryEntity GetBillEntry(string billEntryId);
        BillEntryEntity CreateBillEntry(BillEntryEntity billEntryEntity);
        bool UpdateBillEntry(string billEntryId, BillEntryEntity billEntryEntity);
        bool DeleteBillEntry(string billEntryId);
    }
}
