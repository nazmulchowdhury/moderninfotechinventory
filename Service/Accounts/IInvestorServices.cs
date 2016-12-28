using Model.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public interface IInvestorServices
    {
        ICollection<InvestorEntity> GetAllInvestors();
        InvestorEntity GetInvestor(string investorId);
        InvestorEntity CreateInvestor(InvestorEntity investorEntity);
        bool UpdateInvestor(string investorId, InvestorEntity investorEntity);
        bool DeleteInvestor(string investorId);
    }
}
