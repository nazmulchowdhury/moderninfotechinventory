using System.Collections.Generic;
using Model.Investor;

namespace Service.Investor
{
    public interface IInvestorServices
    {
        IEnumerable<InvestorEntity> GetAllInvestors();
        InvestorEntity GetInvestor(string investorId);
        InvestorEntity CreateInvestor(InvestorEntity investorEntity);
        bool UpdateInvestor(string investorId, InvestorEntity investorEntity);
        bool DeleteInvestor(string investorId);
    }
}
