using System;
using System.Net;
using Model.Sale;
using System.Linq;
using Model.Tenant;
using Service.Sale;
using Service.Tenant;
using System.Net.Http;
using System.Web.Http;
using Model.Inventory;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("saleentry")]
    public class SaleEntryController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly ISaleEntryServices saleEntryServices;

        public SaleEntryController(ISaleEntryServices saleEntryServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.saleEntryServices = saleEntryServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSaleEntries()
        {
            var saleEntryEntities = saleEntryServices.GetAllSaleEntries();
            if (saleEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, saleEntryEntities);
            }
            throw new ApiDataException(1000, "Sale Entries are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSaleEntry(string id)
        {
            var saleEntryEntity = saleEntryServices.GetSaleEntry(id);
            if (saleEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, saleEntryEntity);
            }
            throw new ApiDataException(1001, "No Sale Entry found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSaleEntry(SaleEntryEntity saleEntryEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            saleEntryEntity.SaleEntryId = Guid.NewGuid().ToString();
            saleEntryEntity.TenantId = tenantEntity.TenantId;
            saleEntryEntity.TenantInfo = tenantEntity;
            saleEntryEntity.SaledProducts = saleEntryEntity.SaledProducts.Select(SaledProduct =>
            {
                SaledProduct.ProductQuantityId = Guid.NewGuid().ToString();
                SaledProduct.TenantId = tenantEntity.TenantId;
                return SaledProduct;
            }).ToList();
            
            var insertedEntity = saleEntryServices.CreateSaleEntry(saleEntryEntity);
            return GetSaleEntry(insertedEntity.SaleEntryId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSaleEntry(string id, SaleEntryEntity saleEntryEntity)
        {
            saleEntryEntity.TenantInfo = new TenantEntity
            {
                UserId = RequestContext.Principal.Identity.GetUserId()
            };
            return Request.CreateResponse(HttpStatusCode.OK, saleEntryServices.UpdateSaleEntry(id, saleEntryEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteSaleEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = saleEntryServices.DeleteSaleEntry(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Sale Entry is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateSaleEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var saleEntryEntity = saleEntryServices.GetSaleEntry(id);
                if (saleEntryEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(saleEntryEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(saleEntryEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Sale Entry is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Sale Entry has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Sale Entry is already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}