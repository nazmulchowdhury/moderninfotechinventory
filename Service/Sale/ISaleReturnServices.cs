using Model.Sale;
using System.Collections.Generic;

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
