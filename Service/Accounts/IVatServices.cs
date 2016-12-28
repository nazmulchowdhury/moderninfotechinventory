using Model.Accounts;
using System.Collections.Generic;

namespace Service.Accounts
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
