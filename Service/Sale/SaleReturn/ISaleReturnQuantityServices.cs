using System.Collections.Generic;
using Model.Sale;

namespace Service.Sale.SaleReturn
{
    public interface ISaleReturnQuantityServices
    {
        IEnumerable<SaleReturnQuantityEntity> GetAllSaleReturnQuantities();
        SaleReturnQuantityEntity GetSaleReturnQuantity(string saleReturnQuantityId);
        SaleReturnQuantityEntity CreateSaleReturnQuantity(SaleReturnQuantityEntity saleReturnQuantityEntity);
        bool UpdateSaleReturnQuantity(string saleReturnQuantityId, SaleReturnQuantityEntity saleReturnQuantityEntity);
        bool DeleteSaleReturnQuantity(string saleReturnQuantityId);
    }
}
