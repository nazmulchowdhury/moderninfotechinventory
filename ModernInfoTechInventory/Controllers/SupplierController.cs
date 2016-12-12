using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Supplier;
using AutoMapper;
using Model.Supplier;
using ModernInfoTechInventory.ViewModels.Supplier;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : ApiController
    {
        private readonly ISupplierServices supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSuppliers()
        {
            var supplierEntities = supplierServices.GetAllSuppliers();
            if (supplierEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierEntities);
            }
            throw new ApiDataException(1000, "Suppliers are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSupplier(string id)
        {
            var supplierEntity = supplierServices.GetSupplier(id);
            if (supplierEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierEntity);
            }
            throw new ApiDataException(1001, "No Supplier found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSupplier(SupplierView supplierView)
        {
            var supplierEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<SupplierView, SupplierEntity>()
                    .ConstructUsing((SupplierView sv) =>
                    {
                        var se = new SupplierEntity();
                        se.SupplierId = Guid.NewGuid().ToString();
                        return se;
                    }));

            var supplierEntity = supplierEntityMapper.CreateMapper().Map<SupplierView, SupplierEntity>(supplierView);

            var insertedEntity = supplierServices.CreateSupplier(supplierEntity);
            return GetSupplier(insertedEntity.SupplierId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSupplier(string id, SupplierView supplierView)
        {
            var supplierEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<SupplierView, SupplierEntity>());
            var supplierEntity = supplierEntityMapper.CreateMapper().Map<SupplierView, SupplierEntity>(supplierView);

            return Request.CreateResponse(HttpStatusCode.OK, supplierServices.UpdateSupplier(id, supplierEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteSupplier(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = supplierServices.DeleteSupplier(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Supplier is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}