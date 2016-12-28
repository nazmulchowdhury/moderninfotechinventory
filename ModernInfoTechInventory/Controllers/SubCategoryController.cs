using System;
using System.Net;
using System.Linq;
using Model.Inventory;
using Model.BaseModel;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("subcategory")]
    public class SubCategoryController : ApiController
    {
        private readonly ISubCategoryServices subCategoryServices;

        public SubCategoryController(ISubCategoryServices subCategoryServices)
        {
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

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSubCategory(string id)
        {
            var subCategoryEntity = subCategoryServices.GetSubCategory(id);
            if (subCategoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, subCategoryEntity);
            }
            throw new ApiDataException(1001, "No SubCategory found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("bycategory/{id:length(36)}")]
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

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSubCategory(string id, SubCategoryEntity subCategoryEntity)
        {
            subCategoryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, subCategoryServices.UpdateSubCategory(id, subCategoryEntity));
        }

        [Route("{id:length(36)}")]
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
    }
}