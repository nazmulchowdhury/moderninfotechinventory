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
    public class SubCategoryController : ApiController
    {
        private readonly ISubCategoryServices subCategoryServices;

        public SubCategoryController(ISubCategoryServices subCategoryServices)
        {
            this.subCategoryServices = subCategoryServices;
        }

        public HttpResponseMessage GetAllSubCategories()
        {
            var subcategoryEntities = subCategoryServices.GetAllSubCategories().ToList();
            if (subcategoryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, subcategoryEntities);
            }
            throw new ApiDataException(1000, "SubCategories are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetSubCategory(string id)
        {
            var subCategoryEntity = subCategoryServices.GetSubCategory(id);
            if (subCategoryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, subCategoryEntity);
            }
            throw new ApiDataException(1001, "No SubCategory found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostSubCategory(SubCategoryEntity subCategoryEntity)
        {
            var insertedEntity = subCategoryServices.CreateSubCategory(subCategoryEntity);
            return GetSubCategory(insertedEntity.SubCategoryId);
        }

        public HttpResponseMessage PutSubCategory(string id, SubCategoryEntity subCategoryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, subCategoryServices.UpdateSubCategory(id, subCategoryEntity));
        }

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