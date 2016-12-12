using System.Collections.Generic;
using Model.Sale;

namespace Service.Sale
{
    public interface ISaleReturnServices
    {
        ICollection<SaleReturnEntity> GetAllSaleReturns();
        SaleReturnEntity GetSaleReturn(string saleReturnId);
        SaleReturnEntity CreateSaleReturn(SaleReturnEntity saleReturnEntity);
        bool UpdateSaleReturn(string saleReturnId, SaleReturnEntity saleReturnEntity);
        bool DeleteSaleReturn(string saleReturnId);
    }
}
