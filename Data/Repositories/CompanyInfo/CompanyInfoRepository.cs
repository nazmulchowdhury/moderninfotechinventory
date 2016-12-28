using Data.Helper;
using System.Linq;
using Model.CompanyInfo;
using Data.Infrastructure;

namespace Data.Repositories.CompanyInfo
{
    public class CompanyInfoRepository : RepositoryBase<CompanyInfoEntity>, ICompanyInfoRepositoy
    {
        public CompanyInfoRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        public override CompanyInfoEntity GetById(string companyId)
        {
            return Context.CompanyInfo.Include("Location").Include("LoggedUser").Include("TenantInfo").FirstOrDefault(cmp => cmp.CompanyId == companyId);
        }

        public override bool Delete(string companyId)
        {
            var companyEntity = Context.CompanyInfo.Find(companyId);
            if (companyEntity != null)
            {
                var tenantEntity = Context.Tenant.Find(companyEntity.TenantId);
                if (tenantEntity != null)
                {
                    Context.Tenant.Remove(tenantEntity);
                }
                Context.CompanyInfo.Remove(companyEntity);
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
