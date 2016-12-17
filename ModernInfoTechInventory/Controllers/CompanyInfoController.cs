using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.CompanyInfo;
using Service.CompanyInfo;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.CompanyInfo;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("companyinfo")]
    public class CompanyInfoController : ApiController
    {
        private readonly ICompanyInfoServices companyInfoServices;

        public CompanyInfoController(ICompanyInfoServices companyInfoServices)
        {
            this.companyInfoServices = companyInfoServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllCompanies()
        {
            var companyInfoEntities = companyInfoServices.GetAllCompanies();
            if (companyInfoEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, companyInfoEntities);
            }
            throw new ApiDataException(1000, "Companies are not found", HttpStatusCode.NotFound);            
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetCompany(string id)
        {
            var companyInfoEntity = companyInfoServices.GetCompany(id);
            if (companyInfoEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, companyInfoEntity);
            }
            throw new ApiDataException(1001, "No Company found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostCompany(CompanyInfoView companyInfoView)
        {
            var companyInfoEntity = new CompanyInfoEntity
            {
                CompanyId = RequestContext.Principal.Identity.GetUserId(),
                CompanyName = companyInfoView.CompanyName,
                ShortName = companyInfoView.ShortName,
                PhoneNumber = companyInfoView.PhoneNumber,
                LocationId = companyInfoView.LocationId,
                Description = companyInfoView.Description,
                Note = companyInfoView.Note,
                Status = companyInfoView.Status
            };
            var insertedEntity = companyInfoServices.CreateCompany(companyInfoEntity);
            return GetCompany(insertedEntity.CompanyId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutCompany(string id, CompanyInfoEntity companyInfoEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, companyInfoServices.UpdateCompany(id, companyInfoEntity));
        }

        [Route("{id:length(36)}")]
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