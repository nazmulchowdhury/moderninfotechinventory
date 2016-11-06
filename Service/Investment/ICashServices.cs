using System.Collections.Generic;
using Model.Investment;

namespace Service.Investment
{
    public interface ICashServices
    {
        IEnumerable<CashEntity> GetAllCashes();
        CashEntity GetCash(string cashId);
        CashEntity CreateCash(CashEntity cashEntity);
        bool DeleteCash(string cashId);
    }
}
