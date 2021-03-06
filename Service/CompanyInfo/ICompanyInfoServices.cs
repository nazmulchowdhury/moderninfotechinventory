﻿using Model.CompanyInfo;
using System.Collections.Generic;

namespace Service.CompanyInfo
{
    public interface ICompanyInfoServices
    {
        ICollection<CompanyInfoEntity> GetAllCompanies();
        CompanyInfoEntity GetCompany(string companyId);
        CompanyInfoEntity CreateCompany(CompanyInfoEntity companyInfoEntity);
        bool UpdateCompany(string companyId, CompanyInfoEntity companyInfoEntity);
        bool DeleteCompany(string companyId);
    }
}
