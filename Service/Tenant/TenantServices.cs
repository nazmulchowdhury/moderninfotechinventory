using Model.BaseModel;
using Data.Repositories.Tenant;
using System.Collections.Generic;

namespace Service.Tenant
{
    public class TenantServices : ITenantServices
    {
        private readonly ITenantRepository tenantRepository;

        public TenantServices(ITenantRepository tenantRepository)
        {
            this.tenantRepository = tenantRepository;
        }

        public ICollection<TenantEntity> GetAllTenants()
        {
            return tenantRepository.GetAll();
        }

        public TenantEntity GetTenant(string tenantId)
        {
            return tenantRepository.GetById(tenantId);
        }

        public TenantEntity CreateTenant(TenantEntity tenantEntity)
        {
            return tenantRepository.Add(tenantEntity);
        }

        public bool UpdateTenant(string tenantId, TenantEntity tenantEntity)
        {
            var storedItem = tenantRepository.GetById(tenantId);

            if (storedItem != null && (storedItem.Status ^ tenantEntity.Status))
            {
                storedItem.UserId = tenantEntity.UserId;
                storedItem.Status = tenantEntity.Status;
                if (tenantEntity.Status)
                {
                    storedItem.ActivationDate = tenantEntity.ActivationDate;
                    storedItem.InactivationDate = null;
                }
                else
                {
                    storedItem.InactivationDate = tenantEntity.InactivationDate;
                }

                tenantRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTenant(string tenantId)
        {
            return tenantRepository.Delete(tenantId);
        }
    }
}
