using System.Collections.Generic;
using Data.Repositories.Investor;
using Model.Investor;

namespace Service.Investor
{
    public class InvestorServices : IInvestorServices
    {
        private readonly IInvestorRepository investorRepository;

        public InvestorServices(IInvestorRepository investorRepository)
        {
            this.investorRepository = investorRepository;
        }

        public IEnumerable<InvestorEntity> GetAllInvestors()
        {
            return investorRepository.GetAll();
        }

        public InvestorEntity GetInvestor(string investorId)
        {
            return investorRepository.GetById(investorId);
        }

        public InvestorEntity CreateInvestor(InvestorEntity investorEntity)
        {
            return investorRepository.Add(investorEntity);
        }

        public bool UpdateInvestor(string investorId, InvestorEntity investorEntity)
        {
            var storedItem = investorRepository.GetById(investorId);

            if (storedItem != null)
            {
                storedItem.LocationId = investorEntity.LocationId;
                storedItem.PhoneNumber = investorEntity.PhoneNumber;
                storedItem.Balance = investorEntity.Balance;

                investorRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteInvestor(string investorId)
        {
            return investorRepository.Delete(investorId);
        }
    }
}
