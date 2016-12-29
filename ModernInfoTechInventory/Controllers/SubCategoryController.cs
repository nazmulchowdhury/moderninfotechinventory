using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Inventory;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("subcategory")]
    public class SubCategoryController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly ISubCategoryServices subCategoryServices;

        public SubCategoryController(ISubCategoryServices subCategoryServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.subCategoryServices = subCategoryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSubCategories()
        {
            var subcategoryEntities = subCategoryServices.GetAllSubCategories();
            if (subcategoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, subcategoryEntities);
            }
            throw new ApiDataException(1000, "SubCategories are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage GetSubCategory(string id)
        {
            var subCategoryEntity = subCategoryServices.GetSubCategory(id);
            if (subCategoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, subCategoryEntity);
            }
            throw new ApiDataException(1001, "No SubCategory found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("bycategory/{id:guid}")]
        public HttpResponseMessage GetAllSubCategories(string id)
        {
            var subcategoryEntities = subCategoryServices.GetAllSubCategories(id);
            if (subcategoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, subcategoryEntities);
            }
            throw new ApiDataException(1000, "SubCategories are not found for this category id " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSubCategory(SubCategoryEntity subCategoryEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            subCategoryEntity.SubCategoryId = Guid.NewGuid().ToString();
            subCategoryEntity.TenantId = tenantEntity.TenantId;
            subCategoryEntity.TenantInfo = tenantEntity;
            var insertedEntity = subCategoryServices.CreateSubCategory(subCategoryEntity);
            return GetSubCategory(insertedEntity.SubCategoryId);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PutSubCategory(string id, SubCategoryEntity subCategoryEntity)
        {
            subCategoryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, subCategoryServices.UpdateSubCategory(id, subCategoryEntity));
        }

        [Route("{id:guid}")]
        public HttpResponseMessage DeleteSubCategory(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = subCategoryServices.DeleteSubCategory(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "SubCategory is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateSubCategory(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var subCategoryEntity = subCategoryServices.GetSubCategory(id);
                if (subCategoryEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(subCategoryEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(subCategoryEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "SubCategory is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "SubCategory has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "SubCategory is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}