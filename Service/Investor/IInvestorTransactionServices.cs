using System.Collections.Generic;
using Model.Investor;

namespace Service.Investor
{
    public interface IInvestorTransactionServices
    {
        IEnumerable<InvestorTransactionEntity> GetAllInvestorTransactions();
        InvestorTransactionEntity GetInvestorTransaction(string investorTransactionId);
        InvestorTransactionEntity CreateInvestorTransaction(InvestorTransactionEntity investorTransactionEntity);
        bool UpdateInvestorTransaction(string investorTransactionId, InvestorTransactionEntity investorTransactionEntity);
        bool DeleteInvestorTransaction(string investorTransactionId);
    }
}
