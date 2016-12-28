using Model.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public interface IInvestmentServices
    {
        InvestmentEntity InvestmentAmount(string userId);
        InvestmentEntity CreateInvestment(InvestmentEntity investmentEntity);
        bool DeleteInvestment(string userId);
    }
}
