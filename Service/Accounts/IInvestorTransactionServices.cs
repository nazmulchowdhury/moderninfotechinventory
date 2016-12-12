using System.Collections.Generic;
using Model.Accounts;

namespace Service.Vat
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
