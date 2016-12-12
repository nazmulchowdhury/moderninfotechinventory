using System.Collections.Generic;
using Data.Repositories.Vat;
using Model.Vat;

namespace Service.Vat
{
    public class VatServices : IVatServices
    {
        private readonly IVatRepository vatRepository;

        public VatServices(IVatRepository vatRepository)
        {
            this.vatRepository = vatRepository;
        }

        public ICollection<VatEntity> GetAllVats()
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
                storedItem.LocationId = vatEntity.LocationId;
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
