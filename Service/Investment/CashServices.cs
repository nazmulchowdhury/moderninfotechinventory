using System.Collections.Generic;
using Data.Repositories.Investment;
using Model.Investment;

namespace Service.Investment
{
    public class CashServices : ICashServices
    {
        private readonly ICashRepository cashRepository;

        public CashServices(ICashRepository cashRepository)
        {
            this.cashRepository = cashRepository;
        }

        public IEnumerable<CashEntity> GetAllCashes()
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
