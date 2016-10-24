using System.Collections.Generic;
using Model.CompanyInfo;

namespace Service.CompanyInfo
{
    public interface ICompanyInfoServices
    {
        IEnumerable<CompanyInfoEntity> GetAllCompanies();
        CompanyInfoEntity GetCompany(string companyId);
        CompanyInfoEntity CreateCompany(CompanyInfoEntity companyInfoEntity);
        bool UpdateCompany(string companyId, CompanyInfoEntity companyInfoEntity);
        bool DeleteCompany(string companyId);
    }
}
