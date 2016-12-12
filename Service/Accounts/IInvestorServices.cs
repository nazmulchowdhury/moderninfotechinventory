using System.Collections.Generic;
using Model.Accounts;

namespace Service.Vat
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
