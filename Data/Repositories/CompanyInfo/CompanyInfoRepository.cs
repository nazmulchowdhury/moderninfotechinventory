using Data.Infrastructure;
using Data.Helper;
using Model.CompanyInfo;
using System.Linq;

namespace Data.Repositories.CompanyInfo
{
    public class CompanyInfoRepository : RepositoryBase<CompanyInfoEntity>, ICompanyInfoRepositoy
    {
        public CompanyInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override CompanyInfoEntity GetById(string companyId)
        {
            return Context.CompanyInfo.Include("Location").Include("User").FirstOrDefault(cmp => cmp.CompanyId == companyId);
        }
    }
}
