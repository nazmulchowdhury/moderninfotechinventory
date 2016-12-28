using Model.Accounts;
using Data.Repositories.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
{
    public class CashServices : ICashServices
    {
        private readonly ICashRepository cashRepository;

        public CashServices(ICashRepository cashRepository)
        {
            this.cashRepository = cashRepository;
        }

        public ICollection<CashEntity> GetAllCashes()
        {
            return cashRepository.GetAll();
        }

        public CashEntity GetCash(string cashId)
        {
            return cashRepository.GetById(cashId);
        }

        public CashEntity CreateCash(CashEntity cashEntity)
        {
            return cashRepository.Add(cashEntity);
        }

        public bool DeleteCash(string cashId)
        {
            return cashRepository.Delete(cashId);
        }
    }
}
