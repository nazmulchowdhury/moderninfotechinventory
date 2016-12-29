using Model.Tenant;
using System.Collections.Generic;

namespace Service.Tenant
{
    public interface ITenantServices
    {
        ICollection<TenantEntity> GetAllTenants();
        TenantEntity GetTenant(string tenantId);
        TenantEntity CreateTenant(TenantEntity tenantEntity);
        bool UpdateTenant(string tenantId, TenantEntity tenantEntity);
        bool DeleteTenant(string tenantId);
    }
}
