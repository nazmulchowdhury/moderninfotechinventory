using System.Collections.Generic;
using Model.Investment;

namespace Service.Investment
{
    public interface IInvestmentServices
    {
        InvestmentEntity InvestmentAmount(string userId);
        InvestmentEntity CreateInvestment(InvestmentEntity investmentEntity);
        bool DeleteInvestment(string userId);
    }
}
