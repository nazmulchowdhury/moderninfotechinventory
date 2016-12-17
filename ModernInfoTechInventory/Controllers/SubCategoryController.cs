using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Service.Inventory;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Inventory;

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
        public HttpResponseMessage PostSubCategory(SubCategoryView subCategoryView)
        {
            var subCategoryEntity = new SubCategoryEntity
            {
                SubCategoryId = Guid.NewGuid().ToString(),
                SubCategoryName = subCategoryView.SubCategoryName,
                CategoryId = subCategoryView.CategoryId,
                UnitId = subCategoryView.UnitId
            };
            var insertedEntity = subCategoryServices.CreateSubCategory(subCategoryEntity);
            return GetSubCategory(insertedEntity.SubCategoryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSubCategory(string id, SubCategoryEntity subCategoryEntity)
        {
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