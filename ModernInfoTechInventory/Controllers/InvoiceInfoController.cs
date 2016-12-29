using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using System.Net.Http;
using System.Web.Http;
using Model.InvoiceInfo;
using Service.InvoiceInfo;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("invoiceinfo")]
    public class InvoiceInfoController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IInvoiceInfoServices invoiceInfoServices;

        public InvoiceInfoController(IInvoiceInfoServices invoiceInfoServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
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

        [Route("{id:guid}")]
        public HttpResponseMessage GetInvoice(string id)
        {
            var invoiceInfoEntity = invoiceInfoServices.GetInvoice(id);
            if (invoiceInfoEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, invoiceInfoEntity);
            }
            throw new ApiDataException(1001, "No Invoice found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("{id:guid}")]
        public HttpResponseMessage PostInvoice(InvoiceInfoEntity invoiceInfoEntity)
        {
            var insertedEntity = invoiceInfoServices.CreateInvoice(invoiceInfoEntity);
            return GetInvoice(insertedEntity.InvoiceInfoId);
        }

        [Route("{id:guid}")]
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

        [Route("deactivate/{id:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateInvoice(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var invoiceInfoEntity = invoiceInfoServices.GetInvoice(id);
                if (invoiceInfoEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(invoiceInfoEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(invoiceInfoEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invoice is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Invoice has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Invoice is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}