using System.Collections.Generic;
using Data.Repositories.CompanyInfo;
using Model.CompanyInfo;

namespace Service.CompanyInfo
{
    public class CompanyInfoServices : ICompanyInfoServices
    {
        private readonly ICompanyInfoRepositoy companyInfoRepository;

        public CompanyInfoServices(ICompanyInfoRepositoy companyInfoRepository)
        {
            this.companyInfoRepository = companyInfoRepository;
        }

        public IEnumerable<CompanyInfoEntity> GetAllCompanies()
        {
            return companyInfoRepository.GetAll();
        }

        public CompanyInfoEntity GetCompany(string companyId)
        {
            return companyInfoRepository.GetById(companyId);
        }
        
        public CompanyInfoEntity CreateCompany(CompanyInfoEntity companyInfoEntity)
        {
            return companyInfoRepository.Add(companyInfoEntity);
        }

        public bool UpdateCompany(string companyId, CompanyInfoEntity companyInfoEntity)
        {
            var storedItem = companyInfoRepository.GetById(companyId);
            
            if (storedItem != null)
            {
                storedItem.CompanyName = companyInfoEntity.CompanyName;
                storedItem.ShortName = companyInfoEntity.ShortName;
                storedItem.PhoneNumber = companyInfoEntity.PhoneNumber;
                storedItem.LocationId = companyInfoEntity.LocationId;
                storedItem.Description = companyInfoEntity.Description;
                storedItem.Note = companyInfoEntity.Note;
                storedItem.Status = companyInfoEntity.Status;

                companyInfoRepository.Update(storedItem);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCompany(string companyId)
        {
            return companyInfoRepository.Delete(companyId);
        }
    }
}
