using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Model.InvoiceInfo;
using Service.InvoiceInfo;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("invoiceinfo")]
    public class InvoiceInfoController : ApiController
    {
        private readonly IInvoiceInfoServices invoiceInfoServices;

        public InvoiceInfoController(IInvoiceInfoServices invoiceInfoServices)
        {
            this.invoiceInfoServices = invoiceInfoServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllInvoices()
        {
            var invoiceInfoEntities = invoiceInfoServices.GetAllInvoices();
            if (invoiceInfoEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, invoiceInfoEntities);
            }
            throw new ApiDataException(1000, "Invoices are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetInvoice(string id)
        {
            var productEntity = invoiceInfoServices.GetInvoice(id);
            if (productEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, productEntity);
            }
            throw new ApiDataException(1001, "No Invoice found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PostInvoice(InvoiceInfoEntity invoiceInfoEntity)
        {
            var insertedEntity = invoiceInfoServices.CreateInvoice(invoiceInfoEntity);
            return GetInvoice(insertedEntity.InvoiceInfoId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteInvoiceInfo(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = invoiceInfoServices.DeleteInvoice(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Invoice is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}