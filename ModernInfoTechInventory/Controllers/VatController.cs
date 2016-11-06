using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Investment;
using Model.Investment;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class VatController : ApiController
    {
        private readonly IVatServices vatServices;

        public VatController(IVatServices vatServices)
        {
            this.vatServices = vatServices;
        }

        public HttpResponseMessage GetAllVats()
        {
            var vatEntities = vatServices.GetAllVats().ToList();
            if (vatEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, vatEntities);
            }
            throw new ApiDataException(1000, "Vats are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetVat(string id)
        {
            var vatEntity = vatServices.GetVat(id);
            if (vatEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, vatEntity);
            }
            throw new ApiDataException(1001, "No Vat found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostVat(VatEntity vatEntity)
        {
            var insertedEntity = vatServices.CreateVat(vatEntity);
            return GetVat(insertedEntity.VatId);
        }

        public HttpResponseMessage PutVat(string id, VatEntity vatEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, vatServices.UpdateVat(id, vatEntity));
        }

        public HttpResponseMessage DeleteVat(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = vatServices.DeleteVat(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Vat is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}