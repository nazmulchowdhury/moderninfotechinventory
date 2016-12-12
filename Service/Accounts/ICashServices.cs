using System.Collections.Generic;
using Model.Accounts;

namespace Service.Vat
{
    public interface ICashServices
    {
        ICollection<CashEntity> GetAllCashes();
        CashEntity GetCash(string cashId);
        CashEntity CreateCash(CashEntity cashEntity);
        bool DeleteCash(string cashId);
    }
}
