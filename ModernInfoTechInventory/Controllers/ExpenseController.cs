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
    public class ExpenseController : ApiController
    {
        private readonly IExpenseServices expenseServices;

        public ExpenseController(IExpenseServices expenseServices)
        {
            this.expenseServices = expenseServices;
        }

        public HttpResponseMessage GetAllExpenses()
        {
            var expenseEntities = expenseServices.GetAllExpenses().ToList();
            if (expenseEntities.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, expenseEntities);
            }
            throw new ApiDataException(1000, "Expenses are not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage GetExpense(string id)
        {
            var expenseEntity = expenseServices.GetExpense(id);
            if (expenseEntity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, expenseEntity);
            }
            throw new ApiDataException(1001, "No Expense found for this " + id, HttpStatusCode.NotFound);
        }

        public HttpResponseMessage PostExpense(ExpenseEntity expenseEntity)
        {
            var insertedEntity = expenseServices.CreateExpense(expenseEntity);
            return GetExpense(insertedEntity.ExpenseId);
        }

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
    }
}