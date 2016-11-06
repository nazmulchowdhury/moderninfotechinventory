using System.Collections.Generic;
using Data.Repositories.Investment;
using Model.Investment;

namespace Service.Investment
{
    public class VatServices : IVatServices
    {
        private readonly IVatRepository vatRepository;

        public VatServices(IVatRepository vatRepository)
        {
            this.vatRepository = vatRepository;
        }

        public IEnumerable<VatEntity> GetAllVats()
        {
            return vatRepository.GetAll();
        }

        public VatEntity GetVat(string vatId)
        {
            return vatRepository.GetById(vatId);
        }

        public VatEntity CreateVat(VatEntity vatEntity)
        {
            return vatRepository.Add(vatEntity);
        }

        public bool UpdateVat(string vatId, VatEntity vatEntity)
        {
            var storedItem = vatRepository.GetById(vatId);

            if (storedItem != null)
            {
                storedItem.VatAmount = vatEntity.VatAmount;
                storedItem.VatArea = vatEntity.VatArea;
                storedItem.VatRegistrationNumber = vatEntity.VatRegistrationNumber;

                vatRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteVat(string vatId)
        {
            return vatRepository.Delete(vatId);
        }
    }
}
