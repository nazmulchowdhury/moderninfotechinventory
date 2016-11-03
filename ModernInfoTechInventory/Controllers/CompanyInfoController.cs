using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Service.CompanyInfo;
using Model.CompanyInfo;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class CompanyInfoController : ApiController
    {
        private readonly CompanyInfoServices companyInfoServices;
        private readonly MapperConfiguration companyInfoMapper;

        public CompanyInfoController(CompanyInfoServices companyInfoServices)
        {
            this.companyInfoServices = companyInfoServices;

            companyInfoMapper = new MapperConfiguration(cfg => cfg.CreateMap<CompanyInfoEntity, CompanyInfoView>()
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => src.Location.LocationName))
                .ForMember(dest => dest.CompanyOwnerName, opts => opts.MapFrom(src => src.User.UserName))
                );
        }

        public HttpResponseMessage GetAllCompanies()
        {
            var companyInfoEntities = companyInfoServices.GetAllCompanies().ToList();
            var companiesForView = companyInfoMapper.CreateMapper().Map<List<CompanyInfoEntity>, List<CompanyInfoView>>(companyInfoEntities);
            if (companiesForView.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, companiesForView);
            }
            throw new ApiDataException(1000, "Companies are not found", HttpStatusCode.NotFound);            
        }

        public HttpResponseMessage GetCompany(string id)
        {
            var companyInfoEntity = companyInfoServices.GetCompany(id);
            if (companyInfoEntity != null)
            {
                var companyForView = companyInfoMapper.CreateMapper().Map<CompanyInfoEntity, CompanyInfoView>(companyInfoEntity);
                return Request.CreateResponse(HttpStatusCode.OK, companyForView);
            }
            throw new ApiDataException(1001, "No Company found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostCompany(CompanyInfoEntity companyInfoEntity)
        {
            var insertedEntity = companyInfoServices.CreateCompany(companyInfoEntity);
            return GetCompany(insertedEntity.CompanyId);
        }

        public HttpResponseMessage PutCompany(string id, CompanyInfoEntity companyInfoEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, companyInfoServices.UpdateCompany(id, companyInfoEntity));
        }

        public HttpResponseMessage DeleteCompany(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = companyInfoServices.DeleteCompany(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Company is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}