using Model.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public interface IInvestorTransactionServices
    {
        ICollection<InvestorTransactionEntity> GetAllInvestorTransactions();
        InvestorTransactionEntity GetInvestorTransaction(string investorTransactionId);
        InvestorTransactionEntity CreateInvestorTransaction(InvestorTransactionEntity investorTransactionEntity);
        bool UpdateInvestorTransaction(string investorTransactionId, InvestorTransactionEntity investorTransactionEntity);
        bool DeleteInvestorTransaction(string investorTransactionId);
    }
}
