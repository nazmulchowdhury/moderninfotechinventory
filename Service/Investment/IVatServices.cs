using System.Collections.Generic;
using Model.Investment;

namespace Service.Investment
{
    public interface IVatServices
    {
        IEnumerable<VatEntity> GetAllVats();
        VatEntity GetVat(string vatId);
        VatEntity CreateVat(VatEntity vatEntity);
        bool UpdateVat(string vatId, VatEntity vatEntity);
        bool DeleteVat(string vatId);
    }
}
