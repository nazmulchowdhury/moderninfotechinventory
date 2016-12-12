using System;
using System.Linq;
using System.Net;
using Model.Inventory;
using System.Net.Http;
using System.Web.Http;
using Service.Inventory;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;
using ModernInfoTechInventory.ViewModels.Inventory;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryServices categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
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
        public HttpResponseMessage PostCategory(CategoryView categoryView)
        {
            var categoryEntity = new CategoryEntity
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = categoryView.CategoryName
            };
            var insertedEntity = categoryServices.CreateCategory(categoryEntity);
            return GetCategory(insertedEntity.CategoryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutCategory(string id, CategoryEntity categoryEntity)
        {
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
    }
}