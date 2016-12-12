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
    [RoutePrefix("supplierpayment")]
    public class SupplierPaymentController : ApiController
    {
        private readonly ISupplierPaymentServices supplierPaymentServices;

        public SupplierPaymentController(ISupplierPaymentServices supplierPaymentServices)
        {
            this.supplierPaymentServices = supplierPaymentServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllSupplierPayments()
        {
            var supplierPaymentEntities = supplierPaymentServices.GetAllSupplierPayments();
            if (supplierPaymentEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntities);
            }
            throw new ApiDataException(1000, "SupplierPayments are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetSupplierPayment(string id)
        {
            var supplierPaymentEntity = supplierPaymentServices.GetSupplierPayment(id);
            if (supplierPaymentEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentEntity);
            }
            throw new ApiDataException(1001, "No SupplierPayment found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostSupplierPayment(SupplierPaymentView supplierPaymentView)
        {
            var supplierPaymentEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<SupplierPaymentView, SupplierPaymentEntity>()
                    .ConstructUsing((SupplierPaymentView spv) =>
                    {
                        var spe = new SupplierPaymentEntity();
                        spe.SupplierPaymentId = Guid.NewGuid().ToString();
                        return spe;
                    }));

            var supplierPaymentEntity = supplierPaymentEntityMapper.CreateMapper().Map<SupplierPaymentView, SupplierPaymentEntity>(supplierPaymentView);

            var insertedEntity = supplierPaymentServices.CreateSupplierPayment(supplierPaymentEntity);
            return GetSupplierPayment(insertedEntity.SupplierPaymentId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutSupplierPayment(string id, SupplierPaymentView supplierPaymentView)
        {
            var supplierPaymentEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<SupplierPaymentView, SupplierPaymentEntity>());
            var supplierPaymentEntity = supplierPaymentEntityMapper.CreateMapper().Map<SupplierPaymentView, SupplierPaymentEntity>(supplierPaymentView);

            return Request.CreateResponse(HttpStatusCode.OK, supplierPaymentServices.UpdateSupplierPayment(id, supplierPaymentEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteSupplierPayment(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = supplierPaymentServices.DeleteSupplierPayment(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "SupplierPayment is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}