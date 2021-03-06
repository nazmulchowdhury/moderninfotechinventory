﻿using Model.Accounts;
using Data.Repositories.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public class InvestorTransactionServices : IInvestorTransactionServices
    {
        private readonly IInvestorTransactionRepository investorTransactionRepository;

        public InvestorTransactionServices(IInvestorTransactionRepository investorTransactionRepository)
        {
            this.investorTransactionRepository = investorTransactionRepository;
        }

        public ICollection<InvestorTransactionEntity> GetAllInvestorTransactions()
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
                storedItem.TenantInfo.UserId = investorTransactionEntity.TenantInfo.UserId;

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
