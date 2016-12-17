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
    [RoutePrefix("unit")]
    public class UnitController : ApiController
    {
        private readonly IUnitServices unitServices;

        public UnitController(IUnitServices unitServices)
        {
            this.unitServices = unitServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllUnits()
        {
            var unitEntities = unitServices.GetAllUnitEntities();
            if (unitEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, unitEntities);
            }
            throw new ApiDataException(1000, "Units are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetUnit(string id)
        {
            var unitEntity = unitServices.GetUnit(id);
            if (unitEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, unitEntity);
            }
            throw new ApiDataException(1001, "No Unit found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostUnit(UnitView unitView)
        {
            var insertedEntity = unitServices.CreateUnit(
                new UnitEntity
                {
                    UnitId = Guid.NewGuid().ToString(),
                    UnitName = unitView.UnitName
                });
            return GetUnit(insertedEntity.UnitId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage PutUnit(string id, UnitEntity unitEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, unitServices.UpdateUnit(id, unitEntity));
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteUnit(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = unitServices.DeleteUnit(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Unit is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}