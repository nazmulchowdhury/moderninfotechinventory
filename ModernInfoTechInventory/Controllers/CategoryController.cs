using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Product;
using Model.Product;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private readonly ICategoryServices categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        public HttpResponseMessage GetAllCategories()
        {
            var categoryEntities = categoryServices.GetAllCategories().ToList();
            if (categoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, categoryEntities);
            }
            throw new ApiDataException(1000, "Categories are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetCategory(string id)
        {
            var categoryEntity = categoryServices.GetCategory(id);
            if (categoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, categoryEntity);
            }
            throw new ApiDataException(1001, "No Category found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostCategory(CategoryEntity categoryEntity)
        {
            var insertedEntity = categoryServices.CreateCategory(categoryEntity);
            return GetCategory(insertedEntity.CategoryId);
        }

        public HttpResponseMessage PutCategory(string id, CategoryEntity categoryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, categoryServices.UpdateCategory(id, categoryEntity));
        }

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