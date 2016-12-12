using System.Collections.Generic;
using Model.Vat;

namespace Service.Vat
{
    public interface IVatServices
    {
        ICollection<VatEntity> GetAllVats();
        VatEntity GetVat(string vatId);
        VatEntity CreateVat(VatEntity vatEntity);
        bool UpdateVat(string vatId, VatEntity vatEntity);
        bool DeleteVat(string vatId);
    }
}
