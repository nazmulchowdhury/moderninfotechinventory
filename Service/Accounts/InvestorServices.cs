using Model.Accounts;
using Data.Repositories.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public class InvestorServices : IInvestorServices
    {
        private readonly IInvestorRepository investorRepository;

        public InvestorServices(IInvestorRepository investorRepository)
        {
            this.investorRepository = investorRepository;
        }

        public ICollection<InvestorEntity> GetAllInvestors()
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
                storedItem.InvestorName = investorEntity.InvestorName;
                storedItem.LocationId = investorEntity.LocationId;
                storedItem.PhoneNumber = investorEntity.PhoneNumber;
                storedItem.Balance = investorEntity.Balance;
                storedItem.TenantInfo.UserId = investorEntity.TenantInfo.UserId;

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
