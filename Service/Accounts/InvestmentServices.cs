using System.Collections.Generic;
using Data.Repositories.Vat;
using Model.Accounts;

namespace Service.Vat
{
    public class InvestmentServices : IInvestmentServices
    {
        private readonly IInvestmentRepository investmentRepository;

        public InvestmentServices(IInvestmentRepository investmentRepository)
        {
            this.investmentRepository = investmentRepository;
        }

        public InvestmentEntity InvestmentAmount(string userId)
        {
            return investmentRepository.GetById(userId);
        }

        public InvestmentEntity CreateInvestment(InvestmentEntity investmentEntity)
        {
            return investmentRepository.Add(investmentEntity);
        }

        public bool DeleteInvestment(string userId)
        {
            return investmentRepository.Delete(userId);
        }
    }
}
