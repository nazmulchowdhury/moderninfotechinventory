using Data.Helper;
using System.Linq;
using Model.Tenant;
using Data.Infrastructure;

namespace Data.Repositories.Tenant
{
    public class TenantRepository : RepositoryBase<TenantEntity>, ITenantRepository
    {
        public TenantRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override TenantEntity GetById(string tenantId)
        {
            return Context.Tenant.Include("LoggedUser").FirstOrDefault(ten => ten.TenantId == tenantId);
        }
    }
}
