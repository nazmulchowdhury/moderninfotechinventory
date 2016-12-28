using Data.Helper;
using System.Linq;
using Model.Accounts;
using Data.Infrastructure;

namespace Data.Repositories.Accounts
{
    public class VatRepository : RepositoryBase<VatEntity>, IVatRepository
    {
        public VatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override VatEntity GetById(string vatId)
        {
            return Context.Vat.Include("Location").Include("TenantInfo").FirstOrDefault(vat => vat.VatId == vatId);
        }

        public override bool Delete(string vatId)
        {
            var vatEntity = Context.Vat.Find(vatId);
            if (vatEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(vatEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.Vat.Remove(vatEntity);
                Context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
