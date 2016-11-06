using System.Collections.Generic;
using Data.Repositories.Investor;
using Model.Investor;

namespace Service.Investor
{
    public class InvestorTransactionServices : IInvestorTransactionServices
    {
        private readonly IInvestorTransactionRepository investorTransactionRepository;

        public InvestorTransactionServices(IInvestorTransactionRepository investorTransactionRepository)
        {
            this.investorTransactionRepository = investorTransactionRepository;
        }

        public IEnumerable<InvestorTransactionEntity> GetAllInvestorTransactions()
        {
            return investorTransactionRepository.GetAll();
        }

        public InvestorTransactionEntity GetInvestorTransaction(string investorTransactionId)
        {
            return investorTransactionRepository.GetById(investorTransactionId);
        }

        public InvestorTransactionEntity CreateInvestorTransaction(InvestorTransactionEntity investorTransactionEntity)
        {
            return investorTransactionRepository.Add(investorTransactionEntity);
        }

        public bool UpdateInvestorTransaction(string investorTransactionId, InvestorTransactionEntity investorTransactionEntity)
        {
            var storedItem = investorTransactionRepository.GetById(investorTransactionId);

            if (storedItem != null)
            {
                storedItem.TransactionDate = investorTransactionEntity.TransactionDate;
                storedItem.Amount = investorTransactionEntity.Amount;
                storedItem.Description = investorTransactionEntity.Description;
                storedItem.TransactionType = investorTransactionEntity.TransactionType;

                investorTransactionRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteInvestorTransaction(string investorTransactionId)
        {
            return investorTransactionRepository.Delete(investorTransactionId);
        }
    }
}
