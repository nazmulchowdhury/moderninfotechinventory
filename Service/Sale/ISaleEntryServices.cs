using Model.Sale;
using System.Collections.Generic;

namespace Service.Sale
{
    public interface ISaleEntryServices
    {
        ICollection<SaleEntryEntity> GetAllSaleEntries();
        SaleEntryEntity GetSaleEntry(string saleEntryId);
        SaleEntryEntity CreateSaleEntry(SaleEntryEntity saleEntryEntity);
        bool UpdateSaleEntry(string saleEntryId, SaleEntryEntity saleEntryEntity);
        bool DeleteSaleEntry(string saleEntryId);
    }
}
