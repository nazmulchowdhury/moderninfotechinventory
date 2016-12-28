using Model.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public interface ICashServices
    {
        ICollection<CashEntity> GetAllCashes();
        CashEntity GetCash(string cashId);
        CashEntity CreateCash(CashEntity cashEntity);
        bool DeleteCash(string cashId);
    }
}
