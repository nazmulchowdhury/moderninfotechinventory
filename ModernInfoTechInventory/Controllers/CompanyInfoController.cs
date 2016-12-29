using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using System.Net.Http;
using System.Web.Http;
using Model.CompanyInfo;
using Service.CompanyInfo;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("companyinfo")]
    public class CompanyInfoController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly ICompanyInfoServices companyInfoServices;

        public CompanyInfoController(ICompanyInfoServices companyInfoServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("{id:guid}")]
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
        public HttpResponseMessage PostCompany(CompanyInfoEntity companyInfoEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            companyInfoEntity.CompanyId = RequestContext.Principal.Identity.GetUserId();
            companyInfoEntity.TenantId = tenantEntity.TenantId;
            companyInfoEntity.TenantInfo = tenantEntity;
            var insertedEntity = companyInfoServices.CreateCompany(companyInfoEntity);
            return GetCompany(insertedEntity.CompanyId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutCompany(string id, CompanyInfoEntity companyInfoEntity)
        {
            companyInfoEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, companyInfoServices.UpdateCompany(id, companyInfoEntity));
        }

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateCompany(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var companyInfoEntity = companyInfoServices.GetCompany(id);
                if (companyInfoEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(companyInfoEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(companyInfoEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Company is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Company has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Company is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}