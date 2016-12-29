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
    [RoutePrefix("category")]
    public class CategoryController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly ICategoryServices categoryServices;

        public CategoryController(ICategoryServices categoryServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.categoryServices = categoryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllCategories()
        {
            var categoryEntities = categoryServices.GetAllCategories();
            if (categoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, categoryEntities);
            }
            throw new ApiDataException(1000, "Categories are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetCategory(string id)
        {
            var categoryEntity = categoryServices.GetCategory(id);
            if (categoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, categoryEntity);
            }
            throw new ApiDataException(1001, "No Category found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostCategory(CategoryEntity categoryEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            categoryEntity.CategoryId = Guid.NewGuid().ToString();
            categoryEntity.TenantId = tenantEntity.TenantId;
            categoryEntity.TenantInfo = tenantEntity;
            var insertedEntity = categoryServices.CreateCategory(categoryEntity);
            return GetCategory(insertedEntity.CategoryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutCategory(string id, CategoryEntity categoryEntity)
        {
            categoryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, categoryServices.UpdateCategory(id, categoryEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteCategory(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = categoryServices.DeleteCategory(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Category is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateCategory(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var categoryEntity = categoryServices.GetCategory(id);
                if (categoryEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(categoryEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(categoryEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Category is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Category has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Category is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}