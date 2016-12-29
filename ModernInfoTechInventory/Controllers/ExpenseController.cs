using System;
using System.Net;
using System.Linq;
using Model.Tenant;
using Service.Tenant;
using Model.Accounts;
using System.Net.Http;
using System.Web.Http;
using Service.Accounts;
using Microsoft.AspNet.Identity;
using ModernInfoTechInventory.Helpers;
using ModernInfoTechInventory.ErrorHelper;

namespace ModernInfoTechInventory.Controllers
{
    [Authorize]
    [RoutePrefix("expense")]
    public class ExpenseController : ApiController
    {
        private readonly ITenantServices tenantServices;
        private readonly IExpenseServices expenseServices;

        public ExpenseController(IExpenseServices expenseServices, ITenantServices tenantServices)
        {
            this.tenantServices = tenantServices;
            this.expenseServices = expenseServices;
        }

        [Route("")]
        public HttpResponseMessage GetAllExpenses()
        {
            var expenseEntities = expenseServices.GetAllExpenses();
            if (expenseEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, expenseEntities);
            }
            throw new ApiDataException(1000, "Expenses are not found", HttpStatusCode.NotFound);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage GetExpense(string id)
        {
            var expenseEntity = expenseServices.GetExpense(id);
            if (expenseEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, expenseEntity);
            }
            throw new ApiDataException(1001, "No Expense found for this " + id, HttpStatusCode.NotFound);
        }

        [Route("")]
        public HttpResponseMessage PostExpense(ExpenseEntity expenseEntity)
        {
            var tenantEntity = new TenantEntity(RequestContext.Principal.Identity.GetUserId());
            expenseEntity.ExpenseId = Guid.NewGuid().ToString();
            expenseEntity.TenantId = tenantEntity.TenantId;
            expenseEntity.TenantInfo = tenantEntity;
            var insertedEntity = expenseServices.CreateExpense(expenseEntity);
            return GetExpense(insertedEntity.ExpenseId);
        }

        [Route("{id:length(36)}")]
        public HttpResponseMessage DeleteExpense(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var isSuccess = expenseServices.DeleteExpense(id);
                if (isSuccess)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                }
                throw new ApiDataException(1002, "Expense is already deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }

        [Route("deactivate/{id:length(36)}")]
        [HttpDelete]
        public HttpResponseMessage DeactivateExpense(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var expenseEntity = expenseServices.GetExpense(id);
                if (expenseEntity != null)
                {
                    var tenantEntity = tenantServices.GetTenant(expenseEntity.TenantId).Clone<TenantEntity>();
                    tenantEntity.UserId = RequestContext.Principal.Identity.GetUserId();
                    tenantEntity.InactivationDate = DateTime.Now;
                    tenantEntity.Status = false;
                    var isSuccess = tenantServices.UpdateTenant(expenseEntity.TenantId, tenantEntity);
                    if (isSuccess)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Expense is successfully deactivated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Expense has already been deactivated");
                    }
                }
                throw new ApiDataException(1002, "Expense has already been deleted or not exist in system.", HttpStatusCode.NoContent);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request"
            };
        }
    }
}