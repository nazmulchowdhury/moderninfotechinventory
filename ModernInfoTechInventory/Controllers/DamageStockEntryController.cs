using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service.Product.DamageStockEntry;
using Model.Product;
using ModernInfoTechInventory.ViewModels;
using ModernInfoTechInventory.ErrorHelper;
using ModernInfoTechInventory.ActionFilters;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    public class DamageStockEntryController : ApiController
    {
        private readonly IDamageStockEntryServices damageStockEntryServices;

        public DamageStockEntryController(IDamageStockEntryServices damageStockEntryServices)
        {
            this.damageStockEntryServices = damageStockEntryServices;
        }

        public HttpResponseMessage GetAllDamageStockEntries()
        {
            var damageStockEntryEntities = damageStockEntryServices.GetAllDamageStockEntries().ToList();
            if (damageStockEntryEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, damageStockEntryEntities);
            }
            throw new ApiDataException(1000, "Damage Stock Entries are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetDamageStockEntry(string id)
        {
            var damageStockEntryEntity = damageStockEntryServices.GetDamageStockEntry(id);
            if (damageStockEntryEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, damageStockEntryEntity);
            }
            throw new ApiDataException(1001, "No Damage Stock Entry found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostDamageStockEntry(DamageStockEntryEntity damageStockEntryEntity)
        {
            var insertedEntity = damageStockEntryServices.CreateDamageStockEntry(damageStockEntryEntity);
            return GetDamageStockEntry(insertedEntity.DamageStockEntryId);
        }

        public HttpResponseMessage PutdamageStockEntry(string id, DamageStockEntryEntity damageStockEntryEntity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, damageStockEntryServices.UpdateDamageStockEntry(id, damageStockEntryEntity));
        }

        public HttpResponseMessage DeletedamageStockEntry(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = damageStockEntryServices.DeleteDamageStockEntry(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Damage Stock Entry is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}